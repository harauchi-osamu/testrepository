using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 持帰OCR完了明細
    /// </summary>
    public class TBL_IC_OCR_FINISH
    {
        public TBL_IC_OCR_FINISH()
        {
        }

        public TBL_IC_OCR_FINISH(string FrontImgName)
        {
            m_FRONT_IMG_NAME = FrontImgName;
        }

        public TBL_IC_OCR_FINISH(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_PHYSICAL_NAME = "IC_OCR_FINISH";

        public const string FRONT_IMG_NAME = "FRONT_IMG_NAME";
        public const string OPERATION_DATE = "OPERATION_DATE";
        #endregion

        #region メンバ
        private string m_FRONT_IMG_NAME = "";
        public int m_OPERATION_DATE = 0;
        #endregion

        #region プロパティ

        public string _FRONT_IMG_NAME
        {
            get { return m_FRONT_IMG_NAME; }
        }

        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_FRONT_IMG_NAME = DBConvert.ToStringNull(dr[TBL_IC_OCR_FINISH.FRONT_IMG_NAME]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_IC_OCR_FINISH.OPERATION_DATE]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            string strSql = "SELECT * FROM " + TBL_IC_OCR_FINISH.TABLE_NAME +
                " ORDER BY " +
                  TBL_IC_OCR_FINISH.FRONT_IMG_NAME;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string FrontImgName)
        {
            string strSql = "SELECT * FROM " + TBL_IC_OCR_FINISH.TABLE_NAME +
                " WHERE " +
                  TBL_IC_OCR_FINISH.FRONT_IMG_NAME + "='" + FrontImgName + "' ";
            return strSql;
        }

        /// <summary>
        /// 処理日を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryOpeDate(int OpeDate)
        {
            string strSql = "SELECT * FROM " + TBL_IC_OCR_FINISH.TABLE_NAME +
                " WHERE " +
                  TBL_IC_OCR_FINISH.OPERATION_DATE + "=" + OpeDate + " ";
            return strSql;
        }

        /// <summary>
        /// 処理日以上を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryOpeDateMoreThen(int OpeDate)
        {
            string strSql = "SELECT * FROM " + TBL_IC_OCR_FINISH.TABLE_NAME +
                " WHERE " +
                  TBL_IC_OCR_FINISH.OPERATION_DATE + ">=" + OpeDate + " ";
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_IC_OCR_FINISH.TABLE_NAME + " (" +
                  TBL_IC_OCR_FINISH.FRONT_IMG_NAME + "," +
                  TBL_IC_OCR_FINISH.OPERATION_DATE + ") VALUES (" +
                "'" + m_FRONT_IMG_NAME + "'," +
                m_OPERATION_DATE + " " + ")";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_IC_OCR_FINISH.TABLE_NAME + " SET " +
                  TBL_IC_OCR_FINISH.OPERATION_DATE + "=" + m_OPERATION_DATE + " " +
                " WHERE " +
                  TBL_IC_OCR_FINISH.FRONT_IMG_NAME + "='" + m_FRONT_IMG_NAME + "' ";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_IC_OCR_FINISH.TABLE_NAME +
                " WHERE " +
                  TBL_IC_OCR_FINISH.FRONT_IMG_NAME + "='" + m_FRONT_IMG_NAME + "' ";
            return strSql;
        }

        #endregion
    }
}
