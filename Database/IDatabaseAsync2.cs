using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace ProjectBase.Database
{
    public interface IDatabaseAsync2: IDisposable
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
        /// Asynchronously executes a sql query and returns affected row count.
        /// </summary>
        Task<int> ExecuteQueryAsync(string query);

        /// <summary>
        /// Asynchronously executes a sql command and returns affected row count.
        /// </summary>
        Task<int> ExecuteQueryAsync(IDbCommand query);

        /// <summary>
        /// Asynchronously executes a sql select query and returns results as a data table object.
        /// </summary>
        Task<DataTable> ExecuteQueryDataTableAsync(string query);

        /// <summary>
        /// Asynchronously executes a sql select command and returns results as a data table object.
        /// </summary>
        Task<DataTable> ExecuteQueryDataTableAsync(IDbCommand query);

        /// <summary>
        /// Asynchronously executes a sql select query and fills a data table object.
        /// </summary>
        Task FillObjectAsync(DataTable table, string query);

        /// <summary>
        /// Asynchronously executes a sql select command and fills a data table object.
        /// </summary>
        Task FillObjectAsync(DataTable table, IDbCommand query);

        /// <summary>
        /// Asynchronously executes a sql select query and fills a dataset object.
        /// </summary>
        Task FillObjectAsync(DataSet set, string table, string query);

        /// <summary>
        /// Asynchronously executes a sql select command and fills a dataset object.
        /// </summary>
        Task FillObjectAsync(DataSet set, string table, IDbCommand query);

        /// <summary>
        /// Asynchronously executes a sql select query and returns a data reader object.
        /// </summary>
        Task<IDataReader> GetDataReaderAsync(string query);

        /// <summary>
        /// Asynchronously executes a sql select command and returns a data reader object.
        /// </summary>
        Task<IDataReader> GetDataReaderAsync(IDbCommand query);

        /// <summary>
        /// Asynchronously executes a sql select query and returns results as a desired type object.
        /// </summary>
        Task<T> GetObjectAsync<T>(string query);

        /// <summary>
        /// Asynchronously executes a sql select command and returns results as a desired type object.
        /// </summary>
        Task<T> GetObjectAsync<T>(IDbCommand query);

        /// <summary>
        /// Asynchronously executes a sql select query and returns results as a list of desired type objects.
        /// </summary>
        Task<List<T>> GetObjectListAsync<T>(string query);

        /// <summary>
        /// Asynchronously executes a sql select command and returns results as a list of desired type objects.
        /// </summary>
        Task<List<T>> GetObjectListAsync<T>(IDbCommand query);

        /// <summary>
        /// Asynchronously executes a sql select query and returns results result as a single value.
        /// </summary>
        Task<object> GetSingleValueAsync(string query);

        /// <summary>
        /// Asynchronously executes a sql select command and returns results result as a single value.
        /// </summary>
        Task<object> GetSingleValueAsync(IDbCommand query);

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
        /// Project Base returns true if there is an available transaction that is not committed or rolled back.
        /// </summary>
        bool IsProcessEnded(IDbTransaction exTransaction);
    }
}