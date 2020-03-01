using MySql.Data.MySqlClient;
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
    public class MySqlDatabase2 : DatabaseBase, IDisposable, IDatabase2, IDatabaseAsync2
    {
        /// <summary>
        /// Instantiates a new database interaction object.
        /// </summary>
        public MySqlDatabase2() : base() { }
        /// <summary>
        /// Instantiates a new database interaction object.
        /// </summary>
        public MySqlDatabase2(DbSettings setting) : base(setting) { }
        /// <summary>
        /// Instantiates a new database interaction object.
        /// </summary>
        public MySqlDatabase2(DbSettings setting, IsolationLevel isolation) : base(setting, isolation) { }
        /// <summary>
        /// Executes a sql select query and returns results as a data table object.
        /// </summary>
        public override DataTable ExecuteQueryDataTable(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                GetConnection();
                MySqlDataAdapter adapter = new MySqlDataAdapter(new MySqlCommand(query, myCon as MySqlConnection));
                adapter.Fill(dt);
                return dt;
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Executes a sql select command and returns results as a data table object.
        /// </summary>
        public override DataTable ExecuteQueryDataTable(IDbCommand query)
        {
            DataTable dt = new DataTable();
            MySqlCommand command = query as MySqlCommand;

            try
            {
                GetConnection();
                query.Connection = myCon;
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dt);
                return dt;
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Executes a sql select query and fills a dataset object.
        /// </summary>
        public override void FillObject(DataSet set, string table, string query)
        {
            try
            {
                GetConnection();
                MySqlDataAdapter adapter = new MySqlDataAdapter(new MySqlCommand(query, myCon as MySqlConnection));
                adapter.Fill(set, table);
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Executes a sql select command and fills a dataset object.
        /// </summary>
        public override void FillObject(DataSet set, string table, IDbCommand query)
        {
            query.Connection = myCon;
            MySqlCommand command = query as MySqlCommand;

            try
            {
                GetConnection();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(set, table);
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Executes a sql select query and fills a data table object.
        /// </summary>
        public override void FillObject(DataTable table, string query)
        {
            try
            {
                GetConnection();
                MySqlDataAdapter adapter = new MySqlDataAdapter(new MySqlCommand(query, myCon as MySqlConnection));
                adapter.Fill(table);

            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Executes a sql select command and fills a data table object.
        /// </summary>
        public override void FillObject(DataTable table, IDbCommand query)
        {
            MySqlCommand command = query as MySqlCommand;

            try
            {
                GetConnection();
                query.Connection = myCon;
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);

            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Executes a sql query and returns affected row count.
        /// </summary>
        public override int ExecuteQuery(string query)
        {
            MySqlCommand command = null;

            try
            {
                GetConnection();
                command = new MySqlCommand(query, myCon as MySqlConnection);
                return command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Executes a sql command and returns affected row count.
        /// </summary>
        public override int ExecuteQuery(IDbCommand query)
        {
            try
            {
                GetConnection();
                query.Connection = myCon;
                return query.ExecuteNonQuery(); ;
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Executes a sql select query and returns results result as a single value.
        /// </summary>
        public override object GetSingleValue(string query)
        {
            MySqlCommand command = null;
            try
            {
                GetConnection();
                command = new MySqlCommand(query, myCon as MySqlConnection);
                return command.ExecuteScalar();
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Executes a sql select command and returns results result as a single value.
        /// </summary>
        public override object GetSingleValue(IDbCommand query)
        {
            try
            {
                GetConnection();
                query.Connection = myCon;
                return query.ExecuteScalar();
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Executes a sql select query and returns a data reader object.
        /// </summary>
        public override IDataReader GetDataReader(string query)
        {
            MySqlCommand command = null;

            try
            {
                GetConnection();
                command = new MySqlCommand(query, myCon as MySqlConnection);
                return command.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Executes a sql select command and returns a data reader object.
        /// </summary>
        public override IDataReader GetDataReader(IDbCommand query)
        {
            try
            {
                GetConnection();
                query.Connection = myCon;
                return query.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Executes a sql select query and returns results as a desired type object.
        /// </summary>
        public override T GetObject<T>(string query)
        {
            MySqlDataReader reader = GetDataReader(query) as MySqlDataReader;

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
            catch (MySqlException ex)
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
        /// <summary>
        /// Executes a sql select command and returns results as a desired type object.
        /// </summary>
        public override T GetObject<T>(IDbCommand query)
        {
            MySqlDataReader reader = GetDataReader(query) as MySqlDataReader;

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
            catch (MySqlException ex)
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
        /// <summary>
        /// Executes a sql select query and returns results as a list of desired type objects.
        /// </summary>
        public override List<T> GetObjectList<T>(string query)
        {
            MySqlDataReader reader = GetDataReader(query) as MySqlDataReader;
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
            catch (MySqlException ex)
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
        /// <summary>
        /// Executes a sql select command and returns results as a list of desired type objects.
        /// </summary>
        public override List<T> GetObjectList<T>(IDbCommand query)
        {
            MySqlDataReader reader = GetDataReader(query) as MySqlDataReader;
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
            catch (MySqlException ex)
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
        /// <summary>
        /// Verifies if a data record has desired column using case insensitive compare methot.
        /// </summary>
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
            return new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Asynchronously executes a sql query and returns affected row count.
        /// </summary>
        public Task<int> ExecuteQueryAsync(string query)
        {
            MySqlCommand command = null;

            try
            {
                GetConnection();
                command = new MySqlCommand(query, myCon as MySqlConnection);
                return Task.Run(() => { return command.ExecuteNonQuery(); });
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Asynchronously executes a sql command and returns affected row count.
        /// </summary>
        public Task<int> ExecuteQueryAsync(IDbCommand query)
        {
            try
            {
                GetConnection();
                query.Connection = myCon;
                return Task.Run(() => { return query.ExecuteNonQuery(); });
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Asynchronously executes a sql select query and returns results as a data table object.
        /// </summary>
        public Task<DataTable> ExecuteQueryDataTableAsync(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                GetConnection();
                MySqlDataAdapter adapter = new MySqlDataAdapter(new MySqlCommand(query, myCon as MySqlConnection));
                return Task.Run(() => { adapter.Fill(dt); return dt; });
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Asynchronously executes a sql select command and returns results as a data table object.
        /// </summary>
        public Task<DataTable> ExecuteQueryDataTableAsync(IDbCommand query)
        {
            DataTable dt = new DataTable();
            MySqlCommand command = query as MySqlCommand;

            try
            {
                GetConnection();
                query.Connection = myCon;
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                return Task.Run(() => { adapter.Fill(dt); return dt; });
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Asynchronously executes a sql select query and fills a dataset object.
        /// </summary>
        public Task FillObjectAsync(DataTable table, string query)
        {
            try
            {
                GetConnection();
                MySqlDataAdapter adapter = new MySqlDataAdapter(new MySqlCommand(query, myCon as MySqlConnection));
                return Task.Run(() => { adapter.Fill(table); return table; });
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Asynchronously executes a sql select command and fills a dataset object.
        /// </summary>
        public Task FillObjectAsync(DataTable table, IDbCommand query)
        {
            MySqlCommand command = query as MySqlCommand;

            try
            {
                GetConnection();
                query.Connection = myCon;
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                return Task.Run(() => { adapter.Fill(table); return table; });
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Asynchronously executes a sql select query and fills a data table object.
        /// </summary>
        public Task FillObjectAsync(DataSet set, string table, string query)
        {
            try
            {
                GetConnection();
                MySqlDataAdapter adapter = new MySqlDataAdapter(new MySqlCommand(query, myCon as MySqlConnection));
                return Task.Run(() => { adapter.Fill(set, table); });
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Asynchronously executes a sql select command and fills a data table object.
        /// </summary>
        public Task FillObjectAsync(DataSet set, string table, IDbCommand query)
        {
            query.Connection = myCon;
            MySqlCommand command = query as MySqlCommand;

            try
            {
                GetConnection();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                return Task.Run(() => { adapter.Fill(set, table); });
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Asynchronously executes a sql select query and returns a data reader object.
        /// </summary>
        public Task<IDataReader> GetDataReaderAsync(string query)
        {
            MySqlCommand command = null;

            try
            {
                GetConnection();
                command = new MySqlCommand(query, myCon as MySqlConnection);
                return Task.Run(() => { return (IDataReader)command.ExecuteReader(); });
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Asynchronously executes a sql select command and returns a data reader object.
        /// </summary>
        public Task<IDataReader> GetDataReaderAsync(IDbCommand query)
        {
            try
            {
                GetConnection();
                query.Connection = myCon;
                return Task.Run(() => { return (IDataReader)query.ExecuteReader(); });
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Asynchronously executes a sql select query and returns results as a desired type object.
        /// </summary>
        public Task<T> GetObjectAsync<T>(string query)
        {
            GetConnection();

            return Task.Run(() =>
            {
                MySqlDataReader reader = GetDataReaderNoConnection(query) as MySqlDataReader;

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
                catch (MySqlException ex)
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
            });
        }
        /// <summary>
        /// Asynchronously executes a sql select command and returns results as a desired type object.
        /// </summary>
        public Task<T> GetObjectAsync<T>(IDbCommand query)
        {
            GetConnection();

            return Task.Run(() =>
            {
                MySqlDataReader reader = GetDataReaderNoConnection(query) as MySqlDataReader;

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
                catch (MySqlException ex)
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
            });
        }
        /// <summary>
        /// Asynchronously executes a sql select query and returns results as a list of desired type objects.
        /// </summary>
        public Task<List<T>> GetObjectListAsync<T>(string query)
        {
            List<T> entityList = new List<T>();

            GetConnection();

            return Task.Run(() =>
            {
                MySqlDataReader reader = GetDataReaderNoConnection(query) as MySqlDataReader;

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
                catch (MySqlException ex)
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
            });
        }
        /// <summary>
        /// Asynchronously executes a sql select command and returns results as a list of desired type objects.
        /// </summary>
        public Task<List<T>> GetObjectListAsync<T>(IDbCommand query)
        {
            List<T> entityList = new List<T>();

            GetConnection();

            return Task.Run(() =>
            {
                MySqlDataReader reader = GetDataReaderNoConnection(query) as MySqlDataReader;

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
                catch (MySqlException ex)
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
            });
        }
        /// <summary>
        /// Asynchronously executes a sql select query and returns results result as a single value.
        /// </summary>
        public Task<object> GetSingleValueAsync(string query)
        {
            MySqlCommand command = null;
            try
            {
                GetConnection();
                command = new MySqlCommand(query, myCon as MySqlConnection);
                return Task.Run(() => { return command.ExecuteScalar(); });
            }
            catch (MySqlException ex)
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
        /// <summary>
        /// Asynchronously executes a sql select command and returns results result as a single value.
        /// </summary>
        public Task<object> GetSingleValueAsync(IDbCommand query)
        {
            try
            {
                GetConnection();
                query.Connection = myCon;
                return Task.Run(() => { return query.ExecuteScalar(); });
            }
            catch (MySqlException ex)
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

        private IDataReader GetDataReaderNoConnection(string query)
        {
            MySqlCommand command = null;

            try
            {
                command = new MySqlCommand(query, myCon as MySqlConnection);
                return command.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private IDataReader GetDataReaderNoConnection(IDbCommand query)
        {
            try
            {
                query.Connection = myCon;
                return query.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
