using UnityEngine;
using System;
using Mono.Data.Sqlite;

public class SqliteHelp

{
    /// <summary>
    /// 数据库连接定义
    /// </summary>
    private SqliteConnection Connection;
    /// <summary>
    /// SQL命令定义
    /// </summary>
    private SqliteCommand Command;
    /// <summary>
    /// 数据读取定义
    /// </summary>
    private SqliteDataReader Reader;
    /// <summary>
    /// 构造函数    
    /// </summary>
    /// <param name="connectionString">数据库连接字符串</param>
    public SqliteHelp(string connectionString)
    {
        OpenDB(connectionString);
    }
    public SqliteHelp()
    {
    }
    /// <summary>
    /// 连接数据库并打开
    /// </summary>
    /// <param name="connectionString"></param>
    public void OpenDB(string connectionString)

    {
        try
        {
            //连接
            Connection = new SqliteConnection(connectionString);
            //打开
            Connection.Open();

            Debug.Log("open to db");
        }
        catch (Exception e)
        {
            string temp1 = e.ToString();
            Debug.Log(temp1);
        }

    }
    /// <summary>
    /// 关闭数据库连接
    /// </summary>
    public void CloseSqlConnection()
    {

        if (Command != null)
        {
            Command.Dispose();
        }
        Command = null;
        if (Reader != null)
        {
            Reader.Dispose();
        }
        Reader = null;
        if (Connection != null)
        {
            Connection.Close();
        }
        Connection = null;

        Debug.Log("close from db.");
    }

    /// <summary>
    /// 执行SQL命令
    /// </summary>
    /// <returns>The query.</returns>
    /// <param name="sqlQuery">SQL命令字符串</param>
    public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        Command = Connection.CreateCommand();
        Command.CommandText = sqlQuery;
        Reader = Command.ExecuteReader();

        return Reader;
    }
    /// <summary>
    /// 读取整张数据表
    /// </summary>
    /// <returns>The full table.</returns>
    /// <param name="tableName">数据表名称</param>
    public SqliteDataReader ReadFullTable(string tableName)
    {
        string query = "SELECT * FROM " + tableName;

        return ExecuteQuery(query);
    }
    /// <summary>
    /// 向指定数据表中插入数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="values">插入的数值</param>
    public SqliteDataReader InsertInto(string tableName, string[] values)
    {
        //获取数据表中字段数目
        int fieldCount = ReadFullTable(tableName).FieldCount;
        //当插入的数据长度不等于字段数目时引发异常
        if (values.Length != fieldCount)
        {
            throw new SqliteException("values.Length!=fieldCount");
        }

        string query = "INSERT INTO " + tableName + " VALUES (" + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";

        return ExecuteQuery(query);
    }
    /// <summary>
    /// 更新指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="cols">字段名</param>
    /// <param name="colsValues">字段名对应的数据</param>
    /// <param name="selectkey">关键字</param>
    /// <param name="selectvalue">关键字对应的值</param>
    public SqliteDataReader UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string operation, string selectvalue)
    {
        //当字段名称和字段数值不对应时引发异常
        if (cols.Length != colsvalues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length");
        }

        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += ", " + cols[i] + " =" + colsvalues[i];
        }

        query += " WHERE " + selectkey + operation + selectvalue + " ";

        return ExecuteQuery(query);
    }

    /// <summary>
    /// 删除指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param>
    public SqliteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
        }

        string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += "OR " + colNames[i] + operations[i] + colValues[i];
        }
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 删除指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param>
    public SqliteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
        }

        string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += " AND " + colNames[i] + operations[i] + colValues[i];
        }
        return ExecuteQuery(queryString);
    }
    /// <summary>
    /// 插入数据
    /// </summary>
    /// <param name="tableName">表单名</param>
    /// <param name="cols">类型</param>
    /// <param name="values">值</param>
    /// <returns></returns>
    public SqliteDataReader InsertIntoSpecific(string tableName, string[] cols, string[] values)
    {
        if (cols.Length != values.Length)
        {
            throw new SqliteException("columns.Length != values.Length");
        }

        string query = "INSERT INTO " + tableName + "(" + cols[0];
        for (int i = 1; i < cols.Length; ++i)
        {
            query += ", " + cols[i];
        }
        query += ") VALUES (" + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";

        return ExecuteQuery(query);
    }
    /// <summary>
    /// 删除一张表
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public SqliteDataReader DeleteContents(string tableName)
    {
        string query = "DELETE FROM " + tableName;

        return ExecuteQuery(query);
    }
    /// <summary>
    /// 创建数据表
    /// </summary> 
    /// <returns>The table.</returns>
    /// <param name="name">数据表名</param>
    /// <param name="col">字段名</param>
    /// <param name="colType">字段名类型</param>
    public SqliteDataReader CreateTable(string name, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {
            throw new SqliteException("columns.Length != colType.Length");
        }

        string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];
        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col[i] + " " + colType[i];
        }
        query += ")";

        return ExecuteQuery(query);
    }

    /// <summary>
    /// 读取指定数据
    /// </summary>
    /// <returns>The table.</returns>
    /// <param name="tableName">表名</param>
    /// <param name="items">项</param>
    /// <param name="colNames">字段Key</param>
    /// <param name="operations">运算符</param>
    /// <param name="colValues">字段Values</param>
    public SqliteDataReader ReadTable(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues)
    {
        string queryString = "SELECT " + items[0];
        for (int i = 1; i < items.Length; i++)
        {
            queryString += ", " + items[i];
        }
        queryString += " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " " + colValues[0];
        for (int i = 0; i < colNames.Length; i++)
        {
            queryString += " AND " + colNames[i] + " " + operations[i] + " " + colValues[0] + " ";
        }
        return ExecuteQuery(queryString);
    }
}