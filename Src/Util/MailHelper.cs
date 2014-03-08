using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace Hope.Util
{
    /// <summary>
    /// 邮件工具类
    /// </summary>
    public class MailHelper
    {
        private MailMessage _Msg;
        private SmtpClient _Client;

        #region Constructor

        /// <summary>
        /// 邮件工具实例
        /// </summary>
        public MailHelper()
        {
            Initialize();
        }

        #endregion

        #region Property
        /// <summary>
        /// Msg
        /// </summary>
        public MailMessage Msg
        {
            get { return _Msg; }
        }

        #endregion

        #region Methord

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <returns></returns>
        public bool Send(string subject, string content, params string[] receivers)
        {
            _Msg.Subject = subject;
            _Msg.Body = content; 
            foreach (string receiver in receivers)
            {
                _Msg.To.Add(receiver);
            }

            try
            {
                _Client.Send(_Msg);
            }
            catch (SmtpException ex)
            {
                LogUtil.Write("Send Email Faile!" + ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            _Msg = new MailMessage();
            _Msg.From = new MailAddress(SiteConfigUtil.Instance().MailAccount, SiteConfigUtil.Instance().DisplayName, Encoding.UTF8);
            _Msg.SubjectEncoding = Encoding.UTF8;
            _Msg.BodyEncoding = Encoding.UTF8;
            _Msg.IsBodyHtml = false;
            _Msg.Priority = MailPriority.Normal;

            _Client = new SmtpClient();
            _Client.Host = SiteConfigUtil.Instance().SmtpHost;
            _Client.Port = SiteConfigUtil.Instance().Port;
            _Client.Credentials = new NetworkCredential(SiteConfigUtil.Instance().MailAccount, EncryptionUtil.DecrypePassword(SiteConfigUtil.Instance().MailPasswd));
            _Client.EnableSsl = true;
        }

        #endregion
    }
}
