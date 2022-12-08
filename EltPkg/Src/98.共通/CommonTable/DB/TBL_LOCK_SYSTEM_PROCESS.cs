using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// システムプロセスロック
    /// </summary>
    public class TBL_LOCK_SYSTEM_PROCESS
    {
        public TBL_LOCK_SYSTEM_PROCESS()
        {
        }

        public TBL_LOCK_SYSTEM_PROCESS(int gymid, string appid)
        {
            m_GYM_ID = gymid;
            m_APP_ID = appid;
        }

        public TBL_LOCK_SYSTEM_PROCESS(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_PHYSICAL_NAME = "LOCK_SYSTEM_PROCESS";

        public const string GYM_ID = "GYM_ID";
        public const string APP_ID = "APP_ID";
        public const string CREATE_DATE = "CREATE_DATE";
        public const string CREATE_TIME = "CREATE_TIME";

        #endregion

        #region メンバ

        private int m_GYM_ID = 0;
        private string m_APP_ID = "";
        public int m_CREATE_DATE = 0;
        public int m_CREATE_TIME = 0;

        #endregion

        #region プロパティ

        public int _GYM_ID
        {
            get { return m_GYM_ID; }
        }

        public string _APP_ID
        {
            get { return m_APP_ID; }
        }

        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_LOCK_SYSTEM_PROCESS.GYM_ID]);
            m_APP_ID = DBConvert.ToStringNull(dr[TBL_LOCK_SYSTEM_PROCESS.APP_ID]);
            m_CREATE_DATE = DBConvert.ToIntNull(dr[TBL_LOCK_SYSTEM_PROCESS.CREATE_DATE]);
            m_CREATE_TIME = DBConvert.ToIntNull(dr[TBL_LOCK_SYSTEM_PROCESS.CREATE_TIME]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            string strSql = "SELECT * FROM " + TBL_LOCK_SYSTEM_PROCESS.TABLE_NAME +
                " ORDER BY " +
                TBL_LOCK_SYSTEM_PROCESS.GYM_ID + "," + TBL_LOCK_SYSTEM_PROCESS.APP_ID;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, string appid)
        {
            return GetSelectQuery(gymid, appid, false);
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, string appid, bool Lock)
        {
            string strSql = "SELECT * FROM " + TBL_LOCK_SYSTEM_PROCESS.TABLE_NAME +
                " WHERE " +
                TBL_LOCK_SYSTEM_PROCESS.GYM_ID + "=" + gymid + " AND " +
                TBL_LOCK_SYSTEM_PROCESS.APP_ID + "= '" + appid + "' ";
            if (Lock)
            {
                strSql += DBConvert.QUERY_LOCK + " ";
            }
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetInsertQuery(int gymid, string appid)
        {
            string strSql = "INSERT INTO " + TBL_LOCK_SYSTEM_PROCESS.TABLE_NAME + " (" +
                TBL_LOCK_SYSTEM_PROCESS.GYM_ID + "," +
                TBL_LOCK_SYSTEM_PROCESS.APP_ID + "," +
                TBL_LOCK_SYSTEM_PROCESS.CREATE_DATE + "," +
                TBL_LOCK_SYSTEM_PROCESS.CREATE_TIME + ") VALUES (" +
                gymid + "," +
                "'" + appid + "'," +
                System.DateTime.Now.ToString("yyyyMMdd") + "," +
                System.DateTime.Now.ToString("HHmmssfff") + ")";
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_LOCK_SYSTEM_PROCESS.TABLE_NAME + " (" +
                TBL_LOCK_SYSTEM_PROCESS.GYM_ID + "," +
                TBL_LOCK_SYSTEM_PROCESS.APP_ID + "," +
                TBL_LOCK_SYSTEM_PROCESS.CREATE_DATE + "," +
                TBL_LOCK_SYSTEM_PROCESS.CREATE_TIME + ") VALUES (" +
                m_GYM_ID + "," +
                "'" + m_APP_ID + "'," +
                m_CREATE_DATE + "," +
                m_CREATE_TIME + ")";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_LOCK_SYSTEM_PROCESS.TABLE_NAME + " SET " +
                TBL_LOCK_SYSTEM_PROCESS.CREATE_DATE + "=" + m_CREATE_DATE + ", " +
                TBL_LOCK_SYSTEM_PROCESS.CREATE_TIME + "=" + m_CREATE_TIME +
                " WHERE " +
                TBL_LOCK_SYSTEM_PROCESS.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_LOCK_SYSTEM_PROCESS.APP_ID + "= '" + m_APP_ID + "' ";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_LOCK_SYSTEM_PROCESS.TABLE_NAME +
                " WHERE " +
                TBL_LOCK_SYSTEM_PROCESS.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_LOCK_SYSTEM_PROCESS.APP_ID + "= '" + m_APP_ID + "' ";
            return strSql;
        }

        #endregion
    }
}
