using System.Data;

namespace CommonTable.DB
{
    /// <summary>
    /// 持帰要求結果証券明細テキスト
    /// </summary>
    public class TBL_ICREQRET_BILLMEITXT
    {
        public TBL_ICREQRET_BILLMEITXT(int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
        }

        public TBL_ICREQRET_BILLMEITXT(string meitxtname, int capkbn, string imgname, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;

            m_MEI_TXT_NAME = meitxtname;
            m_CAP_KBN = capkbn;
            m_IMG_NAME = imgname;
        }

        public TBL_ICREQRET_BILLMEITXT(DataRow dr, int Schemabankcd)
        {
            m_SCHEMABANKCD = Schemabankcd;
            initializeByDataRow(dr);
        }

        #region テーブル定義
        public const string TABLE_PHYSICAL_NAME = "ICREQRET_BILLMEITXT";

        public const string MEI_TXT_NAME = "MEI_TXT_NAME";
        public const string CAP_KBN = "CAP_KBN";
        public const string IMG_NAME = "IMG_NAME";
        public const string IMG_ARCH_NAME = "IMG_ARCH_NAME";
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
        public const string KAKUTEI_FLG = "KAKUTEI_FLG";
        public const string KAKUTEI_DATE = "KAKUTEI_DATE";
        public const string KAKUTEI_TIME = "KAKUTEI_TIME";
        public const string KAKUTEI_OPE = "KAKUTEI_OPE";

        public const string ALL_COLUMNS = " MEI_TXT_NAME,CAP_KBN,IMG_NAME,IMG_ARCH_NAME,FRONT_IMG_NAME,IMG_KBN,FILE_OC_BK_NO,CHG_OC_BK_NO,OC_BR_NO,OC_DATE,OC_METHOD,OC_USERID,PAY_KBN,BALANCE_FLG,OCR_IC_BK_NO,QR_IC_BK_NO,MICR_IC_BK_NO,FILE_IC_BK_NO,CHG_IC_BK_NO,TEISEI_IC_BK_NO,PAY_IC_BK_NO,PAYAFT_REV_IC_BK_NO,OCR_IC_BK_NO_CONF,OCR_AMOUNT,MICR_AMOUNT,QR_AMOUNT,FILE_AMOUNT,TEISEI_AMOUNT,PAY_AMOUNT,PAYAFT_REV_AMOUNT,OCR_AMOUNT_CONF,OC_CLEARING_DATE,TEISEI_CLEARING_DATE,CLEARING_DATE,QR_IC_BR_NO,KAMOKU,ACCOUNT,BK_CTL_NO,FREEFIELD,BILL_CODE,BILL_CODE_CONF,QR,MICR,MICR_CONF,BILL_NO,BILL_NO_CONF,FUBI_KBN_01,ZERO_FUBINO_01,FUBI_KBN_02,ZRO_FUBINO_02,FUBI_KBN_03,ZRO_FUBINO_03,FUBI_KBN_04,ZRO_FUBINO_04,FUBI_KBN_05,ZRO_FUBINO_05,IC_FLG,KAKUTEI_FLG,KAKUTEI_DATE,KAKUTEI_TIME,KAKUTEI_OPE ";
        #endregion

        #region メンバ
        private int m_SCHEMABANKCD = 0;

        private string m_MEI_TXT_NAME = "";
        private int m_CAP_KBN = 0;
        private string m_IMG_NAME = "";
        public string m_IMG_ARCH_NAME = "";
        public string m_FRONT_IMG_NAME = "";
        public int m_IMG_KBN = 0;
        public string m_FILE_OC_BK_NO = "";
        public string m_CHG_OC_BK_NO = "";
        public string m_OC_BR_NO = "";
        public int m_OC_DATE = 0;
        public string m_OC_METHOD = "";
        public string m_OC_USERID = "";
        public string m_PAY_KBN = "";
        public string m_BALANCE_FLG = "0";
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
        public string m_IC_FLG = "0";
        public int m_KAKUTEI_FLG = 0;
        public int m_KAKUTEI_DATE = 0;
        public int m_KAKUTEI_TIME = 0;
        public string m_KAKUTEI_OPE = "";

        #endregion

        #region プロパティ

