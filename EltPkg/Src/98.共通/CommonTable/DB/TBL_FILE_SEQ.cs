using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 汎用エントリオペレータ処理状況
    /// </summary>
    public class TBL_FILE_SEQ
    {
        public TBL_FILE_SEQ(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_FILE_SEQ(int send_date, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_SEND_DATE = send_date;
        }

        public TBL_FILE_SEQ(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "FILE_SEQ";

        public const string SEND_DATE = "SEND_DATE";
        public const string LAST_SEQ = "LAST_SEQ";
        public const string LAST_SEQ_DATE = "LAST_SEQ_DATE";
        public const string LAST_SEQ_TIME = "LAST_SEQ_TIME";
        public const string LAST_FILE_ID = "LAST_FILE_ID";
        public const string LAST_FILE_DIVID = "LAST_FILE_DIVID";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private int m_SEND_DATE = 0;
        public int m_LAST_SEQ = 0;
        public int m_LAST_SEQ_DATE = 0;
        public int m_LAST_SEQ_TIME = 0;
        public string m_LAST_FILE_ID = "";
        public string m_LAST_FILE_DIVID = "";
        #endregion

        #region プロパティ

        public int _SEND_DATE
        {
            get { return m_SEND_DATE; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_SEND_DATE = DBConvert.ToIntNull(dr[TBL_FILE_SEQ.SEND_DATE]);
            m_LAST_SEQ = DBConvert.ToIntNull(dr[TBL_FILE_SEQ.LAST_SEQ]);
            m_LAST_SEQ_DATE = DBConvert.ToIntNull(dr[TBL_FILE_SEQ.LAST_SEQ_DATE]);
            m_LAST_SEQ_TIME = DBConvert.ToIntNull(dr[TBL_FILE_SEQ.LAST_SEQ_TIME]);
            m_LAST_FILE_ID = DBConvert.ToStringNull(dr[TBL_FILE_SEQ.LAST_FILE_ID]);
            m_LAST_FILE_DIVID = DBConvert.ToStringNull(dr[TBL_FILE_SEQ.LAST_FILE_DIVID]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_FILE_SEQ.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_FILE_SEQ.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_FILE_SEQ.SEND_DATE;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int send_date, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_FILE_SEQ.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_FILE_SEQ.SEND_DATE + "=" + send_date;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int send_date, int Schemabankcd, bool Lock)
        {
            string strSql = GetSelectQuery(send_date, Schemabankcd);
            if (Lock)
            {
                strSql += string.Format(DBConvert.QUERY_LOCK_WAIT, 10) + " ";
            }
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_FILE_SEQ.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_FILE_SEQ.SEND_DATE + "," +
                TBL_FILE_SEQ.LAST_SEQ + "," +
                TBL_FILE_SEQ.LAST_SEQ_DATE + "," +
                TBL_FILE_SEQ.LAST_SEQ_TIME + "," +
                TBL_FILE_SEQ.LAST_FILE_ID + "," +
                TBL_FILE_SEQ.LAST_FILE_DIVID + ") VALUES (" +
                m_SEND_DATE + "," +
                m_LAST_SEQ + "," +
                m_LAST_SEQ_DATE + "," +
                m_LAST_SEQ_TIME + "," +
                "'" + m_LAST_FILE_ID + "'," +
                "'" + m_LAST_FILE_DIVID + "')";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_FILE_SEQ.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_FILE_SEQ.LAST_SEQ + "=" + m_LAST_SEQ + ", " +
                TBL_FILE_SEQ.LAST_SEQ_DATE + "=" + m_LAST_SEQ_DATE + ", " +
                TBL_FILE_SEQ.LAST_SEQ_TIME + "=" + m_LAST_SEQ_TIME + ", " +
                TBL_FILE_SEQ.LAST_FILE_ID + "='" + m_LAST_FILE_ID + "', " +
                TBL_FILE_SEQ.LAST_FILE_DIVID + "='" + m_LAST_FILE_DIVID + "' " +
                " WHERE " +
                TBL_FILE_SEQ.SEND_DATE + "=" + m_SEND_DATE;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_FILE_SEQ.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_FILE_SEQ.SEND_DATE + "=" + m_SEND_DATE;
            return strSql;
        }

        #endregion
    }
}
