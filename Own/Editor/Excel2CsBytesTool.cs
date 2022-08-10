using System.IO;
using Excel;
using System.Data;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Table;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Excel生成bytes和cs工具
/// </summary>
public class Excel2CsBytesTool
{
    static string ExcelDataPath = Application.dataPath + "/../ExcelData";//源Excel文件夹,xlsx格式
    static string BytesDataPath = Application.dataPath + "/CreatAssetBundle/PlatformLobby/DataTable";//生成的bytes文件夹
    static string CsClassPath = Application.dataPath + "/../../PlatformLobbyCode/DataTable";//生成的c#脚本文件夹
    //static string XmlDataPath = ExcelDataPath + "/tempXmlData";//生成的xml(临时)文件夹..
    static string XmlDataPath = Application.dataPath + "/CreatAssetBundle/PlatformLobby/DataTable";//生成的xml文件夹
    static string AllCsHead = "all";//序列化结构体的数组类.类名前缀

    static char ArrayTypeSplitChar = ',';//数组类型值拆分符: int[] 1#2#34 string[] 你好#再见 bool[] true#false ...
    static bool IsDeleteXmlInFinish = true;//生成bytes后是否删除中间文件xml

    [MenuItem("SDGSupporter/Excel/Excel2Xml(第一步)")]
    static void Excel2Xml2Bytes()
    {
        Init();
        //生成中间文件xml
        Excel2CsOrXml(false);
        //生成bytes
        //WriteBytes();
    }

    //[MenuItem("SDGSupporter/Excel/Excel2Cs(第二步)")]
    static void Excel2Cs()
    {
        Init();
        Excel2CsOrXml(true);
    }

    [MenuItem("SDGSupporter/Excel/Excel2Cs_Xr(第二步)")]
    static void Excel2Cs_Xr()
    {
        Init();
        Excel2CsOrXmlByXr(true);
    }

    static void Init()
    {
        if (!Directory.Exists(CsClassPath))
        {
            Directory.CreateDirectory(CsClassPath);
        }
        if (!Directory.Exists(XmlDataPath))
        {
            Directory.CreateDirectory(XmlDataPath);
        }
        //if (!Directory.Exists(BytesDataPath))
        //{
        //    Directory.CreateDirectory(BytesDataPath);
        //}
    }
    static string ValueReplaceLinefeed(string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;
        return value.Replace("\\n", "&#x000A;");
    }

