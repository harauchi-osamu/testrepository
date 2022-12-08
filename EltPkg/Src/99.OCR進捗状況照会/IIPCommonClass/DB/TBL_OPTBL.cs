using System;
using System.Data;
using System.Data.Common;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// オペレーター
	/// </summary>
	public class TBL_OPTBL
	{
        public TBL_OPTBL()
        {
        }

        public TBL_OPTBL(string opno)
        {
			m_OP_NO = opno;
        }

		public TBL_OPTBL(DataRow dr)
		{
			initializeByDataRow(dr);
		}

        #region 定数定義
        public const int AUTH_TYPE_NASHI = 0;
        public const int AUTH_TYPE_SYSADMIN = 1;
        public const int AUTH_TYPE_OPEADMIN = 2;
        public const int AUTH_TYPE_OPERATOR = 3;
        #endregion

        #region テーブル定義

		public const string TABLE_NAME = "OPTBL";

		public const string OP_NO = "OP_NO";
		public const string OP_NAME = "OP_NAME";
		public const string OP_KBN = "OP_KBN";
		public const string OP_PWORD = "OP_PWORD";
		public const string OP_PWTOUROKU = "OP_PWTOUROKU";
		public const string OP_PWMUKOU = "OP_PWMUKOU";
		public const string OP_REJECT_FLG = "OP_REJECT_FLG";
		public const string OP_REJECT_CNT = "OP_REJECT_CNT";
		public const string OP_PRV_PWORD = "OP_PRV_PWORD";
		public const string KANA_KBN = "KANA_KBN";
		public const string RANK_01 = "RANK_01";
		public const string AUTH_01 = "AUTH_01";
		public const string RANK_02 = "RANK_02";
		public const string AUTH_02 = "AUTH_02";
		public const string RANK_03 = "RANK_03";
		public const string AUTH_03 = "AUTH_03";
		public const string RANK_04 = "RANK_04";
		public const string AUTH_04 = "AUTH_04";
		public const string RANK_05 = "RANK_05";
		public const string AUTH_05 = "AUTH_05";
		public const string RANK_06 = "RANK_06";
		public const string AUTH_06 = "AUTH_06";
		public const string RANK_07 = "RANK_07";
		public const string AUTH_07 = "AUTH_07";
		public const string RANK_08 = "RANK_08";
		public const string AUTH_08 = "AUTH_08";
		public const string RANK_09 = "RANK_09";
		public const string AUTH_09 = "AUTH_09";
		public const string RANK_10 = "RANK_10";
		public const string AUTH_10 = "AUTH_10";
		public const string RANK_11 = "RANK_11";
		public const string AUTH_11 = "AUTH_11";
		public const string RANK_12 = "RANK_12";
		public const string AUTH_12 = "AUTH_12";
		public const string RANK_13 = "RANK_13";
		public const string AUTH_13 = "AUTH_13";
		public const string RANK_14 = "RANK_14";
		public const string AUTH_14 = "AUTH_14";
		public const string RANK_15 = "RANK_15";
		public const string AUTH_15 = "AUTH_15";
		public const string RANK_16 = "RANK_16";
		public const string AUTH_16 = "AUTH_16";
		public const string RANK_17 = "RANK_17";
		public const string AUTH_17 = "AUTH_17";
		public const string RANK_18 = "RANK_18";
		public const string AUTH_18 = "AUTH_18";
		public const string RANK_19 = "RANK_19";
		public const string AUTH_19 = "AUTH_19";
		public const string RANK_20 = "RANK_20";
		public const string AUTH_20 = "AUTH_20";

		#endregion

		#region メンバ

		private string m_OP_NO = "";
		public string m_OP_NAME = "";
		public int m_OP_KBN = 0;
		public string m_OP_PWORD = "";
		public string m_OP_PWTOUROKU = "";
		public string m_OP_PWMUKOU = "";
		public string m_OP_REJECT_FLG = "";
		public int m_OP_REJECT_CNT = 0;
		public string m_OP_PRV_PWORD = "";
		public int m_KANA_KBN = 0;
		public string m_RANK_01 = "";
		public string m_AUTH_01 = "";
		public string m_RANK_02 = "";
		public string m_AUTH_02 = "";
		public string m_RANK_03 = "";
		public string m_AUTH_03 = "";
		public string m_RANK_04 = "";
		public string m_AUTH_04 = "";
		public string m_RANK_05 = "";
		public string m_AUTH_05 = "";
		public string m_RANK_06 = "";
		public string m_AUTH_06 = "";
		public string m_RANK_07 = "";
		public string m_AUTH_07 = "";
		public string m_RANK_08 = "";
		public string m_AUTH_08 = "";
		public string m_RANK_09 = "";
		public string m_AUTH_09 = "";
		public string m_RANK_10 = "";
		public string m_AUTH_10 = "";
		public string m_RANK_11 = "";
		public string m_AUTH_11 = "";
		public string m_RANK_12 = "";
		public string m_AUTH_12 = "";
		public string m_RANK_13 = "";
		public string m_AUTH_13 = "";
		public string m_RANK_14 = "";
		public string m_AUTH_14 = "";
		public string m_RANK_15 = "";
		public string m_AUTH_15 = "";
		public string m_RANK_16 = "";
		public string m_AUTH_16 = "";
		public string m_RANK_17 = "";
		public string m_AUTH_17 = "";
		public string m_RANK_18 = "";
		public string m_AUTH_18 = "";
		public string m_RANK_19 = "";
		public string m_AUTH_19 = "";
		public string m_RANK_20 = "";
		public string m_AUTH_20 = "";

		#endregion

		#region プロパティ

		public string _OP_NO
		{
            get { return m_OP_NO; }
        }

        #endregion

        #region 初期化

        /// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
			m_OP_NO = DBConvert.ToStringNull(dr[TBL_OPTBL.OP_NO]);
			m_OP_NAME = DBConvert.ToStringNull(dr[TBL_OPTBL.OP_NAME]);
			m_OP_KBN = DBConvert.ToIntNull(dr[TBL_OPTBL.OP_KBN]);
			m_OP_PWORD = DBConvert.ToStringNull(dr[TBL_OPTBL.OP_PWORD]);
			m_OP_PWTOUROKU = DBConvert.ToStringNull(dr[TBL_OPTBL.OP_PWTOUROKU]);
			m_OP_PWMUKOU = DBConvert.ToStringNull(dr[TBL_OPTBL.OP_PWMUKOU]);
			m_OP_REJECT_FLG = DBConvert.ToStringNull(dr[TBL_OPTBL.OP_REJECT_FLG]);
			m_OP_REJECT_CNT = DBConvert.ToIntNull(dr[TBL_OPTBL.OP_REJECT_CNT]);
			m_OP_PRV_PWORD = DBConvert.ToStringNull(dr[TBL_OPTBL.OP_PRV_PWORD]);
			m_KANA_KBN = DBConvert.ToIntNull(dr[TBL_OPTBL.KANA_KBN]);
			m_RANK_01 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_01]);
			m_AUTH_01 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_01]);
			m_RANK_02 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_02]);
			m_AUTH_02 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_02]);
			m_RANK_03 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_03]);
			m_AUTH_03 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_03]);
			m_RANK_04 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_04]);
			m_AUTH_04 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_04]);
			m_RANK_05 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_05]);
			m_AUTH_05 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_05]);
			m_RANK_06 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_06]);
			m_AUTH_06 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_06]);
			m_RANK_07 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_07]);
			m_AUTH_07 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_07]);
			m_RANK_08 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_08]);
			m_AUTH_08 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_08]);
			m_RANK_09 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_09]);
			m_AUTH_09 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_09]);
			m_RANK_10 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_10]);
			m_AUTH_10 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_10]);
			m_RANK_11 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_11]);
			m_AUTH_11 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_11]);
			m_RANK_12 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_12]);
			m_AUTH_12 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_12]);
			m_RANK_13 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_13]);
			m_AUTH_13 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_13]);
			m_RANK_14 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_14]);
			m_AUTH_14 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_14]);
			m_RANK_15 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_15]);
			m_AUTH_15 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_15]);
			m_RANK_16 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_16]);
			m_AUTH_16 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_16]);
			m_RANK_17 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_17]);
			m_AUTH_17 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_17]);
			m_RANK_18 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_18]);
			m_AUTH_18 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_18]);
			m_RANK_19 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_19]);
			m_AUTH_19 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_19]);
			m_RANK_20 = DBConvert.ToStringNull(dr[TBL_OPTBL.RANK_20]);
			m_AUTH_20 = DBConvert.ToStringNull(dr[TBL_OPTBL.AUTH_20]);
		}

		#endregion

		#region クエリ取得

		/// <summary>
		/// キー項目を条件とするSELECT文を作成します
		/// </summary>
		/// <param name="gymid">業務番号</param>
		/// <returns></returns>
		public static string GetSelectQuery()
		{
            string strSql = @"SELECT *, ROW_NUMBER() OVER(ORDER BY OP_NO) AS No
                           FROM  " + TBL_OPTBL.TABLE_NAME + 
               " ORDER BY  OP_KBN,OP_NO ";

            return strSql;
		}

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <returns></returns>
        public static string GetSelectQuery(string opno)
        {
            string strSql = "SELECT * FROM " + TBL_OPTBL.TABLE_NAME +
                  " WHERE " +
                        TBL_OPTBL.OP_NO + "='" + opno + "'";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <returns></returns>
        public static string GetSelectQuery(string opno, string pass)
		{
			string strSql = "SELECT * FROM " + TBL_OPTBL.TABLE_NAME +
				  " WHERE " +
						TBL_OPTBL.OP_NO + "='" + opno + "' AND " +
						TBL_OPTBL.OP_PWORD + "='" + pass + "'";
			return strSql;
		}

		/// <summary>
		/// INSERT文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
            string strSql = "INSERT INTO " + TBL_OPTBL.TABLE_NAME + " (" +
				TBL_OPTBL.OP_NO + "," +
				TBL_OPTBL.OP_NAME + "," +
				TBL_OPTBL.OP_KBN + "," +
				TBL_OPTBL.OP_PWORD + "," +
				TBL_OPTBL.OP_PWTOUROKU + "," +
				TBL_OPTBL.OP_PWMUKOU + "," +
				TBL_OPTBL.OP_REJECT_FLG + "," +
				TBL_OPTBL.OP_REJECT_CNT + "," +
				TBL_OPTBL.OP_PRV_PWORD + "," +
				TBL_OPTBL.KANA_KBN + "," +
				TBL_OPTBL.RANK_01 + "," +
				TBL_OPTBL.AUTH_01 + "," +
				TBL_OPTBL.RANK_02 + "," +
				TBL_OPTBL.AUTH_02 + "," +
				TBL_OPTBL.RANK_03 + "," +
				TBL_OPTBL.AUTH_03 + "," +
				TBL_OPTBL.RANK_04 + "," +
				TBL_OPTBL.AUTH_04 + "," +
				TBL_OPTBL.RANK_05 + "," +
				TBL_OPTBL.AUTH_05 + "," +
				TBL_OPTBL.RANK_06 + "," +
				TBL_OPTBL.AUTH_06 + "," +
				TBL_OPTBL.RANK_07 + "," +
				TBL_OPTBL.AUTH_07 + "," +
				TBL_OPTBL.RANK_08 + "," +
				TBL_OPTBL.AUTH_08 + "," +
				TBL_OPTBL.RANK_09 + "," +
				TBL_OPTBL.AUTH_09 + "," +
				TBL_OPTBL.RANK_10 + "," +
				TBL_OPTBL.AUTH_10 + "," +
				TBL_OPTBL.RANK_11 + "," +
				TBL_OPTBL.AUTH_11 + "," +
				TBL_OPTBL.RANK_12 + "," +
				TBL_OPTBL.AUTH_12 + "," +
				TBL_OPTBL.RANK_13 + "," +
				TBL_OPTBL.AUTH_13 + "," +
				TBL_OPTBL.RANK_14 + "," +
				TBL_OPTBL.AUTH_14 + "," +
				TBL_OPTBL.RANK_15 + "," +
				TBL_OPTBL.AUTH_15 + "," +
				TBL_OPTBL.RANK_16 + "," +
				TBL_OPTBL.AUTH_16 + "," +
				TBL_OPTBL.RANK_17 + "," +
				TBL_OPTBL.AUTH_17 + "," +
				TBL_OPTBL.RANK_18 + "," +
				TBL_OPTBL.AUTH_18 + "," +
				TBL_OPTBL.RANK_19 + "," +
				TBL_OPTBL.AUTH_19 + "," +
				TBL_OPTBL.RANK_20 + "," +
				TBL_OPTBL.AUTH_20 + ") VALUES (" +
				"'" + m_OP_NO + "'," +
				"'" + m_OP_NAME + "'," +
				m_OP_KBN + "," +
				"'" + m_OP_PWORD + "'," +
				"'" + m_OP_PWTOUROKU + "'," +
				"'" + m_OP_PWMUKOU + "'," +
				"'" + m_OP_REJECT_FLG + "'," +
				m_OP_REJECT_CNT + "," +
				"'" + m_OP_PRV_PWORD + "'," +
				m_KANA_KBN + "," +
				"'" + m_RANK_01 + "'," +
				"'" + m_AUTH_01 + "'," +
				"'" + m_RANK_02 + "'," +
				"'" + m_AUTH_02 + "'," +
				"'" + m_RANK_03 + "'," +
				"'" + m_AUTH_03 + "'," +
				"'" + m_RANK_04 + "'," +
				"'" + m_AUTH_04 + "'," +
				"'" + m_RANK_05 + "'," +
				"'" + m_AUTH_05 + "'," +
				"'" + m_RANK_06 + "'," +
				"'" + m_AUTH_06 + "'," +
				"'" + m_RANK_07 + "'," +
				"'" + m_AUTH_07 + "'," +
				"'" + m_RANK_08 + "'," +
				"'" + m_AUTH_08 + "'," +
				"'" + m_RANK_09 + "'," +
				"'" + m_AUTH_09 + "'," +
				"'" + m_RANK_10 + "'," +
				"'" + m_AUTH_10 + "'," +
				"'" + m_RANK_11 + "'," +
				"'" + m_AUTH_11 + "'," +
				"'" + m_RANK_12 + "'," +
				"'" + m_AUTH_12 + "'," +
				"'" + m_RANK_13 + "'," +
				"'" + m_AUTH_13 + "'," +
				"'" + m_RANK_14 + "'," +
				"'" + m_AUTH_14 + "'," +
				"'" + m_RANK_15 + "'," +
				"'" + m_AUTH_15 + "'," +
				"'" + m_RANK_16 + "'," +
				"'" + m_AUTH_16 + "'," +
				"'" + m_RANK_17 + "'," +
				"'" + m_AUTH_17 + "'," +
				"'" + m_RANK_18 + "'," +
				"'" + m_AUTH_18 + "'," +
				"'" + m_RANK_19 + "'," +
				"'" + m_AUTH_19 + "'," +
				"'" + m_RANK_20 + "'," +
				"'" + m_AUTH_20 + "')";
			return strSql;
		}

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_OPTBL.TABLE_NAME + " SET " +
				TBL_OPTBL.OP_NAME + "='" + m_OP_NAME + "', " +
				TBL_OPTBL.OP_KBN + "=" + m_OP_KBN + ", " +
				TBL_OPTBL.OP_PWORD + "='" + m_OP_PWORD + "', " +
				TBL_OPTBL.OP_PWTOUROKU + "='" + m_OP_PWTOUROKU + "', " +
				TBL_OPTBL.OP_PWMUKOU + "='" + m_OP_PWMUKOU + "', " +
				TBL_OPTBL.OP_REJECT_FLG + "='" + m_OP_REJECT_FLG + "', " +
				TBL_OPTBL.OP_REJECT_CNT + "=" + m_OP_REJECT_CNT + ", " +
				TBL_OPTBL.OP_PRV_PWORD + "='" + m_OP_PRV_PWORD + "', " +
				TBL_OPTBL.KANA_KBN + "=" + m_KANA_KBN + ", " +
				TBL_OPTBL.RANK_01 + "='" + m_RANK_01 + "', " +
				TBL_OPTBL.AUTH_01 + "='" + m_AUTH_01 + "', " +
				TBL_OPTBL.RANK_02 + "='" + m_RANK_02 + "', " +
				TBL_OPTBL.AUTH_02 + "='" + m_AUTH_02 + "', " +
				TBL_OPTBL.RANK_03 + "='" + m_RANK_03 + "', " +
				TBL_OPTBL.AUTH_03 + "='" + m_AUTH_03 + "', " +
				TBL_OPTBL.RANK_04 + "='" + m_RANK_04 + "', " +
				TBL_OPTBL.AUTH_04 + "='" + m_AUTH_04 + "', " +
				TBL_OPTBL.RANK_05 + "='" + m_RANK_05 + "', " +
				TBL_OPTBL.AUTH_05 + "='" + m_AUTH_05 + "', " +
				TBL_OPTBL.RANK_06 + "='" + m_RANK_06 + "', " +
				TBL_OPTBL.AUTH_06 + "='" + m_AUTH_06 + "', " +
				TBL_OPTBL.RANK_07 + "='" + m_RANK_07 + "', " +
				TBL_OPTBL.AUTH_07 + "='" + m_AUTH_07 + "', " +
				TBL_OPTBL.RANK_08 + "='" + m_RANK_08 + "', " +
				TBL_OPTBL.AUTH_08 + "='" + m_AUTH_08 + "', " +
				TBL_OPTBL.RANK_09 + "='" + m_RANK_09 + "', " +
				TBL_OPTBL.AUTH_09 + "='" + m_AUTH_09 + "', " +
				TBL_OPTBL.RANK_10 + "='" + m_RANK_10 + "', " +
				TBL_OPTBL.AUTH_10 + "='" + m_AUTH_10 + "', " +
				TBL_OPTBL.RANK_11 + "='" + m_RANK_11 + "', " +
				TBL_OPTBL.AUTH_11 + "='" + m_AUTH_11 + "', " +
				TBL_OPTBL.RANK_12 + "='" + m_RANK_12 + "', " +
				TBL_OPTBL.AUTH_12 + "='" + m_AUTH_12 + "', " +
				TBL_OPTBL.RANK_13 + "='" + m_RANK_13 + "', " +
				TBL_OPTBL.AUTH_13 + "='" + m_AUTH_13 + "', " +
				TBL_OPTBL.RANK_14 + "='" + m_RANK_14 + "', " +
				TBL_OPTBL.AUTH_14 + "='" + m_AUTH_14 + "', " +
				TBL_OPTBL.RANK_15 + "='" + m_RANK_15 + "', " +
				TBL_OPTBL.AUTH_15 + "='" + m_AUTH_15 + "', " +
				TBL_OPTBL.RANK_16 + "='" + m_RANK_16 + "', " +
				TBL_OPTBL.AUTH_16 + "='" + m_AUTH_16 + "', " +
				TBL_OPTBL.RANK_17 + "='" + m_RANK_17 + "', " +
				TBL_OPTBL.AUTH_17 + "='" + m_AUTH_17 + "', " +
				TBL_OPTBL.RANK_18 + "='" + m_RANK_18 + "', " +
				TBL_OPTBL.AUTH_18 + "='" + m_AUTH_18 + "', " +
				TBL_OPTBL.RANK_19 + "='" + m_RANK_19 + "', " +
				TBL_OPTBL.AUTH_19 + "='" + m_AUTH_19 + "', " +
				TBL_OPTBL.RANK_20 + "='" + m_RANK_20 + "', " +
				TBL_OPTBL.AUTH_20 + "='" + m_AUTH_20 + "'" +
				" WHERE " +
				TBL_OPTBL.OP_NO + "='" + m_OP_NO + "'";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <returns></returns>
        public static string GetDeleteQuery(string opno)
        {
            string strSql = "DELETE FROM " + TBL_OPTBL.TABLE_NAME +
                  " WHERE " +
                        TBL_OPTBL.OP_NO + "='" + opno + "'";
             
            return strSql;
        }

        #endregion
    }
}
