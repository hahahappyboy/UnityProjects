using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;

public abstract class DatasetBase {
    private SqliteConnection _sqliteConnection;
    private SqliteCommand _sqliteCommand;
    private SqliteDataReader _sqliteDataReader;
    
    /// <summary>
    /// 打开数据库
    /// </summary>
    /// <param name="dataSetPath">数据库名字</param>
    public void OpenDataset(string dataSetPath) {
        _sqliteConnection = new SqliteConnection(dataSetPath);
        _sqliteCommand = _sqliteConnection.CreateCommand();
        _sqliteConnection.Open();
    }
    /// <summary>
    /// 关闭数据库
    /// </summary>
    public void CloseDataset() {
        if (_sqliteDataReader != null) {
            _sqliteDataReader.Close();
        }
        
        if (_sqliteCommand != null) {
            _sqliteCommand.Dispose();
        }
        
        if (_sqliteConnection != null) {
            _sqliteConnection.Close();
        }
    }

    /// <summary>
    /// 返回影响行数
    /// </summary>
    /// <returns></returns>
    public int ExecuteNonQuerySQL(string sql) {
        _sqliteCommand.CommandText = sql;
        int count = _sqliteCommand.ExecuteNonQuery();
        return count;
    }
    /// <summary>
    /// 返回单个结果
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public object ExecuteScalarSQL(string sql) {
        _sqliteCommand.CommandText = sql;
        object result = _sqliteCommand.ExecuteScalar();
        return result;
    }
    
    public List<Dictionary<string, string>> ExecuteReaderSQL(string sql) {
        List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        _sqliteCommand.CommandText = sql;
        _sqliteDataReader = _sqliteCommand.ExecuteReader();
        while (_sqliteDataReader.Read()) {//读一行
            for (int i = 0; i < _sqliteDataReader.FieldCount; i++) {//读这一行得一列
                // Debug.Log(_sqliteDataReader.GetName(i));
                // Debug.Log(_sqliteDataReader.GetValue(i).ToString());
                dictionary.Add(_sqliteDataReader.GetName(i),_sqliteDataReader.GetValue(i).ToString());
            }
            list.Add(dictionary);
        }

        return list;
    }

}
