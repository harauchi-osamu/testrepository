using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// オペレータマスタ
    /// </summary>
    public class TBL_OPERATOR
    {
        public TBL_OPERATOR(string openo)
        {
            m_OPENO = openo;
        }

        public TBL_OPERATOR(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_PHYSICAL_NAME = "OPERATOR";

        public const string OPENO = "OPENO";
        public const string OPENAME = "OPENAME";
        public const string AUTH = "AUTH";
        #endregion

        #region メンバ

        private string m_OPENO = "";
        public string m_OPENAME = "";
        public int m_AUTH = 0;

        #endregion

        #region プロパティ

        public string _OPENO
        {
            get { return m_OPENO; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_OPENO = DBConvert.ToStringNull(dr[TBL_OPERATOR.OPENO]);
            m_OPENAME = DBConvert.ToStringNull(dr[TBL_OPERATOR.OPENAME]);
            m_AUTH = DBConvert.ToIntNull(dr[TBL_OPERATOR.AUTH]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            string strSQL = "SELECT * FROM " + TBL_OPERATOR.TABLE_NAME +
                " ORDER BY " +
                TBL_OPERATOR.OPENO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string OPENO)
        {
            string strSql = "SELECT * FROM " + TBL_OPERATOR.TABLE_NAME +
                " WHERE " +
                        TBL_OPERATOR.OPENO + "='" + OPENO + "' ";
            return strSql;
        }

        #endregion
    }
}
