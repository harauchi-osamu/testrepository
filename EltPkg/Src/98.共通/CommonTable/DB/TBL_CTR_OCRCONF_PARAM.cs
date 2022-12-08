using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 汎用エントリオペレータ処理状況
    /// </summary>
    public class TBL_CTR_OCRCONF_PARAM
    {
        public TBL_CTR_OCRCONF_PARAM(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_CTR_OCRCONF_PARAM(string item_name, string item_type, int ctr_min_conf, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_ITEM_NAME = item_name;
            m_ITEM_TYPE = item_type;
            m_CTR_MIN_CONF = ctr_min_conf;
        }

        public TBL_CTR_OCRCONF_PARAM(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "CTR_OCRCONF_PARAM";

        public const string ITEM_NAME = "ITEM_NAME";
        public const string ITEM_TYPE = "ITEM_TYPE";
        public const string CTR_MIN_CONF = "CTR_MIN_CONF";
        public const string CTR_MAX_CONF = "CTR_MAX_CONF";
        public const string CONF = "CONF";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private string m_ITEM_NAME = "";
        private string m_ITEM_TYPE = "";
        private int m_CTR_MIN_CONF = 0;
        public int m_CTR_MAX_CONF = 0;
        public int m_CONF = 0;
        #endregion

        #region プロパティ

        public string _ITEM_NAME
        {
            get { return m_ITEM_NAME; }
        }
        public string _ITEM_TYPE
        {
            get { return m_ITEM_TYPE; }
        }
        public int _CTR_MIN_CONF
        {
            get { return m_CTR_MIN_CONF; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_ITEM_NAME = DBConvert.ToStringNull(dr[TBL_CTR_OCRCONF_PARAM.ITEM_NAME]);
            m_ITEM_TYPE = DBConvert.ToStringNull(dr[TBL_CTR_OCRCONF_PARAM.ITEM_TYPE]);
            m_CTR_MIN_CONF = DBConvert.ToIntNull(dr[TBL_CTR_OCRCONF_PARAM.CTR_MIN_CONF]);
            m_CTR_MAX_CONF = DBConvert.ToIntNull(dr[TBL_CTR_OCRCONF_PARAM.CTR_MAX_CONF]);
            m_CONF = DBConvert.ToIntNull(dr[TBL_CTR_OCRCONF_PARAM.CONF]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_CTR_OCRCONF_PARAM.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_CTR_OCRCONF_PARAM.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                  TBL_CTR_OCRCONF_PARAM.ITEM_NAME + "," +
                  TBL_CTR_OCRCONF_PARAM.CTR_MIN_CONF + "," +
                  TBL_CTR_OCRCONF_PARAM.ITEM_TYPE;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string item_name, string item_type, int ctr_min_conf, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_CTR_OCRCONF_PARAM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                  TBL_CTR_OCRCONF_PARAM.ITEM_NAME + "='" + item_name + "'" +
                " AND " + TBL_CTR_OCRCONF_PARAM.ITEM_TYPE + "='" + item_type + "'" +
                " AND " + TBL_CTR_OCRCONF_PARAM.CTR_MIN_CONF + "=" + ctr_min_conf;
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_CTR_OCRCONF_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                  TBL_CTR_OCRCONF_PARAM.ITEM_NAME + "," +
                  TBL_CTR_OCRCONF_PARAM.ITEM_TYPE + "," +
                  TBL_CTR_OCRCONF_PARAM.CTR_MIN_CONF + "," +
                  TBL_CTR_OCRCONF_PARAM.CTR_MAX_CONF + "," +
                  TBL_CTR_OCRCONF_PARAM.CONF + ") VALUES (" +
                "'" + m_ITEM_NAME + "'," +
                "'" + m_ITEM_TYPE + "'," +
                m_CTR_MIN_CONF + "," +
                m_CTR_MAX_CONF + "," +
                m_CONF + "," + ")";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_CTR_OCRCONF_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                  TBL_CTR_OCRCONF_PARAM.CTR_MAX_CONF + "=" + m_CTR_MAX_CONF + ", " +
                  TBL_CTR_OCRCONF_PARAM.CONF + "=" + m_CONF +
                " WHERE " +
                  TBL_CTR_OCRCONF_PARAM.ITEM_NAME + "='" + m_ITEM_NAME + "' " +
                  " AND " + TBL_CTR_OCRCONF_PARAM.ITEM_TYPE + "='" + m_ITEM_TYPE + "' " +
                  " AND " + TBL_CTR_OCRCONF_PARAM.CTR_MIN_CONF + "=" + m_CTR_MIN_CONF;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_CTR_OCRCONF_PARAM.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                  TBL_CTR_OCRCONF_PARAM.ITEM_NAME + "='" + m_ITEM_NAME + "' " +
                  " AND " + TBL_CTR_OCRCONF_PARAM.ITEM_TYPE + "='" + m_ITEM_TYPE + "' " +
                  " AND " + TBL_CTR_OCRCONF_PARAM.CTR_MIN_CONF + "=" + m_CTR_MIN_CONF;
            return strSql;
        }

        #endregion
    }
}
