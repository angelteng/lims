using System;
using System.Collections;
using System.IO;
using System.Web;

using Hope.Util;
using Hope.WebBase;

using Commons.Collections;

using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;
using NVelocity.Context;
using NVelocity.Runtime.Parser.Node;
using Hope.ITMS.Model;
using Hope.ITMS.BLL;

namespace Hope.TemplateUtil
{
    /// <summary>
    /// 模板处理
    /// </summary>
    public sealed class VelocityHelper
    {
        private static readonly VelocityHelper theInstance = new VelocityHelper();
        private string templateDir = AppConfig.TemplateRoot;
        private HopeTag HopeTag = new HopeTag();
        private VelocityEngine velocity = null;
        private IContext context = null;

        #region Constructor
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="templatDirectory">模板文件夹路径</param>
        public VelocityHelper(string templatDirectory)
        {
            templateDir = templatDirectory;
            Init();
        }
        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public VelocityHelper()
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public void Init()
        {
            //创建VelocityEngine实例对象
            velocity = new VelocityEngine();

            //使用设置初始化VelocityEngine
            ExtendedProperties props = new ExtendedProperties();
            props.AddProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            //可换成：props.AddProperty("resouce.loader","file"),以下的同道理
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, templateDir);
            props.AddProperty(RuntimeConstants.INPUT_ENCODING, "utf-8");
            props.AddProperty(RuntimeConstants.OUTPUT_ENCODING, "utf-8");
            velocity.Init(props);

            //为模板变量赋值
            context = new VelocityContext();
        }

        #endregion

        /// <summary>
        /// TemplateHandler实例
        /// </summary>
        /// <returns></returns>
        public static VelocityHelper Instance()
        {
            return theInstance;
        }

        /// <summary>
        /// 避免循环套用标签而采用的标志
        /// </summary>
        private Hashtable noRecursiveFlag = new Hashtable();

        #region 模板

        /// <summary>
        /// 给模板变量赋值
        /// </summary>
        /// <param name="key">模板变量</param>
        /// <param name="value">模板变量值</param>
        public void Put(string key, object value)
        {
            if (context == null)
                context = new VelocityContext();
            context.Put(key, value);
        }

        /// <summary>
        /// 处理模板
        /// </summary>
        /// <param name="temFile">模板名称</param>
        /// <returns></returns>
        public string BuildTemplate(string temFile)
        {
            noRecursiveFlag.Clear();// 清空标志位

            string sHtml = "";
            try
            {
                if (File.Exists(templateDir + "/" + temFile)) //判断文件是否存在 
                { 
                    //从文件中读取模板
                    Template temp = velocity.GetTemplate(temFile);

                    context.Put("HopeTag", CreateHopeTag());

                    //读取模板里的放置的标签信息
                    ASTprocess astProcess = (ASTprocess)temp.Data;
                    for (int i = 0; i < astProcess.ChildrenCount; i++)
                    {
                        //ASTReference类为我们添加的标签
                        if ("NVelocity.Runtime.Parser.Node.ASTReference".Equals(astProcess.GetChild(i).ToString()))
                        {
                            //1.如果标签的写法为${},FirstToken取出来的是“${”，第一个标签内容放在next，这个时候取出来的不带$，直接是标签的第一段
                            //eg: ${AAAA},FirstToken = "${", next = "AAAA";
                            //2.如果标签的写法是$,FirstToken取出来的的是$加上第一段标签名，所以需要去掉第一个字符“$”
                            //eg: $AAAA,FirstToken = "$AAAA"
                            string sTagName = astProcess.GetChild(i).FirstToken.ToString(); //添加的标签名称

                            if (sTagName.Equals("${"))
                            {
                                sTagName = astProcess.GetChild(i).FirstToken.Next.ToString();
                            }
                            else
                            {
                                sTagName = sTagName.Substring(1, sTagName.Length - 1);
                            }
                            if (CheckCustomTag(sTagName))
                            {
                                string sTagValue = GetTagProcessStr(sTagName);//
                                context.Put(sTagName, sTagValue);
                            }
                        }
                    }

                    StringWriter writer = new StringWriter();

                    temp.Merge(context, writer);

                    sHtml = writer.ToString();
                }
                else    //文件不存在
                {
                    sHtml = temFile + "  模板文件不存在！";
                }
            }
            catch (Exception ex)
            {
                LogUtil.fatal("BuildTemplate：" + ex.Message + ex.StackTrace);
                //throw;
                sHtml = "模板解析错误：" + ex.Message + ex.StackTrace;
            }
            return sHtml;
        }
        #endregion

        #region 标签

