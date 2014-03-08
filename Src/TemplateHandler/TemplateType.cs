/******************************************************************
 *
 * 所在模块：Enums(系统枚举模块)
 * 类 名 称：TemplateType(模板类型)
 * 功能描述：系统模块的类型
 * 
 * ------------创建信息------------------
 * 作    者：Nick
 * 日    期：2009-08-06
 * ajj82@163.com
 * MSN:ajj82@163.com
 * QQ:46810878
 * ------------编辑修改信息--------------
 * 作    者：Fernando Hua
 * 日    期：2010-04-09
 * 内    容：增加投票模板类型
******************************************************************/
using System;

namespace Hope.TemplateUtil
{
    /// <summary>
    /// 系统模板类型
    /// </summary>
    public enum TemplateType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknow = 0,

        /// <summary>
        /// 首页模板
        /// </summary>
        Tem_Index = 1,

        /// <summary>
        /// 网站公告弹出页模板
        /// </summary>
        Tem_Announce = 2,

        ///// <summary>
        ///// 网站公告内容页模板
        ///// </summary>
        //Tem_AnnounceItem = 3,

        
        ///// <summary>
        ///// 网站公告列表页模板
        ///// </summary>
        //Tem_AnnounceList = 4,


        ///// <summary>
        ///// 分类根节点首页模板
        ///// </summary>
        //Tem_NodeIndex = 5,

        /// <summary>
        /// 分类根节点栏目页模板（含有子栏目的情况）
        /// </summary>
        //Tem_NodeCategoryChild = 7,

        /// <summary>
        /// 分类根节点栏目页模板（有无子栏目）
        /// </summary>
        Tem_Node = 8,

        /// <summary>
        /// 分类根节点栏目页模板（不含有子栏目的情况）
        /// </summary>
        //Tem_NodeCategory = 9,


        /// <summary>
        /// 节点内容页模板
        /// </summary>
        Tem_NodeContent = 11,

        /// <summary>
        /// 搜索结果页
        /// </summary>
        Tem_SearchResult = 13,

        /// <summary>
        /// 问卷调查结果页模板
        /// </summary>
        Tem_SurveyResult = 14,

        /// <summary>
        /// 投票结果页模板
        /// </summary>
        Tem_VoteResult = 15,

        /// <summary>
        /// 专题页模板
        /// </summary>
        Tem_Special = 16,

        ///// <summary>
        ///// 留言板首页模板
        ///// </summary>
        //Tem_GuestBook_Index = 20,


        /// <summary>
        /// 发表留言模板
        /// </summary>
        Tem_GuestBook_Add = 22,


        ///// <summary>
        ///// 留言回复模板
        ///// </summary>
        //Tem_GuestBook_Reply = 24,


        /// <summary>
        /// 留言搜索页模板
        /// </summary>
        //Tem_GuestBook_SearchResult = 26,

        /// <summary>
        /// 会员注册表单模板
        /// </summary>
        Tem_User_RegForm = 31,

        /// <summary>
        /// 会员注册结果模板
        /// </summary>
        Tem_User_RegResult = 33,

        /// <summary>
        /// 会员登录模板
        /// </summary>
        Tem_User_Login = 35,

        /// <summary>
        /// 文章评论页模板
        /// </summary>
        Tem_Comment = 40,

        ///// <summary>
        ///// 自定义栏目模板
        ///// </summary>
        //Tem_Custom = 999,

        ///// <summary>
        ///// 自定义内容模板
        ///// </summary>
        //Tem_CustomContent = 990,

        /// <summary>
        /// 自定义表单模板
        /// </summary>
        Tem_CustomForm = 99,
    }

    /// <summary>
    /// 系统模板类型
    /// </summary>
    public enum TemplateTypeCN
    {
        /// <summary>
        /// 未知
        /// </summary>
        未知 = 0,

        /// <summary>
        /// 首页模板
        /// </summary>
        首页模板 = 1,

        /// <summary>
        /// 网站公告弹出页模板
        /// </summary>
        网站公告弹出页模板 = 2,

        ///// <summary>
        ///// 网站公告模板
        ///// </summary>
        //网站公告内容页模板 = 3,

        ///// <summary>
        ///// 网站公告列表页模板
        ///// </summary>
        //网站公告列表页模板 = 4,

        ///// <summary>
        ///// 分类根节点首页模板
        ///// </summary>
        //分类根节点首页模板 = 5,

        /// <summary>
        /// 分类根节点栏目页模板（含有子栏目的情况）
        /// </summary>
        //分类根节点栏目页模板_含有子栏目 = 7,
        
        /// <summary>
        /// 分类根节点栏目页模板
        /// </summary>
        分类节点栏目页模板 = 8,

        /// <summary>
        /// 分类根节点栏目页模板（不含有子栏目的情况）
        /// </summary>
        //分类根节点栏目页模板_不含有子栏目 = 9,


        /// <summary>
        /// 节点内容页模板
        /// </summary>
        节点内容页模板 = 11,

        /// <summary>
        /// 搜索页
        /// </summary>
        搜索结果页模板 = 13,

        /// <summary>
        /// 问卷调查结果页模板
        /// </summary>
        问卷调查结果页模板 = 14,

        /// <summary>
        /// 投票结果页模板
        /// </summary>
        投票结果页模板 = 15,

        /// <summary>
        /// 专题页模板
        /// </summary>
        专题页模板 = 16,

        ///// <summary>
        ///// 留言板首页模板
        ///// </summary>
        //留言板首页模板 = 20,


        /// <summary>
        /// 发表留言模板
        /// </summary>
        发表留言模板 = 22,


        ///// <summary>
        ///// 留言回复模板
        ///// </summary>
        //留言回复模板 = 24,


        /// <summary>
        /// 留言搜索页模板
        /// </summary>
        //留言搜索页模板 = 26,

        /// <summary>
        /// 会员注册表单模板
        /// </summary>
        会员注册表单模板 = 31,

        /// <summary>
        /// 会员注册结果模板
        /// </summary>
        会员注册结果模板 = 33,

        /// <summary>
        /// 会员登录模板
        /// </summary>
        会员登录模板 = 35,

        /// <summary>
        /// 文章评论页模板
        /// </summary>
        评论回复页模板 = 40,

        ///// <summary>
        ///// 自定义栏目页模板
        ///// </summary>
        //自定义栏目页模板 = 999,

        ///// <summary>
        ///// 自定义内容页模板
        ///// </summary>
        //自定义内容页模板 = 990,

        /// <summary>
        /// 自定义表单模板
        /// </summary>
        自定义表单模板 = 99,
    }
}
