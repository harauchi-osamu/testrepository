using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace CommonTable.DB
{
    /// <summary>
    /// TBL_TRBATCH_HIST
    /// </summary>
    public class TBL_TRBATCH_HIST
    {
        public TBL_TRBATCH_HIST(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_TRBATCH_HIST(int gym_id, int operation_date,int bat_id, string scan_term, int seq, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_GYM_ID = gym_id;
            m_OPERATION_DATE = operation_date;
            m_BAT_ID = bat_id;
            m_SCAN_TERM = scan_term;
            m_SEQ = seq;
           
        }

        public TBL_TRBATCH_HIST(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "BAT_OCRDATA";

        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCAN_TERM = "SCAN_TERM";
        public const string BAT_ID = "BAT_ID";
        public const string SEQ = "SEQ";
        public const string STS = "STS";
        public const string OC_BK_NO = "OC_BK_NO";
        public const string OC_BR_NO = "OC_BR_NO";
        public const string SCAN_BR_NO = "SCAN_BR_NO";
        public const string SCAN_DATE = "SCAN_DATE";
        public const string CLEARING_DATE = "CLEARING_DATE";
        public const string SCAN_COUNT = "SCAN_COUNT";
        public const string TOTAL_COUNT = "TOTAL_COUNT";
        public const string TOTAL_AMOUNT = "TOTAL_AMOUNT";
        public const string DELETE_DATE = "DELETE_DATE";
        public const string DELETE_FLG = "DELETE_FLG";
        public const string E_TMNO = "E_TMNO";
        public const string E_OPENO = "E_OPENO";
        public const string E_YMD = "E_YMD";
        public const string E_TIME = "E_TIME";
        public const string UPDATE_DATE = "UPDATE_DATE";
        public const string UPDATE_TIME = "UPDATE_TIME";
        public const string UPDATE_KBN = "UPDATE_KBN";

        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private int m_GYM_ID = 0;
        private string m_SCAN_TERM = "";
        private int m_OPERATION_DATE = 0;
        private int m_SEQ = 0;
        private int m_BAT_ID = 0;
        public string m_STS = "";
        public int m_OC_BK_NO = 0;
        public int m_OC_BR_NO = 0;
        public int m_SCAN_BR_NO = 0;
        public int m_SCAN_DATE = 0;
        public int m_CLEARING_DATE = 0;
        public int m_SCAN_COUNT = 0;
        public int m_TOTAL_COUNT = 0;
        public int m_TOTAL_AMOUNT = 0;
        public int m_DELETE_DATE = 0;
        public string m_DELETE_FLG = "";
        public string m_E_TMNO = "";
        public string m_E_OPENO = "";
        public int m_E_YMD = 0;
        public int m_E_TIME = 0;
        public int m_UPDATE_DATE = 0;
        public int m_UPDATE_TIME = 0;
        public int m_UPDATE_KBN = 0;
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

        public string _SCAN_TERM
        {
            get { return m_SCAN_TERM; }
        }

        public int _BAT_ID
        {
            get { return m_BAT_ID; }
        }

        public int _SEQ

        {
            get { return m_SEQ; }
        }

    
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.OPERATION_DATE]);
            m_SCAN_TERM = DBConvert.ToStringNull(dr[TBL_TRBATCH_HIST.SCAN_TERM]);
            m_BAT_ID = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.BAT_ID]);
            m_SEQ = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.SEQ]);
            m_STS = DBConvert.ToStringNull(dr[TBL_TRBATCH_HIST.STS]);
            m_OC_BK_NO = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.OC_BK_NO]);
            m_OC_BR_NO = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.OC_BR_NO]);
            m_SCAN_BR_NO = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.SCAN_BR_NO]);
            m_SCAN_DATE = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.SCAN_DATE]);
            m_CLEARING_DATE = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.CLEARING_DATE]);
            m_SCAN_COUNT = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.SCAN_COUNT]);
            m_TOTAL_COUNT = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.TOTAL_COUNT]);
            m_TOTAL_COUNT = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.TOTAL_COUNT]);
            m_DELETE_DATE = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.DELETE_DATE]);
            m_DELETE_FLG = DBConvert.ToStringNull(dr[TBL_TRBATCH_HIST.DELETE_FLG]);
            m_E_TMNO = DBConvert.ToStringNull(dr[TBL_TRBATCH_HIST.E_TMNO]);
            m_E_OPENO = DBConvert.ToStringNull(dr[TBL_TRBATCH_HIST.E_OPENO]);
            m_E_YMD = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.OC_BR_NO]);
            m_E_TIME = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.SCAN_BR_NO]);
            m_UPDATE_DATE = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.SCAN_DATE]);
            m_UPDATE_TIME = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.CLEARING_DATE]);
            m_UPDATE_KBN = DBConvert.ToIntNull(dr[TBL_TRBATCH_HIST.SCAN_COUNT]);

        }

        #endregion

        #region テーブル名取得

        /// <summary>
        /// テーブル名取得
        /// 引数によりスキーマ変更
        /// </summary>
        /// <returns></returns>
        public static string TABLE_NAME(int Schemabankcd)
        {
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_TRBATCH_HIST.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キーを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetSelectQuery(int gym_id, int bat_id, string scan_term, int seq, int operation_date, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TRBATCH_HIST.TABLE_NAME(Schemabankcd) +
                  " WHERE " + TBL_TRBATCH_HIST.GYM_ID + "=" + gym_id +
                  " AND " + TBL_TRBATCH_HIST.BAT_ID + "=" + bat_id +
                  " AND " + TBL_TRBATCH_HIST.SEQ + "='" + seq + "'" +
                  " AND " + TBL_TRBATCH_HIST.SCAN_TERM + "='" + scan_term + "'" +
                  " AND " + TBL_TRBATCH_HIST.OPERATION_DATE + "=" + operation_date;

            return strSql;
        }

        /// <summary>
        /// insert文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_TRBATCH_HIST.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRBATCH_HIST.GYM_ID + "," +
                TBL_TRBATCH_HIST.OPERATION_DATE + "," +
                TBL_TRBATCH_HIST.SCAN_TERM + "," +
                TBL_TRBATCH_HIST.BAT_ID + "," +
                TBL_TRBATCH_HIST.SEQ + "," +
                TBL_TRBATCH_HIST.STS + "," +
                TBL_TRBATCH_HIST.OC_BK_NO + "," +
                TBL_TRBATCH_HIST.OC_BR_NO + "," +
                TBL_TRBATCH_HIST.SCAN_BR_NO + "," +
                TBL_TRBATCH_HIST.SCAN_DATE + "," +
                TBL_TRBATCH_HIST.CLEARING_DATE + "," +
                TBL_TRBATCH_HIST.SCAN_COUNT + "," +
                TBL_TRBATCH_HIST.TOTAL_COUNT + "," +
                TBL_TRBATCH_HIST.TOTAL_AMOUNT + "," +
                TBL_TRBATCH_HIST.DELETE_DATE + "," +
                TBL_TRBATCH_HIST.DELETE_FLG + "," +
                TBL_TRBATCH_HIST.E_TMNO + "," +
                TBL_TRBATCH_HIST.E_OPENO + "," +
                TBL_TRBATCH_HIST.E_YMD + "," +
                TBL_TRBATCH_HIST.E_TIME + "," +
                TBL_TRBATCH_HIST.UPDATE_DATE + "," +
                TBL_TRBATCH_HIST.UPDATE_TIME + "," +
                TBL_TRBATCH_HIST.UPDATE_KBN + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                m_SCAN_TERM + "," +
                m_BAT_ID + "," +
                m_SEQ + "," +
                m_STS + "," +
                "'" + m_OC_BK_NO + "'," +
                m_OC_BR_NO + "," +
                "'" + m_SCAN_BR_NO + "'," +
                "'" + m_SCAN_DATE + "'," +
                m_CLEARING_DATE + "," +
                m_SCAN_COUNT + "," +
                m_TOTAL_COUNT + "," +
                m_TOTAL_AMOUNT + "," +
                m_DELETE_DATE + "," +
                m_DELETE_FLG + "," +
                m_E_TMNO + "," +
                m_E_OPENO + "," +
                m_E_YMD + "," +
                m_E_TIME + "," +
                m_UPDATE_DATE + "," +
                m_UPDATE_TIME + "," +
                m_UPDATE_KBN + ")";

            return strSql;
        }

        /// <summary>
        /// update文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_TRBATCH_HIST.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_TRBATCH_HIST.STS + "=" + m_STS + ", " +
                TBL_TRBATCH_HIST.OC_BK_NO + "=" + m_OC_BK_NO + ", " +
                TBL_TRBATCH_HIST.OC_BR_NO + "=" + m_OC_BR_NO + ", " +
                TBL_TRBATCH_HIST.SCAN_BR_NO + "=" + m_SCAN_BR_NO + ", " +
                TBL_TRBATCH_HIST.SCAN_DATE + "=" + m_SCAN_DATE + ", " +
                TBL_TRBATCH_HIST.CLEARING_DATE + "=" + m_CLEARING_DATE + ", " +
                TBL_TRBATCH_HIST.SCAN_COUNT + "=" + m_SCAN_COUNT + ", " +
                TBL_TRBATCH_HIST.TOTAL_AMOUNT + "=" + m_SCAN_COUNT + ", " +               
                TBL_TRBATCH_HIST.DELETE_DATE + "=" + m_DELETE_DATE + ", " +
                TBL_TRBATCH_HIST.DELETE_FLG + "=" + m_DELETE_FLG + ", " +
                TBL_TRBATCH_HIST.E_TMNO + "=" + m_E_TMNO + ", " +
                TBL_TRBATCH_HIST.E_OPENO + "=" + m_E_OPENO + ", " +
                TBL_TRBATCH_HIST.E_YMD + "=" + m_E_YMD + ", " +
                TBL_TRBATCH_HIST.E_TIME + "=" + m_E_TIME + ", " +
                TBL_TRBATCH_HIST.UPDATE_DATE + "=" + m_UPDATE_DATE + ", " +
                TBL_TRBATCH_HIST.UPDATE_TIME + "=" + m_UPDATE_TIME + ", " +
                TBL_TRBATCH_HIST.UPDATE_KBN + "='" + m_UPDATE_KBN + "' " +
                " WHERE " +
                TBL_TRBATCH_HIST.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRBATCH_HIST.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRBATCH_HIST.SCAN_TERM + "=" + m_SCAN_TERM + " AND " +
                TBL_TRBATCH_HIST.SEQ + "=" + m_SEQ + " AND " +
                TBL_TRBATCH_HIST.OPERATION_DATE + "=" + m_OPERATION_DATE;

            return strSql;
        }

        /// <summary>
        /// delete文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_TRBATCH_HIST.TABLE_NAME(m_SCHEMABANKCD) + " WHERE " + TBL_TRBATCH_HIST.GYM_ID + "=" + m_GYM_ID;
            strSQL += " AND " + TBL_TRBATCH_HIST.BAT_ID + "=" + m_BAT_ID + " AND " + TBL_TRBATCH_HIST.SEQ + "=" + m_SEQ;
            strSQL += " AND " + TBL_TRBATCH_HIST.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " + TBL_TRBATCH_HIST.OPERATION_DATE + "=" + m_OPERATION_DATE;

            return strSQL;
        }

        /// <summary>
        /// delete文を作成します
        /// </summary>
        /// <returns></returns>
   

        #endregion
    }
}
