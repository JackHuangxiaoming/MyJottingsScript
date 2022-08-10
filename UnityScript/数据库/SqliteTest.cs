using Mono.Data.Sqlite;
using UnityEngine;
public class SqliteTest : MonoBehaviour
{
    /// <summary>
    /// SQLite数据库辅助类
    /// </summary>
    private SqliteHelp sql;

    void Start()
    {

        //创建名为Hungmingfu的数据库
        SqliteHelp sql = new SqliteHelp("data source=HaoYouXi.db");

        //创建名为table1的数据表
        try
        {
            sql.CreateTable("表1", new string[] { "ID", "Name", "Age", "Email" }, new string[] { "INT", "TEXT", "INT", "TEXT" });
        }
        catch (System.Exception)
        {

            print("表单重复");
        }
        

        ////插入两条数据
        //sql.InsertInto ("表1", new string[] { "'1'", "'张三'", "'22'", "'Zhang3@163.com'" });
        //sql.InsertInto("表1", new string[] { "'2'", "'李四'", "'25'", "'Li4@163.com'" });

        ////更新数据，将Name = "张三"的记录中的Name改为"Zhang3"
        //sql.UpdateInto("表1", new string[] { "Name" }, new string[] { "'Zhang3'" }, "Name", "=", "'张三'");

        ////插入3条数据
        //sql.InsertInto("表1", new string[] { "3", "'王五'", "25", "'Wang5@163.com'" });
        //sql.InsertInto("表1", new string[] { "4", "'王五'", "26", "'Wang5@163.com'" });
        //sql.InsertInto("表1", new string[] { "5", "'王五'", "27", "'Wang5@163.com'" });

        ////删除Name = "王五"且Age = 26的记录,DeleteValuesOR方法类似
        //sql.DeleteValuesAND("表1", new string[] { "Name", "Age" }, new string[] { "=", "=" }, new string[] { "'王五'", "'26'" });

        ////读取整张表
        //SqliteDataReader reader = sql.ReadFullTable("表1");
        //while (reader.Read())
        //{
        //    //读取ID
        //    Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
        //    //读取Name
        //    Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
        //    //读取Age
        //    Debug.Log(reader.GetInt32(reader.GetOrdinal("Age")));
        //    //读取Email
        //    Debug.Log(reader.GetString(reader.GetOrdinal("Email")));
        //}

        ////读取数据表中Age >= 25的所有记录的ID和Name
        //reader = sql.ReadTable("表1", new string[] { "ID", "Name" }, new string[] { "Age" }, new string[] { ">=" }, new string[] { "'25'" });
        //while (reader.Read())
        //{
        //    //读取ID
        //    Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
        //    //读取Name
        //    Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
        //}

        ////自定义SQL,删除数据表中所有Name = "王五"的记录
        //sql.ExecuteQuery("DELETE FROM 表1 WHERE NAME='王五'");

        //关闭数据库连接
        sql.CloseSqlConnection();
    }
}