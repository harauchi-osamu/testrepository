using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 交換尻管理
    /// </summary>
    public class TBL_BALANCETXT_CTL
    {
        public TBL_BALANCETXT_CTL(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_BALANCETXT_CTL(string file_name, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_FILE_NAME = file_name;
        }

        public TBL_BALANCETXT_CTL(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "BALANCETXT_CTL";

        public const string FILE_NAME = "FILE_NAME";
        public const string FILE_ID = "FILE_ID";
        public const string FILE_DIVID = "FILE_DIVID";
        public const string BK_NO = "BK_NO";
        public const string MAKE_DATE = "MAKE_DATE";
        public const string SEND_SEQ = "SEND_SEQ";
        public const string CLEARING_DATE = "CLEARING_DATE";
        public const string RECORD_COUNT = "RECORD_COUNT";
        public const string RECV_DATE = "RECV_DATE";
        public const string RECV_TIME = "RECV_TIME";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private string m_FILE_NAME = "";
        public string m_FILE_ID = "";
        public string m_FILE_DIVID = "";
        public string m_BK_NO = "";
        public int m_MAKE_DATE = 0;
        public string m_SEND_SEQ = "";
        public int m_CLEARING_DATE = 0;
        public int m_RECORD_COUNT = 0;
        public int m_RECV_DATE = 0;
        public int m_RECV_TIME = 0;
        #endregion

        #region プロパティ

        public string _FILE_NAME
        {
            get { return m_FILE_NAME; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_FILE_NAME = DBConvert.ToStringNull(dr[TBL_BALANCETXT_CTL.FILE_NAME]);
            m_FILE_ID = DBConvert.ToStringNull(dr[TBL_BALANCETXT_CTL.FILE_ID]);
            m_FILE_DIVID = DBConvert.ToStringNull(dr[TBL_BALANCETXT_CTL.FILE_DIVID]);
            m_BK_NO = DBConvert.ToStringNull(dr[TBL_BALANCETXT_CTL.BK_NO]);
            m_MAKE_DATE = DBConvert.ToIntNull(dr[TBL_BALANCETXT_CTL.MAKE_DATE]);
            m_SEND_SEQ = DBConvert.ToStringNull(dr[TBL_BALANCETXT_CTL.SEND_SEQ]);
            m_CLEARING_DATE = DBConvert.ToIntNull(dr[TBL_BALANCETXT_CTL.CLEARING_DATE]);
            m_RECORD_COUNT = DBConvert.ToIntNull(dr[TBL_BALANCETXT_CTL.RECORD_COUNT]);
            m_RECV_DATE = DBConvert.ToIntNull(dr[TBL_BALANCETXT_CTL.RECV_DATE]);
            m_RECV_TIME = DBConvert.ToIntNull(dr[TBL_BALANCETXT_CTL.RECV_TIME]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_BALANCETXT_CTL.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_BALANCETXT_CTL.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_BALANCETXT_CTL.FILE_NAME;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string file_name, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_BALANCETXT_CTL.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_BALANCETXT_CTL.FILE_NAME + "='" + file_name + "'";
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_BALANCETXT_CTL.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_BALANCETXT_CTL.FILE_NAME + "," +
                TBL_BALANCETXT_CTL.FILE_ID + "," +
                TBL_BALANCETXT_CTL.FILE_DIVID + "," +
                TBL_BALANCETXT_CTL.BK_NO + "," +
                TBL_BALANCETXT_CTL.MAKE_DATE + "," +
                TBL_BALANCETXT_CTL.SEND_SEQ + "," +
                TBL_BALANCETXT_CTL.CLEARING_DATE + "," +
                TBL_BALANCETXT_CTL.RECORD_COUNT + "," +
                TBL_BALANCETXT_CTL.RECV_DATE + "," +
                TBL_BALANCETXT_CTL.RECV_TIME + ") VALUES (" +
                "'" + m_FILE_NAME + "'," +
                "'" + m_FILE_ID + "'," +
                "'" + m_FILE_DIVID + "'," +
                "'" + m_BK_NO + "'," +
                m_MAKE_DATE + "," +
                "'" + m_SEND_SEQ + "'," +
                m_CLEARING_DATE + "," +
                m_RECORD_COUNT + "," +
                m_RECV_DATE + "," +
                m_RECV_TIME + "" + ")";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_BALANCETXT_CTL.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_BALANCETXT_CTL.FILE_ID + "='" + m_FILE_ID + "', " +
                TBL_BALANCETXT_CTL.FILE_DIVID + "='" + m_FILE_DIVID + "', " +
                TBL_BALANCETXT_CTL.BK_NO + "='" + m_BK_NO + "', " +
                TBL_BALANCETXT_CTL.MAKE_DATE + "=" + m_MAKE_DATE + ", " +
                TBL_BALANCETXT_CTL.SEND_SEQ + "='" + m_SEND_SEQ + "', " +
                TBL_BALANCETXT_CTL.CLEARING_DATE + "=" + m_CLEARING_DATE + ", " +
                TBL_BALANCETXT_CTL.RECORD_COUNT + "=" + m_RECORD_COUNT + ", " +
                TBL_BALANCETXT_CTL.RECV_DATE + "=" + m_RECV_DATE + ", " +
                TBL_BALANCETXT_CTL.RECV_TIME + "=" + m_RECV_TIME +
                " WHERE " +
                TBL_BALANCETXT_CTL.FILE_NAME + "='" + m_FILE_NAME + "' ";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_BALANCETXT_CTL.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                  TBL_BALANCETXT_CTL.FILE_NAME + "='" + m_FILE_NAME + "' ";
            return strSql;
        }

        #endregion
    }
}
