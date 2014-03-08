using System;
using System.Configuration;
using System.Web;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Hope.WebBase
{
    public class SQLCheck : IHttpModule
    {
        public void Init(HttpApplication application)
        {
            application.BeginRequest += (new
            EventHandler(this.Application_BeginRequest));
        }
        private void Application_BeginRequest(Object source, EventArgs e)
        {
            ProcessRequest pr = new ProcessRequest();
            pr.StartProcessRequest();
        }
        public void Dispose()
        {
        }
    }

    public class ProcessRequest
    {
        private const string SqlStr = @"select|insert|delete|from|count(|drop table|update|truncate|asc(|mid(|char(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user|""|or|and";
        private const string StrRegex = @"[-|;|,|/|(|)|[|]|}|{|%|@|*|!|']";

        /// 
        /// 用来识别是否是流的方式传输 
        /// 
        bool IsUploadRequest(HttpRequest request)
        {
            return StringStartsWithAnotherIgnoreCase(request.ContentType, "multipart/form-data");
        }

        /// 
        /// 比较内容类型 
        /// 
        private static bool StringStartsWithAnotherIgnoreCase(string s1, string s2)
        {
            return (string.Compare(s1, 0, s2, 0, s2.Length, true, CultureInfo.InvariantCulture) == 0);
        }
        //SQL注入式攻击代码分析 
        #region SQL注入式攻击代码分析
        /// 
        /// 处理用户提交的请求 
        /// 
        public void StartProcessRequest()
        {
            HttpRequest Request = System.Web.HttpContext.Current.Request;
            HttpResponse Response = System.Web.HttpContext.Current.Response;
            try
            {
                string getkeys = "";
                if (IsUploadRequest(Request)) return; //如果是流传递就退出 
                //字符串参数 
                if (Request.QueryString != null)
                {
                    for (int i = 0; i < Request.QueryString.Count; i++)
                    {
                        getkeys = Request.QueryString.Keys[i];
                        if (!ProcessSqlStr(Request.QueryString[getkeys]))
                        {
                            Response.Write("<script>alert('参数中含有非法字符串')</script>");
                            //Response.Redirect(sqlErrorPage + "?errmsg=QueryString中含有非法字符串&sqlprocess=true");
                            Response.End();
                        }
                    }
                }
                //form参数 
                if (Request.Form != null)
                {
                    for (int i = 0; i < Request.Form.Count; i++)
                    {
                        getkeys = Request.Form.Keys[i];
                        if (!ProcessSqlStr(Request.Form[getkeys]))
                        {
                            Response.Write("<script>alert('表单中含有非法字符串')</script>");
                            //Response.Redirect(sqlErrorPage + "?errmsg=Form中含有非法字符串&sqlprocess=true");
                            Response.End();
                        }
                    }
                }
                //cookie参数 
                if (Request.Cookies != null)
                {
                    for (int i = 0; i < Request.Cookies.Count; i++)
                    {
                        getkeys = Request.Cookies.Keys[i];
                        if (!ProcessSqlStr(Request.Cookies[getkeys].Value))
                        {
                            Response.Write("<script>alert('Cookie中含有非法字符串')</script>");
                            //Response.Redirect(sqlErrorPage + "?errmsg=Cookie中含有非法字符串&sqlprocess=true");
                            Response.End();
                        }
                    }
                }
            }
            catch
            {
                // 错误处理: 处理用户提交信息! 
                Response.Clear();
                //Response.Write("CustomErrorPage配置错误");
                Response.End();
            }
        }
        
        /// <summary>
        /// 检查是否存在SQL注入可能关键字
        /// </summary>
        /// <param name="Str">待检查字符串</param>
        /// <returns>不存在SQL注入关键字：true表示通过检查，false不通过检查，即存在</returns>
        private bool ProcessSqlStr(string Str)
        {
            if (Regex.IsMatch(Str, SqlStr, RegexOptions.IgnoreCase) || Regex.IsMatch(Str, StrRegex))
                return false;
            return true;            
        }
        #endregion
    }
}