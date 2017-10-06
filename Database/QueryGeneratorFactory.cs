using ProjectBase.AppContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Database
{
    public static class QueryGeneratorFactory
    {
        public static IQueryGenerator GetDbObject()
        {
            ConnectionStringSettings conStr = AppContext2.CONNECTION_STRINGS["context"];

            if (conStr.ProviderName == "Oracle.DataAccess.Client")
            {
                return new OracleQueryGenerator();
            }
            else if (conStr.ProviderName == "System.Data.SqlClient")
            {
                return new SqlQueryGenerator();
            }
            else
                throw new Exception("Provider ilişkilendirilemedi.");
        }
    }
}
