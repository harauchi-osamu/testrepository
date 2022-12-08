using System;
using System.Data;
using System.Data.Common;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// オペレーター
	/// </summary>
	public class TBL_PRC_GYM_SUM
	{
        public TBL_PRC_GYM_SUM()
        {
        }

        public TBL_PRC_GYM_SUM(int gym_id)
        {
            m_GYM_ID = gym_id;
        }

        public TBL_PRC_GYM_SUM(DataRow dr)
		{
			initializeByDataRow(dr);
		}


        #region テーブル定義
        public const string TABLE_NAME = TABLE_SCHEMA + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_SCHEMA = "hen";
        public const string TABLE_PHYSICAL_NAME = "PRC_GYM_SUM";

        public const string GYM_ID = "GYM_ID";
        public const string GYM_SEQ = "GYM_SEQ";
        public const string GYM_KANJI = "GYM_KANJI";
        public const string TALLY_DATE = "TALLY_DATE";
        public const string ACCEPT_CNT = "ACCEPT_CNT";

        #endregion

        #region メンバ
        public int m_GYM_ID = 0;
        public int m_GYM_SEQ = 0;
        public string m_GYM_KANJI = "";
        public int m_TALLY_DATE = 0;
        public int m_ACCEPT_CNT = 0;

        #endregion

        #region プロパティ

        public int _GYM_ID
        {
            get { return m_GYM_ID; }
        }

        #endregion

        #region 初期化

        /// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_PRC_GYM_SUM.GYM_ID]);
            m_GYM_SEQ = DBConvert.ToIntNull(dr[TBL_PRC_GYM_SUM.GYM_SEQ]);
            m_GYM_KANJI = DBConvert.ToStringNull(dr[TBL_PRC_GYM_SUM.GYM_KANJI]);
            m_TALLY_DATE = DBConvert.ToIntNull(dr[TBL_PRC_GYM_SUM.TALLY_DATE]);
            m_ACCEPT_CNT = DBConvert.ToIntNull(dr[TBL_PRC_GYM_SUM.ACCEPT_CNT]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gymid"></param>
        /// <returns></returns>
        public static string GetSelectQuery()
		{
            string strSql = "SELECT * FROM " + TBL_PRC_GYM_SUM.TABLE_NAME +
                " ORDER BY " + 
                TBL_PRC_GYM_SUM.GYM_ID + " , " + 
                TBL_PRC_GYM_SUM.GYM_SEQ + " , " +
                TBL_PRC_GYM_SUM.TALLY_DATE;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="GYM_ID">オペ番号</param>
        /// <returns></returns>
        public static string GetSelectQuery(int gym_id)
        {
            string strSql = "SELECT * FROM " + TBL_PRC_GYM_SUM.TABLE_NAME +
                " WHERE " + TBL_PRC_GYM_SUM.GYM_ID + " = " + gym_id + 
                " ORDER BY " +
                TBL_PRC_GYM_SUM.GYM_ID + " , " +
                TBL_PRC_GYM_SUM.GYM_SEQ + " , " +
                TBL_PRC_GYM_SUM.TALLY_DATE;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gym_id">業務番号</param>
        /// <param name="gym_seq">業務通番</param>
        /// <param name="tally_date">集計日</param>
        /// <returns></returns>
        public static string GetSelectQuery(int gym_id, int gym_seq, int tally_date)
        {
            string strSql = "SELECT * FROM " + TBL_PRC_GYM_SUM.TABLE_NAME +
                " WHERE " +
                TBL_PRC_GYM_SUM.GYM_ID + " = " + gym_id + " AND " +
                TBL_PRC_GYM_SUM.GYM_SEQ + " = " + gym_seq + " AND " +
                TBL_PRC_GYM_SUM.TALLY_DATE + " = " + tally_date +
                " ORDER BY " +
                TBL_PRC_GYM_SUM.GYM_ID + " , " +
                TBL_PRC_GYM_SUM.GYM_SEQ + " , " +
                TBL_PRC_GYM_SUM.TALLY_DATE;
            return strSql;
        }

        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_PRC_GYM_SUM.TABLE_NAME + " (" +
                TBL_PRC_GYM_SUM.GYM_ID + ", " +
                TBL_PRC_GYM_SUM.GYM_SEQ + "," +
                TBL_PRC_GYM_SUM.GYM_KANJI + "," +
                TBL_PRC_GYM_SUM.TALLY_DATE + "," +
                TBL_PRC_GYM_SUM.ACCEPT_CNT + ") VALUES (" +
                m_GYM_ID + "," +
                m_GYM_SEQ + "," +
                "'" + m_GYM_KANJI + "'" +  "," +
                m_TALLY_DATE + "," +
                m_ACCEPT_CNT +
                ")";

            return strSql;
        }

        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_PRC_GYM_SUM.TABLE_NAME + " SET " +
                TBL_PRC_GYM_SUM.GYM_KANJI + " = " + "'" + m_GYM_KANJI + "'" +
                TBL_PRC_GYM_SUM.ACCEPT_CNT + " = " + m_ACCEPT_CNT + ", " +
                " WHERE " +
                TBL_PRC_GYM_SUM.GYM_ID + " = " + m_GYM_ID + " AND " +
                TBL_PRC_GYM_SUM.GYM_SEQ + " = " + m_GYM_SEQ + " AND " +
                TBL_PRC_GYM_SUM.TALLY_DATE + " = " + m_TALLY_DATE;
            return strSql;
        }

        public string GetDeleteQuery(int gymid)
        {
            string strSql = "DELETE FROM " + TBL_PRC_GYM_SUM.TABLE_NAME +
                " WHERE " +
                TBL_PRC_GYM_SUM.GYM_ID + " = " + m_GYM_ID + " AND " +
                TBL_PRC_GYM_SUM.GYM_SEQ + " = " + m_GYM_SEQ + " AND " +
                TBL_PRC_GYM_SUM.TALLY_DATE + " = " + m_TALLY_DATE;

            return strSql;
        }

        #endregion
    }
}
