using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 元号マスタ
    /// </summary>
    public class TBL_ERA
    {
        public TBL_ERA()
        {
        }

        public TBL_ERA(int seq)
        {
            m_SEQ = seq;
        }

        public TBL_ERA(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_PHYSICAL_NAME = "ERA";

        public const string SEQ = "SEQ";
        public const string KANJI = "KANJI";
        public const string SHORTKANJI = "SHORTKANJI";
        public const string ROMAN = "ROMAN";
        public const string SHORTROMAN = "SHORTROMAN";
        public const string TOWAREKIYEAR = "TOWAREKIYEAR";
        public const string STARTDATE = "STARTDATE";
        public const string ENDDATE = "ENDDATE";

        #endregion

        #region メンバ

        private int m_SEQ = 0;
        public string m_KANJI = "";
        public string m_SHORTKANJI = "";
        public string m_ROMAN = "";
        public string m_SHORTROMAN = "";
        public int m_TOWAREKIYEAR = 0;
        public int m_STARTDATE = 0;
        public int m_ENDDATE = 0;

        #endregion

        #region プロパティ

        public int _SEQ
        {
            get { return m_SEQ; }
        }

        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_SEQ = DBConvert.ToIntNull(dr[TBL_ERA.SEQ]);
            m_KANJI = DBConvert.ToStringNull(dr[TBL_ERA.KANJI]);
            m_SHORTKANJI = DBConvert.ToStringNull(dr[TBL_ERA.SHORTKANJI]);
            m_ROMAN = DBConvert.ToStringNull(dr[TBL_ERA.ROMAN]);
            m_SHORTROMAN = DBConvert.ToStringNull(dr[TBL_ERA.SHORTROMAN]);
            m_TOWAREKIYEAR = DBConvert.ToIntNull(dr[TBL_ERA.TOWAREKIYEAR]);
            m_STARTDATE = DBConvert.ToIntNull(dr[TBL_ERA.STARTDATE]);
            m_ENDDATE = DBConvert.ToIntNull(dr[TBL_ERA.ENDDATE]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            string strSql = "SELECT * FROM " + TBL_ERA.TABLE_NAME +
                " ORDER BY " +
                TBL_ERA.SEQ;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int seq)
        {
            string strSql = "SELECT * FROM " + TBL_ERA.TABLE_NAME +
                " WHERE " +
                TBL_ERA.SEQ + "=" + seq;
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_ERA.TABLE_NAME + " (" +
                TBL_ERA.SEQ + "," +
                TBL_ERA.KANJI + "," +
                TBL_ERA.SHORTKANJI + "," +
                TBL_ERA.ROMAN + "," +
                TBL_ERA.SHORTROMAN + "," +
                TBL_ERA.TOWAREKIYEAR + "," +
                TBL_ERA.STARTDATE + "," +
                TBL_ERA.ENDDATE + ") VALUES (" +
                m_SEQ + "," +
                "'" + m_KANJI + "'," +
                "'" + m_SHORTKANJI + "'," +
                "'" + m_ROMAN + "'," +
                "'" + m_SHORTROMAN + "'," +
                m_TOWAREKIYEAR + "," +
                m_STARTDATE + "," +
                m_ENDDATE + ")";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_ERA.TABLE_NAME + " SET " +
                TBL_ERA.SEQ + "=" + m_SEQ + ", " +
                TBL_ERA.KANJI + "='" + m_KANJI + "', " +
                TBL_ERA.SHORTKANJI + "='" + m_SHORTKANJI + "', " +
                TBL_ERA.ROMAN + "='" + m_ROMAN + "', " +
                TBL_ERA.SHORTROMAN + "='" + m_SHORTROMAN + "', " +
                TBL_ERA.TOWAREKIYEAR + "=" + m_TOWAREKIYEAR + ", " +
                TBL_ERA.STARTDATE + "=" + m_STARTDATE + ", " +
                TBL_ERA.ENDDATE + "=" + m_ENDDATE +
                " WHERE " +
                TBL_ERA.SEQ + "=" + m_SEQ;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_ERA.TABLE_NAME +
                " WHERE " +
                TBL_ERA.SEQ + "=" + m_SEQ;
            return strSql;
        }

        #endregion
    }
}
