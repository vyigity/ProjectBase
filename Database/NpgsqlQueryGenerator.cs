using Npgsql;
using NpgsqlTypes;
using ProjectBase.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Database
{
    /// <summary>
    /// Can be used for database command generation with helper functions.
    /// </summary>
    public class NpgsqlQueryGenerator : QueryGeneratorBase,IQueryGenerator
    {
        List<NpgsqlParameter> DataParameters;
        List<NpgsqlParameter> FilterParameters;
        bool isFilled = false;
        NpgsqlCommand command = new NpgsqlCommand();

        public NpgsqlQueryGenerator() : base('@')
        {
            DataParameters = new List<NpgsqlParameter>();
            FilterParameters = new List<NpgsqlParameter>();
        }

        public NpgsqlQueryGenerator(ParameterMode ParameterProcessingMode) : base('@')
        {
            DataParameters = new List<NpgsqlParameter>();
            FilterParameters = new List<NpgsqlParameter>();
            this.ParameterProcessingMode = ParameterProcessingMode;
        }

        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, object value)
        {
            FilterParameters.Add(new NpgsqlParameter(parameterName, value));
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, object value, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, object value, int size, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.Scale = scale;
            param.Precision = precision;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, object dbBaseDbType, object value, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            param.NpgsqlDbType = (NpgsqlDbType)dbBaseDbType;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, object dbBaseDbType, object value, int size, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.NpgsqlDbType = (NpgsqlDbType)dbBaseDbType;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, object dbBaseDbType, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.Scale = scale;
            param.Precision = precision;
            param.NpgsqlDbType = (NpgsqlDbType)dbBaseDbType;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, DbType dbType, object value, int size, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.DbType = dbType;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, DbType dbType, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.Scale = scale;
            param.Precision = precision;
            param.DbType = dbType;
            FilterParameters.Add(param);
        }

        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, object value)
        {
            DataParameters.Add(new NpgsqlParameter(parameterName, value));
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, object value, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, object value, int size, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.Scale = scale;
            param.Precision = precision;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, object dbBaseDbType, object value, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            param.NpgsqlDbType = (NpgsqlDbType)dbBaseDbType;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, object dbBaseDbType, object value, int size, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.NpgsqlDbType = (NpgsqlDbType)dbBaseDbType;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, object dbBaseDbType, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.Scale = scale;
            param.Precision = precision;
            param.NpgsqlDbType = (NpgsqlDbType)dbBaseDbType;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, DbType dbType, object value, int size, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.DbType = dbType;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, DbType dbType, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            NpgsqlParameter param = new NpgsqlParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            param.Scale = scale;
            param.Precision = precision;
            param.DbType = dbType;
            DataParameters.Add(param);
        }
        
        /// <summary>
        /// Returns a database returned parameter.
        /// </summary>
        public override object GetParameterValue(string parameterName)
        {
            foreach (NpgsqlParameter item in command.Parameters)
            {
                if (item.ParameterName == parameterName)
                {
                    return item.Value;
                }
            }

            throw new KeyNotFoundException("Key not found.");
        }
        /// <summary>
        /// Returns generated insert command.
        /// </summary>
        public override IDbCommand GetInsertcommand()
        {
            if (!isFilled)
            {
                StringBuilder bString = new StringBuilder("INSERT INTO ");
                StringBuilder dString = new StringBuilder("(");
                StringBuilder vString = new StringBuilder("(");

                foreach (NpgsqlParameter param in DataParameters)
                {
                    dString.Append("\"");
                    dString.Append(param.ParameterName);
                    dString.Append("\"");
                    dString.Append(",");

                    vString.Append(StringProcessor.DbBasedParameterCharacter);
                    vString.Append(param.ParameterName);
                    vString.Append(",");

                    command.Parameters.Add(param);
                }

                dString.Remove(dString.Length - 1, 1);
                vString.Remove(vString.Length - 1, 1);

                dString.Append(")");
                vString.Append(")");

                bString.Append("\"");
                bString.Append(TableName);
                bString.Append("\"");

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
        public override IDbCommand GetUpdatecommand()
        {
            if (!isFilled)
            {
                StringBuilder bString = new StringBuilder("UPDATE ");
                bString.Append("\"");
                bString.Append(TableName);
                bString.Append("\"");
                bString.Append(" SET ");

                foreach (NpgsqlParameter param in DataParameters)
                {
                    bString.Append("\"");
                    bString.Append(param.ParameterName);
                    bString.Append("\"");
                    bString.Append("=");
                    bString.Append(StringProcessor.DbBasedParameterCharacter);
                    bString.Append(param.ParameterName);
                    bString.Append(",");

                    command.Parameters.Add(param);
                }

                bString.Remove(bString.Length - 1, 1);

                if (FilterText != null)
                {
                    bString.Append(" ");
                    bString.Append(GetPreparedcommandString(FilterText, commandStringType.Filter));
                }

                foreach (NpgsqlParameter param in FilterParameters)
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
        public override IDbCommand GetSelectcommandBasic()
        {
            if (!isFilled)
            {
                StringBuilder bString = new StringBuilder(GetPreparedcommandString(SelectText, commandStringType.Main));

                if (FilterText != null)
                {
                    bString.Append(" ");
                    bString.Append(GetPreparedcommandString(FilterText, commandStringType.Filter));
                }

                foreach (NpgsqlParameter param in FilterParameters)
                {
                    command.Parameters.Add(param);
                }

                bString.Append(" ");

                if (SelectTail != null)
                    bString.Append(GetPreparedcommandString(SelectTail, commandStringType.Tail));


                command.CommandText = bString.ToString();
            }

            isFilled = true;

            return command;
        }
        /// <summary>
        /// Returns generated procedure command.
        /// </summary>
        public override IDbCommand GetProcedure()
        {
            if (!isFilled)
            {
                foreach (NpgsqlParameter param in DataParameters)
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
        /// Clears all query generator instance.
        /// </summary>
        public override void Clear()
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
            command = new NpgsqlCommand();
            isFilled = false;
        }
    }
}
