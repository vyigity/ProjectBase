using ProjectBase.AppContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//vyigity
namespace ProjectBase.Database
{
    public enum DbSettings { AutoConnectionManagement = 0, TransactionMode = 1, ManuelConnectionManagement = 2 }

    public static class DatabaseFactory
    {
        public static IDatabase2 GetDbObject()
        {
            ConnectionStringSettings conStr = AppContext2.CONNECTION_STRINGS[AppContext2.DEFAULT_DB];

            if (conStr.ProviderName == "Oracle.DataAccess.Client")
            {
                return new OracleDatabase2();
            }
            else if (conStr.ProviderName == "Oracle.ManagedDataAccess.Client")
            {
                return new OracleManagedDatabase2();
            }
            else if (conStr.ProviderName == "System.Data.SqlClient")
            {
                return new SqlDatabase2();
            }
            else if (conStr.ProviderName == "System.Data.OleDb")
                return new OleDbDatabase2();
            else
                throw new Exception("Provider ilişkilendirilemedi.");
        }

        public static IDatabase2 GetDbObject(DbSettings setting)
        {
            ConnectionStringSettings conStr = AppContext2.CONNECTION_STRINGS["context"];

            if (conStr.ProviderName == "Oracle.DataAccess.Client")
            {
                return new OracleDatabase2(setting);
            }
            else if (conStr.ProviderName == "Oracle.ManagedDataAccess.Client")
            {
                return new OracleManagedDatabase2(setting;
            }
            else if (conStr.ProviderName == "System.Data.SqlClient")
            {
                return new SqlDatabase2(setting);
            }
            else if (conStr.ProviderName == "System.Data.OleDb")
                return new OleDbDatabase2(setting);
            else
                throw new Exception("Provider ilişkilendirilemedi.");
        }

        public static IDatabase2 GetDbObject(DbSettings setting, IsolationLevel isolation)
        {
            ConnectionStringSettings conStr = AppContext2.CONNECTION_STRINGS["context"];

            if (conStr.ProviderName == "Oracle.DataAccess.Client")
            {
                return new OracleDatabase2(setting, isolation);
            }
            else if (conStr.ProviderName == "Oracle.ManagedDataAccess.Client")
            {
                return new OracleManagedDatabase2(setting, isolation);
            }
            else if (conStr.ProviderName == "System.Data.SqlClient")
            {
                return new SqlDatabase2(setting, isolation);
            }
            else if (conStr.ProviderName == "System.Data.OleDb")
                return new OleDbDatabase2(setting, isolation);
            else
                throw new Exception("Provider ilişkilendirilemedi.");
        }
    }
}
