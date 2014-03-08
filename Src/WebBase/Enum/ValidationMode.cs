

namespace Hope.WebBase
{
    /// <summary>
    /// 验证码模式
    /// </summary>
    public enum ValidationMode
    {
        /// <summary>
        /// 关闭
        /// </summary>
        Off = 0,
        /// <summary>
        /// 数字
        /// </summary>
        Numeric = 1,
        /// <summary>
        /// 英文字母
        /// </summary>
        Character = 2,
        /// <summary>
        /// 中文
        /// </summary>
        ChineseCharacter = 3,
        /// <summary>
        /// 混合数字和字母
        /// </summary>
        Mix = 4,
    }
}
