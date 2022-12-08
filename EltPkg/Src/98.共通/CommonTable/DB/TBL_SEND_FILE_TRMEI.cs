using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 配信ファイル明細内訳
    /// </summary>
    /// <remarks>2022/03/28 銀行導入工程_不具合管理表No97 対応</remarks>
    public class TBL_SEND_FILE_TRMEI
    {
        public TBL_SEND_FILE_TRMEI(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_SEND_FILE_TRMEI(string sendfilename, int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_SEND_FILE_NAME = sendfilename;
            m_GYM_ID = gymid;
            m_OPERATION_DATE = operationdate;
            m_SCAN_TERM = scanterm;
            m_BAT_ID = batid;
            m_DETAILS_NO = detailsno;
        }

        public TBL_SEND_FILE_TRMEI(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "SEND_FILE_TRMEI";

        public const string SEND_FILE_NAME = "SEND_FILE_NAME";
        public const string GYM_ID = "GYM_ID";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string SCAN_TERM = "SCAN_TERM";
        public const string BAT_ID = "BAT_ID";
        public const string DETAILS_NO = "DETAILS_NO";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private string m_SEND_FILE_NAME = "";
        private int m_GYM_ID = 0;
        private int m_OPERATION_DATE = 0;
        private string m_SCAN_TERM = "";
        private int m_BAT_ID = 0;
        private int m_DETAILS_NO = 0;
        #endregion

        #region プロパティ
        public string _SEND_FILE_NAME
        {
            get { return m_SEND_FILE_NAME; }
        }
        public int _GYM_ID
        {
            get { return m_GYM_ID; }
        }
        public int _OPERATION_DATE
        {
            get { return m_OPERATION_DATE; }
        }
        public string _SCAN_TERM
        {
            get { return m_SCAN_TERM; }
        }
        public int _BAT_ID
        {
            get { return m_BAT_ID; }
        }
        public int _DETAILS_NO
        {
            get { return m_DETAILS_NO; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {

            m_SEND_FILE_NAME = DBConvert.ToStringNull(dr[TBL_SEND_FILE_TRMEI.SEND_FILE_NAME]);
            m_GYM_ID = DBConvert.ToIntNull(dr[TBL_SEND_FILE_TRMEI.GYM_ID]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_SEND_FILE_TRMEI.OPERATION_DATE]);
            m_SCAN_TERM = DBConvert.ToStringNull(dr[TBL_SEND_FILE_TRMEI.SCAN_TERM]);
            m_BAT_ID = DBConvert.ToIntNull(dr[TBL_SEND_FILE_TRMEI.BAT_ID]);
            m_DETAILS_NO = DBConvert.ToIntNull(dr[TBL_SEND_FILE_TRMEI.DETAILS_NO]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_SEND_FILE_TRMEI.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_SEND_FILE_TRMEI.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_SEND_FILE_TRMEI.SEND_FILE_NAME + "," +
                TBL_SEND_FILE_TRMEI.GYM_ID + "," +
                TBL_SEND_FILE_TRMEI.OPERATION_DATE + "," +
                TBL_SEND_FILE_TRMEI.SCAN_TERM + "," +
                TBL_SEND_FILE_TRMEI.BAT_ID + "," +
                TBL_SEND_FILE_TRMEI.DETAILS_NO;
            return strSql;
        }

        /// <summary>
        /// ファイル名を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryFileName(string sendfilename, int Schemabankcd)
        {
            string strSql = "DELETE FROM " + TBL_SEND_FILE_TRMEI.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                 TBL_SEND_FILE_TRMEI.SEND_FILE_NAME + "='" + sendfilename + "' ";
            return strSql;
        }

        #endregion
    }
}
