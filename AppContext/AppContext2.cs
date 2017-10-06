using ProjectBase.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.AppContext
{
    public static class AppContext2
    {
        public static string APPLICATION_NAME
        {
            get
            {
                if (ConfigurationManager.AppSettings["APPLICATION_NAME"] != null)
                {
                    return ConfigurationManager.AppSettings["APPLICATION_NAME"];
                }
                else
                    return null;
            }
        }

        public static bool IN_TEST
        {
            get
            {
                if (ConfigurationManager.AppSettings["IN_TEST"] != null)
                {
                    return Util.GetProperty<bool>(ConfigurationManager.AppSettings["IN_TEST"], false);
                }
                else
                    return false;
            }
        }

        public static string DEFAULT_DB
        {
            get
            {
                if (ConfigurationManager.AppSettings["DEFAULT_DB"] != null)
                {
                    return ConfigurationManager.AppSettings["DEFAULT_DB"];
                }
                else
                    return "Context";
            }
        }

        public static bool? Log_Object
        {
            get
            {
                if (ConfigurationManager.AppSettings["LOG_OBJECT"] != null)
                {
                    return Util.GetProperty<bool>(ConfigurationManager.AppSettings["LOG_OBJECT"]);
                }
                else
                    return null;
            }
        }

        public static string MailUserName
        {
            get
            {
                if (ConfigurationManager.AppSettings["MAIL_USER_NAME"] != null)
                {
                    return ConfigurationManager.AppSettings["MAIL_USER_NAME"];
                }
                else
                    return null;
            }
        }
        public static string MailPassword
        {
            get
            {
                if (ConfigurationManager.AppSettings["MAIL_PASSWORD"] != null)
                {
                    return ConfigurationManager.AppSettings["MAIL_PASSWORD"];
                }
                else
                    return null;
            }
        }
        public static string MailAddress
        {
            get
            {
                if (ConfigurationManager.AppSettings["MAIL_ADRESS"] != null)
                {
                    return ConfigurationManager.AppSettings["MAIL_ADRESS"];
                }
                else
                    return null;
            }
        }
        public static string MailHost
        {
            get
            {
                if (ConfigurationManager.AppSettings["MAIL_HOST"] != null)
                {
                    return ConfigurationManager.AppSettings["MAIL_HOST"];
                }
                else
                    return null;
            }
        }
        public static int? MailPort
        {
            get
            {
                if (ConfigurationManager.AppSettings["MAIL_PORT"] != null)
                {
                    return Util.GetProperty<int>(ConfigurationManager.AppSettings["MAIL_PORT"]);
                }
                else
                    return null;
            }
        }

        public static string AppCode
        {
            get
            {
                if (ConfigurationManager.AppSettings["APP_CODE"] != null)
                {
                    return ConfigurationManager.AppSettings["APP_CODE"];
                }
                else
                    return null;
            }
        }
        public static string RootDomain
        {
            get
            {
                if (ConfigurationManager.AppSettings["ROOT_DOMAIN"] != null)
                {
                    return ConfigurationManager.AppSettings["ROOT_DOMAIN"];
                }
                else
                    return null;
            }
        }


        public static ConnectionStringSettingsCollection CONNECTION_STRINGS
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings != null)
                {
                    return ConfigurationManager.ConnectionStrings;
                }
                else
                    return null;
            }
        }

        public static string GetParameterValue(string ParameterName)
        {
            if (ConfigurationManager.AppSettings[ParameterName] != null)
            {
                return ConfigurationManager.AppSettings[ParameterName];
            }
            else
                return null;
        }
    }
}
