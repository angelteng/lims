using System.Web.Mvc;

using Hope.WebBase;
using Hope.Util;
using Hope.ITMS.BLL;
using Hope.ITMS.Model;
using System.Xml;
using System;
using Hope.ITMS.Enums;

namespace Hope.ITMS.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigController : ManageController
    {
        public ActionResult Index()
        {
            return new Config_Index(this);
        }

        public ActionResult Save()
        {
            return new Config_Save(this);
        }
    }

    #region home index ...

    /// <summary>
    /// 系统配置
    /// </summary>
    public class Config_Index : ManageActionResult
    {
        //org.winic.service2.Service1 smsService = new org.winic.service2.Service1();  //短信服务
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public Config_Index( ManageController controller )
            : base(controller)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Executing()
        {
            this.TemplateHelper.Put("Entity", SiteConfigUtil.Instance());
            //GetSMSUserInfo();
        }

        /// <summary>
        /// 读取短信帐户基本信息
        /// </summary>
        //private void GetSMSUserInfo()
        //{
        //    string result = "";    //最后将此输出            
        //    string strLine = smsService.GetUserInfo(SiteConfigUtil.Instance().SMSUserName, EncryptionUtil.DecrypePassword(SiteConfigUtil.Instance().SMSUserPwd));

        //    #region 解析此查询结果
        //    if (string.IsNullOrEmpty(strLine))
        //    {
        //        result = "短信服务提供商连接失败";
        //    }
        //    else
        //    {
        //        string[] strArray = strLine.Split('/');
        //        string userStatus = strArray[0];
        //        int iStatus = ConvertHelper.ToInt(userStatus);

        //        if (Enum.IsDefined(typeof(SMSUserStatus), iStatus))
        //        {
        //            result = Enum.GetName(typeof(SMSUserStatus), iStatus);
        //        }
        //        else
        //        {
        //            result = "状态:正常";
        //            result += " 用户名:" + strArray[1];
        //            result += " 余额:" + strArray[2];
        //            result += " 短信单价:" + strArray[3];
        //            result += " 最大字数:" + strArray[4];
        //        }
        //    }
        //    #endregion...

        //    this.TemplateHelper.Put("SMSAcountInfo", result);
        //}

        #region override ...

        /// <summary>
        /// 页面模式
        /// </summary>
        protected override PageMode CurrentPageMode
        {
            get
            {
                return PageMode.Html;
            }
        }

        /// <summary>
        /// 模板路径
        /// </summary>
        protected override string TemplatePath
        {
            get
            {
                return "Manage\\Config\\index.html";
            }
        }

        #endregion
    }

    #endregion

    #region Config Save ...

    /// <summary>
    /// 保存配置信息
    /// </summary>
    public class Config_Save : ManageActionResult
    {
        //private org.winic.service2.Service1 smsService = new org.winic.service2.Service1();  //短信服务
        private string oldSMSPwd = string.Empty;  //旧SMS密码
        private string newSMSPwd = string.Empty;  //新SMS密码
        private string oldMailPwd = string.Empty; //旧邮件密码
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public Config_Save( ManageController controller )
            : base(controller)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Executing()
        {
            oldSMSPwd = SiteConfigUtil.Instance().SMSUserPwd;
            oldMailPwd = SiteConfigUtil.Instance().MailPasswd;

            if (!CheckInput())
            {
                HandlerMessage.TargetUrl = "window.history.back();";
                this.TemplateHelper.Put("Message", HandlerMessage);
                return;
            }
            ProcessSave();
        }

        /// <summary>
        /// 处理保存
        /// </summary>
        /// <param name="data"></param>
        private void ProcessSave()
        {
            //基本信息
            SiteConfigUtil.Instance().SiteName = RequestUtil.RequestString(Request, "SiteName", "jjf001");
            SiteConfigUtil.Instance().SiteUrl = RequestUtil.RequestString(Request, "SiteUrl", string.Empty);
            SiteConfigUtil.Instance().SiteLogo = RequestUtil.RequestString(Request, "SiteLogo", string.Empty);
            SiteConfigUtil.Instance().SiteICPNum = RequestUtil.RequestString(Request, "SiteICPNum", string.Empty);
            SiteConfigUtil.Instance().SiteCopyright = RequestUtil.RequestString(Request, "SiteCopyright", string.Empty);

            //上传选项
            SiteConfigUtil.Instance().EnbleFileUpload = RequestUtil.RequestBoolean(Request, "EnbleFileUpload", true);
            SiteConfigUtil.Instance().FileUploadMaxSize = RequestUtil.RequestLong(Request, "FileUploadMaxSize", 1024);
            SiteConfigUtil.Instance().FileUploadDir = RequestUtil.RequestString(Request, "FileUploadDir", string.Empty);
            SiteConfigUtil.Instance().FileUploadType = RequestUtil.RequestString(Request, "FileUploadType", string.Empty);
            SiteConfigUtil.Instance().UploadFilePath = RequestUtil.RequestString(Request, "UploadFilePath", string.Empty);
            SiteConfigUtil.Instance().UploadFileName = RequestUtil.RequestString(Request, "UploadFileName", string.Empty);
            SiteConfigUtil.Instance().FileDeleteDelay = RequestUtil.RequestInt(Request, "FileDeleteDelay", 0);

            //短信配置
            SiteConfigUtil.Instance().EnableSMS = RequestUtil.RequestBoolean(Request, "EnableSMS", false);
            SiteConfigUtil.Instance().SMSUserName = RequestUtil.RequestString(Request, "SMSUserName", string.Empty);
            string smsPwd = RequestUtil.RequestString(Request, "SMSUserPwd", string.Empty);
            if (!string.IsNullOrEmpty(smsPwd) && smsPwd != oldSMSPwd)
            {
                SiteConfigUtil.Instance().SMSUserPwd = EncryptionUtil.EncryptPassword(smsPwd);
                newSMSPwd = smsPwd;
            }
            SiteConfigUtil.Instance().SMSDelay = RequestUtil.RequestInt(Request, "SMSDelay", 0);
            SiteConfigUtil.Instance().SMSContent = RequestUtil.RequestString(Request, "SMSContent", string.Empty);

            //邮件配置
            SiteConfigUtil.Instance().EnableMail = RequestUtil.RequestBoolean(Request, "EnableMail", true);
            SiteConfigUtil.Instance().MailAccount = RequestUtil.RequestString(Request, "MailAccount", string.Empty);
            string mailPwd = RequestUtil.RequestString(Request, "MailPasswd", string.Empty);
            if (!string.IsNullOrEmpty(smsPwd) && mailPwd != oldMailPwd)
            {
                SiteConfigUtil.Instance().MailPasswd = EncryptionUtil.EncryptPassword(mailPwd);
            }
            SiteConfigUtil.Instance().DisplayName = RequestUtil.RequestString(Request, "DisplayName", string.Empty);
            SiteConfigUtil.Instance().SmtpHost = RequestUtil.RequestString(Request, "SmtpHost", string.Empty);
            SiteConfigUtil.Instance().Port = RequestUtil.RequestInt(Request, "Port", 25);
            SiteConfigUtil.Instance().RegisterMsg = RequestUtil.RequestString(Request, "RegisterMsg", string.Empty);

            //保存
            if (SiteConfigUtil.Instance().SaveConfigData())
            {
                if (!string.IsNullOrEmpty(newSMSPwd) && smsPwd != oldSMSPwd)
                {
                    //smsService.EditUserPwd(SiteConfigUtil.Instance().SMSUserName, oldSMSPwd, newSMSPwd);
                }

                HandlerMessage.Code = "01";
                HandlerMessage.Text = "系统配置成功!";
                HandlerMessage.Succeed = true;
            }
            else
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "操作失败，请检查文件读写权限!";
                HandlerMessage.Succeed = false;
            }

            this.TemplateHelper.Put("Message", HandlerMessage);
        }

        /// <summary>
        /// 验证输入数据
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            return true;
        }

        #region override ...

        /// <summary>
        /// 页面模式
        /// </summary>
        protected override PageMode CurrentPageMode
        {
            get
            {
                return PageMode.Html;
            }
        }

        /// <summary>
        /// 模板路径
        /// </summary>
        protected override string TemplatePath
        {
            get
            {
                return "Message.html";
            }
        }

        #endregion

    }

    #endregion
}
