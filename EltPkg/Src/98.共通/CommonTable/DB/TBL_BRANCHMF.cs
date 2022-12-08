using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 支店マスタ
    /// </summary>
    public class TBL_BRANCHMF
    {
        public TBL_BRANCHMF(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_BRANCHMF(int brno, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_BR_NO = brno;
        }

        public TBL_BRANCHMF(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "BRANCHMF";

        public const string BR_NO = "BR_NO";
        public const string BR_NAME_KANJI = "BR_NAME_KANJI";
        public const string BR_NAME_KANA = "BR_NAME_KANA";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_BR_NO = 0;
        public string m_BR_NAME_KANJI = "";
        public string m_BR_NAME_KANA = "";

        #endregion

        #region プロパティ

        public int _BR_NO
        {
            get { return m_BR_NO; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_BR_NO = DBConvert.ToIntNull(dr[TBL_BRANCHMF.BR_NO]);
            m_BR_NAME_KANJI = DBConvert.ToStringNull(dr[TBL_BRANCHMF.BR_NAME_KANJI]);
            m_BR_NAME_KANA = DBConvert.ToStringNull(dr[TBL_BRANCHMF.BR_NAME_KANA]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_BRANCHMF.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_BRANCHMF.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_BRANCHMF.BR_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int brno, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_BRANCHMF.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_BRANCHMF.BR_NO + "=" + brno;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_BRANCHMF.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_BRANCHMF.BR_NO + "," +
                TBL_BRANCHMF.BR_NAME_KANJI + "," +
                TBL_BRANCHMF.BR_NAME_KANA + ") VALUES (" +
                m_BR_NO + "," +
                "'" + m_BR_NAME_KANJI + "'," +
                "'" + m_BR_NAME_KANA + "')";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_BRANCHMF.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_BRANCHMF.BR_NAME_KANJI + "='" + m_BR_NAME_KANJI + "', " +
                TBL_BRANCHMF.BR_NAME_KANA + "='" + m_BR_NAME_KANA + "' " +
                " WHERE " +
                TBL_BRANCHMF.BR_NO + "=" + m_BR_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_BRANCHMF.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_BRANCHMF.BR_NO + "=" + m_BR_NO;
            return strSQL;
        }

        #endregion
    }
}
