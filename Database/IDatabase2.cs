using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace ProjectBase.Database
{
    public interface IDatabase2: IDisposable
    {
        ConnectionStringSettings ConnectionString { get; set; }
        IsolationLevel Isolation { get; set; }
        DbSettings Setting { get; set; }
        void ClearConnection();
        void CloseConnection();
        void Commit();
        void Dispose();
        int ExecuteQuery(string query);
        int ExecuteQuery(IDbCommand query);
        DataTable ExecuteQueryDataTable(string query);
        DataTable ExecuteQueryDataTable(IDbCommand query);
        void FillObject(DataTable table, string query);
        void FillObject(DataTable table, IDbCommand query);
        void FillObject(DataSet set, string table, string query);
        void FillObject(DataSet set, string table, IDbCommand query);
        IDbConnection GetConnection();
        IDataReader GetDataReader(string query);
        IDataReader GetDataReader(IDbCommand query);
        T GetObject<T>(string query);
        T GetObject<T>(IDbCommand query);
        List<T> GetObjectList<T>(string query);
        List<T> GetObjectList<T>(IDbCommand query);
        object GetSingleValue(string query);
        object GetSingleValue(IDbCommand query);
        bool HasColumn(IDataRecord dr, string columnName);
        void RollBack();
    }
}