
/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：OrgHospital(OrgHospital)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：wensaint
 * 日    期：2013-03-13
 * wensaint@126.com
 * QQ:286661274
 * ------------编辑修改信息--------------
 * 作    者：
 * 日    期：
 * 内    容：
******************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Hope.Util;

namespace Hope.ITMS
{

    /// <summary>
    /// 系统ID
    /// </summary>
    [Serializable]
    public class SystemId : IComparable, IEquatable<SystemId>
    {

        /// <summary>
        /// 空值
        /// </summary>
        public static readonly SystemId Empty = new SystemId(0);

        private long id = 0;

        /// <summary>
        /// 系统主键
        /// </summary>
        /// <param name="value"></param>
        public SystemId(long value)
        {
            id = value;
        }

        /// <summary>
        /// 将16进制的字符转换为SystemId
        /// </summary>
        /// <param name="value"></param>
        public SystemId(string value)
        {
            id = ConvertHelper.ToLong(value,16);  //将原来16进制转换为long
        }

        /// <summary>
        /// 将任意对象换为SystemId
        /// </summary>
        /// <param name="value"></param>
        public SystemId(object value)
        {
            if (value == null)
            {
                id = ConvertHelper.ToLong(new SystemId(0));
            }
            else
            {
                id = ConvertHelper.ToLong(value, 16);
            }            
        }

        /// <summary>
        /// 创建新键值
        /// </summary>
        /// <returns></returns>
        public static SystemId NewKey()
        {
            string str = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);  //产生为16进制数字
            return new SystemId(str);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (id == 0)
            {
                return "";
            }
            return id.ToString("x16");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public long ToInt64()
        {
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if ((obj as object) == null)
            {
                return false;
            }
            else if (!(obj is SystemId))
            {
                return false;
            }
            return this.id.Equals(((SystemId)obj).ToInt64());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        #region interface ...

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (!(obj is SystemId))
            {
                return 1;
            }

            SystemId _value = (SystemId)obj;
            return this.id.CompareTo(_value.ToInt64());


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SystemId other)
        {
            if (!(other is SystemId))
            {
                return false;
            }
            return this.id.Equals(other.ToInt64());
        }


        #endregion

        #region operator ...

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator ==(SystemId l, SystemId r)
        {
            if ((l as object) == null && (r as object) == null)
            {
                return true;
            }
            else if ((l as object) == null || (r as object) == null)
            {
                return false;
            }

            return l.ToInt64() == r.ToInt64();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator !=(SystemId l, SystemId r)
        {
            if ((l as object) == null && (r as object) == null)
            {
                return false;
            }
            else if ((l as object) == null || (r as object) == null)
            {
                return true;
            }

            return l.ToInt64() != r.ToInt64();
        }
        #endregion
    }


    /// <summary>
    /// 系统id集合
    /// </summary>
    [Serializable]
    public class SystemIdCollection : ICollection
    {

        private List<SystemId> _List = new List<SystemId>();
        /// <summary>
        /// 
        /// </summary>
        public SystemIdCollection() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Add(long value)
        {
            SystemId id = new SystemId(value);
            if (Exists(id) || id == SystemId.Empty)
            {
                return;
            }
            _List.Add(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Add(string value)
        {
            SystemId id = new SystemId(value);
            if (Exists(id) || id == SystemId.Empty)
            {
                return;
            }
            _List.Add(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Add(SystemId id)
        {
            if (Exists(id) || id == SystemId.Empty)
            {
                return;
            }
            _List.Add(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idList"></param>
        public void AddRange(SystemIdCollection idList)
        {
            foreach (SystemId id in idList)
            {
                if (Exists(id))
                {
                    continue;
                }
                _List.Add(id);
            }
        }

        /// <summary>
        /// 元素是否存在于集合中
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(SystemId id)
        {
            if (_List.Count == 0)
            {
                return false;
            }
            else if (id == SystemId.Empty)
            {
                return true;
            }
            return _List.Exists(delegate(SystemId si) { return id.ToInt64() == si.ToInt64(); });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Remove(SystemId id)
        {
            _List.Remove(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SystemId this[int index]
        {
            get { return _List[index]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public long[] ToValueArray()
        {
            List<long> arr = new List<long>();
            foreach (SystemId id in _List)
            {
                arr.Add(id.ToInt64());
            }
            return arr.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (SystemId id in this._List)
            {
                if (sb.Length > 0)
                    sb.Append(",");
                sb.Append(id.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToLongString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (SystemId id in this._List)
            {
                if (sb.Length > 0)
                    sb.Append(",");
                sb.Append(id.ToInt64().ToString());
            }
            return sb.ToString();
        }

        #region interface ...

        /// <summary>
        /// 子项数量
        /// </summary>
        public int Count
        {
            get { return _List.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSynchronized { get { return true; } }

        /// <summary>
        /// 
        /// </summary>
        public object SyncRoot { get { return _List.ToArray().SyncRoot; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Array array, int index)
        {
            foreach (SystemId id in _List.ToArray())
            {
                array.SetValue(id, index);
                index = index + 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        #endregion

    }
}
