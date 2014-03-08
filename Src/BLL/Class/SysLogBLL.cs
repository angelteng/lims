/******************************************************************
 *
 * 所在模块：Business Logic Layer(业务逻辑层)
 * 类 名 称：SysLogBLL(SysLogBLL)
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
using System.Web;
using System.Collections.Generic;

using Hope.ITMS.DAL;
using Hope.ITMS.Model;
using Hope.ITMS.Enums;
using Hope.Util;
using NHibernate.Criterion;

namespace Hope.ITMS.BLL
{
	/// <summary>
	/// BusinessHandler SysLogBLL
	/// </summary>
	public class SysLogBLL:BaseBLL
	{
		private SysLogDAL Provider = DALFactory.Instance().SysLogDAL;
		
		/// <summary>
		/// SysLogBLL
		/// </summary>
		public SysLogBLL()
		{
		}
		
		
		#region add, edit ...
		
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="entity"></param>
		/// <returns>return the handler result</returns>
		public SystemMessage Add(SysLog entity)
		{			
			try
			{
				Provider.Add(entity);
				HandlerMessage.Code = "00";
				HandlerMessage.Text = "添加数据成功";
				HandlerMessage.Succeed = true;
			}
			catch(Exception)
			{				
				HandlerMessage.Code = "01";
				HandlerMessage.Text = "添加数据失败";
				HandlerMessage.Succeed = false;
			}
			return HandlerMessage;
		}

		 /// <summary>
		/// 记录日志
		/// </summary>
		/// <param name="title">日志标题</param>
		/// <param name="message">日志内容</param>
		/// <param name="source">日志代码</param>
		/// <param name="postScript">日志请求路径</param>
		/// <param name="priority">优先级</param>
		/// <param name="type">类型</param>
		/// <returns>成功|失败</returns>
		public bool AddLog(string title, string message, string source, string postScript, int priority, int type)
		{
			SysLog data = new SysLog();
			data.Title = title;
			data.Message = message;
			data.Source = source;
			data.PostString = postScript;
			data.Priority = priority;
			data.Type = type;

			//set default
			data.UserIP = CommonHelper.IPAddress;                  //访问IP
			data.ScriptName = HttpContext.Current.Request.Url.ToString().ToLower().Replace("http://" + CommonHelper.GetServerHost, "");    //访问页面地址 
			data.Timestamp = HttpContext.Current.Timestamp;       //请求时间

			if (HttpContext.Current.Session != null)
			{
				if (HttpContext.Current.Session["_AdminInfo"] != null)
				{
					SysAdmin admin = (SysAdmin)HttpContext.Current.Session["_AdminInfo"];
					data.UserName = admin.Name;
				}
                else if (HttpContext.Current.Session["_UserInfo"] != null)
                {
                    OrgUser user = (OrgUser)HttpContext.Current.Session["_UserInfo"];
                    data.UserName = user.Email;
                }
				else
				{
					data.UserName = "";
				}
			}

			try
			{
				Provider.Add(data);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}
		
		/// <summary>
		/// Edit
		/// </summary>
		/// <param name="entity"></param>
		/// <returns>return the handler result</returns>
		public SystemMessage Edit(SysLog entity)
		{ 
		    SysLog originEntity = Provider.GetData(entity.ID);
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
			catch(Exception)
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
		public SysLog GetData(int id)
		{           
			try
			{
				return Provider.GetData(id);
			}
			catch(Exception)
			{
				return null;
			}
		}
		
		/// <summary>
		/// Get All List
		/// </summary>
		/// <returns></returns>
		public List<SysLog> GetList()
		{
			try
			{
				return Provider.GetDatas();
			}
			catch(Exception)
			{
				return null;
			}
		}

        /// <summary>
        /// Get All List
        /// </summary>
        /// <param name="exps">查询条件</param>
        /// <returns></returns>
        public List<SysLog> GetList(List<SimpleExpression> exps)
        {
            try
            {
                return Provider.GetDatas(exps);
            }
            catch (Exception)
            {
                return null;
            }
        }
		
		/// <summary>
		/// Get Pager List
		/// </summary>
		/// <param name="pager">Pager Info</param>
		/// <returns></returns>
		public List<SysLog> GetList(PagerData pager)
		{
			try
			{
				return Provider.GetDatas(pager);
			}
			catch(Exception)
			{
				return null;
			}
		}

        /// <summary>
        /// Get Pager List
        /// </summary>
        /// <param name="pager">Pager Info</param>
        /// <param name="exps">查询条件</param>
        /// <returns></returns>
        public List<SysLog> GetList(PagerData pager, List<SimpleExpression> exps)
        {
            try
            {
                OrderItemCollection orderlist = new OrderItemCollection();
                orderlist.Add(new OrderItem("ID", OrderType.DESC));
                return Provider.GetDatas(pager, orderlist, exps);
            }
            catch (Exception)
            {
                return null;
            }
        }
		
		/// <summary>
		/// Get Pager List
		/// </summary>
		/// <param name="pager">Pager Info</param>
		/// <param name="order"></param>
		/// <returns></returns>
		public List<SysLog> GetList(PagerData pager,OrderItem order)
		{
			try
			{
				return Provider.GetDatas(pager, order);
			}
			catch(Exception)
			{
				return null;
			}
		}
		
		/// <summary>
		/// Get Pager List
		/// </summary>
		/// <param name="pager">Pager Info</param>
		/// <param name="orders"></param>
		/// <returns></returns>
		public List<SysLog> GetList(PagerData pager,OrderItemCollection orders)
		{
			try
			{
				return Provider.GetDatas(pager, orders);
			}
			catch(Exception)
			{
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
			catch(Exception)
			{
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
			catch(Exception)
			{
				HandlerMessage.Code = "01";
				HandlerMessage.Text = "删除失败";
				HandlerMessage.Succeed = false;
			}
			return HandlerMessage;
		}
		
		#endregion
	}
}