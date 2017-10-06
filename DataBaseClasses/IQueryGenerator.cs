using System.Data;

namespace ProjectBase.DataBaseClasses
{
    public interface IQueryGenerator
    {
        string FilterText { get; set; }
        string ProcedureName { get; set; }
        string SelectTail { get; set; }
        string SelectText { get; set; }
        string TableName { get; set; }
        void AddDataParameter(string parameterName, object value);
        void AddDataParameter(string parameterName, object value, ParameterDirection direction);
        void AddDataParameter(string parameterName, object dbType, object value, ParameterDirection direction);
        void AddDataParameter(string parameterName, object value, int size, ParameterDirection direction);
        void AddDataParameter(string parameterName, object dbType, object value, int size, ParameterDirection direction);
        void AddDataParameter(string parameterName, DbType dbType, object value, int size, ParameterDirection direction);
        void AddFilterParameter(string parameterName, object value);
        void Clear();
        IDbCommand GetInsertCommand();
        object GetParameterValue(string parameterName);
        IDbCommand GetProcedure();
        IDbCommand GetSelectCommandBasic();
        IDbCommand GetUpdateCommand();
    }
}