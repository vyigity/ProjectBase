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
    public class OracleDatabase2 : DatabaseBase, IDisposable, IDatabase2
    {
        public OracleDatabase2() : base() { }
        public OracleDatabase2(DbSettings setting) : base(setting) { }
        public OracleDatabase2(DbSettings setting, IsolationLevel isolation) : base(setting, isolation) { }    
        public override DataTable ExecuteQueryDataTable(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                GetConnection();
                OracleDataAdapter oraadap = new OracleDataAdapter(new OracleCommand(query, myCon as OracleConnection));
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
        public override DataTable ExecuteQueryDataTable(IDbCommand query)
        {
            DataTable dt = new DataTable();
            OracleCommand command = query as OracleCommand;

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
        public override void FillObject(DataSet set, string table, string query)
        {
            DataTable dt = new DataTable();

            try
            {
                GetConnection();
                OracleDataAdapter oraadap = new OracleDataAdapter(new OracleCommand(query, myCon as OracleConnection));
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
        public override void FillObject(DataSet set, string table, IDbCommand query)
        {
            DataTable dt = new DataTable();
            query.Connection = myCon;
            OracleCommand command = query as OracleCommand;

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
        public override void FillObject(DataTable table, string query)
        {
            DataTable dt = new DataTable();

            try
            {
                GetConnection();
                OracleDataAdapter oraadap = new OracleDataAdapter(new OracleCommand(query, myCon as OracleConnection));
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
        public override void FillObject(DataTable table, IDbCommand query)
        {
            DataTable dt = new DataTable();
            OracleCommand command = query as OracleCommand;

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
        public override int ExecuteQuery(string query)
        {
            OracleCommand oracomm = null;

            try
            {
                GetConnection();
                oracomm = new OracleCommand(query, myCon as OracleConnection);
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
        public override int ExecuteQuery(IDbCommand query)
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
        public override object GetSingleValue(string query)
        {
            OracleCommand oracomm = null;
            try
            {
                GetConnection();
                oracomm = new OracleCommand(query, myCon as OracleConnection);
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
        public override object GetSingleValue(IDbCommand query)
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
        public override IDataReader GetDataReader(string query)
        {
            OracleCommand comm = null;

            try
            {
                GetConnection();
                comm = new OracleCommand(query, myCon as OracleConnection);
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
        public override IDataReader GetDataReader(IDbCommand query)
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
        public override T GetObject<T>(string query)
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
        public override T GetObject<T>(IDbCommand query)
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
        public override List<T> GetObjectList<T>(string query)
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
        public override List<T> GetObjectList<T>(IDbCommand query)
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
        public override bool HasColumn(IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
        protected override IDbConnection GetDbSpecificConnection(string connectionString)
        {
            return new OracleConnection(connectionString);
        }
    }
}
