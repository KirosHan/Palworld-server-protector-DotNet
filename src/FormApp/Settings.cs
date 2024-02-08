using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Palworld_server_protector_DotNet
{
    public class Settings
    {
        public string CmdPath { get; set; } = "";
        public string Parameters { get; set; } = "";
        public string BackupPath { get; set; } = "";
        public string GameDataPath { get; set; } = "";
        public int MemTarget { get; set; } = 90;
        public string RconHost { get; set; } = "127.0.0.1";
        public int RconPort { get; set; } = 25575;
        public string RconPassword { get; set; } = "admin";
        public int RebootSeconds { get; set; } = 10;
        public int CheckSeconds { get; set; } = 20;
        public int BackupSeconds { get; set; } = 1800;
        public string WebhookUrl { get; set; } = "";


        public bool IsReboot { get; set; } = false;
        public bool IsStartProcess { get; set; } = false;
        public bool IsParameters { get; set; } = false;
        public bool IsNoti { get; set; } = false;
        public bool IsSave { get; set; } = false;
        public bool IsGetPlayers { get; set; } = false;
        public bool IsWebhook { get; set; } = false;
        public bool IsWebGetPlayers { get; set; } = false;
        public bool IsWebReboot { get; set; } = false;
        public bool IsWebSave { get; set; } = false;
        public bool IsWebStartProcess { get; set; } = false;
        public bool IsWebPlayerStatus { get; set; } = false;
        public bool IsOutputMemInfo {  get; set; } = false;

        public Settings()
        {

        }

        // 从文件加载设置
        public static Settings LoadFromConfigFile(string configFilePath)
        {
            Settings? settings = new Settings();

            if (!File.Exists(configFilePath))
            {
                return settings;
            }

            try
            {
                string json = File.ReadAllText(configFilePath);
                settings = JsonConvert.DeserializeObject<Settings>(json);
                if (settings == null)
                {
                    throw new Exception("配置文件加载失败: 配置文件反序列化后为空");
                }
                return settings;
            }
            catch (Exception ex)
            {
                throw new Exception("配置文件加载失败: " + ex.ToString());
            }

        }

        // 将设置保存到文件
        public void SaveToConfigFile(string configFilePath)
        {
            // 序列化 Settings 对象到 JSON 字符串
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            // 将 JSON 字符串写入文件
            File.WriteAllText(configFilePath, json);
        }

        // 兼容原本ini配置
        public static Settings LoadSettingsFromIniFile(string iniFile)
        {
            Settings settings = new Settings();
            // 读取ini文件到字典
            var iniData = IniSettingsReader.LoadIniFile(iniFile);
            Dictionary<string, string> iniDataUpperCaseKey = new Dictionary<string, string>();

            foreach (var kv in iniData)
            {
                iniDataUpperCaseKey.Add(kv.Key.ToUpper(), kv.Value);
            }

            // 获取Settings类的所有属性
            PropertyInfo[] properties = typeof(Settings).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                // 获取属性名称
                string key = property.Name;

                // 如果ini文件中有这个键
                if (iniDataUpperCaseKey.ContainsKey(key.ToUpper()))
                {
                    string value = iniDataUpperCaseKey[key.ToUpper()];
                    // 尝试将值转换为属性的类型，并设置属性值
                    try
                    {
                        // 如果属性是布尔类型，需要特别处理字符串转换为布尔值
                        if (property.PropertyType == typeof(bool))
                        {
                            property.SetValue(settings, value.Equals("True", StringComparison.OrdinalIgnoreCase));
                        }
                        else
                        {
                            // 使用TypeConverter来转换类型
                            TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
                            property.SetValue(settings, converter.ConvertFromString(value));
                        }
                    }
                    catch (Exception ex)
                    {
                        // 处理转换错误，可能是类型不匹配等问题
                        Console.WriteLine($"Error setting property {key} from ini file: {ex.Message}");
                    }
                }
            }

            return settings;
        }
    }

}
