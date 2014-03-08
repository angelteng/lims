using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Web;

namespace Hope.Util
{
    /// <summary>
    /// Cookie操作类
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 得到CookieContainer对象
        /// </summary>
        /// <param name="cookieStr">HTTP Cookie字符串</param>
        /// <param name="uriString">CookieCollection的URI</param>
        /// <returns></returns>
        public static CookieContainer GetCookieContainer(string cookieStr, string uriString)
        {
            // 声明CookieContainer对象
            CookieContainer cookieContainer = new CookieContainer();

            List<string> cookieStrs = StringHelper.SplitToString(cookieStr, ';');

            if (cookieStrs.Count > 0)
            {
                foreach (string str in cookieStrs)
                {
                    cookieContainer.SetCookies(new Uri(uriString), str);
                }
            }

            return cookieContainer;
        }

        #region 为Cookie赋值方法
        /**/
        /// <summary>
        /// 为Cookie赋值
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <param name="Nvc">Cookie值集合</param>
        /// <param name="days">Cookie日期  0 为无时间限制。浏览器关闭就结束</param>
        /// <param name="Domain">Cookie站点</param>
        /// <returns>返回布尔值</returns>
        public static bool WriteCookie(string CookieName, NameValueCollection Nvc, int days, string Domain)
        {
            bool BoolReturnValue = false;
            if (Nvc != null && !string.IsNullOrEmpty(CookieName)) //判断是否能建Cookie
            {
                HttpCookie httpCookie = new HttpCookie(CookieName);
                for (int i = 0; i < Nvc.Count; i++)
                {
                    httpCookie.Values.Add(Nvc.GetKey(i), HttpUtility.UrlEncode(Nvc.Get(i))); //将值添入Cookie中
                }
                if (days > 0)
                {
                    httpCookie.Expires = DateTime.Now.AddDays(days);  //设置Cookie有效期
                }

                if (!string.IsNullOrEmpty(Domain)) //判断Cookie站点有效
                {
                    httpCookie.Domain = Domain; //设置Cookie站点
                }
                HttpContext.Current.Response.AppendCookie(httpCookie); //添加Cookie
                BoolReturnValue = true;
            }
            return BoolReturnValue;
        }
        /// <summary>
        /// 不限制日期的写cookies
        /// </summary>
        /// <param name="CookieName">cookie名称</param>
        /// <param name="Nvc">集合</param>
        /// <returns></returns>
        public static bool WriteCookieNoDay(string CookieName, NameValueCollection Nvc)
        {
            bool BoolReturnValue = false;
            if (Nvc != null && !string.IsNullOrEmpty(CookieName)) //判断是否能建Cookie
            {
                HttpCookie httpCookie = new HttpCookie(CookieName);
                for (int i = 0; i < Nvc.Count; i++)
                {
                    httpCookie.Values.Add(Nvc.GetKey(i), Nvc.Get(i)); //将值添入Cookie中
                }
                HttpContext.Current.Response.AppendCookie(httpCookie); //添加Cookie
                BoolReturnValue = true;
            }
            return BoolReturnValue;
        }
        /**/
        /// <summary>
        /// 为Cookie赋值
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <param name="Nvc">Cookie值集合</param>
        /// <param name="days">Cookie日期</param>
        /// <returns>返回布尔值</returns>
        public static bool WriteCookie(string CookieName, NameValueCollection Nvc, int days)
        {
            return WriteCookie(CookieName, Nvc, days, null);
        }
        /**/
        /// <summary>
        /// 为Cookie赋值
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <param name="Nvc">Cookie值集合</param>
        /// <returns>返回布尔值</returns>
        public static bool WriteCookie(string CookieName, NameValueCollection Nvc)
        {
            return WriteCookie(CookieName, Nvc, 1, null);
        }
        #endregion

        #region 添加Cookie值
        /// <summary>
        /// 添加Cookie值
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <param name="Nvc">Cookie值集合</param>
        /// <returns></returns>
        public static bool AddCookie(string CookieName, NameValueCollection Nvc)
        {
            bool BoolReturnValue = false;
            if (Nvc != null && !string.IsNullOrEmpty(CookieName)) //判断是否能建Cookie
            {
                for (int i = 0; i < Nvc.Count; i++)
                {
                    HttpContext.Current.Request.Cookies[CookieName].Values.Add(Nvc.GetKey(i), Nvc.Get(i));//添加Cookie;
                    HttpContext.Current.Response.Cookies[CookieName].Values.Add(Nvc.GetKey(i), Nvc.Get(i));//添加Cookie;
                }
            }
            return BoolReturnValue;
        }
        #endregion

