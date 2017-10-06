using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using ProjectBase.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Database
{
    //vyigity
    public class SqlQueryGenerator:IQueryGenerator
    {
        public String TableName { get; set; }
        List<SqlParameter> DataParameters;
        List<SqlParameter> FilterParameters;

        bool isFilled = false;

        public string SelectText { get; set; }
        public string FilterText { get; set; }
        public string SelectTail { get; set; }
        public string ProcedureName { get; set; }

        SqlCommand command = new SqlCommand();

        public void AddFilterParameter(string parameterName, object value)
        {
            FilterParameters.Add(new SqlParameter(parameterName, value));
        }

        public void AddDataParameter(string parameterName, object value)
        {
            DataParameters.Add(new SqlParameter(parameterName, value));
        }

        public void AddDataParameter(string parameterName, object value, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.Direction = direction;
            DataParameters.Add(param);
        }

        public void AddDataParameter(string parameterName, object value, int size, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            DataParameters.Add(param);
        }

        public void AddDataParameter(string parameterName, object dbType, object value, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, (SqlDbType)dbType);
            param.Direction = direction;
            param.Value = value;
            DataParameters.Add(param);
        }

        public void AddDataParameter(string parameterName, object dbType, object value, int size, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, (SqlDbType)dbType);
            param.Direction = direction;
            param.Value = value;
            param.Size = size;
            DataParameters.Add(param);
        }

        public SqlQueryGenerator()
        {
            DataParameters = new List<SqlParameter>();
            FilterParameters = new List<SqlParameter>();
        }

        public object GetParameterValue(string parameterName)
        {
            foreach (SqlParameter item in command.Parameters)
            {
                if (item.ParameterName == parameterName)
                {
                    if (item.Value.GetType() == typeof(System.Data.SqlTypes.SqlBinary))
                    {
                        if (!((SqlBinary)item.Value).IsNull)
                        {
                            return ((SqlBinary)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(System.Data.SqlTypes.SqlBoolean))
                    {
                        if (!((SqlBoolean)item.Value).IsNull)
                        {
                            return ((SqlBoolean)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(System.Data.SqlTypes.SqlByte))
                    {
                        if (!((SqlByte)item.Value).IsNull)
                        {
                            return ((SqlByte)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(System.Data.SqlTypes.SqlDateTime))
                    {
                        if (!((SqlDateTime)item.Value).IsNull)
                        {
                            return ((SqlDateTime)item.Value).Value;
                        }
                        else
                            return null;

                    }
                    else if (item.Value.GetType() == typeof(System.Data.SqlTypes.SqlDecimal))
                    {
                        if (!((SqlDecimal)item.Value).IsNull)
                        {
                            return ((SqlDecimal)item.Value).Value;
                        }
                        else
                            return null;

                    }
                    else if (item.Value.GetType() == typeof(System.Data.SqlTypes.SqlDouble))
                    {
                        if (!((SqlDouble)item.Value).IsNull)
                        {
                            return ((SqlDouble)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(System.Data.SqlTypes.SqlGuid))
                    {
                        if (!((SqlGuid)item.Value).IsNull)
                        {
                            return ((SqlGuid)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(System.Data.SqlTypes.SqlInt16))
                    {
                        if (!((SqlInt16)item.Value).IsNull)
                        {
                            return ((SqlInt16)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(System.Data.SqlTypes.SqlInt32))
                    {
                        if (!((SqlInt32)item.Value).IsNull)
                        {
                            return ((SqlInt32)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(System.Data.SqlTypes.SqlInt64))
                    {
                        if (!((SqlInt64)item.Value).IsNull)
                        {
                            return ((SqlInt64)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(System.Data.SqlTypes.SqlMoney))
                    {
                        if (!((SqlMoney)item.Value).IsNull)
                        {
                            return ((SqlMoney)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(System.Data.SqlTypes.SqlSingle))
                    {
                        if (!((SqlSingle)item.Value).IsNull)
                        {
                            return ((SqlSingle)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else
                    {
                        throw new InvalidOperationException("Belirtilen değere karşılık gelen nesne tipi bulunamadı");
                    }
                }
            }

            throw new KeyNotFoundException("Belirtilen anahtar değerine sahip bir parametre bulunamadı");
        }

        public IDbCommand GetInsertCommand()
        {
            if (!isFilled)
            {
                StringBuilder bString = new StringBuilder("INSERT INTO ");
                StringBuilder dString = new StringBuilder("(");
                StringBuilder vString = new StringBuilder("(");

                foreach (SqlParameter param in DataParameters)
                {
                    dString.Append(param.ParameterName);
                    dString.Append(",");

                    vString.Append(":");
                    vString.Append(param.ParameterName);
                    vString.Append(",");

                    command.Parameters.Add(param);
                }

                dString.Remove(dString.Length - 1, 1);
                vString.Remove(vString.Length - 1, 1);

                dString.Append(")");
                vString.Append(")");

                bString.Append(TableName);
                bString.Append(dString.ToString());

                bString.Append(" VALUES ");
                bString.Append(vString);

                command.CommandText = bString.ToString();
            }

            isFilled = true;

            return command;
        }

        public IDbCommand GetUpdateCommand()
        {
            if (!isFilled)
            {
                StringBuilder bString = new StringBuilder("UPDATE ");
                bString.Append(TableName);
                bString.Append(" SET ");

                foreach (SqlParameter param in DataParameters)
                {
                    bString.Append(param.ParameterName);
                    bString.Append("=:");
                    bString.Append(param.ParameterName);
                    bString.Append(",");

                    command.Parameters.Add(param);
                }

                bString.Remove(bString.Length - 1, 1);

                if (FilterText != null)
                {
                    bString.Append(" ");
                    FilterText.Replace(":", "@");
                    bString.Append(FilterText);
                }

                foreach (SqlParameter param in FilterParameters)
                {
                    command.Parameters.Add(param);
                }

                command.CommandText = bString.ToString();
            }

            isFilled = true;

            return command;
        }

        public IDbCommand GetSelectCommandBasic()
        {
            if (!isFilled)
            {
                StringBuilder bString = new StringBuilder(SelectText);

                if (FilterText != null)
                {
                    bString.Append(" ");

                    FilterText = FilterText.Replace(":", "@");
                    bString.Append(FilterText);
                }

                foreach (SqlParameter param in FilterParameters)
                {
                    command.Parameters.Add(param);
                }

                bString.Append(" ");

                if (SelectTail != null)
                    bString.Append(SelectTail);

                command.CommandText = bString.ToString();
            }

            isFilled = true;

            return command;
        }

        public IDbCommand GetProcedure()
        {
            if (!isFilled)
            {
                foreach (SqlParameter param in DataParameters)
                {
                    command.Parameters.Add(param);
                }

                command.CommandText = ProcedureName;
                command.CommandType = System.Data.CommandType.StoredProcedure;
            }

            isFilled = true;

            return command;
        }

        public void Clear()
        {
            if (DataParameters != null)
                DataParameters.Clear();
            if (FilterParameters != null)
                FilterParameters.Clear();

            TableName = null;
            SelectText = null;
            FilterText = null;
            SelectTail = null;
            ProcedureName = null;
            command = new SqlCommand();
            isFilled = false;
        }

        public void AddDataParameter(string parameterName, DbType dbType, object value, int size, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.DbType = dbType;
            DataParameters.Add(param);
        }
    }
}
