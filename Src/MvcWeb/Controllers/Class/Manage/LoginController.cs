using System;
using System.Drawing;
using System.Web;
using System.Web.Mvc;

using Hope.WebBase;
using Hope.ITMS.Model;
using Hope.ITMS.Enums;
using Hope.Util;
using System.Collections;
using Hope.ITMS.BLL;
using System.Collections.Generic;

namespace Hope.ITMS.Web
{
    public class LoginController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return Login();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return new Login_Index(this);
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Jump()
        {
            return new Login_Jump(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Check()
        {
            return new Login_Check(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            return new Login_Logout(this);
        }

        public ActionResult ValidateCode()
        {
            return new Login_ValidateCode(this);
        }

    }


    #region login ...

    /// <summary>
    /// 
    /// </summary>
    public class Login_Index : BaseActionResult
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public Login_Index(BaseController controller)
            : base(controller) { }

        /// <summary>
        /// 
        /// </summary>
        protected override void Executing()
        {
            string backUrl = ConvertHelper.ToString(Request["BackUrl"]);
            this.TemplateHelper.Put("BackUrl", backUrl);
            this.TemplateHelper.Put("ValidateMode", AppConfig.ValidateMode);
        }

        #region override ...

        /// <summary>
        /// 当前页面类型
        /// </summary>
        protected override PageMode CurrentPageMode
        {
            get
            {
                return PageMode.Html;
            }
        }

        /// <summary>
        /// 模板文件路径
        /// </summary>
        protected override string TemplatePath
        {
            get
            {
                return "Manage\\Login\\login.html";
            }
        }

        #endregion

    }

    #endregion

    #region loing jump ...

    /// <summary>
    /// 
    /// </summary>
    public class Login_Jump : BaseActionResult
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public Login_Jump(BaseController controller)
            : base(controller) { }

        /// <summary>
        /// 
        /// </summary>
        protected override void Executing()
        {
            SysAdmin admin = (SysAdmin)Session["_AdminInfo"];
            if (admin==null)
            {
                Response.Redirect(ApplicationUrl + "Login/");
            }

            string targetUrl = ConvertHelper.ToString(Request["BackUrl"]);
            if (targetUrl == string.Empty)
                targetUrl = ApplicationUrl + "Manage/";
            //Response.Redirect(targetUrl);
            this.TemplateHelper.Put("TargetUrl", targetUrl);
        }

        #region override ...

        /// <summary>
        /// 当前页面类型
        /// </summary>
        protected override PageMode CurrentPageMode
        {
            get
            {
                return PageMode.Html;
            }
        }

        /// <summary>
        /// 模板文件路径
        /// </summary>
        protected override string TemplatePath
        {
            get
            {
                return "Manage\\Login\\Jump.html";
            }
        }

        #endregion

    }

    #endregion

    #region check login ...

    /// <summary>
    /// 
    /// </summary>
    public class Login_Check : BaseActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public Login_Check(BaseController controller)
            : base(controller) { }

        /// <summary>
        /// 
        /// </summary>
        protected override void Executing()
        {
            ProcessLogin();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ProcessLogin()
        {
            if (!CheckInput())
            {
                return;
            }
            SysAdmin data = null;
            string account = ConvertHelper.ToString(Request["Account"]);
            string password = ConvertHelper.ToString(Request["Password"]);

            HandlerMessage = BLLFactory.Instance().SysAdminBLL.Login(account, password, out data);
            if (!HandlerMessage.Succeed)
            {
                return;
            }

            #region 登录成功 
          
            SetSession(data);
            
            BuildModuleStructure();

            #endregion
        }

        #region 保存Session
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="data"></param>
        private void SetSession(SysAdmin data)
        {
            Session["_AdminInfo"] = data;

            //是否超级管理员
            bool bSuperAdmin = BLLFactory.Instance().SysRoleBLL.IsSuperRole(data.RoleID);
            Session["_SuperRole"] = bSuperAdmin;
            
        }
        #endregion

        #region 初始化权限 Hash Table
        /// <summary>
        /// Build Admin Module Structure
        /// </summary>
        private void BuildModuleStructure()
        {
            Hashtable moduleRoleTable = new Hashtable();   // 存储角色权限模块信息

            SysAdmin adminData = (SysAdmin)Session["_AdminInfo"]; //取得登录的管理员信息

            //根据角色ID，取得权限列表，权限列表包括模块ID，和模块下的功能数值
            List<SysPermission> permissionDatas = BLLFactory.Instance().SysPermissionBLL.GetList(adminData.RoleID);

            if (permissionDatas.Count > 0)
            {
                foreach (SysPermission permissionData in permissionDatas)  //循环所得到的权限信息
                {
                    if (permissionData.FunctionValues > 0) //判断模块下是否有功能
                    {
                        //把有权限的模块，和模块下的功能权限数值存入Hashtable
                        if (moduleRoleTable.Contains(permissionData.ModuleID))
                        {
                            int fv = (int)moduleRoleTable[permissionData.ModuleID];
                            moduleRoleTable[permissionData.ModuleID] = fv | permissionData.FunctionValues;
                        }
                        else
                        {
                            moduleRoleTable.Add(permissionData.ModuleID, permissionData.FunctionValues);
                        }
                    }
                }
            }
            ModulePermissionTable = moduleRoleTable;  //保存全局的模块--权限数值列表 
        }
        #endregion

        #region 输入判断
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            if (Request["Account"] == null || Request["Account"].Trim() == string.Empty)
            {
                HandlerMessage.Code = "system_admin_004";
                HandlerMessage.Text = "登录帐号不能为空";
                HandlerMessage.Succeed = false;
                return false;
            }
            else if (Request["Password"] == null || Request["Password"].Trim() == string.Empty)
            {
                HandlerMessage.Code = "system_admin_005";
                HandlerMessage.Text = "登录密码不能为空";
                HandlerMessage.Succeed = false;
                return false;
            }
            if (AppConfig.ValidateMode == ValidationMode.Off)
            {
                return true;
            }

            if (Request["ValidateCode"] == null || Request["ValidateCode"].Trim() == string.Empty)
            {
                HandlerMessage.Code = "system_admin_006";
                HandlerMessage.Text = "验证码不能为空";
                HandlerMessage.Succeed = false;
                return false;
            }
            else if (Session["ValidateCode"] == null)
            {
                HandlerMessage.Code = "system_admin_006";
                HandlerMessage.Text = "验证码不能为空";
                HandlerMessage.Succeed = false;
                return false;
            }
            string validCode = ConvertHelper.ToString(Request["ValidateCode"]).ToLower();
            string strSystemCode = Session["ValidateCode"].ToString().ToLower();
            if (!strSystemCode.Equals(validCode))
            {
                HandlerMessage.Code = "system_admin_007";
                HandlerMessage.Text = "验证码不正确";
                HandlerMessage.Succeed = false;
                return false;
            }
            return true;
        }
        #endregion

