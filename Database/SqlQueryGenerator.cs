using ProjectBase.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Database
{
    /// <summary>
    /// Can be used for database command generation with helper functions.
    /// </summary>
    public class SqlQueryGenerator:IQueryGenerator
    {
        List<SqlParameter> DataParameters;
        List<SqlParameter> FilterParameters;
        bool isFilled = false;
        SqlCommand command = new SqlCommand();

        public SqlQueryGenerator()
        {
            DataParameters = new List<SqlParameter>();
            FilterParameters = new List<SqlParameter>();
        }

        /// <summary>
        /// Query generator will use this string as table name while generating update and insert statements.
        /// </summary>
        public String TableName { get; set; }
        /// <summary>
        /// Query generator will use this string as main sql query text. It can be used for any kind of command like DML and DDL. It can be used mainly for a select query.
        /// </summary>
        public string SelectText { get; set; }
        /// <summary>
        /// Query generator will concate this string to end of query. String must include sql key word like WHERE. For parameter usage in query, symbols of : or @ can be used. For UPDATE generation, this must be used for specify filter text.
        /// </summary>
        public string FilterText { get; set; }
        /// <summary>
        /// Query generator will concate this string to end of query. It can be used for group by expressions.
        /// </summary>
        public string SelectTail { get; set; }
        /// <summary>
        /// Query generator will use this string as procedure name. It can be a database function or procedure.
        /// </summary>
        public string ProcedureName { get; set; }
        
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with SelectText property.
        /// </summary>
        public void AddFilterParameter(string parameterName, object value)
        {
            FilterParameters.Add(new SqlParameter(parameterName, value));
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with SelectText property.
        /// </summary>
        public void AddFilterParameter(string parameterName, object value, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.Direction = direction;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with SelectText property.
        /// </summary>
        public void AddFilterParameter(string parameterName, object value, int size, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with SelectText property.
        /// </summary>
        public void AddFilterParameter(string parameterName, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.Scale = scale;
            param.Precision = precision;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with SelectText property.
        /// </summary>
        public void AddFilterParameter(string parameterName, object dbType, object value, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, (SqlDbType)dbType);
            param.Direction = direction;
            param.Value = value;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with SelectText property.
        /// </summary>
        public void AddFilterParameter(string parameterName, object dbType, object value, int size, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, (SqlDbType)dbType);
            param.Direction = direction;
            param.Value = value;
            param.Size = size;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with SelectText property.
        /// </summary>
        public void AddFilterParameter(string parameterName, object dbType, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, (SqlDbType)dbType);
            param.Direction = direction;
            param.Value = value;
            param.Size = size;
            param.Scale = scale;
            param.Precision = precision;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with SelectText property.
        /// </summary>
        public void AddFilterParameter(string parameterName, DbType dbType, object value, int size, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.DbType = dbType;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with SelectText property.
        /// </summary>
        public void AddFilterParameter(string parameterName, DbType dbType, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.DbType = dbType;
            param.Scale = scale;
            param.Precision = precision;
            FilterParameters.Add(param);
        }

        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public void AddDataParameter(string parameterName, object value)
        {
            DataParameters.Add(new SqlParameter(parameterName, value));
        }   
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public void AddDataParameter(string parameterName, object value, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.Direction = direction;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public void AddDataParameter(string parameterName, object value, int size, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public void AddDataParameter(string parameterName, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.Scale = scale;
            param.Precision = precision;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public void AddDataParameter(string parameterName, object dbType, object value, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, (SqlDbType)dbType);
            param.Direction = direction;
            param.Value = value;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public void AddDataParameter(string parameterName, object dbType, object value, int size, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, (SqlDbType)dbType);
            param.Direction = direction;
            param.Value = value;
            param.Size = size;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public void AddDataParameter(string parameterName, object dbType, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, (SqlDbType)dbType);
            param.Direction = direction;
            param.Value = value;
            param.Size = size;
            param.Scale = scale;
            param.Precision = precision;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public void AddDataParameter(string parameterName, DbType dbType, object value, int size, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.DbType = dbType;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public void AddDataParameter(string parameterName, DbType dbType, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.DbType = dbType;
            param.Scale = scale;
            param.Precision = precision;
            DataParameters.Add(param);
        }
        
        /// <summary>
        /// Returns a database returned parameter.
        /// </summary>
        public object GetParameterValue(string parameterName)
        {
            foreach (SqlParameter item in command.Parameters)
            {
                if (item.ParameterName == parameterName)
                {
                    return item.Value;
                }
            }

            throw new KeyNotFoundException("Belirtilen anahtar değerine sahip bir parametre bulunamadı");
        }
        /// <summary>
        /// Returns generated insert command.
        /// </summary>
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

                    vString.Append("@");
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
        /// <summary>
        /// Returns generated update command.
        /// </summary>
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
                    bString.Append("=@");
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
        /// <summary>
        /// Returns generated general command.
        /// </summary>
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
        /// <summary>
        /// Returns generated procedure command.
        /// </summary>
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
        /// <summary>
        /// Clears all query generater instance.
        /// </summary>
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
    }
}
