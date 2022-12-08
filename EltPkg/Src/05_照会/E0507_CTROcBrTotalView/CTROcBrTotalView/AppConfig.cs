using System.Configuration;
using CommonTable.DB;

namespace CTROcBrTotalView
{
    public class AppConfig
    {
        private AppConfig() { }

        /// <summary>一覧表示上限</summary>
        public static int ListDispLimit { get { return DBConvert.ToIntNull(ConfigurationManager.AppSettings["ListDispLimit"]); } }

        /// <summary>合計票イメージ縮小率（一覧画面）</summary>
        public static int BatImageReduceRate1 { get { return DBConvert.ToIntNull(ConfigurationManager.AppSettings["BatImageReduceRate1"]); } }

        /// <summary>合計票イメージ縮小率（詳細画面）</summary>
        public static int BatImageReduceRate2 { get { return DBConvert.ToIntNull(ConfigurationManager.AppSettings["BatImageReduceRate2"]); } }
    }
}
