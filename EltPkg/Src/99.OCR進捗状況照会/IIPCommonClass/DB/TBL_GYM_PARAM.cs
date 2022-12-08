using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// 業務パラメーター
	/// </summary>
	public class TBL_GYM_PARAM
	{
        public TBL_GYM_PARAM()
        {
        }

        public TBL_GYM_PARAM(int gym_id)
        {
            m_GYM_ID = gym_id;
        }

		public TBL_GYM_PARAM(DataRow dr)
		{
			initializeByDataRow(dr);
		}

        #region テーブル定義
        public const string TABLE_NAME = TABLE_SCHEMA + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_SCHEMA = "hen";
        public const string TABLE_PHYSICAL_NAME = "GYM_PARAM";
		//カラム
		public const string GYM_ID = "GYM_ID";
		public const string GYM_KANA = "GYM_KANA";
		public const string GYM_KANJI = "GYM_KANJI";
		public const string GROUP_NO = "GROUP_NO";
		public const string OPERATIONDATE_DIFFDAYS = "OPERATIONDATE_DIFFDAYS";
		public const string BUSINESSDATE_DIFFDAYS = "BUSINESSDATE_DIFFDAYS";
		public const string IMAGE_FLG = "IMAGE_FLG";
		public const string METHOD = "METHOD";
		public const string PAPER_TYPE = "PAPER_TYPE";
		public const string ENTRY_MODE = "ENTRY_MODE";
		public const string FST_DSP_ID = "FST_DSP_ID";
		public const string HST_FLG = "HST_FLG";
		public const string FILING_FLG = "FILING_FLG";
		public const string WEB_FLG = "WEB_FLG";
		public const string SAVE_DAYS = "SAVE_DAYS";
		public const string WEB_SAVE_MONTH = "WEB_SAVE_MONTH";
		public const string OUT_FLG = "OUT_FLG";
		public const string OUT_GYM_ID = "OUT_GYM_ID";
		public const string OUT_GYM_KANA = "OUT_GYM_KANA";
		public const string OUT_GYM_KANJI = "OUT_GYM_KANJI";
		public const string OUT_LEN1 = "OUT_LEN1";
		public const string OUT_FILE = "OUT_FILE";
		public const string OUT_SUBRTN = "OUT_SUBRTN";
		public const string BACK_COLOR = "BACK_COLOR";
		public const string BAT_AUTO_SET = "BAT_AUTO_SET";
		public const string DONE_FLG = "DONE_FLG";
		public const string CREATE_USER = "CREATE_USER";
		public const string CREATE_TIME = "CREATE_TIME";
		public const string UPDATE_USER = "UPDATE_USER";
		public const string UPDATE_TIME = "UPDATE_TIME";

		#endregion

		#region メンバ

		public int m_GYM_ID = 0;
		public string m_GYM_KANA = "";
		public string m_GYM_KANJI = "";
		public int m_GROUP_NO = 0;
		public int m_OPERATIONDATE_DIFFDAYS = 0;
		public int m_BUSINESSDATE_DIFFDAYS = 0;
		public int m_IMAGE_FLG = 0;
		public int m_METHOD = 0;
		public int m_PAPER_TYPE = 0;
		public int m_ENTRY_MODE = 0;
		public int m_FST_DSP_ID = 0;
		public int m_HST_FLG = 0;
		public int m_FILING_FLG = 0;
		public int m_WEB_FLG = 0;
		public int m_SAVE_DAYS = 0;
		public int m_WEB_SAVE_MONTH = 0;
		public string m_OUT_FLG = "";
		public int m_OUT_GYM_ID = 0;
		public string m_OUT_GYM_KANA = "";
		public string m_OUT_GYM_KANJI = "";
		public int m_OUT_LEN1 = 0;
		public string m_OUT_FILE = "";
		public string m_OUT_SUBRTN = "";
		public string m_BACK_COLOR = "";
		public string m_BAT_AUTO_SET = "";
		public string m_DONE_FLG = "";
		public string m_CREATE_USER = "";
		public string m_CREATE_TIME = "";
		public string m_UPDATE_USER = "";
		public string m_UPDATE_TIME = "";

		#endregion

		#region 初期化

		/// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
			m_GYM_ID = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.GYM_ID]);
			m_GYM_KANA = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.GYM_KANA]);
			m_GYM_KANJI = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.GYM_KANJI]);
			m_GROUP_NO = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.GROUP_NO]);
			m_OPERATIONDATE_DIFFDAYS = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.OPERATIONDATE_DIFFDAYS]);
			m_BUSINESSDATE_DIFFDAYS = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.BUSINESSDATE_DIFFDAYS]);
			m_IMAGE_FLG = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.IMAGE_FLG]);
			m_METHOD = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.METHOD]);
			m_PAPER_TYPE = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.PAPER_TYPE]);
			m_ENTRY_MODE = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.ENTRY_MODE]);
			m_FST_DSP_ID = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.FST_DSP_ID]);
			m_HST_FLG = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.HST_FLG]);
			m_FILING_FLG = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.FILING_FLG]);
			m_WEB_FLG = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.WEB_FLG]);
			m_SAVE_DAYS = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.SAVE_DAYS]);
			m_WEB_SAVE_MONTH = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.WEB_SAVE_MONTH]);
			m_OUT_FLG = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.OUT_FLG]);
			m_OUT_GYM_ID = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.OUT_GYM_ID]);
			m_OUT_GYM_KANA = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.OUT_GYM_KANA]);
			m_OUT_GYM_KANJI = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.OUT_GYM_KANJI]);
			m_OUT_LEN1 = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.OUT_LEN1]);
			m_OUT_FILE = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.OUT_FILE]);
			m_OUT_SUBRTN = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.OUT_SUBRTN]);
			m_BACK_COLOR = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.BACK_COLOR]);
			m_BAT_AUTO_SET = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.BAT_AUTO_SET]);
			m_DONE_FLG = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.DONE_FLG]);
			m_CREATE_USER = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.CREATE_USER]);
			m_CREATE_TIME = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.CREATE_TIME]);
			m_UPDATE_USER = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.UPDATE_USER]);
			m_UPDATE_TIME = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.UPDATE_TIME]);
		}

		#endregion

		#region クエリ取得

		/// <summary>
		/// 業務番号を条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gymid">業務番号</param>
		/// <returns></returns>
		public string GetSelectQuery(int gymid)
        {
            return GetSelectQuery(gymid, true);
        }

        /// <summary>
        /// 業務番号を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <param name="locked">編集ロックされているもの(DONE_FLG=0)は含めるかどうか</param>
        /// <returns></returns>
        public string GetSelectQuery(int gymid, bool locked)
        {
            string strSql = "SELECT * FROM " + TBL_GYM_PARAM.TABLE_NAME +
                " WHERE " + TBL_GYM_PARAM.GYM_ID + "=" + gymid;
            if (!locked)
            {
                strSql += " AND " + TBL_GYM_PARAM.DONE_FLG + "='1'";
            }
            return strSql;
        }

		/// <summary>
		/// insert文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
			string strSql = "INSERT INTO " + TBL_GYM_PARAM.TABLE_NAME + " (" +
				TBL_GYM_PARAM.GYM_ID + "," +
				TBL_GYM_PARAM.GYM_KANA + "," +
				TBL_GYM_PARAM.GYM_KANJI + "," +
				TBL_GYM_PARAM.GROUP_NO + "," +
				TBL_GYM_PARAM.OPERATIONDATE_DIFFDAYS + "," +
				TBL_GYM_PARAM.BUSINESSDATE_DIFFDAYS + "," +
				TBL_GYM_PARAM.IMAGE_FLG + "," +
				TBL_GYM_PARAM.METHOD + "," +
				TBL_GYM_PARAM.PAPER_TYPE + "," +
				TBL_GYM_PARAM.ENTRY_MODE + "," +
				TBL_GYM_PARAM.FST_DSP_ID + "," +
				TBL_GYM_PARAM.HST_FLG + "," +
				TBL_GYM_PARAM.FILING_FLG + "," +
				TBL_GYM_PARAM.WEB_FLG + "," +
				TBL_GYM_PARAM.SAVE_DAYS + "," +
				TBL_GYM_PARAM.WEB_SAVE_MONTH + "," +
				TBL_GYM_PARAM.OUT_FLG + "," +
				TBL_GYM_PARAM.OUT_GYM_ID + "," +
				TBL_GYM_PARAM.OUT_GYM_KANA + "," +
				TBL_GYM_PARAM.OUT_GYM_KANJI + "," +
				TBL_GYM_PARAM.OUT_LEN1 + "," +
				TBL_GYM_PARAM.OUT_FILE + "," +
				TBL_GYM_PARAM.OUT_SUBRTN + "," +
				TBL_GYM_PARAM.BACK_COLOR + "," +
				TBL_GYM_PARAM.BAT_AUTO_SET + "," +
				TBL_GYM_PARAM.DONE_FLG + "," +
				TBL_GYM_PARAM.CREATE_USER + "," +
				TBL_GYM_PARAM.CREATE_TIME + "," +
				TBL_GYM_PARAM.UPDATE_USER + "," +
				TBL_GYM_PARAM.UPDATE_TIME + ") VALUES (" +
				m_GYM_ID + "," +
				"'" + m_GYM_KANA + "'," +
				"'" + m_GYM_KANJI + "'," +
				m_GROUP_NO + "," +
				m_OPERATIONDATE_DIFFDAYS + "," +
				m_BUSINESSDATE_DIFFDAYS + "," +
				m_IMAGE_FLG + "," +
				m_METHOD + "," +
				m_PAPER_TYPE + "," +
				m_ENTRY_MODE + "," +
				m_FST_DSP_ID + "," +
				m_HST_FLG + "," +
				m_FILING_FLG + "," +
				m_WEB_FLG + "," +
				m_SAVE_DAYS + "," +
				m_WEB_SAVE_MONTH + "," +
				"'" + m_OUT_FLG + "'," +
				m_OUT_GYM_ID + "," +
				"'" + m_OUT_GYM_KANA + "'," +
				"'" + m_OUT_GYM_KANJI + "'," +
				m_OUT_LEN1 + "," +
				"'" + m_OUT_FILE + "'," +
				"'" + m_OUT_SUBRTN + "'," +
				"'" + m_BACK_COLOR + "'," +
				"'" + m_BAT_AUTO_SET + "'," +
				"'" + m_DONE_FLG + "'," +
				"'" + m_CREATE_USER + "'," +
				"'" + m_CREATE_TIME + "'," +
				"'" + m_UPDATE_USER + "'," +
				"'" + m_UPDATE_TIME + "')";
			return strSql;
		}

		/// <summary>
		/// update文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetUpdateQuery()
        {
			string strSql = "UPDATE " + TBL_GYM_PARAM.TABLE_NAME + " SET " +
				TBL_GYM_PARAM.GYM_KANA + "='" + m_GYM_KANA + "', " +
				TBL_GYM_PARAM.GYM_KANJI + "='" + m_GYM_KANJI + "', " +
				TBL_GYM_PARAM.GROUP_NO + "=" + m_GROUP_NO + ", " +
				TBL_GYM_PARAM.OPERATIONDATE_DIFFDAYS + "=" + m_OPERATIONDATE_DIFFDAYS + ", " +
				TBL_GYM_PARAM.BUSINESSDATE_DIFFDAYS + "=" + m_BUSINESSDATE_DIFFDAYS + ", " +
				TBL_GYM_PARAM.IMAGE_FLG + "=" + m_IMAGE_FLG + ", " +
				TBL_GYM_PARAM.METHOD + "=" + m_METHOD + ", " +
				TBL_GYM_PARAM.PAPER_TYPE + "=" + m_PAPER_TYPE + ", " +
				TBL_GYM_PARAM.ENTRY_MODE + "=" + m_ENTRY_MODE + ", " +
				TBL_GYM_PARAM.FST_DSP_ID + "=" + m_FST_DSP_ID + ", " +
				TBL_GYM_PARAM.HST_FLG + "=" + m_HST_FLG + ", " +
				TBL_GYM_PARAM.FILING_FLG + "=" + m_FILING_FLG + ", " +
				TBL_GYM_PARAM.WEB_FLG + "=" + m_WEB_FLG + ", " +
				TBL_GYM_PARAM.SAVE_DAYS + "=" + m_SAVE_DAYS + ", " +
				TBL_GYM_PARAM.WEB_SAVE_MONTH + "=" + m_WEB_SAVE_MONTH + ", " +
				TBL_GYM_PARAM.OUT_FLG + "='" + m_OUT_FLG + "', " +
				TBL_GYM_PARAM.OUT_GYM_ID + "=" + m_OUT_GYM_ID + ", " +
				TBL_GYM_PARAM.OUT_GYM_KANA + "='" + m_OUT_GYM_KANA + "', " +
				TBL_GYM_PARAM.OUT_GYM_KANJI + "='" + m_OUT_GYM_KANJI + "', " +
				TBL_GYM_PARAM.OUT_LEN1 + "=" + m_OUT_LEN1 + ", " +
				TBL_GYM_PARAM.OUT_FILE + "='" + m_OUT_FILE + "', " +
				TBL_GYM_PARAM.OUT_SUBRTN + "='" + m_OUT_SUBRTN + "', " +
				TBL_GYM_PARAM.BACK_COLOR + "='" + m_BACK_COLOR + "', " +
				TBL_GYM_PARAM.BAT_AUTO_SET + "='" + m_BAT_AUTO_SET + "', " +
				TBL_GYM_PARAM.DONE_FLG + "='" + m_DONE_FLG + "', " +
				TBL_GYM_PARAM.CREATE_USER + "='" + m_CREATE_USER + "', " +
				TBL_GYM_PARAM.CREATE_TIME + "='" + m_CREATE_TIME + "', " +
				TBL_GYM_PARAM.UPDATE_USER + "='" + m_UPDATE_USER + "', " +
				TBL_GYM_PARAM.UPDATE_TIME + "='" + m_UPDATE_TIME + "'" +
				"WHERE " +
				TBL_GYM_PARAM.GYM_ID + "=" + m_GYM_ID;
			return strSql;
		}

		#endregion

	}
}
