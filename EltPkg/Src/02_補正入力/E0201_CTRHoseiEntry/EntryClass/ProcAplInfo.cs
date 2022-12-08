using System;
using System.Collections.Generic;
using System.Configuration;
using CommonClass.DB;
using CommonTable.DB;

namespace EntryClass
{
	public class ProcAplInfo
	{
		private static List<int> _gokeihyoDspId = null;
		private static List<string> _gokeihyoOcrId = null;
		private static List<string> _gokei_w = null;
		public static TBL_GYM_SETTING GYM_SETTING { get; private set; } = null;
		public static V_GYM_SETTING V_GYM_SETTING { get; private set; } = null;

		static ProcAplInfo()
		{
			_gokeihyoDspId = new List<int>();
			_gokeihyoOcrId = new List<string>();
			_gokei_w = new List<string>();
			Utiwake_Word = new List<string>();
		}

		/// <summary>
		/// 合計票DspIDの文字列
		/// </summary>
		public static List<int> GokeihyoDspIDList
		{
			get
			{
				if (_gokeihyoDspId == null) { return new List<int>(); }
				return _gokeihyoDspId;
			}
			set { _gokeihyoDspId = value; }
		}

		/// <summary>
		/// 合計票OCRIDの文字列
		/// </summary>
		public static List<string> GokeihyoOCRIDList
		{
			get { return _gokeihyoOcrId; }
			set { _gokeihyoOcrId = value; }
		}

		/// <summary>
		/// 金額合計を表す単語
		/// </summary>
		public static List<string> Gokei_Word
		{
			get { return _gokei_w; }
			set { _gokei_w = value; }
		}

		/// <summary>
		/// 金額内訳を表す単語
		/// </summary>
		public static List<string> Utiwake_Word { get; set; }

		public static string DefaultDspID
		{
			get { return string.Empty; }
			//get { return ConfigurationManager.AppSettings["Default_DspID"]; }
		}

		/// <summary>
		/// 共通画面の画面番号
		/// </summary>
		public static string GokeihyoOCRID
		{
			get { return string.Empty; }
			//get { return ConfigurationManager.AppSettings["Gokeihyo_OCRID"]; }
		}

		public static string InputShijiOCRID
		{
			get { return string.Empty; }
			//get { return ConfigurationManager.AppSettings["InputShiji_OCRID"]; }
		}

		public static string DefaultOCRID
		{
			get { return string.Empty; }
			//get { return ConfigurationManager.AppSettings["Default_OCRID"]; }
		}

		public static int DefaultRowCount
		{
			get { return 0; }
			//get { return DBManager.ToIntNull(ConfigurationManager.AppSettings["Default_RowCount"]); }
		}

		public static string DB_ID
		{
			//get { return ConfigurationManager.AppSettings["DB_ID"]; }
			get { return NCR.Terminal.DbName; }
		}

		/// <summary>
		/// 業務設定を取得
		/// </summary>
		/// <param name="gymno">業務番号</param>
		/// <returns></returns>
		public static bool GetGymSetting(int gymkbn)
		{
			GYM_SETTING = null;
			try
			{
				GYM_SETTING = EntryDBManager.GetGymSetMstData(gymkbn);
			}
			catch
			{
				return false;
			}
			return (GYM_SETTING != null);
		}

		/// <summary>
		/// 業務設定を取得
		/// </summary>
		/// <param name="gymid">業務番号</param>
		/// <returns></returns>
		public static bool GetGymSettingView(int gymid)
		{
			V_GYM_SETTING = null;
			try
			{
				V_GYM_SETTING = EntryDBManager.GetGymSetMstDataView(gymid);
			}
			catch
			{
				return false;
			}
			return (V_GYM_SETTING != null);
		}

		public static bool AutoDate
		{
			get { return ConfigurationManager.AppSettings["AUTODATE"].Equals("true"); }
		}

		/// <summary>
		/// 交換日を返す
		/// AUTODATE=trueのときは業務設定から取得
		/// </summary>
		/// <returns></returns>
		public static int KoDate()
		{
			//try
			//{
			//    if (!AutoDate)
			//    {
			//        return DBManager.ToIntNull(ConfigurationManager.AppSettings["STODAYDATE"]);
			//    }
			//    return GYM_SETTING.KODATE;
			//}
			//catch
			//{
			//    return 0;
			//}
			return 0;
		}

	}
}
