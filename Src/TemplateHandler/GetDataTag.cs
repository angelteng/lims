

using System.Collections.Generic;
using System.Linq;
using Hope.ITMS.Model;
using Hope.ITMS.BLL;
using Hope.ITMS;
using Hope.ITMS.Model.Enum;
using Hope.Util;
using Hope.ITMS.Enums;
using NHibernate.Criterion;

namespace Hope.TemplateUtil
{
    /// <summary>
    /// 关联表标签
    /// </summary>
    public partial class HopeTag
    {      
        #region 获取管理员
        /// <summary>
        /// 获取管理员
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>
        /// 调用实例：${HopeTag.GetAdmin(1)}
        /// </returns>
        public SysAdmin GetAdmin(int id)
        {
            SysAdmin data = BLLFactory.Instance().SysAdminBLL.GetData(id);
            return data;
        }

        /// <summary>
        /// 获取管理员真实姓名
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>
        /// 调用实例：${HopeTag.GetAdminName(1)}
        /// </returns>
        public string GetAdminName(int id)
        {
            SysAdmin data = BLLFactory.Instance().SysAdminBLL.GetData(id);
            return data == null ? "" : data.RealName;
        }
        #endregion

        #region 获取角色
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>
        /// 调用实例：${HopeTag.GetRole(1)}
        /// </returns>
        public SysRole GetRole(int id)
        {
            SysRole data = BLLFactory.Instance().SysRoleBLL.GetData(id);
            return data;
        }

        /// <summary>
        /// 获取角色中文名称
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>
        /// 调用实例：${HopeTag.GetRole(1)}
        /// </returns>
        public string GetRoleCNName(int id)
        {
            SysRole data = BLLFactory.Instance().SysRoleBLL.GetData(id);
            return data == null ? "" : data.CNName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SysRole> GetRoleList()
        {
            return BLLFactory.Instance().SysRoleBLL.GetList();
        }

        #endregion
        
        #region 从属性集合中查找对应属性值
        /// <summary>
        /// 从属性集合中查找对应属性值
        /// <param name="name">属性名</param>
        /// <param name="properties">属性集合</param>
        /// </summary>
        /// <returns>
        /// 调用实例：${HopeTag.GetFieldValue("Name",properties)}
        /// </returns>
        public string GetFieldValue(string name,IDictionary<string, string> properties)
        {
            return PropertiesHelper.GetString(name, properties);
        }
        #endregion

        #region 获取用户ID

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrgUser GetUser(int id)
        {
            return BLLFactory.Instance().OrgUserBLL.GetData(id);
        }

        #endregion


        #region 获取用户组

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrgGroup GetGroup(int id)
        {
            return BLLFactory.Instance().OrgGroupBLL.GetData(id);
        }

        #endregion
        
        #region 获取用户组集合

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrgGroup> GetGroupList()
        {
            return BLLFactory.Instance().OrgGroupBLL.GetList();
        }

        #endregion

        #region 获取模型

        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public CommonModel GetModel(int modelId)
        {
            return BLLFactory.Instance().CommonModelBLL.GetData(modelId);
        }

        #endregion

        #region 获取用户模型集合

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CommonModel> GetUserModelList()
        {
            List<CommonModel> list = BLLFactory.Instance().CommonModelBLL.GetList();
            
            return from model in list where model.Type == (int) ModelType.UserModel select model;
        }

        #endregion

        #region 获取任务集合

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TaskTaskType> GetTaskTypeList()
        {
            List<SimpleExpression> exps = new List<SimpleExpression>();
            exps.Add(Restrictions.Gt("ID", 1));

            return BLLFactory.Instance().TaskTaskTypeBLL.GetList(exps);
        }

        #endregion
        
        #region 获取任务类别

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TaskTaskType GetTaskType(int id)
        {
            return BLLFactory.Instance().TaskTaskTypeBLL.GetData(id);
        }

        #endregion


    }
}