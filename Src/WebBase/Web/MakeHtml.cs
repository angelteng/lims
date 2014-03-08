namespace Hope.WebBase
{
    /// <summary>
    /// MakeHtml 的摘要说明
    /// </summary>
    public class MakeHtml
    {
        MakeStrategy strategy;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strategy"></param>
        public MakeHtml(MakeStrategy strategy)
        {
            this.strategy = strategy;
        }

        /// <summary>
        /// 生成HTML
        /// </summary>
        /// <returns></returns>
        public string GetHtml()
        {
            return strategy.GenerateHTML();
        }
    }
}