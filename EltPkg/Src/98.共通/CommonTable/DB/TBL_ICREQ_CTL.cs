using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 持帰要求管理
    /// </summary>
    public class TBL_ICREQ_CTL
    {
        public TBL_ICREQ_CTL(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_ICREQ_CTL(string req_txt_name, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_REQ_TXT_NAME = req_txt_name;
        }

        public TBL_ICREQ_CTL(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }
        public TBL_ICREQ_CTL(DataRow dr)
        {
            initializeByDataRow(dr);
        }
        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "ICREQ_CTL";
        public const string REQ_TXT_NAME = "REQ_TXT_NAME";
        public const string REQ_DATE = "REQ_DATE";
        public const string REQ_TIME = "REQ_TIME";
        public const string CLEARING_DATE_S = "CLEARING_DATE_S";
        public const string CLEARING_DATE_E = "CLEARING_DATE_E";
        public const string BILL_CODE = "BILL_CODE";
        public const string IC_TYPE = "IC_TYPE";
        public const string IMG_NEED = "IMG_NEED";
        public const string RET_TXT_NAME = "RET_TXT_NAME";
        public const string RET_COUNT = "RET_COUNT";
        public const string RET_STS = "RET_STS";
        public const string RET_MAKE_DATE = "RET_MAKE_DATE";
        public const string RET_REQ_TXT_NAME = "RET_REQ_TXT_NAME";
        public const string RET_FILE_CHK_CODE = "RET_FILE_CHK_CODE";
        public const string RET_CLEARING_DATE_S = "RET_CLEARING_DATE_S";
        public const string RET_CLEARING_DATE_E = "RET_CLEARING_DATE_E";
        public const string RET_BILL_CODE = "RET_BILL_CODE";
        public const string RET_IC_TYPE = "RET_IC_TYPE";
        public const string RET_IMG_NEED = "RET_IMG_NEED";
        public const string RET_PROC_RETCODE = "RET_PROC_RETCODE";
        public const string RET_DATE = "RET_DATE";
        public const string RET_TIME = "RET_TIME";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private string m_REQ_TXT_NAME = "";
        public int m_REQ_DATE = 0;
        public int m_REQ_TIME = 0;
        public int m_CLEARING_DATE_S = 0;
        public int m_CLEARING_DATE_E = 0;
        public string m_BILL_CODE = "";
        public int m_IC_TYPE = 0;
        public int m_IMG_NEED = 0;
        public string m_RET_TXT_NAME = "";
        public int m_RET_COUNT = 0;
        public int m_RET_STS = 0;
        public int m_RET_MAKE_DATE = 0;
        public string m_RET_REQ_TXT_NAME = "";
        public string m_RET_FILE_CHK_CODE = "";
        public string m_RET_CLEARING_DATE_S = "";
        public string m_RET_CLEARING_DATE_E = "";
        public string m_RET_BILL_CODE = "";
        public string m_RET_IC_TYPE = "";
        public string m_RET_IMG_NEED = "";
        public string m_RET_PROC_RETCODE = "";
        public int m_RET_DATE = 0;
        public int m_RET_TIME = 0;
        #endregion

        #region プロパティ

        public string _REQ_TXT_NAME
        {
            get { return m_REQ_TXT_NAME; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_REQ_TXT_NAME = DBConvert.ToStringNull(dr[TBL_ICREQ_CTL.REQ_TXT_NAME]);
            m_REQ_DATE = DBConvert.ToIntNull(dr[TBL_ICREQ_CTL.REQ_DATE]);
            m_REQ_TIME = DBConvert.ToIntNull(dr[TBL_ICREQ_CTL.REQ_TIME]);
            m_CLEARING_DATE_S = DBConvert.ToIntNull(dr[TBL_ICREQ_CTL.CLEARING_DATE_S]);
            m_CLEARING_DATE_E = DBConvert.ToIntNull(dr[TBL_ICREQ_CTL.CLEARING_DATE_E]);
            m_BILL_CODE = DBConvert.ToStringNull(dr[TBL_ICREQ_CTL.BILL_CODE]);
            m_IC_TYPE = DBConvert.ToIntNull(dr[TBL_ICREQ_CTL.IC_TYPE]);
            m_IMG_NEED = DBConvert.ToIntNull(dr[TBL_ICREQ_CTL.IMG_NEED]);
            m_RET_TXT_NAME = DBConvert.ToStringNull(dr[TBL_ICREQ_CTL.RET_TXT_NAME]);
            m_RET_COUNT = DBConvert.ToIntNull(dr[TBL_ICREQ_CTL.RET_COUNT]);
            m_RET_STS = DBConvert.ToIntNull(dr[TBL_ICREQ_CTL.RET_STS]);
            m_RET_MAKE_DATE = DBConvert.ToIntNull(dr[TBL_ICREQ_CTL.RET_MAKE_DATE]);
            m_RET_REQ_TXT_NAME = DBConvert.ToStringNull(dr[TBL_ICREQ_CTL.RET_REQ_TXT_NAME]);
            m_RET_FILE_CHK_CODE = DBConvert.ToStringNull(dr[TBL_ICREQ_CTL.RET_FILE_CHK_CODE]);
            m_RET_CLEARING_DATE_S = DBConvert.ToStringNull(dr[TBL_ICREQ_CTL.RET_CLEARING_DATE_S]);
            m_RET_CLEARING_DATE_E = DBConvert.ToStringNull(dr[TBL_ICREQ_CTL.RET_CLEARING_DATE_E]);
            m_RET_BILL_CODE = DBConvert.ToStringNull(dr[TBL_ICREQ_CTL.RET_BILL_CODE]);
            m_RET_IC_TYPE = DBConvert.ToStringNull(dr[TBL_ICREQ_CTL.RET_IC_TYPE]);
            m_RET_IMG_NEED = DBConvert.ToStringNull(dr[TBL_ICREQ_CTL.RET_IMG_NEED]);
            m_RET_PROC_RETCODE = DBConvert.ToStringNull(dr[TBL_ICREQ_CTL.RET_PROC_RETCODE]);
            m_RET_DATE = DBConvert.ToIntNull(dr[TBL_ICREQ_CTL.RET_DATE]);
            m_RET_TIME = DBConvert.ToIntNull(dr[TBL_ICREQ_CTL.RET_TIME]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_ICREQ_CTL.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_ICREQ_CTL.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_ICREQ_CTL.REQ_TXT_NAME;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string req_txt_name, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_ICREQ_CTL.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_ICREQ_CTL.REQ_TXT_NAME + "='" + req_txt_name + "'";
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_ICREQ_CTL.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_ICREQ_CTL.REQ_TXT_NAME + "," +
                TBL_ICREQ_CTL.REQ_DATE + "," +
                TBL_ICREQ_CTL.REQ_TIME + "," +
                TBL_ICREQ_CTL.CLEARING_DATE_S + "," +
                TBL_ICREQ_CTL.CLEARING_DATE_E + "," +
                TBL_ICREQ_CTL.BILL_CODE + "," +
                TBL_ICREQ_CTL.IC_TYPE + "," +
                TBL_ICREQ_CTL.IMG_NEED + "," +
                TBL_ICREQ_CTL.RET_TXT_NAME + "," +
                TBL_ICREQ_CTL.RET_COUNT + "," +
                TBL_ICREQ_CTL.RET_STS + "," +
                TBL_ICREQ_CTL.RET_MAKE_DATE + "," +
                TBL_ICREQ_CTL.RET_REQ_TXT_NAME + "," +
                TBL_ICREQ_CTL.RET_FILE_CHK_CODE + "," +
                TBL_ICREQ_CTL.RET_CLEARING_DATE_S + "," +
                TBL_ICREQ_CTL.RET_CLEARING_DATE_E + "," +
                TBL_ICREQ_CTL.RET_BILL_CODE + "," +
                TBL_ICREQ_CTL.RET_IC_TYPE + "," +
                TBL_ICREQ_CTL.RET_IMG_NEED + "," +
                TBL_ICREQ_CTL.RET_PROC_RETCODE + "," +
                TBL_ICREQ_CTL.RET_DATE + "," +
                TBL_ICREQ_CTL.RET_TIME + ") VALUES (" +
                "'" + m_REQ_TXT_NAME + "'," +
                m_REQ_DATE + "," +
                m_REQ_TIME + "," +
                m_CLEARING_DATE_S + "," +
                m_CLEARING_DATE_E + "," +
                "'" + m_BILL_CODE + "'," +
                m_IC_TYPE + "," +
                m_IMG_NEED + "," +
                "'" + m_RET_TXT_NAME + "'," +
                m_RET_COUNT + "," +
                m_RET_STS + "," +
                m_RET_MAKE_DATE + "," +
                "'" + m_RET_REQ_TXT_NAME + "'," +
                "'" + m_RET_FILE_CHK_CODE + "'," +
                "'" + m_RET_CLEARING_DATE_S + "'," +
                "'" + m_RET_CLEARING_DATE_E + "'," +
                "'" + m_RET_BILL_CODE + "'," +
                "'" + m_RET_IC_TYPE + "'," +
                "'" + m_RET_IMG_NEED + "'," +
                "'" + m_RET_PROC_RETCODE + "'," +
                m_RET_DATE + "," +
                m_RET_TIME + ")";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_ICREQ_CTL.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_ICREQ_CTL.REQ_DATE + "=" + m_REQ_DATE + ", " +
                TBL_ICREQ_CTL.REQ_TIME + "=" + m_REQ_TIME + ", " +
                TBL_ICREQ_CTL.CLEARING_DATE_S + "=" + m_CLEARING_DATE_S + ", " +
                TBL_ICREQ_CTL.CLEARING_DATE_E + "=" + m_CLEARING_DATE_E + ", " +
                TBL_ICREQ_CTL.BILL_CODE + "='" + m_BILL_CODE + "', " +
                TBL_ICREQ_CTL.IC_TYPE + "=" + m_IC_TYPE + ", " +
                TBL_ICREQ_CTL.IMG_NEED + "=" + m_IMG_NEED + ", " +
                TBL_ICREQ_CTL.RET_TXT_NAME + "='" + m_RET_TXT_NAME + "', " +
                TBL_ICREQ_CTL.RET_COUNT + "=" + m_RET_COUNT + ", " +
                TBL_ICREQ_CTL.RET_STS + "=" + m_RET_STS + ", " +
                TBL_ICREQ_CTL.RET_MAKE_DATE + "=" + m_RET_MAKE_DATE + ", " +
                TBL_ICREQ_CTL.RET_REQ_TXT_NAME + "='" + m_RET_REQ_TXT_NAME + "', " +
                TBL_ICREQ_CTL.RET_FILE_CHK_CODE + "='" + m_RET_FILE_CHK_CODE + "', " +
                TBL_ICREQ_CTL.RET_CLEARING_DATE_S + "='" + m_RET_CLEARING_DATE_S + "', " +
                TBL_ICREQ_CTL.RET_CLEARING_DATE_E + "='" + m_RET_CLEARING_DATE_E + "', " +
                TBL_ICREQ_CTL.RET_BILL_CODE + "='" + m_RET_BILL_CODE + "', " +
                TBL_ICREQ_CTL.RET_IC_TYPE + "='" + m_RET_IC_TYPE + "', " +
                TBL_ICREQ_CTL.RET_IMG_NEED + "='" + m_RET_IMG_NEED + "', " +
                TBL_ICREQ_CTL.RET_PROC_RETCODE + "='" + m_RET_PROC_RETCODE + "', " +
                TBL_ICREQ_CTL.RET_DATE + "=" + m_RET_DATE + ", " +
                TBL_ICREQ_CTL.RET_TIME + "=" + m_RET_TIME + " " +
                " WHERE " +
                TBL_ICREQ_CTL.REQ_TXT_NAME + "='" + m_REQ_TXT_NAME + "' ";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// 持帰要求結果取込処理
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQueryReqResult()
        {
            string strSql = "UPDATE " + TBL_ICREQ_CTL.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_ICREQ_CTL.RET_TXT_NAME + "='" + m_RET_TXT_NAME + "', " +
                TBL_ICREQ_CTL.RET_COUNT + "=" + m_RET_COUNT + ", " +
                TBL_ICREQ_CTL.RET_STS + "=" + m_RET_STS + ", " +
                TBL_ICREQ_CTL.RET_MAKE_DATE + "=" + m_RET_MAKE_DATE + ", " +
                TBL_ICREQ_CTL.RET_REQ_TXT_NAME + "='" + m_RET_REQ_TXT_NAME + "', " +
                TBL_ICREQ_CTL.RET_FILE_CHK_CODE + "='" + m_RET_FILE_CHK_CODE + "', " +
                TBL_ICREQ_CTL.RET_CLEARING_DATE_S + "='" + m_RET_CLEARING_DATE_S + "', " +
                TBL_ICREQ_CTL.RET_CLEARING_DATE_E + "='" + m_RET_CLEARING_DATE_E + "', " +
                TBL_ICREQ_CTL.RET_BILL_CODE + "='" + m_RET_BILL_CODE + "', " +
                TBL_ICREQ_CTL.RET_IC_TYPE + "='" + m_RET_IC_TYPE + "', " +
                TBL_ICREQ_CTL.RET_IMG_NEED + "='" + m_RET_IMG_NEED + "', " +
                TBL_ICREQ_CTL.RET_PROC_RETCODE + "='" + m_RET_PROC_RETCODE + "', " +
                TBL_ICREQ_CTL.RET_DATE + "=" + m_RET_DATE + ", " +
                TBL_ICREQ_CTL.RET_TIME + "=" + m_RET_TIME + " " +
                " WHERE " +
                TBL_ICREQ_CTL.REQ_TXT_NAME + "='" + m_REQ_TXT_NAME + "' ";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_ICREQ_CTL.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                  TBL_ICREQ_CTL.REQ_TXT_NAME + "='" + m_REQ_TXT_NAME + "' ";
            return strSql;
        }

        #endregion
    }
}