    static void WriteCs(string className, string[] names, string[] types, string[] descs)
    {
        try
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("using System;");
            stringBuilder.AppendLine("using System.Collections.Generic;");
            stringBuilder.AppendLine("using System.IO;");
            stringBuilder.AppendLine("using System.Runtime.Serialization.Formatters.Binary;");
            stringBuilder.AppendLine("using System.Xml.Serialization;");
            stringBuilder.AppendLine("using System.Xml;");
            stringBuilder.AppendLine("using PlatformLobbyCode;");
            stringBuilder.AppendLine("using FairyGUI;");
            stringBuilder.Append("\n");
            stringBuilder.AppendLine("namespace Table");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("    [Serializable]");
            stringBuilder.AppendLine("    public class " + className + " : DefaultDataBase");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        private static DefaultDataBase _inst;");
            stringBuilder.AppendLine("        public static DefaultDataBase Instance");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            get");
            stringBuilder.AppendLine("            {");
            stringBuilder.AppendLine("                if (_inst == null)");
            stringBuilder.AppendLine("                {");
            stringBuilder.AppendLine("                    _inst = new " + className + "();");
            stringBuilder.AppendLine("                }");
            stringBuilder.AppendLine("                return _inst;");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("        }");
            for (int i = 0; i < names.Length; i++)
            {
                stringBuilder.AppendLine("        /// <summary>");
                stringBuilder.AppendLine("        /// " + descs[i]);
                stringBuilder.AppendLine("        /// </summary>");
                stringBuilder.AppendLine("        [XmlAttribute(\"" + names[i] + "\")]");

                string type = types[i];
                if (type.Contains("[]"))
                {
                    //type = type.Replace("[]", "");
                    //stringBuilder.AppendLine("        public List<" + type + "> " + names[i] + ";");

                    //可选代码：
                    //用_name字段去反序列化，name取_name.item的值,直接返回list<type>。
                    //因为xml每行可能有多个数组字段，这样就多了一层变量item，所以访问的时候需要.item才能取到list<type>
                    //因此用额外的一个变量直接返回List<type>。
                    type = type.Replace("[]", "");
                    stringBuilder.AppendLine("        public List<" + type + "> " + names[i] + "");
                    stringBuilder.AppendLine("        {");
                    stringBuilder.AppendLine("            get");
                    stringBuilder.AppendLine("            {");
                    stringBuilder.AppendLine("                if (_" + names[i] + " != null)");
                    stringBuilder.AppendLine("                {");
                    stringBuilder.AppendLine("                    return _" + names[i] + ".item;");
                    stringBuilder.AppendLine("                }");
                    stringBuilder.AppendLine("                return null;");
                    stringBuilder.AppendLine("            }");
                    stringBuilder.AppendLine("        }");
                    stringBuilder.AppendLine("        [XmlElementAttribute(\"" + names[i] + "\")]");
                    stringBuilder.AppendLine("        public " + type + "Array _" + names[i] + ";");
                }
                else
                {
                    if (names[i] == "ID")
                        stringBuilder.AppendLine("        public override " + type + " " + names[i] + "{ get; set; }");
                    else
                        stringBuilder.AppendLine("        public " + type + " " + names[i] + ";");
                }

                stringBuilder.Append("\n");
            }

            stringBuilder.AppendLine("        protected override void LoadBytesInfo()");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            Utils.LoadXMLByBundle(\"" + className + ".xml\", loadData, CustPackageName.DataTable, CustPackageName.PlatformLobby);");
            //stringBuilder.AppendLine("            Utils.LoadXMLByBundleThread(\"" + className + ".xml\", loadData, CustPackageName.DataTable, CustPackageName.PlatformLobby);");
            stringBuilder.AppendLine("        }");
            stringBuilder.Append("\n");
            stringBuilder.AppendLine("        protected override void loadDataInfo(XmlDocument doc)");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            XmlNode xn = doc.SelectSingleNode(\"" + AllCsHead + className + "\");");
            stringBuilder.AppendLine("            XmlNodeList xnl = xn.ChildNodes;");
            stringBuilder.AppendLine("            for (int i = 0; i < xnl.Count; i++)");
            stringBuilder.AppendLine("            {");
            stringBuilder.AppendLine("                 " + className + " data = new " + className + "();");
            stringBuilder.AppendLine("                 XmlElement xe = (XmlElement)xnl[i];");
            for (int i = 0; i < names.Length; i++)
            {
                string type = types[i];
                if (type == "int")
                {
                    stringBuilder.AppendLine("                 data." + names[i] + "= int.Parse(xe.GetAttribute(\"" + names[i] + "\"));");
                }
                else if (type == "float")
                {
                    stringBuilder.AppendLine("                 data." + names[i] + "= float.Parse(xe.GetAttribute(\"" + names[i] + "\"));");
                }
                else if (type == "string")
                {
                    stringBuilder.AppendLine("                 data." + names[i] + "= xe.GetAttribute(\"" + names[i] + "\").ToString();");

                    stringBuilder.AppendLine("                 if(data." + names[i] + " == \"0\")");
                    stringBuilder.AppendLine("                     data." + names[i] + " = string.Empty;");
                }
                else if (type == "bool")
                {
                    stringBuilder.AppendLine("                 data." + names[i] + "= bool.Parse(xe.GetAttribute(\"" + names[i] + "\"));");
                }
            }
            stringBuilder.AppendLine("                 lock (datas)");
            stringBuilder.AppendLine("                 {");
            stringBuilder.AppendLine("                     datas.Add(data);");
            stringBuilder.AppendLine("                 };");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");

            string csPath = CsClassPath + "/" + className + ".cs";
            if (File.Exists(csPath))
            {
                File.Delete(csPath);
            }
            using (StreamWriter sw = new StreamWriter(csPath))
            {
                sw.Write(stringBuilder);
                Debug.Log("生成:" + csPath);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("写入CS失败:" + e.Message);
            throw;
        }
    }

