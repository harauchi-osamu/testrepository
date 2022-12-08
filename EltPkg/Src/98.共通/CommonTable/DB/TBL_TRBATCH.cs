using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// バッチデータ
    /// </summary>
    public class TBL_TRBATCH
    {
        public TBL_TRBATCH(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_TRBATCH(int gymid, int operationdate, string scanterm, int batid, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_GYM_ID = gymid;
            m_OPERATION_DATE = operationdate;
            m_SCAN_TERM = scanterm;
            m_BAT_ID = batid;
        }

        public TBL_TRBATCH(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "TRBATCH";

        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCAN_TERM = "SCAN_TERM";
        public const string BAT_ID = "BAT_ID";
        public const string STS = "STS";
        public const string INPUT_ROUTE = "INPUT_ROUTE";
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
        public const string E_TERM = "E_TERM";
        public const string E_OPENO = "E_OPENO";
        public const string E_YMD = "E_YMD";
        public const string E_TIME = "E_TIME";

        public const string ALL_COLUMNS = " GYM_ID,OPERATION_DATE,SCAN_TERM,BAT_ID,STS,INPUT_ROUTE,OC_BK_NO,OC_BR_NO,SCAN_BR_NO,SCAN_DATE,CLEARING_DATE,SCAN_COUNT,TOTAL_COUNT,TOTAL_AMOUNT,DELETE_DATE,DELETE_FLG,E_TERM,E_OPENO,E_YMD,E_TIME ";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        private string m_SCAN_TERM = "";
        private int m_BAT_ID = 0;
        public int m_STS = 0;
        public int m_INPUT_ROUTE = 0;
        public int m_OC_BK_NO = 0;
        public int m_OC_BR_NO = 0;
        public int m_SCAN_BR_NO = 0;
        public int m_SCAN_DATE = 0;
        public int m_CLEARING_DATE = 0;
        public int m_SCAN_COUNT = 0;
        public int m_TOTAL_COUNT = 0;
        public long m_TOTAL_AMOUNT = 0;
        public int m_DELETE_DATE = 0;
        public int m_DELETE_FLG = 0;
        public string m_E_TERM = "";
        public string m_E_OPENO = "";
        public int m_E_YMD = 0;
        public int m_E_TIME = 0;

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
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_TRBATCH.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_TRBATCH.OPERATION_DATE]);
            m_SCAN_TERM = DBConvert.ToStringNull(dr[TBL_TRBATCH.SCAN_TERM]);
            m_BAT_ID = DBConvert.ToIntNull(dr[TBL_TRBATCH.BAT_ID]);
            m_STS = DBConvert.ToIntNull(dr[TBL_TRBATCH.STS]);
            m_INPUT_ROUTE = DBConvert.ToIntNull(dr[TBL_TRBATCH.INPUT_ROUTE]);
            m_OC_BK_NO = DBConvert.ToIntNull(dr[TBL_TRBATCH.OC_BK_NO]);
            m_OC_BR_NO = DBConvert.ToIntNull(dr[TBL_TRBATCH.OC_BR_NO]);
            m_SCAN_BR_NO = DBConvert.ToIntNull(dr[TBL_TRBATCH.SCAN_BR_NO]);
            m_SCAN_DATE = DBConvert.ToIntNull(dr[TBL_TRBATCH.SCAN_DATE]);
            m_CLEARING_DATE = DBConvert.ToIntNull(dr[TBL_TRBATCH.CLEARING_DATE]);
            m_SCAN_COUNT = DBConvert.ToIntNull(dr[TBL_TRBATCH.SCAN_COUNT]);
            m_TOTAL_COUNT = DBConvert.ToIntNull(dr[TBL_TRBATCH.TOTAL_COUNT]);
            m_TOTAL_AMOUNT = DBConvert.ToLongNull(dr[TBL_TRBATCH.TOTAL_AMOUNT]);
            m_DELETE_DATE = DBConvert.ToIntNull(dr[TBL_TRBATCH.DELETE_DATE]);
            m_DELETE_FLG = DBConvert.ToIntNull(dr[TBL_TRBATCH.DELETE_FLG]);
            m_E_TERM = DBConvert.ToStringNull(dr[TBL_TRBATCH.E_TERM]);
            m_E_OPENO = DBConvert.ToStringNull(dr[TBL_TRBATCH.E_OPENO]);
            m_E_YMD = DBConvert.ToIntNull(dr[TBL_TRBATCH.E_YMD]);
            m_E_TIME = DBConvert.ToIntNull(dr[TBL_TRBATCH.E_TIME]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_TRBATCH.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRBATCH.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_TRBATCH.GYM_ID + "," +
                TBL_TRBATCH.OPERATION_DATE + "," +
                TBL_TRBATCH.SCAN_TERM + "," +
                TBL_TRBATCH.BAT_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_TRBATCH.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRBATCH.GYM_ID + "=" + gymid + " AND " +
                TBL_TRBATCH.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRBATCH.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRBATCH.BAT_ID + "=" + batid;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_TRBATCH.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TRBATCH.GYM_ID + "," +
                TBL_TRBATCH.OPERATION_DATE + "," +
                TBL_TRBATCH.SCAN_TERM + "," +
                TBL_TRBATCH.BAT_ID + "," +
                TBL_TRBATCH.STS + "," +
                TBL_TRBATCH.INPUT_ROUTE + "," +
                TBL_TRBATCH.OC_BK_NO + "," +
                TBL_TRBATCH.OC_BR_NO + "," +
                TBL_TRBATCH.SCAN_BR_NO + "," +
                TBL_TRBATCH.SCAN_DATE + "," +
                TBL_TRBATCH.CLEARING_DATE + "," +
                TBL_TRBATCH.SCAN_COUNT + "," +
                TBL_TRBATCH.TOTAL_COUNT + "," +
                TBL_TRBATCH.TOTAL_AMOUNT + "," +
                TBL_TRBATCH.DELETE_DATE + "," +
                TBL_TRBATCH.DELETE_FLG + "," +
                TBL_TRBATCH.E_TERM + "," +
                TBL_TRBATCH.E_OPENO + "," +
                TBL_TRBATCH.E_YMD + "," +
                TBL_TRBATCH.E_TIME + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_STS + "," +
                m_INPUT_ROUTE + "," +
                m_OC_BK_NO + "," +
                m_OC_BR_NO + "," +
                m_SCAN_BR_NO + "," +
                m_SCAN_DATE + "," +
                m_CLEARING_DATE + "," +
                m_SCAN_COUNT + "," +
                m_TOTAL_COUNT + "," +
                m_TOTAL_AMOUNT + "," +
                m_DELETE_DATE + "," +
                m_DELETE_FLG + "," +
                "'" + m_E_TERM + "'," +
                "'" + m_E_OPENO + "'," +
                m_E_YMD + "," +
                m_E_TIME + ")";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_TRBATCH.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_TRBATCH.GYM_ID + "=" + m_GYM_ID + ", " +
                TBL_TRBATCH.OPERATION_DATE + "=" + m_OPERATION_DATE + ", " +
                TBL_TRBATCH.SCAN_TERM + "='" + m_SCAN_TERM + "', " +
                TBL_TRBATCH.BAT_ID + "=" + m_BAT_ID + ", " +
                TBL_TRBATCH.STS + "=" + m_STS + ", " +
                TBL_TRBATCH.INPUT_ROUTE + "=" + m_INPUT_ROUTE + ", " +
                TBL_TRBATCH.OC_BK_NO + "=" + m_OC_BK_NO + ", " +
                TBL_TRBATCH.OC_BR_NO + "=" + m_OC_BR_NO + ", " +
                TBL_TRBATCH.SCAN_BR_NO + "=" + m_SCAN_BR_NO + ", " +
                TBL_TRBATCH.SCAN_DATE + "=" + m_SCAN_DATE + ", " +
                TBL_TRBATCH.CLEARING_DATE + "=" + m_CLEARING_DATE + ", " +
                TBL_TRBATCH.SCAN_COUNT + "=" + m_SCAN_COUNT + ", " +
                TBL_TRBATCH.TOTAL_COUNT + "=" + m_TOTAL_COUNT + ", " +
                TBL_TRBATCH.TOTAL_AMOUNT + "=" + m_TOTAL_AMOUNT + ", " +
                TBL_TRBATCH.DELETE_DATE + "=" + m_DELETE_DATE + ", " +
                TBL_TRBATCH.DELETE_FLG + "=" + m_DELETE_FLG + ", " +
                TBL_TRBATCH.E_TERM + "='" + m_E_TERM + "', " +
                TBL_TRBATCH.E_OPENO + "='" + m_E_OPENO + "', " +
                TBL_TRBATCH.E_YMD + "=" + m_E_YMD + ", " +
                TBL_TRBATCH.E_TIME + "=" + m_E_TIME + " " +
                " WHERE " +
                TBL_TRBATCH.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRBATCH.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRBATCH.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRBATCH.BAT_ID + "=" + m_BAT_ID;
            return strSQL;
        }

        /// <summary>
        /// 枚数や金額項目をUPDATEする
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQueryNumber()
        {
            string strSQL = "UPDATE " + TBL_TRBATCH.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_TRBATCH.SCAN_COUNT + "=" + m_SCAN_COUNT + ", " +
                TBL_TRBATCH.TOTAL_COUNT + "=" + m_TOTAL_COUNT + ", " +
                TBL_TRBATCH.TOTAL_AMOUNT + "=" + m_TOTAL_AMOUNT + ", " +
                TBL_TRBATCH.DELETE_DATE + "=" + m_DELETE_DATE + ", " +
                TBL_TRBATCH.DELETE_FLG + "=" + m_DELETE_FLG + " " +
                " WHERE " +
                TBL_TRBATCH.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRBATCH.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRBATCH.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRBATCH.BAT_ID + "=" + m_BAT_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_TRBATCH.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_TRBATCH.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_TRBATCH.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_TRBATCH.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_TRBATCH.BAT_ID + "=" + m_BAT_ID;
            return strSQL;
        }

        #endregion
    }
}
