using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 補正ステータス
    /// </summary>
    public class TBL_HOSEI_STATUS
    {
        public TBL_HOSEI_STATUS(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_HOSEI_STATUS(int gymid, int operationdate, string scanterm, int batid, int detailsno, int hoseiinptmode, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_GYM_ID = gymid;
            m_OPERATION_DATE = operationdate;
            m_SCAN_TERM = scanterm;
            m_BAT_ID = batid;
            m_DETAILS_NO = detailsno;
            m_HOSEI_INPTMODE = hoseiinptmode;
        }

        public TBL_HOSEI_STATUS(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        /// <summary>
        /// クラスの簡易コピー
        /// </summary>
        /// <returns></returns>
        public TBL_HOSEI_STATUS ShallowCopy()
        {
            return (TBL_HOSEI_STATUS)this.MemberwiseClone();
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "HOSEI_STATUS";

        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCAN_TERM = "SCAN_TERM";
        public const string BAT_ID = "BAT_ID";
        public const string DETAILS_NO = "DETAILS_NO";
        public const string HOSEI_INPTMODE = "HOSEI_INPTMODE";
        public const string INPT_STS = "INPT_STS";
        public const string TMNO = "TMNO";
        public const string OPENO = "OPENO";
        public const string E_OPENO = "E_OPENO";

        public const string ALL_COLUMNS = " GYM_ID,OPERATION_DATE,SCAN_TERM,BAT_ID,DETAILS_NO,HOSEI_INPTMODE,INPT_STS,TMNO,OPENO,E_OPENO ";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        private string m_SCAN_TERM = "";
        private int m_BAT_ID = 0;
        private int m_DETAILS_NO = 0;
        private int m_HOSEI_INPTMODE = 0;
        public int m_INPT_STS = 0;
        public string m_TMNO = "";
        public string m_OPENO = "";
        public string m_E_OPENO = "";

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
        public int _HOSEI_INPTMODE
        {
            get { return m_HOSEI_INPTMODE; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_HOSEI_STATUS.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_HOSEI_STATUS.OPERATION_DATE]);
            m_SCAN_TERM = DBConvert.ToStringNull(dr[TBL_HOSEI_STATUS.SCAN_TERM]);
            m_BAT_ID = DBConvert.ToIntNull(dr[TBL_HOSEI_STATUS.BAT_ID]);
            m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_HOSEI_STATUS.DETAILS_NO]);
            m_HOSEI_INPTMODE = DBConvert.ToIntNull(dr[TBL_HOSEI_STATUS.HOSEI_INPTMODE]);
            m_INPT_STS = DBConvert.ToIntNull(dr[TBL_HOSEI_STATUS.INPT_STS]);
            m_TMNO = DBConvert.ToStringNull(dr[TBL_HOSEI_STATUS.TMNO]);
            m_OPENO = DBConvert.ToStringNull(dr[TBL_HOSEI_STATUS.OPENO]);
            m_E_OPENO = DBConvert.ToStringNull(dr[TBL_HOSEI_STATUS.E_OPENO]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_HOSEI_STATUS.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_HOSEI_STATUS.GYM_ID + "," +
                TBL_HOSEI_STATUS.OPERATION_DATE + "," +
                TBL_HOSEI_STATUS.SCAN_TERM + "," +
                TBL_HOSEI_STATUS.BAT_ID + "," +
                TBL_HOSEI_STATUS.DETAILS_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_HOSEI_STATUS.GYM_ID + "=" + gymid + " AND " +
                TBL_HOSEI_STATUS.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_HOSEI_STATUS.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_HOSEI_STATUS.BAT_ID + "=" + batid + " " +
                " ORDER BY " +
                TBL_HOSEI_STATUS.DETAILS_NO + ", " +
                TBL_HOSEI_STATUS.HOSEI_INPTMODE;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryHosei(int gymid, int operationdate, string scanterm, int batid, int hoseiinptmode, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_HOSEI_STATUS.GYM_ID + "=" + gymid + " AND " +
                TBL_HOSEI_STATUS.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_HOSEI_STATUS.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_HOSEI_STATUS.BAT_ID + "=" + batid + " AND " +
                TBL_HOSEI_STATUS.HOSEI_INPTMODE + "=" + hoseiinptmode;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_HOSEI_STATUS.GYM_ID + "=" + gymid + " AND " +
                TBL_HOSEI_STATUS.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_HOSEI_STATUS.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_HOSEI_STATUS.BAT_ID + "=" + batid + " AND " +
                TBL_HOSEI_STATUS.DETAILS_NO + "=" + detailsno + " " +
                " ORDER BY " +
                TBL_HOSEI_STATUS.HOSEI_INPTMODE;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int operationdate, string scanterm, int batid, int detailsno, int hoseiinptmode, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_HOSEI_STATUS.GYM_ID + "=" + gymid + " AND " +
                TBL_HOSEI_STATUS.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_HOSEI_STATUS.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_HOSEI_STATUS.BAT_ID + "=" + batid + " AND " +
                TBL_HOSEI_STATUS.DETAILS_NO + "=" + detailsno + " AND " +
                TBL_HOSEI_STATUS.HOSEI_INPTMODE + "=" + hoseiinptmode;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_HOSEI_STATUS.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_HOSEI_STATUS.GYM_ID + "," +
                TBL_HOSEI_STATUS.OPERATION_DATE + "," +
                TBL_HOSEI_STATUS.SCAN_TERM + "," +
                TBL_HOSEI_STATUS.BAT_ID + "," +
                TBL_HOSEI_STATUS.DETAILS_NO + "," +
                TBL_HOSEI_STATUS.HOSEI_INPTMODE + "," +
                TBL_HOSEI_STATUS.INPT_STS + "," +
                TBL_HOSEI_STATUS.TMNO + "," +
                TBL_HOSEI_STATUS.OPENO + "," +
                TBL_HOSEI_STATUS.E_OPENO + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_HOSEI_INPTMODE + "," +
                m_INPT_STS + "," +
                "'" + m_TMNO + "'," +
                "'" + m_OPENO + "'," +
                "'" + m_E_OPENO + "')";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// (イメージ取込処理)
        /// </summary>
        /// <returns></returns>
        public string GetInsertQueryImageImport()
        {
            string strSQL = "INSERT INTO " + TBL_HOSEI_STATUS.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_HOSEI_STATUS.GYM_ID + "," +
                TBL_HOSEI_STATUS.OPERATION_DATE + "," +
                TBL_HOSEI_STATUS.SCAN_TERM + "," +
                TBL_HOSEI_STATUS.BAT_ID + "," +
                TBL_HOSEI_STATUS.DETAILS_NO + "," +
                TBL_HOSEI_STATUS.HOSEI_INPTMODE + "," +
                TBL_HOSEI_STATUS.INPT_STS + ") VALUES (" +
                m_GYM_ID + "," +
                m_OPERATION_DATE + "," +
                "'" + m_SCAN_TERM + "'," +
                m_BAT_ID + "," +
                m_DETAILS_NO + "," +
                m_HOSEI_INPTMODE + "," +
                m_INPT_STS + ")";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_HOSEI_STATUS.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_HOSEI_STATUS.INPT_STS + "=" + m_INPT_STS + ", " +
                TBL_HOSEI_STATUS.TMNO + "='" + m_TMNO + "', " +
                TBL_HOSEI_STATUS.OPENO + "='" + m_OPENO + "', " +
                TBL_HOSEI_STATUS.E_OPENO + "='" + m_E_OPENO + "' " +
                " WHERE " +
                TBL_HOSEI_STATUS.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_HOSEI_STATUS.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_HOSEI_STATUS.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_HOSEI_STATUS.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_HOSEI_STATUS.DETAILS_NO + "=" + m_DETAILS_NO + " AND " +
                TBL_HOSEI_STATUS.HOSEI_INPTMODE + "=" + m_HOSEI_INPTMODE;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_HOSEI_STATUS.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_HOSEI_STATUS.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_HOSEI_STATUS.OPERATION_DATE + "=" + m_OPERATION_DATE + " AND " +
                TBL_HOSEI_STATUS.SCAN_TERM + "='" + m_SCAN_TERM + "' AND " +
                TBL_HOSEI_STATUS.BAT_ID + "=" + m_BAT_ID + " AND " +
                TBL_HOSEI_STATUS.DETAILS_NO + "=" + m_DETAILS_NO + " AND " +
                TBL_HOSEI_STATUS.HOSEI_INPTMODE + "=" + m_HOSEI_INPTMODE;
            return strSQL;
        }

        /// <summary>
        /// 明細単位でのDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryDetails(int gymid, int operationdate, string scanterm, long batid, int detailsno, int Schemabankcd)
        {
            string strSQL = "DELETE FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_HOSEI_STATUS.GYM_ID + "=" + gymid + " AND " +
                TBL_HOSEI_STATUS.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_HOSEI_STATUS.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_HOSEI_STATUS.BAT_ID + "=" + batid + " AND " +
                TBL_HOSEI_STATUS.DETAILS_NO + "=" + detailsno + " ";
            return strSQL;
        }

        /// <summary>
        /// 業務ID・業務日付・バッチ番号・明細番号を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryBatIDDetailNo(int gymid, int operationdate, int batid, int detailsno, int Schemabankcd)
        {
            string strSQL = "DELETE FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_HOSEI_STATUS.GYM_ID + "=" + gymid + " AND " +
                TBL_HOSEI_STATUS.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_HOSEI_STATUS.BAT_ID + "=" + batid + " AND " +
                TBL_HOSEI_STATUS.DETAILS_NO + "=" + detailsno + " ";
            return strSQL;
        }


        #endregion
    }
}
