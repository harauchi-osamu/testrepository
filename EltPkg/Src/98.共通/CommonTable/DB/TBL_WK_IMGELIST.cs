using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 明細イメージリスト
    /// </summary>
    public class TBL_WK_IMGELIST
    {
        public TBL_WK_IMGELIST()
        {
        }

        public TBL_WK_IMGELIST(string term, int gymid, int operationdate, string scanterm, int batid, int detailsno)
        {
            m_SEARCH_TERMID = term;
            m_GYM_ID = gymid;
            m_OPERATION_DATE = operationdate;
            m_SCAN_TERM = scanterm;
            m_BAT_ID = batid;
            m_DETAILS_NO = detailsno;
        }

        public TBL_WK_IMGELIST(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_PHYSICAL_NAME = "WK_IMGELIST";

        public const string SEARCH_TERMID = "SEARCH_TERMID";
        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCAN_TERM = "SCAN_TERM";
        public const string BAT_ID = "BAT_ID";
        public const string DETAILS_NO = "DETAILS_NO";
        public const string SORT_NO = "SORT_NO";
        #endregion

        #region メンバ

        private string m_SEARCH_TERMID = "";
        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        private string m_SCAN_TERM = "";
        private int m_BAT_ID = 0;
        private int m_DETAILS_NO = 0;
        public int m_SORT_NO = 0;

        #endregion

        #region プロパティ

        public string _SEARCH_TERMID
        {
            get { return m_SEARCH_TERMID; }
        }
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
            m_SEARCH_TERMID = DBConvert.ToStringNull(dr[TBL_WK_IMGELIST.SEARCH_TERMID]);
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_WK_IMGELIST.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_WK_IMGELIST.OPERATION_DATE]);
            m_SCAN_TERM = DBConvert.ToStringNull(dr[TBL_WK_IMGELIST.SCAN_TERM]);
            m_BAT_ID = DBConvert.ToIntNull(dr[TBL_WK_IMGELIST.BAT_ID]);
            m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_WK_IMGELIST.DETAILS_NO]);
            m_SORT_NO = DBConvert.ToIntNull(dr[TBL_WK_IMGELIST.SORT_NO]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            string strSQL = "SELECT * FROM " + TBL_WK_IMGELIST.TABLE_NAME +
                " ORDER BY " +
                TBL_WK_IMGELIST.SEARCH_TERMID + "," +
                TBL_WK_IMGELIST.GYM_ID + "," +
                TBL_WK_IMGELIST.OPERATION_DATE + "," +
                TBL_WK_IMGELIST.SCAN_TERM + "," +
                TBL_WK_IMGELIST.BAT_ID + "," +
                TBL_WK_IMGELIST.DETAILS_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string term, int gymid)
        {
            string strSQL = "SELECT * FROM " + TBL_WK_IMGELIST.TABLE_NAME +
                " WHERE " +
                TBL_WK_IMGELIST.SEARCH_TERMID + "='" + term + "' AND " +
                TBL_WK_IMGELIST.GYM_ID + "=" + gymid +
                " ORDER BY " +
                TBL_WK_IMGELIST.GYM_ID + "," +
                TBL_WK_IMGELIST.OPERATION_DATE + "," +
                TBL_WK_IMGELIST.SCAN_TERM + "," +
                TBL_WK_IMGELIST.BAT_ID + "," +
                TBL_WK_IMGELIST.DETAILS_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string term, int gymid, int operationdate, string scanterm, int batid, int detailsno)
        {
            string strSQL = "SELECT * FROM " + TBL_WK_IMGELIST.TABLE_NAME +
                " WHERE " +
                TBL_WK_IMGELIST.SEARCH_TERMID + "='" + term + "' AND " +
                TBL_WK_IMGELIST.GYM_ID + "=" + gymid + " AND " +
                TBL_WK_IMGELIST.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_WK_IMGELIST.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_WK_IMGELIST.BAT_ID + "=" + batid + " AND " +
                TBL_WK_IMGELIST.DETAILS_NO + "=" + detailsno;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_WK_IMGELIST.TABLE_NAME + " (" +
                TBL_WK_IMGELIST.SEARCH_TERMID + "," +
                TBL_WK_IMGELIST.GYM_ID + "," +
                TBL_WK_IMGELIST.OPERATION_DATE + "," +
                TBL_WK_IMGELIST.SCAN_TERM + "," +
                TBL_WK_IMGELIST.BAT_ID + "," +
                TBL_WK_IMGELIST.DETAILS_NO + "," +
                TBL_WK_IMGELIST.SORT_NO + ") VALUES (" +
                "'" + m_SEARCH_TERMID + "'," +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_SORT_NO + ")";
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_WK_IMGELIST.TABLE_NAME +
                " WHERE " +
                TBL_WK_IMGELIST.SEARCH_TERMID + "='" + m_SEARCH_TERMID + "' AND " +
                TBL_WK_IMGELIST.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_WK_IMGELIST.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_WK_IMGELIST.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_WK_IMGELIST.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_WK_IMGELIST.DETAILS_NO + "=" + m_DETAILS_NO;
            return strSQL;
        }

        /// <summary>
        /// SEARCH_TERMIDを条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQuerySerchTermID(string term)
        {
            string strSQL = "DELETE FROM " + TBL_WK_IMGELIST.TABLE_NAME +
                " WHERE " +
                TBL_WK_IMGELIST.SEARCH_TERMID + "='" + term + "' ";
            return strSQL;
        }


        #endregion
    }
}
