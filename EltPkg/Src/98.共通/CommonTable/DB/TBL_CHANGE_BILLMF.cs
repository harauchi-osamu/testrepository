using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 交換証券種類変換マスタ
    /// </summary>
    public class TBL_CHANGE_BILLMF
    {
        public TBL_CHANGE_BILLMF()
        {
           
        }

        public TBL_CHANGE_BILLMF(int dspid, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_DSP_ID = dspid;
        }

        public TBL_CHANGE_BILLMF(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "CHANGE_BILLMF";

        public const string DSP_ID = "DSP_ID";
        public const string BILL_CODE = "BILL_CODE";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private int m_DSP_ID = 0;
        public int m_BILL_CODE = 0;
        #endregion

        #region プロパティ
        public int _DSP_ID
        {
            get { return m_DSP_ID; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_DSP_ID = DBConvert.ToIntNull(dr[TBL_CHANGE_BILLMF.DSP_ID]);
            m_BILL_CODE = DBConvert.ToIntNull(dr[TBL_CHANGE_BILLMF.BILL_CODE]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_CHANGE_BILLMF.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_CHANGE_BILLMF.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                  TBL_CHANGE_BILLMF.DSP_ID;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int dspid, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_CHANGE_BILLMF.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                  TBL_CHANGE_BILLMF.DSP_ID + "=" + dspid + " ";
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_CHANGE_BILLMF.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                  TBL_CHANGE_BILLMF.DSP_ID + "," +
                  TBL_CHANGE_BILLMF.BILL_CODE + ") VALUES (" +
                m_DSP_ID + "," +
                m_BILL_CODE + " " + ")";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSql = "UPDATE " + TBL_CHANGE_BILLMF.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                  TBL_CHANGE_BILLMF.BILL_CODE + "=" + m_BILL_CODE +
                " WHERE " +
                  TBL_CHANGE_BILLMF.DSP_ID + "=" + m_DSP_ID + " ";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_CHANGE_BILLMF.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                  TBL_CHANGE_BILLMF.DSP_ID + "=" + m_DSP_ID + " ";
            return strSql;
        }

        #endregion

    }
}
