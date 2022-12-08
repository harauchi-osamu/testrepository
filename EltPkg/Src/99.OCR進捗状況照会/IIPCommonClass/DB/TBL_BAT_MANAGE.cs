using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// バッチ
	/// </summary>
	public class TBL_BAT_MANAGE
	{
        public TBL_BAT_MANAGE()
        {
        }

        public TBL_BAT_MANAGE(int gym_id, int bat_id, string scanner_id, int operation_date)
        {
            m_GYM_ID = gym_id;
            m_BAT_ID = bat_id;
            m_SCANNER_ID = scanner_id;
            m_OPERATION_DATE = operation_date;
        }

		public TBL_BAT_MANAGE(DataRow dr)
		{
			initializeByDataRow(dr);
		}

		#region テーブル定義

  		public const string TABLE_NAME = "BAT_MANAGE";

		public const string GYM_ID = "GYM_ID";
		public const string OPERATION_DATE = "OPERATION_DATE";
		public const string SCANNER_ID = "SCANNER_ID";
		public const string BAT_ID = "BAT_ID";
		public const string MANAGE_STS = "MANAGE_STS";
		public const string MANAGE_KBN = "MANAGE_KBN";
		public const string MANAGE_TIME = "MANAGE_TIME";
		public const string MEMO1 = "MEMO1";
		public const string MEMO2 = "MEMO2";
		public const string MEMO3 = "MEMO3";
		public const string MEMO4 = "MEMO4";
		public const string MEMO5 = "MEMO5";
		public const string FIRST_DSP_ID = "FIRST_DSP_ID";
		public const string LAST_DSP_ID = "LAST_DSP_ID";
		public const string DSP_ID = "DSP_ID";
		public const string LAST_LINENO = "LAST_LINENO";
		public const string DETAILS_NO = "DETAILS_NO";
		public const string DETAILS_COUNT = "DETAILS_COUNT";
		public const string IMG_ADD_FLG = "IMG_ADD_FLG";
		public const string S_NUMERICING = "S_NUMERICING";
		public const string E_NUMERICING = "E_NUMERICING";
		public const string TARGET_DATE = "TARGET_DATE";
		public const string DELYMD = "DELYMD";
		public const string DELETE_FLG = "DELETE_FLG";
		public const string JOIN_FLG = "JOIN_FLG";
		public const string PRF_STS = "PRF_STS";
		public const string OLD_BAT_ID = "OLD_BAT_ID";
		public const string OLD_MANAGE_STS = "OLD_MANAGE_STS";
		public const string OLD_MANAGE_KBN = "OLD_MANAGE_KBN";
		public const string E_TMNO = "E_TMNO";
		public const string E_OPENO = "E_OPENO";
		public const string V_TMNO = "V_TMNO";
		public const string V_OPEN = "V_OPEN";
		public const string C_TMNO = "C_TMNO";
		public const string C_OPEN = "C_OPEN";
		public const string O_TMNO = "O_TMNO";
		public const string O_OPENO = "O_OPENO";
		public const string O_STIME = "O_STIME";
		public const string O_ETIME = "O_ETIME";
		public const string O_YMD = "O_YMD";
		public const string O_TIME = "O_TIME";
		public const string O_KENSU = "O_KENSU";
		public const string SEL_OPENO = "SEL_OPENO";
		public const string SEL_DATE = "SEL_DATE";
		public const string SEL_CNT = "SEL_CNT";
		public const string CREATE_USER = "CREATE_USER";
		public const string CREATE_TIME = "CREATE_TIME";
		public const string UPDATE_USER = "UPDATE_USER";
		public const string UPDATE_TIME = "UPDATE_TIME";
		public const string TODOKEDE_FLG = "TODOKEDE_FLG";
		public const string INNKAN_SHOGO_FLG = "INNKAN_SHOGO_FLG";
		public const string HOST_TOROKU_FLG = "HOST_TOROKU_FLG";

		#endregion

		#region 定数定義

		public enum KUBUN
        {
            /// <summary>
            /// エントリー(1)
            /// </summary>
            ENTRY_DO = 1,
            /// <summary>
            /// エントリー再開(2)
            /// </summary>
            ENTRY_REDO = 2,
            /// <summary>
            /// 強制修正(3)
            /// </summary>
            /// <summary>
            /// ベリファイ(4)
            /// </summary>
            VERIFY_DO = 4,
            /// <summary>
            /// ベリファイ再開(5)
            /// </summary>
            VERIFY_REDO = 5,
            /// <summary>
            /// 不能項目訂正(6)
            /// </summary>
            TEISEI_DO = 6,
            /// <summary>
            /// 不能項目訂正再開(7)
            /// </summary>
            TEISEI_REDO = 7,
            /// <summary>
            /// ダイレクトエントリー(8)
            /// </summary>
            DIRECT_DO = 8,
            /// <summary>
            /// ダイレクトエントリー再開(9)
            /// </summary>
            DIRECT_REDO = 9,
            HENSYU_DO = 99
        }

        /// <summary>
        /// MANAGE_STSの定数一覧
        /// </summary>
        public enum STATUS
        {
            /// <summary>
            /// OCR読込中(2) 
            /// </summary>
            OCR_DO = 2,
            /// <summary>
            /// OCR読込完了(3)
            /// </summary>
            OCR_COMPLETE = 3,
            /// <summary>
            /// 集計相違(4)
            /// </summary>
            OCR_ERR = 4,
            /// <summary>
            /// 小計票なし(5)
            /// </summary>
            SYOKEIHYO_NASHI = 5,
            /// <summary>
            /// エントリー中(10)
            /// </summary>
            ENTRY_DO = 10,
            /// <summary>
            /// エントリー保留(11)
            /// </summary>
            ENTRY_PENDING = 11,
            /// <summary>
            /// エントリー再開(12)
            /// </summary>
            ENTRY_REDO = 12,
            /// <summary>
            /// エントリー完了(13)
            /// </summary>
            ENTRY_COMPLETE = 13,
            /// <summary>
            /// ベリファイ中(20)
            /// </summary>
            VERIFY_DO = 20,
            /// <summary>
            /// ベリファイ保留(21)
            /// </summary>
            VERIFY_PENDING = 21,
            /// <summary>
            /// ベリファイ再開(22)
            /// </summary>
            VERIFY_REDO = 22,
            /// <summary>
            /// ベリファイ完了(23)
            /// </summary>
            VERIFY_COMPLETE = 23,
            /// <summary>
            /// 不能項目訂正中(30)
            /// </summary>
            TEISEI_DO = 30,
            /// <summary>
            /// 不能項目訂正保留(31)
            /// </summary>
            TEISEI_PENDING = 31,
            /// <summary>
            /// 不能項目訂正再開(32)
            /// </summary>
            TEISEI_REDO = 32,
            /// <summary>
            /// 不能項目訂正完了(33)
            /// </summary>
            TEISEI_COMPLETE = 33,
            /// <summary>
            /// 抽出待ち(41)
            /// </summary>
            TRANS_WAIT = 41,
            /// <summary>
            /// 抽出中(42)
            /// </summary>
            TRANS_DO = 42,
            /// <summary>
            /// 抽出完了(43)
            /// </summary>
            TRANS_COMPLETE = 43,
            /// <summary>
            /// ダイレクトエントリー中(50)
            /// </summary>
            DIRECT_DO = 50,
            /// <summary>
            /// ダイレクトエントリー保留(51)
            /// </summary>
            DIRECT_PENDING = 51,
            /// <summary>
            /// ダイレクトエントリー再開(52)
            /// </summary>
            DIRECT_REDO = 52,
            /// <summary>
            /// ダイレクトエントリー完了(53)
            /// </summary>
            DIRECT_COMPLETE = 53,
            HENSYU_DO = 99
        }
		#endregion

		#region メンバ

		private int m_GYM_ID = 0;
		private int m_OPERATION_DATE = 0;
		private string m_SCANNER_ID = "";
		private int m_BAT_ID = 0;
		public string m_MANAGE_STS = "";
		public string m_MANAGE_KBN = "";
		public int m_MANAGE_TIME = 0;
		public string m_MEMO1 = "";
		public string m_MEMO2 = "";
		public string m_MEMO3 = "";
		public string m_MEMO4 = "";
		public string m_MEMO5 = "";
		public int m_FIRST_DSP_ID = 0;
		public int m_LAST_DSP_ID = 0;
		public int m_DSP_ID = 0;
		public int m_LAST_LINENO = 0;
		public int m_DETAILS_NO = 0;
		public int m_DETAILS_COUNT = 0;
		public string m_IMG_ADD_FLG = "";
		public string m_S_NUMERICING = "";
		public string m_E_NUMERICING = "";
		public int m_TARGET_DATE = 0;
		public int m_DELYMD = 0;
		public string m_DELETE_FLG = "";
		public string m_JOIN_FLG = "";
		public int m_PRF_STS = 0;
		public int m_OLD_BAT_ID = 0;
		public string m_OLD_MANAGE_STS = "";
		public string m_OLD_MANAGE_KBN = "";
		public string m_E_TMNO = "";
		public string m_E_OPENO = "";
		public string m_V_TMNO = "";
		public string m_V_OPEN = "";
		public string m_C_TMNO = "";
		public string m_C_OPEN = "";
		public string m_O_TMNO = "";
		public string m_O_OPENO = "";
		public int m_O_STIME = 0;
		public int m_O_ETIME = 0;
		public int m_O_YMD = 0;
		public int m_O_TIME = 0;
		public int m_O_KENSU = 0;
		public string m_SEL_OPENO = "";
		public int m_SEL_DATE = 0;
		public int m_SEL_CNT = 0;
		public string m_CREATE_USER = "";
		public string m_CREATE_TIME = "";
		public string m_UPDATE_USER = "";
		public string m_UPDATE_TIME = "";
		public string m_TODOKEDE_FLG = "";
		public string m_INNKAN_SHOGO_FLG = "";
		public string m_HOST_TOROKU_FLG = "";

		#endregion

		#region プロパティ

		public int _GYM_ID
		{
			get { return m_GYM_ID; }
		}

		public int _OPERATION_DATE
		{
			get { return m_OPERATION_DATE; }
		}

		public string _SCANNER_ID
		{
			get { return m_SCANNER_ID; }
		}

		public int _BAT_ID
		{
			get { return m_BAT_ID; }
		}

		#endregion

		#region 初期化

		/// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
			m_GYM_ID = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.GYM_ID]);
			m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.OPERATION_DATE]);
			m_SCANNER_ID = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.SCANNER_ID]);
			m_BAT_ID = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.BAT_ID]);
			m_MANAGE_STS = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.MANAGE_STS]);
			m_MANAGE_KBN = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.MANAGE_KBN]);
			m_MANAGE_TIME = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.MANAGE_TIME]);
			m_MEMO1 = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.MEMO1]);
			m_MEMO2 = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.MEMO2]);
			m_MEMO3 = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.MEMO3]);
			m_MEMO4 = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.MEMO4]);
			m_MEMO5 = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.MEMO5]);
			m_FIRST_DSP_ID = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.FIRST_DSP_ID]);
			m_LAST_DSP_ID = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.LAST_DSP_ID]);
			m_DSP_ID = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.DSP_ID]);
			m_LAST_LINENO = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.LAST_LINENO]);
			m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.DETAILS_NO]);
			m_DETAILS_COUNT = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.DETAILS_COUNT]);
			m_IMG_ADD_FLG = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.IMG_ADD_FLG]);
			m_S_NUMERICING = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.S_NUMERICING]);
			m_E_NUMERICING = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.E_NUMERICING]);
			m_TARGET_DATE = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.TARGET_DATE]);
			m_DELYMD = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.DELYMD]);
			m_DELETE_FLG = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.DELETE_FLG]);
			m_JOIN_FLG = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.JOIN_FLG]);
			m_PRF_STS = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.PRF_STS]);
			m_OLD_BAT_ID = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.OLD_BAT_ID]);
			m_OLD_MANAGE_STS = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.OLD_MANAGE_STS]);
			m_OLD_MANAGE_KBN = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.OLD_MANAGE_KBN]);
			m_E_TMNO = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.E_TMNO]);
			m_E_OPENO = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.E_OPENO]);
			m_V_TMNO = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.V_TMNO]);
			m_V_OPEN = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.V_OPEN]);
			m_C_TMNO = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.C_TMNO]);
			m_C_OPEN = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.C_OPEN]);
			m_O_TMNO = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.O_TMNO]);
			m_O_OPENO = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.O_OPENO]);
			m_O_STIME = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.O_STIME]);
			m_O_ETIME = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.O_ETIME]);
			m_O_YMD = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.O_YMD]);
			m_O_TIME = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.O_TIME]);
			m_O_KENSU = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.O_KENSU]);
			m_SEL_OPENO = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.SEL_OPENO]);
			m_SEL_DATE = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.SEL_DATE]);
			m_SEL_CNT = DBConvert.ToIntNull(dr[TBL_BAT_MANAGE.SEL_CNT]);
			m_CREATE_USER = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.CREATE_USER]);
			m_CREATE_TIME = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.CREATE_TIME]);
			m_UPDATE_USER = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.UPDATE_USER]);
			m_UPDATE_TIME = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.UPDATE_TIME]);
			m_TODOKEDE_FLG = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.TODOKEDE_FLG]);
			m_INNKAN_SHOGO_FLG = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.INNKAN_SHOGO_FLG]);
			m_HOST_TOROKU_FLG = DBConvert.ToStringNull(dr[TBL_BAT_MANAGE.HOST_TOROKU_FLG]);
		}

		#endregion

		#region クエリ取得

		/// <summary>
		/// キーを条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gym_id">業務番号</param>
		/// <param name="bat_id">バッチＩＤ</param>
		/// <param name="scanner_id">スキャナー号機</param>
		/// <param name="operation_date">処理日</param>
		/// <returns></returns>
		public static string GetSelectQuery(int gym_id, long bat_id, string scanner_id, int operation_date)
        {
            return GetSelectQuery(gym_id, bat_id, scanner_id, operation_date, false);
        }

		/// <summary>
		/// キーを条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gym_id">業務番号</param>
		/// <param name="bat_id">バッチＩＤ</param>
		/// <param name="scanner_id">スキャナー号機</param>
		/// <param name="operation_date">処理日</param>
		/// <param name="forupdate">FOR UPDATE文を付けるかどうか</param>
		/// <returns></returns>
		public static string GetSelectQuery(int gym_id, long bat_id, string scanner_id, int operation_date, bool forupdate)
		{
			string lockStr = forupdate ? DBConvert.QUERY_LOCK : DBConvert.QUERY_NOLOCK;

			string strSql = "SELECT * FROM " + TBL_BAT_MANAGE.TABLE_NAME + lockStr +
				  " WHERE " + TBL_BAT_MANAGE.GYM_ID + "=" + gym_id +
				  " AND " + TBL_BAT_MANAGE.BAT_ID + "=" + bat_id +
				  " AND " + TBL_BAT_MANAGE.SCANNER_ID + "='" + scanner_id + "'" +
				  " AND " + TBL_BAT_MANAGE.OPERATION_DATE + "=" + operation_date;
			return strSql;
		}

		#endregion
	}
}
