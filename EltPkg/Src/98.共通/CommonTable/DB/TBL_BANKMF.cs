using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 銀行マスタ
    /// </summary>
    public class TBL_BANKMF
    {
        public TBL_BANKMF()
        {
        }

        public TBL_BANKMF(int bkno)
        {
            m_BK_NO = bkno;
        }

        public TBL_BANKMF(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_PHYSICAL_NAME = "BANKMF";

        public const string BK_NO = "BK_NO";
        public const string BK_NAME_KANJI = "BK_NAME_KANJI";
        public const string BK_NAME_KANA = "BK_NAME_KANA";
        public const string KAMEI_FLG = "KAMEI_FLG";
        #endregion

        #region メンバ
        private int m_BK_NO = 0;
        public string m_BK_NAME_KANJI = "";
        public string m_BK_NAME_KANA = "";
        public int m_KAMEI_FLG = 0;

        #endregion

        #region プロパティ

        public int _BK_NO
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
            m_BK_NO = DBConvert.ToIntNull(dr[TBL_BANKMF.BK_NO]);
            m_BK_NAME_KANJI = DBConvert.ToStringNull(dr[TBL_BANKMF.BK_NAME_KANJI]);
            m_BK_NAME_KANA = DBConvert.ToStringNull(dr[TBL_BANKMF.BK_NAME_KANA]);
            m_KAMEI_FLG = DBConvert.ToIntNull(dr[TBL_BANKMF.KAMEI_FLG]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            string strSQL = "SELECT * FROM " + TBL_BANKMF.TABLE_NAME +
                " ORDER BY " +
                TBL_BANKMF.BK_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int bkno)
        {
            string strSQL = "SELECT * FROM " + TBL_BANKMF.TABLE_NAME +
                " WHERE " +
                TBL_BANKMF.BK_NO + "=" + bkno;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_BANKMF.TABLE_NAME + " (" +
                TBL_BANKMF.BK_NO + "," +
                TBL_BANKMF.BK_NAME_KANJI + "," +
                TBL_BANKMF.BK_NAME_KANA + "," +
                TBL_BANKMF.KAMEI_FLG + ") VALUES (" +
                m_BK_NO + "," +
                "'" + m_BK_NAME_KANJI + "'," +
                "'" + m_BK_NAME_KANA + "'," +
                m_KAMEI_FLG + ")";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_BANKMF.TABLE_NAME + " SET " +
                TBL_BANKMF.BK_NAME_KANJI + "='" + m_BK_NAME_KANJI + "', " +
                TBL_BANKMF.BK_NAME_KANA + "='" + m_BK_NAME_KANA + "', " +
                TBL_BANKMF.KAMEI_FLG + "=" + m_KAMEI_FLG + " " +
                " WHERE " +
                TBL_BANKMF.BK_NO + "=" + m_BK_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_BANKMF.TABLE_NAME +
                " WHERE " +
                TBL_BANKMF.BK_NO + "=" + m_BK_NO;
            return strSQL;
        }

        #endregion
    }
}
