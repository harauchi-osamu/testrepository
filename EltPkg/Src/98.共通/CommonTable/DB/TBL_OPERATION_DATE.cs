using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 処理日付
    /// </summary>
    public class TBL_OPERATION_DATE
    {
        public TBL_OPERATION_DATE(int opedate)
        {
            _OPEDATE = opedate;
        }

        public TBL_OPERATION_DATE(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_PHYSICAL_NAME = "OPERATION_DATE";

        public const string OPEDATE = "OPEDATE";
        #endregion

        #region メンバ

        public int _OPEDATE = 0;

        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            _OPEDATE = DBConvert.ToIntNull(dr[TBL_OPERATION_DATE.OPEDATE]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            string strSQL = "SELECT * FROM " + TBL_OPERATION_DATE.TABLE_NAME;
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_OPERATION_DATE.TABLE_NAME + " SET " +
                TBL_OPERATION_DATE.OPEDATE + "=" + _OPEDATE;
            return strSQL;
        }

        #endregion
    }
}
