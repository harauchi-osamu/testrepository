using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 項目トランザクション
    /// </summary>
    public class TBL_GYM_PARAM
    {
        public TBL_GYM_PARAM(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_GYM_PARAM(int gymid, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_GYM_ID = gymid;
        }

        public TBL_GYM_PARAM(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "GYM_PARAM";

        public const string GYM_ID = "GYM_ID";
        public const string GYM_KANA = "GYM_KANA";
        public const string GYM_KANJI = "GYM_KANJI";
        public const string DONE_FLG = "DONE_FLG";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_GYM_ID = 0;
        public string m_GYM_KANA = "";
        public string m_GYM_KANJI = "";
        public string m_DONE_FLG = "0";

        #endregion

        #region プロパティ

        public int _GYM_ID
        {
            get { return m_GYM_ID; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_GYM_PARAM.GYM_ID]);
            m_GYM_KANA = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.GYM_KANA]);
            m_GYM_KANJI = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.GYM_KANJI]);
            m_DONE_FLG = DBConvert.ToStringNull(dr[TBL_GYM_PARAM.DONE_FLG]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_GYM_PARAM.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_GYM_PARAM.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_GYM_PARAM.GYM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_GYM_PARAM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_GYM_PARAM.GYM_ID + "=" + gymid;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_GYM_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_GYM_PARAM.GYM_ID + "," +
                TBL_GYM_PARAM.GYM_KANA + "," +
                TBL_GYM_PARAM.GYM_KANJI + "," +
                TBL_GYM_PARAM.DONE_FLG +  ") VALUES (" +
                m_GYM_ID + "," +
                "'" + m_GYM_KANA + "'," +
                "'" + m_GYM_KANJI + "'," +
                "'" + m_DONE_FLG + "')";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_GYM_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_GYM_PARAM.GYM_KANA + "='" + m_GYM_KANA + "', " +
                TBL_GYM_PARAM.GYM_KANJI + "='" + m_GYM_KANJI + "', " +
                TBL_GYM_PARAM.DONE_FLG + "='" + m_DONE_FLG + "' " +
                " WHERE " +
                TBL_GYM_PARAM.GYM_ID + "=" + m_GYM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_GYM_PARAM.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_GYM_PARAM.GYM_ID + "=" + m_GYM_ID;
            return strSQL;
        }

        #endregion
    }
}
