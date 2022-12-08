using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace IIPCommonClass
{
    public class ConfigAccess
    {
        private Configuration _config;

        private static ConfigAccess _singleton = new ConfigAccess();

        /// <summary>
        /// インスタンス取得
        /// </summary>
        /// <returns></returns>
        public static ConfigAccess Instance
        {
            get { return _singleton; }
        }

        private ConfigAccess()
        {
            this.GetConfig();
        }

        public string MsgINI
        {
            get { return GetValue("MsgINI"); }
        }

        public string LogFolderPath
        {
            get { return GetValue("LogFolderPath"); }
        }

        public string ErrLogFolderPath
        {
            get { return GetValue("ErrLogFolderPath"); }
        }

        public string OCR1
        {
            get { return GetValue("OCR1"); }
        }

        public string OCR2
        {
            get { return GetValue("OCR2"); }
        }

        public string ErrLogDays
        {
            get { return GetValue("ErrLogDays");  }
        }

        public string DISPSIZE
        {
            get { return GetValue("DISPSIZE"); }
        }

        public string FontName
        {
            get { return GetValue("FontName"); }
        }



        private string GetValue(string key)
        {
            if (this._config == null) return "";
            KeyValueConfigurationElement elem = this._config.AppSettings.Settings[key];
            if (elem == null) return "";
            return elem.Value;
        }

        private void GetConfig()
        {
            string dll_config_path = this.GetType().Assembly.Location + ".config";

            if (!File.Exists(dll_config_path))
            {
                return;
            }

            var file_map = new ExeConfigurationFileMap();
            file_map.ExeConfigFilename = dll_config_path;
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(file_map, ConfigurationUserLevel.None);

            this._config = config;
        }
    }
}
