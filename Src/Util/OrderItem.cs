using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Hope.Util
{
    #region 排序项 ...

    /// <summary>
    /// 排序项
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// 排序项
        /// </summary>
        public OrderItem() { }

        /// <summary>
        /// 排序的属性名
        /// </summary>
        /// <param name="propertyName"></param>
        public OrderItem(string propertyName)
        {
            _Property = propertyName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="type"></param>
        public OrderItem(string propertyName, OrderType isASC)
        {
            _Property = propertyName;
            _IsASC = isASC;
        }

        private string _Property;
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Property
        {
            get { return _Property; }
            set { _Property = value; }
        }

        private OrderType _IsASC = OrderType.ASC;
        /// <summary>
        /// 是否顺序
        /// </summary>
        public OrderType IsASC
        {
            get { return _IsASC; }
            set { _IsASC = value; }
        }
    }

    /// <summary>
    /// 排序类型
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 顺序
        /// </summary>
        ASC,
        /// <summary>
        /// 倒序
        /// </summary>
        DESC,
    }

    #endregion

    #region 排序集合 ...

    /// <summary>
    /// 排序集合
    /// </summary>
    public class OrderItemCollection : IEnumerable
    {

        /// <summary>
        /// 排序集合
        /// </summary>
        public OrderItemCollection() { }

        /// <summary>
        /// 集合数
        /// </summary>
        public int Count
        {
            get { return _Items.Count; }
        }

        private List<OrderItem> _Items = new List<OrderItem>();
        /// <summary>
        /// 获取或设置指定索引处的元素。
        /// </summary>
        /// <param name="index">要获得或设置的元素从零开始的索引</param>
        /// <returns>指定索引处的元素。</returns>
        public OrderItem this[int index]
        {
            get { return _Items[index]; }
            set { _Items[index] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(OrderItem item)
        {
            _Items.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        public void Add(string propertyName)
        {
            _Items.Add(new OrderItem(propertyName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="type"></param>
        public void Add(string propertyName, OrderType isASC)
        {
            _Items.Add(new OrderItem(propertyName, isASC));
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _Items.Clear();
        }

        /// <summary>
        /// 是否包含特定值
        /// </summary>
        /// <param name="item">中查找的值</param>
        /// <returns>如果找到，则为 true；否则为 false。</returns>
        public bool Contains(OrderItem item)
        {
            return _Items.Contains(item);
        }

        /// <summary>
        /// 确定特定项的索引。
        /// </summary>
        /// <param name="value">查找的值</param>
        /// <returns>如果在列表中找到，则为 value 的索引；否则为 -1。</returns>
        public int IndexOf(OrderItem item)
        {
            return _Items.IndexOf(item);
        }

        /// <summary>
        /// 将一个项插入指定索引处
        /// </summary>
        /// <param name="index">从零开始的索引，应在该位置插入 value。</param>
        /// <param name="item">要插入的项</param>
        public void Insert(int index, OrderItem item)
        {
            _Items.Insert(index, item);
        }

        /// <summary>
        /// 移除特定对象的第一个匹配项。
        /// </summary>
        /// <param name="item">要移除的项</param>
        public void Remove(OrderItem item)
        {
            _Items.Remove(item);
        }

        /// <summary>
        /// 移除指定索引处的项。
        /// </summary>
        /// <param name="index">从零开始的索引（属于要移除的项）。</param>
        public void RemoveAt(int index)
        {
            _Items.RemoveAt(index);
        }


        #region get Enumerable

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return this._Items.GetEnumerator();
        }

        #endregion
    }

    #endregion
}
