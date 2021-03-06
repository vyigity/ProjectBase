﻿using Npgsql;
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
    public class NpgsqlDatabase2 : DatabaseBase, IDisposable, IDatabase2, IDatabaseAsync2
    {
        /// <summary>
        /// Instantiates a new database interaction object.
        /// </summary>
        public NpgsqlDatabase2() : base() { }
        /// <summary>
        /// Instantiates a new database interaction object.
        /// </summary>
        public NpgsqlDatabase2(DbSettings setting) : base(setting) { }
        /// <summary>
        /// Instantiates a new database interaction object.
        /// </summary>
        public NpgsqlDatabase2(DbSettings setting, IsolationLevel isolation) : base(setting, isolation) { }
        /// <summary>
        /// Executes a sql select query and returns results as a data table object.
        /// </summary>
        public override DataTable ExecuteQueryDataTable(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                GetConnection();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(new NpgsqlCommand(query, myCon as NpgsqlConnection));
                adapter.Fill(dt);
                return dt;
            }
            catch (NpgsqlException ex)
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
            NpgsqlCommand command = query as NpgsqlCommand;

            try
            {
                GetConnection();
                query.Connection = myCon;
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                adapter.Fill(dt);
                return dt;
            }
            catch (NpgsqlException ex)
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
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(new NpgsqlCommand(query, myCon as NpgsqlConnection));
                adapter.Fill(set, table);
            }
            catch (NpgsqlException ex)
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
            NpgsqlCommand command = query as NpgsqlCommand;

            try
            {
                GetConnection();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                adapter.Fill(set, table);
            }
            catch (NpgsqlException ex)
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
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(new NpgsqlCommand(query, myCon as NpgsqlConnection));
                adapter.Fill(table);

            }
            catch (NpgsqlException ex)
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
            NpgsqlCommand command = query as NpgsqlCommand;

            try
            {
                GetConnection();
                query.Connection = myCon;
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                adapter.Fill(table);

            }
            catch (NpgsqlException ex)
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
            NpgsqlCommand command = null;

            try
            {
                GetConnection();
                command = new NpgsqlCommand(query, myCon as NpgsqlConnection);
                return command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
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
            catch (NpgsqlException ex)
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
            NpgsqlCommand command = null;
            try
            {
                GetConnection();
                command = new NpgsqlCommand(query, myCon as NpgsqlConnection);
                return command.ExecuteScalar();
            }
            catch (NpgsqlException ex)
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
            catch (NpgsqlException ex)
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
            NpgsqlCommand command = null;

            try
            {
                GetConnection();
                command = new NpgsqlCommand(query, myCon as NpgsqlConnection);
                return command.ExecuteReader();
            }
            catch (NpgsqlException ex)
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
            catch (NpgsqlException ex)
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
            NpgsqlDataReader reader = GetDataReader(query) as NpgsqlDataReader;

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
            catch (NpgsqlException ex)
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
            NpgsqlDataReader reader = GetDataReader(query) as NpgsqlDataReader;

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
            catch (NpgsqlException ex)
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
            NpgsqlDataReader reader = GetDataReader(query) as NpgsqlDataReader;
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
            catch (NpgsqlException ex)
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
            NpgsqlDataReader reader = GetDataReader(query) as NpgsqlDataReader;
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
            catch (NpgsqlException ex)
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
            return new NpgsqlConnection(connectionString);
        }

        /// <summary>
        /// Asynchronously executes a sql query and returns affected row count.
        /// </summary>
        public Task<int> ExecuteQueryAsync(string query)
        {
            NpgsqlCommand command = null;

            try
            {
                GetConnection();
                command = new NpgsqlCommand(query, myCon as NpgsqlConnection);
                return Task.Run(() => { return command.ExecuteNonQuery(); });
            }
            catch (NpgsqlException ex)
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
            catch (NpgsqlException ex)
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
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(new NpgsqlCommand(query, myCon as NpgsqlConnection));
                return Task.Run(() => { adapter.Fill(dt); return dt; });
            }
            catch (NpgsqlException ex)
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
            NpgsqlCommand command = query as NpgsqlCommand;

            try
            {
                GetConnection();
                query.Connection = myCon;
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                return Task.Run(() => { adapter.Fill(dt); return dt; });
            }
            catch (NpgsqlException ex)
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
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(new NpgsqlCommand(query, myCon as NpgsqlConnection));
                return Task.Run(() => { adapter.Fill(table); return table; });
            }
            catch (NpgsqlException ex)
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
            NpgsqlCommand command = query as NpgsqlCommand;

            try
            {
                GetConnection();
                query.Connection = myCon;
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                return Task.Run(() => { adapter.Fill(table); return table; });
            }
            catch (NpgsqlException ex)
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
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(new NpgsqlCommand(query, myCon as NpgsqlConnection));
                return Task.Run(() => { adapter.Fill(set, table); });
            }
            catch (NpgsqlException ex)
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
            NpgsqlCommand command = query as NpgsqlCommand;

            try
            {
                GetConnection();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                return Task.Run(() => { adapter.Fill(set, table); });
            }
            catch (NpgsqlException ex)
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
            NpgsqlCommand command = null;

            try
            {
                GetConnection();
                command = new NpgsqlCommand(query, myCon as NpgsqlConnection);
                return Task.Run(() => { return (IDataReader)command.ExecuteReader(); });
            }
            catch (NpgsqlException ex)
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
            catch (NpgsqlException ex)
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
                NpgsqlDataReader reader = GetDataReaderNoConnection(query) as NpgsqlDataReader;

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
                catch (NpgsqlException ex)
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
                NpgsqlDataReader reader = GetDataReaderNoConnection(query) as NpgsqlDataReader;

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
                catch (NpgsqlException ex)
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
                NpgsqlDataReader reader = GetDataReaderNoConnection(query) as NpgsqlDataReader;

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
                catch (NpgsqlException ex)
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
                NpgsqlDataReader reader = GetDataReaderNoConnection(query) as NpgsqlDataReader;

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
                catch (NpgsqlException ex)
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
            NpgsqlCommand command = null;
            try
            {
                GetConnection();
                command = new NpgsqlCommand(query, myCon as NpgsqlConnection);
                return Task.Run(() => { return command.ExecuteScalar(); });
            }
            catch (NpgsqlException ex)
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
            catch (NpgsqlException ex)
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
            NpgsqlCommand command = null;

            try
            {
                command = new NpgsqlCommand(query, myCon as NpgsqlConnection);
                return command.ExecuteReader();
            }
            catch (NpgsqlException ex)
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
            catch (NpgsqlException ex)
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
