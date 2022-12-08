using System.Configuration;
using CommonTable.DB;

namespace CTRIcRequestMk
{
	public class AppConfig
	{
		private AppConfig() { }

        /// <summary>処理可能交換希望月数[Nヵ月]</summary>
        public static int ClearingMonthMax { get { return DBConvert.ToIntNull(ConfigurationManager.AppSettings["ClearingMonthMax"]); } }
        /// <summary>指定交換での交換日To初期値営業日差日数</summary>
        public static int ClearingDefDateDiff { get { return DBConvert.ToIntNull(ConfigurationManager.AppSettings["ClearingDefDateDiff"]); } }
    }
}
