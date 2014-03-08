using System;
using System.Xml;

namespace Hope.Util
{
    /// <summary>
    /// XML操作类
    /// </summary>
    public class XMLHelper
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XMLHelper()
        {

        }

        #endregion

        private static XmlDocument xmlDoc = new XmlDocument();

        /// <summary>
        /// 加载xml文件
        /// </summary>
        /// <param name="path">xml文件的物理路径</param>
        private static void LoadXml(string path, string node_root)
        {
            xmlDoc = new XmlDocument();
            //判断xml文件是否存在
            if (!System.IO.File.Exists(path))
            {
                //创建xml 声明节点
                XmlNode xmlnode = xmlDoc.CreateNode(System.Xml.XmlNodeType.XmlDeclaration, "", "");
                //添加上述创建和 xml声明节点
                xmlDoc.AppendChild(xmlnode);
                //创建xml dbGuest 元素（根节点）
                XmlElement xmlelem = xmlDoc.CreateElement("", node_root, "");
                xmlDoc.AppendChild(xmlelem);
                try
                {
                    xmlDoc.Save(path);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                xmlDoc.Load(path);
            }
            else
            {
                //加载xml文件
                xmlDoc.Load(path);
            }
        }

        /// <summary>
        /// 添加xml子节点
        /// </summary>
        /// <param name="path">xml文件的物理路径</param>
        /// <param name="node_root">根节点名称</param>
        /// <param name="node_name">添加的子节点名称</param>
        /// <param name="node_text">子节点文本</param>
        public static void addElement(string path, string node_root, string node_name, string node_text, string att_name, string att_value)
        {
            LoadXml(path, node_root);

            XmlNodeList nodeList = xmlDoc.SelectSingleNode(node_root).ChildNodes;//获取bookstore节点的所有子节点
            //判断是否有节点,有节点就遍历所有子节点,看看有没有重复节点,没节点就添加一个新节点
            if (nodeList.Count > 0)
            {
                foreach (XmlNode xn in nodeList)//遍历所有子节点 
                {
                    XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                    if (xe.GetAttribute(att_name) != att_value)
                    {
                        XmlNode xmldocSelect = xmlDoc.SelectSingleNode(node_root);   //选中根节点
                        XmlElement son_node = xmlDoc.CreateElement(node_name);    //添加子节点 
                        son_node.SetAttribute(att_name, att_value);     //设置属性
                        son_node.InnerText = node_text;    //添加节点文本
                        xmldocSelect.AppendChild(son_node);       //添加子节点
                        xmlDoc.Save(path);          //保存xml文件
                        break;
                    }
                }

            }
            else
            {
                XmlNode xmldocSelect = xmlDoc.SelectSingleNode(node_root);   //选中根节点
                XmlElement son_node = xmlDoc.CreateElement(node_name);    //添加子节点 
                son_node.SetAttribute(att_name, att_value);     //设置属性
                son_node.InnerText = node_text;    //添加节点文本
                xmldocSelect.AppendChild(son_node);       //添加子节点
                xmlDoc.Save(path);          //保存xml文件
            }
        }

        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="path">xml文件的物理路径</param>
        /// <param name="node_root">根节点名称</param>
        /// <param name="new_text">节点的新内容</param>
        /// <param name="att_name">节点的属性名</param>
        /// <param name="att_value">节点的属性值</param>
        public static void UpdateElement(string path, string node_root, string new_text, string att_name, string att_value)
        {
            LoadXml(path, node_root);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode(node_root).ChildNodes;//获取bookstore节点的所有子节点 
            foreach (XmlNode xn in nodeList)//遍历所有子节点 
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                if (xe.GetAttribute(att_name) == att_value)
                {
                    xe.InnerText = new_text;    //内容赋值
                    xmlDoc.Save(path);//保存 
                    break;
                }
            }

        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="path">xml文件的物理路径</param>
        /// <param name="node_root">根节点名称</param>
        /// <param name="att_name">节点的属性名</param>
        /// <param name="att_value">节点的属性值</param>
        public static void deleteNode(string path, string node_root, string att_name, string att_value)
        {

            LoadXml(path, node_root);

            XmlNodeList nodeList = xmlDoc.SelectSingleNode(node_root).ChildNodes;
            XmlNode root = xmlDoc.SelectSingleNode(node_root);

            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn;

                if (xe.GetAttribute(att_name) == att_value)
                {
                    //xe.RemoveAttribute("name");//删除name属性 
                    xe.RemoveAll();//删除该节点的全部内容 
                    root.RemoveChild(xe);
                    xmlDoc.Save(path);//保存 
                    break;
                }

            }
        }

        #region XML函数

