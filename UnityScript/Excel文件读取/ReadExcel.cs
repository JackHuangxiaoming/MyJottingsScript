using Excel;
using System.Data;
using System.IO;
using UnityEngine;
/// <summary>
/// 读取Excel文件类
/// </summary>
public class ReadExcel
{
    /// <summary>
    /// 读取指定文件
    /// </summary>
    /// <param name="filename">文件名</param>
    /// <param name="folder">文件夹</param>
    /// <param name="filesForm">文件后缀名</param>
    /// <returns>DateSet格式的读取后文件</returns>
    public static DataSet Read(string filename, string folder = "", string filesForm = "xlsx")
    {
        FileStream stream = File.Open(Application.dataPath + "/" + folder + "/" + filename +"."+ filesForm, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();
        stream.Close();

        return result;
    }
}