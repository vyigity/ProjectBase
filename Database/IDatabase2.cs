using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace ProjectBase.Database
{
    public interface IDatabase2: IDisposable
    {
        /// <summary>
        /// Gets or sets connection string that is used by database interaction class.
        /// </summary>
        ConnectionStringSettings ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets transaction isolation level.
        /// </summary>
        IsolationLevel Isolation { get; set; }

        /// <summary>
        /// Gets or sets database access mode.
        /// </summary>
        DbSettings Setting { get; set; }

        /// <summary>
        /// Rollbacks current transaction if in transaction mod and closes current connection.
        /// </summary>
        void ClearConnection();

        /// <summary>
        /// Rollbacks current transaction if in transaction mod and closes current connection.
        /// </summary>
        void CloseConnection();

        /// <summary>
        /// Commits current transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Executes a sql query and returns affected row count.
        /// </summary>
        int ExecuteQuery(string query);

        /// <summary>
        /// Executes a sql command and returns affected row count.
        /// </summary>
        int ExecuteQuery(IDbCommand query);

        /// <summary>
        /// Executes a sql select query and returns results as a data table object.
        /// </summary>
        DataTable ExecuteQueryDataTable(string query);

        /// <summary>
        /// Executes a sql select command and returns results as a data table object.
        /// </summary>
        DataTable ExecuteQueryDataTable(IDbCommand query);

        /// <summary>
        /// Executes a sql select query and fills a data table object.
        /// </summary>
        void FillObject(DataTable table, string query);

        /// <summary>
        /// Executes a sql select command and fills a data table object.
        /// </summary>
        void FillObject(DataTable table, IDbCommand query);

        /// <summary>
        /// Executes a sql select query and fills a dataset object.
        /// </summary>
        void FillObject(DataSet set, string table, string query);

        /// <summary>
        /// Executes a sql select command and fills a dataset object.
        /// </summary>
        void FillObject(DataSet set, string table, IDbCommand query);

        /// <summary>
        /// Returns current connection object.
        /// </summary>
        IDbConnection GetConnection();

        /// <summary>
        /// Executes a sql select query and returns a data reader object.
        /// </summary>
        IDataReader GetDataReader(string query);

        /// <summary>
        /// Executes a sql select command and returns a data reader object.
        /// </summary>
        IDataReader GetDataReader(IDbCommand query);

        /// <summary>
        /// Executes a sql select query and returns results as a desired type object.
        /// </summary>
        T GetObject<T>(string query);

        /// <summary>
        /// Executes a sql select command and returns results as a desired type object.
        /// </summary>
        T GetObject<T>(IDbCommand query);

        /// <summary>
        /// Executes a sql select query and returns results as a list of desired type objects.
        /// </summary>
        List<T> GetObjectList<T>(string query);

        /// <summary>
        /// Executes a sql select command and returns results as a list of desired type objects.
        /// </summary>
        List<T> GetObjectList<T>(IDbCommand query);

        /// <summary>
        /// Executes a sql select query and returns results result as a single value.
        /// </summary>
        object GetSingleValue(string query);

        /// <summary>
        /// Executes a sql select command and returns results result as a single value.
        /// </summary>
        object GetSingleValue(IDbCommand query);

        /// <summary>
        /// Verifies if a data record has desired column using case insensitive compare methot.
        /// </summary>
        bool HasColumn(IDataRecord dr, string columnName);

        /// <summary>
        /// Rollbacks current transaction.
        /// </summary>
        void RollBack();

        /// <summary>
        /// ProjectBase uses an external transaction.
        /// </summary>
        void UseExternalTransaction(IDbTransaction exTransaction);

        /// <summary>
        /// ProjectBase returns true if there is an available transaction that is not committed or rolled back.
        /// </summary>
        bool IsProcessEnded(IDbTransaction exTransaction);
    }
}