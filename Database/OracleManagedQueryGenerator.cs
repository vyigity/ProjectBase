using Oracle.ManagedDataAccess.Client;
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
    public class OracleManagedQueryGenerator : QueryGeneratorBase, IQueryGenerator
    {
        List<OracleParameter> DataParameters;
        List<OracleParameter> FilterParameters;
        bool isFilled = false;
        OracleCommand command = new OracleCommand();

        public OracleManagedQueryGenerator() : base(':')
        {
            DataParameters = new List<OracleParameter>();
            FilterParameters = new List<OracleParameter>();
            command.BindByName = true;
        }

        public OracleManagedQueryGenerator(ParameterMode ParameterProcessingMode) : base(':')
        {
            DataParameters = new List<OracleParameter>();
            FilterParameters = new List<OracleParameter>();
            this.ParameterProcessingMode = ParameterProcessingMode;
            command.BindByName = true;
        }

        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, object value)
        {
            FilterParameters.Add(new OracleParameter(parameterName, value));
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, object value, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(parameterName, value);
            param.Direction = direction;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, object value, int size, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(parameterName, value);
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
            FilterParameters.Add(new OracleParameter(parameterName, (OracleDbType)dbBaseDbType, value, direction));
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, object dbBaseDbType, object value, int size, ParameterDirection direction)
        {
            FilterParameters.Add(new OracleParameter(parameterName, (OracleDbType)dbBaseDbType, size, value, direction));
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, object dbBaseDbType, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(parameterName, (OracleDbType)dbBaseDbType, size, value, direction);
            param.Scale = scale;
            param.Precision = precision;
            FilterParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        public override void AddFilterParameter(string parameterName, DbType dbType, object value, int size, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(parameterName, value);
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
            OracleParameter param = new OracleParameter(parameterName, value);
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
            DataParameters.Add(new OracleParameter(parameterName, value));
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, object value, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(parameterName, value);
            param.Direction = direction;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, object value, int size, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(parameterName, value);
            param.Direction = direction;
            param.Size = size;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(parameterName, value);
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
            DataParameters.Add(new OracleParameter(parameterName, (OracleDbType)dbBaseDbType, value, direction));
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, object dbBaseDbType, object value, int size, ParameterDirection direction)
        {
            DataParameters.Add(new OracleParameter(parameterName, (OracleDbType)dbBaseDbType, size, value, direction));
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, object dbBaseDbType, object value, int size, byte scale, byte precision, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(parameterName, (OracleDbType)dbBaseDbType, size, value, direction);
            param.Scale = scale;
            param.Precision = precision;
            DataParameters.Add(param);
        }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        public override void AddDataParameter(string parameterName, DbType dbType, object value, int size, ParameterDirection direction)
        {
            OracleParameter param = new OracleParameter(parameterName, value);
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
            OracleParameter param = new OracleParameter(parameterName, value);
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
            foreach (OracleParameter item in command.Parameters)
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

                foreach (OracleParameter param in DataParameters)
                {
                    dString.Append(param.ParameterName);
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
        public override IDbCommand GetUpdatecommand()
        {
            if (!isFilled)
            {
                StringBuilder bString = new StringBuilder("UPDATE ");
                bString.Append(TableName);
                bString.Append(" SET ");

                foreach (OracleParameter param in DataParameters)
                {
                    bString.Append(param.ParameterName);
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

                foreach (OracleParameter param in FilterParameters)
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

                foreach (OracleParameter param in FilterParameters)
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
                foreach (OracleParameter param in DataParameters)
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
            command = new OracleCommand();
            isFilled = false;
        }
    }
}
