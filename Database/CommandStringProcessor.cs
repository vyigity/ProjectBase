using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectBase.Database
{
    public class commandStringProcessor
    {
        /// <summary>
        /// Parameter character that is used by specific database.
        /// </summary>
        public char DbBasedParameterCharacter { get; set; }

        /// <summary>
        /// List of parameter characters that are used by all databases.
        /// </summary>
        public List<char> ParameterCharacters { get; set; }

        /// <summary>
        /// Regular expression text to find global parameters.
        /// </summary>
        public string GlobalParameterRegExp { get; set; }

        public commandStringProcessor()
        {
            ParameterCharacters = new List<char>() { ':', '@', '$' };
            GlobalParameterRegExp = "\\.p\\.\\w";
        }

        public commandStringProcessor(char dbBasedParameterCharacter)
        {
            ParameterCharacters = new List<char>() { ':', '@', '$' };
            GlobalParameterRegExp = "\\.p\\.\\w";
            DbBasedParameterCharacter = dbBasedParameterCharacter;
        }

        /// <summary>
        /// Produces processable command text from raw command text that includes global parameter definitions.
        /// </summary>
        public string GetPreparedGlobalcommandString(string commandString)
        {
            StringBuilder sbuilder = new StringBuilder(commandString);

            Regex rex = new Regex(GlobalParameterRegExp);
            MatchCollection mcol = rex.Matches(commandString);

            foreach (Match item in mcol)
            {
                sbuilder[item.Index] = ' ';
                sbuilder[item.Index + 1] = ' ';
                sbuilder[item.Index + 2] = DbBasedParameterCharacter;
            }

            return sbuilder.ToString();
        }

        /// <summary>
        /// Produces database specific processable command text from raw command text that includes database based parameter definitions.
        /// </summary>
        public string GetPreparedLocalcommandString(string commandString)
        {
            StringBuilder result = new StringBuilder(commandString);

            for (int i = 0; i < result.Length; i++)
            {
                foreach (char c in ParameterCharacters)
                {
                    if(result[i] == c)
                    {
                        result[i] = DbBasedParameterCharacter;
                    }
                }
            }
            
            return result.ToString();
        }
    }
}
