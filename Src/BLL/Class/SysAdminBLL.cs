
/******************************************************************
 *
 * 所在模块：Business Logic Layer(业务逻辑层)
 * 类 名 称：SysAdminBLL(SysAdminBLL)
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
using NHibernate.Criterion;

using Hope.ITMS.DAL;
using Hope.ITMS.Model;
using Hope.ITMS.Enums;
using Hope.Util;

namespace Hope.ITMS.BLL
{
    /// <summary>
    /// BusinessHandler SysAdminBLL
    /// </summary>
    public class SysAdminBLL : BaseBLL
    {
        private SysAdminDAL Provider = DALFactory.Instance().SysAdminDAL;

        /// <summary>
        /// SysAdminBLL
        /// </summary>
        public SysAdminBLL()
        {
        }


        #region add, edit ...

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return the handler result</returns>
        public SystemMessage Add(SysAdmin entity)
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
                Edit(entity);

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
        public SystemMessage Edit(SysAdmin entity)
        {
            SysAdmin originEntity = Provider.GetData(entity.Name);
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

            HandlerMessage.Code = "00";
            HandlerMessage.Text = "修改数据成功";
            HandlerMessage.Succeed = true; 
            if (!Provider.Edit(entity))
            {
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
        public SysAdmin GetData(int id)
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
        public List<SysAdmin> GetList()
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
        /// Get All List by query
        /// </summary>
        /// <param name="exps">查询条件</param>
        /// <returns></returns>
        public List<SysAdmin> GetList(List<SimpleExpression> exps)
        {
            try
            {
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
        /// Get All List by query order by
        /// </summary>
        /// <param name="exps">查询条件</param>
        /// <returns></returns>
        public List<SysAdmin> GetList(List<SimpleExpression> exps, OrderItemCollection orders)
        {
            try
            {
                return Provider.GetDatas(exps,orders);
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
        public List<SysAdmin> GetList(PagerData pager)
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
        /// Get Pager List 默认ID倒序
        /// </summary>
        /// <param name="pager">Pager Info</param>
        /// <param name="exps">查询条件</param>
        /// <returns></returns>
        public List<SysAdmin> GetList(PagerData pager, List<SimpleExpression> exps)
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
        public List<SysAdmin> GetList(PagerData pager, OrderItem order)
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
        public List<SysAdmin> GetList(PagerData pager, OrderItemCollection orders)
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
        public List<SysAdmin> GetList(PagerData pager, OrderItemCollection orders, List<SimpleExpression> exps)
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

        #region login ...

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="account">管理员用户名</param>
        /// <param name="password">管理员密码</param>
        /// <param name="loginUser">管理员</param>
        /// <returns></returns>
        public SystemMessage Login(string account, string password, out SysAdmin loginAdmin)
        {
            string logTitle = "";
            loginAdmin = null;
            SysAdmin data = Provider.GetLoginData(account);
            if (data == null)
            {
                //写入日志
                logTitle = "管理员" + account + "登录失败";
                BLLFactory.Instance().SysLogBLL.AddLog(logTitle, "管理员不存在", "", "", (int)LogPriority.Normal, (int)LogType.LoginFailed);
                
                HandlerMessage.Succeed = false;
                HandlerMessage.Code = "system_admin_001";
                HandlerMessage.Text = "帐号或密码不正确";
                return HandlerMessage;
            }
            loginAdmin = data;
            if (EncryptionUtil.MD5Hash(password) != loginAdmin.Password)
            {
                //写入日志
                logTitle = "管理员" + account + "登录失败";
                BLLFactory.Instance().SysLogBLL.AddLog(logTitle, "密码不正确", "", "", (int)LogPriority.Normal, (int)LogType.LoginFailed);

                //更新登录失败时间和登录失败次数
                loginAdmin.LastErrorTime = DateTime.Now;
                loginAdmin.ErrorTimes += 1;
                Edit(loginAdmin);

                loginAdmin = null;
                HandlerMessage.Succeed = false;
                HandlerMessage.Code = "system_admin_001";
                HandlerMessage.Text = "帐号或密码不正确";
                return HandlerMessage;
            }
            if (loginAdmin.RoleID <=0)
            {
                 //写入日志
                logTitle = "管理员" + account + "没有分配角色";
                BLLFactory.Instance().SysLogBLL.AddLog(logTitle, "没有分配角色", "", "", (int)LogPriority.High, (int)LogType.LoginFailed);

                HandlerMessage.Succeed = false;
                HandlerMessage.Code = "system_admin_002";
                HandlerMessage.Text = "该管理员暂时无法正常登录！";
                return HandlerMessage;
            }
            if (loginAdmin.Status == (int)ObjectStatus.Disable)
            {
                //写入日志
                logTitle = "管理员" + account + "被禁用后尝试登录";
                BLLFactory.Instance().SysLogBLL.AddLog(logTitle, "管理员被禁用", "", "", (int)LogPriority.High, (int)LogType.LoginFailed);

                loginAdmin = null;

                HandlerMessage.Succeed = false;
                HandlerMessage.Code = "system_admin_002";
                HandlerMessage.Text = "帐号被禁用，请与管理员联系";
                return HandlerMessage;
            }
            //loginAdmin.Password = "";

            //写入日志
            logTitle = "管理员" + account + "登录成功";
            string logMsg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "登录成功";
            BLLFactory.Instance().SysLogBLL.AddLog(logTitle, logMsg, "", "", (int)LogPriority.Normal, (int)LogType.LoginSucceed);

            // 更新管理员信息
            loginAdmin.LastLoginTime = DateTime.Now; // 更新最后登录时间
            loginAdmin.LoginTimes += 1;    // 登录次数加1
            BLLFactory.Instance().SysAdminBLL.Edit(loginAdmin);

            HandlerMessage.Succeed = true;
            HandlerMessage.Code = "system_admin_003";
            HandlerMessage.Text = "登录系统成功";
            return HandlerMessage;
        }

        #endregion

        #region change password ...

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public SystemMessage ChangePassword(string id, string oldPassword, string newPassword)
        {
            HandlerMessage.Succeed = false;
            SysAdmin data = DALFactory.Instance().SysAdminDAL.GetData(id);
            if (data == null)
            {
                HandlerMessage.Code = "system_admin_012";
                HandlerMessage.Text = "修改的用户信息不存在";
                return HandlerMessage;
            }
            else if (newPassword.Length < 6 || newPassword.Length > 20)
            {
                HandlerMessage.Code = "system_admin_024";
                HandlerMessage.Text = "用户密码必须为6-20个字符";
                return HandlerMessage;
            }
            oldPassword = EncryptionUtil.EncryptPassword(oldPassword);
            if (oldPassword != data.Password)
            {
                HandlerMessage.Code = "system_admin_025";
                HandlerMessage.Text = "旧密码不正确";
                return HandlerMessage;
            }

            data.Password = EncryptionUtil.EncryptPassword(newPassword);
            if (!DALFactory.Instance().SysAdminDAL.Edit(data))
            {
                HandlerMessage.Code = "system_admin_026";
                HandlerMessage.Text = "修改密码失败";
                return HandlerMessage;
            }

            HandlerMessage.Code = "system_admin_027";
            HandlerMessage.Text = "修改密码成功";
            HandlerMessage.Succeed = true;
            return HandlerMessage;
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


