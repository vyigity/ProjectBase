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
        /// <summary>
        /// Instantiates a new encapsulated QueryGenerator object.
        /// </summary>
        public static IQueryGenerator GetDbObject()
        {
            ConnectionStringSettings conStr = AppContext2.CONNECTION_STRINGS[AppContext2.DEFAULT_DB];

            if (conStr.ProviderName == "Oracle.DataAccess.Client" || conStr.ProviderName == "Oracle.ManagedDataAccess.Client")
            {
                return new OracleManagedQueryGenerator();
            }
            else if (conStr.ProviderName == "System.Data.SqlClient")
            {
                return new SqlQueryGenerator();
            }
            else if (conStr.ProviderName == "MySql.Data.MySqlClient")
            {
                return new MySqlQueryGenerator();
            }
            else
                throw new Exception("Provider is not recognized.");
        }
    }
}
