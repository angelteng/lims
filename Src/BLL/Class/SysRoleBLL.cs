
/******************************************************************
 *
 * 所在模块：Business Logic Layer(业务逻辑层)
 * 类 名 称：SysRoleBLL(SysRoleBLL)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：wensaint
 * 日    期：2012-05-23
 * wensaint@126.com
 * QQ:286661274
 * ------------编辑修改信息--------------
 * 作    者：
 * 日    期：
 * 内    容：
******************************************************************/
using System;
using System.Collections.Generic;

using Hope.ITMS.DAL;
using Hope.ITMS.Model;
using Hope.ITMS.Enums;
using Hope.Util;
using NHibernate.Criterion;

namespace Hope.ITMS.BLL
{
    /// <summary>
    /// BusinessHandler SysRoleBLL
    /// </summary>
    public class SysRoleBLL : BaseBLL
    {
        private SysRoleDAL Provider = DALFactory.Instance().SysRoleDAL;

        /// <summary>
        /// SysRoleBLL
        /// </summary>
        public SysRoleBLL()
        {
        }


        #region add, edit ...

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return the handler result</returns>
        public SystemMessage Add(SysRole entity)
        {
            if (Provider.GetData(entity.Name) != null)
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "相同名称的记录已经存在";
                HandlerMessage.Succeed = false;
                return HandlerMessage;
            }
            try
            {
                Provider.Add(entity);
                HandlerMessage.Code = "00";
                HandlerMessage.Text = "添加数据成功";
                HandlerMessage.Succeed = true;
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "添加数据失败";
                HandlerMessage.Succeed = false;
            }
            return HandlerMessage;
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return the handler result</returns>
        public SystemMessage Edit(SysRole entity)
        {
            SysRole originEntity = Provider.GetData(entity.Name);
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

            try
            {
                Provider.Edit(entity);
                HandlerMessage.Code = "00";
                HandlerMessage.Text = "修改数据成功";
                HandlerMessage.Succeed = true;
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "修改的数据失败";
                HandlerMessage.Succeed = false;
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
        public SysRole GetData(int id)
        {
            try
            {
                return Provider.GetData(id);
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        /// <summary>
        /// 检查当前管理员是否为超级管理员
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public bool IsSuperRole(int RoleID)
        {
            bool bResult = false;

            SysRole tempData = Provider.GetData(RoleID);
            if (tempData.ID == 1)    //角色ID＝1 表示其为超级管理员，拥有所有权限
            {
                bResult = true;
            }

            return bResult;
        }

        /// <summary>
        /// Get All List
        /// </summary>
        /// <returns></returns>
        public List<SysRole> GetList()
        {
            try
            {
                return Provider.GetDatas();
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        /// <summary>
        /// Get All List 用于角色配置页面
        /// </summary>
        /// <returns></returns>
        public List<SysRole> GetList(int roleID)
        {
            try
            {
                List<SimpleExpression> exps = new List<SimpleExpression>();
                if (roleID != 1)
                {
                    exps.Add(NHibernate.Criterion.Expression.Gt("ID", 1));
                }
                return Provider.GetDatas(exps);
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        /// <summary>
        /// Get Pager List
        /// </summary>
        /// <param name="pager">Pager Info</param>
        /// <returns></returns>
        public List<SysRole> GetList(PagerData pager)
        {
            try
            {
                return Provider.GetDatas(pager);
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        /// <summary>
        /// Get Pager List
        /// </summary>
        /// <param name="pager">Pager Info</param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<SysRole> GetList(PagerData pager, OrderItem order)
        {
            try
            {
                return Provider.GetDatas(pager, order);
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                return null;
            }
        }

        /// <summary>
        /// Get Pager List
        /// </summary>
        /// <param name="pager">Pager Info</param>
        /// <param name="orders"></param>
        /// <returns></returns>
        public List<SysRole> GetList(PagerData pager, OrderItemCollection orders)
        {
            try
            {
                return Provider.GetDatas(pager, orders);
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
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
        public List<SysRole> GetList(PagerData pager, OrderItemCollection orders, List<SimpleExpression> exps)
        {
            try
            {
                return Provider.GetDatas(pager, orders, exps);
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
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
            try
            {
                Provider.Delete(idList);
                HandlerMessage.Code = "00";
                HandlerMessage.Text = "删除成功";
                HandlerMessage.Succeed = true;
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "删除失败";
                HandlerMessage.Succeed = false;
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
            try
            {
                Provider.Delete(idList);
                HandlerMessage.Code = "00";
                HandlerMessage.Text = "删除成功";
                HandlerMessage.Succeed = true;
            }
            catch (Exception ex)
            {
                // 将错误日志写入数据库
                BLLFactory.Instance().SysLogBLL.AddLog(ex.TargetSite.ToString(), ex.Message, ex.Source, ex.StackTrace, (int)LogPriority.High, (int)LogType.Exception);
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "删除失败";
                HandlerMessage.Succeed = false;
            }
            return HandlerMessage;
        }

        #endregion
    }
}