    static void WriteCsByXr(string className, string[] names, string[] types, string[] descs)
    {
        try
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("using System;");
            stringBuilder.AppendLine("using System.Collections.Generic;");
            stringBuilder.AppendLine("using System.IO;");
            stringBuilder.AppendLine("using System.Runtime.Serialization.Formatters.Binary;");
            stringBuilder.AppendLine("using System.Xml.Serialization;");
            stringBuilder.AppendLine("using System.Xml;");
            stringBuilder.AppendLine("using PlatformLobbyCode;");
            stringBuilder.AppendLine("using FairyGUI;");
            stringBuilder.Append("\n");
            stringBuilder.AppendLine("namespace Table");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("    [Serializable]");
            stringBuilder.AppendLine("    public class " + className + " : DefaultDataBase");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        private static DefaultDataBase _inst;");
            stringBuilder.AppendLine("        public static DefaultDataBase Instance");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            get");
            stringBuilder.AppendLine("            {");
            stringBuilder.AppendLine("                if (_inst == null)");
            stringBuilder.AppendLine("                {");
            stringBuilder.AppendLine("                    _inst = new " + className + "();");
            stringBuilder.AppendLine("                }");
            stringBuilder.AppendLine("                return _inst;");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("        }");
            for (int i = 0; i < names.Length; i++)
            {
                stringBuilder.AppendLine("        /// <summary>");
                stringBuilder.AppendLine("        /// " + descs[i]);
                stringBuilder.AppendLine("        /// </summary>");
                stringBuilder.AppendLine("        [XmlAttribute(\"" + names[i] + "\")]");

                string type = types[i];
                if (type.Contains("[]"))
                {
                    //type = type.Replace("[]", "");
                    //stringBuilder.AppendLine("        public List<" + type + "> " + names[i] + ";");

                    //可选代码：
                    //用_name字段去反序列化，name取_name.item的值,直接返回list<type>。
                    //因为xml每行可能有多个数组字段，这样就多了一层变量item，所以访问的时候需要.item才能取到list<type>
                    //因此用额外的一个变量直接返回List<type>。
                    type = type.Replace("[]", "");
                    stringBuilder.AppendLine("        public List<" + type + "> " + names[i] + "");
                    stringBuilder.AppendLine("        {");
                    stringBuilder.AppendLine("            get");
                    stringBuilder.AppendLine("            {");
                    stringBuilder.AppendLine("                if (_" + names[i] + " != null)");
                    stringBuilder.AppendLine("                {");
                    stringBuilder.AppendLine("                    return _" + names[i] + ".item;");
                    stringBuilder.AppendLine("                }");
                    stringBuilder.AppendLine("                return null;");
                    stringBuilder.AppendLine("            }");
                    stringBuilder.AppendLine("        }");
                    stringBuilder.AppendLine("        [XmlElementAttribute(\"" + names[i] + "\")]");
                    stringBuilder.AppendLine("        public " + type + "Array _" + names[i] + ";");
                }
                else
                {
                    if (names[i] == "ID")
                        stringBuilder.AppendLine("        public override " + type + " " + names[i] + "{ get; set; }");
                    else
                        stringBuilder.AppendLine("        public " + type + " " + names[i] + ";");
                }

                stringBuilder.Append("\n");
            }

            stringBuilder.AppendLine("        protected override void LoadBytesInfo()");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            Utils.LoadXMLByBundleByThreadAsync(\"" + className + ".xml\", loadData, CustPackageName.DataTable, CustPackageName.PlatformLobby);");
            stringBuilder.AppendLine("        }");
            stringBuilder.Append("\n");
            stringBuilder.AppendLine("        protected async override void loadDataInfo(XmlReader reader)");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            if (reader == null)");
            stringBuilder.AppendLine("                return;");
            stringBuilder.AppendLine("            while (await reader.ReadAsync())");
            stringBuilder.AppendLine("            {");
            stringBuilder.AppendLine("            try");
            stringBuilder.AppendLine("            {");
            stringBuilder.AppendLine("                 if (reader.NodeType != XmlNodeType.Element || !reader.HasAttributes)");
            stringBuilder.AppendLine("                     continue;");
            stringBuilder.AppendLine("                 " + className + " data = new " + className + "();");
            for (int i = 0; i < names.Length; i++)
            {
                string type = types[i];
                if (type == "int")
                {
                    stringBuilder.AppendLine("                 data." + names[i] + "= int.Parse(reader.GetAttribute(\"" + names[i] + "\"));");
                }
                else if (type == "long")
                {
                    stringBuilder.AppendLine("                 data." + names[i] + "= long.Parse(reader.GetAttribute(\"" + names[i] + "\"));");
                }
                else if (type == "float")
                {
                    stringBuilder.AppendLine("                 data." + names[i] + "= float.Parse(reader.GetAttribute(\"" + names[i] + "\"));");
                }
                else if (type == "string")
                {
                    stringBuilder.AppendLine("                 data." + names[i] + "= reader.GetAttribute(\"" + names[i] + "\").ToString();");

                    stringBuilder.AppendLine("                 if(data." + names[i] + " == \"0\")");
                    stringBuilder.AppendLine("                     data." + names[i] + " = string.Empty;");
                }
                else if (type == "bool")
                {
                    stringBuilder.AppendLine("                 data." + names[i] + "= bool.Parse(reader.GetAttribute(\"" + names[i] + "\"));");
                }
            }
            stringBuilder.AppendLine("                 lock (datas)");
            stringBuilder.AppendLine("                 {");
            stringBuilder.AppendLine("                     datas.Add(data);");
            stringBuilder.AppendLine("                 };");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            catch (Exception e)");
            stringBuilder.AppendLine("            {");
            stringBuilder.AppendLine("                FailDispatchEvent();");
            stringBuilder.AppendLine("                return;");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            IsLoadSucceed = true;");
            stringBuilder.AppendLine("            SucceedDispatchEvent();");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");

            string csPath = CsClassPath + "/" + className + ".cs";
            if (File.Exists(csPath))
            {
                File.Delete(csPath);
            }
            using (StreamWriter sw = new StreamWriter(csPath))
            {
                sw.Write(stringBuilder);
                Debug.Log("生成:" + csPath);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("写入CS失败:" + e.Message);
            throw;
        }
    }

