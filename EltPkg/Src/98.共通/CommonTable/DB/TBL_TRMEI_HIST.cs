using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace CommonTable.DB
{
    /// <summary>
    /// 項目
    /// </summary>
    public class TBL_TRMEI_HIST
    {
        public TBL_TRMEI_HIST(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="batid"></param>
        /// <param name="detailsno"></param>
        /// <param name="IC_OC_BK_NO"></param>
        /// <param name="SCAN_TERM"></param>
        /// <param name="operationdate"></param>
        /// <param name="imageno"></param>
        public TBL_TRMEI_HIST(int gymid, int operationdate, string scannerterm, int batid, int detailsno, int seq, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_GYM_ID = gymid;
            m_OPERATION_DATE = operationdate;
            m_SCAN_TERM = scannerterm;
            m_BAT_ID = batid;
            m_DETAILS_NO = detailsno;
            m_SEQ = seq;
        }

        public TBL_TRMEI_HIST(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "TRMEI_HIST";

        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCAN_TERM = "SCAN_TERM";
        public const string BAT_ID = "BAT_ID";
        public const string DETAILS_NO = "DETAILS_NO";
        public const string SEQ = "SEQ";
        public const string IC_OC_BK_NO = "IC_OC_BK_NO";
        public const string IC_OLD_OC_BK_NO = "IC_OLD_OC_BK_NO";
        public const string BUA_MARK = "BUA_MARK";
        public const string BUB_MARK = "BUB_MARK";
        public const string BCA_MARK = "BCA_MARK";
        public const string GMA_MARK = "GMA_MARK";
        public const string GMB_MARK = "GMB_MARK";
        public const string GRA_MARK = "GRA_MARK";
        public const string GXA_MARK = "GXA_MARK";
        public const string GXB_MARK = "GXB_MARK";
        public const string MRA_MARK = "MRA_MARK";
        public const string MRB_MARK = "MRB_MARK";
        public const string MRC_MARK = "MRC_MARK";
        public const string MRD_MARK = "MRD_MARK";
        public const string YCA_MARK = "YCA_MARK";
        public const string UPDATE_DATE = "UPDATE_DATE";
        public const string UPDATE_TIME = "UPDATE_TIME";
        public const string UPDATE_KBN = "UPDATE_KBN";
        public const string DELETE_DATE = "DELETE_DATE";
        public const string DELETE_FLG = "DELETE_FLG";

        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        private string m_SCAN_TERM = "";
        private int m_BAT_ID = 0;
        private int m_DETAILS_NO = 0;
        private int m_SEQ = 0;
        public string m_IC_OC_BK_NO = "";
        public string m_IC_OLD_OC_BK_NO = "";
        public string m_BUA_MARK = "";
        public string m_BUB_MARK = "";
        public string m_BCA_MARK = "";
        public string m_GMA_MARK = "";
        public string m_GMB_MARK = "";
        public string m_GRA_MARK = "";
        public string m_GXA_MARK = "";
        public string m_GXB_MARK = "";
        public string m_MRA_MARK = "";
        public string m_MRB_MARK = "";
        public int m_MRC_MARK = 0;
        public int m_MRD_MARK = 0;
        public int m_YCA_MARK = 0;
        public int m_UPDATE_DATE = 0;
        public int m_UPDATE_TIME = 0;
        public int m_UPDATE_KBN = 0;
        public int m_DELETE_DATE = 0;
        public string m_DELETE_FLG = "";

        #endregion

        #region プロパティ

        public int _GYM_ID
        {
            get { return m_GYM_ID; }
        }

        public int _BAT_ID
        {
            get { return m_BAT_ID; }
        }

        public int _DETAILS_NO
        {
            get { return m_DETAILS_NO; }
        }
        public string _SCAN_TERM
        {
            get { return m_SCAN_TERM; }
        }

        public int _OPERATION_DATE
        {
            get { return m_OPERATION_DATE; }
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
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_TRMEI_HIST.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI_HIST.OPERATION_DATE]);
            m_SCAN_TERM = DBConvert.ToStringNull(dr[TBL_TRMEI_HIST.SCAN_TERM]);
            m_BAT_ID = DBConvert.ToIntNull(dr[TBL_TRMEI_HIST.BAT_ID]);
            m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_TRMEI_HIST.DETAILS_NO]);
            m_SEQ = DBConvert.ToIntNull(dr[TBL_TRMEI_HIST.SEQ]);
            m_IC_OC_BK_NO = DBConvert.ToStringNull(dr[TBL_TRMEI_HIST.IC_OC_BK_NO]);
            m_IC_OLD_OC_BK_NO = DBConvert.ToStringNull(dr[TBL_TRMEI_HIST.IC_OLD_OC_BK_NO]);
            m_BUA_MARK = DBConvert.ToStringNull(dr[TBL_TRMEI_HIST.BUA_MARK]);
            m_BUB_MARK = DBConvert.ToStringNull(dr[TBL_TRMEI_HIST.BUB_MARK]);
            m_BCA_MARK = DBConvert.ToStringNull(dr[TBL_TRMEI_HIST.BCA_MARK]);
            m_GMA_MARK = DBConvert.ToStringNull(dr[TBL_TRMEI_HIST.GMA_MARK]);
            m_GMB_MARK = DBConvert.ToStringNull(dr[TBL_TRMEI_HIST.GMB_MARK]);
            m_GRA_MARK = DBConvert.ToStringNull(dr[TBL_TRMEI_HIST.GRA_MARK]);
            m_GXA_MARK = DBConvert.ToStringNull(dr[TBL_TRMEI_HIST.GXA_MARK]);
            m_GXB_MARK = DBConvert.ToStringNull(dr[TBL_TRMEI_HIST.GXB_MARK]);
            m_MRA_MARK = DBConvert.ToStringNull(dr[TBL_TRMEI_HIST.MRA_MARK]);
            m_MRB_MARK = DBConvert.ToStringNull(dr[TBL_TRMEI_HIST.MRB_MARK]);
            m_MRC_MARK = DBConvert.ToIntNull(dr[TBL_TRMEI_HIST.MRC_MARK]);
            m_MRD_MARK = DBConvert.ToIntNull(dr[TBL_TRMEI_HIST.MRD_MARK]);
            m_YCA_MARK = DBConvert.ToIntNull(dr[TBL_TRMEI_HIST.YCA_MARK]);
            m_UPDATE_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI_HIST.UPDATE_DATE]);
            m_UPDATE_TIME = DBConvert.ToIntNull(dr[TBL_TRMEI_HIST.UPDATE_TIME]);
            m_UPDATE_KBN = DBConvert.ToIntNull(dr[TBL_TRMEI_HIST.UPDATE_KBN]);
            m_DELETE_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI_HIST.DELETE_DATE]);
            m_DELETE_FLG = DBConvert.ToStringNull(dr[TBL_TRMEI_HIST.DELETE_FLG]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_TRMEI_HIST.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キーを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gym_id, int bat_id, string scan_term, int operation_date, int details_no, int seq, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TRMEI_HIST.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEI_HIST.GYM_ID + "=" + gym_id + " AND " +
                TBL_TRMEI_HIST.OPERATION_DATE + "=" + operation_date + " AND " +
                TBL_TRMEI_HIST.SCAN_TERM + "='" + scan_term + "' AND " +
                TBL_TRMEI_HIST.DETAILS_NO + "='" + details_no + "' AND " +
                TBL_TRMEI_HIST.SEQ + "='" + seq + "' AND " +
                TBL_TRMEI_HIST.BAT_ID + "=" + bat_id +
                " ORDER BY " +
                TBL_TRMEI_HIST.GYM_ID + ", " +
                TBL_TRMEI_HIST.OPERATION_DATE + ", " +
                TBL_TRMEI_HIST.SCAN_TERM + ", " +
                TBL_TRMEI_HIST.BAT_ID + ", " +
                TBL_TRMEI_HIST.SEQ + ", " +
                TBL_TRMEI_HIST.DETAILS_NO;
            return strSql;
        }

        /// <summary>
        /// キーを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gym_id, int bat_id, string scan_term, int operation_date, int seq, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TRMEI_HIST.TABLE_NAME(Schemabankcd) +
                " WHERE " + TBL_TRMEI_HIST.GYM_ID + "=" + gym_id +
                " AND " + TBL_TRMEI_HIST.OPERATION_DATE + "=" + operation_date +
                " AND " + TBL_TRMEI_HIST.SCAN_TERM + "='" + scan_term + "'" +
                " AND " + TBL_TRMEI_HIST.BAT_ID + "=" + bat_id +
                " AND " + TBL_TRMEI_HIST.SEQ + "=" + seq +
                " ORDER BY " + TBL_TRMEI_HIST.GYM_ID;
            return strSql;
        }

        /// <summary>
        /// キーを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gym_id, int bat_id, int details_no, string scan_term, int operation_date, int seq, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TRMEI_HIST.TABLE_NAME(Schemabankcd) +
                " WHERE " + TBL_TRMEI_HIST.GYM_ID + "=" + gym_id +
                " AND " + TBL_TRMEI_HIST.OPERATION_DATE + "=" + operation_date +
                " AND " + TBL_TRMEI_HIST.SCAN_TERM + "='" + scan_term + "'" +
                " AND " + TBL_TRMEI_HIST.BAT_ID + "=" + bat_id +
                " AND " + TBL_TRMEI_HIST.SEQ + "=" + seq +
                " AND " + TBL_TRMEI_HIST.DETAILS_NO + "=" + details_no;
            return strSql;
        }

        /// <summary>
        /// insert文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_TRMEI_HIST.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRMEI_HIST.GYM_ID + "," +
                TBL_TRMEI_HIST.OPERATION_DATE + "," +
                TBL_TRMEI_HIST.SCAN_TERM + "," +
                TBL_TRMEI_HIST.BAT_ID + "," +
                TBL_TRMEI_HIST.DETAILS_NO + "," +
                TBL_TRMEI_HIST.SEQ + "," +
                TBL_TRMEI_HIST.IC_OC_BK_NO + "," +
                TBL_TRMEI_HIST.IC_OLD_OC_BK_NO + "," +
                TBL_TRMEI_HIST.BUA_MARK + "," +
                TBL_TRMEI_HIST.BUB_MARK + "," +
                TBL_TRMEI_HIST.BCA_MARK + "," +
                TBL_TRMEI_HIST.GMA_MARK + "," +
                TBL_TRMEI_HIST.GMB_MARK + "," +
                TBL_TRMEI_HIST.GRA_MARK + "," +
                TBL_TRMEI_HIST.GXA_MARK + "," +
                TBL_TRMEI_HIST.GXB_MARK + "," +
                TBL_TRMEI_HIST.MRA_MARK + "," +
                TBL_TRMEI_HIST.MRB_MARK + "," +
                TBL_TRMEI_HIST.MRC_MARK + "," +
                TBL_TRMEI_HIST.MRD_MARK + "," +
                TBL_TRMEI_HIST.YCA_MARK + "," +
                TBL_TRMEI_HIST.UPDATE_DATE + "," +
                TBL_TRMEI_HIST.UPDATE_TIME + "," +
                TBL_TRMEI_HIST.UPDATE_KBN + "," +
                TBL_TRMEI_HIST.DELETE_DATE + "," +
                TBL_TRMEI_HIST.DELETE_FLG + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_SEQ + "," +
                m_IC_OC_BK_NO + "," +
                "'" + m_IC_OLD_OC_BK_NO + "'," +
                "'" + m_BUA_MARK + "'," +
                "'" + m_BUB_MARK + "'," +
                "'" + m_BCA_MARK + "'," +
                "'" + m_GMA_MARK + "'," +
                "'" + m_GMB_MARK + "'," +
                "'" + m_GRA_MARK + "'," +
                "'" + m_GXA_MARK + "'," +
                "'" + m_GXB_MARK + "'," +
                "'" + m_MRA_MARK + "'," +
                "'" + m_MRB_MARK + "'," +
                m_MRC_MARK + "," +
                m_MRD_MARK + "," +
                m_YCA_MARK + "," +
                m_UPDATE_DATE + "," +
                m_UPDATE_TIME + "," +
                m_UPDATE_KBN + "," +
                m_DELETE_DATE + "," +
                m_DELETE_FLG + ")";
            return strSql;
        }

        /// <summary>
        /// update文を作成します（エントリ用）（★GMB_MARKは更新不可）
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQueryEnt()
        {
            string strSql = "UPDATE " + TBL_TRMEI_HIST.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_TRMEI_HIST.IC_OC_BK_NO + "=" + m_IC_OC_BK_NO + "', " +
                TBL_TRMEI_HIST.IC_OLD_OC_BK_NO + "='" + m_IC_OLD_OC_BK_NO + "', " +
                TBL_TRMEI_HIST.BUA_MARK + "='" + m_BUA_MARK + "', " +
                TBL_TRMEI_HIST.BUB_MARK + "='" + m_BUB_MARK + "', " +
                TBL_TRMEI_HIST.BCA_MARK + "='" + m_BCA_MARK + "', " +
                TBL_TRMEI_HIST.GMA_MARK + "='" + m_GMA_MARK + "', " +
                //  TBL_TRMEI_HIST.GMB_MARK + "='" + m_GMB_MARK + "', " +
                TBL_TRMEI_HIST.GRA_MARK + "='" + m_GRA_MARK + "', " +
                TBL_TRMEI_HIST.GXA_MARK + "='" + m_GXA_MARK + "', " +
                TBL_TRMEI_HIST.GXB_MARK + "='" + m_GXB_MARK + "', " +
                TBL_TRMEI_HIST.MRA_MARK + "='" + m_MRA_MARK + "', " +
                TBL_TRMEI_HIST.MRB_MARK + "='" + m_MRB_MARK + "', " +
                TBL_TRMEI_HIST.MRC_MARK + "=" + m_MRC_MARK + ", " +
                TBL_TRMEI_HIST.MRD_MARK + "=" + m_MRD_MARK + ", " +
                TBL_TRMEI_HIST.YCA_MARK + "=" + m_YCA_MARK + ", " +
                TBL_TRMEI_HIST.UPDATE_DATE + "=" + m_UPDATE_DATE +
                TBL_TRMEI_HIST.UPDATE_TIME + "=" + m_UPDATE_TIME + ", " +
                TBL_TRMEI_HIST.UPDATE_KBN + "=" + m_UPDATE_KBN + ", " +
                TBL_TRMEI_HIST.DELETE_DATE + "=" + m_DELETE_DATE + ", " +
                TBL_TRMEI_HIST.DELETE_FLG + "=" + m_DELETE_FLG +
                " WHERE " +
                TBL_TRMEI_HIST.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRMEI_HIST.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRMEI_HIST.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRMEI_HIST.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRMEI_HIST.SEQ + "=" + m_SEQ + " AND " +
                TBL_TRMEI_HIST.DETAILS_NO + "=" + m_DETAILS_NO;
            return strSql;
        }

        /// <summary>
        /// update文を作成します（ベリファイ用）（★GMB_MARKは更新不可）
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQueryVfy()
        {
            string strSql = "UPDATE " + TBL_TRMEI_HIST.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_TRMEI_HIST.IC_OLD_OC_BK_NO + "='" + m_IC_OLD_OC_BK_NO + "', " +
                //TBL_TRMEI_HIST.BUA_MARK + "='" + m_BUA_MARK + "', " +
                //TBL_TRMEI_HIST.BUB_MARK + "='" + m_BUB_MARK + "', " +
                TBL_TRMEI_HIST.BCA_MARK + "='" + m_BCA_MARK + "', " +
                TBL_TRMEI_HIST.GMA_MARK + "='" + m_GMA_MARK + "', " +
                //TBL_TRMEI_HIST.GMB_MARK + "='" + m_GMB_MARK + "', " +
                //TBL_TRMEI_HIST.GRA_MARK + "='" + m_GRA_MARK + "', " +
                TBL_TRMEI_HIST.GXA_MARK + "='" + m_GXA_MARK + "', " +
                TBL_TRMEI_HIST.GXB_MARK + "='" + m_GXB_MARK + "', " +
                TBL_TRMEI_HIST.MRA_MARK + "='" + m_MRA_MARK + "', " +
                TBL_TRMEI_HIST.MRB_MARK + "='" + m_MRB_MARK + "', " +
                TBL_TRMEI_HIST.MRC_MARK + "=" + m_MRC_MARK + ", " +
                TBL_TRMEI_HIST.MRD_MARK + "=" + m_MRD_MARK + ", " +
                TBL_TRMEI_HIST.YCA_MARK + "=" + m_YCA_MARK + ", " +
                TBL_TRMEI_HIST.UPDATE_DATE + "=" + m_UPDATE_DATE + ", " +
                TBL_TRMEI_HIST.UPDATE_TIME + "=" + m_UPDATE_TIME + ", " +
                TBL_TRMEI_HIST.UPDATE_KBN + "=" + m_UPDATE_KBN + ", " +
                TBL_TRMEI_HIST.DELETE_DATE + "=" + m_DELETE_DATE + ", " +
                TBL_TRMEI_HIST.DELETE_FLG + "=" + m_DELETE_FLG +
                " WHERE " +
                TBL_TRMEI_HIST.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRMEI_HIST.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRMEI_HIST.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRMEI_HIST.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRMEI_HIST.SEQ + "=" + m_SEQ + " AND " +
                TBL_TRMEI_HIST.DETAILS_NO + "=" + m_DETAILS_NO;
            return strSql;
        }

        /// <summary>
        /// delete文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_TRMEI_HIST.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_TRMEI_HIST.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRMEI_HIST.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRMEI_HIST.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRMEI_HIST.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRMEI_HIST.SEQ + "=" + m_SEQ + " AND " +
                TBL_TRMEI_HIST.DETAILS_NO + "=" + m_DETAILS_NO;
            return strSql;
        }

        /// <summary>
        /// delete文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQuery(int gymid, int operationdate, string scannerid, long batid, int detailsno,int seq, int Schemabankcd)
        {
            string strSql = "DELETE FROM " + TBL_TRMEI_HIST.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEI_HIST.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEI_HIST.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEI_HIST.SCAN_TERM + "='" + scannerid + "' AND " +
                TBL_TRMEI_HIST.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEI_HIST.SEQ + "=" + seq + " AND " +
                TBL_TRMEI_HIST.DETAILS_NO + "=" + detailsno;
            return strSql;
        }

        /// <summary>
        /// 明細単位でのdelete文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryDetails(int gymid, int operationdate, string scannerid, long batid, int detailsno, int Schemabankcd)
        {
            string strSql = "DELETE FROM " + TBL_TRMEI_HIST.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEI_HIST.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEI_HIST.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEI_HIST.SCAN_TERM + "='" + scannerid + "' AND " +
                TBL_TRMEI_HIST.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEI_HIST.DETAILS_NO + "=" + detailsno;
            return strSql;
        }

        #endregion
    }
}
