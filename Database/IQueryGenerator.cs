using System.Data;

namespace ProjectBase.Database
{
    /// <summary>
    /// Can be used for database command generation with helper functions.
    /// </summary>
    public interface IQueryGenerator
    {
        /// <summary>
        /// Query generator will concate this string to end of query. String must include sql key word like WHERE. For parameter usage in query, symbols of : or @ can be used. For UPDATE generation, this must be used for specify filter text.
        /// </summary>
        string FilterText { get; set; }
        /// <summary>
        /// Query generator will use this string as procedure name. It can be a database function or procedure.
        /// </summary>
        string ProcedureName { get; set; }
        /// <summary>
        /// Query generator will concate this string to end of query. It can be used for group by expressions. For parameter usage in query, symbols of : or @ can be used.
        /// </summary>
        string SelectTail { get; set; }
        /// <summary>
        /// Query generator will use this string as main sql query text. It can be used for any kind of command like DML and DDL. It can be used mainly for a select query. For parameter usage in query, symbols of : or @ can be used.
        /// </summary>
        string SelectText { get; set; }
        /// <summary>
        /// Query generator will use this string as table name while generating update and insert statements.
        /// </summary>
        string TableName { get; set; }
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        void AddDataParameter(string parameterName, object value);
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        void AddDataParameter(string parameterName, object value, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        void AddDataParameter(string parameterName, object dbType, object value, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        void AddDataParameter(string parameterName, object value, int size, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        void AddDataParameter(string parameterName, object dbType, object value, int size, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter while generating update and insert statements or procedure calls. For statement generation, parameter name must be same with column name in database table.
        /// </summary>
        void AddDataParameter(string parameterName, DbType dbType, object value, int size, ParameterDirection direction);
        /// <summary>
        /// Query generator will use this parameter for non-generated sql statement that is given with SelectText property.
        /// </summary>
        void AddFilterParameter(string parameterName, object value);
        /// <summary>
        /// Clears all query generater instance.
        /// </summary>
        void Clear();
        /// <summary>
        /// Returns generated insert command.
        /// </summary>
        IDbCommand GetInsertCommand();
        /// <summary>
        /// Returns a database returned parameter.
        /// </summary>
        object GetParameterValue(string parameterName);
        /// <summary>
        /// Returns generated procedure command.
        /// </summary>
        IDbCommand GetProcedure();
        /// <summary>
        /// Returns generated general command.
        /// </summary>
        IDbCommand GetSelectCommandBasic();
        /// <summary>
        /// Returns generated update command.
        /// </summary>
        IDbCommand GetUpdateCommand();
    }
}