        #region 更新Cookie
        /**/
        /// <summary>
        /// 更新Cookie
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <param name="Nvc">Cookie值集合</param>
        /// <returns>bool</returns>
        public static bool UpdateCookie(string CookieName, NameValueCollection Nvc)
        {
            bool BoolReturnValue = false;
            if (Nvc != null && !string.IsNullOrEmpty(CookieName)) //判断是否能建Cookie
            {
                HttpCookie httpCookie = new HttpCookie(CookieName);
                NameValueCollection NonceNvc = HttpContext.Current.Request.Cookies[CookieName].Values; //得到已有的Cookie值集合
                if (NonceNvc != null) //判断Cookie值集合是否为空
                {
                    string CookieValue = string.Empty;
                    //------------------------循环判断Cookie值是否存在于新Cookie中，如果存在就予以更新-----------------------
                    for (int i = 0; i < NonceNvc.Count; i++)
                    {
                        CookieValue = NonceNvc.Get(i);
                        for (int y = 0; y < Nvc.Count; y++)
                        {
                            if (NonceNvc.GetKey(i) == Nvc.GetKey(y))
                            {
                                if (CookieValue != Nvc.Get(y))
                                {
                                    CookieValue = Nvc.Get(y);
                                }
                                break;
                            }
                        }
                        httpCookie.Values.Add(NonceNvc.GetKey(i), CookieValue); //不存在于原Cookie的值 ，被新加入
                        CookieValue = string.Empty;
                    }
                    //---------------------------------------------------------------------------------------------------------
                    HttpContext.Current.Response.AppendCookie(httpCookie); //加入Cookie
                    BoolReturnValue = true;
                }
            }
            return BoolReturnValue;
        }
        #endregion

        #region 得到Cookie值集合
        /**/
        /// <summary>
        /// 得到Cookie值集合
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <returns>返回NameValueCollection集合</returns>
        public static NameValueCollection GetCookie(string CookieName)
        {
            NameValueCollection Nvc = new NameValueCollection();
            if (!string.IsNullOrEmpty(CookieName) && HttpContext.Current.Request.Cookies[CookieName] != null) //判断Cookie是否存在
            {
                Nvc = HttpContext.Current.Request.Cookies[CookieName].Values; //得到Cookie值集合
            }
            return Nvc;
        }
        #endregion