        #region override ...

        protected override string TemplatePath
        {
            get
            {
                return "Manage\\Login\\Check.html";
            }
        }

        /// <summary>
        /// 当前页面类型
        /// </summary>
        protected override PageMode CurrentPageMode
        {
            get
            {
                return PageMode.Text;
            }
        }

        #endregion
    }

    #endregion

    #region logout ...

    /// <summary>
    /// 
    /// </summary>
    public class Login_Logout : BaseActionResult
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public Login_Logout(BaseController controller)
            : base(controller) { }

        /// <summary>
        /// 
        /// </summary>
        protected override void Executing()
        {
            Session["_AdminInfo"] = null;
            Session.Clear();
        }

        #region override ...

        /// <summary>
        /// 当前页面类型
        /// </summary>
        protected override PageMode CurrentPageMode
        {
            get
            {
                return PageMode.Html;
            }
        }

        /// <summary>
        /// 模板文件路径
        /// </summary>
        protected override string TemplatePath
        {
            get
            {
                return "Manage\\Login\\Logout.html";
            }
        }

        #endregion

    }

    #endregion

    #region generate validate code ...

    /// <summary>
    /// 登录验证码
    /// </summary>
    public class Login_ValidateCode : BaseActionResult
    {
        private int validlength = AppConfig.ValidateLength;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public Login_ValidateCode(BaseController controller)
            : base(controller) { }

        /// <summary>
        /// 
        /// </summary>
        protected override void Executing()
        {
            string validatorCode = GenerateRandomCode();
            Session["ValidateCode"] = validatorCode;
            GendrateImage(validatorCode);
            Response.End();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 生成随机码
        /// </summary>
        /// <returns></returns>
        private string GenerateRandomCode()
        {
           
            switch (AppConfig.ValidateMode)
            {
                case ValidationMode.Numeric:
                    return GenerateNumeric();
                case ValidationMode.Character:
                    return GenerateCharacter();
                case ValidationMode.Mix:
                    return GenerateMix();
                case ValidationMode.ChineseCharacter:
                    return GenerateChineseCharacter();
                default:
                    break;
            }
            return "";
        }

        /// <summary>
        /// 生成数字随机码
        /// </summary>
        /// <returns></returns>
        private string GenerateNumeric()
        {
            Random r = new Random(unchecked((int)DateTime.Now.Ticks));
            double minNum = Math.Pow(10.00, ConvertHelper.ToDouble(validlength - 1));
            double maxNum = Math.Pow(10.00, ConvertHelper.ToDouble(validlength));

            return r.Next(ConvertHelper.ToInt(minNum), ConvertHelper.ToInt(maxNum)-1).ToString();
        }

        /// <summary>
        /// 生成英文字母
        /// </summary>
        /// <returns></returns>
        private string GenerateCharacter()
        {
            char[] s = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string str = "";
            Random r = new Random();
            for (int i = 0; i < validlength; i++)
            {
                str += s[r.Next(0, s.Length)].ToString();
            }
            return str;
        }

        /// <summary>
        /// 生成英文字母跟数字混合的随机码
        /// </summary>
        /// <param name="strlength">需要生成的字符长度</param>
        /// <returns>生成英文字母（区分大小写）跟数字混合的字符串</returns>
        public string GenerateMix()
        {
            char[] s = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string num = "";
            Random r = new Random();
            for (int i = 0; i < validlength; i++)
            {
                num += s[r.Next(0, s.Length)].ToString();
            }
            return num;
        }

        /// <summary>
        /// 生成中文的随机码
        /// </summary>
        /// <returns>返回生成的中文字符串</returns>
        public string GenerateChineseCharacter()
        {
            string str = "";
            System.Text.Encoding gb = System.Text.Encoding.GetEncoding("gb2312");

            //调用函数产生指定个数的随机中文汉字编码 
            object[] bytes = GenerateRegionCode(validlength);


            //根据汉字编码的字节数组解码出中文汉字 
            for (int i = 0; i < validlength; i++)
            {
                str += gb.GetString((byte[])Convert.ChangeType(bytes[i], typeof(byte[])));
            }

            return str;
        }


        /// <summary>
        /// 生成中文汉字编码
        /// </summary>
        /// <param name="strlength">需要产生的汉字长度</param>
        /// <returns>返回汉子编码</returns>
        /// <remarks>
        /// 此函数在汉字编码范围内随机创建含两个元素的十六进制字节数组，每个字节数组代表一个汉字，
        /// 并将四个字节数组存储在object数组中
        /// 
        /// 因为第15区也就是AF区以前都没有汉字，只有少量符号，汉字都从第16区B0开始，
        /// 并且从区位D7开始以后的汉字都是和很难见到的繁杂汉字，所以这些都要排出掉。
        /// 所以随机生成的汉字十六进制区位码第1位范围在B、C、D之间，如果第1位是D的话，
        /// 第2位区位码就不能是7以后的十六进制数。在来看看区位码表发现每区的第一个位置和最后一个位置都是空的，
        /// 没有汉字，因此随机生成的区位码第3位如果是A的话，第4位就不能是0；第3位如果是F的话，第4位就不能是F。
        /// </remarks>
        private static object[] GenerateRegionCode(int strlength)
        {
            //定义一个字符串数组储存汉字编码的组成元素 
            string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

            Random rnd = new Random();

            //定义一个object数组用来 
            object[] bytes = new object[strlength];

            /*每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bject数组中 
             每个汉字有四个区位码组成 
             区位码第1位和区位码第2位作为字节数组第一个元素 
             区位码第3位和区位码第4位作为字节数组第二个元素 
            */
            for (int i = 0; i < strlength; i++)
            {
                //区位码第1位 
                int r1 = rnd.Next(11, 14);
                string str_r1 = rBase[r1].Trim();

                //区位码第2位 
                rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);  //更换随机数发生器的种子避免产生重复值 

                int r2;
                if (r1 == 13)
                {
                    r2 = rnd.Next(0, 7);
                }
                else
                {
                    r2 = rnd.Next(0, 16);
                }
                string str_r2 = rBase[r2].Trim();

                //区位码第3位 
                rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
                int r3 = rnd.Next(10, 16);
                string str_r3 = rBase[r3].Trim();

                //区位码第4位 
                rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                int r4;
                if (r3 == 10)
                {
                    r4 = rnd.Next(1, 16);
                }
                else if (r3 == 15)
                {
                    r4 = rnd.Next(0, 15);
                }
                else
                {
                    r4 = rnd.Next(0, 16);
                }
                string str_r4 = rBase[r4].Trim();

                //定义两个字节变量存储产生的随机汉字区位码 
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
                //将两个字节变量存储在字节数组中 
                byte[] str_r = new byte[] { byte1, byte2 };

                //将产生的一个汉字的字节数组放入object数组中 
                bytes.SetValue(str_r, i);

            }

            return bytes;
        }


        /// <summary>
        /// 生成图片
        /// </summary>
        /// <param name="validatorCode"></param>
        private void GendrateImage(string validatorCode)
        {
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(Convert.ToInt32(Math.Ceiling((decimal)(validatorCode.Length * 15))), 25);
            if (AppConfig.ValidateMode == ValidationMode.ChineseCharacter)
            {
                image = new System.Drawing.Bitmap(Convert.ToInt32(Math.Ceiling((decimal)(validatorCode.Length * 22))), 25);
            }
            Graphics g = Graphics.FromImage(image);
            try
            {

                Random random = new Random();
                g.Clear(Color.AliceBlue);

                for (int i = 0; i < 20; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Transparent), x1, y1, x2, y2);
                }

                Font font = new System.Drawing.Font("", 12, System.Drawing.FontStyle.Bold);
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Bisque, Color.Black, 0f, true);
                g.DrawString(validatorCode, font, new SolidBrush(Color.Sienna), 3, 2);

                //画背景混淆点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画矩形
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());

            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        #region override ...

        /// <summary>
        /// 当前页面类型
        /// </summary>
        protected override PageMode CurrentPageMode
        {
            get
            {
                return PageMode.Image;
            }
        }

        #endregion

    }

    #endregion
}