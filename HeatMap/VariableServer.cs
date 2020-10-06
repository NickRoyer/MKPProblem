using System;
using System.Collections.Generic;
using System.Text;

namespace HeatMap
{
    public static class VariableServer
    {
        private static Dictionary<string, string> Variables { get; set; } = new Dictionary<string, string>();

        public static string GetString(string key)
        {
            return Variables[key];
        }

        public static int GetInt(string key)
        {            
            return Convert.ToInt32(Variables[key]);
        }
    }
}
