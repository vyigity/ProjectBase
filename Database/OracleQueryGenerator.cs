using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
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
    public class OracleQueryGenerator : QueryGeneratorBase, IQueryGenerator
    {     
        List<OracleParameter> DataParameters;
        List<OracleParameter> FilterParameters;
        bool isFilled = false;
        OracleCommand command = new OracleCommand();

        public OracleQueryGenerator() : base(':')
        {
            DataParameters = new List<OracleParameter>();
            FilterParameters = new List<OracleParameter>();
            command.BindByName = true;
        }

        public OracleQueryGenerator(ParameterMode ParameterProcessingMode) : base(':')
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
                    if (item.Value.GetType() == typeof(Oracle.DataAccess.Types.OracleBFile))
                    {
                        if (!((OracleBFile)item.Value).IsNull)
                        {
                            return ((OracleBFile)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(Oracle.DataAccess.Types.OracleBinary))
                    {
                        if (!((OracleBinary)item.Value).IsNull)
                        {
                            return ((OracleBinary)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(Oracle.DataAccess.Types.OracleBlob))
                    {
                        if (!((OracleBlob)item.Value).IsNull)
                        {
                            return ((OracleBlob)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(Oracle.DataAccess.Types.OracleClob))
                    {
                        if (!((OracleClob)item.Value).IsNull)
                        {
                            return ((OracleClob)item.Value).Value;
                        }
                        else
                            return null;

                    }
                    else if (item.Value.GetType() == typeof(Oracle.DataAccess.Types.OracleDate))
                    {
                        if (!((OracleDate)item.Value).IsNull)
                        {
                            return ((OracleDate)item.Value).Value;
                        }
                        else
                            return null;

                    }
                    else if (item.Value.GetType() == typeof(Oracle.DataAccess.Types.OracleDecimal))
                    {
                        if (!((OracleDecimal)item.Value).IsNull)
                        {
                            return ((OracleDecimal)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(Oracle.DataAccess.Types.OracleIntervalDS))
                    {
                        if (!((OracleIntervalDS)item.Value).IsNull)
                        {
                            return ((OracleIntervalDS)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(Oracle.DataAccess.Types.OracleIntervalYM))
                    {
                        if (!((OracleIntervalYM)item.Value).IsNull)
                        {
                            return ((OracleIntervalYM)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(Oracle.DataAccess.Types.OracleRefCursor))
                    {
                        if (!((OracleRefCursor)item.Value).IsNull)
                        {
                            return ((OracleRefCursor)item.Value).GetDataReader();
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(Oracle.DataAccess.Types.OracleString))
                    {
                        if (!((OracleString)item.Value).IsNull)
                        {
                            return ((OracleString)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(Oracle.DataAccess.Types.OracleTimeStamp))
                    {
                        if (!((OracleTimeStamp)item.Value).IsNull)
                        {
                            return ((OracleTimeStamp)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(Oracle.DataAccess.Types.OracleTimeStampLTZ))
                    {
                        if (!((OracleTimeStampLTZ)item.Value).IsNull)
                        {
                            return ((OracleTimeStampLTZ)item.Value).Value;
                        }
                        else
                            return null;
                    }
                    else if (item.Value.GetType() == typeof(Oracle.DataAccess.Types.OracleTimeStampTZ))
                    {
                        if (!((OracleRefCursor)item.Value).IsNull)
                        {
                            return ((OracleRefCursor)item.Value).GetDataReader();
                        }
                        else
                            return null;
                    }
                    else
                    {
                        throw new InvalidOperationException("Type is invalid.");
                    }
                }
            }

            throw new KeyNotFoundException("Key not found.");
        }
        /// <summary>
        /// Returns generated insert command.
        /// </summary>
        public override IDbCommand GetInsertCommand()
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
        public override IDbCommand GetUpdateCommand()
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
                    bString.Append(GetPreparedCommandString(FilterText, CommandStringType.Filter));
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
        public override IDbCommand GetSelectCommandBasic()
        {
            if (!isFilled)
            {
                StringBuilder bString = new StringBuilder(GetPreparedCommandString(SelectText, CommandStringType.Main));

                if (FilterText != null)
                {
                    bString.Append(" ");
                    bString.Append(GetPreparedCommandString(FilterText, CommandStringType.Filter));
                }

                foreach (OracleParameter param in FilterParameters)
                {
                    command.Parameters.Add(param);
                }

                bString.Append(" ");

                if (SelectTail != null)
                    bString.Append(GetPreparedCommandString(SelectTail, CommandStringType.Tail));

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
