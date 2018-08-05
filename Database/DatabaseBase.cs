using ProjectBase.AppContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Database
{
    public abstract class DatabaseBase : IDisposable
    {
        protected DbSettings setting = DbSettings.AutoConnectionManagement;

        protected IsolationLevel isolation = IsolationLevel.ReadCommitted;

        protected bool closeConnectionImmediate = true;

        protected bool useTransaction = false;

        protected bool processEnded = true;

        protected IDbConnection myCon = null;

        protected IDbTransaction tran = null;

        /// <summary>
        /// Gets or sets transaction isolation level.
        /// </summary>
        public IsolationLevel Isolation
        {
            get
            {
                return isolation;
            }
            set
            {
                if (!processEnded)
                    throw new InvalidOperationException("While running a process, isolation level cannot be changed");

                isolation = value;
            }
        }

        protected ConnectionStringSettings connectionString = null;

        /// <summary>
        /// Gets or sets connection string that is used by database interaction class.
        /// </summary>
        public ConnectionStringSettings ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                if (!processEnded)
                    throw new InvalidOperationException("While running a process, connection string cannot be changed");

                connectionString = value;
            }
        }

        /// <summary>
        /// Gets or sets database access mode.
        /// </summary>
        public DbSettings Setting
        {
            get
            {
                return setting;
            }
            set
            {
                switch (value)
                {
                    case DbSettings.AutoConnectionManagement:

                        closeConnectionImmediate = true;
                        useTransaction = false;

                        break;
                    case DbSettings.TransactionMode:

                        closeConnectionImmediate = false;
                        useTransaction = true;

                        break;
                    case DbSettings.ManuelConnectionManagement:

                        closeConnectionImmediate = false;
                        useTransaction = false;

                        break;
                    default:

                        closeConnectionImmediate = true;
                        useTransaction = false;

                        break;
                }

                setting = value;
            }
        }

        /// <summary>
        /// Instantiates a new database interaction object.
        /// </summary>
        public DatabaseBase()
        {
            ConnectionString = AppContext2.CONNECTION_STRINGS[AppContext2.DEFAULT_DB];
        }

        /// <summary>
        /// Instantiates a new database interaction object.
        /// </summary>
        public DatabaseBase(DbSettings setting)
        {
            ConnectionString = AppContext2.CONNECTION_STRINGS[AppContext2.DEFAULT_DB];
            this.Setting = setting;
        }

        /// <summary>
        /// Instantiates a new database interaction object.
        /// </summary>
        public DatabaseBase(DbSettings setting, IsolationLevel isolation)
        {
            ConnectionString = AppContext2.CONNECTION_STRINGS[AppContext2.DEFAULT_DB];
            this.Setting = setting;
            this.isolation = isolation;
        }

        /// <summary>
        /// Executes a sql query and returns affected row count.
        /// </summary>
        abstract public int ExecuteQuery(string query);

        /// <summary>
        /// Executes a sql command and returns affected row count.
        /// </summary>
        abstract public int ExecuteQuery(IDbCommand query);

        /// <summary>
        /// Executes a sql select query and returns results as a data table object.
        /// </summary>
        abstract public DataTable ExecuteQueryDataTable(string query);

        /// <summary>
        /// Executes a sql select command and returns results as a data table object.
        /// </summary>
        abstract public DataTable ExecuteQueryDataTable(IDbCommand query);

        /// <summary>
        /// Executes a sql select query and fills a data table object.
        /// </summary>
        abstract public void FillObject(DataTable table, string query);

        /// <summary>
        /// Executes a sql select command and fills a data table object.
        /// </summary>
        abstract public void FillObject(DataTable table, IDbCommand query);

        /// <summary>
        /// Executes a sql select query and fills a dataset object.
        /// </summary>
        abstract public void FillObject(DataSet set, string table, string query);

        /// <summary>
        /// Executes a sql select command and fills a dataset object.
        /// </summary>
        abstract public void FillObject(DataSet set, string table, IDbCommand query);

        /// <summary>
        /// Executes a sql select query and returns a data reader object.
        /// </summary>
        abstract public IDataReader GetDataReader(string query);

        /// <summary>
        /// Executes a sql select command and returns a data reader object.
        /// </summary>
        abstract public IDataReader GetDataReader(IDbCommand query);

        /// <summary>
        /// Executes a sql select query and returns results as a desired type object.
        /// </summary>
        abstract public T GetObject<T>(string query);

        /// <summary>
        /// Executes a sql select command and returns results as a desired type object.
        /// </summary>
        abstract public T GetObject<T>(IDbCommand query);

        /// <summary>
        /// Executes a sql select query and returns results as a list of desired type objects.
        /// </summary>
        abstract public List<T> GetObjectList<T>(string query);

        /// <summary>
        /// Executes a sql select command and returns results as a list of desired type objects.
        /// </summary>
        abstract public List<T> GetObjectList<T>(IDbCommand query);

        /// <summary>
        /// Executes a sql select query and returns results result as a single value.
        /// </summary>
        abstract public object GetSingleValue(string query);

        /// <summary>
        /// Executes a sql select command and returns results result as a single value.
        /// </summary>
        abstract public object GetSingleValue(IDbCommand query);

        /// <summary>
        /// Verifies if a data record has desired column using case insensitive compare methot.
        /// </summary>
        abstract public bool HasColumn(IDataRecord dr, string columnName);

        /// <summary>
        /// Disposes database interaction object.
        /// </summary>
        public void Dispose()
        {
            CloseConnection();
        }

        protected void Close()
        {
            if (myCon.State == ConnectionState.Open && closeConnectionImmediate)
                myCon.Close();
        }

        protected void GetTransaction()
        {
            if (useTransaction && processEnded)
            {
                tran = myCon.BeginTransaction(isolation);
                processEnded = false;
            }
        }

        /// <summary>
        /// Commits current transaction.
        /// </summary>
        public virtual void Commit()
        {
            if (useTransaction)
            {
                if (!processEnded)
                {
                    tran.Commit();
                    processEnded = true;
                }
            }
        }

        /// <summary>
        /// Rollbacks current transaction.
        /// </summary>
        public virtual void RollBack()
        {
            if (useTransaction)
            {
                if (!processEnded)
                {
                    tran.Rollback();
                    processEnded = true;
                }
            }
        }

        /// <summary>
        /// Rollbacks current transaction if in transaction mod and closes current connection.
        /// </summary>
        public virtual void CloseConnection()
        {
            RollBack();

            if (myCon.State == ConnectionState.Open)
                myCon.Close();

            tran = null;
            processEnded = true;
        }

        /// <summary>
        /// Rollbacks current transaction if in transaction mod and closes current connection.
        /// </summary>
        public virtual void ClearConnection()
        {
            if (myCon != null)
            {
                CloseConnection();
                myCon = null;
            }
        }

        abstract protected IDbConnection GetDbSpecificConnection(string connectionString);

        /// <summary>
        /// Returns current connection object.
        /// </summary>
        public virtual IDbConnection GetConnection()
        {
            if (myCon != null)
            {
                if (myCon.State == ConnectionState.Closed)
                    myCon.Open();

                GetTransaction();

                return myCon;
            }
            else
            {
                myCon = GetDbSpecificConnection(connectionString.ConnectionString);

                if (myCon.State == ConnectionState.Closed)
                    myCon.Open();

                GetTransaction();

                return myCon;
            }
        }

        /// <summary>
        /// ProjectBase uses an external transaction.
        /// </summary>
        public void UseExternalTransaction(IDbTransaction exTransaction)
        {
            if (setting == DbSettings.TransactionMode)
            {
                if (processEnded && (myCon == null || myCon.State == ConnectionState.Closed))
                {
                    if (exTransaction.Connection.State == ConnectionState.Open)
                    {
                        myCon = exTransaction.Connection;
                        tran = exTransaction;
                        processEnded = false;
                    }
                    else
                    {
                        throw new InvalidOperationException("External connection must be in a opened state.");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Currently used connection is not in closed state or there is a currently available transaction. This operation requires a clean state.");
                }
            }
            else
            {
                throw new InvalidOperationException("This operation is available only in transaction mode.");
            }
        }

        /// <summary>
        /// Project Base returns true if there is an available transaction that is not committed or rolled back.
        /// </summary>
        public bool IsProcessEnded(IDbTransaction exTransaction)
        {
            return processEnded;
        }
    }
}
