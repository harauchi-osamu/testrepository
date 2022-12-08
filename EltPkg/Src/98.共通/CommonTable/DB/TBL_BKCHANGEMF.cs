using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 銀行読替マスタ
    /// </summary>
    public class TBL_BKCHANGEMF
    {
        public TBL_BKCHANGEMF()
        {
        }

        public TBL_BKCHANGEMF(int oldbkno)
        {
            m_OLD_BK_NO = oldbkno;
        }

        public TBL_BKCHANGEMF(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_PHYSICAL_NAME = "BKCHANGEMF";

        public const string OLD_BK_NO = "OLD_BK_NO";
        public const string NEW_BK_NO = "NEW_BK_NO";
        public const string DESCRIPTION = "DESCRIPTION";
        #endregion

        #region メンバ
        private int m_OLD_BK_NO = 0;
        public int m_NEW_BK_NO = -1;
        public string m_DESCRIPTION = "";

        #endregion

        #region プロパティ

        public int _OLD_BK_NO
        {
            get { return m_OLD_BK_NO; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_OLD_BK_NO = DBConvert.ToIntNull(dr[TBL_BKCHANGEMF.OLD_BK_NO]);
            m_NEW_BK_NO = DBConvert.ToIntNull(dr[TBL_BKCHANGEMF.NEW_BK_NO]);
            m_DESCRIPTION = DBConvert.ToStringNull(dr[TBL_BKCHANGEMF.DESCRIPTION]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            string strSQL = "SELECT * FROM " + TBL_BKCHANGEMF.TABLE_NAME +
                " ORDER BY " +
                TBL_BKCHANGEMF.OLD_BK_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int oldbkno)
        {
            string strSQL = "SELECT * FROM " + TBL_BKCHANGEMF.TABLE_NAME +
                " WHERE " +
                TBL_BKCHANGEMF.OLD_BK_NO + "=" + oldbkno;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_BKCHANGEMF.TABLE_NAME + " (" +
                TBL_BKCHANGEMF.OLD_BK_NO + "," +
                TBL_BKCHANGEMF.NEW_BK_NO + "," +
                TBL_BKCHANGEMF.DESCRIPTION + ") VALUES (" +
                m_OLD_BK_NO + "," +
                m_NEW_BK_NO + "," +
                "'" + m_DESCRIPTION + "')";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_BKCHANGEMF.TABLE_NAME + " SET " +
                TBL_BKCHANGEMF.NEW_BK_NO + "=" + m_NEW_BK_NO + ", " +
                TBL_BKCHANGEMF.DESCRIPTION + "='" + m_DESCRIPTION + "' " +
                " WHERE " +
                TBL_BKCHANGEMF.OLD_BK_NO + "=" + m_OLD_BK_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_BKCHANGEMF.TABLE_NAME +
                " WHERE " +
                TBL_BKCHANGEMF.OLD_BK_NO + "=" + m_OLD_BK_NO;
            return strSQL;
        }

        #endregion
    }
}
