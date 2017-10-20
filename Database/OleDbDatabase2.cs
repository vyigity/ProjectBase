using ProjectBase.AppContext;
using ProjectBase.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Database
{
    //vyigity
    public class OleDbDatabase2: DatabaseBase, IDisposable, IDatabase2
    {
        public OleDbDatabase2() : base() { }
        public OleDbDatabase2(DbSettings setting) : base(setting) { }
        public OleDbDatabase2(DbSettings setting, IsolationLevel isolation) : base(setting, isolation) { }
        public override DataTable ExecuteQueryDataTable(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                GetConnection();
                OleDbDataAdapter oraadap = new OleDbDataAdapter(new OleDbCommand(query, myCon as OleDbConnection));
                oraadap.Fill(dt);
                return dt;
            }
            catch (OleDbException ex)
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
            OleDbCommand command = query as OleDbCommand;

            try
            {
                GetConnection();
                query.Connection = myCon;
                OleDbDataAdapter oraadap = new OleDbDataAdapter(command);
                oraadap.Fill(dt);
                return dt;
            }
            catch (OleDbException ex)
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
                OleDbDataAdapter oraadap = new OleDbDataAdapter(new OleDbCommand(query, myCon as OleDbConnection));
                oraadap.Fill(set, table);
            }
            catch (OleDbException ex)
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
            OleDbCommand command = query as OleDbCommand;

            try
            {
                GetConnection();
                OleDbDataAdapter oraadap = new OleDbDataAdapter(command);
                oraadap.Fill(set, table);
            }
            catch (OleDbException ex)
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
                OleDbDataAdapter oraadap = new OleDbDataAdapter(new OleDbCommand(query, myCon as OleDbConnection));
                oraadap.Fill(table);

            }
            catch (OleDbException ex)
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
            OleDbCommand command = query as OleDbCommand;

            try
            {
                GetConnection();
                query.Connection = myCon;
                OleDbDataAdapter oraadap = new OleDbDataAdapter(command);
                oraadap.Fill(table);

            }
            catch (OleDbException ex)
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
            OleDbCommand oracomm = null;

            try
            {
                GetConnection();
                oracomm = new OleDbCommand(query, myCon as OleDbConnection);
                return oracomm.ExecuteNonQuery();
            }
            catch (OleDbException ex)
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
            catch (OleDbException ex)
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
            OleDbCommand oracomm = null;
            try
            {
                GetConnection();
                oracomm = new OleDbCommand(query, myCon as OleDbConnection);
                return oracomm.ExecuteScalar();
            }
            catch (OleDbException ex)
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
            catch (OleDbException ex)
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
            OleDbCommand comm = null;

            try
            {
                GetConnection();
                comm = new OleDbCommand(query, myCon as OleDbConnection);
                return comm.ExecuteReader();
            }
            catch (OleDbException ex)
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
            catch (OleDbException ex)
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
            OleDbDataReader reader = GetDataReader(query) as OleDbDataReader;

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
            catch (OleDbException ex)
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
            OleDbDataReader reader = GetDataReader(query) as OleDbDataReader;

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
            catch (OleDbException ex)
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
            OleDbDataReader reader = GetDataReader(query) as OleDbDataReader;
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
            catch (OleDbException ex)
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
            OleDbDataReader reader = GetDataReader(query) as OleDbDataReader;
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
            catch (OleDbException ex)
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
            return new OleDbConnection(connectionString);
        }
    }
}
