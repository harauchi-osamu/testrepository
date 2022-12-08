using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 画面番号変換マスタ
    /// </summary>
    public class TBL_CHANGE_DSPIDMF
    {
        public TBL_CHANGE_DSPIDMF()
        {
           
        }

        public TBL_CHANGE_DSPIDMF(int billcode)
        {
            m_BILL_CODE = billcode;
        }

        public TBL_CHANGE_DSPIDMF(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "CHANGE_DSPIDMF";
        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
        public const string BILL_CODE = "BILL_CODE";
        public const string DSP_ID = "DSP_ID";
        #endregion

        #region メンバ

        private int m_BILL_CODE = 0;
        public int m_DSP_ID = 0;

        #endregion

        #region プロパティ

        public int _BILL_CODE
        {
            get { return m_BILL_CODE; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_BILL_CODE = DBConvert.ToIntNull(dr[TBL_CHANGE_DSPIDMF.BILL_CODE]);
            m_DSP_ID = DBConvert.ToIntNull(dr[TBL_CHANGE_DSPIDMF.DSP_ID]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            string strSQL = "SELECT * FROM " + TBL_CHANGE_DSPIDMF.TABLE_NAME +
                " ORDER BY " +
                TBL_CHANGE_DSPIDMF.BILL_CODE;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int billcode)
        {
            string strSQL = "SELECT * FROM " + TBL_CHANGE_DSPIDMF.TABLE_NAME +
                " WHERE " +
                TBL_CHANGE_DSPIDMF.BILL_CODE + "=" + billcode;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_CHANGE_DSPIDMF.TABLE_NAME + " (" +
                TBL_CHANGE_DSPIDMF.BILL_CODE + "," +
                TBL_CHANGE_DSPIDMF.DSP_ID + ") VALUES ("
                 + m_BILL_CODE + "," +
                 + m_DSP_ID + "')";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_CHANGE_DSPIDMF.TABLE_NAME + " SET " +
                TBL_CHANGE_DSPIDMF.DSP_ID + "='" + m_DSP_ID + "' " +
                " WHERE " +
                TBL_CHANGE_DSPIDMF.BILL_CODE + "=" + m_BILL_CODE;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_CHANGE_DSPIDMF.TABLE_NAME +
                " WHERE " +
                TBL_CHANGE_DSPIDMF.BILL_CODE + "=" + m_BILL_CODE;
            return strSQL;
        }

        #endregion
    }
}
