using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Hope.Util
{
    /// <summary>
    /// Cookie操作类
    /// </summary>
    public class CommonHelper
    {
        #region "获取服务器IP"
        /// <summary>
        /// 获取服务器IP
        /// </summary>
        public static string GetServerIp
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"].ToString();
            }
        }
        #endregion

        #region "获取服务器操作系统"
        /// <summary>
        /// 获取服务器操作系统
        /// </summary>
        public static string GetServerOS
        {
            get
            {
                return Environment.OSVersion.VersionString;
            }
        }
        #endregion

        #region "获取服务器域名"
        /// <summary>
        /// 获取服务器域名
        /// </summary>
        public static string GetServerHost
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            }
        }
        #endregion

        #region "获得客户端操作系统"

        /// <summary>
        /// 获得操作系统
        /// </summary>
        /// <returns>操作系统名称</returns>
        public static string GetClientSystem
        {
            get
            {
                string s = HttpContext.Current.Request.UserAgent.Trim().Replace("(", "").Replace(")", "");
                string[] sArray = s.Split(';');
                switch (sArray[2].Trim())
                {
                    case "Windows 4.10":
                        s = "Windows 98";
                        break;
                    case "Windows 4.9":
                        s = "Windows Me";
                        break;
                    case "Windows NT 5.0":
                        s = "Windows 2000";
                        break;
                    case "Windows NT 5.1":
                        s = "Windows XP";
                        break;
                    case "Windows NT 5.2":
                        s = "Windows 2003";
                        break;
                    case "Windows NT 6.0":
                        s = "Windows Vista";
                        break;
                    default:
                        s = "Other";
                        break;
                }
                return s;
            }
        }

        #endregion

        #region 获取客户端浏览器类型

        /// <summary>
        /// 获取客户端浏览器类型
        /// </summary>
        /// <returns>客户端浏览器类型</returns>
        public static string GetClientBrowserType()
        {
            string browserName = "";
            string userAgent = HttpContext.Current.Request.UserAgent;
            if (userAgent != null)
            {
                if (userAgent.Contains("MSIE"))
                {
                    browserName = "Internet Explorer";
                }
                else if (userAgent.Contains("Firefox"))
                {
                    browserName = "Firefox";
                }
                else if (userAgent.Contains("Opera"))
                {
                    browserName = "Opera";
                }
                else if (userAgent.Contains("Chrome"))  // Chrome需要放在在Safari前面判断
                {
                    browserName = "Chrome";
                }
                else if (userAgent.Contains("Safari"))
                {
                    browserName = "Safari";
                }
                else
                {
                    browserName = "Unknown";
                }
            }

            return browserName;
        }

        #endregion

        #region 获取客户端浏览器版本号

        /// <summary>
        /// 获取客户端浏览器版本号
        /// </summary>
        /// <returns>客户端浏览器版本号</returns>
        public static string GetClientBrowserVersion()
        {
            string version = "";

            string userAgent = HttpContext.Current.Request.UserAgent;
            if (userAgent != null)
            {
                int i = -1;

                if (userAgent.IndexOf("MSIE ") >= 0) // Internet Explorer
                {
                    i = userAgent.IndexOf("MSIE ");
                    version = userAgent.Substring(i + 5, userAgent.IndexOf(";", i) - i - 5);
                }
                else if (userAgent.IndexOf("Firefox/") >= 0) // Firefox
                {
                    i = userAgent.IndexOf("Firefox/");
                    int j = userAgent.IndexOf(" ", i);  // userAgent结尾空格索引
                    version = j > 0 ? userAgent.Substring(i + 8, j - i - 8) : userAgent.Substring(i + 8);
                }
                else if (userAgent.Contains("Opera"))  // Opera
                {
                    if (userAgent.IndexOf("Version/") >= 0)
                    {
                        i = userAgent.IndexOf("Version/");
                        version = userAgent.Substring(i + 8);
                    }
                    else if (userAgent.IndexOf("Opera/") >= 0) // 10.00之前版本
                    {
                        i = userAgent.IndexOf("Opera/");
                        version = userAgent.Substring(i + 6, userAgent.IndexOf(" ", i) - 1 - 6);
                    }
                    else
                    {
                        version = "Unknown";
                    }
                }
                else if (userAgent.IndexOf("Chrome") >= 0)   // Chrome，最好放在Safari之前判断
                {
                    i = userAgent.IndexOf("Chrome");
                    version = userAgent.Substring(i + 7, userAgent.IndexOf(" ", i) - i - 7);
                }
                else if (userAgent.Contains("Safari") && userAgent.IndexOf("Version/") >= 0) // Safari
                {
                    i = userAgent.IndexOf("Version/");
                    version = userAgent.Substring(i + 8, userAgent.IndexOf(" ", i) - i - 8);
                }
                else    // Unknown
                {
                    version = "Unknown";
                }
            }

            return version;
        }

        #endregion

        #region 取得客户端真实IP，如果有代理则取第一个非内网地址

        /// <summary>
        /// 取得客户端真实IP，如果有代理则取第一个非内网地址
        /// </summary>
        public static string IPAddress
        {
            get
            {
                string result = String.Empty;
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (result != null && result != String.Empty)
                {
                    //可能有代理
                    if (result.IndexOf(".") == -1)     //没有“.”肯定是非IPv4格式
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //有“,”，估计多个代理。取第一个不是内网的IP。
                            result = result.Replace(" ", "").Replace("'", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIPAddress(temparyip[i])
                                && temparyip[i].Substring(0, 3) != "10."
                                && temparyip[i].Substring(0, 7) != "192.168"
                                && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];     //找到不是内网的地址
                                }
                            }
                        }
                        else if (IsIPAddress(result)) //代理即是IP格式
                            return result;
                        else
                            result = null;     //代理中的内容 非IP，取IP
                    }
                }

                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (null == result || result == String.Empty)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (result == null || result == String.Empty)
                    result = HttpContext.Current.Request.UserHostAddress;
                return result;
            }
        }
        #endregion...

        #region 判断是否是IP格式

        /// 判断是否是IP地址格式 0.0.0.0
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        public static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;
            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }
        #endregion

        #region 注册页面JS脚本
        /* 如果你的脚本有与页面对象(doucument对象)进行交互的语句（这在我们后面的例子中看到），则推荐使用RegisterStartupScript，
        * 反之如果要想客户端脚本尽可能早的执行，则可以使用RegisterClientScriptBlock或Response.Write。
        *
        * 应为页面上的所有 JavaScript 指定唯一的关键字，
        * 这一点十分重要（这可通过该方法中要求的 key 参数来实现）。
        * 如果多个 JavaScript 具有相同的关键字名称，则只会在页面中嵌入第一个 JavaScript。
        */

        /// <summary>
        /// 在 Page 对象的 元素的开始标记后立即发出客户端脚本。 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="handler"></param>
        public static void RegScript(Page page, string handler)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "gogo", "<script language=\"javascript\" type=\"text/javascript\">" + handler + "</script>");
        }

        /// <summary>
        /// 在 Page 对象的 元素的结束标记前发出客户端脚本。 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <param name="handler"></param>
        public static void RegScript(Page page, string key, string handler)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), key, "<script language=\"javascript\" type=\"text/javascript\">" + handler + "</script>");
        }

        /// <summary>
        /// 在Page 对象的 元素的结束标记之前发出该脚本。 
        /// 在页面的底部、表单 (form) 的最后，嵌入了一个 JavaScript 函数。
        /// </summary>
        /// <param name="page"></param>
        /// <param name="handler"></param>
        public static void RegStartupScript(Page page, string handler)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "gogo", "<script language=\"javascript\" type=\"text/javascript\">" + handler + "</script>");
        }

        /// <summary>
        /// 在 Page 对象的 元素的开始标记后立即发出客户端脚本。 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <param name="handler"></param>
        public static void RegStartupScript(Page page, string key, string handler)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), key, "<script language=\"javascript\" type=\"text/javascript\">" + handler + "</script>");
        }
        #endregion

        #region "获得Web目录物理路径"
        /// <summary>
        /// 获得Web目录物理路径
        /// </summary>
        /// <returns>Web目录物理路径</returns>
        public static string GetWebFullPath()
        {
            string AppDir = System.AppDomain.CurrentDomain.BaseDirectory;
            return AppDir;
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="controls"></param>
        public static void SetSortColumn(Page currentPage, params System.Web.UI.WebControls.Panel[] controls)
        {
            string orderType = "ASC";
            string orderColumn = currentPage.Request["OrderColumn"] == null ? "" : currentPage.Request["OrderColumn"];
            if (currentPage.Request["OrderType"] != null)
            {
                orderType = currentPage.Request["OrderType"];
            }
            foreach (System.Web.UI.WebControls.WebControl control in controls)
            {
                if (control == null)
                {
                    continue;
                }
                else if (control.Attributes["OrderColumn"] == null || control.Attributes["OrderColumn"] != orderColumn)
                {
                    continue;
                }
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                img.Height = System.Web.UI.WebControls.Unit.Pixel(12);
                img.Width = System.Web.UI.WebControls.Unit.Pixel(12);
                img.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
                img.ImageUrl = string.Format("~/Images/Order{0}.gif", orderType);
                control.Controls.Add(img);
            }

            string strQuery = currentPage.Request.Url.Query;
            string strPrefix = currentPage.Request.Url.ToString();
            if (strQuery != string.Empty)
            {
                strPrefix = strPrefix.Replace(strQuery, "");
                strQuery = strQuery.Replace("?", "&");
            }
            if (orderColumn != string.Empty)
            {
                strQuery = strQuery.Replace(string.Format("&{0}={1}", "OrderColumn", currentPage.Request["OrderColumn"]), "");
                strQuery = strQuery.Replace(string.Format("&{0}={1}", "OrderType", currentPage.Request["OrderType"]), "");
            }

            currentPage.ClientScript.RegisterClientScriptBlock(currentPage.GetType(), "", string.Format("<script language=\"javascript\" type=\"text/javascript\">currentOrderColumn = \"{0}\";currentOrderType=\"{1}\";currentPageUrl=\"{2}?{3}\";</script>", orderColumn, orderType, strPrefix, strQuery));
        }
        #endregion

        #region 字符串分割成数组

        /// <summary>
        /// 分割字符串，接受任意char做分隔符，值唯一
        /// </summary>
        /// <param name="text">要处理的字符串</param>
        /// <returns>唯一的Int数组</returns>
        public static List<int> SplitString(string text)
        {
            List<int> exclude = new List<int>();

            List<int> result = SplitString(text, exclude);

            return result;
        }

        /// <summary>
        /// 分割字符串，接受任意char做分隔符，值唯一
        /// </summary>
        /// <param name="text">要处理的字符串</param>
        /// <param name="splitter">分隔符</param>
        /// <returns>唯一的Int数组</returns>
        public static List<int> SplitString(string text, char splitter)
        {
            List<int> exclude = new List<int>();
            List<int> result = SplitString(text, splitter, exclude);

            return result;
        }

        /// <summary>
        /// 分割字符串，以逗号做分隔符，值唯一，可排除不需要的数值
        /// </summary>
        /// <param name="text">要处理的字符串</param>
        /// <param name="exclude">排除值</param>
        /// <returns>唯一的Int数组</returns>
        public static List<int> SplitString(string text, int exclude)
        {
            List<int> exc = new List<int>();
            exc.Add(exclude);
            char splitter = ',';
            List<int> result = SplitString(text, splitter, exc);

            return result;
        }

        /// <summary>
        /// 分割字符串，以逗号做分隔符，值唯一，可排除不需要的数值
        /// </summary>
        /// <param name="text">要处理的字符串</param>
        /// <param name="exclude">排除值</param>
        /// <returns>唯一的Int数组</returns>
        public static List<int> SplitString(string text, List<int> exclude)
        {
            char splitter = ',';
            List<int> result = SplitString(text, splitter, exclude);

            return result;
        }

        /// <summary>
        /// 分割字符串，接受任意char做分隔符，值唯一，可排除不需要的数值
        /// </summary>
        /// <param name="text">要处理的字符串</param>
        /// <param name="splitter">分隔符</param>
        /// <param name="exclude">排除值</param>
        /// <returns>唯一的Int数组</returns>
        public static List<int> SplitString(string text, char splitter, List<int> exclude)
        {
            List<int> result = null;
            if (!string.IsNullOrEmpty(text))
            {
                result = new List<int>();
                string[] arr = text.Split(splitter);
                foreach (string str in arr)
                {
                    // 排除空值
                    if (string.IsNullOrEmpty(str))
                    {
                        continue;
                    }
                    int to_add = ConvertHelper.ToInt(str, 0);

                    // 排除，如除0
                    if (exclude.Contains(to_add))
                    {
                        continue;
                    }

                    // 排除相同值
                    if (!result.Contains(to_add))
                    {
                        result.Add(to_add);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 分割字符串，接受任意char做分隔符，值唯一
        /// </summary>
        /// <param name="text">要处理的字符串</param>
        /// <returns>唯一的String数组</returns>
        public static List<string> SplitToString(string text)
        {
            List<string> exclude = new List<string>();

            List<string> result = SplitToString(text, exclude);

            return result;
        }

        /// <summary>
        /// 分割字符串，接受任意char做分隔符，值唯一
        /// </summary>
        /// <param name="text">要处理的字符串</param>
        /// <param name="splitter">分隔符</param>
        /// <returns>唯一的String数组</returns>
        public static List<string> SplitToString(string text, char splitter)
        {
            List<string> exclude = new List<string>();
            List<string> result = SplitToString(text, splitter, exclude);

            return result;
        }

        /// <summary>
        /// 分割字符串，以逗号做分隔符，值唯一，可排除不需要的数值
        /// </summary>
        /// <param name="text">要处理的字符串</param>
        /// <param name="exclude">排除值</param>
        /// <returns>唯一的String数组</returns>
        public static List<string> SplitToString(string text, List<string> exclude)
        {
            char splitter = ',';
            List<string> result = SplitToString(text, splitter, exclude);

            return result;
        }

        /// <summary>
        /// 分割字符串，接受任意char做分隔符，值唯一，可排除不需要的数值
        /// </summary>
        /// <param name="text">要处理的字符串</param>
        /// <param name="splitter">分隔符</param>
        /// <param name="exclude">排除值</param>
        /// <returns>唯一的String数组</returns>
        public static List<string> SplitToString(string text, char splitter, List<string> exclude)
        {
            List<string> result = new List<string>();

            if (!String.IsNullOrEmpty(text))
            {
                string[] arr = text.Split(splitter);

                foreach (string str in arr)
                {
                    // 排除空值
                    if (String.IsNullOrEmpty(str))
                    {
                        continue;
                    }

                    string to_add = str;

                    // 排除
                    if (exclude.Contains(to_add))
                    {
                        continue;
                    }

                    // 排除相同值
                    if (!result.Contains(to_add))
                    {
                        result.Add(to_add);
                    }
                }
            }

            return result;
        }

        #endregion

        #region 字符检查与过滤

        /**************************************************
            函数：FoundInArr
            作  用：检查一个数组中所有元素是否包含指定字符串
            参  数：strArr     ----存储数据数据的字串
                   strToFind    ----要查找的字符串
                   strSplit    ----数组的分隔符
            返回值：True,False
            **************************************************/
        /// <summary>
        /// 检查一个数组中所有元素是否包含指定字符串
        /// </summary>
        /// <param name="strArr"></param>
        /// <param name="strToFind"></param>
        /// <param name="strSplit"></param>
        /// <returns></returns>
        public static bool FoundInArr(string strArr, string strToFind, char strSplit)
        {
            bool canFind = false;
            if (strArr.IndexOf(strSplit) >= 0)
            {
                string[] strArray = strArr.Split(strSplit);
                foreach (string i2 in strArray)
                {
                    if (i2.ToLower() == strToFind.ToLower())
                    {
                        canFind = true;
                        break;
                    }
                }
            }
            else
            {
                if (strArr.ToLower() == strToFind.ToLower())
                {
                    canFind = true;
                }
            }
            return canFind;
        }
        /**************************************************
        '函数名：FindBadChar
        '作  用：查找非法的SQL字符
        '参  数：strChar-----要查找的字符串
        '返回值：true代表存在非法的字符，flase代表没有非法字符
        '**************************************************/
        /// <summary>
        /// 查找非法的SQL字符
        /// </summary>
        /// <param name="strChar"></param>
        /// <returns></returns>
        public static bool FindBadChar(string strChar)
        {
            if (strChar.Equals(null) || strChar.Equals(""))
            {
                return false;
            }
            else
            {
                string strBadChar = "\',%,^,&,?,(,),<,>,[,],{,},/,\\,;,:,\",\0,=,exec,insert,select,delete,update,chr,mid,truncate,declare,join";
                string[] BadArr = strBadChar.Split(',');
                bool b = false;
                foreach (string i in BadArr)
                {
                    if (strChar.IndexOf(i) >= 0)
                    {
                        b = true;
                        break;
                    }
                }
                return b;
            }

        }
        /**************************************************
        '函数名：ReplaceBadChar
        '作  用：过滤非法的SQL字符
        '参  数：strChar-----要过滤的字符
        '返回值：过滤后的字符
        '**************************************************/
        /// <summary>
        /// 过滤非法的SQL字符
        /// </summary>
        /// <param name="strChar"></param>
        /// <returns></returns>
        public static string ReplaceBadChar(string strChar)
        {
            if (strChar.Equals(null) || strChar.Equals(""))
            {
                return "";
            }
            else
            {
                string strBadChar = "\',%,^,&,?,(,),<,>,[,],{,},/,\\,;,:,\",\0,=,exec,insert,select,delete,update,chr,mid,truncate,declare,join";
                string[] badArray = strBadChar.Split(',');
                string tempChar = strChar;
                foreach (string i in badArray)
                {
                    tempChar = tempChar.Replace(i, "");
                }
                return tempChar;
            }
        }

        /************************************************
        '功能：格式化字符串，按指定字符与位数将字符串补足
        '参数说明：
        'str			源字符串	
        'destLen		要求的字符串长度
        'placeholder	占位符
        '************************************************/
        /// <summary>
        /// 格式化字符串，按指定字符与位数将字符串补足
        /// </summary>
        /// <param name="str"></param>
        /// <param name="destlen"></param>
        /// <param name="placeholder"></param>
        /// <returns></returns>
        public static string FormatStr(string str, int destlen, string placeholder)
        {
            int len = str.Length;
            int i = 0;
            string tempStr = str;
            while (i < destlen - len)
            {
                tempStr = tempStr + placeholder;
                i++;
            }
            return tempStr;
        }
        /**************************************************
        '函数名：FixStr
        '作  用：格式化字符串，将超过指定长度的字符串用指定字符串补足
        '参  数：str ----要格式化的字符串
        '		 length--需要的字符串长度
        '		 c ------如果不足用c字符串填充
        '返回值：  ----处理好的字符串
        '**************************************************/
        /// <summary>
        /// 格式化字符串，将超过指定长度的字符串用指定字符串补足
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string FixStr(string str, int length, string c)
        {
            int len = str.Length;
            if (len > length)
            {
                return str.Substring(0, length) + c;
            }
            else
                return str;
        }
        /**************************************************
        '函数名：CheckStr
        '作  用：检查字符串中的单引号，为SQL处理做准备
        '参  数：str ----要处理的字符串
        '返回值： 处理好之后的字符串
        '**************************************************/
        /// <summary>
        /// 检查字符串中的单引号，为SQL处理做准备
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CheckStr(string str)
        {
            if (str.Equals(null))
                return "";
            else
            {
                return str.Replace("\0", "").Replace("\'", "");
            }
        }
        /**************************************************
        '函数名：AbbrStr
        '作  用：检查字符串是否超长，将超长部分以省略号表示
        '参  数：str ----要处理的字符串
        '		 length--所需的字符串长度
        '返回值： 处理好之后的字符串
        '**************************************************/
        /// <summary>
        /// 检查字符串是否超长，将超长部分以省略号表示
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string AbbrStr(string str, int length)
        {
            if (str.Equals(null) || str.Equals(""))
                return "";
            else
            {
                if (str.Length > length)
                {
                    return str.Substring(0, length) + "...";
                }
                else
                {
                    return str;
                }
            }
        }
        /**************************************************
        '函数名：iHTMLEncode
        '作  用：用于字符串的过滤，不带脏话过滤
        '参  数：str ----要处理的字符串
        '返回值： 处理好之后的字符串
        '**************************************************/
        /// <summary>
        /// 用于字符串的过滤，不带脏话过滤
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string iHTMLEncode(string str)
        {
            string tempStr = str;
            char[] c = new char[] { (char)32, (char)9, (char)34, (char)39, (char)13, (char)10 };
            if (str.Equals(null) || str.Equals(""))
                return "";
            tempStr = tempStr.Replace(">", "&gt;");
            tempStr = tempStr.Replace("<", "&lt;");
            tempStr = tempStr.Replace(c[0].ToString(), "&nbsp;");
            tempStr = tempStr.Replace(c[1].ToString(), " ");
            tempStr = tempStr.Replace(c[2].ToString(), "&quot;");
            tempStr = tempStr.Replace(c[3].ToString(), "&#39;");
            tempStr = tempStr.Replace(c[4].ToString(), "");
            tempStr = tempStr.Replace(c[5].ToString() + c[5].ToString(), "</p><p>");
            tempStr = tempStr.Replace(c[5].ToString(), "<br>");
            return tempStr;
        }
        #endregion...

        #region 由汉字获取拼音首字母
        /// <summary>
        /// 提取汉字首字母
        /// </summary>
        /// <param name="strText">需要转换的字符串</param>
        /// <returns>转换结果</returns>
        public static string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += GetSpell(strText.Substring(i, 1));
            }
            return myStr;
        }

        /// <summary>
        /// 获取单个汉字的首拼音
        /// </summary>
        /// <param name="myChar">需要转换的字符</param>
        /// <returns>转换结果</returns>
        public static string GetSpell(string myChar)
        {
            byte[] arrCN = System.Text.Encoding.Default.GetBytes(myChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return System.Text.Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "_";
            }
            else return myChar;
        }
        #endregion
    }
}