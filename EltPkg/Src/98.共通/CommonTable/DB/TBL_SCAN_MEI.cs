using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 汎用エントリオペレータ処理状況
    /// </summary>
    public class TBL_SCAN_MEI
    {
        public TBL_SCAN_MEI()
        {
        }

        public TBL_SCAN_MEI(string img_name, int operation_date)
        {
            m_IMG_NAME = img_name;
            m_OPERATION_DATE = operation_date;
        }

        public TBL_SCAN_MEI(DataRow dr)
        {
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_NAME = DBConvert.TABLE_SCHEMA_DBCTR + "." + TABLE_PHYSICAL_NAME;
        public const string TABLE_PHYSICAL_NAME = "SCAN_MEI";

        public const string IMG_NAME = "IMG_NAME";
        public const string OPERATION_DATE = "OPERATION_DATE";
        public const string INPUT_ROUTE = "INPUT_ROUTE";
        public const string BATCH_FOLDER_NAME = "BATCH_FOLDER_NAME";
        public const string BATCH_UCHI_RENBAN = "BATCH_UCHI_RENBAN";
        public const string IMG_KBN = "IMG_KBN";
        public const string I_TERM = "I_TERM";
        public const string I_OPENO = "I_OPENO";
        public const string I_YMD = "I_YMD";
        public const string I_TIME = "I_TIME";

        #endregion

        #region メンバ

        private string m_IMG_NAME = "";
        private int m_OPERATION_DATE = 0;
        public int m_INPUT_ROUTE = 0;
        public string m_BATCH_FOLDER_NAME = "";
        public int m_BATCH_UCHI_RENBAN = 0;
        public int m_IMG_KBN = 0;
        public string m_I_TERM = "";
        public string m_I_OPENO = "";
        public int m_I_YMD = 0;
        public int m_I_TIME = 0;

        #endregion

        #region プロパティ

        public string _IMG_NAME
        {
            get { return m_IMG_NAME; }
        }
        public int _OPERATION_DATE
        {
            get { return m_OPERATION_DATE; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_IMG_NAME = DBConvert.ToStringNull(dr[TBL_SCAN_MEI.IMG_NAME]);
            m_OPERATION_DATE = DBConvert.ToIntNull(dr[TBL_SCAN_MEI.OPERATION_DATE]);
            m_INPUT_ROUTE = DBConvert.ToIntNull(dr[TBL_SCAN_MEI.INPUT_ROUTE]);
            m_BATCH_FOLDER_NAME = DBConvert.ToStringNull(dr[TBL_SCAN_MEI.BATCH_FOLDER_NAME]);
            m_BATCH_UCHI_RENBAN = DBConvert.ToIntNull(dr[TBL_SCAN_MEI.BATCH_UCHI_RENBAN]);
            m_IMG_KBN = DBConvert.ToIntNull(dr[TBL_SCAN_MEI.IMG_KBN]);
            m_I_TERM = DBConvert.ToStringNull(dr[TBL_SCAN_MEI.I_TERM]);
            m_I_OPENO = DBConvert.ToStringNull(dr[TBL_SCAN_MEI.I_OPENO]);
            m_I_YMD = DBConvert.ToIntNull(dr[TBL_SCAN_MEI.I_YMD]);
            m_I_TIME = DBConvert.ToIntNull(dr[TBL_SCAN_MEI.I_TIME]);
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery()
        {
            string strSql = "SELECT * FROM " + TBL_SCAN_MEI.TABLE_NAME +
                " ORDER BY " +
               TBL_SCAN_MEI.IMG_NAME + "," +
                TBL_SCAN_MEI.OPERATION_DATE;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string img_name, int operation_date)
        {
            string strSql = "SELECT * FROM " + TBL_SCAN_MEI.TABLE_NAME +
                " WHERE " +
                TBL_SCAN_MEI.IMG_NAME + "='" + img_name + "' AND " +
                TBL_SCAN_MEI.OPERATION_DATE + "=" + operation_date + "";
            return strSql;
        }

        /// <summary>
        /// バッチフォルダ単位で取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryBatchFolder(int operation_date, int inputroute, string batchfoldername)
        {
            string strSql = "SELECT * FROM " + TBL_SCAN_MEI.TABLE_NAME +
                " WHERE " +
                TBL_SCAN_MEI.OPERATION_DATE + "=" + operation_date + " AND " + 
                TBL_SCAN_MEI.INPUT_ROUTE + "=" + inputroute + " AND " +
                TBL_SCAN_MEI.BATCH_FOLDER_NAME + "='" + batchfoldername + "' " + 
                " ORDER BY " + TBL_SCAN_MEI.BATCH_UCHI_RENBAN + " ";
            return strSql;
        }

        /// <summary>
        /// バッチフォルダ単位で連番一覧を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryRenban(int operation_date, int inputroute, string batchfoldername)
        {
            string strSql = "SELECT "+ TBL_SCAN_MEI.BATCH_UCHI_RENBAN + " FROM " + TBL_SCAN_MEI.TABLE_NAME +
                " WHERE " +
                TBL_SCAN_MEI.OPERATION_DATE + "=" + operation_date + " AND " +
                TBL_SCAN_MEI.INPUT_ROUTE + "=" + inputroute + " AND " +
                TBL_SCAN_MEI.BATCH_FOLDER_NAME + "='" + batchfoldername + "' " +
                " GROUP BY " + TBL_SCAN_MEI.BATCH_UCHI_RENBAN + " " +
                " ORDER BY " + TBL_SCAN_MEI.BATCH_UCHI_RENBAN + " ";
            return strSql;
        }


        /// <summary>
        /// バッチフォルダ単位で指定連番以降の連番を更新するUPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateQueryRenban(int operation_date, int inputroute, string batchfoldername, int renban)
        {

            string strSql = "UPDATE " + TBL_SCAN_MEI.TABLE_NAME + " SET " +
                TBL_SCAN_MEI.BATCH_UCHI_RENBAN + "=" + TBL_SCAN_MEI.BATCH_UCHI_RENBAN + " - 1 " +
                " WHERE " +
                TBL_SCAN_MEI.OPERATION_DATE + "=" + operation_date + " AND " +
                TBL_SCAN_MEI.INPUT_ROUTE + "=" + inputroute + " AND " +
                TBL_SCAN_MEI.BATCH_FOLDER_NAME + "='" + batchfoldername + "' AND " +
                TBL_SCAN_MEI.BATCH_UCHI_RENBAN + ">=" + renban + " ";
            return strSql;
        }

        /// <summary>
        /// バッチフォルダ・連番単位で削除するDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryRenban(int operation_date, int inputroute, string batchfoldername, int renban)
        {
            string strSql = "DELETE FROM " + TBL_SCAN_MEI.TABLE_NAME +
                " WHERE " +
                TBL_SCAN_MEI.OPERATION_DATE + "=" + operation_date + " AND " +
                TBL_SCAN_MEI.INPUT_ROUTE + "=" + inputroute + " AND " +
                TBL_SCAN_MEI.BATCH_FOLDER_NAME + "='" + batchfoldername + "' AND " +
                TBL_SCAN_MEI.BATCH_UCHI_RENBAN + "=" + renban + " ";
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_SCAN_MEI.TABLE_NAME + " (" +
                TBL_SCAN_MEI.IMG_NAME + "," +
                TBL_SCAN_MEI.OPERATION_DATE + "," +
                TBL_SCAN_MEI.INPUT_ROUTE + "," +
                TBL_SCAN_MEI.BATCH_FOLDER_NAME + "," +
                TBL_SCAN_MEI.BATCH_UCHI_RENBAN + "," +
                TBL_SCAN_MEI.IMG_KBN + "," +
                TBL_SCAN_MEI.I_TERM + "," +
                TBL_SCAN_MEI.I_OPENO + "," +
                TBL_SCAN_MEI.I_YMD + "," +
                TBL_SCAN_MEI.I_TIME + ") VALUES (" +
                "'" + m_IMG_NAME + "'," +
                m_OPERATION_DATE + "," +
                m_INPUT_ROUTE + "," +
                "'" + m_BATCH_FOLDER_NAME + "'," +
                m_BATCH_UCHI_RENBAN + "," +
                m_IMG_KBN + "," +
                "'" + m_I_TERM + "'," +
                "'" + m_I_OPENO + "'," +
                m_I_YMD + "," +
                m_I_TIME + ")";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_SCAN_MEI.TABLE_NAME + " SET " +
                TBL_SCAN_MEI.INPUT_ROUTE + "=" + m_INPUT_ROUTE + ", " +
                TBL_SCAN_MEI.BATCH_FOLDER_NAME + "='" + m_BATCH_FOLDER_NAME + "', " +
                TBL_SCAN_MEI.IMG_KBN + "=" + m_IMG_KBN + ", " +
                TBL_SCAN_MEI.I_TERM + "='" + m_I_TERM + "', " +
                TBL_SCAN_MEI.I_OPENO + "='" + m_I_OPENO + "', " +
                TBL_SCAN_MEI.I_YMD + "=" + m_I_YMD + ", " +
                TBL_SCAN_MEI.I_TIME + "=" + m_I_TIME +
                " WHERE " +
                TBL_SCAN_MEI.IMG_NAME + "='" + m_IMG_NAME + "' AND " +
                TBL_SCAN_MEI.OPERATION_DATE + "=" + m_OPERATION_DATE;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_SCAN_MEI.TABLE_NAME +
                " WHERE " +
                 TBL_SCAN_MEI.IMG_NAME + "='" + m_IMG_NAME + "' AND " +
                 TBL_SCAN_MEI.OPERATION_DATE + "=" + m_OPERATION_DATE;
            return strSql;
        }

        #endregion
    }
}
