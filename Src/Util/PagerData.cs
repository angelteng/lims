
/******************************************************************
 *
 * 所在模块：
 * 类 名 称：PagerData(分页数据实体)
 * 功能描述：总页数及索引值等的计算
 * 
 * ------------创建信息------------------
 * 作    者：wensaint
 * 日    期：2009-04-00
 * wensaint@126.com
 * MSN:wensaint@live.cn
 * QQ:286661274
 * ------------编辑修改信息--------------
 * 作    者：
 * 日    期：
 * 内    容：
******************************************************************/
using System;
using System.ComponentModel;

namespace Hope.Util
{
    /// <summary>
    /// 分页信息
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public sealed class PagerData
    {
        /// <summary>
        /// 
        /// </summary>
        public PagerData() { }

        /// <summary>
        /// 分页信息
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        public PagerData(int pageSize)
        {
            PageSize = pageSize;
        }

        /// <summary>
        /// 分页信息
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="currentPage">当前页码</param>
        public PagerData(int pageSize, int currentPage)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;            
        }

        private int _CurrentIndex;
        /// <summary>
        /// 获取当前页记录开始索引值
        /// </summary>
        public int CurrentIndex
        {
            get { return _CurrentIndex; }
        }

        private int _CurrentPage;
        /// <summary>
        /// 获取或设置当前页码
        /// </summary>
        public int CurrentPage
        {
            set { _CurrentPage = value; }
            get
            {
                if (_CurrentPage < 1)
                {
                    _CurrentPage = 1;
                }
                return _CurrentPage;
            }
        }

        private int _CurrentPageSize;
        /// <summary>
        /// 获取当前页显示数
        /// </summary>
        public int CurrentPageSize
        {
            get { return _CurrentPageSize; }
        }

        private int _PageSize;
        /// <summary>
        /// 获取或设置每页显示数
        /// </summary>
        public int PageSize
        {
            set
            {
                _PageSize = value;
                if (_PageSize < 1)
                {
                    _PageSize = 1;
                }
                Calculate();
            }
            get { return _PageSize; }
        }

        private int _TotalPage;
        /// <summary>
        /// 获取总页数
        /// </summary>
        public int TotalPage
        {
            get { return _TotalPage; }
        }

        private int _TotalRecord;
        /// <summary>
        /// 获取或设置总记录数
        /// </summary>
        public int TotalRecord
        {
            set
            {
                _TotalRecord = value;
                Calculate();
            }
            get { return _TotalRecord; }
        }

        /// <summary>
        /// 计算
        /// </summary>
        private void Calculate()
        {
            _TotalPage = _TotalRecord / _PageSize;
            if (_TotalPage == 0 || (_TotalRecord % _PageSize) > 0)
            {
                _TotalPage += 1;
            }
            //判断当前页码
            if (CurrentPage <= 0)
            {
                CurrentPage = 1;
            }
            else if (CurrentPage > _TotalPage)
            {
                CurrentPage = _TotalPage;
            }
            //计算当前页面的开始索引值
            _CurrentIndex = _PageSize * (CurrentPage - 1);
            //计算当前页的显示数
            if ((_TotalRecord < _PageSize * CurrentPage) && (_TotalRecord > 0))
            {
                _CurrentPageSize = _TotalRecord - _CurrentIndex;
            }
            else
            {
                _CurrentPageSize = _PageSize;
            }
        }
    }
}
