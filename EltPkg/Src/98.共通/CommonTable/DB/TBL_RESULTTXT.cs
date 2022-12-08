using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 汎用エントリオペレータ処理状況
    /// </summary>
    public class TBL_RESULTTXT
    {
        public TBL_RESULTTXT(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_RESULTTXT(string filename, int seq, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            m_FILE_NAME = filename;
            m_SEQ = seq;
        }

        public TBL_RESULTTXT(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義

        public const string TABLE_PHYSICAL_NAME = "RESULTTXT";

        public const string FILE_NAME = "FILE_NAME";
        public const string SEQ = "SEQ";
        public const string IMG_NAME = "IMG_NAME";
        public const string RECEPTION = "RECEPTION";
        public const string RET_CODE = "RET_CODE";
        public const string FRONT_IMG_NAME = "FRONT_IMG_NAME";
        public const string IMG_KBN = "IMG_KBN";
        public const string FILE_OC_BK_NO = "FILE_OC_BK_NO";
        public const string CHG_OC_BK_NO = "CHG_OC_BK_NO";
        public const string OC_BR_NO = "OC_BR_NO";
        public const string OC_DATE = "OC_DATE";
        public const string OC_METHOD = "OC_METHOD";
        public const string OC_USERID = "OC_USERID";
        public const string PAY_KBN = "PAY_KBN";
        public const string BALANCE_FLG = "BALANCE_FLG";
        public const string OCR_IC_BK_NO = "OCR_IC_BK_NO";
        public const string QR_IC_BK_NO = "QR_IC_BK_NO";
        public const string MICR_IC_BK_NO = "MICR_IC_BK_NO";
        public const string FILE_IC_BK_NO = "FILE_IC_BK_NO";
        public const string CHG_IC_BK_NO = "CHG_IC_BK_NO";
        public const string TEISEI_IC_BK_NO = "TEISEI_IC_BK_NO";
        public const string PAY_IC_BK_NO = "PAY_IC_BK_NO";
        public const string PAYAFT_REV_IC_BK_NO = "PAYAFT_REV_IC_BK_NO";
        public const string OCR_IC_BK_NO_CONF = "OCR_IC_BK_NO_CONF";
        public const string OCR_AMOUNT = "OCR_AMOUNT";
        public const string MICR_AMOUNT = "MICR_AMOUNT";
        public const string QR_AMOUNT = "QR_AMOUNT";
        public const string FILE_AMOUNT = "FILE_AMOUNT";
        public const string TEISEI_AMOUNT = "TEISEI_AMOUNT";
        public const string PAY_AMOUNT = "PAY_AMOUNT";
        public const string PAYAFT_REV_AMOUNT = "PAYAFT_REV_AMOUNT";
        public const string OCR_AMOUNT_CONF = "OCR_AMOUNT_CONF";
        public const string OC_CLEARING_DATE = "OC_CLEARING_DATE";
        public const string TEISEI_CLEARING_DATE = "TEISEI_CLEARING_DATE";
        public const string CLEARING_DATE = "CLEARING_DATE";
        public const string QR_IC_BR_NO = "QR_IC_BR_NO";
        public const string KAMOKU = "KAMOKU";
        public const string ACCOUNT = "ACCOUNT";
        public const string BK_CTL_NO = "BK_CTL_NO";
        public const string FREEFIELD = "FREEFIELD";
        public const string BILL_CODE = "BILL_CODE";
        public const string BILL_CODE_CONF = "BILL_CODE_CONF";
        public const string QR = "QR";
        public const string MICR = "MICR";
        public const string MICR_CONF = "MICR_CONF";
        public const string BILL_NO = "BILL_NO";
        public const string BILL_NO_CONF = "BILL_NO_CONF";
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
        public const string IC_FLG = "IC_FLG";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;
        private string m_FILE_NAME = "";
        private int m_SEQ = 0;
        public string m_RECEPTION = "";
        public string m_RET_CODE = "";
        public string m_IMG_NAME = "";
        public string m_FRONT_IMG_NAME = "";
        public int m_IMG_KBN = 0;
        public string m_FILE_OC_BK_NO = "";
        public string m_CHG_OC_BK_NO = "";
        public string m_OC_BR_NO = "";
        public int m_OC_DATE = 0;
        public string m_OC_METHOD = "";
        public string m_OC_USERID = "";
        public string m_PAY_KBN = "";
        public string m_BALANCE_FLG = "";
        public string m_OCR_IC_BK_NO = "";
        public string m_QR_IC_BK_NO = "";
        public string m_MICR_IC_BK_NO = "";
        public string m_FILE_IC_BK_NO = "";
        public string m_CHG_IC_BK_NO = "";
        public string m_TEISEI_IC_BK_NO = "";
        public string m_PAY_IC_BK_NO = "";
        public string m_PAYAFT_REV_IC_BK_NO = "";
        public string m_OCR_IC_BK_NO_CONF = "";
        public string m_OCR_AMOUNT = "";
        public string m_MICR_AMOUNT = "";
        public string m_QR_AMOUNT = "";
        public string m_FILE_AMOUNT = "";
        public string m_TEISEI_AMOUNT = "";
        public string m_PAY_AMOUNT = "";
        public string m_PAYAFT_REV_AMOUNT = "";
        public string m_OCR_AMOUNT_CONF = "";
        public string m_OC_CLEARING_DATE = "";
        public string m_TEISEI_CLEARING_DATE = "";
        public string m_CLEARING_DATE = "";
        public string m_QR_IC_BR_NO = "";
        public string m_KAMOKU = "";
        public string m_ACCOUNT = "";
        public string m_BK_CTL_NO = "";
        public string m_FREEFIELD = "";
        public string m_BILL_CODE = "";
        public string m_BILL_CODE_CONF = "";
        public string m_QR = "";
        public string m_MICR = "";
        public string m_MICR_CONF = "";
        public string m_BILL_NO = "";
        public string m_BILL_NO_CONF = "";
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
        public string m_IC_FLG = "";

        #endregion

        #region プロパティ

        public string _FILE_NAME
        {
            get { return m_FILE_NAME; }
        }
        public int _SEQ
        {
            get { return m_SEQ; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_FILE_NAME = DBConvert.ToStringNull(dr[TBL_RESULTTXT.FILE_NAME]);
            m_SEQ = DBConvert.ToIntNull(dr[TBL_RESULTTXT.SEQ]);
            m_RECEPTION = DBConvert.ToStringNull(dr[TBL_RESULTTXT.RECEPTION]);
            m_RET_CODE = DBConvert.ToStringNull(dr[TBL_RESULTTXT.RET_CODE]);
            m_IMG_NAME = DBConvert.ToStringNull(dr[TBL_RESULTTXT.IMG_NAME]);
            m_FRONT_IMG_NAME = DBConvert.ToStringNull(dr[TBL_RESULTTXT.FRONT_IMG_NAME]);
            m_IMG_KBN = DBConvert.ToIntNull(dr[TBL_RESULTTXT.IMG_KBN]);
            m_FILE_OC_BK_NO = DBConvert.ToStringNull(dr[TBL_RESULTTXT.FILE_OC_BK_NO]);
            m_CHG_OC_BK_NO = DBConvert.ToStringNull(dr[TBL_RESULTTXT.CHG_OC_BK_NO]);
            m_OC_BR_NO = DBConvert.ToStringNull(dr[TBL_RESULTTXT.OC_BR_NO]);
            m_OC_DATE = DBConvert.ToIntNull(dr[TBL_RESULTTXT.OC_DATE]);
            m_OC_METHOD = DBConvert.ToStringNull(dr[TBL_RESULTTXT.OC_METHOD]);
            m_OC_USERID = DBConvert.ToStringNull(dr[TBL_RESULTTXT.OC_USERID]);
            m_PAY_KBN = DBConvert.ToStringNull(dr[TBL_RESULTTXT.PAY_KBN]);
            m_BALANCE_FLG = DBConvert.ToStringNull(dr[TBL_RESULTTXT.BALANCE_FLG]);
            m_OCR_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_RESULTTXT.OCR_IC_BK_NO]);
            m_QR_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_RESULTTXT.QR_IC_BK_NO]);
            m_MICR_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_RESULTTXT.MICR_IC_BK_NO]);
            m_FILE_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_RESULTTXT.FILE_IC_BK_NO]);
            m_CHG_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_RESULTTXT.CHG_IC_BK_NO]);
            m_TEISEI_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_RESULTTXT.TEISEI_IC_BK_NO]);
            m_PAY_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_RESULTTXT.PAY_IC_BK_NO]);
            m_PAYAFT_REV_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_RESULTTXT.PAYAFT_REV_IC_BK_NO]);
            m_OCR_IC_BK_NO_CONF = DBConvert.ToStringNull(dr[TBL_RESULTTXT.OCR_IC_BK_NO_CONF]);
            m_OCR_AMOUNT = DBConvert.ToStringNull(dr[TBL_RESULTTXT.OCR_AMOUNT]);
            m_MICR_AMOUNT = DBConvert.ToStringNull(dr[TBL_RESULTTXT.MICR_AMOUNT]);
            m_QR_AMOUNT = DBConvert.ToStringNull(dr[TBL_RESULTTXT.QR_AMOUNT]);
            m_FILE_AMOUNT = DBConvert.ToStringNull(dr[TBL_RESULTTXT.FILE_AMOUNT]);
            m_TEISEI_AMOUNT = DBConvert.ToStringNull(dr[TBL_RESULTTXT.TEISEI_AMOUNT]);
            m_PAY_AMOUNT = DBConvert.ToStringNull(dr[TBL_RESULTTXT.PAY_AMOUNT]);
            m_PAYAFT_REV_AMOUNT = DBConvert.ToStringNull(dr[TBL_RESULTTXT.PAYAFT_REV_AMOUNT]);
            m_OCR_AMOUNT_CONF = DBConvert.ToStringNull(dr[TBL_RESULTTXT.OCR_AMOUNT_CONF]);
            m_OC_CLEARING_DATE = DBConvert.ToStringNull(dr[TBL_RESULTTXT.OC_CLEARING_DATE]);
            m_TEISEI_CLEARING_DATE = DBConvert.ToStringNull(dr[TBL_RESULTTXT.TEISEI_CLEARING_DATE]);
            m_CLEARING_DATE = DBConvert.ToStringNull(dr[TBL_RESULTTXT.CLEARING_DATE]);
            m_QR_IC_BR_NO = DBConvert.ToStringNull(dr[TBL_RESULTTXT.QR_IC_BR_NO]);
            m_KAMOKU = DBConvert.ToStringNull(dr[TBL_RESULTTXT.KAMOKU]);
            m_ACCOUNT = DBConvert.ToStringNull(dr[TBL_RESULTTXT.ACCOUNT]);
            m_BK_CTL_NO = DBConvert.ToStringNull(dr[TBL_RESULTTXT.BK_CTL_NO]);
            m_FREEFIELD = DBConvert.ToStringNull(dr[TBL_RESULTTXT.FREEFIELD]);
            m_BILL_CODE = DBConvert.ToStringNull(dr[TBL_RESULTTXT.BILL_CODE]);
            m_BILL_CODE_CONF = DBConvert.ToStringNull(dr[TBL_RESULTTXT.BILL_CODE_CONF]);
            m_QR = DBConvert.ToStringNull(dr[TBL_RESULTTXT.QR]);
            m_MICR = DBConvert.ToStringNull(dr[TBL_RESULTTXT.MICR]);
            m_MICR_CONF = DBConvert.ToStringNull(dr[TBL_RESULTTXT.MICR_CONF]);
            m_BILL_NO = DBConvert.ToStringNull(dr[TBL_RESULTTXT.BILL_NO]);
            m_BILL_NO_CONF = DBConvert.ToStringNull(dr[TBL_RESULTTXT.BILL_NO_CONF]);
            m_FUBI_KBN_01 = DBConvert.ToStringNull(dr[TBL_RESULTTXT.FUBI_KBN_01]);
            m_ZERO_FUBINO_01 = DBConvert.ToIntNull(dr[TBL_RESULTTXT.ZERO_FUBINO_01]);
            m_FUBI_KBN_02 = DBConvert.ToStringNull(dr[TBL_RESULTTXT.FUBI_KBN_02]);
            m_ZRO_FUBINO_02 = DBConvert.ToIntNull(dr[TBL_RESULTTXT.ZRO_FUBINO_02]);
            m_FUBI_KBN_03 = DBConvert.ToStringNull(dr[TBL_RESULTTXT.FUBI_KBN_03]);
            m_ZRO_FUBINO_03 = DBConvert.ToIntNull(dr[TBL_RESULTTXT.ZRO_FUBINO_03]);
            m_FUBI_KBN_04 = DBConvert.ToStringNull(dr[TBL_RESULTTXT.FUBI_KBN_04]);
            m_ZRO_FUBINO_04 = DBConvert.ToIntNull(dr[TBL_RESULTTXT.ZRO_FUBINO_04]);
            m_FUBI_KBN_05 = DBConvert.ToStringNull(dr[TBL_RESULTTXT.FUBI_KBN_05]);
            m_ZRO_FUBINO_05 = DBConvert.ToIntNull(dr[TBL_RESULTTXT.ZRO_FUBINO_05]);
            m_IC_FLG = DBConvert.ToStringNull(dr[TBL_RESULTTXT.IC_FLG]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_RESULTTXT.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_RESULTTXT.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_RESULTTXT.FILE_NAME + ", " +
                TBL_RESULTTXT.SEQ;
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string FILE_NAME, string seq, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_RESULTTXT.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                        TBL_RESULTTXT.FILE_NAME + "='" + FILE_NAME + "' AND " +
                        TBL_RESULTTXT.SEQ + "=" + seq;
            return strSql;
        }

        /// <summary>
        /// FILE_NAMEを条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQueryFileName(string FILE_NAME, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_RESULTTXT.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                        TBL_RESULTTXT.FILE_NAME + "='" + FILE_NAME + "' ";
            return strSql;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSql = "INSERT INTO " + TBL_RESULTTXT.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_RESULTTXT.FILE_NAME + "," +
                TBL_RESULTTXT.SEQ + "," +
                TBL_RESULTTXT.RECEPTION + "," +
                TBL_RESULTTXT.RET_CODE + "," +
                TBL_RESULTTXT.IMG_NAME + "," +
                TBL_RESULTTXT.FRONT_IMG_NAME + "," +
                TBL_RESULTTXT.IMG_KBN + "," +
                TBL_RESULTTXT.FILE_OC_BK_NO + "," +
                TBL_RESULTTXT.CHG_OC_BK_NO + "," +
                TBL_RESULTTXT.OC_BR_NO + "," +
                TBL_RESULTTXT.OC_DATE + "," +
                TBL_RESULTTXT.OC_METHOD + "," +
                TBL_RESULTTXT.OC_USERID + "," +
                TBL_RESULTTXT.PAY_KBN + "," +
                TBL_RESULTTXT.BALANCE_FLG + "," +
                TBL_RESULTTXT.OCR_IC_BK_NO + "," +
                TBL_RESULTTXT.QR_IC_BK_NO + "," +
                TBL_RESULTTXT.MICR_IC_BK_NO + "," +
                TBL_RESULTTXT.FILE_IC_BK_NO + "," +
                TBL_RESULTTXT.CHG_IC_BK_NO + "," +
                TBL_RESULTTXT.TEISEI_IC_BK_NO + "," +
                TBL_RESULTTXT.PAY_IC_BK_NO + "," +
                TBL_RESULTTXT.PAYAFT_REV_IC_BK_NO + "," +
                TBL_RESULTTXT.OCR_IC_BK_NO_CONF + "," +
                TBL_RESULTTXT.OCR_AMOUNT + "," +
                TBL_RESULTTXT.MICR_AMOUNT + "," +
                TBL_RESULTTXT.QR_AMOUNT + "," +
                TBL_RESULTTXT.FILE_AMOUNT + "," +
                TBL_RESULTTXT.TEISEI_AMOUNT + "," +
                TBL_RESULTTXT.PAY_AMOUNT + "," +
                TBL_RESULTTXT.PAYAFT_REV_AMOUNT + "," +
                TBL_RESULTTXT.OCR_AMOUNT_CONF + "," +
                TBL_RESULTTXT.OC_CLEARING_DATE + "," +
                TBL_RESULTTXT.TEISEI_CLEARING_DATE + "," +
                TBL_RESULTTXT.CLEARING_DATE + "," +
                TBL_RESULTTXT.QR_IC_BR_NO + "," +
                TBL_RESULTTXT.KAMOKU + "," +
                TBL_RESULTTXT.ACCOUNT + "," +
                TBL_RESULTTXT.BK_CTL_NO + "," +
                TBL_RESULTTXT.FREEFIELD + "," +
                TBL_RESULTTXT.BILL_CODE + "," +
                TBL_RESULTTXT.BILL_CODE_CONF + "," +
                TBL_RESULTTXT.QR + "," +
                TBL_RESULTTXT.MICR + "," +
                TBL_RESULTTXT.MICR_CONF + "," +
                TBL_RESULTTXT.BILL_NO + "," +
                TBL_RESULTTXT.BILL_NO_CONF + "," +
                TBL_RESULTTXT.FUBI_KBN_01 + "," +
                TBL_RESULTTXT.ZERO_FUBINO_01 + "," +
                TBL_RESULTTXT.FUBI_KBN_02 + "," +
                TBL_RESULTTXT.ZRO_FUBINO_02 + "," +
                TBL_RESULTTXT.FUBI_KBN_03 + "," +
                TBL_RESULTTXT.ZRO_FUBINO_03 + "," +
                TBL_RESULTTXT.FUBI_KBN_04 + "," +
                TBL_RESULTTXT.ZRO_FUBINO_04 + "," +
                TBL_RESULTTXT.FUBI_KBN_05 + "," +
                TBL_RESULTTXT.ZRO_FUBINO_05 + "," +
                TBL_RESULTTXT.IC_FLG + ") VALUES (" +
                "'" + m_FILE_NAME + "'," +
                m_SEQ + "," +
                "'" + m_RECEPTION + "'," +
                "'" + m_RET_CODE + "'," +
                "'" + m_IMG_NAME + "'," +
                "'" + m_FRONT_IMG_NAME + "'," +
                m_IMG_KBN + "," +
                "'" + m_FILE_OC_BK_NO + "'," +
                "'" + m_CHG_OC_BK_NO + "'," +
                "'" + m_OC_BR_NO + "'," +
                m_OC_DATE + "," +
                "'" + m_OC_METHOD + "'," +
                "'" + m_OC_USERID + "'," +
                "'" + m_PAY_KBN + "'," +
                "'" + m_BALANCE_FLG + "'," +
                "'" + m_OCR_IC_BK_NO + "'," +
                "'" + m_QR_IC_BK_NO + "'," +
                "'" + m_MICR_IC_BK_NO + "'," +
                "'" + m_FILE_IC_BK_NO + "'," +
                "'" + m_CHG_IC_BK_NO + "'," +
                "'" + m_TEISEI_IC_BK_NO + "'," +
                "'" + m_PAY_IC_BK_NO + "'," +
                "'" + m_PAYAFT_REV_IC_BK_NO + "'," +
                "'" + m_OCR_IC_BK_NO_CONF + "'," +
                "'" + m_OCR_AMOUNT + "'," +
                "'" + m_MICR_AMOUNT + "'," +
                "'" + m_QR_AMOUNT + "'," +
                "'" + m_FILE_AMOUNT + "'," +
                "'" + m_TEISEI_AMOUNT + "'," +
                "'" + m_PAY_AMOUNT + "'," +
                "'" + m_PAYAFT_REV_AMOUNT + "'," +
                "'" + m_OCR_AMOUNT_CONF + "'," +
                "'" + m_OC_CLEARING_DATE + "'," +
                "'" + m_TEISEI_CLEARING_DATE + "'," +
                "'" + m_CLEARING_DATE + "'," +
                "'" + m_QR_IC_BR_NO + "'," +
                "'" + m_KAMOKU + "'," +
                "'" + m_ACCOUNT + "'," +
                "'" + m_BK_CTL_NO + "'," +
                "'" + m_FREEFIELD + "'," +
                "'" + m_BILL_CODE + "'," +
                "'" + m_BILL_CODE_CONF + "'," +
                "'" + m_QR + "'," +
                "'" + m_MICR + "'," +
                "'" + m_MICR_CONF + "'," +
                "'" + m_BILL_NO + "'," +
                "'" + m_BILL_NO_CONF + "'," +
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
                "'" + m_IC_FLG + "' " + ")";
            return strSql;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSql = "DELETE FROM " + TBL_RESULTTXT.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                  TBL_RESULTTXT.FILE_NAME + "='" + m_FILE_NAME + "' AND " +
                  TBL_RESULTTXT.SEQ + "=" + m_SEQ;
            return strSql;
        }

        #endregion
    }
}