        /// <summary>
        /// 创建HopeTag实例，并传入外部值
        /// </summary>
        /// <returns></returns>
        public HopeTag CreateHopeTag()
        {
            if (HopeTag == null)
            {
                HopeTag = new HopeTag();
            }
            HopeTag.TemplateHandlerInstance = theInstance;
            HopeTag.ShowDate = _ShowDate;            

            return HopeTag;
        }

        /// <summary>
        /// 处理自定义标签，从数据库查找是否有定义，没有定义的话返回标签名称
        /// </summary>
        /// <param name="sTagName">标签名称，不带特殊符号</param>
        /// <returns>标签结果字符串</returns>        
        private string GetTagProcessStr(string sTagName)
        {
            // 如果嵌套使用标签，则直接退出，返回空字符串
            if (noRecursiveFlag.ContainsKey(sTagName))
            {
                return string.Format("标签{0}重复调用", sTagName);
            }

            noRecursiveFlag.Add(sTagName, sTagName);   //将标签名写入   

            string sTagValue = "";
            SysTag tagData = BLLFactory.Instance().SysTagBLL.GetData(sTagName);
            string tagFile = GetTagFileName(tagData);
            if (tagData != null && File.Exists(tagFile))
            {
                string tagFileName = GetRelativeTagFileName(tagData);
                
                //从文件中读取标签
                Template temp = velocity.GetTemplate(tagFileName);

                context.Put("HopeTag", CreateHopeTag());

                //读取模板里的放置的标签信息
                ASTprocess astProcess = (ASTprocess)temp.Data;
                for (int i = 0; i < astProcess.ChildrenCount; i++)
                {
                    //ASTReference类为我们添加的标签
                    if ("NVelocity.Runtime.Parser.Node.ASTReference".Equals(astProcess.GetChild(i).ToString()))
                    {
                        string sTemTagName = astProcess.GetChild(i).FirstToken.ToString(); //添加的标签名称
                        if (sTemTagName.Equals("${"))
                        {
                            sTemTagName = astProcess.GetChild(i).FirstToken.Next.ToString();
                        }
                        else
                        {
                            sTemTagName = sTemTagName.Substring(1, sTemTagName.Length - 1);
                        }
                        if (CheckCustomTag(sTemTagName))
                        {
                            //递归出来嵌套标签
                            string sTemTagValue = GetTagProcessStr(sTemTagName);
                            context.Put(sTemTagName, sTemTagValue);
                        }
                    }
                }

                StringWriter writer = new StringWriter();

                temp.Merge(context, writer);

                sTagValue = writer.ToString();
            }
            else
            {
                sTagValue = "$" + sTagName;
            }

            noRecursiveFlag.Remove(sTagName);   //将标签名移除
            return sTagValue;
        }

        /// <summary>
        /// 检查标签是否为自定义标签
        /// </summary>
        /// <param name="sTagName">标签名称</param>
        /// <returns>是|否</returns>
        private bool CheckCustomTag(string sTagName)
        {
            if (sTagName.Length < 3)
            {
                return false;
            }
            string sPriMark = sTagName.Substring(0, 3);

            return sPriMark.Equals("My_");
        }

        #endregion

        #region 属性

        private int _CurrentPageIndex;
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                return _CurrentPageIndex;
            }
            set
            {
                _CurrentPageIndex = value;
            }
        }


        private DateTime _ShowDate;
        /// <summary>
        /// 显示日期
        /// </summary>
        public DateTime ShowDate
        {
            get
            {
                return _ShowDate;
            }
            set
            {
                _ShowDate = value;
            }
        }

        #endregion

        #region 根据模板关联信息，输出Html

        /// <summary>
        /// 根据模板关联信息，输出Html
        /// </summary>
        /// <param name="templateFileName"></param>
        /// <returns></returns>
        public void GetResponsePageHtml(string templateFileName)
        {
            string sHtml =BuildTemplate(templateFileName); //处理模板 

            HttpContext.Current.Response.Write(sHtml);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 取得标签文件路径

        /// <summary>
        /// 取得有规则的标签文件完整路径
        /// </summary>
        /// <param name="tagData">标签对象，类型：Hope.ORS.Model.SysTag</param>
        /// <returns>标签文件完整路径,类型：string</returns>
        public string GetTagFileName(SysTag tagData)
        {
            if (tagData != null)
            {
                return string.Format("{0}{1}", templateDir, GetRelativeTagFileName(tagData));
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 取得有规则的标签文件完整路径
        /// </summary>
        /// <param name="tagData">标签对象，类型：Hope.ORS.Model.SysTag</param>
        /// <returns>标签文件完整路径,类型：string</returns>
        public string GetRelativeTagFileName(SysTag tagData)
        {
            if (tagData != null)
            {
                return string.Format("TagFile\\{0}\\{1}.tag", tagData.TagCategory, tagData.Name);
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion
    }
}