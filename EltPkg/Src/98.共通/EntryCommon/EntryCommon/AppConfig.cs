using System.Configuration;
using CommonTable.DB;

namespace EntryCommon
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

        /// <summary>ＩＰアドレス必須要素</summary>
        public static string MustBeIncludedIp { get { return _config.AppSettings.Settings["MustBeIncludedIp"].Value; } }

    }
}
