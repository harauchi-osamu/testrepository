using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 汎用エントリオペレータ処理状況
    /// </summary>
    public class TBL_TSUCHITXT
    {
        public TBL_TSUCHITXT(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_TSUCHITXT(string file_name, int recordseq, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_FILE_NAME = file_name;
            m_RECORD_SEQ = recordseq;
        }

        public TBL_TSUCHITXT(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "TSUCHITXT";

        public const string FILE_NAME = "FILE_NAME";
        public const string RECORD_SEQ = "RECORD_SEQ";
        public const string IMG_NAME = "IMG_NAME";
        public const string BK_NO_TEISEI_FLG = "BK_NO_TEISEI_FLG";
        public const string TEISEI_BEF_BK_NO = "TEISEI_BEF_BK_NO";
        public const string TEISEI_AFT_BK_NO = "TEISEI_AFT_BK_NO";
        public const string CLEARING_TEISEI_FLG = "CLEARING_TEISEI_FLG";
        public const string TEISEI_BEF_CLEARING_DATE = "TEISEI_BEF_CLEARING_DATE";
        public const string TEISEI_CLEARING_DATE = "TEISEI_CLEARING_DATE";
        public const string AMOUNT_TEISEI_FLG = "AMOUNT_TEISEI_FLG";
        public const string TEISEI_BEF_AMOUNT = "TEISEI_BEF_AMOUNT";
        public const string TEISEI_AMOUNT = "TEISEI_AMOUNT";
        public const string DUPLICATE_IMG_NAME = "DUPLICATE_IMG_NAME";
        public const string FUBI_REG_KBN = "FUBI_REG_KBN";
        public const string FUBI_KBN_01 = "FUBI_KBN_01";
        public const string ZERO_FUBINO_01 = "ZERO_FUBINO_01";
        public const string FUBI_KBN_02 = "FUBI_KBN_02";
        public const string ZRO_FUBINO_02 = "ZRO_FUBINO_02";
        public const string FUBI_KBN_03 = "FUBI_KBN_03";
        public const string ZRO_FUBINO_03 = "ZRO_FUBINO_03";
        public const string FUBI_KBN_04 = "FUBI_KBN_04";
        public const string ZRO_FUBINO_04 = "ZRO_FUBINO_04";
        public const string FUBI_KBN_05 = "FUBI_KBN_05";
        public const string ZRO_FUBINO_05 = "ZRO_FUBINO_05";
        public const string REV_CLEARING_FLG = "REV_CLEARING_FLG";

        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private string m_FILE_NAME = "";
        private int m_RECORD_SEQ = 0;
        public string m_IMG_NAME = "";
        public string m_BK_NO_TEISEI_FLG = "";
        public string m_TEISEI_BEF_BK_NO = "";
        public string m_TEISEI_AFT_BK_NO = "";
        public string m_CLEARING_TEISEI_FLG = "";
        public string m_TEISEI_BEF_CLEARING_DATE = "";
        public string m_TEISEI_CLEARING_DATE = "";
        public string m_AMOUNT_TEISEI_FLG = "";
        public string m_TEISEI_BEF_AMOUNT = "";
        public string m_TEISEI_AMOUNT = "";
        public string m_DUPLICATE_IMG_NAME = "";
        public string m_FUBI_REG_KBN = "";
        public string m_FUBI_KBN_01 = "";
        public int m_ZERO_FUBINO_01 = 0;
        public string m_FUBI_KBN_02 = "";
        public int m_ZRO_FUBINO_02 = 0;
        public string m_FUBI_KBN_03 = "";
        public int m_ZRO_FUBINO_03 = 0;
        public string m_FUBI_KBN_04 = "";
        public int m_ZRO_FUBINO_04 = 0;
        public string m_FUBI_KBN_05 = "";
        public int m_ZRO_FUBINO_05 = 0;
        public string m_REV_CLEARING_FLG = "";
        #endregion

        #region プロパティ

        public string _FILE_NAME
        {
            get { return m_FILE_NAME; }
        }
        public int _RECORD_SEQ
        {
            get { return m_RECORD_SEQ; }
        }

        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_FILE_NAME = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.FILE_NAME]);
            m_RECORD_SEQ = DBConvert.ToIntNull(dr[TBL_TSUCHITXT.RECORD_SEQ]);
            m_IMG_NAME = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.IMG_NAME]);
            m_BK_NO_TEISEI_FLG = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.BK_NO_TEISEI_FLG]);
            m_TEISEI_BEF_BK_NO = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.TEISEI_BEF_BK_NO]);
            m_TEISEI_AFT_BK_NO = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.TEISEI_AFT_BK_NO]);
            m_CLEARING_TEISEI_FLG = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.CLEARING_TEISEI_FLG]);
            m_TEISEI_BEF_CLEARING_DATE = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.TEISEI_BEF_CLEARING_DATE]);
            m_TEISEI_CLEARING_DATE = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.TEISEI_CLEARING_DATE]);
            m_AMOUNT_TEISEI_FLG = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.AMOUNT_TEISEI_FLG]);
            m_TEISEI_BEF_AMOUNT = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.TEISEI_BEF_AMOUNT]);
            m_TEISEI_AMOUNT = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.TEISEI_AMOUNT]);
            m_DUPLICATE_IMG_NAME = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.DUPLICATE_IMG_NAME]);
            m_FUBI_REG_KBN = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.FUBI_REG_KBN]);
            m_FUBI_KBN_01 = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.FUBI_KBN_01]);
            m_ZERO_FUBINO_01 = DBConvert.ToIntNull(dr[TBL_TSUCHITXT.ZERO_FUBINO_01]);
            m_FUBI_KBN_02 = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.FUBI_KBN_02]);
            m_ZRO_FUBINO_02 = DBConvert.ToIntNull(dr[TBL_TSUCHITXT.ZRO_FUBINO_02]);
            m_FUBI_KBN_03 = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.FUBI_KBN_03]);
            m_ZRO_FUBINO_03 = DBConvert.ToIntNull(dr[TBL_TSUCHITXT.ZRO_FUBINO_03]);
            m_FUBI_KBN_04 = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.FUBI_KBN_04]);
            m_ZRO_FUBINO_04 = DBConvert.ToIntNull(dr[TBL_TSUCHITXT.ZRO_FUBINO_04]);
            m_FUBI_KBN_05 = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.FUBI_KBN_05]);
            m_ZRO_FUBINO_05 = DBConvert.ToIntNull(dr[TBL_TSUCHITXT.ZRO_FUBINO_05]);
            m_REV_CLEARING_FLG = DBConvert.ToStringNull(dr[TBL_TSUCHITXT.REV_CLEARING_FLG]);

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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_TSUCHITXT.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TSUCHITXT.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_TSUCHITXT.FILE_NAME + "," +
                TBL_TSUCHITXT.RECORD_SEQ;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string file_name, int recordseq, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TSUCHITXT.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TSUCHITXT.FILE_NAME + "='" + file_name + "'"+
                "   AND " + TBL_TSUCHITXT.RECORD_SEQ + "=" + recordseq;
            return strSql;
        }

        /// <summary>
        /// ファイル名を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryFileName(string file_name, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TSUCHITXT.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TSUCHITXT.FILE_NAME + "='" + file_name + "'";
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_TSUCHITXT.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_TSUCHITXT.FILE_NAME + "," +
                TBL_TSUCHITXT.RECORD_SEQ + "," +
                TBL_TSUCHITXT.IMG_NAME + "," +
                TBL_TSUCHITXT.BK_NO_TEISEI_FLG + "," +
                TBL_TSUCHITXT.TEISEI_BEF_BK_NO + "," +
                TBL_TSUCHITXT.TEISEI_AFT_BK_NO + "," +
                TBL_TSUCHITXT.CLEARING_TEISEI_FLG + "," +
                TBL_TSUCHITXT.TEISEI_BEF_CLEARING_DATE + "," +
                TBL_TSUCHITXT.TEISEI_CLEARING_DATE + "," +
                TBL_TSUCHITXT.AMOUNT_TEISEI_FLG + "," +
                TBL_TSUCHITXT.TEISEI_BEF_AMOUNT + "," +
                TBL_TSUCHITXT.TEISEI_AMOUNT + "," +
                TBL_TSUCHITXT.DUPLICATE_IMG_NAME + "," +
                TBL_TSUCHITXT.FUBI_REG_KBN + "," +
                TBL_TSUCHITXT.FUBI_KBN_01 + "," +
                TBL_TSUCHITXT.ZERO_FUBINO_01 + "," +
                TBL_TSUCHITXT.FUBI_KBN_02 + "," +     
                TBL_TSUCHITXT.ZRO_FUBINO_02 + "," +
                TBL_TSUCHITXT.FUBI_KBN_03 + "," +
                TBL_TSUCHITXT.ZRO_FUBINO_03 + "," +
                TBL_TSUCHITXT.FUBI_KBN_04 + "," +
                TBL_TSUCHITXT.ZRO_FUBINO_04 + "," +
                TBL_TSUCHITXT.FUBI_KBN_05 + "," +
                TBL_TSUCHITXT.ZRO_FUBINO_05 + "," +
                TBL_TSUCHITXT.REV_CLEARING_FLG + ") VALUES (" +
                "'" + m_FILE_NAME + "'," +
                m_RECORD_SEQ + "," +
                "'" + m_IMG_NAME + "'," +
                "'" + m_BK_NO_TEISEI_FLG + "'," +
                "'" + m_TEISEI_BEF_BK_NO + "'," +
                "'" + m_TEISEI_AFT_BK_NO + "'," +
                "'" + m_CLEARING_TEISEI_FLG + "'," +
                "'" + m_TEISEI_BEF_CLEARING_DATE + "'," +
                "'" + m_TEISEI_CLEARING_DATE + "'," +
                "'" + m_AMOUNT_TEISEI_FLG + "'," +
                "'" + m_TEISEI_BEF_AMOUNT + "'," +
                "'" + m_TEISEI_AMOUNT + "'," +
                "'" + m_DUPLICATE_IMG_NAME + "'," +
                "'" + m_FUBI_REG_KBN + "'," +
                "'" + m_FUBI_KBN_01 + "'," +
                m_ZERO_FUBINO_01 + "," +
                "'" + m_FUBI_KBN_02 + "'," +
                m_ZRO_FUBINO_02 + "," +
                "'" + m_FUBI_KBN_03 + "'," +
                m_ZRO_FUBINO_03 + "," +
                "'" + m_FUBI_KBN_04 + "'," +
                m_ZRO_FUBINO_04 + "," +
                "'" + m_FUBI_KBN_05 + "'," +
                m_ZRO_FUBINO_05 + "," +
                "'" + m_REV_CLEARING_FLG + "' " + ")";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_TSUCHITXT.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                  TBL_TSUCHITXT.FILE_NAME + "='" + m_FILE_NAME + "' AND " +
                  TBL_TSUCHITXT.RECORD_SEQ + "=" + m_RECORD_SEQ;
            return strSql;
        }

        #endregion
    }
}
