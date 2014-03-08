using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Hope.Util
{
    /// <summary>
    /// 加密、解密类
    /// </summary>
    public class EncryptionUtil
    {
        private static readonly byte[] _PasswordVerctor = new byte[] { 138, 149, 107, 59, 190, 247, 130, 49 };
        private static readonly byte[] _PasswordKey = new byte[] { 13, 121, 68, 218, 42, 100, 86, 151, 187, 194, 252, 69, 78, 130, 66, 48, 179, 58, 37, 190, 169, 129, 159, 79 };

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public EncryptionUtil()
        {

        }

        #endregion

        #region 密码加密与解密

        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="passwordText">密码原文</param>
        /// <returns>返回加密后的密文</returns>
        public static string EncryptPassword(string passwordText)
        {
            return Encrypt3DESString(passwordText, _PasswordKey,_PasswordVerctor);
        }

        /// <summary>
        /// 密码解密
        /// </summary>
        /// <param name="passwordText">密文</param>
        /// <returns>返回密码原文</returns>
        public static string DecrypePassword(string passwordText)
        {
            return Decrypt3DESString(passwordText, _PasswordKey, _PasswordVerctor);
        }

        #endregion

        #region 3DES加密字符串

        /// <summary>
        /// 3DES加密处理
        /// </summary>
        /// <param name="strData">将被加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="IV">算法的初始化向量</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string Encrypt3DESString(string encryptString, byte[] key, byte[] IV)
        {
            string a = string.Empty;
            string b = string.Empty;
            try
            {
                //创建内存流
                MemoryStream mStream = new MemoryStream();
                //创建加密流
                CryptoStream cStream = new CryptoStream(mStream, new TripleDESCryptoServiceProvider().CreateEncryptor(key, IV), CryptoStreamMode.Write);
                byte[] inputByteArray = new ASCIIEncoding().GetBytes(encryptString);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        #endregion

        #region 3DES解密字符串

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="IV">算法的初始化向量</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string Decrypt3DESString(string decryptString, byte[] key, byte[] IV)
        {
            try
            {                
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                TripleDESCryptoServiceProvider DCSP = new TripleDESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        #endregion

        #region MD5哈希

        /// <summary>
        /// 使用MD5算法进行哈希
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns>杂凑字串</returns>
        public static string MD5Hash(string sourceStr)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(sourceStr, "MD5");
        }

        /// <summary>
        /// 使用MD5算法进行哈希
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns>杂凑字串</returns>
        public static string MD5Hash(string sourceStr, int code)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
            {
                strEncrypt = FormsAuthentication.HashPasswordForStoringInConfigFile(sourceStr, "MD5").Substring(8, 16);
            }

            if (code == 32)
            {
                strEncrypt = FormsAuthentication.HashPasswordForStoringInConfigFile(sourceStr, "MD5");
            }

            return strEncrypt;
        }

        #endregion

        #region SHA1哈希

        /// <summary>
        /// 使用SHA1算法进行哈希
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <returns>杂凑字串</returns>
        public static string SHA1Hash(string sourceStr)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(sourceStr, "SHA1");
        }

        #endregion

        #region 混合哈希

        /// <summary>
        /// 混合哈希
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="keyStr">杂凑字串</param>
        /// <returns></returns>
        public static string Hash(string sourceStr, string keyStr)
        {
            return SHA1Hash(MD5Hash(sourceStr) + MD5Hash(keyStr));
        }

        #endregion

    }
}
