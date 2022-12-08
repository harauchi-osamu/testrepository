using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 種類マスタ
    /// </summary>
    public class TBL_SYURUIMF
    {
        public TBL_SYURUIMF(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_SYURUIMF(int syuruicode, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_SYURUI_CODE = syuruicode;
        }

        public TBL_SYURUIMF(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "SYURUIMF";

        public const string SYURUI_CODE = "SYURUI_CODE";
        public const string SYURUI_NAME = "SYURUI_NAME";
        public const string INPUT_DSP_ID = "INPUT_DSP_ID";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private int m_SYURUI_CODE = 0;
        public string m_SYURUI_NAME = "";
        public string m_INPUT_DSP_ID = "";

        #endregion

        #region プロパティ

        public int _SYURUI_CODE
        {
            get { return m_SYURUI_CODE; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_SYURUI_CODE = DBConvert.ToIntNull(dr[TBL_SYURUIMF.SYURUI_CODE]);
            m_SYURUI_NAME = DBConvert.ToStringNull(dr[TBL_SYURUIMF.SYURUI_NAME]);
            m_INPUT_DSP_ID = DBConvert.ToStringNull(dr[TBL_SYURUIMF.INPUT_DSP_ID]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_SYURUIMF.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_SYURUIMF.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_SYURUIMF.SYURUI_CODE;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int syuruicode, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_SYURUIMF.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_SYURUIMF.SYURUI_CODE + "=" + syuruicode;
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_SYURUIMF.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_SYURUIMF.SYURUI_CODE + "," +
                TBL_SYURUIMF.SYURUI_NAME + "," +
                TBL_SYURUIMF.INPUT_DSP_ID + ") VALUES (" +
                m_SYURUI_CODE + "," +
                "'" + m_SYURUI_NAME + "'," + 
                "'" + m_INPUT_DSP_ID + "'" + ")";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_SYURUIMF.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_SYURUIMF.SYURUI_NAME + "='" + m_SYURUI_NAME + "', " +
                TBL_SYURUIMF.INPUT_DSP_ID + "='" + m_INPUT_DSP_ID + "' " +
                " WHERE " +
                TBL_SYURUIMF.SYURUI_CODE + "=" + m_SYURUI_CODE;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_SYURUIMF.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_SYURUIMF.SYURUI_CODE + "=" + m_SYURUI_CODE;
            return strSQL;
        }

        #endregion
    }
}
