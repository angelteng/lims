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
    /// ģ�崦��
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
        /// ���캯��
        /// </summary>
        /// <param name="templatDirectory">ģ���ļ���·��</param>
        public VelocityHelper(string templatDirectory)
        {
            templateDir = templatDirectory;
            Init();
        }
        /// <summary>
        /// �޲������캯��
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
            //����VelocityEngineʵ������
            velocity = new VelocityEngine();

            //ʹ�����ó�ʼ��VelocityEngine
            ExtendedProperties props = new ExtendedProperties();
            props.AddProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            //�ɻ��ɣ�props.AddProperty("resouce.loader","file"),���µ�ͬ����
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, templateDir);
            props.AddProperty(RuntimeConstants.INPUT_ENCODING, "utf-8");
            props.AddProperty(RuntimeConstants.OUTPUT_ENCODING, "utf-8");
            velocity.Init(props);

            //Ϊģ�������ֵ
            context = new VelocityContext();
        }

        #endregion

        /// <summary>
        /// TemplateHandlerʵ��
        /// </summary>
        /// <returns></returns>
        public static VelocityHelper Instance()
        {
            return theInstance;
        }

        /// <summary>
        /// ����ѭ�����ñ�ǩ�����õı�־
        /// </summary>
        private Hashtable noRecursiveFlag = new Hashtable();

        #region ģ��

        /// <summary>
        /// ��ģ�������ֵ
        /// </summary>
        /// <param name="key">ģ�����</param>
        /// <param name="value">ģ�����ֵ</param>
        public void Put(string key, object value)
        {
            if (context == null)
                context = new VelocityContext();
            context.Put(key, value);
        }

        /// <summary>
        /// ����ģ��
        /// </summary>
        /// <param name="temFile">ģ������</param>
        /// <returns></returns>
        public string BuildTemplate(string temFile)
        {
            noRecursiveFlag.Clear();// ��ձ�־λ

            string sHtml = "";
            try
            {
                if (File.Exists(templateDir + "/" + temFile)) //�ж��ļ��Ƿ���� 
                { 
                    //���ļ��ж�ȡģ��
                    Template temp = velocity.GetTemplate(temFile);

                    context.Put("HopeTag", CreateHopeTag());

                    //��ȡģ����ķ��õı�ǩ��Ϣ
                    ASTprocess astProcess = (ASTprocess)temp.Data;
                    for (int i = 0; i < astProcess.ChildrenCount; i++)
                    {
                        //ASTReference��Ϊ������ӵı�ǩ
                        if ("NVelocity.Runtime.Parser.Node.ASTReference".Equals(astProcess.GetChild(i).ToString()))
                        {
                            //1.�����ǩ��д��Ϊ${},FirstTokenȡ�������ǡ�${������һ����ǩ���ݷ���next�����ʱ��ȡ�����Ĳ���$��ֱ���Ǳ�ǩ�ĵ�һ��
                            //eg: ${AAAA},FirstToken = "${", next = "AAAA";
                            //2.�����ǩ��д����$,FirstTokenȡ�����ĵ���$���ϵ�һ�α�ǩ����������Ҫȥ����һ���ַ���$��
                            //eg: $AAAA,FirstToken = "$AAAA"
                            string sTagName = astProcess.GetChild(i).FirstToken.ToString(); //��ӵı�ǩ����

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
                else    //�ļ�������
                {
                    sHtml = temFile + "  ģ���ļ������ڣ�";
                }
            }
            catch (Exception ex)
            {
                LogUtil.fatal("BuildTemplate��" + ex.Message + ex.StackTrace);
                //throw;
                sHtml = "ģ���������" + ex.Message + ex.StackTrace;
            }
            return sHtml;
        }
        #endregion

        #region ��ǩ

        /// <summary>
        /// ����HopeTagʵ�����������ⲿֵ
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
        /// �����Զ����ǩ�������ݿ�����Ƿ��ж��壬û�ж���Ļ����ر�ǩ����
        /// </summary>
        /// <param name="sTagName">��ǩ���ƣ������������</param>
        /// <returns>��ǩ����ַ���</returns>        
        private string GetTagProcessStr(string sTagName)
        {
            // ���Ƕ��ʹ�ñ�ǩ����ֱ���˳������ؿ��ַ���
            if (noRecursiveFlag.ContainsKey(sTagName))
            {
                return string.Format("��ǩ{0}�ظ�����", sTagName);
            }

            noRecursiveFlag.Add(sTagName, sTagName);   //����ǩ��д��   

            string sTagValue = "";
            SysTag tagData = BLLFactory.Instance().SysTagBLL.GetData(sTagName);
            string tagFile = GetTagFileName(tagData);
            if (tagData != null && File.Exists(tagFile))
            {
                string tagFileName = GetRelativeTagFileName(tagData);
                
                //���ļ��ж�ȡ��ǩ
                Template temp = velocity.GetTemplate(tagFileName);

                context.Put("HopeTag", CreateHopeTag());

                //��ȡģ����ķ��õı�ǩ��Ϣ
                ASTprocess astProcess = (ASTprocess)temp.Data;
                for (int i = 0; i < astProcess.ChildrenCount; i++)
                {
                    //ASTReference��Ϊ������ӵı�ǩ
                    if ("NVelocity.Runtime.Parser.Node.ASTReference".Equals(astProcess.GetChild(i).ToString()))
                    {
                        string sTemTagName = astProcess.GetChild(i).FirstToken.ToString(); //��ӵı�ǩ����
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
                            //�ݹ����Ƕ�ױ�ǩ
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

            noRecursiveFlag.Remove(sTagName);   //����ǩ���Ƴ�
            return sTagValue;
        }

        /// <summary>
        /// ����ǩ�Ƿ�Ϊ�Զ����ǩ
        /// </summary>
        /// <param name="sTagName">��ǩ����</param>
        /// <returns>��|��</returns>
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

        #region ����

        private int _CurrentPageIndex;
        /// <summary>
        /// ��ǰҳ
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
        /// ��ʾ����
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

        #region ����ģ�������Ϣ�����Html

        /// <summary>
        /// ����ģ�������Ϣ�����Html
        /// </summary>
        /// <param name="templateFileName"></param>
        /// <returns></returns>
        public void GetResponsePageHtml(string templateFileName)
        {
            string sHtml =BuildTemplate(templateFileName); //����ģ�� 

            HttpContext.Current.Response.Write(sHtml);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        #endregion

        #region ȡ�ñ�ǩ�ļ�·��

        /// <summary>
        /// ȡ���й���ı�ǩ�ļ�����·��
        /// </summary>
        /// <param name="tagData">��ǩ�������ͣ�Hope.ORS.Model.SysTag</param>
        /// <returns>��ǩ�ļ�����·��,���ͣ�string</returns>
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
        /// ȡ���й���ı�ǩ�ļ�����·��
        /// </summary>
        /// <param name="tagData">��ǩ�������ͣ�Hope.ORS.Model.SysTag</param>
        /// <returns>��ǩ�ļ�����·��,���ͣ�string</returns>
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