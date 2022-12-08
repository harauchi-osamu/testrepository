using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// システムプロセスロック
    /// </summary>
    public class TBL_LOCK_BANK_PROCESS
    {
        public TBL_LOCK_BANK_PROCESS(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

        }

        public TBL_LOCK_BANK_PROCESS(int gymid, string appid, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_GYM_ID = gymid;
            m_APP_ID = appid;
        }

        public TBL_LOCK_BANK_PROCESS(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "LOCK_BANK_PROCESS";

        public const string GYM_ID = "GYM_ID";
        public const string APP_ID = "APP_ID";
        public const string CREATE_DATE = "CREATE_DATE";
        public const string CREATE_TIME = "CREATE_TIME";

        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
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
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_LOCK_BANK_PROCESS.GYM_ID]);
            m_APP_ID = DBConvert.ToStringNull(dr[TBL_LOCK_BANK_PROCESS.APP_ID]);
            m_CREATE_DATE = DBConvert.ToIntNull(dr[TBL_LOCK_BANK_PROCESS.CREATE_DATE]);
            m_CREATE_TIME = DBConvert.ToIntNull(dr[TBL_LOCK_BANK_PROCESS.CREATE_TIME]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_LOCK_BANK_PROCESS.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_LOCK_BANK_PROCESS.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_LOCK_BANK_PROCESS.GYM_ID + "," + TBL_LOCK_BANK_PROCESS.APP_ID;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, string appid, int Schemabankcd)
        {
            return GetSelectQuery(gymid, appid, Schemabankcd, false);
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int gymid, string appid, int Schemabankcd, bool Lock)
        {
            string strSql = "SELECT * FROM " + TBL_LOCK_BANK_PROCESS.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_LOCK_BANK_PROCESS.GYM_ID + "=" + gymid + " AND " +
                TBL_LOCK_BANK_PROCESS.APP_ID + "= '" + appid + "' ";
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
        public static string GetInsertQuery(int gymid, string appid, int Schemabankcd)
        {
            string strSql = "INSERT INTO " + TBL_LOCK_BANK_PROCESS.TABLE_NAME(Schemabankcd) + " (" +
                TBL_LOCK_BANK_PROCESS.GYM_ID + "," +
                TBL_LOCK_BANK_PROCESS.APP_ID + "," +
                TBL_LOCK_BANK_PROCESS.CREATE_DATE + "," +
                TBL_LOCK_BANK_PROCESS.CREATE_TIME + ") VALUES (" +
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
            string strSql = "INSERT INTO " + TBL_LOCK_BANK_PROCESS.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_LOCK_BANK_PROCESS.GYM_ID + "," +
                TBL_LOCK_BANK_PROCESS.APP_ID + "," +
                TBL_LOCK_BANK_PROCESS.CREATE_DATE + "," +
                TBL_LOCK_BANK_PROCESS.CREATE_TIME + ") VALUES (" +
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
            string strSql = "UPDATE " + TBL_LOCK_BANK_PROCESS.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_LOCK_BANK_PROCESS.CREATE_DATE + "=" + m_CREATE_DATE + ", " +
                TBL_LOCK_BANK_PROCESS.CREATE_TIME + "=" + m_CREATE_TIME +
                " WHERE " +
                TBL_LOCK_BANK_PROCESS.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_LOCK_BANK_PROCESS.APP_ID + "= '" + m_APP_ID + "' ";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_LOCK_BANK_PROCESS.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_LOCK_BANK_PROCESS.GYM_ID + "=" + m_GYM_ID + " AND " +
                TBL_LOCK_BANK_PROCESS.APP_ID + "= '" + m_APP_ID + "' ";
            return strSql;
        }

        #endregion
    }
}
