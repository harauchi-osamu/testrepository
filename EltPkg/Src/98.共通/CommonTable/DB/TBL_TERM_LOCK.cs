using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 取扱端末ロック
    /// </summary>
    public class TBL_TERM_LOCK
    {
        public TBL_TERM_LOCK()
        {
        }

        public TBL_TERM_LOCK(int LastIPAddress)
        {
            m_LASTIPADDRESS = LastIPAddress;
        }

        public TBL_TERM_LOCK(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_PHYSICAL_NAME = "TERM_LOCK";

        public const string LASTIPADDRESS = "LASTIPADDRESS";
        public const string CREATE_DATE = "CREATE_DATE";
        public const string CREATE_TIME = "CREATE_TIME";
        #endregion

        #region メンバ

        private int m_LASTIPADDRESS = 0;
        public int m_CREATE_DATE = 0;
        public int m_CREATE_TIME = 0;

        #endregion

        #region プロパティ
        public int _LASTIPADDRESS
        {
            get { return m_LASTIPADDRESS; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_LASTIPADDRESS = DBConvert.ToIntNull(dr[TBL_TERM_LOCK.LASTIPADDRESS]);
            m_CREATE_DATE = DBConvert.ToIntNull(dr[TBL_TERM_LOCK.CREATE_DATE]);
            m_CREATE_TIME = DBConvert.ToIntNull(dr[TBL_TERM_LOCK.CREATE_TIME]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int LastIPAddress)
        {
            return GetSelectQuery(LastIPAddress);
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int LastIPAddress, bool Lock)
        {
            string strSql = "SELECT * FROM " + TBL_TERM_LOCK.TABLE_NAME +
                " WHERE " +
                TBL_TERM_LOCK.LASTIPADDRESS + "=" + LastIPAddress + " ";
            if (Lock)
            {
                strSql += DBConvert.QUERY_LOCK + " ";
            }
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetInsertQuery(int LastIPAddress)
        {
            string strSql = "INSERT INTO " + TBL_TERM_LOCK.TABLE_NAME + " (" +
                TBL_TERM_LOCK.LASTIPADDRESS + "," +
                TBL_TERM_LOCK.CREATE_DATE + "," +
                TBL_TERM_LOCK.CREATE_TIME + ") VALUES (" +
                LastIPAddress + "," +
                System.DateTime.Now.ToString("yyyyMMdd") + "," +
                System.DateTime.Now.ToString("HHmmssfff") + ")";
            return strSql;
        }

        #endregion
    }
}
