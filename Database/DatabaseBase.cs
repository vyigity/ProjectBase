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
        public DatabaseBase()
        {
            ConnectionString = AppContext2.CONNECTION_STRINGS[AppContext2.DEFAULT_DB];
        }
        public DatabaseBase(DbSettings setting)
        {
            ConnectionString = AppContext2.CONNECTION_STRINGS[AppContext2.DEFAULT_DB];
            this.Setting = setting;
        }
        public DatabaseBase(DbSettings setting, IsolationLevel isolation)
        {
            ConnectionString = AppContext2.CONNECTION_STRINGS[AppContext2.DEFAULT_DB];
            this.Setting = setting;
            this.isolation = isolation;
        }
        abstract public int ExecuteQuery(string query);
        abstract public int ExecuteQuery(IDbCommand query);
        abstract public DataTable ExecuteQueryDataTable(string query);
        abstract public DataTable ExecuteQueryDataTable(IDbCommand query);
        abstract public void FillObject(DataTable table, string query);
        abstract public void FillObject(DataTable table, IDbCommand query);
        abstract public void FillObject(DataSet set, string table, string query);
        abstract public void FillObject(DataSet set, string table, IDbCommand query);
        abstract public IDataReader GetDataReader(string query);
        abstract public IDataReader GetDataReader(IDbCommand query);
        abstract public T GetObject<T>(string query);
        abstract public T GetObject<T>(IDbCommand query);
        abstract public List<T> GetObjectList<T>(string query);
        abstract public List<T> GetObjectList<T>(IDbCommand query);
        abstract public object GetSingleValue(string query);
        abstract public object GetSingleValue(IDbCommand query);
        abstract public bool HasColumn(IDataRecord dr, string columnName);
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
        public virtual void CloseConnection()
        {
            RollBack();

            if (myCon.State == ConnectionState.Open)
                myCon.Close();

            tran = null;
            processEnded = true;
        }
        public virtual void ClearConnection()
        {
            if (myCon != null)
            {
                CloseConnection();
                myCon = null;
            }
        }
        abstract protected IDbConnection GetDbSpecificConnection(string connectionString);
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
    }
}
