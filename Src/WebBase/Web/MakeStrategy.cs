using System;
using System.Text;
using System.Xml;

namespace Hope.WebBase
{
    /// <summary>
    /// IMakeHTML 的摘要说明
    /// </summary>
    public abstract class MakeStrategy
    {
        /// <summary>
        /// 抽象方法，生成HMTL
        /// </summary>
        /// <returns></returns>
        public abstract string GenerateHTML();
    }
}