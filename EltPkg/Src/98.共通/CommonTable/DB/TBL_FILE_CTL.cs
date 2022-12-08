using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// ファイル集配信管理
    /// </summary>
    public class TBL_FILE_CTL
    {
        public TBL_FILE_CTL(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_FILE_CTL(string fileid, string filedivid, string sendfilename, string capfilename, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_FILE_ID = fileid;
            m_FILE_DIVID = filedivid;
            m_SEND_FILE_NAME = sendfilename;
            m_CAP_FILE_NAME = capfilename;
        }

        public TBL_FILE_CTL(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "FILE_CTL";

        public const string FILE_ID = "FILE_ID";
        public const string FILE_DIVID = "FILE_DIVID";
        public const string SEND_FILE_NAME = "SEND_FILE_NAME";
        public const string SEND_FILE_LENGTH = "SEND_FILE_LENGTH";
        public const string SEND_STS = "SEND_STS";
        public const string MAKE_OPENO = "MAKE_OPENO";
        public const string MAKE_DATE = "MAKE_DATE";
        public const string MAKE_TIME = "MAKE_TIME";
        public const string SEND_DATE = "SEND_DATE";
        public const string SEND_TIME = "SEND_TIME";
        public const string CAP_FILE_NAME = "CAP_FILE_NAME";
        public const string CAP_FILE_LENGTH = "CAP_FILE_LENGTH";
        public const string CAP_STS = "CAP_STS";
        public const string CAP_DATE = "CAP_DATE";
        public const string CAP_TIME = "CAP_TIME";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private string m_FILE_ID = "";
        private string m_FILE_DIVID = "";
        private string m_SEND_FILE_NAME = "";
        public long m_SEND_FILE_LENGTH = 0;
        public int m_SEND_STS = 0;
        public string m_MAKE_OPENO = "";
        public int m_MAKE_DATE = 0;
        public int m_MAKE_TIME = 0;
        public int m_SEND_DATE = 0;
        public int m_SEND_TIME = 0;
        private string m_CAP_FILE_NAME = "";
        public long m_CAP_FILE_LENGTH = 0;
        public int m_CAP_STS = 0;
        public int m_CAP_DATE = 0;
        public int m_CAP_TIME = 0;

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
        public string _SEND_FILE_NAME
        {
            get { return m_SEND_FILE_NAME; }
        }
        public string _CAP_FILE_NAME
        {
            get { return m_CAP_FILE_NAME; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_FILE_ID = DBConvert.ToStringNull(dr[TBL_FILE_CTL.FILE_ID]);
            m_FILE_DIVID = DBConvert.ToStringNull(dr[TBL_FILE_CTL.FILE_DIVID]);
            m_SEND_FILE_NAME = DBConvert.ToStringNull(dr[TBL_FILE_CTL.SEND_FILE_NAME]);
            m_SEND_FILE_LENGTH = DBConvert.ToLongNull(dr[TBL_FILE_CTL.SEND_FILE_LENGTH]);
            m_SEND_STS = DBConvert.ToIntNull(dr[TBL_FILE_CTL.SEND_STS]);
            m_MAKE_OPENO = DBConvert.ToStringNull(dr[TBL_FILE_CTL.MAKE_OPENO]);
            m_MAKE_DATE = DBConvert.ToIntNull(dr[TBL_FILE_CTL.MAKE_DATE]);
            m_MAKE_TIME = DBConvert.ToIntNull(dr[TBL_FILE_CTL.MAKE_TIME]);
            m_SEND_DATE = DBConvert.ToIntNull(dr[TBL_FILE_CTL.SEND_DATE]);
            m_SEND_TIME = DBConvert.ToIntNull(dr[TBL_FILE_CTL.SEND_TIME]);
            m_CAP_FILE_NAME = DBConvert.ToStringNull(dr[TBL_FILE_CTL.CAP_FILE_NAME]);
            m_CAP_FILE_LENGTH = DBConvert.ToLongNull(dr[TBL_FILE_CTL.CAP_FILE_LENGTH]);
            m_CAP_STS = DBConvert.ToIntNull(dr[TBL_FILE_CTL.CAP_STS]);
            m_CAP_DATE = DBConvert.ToIntNull(dr[TBL_FILE_CTL.CAP_DATE]);
            m_CAP_TIME = DBConvert.ToIntNull(dr[TBL_FILE_CTL.CAP_TIME]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_FILE_CTL.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_FILE_CTL.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_FILE_CTL.FILE_ID + "," +
                TBL_FILE_CTL.FILE_DIVID + "," +
                TBL_FILE_CTL.SEND_FILE_NAME + "," +
                TBL_FILE_CTL.CAP_FILE_NAME;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string fileid, string filedivid, string sendfilename, string capfilename, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_FILE_CTL.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_FILE_CTL.FILE_ID + "='" + fileid + "' AND " +
                TBL_FILE_CTL.FILE_DIVID + "='" + filedivid + "' AND " +
                TBL_FILE_CTL.SEND_FILE_NAME + "='" + sendfilename + "' AND " +
                TBL_FILE_CTL.CAP_FILE_NAME + "='" + capfilename + "' ";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_FILE_CTL.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_FILE_CTL.FILE_ID + "," +
                TBL_FILE_CTL.FILE_DIVID + "," +
                TBL_FILE_CTL.SEND_FILE_NAME + "," +
                TBL_FILE_CTL.SEND_FILE_LENGTH + "," +
                TBL_FILE_CTL.SEND_STS + "," +
                TBL_FILE_CTL.MAKE_OPENO + "," +
                TBL_FILE_CTL.MAKE_DATE + "," +
                TBL_FILE_CTL.MAKE_TIME + "," +
                TBL_FILE_CTL.SEND_DATE + "," +
                TBL_FILE_CTL.SEND_TIME + "," +
                TBL_FILE_CTL.CAP_FILE_NAME + "," +
                TBL_FILE_CTL.CAP_FILE_LENGTH + "," +
                TBL_FILE_CTL.CAP_STS + "," +
                TBL_FILE_CTL.CAP_DATE + "," +
                TBL_FILE_CTL.CAP_TIME + ") VALUES (" +
                "'" + m_FILE_ID + "'," +
                "'" + m_FILE_DIVID + "'," +
                "'" + m_SEND_FILE_NAME + "'," +
                m_SEND_FILE_LENGTH + "," +
                m_SEND_STS + "," +
                "'" + m_MAKE_OPENO + "'," +
                m_MAKE_DATE + "," +
                m_MAKE_TIME + "," +
                m_SEND_DATE + "," +
                m_SEND_TIME + "," +
                "'" + m_CAP_FILE_NAME + "'," +
                m_CAP_FILE_LENGTH + "," +
                m_CAP_STS + "," +
                m_CAP_DATE + "," +
                m_CAP_TIME + ")";
            return strSQL;
        }


        /// <summary>
        /// INSERT文を作成します
        /// 取込データ
        /// </summary>
        /// <returns></returns>
        public string GetInsertQueryCAPData()
        {
            string strSql = "INSERT INTO " + TBL_FILE_CTL.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_FILE_CTL.FILE_ID + "," +
                TBL_FILE_CTL.FILE_DIVID + "," +
                TBL_FILE_CTL.SEND_FILE_NAME + "," +
                TBL_FILE_CTL.SEND_STS + "," +
                TBL_FILE_CTL.CAP_FILE_NAME + "," +
                TBL_FILE_CTL.CAP_FILE_LENGTH + "," +
                TBL_FILE_CTL.CAP_STS + "," +
                TBL_FILE_CTL.CAP_DATE + "," +
                TBL_FILE_CTL.CAP_TIME + ") VALUES (" +
                "'" + m_FILE_ID + "'," +
                "'" + m_FILE_DIVID + "'," +
                "'" + m_SEND_FILE_NAME + "'," +
                m_SEND_STS + "," +
                "'" + m_CAP_FILE_NAME + "'," +
                m_CAP_FILE_LENGTH + "," +
                m_CAP_STS + "," +
                m_CAP_DATE + "," +
                m_CAP_TIME + ")";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_FILE_CTL.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_FILE_CTL.SEND_FILE_LENGTH + "=" + m_SEND_FILE_LENGTH + ", " +
                TBL_FILE_CTL.SEND_STS + "=" + m_SEND_STS + ", " +
                TBL_FILE_CTL.MAKE_OPENO + "='" + m_MAKE_OPENO + "', " +
                TBL_FILE_CTL.MAKE_DATE + "=" + m_MAKE_DATE + ", " +
                TBL_FILE_CTL.MAKE_TIME + "=" + m_MAKE_TIME + ", " +
                TBL_FILE_CTL.SEND_DATE + "=" + m_SEND_DATE + ", " +
                TBL_FILE_CTL.SEND_TIME + "=" + m_SEND_TIME + ", " +
                TBL_FILE_CTL.CAP_FILE_LENGTH + "=" + m_CAP_FILE_LENGTH + ", " +
                TBL_FILE_CTL.CAP_STS + "=" + m_CAP_STS + ", " +
                TBL_FILE_CTL.CAP_DATE + "=" + m_CAP_DATE + ", " +
                TBL_FILE_CTL.CAP_TIME + "=" + m_CAP_TIME +
                " WHERE " +
                TBL_FILE_CTL.FILE_ID + "='" + m_FILE_ID + "' AND " +
                TBL_FILE_CTL.FILE_DIVID + "='" + m_FILE_DIVID + "' AND " +
                TBL_FILE_CTL.SEND_FILE_NAME + "='" + m_SEND_FILE_NAME + "' AND " +
                TBL_FILE_CTL.CAP_FILE_NAME + "='" + m_CAP_FILE_NAME + "' ";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// 取込データ箇所(ステータス・ファイルサイズ)
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQueryCAPFilesizeData()
        {
            string strSql = "UPDATE " + TBL_FILE_CTL.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_FILE_CTL.CAP_FILE_LENGTH + "=" + m_CAP_FILE_LENGTH + ", " +
                TBL_FILE_CTL.CAP_STS + "=" + m_CAP_STS + " " +
                " WHERE " +
                TBL_FILE_CTL.FILE_ID + "='" + m_FILE_ID + "' AND " +
                TBL_FILE_CTL.FILE_DIVID + "='" + m_FILE_DIVID + "' AND " +
                TBL_FILE_CTL.SEND_FILE_NAME + "='" + m_SEND_FILE_NAME + "' AND " +
                TBL_FILE_CTL.CAP_FILE_NAME + "='" + m_CAP_FILE_NAME + "' ";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// 取込データ箇所(ステータス・取込日)
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQueryCAPSTSData()
        {
            string strSql = "UPDATE " + TBL_FILE_CTL.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_FILE_CTL.CAP_STS + "=" + m_CAP_STS + ", " +
                TBL_FILE_CTL.CAP_DATE + "=" + m_CAP_DATE + ", " +
                TBL_FILE_CTL.CAP_TIME + "=" + m_CAP_TIME + " " +
                " WHERE " +
                TBL_FILE_CTL.FILE_ID + "='" + m_FILE_ID + "' AND " +
                TBL_FILE_CTL.FILE_DIVID + "='" + m_FILE_DIVID + "' AND " +
                TBL_FILE_CTL.SEND_FILE_NAME + "='" + m_SEND_FILE_NAME + "' AND " +
                TBL_FILE_CTL.CAP_FILE_NAME + "='" + m_CAP_FILE_NAME + "' ";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_FILE_CTL.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_FILE_CTL.FILE_ID + "='" + m_FILE_ID + "' AND " +
                TBL_FILE_CTL.FILE_DIVID + "='" + m_FILE_DIVID + "' AND " +
                TBL_FILE_CTL.SEND_FILE_NAME + "='" + m_SEND_FILE_NAME + "' AND " +
                TBL_FILE_CTL.CAP_FILE_NAME + "='" + m_CAP_FILE_NAME + "' ";
            return strSQL;
        }

        #endregion
    }
}