        #region 删除Cookie
        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="CookieName">cookie的值</param>
        /// <param name="CookieDomain">cookie的域</param>
        /// <returns></returns>
        public static bool DeleteCookie(string CookieName, string CookieDomain)
        {
            bool BoolReturnValue = false;
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookieName];
            if (httpCookie != null)  //如果Cookie存在
            {
                if (!string.IsNullOrEmpty(CookieDomain))
                {
                    httpCookie.Domain = CookieDomain;
                }
                httpCookie.Expires = DateTime.Now.AddDays(-30); //来他个负30天，看他怎么存在
                HttpContext.Current.Response.Cookies.Add(httpCookie);
                BoolReturnValue = true;
            }
            return BoolReturnValue;
        }
        /**/
        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <returns>布尔值</returns>
        public static bool DeleteCookie(string CookieName)
        {
            return DeleteCookie(CookieName, null);
        }
        #endregion

        #region 得到单独一条Cookie的值
        /**/
        /// <summary>
        /// 得到单独一条Cookie的值
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <param name="KeyName">Key名称</param>
        /// <returns>返回string</returns>
        public static string GetSingleValue(string CookieName, string KeyName)
        {
            string StrReturnValue = string.Empty;
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookieName];
            if (httpCookie != null)  //如果Cookie存在
            {
                StrReturnValue = HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[CookieName].Values[KeyName]); //得到指定Key的Value
            }
            return StrReturnValue;
        }
        /**/
        /// <summary>
        /// 得到服务器端单独一条Cookie的值
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <param name="KeyName">Key名称</param>
        /// <returns>返回string</returns>
        public static string GetSingleValueFromServer(string CookieName, string KeyName)
        {
            string StrReturnValue = string.Empty;
            HttpCookie httpCookie = HttpContext.Current.Response.Cookies[CookieName];
            if (httpCookie != null)  //如果Cookie存在
            {
                StrReturnValue = HttpContext.Current.Response.Cookies[CookieName].Values[KeyName]; //得到指定Key的Value
            }
            return StrReturnValue;
        }

        #endregion

        #region 更新单独一条Cookie的值
        /**/
        /// <summary>
        /// 更新单独一条Cookie的值
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <param name="KeyName">Key名称</param>
        /// <param name="Value">值</param>
        /// <returns>返回布尔值</returns>
        public static bool UpdateSingleValue(string CookieName, string KeyName, string Value)
        {
            bool BoolReturnValue = false;
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add(KeyName, Value);
            if (!string.IsNullOrEmpty(CookieName)) //判断是否能建Cookie
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookieName];
                if (httpCookie != null)
                {
                    if (HttpContext.Current.Request.Cookies[CookieName].Values[KeyName] != null)
                    {
                        BoolReturnValue = UpdateCookie(CookieName, nvc);
                    }
                    else
                    {
                        BoolReturnValue = AddCookie(CookieName, nvc);
                    }
                }
                else
                {
                    BoolReturnValue = WriteCookie(CookieName, nvc);
                }
            }
            return BoolReturnValue;
        }
        #endregion

        #region 添加单独的一条Cookie值
        /// <summary>
        /// 添加单独的一条Cookie值
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <param name="KeyName">Key名称</param>
        /// <param name="Value">值</param>
        /// <returns></returns>
        public static bool AddSingleCookie(string CookieName, string KeyName, string Value)
        {
            bool BoolReturnValue = false;
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add(KeyName, Value);
            if (!string.IsNullOrEmpty(CookieName)) //判断是否能建Cookie
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies[CookieName];
                if (httpCookie != null)
                {
                    if (HttpContext.Current.Request.Cookies[CookieName].Values[KeyName] != null)
                    {
                        BoolReturnValue = UpdateCookie(CookieName, nvc);
                    }
                    else
                    {
                        BoolReturnValue = AddCookie(CookieName, nvc);
                    }
                }
                else
                {
                    BoolReturnValue = WriteCookie(CookieName, nvc);
                }
            }
            return BoolReturnValue;
        }
        #endregion

        #region 判断是否存在Cookie表
        /// <summary>
        /// 判断是否存在Cookie表
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <returns></returns>
        public static bool HasCookie(string CookieName)
        {
            bool BoolReturnValue = false;
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
                BoolReturnValue = true;
            return BoolReturnValue;
        }
        #endregion

        #region Cookie Helper 2

        /***********************************************************************************************************************************
         * 
         * Cookie Helper 2
         * 
         * ********************************************************************************************************************************/

        /***********************************************************************************************************************************
         * 
         * 示例：
                Cookie Cookie = new Cookie();
                Cookie.setCookie("name", "aaa",1);//赋值
                Cookie.getCookie("name");//取值
                Cookie.delCookie("name");//删除
                注意:当Cookie存中文出现乱码,则在存放时给中文编码,如Cookie.setCookie("name", Server.UrlEncode("aaa"),1),读取时解码即可


                另外：只要不给cookie设置过期时间,cookie在浏览器关闭的时候自动失效,同时支持用户用登陆

                如设置失效时间只支持单用户登陆
         * 
         * ********************************************************************************************************************************/

        /// <summary>
        /// Cookies赋值
        /// </summary>
        /// <param name="strName">主键</param>
        /// <param name="strValue">键值</param>
        /// <param name="strDay">有效天数</param>
        /// <returns></returns>
        public bool SetCookie(string strName, string strValue, int strDay)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                //Cookie.Domain = ".xxx.com";//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
                Cookie.Expires = DateTime.Now.AddDays(strDay);
                Cookie.Value = strValue;
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>

        public string getCookie(string strName)
        {
            HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
            if (Cookie != null)
            {
                return Cookie.Value.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>
        public bool delCookie(string strName)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                //Cookie.Domain = ".xxx.com";//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
                Cookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Cookie Helper 3

        /***********************************************************************************************************************************
         * 
         * Cookie Helper 3
         * 
         * ********************************************************************************************************************************/

        /***********************************************************************************************************************************
         * 
         private void Page_Load(object sender, System.EventArgs e)
         {
              // 在这里放置使用者程序代码以初始化网页     
              this.txt_UserID.Text=this.GetCookieValue("UserName","UserID");//取得用户名
         }
 
         private void btn_Submit_Click(object sender, System.EventArgs e)
         {    
            //用Cookie进行保存登入用户名
              if(this.chb_IsSave.Checked)
              {
                   //将用户保存一个小时，具体设置可以进行调整。。
                   //这里用了固定的公用的cookie用户UserName,用户编号UserID进行访问
                   CreateCookieValue("UserName","UserName","UserID",this.txt_UserID.Text,DateTime.Now+new TimeSpan(0,1,0,0));//设置保存用户名
              }
            }
         * 
         ************************************************************************************************************************************/

        #region 关于操作Cookie的方法

        ///<summary>
        ///创建cookie值
        ///</summary>
        ///<param name="cookieName">cookie名称</param>
        ///<param name="cookieValue">cookie值</param>
        ///<param name="cookieTime">cookie有效时间</param>
        private void CreateCookieValue(string cookieName, string cookieValue, DateTime cookieTime)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = cookieValue;
            //DateTime dtNow = DateTime.Now ;
            //TimeSpan tsMinute = cookieTime;
            cookie.Expires = cookieTime;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }
        ///<summary>
        ///创建cookie值
        ///</summary>
        ///<param name="cookieName">cookie名称</param>   
        ///<param name="cookieValue">cookie值</param>
        ///<param name="subCookieName">子信息cookie名称</param>
        ///<param name="subCookieValue">子信息cookie值</param>
        ///<param name="cookieTime">cookie有效时间</param>
        private void CreateCookieValue(string cookieName, string cookieValue, string subCookieName, string subCookieValue, DateTime cookieTime)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = cookieValue;
            cookie[subCookieName] = subCookieValue;
            cookie.Expires = cookieTime;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }
        ///<summary>
        ///取得cookie的值
        ///</summary>
        ///<param name="cookieName">cookie名称</param>
        ///<returns></returns>
        private string GetCookieValue(string cookieName)
        {
            string cookieValue = "";
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[cookieName];
            if (null == cookie)
            {
                cookieValue = "";
            }
            else
            {
                cookieValue = cookie.Value;
            }
            return cookieValue;
        }
        ///<summary>
        ///取得cookie的值
        ///</summary>
        ///<param name="cookieName">cookie名称</param>
        ///<param name="subCookieName">cookie子信息值</param>
        ///<returns></returns>
        private string GetCookieValue(string cookieName, string subCookieName)
        {
            string cookieValue = "";
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[cookieName];
            if (null == cookie)
            {
                cookieValue = "";
            }
            else
            {
                cookieValue = cookie.Value;
                cookieValue = cookieValue.Split('&')[1].ToString().Split('=')[1];
            }
            return cookieValue;
        }
        ///<summary>
        ///删除某个固定的cookie值[此方法一是在原有的cookie上再创建同样的cookie值，但是时间是过期的时间]
        ///</summary>
        ///<param name="cookieName"></param>
        private void RemoteCookieValue(string cookieName)
        {
            string dt = "1900-01-01 12:00:00";
            CreateCookieValue(cookieName, "", Convert.ToDateTime(dt));
        }

        #endregion

        #endregion

        #region Cookie Helper 4

        /// 创建COOKIE对象并赋Value值
        /// <summary>
        /// 创建COOKIE对象并赋Value值，修改COOKIE的Value值也用此方法，因为对COOKIE修改必须重新设Expires
        /// </summary>
        /// <param name="strCookieName">COOKIE对象名</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>
        /// <param name="strValue">COOKIE对象Value值</param>
        //public static void SetObj(string strCookieName, int iExpires, string strValue)
        //{
        //    HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
        //    objCookie.Value = Utils.EncodeURIComponent(strValue.Trim());
        //    objCookie.Domain = N0.Config.CommonConfig.strDomain;
        //    if (iExpires > 0)
        //    {
        //        if (iExpires == 1)
        //        {
        //            objCookie.Expires = DateTime.MaxValue;
        //        }
        //        else
        //        {
        //            objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
        //        }
        //    }
        //    HttpContext.Current.Response.Cookies.Add(objCookie);
        //}

        /// 创建COOKIE对象并赋多个KEY键值
        /// <summary>
        /// 创建COOKIE对象并赋多个KEY键值
        /// 设键/值如下：
        /// NameValueCollection myCol = new NameValueCollection();
        /// myCol.Add("red", "rojo");
        /// myCol.Add("green", "verde");
        /// myCol.Add("blue", "azul");
        /// myCol.Add("red", "rouge");   结果“red:rojo,rouge；green:verde；blue:azul”
        /// </summary>
        /// <param name="strCookieName">COOKIE对象名</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>
        /// <param name="KeyValue">键/值对集合</param>
        //public static void SetObj(string strCookieName, int iExpires, NameValueCollection KeyValue)
        //{
        //    HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
        //    foreach (String key in KeyValue.AllKeys)
        //    {
        //        objCookie[key] = Utils.EncodeURIComponent(KeyValue[key].Trim());
        //    }
        //    objCookie.Domain = N0.Config.CommonConfig.strDomain;
        //    if (iExpires > 0)
        //    {
        //        if (iExpires == 1)
        //        {
        //            objCookie.Expires = DateTime.MaxValue;
        //        }
        //        else
        //        {
        //            objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
        //        }
        //    }
        //    HttpContext.Current.Response.Cookies.Add(objCookie);
        //}

        /// 创建COOKIE对象并赋Value值
        /// <summary>
        /// 创建COOKIE对象并赋Value值，修改COOKIE的Value值也用此方法，因为对COOKIE修改必须重新设Expires
        /// </summary>
        /// <param name="strCookieName">COOKIE对象名</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>
        /// <param name="strDomain">作用域</param>
        /// <param name="strValue">COOKIE对象Value值</param>
        //public static void SetObj(string strCookieName, int iExpires, string strValue, string strDomain)
        //{
        //    HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
        //    objCookie.Value = Utils.EncodeURIComponent(strValue.Trim());
        //    objCookie.Domain = strDomain.Trim();
        //    if (iExpires > 0)
        //    {
        //        if (iExpires == 1)
        //        {
        //            objCookie.Expires = DateTime.MaxValue;
        //        }
        //        else
        //        {
        //            objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
        //        }
        //    }
        //    HttpContext.Current.Response.Cookies.Add(objCookie);
        //}

        /// 创建COOKIE对象并赋多个KEY键值
        /// <summary>
        /// 创建COOKIE对象并赋多个KEY键值
        /// 设键/值如下：
        /// NameValueCollection myCol = new NameValueCollection();
        /// myCol.Add("red", "rojo");
        /// myCol.Add("green", "verde");
        /// myCol.Add("blue", "azul");
        /// myCol.Add("red", "rouge");   结果“red:rojo,rouge；green:verde；blue:azul”
        /// </summary>
        /// <param name="strCookieName">COOKIE对象名</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>
        /// <param name="strDomain">作用域</param>
        /// <param name="KeyValue">键/值对集合</param>
        //public static void SetObj(string strCookieName, int iExpires, NameValueCollection KeyValue, string strDomain)
        //{
        //    HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
        //    foreach (String key in KeyValue.AllKeys)
        //    {
        //        objCookie[key] = Utils.EncodeURIComponent(KeyValue[key].Trim());
        //    }
        //    objCookie.Domain = strDomain.Trim();
        //    if (iExpires > 0)
        //    {
        //        if (iExpires == 1)
        //        {
        //            objCookie.Expires = DateTime.MaxValue;
        //        }
        //        else
        //        {
        //            objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
        //        }
        //    }
        //    HttpContext.Current.Response.Cookies.Add(objCookie);
        //}

        /// 读取Cookie某个对象的Value值
        /// <summary>
        /// 读取Cookie某个对象的Value值，返回Value值，如果对象本就不存在，则返回字符串"CookieNonexistence"
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <returns>Value值，如果对象本就不存在，则返回字符串"CookieNonexistence"</returns>
        //public static string GetValue(string strCookieName)
        //{
        //    if (HttpContext.Current.Request.Cookies[strCookieName] == null)
        //    {
        //        return "CookieNonexistence";
        //    }
        //    else
        //    {
        //        return Utils.DecodeURIComponent(HttpContext.Current.Request.Cookies[strCookieName].Value);
        //    }
        //}

        /// 读取Cookie某个对象的某个Key键的键值
        /// <summary>
        /// 读取Cookie某个对象的某个Key键的键值，返回Key键值，如果对象本就不存在，则返回字符串"CookieNonexistence"，如果Key键不存在，则返回字符串"KeyNonexistence"
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <param name="strKeyName">Key键名</param>
        /// <returns>Key键值，如果对象本就不存在，则返回字符串"CookieNonexistence"，如果Key键不存在，则返回字符串"KeyNonexistence"</returns>
        //public static string GetValue(string strCookieName, string strKeyName)
        //{
        //    if (HttpContext.Current.Request.Cookies[strCookieName] == null)
        //    {
        //        return "CookieNonexistence";
        //    }
        //    else
        //    {
        //        string strObjValue = Utils.DecodeURIComponent(HttpContext.Current.Request.Cookies[strCookieName].Value);
        //        string strKeyName2 = strKeyName + "=";
        //        if (strObjValue.IndexOf(strKeyName2) == -1)
        //        {
        //            return "KeyNonexistence";
        //        }
        //        else
        //        {
        //            return Utils.DecodeURIComponent(HttpContext.Current.Request.Cookies[strCookieName][strKeyName]);
        //        }
        //    }
        //}

        /// 修改某个COOKIE对象某个Key键的键值 或 给某个COOKIE对象添加Key键 都调用本方法
        /// <summary>
        /// 修改某个COOKIE对象某个Key键的键值 或 给某个COOKIE对象添加Key键 都调用本方法，操作成功返回字符串"success"，如果对象本就不存在，则返回字符串"CookieNonexistence"。
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <param name="strKeyName">Key键名</param>
        /// <param name="KeyValue">Key键值</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)。注意：虽是修改功能，实则重建覆盖，所以时间也要重设，因为没办法获得旧的有效期< /param>
        /// <returns>如果对象本就不存在，则返回字符串"CookieNonexistence"，如果操作成功返回字符串"success"。</returns>
        //public static string Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires)
        //{
        //    if (HttpContext.Current.Request.Cookies[strCookieName] == null)
        //    {
        //        return "CookieNonexistence";
        //    }
        //    else
        //    {
        //        HttpCookie objCookie = HttpContext.Current.Request.Cookies[strCookieName];
        //        objCookie[strKeyName] = Utils.EncodeURIComponent(KeyValue.Trim());
        //        objCookie.Domain = N0.Config.CommonConfig.strDomain;
        //        if (iExpires > 0)
        //        {
        //            if (iExpires == 1)
        //            {
        //                objCookie.Expires = DateTime.MaxValue;
        //            }
        //            else
        //            {
        //                objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
        //            }
        //        }
        //        HttpContext.Current.Response.Cookies.Add(objCookie);
        //        return "success";
        //    }
        //}

        /// 删除COOKIE对象
        /// <summary>
        /// 删除COOKIE对象
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        //public static void Del(string strCookieName)
        //{
        //    HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
        //    objCookie.Domain = N0.Config.CommonConfig.strDomain;
        //    objCookie.Expires = DateTime.Now.AddYears(-5);
        //    HttpContext.Current.Response.Cookies.Add(objCookie);
        //}

        /// 删除某个COOKIE对象某个Key子键
        /// <summary>
        /// 删除某个COOKIE对象某个Key子键，操作成功返回字符串"success"，如果对象本就不存在，则返回字符串"CookieNonexistence"
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <param name="strKeyName">Key键名</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)。注意：虽是修改功能，实则重建覆盖，所以时间也要重设，因为没办法获得旧的有效期< /param>
        /// <returns>如果对象本就不存在，则返回字符串"CookieNonexistence"，如果操作成功返回字符串"success"。</returns>
        //public static string Del(string strCookieName, string strKeyName, int iExpires)
        //{
        //    if (HttpContext.Current.Request.Cookies[strCookieName] == null)
        //    {
        //        return "CookieNonexistence";
        //    }
        //    else
        //    {
        //        HttpCookie objCookie = HttpContext.Current.Request.Cookies[strCookieName];
        //        objCookie.Values.Remove(strKeyName);
        //        objCookie.Domain = N0.Config.CommonConfig.strDomain;
        //        if (iExpires > 0)
        //        {
        //            if (iExpires == 1)
        //            {
        //                objCookie.Expires = DateTime.MaxValue;
        //            }
        //            else
        //            {
        //                objCookie.Expires = DateTime.Now.AddSeconds(iExpires);
        //            }
        //        }
        //        HttpContext.Current.Response.Cookies.Add(objCookie);
        //        return "success";
        //    }
        //}

        #endregion

    }
}