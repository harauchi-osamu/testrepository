using System.Configuration;
using CommonTable.DB;

namespace HulftIO
{
	public class AppConfig
	{
        /// <summary>共通コンフィグ</summary>
        private static Configuration _config = null;

        private AppConfig() { }

        static AppConfig()
        {
            AppConfig ai = new AppConfig();
            string configPath = ai.GetType().Assembly.Location;
            _config = ConfigurationManager.OpenExeConfiguration(configPath);
        }

        /// <summary>HULFT送信可否</summary>
        public static bool HulftSendExec { get { return DBConvert.ToBoolNull(_config.AppSettings.Settings["HulftSendExec"].Value); } }

        /// <summary>HULFT集信可否</summary>
        public static bool HulftRecvExec { get { return DBConvert.ToBoolNull(_config.AppSettings.Settings["HulftRecvExec"].Value); } }
    }
}
