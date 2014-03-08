using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Hope.Util;
using NHibernate;
using NHibernate.Criterion;

namespace Hope.ITMS.DAL
{
    /// <summary>
    /// SQL Server数据访问基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseDAL<T>
    {
        /// <summary>
        /// SQL Server数据访问基类
        /// </summary>
        public BaseDAL(){}

        #region add edit delete ...

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add(T entity)
        {
            bool result = true;
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ITransaction trans = session.BeginTransaction();
            try
            {
                session.Save(entity);
                trans.Commit();
            }
            catch (Exception e)
            {
                LogUtil.Write(typeof(T), e);
                trans.Rollback();
                result = false;
            }
            finally
            {
                session.Close();
            }
            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(T entity)
        {
            bool result = true;
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ITransaction trans = session.BeginTransaction();
            try
            {
                session.Update(entity);
                trans.Commit();
            }
            catch (Exception e)
            {
                LogUtil.Write(typeof(T), e);
                trans.Rollback();
                result = false;
            }
            finally
            {
                //session.Flush();
                session.Close();
            }
            return result;
        }

        #region Delete        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            bool result = true;
            string cmdText = string.Format("from {0} where ID=?", typeof(T).FullName);
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            try
            {
                session.Delete(cmdText, id, NHibernateUtil.Int32);
            }
            catch (Exception e)
            {
                LogUtil.Write(typeof(T), e);
                result = false;
            }
            finally
            {
                session.Flush();
                session.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool Delete(List<int> idList)
        {
            bool result = true;
            string cmdText = string.Format("from {0} where ID=?", typeof(T).FullName);
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ITransaction trans = session.BeginTransaction();
            try
            {
                foreach (int key in idList)
                {
                    session.Delete(cmdText, key, NHibernateUtil.Int32);
                }
                trans.Commit();
            }
            catch (Exception e)
            {
                LogUtil.Write(typeof(T), e);
                trans.Rollback();
                result = false;
            }
            finally
            {
                session.Flush();
                session.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool Delete(List<string> idList)
        {
            bool result = true;
            string cmdText = string.Format("from {0} where ID=?", typeof(T).FullName);
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ITransaction trans = session.BeginTransaction();
            try
            {
                foreach (string id in idList)
                {
                    session.Delete(cmdText, id, NHibernateUtil.String);
                }
                trans.Commit();
            }
            catch (Exception e)
            {
                LogUtil.Write(typeof(T), e);
                trans.Rollback();
                result = false;
            }
            finally
            {
                session.Flush();
                session.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(SystemId id)
        {
            bool result = true;
            string cmdText = string.Format("from {0} where ID=?", typeof(T).FullName);
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            try
            {
                session.Delete(cmdText, id.ToInt64(), NHibernateUtil.Int64);
            }
            catch (Exception e)
            {
                LogUtil.Write(typeof(T), e);
                result = false;
            }
            finally
            {
                session.Flush();
                session.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool Delete(SystemIdCollection idList)
        {
            bool result = true;
            string cmdText = string.Format("from {0} where ID=?", typeof(T).FullName);
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ITransaction trans = session.BeginTransaction();
            try
            {
                foreach (SystemId id in idList)
                {
                    session.Delete(cmdText, id.ToInt64(), NHibernateUtil.Int64);
                }
                trans.Commit();
            }
            catch (Exception e)
            {
                LogUtil.Write(typeof(T), e);
                trans.Rollback();
                result = false;
            }
            finally
            {
                session.Flush();
                session.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool Delete(string property, params string[] idList)
        {
            bool result = true;
            string cmdText = string.Format("from {0} where {1}=?", typeof(T).FullName, property);
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ITransaction trans = session.BeginTransaction();
            try
            {
                foreach (string id in idList)
                {
                    session.Delete(cmdText, id, NHibernateUtil.String);
                }
                trans.Commit();
            }
            catch (Exception e)
            {
                LogUtil.Write(typeof(T), e);
                trans.Rollback();
                result = false;
            }
            finally
            {
                session.Close();
            }
            return result;
        }

        /// <summary>
        /// remove by querys
        /// </summary>
        /// <returns>true or false</returns>
        public bool Delete(List<SimpleExpression> expList)
        {
            //必须要有条件，否则直接返回
            if (expList.Count==0)
            {
                return false;
            }

            bool result = true;
            string cmdText = string.Format("from {0}", typeof(T).FullName);

            string strWhere = " WHERE 1=1";
            foreach (SimpleExpression exp in expList)
            {
                strWhere += " AND " + exp.ToString();
            }

            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ITransaction trans = session.BeginTransaction();
            try
            {
                session.Delete(cmdText + strWhere);                
                trans.Commit();
            }
            catch (Exception e)
            {
                LogUtil.Write(typeof(T), e);
                trans.Rollback();
                result = false;
            }
            finally
            {
                session.Close();
            }
            return result;
        }
        #endregion

        #endregion

        #region get data,get datas ...

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetData(object id)
        {
            T entity = default(T);
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            entity = session.Get<T>(id);
            session.Close();
            return entity;
        }

        /// <summary>
        /// 获取符合 查询 条件的记录
        /// </summary>
        /// <param name="expList">条件列表</param>
        /// <returns></returns>
        public T GetDataByQuery(List<SimpleExpression> expList)
        {
            T entity = default(T);
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ICriteria crit = session.CreateCriteria(typeof(T));
            if (expList.Count > 0)
            {
                foreach (SimpleExpression exp in expList)
                {
                    crit.Add(exp);
                }
            }
            IList<T> list = crit.List<T>();
            if (list.Count > 0) { entity = list[0]; }
            session.Close();
            return entity;
        }

        /// <summary>
        /// 获取符合 "属性名＝属性值" 条件的记录
        /// </summary>
        /// <param name="property">属性字段名称</param>
        /// <param name="value">属性值</param>
        /// <returns></returns>
        public T GetDataByProperty(string property, object value)
        {
            T entity = default(T);
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            IList<T> list = session.CreateCriteria(typeof(T)).Add(Restrictions.Eq(property, value)).List<T>();
            if (list.Count > 0) { entity = list[0]; }
            session.Close();
            return entity;
        }

        /// <summary>
        /// 获取符合 "属性名＝属性值" 条件的记录
        /// </summary>
        /// <param name="property">属性字段名称</param>
        /// <param name="value">属性值</param>
        /// <returns></returns>
        public List<T> GetDatasByProperty(string property, object value)
        {
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            IList<T> list = session.CreateCriteria(typeof(T)).Add(Restrictions.Eq(property, value)).List<T>();
            session.Close();
            return list.ToList<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<T> GetDatas()
        {
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ICriteria crit = session.CreateCriteria(typeof(T));
            IList<T> list = crit.List<T>();
            session.Close();
            return list.ToList<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expList">querys</param>
        /// <returns></returns>
        public List<T> GetDatas(List<SimpleExpression> expList)
        {
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ICriteria crit = session.CreateCriteria(typeof(T));
            if (expList.Count > 0)
            {
                foreach (SimpleExpression exp in expList)
                {
                    crit.Add(exp);
                }
            }
            IList<T> list = crit.List<T>();
            session.Close();
            return list.ToList<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expList">querys</param>
        /// <param name="orders">order items</param>
        /// <returns></returns>
        public List<T> GetDatas(List<SimpleExpression> expList, OrderItemCollection orders)
        {
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ICriteria crit = session.CreateCriteria(typeof(T));
            if (expList.Count > 0)
            {
                foreach (SimpleExpression exp in expList)
                {
                    crit.Add(exp);
                }
            }
            foreach (OrderItem order in orders)
            {
                crit.AddOrder(new Order(order.Property, order.IsASC == OrderType.ASC ? true : false));
            }

            IList<T> list = crit.List<T>();
            session.Close();
            return list.ToList<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<T> GetDatas(PagerData pager)
        {
            pager.TotalRecord = GetCount();
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            IList<T> list = session.CreateCriteria(typeof(T))
                .SetFirstResult(pager.CurrentIndex)
                .SetMaxResults(pager.CurrentPageSize)
                .List<T>();
            session.Close();
            return list.ToList<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<T> GetDatas(PagerData pager, OrderItem order)
        {
            pager.TotalRecord = GetCount();
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            IList<T> list = session.CreateCriteria(typeof(T))
                .AddOrder(new Order(order.Property, order.IsASC == OrderType.ASC ? true : false))
                .SetFirstResult(pager.CurrentIndex)
                .SetMaxResults(pager.CurrentPageSize)
                .List<T>();
            session.Close();
            return list.ToList<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        public List<T> GetDatas(PagerData pager, OrderItemCollection orders)
        {
            pager.TotalRecord = GetCount();
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ICriteria crit = session.CreateCriteria(typeof(T));
            foreach (OrderItem order in orders)
            {
                crit.AddOrder(new Order(order.Property, order.IsASC == OrderType.ASC ? true : false));
            }

            IList<T> list = crit.SetFirstResult(pager.CurrentIndex)
                .SetMaxResults(pager.CurrentPageSize)
                .List<T>();
            session.Close();
            return list.ToList<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="orders"></param>
        /// <param name="exps"></param>
        /// <returns></returns>
        public List<T> GetDatas(PagerData pager, OrderItemCollection orders, List<SimpleExpression> expList)
        {
            pager.TotalRecord = GetCount(expList);
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ICriteria crit = session.CreateCriteria(typeof(T));
            foreach (OrderItem order in orders)
            {
                crit.AddOrder(new Order(order.Property, order.IsASC == OrderType.ASC ? true : false));
            }
            if (expList.Count > 0)
            {
                foreach (SimpleExpression exp in expList)
                {
                    crit.Add(exp);
                }
            }            

            IList<T> list = crit.SetFirstResult(pager.CurrentIndex)
                .SetMaxResults(pager.CurrentPageSize)
                .List<T>();
            session.Close();
            return list.ToList<T>();
        }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <returns></returns>
        private int GetCount()
        {
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            string cmdText = string.Format("select count(*) from {0}", typeof(T).FullName);
            IList list = session.CreateQuery(cmdText).List();

            int count = ConvertHelper.ToInt(list[0]);
            session.Close();
            return count;
        }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <param name="exps">查询条件</param>
        /// <returns></returns>
        private int GetCount(List<SimpleExpression> expList)
        {
            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ICriteria crit = session.CreateCriteria(typeof(T));
            foreach (SimpleExpression exp in expList)
            {
                crit.Add(exp);
            }
            int count = crit.List().Count;
            session.Close();
            return count;
        }
        #endregion     
    }
}
