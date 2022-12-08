using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 休日マスタ
    /// </summary>
    public class TBL_HOLIDAY
    {
        public TBL_HOLIDAY()
        {
           
        }

        public TBL_HOLIDAY(int day)
        {
            m_DAY = day;
        }

        public TBL_HOLIDAY(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "HOLIDAY";
        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
        public const string DAY = "DAY";
        public const string DESCRIPTION = "DESCRIPTION";
        #endregion

        #region メンバ

        private int m_DAY = 0;
        public string m_DESCRIPTION = "";

        #endregion

        #region プロパティ

        public int _DAY
        {
            get { return m_DAY; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_DAY = DBConvert.ToIntNull(dr[TBL_HOLIDAY.DAY]);
            m_DESCRIPTION = DBConvert.ToStringNull(dr[TBL_HOLIDAY.DESCRIPTION]);
        }

        #endregion

        #region テーブル名取得
     

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            string strSQL = "SELECT * FROM " + TBL_HOLIDAY.TABLE_NAME +
                " ORDER BY " +
                TBL_HOLIDAY.DAY;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int day)
        {
            string strSQL = "SELECT * FROM " + TBL_HOLIDAY.TABLE_NAME +
                " WHERE " +
                TBL_HOLIDAY.DAY + "=" + day;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_HOLIDAY.TABLE_NAME + " (" +
                TBL_HOLIDAY.DAY + "," +
                TBL_HOLIDAY.DESCRIPTION + ") VALUES (" +
                m_DAY + "," +
                "'" + m_DESCRIPTION + "')";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_HOLIDAY.TABLE_NAME + " SET " +
                TBL_HOLIDAY.DESCRIPTION + "='" + m_DESCRIPTION + "' " +
                " WHERE " +
                TBL_HOLIDAY.DAY + "=" + m_DAY;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_HOLIDAY.TABLE_NAME +
                " WHERE " +
                TBL_HOLIDAY.DAY + "=" + m_DAY;
            return strSQL;
        }

        #endregion
    }
}