    static void WriteXml(string className, string[] names, string[] types, List<string[]> datasList)
    {
        try
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            stringBuilder.AppendLine("<" + AllCsHead + className + ">");
            //stringBuilder.AppendLine("<" + className + "s>");
            for (int d = 0; d < datasList.Count; d++)
            {
                stringBuilder.Append("\t<" + className + " ");
                //单行数据
                string[] datas = datasList[d];
                //填充属性节点
                for (int c = 0; c < datas.Length; c++)
                {
                    string type = types[c];
                    if (!type.Contains("[]"))
                    {
                        string name = names[c];
                        string value = datas[c];
                        value = ValueReplaceLinefeed(value);
                        stringBuilder.Append(name + "=\"" + value + "\"" + (c == datas.Length - 1 ? "" : " "));
                    }
                }
                stringBuilder.Append(">\n");
                //填充子元素节点(数组类型字段)
                for (int c = 0; c < datas.Length; c++)
                {
                    string type = types[c];
                    if (type.Contains("[]"))
                    {
                        string name = names[c];
                        string value = datas[c];
                        string[] values = value.Split(ArrayTypeSplitChar);
                        stringBuilder.AppendLine("\t\t<" + name + ">");
                        for (int v = 0; v < values.Length; v++)
                        {
                            stringBuilder.AppendLine("\t\t\t<item>" + ValueReplaceLinefeed(values[v]) + "</item>");
                        }
                        stringBuilder.AppendLine("\t\t</" + name + ">");
                    }
                }
                stringBuilder.AppendLine("\t</" + className + ">");
            }
            //stringBuilder.AppendLine("</" + className + "s>");
            stringBuilder.AppendLine("</" + AllCsHead + className + ">");

            string xmlPath = XmlDataPath + "/" + className + ".xml";
            if (File.Exists(xmlPath))
            {
                File.Delete(xmlPath);
            }
            using (StreamWriter sw = new StreamWriter(xmlPath))
            {
                sw.Write(stringBuilder);
                Debug.Log("生成文件:" + xmlPath);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("写入Xml失败:" + e.Message);
        }
    }

