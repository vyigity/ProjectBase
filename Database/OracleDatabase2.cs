using Oracle.DataAccess.Client;
using ProjectBase.AppContext;
using ProjectBase.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Database
{
    //vyigity
    public class OracleDatabase2 : IDisposable, IDatabase2
    {
        OracleConnection myCon = null;
        OracleTransaction tran = null;
        OracleCommand command = null;

        DbSettings setting = DbSettings.AutoConnectionManagement;
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

        bool closeConnectionImmediate = true;
        bool useTransaction = false;
        bool processEnded = true;

        IsolationLevel isolation = IsolationLevel.ReadCommitted;
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

        ConnectionStringSettings connectionString = null;
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

        public OracleDatabase2()
        {
            ConnectionString = AppContext2.CONNECTION_STRINGS[AppContext2.DEFAULT_DB];
        }

        public OracleDatabase2(DbSettings setting)
        {
            ConnectionString = AppContext2.CONNECTION_STRINGS[AppContext2.DEFAULT_DB];
            this.Setting = setting;
        }

        public OracleDatabase2(DbSettings setting, IsolationLevel isolation)
        {
            ConnectionString = AppContext2.CONNECTION_STRINGS[AppContext2.DEFAULT_DB];
            this.Setting = setting;
            this.isolation = isolation;
        }

        void Close()
        {
            if (myCon.State == ConnectionState.Open && closeConnectionImmediate)
                myCon.Close();
        }

        public void Commit()
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

        public void RollBack()
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

        public void CloseConnection()
        {
            RollBack();

            if (myCon.State == ConnectionState.Open)
                myCon.Close();

            tran = null;
            processEnded = true;
        }

        public void ClearConnection()
        {
            if (myCon != null)
            {
                CloseConnection();
                myCon = null;
            }
        }

        public IDbConnection GetConnection()
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
                myCon = new OracleConnection(connectionString.ConnectionString);

                if (myCon.State == ConnectionState.Closed)
                    myCon.Open();

                GetTransaction();

                return myCon;
            }
        }

        void GetTransaction()
        {
            if (useTransaction && processEnded)
            {
                tran = myCon.BeginTransaction(isolation);
                processEnded = false;
            }
        }

        public DataTable ExecuteQueryDataTable(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                GetConnection();
                OracleDataAdapter oraadap = new OracleDataAdapter(new OracleCommand(query, myCon));
                oraadap.Fill(dt);
                return dt;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public DataTable ExecuteQueryDataTable(IDbCommand query)
        {
            DataTable dt = new DataTable();
            command = query as OracleCommand;

            try
            {
                GetConnection();
                query.Connection = myCon;
                OracleDataAdapter oraadap = new OracleDataAdapter(command);
                oraadap.Fill(dt);
                return dt;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public void FillObject(DataSet set, string table, string query)
        {
            DataTable dt = new DataTable();

            try
            {
                GetConnection();
                OracleDataAdapter oraadap = new OracleDataAdapter(new OracleCommand(query, myCon));
                oraadap.Fill(set, table);
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public void FillObject(DataSet set, string table, IDbCommand query)
        {
            DataTable dt = new DataTable();
            query.Connection = myCon;
            command = query as OracleCommand;

            try
            {
                GetConnection();
                OracleDataAdapter oraadap = new OracleDataAdapter(command);
                oraadap.Fill(set, table);
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public void FillObject(DataTable table, string query)
        {
            DataTable dt = new DataTable();

            try
            {
                GetConnection();
                OracleDataAdapter oraadap = new OracleDataAdapter(new OracleCommand(query, myCon));
                oraadap.Fill(table);

            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public void FillObject(DataTable table, IDbCommand query)
        {
            DataTable dt = new DataTable();
            command = query as OracleCommand;

            try
            {
                GetConnection();
                query.Connection = myCon;
                OracleDataAdapter oraadap = new OracleDataAdapter(command);
                oraadap.Fill(table);

            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public int ExecuteQuery(string query)
        {
            OracleCommand oracomm = null;

            try
            {
                GetConnection();
                oracomm = new OracleCommand(query, myCon);
                return oracomm.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public int ExecuteQuery(IDbCommand query)
        {
            try
            {
                GetConnection();
                query.Connection = myCon;
                return query.ExecuteNonQuery(); ;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public object GetSingleValue(string query)
        {
            OracleCommand oracomm = null;
            try
            {
                GetConnection();
                oracomm = new OracleCommand(query, myCon);
                return oracomm.ExecuteScalar();
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }

        }

        public object GetSingleValue(IDbCommand query)
        {
            try
            {
                GetConnection();
                query.Connection = myCon;
                return query.ExecuteScalar();
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }

        }

        public IDataReader GetDataReader(string query)
        {
            OracleCommand comm = null;

            try
            {
                GetConnection();
                comm = new OracleCommand(query, myCon);
                return comm.ExecuteReader();
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IDataReader GetDataReader(IDbCommand query)
        {
            try
            {
                GetConnection();
                query.Connection = myCon;
                return query.ExecuteReader();
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetObject<T>(string query)
        {
            OracleDataReader reader = GetDataReader(query) as OracleDataReader;

            try
            {
                T instance = (T)Activator.CreateInstance(typeof(T));

                reader.Read();

                var props = typeof(T).GetProperties();

                foreach (PropertyInfo inf in props)
                {
                    if (HasColumn(reader, inf.Name))
                    {
                        inf.SetValue(instance, Util.IsNull(reader[inf.Name]) ? null : Util.GetProperty(reader[inf.Name], inf.PropertyType));
                    }
                }

                return instance;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!reader.IsClosed)
                    reader.Close();

                Close();
            }
        }

        public T GetObject<T>(IDbCommand query)
        {
            OracleDataReader reader = GetDataReader(query) as OracleDataReader;

            try
            {
                T instance = (T)Activator.CreateInstance(typeof(T));

                reader.Read();

                var props = typeof(T).GetProperties();

                foreach (PropertyInfo inf in props)
                {
                    if (HasColumn(reader, inf.Name))
                    {
                        inf.SetValue(instance, Util.IsNull(reader[inf.Name]) ? null : Util.GetProperty(reader[inf.Name], inf.PropertyType));
                    }
                }

                return instance;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!reader.IsClosed)
                    reader.Close();

                Close();
            }
        }

        public List<T> GetObjectList<T>(string query)
        {
            OracleDataReader reader = GetDataReader(query) as OracleDataReader;
            List<T> entityList = new List<T>();

            try
            {
                var props = typeof(T).GetProperties();

                while (reader.Read())
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));

                    foreach (PropertyInfo inf in props)
                    {
                        if (HasColumn(reader, inf.Name))
                        {
                            inf.SetValue(instance, Util.IsNull(reader[inf.Name]) ? null : Util.GetProperty(reader[inf.Name], inf.PropertyType));
                        }
                    }

                    entityList.Add(instance);
                }

                return entityList;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!reader.IsClosed)
                    reader.Close();
                
                Close();
            }
        }

        public List<T> GetObjectList<T>(IDbCommand query)
        {
            OracleDataReader reader = GetDataReader(query) as OracleDataReader;
            List<T> entityList = new List<T>();

            try
            {
                var props = typeof(T).GetProperties();

                while (reader.Read())
                {
                    T instance = (T)Activator.CreateInstance(typeof(T));

                    foreach (PropertyInfo inf in props)
                    {
                        if (HasColumn(reader, inf.Name))
                        {
                            inf.SetValue(instance, Util.IsNull(reader[inf.Name]) ? null : Util.GetProperty(reader[inf.Name], inf.PropertyType));
                        }
                    }

                    entityList.Add(instance);
                }

                return entityList;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!reader.IsClosed)
                    reader.Close();

                Close();
            }
        }

        public void Dispose()
        {
            CloseConnection();
        }

        public bool HasColumn(IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
