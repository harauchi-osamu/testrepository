using System;
using System.Data;
using System.Data.Common;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// 不備トランザクション
	/// </summary>
	public class TBL_BAT_TRFUBI
	{
        public TBL_BAT_TRFUBI()
        {
        }

        public TBL_BAT_TRFUBI(int gymid, int opedate, string scanid, int batid, int imageno, int detailno)
        {
			m_GYM_ID = gymid;
			m_OPERATION_DATE = opedate;
			m_SCANNER_ID = scanid;
			m_BAT_ID = batid;
			m_IMAGE_NO = imageno;
			m_DETAILS_NO = detailno;
        }

		public TBL_BAT_TRFUBI(DataRow dr)
		{
			initializeByDataRow(dr);
		}

        #region テーブル定義

		public const string TABLE_NAME = "OPTBL";

		public const string GYM_ID = "GYM_ID";
		public const string OPERATION_DATE = "OPERATION_DATE";
		public const string SCANNER_ID = "SCANNER_ID";
		public const string BAT_ID = "BAT_ID";
		public const string IMAGE_NO = "IMAGE_NO";
		public const string DETAILS_NO = "DETAILS_NO";
		public const string FUBI_NO_01 = "FUBI_NO_01";
		public const string FUBI_NO_02 = "FUBI_NO_02";
		public const string FUBI_NO_03 = "FUBI_NO_03";
		public const string FUBI_NO_04 = "FUBI_NO_04";
		public const string FUBI_NO_05 = "FUBI_NO_05";
		public const string FUBI_NO_06 = "FUBI_NO_06";
		public const string FUBI_NO_07 = "FUBI_NO_07";
		public const string FUBI_NO_08 = "FUBI_NO_08";
		public const string FUBI_NO_09 = "FUBI_NO_09";
		public const string FUBI_NO_10 = "FUBI_NO_10";
		public const string FUBI_BIKOU = "FUBI_BIKOU";

		#endregion

		#region メンバ

		private int m_GYM_ID = 0;
		private int m_OPERATION_DATE = 0;
		private string m_SCANNER_ID = "";
		private int m_BAT_ID = 0;
		private int m_IMAGE_NO = 0;
		private int m_DETAILS_NO = 0;
		public int m_FUBI_NO_01 = 0;
		public int m_FUBI_NO_02 = 0;
		public int m_FUBI_NO_03 = 0;
		public int m_FUBI_NO_04 = 0;
		public int m_FUBI_NO_05 = 0;
		public int m_FUBI_NO_06 = 0;
		public int m_FUBI_NO_07 = 0;
		public int m_FUBI_NO_08 = 0;
		public int m_FUBI_NO_09 = 0;
		public int m_FUBI_NO_10 = 0;
		public string m_FUBI_BIKOU = "";

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

		public int _IMAGE_NO
		{
			get { return m_IMAGE_NO; }
		}

		public int _DETAILS_NO
		{
			get { return m_DETAILS_NO; }
		}

		#endregion

		#region 初期化

		/// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
			m_GYM_ID = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.GYM_ID]);
			m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.OPERATION_DATE]);
			m_SCANNER_ID = DBConvert.ToStringNull(dr[TBL_BAT_TRFUBI.SCANNER_ID]);
			m_BAT_ID = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.BAT_ID]);
			m_IMAGE_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.IMAGE_NO]);
			m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.DETAILS_NO]);
			m_FUBI_NO_01 = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.FUBI_NO_01]);
			m_FUBI_NO_02 = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.FUBI_NO_02]);
			m_FUBI_NO_03 = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.FUBI_NO_03]);
			m_FUBI_NO_04 = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.FUBI_NO_04]);
			m_FUBI_NO_05 = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.FUBI_NO_05]);
			m_FUBI_NO_06 = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.FUBI_NO_06]);
			m_FUBI_NO_07 = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.FUBI_NO_07]);
			m_FUBI_NO_08 = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.FUBI_NO_08]);
			m_FUBI_NO_09 = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.FUBI_NO_09]);
			m_FUBI_NO_10 = DBConvert.ToIntNull(dr[TBL_BAT_TRFUBI.FUBI_NO_10]);
			m_FUBI_BIKOU = DBConvert.ToStringNull(dr[TBL_BAT_TRFUBI.FUBI_BIKOU]);
		}

		#endregion

		#region クエリ取得

		/// <summary>
		/// キー項目を条件とするSELECT文を作成します
		/// </summary>
		/// <returns></returns>
		public static string GetSelectQuery()
		{
			string strSql = "SELECT * FROM " + TBL_BAT_TRFUBI.TABLE_NAME +
			" ORDER BY " +
				TBL_BAT_TRFUBI.GYM_ID + "," +
				TBL_BAT_TRFUBI.OPERATION_DATE + "," +
				TBL_BAT_TRFUBI.SCANNER_ID + "," +
				TBL_BAT_TRFUBI.BAT_ID + "," +
				TBL_BAT_TRFUBI.IMAGE_NO + "," +
				TBL_BAT_TRFUBI.DETAILS_NO;
			return strSql;
		}

		/// <summary>
		/// キー項目を条件とするSELECT文を作成します
		/// </summary>
		/// <returns></returns>
		public static string GetSelectQuery(int gymid, int opedate, string scanid, int batid, int imageno, int detailno)
        {
			string strSql = "SELECT * FROM " + TBL_BAT_TRFUBI.TABLE_NAME +
			" WHERE " +
				TBL_BAT_TRFUBI.GYM_ID + "=" + gymid + " AND " +
				TBL_BAT_TRFUBI.OPERATION_DATE + "=" + opedate + " AND " +
				TBL_BAT_TRFUBI.SCANNER_ID + "='" + scanid + "' AND " +
				TBL_BAT_TRFUBI.BAT_ID + "=" + batid + " AND " +
				TBL_BAT_TRFUBI.IMAGE_NO + "=" + imageno + " AND " +
				TBL_BAT_TRFUBI.DETAILS_NO + "=" + detailno;
			return strSql;
        }

		/// <summary>
		/// INSERT文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
            string strSql = "INSERT INTO " + TBL_BAT_TRFUBI.TABLE_NAME + " (" +
				TBL_BAT_TRFUBI.GYM_ID + "," +
				TBL_BAT_TRFUBI.OPERATION_DATE + "," +
				TBL_BAT_TRFUBI.SCANNER_ID + "," +
				TBL_BAT_TRFUBI.BAT_ID + "," +
				TBL_BAT_TRFUBI.IMAGE_NO + "," +
				TBL_BAT_TRFUBI.DETAILS_NO + "," +
				TBL_BAT_TRFUBI.FUBI_NO_01 + "," +
				TBL_BAT_TRFUBI.FUBI_NO_02 + "," +
				TBL_BAT_TRFUBI.FUBI_NO_03 + "," +
				TBL_BAT_TRFUBI.FUBI_NO_04 + "," +
				TBL_BAT_TRFUBI.FUBI_NO_05 + "," +
				TBL_BAT_TRFUBI.FUBI_NO_06 + "," +
				TBL_BAT_TRFUBI.FUBI_NO_07 + "," +
				TBL_BAT_TRFUBI.FUBI_NO_08 + "," +
				TBL_BAT_TRFUBI.FUBI_NO_09 + "," +
				TBL_BAT_TRFUBI.FUBI_NO_10 + "," +
				TBL_BAT_TRFUBI.FUBI_BIKOU + ") VALUES (" +
				m_GYM_ID + "," +
				m_OPERATION_DATE + "," +
				"'" + m_SCANNER_ID + "'," +
				m_BAT_ID + "," +
				m_IMAGE_NO + "," +
				m_DETAILS_NO + "," +
				m_FUBI_NO_01 + "," +
				m_FUBI_NO_02 + "," +
				m_FUBI_NO_03 + "," +
				m_FUBI_NO_04 + "," +
				m_FUBI_NO_05 + "," +
				m_FUBI_NO_06 + "," +
				m_FUBI_NO_07 + "," +
				m_FUBI_NO_08 + "," +
				m_FUBI_NO_09 + "," +
				m_FUBI_NO_10 + "," +
				"'" + m_FUBI_BIKOU + "')";
			return strSql;
		}

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_BAT_TRFUBI.TABLE_NAME + " SET " +
				TBL_BAT_TRFUBI.FUBI_NO_01 + "=" + m_FUBI_NO_01 + ", " +
				TBL_BAT_TRFUBI.FUBI_NO_02 + "=" + m_FUBI_NO_02 + ", " +
				TBL_BAT_TRFUBI.FUBI_NO_03 + "=" + m_FUBI_NO_03 + ", " +
				TBL_BAT_TRFUBI.FUBI_NO_04 + "=" + m_FUBI_NO_04 + ", " +
				TBL_BAT_TRFUBI.FUBI_NO_05 + "=" + m_FUBI_NO_05 + ", " +
				TBL_BAT_TRFUBI.FUBI_NO_06 + "=" + m_FUBI_NO_06 + ", " +
				TBL_BAT_TRFUBI.FUBI_NO_07 + "=" + m_FUBI_NO_07 + ", " +
				TBL_BAT_TRFUBI.FUBI_NO_08 + "=" + m_FUBI_NO_08 + ", " +
				TBL_BAT_TRFUBI.FUBI_NO_09 + "=" + m_FUBI_NO_09 + ", " +
				TBL_BAT_TRFUBI.FUBI_NO_10 + "=" + m_FUBI_NO_10 + ", " +
				TBL_BAT_TRFUBI.FUBI_BIKOU + "='" + m_FUBI_BIKOU + "'" +
				" WHERE " +
				TBL_BAT_TRFUBI.GYM_ID + "=" + m_GYM_ID + " AND " +
				TBL_BAT_TRFUBI.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
				TBL_BAT_TRFUBI.SCANNER_ID + "='" + m_SCANNER_ID + "' AND " +
				TBL_BAT_TRFUBI.BAT_ID + "=" + m_BAT_ID + " AND " +
				TBL_BAT_TRFUBI.IMAGE_NO + "=" + m_IMAGE_NO + " AND " +
				TBL_BAT_TRFUBI.DETAILS_NO + "=" + m_DETAILS_NO;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_BAT_TRFUBI.TABLE_NAME +
				" WHERE " +
				TBL_BAT_TRFUBI.GYM_ID + "=" + m_GYM_ID + " AND " +
				TBL_BAT_TRFUBI.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
				TBL_BAT_TRFUBI.SCANNER_ID + "='" + m_SCANNER_ID + "' AND " +
				TBL_BAT_TRFUBI.BAT_ID + "=" + m_BAT_ID + " AND " +
				TBL_BAT_TRFUBI.IMAGE_NO + "=" + m_IMAGE_NO + " AND " +
				TBL_BAT_TRFUBI.DETAILS_NO + "=" + m_DETAILS_NO;
			return strSql;
        }

        #endregion
    }
}
