using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 支払人マスタ
    /// </summary>
    public class TBL_PAYERMF
    {
        public TBL_PAYERMF(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_PAYERMF(int brno, int accoutno, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_BR_NO = brno;
            m_ACCOUNT_NO = accoutno;
        }

        public TBL_PAYERMF(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "PAYERMF";

        public const string BR_NO = "BR_NO";
        public const string ACCOUNT_NO = "ACCOUNT_NO";
        public const string NAME_KANJI = "NAME_KANJI";
        public const string NAME_KANA = "NAME_KANA";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_BR_NO = 0;
        private long m_ACCOUNT_NO = 0;
        public string m_NAME_KANJI = "";
        public string m_NAME_KANA = "";

        #endregion

        #region プロパティ

        public int _BR_NO
        {
            get { return m_BR_NO; }
        }
        public long _ACCOUNT_NO
        {
            get { return m_ACCOUNT_NO; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_BR_NO = DBConvert.ToIntNull(dr[TBL_PAYERMF.BR_NO]);
            m_ACCOUNT_NO = DBConvert.ToLongNull(dr[TBL_PAYERMF.ACCOUNT_NO]);
            m_NAME_KANJI = DBConvert.ToStringNull(dr[TBL_PAYERMF.NAME_KANJI]);
            m_NAME_KANA = DBConvert.ToStringNull(dr[TBL_PAYERMF.NAME_KANA]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_PAYERMF.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_PAYERMF.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_PAYERMF.BR_NO + "," +
                TBL_PAYERMF.ACCOUNT_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int brno, int accoutno, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_PAYERMF.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_PAYERMF.BR_NO + "=" + brno + " AND " +
                TBL_PAYERMF.ACCOUNT_NO + "=" + accoutno;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_PAYERMF.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_PAYERMF.BR_NO + "," +
                TBL_PAYERMF.ACCOUNT_NO + "," +
                TBL_PAYERMF.NAME_KANJI + "," +
                TBL_PAYERMF.NAME_KANA + ") VALUES (" +
                m_BR_NO + "," +
                m_ACCOUNT_NO + "," +
                "'" + m_NAME_KANJI + "'," +
                "'" + m_NAME_KANA + "')";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_PAYERMF.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_PAYERMF.NAME_KANJI + "='" + m_NAME_KANJI + "', " +
                TBL_PAYERMF.NAME_KANA + "='" + m_NAME_KANA + "' " +
                " WHERE " +
                TBL_PAYERMF.BR_NO + "=" + m_BR_NO + " AND " +
                TBL_PAYERMF.ACCOUNT_NO + "=" + m_ACCOUNT_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_PAYERMF.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_PAYERMF.BR_NO + "=" + m_BR_NO + " AND " +
                TBL_PAYERMF.ACCOUNT_NO + "=" + m_ACCOUNT_NO;
            return strSQL;
        }

        #endregion
    }
}
