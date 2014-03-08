
/******************************************************************
 *
 * 所在模块：Business Logic Layer(业务逻辑层)
 * 类 名 称：SysPermissionBLL(SysPermissionBLL)
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
using System.Text;

using Hope.ITMS.DAL;
using Hope.ITMS.Model;
using Hope.ITMS.Enums;
using Hope.Util;
using NHibernate.Criterion;

namespace Hope.ITMS.BLL
{
    /// <summary>
    /// BusinessHandler SysPermissionBLL
    /// </summary>
    public class SysPermissionBLL : BaseBLL
    {
        private SysPermissionDAL Provider = DALFactory.Instance().SysPermissionDAL;

        /// <summary>
        /// SysPermissionBLL
        /// </summary>
        public SysPermissionBLL()
        {
        }


        #region add, edit ...

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return the handler result</returns>
        public SystemMessage Add(SysPermission entity)
        {
            HandlerMessage.Succeed = false;
            if (!Provider.Add(entity))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "添加数据失败";
                return HandlerMessage;
            }
            HandlerMessage.Code = "00";
            HandlerMessage.Text = "添加数据成功";
            HandlerMessage.Succeed = true;
            return HandlerMessage;
        }

        /// <summary>
        /// 更新数据列表
        /// </summary>
        /// <param name="List">类型: Model.SysPermission 实例列表</param>
        /// <returns>return the handler result</returns>
        public SystemMessage BatchUpdateRolePValues(List<SysPermission> List)
        {
            HandlerMessage.Code = "00";
            HandlerMessage.Text = "编辑权限成功！";
            HandlerMessage.Succeed = true;
            if (!Provider.BatchUpdateRolePValues(List))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "编辑权限失败！";
                HandlerMessage.Succeed = false;
                return HandlerMessage;
            }
            return HandlerMessage;
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return the handler result</returns>
        public SystemMessage Edit(SysPermission entity)
        {
            HandlerMessage.Succeed = false;
            if (!Provider.Edit(entity))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "修改失败";
                return HandlerMessage;
            }
            HandlerMessage.Code = "00";
            HandlerMessage.Text = "修改成功";
            HandlerMessage.Succeed = true;
            return HandlerMessage;
        }

        #endregion

        #region Get Data
        /// <summary>
        /// 根据模块和角色ID获取权限信息
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public SysPermission GetData(int ModuleID, int RoleID)
        {
            List<SimpleExpression> exps = new List<SimpleExpression>();
            exps.Add(Expression.Eq("ModuleID", ModuleID));
            exps.Add(Expression.Eq("RoleID", RoleID));

            return Provider.GetDataByQuery(exps);
        }
        #endregion

        #region GetData GetList ...


        /// <summary>
        /// Get All List
        /// </summary>
        /// <returns></returns>
        public List<SysPermission> GetList()
        {
            return Provider.GetDatas();
        }

        /// <summary>
        /// Get All List by roleID
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <returns></returns>
        public List<SysPermission> GetList(int RoleID)
        {
            List<SimpleExpression> exps = new List<SimpleExpression>();
            exps.Add(Expression.Eq("RoleID", RoleID));
            return Provider.GetDatas(exps);
        }
        #endregion

        #region remove ...

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <returns>return the handler result</returns>
        public SystemMessage RemoveRole(int RoleID)
        {
            HandlerMessage.Code = "00";
            HandlerMessage.Text = "删除成功！";
            HandlerMessage.Succeed = true;

            List<SimpleExpression> exps = new List<SimpleExpression>();
            exps.Add(Expression.Eq("RoleID", RoleID));
            if (!Provider.Delete(exps))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "删除失败！";
                HandlerMessage.Succeed = false;
                return HandlerMessage;
            }
            return HandlerMessage;
        }

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="ModuleID">模块ID</param>
        /// <returns>return the handler result</returns>
        public SystemMessage RemoveModule(int ModuleID)
        {
            HandlerMessage.Code = "00";
            HandlerMessage.Text = "删除成功！";
            HandlerMessage.Succeed = true;

            List<SimpleExpression> exps = new List<SimpleExpression>();
            exps.Add(Expression.Eq("ModuleID", ModuleID));
            if (!Provider.Delete(exps))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "删除失败！";
                HandlerMessage.Succeed = false;
                return HandlerMessage;
            }
            return HandlerMessage;
        }

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <param name="ModuleID">模块ID</param>
        /// <param name="RoleID">角色ID</param>
        /// <returns>return the handler result</returns>
        public SystemMessage Remove(int ModuleID, int RoleID)
        {
            HandlerMessage.Code = "00";
            HandlerMessage.Text = "删除成功！";
            HandlerMessage.Succeed = true;

            List<SimpleExpression> exps = new List<SimpleExpression>();
            exps.Add(Expression.Eq("ModuleID", ModuleID));
            exps.Add(Expression.Eq("RoleID", RoleID));
            if (!Provider.Delete(exps))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "删除失败！";
                HandlerMessage.Succeed = false;
                return HandlerMessage;
            }
            return HandlerMessage;
        }

        #endregion
    }
}


