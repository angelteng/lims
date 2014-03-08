
/******************************************************************
 *
 * 所在模块：Business Logic Layer(业务逻辑层)
 * 类 名 称：SysModuleBLL(SysModuleBLL)
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

namespace Hope.ITMS.BLL
{
    /// <summary>
    /// BusinessHandler SysModuleBLL
    /// </summary>
    public class SysModuleBLL : BaseBLL
    {
        private SysModuleDAL Provider = DALFactory.Instance().SysModuleDAL;

        /// <summary>
        /// SysModuleBLL
        /// </summary>
        public SysModuleBLL()
        {
        }


        #region add, edit ...

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return the handler result</returns>
        public SystemMessage Add(SysModule entity)
        {
            if (Provider.GetData(entity.ModuleName) != null)
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
        public SystemMessage Edit(SysModule entity)
        {
            SysModule originEntity = Provider.GetData(entity.ModuleName);
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
        public SysModule GetData(int id)
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
        /// Get All List
        /// </summary>
        /// <returns></returns>
        public List<SysModule> GetList()
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
        /// Get Pager List
        /// </summary>
        /// <param name="pager">Pager Info</param>
        /// <returns></returns>
        public List<SysModule> GetList(PagerData pager)
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
        public List<SysModule> GetList(PagerData pager, OrderItem order)
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
        public List<SysModule> GetList(PagerData pager, OrderItemCollection orders)
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
        /// 根据ParentID获取全部记录 
        /// </summary>
        /// <param name="ParentID">类型: int, HopeCMS.Model.PermissionModule.ModuleID</param>
        /// <returns>HopeCMS.Model.PermissionModule 实例列表</returns>
        public List<SysModule> GetListByParentID(int ParentID)
        {
            try
            {
                return Provider.GetListByParentID(ParentID);
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


