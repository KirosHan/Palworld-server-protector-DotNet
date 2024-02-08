using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palworld_server_protector_DotNet
{
    public class IniSettingsReader
    {
        public static Dictionary<string, string> LoadIniFile(string iniFile)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string[] lines = File.ReadAllLines(iniFile);
            foreach (string line in lines)
            {
                string[] parts = line.Split('=');
                if (parts.Length < 2) continue;

                string key = parts[0];
                string value = string.Join("=", parts.Skip(1));
                keyValues.Add(key, value);
            }

            return keyValues;
        }
    }
}
