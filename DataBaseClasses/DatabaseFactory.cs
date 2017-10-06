using ProjectBase.AppContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//vyigity
namespace ProjectBase.DataBaseClasses
{
    public enum DbSettings { AutoConnectionManagement = 0, TransactionMode = 1, ManuelConnectionManagement = 2 }

    public static class DatabaseFactory
    {
        public static IKbDatabase2 GetDbObject()
        {
            ConnectionStringSettings conStr = KbAppContext.CONNECTION_STRINGS[KbAppContext.DEFAULT_DB];

            if (conStr.ProviderName == "Oracle.DataAccess.Client")
            {
                return new KbOracleDatabase2();
            }
            else if (conStr.ProviderName == "System.Data.SqlClient")
            {
                return new KbSqlDatabase2();
            }
            else if (conStr.ProviderName == "System.Data.OleDb")
                return new KbOleDbDatabase2();
            else
                throw new Exception("Provider ilişkilendirilemedi.");
        }

        public static IKbDatabase2 GetDbObject(DbSettings setting)
        {
            ConnectionStringSettings conStr = KbAppContext.CONNECTION_STRINGS["context"];

            if (conStr.ProviderName == "Oracle.DataAccess.Client")
            {
                return new KbOracleDatabase2(setting);
            }
            else if (conStr.ProviderName == "System.Data.SqlClient")
            {
                return new KbSqlDatabase2(setting);
            }
            else if (conStr.ProviderName == "System.Data.OleDb")
                return new KbOleDbDatabase2(setting);
            else
                throw new Exception("Provider ilişkilendirilemedi.");
        }

        public static IKbDatabase2 GetDbObject(DbSettings setting, IsolationLevel isolation)
        {
            ConnectionStringSettings conStr = KbAppContext.CONNECTION_STRINGS["context"];

            if (conStr.ProviderName == "Oracle.DataAccess.Client")
            {
                return new KbOracleDatabase2(setting, isolation);
            }
            else if (conStr.ProviderName == "System.Data.SqlClient")
            {
                return new KbSqlDatabase2(setting, isolation);
            }
            else if (conStr.ProviderName == "System.Data.OleDb")
                return new KbOleDbDatabase2(setting, isolation);
            else
                throw new Exception("Provider ilişkilendirilemedi.");
        }
    }
}
