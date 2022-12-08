using System;
using System.Data;
using System.Data.Common;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// オペレーター
	/// </summary>
	public class TBL_PRC_OPE_SUM
	{
        public TBL_PRC_OPE_SUM()
        {
        }

        public TBL_PRC_OPE_SUM(int gym_id)
        {
            m_GYM_ID = gym_id;
        }

        public TBL_PRC_OPE_SUM(DataRow dr)
		{
			initializeByDataRow(dr);
		}


        #region テーブル定義
        public const string TABLE_NAME = TABLE_SCHEMA + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_SCHEMA = "hen";
        public const string TABLE_PHYSICAL_NAME = "PRC_OPE_SUM";

        public const string OP_NO = "OP_NO";
        public const string OP_NAME = "OP_NAME";
        public const string OP_KBN = "OP_KBN";
        public const string GYM_ID = "GYM_ID";
        public const string GYM_SEQ = "GYM_SEQ";
        public const string GYM_KANJI = "GYM_KANJI";
        public const string TALLY_DATE = "TALLY_DATE";
        public const string TTL_TIME = "TTL_TIME";
        public const string CORR_CNT = "CORR_CNT";
        public const string CNT1 = "CNT1";
        public const string CNT2 = "CNT2";
        public const string CNT3 = "CNT3";
        public const string CNT4 = "CNT4";
        public const string CNT5 = "CNT5";
        public const string TTL_CNT = "TTL_CNT";

        #endregion

        #region メンバ
        public string m_OP_NO = "";
        public string m_OP_NAME = "";
        public int m_OP_KBN = 0;
        public int m_GYM_ID = 0;
        public int m_GYM_SEQ = 0;
        public string m_GYM_KANJI = "";
        public int m_TALLY_DATE = 0;
        public int m_TTL_TIME = 0;
        public int m_CORR_CNT = 0;
        public int m_CNT1 = 0;
        public int m_CNT2 = 0;
        public int m_CNT3 = 0;
        public int m_CNT4 = 0;
        public int m_CNT5 = 0;
        public int m_TTL_CNT = 0;

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
            m_OP_NO = DBConvert.ToStringNull(dr[TBL_PRC_OPE_SUM.OP_NO]);
            m_OP_NAME = DBConvert.ToStringNull(dr[TBL_PRC_OPE_SUM.OP_NAME]);
            m_OP_KBN = DBConvert.ToIntNull(dr[TBL_PRC_OPE_SUM.OP_KBN]);
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_PRC_OPE_SUM.GYM_ID]);
            m_GYM_SEQ = DBConvert.ToIntNull(dr[TBL_PRC_OPE_SUM.GYM_SEQ]);
            m_GYM_KANJI = DBConvert.ToStringNull(dr[TBL_PRC_OPE_SUM.GYM_KANJI]);
            m_TALLY_DATE = DBConvert.ToIntNull(dr[TBL_PRC_OPE_SUM.TALLY_DATE]);
            m_TTL_TIME = DBConvert.ToIntNull(dr[TBL_PRC_OPE_SUM.TTL_TIME]);
            m_CORR_CNT = DBConvert.ToIntNull(dr[TBL_PRC_OPE_SUM.CORR_CNT]);
            m_CNT1 = DBConvert.ToIntNull(dr[TBL_PRC_OPE_SUM.CNT1]);
            m_CNT2 = DBConvert.ToIntNull(dr[TBL_PRC_OPE_SUM.CNT2]);
            m_CNT3 = DBConvert.ToIntNull(dr[TBL_PRC_OPE_SUM.CNT3]);
            m_CNT4 = DBConvert.ToIntNull(dr[TBL_PRC_OPE_SUM.CNT4]);
            m_CNT5 = DBConvert.ToIntNull(dr[TBL_PRC_OPE_SUM.CNT5]);
            m_TTL_CNT = DBConvert.ToIntNull(dr[TBL_PRC_OPE_SUM.TTL_CNT]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string GetSelectQuery()
		{
            string strSql = "SELECT * FROM " + TBL_PRC_OPE_SUM.TABLE_NAME +
                " ORDER BY " +
                TBL_PRC_OPE_SUM.OP_NO + " , " +
                TBL_PRC_OPE_SUM.GYM_ID + " , " + 
                TBL_PRC_OPE_SUM.GYM_SEQ;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="op_no">オペ番号</param>
        /// <returns></returns>
        public static string GetSelectQuery(string op_no)
        {
            string strSql = "SELECT * FROM " + TBL_PRC_OPE_SUM.TABLE_NAME +
                " WHERE " + TBL_PRC_OPE_SUM.OP_NO + "='" + op_no + "'" +
                " ORDER BY " +
                TBL_PRC_OPE_SUM.GYM_ID + " , " +
                TBL_PRC_OPE_SUM.GYM_SEQ;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="op_no">オペ番号</param>
        /// <param name="gym_id">業務番号</param>
        /// <param name="gym_seq">業務通番</param>
        /// <returns></returns>
        public static string GetSelectQuery(string op_no, int gym_id, int gym_seq)
        {
            string strSql = "SELECT * FROM " + TBL_PRC_OPE_SUM.TABLE_NAME +
                " WHERE " +
                TBL_PRC_OPE_SUM.OP_NO + "='" + op_no + "'" + " AND " +
                TBL_PRC_OPE_SUM.GYM_ID + " = " + gym_id + " AND " +
                TBL_PRC_OPE_SUM.GYM_SEQ + " = " + gym_seq +
                " ORDER BY " +
                TBL_PRC_OPE_SUM.GYM_ID + " , " +
                TBL_PRC_OPE_SUM.GYM_SEQ;
            return strSql;
        }


        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_PRC_OPE_SUM.TABLE_NAME + " (" +
                TBL_PRC_OPE_SUM.OP_NO + ", " +
                TBL_PRC_OPE_SUM.OP_NAME + ", " +
                TBL_PRC_OPE_SUM.OP_KBN + ", " +
                TBL_PRC_OPE_SUM.GYM_ID + "," +
                TBL_PRC_OPE_SUM.GYM_SEQ + "," +
                TBL_PRC_OPE_SUM.GYM_KANJI + ", " +
                TBL_PRC_OPE_SUM.TALLY_DATE + ", " +
                TBL_PRC_OPE_SUM.TTL_TIME + ", " +
                TBL_PRC_OPE_SUM.CORR_CNT + ", " +
                TBL_PRC_OPE_SUM.CNT1 + ", " +
                TBL_PRC_OPE_SUM.CNT2 + ", " +
                TBL_PRC_OPE_SUM.CNT3 + ", " +
                TBL_PRC_OPE_SUM.CNT4 + ", " +
                TBL_PRC_OPE_SUM.CNT5 + ", " +
                TBL_PRC_OPE_SUM.TTL_CNT + ") VALUES (" +
                "'" + m_OP_NO + "'" + "," +
                "'" + m_OP_NAME + "'" + "," +
                m_OP_KBN + "," +
                m_GYM_ID + "," +
                m_GYM_SEQ + "," +
                "'" + m_GYM_KANJI + "'" + "," +
                m_TALLY_DATE + "," +
                m_TTL_TIME + "," +
                m_CORR_CNT + "," +
                m_CNT1 + "," +
                m_CNT2 + "," +
                m_CNT3 + "," +
                m_CNT4 + "," +
                m_CNT5 + "," +
                m_TTL_CNT +
                ")";

            return strSql;
        }

        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_PRC_OPE_SUM.TABLE_NAME + " SET " +
                TBL_PRC_OPE_SUM.GYM_ID + " = " + m_GYM_ID + ", " +
                TBL_PRC_OPE_SUM.GYM_SEQ + " = " + m_GYM_SEQ + ", " +
                TBL_PRC_OPE_SUM.TTL_TIME + " = " + m_TTL_TIME +
                " WHERE " +
                TBL_PRC_OPE_SUM.OP_NO + " = " + "'" + m_OP_NO + "'" + " AND " +
                TBL_PRC_OPE_SUM.GYM_ID + " = " + m_GYM_ID + " AND " +
                TBL_PRC_OPE_SUM.GYM_SEQ + " = " + m_GYM_SEQ + " AND " +
                TBL_PRC_OPE_SUM.TALLY_DATE + " = " + m_TALLY_DATE;
            return strSql;
        }

        public string GetDeleteQuery(string op_no, int gym_id, int gym_seq, int tally_date, int gym_id2)
        {
            string strSql = "DELETE FROM " + TBL_PRC_OPE_SUM.TABLE_NAME +
                " WHERE " +
                TBL_PRC_OPE_SUM.OP_NO + " = " + "'" + op_no + "'" + " AND " +
                TBL_PRC_OPE_SUM.GYM_ID + " = " + gym_id + " AND " +
                TBL_PRC_OPE_SUM.GYM_ID + " = " + gym_id2 + " AND " +
                TBL_PRC_OPE_SUM.GYM_SEQ + " = " + gym_seq + " AND " +
                TBL_PRC_OPE_SUM.TALLY_DATE + " = " + tally_date;

            return strSql;
        }

        #endregion
    }
}
