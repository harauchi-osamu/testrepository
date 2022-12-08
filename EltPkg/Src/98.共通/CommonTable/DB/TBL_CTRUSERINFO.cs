using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 電子交換ユーザー情報マスタ
    /// </summary>
    public class TBL_CTRUSERINFO
    {
        public TBL_CTRUSERINFO(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_CTRUSERINFO(string userid, int sdate, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_USERID = userid;
            m_S_DATE = sdate;
        }

        public TBL_CTRUSERINFO(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        /// <summary>
        /// 基準データから主キー項目を置き換えるインスタンス作成処理
        /// </summary>
        /// <param name="dr"></param>
        public TBL_CTRUSERINFO(string userid, int sdate, TBL_CTRUSERINFO baseinfo, int Schemabankcd) : this(userid, sdate, Schemabankcd)
        {
            m_PASSWORD = baseinfo.m_PASSWORD;
            m_E_DATE = baseinfo.m_E_DATE;
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "CTRUSERINFO";

        public const string USERID = "USERID";
        public const string S_DATE = "S_DATE";
        public const string PASSWORD = "PASSWORD";
        public const string E_DATE = "E_DATE";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private string m_USERID = "";
        private int m_S_DATE = 0;
        public string m_PASSWORD = "";
        public int m_E_DATE = 99991231;

        #endregion

        #region プロパティ

        public string _USERID
        {
            get { return m_USERID; }
        }
        public int _S_DATE
        {
            get { return m_S_DATE; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_USERID = DBConvert.ToStringNull(dr[TBL_CTRUSERINFO.USERID]);
            m_S_DATE = DBConvert.ToIntNull(dr[TBL_CTRUSERINFO.S_DATE]);
            m_PASSWORD = DBConvert.ToStringNull(dr[TBL_CTRUSERINFO.PASSWORD]);
            m_E_DATE = DBConvert.ToIntNull(dr[TBL_CTRUSERINFO.E_DATE]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_CTRUSERINFO.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_CTRUSERINFO.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_CTRUSERINFO.USERID + "," +
                TBL_CTRUSERINFO.S_DATE;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string userid, int sdate, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_CTRUSERINFO.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_CTRUSERINFO.USERID + "='" + userid + "' AND " +
                TBL_CTRUSERINFO.S_DATE + "=" + sdate;
            return strSQL;
        }

        /// <summary>
        /// 日付を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryDate(int date, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_CTRUSERINFO.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_CTRUSERINFO.S_DATE + " <= " + date + " AND " +
                TBL_CTRUSERINFO.E_DATE + " >= " + date + " ";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_CTRUSERINFO.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_CTRUSERINFO.USERID + "," +
                TBL_CTRUSERINFO.S_DATE + "," +
                TBL_CTRUSERINFO.PASSWORD + "," +
                TBL_CTRUSERINFO.E_DATE + ") VALUES (" +
                "'" + m_USERID + "'," +
                m_S_DATE + "," +
                "'" + m_PASSWORD + "'," +
                m_E_DATE + ")";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_CTRUSERINFO.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_CTRUSERINFO.PASSWORD + "='" + m_PASSWORD + "', " +
                TBL_CTRUSERINFO.E_DATE + "=" + m_E_DATE +
                " WHERE " +
                TBL_CTRUSERINFO.USERID + "='" + m_USERID + "' AND " +
                TBL_CTRUSERINFO.S_DATE + "=" + m_S_DATE;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_CTRUSERINFO.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_CTRUSERINFO.USERID + "='" + m_USERID + "' AND " +
                TBL_CTRUSERINFO.S_DATE + "=" + m_S_DATE;
            return strSQL;
        }

        #endregion
    }
}
