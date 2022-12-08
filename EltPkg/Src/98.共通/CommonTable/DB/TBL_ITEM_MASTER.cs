using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 項目定義
    /// </summary>
    public class TBL_ITEM_MASTER
    {
        public TBL_ITEM_MASTER(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_ITEM_MASTER(int itemid, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_ITEM_ID = itemid;
        }

        public TBL_ITEM_MASTER(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "ITEM_MASTER";

        public const string ITEM_ID = "ITEM_ID";
        public const string ITEM_NAME = "ITEM_NAME";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_ITEM_ID = 0;
        public string m_ITEM_NAME = "";

        #endregion

        #region プロパティ

        public int _ITEM_ID
        {
            get { return m_ITEM_ID; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_ITEM_ID = DBConvert.ToIntNull(dr[TBL_ITEM_MASTER.ITEM_ID]);
            m_ITEM_NAME = DBConvert.ToStringNull(dr[TBL_ITEM_MASTER.ITEM_NAME]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_ITEM_MASTER.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_ITEM_MASTER.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_ITEM_MASTER.ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int itemid, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_ITEM_MASTER.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_ITEM_MASTER.ITEM_ID + "=" + itemid;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_ITEM_MASTER.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_ITEM_MASTER.ITEM_ID + "," +
                TBL_ITEM_MASTER.ITEM_NAME + ") VALUES (" +
                m_ITEM_ID + "," +
                "'" + m_ITEM_NAME + "')";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_ITEM_MASTER.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_ITEM_MASTER.ITEM_NAME + "='" + m_ITEM_NAME + "' " +
                " WHERE " +
                TBL_ITEM_MASTER.ITEM_ID + "=" + m_ITEM_ID;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_ITEM_MASTER.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_ITEM_MASTER.ITEM_ID + "=" + m_ITEM_ID;
            return strSQL;
        }

        #endregion
    }
}
