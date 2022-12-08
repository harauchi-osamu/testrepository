using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 交換尻
    /// </summary>
    public class TBL_BALANCETXT
    {
        public TBL_BALANCETXT(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_BALANCETXT(string file_name, string bk_no, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_FILE_NAME = file_name;
            m_BK_NO = bk_no;
        }

        public TBL_BALANCETXT(DataRow dr, int Schemabankcd)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "BALANCETXT";

        public const string FILE_NAME = "FILE_NAME";
        public const string BK_NO = "BK_NO";
        public const string CONFIRM_FLG = "CONFIRM_FLG";
        public const string LOAN_KBN = "LOAN_KBN";
        public const string PAY_AMOUNT = "PAY_AMOUNT";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private string m_FILE_NAME = "";
        private string m_BK_NO = "";
        public string m_CONFIRM_FLG = "";
        public int m_LOAN_KBN = 0;
        public string m_PAY_AMOUNT = "";
        #endregion

        #region プロパティ

        public string _FILE_NAME
        {
            get { return m_FILE_NAME; }
        }
        public string _BK_NO
        {
            get { return m_BK_NO; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_FILE_NAME = DBConvert.ToStringNull(dr[TBL_BALANCETXT.FILE_NAME]);
            m_BK_NO = DBConvert.ToStringNull(dr[TBL_BALANCETXT.BK_NO]);
            m_CONFIRM_FLG = DBConvert.ToStringNull(dr[TBL_BALANCETXT.CONFIRM_FLG]);
            m_LOAN_KBN = DBConvert.ToIntNull(dr[TBL_BALANCETXT.LOAN_KBN]);
            m_PAY_AMOUNT = DBConvert.ToStringNull(dr[TBL_BALANCETXT.PAY_AMOUNT]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_BALANCETXT.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_BALANCETXT.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_BALANCETXT.FILE_NAME + "," +
                TBL_BALANCETXT.BK_NO;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string file_name, string bk_no, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_BALANCETXT.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_BALANCETXT.FILE_NAME + "='" + file_name + "'"+
                " AND " + TBL_BALANCETXT.BK_NO + "=" + bk_no;
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_BALANCETXT.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_BALANCETXT.FILE_NAME + "," +
                TBL_BALANCETXT.BK_NO + "," +
                TBL_BALANCETXT.CONFIRM_FLG + "," +
                TBL_BALANCETXT.LOAN_KBN + "," +
                TBL_BALANCETXT.PAY_AMOUNT + ") VALUES (" +
                "'" + m_FILE_NAME + "'," +
                "'" + m_BK_NO + "'," +
                "'" + m_CONFIRM_FLG + "'," +
                m_LOAN_KBN + "," +
                "'" + m_PAY_AMOUNT + "' " + ")";
            return strSql;
        }
        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_BALANCETXT.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_BALANCETXT.CONFIRM_FLG + "='" + m_CONFIRM_FLG + "', " +
                TBL_BALANCETXT.LOAN_KBN + "=" + m_LOAN_KBN + ", " +
                TBL_BALANCETXT.PAY_AMOUNT + "='" + m_PAY_AMOUNT + "' " +
                " WHERE " +
                TBL_BALANCETXT.FILE_NAME + "='" + m_FILE_NAME + "' " +
                TBL_BALANCETXT.BK_NO + "='" + m_BK_NO + "' ";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_BALANCETXT.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_BALANCETXT.FILE_NAME + "='" + m_FILE_NAME + "' " +
                TBL_BALANCETXT.BK_NO + "='" + m_BK_NO + "' ";
            return strSql;
        }

        /// <summary>
        /// FILE_NAMEを条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryfileName(string filename, int Schemabankcd)
        {
            string strSql = "DELETE FROM " + TBL_BALANCETXT.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                  TBL_BALANCETXT.FILE_NAME + "='" + filename + "' ";
            return strSql;
        }

        #endregion
    }
}
