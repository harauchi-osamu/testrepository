using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 不渡事由コードマスタ
    /// </summary>
    public class TBL_FUWATARIMF
    {
        public TBL_FUWATARIMF()
        {
        }

        public TBL_FUWATARIMF(int code)
        {
            m_CODE = code;
        }

        public TBL_FUWATARIMF(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_PHYSICAL_NAME = "FUWATARIMF";

        public const string CODE = "CODE";
        public const string KBN_NAME = "KBN_NAME";
        public const string FUBI_JIYUU = "FUBI_JIYUU";
        public const string DESCRIPTION = "DESCRIPTION";
        #endregion

        #region メンバ
        private int m_CODE = 0;
        public string m_KBN_NAME = "";
        public string m_FUBI_JIYUU = "";
        public string m_DESCRIPTION = "";

        #endregion

        #region プロパティ

        public int _CODE
        {
            get { return m_CODE; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_CODE = DBConvert.ToIntNull(dr[TBL_FUWATARIMF.CODE]);
            m_KBN_NAME = DBConvert.ToStringNull(dr[TBL_FUWATARIMF.KBN_NAME]);
            m_FUBI_JIYUU = DBConvert.ToStringNull(dr[TBL_FUWATARIMF.FUBI_JIYUU]);
            m_DESCRIPTION = DBConvert.ToStringNull(dr[TBL_FUWATARIMF.DESCRIPTION]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            string strSQL = "SELECT * FROM " + TBL_FUWATARIMF.TABLE_NAME +
                " ORDER BY " +
                TBL_FUWATARIMF.CODE;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int code)
        {
            string strSQL = "SELECT * FROM " + TBL_FUWATARIMF.TABLE_NAME +
                " WHERE " +
                TBL_FUWATARIMF.CODE + "=" + code;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_FUWATARIMF.TABLE_NAME + " (" +
                TBL_FUWATARIMF.CODE + "," +
                TBL_FUWATARIMF.KBN_NAME + "," +
                TBL_FUWATARIMF.FUBI_JIYUU + "," +
                TBL_FUWATARIMF.DESCRIPTION + ") VALUES (" +
                m_CODE + "," +
                "'" + m_KBN_NAME + "'," +
                "'" + m_FUBI_JIYUU + "'," +
                "'" + m_DESCRIPTION + "')";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_FUWATARIMF.TABLE_NAME + " SET " +
                TBL_FUWATARIMF.KBN_NAME + "='" + m_KBN_NAME + "', " +
                TBL_FUWATARIMF.FUBI_JIYUU + "='" + m_FUBI_JIYUU + "', " +
                TBL_FUWATARIMF.DESCRIPTION + "='" + m_DESCRIPTION + "' " +
                " WHERE " +
                TBL_FUWATARIMF.CODE + "=" + m_CODE;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_FUWATARIMF.TABLE_NAME +
                " WHERE " +
                TBL_FUWATARIMF.CODE + "=" + m_CODE;
            return strSQL;
        }

        #endregion
    }
}