    static void Excel2CsOrXml(bool isCs)
    {
        if (!isCs)
        {
            Directory.Delete(XmlDataPath, true);
            Directory.CreateDirectory(XmlDataPath);
        }
        else
        {
            Directory.Delete(CsClassPath, true);
            Directory.CreateDirectory(CsClassPath);
        }
        string[] excelPaths = Directory.GetFiles(ExcelDataPath, "*.xlsx");
        for (int e = 0; e < excelPaths.Length; e++)
        {
            //0.读Excel
            string className;//类型名
            string[] names;//字段名
            string[] types;//字段类型
            string[] descs;//字段描述
            List<string[]> datasList;//数据

            try
            {
                string excelPath = excelPaths[e];//excel路径  
                className = Path.GetFileNameWithoutExtension(excelPath).ToLower();
                FileStream fileStream = File.Open(excelPath, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
                // 表格数据全部读取到result里
                DataSet result = excelDataReader.AsDataSet();
                // 获取表格列数
                int columns = result.Tables[0].Columns.Count;
                // 获取表格行数
                int rows = result.Tables[0].Rows.Count;
                // 根据行列依次读取表格中的每个数据
                names = new string[columns];
                types = new string[columns];
                descs = new string[columns];
                datasList = new List<string[]>();
                for (int r = 0; r < rows; r++)
                {
                    string[] curRowData = new string[columns];
                    for (int c = 0; c < columns; c++)
                    {
                        //解析：获取第一个表格中指定行指定列的数据
                        string value = result.Tables[0].Rows[r][c].ToString();
                        if (value.StartsWith("^"))
                        {
                            value = "cehuaUse" + c;
                        }

                        //清除前两行的变量名、变量类型 首尾空格
                        if (r < 2)
                        {
                            value = value.TrimStart(' ').TrimEnd(' ');
                        }

                        curRowData[c] = value;
                    }
                    //解析：第一行类变量名
                    if (r == 0)
                    {
                        names = curRowData;
                    }//解析：第二行类变量类型
                    else if (r == 1)
                    {
                        types = curRowData;
                    }//解析：第三行类变量描述
                    else if (r == 2)
                    {
                        descs = curRowData;
                    }//解析：第三行开始是数据
                    else
                    {
                        datasList.Add(curRowData);
                    }
                }
            }
            catch (System.Exception exc)
            {
                Debug.LogError("请关闭Excel:" + exc.Message);
                return;
            }

            if (isCs)
            {
                //写Cs
                WriteCs(className, names, types, descs);
            }
            else
            {
                //写Xml
                WriteXml(className, names, types, datasList);
            }
        }

        AssetDatabase.Refresh();
    }

    static void Excel2CsOrXmlByXr(bool isCs)
    {
        if (!isCs)
        {
            Directory.Delete(XmlDataPath, true);
            Directory.CreateDirectory(XmlDataPath);
        }
        else
        {
            Directory.Delete(CsClassPath, true);
            Directory.CreateDirectory(CsClassPath);
        }
        string[] excelPaths = Directory.GetFiles(ExcelDataPath, "*.xlsx");
        for (int e = 0; e < excelPaths.Length; e++)
        {
            //0.读Excel
            string className;//类型名
            string[] names;//字段名
            string[] types;//字段类型
            string[] descs;//字段描述
            List<string[]> datasList;//数据

            try
            {
                string excelPath = excelPaths[e];//excel路径  
                className = Path.GetFileNameWithoutExtension(excelPath).ToLower();
                FileStream fileStream = File.Open(excelPath, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
                // 表格数据全部读取到result里
                DataSet result = excelDataReader.AsDataSet();
                // 获取表格列数
                int columns = result.Tables[0].Columns.Count;
                // 获取表格行数
                int rows = result.Tables[0].Rows.Count;
                // 根据行列依次读取表格中的每个数据
                names = new string[columns];
                types = new string[columns];
                descs = new string[columns];
                datasList = new List<string[]>();
                for (int r = 0; r < rows; r++)
                {
                    string[] curRowData = new string[columns];
                    for (int c = 0; c < columns; c++)
                    {
                        //解析：获取第一个表格中指定行指定列的数据
                        string value = result.Tables[0].Rows[r][c].ToString();
                        if (value.StartsWith("^"))
                        {
                            value = "cehuaUse" + c;
                        }

                        //清除前两行的变量名、变量类型 首尾空格
                        if (r < 2)
                        {
                            value = value.TrimStart(' ').TrimEnd(' ');
                        }

                        curRowData[c] = value;
                    }
                    //解析：第一行类变量名
                    if (r == 0)
                    {
                        names = curRowData;
                    }//解析：第二行类变量类型
                    else if (r == 1)
                    {
                        types = curRowData;
                    }//解析：第三行类变量描述
                    else if (r == 2)
                    {
                        descs = curRowData;
                    }//解析：第三行开始是数据
                    else
                    {
                        datasList.Add(curRowData);
                    }
                }
            }
            catch (System.Exception exc)
            {
                Debug.LogError("请关闭Excel:" + exc.Message);
                return;
            }

            if (isCs)
            {
                //写Cs
                WriteCsByXr(className, names, types, descs);
            }
            else
            {
                //写Xml
                WriteXml(className, names, types, datasList);
            }
        }

        AssetDatabase.Refresh();
    }
    static void WriteBytes()
    {
        //string csAssemblyPath = Application.dataPath + "/../Library/ScriptAssemblies/Assembly-CSharp.dll";
        string csAssemblyPath = URLFactory.DownLoadHotFilePath + @"\PlatformLobbyCode\PlatformLobbyCode.dll";
        Assembly assembly = Assembly.LoadFile(csAssemblyPath);
        if (assembly != null)
        {
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                Type type = types[i];
                if (type.Namespace == "Table" && type.Name.Contains(AllCsHead))
                {
                    string className = type.Name.Replace(AllCsHead, "");

                    //读取xml数据
                    string xmlPath = XmlDataPath + "/" + className + ".xml";
                    if (!File.Exists(xmlPath))
                    {
                        Debug.LogError("Xml文件读取失败:" + xmlPath);
                        continue;
                    }
                    object table;
                    using (Stream reader = new FileStream(xmlPath, FileMode.Open))
                    {
                        //读取xml实例化table: all+classname
                        //object table = assembly.CreateInstance("Table." + type.Name);
                        XmlSerializer xmlSerializer = new XmlSerializer(type);
                        table = xmlSerializer.Deserialize(reader);
                    }
                    //obj序列化二进制
                    string bytesPath = BytesDataPath + "/" + className + ".bytes";
                    if (File.Exists(bytesPath))
                    {
                        File.Delete(bytesPath);
                    }
                    using (FileStream fileStream = new FileStream(bytesPath, FileMode.Create))
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();
                        binaryFormatter.Serialize(fileStream, table);
                        Debug.Log("生成:" + bytesPath);
                    }

                    if (IsDeleteXmlInFinish)
                    {
                        File.Delete(xmlPath);
                        Debug.Log("删除:" + bytesPath);
                    }
                }
            }
        }

        if (IsDeleteXmlInFinish)
        {
            Directory.Delete(XmlDataPath);
            Debug.Log("删除:" + XmlDataPath);
        }
    }
}
