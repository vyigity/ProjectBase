using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Database
{
    public enum ParameterMode { Local, Global }
    public enum commandStringType { Main, Filter, Tail }
    /// <summary>
    /// Can be used for database command generation with helper functions.
    /// </summary>
    public abstract class QueryGeneratorBase
    {
        commandStringProcessor stringProcessor = null;
        public commandStringProcessor StringProcessor { get { return stringProcessor; }}

        ParameterMode parameterProcessingMode = ParameterMode.Local;
        public ParameterMode ParameterProcessingMode
        {
            get
            {
                return parameterProcessingMode;
            }

            set
            {
                parameterProcessingMode = value;
            }
        }

        /// <summary>
        /// Query generator will use this string as table name while generating update and insert statements.
        /// </summary>
        public string TableName { get; set; }
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

        public QueryGeneratorBase(char parameterCharacter)
        {
            stringProcessor = new commandStringProcessor(parameterCharacter);
        }

        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        abstract public void AddFilterParameter(string parameterName, object value);
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        abstract public void AddFilterParameter(string parameterName, object value, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        abstract public void AddFilterParameter(string parameterName, object dbType, object value, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        abstract public void AddFilterParameter(string parameterName, object value, int size, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        abstract public void AddFilterParameter(string parameterName, object value, int size, byte scale, byte precision, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        abstract public void AddFilterParameter(string parameterName, object dbType, object value, int size, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        abstract public void AddFilterParameter(string parameterName, object dbType, object value, int size, byte scale, byte precision, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        abstract public void AddFilterParameter(string parameterName, DbType dbType, object value, int size, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with FilterText property.
        /// </summary>
        abstract public void AddFilterParameter(string parameterName, DbType dbType, object value, int size, byte scale, byte precision, ParameterDirection direction);

        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        abstract public void AddDataParameter(string parameterName, object value);
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        abstract public void AddDataParameter(string parameterName, object value, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        abstract public void AddDataParameter(string parameterName, object dbType, object value, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        abstract public void AddDataParameter(string parameterName, object value, int size, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        abstract public void AddDataParameter(string parameterName, object value, int size, byte scale, byte precision, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        abstract public void AddDataParameter(string parameterName, object dbType, object value, int size, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        abstract public void AddDataParameter(string parameterName, object dbType, object value, int size, byte scale, byte precision, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        abstract public void AddDataParameter(string parameterName, DbType dbType, object value, int size, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        abstract public void AddDataParameter(string parameterName, DbType dbType, object value, int size, byte scale, byte precision, ParameterDirection direction);

        /// <summary>
        /// Returns a database returned parameter.
        /// </summary>
        abstract public object GetParameterValue(string parameterName);
        /// <summary>
        /// Returns generated insert command.
        /// </summary>
        abstract public IDbCommand GetInsertcommand();
        /// <summary>
        /// Returns generated update command.
        /// </summary>
        abstract public IDbCommand GetUpdatecommand();
        /// <summary>
        /// Returns generated general command.
        /// </summary>
        abstract public IDbCommand GetSelectcommandBasic();
        /// <summary>
        /// Returns generated procedure command.
        /// </summary>
        abstract public IDbCommand GetProcedure();
        /// <summary>
        /// Clears all query generator instance.
        /// </summary>
        abstract public void Clear();

        public string GetPreparedcommandString(string commandString, commandStringType csType)
        {
            switch (ParameterProcessingMode)
            {
                case ParameterMode.Local:

                    if (csType == commandStringType.Filter)
                        return StringProcessor.GetPreparedLocalcommandString(commandString);
                    else
                        return commandString;

                case ParameterMode.Global:
                    return StringProcessor.GetPreparedGlobalcommandString(commandString);
                default:
                    return null;
            }
        }
    }
}
