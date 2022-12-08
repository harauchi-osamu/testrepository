using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 証券明細テキスト管理
    /// </summary>
    public class TBL_BILLMEITXT_CTL
    {
        public TBL_BILLMEITXT_CTL(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_BILLMEITXT_CTL(string txtname, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_TXTNAME = txtname;
        }

        public TBL_BILLMEITXT_CTL(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "BILLMEITXT_CTL";

        public const string TXTNAME = "TXTNAME";
        public const string FILE_ID = "FILE_ID";
        public const string FILE_DIVID = "FILE_DIVID";
        public const string BK_NO = "BK_NO";
        public const string CREATE_DATE = "CREATE_DATE";
        public const string SEND_SEQ = "SEND_SEQ";
        public const string PAY_RECORD_COUNT = "PAY_RECORD_COUNT";
        public const string PAY_TOTAL_AMOUNT = "PAY_TOTAL_AMOUNT";
        public const string PAY_TOTAL_COUNT = "PAY_TOTAL_COUNT";
        public const string NONEPAY_RECORD_COUNT = "NONEPAY_RECORD_COUNT";
        public const string NONEPAY_TOTAL_AMOUNT = "NONEPAY_TOTAL_AMOUNT";
        public const string NONEPAY_TOTAL_COUNT = "NONEPAY_TOTAL_COUNT";
        public const string OTHER_FRONT_COUNT = "OTHER_FRONT_COUNT";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private string m_TXTNAME = "";
        public string m_FILE_ID = "";
        public string m_FILE_DIVID = "";
        public string m_BK_NO = "";
        public int m_CREATE_DATE = 0;
        public string m_SEND_SEQ = "";
        public int m_PAY_RECORD_COUNT = 0;
        public long m_PAY_TOTAL_AMOUNT = 0;
        public int m_PAY_TOTAL_COUNT = 0;
        public int m_NONEPAY_RECORD_COUNT = 0;
        public long m_NONEPAY_TOTAL_AMOUNT = 0;
        public int m_NONEPAY_TOTAL_COUNT = 0;
        public int m_OTHER_FRONT_COUNT = 0;

        #endregion

        #region プロパティ

        public string _TXTNAME
        {
            get { return m_TXTNAME; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_TXTNAME = DBConvert.ToStringNull(dr[TBL_BILLMEITXT_CTL.TXTNAME]);
            m_FILE_ID = DBConvert.ToStringNull(dr[TBL_BILLMEITXT_CTL.FILE_ID]);
            m_FILE_DIVID = DBConvert.ToStringNull(dr[TBL_BILLMEITXT_CTL.FILE_DIVID]);
            m_BK_NO = DBConvert.ToStringNull(dr[TBL_BILLMEITXT_CTL.BK_NO]);
            m_CREATE_DATE = DBConvert.ToIntNull(dr[TBL_BILLMEITXT_CTL.CREATE_DATE]);
            m_SEND_SEQ = DBConvert.ToStringNull(dr[TBL_BILLMEITXT_CTL.SEND_SEQ]);
            m_PAY_RECORD_COUNT = DBConvert.ToIntNull(dr[TBL_BILLMEITXT_CTL.PAY_RECORD_COUNT]);
            m_PAY_TOTAL_AMOUNT = DBConvert.ToLongNull(dr[TBL_BILLMEITXT_CTL.PAY_TOTAL_AMOUNT]);
            m_PAY_TOTAL_COUNT = DBConvert.ToIntNull(dr[TBL_BILLMEITXT_CTL.PAY_TOTAL_COUNT]);
            m_NONEPAY_RECORD_COUNT = DBConvert.ToIntNull(dr[TBL_BILLMEITXT_CTL.NONEPAY_RECORD_COUNT]);
            m_NONEPAY_TOTAL_AMOUNT = DBConvert.ToLongNull(dr[TBL_BILLMEITXT_CTL.NONEPAY_TOTAL_AMOUNT]);
            m_NONEPAY_TOTAL_COUNT = DBConvert.ToIntNull(dr[TBL_BILLMEITXT_CTL.NONEPAY_TOTAL_COUNT]);
            m_OTHER_FRONT_COUNT = DBConvert.ToIntNull(dr[TBL_BILLMEITXT_CTL.OTHER_FRONT_COUNT]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_BILLMEITXT_CTL.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_BILLMEITXT_CTL.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_BILLMEITXT_CTL.TXTNAME;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string txtname, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_BILLMEITXT_CTL.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                        TBL_BILLMEITXT_CTL.TXTNAME + "=" + txtname;
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_BILLMEITXT_CTL.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_BILLMEITXT_CTL.TXTNAME + "," +
                TBL_BILLMEITXT_CTL.FILE_ID + "," +
                TBL_BILLMEITXT_CTL.FILE_DIVID + "," +
                TBL_BILLMEITXT_CTL.BK_NO + "," +
                TBL_BILLMEITXT_CTL.CREATE_DATE + "," +
                TBL_BILLMEITXT_CTL.SEND_SEQ + "," +
                TBL_BILLMEITXT_CTL.PAY_RECORD_COUNT + "," +
                TBL_BILLMEITXT_CTL.PAY_TOTAL_AMOUNT + "," +
                TBL_BILLMEITXT_CTL.PAY_TOTAL_COUNT + "," +
                TBL_BILLMEITXT_CTL.NONEPAY_RECORD_COUNT + "," +
                TBL_BILLMEITXT_CTL.NONEPAY_TOTAL_AMOUNT + "," +
                TBL_BILLMEITXT_CTL.NONEPAY_TOTAL_COUNT + "," +
                TBL_BILLMEITXT_CTL.OTHER_FRONT_COUNT + ") VALUES (" +
                "'" + m_TXTNAME + "'," +
                "'" + m_FILE_ID + "'," +
                "'" + m_FILE_DIVID + "'," +
                "'" + m_BK_NO + "'," +
                m_CREATE_DATE + "," +
                "'" + m_SEND_SEQ + "'," +
                m_PAY_RECORD_COUNT + "," +
                m_PAY_TOTAL_AMOUNT + "," +
                m_PAY_TOTAL_COUNT + "," +
                m_NONEPAY_RECORD_COUNT + "," +
                m_NONEPAY_TOTAL_AMOUNT + "," +
                m_NONEPAY_TOTAL_COUNT + "," +
                m_OTHER_FRONT_COUNT + ")";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_BILLMEITXT_CTL.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_BILLMEITXT_CTL.FILE_ID + "='" + m_FILE_ID + "', " +
                TBL_BILLMEITXT_CTL.FILE_DIVID + "='" + m_FILE_DIVID + "', " +
                TBL_BILLMEITXT_CTL.BK_NO + "='" + m_BK_NO + "', " +
                TBL_BILLMEITXT_CTL.CREATE_DATE + "=" + m_CREATE_DATE + ", " +
                TBL_BILLMEITXT_CTL.SEND_SEQ + "='" + m_SEND_SEQ + "', " +
                TBL_BILLMEITXT_CTL.PAY_RECORD_COUNT + "=" + m_PAY_RECORD_COUNT + ", " +
                TBL_BILLMEITXT_CTL.PAY_TOTAL_AMOUNT + "=" + m_PAY_TOTAL_AMOUNT + ", " +
                TBL_BILLMEITXT_CTL.PAY_TOTAL_COUNT + "=" + m_PAY_TOTAL_COUNT + ", " +
                TBL_BILLMEITXT_CTL.NONEPAY_RECORD_COUNT + "=" + m_NONEPAY_RECORD_COUNT + ", " +
                TBL_BILLMEITXT_CTL.NONEPAY_TOTAL_AMOUNT + "=" + m_NONEPAY_TOTAL_AMOUNT + ", " +
                TBL_BILLMEITXT_CTL.NONEPAY_TOTAL_COUNT + "=" + m_NONEPAY_TOTAL_COUNT + ", " +
                TBL_BILLMEITXT_CTL.OTHER_FRONT_COUNT + "=" + m_OTHER_FRONT_COUNT + " " +
                " WHERE " +
                TBL_BILLMEITXT_CTL.TXTNAME + "='" + m_TXTNAME + "' ";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_BILLMEITXT_CTL.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_BILLMEITXT_CTL.TXTNAME + "='" + m_TXTNAME + "' ";
            return strSql;
        }

        #endregion
    }
}
