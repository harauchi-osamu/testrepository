using System;
using System.Data;
using System.Data.Common;

namespace IIPCommonClass.DB
{
	/// <summary>
	/// オペレーター
	/// </summary>
	public class TBL_PRC_OPE_RUN
	{
        public TBL_PRC_OPE_RUN()
        {
        }

        public TBL_PRC_OPE_RUN(int gym_id)
        {
            m_GYM_ID = gym_id;
        }

        public TBL_PRC_OPE_RUN(DataRow dr)
		{
			initializeByDataRow(dr);
		}


        #region テーブル定義
        public const string TABLE_NAME = TABLE_SCHEMA + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_SCHEMA = "hen";
        public const string TABLE_PHYSICAL_NAME = "PRC_OPE_RUN";

        public const string OP_NO = "OP_NO";
        public const string GYM_ID = "GYM_ID";
        public const string GYM_SEQ = "GYM_SEQ";
        public const string DATA_TIME = "DATA_TIME";

        #endregion

        #region メンバ
        public string m_OP_NO = "";
        public int m_GYM_ID = 0;
        public int m_GYM_SEQ = 0;
        public long m_DATA_TIME = 0;
       
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
            m_OP_NO = DBConvert.ToStringNull(dr[TBL_PRC_OPE_RUN.OP_NO]);
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_PRC_OPE_RUN.GYM_ID]);
            m_GYM_SEQ = DBConvert.ToIntNull(dr[TBL_PRC_OPE_RUN.GYM_SEQ]);
            m_DATA_TIME = DBConvert.ToLongNull(dr[TBL_PRC_OPE_RUN.DATA_TIME]);

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
            string strSql = "SELECT * FROM " + TBL_PRC_OPE_RUN.TABLE_NAME +
                " ORDER BY " + TBL_PRC_OPE_RUN.GYM_ID + " , " + TBL_PRC_OPE_RUN.GYM_SEQ;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <param name="op_no">オペ番号</param>
        /// <returns></returns>
        public static string GetSelectQuery(string op_no)
        {
            string strSql = "SELECT * FROM " + TBL_PRC_OPE_RUN.TABLE_NAME +
                " WHERE " + TBL_PRC_OPE_RUN.OP_NO + "='" + op_no + "'" +
                " ORDER BY " + 
                TBL_PRC_OPE_RUN.GYM_ID + " , " + 
                TBL_PRC_OPE_RUN.GYM_SEQ;
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
            string strSql = "SELECT * FROM " + TBL_PRC_OPE_RUN.TABLE_NAME +
                " WHERE " + 
                TBL_PRC_OPE_RUN.OP_NO + "='" + op_no + "'" + " AND " +
                TBL_PRC_OPE_RUN.GYM_ID + " = " + gym_id + " AND " +
                TBL_PRC_OPE_RUN.GYM_SEQ + " = " + gym_seq +
                " ORDER BY " + 
                TBL_PRC_OPE_RUN.GYM_ID + " , " + 
                TBL_PRC_OPE_RUN.GYM_SEQ;
            return strSql;
        }

        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_PRC_OPE_RUN.TABLE_NAME + " (" +
                TBL_PRC_OPE_RUN.OP_NO + ", " +
                TBL_PRC_OPE_RUN.GYM_ID + "," +
                TBL_PRC_OPE_RUN.GYM_SEQ + "," +
                TBL_PRC_OPE_RUN.DATA_TIME + ") VALUES (" +
                "'" + m_OP_NO + "'" + "," +
                m_GYM_ID + "," +
                m_GYM_SEQ + "," +
                m_DATA_TIME +
                ")";

            return strSql;
        }

        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_PRC_OPE_RUN.TABLE_NAME + " SET " +
                TBL_PRC_OPE_RUN.GYM_ID + " = " + m_GYM_ID + ", " +
                TBL_PRC_OPE_RUN.GYM_SEQ + " = " + m_GYM_SEQ + ", " +
                TBL_PRC_OPE_RUN.DATA_TIME + " = " + m_DATA_TIME +
                " WHERE " +
                TBL_PRC_OPE_RUN.OP_NO + " = " + "'" + m_OP_NO + "'" + " AND " +
                TBL_PRC_OPE_RUN.GYM_ID + " = " + m_GYM_ID + " AND " +
                TBL_PRC_OPE_RUN.GYM_SEQ + " = " + m_GYM_SEQ;
            return strSql;
        }

        public string GetDeleteQuery(int gymid)
        {
            string strSql = "DELETE FROM " + TBL_PRC_OPE_RUN.TABLE_NAME +
                " WHERE " +
                TBL_PRC_OPE_RUN.GYM_ID + " = " + gymid;

            return strSql;
        }

        #endregion
    }
}
