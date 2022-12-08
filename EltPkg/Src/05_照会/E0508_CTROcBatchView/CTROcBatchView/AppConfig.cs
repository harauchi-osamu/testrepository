using System.Configuration;
using CommonTable.DB;

namespace CTROcBatchView
{
	public class AppConfig
	{
		private AppConfig() { }

        /// <summary>一覧表示上限</summary>
        public static int ListDispLimit { get { return DBConvert.ToIntNull(ConfigurationManager.AppSettings["ListDispLimit"]); } }

        /// <summary>バッチイメージ縮小率</summary>
        public static int BatImageReduceRate { get { return DBConvert.ToIntNull(ConfigurationManager.AppSettings["BatImageReduceRate"]); } }
    }
}
