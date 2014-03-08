
/******************************************************************
 *
 * ����ģ�飺
 * �� �� �ƣ�PagerData(��ҳ����ʵ��)
 * ������������ҳ��������ֵ�ȵļ���
 * 
 * ------------������Ϣ------------------
 * ��    �ߣ�wensaint
 * ��    �ڣ�2009-04-00
 * wensaint@126.com
 * MSN:wensaint@live.cn
 * QQ:286661274
 * ------------�༭�޸���Ϣ--------------
 * ��    �ߣ�
 * ��    �ڣ�
 * ��    �ݣ�
******************************************************************/
using System;
using System.ComponentModel;

namespace Hope.Util
{
    /// <summary>
    /// ��ҳ��Ϣ
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
        /// ��ҳ��Ϣ
        /// </summary>
        /// <param name="pageSize">ÿҳ��ʾ��¼��</param>
        public PagerData(int pageSize)
        {
            PageSize = pageSize;
        }

        /// <summary>
        /// ��ҳ��Ϣ
        /// </summary>
        /// <param name="pageSize">ÿҳ��ʾ��¼��</param>
        /// <param name="currentPage">��ǰҳ��</param>
        public PagerData(int pageSize, int currentPage)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;            
        }

        private int _CurrentIndex;
        /// <summary>
        /// ��ȡ��ǰҳ��¼��ʼ����ֵ
        /// </summary>
        public int CurrentIndex
        {
            get { return _CurrentIndex; }
        }

        private int _CurrentPage;
        /// <summary>
        /// ��ȡ�����õ�ǰҳ��
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
        /// ��ȡ��ǰҳ��ʾ��
        /// </summary>
        public int CurrentPageSize
        {
            get { return _CurrentPageSize; }
        }

        private int _PageSize;
        /// <summary>
        /// ��ȡ������ÿҳ��ʾ��
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
        /// ��ȡ��ҳ��
        /// </summary>
        public int TotalPage
        {
            get { return _TotalPage; }
        }

        private int _TotalRecord;
        /// <summary>
        /// ��ȡ�������ܼ�¼��
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
        /// ����
        /// </summary>
        private void Calculate()
        {
            _TotalPage = _TotalRecord / _PageSize;
            if (_TotalPage == 0 || (_TotalRecord % _PageSize) > 0)
            {
                _TotalPage += 1;
            }
            //�жϵ�ǰҳ��
            if (CurrentPage <= 0)
            {
                CurrentPage = 1;
            }
            else if (CurrentPage > _TotalPage)
            {
                CurrentPage = _TotalPage;
            }
            //���㵱ǰҳ��Ŀ�ʼ����ֵ
            _CurrentIndex = _PageSize * (CurrentPage - 1);
            //���㵱ǰҳ����ʾ��
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
