using System;
using System.Data;
using System.Data.Common;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// オペレーター
	/// </summary>
	public class TBL_TANTOU_MST
	{
        public TBL_TANTOU_MST()
        {
        }

        public TBL_TANTOU_MST(string tantou_no)
        {
            m_TANTOU_NO = tantou_no;
        }

        public TBL_TANTOU_MST(DataRow dr)
		{
			initializeByDataRow(dr);
		}

        

        #region テーブル定義

		public const string TABLE_NAME = "TANTOU_MST";

        public const string TANTOU_NO = "TANTOU_NO";
        public const string TANTOU_NAME = "TANTOU_NAME";
        public const string BRANCH_NO = "BRANCH_NO";
        public const string PASSWORD = "PASSWORD";
        public const string AUTH = "AUTH";
        public const string REJECT_FLG = "REJECT_FLG";
        public const string PW_REJECT_CNT = "PW_REJECT_CNT";
        public const string UPDATE_DATE = "UPDATE_DATE";

        #endregion

        #region メンバ

        public string m_TANTOU_NO = "";
        public string m_TANTOU_NAME = "";
        public string m_BRANCH_NO = "";
        public string m_PASSWORD = "";
        public string m_AUTH = "";
        public int m_REJECT_FLG = 0;
        public int m_PW_REJECT_CNT = 0;
        public string m_UPDATE_DATE = "";

        #endregion

        #region プロパティ

        public string _TANTOU_NO
        {
            get { return m_TANTOU_NO; }
        }

        #endregion

        #region 初期化

        /// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
            m_TANTOU_NO = DBConvert.ToStringNull(dr[TBL_TANTOU_MST.TANTOU_NO]);
            m_TANTOU_NAME = DBConvert.ToStringNull(dr[TBL_TANTOU_MST.TANTOU_NAME]);
            m_BRANCH_NO = DBConvert.ToStringNull(dr[TBL_TANTOU_MST.BRANCH_NO]);
            m_PASSWORD = DBConvert.ToStringNull(dr[TBL_TANTOU_MST.PASSWORD]);
            m_AUTH = DBConvert.ToStringNull(dr[TBL_TANTOU_MST.AUTH]);
            m_REJECT_FLG = DBConvert.ToIntNull(dr[TBL_TANTOU_MST.REJECT_FLG]);
            m_PW_REJECT_CNT = DBConvert.ToIntNull(dr[TBL_TANTOU_MST.PW_REJECT_CNT]);
            m_UPDATE_DATE = DBConvert.ToStringNull(dr[TBL_TANTOU_MST.UPDATE_DATE]);
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
            string strSql = @"SELECT *, ROW_NUMBER() OVER(ORDER BY TANTOU_NO) AS No
                           FROM  " + TBL_TANTOU_MST.TABLE_NAME +
               " ORDER BY  TANTOU_NO";

            return strSql;
		}

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <returns></returns>
        public static string GetSelectQuery(string tantou_no)
        {
            string strSql = "SELECT * FROM " + TBL_TANTOU_MST.TABLE_NAME +
                  " WHERE " +
                        TBL_TANTOU_MST.TANTOU_NO + "='" + tantou_no + "'";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <returns></returns>
        public static string GetSelectQuery(string tantou_no, string pass)
		{
			string strSql = "SELECT * FROM " + TBL_TANTOU_MST.TABLE_NAME +
				  " WHERE " +
						TBL_TANTOU_MST.TANTOU_NO + "='" + tantou_no + "' AND " +
						TBL_TANTOU_MST.PASSWORD + "='" + pass + "'";
			return strSql;
		}

		/// <summary>
		/// INSERT文を作成します
		/// </summary>
		/// <returns></returns>
		public string GetInsertQuery()
		{
            string strSql = "INSERT INTO " + TBL_TANTOU_MST.TABLE_NAME + " (" +


                TBL_TANTOU_MST.TANTOU_NO + "," +
                TBL_TANTOU_MST.TANTOU_NAME + "," +
                TBL_TANTOU_MST.BRANCH_NO + "," +
                TBL_TANTOU_MST.PASSWORD + "," +
                TBL_TANTOU_MST.AUTH + "," +
                TBL_TANTOU_MST.REJECT_FLG + "," +
                TBL_TANTOU_MST.PW_REJECT_CNT + "," +
                TBL_TANTOU_MST.UPDATE_DATE +  ") VALUES (" +
                "'" + m_TANTOU_NO + "'," +
                "'" + m_TANTOU_NAME + "'," +
                "'" + m_BRANCH_NO + "'," +
                "'" + m_PASSWORD + "'," +
                "'" + m_AUTH + "'," +
                m_REJECT_FLG + "," +
                m_PW_REJECT_CNT + "," +
                "'" + m_UPDATE_DATE + "'" +
                 ")";
			return strSql;
		}

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_TANTOU_MST.TABLE_NAME + " SET " +
                TBL_TANTOU_MST.TANTOU_NAME + "='" + m_TANTOU_NAME + "', " +
                TBL_TANTOU_MST.BRANCH_NO + "='" + m_BRANCH_NO + "', " +
                TBL_TANTOU_MST.PASSWORD + "='" + m_PASSWORD + "', " +
                TBL_TANTOU_MST.AUTH + "='" + m_AUTH + "', " +
                TBL_TANTOU_MST.REJECT_FLG + "=" + m_REJECT_FLG + ", " +
                TBL_TANTOU_MST.PW_REJECT_CNT + "=" + m_PW_REJECT_CNT + ", " +
                TBL_TANTOU_MST.UPDATE_DATE + "='" + m_UPDATE_DATE + "'" +
                " WHERE " + TBL_TANTOU_MST.TANTOU_NO + "='" + m_TANTOU_NO + "'";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <returns></returns>
        public static string GetDeleteQuery(string tano)
        {
            string strSql = "DELETE FROM " + TBL_TANTOU_MST.TABLE_NAME +
                  " WHERE " + TBL_TANTOU_MST.TANTOU_NO + "='" + tano + "'";

            return strSql;
        }

        #endregion
    }
}
