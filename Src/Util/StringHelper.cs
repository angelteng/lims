using System;
using System.Collections.Generic;
using System.Text;

namespace Hope.Util
{
    public class StringHelper
    {
        #region 截取字符串
        /// <summary>
        /// 功能:截取字符串长度
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="length">字符串长度</param>
        /// <param name="flg">true:加...,flase:不加</param>
        /// <returns></returns>
        public static string GetString(string str, int length, bool flg)
        {
            int i = 0, j = 0;
            foreach (char chr in str)
            {
                if ((int)chr > 127)
                {
                    i += 2;
                }
                else
                {
                    i++;
                }
                if (i > length)
                {
                    str = str.Substring(0, j);
                    if (flg)
                        str += "......";
                    break;
                }
                j++;
            }
            return str;
        }
        #endregion

        #region 截取字符串+…
        /// <summary>
        /// 格式化字符串,取字符串前 strLength 位，其他的用...代替.
        /// 计算字符串长度。汉字两个字节，字母一个字节
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="strLength">字符串长度</param>
        /// <returns></returns>
        public static string FormatStr(string str, int len)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                { tempLen += 2; }
                else
                { tempLen += 1; }
                try
                { tempString += str.Substring(i, 1); }
                catch
                { break; }
                if (tempLen > len) break;
            }
            //如果截过则加上半个省略号 
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(str);
            if (mybyte.Length > len)
                tempString += "...";
            tempString = tempString.Replace(" ", " ");
            tempString = tempString.Replace("<", "<");
            tempString = tempString.Replace(">", ">");
            tempString = tempString.Replace('\n'.ToString(), "<br>");
            return tempString;
        }

        /// <summary>
        /// 截取字符串+…
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="intlen"></param>
        /// <returns></returns>
        public string CutString(string strInput, int intlen)//截取字符串
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            int intLength = 0;
            string strString = "";
            byte[] s = ascii.GetBytes(strInput);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    intLength += 2;
                }
                else
                {
                    intLength += 1;
                }

                try
                {
                    strString += strInput.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (intLength > intlen)
                {
                    break;
                }
            }
            //如果截过则加上半个省略号
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(strInput);
            if (mybyte.Length > intlen)
            {
                strString += "…";
            }
            return strString;
        }
        #endregion

        #region 字符串分函数
        /// <summary>
        /// 字符串分函数
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="index"></param>
        /// <param name="Separ"></param>
        /// <returns></returns>
        public static string StringSplit(string strings, int index, string Separ)
        {
            string[] s = strings.Split(char.Parse(Separ));
            return s[index];
        }
        #endregion

        #region 分解字符串为数组
        /// <summary>
        /// 字符串分函数
        /// </summary>
        /// <param name="str">要分解的字符串</param>
        /// <param name="splitstr">分割符,可以为string类型</param>
        /// <returns>字符数组</returns>
        public static string[] splitstr(string str, string splitstr)
        {
            if (splitstr != "")
            {
                System.Collections.ArrayList c = new System.Collections.ArrayList();
                while (true)
                {
                    int thissplitindex = str.IndexOf(splitstr);
                    if (thissplitindex >= 0)
                    {
                        c.Add(str.Substring(0, thissplitindex));
                        str = str.Substring(thissplitindex + splitstr.Length);
                    }
                    else
                    {
                        c.Add(str);
                        break;
                    }
                }
                string[] d = new string[c.Count];
                for (int i = 0; i < c.Count; i++)
                {
                    d[i] = c[i].ToString();
                }
                return d;
            }
            else
            {
                return new string[] { str };
            }
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

        #region 根据传入的字符串是否为yes/no返回Bit
        /// <summary>
        /// 根据传入的字符串是否为yes/no返回Bit
        /// </summary>
        /// <param name="flg"></param>
        /// <returns></returns>
        public static int GetBitBool(string flg)
        {
            int str = 0;
            switch (flg.ToLower())
            {
                case "yes":
                    str = 1;
                    break;
                case "no":
                    str = 0;
                    break;
                default:
                    break;
            }
            return str;
        }
        #endregion

        #region 检测一个字符符,是否在另一个字符中,存在,存在返回true,否则返回false
        /// <summary>
        /// 检测一个字符符,是否在另一个字符中,存在,存在返回true,否则返回false
        /// </summary>
        /// <param name="srcString">原始字符串</param>
        /// <param name="aimString">目标字符串</param>
        /// <returns></returns>
        public static bool IsEnglish(string srcString, string aimString)
        {
            bool Rev = true;
            string chr;
            if (aimString == "" || aimString == null) return false;
            for (int i = 0; i < aimString.Length; i++)
            {
                chr = aimString.Substring(i, 1);
                if (srcString.IndexOf(chr) < 0)
                {
                    return false;
                }

            }
            return Rev;
        }
        #endregion

        #region 检测字符串中是否含有中文及中文长度
        /// <summary>
        /// 检测字符串中是否含有中文及中文长度
        /// </summary>
        /// <param name="str">要检测的字符串</param>
        /// <returns>中文字符串长度</returns>
        public static int CnStringLength(string str)
        {
            ASCIIEncoding n = new ASCIIEncoding();
            byte[] b = n.GetBytes(str);
            int l = 0;  // l 为字符串之实际长度 
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63)  //判断是否为汉字或全脚符号 
                {
                    l++;
                }
            }
            return l;

        }
        #endregion

        #region 取字符串右侧的几个字符
        /// <summary>
        /// 取字符串右侧的几个字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="length">右侧的几个字符</param>
        /// <returns></returns>
        public static string GetStrRight(string str, int length)
        {
            string Rev = "";

            if (str.Length < length)
            {
                Rev = str;

            }
            else
            {
                Rev = str.Substring(str.Length - length, length);
            }
            return Rev;


        }
        #endregion

        #region 替换右侧的字符串

        /// <summary>
        /// 替换右侧的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="strsrc">右侧的字符串</param>
        /// <param name="straim">要替换为的字符串</param>
        /// <returns></returns>
        public static string RepStrRight(string str, string strsrc, string straim)
        {

            string Rev = "";
            if (GetStrRight(str, strsrc.Length) != strsrc)
            {
                Rev = str;
            }
            else
            {
                Rev = str.Substring(0, str.Length - strsrc.Length).ToString() + straim.ToString();
            }
            return Rev;
        }

        #endregion

    }
}
