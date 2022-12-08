using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 持帰要求結果管理
    /// </summary>
    public class TBL_ICREQRET_CTL
    {
        public TBL_ICREQRET_CTL(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_ICREQRET_CTL(string retreqtxtname, string meitxtname, int capkbn, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_RET_REQ_TXT_NAME = retreqtxtname;
            m_MEI_TXT_NAME = meitxtname;
            m_CAP_KBN = capkbn;
        }

        public TBL_ICREQRET_CTL(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "ICREQRET_CTL";

        public const string RET_REQ_TXT_NAME = "RET_REQ_TXT_NAME";
        public const string MEI_TXT_NAME = "MEI_TXT_NAME";
        public const string CAP_KBN = "CAP_KBN";
        public const string CAP_STS = "CAP_STS";
        public const string IMG_ARCH_NAME = "IMG_ARCH_NAME";
        public const string IMG_ARCH_CAP_STS = "IMG_ARCH_CAP_STS";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private string m_RET_REQ_TXT_NAME = "";
        private string m_MEI_TXT_NAME = "";
        private int m_CAP_KBN = 0;
        public int m_CAP_STS = 0;
        public string m_IMG_ARCH_NAME = "";
        public int m_IMG_ARCH_CAP_STS = -1;

        #endregion

        #region プロパティ

        public string _RET_REQ_TXT_NAME
        {
            get { return m_RET_REQ_TXT_NAME; }
        }
        public string _MEI_TXT_NAME
        {
            get { return m_MEI_TXT_NAME; }
        }
        public int _CAP_KBN
        {
            get { return m_CAP_KBN; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_RET_REQ_TXT_NAME = DBConvert.ToStringNull(dr[TBL_ICREQRET_CTL.RET_REQ_TXT_NAME]);
            m_MEI_TXT_NAME = DBConvert.ToStringNull(dr[TBL_ICREQRET_CTL.MEI_TXT_NAME]);
            m_CAP_KBN = DBConvert.ToIntNull(dr[TBL_ICREQRET_CTL.CAP_KBN]);
            m_CAP_STS = DBConvert.ToIntNull(dr[TBL_ICREQRET_CTL.CAP_STS]);
            m_IMG_ARCH_NAME = DBConvert.ToStringNull(dr[TBL_ICREQRET_CTL.IMG_ARCH_NAME]);
            m_IMG_ARCH_CAP_STS = DBConvert.ToIntNull(dr[TBL_ICREQRET_CTL.IMG_ARCH_CAP_STS]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_ICREQRET_CTL.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_ICREQRET_CTL.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_ICREQRET_CTL.RET_REQ_TXT_NAME + "," +
                TBL_ICREQRET_CTL.MEI_TXT_NAME + "," +
                TBL_ICREQRET_CTL.CAP_KBN;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string retreqtxtname, string meitxtname, int capkbn, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_ICREQRET_CTL.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_ICREQRET_CTL.RET_REQ_TXT_NAME + "='" + retreqtxtname + "' AND " +
                TBL_ICREQRET_CTL.MEI_TXT_NAME + "='" + meitxtname + "' AND " +
                TBL_ICREQRET_CTL.CAP_KBN + "=" + capkbn;
            return strSQL;
        }

        /// <summary>
        /// 要求結果テキストを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryRetReqTxtData(string retreqtxtname, int capkbn, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_ICREQRET_CTL.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_ICREQRET_CTL.RET_REQ_TXT_NAME + "='" + retreqtxtname + "' AND " +
                TBL_ICREQRET_CTL.CAP_KBN + "=" + capkbn;
            return strSQL;
        }

        /// <summary>
        /// 証券明細テキストを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryMeiTxtData(string meitxtname, int capkbn, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_ICREQRET_CTL.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_ICREQRET_CTL.MEI_TXT_NAME + "='" + meitxtname + "' AND " +
                TBL_ICREQRET_CTL.CAP_KBN + "=" + capkbn;
            return strSQL;
        }

        /// <summary>
        /// 要求結果テキスト・証券明細テキストを条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteQueryReqMeiTxt(string retreqtxtname, string meitxtname, int capkbn, int Schemabankcd)
        {
            string strSQL = "DELETE FROM " + TBL_ICREQRET_CTL.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                " NOT (" + TBL_ICREQRET_CTL.CAP_STS + "= 10 " + " OR " + TBL_ICREQRET_CTL.IMG_ARCH_CAP_STS + "= 10 " + " ) AND " +
                TBL_ICREQRET_CTL.CAP_KBN + "=" + capkbn + " AND " +
                TBL_ICREQRET_CTL.RET_REQ_TXT_NAME + "='" + retreqtxtname + "' AND " +
                TBL_ICREQRET_CTL.MEI_TXT_NAME + "='" + meitxtname + "' ";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// イメージアーカイブ関連更新
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateQueryImgArchData(int capkbn, string archname, int capsts, int Schemabankcd)
        {
            string strSql = "UPDATE " + TBL_ICREQRET_CTL.TABLE_NAME(Schemabankcd) + " SET " +
                TBL_ICREQRET_CTL.IMG_ARCH_CAP_STS + "=" + capsts + " " +
                " WHERE " +
                TBL_ICREQRET_CTL.CAP_KBN + "=" + capkbn + " AND " +
                TBL_ICREQRET_CTL.IMG_ARCH_NAME + "='" + archname + "' ";
            return strSql;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// 証券明細テキスト関連更新
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateQueryMeiTxtData(int capkbn, string meitxt, int capsts, int Schemabankcd)
        {
            string strSql = "UPDATE " + TBL_ICREQRET_CTL.TABLE_NAME(Schemabankcd) + " SET " +
                TBL_ICREQRET_CTL.CAP_STS + "=" + capsts + " " +
                " WHERE " +
                TBL_ICREQRET_CTL.CAP_KBN + "=" + capkbn + " AND " +
                TBL_ICREQRET_CTL.MEI_TXT_NAME + "='" + meitxt + "' ";
            return strSql;
        }


        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_ICREQRET_CTL.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_ICREQRET_CTL.RET_REQ_TXT_NAME + "," +
                TBL_ICREQRET_CTL.MEI_TXT_NAME + "," +
                TBL_ICREQRET_CTL.CAP_KBN + "," +
                TBL_ICREQRET_CTL.CAP_STS + "," +
                TBL_ICREQRET_CTL.IMG_ARCH_NAME + "," +
                TBL_ICREQRET_CTL.IMG_ARCH_CAP_STS + ") VALUES (" +
                "'" + m_RET_REQ_TXT_NAME + "'," +
                "'" + m_MEI_TXT_NAME + "'," +
                m_CAP_KBN + "," +
                m_CAP_STS + "," +
                "'" + m_IMG_ARCH_NAME + "'," +
                m_IMG_ARCH_CAP_STS + ")";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_ICREQRET_CTL.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_ICREQRET_CTL.CAP_STS + "=" + m_CAP_STS + ", " +
                TBL_ICREQRET_CTL.IMG_ARCH_NAME + "='" + m_IMG_ARCH_NAME + "', " +
                TBL_ICREQRET_CTL.IMG_ARCH_CAP_STS + "=" + m_IMG_ARCH_CAP_STS +
                " WHERE " +
                TBL_ICREQRET_CTL.RET_REQ_TXT_NAME + "='" + m_RET_REQ_TXT_NAME + "' AND " +
                TBL_ICREQRET_CTL.MEI_TXT_NAME + "='" + m_MEI_TXT_NAME + "' AND " +
                TBL_ICREQRET_CTL.CAP_KBN + "=" + m_CAP_KBN;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_ICREQRET_CTL.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_ICREQRET_CTL.RET_REQ_TXT_NAME + "='" + m_RET_REQ_TXT_NAME + "' AND " +
                TBL_ICREQRET_CTL.MEI_TXT_NAME + "='" + m_MEI_TXT_NAME + "' AND " +
                TBL_ICREQRET_CTL.CAP_KBN + "=" + m_CAP_KBN;
            return strSQL;
        }

        #endregion
    }
}
