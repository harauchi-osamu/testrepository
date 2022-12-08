using System;
using System.Data;
using System.Data.Common;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// オペレーター
	/// </summary>
	public class TBL_PRC_SUM
	{
        public TBL_PRC_SUM()
        {
        }

        public TBL_PRC_SUM(int gym_id)
        {
            m_GYM_ID = gym_id;
        }

        public TBL_PRC_SUM(DataRow dr)
		{
			initializeByDataRow(dr);
		}


        #region テーブル定義
        public const string TABLE_NAME = TABLE_SCHEMA + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_SCHEMA = "hen";
        public const string TABLE_PHYSICAL_NAME = "PRC_SUM";

        public const string GYM_ID = "GYM_ID";
        public const string GYM_SEQ = "GYM_SEQ";
        public const string DATA_TIME = "DATA_TIME";
        public const string ACCEPT_CNT = "ACCEPT_CNT";
        public const string END_CNT = "END_CNT";
        public const string RUN_CNT = "RUN_CNT";
        public const string STAGE1_END = "STAGE1_END";
        public const string STAGE1_RUN = "STAGE1_RUN";
        public const string STAGE2_END = "STAGE2_END";
        public const string STAGE2_RUN = "STAGE2_RUN";
        public const string STAGE3_END = "STAGE3_END";
        public const string STAGE3_RUN = "STAGE3_RUN";
        public const string STAGE4_END = "STAGE4_END";
        public const string STAGE4_RUN = "STAGE4_RUN";
        public const string STAGE5_END = "STAGE5_END";
        public const string STAGE5_RUN = "STAGE5_RUN";
        public const string HEAD_CNT = "HEAD_CNT";
        public const string LIMIT_TIME = "LIMIT_TIME";
        public const string END_TIME = "END_TIME";
        public const string BESTHEAD_CNT = "BESTHEAD_CNT";
    
        #endregion

        #region メンバ
        public int m_GYM_ID = 0;
        public int m_GYM_SEQ = 0;
        public long m_DATA_TIME = 0;
        public int m_ACCEPT_CNT = 0;
        public int m_END_CNT = 0;
        public int m_RUN_CNT = 0;
        public int m_STAGE1_END = 0;
        public int m_STAGE1_RUN = 0;
        public int m_STAGE2_END = 0;
        public int m_STAGE2_RUN = 0;
        public int m_STAGE3_END = 0;
        public int m_STAGE3_RUN = 0;
        public int m_STAGE4_END = 0;
        public int m_STAGE4_RUN = 0;
        public int m_STAGE5_END = 0;
        public int m_STAGE5_RUN = 0;
        public int m_HEAD_CNT = 0;
        public int m_LIMIT_TIME = 0;
        public int m_END_TIME = 0;
        public int m_BESTHEAD_CNT = 0;



        #endregion

        #region プロパティ

        public int _GYM_ID
        {
            get { return m_GYM_ID; }
        }

        public int _GYM_SEQ
        {
            get { return m_GYM_SEQ; }
        }

        #endregion

        #region 初期化

        /// <summary>
		/// DataRowの値をセットする
		/// </summary>
		/// <param name="dr"></param>
		protected void initializeByDataRow(DataRow dr)
		{
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_PRC_SUM.GYM_ID]);
            m_GYM_SEQ = DBConvert.ToIntNull(dr[TBL_PRC_SUM.GYM_SEQ]);
            m_DATA_TIME = DBConvert.ToLongNull(dr[TBL_PRC_SUM.DATA_TIME]);
            m_ACCEPT_CNT = DBConvert.ToIntNull(dr[TBL_PRC_SUM.ACCEPT_CNT]);
            m_END_CNT = DBConvert.ToIntNull(dr[TBL_PRC_SUM.END_CNT]);
            m_RUN_CNT = DBConvert.ToIntNull(dr[TBL_PRC_SUM.RUN_CNT]);
            m_STAGE1_END = DBConvert.ToIntNull(dr[TBL_PRC_SUM.STAGE1_END]);
            m_STAGE1_RUN = DBConvert.ToIntNull(dr[TBL_PRC_SUM.STAGE1_RUN]);
            m_STAGE2_END = DBConvert.ToIntNull(dr[TBL_PRC_SUM.STAGE2_END]);
            m_STAGE2_RUN = DBConvert.ToIntNull(dr[TBL_PRC_SUM.STAGE2_RUN]);
            m_STAGE3_END = DBConvert.ToIntNull(dr[TBL_PRC_SUM.STAGE3_END]);
            m_STAGE3_RUN = DBConvert.ToIntNull(dr[TBL_PRC_SUM.STAGE3_RUN]);
            m_STAGE4_END = DBConvert.ToIntNull(dr[TBL_PRC_SUM.STAGE4_END]);
            m_STAGE4_RUN = DBConvert.ToIntNull(dr[TBL_PRC_SUM.STAGE4_RUN]);
            m_STAGE5_END = DBConvert.ToIntNull(dr[TBL_PRC_SUM.STAGE5_END]);
            m_STAGE5_RUN = DBConvert.ToIntNull(dr[TBL_PRC_SUM.STAGE5_RUN]);
            m_HEAD_CNT = DBConvert.ToIntNull(dr[TBL_PRC_SUM.HEAD_CNT]);
            m_LIMIT_TIME = DBConvert.ToIntNull(dr[TBL_PRC_SUM.LIMIT_TIME]);
            m_END_TIME = DBConvert.ToIntNull(dr[TBL_PRC_SUM.END_TIME]);
            m_BESTHEAD_CNT = DBConvert.ToIntNull(dr[TBL_PRC_SUM.BESTHEAD_CNT]);

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
            string strSql = "SELECT * FROM " + TBL_PRC_SUM.TABLE_NAME +
                " ORDER BY " + TBL_PRC_SUM.GYM_ID;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid)
        {
            string strSql = "SELECT * FROM " + TBL_PRC_SUM.TABLE_NAME +
                " WHERE " + TBL_PRC_SUM.GYM_ID + "=" + gymid +
                " ORDER BY " + TBL_PRC_SUM.GYM_SEQ;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <param name="gymseq">連番</param>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int gymseq)
        {
            string strSql = "SELECT * FROM " + TBL_PRC_SUM.TABLE_NAME +
                " WHERE " + 
                TBL_PRC_SUM.GYM_ID + " = " + gymid + " AND " +
                TBL_PRC_SUM.GYM_SEQ + " = " + gymseq +
                " ORDER BY " + TBL_PRC_SUM.GYM_SEQ;
            return strSql;
        }

        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_PRC_SUM.TABLE_NAME + " (" +
                TBL_PRC_SUM.GYM_ID + "," +
                TBL_PRC_SUM.GYM_SEQ + "," +
                TBL_PRC_SUM.DATA_TIME + "," +
                TBL_PRC_SUM.ACCEPT_CNT + "," +
                TBL_PRC_SUM.END_CNT + "," +
                TBL_PRC_SUM.RUN_CNT + "," +
                TBL_PRC_SUM.STAGE1_END + "," +
                TBL_PRC_SUM.STAGE1_RUN + "," +
                TBL_PRC_SUM.STAGE2_END + "," +
                TBL_PRC_SUM.STAGE2_RUN + "," +
                TBL_PRC_SUM.STAGE3_END + "," +
                TBL_PRC_SUM.STAGE3_RUN + "," +
                TBL_PRC_SUM.STAGE4_END + "," +
                TBL_PRC_SUM.STAGE4_RUN + "," +
                TBL_PRC_SUM.STAGE5_END + "," +
                TBL_PRC_SUM.STAGE5_RUN + "," +
                TBL_PRC_SUM.HEAD_CNT + "," +
                TBL_PRC_SUM.LIMIT_TIME + "," +
                TBL_PRC_SUM.END_TIME + "," +
                TBL_PRC_SUM.BESTHEAD_CNT + ") VALUES (" +
                m_GYM_ID + "," +
                m_GYM_SEQ + "," +
                m_DATA_TIME + "," +
                m_ACCEPT_CNT + "," +
                m_END_CNT + "," +
                m_RUN_CNT + "," +
                m_STAGE1_END + "," +
                m_STAGE1_RUN + "," +
                m_STAGE2_END + "," +
                m_STAGE2_RUN + "," +
                m_STAGE3_END + "," +
                m_STAGE3_RUN + "," +
                m_STAGE4_END + "," +
                m_STAGE4_RUN + "," +
                m_STAGE5_END + "," +
                m_STAGE5_RUN + "," +
                m_HEAD_CNT + "," +
                m_LIMIT_TIME + "," +
                m_END_TIME + ", " +
                m_BESTHEAD_CNT + 
                ")";

            return strSql;
        }

        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_PRC_SUM.TABLE_NAME + " SET " +
                TBL_PRC_SUM.DATA_TIME + "=" + m_DATA_TIME + ", " +
                TBL_PRC_SUM.ACCEPT_CNT + "=" + m_ACCEPT_CNT + ", " +
                TBL_PRC_SUM.END_CNT + "=" + m_END_CNT + ", " +
                TBL_PRC_SUM.RUN_CNT + "=" + m_RUN_CNT + ", " +
                TBL_PRC_SUM.STAGE1_END + "=" + m_STAGE1_END + ", " +
                TBL_PRC_SUM.STAGE1_RUN + "=" + m_STAGE1_RUN + ", " +
                TBL_PRC_SUM.STAGE2_END + "=" + m_STAGE2_END + ", " +
                TBL_PRC_SUM.STAGE2_RUN + "=" + m_STAGE2_RUN + ", " +
                TBL_PRC_SUM.STAGE3_END + "=" + m_STAGE3_END + ", " +
                TBL_PRC_SUM.STAGE3_RUN + "=" + m_STAGE3_RUN + ", " +
                TBL_PRC_SUM.STAGE4_END + "=" + m_STAGE4_END + ", " +
                TBL_PRC_SUM.STAGE4_RUN + "=" + m_STAGE4_RUN + ", " +
                TBL_PRC_SUM.STAGE5_END + "=" + m_STAGE5_END + ", " +
                TBL_PRC_SUM.STAGE5_RUN + "=" + m_STAGE5_RUN + ", " +
                TBL_PRC_SUM.HEAD_CNT + "=" + m_HEAD_CNT + ", " +
                TBL_PRC_SUM.LIMIT_TIME + "=" + m_LIMIT_TIME + ", " +
                TBL_PRC_SUM.END_TIME + "=" + m_END_TIME + ", " +
                TBL_PRC_SUM.BESTHEAD_CNT + "=" + m_BESTHEAD_CNT + 
                " WHERE " +
                TBL_PRC_SUM.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_PRC_SUM.GYM_SEQ + "=" + m_GYM_SEQ;

            return strSql;
        }

        public string GetDeleteQuery(int gymid, int gymseq)
        {
            string strSql = "DELETE FROM " + TBL_PRC_SUM.TABLE_NAME +
                " WHERE " +
                TBL_PRC_SUM.GYM_ID + "=" + gymid + " AND " +
                TBL_PRC_SUM.GYM_SEQ + "=" + gymseq;

            return strSql;
        }

        public string GetDeleteQuery(int gymid, long datatime)
        {
            string strSql = "DELETE FROM " + TBL_PRC_SUM.TABLE_NAME +
                " WHERE " +
                TBL_PRC_SUM.GYM_ID + "=" + gymid + " AND " +
                TBL_PRC_SUM.DATA_TIME + " < " + datatime;

            return strSql;
        }

        public string GetDeleteQuery(int gymid, int gymseq, long datatime)
        {
            string strSql = "DELETE FROM " + TBL_PRC_SUM.TABLE_NAME +
                " WHERE " +
                TBL_PRC_SUM.GYM_ID + "=" + gymid + " AND " +
                TBL_PRC_SUM.GYM_SEQ + "=" + gymseq + " AND " +
                TBL_PRC_SUM.DATA_TIME + " < " + datatime;

            return strSql;
        }

        #endregion
    }
}
