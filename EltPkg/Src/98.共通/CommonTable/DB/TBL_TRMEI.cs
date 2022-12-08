using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 明細トランザクション
    /// </summary>
    public class TBL_TRMEI
    {
        public TBL_TRMEI(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_TRMEI(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_GYM_ID = gymid;
            m_OPERATION_DATE = operationdate;
            m_SCAN_TERM = scanterm;
            m_BAT_ID = batid;
            m_DETAILS_NO = detailsno;
        }

        public TBL_TRMEI(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "TRMEI";

        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCAN_TERM = "SCAN_TERM";
        public const string BAT_ID = "BAT_ID";
        public const string DETAILS_NO = "DETAILS_NO";
        public const string DSP_ID = "DSP_ID";
        public const string IC_OC_BK_NO = "IC_OC_BK_NO";
        public const string IC_OLD_OC_BK_NO = "IC_OLD_OC_BK_NO";
        public const string BUA_DATE = "BUA_DATE";
        public const string BUB_DATE = "BUB_DATE";
        public const string BCA_DATE = "BCA_DATE";
        public const string GMA_DATE = "GMA_DATE";
        public const string GMB_DATE = "GMB_DATE";
        public const string GRA_DATE = "GRA_DATE";
        public const string GXA_DATE = "GXA_DATE";
        public const string GXB_DATE = "GXB_DATE";
        public const string MRA_DATE = "MRA_DATE";
        public const string MRB_DATE = "MRB_DATE";
        public const string MRC_DATE = "MRC_DATE";
        public const string MRD_DATE = "MRD_DATE";
        public const string YCA_MARK = "YCA_MARK";
        public const string EDIT_FLG = "EDIT_FLG";
        public const string BCA_STS = "BCA_STS";
        public const string GMA_STS = "GMA_STS";
        public const string GRA_STS = "GRA_STS";
        public const string GRA_CONFIRMDATE = "GRA_CONFIRMDATE";
        public const string MEMO = "MEMO";
        public const string DELETE_DATE = "DELETE_DATE";
        public const string DELETE_FLG = "DELETE_FLG";

        public const string ALL_COLUMNS = " GYM_ID,OPERATION_DATE,SCAN_TERM,BAT_ID,DETAILS_NO,DSP_ID,IC_OC_BK_NO,IC_OLD_OC_BK_NO,BUA_DATE,BUB_DATE,BCA_DATE,GMA_DATE,GMB_DATE,GRA_DATE,GXA_DATE,GXB_DATE,MRA_DATE,MRB_DATE,MRC_DATE,MRD_DATE,YCA_MARK,EDIT_FLG,BCA_STS,GMA_STS,GRA_STS,GRA_CONFIRMDATE,MEMO,DELETE_DATE,DELETE_FLG ";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        private string m_SCAN_TERM = "";
        private int m_BAT_ID = 0;
        private int m_DETAILS_NO = 0;
        public int m_DSP_ID = 999;
        public int m_IC_OC_BK_NO = -1;
        public int m_IC_OLD_OC_BK_NO = -1;
        public int m_BUA_DATE = 0;
        public int m_BUB_DATE = 0;
        public int m_BCA_DATE = 0;
        public int m_GMA_DATE = 0;
        public int m_GMB_DATE = 0;
        public int m_GRA_DATE = 0;
        public int m_GXA_DATE = 0;
        public int m_GXB_DATE = 0;
        public int m_MRA_DATE = 0;
        public int m_MRB_DATE = 0;
        public int m_MRC_DATE = 0;
        public int m_MRD_DATE = 0;
        public int m_YCA_MARK = 0;
        public int m_EDIT_FLG = 0;
        public int m_BCA_STS = 0;
        public int m_GMA_STS = 0;
        public int m_GRA_STS = 0;
        public int m_GRA_CONFIRMDATE = 0;
        public string m_MEMO = "";
        public int m_DELETE_DATE = 0;
        public int m_DELETE_FLG = 0;

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
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_TRMEI.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI.OPERATION_DATE]);
            m_SCAN_TERM = DBConvert.ToStringNull(dr[TBL_TRMEI.SCAN_TERM]);
            m_BAT_ID = DBConvert.ToIntNull(dr[TBL_TRMEI.BAT_ID]);
            m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_TRMEI.DETAILS_NO]);
            m_DSP_ID = DBConvert.ToIntNull(dr[TBL_TRMEI.DSP_ID]);
            m_IC_OC_BK_NO = DBConvert.ToIntNull(dr[TBL_TRMEI.IC_OC_BK_NO]);
            m_IC_OLD_OC_BK_NO = DBConvert.ToIntNull(dr[TBL_TRMEI.IC_OLD_OC_BK_NO]);
            m_BUA_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI.BUA_DATE]);
            m_BUB_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI.BUB_DATE]);
            m_BCA_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI.BCA_DATE]);
            m_GMA_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI.GMA_DATE]);
            m_GMB_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI.GMB_DATE]);
            m_GRA_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI.GRA_DATE]);
            m_GXA_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI.GXA_DATE]);
            m_GXB_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI.GXB_DATE]);
            m_MRA_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI.MRA_DATE]);
            m_MRB_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI.MRB_DATE]);
            m_MRC_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI.MRC_DATE]);
            m_MRD_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI.MRD_DATE]);
            m_YCA_MARK = DBConvert.ToIntNull(dr[TBL_TRMEI.YCA_MARK]);
            m_EDIT_FLG = DBConvert.ToIntNull(dr[TBL_TRMEI.EDIT_FLG]);
            m_BCA_STS = DBConvert.ToIntNull(dr[TBL_TRMEI.BCA_STS]);
            m_GMA_STS = DBConvert.ToIntNull(dr[TBL_TRMEI.GMA_STS]);
            m_GRA_STS = DBConvert.ToIntNull(dr[TBL_TRMEI.GRA_STS]);
            m_GRA_CONFIRMDATE = DBConvert.ToIntNull(dr[TBL_TRMEI.GRA_CONFIRMDATE]);
            m_MEMO = DBConvert.ToStringNull(dr[TBL_TRMEI.MEMO]);
            m_DELETE_DATE = DBConvert.ToIntNull(dr[TBL_TRMEI.DELETE_DATE]);
            m_DELETE_FLG = DBConvert.ToIntNull(dr[TBL_TRMEI.DELETE_FLG]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_TRMEI.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_TRMEI.GYM_ID + "," +
                TBL_TRMEI.OPERATION_DATE + "," +
                TBL_TRMEI.SCAN_TERM + "," +
                TBL_TRMEI.BAT_ID + "," +
                TBL_TRMEI.DETAILS_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEI.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEI.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEI.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEI.BAT_ID + "=" + batid + " " +
                " ORDER BY " +
                TBL_TRMEI.DETAILS_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEI.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEI.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEI.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEI.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEI.DETAILS_NO + "=" + detailsno;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_TRMEI.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRMEI.GYM_ID + "," +
                TBL_TRMEI.OPERATION_DATE + "," +
                TBL_TRMEI.SCAN_TERM + "," +
                TBL_TRMEI.BAT_ID + "," +
                TBL_TRMEI.DETAILS_NO + "," +
                TBL_TRMEI.DSP_ID + "," +
                TBL_TRMEI.IC_OC_BK_NO + "," +
                TBL_TRMEI.IC_OLD_OC_BK_NO + "," +
                TBL_TRMEI.BUA_DATE + "," +
                TBL_TRMEI.BUB_DATE + "," +
                TBL_TRMEI.BCA_DATE + "," +
                TBL_TRMEI.GMA_DATE + "," +
                TBL_TRMEI.GMB_DATE + "," +
                TBL_TRMEI.GRA_DATE + "," +
                TBL_TRMEI.GXA_DATE + "," +
                TBL_TRMEI.GXB_DATE + "," +
                TBL_TRMEI.MRA_DATE + "," +
                TBL_TRMEI.MRB_DATE + "," +
                TBL_TRMEI.MRC_DATE + "," +
                TBL_TRMEI.MRD_DATE + "," +
                TBL_TRMEI.YCA_MARK + "," +
                TBL_TRMEI.EDIT_FLG + "," +
                TBL_TRMEI.BCA_STS + "," +
                TBL_TRMEI.GMA_STS + "," +
                TBL_TRMEI.GRA_STS + "," +
                TBL_TRMEI.GRA_CONFIRMDATE + "," +
                TBL_TRMEI.MEMO + "," +
                TBL_TRMEI.DELETE_DATE + "," +
                TBL_TRMEI.DELETE_FLG + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_DSP_ID + "," +
                m_IC_OC_BK_NO + "," +
                m_IC_OLD_OC_BK_NO + "," +
                m_BUA_DATE + "," +
                m_BUB_DATE + "," +
                m_BCA_DATE + "," +
                m_GMA_DATE + "," +
                m_GMB_DATE + "," +
                m_GRA_DATE + "," +
                m_GXA_DATE + "," +
                m_GXB_DATE + "," +
                m_MRA_DATE + "," +
                m_MRB_DATE + "," +
                m_MRC_DATE + "," +
                m_MRD_DATE + "," +
                m_YCA_MARK + "," +
                m_EDIT_FLG + "," +
                m_BCA_STS + "," +
                m_GMA_STS + "," +
                m_GRA_STS + "," +
                m_GRA_CONFIRMDATE + "," +
                "'" + m_MEMO + "'," +
                m_DELETE_DATE + "," +
                m_DELETE_FLG + ")";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// (イメージ取込処理)
        /// </summary>
        /// <returns></returns>
        public string GetInsertQueryImageImport()
        {
            string strSQL = "INSERT INTO " + TBL_TRMEI.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRMEI.GYM_ID + "," +
                TBL_TRMEI.OPERATION_DATE + "," +
                TBL_TRMEI.SCAN_TERM + "," +
                TBL_TRMEI.BAT_ID + "," +
                TBL_TRMEI.DETAILS_NO + "," +
                TBL_TRMEI.DSP_ID + "," +
                TBL_TRMEI.BUA_DATE + "," +
                TBL_TRMEI.GMA_DATE + "," +
                TBL_TRMEI.GRA_DATE + "," +
                TBL_TRMEI.GXA_DATE + "," +
                TBL_TRMEI.MRA_DATE + "," +
                TBL_TRMEI.MRC_DATE + "," +
                TBL_TRMEI.EDIT_FLG + "," +
                TBL_TRMEI.BCA_STS + "," +
                TBL_TRMEI.MEMO + "," +
                TBL_TRMEI.DELETE_DATE + "," +
                TBL_TRMEI.DELETE_FLG + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_DSP_ID + "," +
                m_BUA_DATE + "," +
                m_GMA_DATE + "," +
                m_GRA_DATE + "," +
                m_GXA_DATE + "," +
                m_MRA_DATE + "," +
                m_MRC_DATE + "," +
                m_EDIT_FLG + "," +
                m_BCA_STS + "," +
                "'" + m_MEMO + "'," +
                m_DELETE_DATE + "," +
                m_DELETE_FLG + ")";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// (持帰確定処理)
        /// </summary>
        /// <returns></returns>
        public string GetInsertQueryFileImport()
        {
            string strSQL = "INSERT INTO " + TBL_TRMEI.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRMEI.GYM_ID + "," +
                TBL_TRMEI.OPERATION_DATE + "," +
                TBL_TRMEI.SCAN_TERM + "," +
                TBL_TRMEI.BAT_ID + "," +
                TBL_TRMEI.DETAILS_NO + "," +
                TBL_TRMEI.DSP_ID + "," +
                TBL_TRMEI.IC_OC_BK_NO + "," +
                TBL_TRMEI.IC_OLD_OC_BK_NO + "," +
                TBL_TRMEI.BUB_DATE + "," +
                TBL_TRMEI.BCA_DATE + "," +
                TBL_TRMEI.GMB_DATE + "," +
                TBL_TRMEI.GXB_DATE + "," +
                TBL_TRMEI.MRB_DATE + "," +
                TBL_TRMEI.MRD_DATE + "," +
                TBL_TRMEI.YCA_MARK + "," +
                TBL_TRMEI.EDIT_FLG + "," +
                TBL_TRMEI.GMA_STS + "," +
                TBL_TRMEI.GRA_STS + "," +
                TBL_TRMEI.GRA_CONFIRMDATE + "," +
                TBL_TRMEI.DELETE_DATE + "," +
                TBL_TRMEI.DELETE_FLG + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_DSP_ID + "," +
                m_IC_OC_BK_NO + "," +
                m_IC_OLD_OC_BK_NO + "," +
                m_BUB_DATE + "," +
                m_BCA_DATE + "," +
                m_GMB_DATE + "," +
                m_GXB_DATE + "," +
                m_MRB_DATE + "," +
                m_MRD_DATE + "," +
                m_YCA_MARK + "," +
                m_EDIT_FLG + "," +
                m_GMA_STS + "," +
                m_GRA_STS + "," +
                m_GRA_CONFIRMDATE + "," +
                m_DELETE_DATE + "," +
                m_DELETE_FLG + ")";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_TRMEI.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_TRMEI.DSP_ID + "=" + m_DSP_ID + ", " +
                TBL_TRMEI.IC_OC_BK_NO + "=" + m_IC_OC_BK_NO + ", " +
                TBL_TRMEI.IC_OLD_OC_BK_NO + "=" + m_IC_OLD_OC_BK_NO + ", " +
                TBL_TRMEI.BUA_DATE + "=" + m_BUA_DATE + ", " +
                TBL_TRMEI.BUB_DATE + "=" + m_BUB_DATE + ", " +
                TBL_TRMEI.BCA_DATE + "=" + m_BCA_DATE + ", " +
                TBL_TRMEI.GMA_DATE + "=" + m_GMA_DATE + ", " +
                TBL_TRMEI.GMB_DATE + "=" + m_GMB_DATE + ", " +
                TBL_TRMEI.GRA_DATE + "=" + m_GRA_DATE + ", " +
                TBL_TRMEI.GXA_DATE + "=" + m_GXA_DATE + ", " +
                TBL_TRMEI.GXB_DATE + "=" + m_GXB_DATE + ", " +
                TBL_TRMEI.MRA_DATE + "=" + m_MRA_DATE + ", " +
                TBL_TRMEI.MRB_DATE + "=" + m_MRB_DATE + ", " +
                TBL_TRMEI.MRC_DATE + "=" + m_MRC_DATE + ", " +
                TBL_TRMEI.MRD_DATE + "=" + m_MRD_DATE + ", " +
                TBL_TRMEI.YCA_MARK + "=" + m_YCA_MARK + ", " +
                TBL_TRMEI.EDIT_FLG + "=" + m_EDIT_FLG + ", " +
                TBL_TRMEI.BCA_STS + "=" + m_BCA_STS + ", " +
                TBL_TRMEI.GMA_STS + "=" + m_GMA_STS + ", " +
                TBL_TRMEI.GRA_STS + "=" + m_GRA_STS + ", " +
                TBL_TRMEI.GRA_CONFIRMDATE + "=" + m_GRA_CONFIRMDATE + ", " +
                TBL_TRMEI.MEMO + "='" + m_MEMO + "', " +
                TBL_TRMEI.DELETE_DATE + "=" + m_DELETE_DATE + ", " +
                TBL_TRMEI.DELETE_FLG + "=" + m_DELETE_FLG + " " +
                " WHERE " +
                TBL_TRMEI.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRMEI.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRMEI.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRMEI.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRMEI.DETAILS_NO + "=" + m_DETAILS_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_TRMEI.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_TRMEI.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRMEI.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRMEI.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRMEI.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_TRMEI.DETAILS_NO + "=" + m_DETAILS_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQuery(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            string strSQL = "DELETE FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEI.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEI.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEI.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEI.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEI.DETAILS_NO + "=" + detailsno;
            return strSQL;
        }

        #endregion
    }
}
