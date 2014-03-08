
/******************************************************************
 *
 * 所在模块：Business Logic Layer(业务逻辑层)
 * 类 名 称：SysTagBLL(SysTagBLL)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：wensaint
 * 日    期：2012-07-10
 * wensaint@126.com
 * QQ:286661274
 * ------------编辑修改信息--------------
 * 作    者：
 * 日    期：
 * 内    容：
******************************************************************/
using System;
using System.Collections.Generic;
using NHibernate.Criterion;

using Hope.ITMS.DAL;
using Hope.ITMS.Model;
using Hope.ITMS.Enums;
using Hope.Util;

namespace Hope.ITMS.BLL
{
    /// <summary>
    /// BusinessHandler SysTagBLL
    /// </summary>
    public class SysTagBLL : BaseBLL
    {
        //申明DAL接口
        private SysTagDAL Provider = DALFactory.Instance().SysTagDAL;

        #region constructor
        /// <summary>
        /// SysTagBLL
        /// </summary>
        public SysTagBLL()
        {
        }
        #endregion


        #region add, edit ...

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return the handler result</returns>
        public SystemMessage Add(SysTag entity)
        {
            if (Provider.GetData(entity.Name) != null)
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "相同名称的记录已经存在";
                HandlerMessage.Succeed = false;
                return HandlerMessage;
            }
            HandlerMessage.Code = "01";
            HandlerMessage.Text = "添加数据失败";
            HandlerMessage.Succeed = false;
            if (Provider.Add(entity))
            {
                HandlerMessage.Code = "00";
                HandlerMessage.Text = "添加数据成功";
                HandlerMessage.Succeed = true;
            }
            return HandlerMessage;
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return the handler result</returns>
        public SystemMessage Edit(SysTag entity)
        {
            SysTag originEntity = Provider.GetData(entity.Name);
            if (originEntity != null && originEntity.ID != entity.ID)
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "相同名称的记录已经存在";
                HandlerMessage.Succeed = false;
                return HandlerMessage;
            }
            originEntity = Provider.GetData(entity.ID);
            if (originEntity == null)
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "修改的记录不存在";
                HandlerMessage.Succeed = false;
                return HandlerMessage;
            }

            HandlerMessage.Code = "01";
            HandlerMessage.Text = "修改的数据失败";
            HandlerMessage.Succeed = false;
            if (Provider.Edit(entity))
            {
                HandlerMessage.Code = "00";
                HandlerMessage.Text = "修改数据成功";
                HandlerMessage.Succeed = true;
            }
            return HandlerMessage;
        }

        #endregion

        #region GetData GetList ...

        /// <summary>
        /// GetInfo
        /// </summary>
        ///<param name="id"></param>
        /// <returns></returns>
        public SysTag GetData(int id)
        {
            try
            {
                return Provider.GetData(id);
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                //BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        /// <summary>
        /// GetInfo by name
        /// </summary>
        ///<param name="name"></param>
        /// <returns></returns>
        public SysTag GetData(string name)
        {
            try
            {
                return Provider.GetData(name);
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                //BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        /// <summary>
        /// Get All List
        /// </summary>
        /// <returns></returns>
        public List<SysTag> GetList()
        {
            try
            {
                return Provider.GetDatas();
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
               // BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        /// <summary>
        /// Get TagCategory List
        /// </summary>
        /// <returns></returns>
        public List<string> GetTagCategoryList()
        {
            try
            {
                return Provider.GetTagCategoryList();
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                //BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        /// <summary>
        /// Get All List by query
        /// </summary>
        /// <param name="exps">查询条件</param>
        /// <returns></returns>
        public List<SysTag> GetList(List<SimpleExpression> exps)
        {
            try
            {
                return Provider.GetDatas(exps);
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                //BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        /// <summary>
        /// Get Pager List
        /// </summary>
        /// <param name="pager">Pager Info</param>
        /// <returns></returns>
        public List<SysTag> GetList(PagerData pager)
        {
            try
            {
                return Provider.GetDatas(pager);
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                //BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        /// <summary>
        /// Get Pager List 默认ID倒序
        /// </summary>
        /// <param name="pager">Pager Info</param>
        /// <param name="exps">查询条件</param>
        /// <returns></returns>
        public List<SysTag> GetList(PagerData pager, List<SimpleExpression> exps)
        {
            try
            {
                OrderItemCollection orderlist = new OrderItemCollection();
                orderlist.Add(new OrderItem("ID", OrderType.DESC));
                return Provider.GetDatas(pager, orderlist, exps);
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                //BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        /// <summary>
        /// Get Pager List
        /// </summary>
        /// <param name="pager">Pager Info</param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<SysTag> GetList(PagerData pager, OrderItem order)
        {
            try
            {
                return Provider.GetDatas(pager, order);
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                //BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        /// <summary>
        /// Get Pager List
        /// </summary>
        /// <param name="pager">Pager Info</param>
        /// <param name="orders"></param>
        /// <returns></returns>
        public List<SysTag> GetList(PagerData pager, OrderItemCollection orders)
        {
            try
            {
                return Provider.GetDatas(pager, orders);
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                //BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        /// <summary>
        /// Get Pager List
        /// </summary>
        /// <param name="pager">Pager Info</param>
        /// <param name="orders"></param>
        /// <param name="exps">查询条件</param>
        /// <returns></returns>
        public List<SysTag> GetList(PagerData pager, OrderItemCollection orders, List<SimpleExpression> exps)
        {
            try
            {
                return Provider.GetDatas(pager, orders, exps);
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                //BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        #endregion

        #region remove ...

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="idList"></param>
        /// <returns>return the handler result</returns>
        public SystemMessage Remove(List<int> idList)
        {
            HandlerMessage.Code = "01";
            HandlerMessage.Text = "删除失败";
            HandlerMessage.Succeed = false;
            if (Provider.Delete(idList))
            {
                HandlerMessage.Code = "00";
                HandlerMessage.Text = "删除成功";
                HandlerMessage.Succeed = true;
            }
            return HandlerMessage;
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="idList"></param>
        /// <returns>return the handler result</returns>
        public SystemMessage Remove(List<string> idList)
        {
            HandlerMessage.Code = "01";
            HandlerMessage.Text = "删除失败";
            HandlerMessage.Succeed = false;
            if (Provider.Delete(idList))
            {
                HandlerMessage.Code = "00";
                HandlerMessage.Text = "删除成功";
                HandlerMessage.Succeed = true;
            }
            return HandlerMessage;
        }

        #endregion
    }
}