        public string _MEI_TXT_NAME
        {
            get { return m_MEI_TXT_NAME; }
        }
        public int _CAP_KBN
        {
            get { return m_CAP_KBN; }
        }
        public string _IMG_NAME
        {
            get { return m_IMG_NAME; }
        }
        #endregion

        #region 初期化

        /// <summary>
        /// DataRowの値をセットする
        /// </summary>
        /// <param name="dr"></param>
        protected void initializeByDataRow(DataRow dr)
        {
            m_MEI_TXT_NAME = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.MEI_TXT_NAME]);
            m_CAP_KBN = DBConvert.ToIntNull(dr[TBL_ICREQRET_BILLMEITXT.CAP_KBN]);
            m_IMG_NAME = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.IMG_NAME]);
            m_IMG_ARCH_NAME = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.IMG_ARCH_NAME]);
            m_FRONT_IMG_NAME = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME]);
            m_IMG_KBN = DBConvert.ToIntNull(dr[TBL_ICREQRET_BILLMEITXT.IMG_KBN]);
            m_FILE_OC_BK_NO = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.FILE_OC_BK_NO]);
            m_CHG_OC_BK_NO = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.CHG_OC_BK_NO]);
            m_OC_BR_NO = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.OC_BR_NO]);
            m_OC_DATE = DBConvert.ToIntNull(dr[TBL_ICREQRET_BILLMEITXT.OC_DATE]);
            m_OC_METHOD = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.OC_METHOD]);
            m_OC_USERID = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.OC_USERID]);
            m_PAY_KBN = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.PAY_KBN]);
            m_BALANCE_FLG = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.BALANCE_FLG]);
            m_OCR_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.OCR_IC_BK_NO]);
            m_QR_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.QR_IC_BK_NO]);
            m_MICR_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.MICR_IC_BK_NO]);
            m_FILE_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.FILE_IC_BK_NO]);
            m_CHG_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.CHG_IC_BK_NO]);
            m_TEISEI_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.TEISEI_IC_BK_NO]);
            m_PAY_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.PAY_IC_BK_NO]);
            m_PAYAFT_REV_IC_BK_NO = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.PAYAFT_REV_IC_BK_NO]);
            m_OCR_IC_BK_NO_CONF = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.OCR_IC_BK_NO_CONF]);
            m_OCR_AMOUNT = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.OCR_AMOUNT]);
            m_MICR_AMOUNT = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.MICR_AMOUNT]);
            m_QR_AMOUNT = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.QR_AMOUNT]);
            m_FILE_AMOUNT = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.FILE_AMOUNT]);
            m_TEISEI_AMOUNT = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.TEISEI_AMOUNT]);
            m_PAY_AMOUNT = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.PAY_AMOUNT]);
            m_PAYAFT_REV_AMOUNT = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.PAYAFT_REV_AMOUNT]);
            m_OCR_AMOUNT_CONF = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.OCR_AMOUNT_CONF]);
            m_OC_CLEARING_DATE = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.OC_CLEARING_DATE]);
            m_TEISEI_CLEARING_DATE = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.TEISEI_CLEARING_DATE]);
            m_CLEARING_DATE = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.CLEARING_DATE]);
            m_QR_IC_BR_NO = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.QR_IC_BR_NO]);
            m_KAMOKU = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.KAMOKU]);
            m_ACCOUNT = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.ACCOUNT]);
            m_BK_CTL_NO = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.BK_CTL_NO]);
            m_FREEFIELD = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.FREEFIELD]);
            m_BILL_CODE = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.BILL_CODE]);
            m_BILL_CODE_CONF = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.BILL_CODE_CONF]);
            m_QR = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.QR]);
            m_MICR = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.MICR]);
            m_MICR_CONF = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.MICR_CONF]);
            m_BILL_NO = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.BILL_NO]);
            m_BILL_NO_CONF = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.BILL_NO_CONF]);
            m_FUBI_KBN_01 = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.FUBI_KBN_01]);
            m_ZERO_FUBINO_01 = DBConvert.ToIntNull(dr[TBL_ICREQRET_BILLMEITXT.ZERO_FUBINO_01]);
            m_FUBI_KBN_02 = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.FUBI_KBN_02]);
            m_ZRO_FUBINO_02 = DBConvert.ToIntNull(dr[TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_02]);
            m_FUBI_KBN_03 = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.FUBI_KBN_03]);
            m_ZRO_FUBINO_03 = DBConvert.ToIntNull(dr[TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_03]);
            m_FUBI_KBN_04 = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.FUBI_KBN_04]);
            m_ZRO_FUBINO_04 = DBConvert.ToIntNull(dr[TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_04]);
            m_FUBI_KBN_05 = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.FUBI_KBN_05]);
            m_ZRO_FUBINO_05 = DBConvert.ToIntNull(dr[TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_05]);
            m_IC_FLG = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.IC_FLG]);
            m_KAKUTEI_FLG = DBConvert.ToIntNull(dr[TBL_ICREQRET_BILLMEITXT.KAKUTEI_FLG]);
            m_KAKUTEI_DATE = DBConvert.ToIntNull(dr[TBL_ICREQRET_BILLMEITXT.KAKUTEI_DATE]);
            m_KAKUTEI_TIME = DBConvert.ToIntNull(dr[TBL_ICREQRET_BILLMEITXT.KAKUTEI_TIME]);
            m_KAKUTEI_OPE = DBConvert.ToStringNull(dr[TBL_ICREQRET_BILLMEITXT.KAKUTEI_OPE]);
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
            return string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, Schemabankcd) + "." + TBL_ICREQRET_BILLMEITXT.TABLE_PHYSICAL_NAME;
        }

        #endregion

        #region クエリ取得

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_ICREQRET_BILLMEITXT.TABLE_NAME(Schemabankcd) +
                " ORDER BY " +
                TBL_ICREQRET_BILLMEITXT.MEI_TXT_NAME + "," +
                TBL_ICREQRET_BILLMEITXT.CAP_KBN + "," +
                TBL_ICREQRET_BILLMEITXT.IMG_NAME;
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSelectQuery(string meitxtname, int capkbn, string imgname, int Schemabankcd)
        {
            string strSQL = "SELECT * FROM " + TBL_ICREQRET_BILLMEITXT.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_ICREQRET_BILLMEITXT.MEI_TXT_NAME + "='" + meitxtname + "' AND " +
                TBL_ICREQRET_BILLMEITXT.CAP_KBN + "=" + capkbn + " AND " +
                TBL_ICREQRET_BILLMEITXT.IMG_NAME + "='" + imgname + "' ";
            return strSQL;
        }

        /// <summary>
        /// INSERT文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetInsertQuery()
        {
            string strSQL = "INSERT INTO " + TBL_ICREQRET_BILLMEITXT.TABLE_NAME(m_SCHEMABANKCD) + " (" +
                TBL_ICREQRET_BILLMEITXT.MEI_TXT_NAME + "," +
                TBL_ICREQRET_BILLMEITXT.CAP_KBN + "," +
                TBL_ICREQRET_BILLMEITXT.IMG_NAME + "," +
                TBL_ICREQRET_BILLMEITXT.IMG_ARCH_NAME + "," +
                TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME + "," +
                TBL_ICREQRET_BILLMEITXT.IMG_KBN + "," +
                TBL_ICREQRET_BILLMEITXT.FILE_OC_BK_NO + "," +
                TBL_ICREQRET_BILLMEITXT.CHG_OC_BK_NO + "," +
                TBL_ICREQRET_BILLMEITXT.OC_BR_NO + "," +
                TBL_ICREQRET_BILLMEITXT.OC_DATE + "," +
                TBL_ICREQRET_BILLMEITXT.OC_METHOD + "," +
                TBL_ICREQRET_BILLMEITXT.OC_USERID + "," +
                TBL_ICREQRET_BILLMEITXT.PAY_KBN + "," +
                TBL_ICREQRET_BILLMEITXT.BALANCE_FLG + "," +
                TBL_ICREQRET_BILLMEITXT.OCR_IC_BK_NO + "," +
                TBL_ICREQRET_BILLMEITXT.QR_IC_BK_NO + "," +
                TBL_ICREQRET_BILLMEITXT.MICR_IC_BK_NO + "," +
                TBL_ICREQRET_BILLMEITXT.FILE_IC_BK_NO + "," +
                TBL_ICREQRET_BILLMEITXT.CHG_IC_BK_NO + "," +
                TBL_ICREQRET_BILLMEITXT.TEISEI_IC_BK_NO + "," +
                TBL_ICREQRET_BILLMEITXT.PAY_IC_BK_NO + "," +
                TBL_ICREQRET_BILLMEITXT.PAYAFT_REV_IC_BK_NO + "," +
                TBL_ICREQRET_BILLMEITXT.OCR_IC_BK_NO_CONF + "," +
                TBL_ICREQRET_BILLMEITXT.OCR_AMOUNT + "," +
                TBL_ICREQRET_BILLMEITXT.MICR_AMOUNT + "," +
                TBL_ICREQRET_BILLMEITXT.QR_AMOUNT + "," +
                TBL_ICREQRET_BILLMEITXT.FILE_AMOUNT + "," +
                TBL_ICREQRET_BILLMEITXT.TEISEI_AMOUNT + "," +
                TBL_ICREQRET_BILLMEITXT.PAY_AMOUNT + "," +
                TBL_ICREQRET_BILLMEITXT.PAYAFT_REV_AMOUNT + "," +
                TBL_ICREQRET_BILLMEITXT.OCR_AMOUNT_CONF + "," +
                TBL_ICREQRET_BILLMEITXT.OC_CLEARING_DATE + "," +
                TBL_ICREQRET_BILLMEITXT.TEISEI_CLEARING_DATE + "," +
                TBL_ICREQRET_BILLMEITXT.CLEARING_DATE + "," +
                TBL_ICREQRET_BILLMEITXT.QR_IC_BR_NO + "," +
                TBL_ICREQRET_BILLMEITXT.KAMOKU + "," +
                TBL_ICREQRET_BILLMEITXT.ACCOUNT + "," +
                TBL_ICREQRET_BILLMEITXT.BK_CTL_NO + "," +
                TBL_ICREQRET_BILLMEITXT.FREEFIELD + "," +
                TBL_ICREQRET_BILLMEITXT.BILL_CODE + "," +
                TBL_ICREQRET_BILLMEITXT.BILL_CODE_CONF + "," +
                TBL_ICREQRET_BILLMEITXT.QR + "," +
                TBL_ICREQRET_BILLMEITXT.MICR + "," +
                TBL_ICREQRET_BILLMEITXT.MICR_CONF + "," +
                TBL_ICREQRET_BILLMEITXT.BILL_NO + "," +
                TBL_ICREQRET_BILLMEITXT.BILL_NO_CONF + "," +
                TBL_ICREQRET_BILLMEITXT.FUBI_KBN_01 + "," +
                TBL_ICREQRET_BILLMEITXT.ZERO_FUBINO_01 + "," +
                TBL_ICREQRET_BILLMEITXT.FUBI_KBN_02 + "," +
                TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_02 + "," +
                TBL_ICREQRET_BILLMEITXT.FUBI_KBN_03 + "," +
                TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_03 + "," +
                TBL_ICREQRET_BILLMEITXT.FUBI_KBN_04 + "," +
                TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_04 + "," +
                TBL_ICREQRET_BILLMEITXT.FUBI_KBN_05 + "," +
                TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_05 + "," +
                TBL_ICREQRET_BILLMEITXT.IC_FLG + "," +
                TBL_ICREQRET_BILLMEITXT.KAKUTEI_FLG + "," +
                TBL_ICREQRET_BILLMEITXT.KAKUTEI_DATE + "," +
                TBL_ICREQRET_BILLMEITXT.KAKUTEI_TIME + "," +
                TBL_ICREQRET_BILLMEITXT.KAKUTEI_OPE + ") VALUES (" +
                "'" + m_MEI_TXT_NAME + "'," +
                m_CAP_KBN + "," +
                "'" + m_IMG_NAME + "'," +
                "'" + m_IMG_ARCH_NAME + "'," +
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
                "'" + m_IC_FLG + "'," +
                m_KAKUTEI_FLG + "," +
                m_KAKUTEI_DATE + "," +
                m_KAKUTEI_TIME + "," +
                "'" + m_KAKUTEI_OPE + "')";
            return strSQL;
        }

        /// <summary>
        /// UPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            string strSQL = "UPDATE " + TBL_ICREQRET_BILLMEITXT.TABLE_NAME(m_SCHEMABANKCD) + " SET " +
                TBL_ICREQRET_BILLMEITXT.IMG_ARCH_NAME + "='" + m_IMG_ARCH_NAME + "', " +
                TBL_ICREQRET_BILLMEITXT.FRONT_IMG_NAME + "='" + m_FRONT_IMG_NAME + "', " +
                TBL_ICREQRET_BILLMEITXT.IMG_KBN + "=" + m_IMG_KBN + ", " +
                TBL_ICREQRET_BILLMEITXT.FILE_OC_BK_NO + "='" + m_FILE_OC_BK_NO + "', " +
                TBL_ICREQRET_BILLMEITXT.CHG_OC_BK_NO + "='" + m_CHG_OC_BK_NO + "', " +
                TBL_ICREQRET_BILLMEITXT.OC_BR_NO + "='" + m_OC_BR_NO + "', " +
                TBL_ICREQRET_BILLMEITXT.OC_DATE + "=" + m_OC_DATE + ", " +
                TBL_ICREQRET_BILLMEITXT.OC_METHOD + "='" + m_OC_METHOD + "', " +
                TBL_ICREQRET_BILLMEITXT.OC_USERID + "='" + m_OC_USERID + "', " +
                TBL_ICREQRET_BILLMEITXT.PAY_KBN + "='" + m_PAY_KBN + "', " +
                TBL_ICREQRET_BILLMEITXT.BALANCE_FLG + "='" + m_BALANCE_FLG + "', " +
                TBL_ICREQRET_BILLMEITXT.OCR_IC_BK_NO + "='" + m_OCR_IC_BK_NO + "', " +
                TBL_ICREQRET_BILLMEITXT.QR_IC_BK_NO + "='" + m_QR_IC_BK_NO + "', " +
                TBL_ICREQRET_BILLMEITXT.MICR_IC_BK_NO + "='" + m_MICR_IC_BK_NO + "', " +
                TBL_ICREQRET_BILLMEITXT.FILE_IC_BK_NO + "='" + m_FILE_IC_BK_NO + "', " +
                TBL_ICREQRET_BILLMEITXT.CHG_IC_BK_NO + "='" + m_CHG_IC_BK_NO + "', " +
                TBL_ICREQRET_BILLMEITXT.TEISEI_IC_BK_NO + "='" + m_TEISEI_IC_BK_NO + "', " +
                TBL_ICREQRET_BILLMEITXT.PAY_IC_BK_NO + "='" + m_PAY_IC_BK_NO + "', " +
                TBL_ICREQRET_BILLMEITXT.PAYAFT_REV_IC_BK_NO + "='" + m_PAYAFT_REV_IC_BK_NO + "', " +
                TBL_ICREQRET_BILLMEITXT.OCR_IC_BK_NO_CONF + "='" + m_OCR_IC_BK_NO_CONF + "', " +
                TBL_ICREQRET_BILLMEITXT.OCR_AMOUNT + "='" + m_OCR_AMOUNT + "', " +
                TBL_ICREQRET_BILLMEITXT.MICR_AMOUNT + "='" + m_MICR_AMOUNT + "', " +
                TBL_ICREQRET_BILLMEITXT.QR_AMOUNT + "='" + m_QR_AMOUNT + "', " +
                TBL_ICREQRET_BILLMEITXT.FILE_AMOUNT + "='" + m_FILE_AMOUNT + "', " +
                TBL_ICREQRET_BILLMEITXT.TEISEI_AMOUNT + "='" + m_TEISEI_AMOUNT + "', " +
                TBL_ICREQRET_BILLMEITXT.PAY_AMOUNT + "='" + m_PAY_AMOUNT + "', " +
                TBL_ICREQRET_BILLMEITXT.PAYAFT_REV_AMOUNT + "='" + m_PAYAFT_REV_AMOUNT + "', " +
                TBL_ICREQRET_BILLMEITXT.OCR_AMOUNT_CONF + "='" + m_OCR_AMOUNT_CONF + "', " +
                TBL_ICREQRET_BILLMEITXT.OC_CLEARING_DATE + "='" + m_OC_CLEARING_DATE + "', " +
                TBL_ICREQRET_BILLMEITXT.TEISEI_CLEARING_DATE + "='" + m_TEISEI_CLEARING_DATE + "', " +
                TBL_ICREQRET_BILLMEITXT.CLEARING_DATE + "='" + m_CLEARING_DATE + "', " +
                TBL_ICREQRET_BILLMEITXT.QR_IC_BR_NO + "='" + m_QR_IC_BR_NO + "', " +
                TBL_ICREQRET_BILLMEITXT.KAMOKU + "='" + m_KAMOKU + "', " +
                TBL_ICREQRET_BILLMEITXT.ACCOUNT + "='" + m_ACCOUNT + "', " +
                TBL_ICREQRET_BILLMEITXT.BK_CTL_NO + "='" + m_BK_CTL_NO + "', " +
                TBL_ICREQRET_BILLMEITXT.FREEFIELD + "='" + m_FREEFIELD + "', " +
                TBL_ICREQRET_BILLMEITXT.BILL_CODE + "='" + m_BILL_CODE + "', " +
                TBL_ICREQRET_BILLMEITXT.BILL_CODE_CONF + "='" + m_BILL_CODE_CONF + "', " +
                TBL_ICREQRET_BILLMEITXT.QR + "='" + m_QR + "', " +
                TBL_ICREQRET_BILLMEITXT.MICR + "='" + m_MICR + "', " +
                TBL_ICREQRET_BILLMEITXT.MICR_CONF + "='" + m_MICR_CONF + "', " +
                TBL_ICREQRET_BILLMEITXT.BILL_NO + "='" + m_BILL_NO + "', " +
                TBL_ICREQRET_BILLMEITXT.BILL_NO_CONF + "='" + m_BILL_NO_CONF + "', " +
                TBL_ICREQRET_BILLMEITXT.FUBI_KBN_01 + "='" + m_FUBI_KBN_01 + "', " +
                TBL_ICREQRET_BILLMEITXT.ZERO_FUBINO_01 + "=" + m_ZERO_FUBINO_01 + ", " +
                TBL_ICREQRET_BILLMEITXT.FUBI_KBN_02 + "='" + m_FUBI_KBN_02 + "', " +
                TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_02 + "=" + m_ZRO_FUBINO_02 + ", " +
                TBL_ICREQRET_BILLMEITXT.FUBI_KBN_03 + "='" + m_FUBI_KBN_03 + "', " +
                TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_03 + "=" + m_ZRO_FUBINO_03 + ", " +
                TBL_ICREQRET_BILLMEITXT.FUBI_KBN_04 + "='" + m_FUBI_KBN_04 + "', " +
                TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_04 + "=" + m_ZRO_FUBINO_04 + ", " +
                TBL_ICREQRET_BILLMEITXT.FUBI_KBN_05 + "='" + m_FUBI_KBN_05 + "', " +
                TBL_ICREQRET_BILLMEITXT.ZRO_FUBINO_05 + "=" + m_ZRO_FUBINO_05 + ", " +
                TBL_ICREQRET_BILLMEITXT.IC_FLG + "='" + m_IC_FLG + "', " +
                TBL_ICREQRET_BILLMEITXT.KAKUTEI_FLG + "=" + m_KAKUTEI_FLG + ", " +
                TBL_ICREQRET_BILLMEITXT.KAKUTEI_DATE + "=" + m_KAKUTEI_DATE + ", " +
                TBL_ICREQRET_BILLMEITXT.KAKUTEI_TIME + "=" + m_KAKUTEI_TIME + ", " +
                TBL_ICREQRET_BILLMEITXT.KAKUTEI_OPE + "='" + m_KAKUTEI_OPE + "' " +
                " WHERE " +
                TBL_ICREQRET_BILLMEITXT.MEI_TXT_NAME + "='" + m_MEI_TXT_NAME + "' AND " +
                TBL_ICREQRET_BILLMEITXT.CAP_KBN + "=" + m_CAP_KBN + " AND " +
                TBL_ICREQRET_BILLMEITXT.IMG_NAME + "='" + m_IMG_NAME + "' ";
            return strSQL;
        }

        /// <summary>
        /// キー項目を条件とするDELETE文を作成します
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string strSQL = "DELETE FROM " + TBL_ICREQRET_BILLMEITXT.TABLE_NAME(m_SCHEMABANKCD) +
                " WHERE " +
                TBL_ICREQRET_BILLMEITXT.MEI_TXT_NAME + "='" + m_MEI_TXT_NAME + "' AND " +
                TBL_ICREQRET_BILLMEITXT.CAP_KBN + "=" + m_CAP_KBN + " AND " +
                TBL_ICREQRET_BILLMEITXT.IMG_NAME + "='" + m_IMG_NAME + "' ";
            return strSQL;
        }

        #endregion
    }
}
