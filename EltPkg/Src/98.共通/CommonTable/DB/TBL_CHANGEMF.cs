using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 読替マスタ
    /// </summary>
    public class TBL_CHANGEMF
    {
        public TBL_CHANGEMF(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_CHANGEMF(int oldbrno, int oldaccountno, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_OLD_BR_NO = oldbrno;
            m_OLD_ACCOUNT_NO = oldaccountno;
        }

        public TBL_CHANGEMF(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "CHANGEMF";

        public const string OLD_BR_NO = "OLD_BR_NO";
        public const string OLD_ACCOUNT_NO = "OLD_ACCOUNT_NO";
        public const string NEW_BR_NO = "NEW_BR_NO";
        public const string NEW_ACCOUNT_NO = "NEW_ACCOUNT_NO";
        public const string DESCRIPTION = "DESCRIPTION";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_OLD_BR_NO = 0;
        private long m_OLD_ACCOUNT_NO = 0;
        public int m_NEW_BR_NO = -1;
        public long m_NEW_ACCOUNT_NO = -1;
        public string m_DESCRIPTION = "";

        #endregion

        #region プロパティ

        public int _OLD_BR_NO
        {
            get { return m_OLD_BR_NO; }
        }
        public long _OLD_ACCOUNT_NO
        {
            get { return m_OLD_ACCOUNT_NO; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_OLD_BR_NO = DBConvert.ToIntNull(dr[TBL_CHANGEMF.OLD_BR_NO]);
            m_OLD_ACCOUNT_NO = DBConvert.ToLongNull(dr[TBL_CHANGEMF.OLD_ACCOUNT_NO]);
            m_NEW_BR_NO = DBConvert.ToIntNull(dr[TBL_CHANGEMF.NEW_BR_NO]);
            m_NEW_ACCOUNT_NO = DBConvert.ToLongNull(dr[TBL_CHANGEMF.NEW_ACCOUNT_NO]);
            m_DESCRIPTION = DBConvert.ToStringNull(dr[TBL_CHANGEMF.DESCRIPTION]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_CHANGEMF.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_CHANGEMF.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_CHANGEMF.OLD_BR_NO + "," +
                TBL_CHANGEMF.OLD_ACCOUNT_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int oldbrno, int oldaccountno, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_CHANGEMF.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_CHANGEMF.OLD_BR_NO + "=" + oldbrno + " AND " +
                TBL_CHANGEMF.OLD_ACCOUNT_NO + "=" + oldaccountno;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_CHANGEMF.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_CHANGEMF.OLD_BR_NO + "," +
                TBL_CHANGEMF.OLD_ACCOUNT_NO + "," +
                TBL_CHANGEMF.NEW_BR_NO + "," +
                TBL_CHANGEMF.NEW_ACCOUNT_NO + "," +
                TBL_CHANGEMF.DESCRIPTION + ") VALUES (" +
                m_OLD_BR_NO + "," +
                m_OLD_ACCOUNT_NO + "," +
                m_NEW_BR_NO + "," +
                m_NEW_ACCOUNT_NO + "," +
                "'" + m_DESCRIPTION + "')";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_CHANGEMF.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_CHANGEMF.NEW_BR_NO + "=" + m_NEW_BR_NO + ", " +
                TBL_CHANGEMF.NEW_ACCOUNT_NO + "=" + m_NEW_ACCOUNT_NO + ", " +
                TBL_CHANGEMF.DESCRIPTION + "='" + m_DESCRIPTION + "' " +
                " WHERE " +
                TBL_CHANGEMF.OLD_BR_NO + "=" + m_OLD_BR_NO + " AND " +
                TBL_CHANGEMF.OLD_ACCOUNT_NO + "=" + m_OLD_ACCOUNT_NO;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_CHANGEMF.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_CHANGEMF.OLD_BR_NO + "=" + m_OLD_BR_NO + " AND " +
                TBL_CHANGEMF.OLD_ACCOUNT_NO + "=" + m_OLD_ACCOUNT_NO;
            return strSQL;
        }

        #endregion
    }
}