        /// <summary>
        /// 创建XML
        /// </summary>
        /// <returns></returns>
        public static XmlDocument CreateXmlDoc()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><root></root>");
            return xmlDoc;
        }

        /// <summary>
        /// 创建XML
        /// </summary>
        /// <returns></returns>
        public static XmlDocument CreateXmlDoc(string nodeName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><" + nodeName + "></" + nodeName + ">");
            return xmlDoc;
        }


        public static void AddAttributeXMLNode(XmlDocument xmlDoc, XmlNode parentNode, string nodeName, string nodeValue)
        {
            XmlNode temNode = xmlDoc.CreateNode(XmlNodeType.Attribute, nodeName, "");
            temNode.Value = nodeValue;
            parentNode.Attributes.SetNamedItem(temNode);
        }

        #endregion

        /*****************************/

        protected string strXmlFile;
        protected XmlDocument objXmlDoc = new XmlDocument();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xmlFile"></param>
        public XMLHelper(string xmlFile)
        {
            try
            {
                objXmlDoc.Load(xmlFile);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            strXmlFile = xmlFile;
        }

        /// <summary>
        /// 查找数据，返回一个DataView
        /// </summary>
        /// <param name="XmlPathNode"></param>
        /// <returns></returns>
        //public DataView GetData(string XmlPathNode)
        //{
        //    DataSet ds = new DataSet();
        //    StringReader read = new StringReader(objXmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
        //    ds.ReadXml(read);
        //    return ds.Tables[0].DefaultView;
        //}

        /// <summary>
        /// 更新节点内容
        /// </summary>
        /// <param name="XmlPathNode"></param>
        /// <param name="Content"></param>
        public void Replace(string XmlPathNode, string Content)
        {
            objXmlDoc.SelectSingleNode(XmlPathNode).InnerText = Content;
        }

        /// <summary>
        /// 删除一个节点
        /// </summary>
        /// <param name="Node"></param>
        public void Delete(string Node)
        {
            string mainNode = Node.Substring(0, Node.LastIndexOf("/"));
            objXmlDoc.SelectSingleNode(mainNode).RemoveChild(objXmlDoc.SelectSingleNode(Node));
        }

        /// <summary>
        /// 插入一节点和此节点的一子节点
        /// </summary>
        /// <param name="MainNode"></param>
        /// <param name="ChildNode"></param>
        /// <param name="Element"></param>
        /// <param name="Content"></param>
        public void InsertNode(string MainNode, string ChildNode, string Element, string Content)
        {
            XmlNode objRootNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objChildNode = objXmlDoc.CreateElement(ChildNode);
            objRootNode.AppendChild(objChildNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.InnerText = Content;
            objChildNode.AppendChild(objElement);
        }

        /// <summary>
        /// 插入一个节点，带一个属性
        /// </summary>
        /// <param name="MainNode"></param>
        /// <param name="Element"></param>
        /// <param name="Attrib"></param>
        /// <param name="AttribContent"></param>
        /// <param name="Content"></param>
        public void InsertElement(string MainNode, string Element, string Attrib, string AttribContent, string Content)
        {
            XmlNode objNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.SetAttribute(Attrib, AttribContent);
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
        }

        /// <summary>
        /// 插入一个节点，不带属性
        /// </summary>
        /// <param name="MainNode"></param>
        /// <param name="Element"></param>
        /// <param name="Content"></param>
        public void InsertElement(string MainNode, string Element, string Content)
        {
            XmlNode objNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
        }

        /// <summary>
        /// 保存文档
        /// </summary>
        public void Save()
        {
            try
            {
                objXmlDoc.Save(strXmlFile);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            objXmlDoc = null;
        }

        /*****************************/

        /******************************************************************************************
         * 
            实例应用：

            string strXmlFile = Server.MapPath("TestXml.xml");
            XmlControl xmlTool = new XmlControl(strXmlFile);

             数据显视
             dgList.DataSource = xmlTool.GetData("Book/Authors[ISBN=\"0002\"]");
             dgList.DataBind();

             更新元素内容
             xmlTool.Replace("Book/Authors[ISBN=\"0002\"]/Content","ppppppp");
             xmlTool.Save();

             添加一个新节点
             xmlTool.InsertNode("Book","Author","ISBN","0004");
             xmlTool.InsertElement("Book/Author[ISBN=\"0004\"]","Content","aaaaaaaaa");
             xmlTool.InsertElement("Book/Author[ISBN=\"0004\"]","Title","Sex","man","iiiiiiii");
             xmlTool.Save();

             删除一个指定节点的所有内容和属性
             xmlTool.Delete("Book/Author[ISBN=\"0004\"]");
             xmlTool.Save();

             删除一个指定节点的子节点
             xmlTool.Delete("Book/Authors[ISBN=\"0003\"]");
             xmlTool.Save();
         * 
         *******************************************************************************************/

    }
}