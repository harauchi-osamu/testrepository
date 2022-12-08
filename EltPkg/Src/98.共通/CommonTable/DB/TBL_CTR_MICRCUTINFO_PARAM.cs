using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 電子交換所OCR切出情報パラメータ
    /// </summary>
    public class TBL_CTR_MICRCUTINFO_PARAM
    {
        public TBL_CTR_MICRCUTINFO_PARAM(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_CTR_MICRCUTINFO_PARAM(string item_name, int seq, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_ITEM_NAME = item_name;
            m_SEQ = seq;
        }

        public TBL_CTR_MICRCUTINFO_PARAM(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "CTR_MICRCUTINFO_PARAM";

        public const string ITEM_NAME = "ITEM_NAME";
        public const string SEQ = "SEQ";
        public const string REGEXPATTERN = "REGEXPATTERN";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private string m_ITEM_NAME = "";
        private int m_SEQ = 0;
        public string m_REGEXPATTERN = "";
        #endregion

        #region プロパティ

        public string _ITEM_NAME
        {
            get { return m_ITEM_NAME; }
        }
        public int _SEQ
        {
            get { return m_SEQ; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_ITEM_NAME = DBConvert.ToStringNull(dr[TBL_CTR_MICRCUTINFO_PARAM.ITEM_NAME]);
            m_SEQ = DBConvert.ToIntNull(dr[TBL_CTR_MICRCUTINFO_PARAM.SEQ]);
            m_REGEXPATTERN = DBConvert.ToStringNull(dr[TBL_CTR_MICRCUTINFO_PARAM.REGEXPATTERN]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_CTR_MICRCUTINFO_PARAM.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_CTR_MICRCUTINFO_PARAM.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                  TBL_CTR_MICRCUTINFO_PARAM.ITEM_NAME + "," +
                  TBL_CTR_MICRCUTINFO_PARAM.SEQ;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string item_name, int seq, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_CTR_MICRCUTINFO_PARAM.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                  TBL_CTR_MICRCUTINFO_PARAM.ITEM_NAME + "='" + item_name + "' " +
                " AND " + TBL_CTR_MICRCUTINFO_PARAM.SEQ + "=" + seq;
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_CTR_MICRCUTINFO_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                  TBL_CTR_MICRCUTINFO_PARAM.ITEM_NAME + "," +
                  TBL_CTR_MICRCUTINFO_PARAM.SEQ + "," +
                  TBL_CTR_MICRCUTINFO_PARAM.REGEXPATTERN + ") VALUES (" +
                "'" + m_ITEM_NAME + "'," +
                m_SEQ + "," +
                "'" + m_REGEXPATTERN + "'" + ")";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_CTR_MICRCUTINFO_PARAM.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                  TBL_CTR_MICRCUTINFO_PARAM.REGEXPATTERN + "='" + m_REGEXPATTERN + "' " +
                " WHERE " +
                  TBL_CTR_MICRCUTINFO_PARAM.ITEM_NAME + "='" + m_ITEM_NAME + "' " + " AND " +
                  TBL_CTR_MICRCUTINFO_PARAM.SEQ + "=" + m_SEQ;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_CTR_MICRCUTINFO_PARAM.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                  TBL_CTR_MICRCUTINFO_PARAM.ITEM_NAME + "='" + m_ITEM_NAME + "' " + " AND " +
                  TBL_CTR_MICRCUTINFO_PARAM.SEQ + "=" + m_SEQ;
            return strSql;
        }

        #endregion
    }
}
