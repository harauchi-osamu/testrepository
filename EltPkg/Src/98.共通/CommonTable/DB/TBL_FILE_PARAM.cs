using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// ファイルパラメータ
    /// </summary>
    public class TBL_FILE_PARAM
    {
        public TBL_FILE_PARAM(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_FILE_PARAM(string fileid, string filedivid, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_FILE_ID = fileid;
            m_FILE_DIVID = filedivid;
        }

        public TBL_FILE_PARAM(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "FILE_PARAM";

        public const string FILE_ID = "FILE_ID";
        public const string FILE_DIVID = "FILE_DIVID";
        public const string FILE_NAME = "FILE_NAME";
        public const string FILE_COURSE = "FILE_COURSE";
        public const string FILE_TYPE = "FILE_TYPE";
        public const string FILE_LENGTH = "FILE_LENGTH";
        public const string FILE_EXTENSION = "FILE_EXTENSION";
        public const string RET_FILE_ID = "RET_FILE_ID";
        public const string RET_FILE_DIVID = "RET_FILE_DIVID";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private string m_FILE_ID = "";
        private string m_FILE_DIVID = "";
        public string m_FILE_NAME = "";
        public int m_FILE_COURSE = -1;
        public int m_FILE_TYPE = -1;
        public int m_FILE_LENGTH = 0;
        public string m_FILE_EXTENSION = "";
        public string m_RET_FILE_ID = "";
        public string m_RET_FILE_DIVID = "";

        #endregion

        #region プロパティ

        public string _FILE_ID
        {
            get { return m_FILE_ID; }
        }
        public string _FILE_DIVID
        {
            get { return m_FILE_DIVID; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_FILE_ID = DBConvert.ToStringNull(dr[TBL_FILE_PARAM.FILE_ID]);
            m_FILE_DIVID = DBConvert.ToStringNull(dr[TBL_FILE_PARAM.FILE_DIVID]);
            m_FILE_NAME = DBConvert.ToStringNull(dr[TBL_FILE_PARAM.FILE_NAME]);
            m_FILE_COURSE = DBConvert.ToIntNull(dr[TBL_FILE_PARAM.FILE_COURSE]);
            m_FILE_TYPE = DBConvert.ToIntNull(dr[TBL_FILE_PARAM.FILE_TYPE]);
            m_FILE_LENGTH = DBConvert.ToIntNull(dr[TBL_FILE_PARAM.FILE_LENGTH]);
            m_FILE_EXTENSION = DBConvert.ToStringNull(dr[TBL_FILE_PARAM.FILE_EXTENSION]);
            m_RET_FILE_ID = DBConvert.ToStringNull(dr[TBL_FILE_PARAM.RET_FILE_ID]);
            m_RET_FILE_DIVID = DBConvert.ToStringNull(dr[TBL_FILE_PARAM.RET_FILE_DIVID]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_FILE_PARAM.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_FILE_PARAM.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_FILE_PARAM.FILE_ID + "," +
                TBL_FILE_PARAM.FILE_DIVID;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string fileid, string filedivid, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_FILE_PARAM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_FILE_PARAM.FILE_ID + "='" + fileid + "' AND " +
                TBL_FILE_PARAM.FILE_DIVID + "='" + filedivid + "' ";
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_FILE_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_FILE_PARAM.FILE_ID + "," +
                TBL_FILE_PARAM.FILE_DIVID + "," +
                TBL_FILE_PARAM.FILE_NAME + "," +
                TBL_FILE_PARAM.FILE_COURSE + "," +
                TBL_FILE_PARAM.FILE_TYPE + "," +
                TBL_FILE_PARAM.FILE_LENGTH + "," +
                TBL_FILE_PARAM.FILE_EXTENSION + "," +
                TBL_FILE_PARAM.RET_FILE_ID + "," +
                TBL_FILE_PARAM.RET_FILE_DIVID + ") VALUES (" +
                "'" + m_FILE_ID + "'," +
                "'" + m_FILE_DIVID + "'," +
                "'" + m_FILE_NAME + "'," +
                m_FILE_COURSE + "," +
                m_FILE_TYPE + "," +
                m_FILE_LENGTH + "," +
                "'" + m_FILE_EXTENSION + "'," +
                "'" + m_RET_FILE_ID + "'," +
                "'" + m_RET_FILE_DIVID + "')";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_FILE_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_FILE_PARAM.FILE_NAME + "='" + m_FILE_NAME + "', " +
                TBL_FILE_PARAM.FILE_COURSE + "=" + m_FILE_COURSE + ", " +
                TBL_FILE_PARAM.FILE_TYPE + "=" + m_FILE_TYPE + ", " +
                TBL_FILE_PARAM.FILE_LENGTH + "=" + m_FILE_LENGTH + ", " +
                TBL_FILE_PARAM.FILE_EXTENSION + "='" + m_FILE_EXTENSION + "', " +
                TBL_FILE_PARAM.RET_FILE_ID + "='" + m_RET_FILE_ID + "', " +
                TBL_FILE_PARAM.RET_FILE_DIVID + "='" + m_RET_FILE_DIVID + "' " +
                " WHERE " +
                TBL_FILE_PARAM.FILE_ID + "='" + m_FILE_ID + "' AND " +
                TBL_FILE_PARAM.FILE_DIVID + "='" + m_FILE_DIVID + "' ";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_FILE_PARAM.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_FILE_PARAM.FILE_ID + "='" + m_FILE_ID + "' AND " +
                TBL_FILE_PARAM.FILE_DIVID + "='" + m_FILE_DIVID + "' ";
            return strSql;
        }

        #endregion
    }
}
