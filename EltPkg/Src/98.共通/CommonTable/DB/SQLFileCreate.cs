using System.Collections.Generic;

namespace CommonTable.DB
{
    /// <summary>
    /// ファイル作成
    /// 　E0301　HULFT管理
    /// 　E0303　持帰証券データ訂正テキスト作成
    /// 　E0305　持出取消テキスト作成
    /// 　E0306　持出証券イメージアーカイブ作成
    /// 　E0307　不渡返還テキスト作成
    /// </summary>
    public class SQLFileCreate
    {
        /// <summary>検索モード</summary>
        public enum SearchType
        {
            Type0,
            Type1,
            Type2,
        }

        // *******************************************************************
        // 共通
        // *******************************************************************

        #region * 共通
        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 呼出画面：E0306　持出証券イメージアーカイブ作成
        /// 処理内容：一時テーブル作成クエリ（TRMEIIMG）
        /// </summary>
        /// <returns></returns>
        public static string GetCreateTRMEIIMG(string tableName)
        {
            string strSQL = "";
            strSQL += " CREATE GLOBAL TEMPORARY TABLE " + tableName + " ( ";
            strSQL += "     GYM_ID number(3,0) NOT NULL, ";
            strSQL += "     OPERATION_DATE number(8,0) NOT NULL, ";
            strSQL += "     SCAN_TERM varchar2(20) NOT NULL, ";
            strSQL += "     BAT_ID number(6,0) NOT NULL, ";
            strSQL += "     DETAILS_NO number(6,0) NOT NULL, ";
            strSQL += "     IMG_KBN number(2,0) NOT NULL, ";
            strSQL += "     IMG_FLNM varchar2(62), ";
            strSQL += "     IMG_FLNM_OLD varchar2(100), ";
            strSQL += "     OC_OC_BK_NO varchar2(4), ";
            strSQL += "     OC_OC_BR_NO varchar2(4), ";
            strSQL += "     OC_IC_BK_NO varchar2(4), ";
            strSQL += "     OC_OC_DATE varchar2(8), ";
            strSQL += "     OC_CLEARING_DATE varchar2(8), ";
            strSQL += "     OC_AMOUNT varchar2(12), ";
            strSQL += "     PAY_KBN varchar2(1), ";
            strSQL += "     UNIQUE_CODE varchar2(15), ";
            strSQL += "     FILE_EXTENSION varchar2(4), ";
            strSQL += "     BUA_STS number(2,0) default 0  NOT NULL, ";
            strSQL += "     BUB_CONFIRMDATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     BUA_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     BUA_TIME number(6,0) default 0  NOT NULL, ";
            strSQL += "     GDA_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     GDA_TIME number(6,0) default 0  NOT NULL, ";
            strSQL += "     IMG_ARCH_NAME varchar2(32), ";
            strSQL += "     DELETE_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     DELETE_FLG number(1) default 0  NOT NULL, ";
            strSQL += " PRIMARY KEY ( ";
            strSQL += "      GYM_ID ";
            strSQL += "     ,OPERATION_DATE ";
            strSQL += "     ,SCAN_TERM ";
            strSQL += "     ,BAT_ID ";
            strSQL += "     ,DETAILS_NO ";
            strSQL += "     ,IMG_KBN ";
            strSQL += " )) ";
            strSQL += " ON COMMIT DELETE ROWS ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 呼出画面：E0305　持出取消テキスト作成
        /// 呼出画面：E0306　持出証券イメージアーカイブ作成
        /// 呼出画面：E0307　不渡返還テキスト作成
        /// 処理内容：一時テーブル作成クエリ（TRMEI）
        /// </summary>
        /// <returns></returns>
        public static string GetCreateTRMEI(string tableName)
        {
            string strSQL = "";
            strSQL += " CREATE GLOBAL TEMPORARY TABLE " + tableName + " ( ";
            strSQL += "     GYM_ID number(3,0) NOT NULL, ";
            strSQL += "     OPERATION_DATE number(8,0) NOT NULL, ";
            strSQL += "     SCAN_TERM varchar2(20) NOT NULL, ";
            strSQL += "     BAT_ID number(6,0) NOT NULL, ";
            strSQL += "     DETAILS_NO number(6,0) NOT NULL, ";
            strSQL += "     DSP_ID number(3,0) default 999  NOT NULL, ";
            strSQL += "     IC_OC_BK_NO number(4,0) default -1  NOT NULL, ";
            strSQL += "     IC_OLD_OC_BK_NO number(4,0) default -1  NOT NULL, ";
            strSQL += "     BUA_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     BUB_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     BCA_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     GMA_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     GMB_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     GRA_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     GXA_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     GXB_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     MRA_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     MRB_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     MRC_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     MRD_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     YCA_MARK number(2,0) default 0  NOT NULL, ";
            strSQL += "     EDIT_FLG number(1,0) default 0  NOT NULL, ";
            strSQL += "     BCA_STS number(2,0) default 0  NOT NULL, ";
            strSQL += "     GMA_STS number(2,0) default 0  NOT NULL, ";
            strSQL += "     GRA_STS number(2,0) default 0  NOT NULL, ";
            strSQL += "     GRA_CONFIRMDATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     MEMO varchar2(256), ";
            strSQL += "     DELETE_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     DELETE_FLG number(1,0) default 0  NOT NULL, ";
            strSQL += " PRIMARY KEY ( ";
            strSQL += "      GYM_ID ";
            strSQL += "     ,OPERATION_DATE ";
            strSQL += "     ,SCAN_TERM ";
            strSQL += "     ,BAT_ID ";
            strSQL += "     ,DETAILS_NO ";
            strSQL += " )) ";
            strSQL += " ON COMMIT DELETE ROWS ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0307　不渡返還テキスト作成
        /// 処理内容：一時テーブル作成クエリ（TRFUWATARI）
        /// </summary>
        /// <returns></returns>
        public static string GetCreateTRFUWATARI(string tableName)
        {
            string strSQL = "";
            strSQL += " CREATE GLOBAL TEMPORARY TABLE " + tableName + " ( ";
            strSQL += "     GYM_ID number(3,0) NOT NULL, ";
            strSQL += "     OPERATION_DATE number(8,0) NOT NULL, ";
            strSQL += "     SCAN_TERM varchar2(20) NOT NULL, ";
            strSQL += "     BAT_ID number(6,0) NOT NULL, ";
            strSQL += "     DETAILS_NO number(6,0) NOT NULL, ";
            strSQL += "     FUBI_KBN_01 number(1,0) default -1  NOT NULL, ";
            strSQL += "     ZERO_FUBINO_01 number(2,0) default -1  NOT NULL, ";
            strSQL += "     FUBI_KBN_02 number(1,0) default -1  NOT NULL, ";
            strSQL += "     ZERO_FUBINO_02 number(2,0) default -1  NOT NULL, ";
            strSQL += "     FUBI_KBN_03 number(1,0) default -1  NOT NULL, ";
            strSQL += "     ZERO_FUBINO_03 number(2,0) default -1  NOT NULL, ";
            strSQL += "     FUBI_KBN_04 number(1,0) default -1  NOT NULL, ";
            strSQL += "     ZERO_FUBINO_04 number(2,0) default -1  NOT NULL, ";
            strSQL += "     FUBI_KBN_05 number(1,0) default -1  NOT NULL, ";
            strSQL += "     ZERO_FUBINO_05 number(2,0) default -1  NOT NULL, ";
            strSQL += "     DELETE_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     DELETE_FLG number(1) default 0  NOT NULL, ";
            strSQL += "     E_TERM varchar2(20), ";
            strSQL += "     E_OPENO varchar2(20), ";
            strSQL += "     E_YMD number(8,0) default 0  NOT NULL, ";
            strSQL += "     E_TIME number(11,0) default 0  NOT NULL, ";
            strSQL += " PRIMARY KEY ( ";
            strSQL += "      GYM_ID ";
            strSQL += "     ,OPERATION_DATE ";
            strSQL += "     ,SCAN_TERM ";
            strSQL += "     ,BAT_ID ";
            strSQL += "     ,DETAILS_NO ";
            strSQL += " )) ";
            strSQL += " ON COMMIT DELETE ROWS ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 処理内容：一時テーブル作成クエリ（TRITEM）
        /// </summary>
        /// <returns></returns>
        public static string GetCreateTRITEM(string tableName)
        {
            string strSQL = "";
            strSQL += " CREATE GLOBAL TEMPORARY TABLE " + tableName + " ( ";
            strSQL += "     GYM_ID number(3,0) NOT NULL, ";
            strSQL += "     OPERATION_DATE number(8,0) NOT NULL, ";
            strSQL += "     SCAN_TERM varchar2(20) NOT NULL, ";
            strSQL += "     BAT_ID number(6,0) NOT NULL, ";
            strSQL += "     DETAILS_NO number(6,0) NOT NULL, ";
            strSQL += "     ITEM_ID number(3,0) NOT NULL, ";
            strSQL += "     ITEM_NAME varchar2(40), ";
            strSQL += "     OCR_ENT_DATA varchar2(100), ";
            strSQL += "     OCR_VFY_DATA varchar2(100), ";
            strSQL += "     ENT_DATA varchar2(100), ";
            strSQL += "     VFY_DATA varchar2(100), ";
            strSQL += "     END_DATA varchar2(100), ";
            strSQL += "     BUA_DATA varchar2(100), ";
            strSQL += "     CTR_DATA varchar2(100), ";
            strSQL += "     ICTEISEI_DATA varchar2(100), ";
            strSQL += "     MRC_CHG_BEFDATA varchar2(100), ";
            strSQL += "     E_TERM varchar2(20), ";
            strSQL += "     E_OPENO varchar2(20), ";
            strSQL += "     E_STIME number(9,0) default 0  NOT NULL, ";
            strSQL += "     E_ETIME number(9,0) default 0  NOT NULL, ";
            strSQL += "     E_YMD number(8,0) default 0  NOT NULL, ";
            strSQL += "     E_TIME number(9,0) default 0  NOT NULL, ";
            strSQL += "     V_TERM varchar2(20), ";
            strSQL += "     V_OPENO varchar2(20), ";
            strSQL += "     V_STIME number(9,0) default 0  NOT NULL, ";
            strSQL += "     V_ETIME number(9,0) default 0  NOT NULL, ";
            strSQL += "     V_YMD number(8,0) default 0  NOT NULL, ";
            strSQL += "     V_TIME number(9,0) default 0  NOT NULL, ";
            strSQL += "     C_TERM varchar2(20), ";
            strSQL += "     C_OPENO varchar2(20), ";
            strSQL += "     C_STIME number(9,0) default 0  NOT NULL, ";
            strSQL += "     C_ETIME number(9,0) default 0  NOT NULL, ";
            strSQL += "     C_YMD number(8,0) default 0  NOT NULL, ";
            strSQL += "     C_TIME number(9,0) default 0  NOT NULL, ";
            strSQL += "     O_TERM varchar2(20), ";
            strSQL += "     O_OPENO varchar2(20), ";
            strSQL += "     O_STIME number(9,0) default 0  NOT NULL, ";
            strSQL += "     O_ETIME number(9,0) default 0  NOT NULL, ";
            strSQL += "     O_YMD number(8,0) default 0  NOT NULL, ";
            strSQL += "     O_TIME number(9,0) default 0  NOT NULL, ";
            strSQL += "     ITEM_TOP number(10,1) default -1  NOT NULL, ";
            strSQL += "     ITEM_LEFT number(10,1) default -1  NOT NULL, ";
            strSQL += "     ITEM_WIDTH number(10,1) default -1  NOT NULL, ";
            strSQL += "     ITEM_HEIGHT number(10,1) default -1  NOT NULL, ";
            strSQL += "     FIX_TRIGGER varchar2(20), ";
            strSQL += " PRIMARY KEY ( ";
            strSQL += "      GYM_ID ";
            strSQL += "     ,OPERATION_DATE ";
            strSQL += "     ,SCAN_TERM ";
            strSQL += "     ,BAT_ID ";
            strSQL += "     ,DETAILS_NO ";
            strSQL += "     ,ITEM_ID ";
            strSQL += " )) ";
            strSQL += " ON COMMIT DELETE ROWS ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 呼出画面：E0305　持出取消テキスト作成
        /// 呼出画面：E0306　持出証券イメージアーカイブ作成
        /// 呼出画面：E0307　不渡返還テキスト作成
        /// 処理内容：SELECT句のレコードを一時テーブルに登録する
        /// </summary>
        /// <param name="srcSELECT"></param>
        /// <param name="dstTableName"></param>
        /// <param name="allColumns"></param>
        /// <returns></returns>
        public static string GetInsertTmpTable(string srcSELECT, string dstTableName, string allColumns)
        {
            string strSQL = "";
            strSQL += " INSERT INTO " + dstTableName + " ";
            strSQL += "     ( " + allColumns + " ) ";
            strSQL += srcSELECT;
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 呼出画面：E0305　持出取消テキスト作成
        /// 呼出画面：E0307　不渡返還テキスト作成
        /// 取得内容：一時テーブルのキーに合致する実テーブルのデータを取得する
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetTmpMeiListSelect(string tmpTableName, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " SELECT ";
            strSQL += "     MEI.* ";
            strSQL += " FROM ";
            strSQL += "     " + tmpTableName + " TMP ";
            strSQL += "     INNER JOIN " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSQL += "         ON  MEI.GYM_ID = TMP.GYM_ID ";
            strSQL += "         AND MEI.OPERATION_DATE = TMP.OPERATION_DATE ";
            strSQL += "         AND MEI.SCAN_TERM = TMP.SCAN_TERM ";
            strSQL += "         AND MEI.BAT_ID = TMP.BAT_ID ";
            strSQL += "         AND MEI.DETAILS_NO = TMP.DETAILS_NO ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0307　不渡返還テキスト作成
        /// 取得内容：一時テーブルのキーに合致する実テーブルのデータを取得する
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetTmpFuwaListSelect(string tmpTableName, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " SELECT ";
            strSQL += "     FUWA.* ";
            strSQL += " FROM ";
            strSQL += "     " + tmpTableName + " TMP ";
            strSQL += "     INNER JOIN " + TBL_TRFUWATARI.TABLE_NAME(SchemaBankCD) + " FUWA ";
            strSQL += "         ON  FUWA.GYM_ID = TMP.GYM_ID ";
            strSQL += "         AND FUWA.OPERATION_DATE = TMP.OPERATION_DATE ";
            strSQL += "         AND FUWA.SCAN_TERM = TMP.SCAN_TERM ";
            strSQL += "         AND FUWA.BAT_ID = TMP.BAT_ID ";
            strSQL += "         AND FUWA.DETAILS_NO = TMP.DETAILS_NO ";
            return strSQL;
        }

        /// <summary>
        /// 他行データ判定クエリ（持出データ用）
        /// </summary>
        /// <param name="bankCd">自行番号</param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        private static string GetOtherBankQueryOC(int bankCd, int SchemaBankCD)
        {
            string strInternalExchange = "";
            strInternalExchange += "     AND EXISTS ( ";
            strInternalExchange += "       SELECT 1 FROM " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " WT ";
            strInternalExchange += "       WHERE ";
            strInternalExchange += "           WT.GYM_ID = MEI.GYM_ID ";
            strInternalExchange += "           AND WT.OPERATION_DATE = MEI.OPERATION_DATE ";
            strInternalExchange += "           AND WT.SCAN_TERM = MEI.SCAN_TERM ";
            strInternalExchange += "           AND WT.BAT_ID = MEI.BAT_ID ";
            strInternalExchange += "           AND WT.DETAILS_NO = MEI.DETAILS_NO ";
            strInternalExchange += "           AND WT.ITEM_ID = " + DspItem.ItemId.持帰銀行コード + " ";
            strInternalExchange += "           AND ( WT.END_DATA <> '" + bankCd.ToString("D4") + "' ";
            strInternalExchange += "              OR WT.END_DATA IS NULL ) ";
            strInternalExchange += "     ) ";
            return strInternalExchange;
        }

        /// <summary>
        /// 他行データ判定クエリ（持帰データ用）
        /// </summary>
        /// <param name="bankCd">自行番号</param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        private static string GetOtherBankQueryIC(int bankCd, int SchemaBankCD)
        {
            return "     AND MEI.IC_OC_BK_NO <> " + bankCd; ;
        }

        /// <summary>
        /// 自行データ判定クエリ（持出データ用）
        /// </summary>
        /// <param name="bankCd">自行番号</param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        private static string GetOwnBankQueryOC(int bankCd, int SchemaBankCD)
        {
            string strInternalExchange = "";
            strInternalExchange += "     AND EXISTS ( ";
            strInternalExchange += "       SELECT 1 FROM " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " WT ";
            strInternalExchange += "       WHERE ";
            strInternalExchange += "           WT.GYM_ID = MEI.GYM_ID ";
            strInternalExchange += "           AND WT.OPERATION_DATE = MEI.OPERATION_DATE ";
            strInternalExchange += "           AND WT.SCAN_TERM = MEI.SCAN_TERM ";
            strInternalExchange += "           AND WT.BAT_ID = MEI.BAT_ID ";
            strInternalExchange += "           AND WT.DETAILS_NO = MEI.DETAILS_NO ";
            strInternalExchange += "           AND WT.ITEM_ID = " + DspItem.ItemId.持帰銀行コード + " ";
            strInternalExchange += "           AND NVL(WT.END_DATA,' ') = '" + bankCd.ToString("D4") + "' ";
            strInternalExchange += "     ) ";
            return strInternalExchange;
        }

        /// <summary>
        /// 自行データ判定クエリ（持帰データ用）
        /// </summary>
        /// <param name="bankCd">自行番号</param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        private static string GetOwnBankQueryIC(int bankCd, int SchemaBankCD)
        {
            return "     AND MEI.IC_OC_BK_NO = " + bankCd; ;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 呼出画面：E0305　持出取消テキスト作成
        /// 呼出画面：E0307　不渡返還テキスト作成
        /// 処理内容：配信ファイル明細内訳に登録
        /// </summary>
        /// <returns></returns>
        /// <remarks>2022/03/28 銀行導入工程_不具合管理表No97 対応</remarks>
        public static string GetSendFileTRMeiInsert(string sendfilename, string tmpTableName, int SchemaBankCD)
        {
            // 一時テーブルのデータを配信ファイル明細内訳テーブルに登録する
            string strSQL = "";
            strSQL += " INSERT INTO " + TBL_SEND_FILE_TRMEI.TABLE_NAME(SchemaBankCD) + " ";
            strSQL += " (SEND_FILE_NAME,GYM_ID,OPERATION_DATE,SCAN_TERM,BAT_ID,DETAILS_NO) ";
            strSQL += " SELECT ";
            strSQL += "    '" + sendfilename + "' ";
            strSQL += "  , GYM_ID ";
            strSQL += "  , OPERATION_DATE ";
            strSQL += "  , SCAN_TERM ";
            strSQL += "  , BAT_ID ";
            strSQL += "  , DETAILS_NO ";
            strSQL += " FROM ";
            strSQL += "     " + tmpTableName + " ";
            return strSQL;
        }

        #endregion

        // *******************************************************************
        // E0301　HULFT管理
        // *******************************************************************

        #region * E0301　HULFT管理
        /// <summary>
        /// 呼出画面：E0301　HULFT管理
        /// 取得内容：ファイル集配信管理
        /// </summary>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetHulftMgrFileCtlSelect(int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " SELECT * FROM " + TBL_FILE_CTL.TABLE_NAME(SchemaBankCD) + " ";
            strSQL += " WHERE ";
            strSQL += "     SEND_STS IN ( ";
            strSQL += "        " + FileCtl.SendSts.ファイル作成 + ", ";
            strSQL += "        " + FileCtl.SendSts.配信エラー + " ";
            strSQL += "     ) ";
            strSQL += " ORDER BY ";
            strSQL += "     MAKE_DATE, MAKE_TIME ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0301　HULFT管理
        /// 更新内容：明細イメージ
        /// イメージアーカイブファイル名からTRIMEIIMGのステータス項目を更新するUPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetTRMeiImgArchNameUpdate(int GymID, string ImgArchName, Dictionary<string, int> UpdateData, int Schemabankcd)
        {
            string SET = string.Empty;
            foreach (string Key in UpdateData.Keys)
            {
                if (!string.IsNullOrEmpty(SET)) SET += ",";
                SET += "  TRMEIIMG." + Key + "=" + UpdateData[Key] + " ";
            }
            string strSql = "UPDATE " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                " SET " + SET +
                " WHERE " +
                "     TRMEIIMG.GYM_ID = " + GymID + " " +
                " AND TRMEIIMG.IMG_ARCH_NAME = '" + ImgArchName + "' ";
            return strSql;
        }

        /// <summary>
        /// 呼出画面：E0301　HULFT管理
        /// 更新内容：明細
        /// 配信ファイル明細内訳からTRMEIの送信ステータス項目を更新するUPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        /// <remarks>2022/03/28 銀行導入工程_不具合管理表No97 対応</remarks>
        public static string GetTRMeiSendStsUpdate(string SendFileName, Dictionary<string, int> UpdateData, int Schemabankcd)
        {
            string SET = string.Empty;
            foreach (string Key in UpdateData.Keys)
            {
                if (!string.IsNullOrEmpty(SET)) SET += ",";
                SET += "  TRMEI." + Key + "=" + UpdateData[Key] + " ";
            }
            string strSql = "UPDATE " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                " SET " + SET +
                " WHERE " +
                "     EXISTS ( " +
                "        SELECT 1 " +
                "        FROM " + TBL_SEND_FILE_TRMEI.TABLE_NAME(Schemabankcd) + " SEND_FILE_TRMEI " +
                "        WHERE " +
                "             SEND_FILE_TRMEI.SEND_FILE_NAME = '" + SendFileName + "' " +
                "         AND SEND_FILE_TRMEI.GYM_ID = TRMEI.GYM_ID " +
                "         AND SEND_FILE_TRMEI.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "         AND SEND_FILE_TRMEI.SCAN_TERM = TRMEI.SCAN_TERM " +
                "         AND SEND_FILE_TRMEI.BAT_ID = TRMEI.BAT_ID " +
                "         AND SEND_FILE_TRMEI.DETAILS_NO = TRMEI.DETAILS_NO " +
                "     ) ";
            return strSql;
        }

        #endregion

        // *******************************************************************
        // E0303　持帰証券データ訂正テキスト作成
        // *******************************************************************

        #region * E0303　持帰証券データ訂正テキスト作成
        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 取得内容：作成対象データ（明細：持帰）
        /// </summary>
        /// <param name="type1">Type1：訂正データ、Type2：取消データ</param>
        /// <param name="type2">Type0：全データ、Type1：他行データ、Type2：自行データ</param>
        /// <param name="bankCd">自行番号</param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetTeiseiTextICMeiListSelect(SearchType type1, SearchType type2, int bankCd, int SchemaBankCD, bool Orderby)
        {
            // データ種別
            string strDataType = "";
            if (type1 == SearchType.Type1)
            {
                // 訂正データ
                strDataType += "     AND MEI.GMA_STS IN ( ";
                strDataType += "       " + TrMei.Sts.未作成 + ", ";
                strDataType += "       " + TrMei.Sts.再作成対象 + ", ";
                strDataType += "       " + TrMei.Sts.結果正常 + " ";
                strDataType += "     ) ";
                strDataType += "     AND EXISTS ( ";
                strDataType += "         SELECT 1 FROM " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " WT ";
                strDataType += "         WHERE ";
                strDataType += "             WT.GYM_ID = ITEM.GYM_ID ";
                strDataType += "             AND WT.OPERATION_DATE = ITEM.OPERATION_DATE ";
                strDataType += "             AND WT.SCAN_TERM = ITEM.SCAN_TERM ";
                strDataType += "             AND WT.BAT_ID = ITEM.BAT_ID ";
                strDataType += "             AND WT.DETAILS_NO = ITEM.DETAILS_NO ";
                strDataType += "             AND WT.ITEM_ID = ITEM.ITEM_ID ";
                strDataType += "             AND NVL(WT.CTR_DATA,' ') <> NVL(ITEM.END_DATA,' ') ";
                strDataType += "             AND NVL(WT.ICTEISEI_DATA,' ') <> NVL(ITEM.END_DATA,' ') ";
                strDataType += "     ) ";
            }
            else if (type1 == SearchType.Type2)
            {
                // 取消データ
                // 取消の場合はICTEISEI_DATAがNULL以外[結果取込で訂正送信済以外のデータは対象外]
                strDataType += "     AND MEI.GMA_STS IN ( ";
                strDataType += "       " + TrMei.Sts.再作成対象 + ", ";
                strDataType += "       " + TrMei.Sts.結果正常 + " ";
                strDataType += "     ) ";
                strDataType += "     AND EXISTS ( ";
                strDataType += "         SELECT 1 FROM " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " WT ";
                strDataType += "         WHERE ";
                strDataType += "             WT.GYM_ID = MEI.GYM_ID ";
                strDataType += "             AND WT.OPERATION_DATE = MEI.OPERATION_DATE ";
                strDataType += "             AND WT.SCAN_TERM = MEI.SCAN_TERM ";
                strDataType += "             AND WT.BAT_ID = MEI.BAT_ID ";
                strDataType += "             AND WT.DETAILS_NO = MEI.DETAILS_NO ";
                strDataType += "             AND WT.ITEM_ID = ITEM.ITEM_ID ";
                strDataType += "             AND NVL(WT.CTR_DATA,' ') = NVL(ITEM.END_DATA,' ') ";
                strDataType += "             AND NVL(WT.ICTEISEI_DATA,' ') <> NVL(ITEM.END_DATA,' ') ";
                strDataType += "             AND WT.ICTEISEI_DATA IS NOT NULL ";
                strDataType += "     ) ";
            }

            // 行内交換する場合は自行を対象外にする
            string strInternalExchange = "";
            if (type2 == SearchType.Type1)
            {
                // 他行データ
                strInternalExchange = GetOtherBankQueryIC(bankCd, SchemaBankCD);
            }
            else if (type2 == SearchType.Type2)
            {
                // 自行データ
                strInternalExchange = GetOwnBankQueryIC(bankCd, SchemaBankCD);
            }

            string strSQL = "";
            strSQL += " SELECT DISTINCT {0} ";
            strSQL += " FROM ";
            strSQL += "     " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSQL += "     INNER JOIN " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " IMG ";
            strSQL += "         ON  IMG.GYM_ID = MEI.GYM_ID ";
            strSQL += "         AND IMG.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "         AND IMG.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "         AND IMG.BAT_ID = MEI.BAT_ID ";
            strSQL += "         AND IMG.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "         AND IMG.IMG_KBN = " + TrMeiImg.ImgKbn.表 + " ";
            strSQL += "     INNER JOIN " + TBL_DSP_ITEM.TABLE_NAME(SchemaBankCD) + " DI ";
            strSQL += "         ON  MEI.GYM_ID = DI.GYM_ID ";
            strSQL += "         AND MEI.DSP_ID = DI.DSP_ID ";
            strSQL += "     INNER JOIN " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " ITEM ";
            strSQL += "         ON  ITEM.GYM_ID = MEI.GYM_ID ";
            strSQL += "         AND ITEM.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "         AND ITEM.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "         AND ITEM.BAT_ID = MEI.BAT_ID ";
            strSQL += "         AND ITEM.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "         AND ITEM.ITEM_ID = DI.ITEM_ID ";
            strSQL += " WHERE ";
            strSQL += "         MEI.GYM_ID = " + GymParam.GymId.持帰 + " ";
            strSQL += "     AND MEI.DELETE_FLG = 0 ";
            strSQL += "     AND ITEM.ITEM_ID IN ( ";
            strSQL += "             " + DspItem.ItemId.持帰銀行コード + ", ";
            strSQL += "             " + DspItem.ItemId.交換日 + ", ";
            strSQL += "             " + DspItem.ItemId.金額 + " ";
            strSQL += "         ) ";
            // 補正完了データが対象
            strSQL += "     AND EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS1 ";
            strSQL += "         WHERE ";
            strSQL += "             STS1.GYM_ID = MEI.GYM_ID ";
            strSQL += "             AND STS1.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "             AND STS1.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "             AND STS1.BAT_ID = MEI.BAT_ID ";
            strSQL += "             AND STS1.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "             AND STS1.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.持帰銀行 + " ";
            strSQL += "             AND STS1.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ";
            strSQL += "     ) ";
            strSQL += "     AND EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS2 ";
            strSQL += "         WHERE ";
            strSQL += "             STS2.GYM_ID = MEI.GYM_ID ";
            strSQL += "             AND STS2.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "             AND STS2.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "             AND STS2.BAT_ID = MEI.BAT_ID ";
            strSQL += "             AND STS2.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "             AND STS2.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.交換希望日 + " ";
            strSQL += "             AND STS2.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ";
            strSQL += "     ) ";
            strSQL += "     AND EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS3 ";
            strSQL += "         WHERE ";
            strSQL += "             STS3.GYM_ID = MEI.GYM_ID ";
            strSQL += "             AND STS3.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "             AND STS3.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "             AND STS3.BAT_ID = MEI.BAT_ID ";
            strSQL += "             AND STS3.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "             AND STS3.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.金額 + " ";
            strSQL += "             AND STS3.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ";
            strSQL += "     ) ";
            strSQL += "     AND EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS5 ";
            strSQL += "         WHERE ";
            strSQL += "             STS5.GYM_ID = MEI.GYM_ID ";
            strSQL += "             AND STS5.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "             AND STS5.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "             AND STS5.BAT_ID = MEI.BAT_ID ";
            strSQL += "             AND STS5.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "             AND STS5.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.交換尻 + " ";
            strSQL += "             AND STS5.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ";
            strSQL += "     ) ";

            strSQL += strDataType;
            strSQL += strInternalExchange;
            if (Orderby)
            {
                strSQL += " ORDER BY ";
                strSQL += "     1,2,3,4,5 ";
            }
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 取得内容：行ロック時の条件追加
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static string GetTeiseiTextICMeiListSelectExists(string tmpTableName, string strSQL)
        {
            string strSQLAdd = string.Empty;
            strSQLAdd += "   AND NOT EXISTS ( ";
            strSQLAdd += "         SELECT 1 FROM " + tmpTableName + " WKTBL ";
            strSQLAdd += "         WHERE ";
            strSQLAdd += "               WKTBL.GYM_ID = MEI.GYM_ID ";
            strSQLAdd += "           AND WKTBL.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQLAdd += "           AND WKTBL.SCAN_TERM = MEI.SCAN_TERM ";
            strSQLAdd += "           AND WKTBL.BAT_ID = MEI.BAT_ID ";
            strSQLAdd += "           AND WKTBL.DETAILS_NO = MEI.DETAILS_NO ";
            strSQLAdd += "   ) ";
            strSQL += strSQLAdd;

            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 取得内容：作成対象データ（明細：持出）
        /// </summary>
        /// <param name="fileNameList"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetTeiseiTextOCMeiListSelect(List<string> fileNameList, int SchemaBankCD)
        {
            string strSQLFileList = "";
            string strSQLSub = "";
            foreach (string fileName in fileNameList)
            {
                strSQLFileList += string.Format(" {0} OCIMG.IMG_FLNM ='{1}' ", strSQLSub, fileName);
                strSQLSub = "OR";
            }

            string strSQL = "";
            strSQL += " SELECT DISTINCT {0} ";
            strSQL += " FROM ";
            strSQL += "     " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " OCIMG ";
            strSQL += "     INNER JOIN " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " OCMEI ";
            strSQL += "         ON  OCIMG.GYM_ID = OCMEI.GYM_ID ";
            strSQL += "         AND OCIMG.OPERATION_DATE = OCMEI.OPERATION_DATE ";
            strSQL += "         AND OCIMG.SCAN_TERM = OCMEI.SCAN_TERM ";
            strSQL += "         AND OCIMG.BAT_ID = OCMEI.BAT_ID ";
            strSQL += "         AND OCIMG.DETAILS_NO = OCMEI.DETAILS_NO ";
            strSQL += "     INNER JOIN " + TBL_DSP_ITEM.TABLE_NAME(SchemaBankCD) + " DI ";
            strSQL += "         ON  DI.GYM_ID = OCMEI.GYM_ID ";
            strSQL += "         AND DI.DSP_ID = OCMEI.DSP_ID ";
            strSQL += "     INNER JOIN " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " OCITEM ";
            strSQL += "         ON  OCITEM.GYM_ID = OCMEI.GYM_ID ";
            strSQL += "         AND OCITEM.OPERATION_DATE = OCMEI.OPERATION_DATE ";
            strSQL += "         AND OCITEM.SCAN_TERM = OCMEI.SCAN_TERM ";
            strSQL += "         AND OCITEM.BAT_ID = OCMEI.BAT_ID ";
            strSQL += "         AND OCITEM.DETAILS_NO = OCMEI.DETAILS_NO ";
            strSQL += "         AND OCITEM.ITEM_ID = DI.ITEM_ID ";
            strSQL += " WHERE ";
            strSQL += "         OCIMG.GYM_ID = " + GymParam.GymId.持出 + " ";
            strSQL += "     AND OCIMG.IMG_KBN = " + TrMeiImg.ImgKbn.表 + " ";
            strSQL += "     AND (" + strSQLFileList + ") ";
            strSQL += "     AND OCMEI.DELETE_FLG = 0 ";
            strSQL += " ORDER BY ";
            strSQL += "     1,2,3,4,5 ";

            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 登録内容：作成対象データ（明細トランザクション）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="mei"></param>
        /// <returns></returns>
        public static string GetTeiseiTextTrMeiInsertAll(string tmpTableName, TBL_TRMEI mei)
        {
            // マルチテーブルインサート用のクエリ
            string strSQL = "";
            strSQL += " INTO " + tmpTableName + " ";
            strSQL += "     ( ";
            // キー
            strSQL += "          GYM_ID, ";
            strSQL += "          OPERATION_DATE, ";
            strSQL += "          SCAN_TERM, ";
            strSQL += "          BAT_ID, ";
            strSQL += "          DETAILS_NO, ";
            // データ
            strSQL += "          GMA_DATE, ";
            strSQL += "          GMA_STS, ";
            strSQL += "          DELETE_DATE, ";
            strSQL += "          DELETE_FLG ";
            strSQL += "     ) VALUES ( ";
            // キー
            strSQL += "           " + mei._GYM_ID + ", ";
            strSQL += "           " + mei._OPERATION_DATE + ", ";
            strSQL += "          '" + mei._SCAN_TERM + "', ";
            strSQL += "           " + mei._BAT_ID + ", ";
            strSQL += "           " + mei._DETAILS_NO + ", ";
            // データ
            strSQL += "           " + mei.m_GMA_DATE + ", ";
            strSQL += "           " + mei.m_GMA_STS + ", ";
            strSQL += "           " + mei.m_DELETE_DATE + ", ";
            strSQL += "           " + mei.m_DELETE_FLG + " ";
            strSQL += "     ) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 登録内容：作成対象データ（項目トランザクション）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetTeiseiTextTrItemInsertAll(string tmpTableName, TBL_TRITEM item)
        {
            // マルチテーブルインサート用のクエリ
            string strSQL = "";
            strSQL += " INTO " + tmpTableName + " ";
            strSQL += "     ( ";
            // キー
            strSQL += "          GYM_ID, ";
            strSQL += "          OPERATION_DATE, ";
            strSQL += "          SCAN_TERM, ";
            strSQL += "          BAT_ID, ";
            strSQL += "          DETAILS_NO, ";
            strSQL += "          ITEM_ID, ";
            // データ
            strSQL += "          END_DATA, ";
            strSQL += "          ICTEISEI_DATA, ";
            strSQL += "          FIX_TRIGGER ";
            strSQL += "     ) VALUES ( ";
            // キー
            strSQL += "           " + item._GYM_ID + ", ";
            strSQL += "           " + item._OPERATION_DATE + ", ";
            strSQL += "          '" + item._SCAN_TERM + "', ";
            strSQL += "           " + item._BAT_ID + ", ";
            strSQL += "           " + item._DETAILS_NO + ", ";
            strSQL += "           " + item._ITEM_ID + ", ";
            // データ
            strSQL += "          '" + item.m_END_DATA + "', ";
            strSQL += "          '" + item.m_ICTEISEI_DATA + "', ";
            strSQL += "          '" + item.m_FIX_TRIGGER + "' ";
            strSQL += "     ) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 登録内容：作成対象データ（明細イメージ）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string GetTeiseiTextTrImgInsertAll(string tmpTableName, TBL_TRMEIIMG img)
        {
            // マルチテーブルインサート用のクエリ
            string strSQL = "";
            strSQL += " INTO " + tmpTableName + " ";
            strSQL += "     ( ";
            // キー
            strSQL += "          GYM_ID, ";
            strSQL += "          OPERATION_DATE, ";
            strSQL += "          SCAN_TERM, ";
            strSQL += "          BAT_ID, ";
            strSQL += "          DETAILS_NO, ";
            strSQL += "          IMG_KBN, ";
            // データ
            strSQL += "          BUA_STS, ";
            strSQL += "          BUB_CONFIRMDATE, ";
            strSQL += "          BUA_DATE, ";
            strSQL += "          BUA_TIME ";
            strSQL += "     ) VALUES ( ";
            // キー
            strSQL += "           " + img._GYM_ID + ", ";
            strSQL += "           " + img._OPERATION_DATE + ", ";
            strSQL += "          '" + img._SCAN_TERM + "', ";
            strSQL += "           " + img._BAT_ID + ", ";
            strSQL += "           " + img._DETAILS_NO + ", ";
            strSQL += "           " + img._IMG_KBN + ", ";
            // データ
            strSQL += "           " + img.m_BUA_STS + ", ";
            strSQL += "           " + img.m_BUB_CONFIRMDATE + ", ";
            strSQL += "           " + img.m_BUA_DATE + ", ";
            strSQL += "           " + img.m_BUA_TIME + " ";
            strSQL += "     ) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 更新内容：作成対象データ（明細トランザクション）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetTeiseiTextMeiUpdateAll(string tmpTableName, int SchemaBankCD)
        {
            // 一時テーブルのデータを実テーブルに反映する（一発UPDATE）
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSQL += " SET ";
            strSQL += "   ( ";
            strSQL += "       MEI.GMA_DATE ";
            strSQL += "      ,MEI.GMA_STS ";
            strSQL += "      ,MEI.DELETE_DATE ";
            strSQL += "      ,MEI.DELETE_FLG ";
            strSQL += "   ) = ( ";
            strSQL += "     SELECT ";
            strSQL += "         TMP.GMA_DATE ";
            strSQL += "        ,TMP.GMA_STS ";
            strSQL += "        ,TMP.DELETE_DATE ";
            strSQL += "        ,TMP.DELETE_FLG ";
            strSQL += "     FROM ";
            strSQL += "       " + tmpTableName + " TMP ";
            strSQL += "     WHERE ";
            strSQL += "           TMP.GYM_ID = MEI.GYM_ID ";
            strSQL += "       AND TMP.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "       AND TMP.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "       AND TMP.BAT_ID = MEI.BAT_ID ";
            strSQL += "       AND TMP.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "   ) ";
            strSQL += " WHERE ";
            strSQL += "     EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + tmpTableName + " WKMEI ";
            strSQL += "         WHERE ";
            strSQL += "               WKMEI.GYM_ID = MEI.GYM_ID ";
            strSQL += "           AND WKMEI.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "           AND WKMEI.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "           AND WKMEI.BAT_ID = MEI.BAT_ID ";
            strSQL += "           AND WKMEI.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "     ) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 更新内容：作成対象データ（項目トランザクション）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetTeiseiTextItemUpdateAll(string tmpTableName, int SchemaBankCD)
        {
            // 一時テーブルのデータを実テーブルに反映する（一発UPDATE）
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " ITEM ";
            strSQL += " SET ";
            strSQL += "   ( ";
            strSQL += "       ITEM.END_DATA ";
            strSQL += "      ,ITEM.ICTEISEI_DATA ";
            strSQL += "      ,ITEM.FIX_TRIGGER ";
            strSQL += "   ) = ( ";
            strSQL += "     SELECT ";
            strSQL += "         TMP.END_DATA ";
            strSQL += "        ,TMP.ICTEISEI_DATA ";
            strSQL += "        ,TMP.FIX_TRIGGER ";
            strSQL += "     FROM ";
            strSQL += "       " + tmpTableName + " TMP ";
            strSQL += "     WHERE ";
            strSQL += "           TMP.GYM_ID = ITEM.GYM_ID ";
            strSQL += "       AND TMP.OPERATION_DATE = ITEM.OPERATION_DATE ";
            strSQL += "       AND TMP.SCAN_TERM = ITEM.SCAN_TERM ";
            strSQL += "       AND TMP.BAT_ID = ITEM.BAT_ID ";
            strSQL += "       AND TMP.DETAILS_NO = ITEM.DETAILS_NO ";
            strSQL += "       AND TMP.ITEM_ID = ITEM.ITEM_ID ";
            strSQL += "   ) ";
            strSQL += " WHERE ";
            strSQL += "     EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + tmpTableName + " WKITEM ";
            strSQL += "         WHERE ";
            strSQL += "               WKITEM.GYM_ID = ITEM.GYM_ID ";
            strSQL += "           AND WKITEM.OPERATION_DATE = ITEM.OPERATION_DATE ";
            strSQL += "           AND WKITEM.SCAN_TERM = ITEM.SCAN_TERM ";
            strSQL += "           AND WKITEM.BAT_ID = ITEM.BAT_ID ";
            strSQL += "           AND WKITEM.DETAILS_NO = ITEM.DETAILS_NO ";
            strSQL += "           AND WKITEM.ITEM_ID = ITEM.ITEM_ID ";
            strSQL += "     ) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 更新内容：作成対象データ（項目トランザクション）[持帰]
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetTeiseiTextItemUpdateInClearingAll(string tmpTableName, string FixTrigger, int SchemaBankCD)
        {
            // 一時テーブルのデータから実テーブルに反映する
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " ITEM ";
            strSQL += " SET ";
            strSQL += "   ITEM.ICTEISEI_DATA = ITEM.END_DATA ";
            strSQL += "  ,ITEM.FIX_TRIGGER = '" + FixTrigger + "' ";
            strSQL += " WHERE ";
            strSQL += "     ITEM.ITEM_ID IN(" + DspItem.ItemId.券面持帰銀行コード + "," + DspItem.ItemId.持帰銀行コード + ","
                                              + DspItem.ItemId.持帰銀行名 + "," + DspItem.ItemId.金額 + "," + DspItem.ItemId.入力交換希望日 + ","
                                              + DspItem.ItemId.和暦交換希望日 + "," + DspItem.ItemId.交換日 + ")";
            strSQL += " AND EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + tmpTableName + " WKITEM ";
            strSQL += "         WHERE ";
            strSQL += "               WKITEM.GYM_ID = ITEM.GYM_ID ";
            strSQL += "           AND WKITEM.OPERATION_DATE = ITEM.OPERATION_DATE ";
            strSQL += "           AND WKITEM.SCAN_TERM = ITEM.SCAN_TERM ";
            strSQL += "           AND WKITEM.BAT_ID = ITEM.BAT_ID ";
            strSQL += "           AND WKITEM.DETAILS_NO = ITEM.DETAILS_NO ";
            strSQL += "     ) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 更新内容：作成対象データ（項目トランザクション）[持帰]
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns>持帰銀行訂正時の削除処理</returns>
        public static string GetTeiseiTextImgUpdateInClearingAll(string tmpTableName, int OpDate, int SchemaBankCD)
        {
            // 一時テーブルのデータから実テーブルに反映する
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " MEIIMG ";
            strSQL += " SET ";
            strSQL += "   MEIIMG.DELETE_DATE = " + OpDate + " ";
            strSQL += "  ,MEIIMG.DELETE_FLG = 1 ";
            strSQL += " WHERE ";
            strSQL += "     MEIIMG.DELETE_FLG = 0 ";
            strSQL += " AND EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + tmpTableName + " WKMEI ";
            strSQL += "         WHERE ";
            strSQL += "               WKMEI.GYM_ID = MEIIMG.GYM_ID ";
            strSQL += "           AND WKMEI.OPERATION_DATE = MEIIMG.OPERATION_DATE ";
            strSQL += "           AND WKMEI.SCAN_TERM = MEIIMG.SCAN_TERM ";
            strSQL += "           AND WKMEI.BAT_ID = MEIIMG.BAT_ID ";
            strSQL += "           AND WKMEI.DETAILS_NO = MEIIMG.DETAILS_NO ";
            strSQL += "           AND WKMEI.DELETE_FLG = 1 ";
            strSQL += "     ) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0303　持帰証券データ訂正テキスト作成
        /// 更新内容：作成対象データ（明細イメージトランザクション）[持出]
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns>持出アップロードの初期化</returns>
        public static string GetTeiseiTextImgUpdateBUAResetAll(string tmpTableName, int SchemaBankCD)
        {
            // WKテーブルには表面のみ登録されるため表面の内容をすべてのイメージに反映
            // (結果正常で未削除データが対象)
            // 一時テーブルのデータを実テーブルに反映する（一発UPDATE）
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " MEIIMG ";
            strSQL += " SET ";
            strSQL += "   ( ";
            strSQL += "       MEIIMG.BUA_STS ";
            strSQL += "      ,MEIIMG.BUB_CONFIRMDATE ";
            strSQL += "      ,MEIIMG.BUA_DATE ";
            strSQL += "      ,MEIIMG.BUA_TIME ";
            strSQL += "   ) = ( ";
            strSQL += "     SELECT ";
            strSQL += "         TMP.BUA_STS ";
            strSQL += "        ,TMP.BUB_CONFIRMDATE ";
            strSQL += "        ,TMP.BUA_DATE ";
            strSQL += "        ,TMP.BUA_TIME ";
            strSQL += "     FROM ";
            strSQL += "       " + tmpTableName + " TMP ";
            strSQL += "     WHERE ";
            strSQL += "           TMP.GYM_ID = MEIIMG.GYM_ID ";
            strSQL += "       AND TMP.OPERATION_DATE = MEIIMG.OPERATION_DATE ";
            strSQL += "       AND TMP.SCAN_TERM = MEIIMG.SCAN_TERM ";
            strSQL += "       AND TMP.BAT_ID = MEIIMG.BAT_ID ";
            strSQL += "       AND TMP.DETAILS_NO = MEIIMG.DETAILS_NO ";
            strSQL += "   ) ";
            strSQL += " WHERE ";
            strSQL += "     MEIIMG.DELETE_FLG = 0 ";
            strSQL += " AND MEIIMG.BUA_STS =  " + TrMei.Sts.結果正常 + " ";
            strSQL += " AND EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + tmpTableName + " WKIMG ";
            strSQL += "         WHERE ";
            strSQL += "               WKIMG.GYM_ID = MEIIMG.GYM_ID ";
            strSQL += "           AND WKIMG.OPERATION_DATE = MEIIMG.OPERATION_DATE ";
            strSQL += "           AND WKIMG.SCAN_TERM = MEIIMG.SCAN_TERM ";
            strSQL += "           AND WKIMG.BAT_ID = MEIIMG.BAT_ID ";
            strSQL += "           AND WKIMG.DETAILS_NO = MEIIMG.DETAILS_NO ";
            strSQL += "     ) ";
            return strSQL;
        }

        #endregion

        // *******************************************************************
        // E0305　持出取消テキスト作成
        // *******************************************************************

        #region * E0305　持出取消テキスト作成
        /// <summary>
        /// 呼出画面：E0305　持出取消テキスト作成
        /// 取得内容：作成対象データ（明細：持出）
        /// </summary>
        /// <param name="type1">Type0：全データ、Type1：他行データ、Type2：自行データ</param>
        /// <param name="bankCd">自行番号</param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetCancelTextOCMeiListSelect(SearchType type1, int bankCd, int SchemaBankCD)
        {
            // 行内交換する場合は自行を対象外にする
            string strInternalExchange = "";
            if (type1 == SearchType.Type1)
            {
                // 他行データ
                strInternalExchange = GetOtherBankQueryOC(bankCd, SchemaBankCD);
            }
            else if (type1 == SearchType.Type2)
            {
                // 自行データ
                strInternalExchange = GetOwnBankQueryOC(bankCd, SchemaBankCD);
            }

            string strSQL = "";
            strSQL += " SELECT DISTINCT {0} ";
            strSQL += " FROM ";
            strSQL += "     " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSQL += "     INNER JOIN " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " IMG ";
            strSQL += "         ON  IMG.GYM_ID = MEI.GYM_ID ";
            strSQL += "         AND IMG.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "         AND IMG.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "         AND IMG.BAT_ID = MEI.BAT_ID ";
            strSQL += "         AND IMG.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "         AND IMG.IMG_KBN = " + TrMeiImg.ImgKbn.表 + " ";
            strSQL += "     INNER JOIN " + TBL_DSP_ITEM.TABLE_NAME(SchemaBankCD) + " DI ";
            strSQL += "         ON  MEI.GYM_ID = DI.GYM_ID ";
            strSQL += "         AND MEI.DSP_ID = DI.DSP_ID ";
            strSQL += "     INNER JOIN " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " ITEM ";
            strSQL += "         ON  ITEM.GYM_ID = MEI.GYM_ID ";
            strSQL += "         AND ITEM.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "         AND ITEM.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "         AND ITEM.BAT_ID = MEI.BAT_ID ";
            strSQL += "         AND ITEM.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "         AND ITEM.ITEM_ID = DI.ITEM_ID ";
            strSQL += " WHERE ";
            strSQL += "         MEI.GYM_ID = " + GymParam.GymId.持出 + " ";
            //strSQL += "     AND MEI.DELETE_FLG = 1 ";
            //strSQL += "     AND NOT( MEI.GRA_DATE > 0 ) "; // 不渡返還通知での削除データは対象外
            //strSQL += "     AND MEI.BCA_STS IN ( ";
            //strSQL += "       " + TrMei.Sts.未作成 + ", ";
            //strSQL += "       " + TrMei.Sts.再作成対象 + " ";
            //strSQL += "     ) ";
            // 持出取消アップロード状態が「作成対象」 & 表面がアップロード済
            strSQL += "     AND MEI.BCA_STS IN ( ";
            strSQL += "       " + TrMei.Sts.作成対象 + " ";
            strSQL += "     ) ";
            strSQL += "     AND IMG.BUA_STS = " + TrMei.Sts.結果正常 + " ";
            strSQL += strInternalExchange;
            strSQL += " ORDER BY ";
            strSQL += "     1,2,3,4,5 ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：持出取消テキスト作成
        /// 取得内容：作成対象データ（明細：持帰）
        /// </summary>
        /// <param name="fileNameList"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetCancelTextICMeiListSelect(List<string> fileNameList, int SchemaBankCD)
        {
            string strSQLFileList = "";
            string strSQLSub = "";
            foreach (string fileName in fileNameList)
            {
                strSQLFileList += string.Format(" {0} ICIMG.IMG_FLNM ='{1}' ", strSQLSub, fileName);
                strSQLSub = "OR";
            }

            string strSQL = "";
            strSQL += " SELECT DISTINCT {0} ";
            strSQL += " FROM ";
            strSQL += "     " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " ICIMG ";
            strSQL += "     INNER JOIN " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " ICMEI ";
            strSQL += "         ON  ICIMG.GYM_ID = ICMEI.GYM_ID ";
            strSQL += "         AND ICIMG.OPERATION_DATE = ICMEI.OPERATION_DATE ";
            strSQL += "         AND ICIMG.SCAN_TERM = ICMEI.SCAN_TERM ";
            strSQL += "         AND ICIMG.BAT_ID = ICMEI.BAT_ID ";
            strSQL += "         AND ICIMG.DETAILS_NO = ICMEI.DETAILS_NO ";
            strSQL += "     INNER JOIN " + TBL_DSP_ITEM.TABLE_NAME(SchemaBankCD) + " DI ";
            strSQL += "         ON  DI.GYM_ID = ICMEI.GYM_ID ";
            strSQL += "         AND DI.DSP_ID = ICMEI.DSP_ID ";
            strSQL += "     INNER JOIN " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " OCITEM ";
            strSQL += "         ON  OCITEM.GYM_ID = ICMEI.GYM_ID ";
            strSQL += "         AND OCITEM.OPERATION_DATE = ICMEI.OPERATION_DATE ";
            strSQL += "         AND OCITEM.SCAN_TERM = ICMEI.SCAN_TERM ";
            strSQL += "         AND OCITEM.BAT_ID = ICMEI.BAT_ID ";
            strSQL += "         AND OCITEM.DETAILS_NO = ICMEI.DETAILS_NO ";
            strSQL += "         AND OCITEM.ITEM_ID = DI.ITEM_ID ";
            strSQL += " WHERE ";
            strSQL += "         ICIMG.GYM_ID = " + GymParam.GymId.持帰 + " ";
            strSQL += "     AND ICIMG.IMG_KBN = " + TrMeiImg.ImgKbn.表 + " ";
            strSQL += "     AND (" + strSQLFileList + ") ";
            //strSQL += "     AND ICMEI.DELETE_FLG = 0 ";
            strSQL += " ORDER BY ";
            strSQL += "     1,2,3,4,5 ";

            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0305　持出取消テキスト作成
        /// 登録内容：作成対象データ（明細トランザクション）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="mei"></param>
        /// <returns></returns>
        public static string GetCancelTextTrMeiInsertAll(string tmpTableName, TBL_TRMEI mei)
        {
            // マルチテーブルインサート用のクエリ
            string strSQL = "";
            strSQL += " INTO " + tmpTableName + " ";
            strSQL += "     ( ";
            // キー
            strSQL += "          GYM_ID, ";
            strSQL += "          OPERATION_DATE, ";
            strSQL += "          SCAN_TERM, ";
            strSQL += "          BAT_ID, ";
            strSQL += "          DETAILS_NO, ";
            // データ
            strSQL += "          BCA_STS, ";
            strSQL += "          BCA_DATE, ";
            strSQL += "          DELETE_DATE, ";
            strSQL += "          DELETE_FLG ";
            strSQL += "     ) VALUES ( ";
            // キー
            strSQL += "           " + mei._GYM_ID + ", ";
            strSQL += "           " + mei._OPERATION_DATE + ", ";
            strSQL += "          '" + mei._SCAN_TERM + "', ";
            strSQL += "           " + mei._BAT_ID + ", ";
            strSQL += "           " + mei._DETAILS_NO + ", ";
            // データ
            strSQL += "           " + mei.m_BCA_STS + ", ";
            strSQL += "           " + mei.m_BCA_DATE + ", ";
            strSQL += "           " + mei.m_DELETE_DATE + ", ";
            strSQL += "           " + mei.m_DELETE_FLG + " ";
            strSQL += "     ) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0305　持出取消テキスト作成
        /// 更新内容：作成対象データ（明細トランザクション）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetCancelTextMeiUpdateAll(string tmpTableName, int SchemaBankCD)
        {
            // 一時テーブルのデータを実テーブルに反映する（一発UPDATE）
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSQL += " SET ";
            strSQL += "   ( ";
            strSQL += "       MEI.BCA_STS ";
            strSQL += "      ,MEI.BCA_DATE ";
            strSQL += "      ,MEI.DELETE_DATE ";
            strSQL += "      ,MEI.DELETE_FLG ";
            strSQL += "   ) = ( ";
            strSQL += "     SELECT ";
            strSQL += "         TMP.BCA_STS ";
            strSQL += "        ,TMP.BCA_DATE ";
            strSQL += "        ,TMP.DELETE_DATE ";
            strSQL += "        ,TMP.DELETE_FLG ";
            strSQL += "     FROM ";
            strSQL += "       " + tmpTableName + " TMP ";
            strSQL += "     WHERE ";
            strSQL += "           TMP.GYM_ID = MEI.GYM_ID ";
            strSQL += "       AND TMP.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "       AND TMP.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "       AND TMP.BAT_ID = MEI.BAT_ID ";
            strSQL += "       AND TMP.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "   ) ";
            strSQL += " WHERE ";
            strSQL += "     EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + tmpTableName + " WKMEI ";
            strSQL += "         WHERE ";
            strSQL += "               WKMEI.GYM_ID = MEI.GYM_ID ";
            strSQL += "           AND WKMEI.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "           AND WKMEI.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "           AND WKMEI.BAT_ID = MEI.BAT_ID ";
            strSQL += "           AND WKMEI.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "     ) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0305　持出取消テキスト作成
        /// 更新内容：作成対象データ（明細イメージトランザクション）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetCancelTextMeiImgUpdateAll(string tmpTableName, int SchemaBankCD)
        {
            // 一時テーブルのデータを実テーブルに反映する（一発UPDATE）
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " MEIIMG ";
            strSQL += " SET ";
            strSQL += "   ( ";
            strSQL += "       MEIIMG.DELETE_DATE ";
            strSQL += "      ,MEIIMG.DELETE_FLG ";
            strSQL += "   ) = ( ";
            strSQL += "     SELECT ";
            strSQL += "         TMP.DELETE_DATE ";
            strSQL += "        ,TMP.DELETE_FLG ";
            strSQL += "     FROM ";
            strSQL += "       " + tmpTableName + " TMP ";
            strSQL += "     WHERE ";
            strSQL += "           TMP.GYM_ID = MEIIMG.GYM_ID ";
            strSQL += "       AND TMP.OPERATION_DATE = MEIIMG.OPERATION_DATE ";
            strSQL += "       AND TMP.SCAN_TERM = MEIIMG.SCAN_TERM ";
            strSQL += "       AND TMP.BAT_ID = MEIIMG.BAT_ID ";
            strSQL += "       AND TMP.DETAILS_NO = MEIIMG.DETAILS_NO ";
            strSQL += "   ) ";
            strSQL += " WHERE ";
            strSQL += "     EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + tmpTableName + " WKMEI ";
            strSQL += "         WHERE ";
            strSQL += "               WKMEI.GYM_ID = MEIIMG.GYM_ID ";
            strSQL += "           AND WKMEI.OPERATION_DATE = MEIIMG.OPERATION_DATE ";
            strSQL += "           AND WKMEI.SCAN_TERM = MEIIMG.SCAN_TERM ";
            strSQL += "           AND WKMEI.BAT_ID = MEIIMG.BAT_ID ";
            strSQL += "           AND WKMEI.DETAILS_NO = MEIIMG.DETAILS_NO ";
            strSQL += "           AND WKMEI.DELETE_FLG = 1 ";
            strSQL += "     ) ";
            return strSQL;
        }

        #endregion

        // *******************************************************************
        // E0306　持出証券イメージアーカイブ作成
        // *******************************************************************

        #region * E0306　持出証券イメージアーカイブ作成
        /// <summary>
        /// 呼出画面：E0306　持出証券イメージアーカイブ作成
        /// 取得内容：作成対象データ（明細）
        /// </summary>
        /// <param name="type1">Type1：期日管理以外、Type2：期日管理</param>
        /// <param name="type2">Type0：全データ、Type1：他行データ、Type2：自行データ</param>
        /// <param name="bankCd">自行番号</param>
        /// <param name="clearingDate"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetArchiveMeiListSelect(SearchType type1, SearchType type2, int bankCd, int clearingDate, int SchemaBankCD)
        {
            // 交換希望日
            string strClearingDate = "";
            if (clearingDate > 0)
            {
                strClearingDate += "     AND EXISTS ( ";
                strClearingDate += "         SELECT 1 FROM " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " WT ";
                strClearingDate += "         WHERE ";
                strClearingDate += "             WT.GYM_ID = MEI.GYM_ID ";
                strClearingDate += "             AND WT.OPERATION_DATE = MEI.OPERATION_DATE ";
                strClearingDate += "             AND WT.SCAN_TERM = MEI.SCAN_TERM ";
                strClearingDate += "             AND WT.BAT_ID = MEI.BAT_ID ";
                strClearingDate += "             AND WT.DETAILS_NO = MEI.DETAILS_NO ";
                strClearingDate += "             AND WT.ITEM_ID = " + DspItem.ItemId.入力交換希望日 + " ";
                strClearingDate += "             AND NVL(WT.END_DATA,' ') = '" + clearingDate + "' ";
                strClearingDate += "     ) ";
            }

            // 業務
            string strInputRoute = "";
            if (type1 == SearchType.Type1)
            {
                strInputRoute = "     AND BAT.INPUT_ROUTE <> " + TrBatch.InputRoute.期日管理 + " ";
            }
            else if (type1 == SearchType.Type2)
            {
                strInputRoute = "     AND BAT.INPUT_ROUTE = " + TrBatch.InputRoute.期日管理 + " ";
            }

            // 行内交換する場合は自行を対象外にする
            string strInternalExchange = "";
            if (type2 == SearchType.Type1)
            {
                // 他行データ
                strInternalExchange = GetOtherBankQueryOC(bankCd, SchemaBankCD);
            }
            else if (type2 == SearchType.Type2)
            {
                // 自行データ
                strInternalExchange = GetOwnBankQueryOC(bankCd, SchemaBankCD);
            }

            string strFROM = "";
            strFROM += " FROM ";
            strFROM += "     " + TBL_TRBATCH.TABLE_NAME(SchemaBankCD) + " BAT ";
            strFROM += "     INNER JOIN " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strFROM += "         ON BAT.GYM_ID = MEI.GYM_ID ";
            strFROM += "         AND BAT.OPERATION_DATE = MEI.OPERATION_DATE ";
            strFROM += "         AND BAT.SCAN_TERM = MEI.SCAN_TERM ";
            strFROM += "         AND BAT.BAT_ID = MEI.BAT_ID ";
            strFROM += "     INNER JOIN " + TBL_DSP_ITEM.TABLE_NAME(SchemaBankCD) + " DI ";
            strFROM += "         ON  MEI.GYM_ID = DI.GYM_ID ";
            strFROM += "         AND MEI.DSP_ID = DI.DSP_ID ";
            strFROM += "     INNER JOIN " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS ";
            strFROM += "         ON  MEI.GYM_ID = STS.GYM_ID ";
            strFROM += "         AND MEI.OPERATION_DATE = STS.OPERATION_DATE ";
            strFROM += "         AND MEI.SCAN_TERM = STS.SCAN_TERM ";
            strFROM += "         AND MEI.BAT_ID = STS.BAT_ID ";
            strFROM += "         AND MEI.DETAILS_NO = STS.DETAILS_NO ";
            strFROM += "     INNER JOIN " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " IMG ";
            strFROM += "         ON  IMG.GYM_ID = MEI.GYM_ID ";
            strFROM += "         AND IMG.OPERATION_DATE = MEI.OPERATION_DATE ";
            strFROM += "         AND IMG.SCAN_TERM = MEI.SCAN_TERM ";
            strFROM += "         AND IMG.BAT_ID = MEI.BAT_ID ";
            strFROM += "         AND IMG.DETAILS_NO = MEI.DETAILS_NO ";
            strFROM += "     INNER JOIN " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " ITEM ";
            strFROM += "         ON  ITEM.GYM_ID = MEI.GYM_ID ";
            strFROM += "         AND ITEM.OPERATION_DATE = MEI.OPERATION_DATE ";
            strFROM += "         AND ITEM.SCAN_TERM = MEI.SCAN_TERM ";
            strFROM += "         AND ITEM.BAT_ID = MEI.BAT_ID ";
            strFROM += "         AND ITEM.DETAILS_NO = MEI.DETAILS_NO ";
            strFROM += "         AND ITEM.ITEM_ID = DI.ITEM_ID ";
            strFROM += " WHERE ";
            strFROM += "         BAT.GYM_ID = " + GymParam.GymId.持出 + " ";
            strFROM += "     AND MEI.DELETE_FLG = 0 ";
            strFROM += "     AND IMG.DELETE_FLG = 0 ";
            strFROM += "     AND IMG.BUA_STS IN ( ";
            strFROM += "       " + TrMei.Sts.未作成 + ", ";
            strFROM += "       " + TrMei.Sts.再作成対象 + " ";
            strFROM += "     ) ";
            strFROM += "     AND STS.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.交換尻 + " ";
            strFROM += "     AND STS.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ";
            strFROM += "     AND EXISTS ( ";
            strFROM += "         SELECT 1 FROM " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " WT ";
            strFROM += "         WHERE ";
            strFROM += "             WT.GYM_ID = MEI.GYM_ID ";
            strFROM += "             AND WT.OPERATION_DATE = MEI.OPERATION_DATE ";
            strFROM += "             AND WT.SCAN_TERM = MEI.SCAN_TERM ";
            strFROM += "             AND WT.BAT_ID = MEI.BAT_ID ";
            strFROM += "             AND WT.DETAILS_NO = MEI.DETAILS_NO ";
            strFROM += "             AND WT.ITEM_ID = " + DspItem.ItemId.入力交換希望日 + " ";
            strFROM += "             AND WT.END_DATA IS NOT NULL ";
            strFROM += "     ) ";
            strFROM += "     AND EXISTS ( ";
            strFROM += "         SELECT 1 FROM " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS1 ";
            strFROM += "         WHERE ";
            strFROM += "             STS1.GYM_ID = MEI.GYM_ID ";
            strFROM += "             AND STS1.OPERATION_DATE = MEI.OPERATION_DATE ";
            strFROM += "             AND STS1.SCAN_TERM = MEI.SCAN_TERM ";
            strFROM += "             AND STS1.BAT_ID = MEI.BAT_ID ";
            strFROM += "             AND STS1.DETAILS_NO = MEI.DETAILS_NO ";
            strFROM += "             AND STS1.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.持帰銀行 + " ";
            strFROM += "             AND STS1.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ";
            strFROM += "     ) ";
            strFROM += "     AND EXISTS ( ";
            strFROM += "         SELECT 1 FROM " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS3 ";
            strFROM += "         WHERE ";
            strFROM += "             STS3.GYM_ID = MEI.GYM_ID ";
            strFROM += "             AND STS3.OPERATION_DATE = MEI.OPERATION_DATE ";
            strFROM += "             AND STS3.SCAN_TERM = MEI.SCAN_TERM ";
            strFROM += "             AND STS3.BAT_ID = MEI.BAT_ID ";
            strFROM += "             AND STS3.DETAILS_NO = MEI.DETAILS_NO ";
            strFROM += "             AND STS3.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.金額 + " ";
            strFROM += "             AND STS3.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ";
            strFROM += "     ) ";
            strFROM += strClearingDate;
            strFROM += strInputRoute;
            strFROM += strInternalExchange;

            string strSQL = "";
            strSQL = " SELECT DISTINCT {0} " + strFROM;
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0306　持出証券イメージアーカイブ作成
        /// 取得内容：作成対象データ（明細イメージ）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetArchiveImgListSelect(string tmpTableName, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " SELECT ";
            strSQL += "     IMG.* ";
            strSQL += " FROM ";
            strSQL += "     " + tmpTableName + " TMP ";
            strSQL += "     INNER JOIN " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " IMG ";
            strSQL += "         ON  IMG.GYM_ID = TMP.GYM_ID ";
            strSQL += "         AND IMG.OPERATION_DATE = TMP.OPERATION_DATE ";
            strSQL += "         AND IMG.SCAN_TERM = TMP.SCAN_TERM ";
            strSQL += "         AND IMG.BAT_ID = TMP.BAT_ID ";
            strSQL += "         AND IMG.DETAILS_NO = TMP.DETAILS_NO ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0306　持出証券イメージアーカイブ作成
        /// 登録内容：作成対象データ（明細イメージ）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string GetArchiveImgInsertAll(string tmpTableName, TBL_TRMEIIMG img)
        {
            // マルチテーブルインサート用のクエリ
            string strSQL = "";
            strSQL += " INTO " + tmpTableName + " ";
            strSQL += "     ( ";
            // キー
            strSQL += "          GYM_ID, ";
            strSQL += "          OPERATION_DATE, ";
            strSQL += "          SCAN_TERM, ";
            strSQL += "          BAT_ID, ";
            strSQL += "          DETAILS_NO, ";
            strSQL += "          IMG_KBN, ";
            // データ
            strSQL += "          IMG_FLNM, ";
            strSQL += "          OC_IC_BK_NO, ";
            strSQL += "          OC_OC_DATE, ";
            strSQL += "          OC_CLEARING_DATE, ";
            strSQL += "          OC_AMOUNT, ";
            strSQL += "          PAY_KBN, ";
            strSQL += "          BUA_STS, ";
            strSQL += "          BUB_CONFIRMDATE, ";
            strSQL += "          BUA_DATE, ";
            strSQL += "          BUA_TIME, ";
            strSQL += "          IMG_ARCH_NAME ";
            strSQL += "     ) VALUES ( ";
            // キー
            strSQL += "           " + img._GYM_ID + ", ";
            strSQL += "           " + img._OPERATION_DATE + ", ";
            strSQL += "          '" + img._SCAN_TERM + "', ";
            strSQL += "           " + img._BAT_ID + ", ";
            strSQL += "           " + img._DETAILS_NO + ", ";
            strSQL += "           " + img._IMG_KBN + ", ";
            // データ
            strSQL += "          '" + img.m_IMG_FLNM + "', ";
            strSQL += "          '" + img.m_OC_IC_BK_NO + "', ";
            strSQL += "          '" + img.m_OC_OC_DATE + "', ";
            strSQL += "          '" + img.m_OC_CLEARING_DATE + "', ";
            strSQL += "          '" + img.m_OC_AMOUNT + "', ";
            strSQL += "          '" + img.m_PAY_KBN + "', ";
            strSQL += "           " + img.m_BUA_STS + ", ";
            strSQL += "           " + img.m_BUB_CONFIRMDATE + ", ";
            strSQL += "           " + img.m_BUA_DATE + ", ";
            strSQL += "           " + img.m_BUA_TIME + ", ";
            strSQL += "          '" + img.m_IMG_ARCH_NAME + "' ";
            strSQL += "     ) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0306　持出証券イメージアーカイブ作成
        /// 登録内容：作成対象データ（持帰要求結果証券明細テキスト）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="bill"></param>
        /// <returns></returns>
        public static string GetArchiveBillInsertAll(string tmpTableName, TBL_ICREQRET_BILLMEITXT bill)
        {
            // マルチテーブルインサート用のクエリ
            string strSQL = "";
            strSQL += " INTO " + tmpTableName + " ";
            strSQL += "     ( ";
            strSQL += "       " + TBL_ICREQRET_BILLMEITXT.ALL_COLUMNS;
            strSQL += "     ) VALUES ( ";
            // キー
            strSQL += "          '" + bill._MEI_TXT_NAME + "',";
            strSQL += "           " + bill._CAP_KBN + ",";
            strSQL += "          '" + bill._IMG_NAME + "',";
            // データ
            strSQL += "          '" + bill.m_IMG_ARCH_NAME + "',";
            strSQL += "          '" + bill.m_FRONT_IMG_NAME + "',";
            strSQL += "           " + bill.m_IMG_KBN + ",";
            strSQL += "          '" + bill.m_FILE_OC_BK_NO + "',";
            strSQL += "          '" + bill.m_CHG_OC_BK_NO + "',";
            strSQL += "          '" + bill.m_OC_BR_NO + "',";
            strSQL += "           " + bill.m_OC_DATE + ",";
            strSQL += "          '" + bill.m_OC_METHOD + "',";
            strSQL += "          '" + bill.m_OC_USERID + "',";
            strSQL += "          '" + bill.m_PAY_KBN + "',";
            strSQL += "          '" + bill.m_BALANCE_FLG + "',";
            strSQL += "          '" + bill.m_OCR_IC_BK_NO + "',";
            strSQL += "          '" + bill.m_QR_IC_BK_NO + "',";
            strSQL += "          '" + bill.m_MICR_IC_BK_NO + "',";
            strSQL += "          '" + bill.m_FILE_IC_BK_NO + "',";
            strSQL += "          '" + bill.m_CHG_IC_BK_NO + "',";
            strSQL += "          '" + bill.m_TEISEI_IC_BK_NO + "',";
            strSQL += "          '" + bill.m_PAY_IC_BK_NO + "',";
            strSQL += "          '" + bill.m_PAYAFT_REV_IC_BK_NO + "',";
            strSQL += "          '" + bill.m_OCR_IC_BK_NO_CONF + "',";
            strSQL += "          '" + bill.m_OCR_AMOUNT + "',";
            strSQL += "          '" + bill.m_MICR_AMOUNT + "',";
            strSQL += "          '" + bill.m_QR_AMOUNT + "',";
            strSQL += "          '" + bill.m_FILE_AMOUNT + "',";
            strSQL += "          '" + bill.m_TEISEI_AMOUNT + "',";
            strSQL += "          '" + bill.m_PAY_AMOUNT + "',";
            strSQL += "          '" + bill.m_PAYAFT_REV_AMOUNT + "',";
            strSQL += "          '" + bill.m_OCR_AMOUNT_CONF + "',";
            strSQL += "          '" + bill.m_OC_CLEARING_DATE + "',";
            strSQL += "          '" + bill.m_TEISEI_CLEARING_DATE + "',";
            strSQL += "          '" + bill.m_CLEARING_DATE + "',";
            strSQL += "          '" + bill.m_QR_IC_BR_NO + "',";
            strSQL += "          '" + bill.m_KAMOKU + "',";
            strSQL += "          '" + bill.m_ACCOUNT + "',";
            strSQL += "          '" + bill.m_BK_CTL_NO + "',";
            strSQL += "          '" + bill.m_FREEFIELD + "',";
            strSQL += "          '" + bill.m_BILL_CODE + "',";
            strSQL += "          '" + bill.m_BILL_CODE_CONF + "',";
            strSQL += "          '" + bill.m_QR + "',";
            strSQL += "          '" + bill.m_MICR + "',";
            strSQL += "          '" + bill.m_MICR_CONF + "',";
            strSQL += "          '" + bill.m_BILL_NO + "',";
            strSQL += "          '" + bill.m_BILL_NO_CONF + "',";
            strSQL += "          '" + bill.m_FUBI_KBN_01 + "',";
            strSQL += "           " + bill.m_ZERO_FUBINO_01 + ",";
            strSQL += "          '" + bill.m_FUBI_KBN_02 + "',";
            strSQL += "           " + bill.m_ZRO_FUBINO_02 + ",";
            strSQL += "          '" + bill.m_FUBI_KBN_03 + "',";
            strSQL += "           " + bill.m_ZRO_FUBINO_03 + ",";
            strSQL += "          '" + bill.m_FUBI_KBN_04 + "',";
            strSQL += "           " + bill.m_ZRO_FUBINO_04 + ",";
            strSQL += "          '" + bill.m_FUBI_KBN_05 + "',";
            strSQL += "           " + bill.m_ZRO_FUBINO_05 + ",";
            strSQL += "          '" + bill.m_IC_FLG + "',";
            strSQL += "           " + bill.m_KAKUTEI_FLG + ",";
            strSQL += "           " + bill.m_KAKUTEI_DATE + ",";
            strSQL += "           " + bill.m_KAKUTEI_TIME + ",";
            strSQL += "          '" + bill.m_KAKUTEI_OPE + "'";
            strSQL += "     ) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0306　持出証券イメージアーカイブ作成
        /// 更新内容：作成対象データ（明細イメージ）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetArchiveImgUpdateAll(string tmpTableName, int SchemaBankCD)
        {
            // 一時テーブルのデータを実テーブルに反映する（一発UPDATE）
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " IMG ";
            strSQL += " SET ";
            strSQL += "   ( ";
            strSQL += "       IMG.IMG_FLNM ";
            strSQL += "     , IMG.OC_IC_BK_NO ";
            strSQL += "     , IMG.OC_OC_DATE ";
            strSQL += "     , IMG.OC_CLEARING_DATE ";
            strSQL += "     , IMG.OC_AMOUNT ";
            strSQL += "     , IMG.PAY_KBN ";
            strSQL += "     , IMG.BUA_STS ";
            strSQL += "     , IMG.BUB_CONFIRMDATE ";
            strSQL += "     , IMG.BUA_DATE ";
            strSQL += "     , IMG.BUA_TIME ";
            strSQL += "     , IMG.IMG_ARCH_NAME ";
            strSQL += "   ) = ( ";
            strSQL += "     SELECT ";
            strSQL += "         TMP.IMG_FLNM ";
            strSQL += "       , TMP.OC_IC_BK_NO ";
            strSQL += "       , TMP.OC_OC_DATE ";
            strSQL += "       , TMP.OC_CLEARING_DATE ";
            strSQL += "       , TMP.OC_AMOUNT ";
            strSQL += "       , TMP.PAY_KBN ";
            strSQL += "       , TMP.BUA_STS ";
            strSQL += "       , TMP.BUB_CONFIRMDATE ";
            strSQL += "       , TMP.BUA_DATE ";
            strSQL += "       , TMP.BUA_TIME ";
            strSQL += "       , TMP.IMG_ARCH_NAME ";
            strSQL += "     FROM ";
            strSQL += "       " + tmpTableName + " TMP ";
            strSQL += "     WHERE ";
            strSQL += "           TMP.GYM_ID = IMG.GYM_ID ";
            strSQL += "       AND TMP.OPERATION_DATE = IMG.OPERATION_DATE ";
            strSQL += "       AND TMP.SCAN_TERM = IMG.SCAN_TERM ";
            strSQL += "       AND TMP.BAT_ID = IMG.BAT_ID ";
            strSQL += "       AND TMP.DETAILS_NO = IMG.DETAILS_NO ";
            strSQL += "       AND TMP.IMG_KBN = IMG.IMG_KBN ";
            strSQL += "   ) ";
            strSQL += " WHERE ";
            strSQL += "     EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + tmpTableName + " WKIMG ";
            strSQL += "         WHERE ";
            strSQL += "               WKIMG.GYM_ID = IMG.GYM_ID ";
            strSQL += "           AND WKIMG.OPERATION_DATE = IMG.OPERATION_DATE ";
            strSQL += "           AND WKIMG.SCAN_TERM = IMG.SCAN_TERM ";
            strSQL += "           AND WKIMG.BAT_ID = IMG.BAT_ID ";
            strSQL += "           AND WKIMG.DETAILS_NO = IMG.DETAILS_NO ";
            strSQL += "           AND WKIMG.IMG_KBN = IMG.IMG_KBN ";
            strSQL += "     ) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0306　持出証券イメージアーカイブ作成
        /// 更新内容：作成対象データ（項目トランザクション）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetArchiveItemUpdateAll(string tmpTableName, int SchemaBankCD)
        {
            // 一時テーブルのデータから更新する
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " ITEM ";
            strSQL += " SET ";
            strSQL += "     ITEM.BUA_DATA = ITEM.END_DATA, ";
            strSQL += "     ITEM.CTR_DATA = ITEM.END_DATA, ";
            strSQL += "     ITEM.FIX_TRIGGER = 'イメージアーカイブ' ";
            strSQL += " WHERE ";
            strSQL += "     ITEM.ITEM_ID IN(" + DspItem.ItemId.券面持帰銀行コード + "," + DspItem.ItemId.持帰銀行コード + ","
                                              + DspItem.ItemId.持帰銀行名 + "," + DspItem.ItemId.金額 + "," + DspItem.ItemId.入力交換希望日 + ","
                                              + DspItem.ItemId.和暦交換希望日 + "," + DspItem.ItemId.交換日 + "," + DspItem.ItemId.決済フラグ + ")";
            strSQL += " AND EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + tmpTableName + " WKIMG ";
            strSQL += "         WHERE ";
            strSQL += "               WKIMG.GYM_ID = ITEM.GYM_ID ";
            strSQL += "           AND WKIMG.OPERATION_DATE = ITEM.OPERATION_DATE ";
            strSQL += "           AND WKIMG.SCAN_TERM = ITEM.SCAN_TERM ";
            strSQL += "           AND WKIMG.BAT_ID = ITEM.BAT_ID ";
            strSQL += "           AND WKIMG.DETAILS_NO = ITEM.DETAILS_NO ";
            strSQL += "     ) ";

            return strSQL;
        }

        #endregion

        // *******************************************************************
        // E0307　不渡返還テキスト作成
        // *******************************************************************

        #region * E0307　不渡返還テキスト作成
        /// <summary>
        /// 呼出画面：E0307　不渡返還テキスト作成
        /// 取得内容：作成対象データ（明細：持帰）
        /// </summary>
        /// <param name="type1">Type1：訂正データ、Type2：取消データ</param>
        /// <param name="type2">Type0：全データ、Type1：他行データ、Type2：自行データ</param>
        /// <param name="bankCd">自行番号</param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetFuwatariTextICMeiListSelect(SearchType type1, SearchType type2, int bankCd, int SchemaBankCD)
        {
            // データ種別
            string strDataType = "";
            if (type1 == SearchType.Type1)
            {
                // 登録データ
                strDataType += "     AND MEI.GRA_STS IN ( ";
                strDataType += "       " + TrMei.Sts.未作成 + ", ";
                strDataType += "       " + TrMei.Sts.再作成対象 + " ";
                strDataType += "     ) ";
                strDataType += "     AND FUWA.DELETE_FLG = 0 ";
            }
            else if (type1 == SearchType.Type2)
            {
                // 取消データ
                strDataType += "     AND MEI.GRA_STS IN ( ";
                strDataType += "       " + TrMei.Sts.再作成対象 + " ";
                strDataType += "     ) ";
                strDataType += "     AND FUWA.DELETE_FLG = 1 ";
            }

            // 行内交換する場合は自行を対象外にする
            string strInternalExchange = "";
            if (type2 == SearchType.Type1)
            {
                // 他行データ
                strInternalExchange = GetOtherBankQueryIC(bankCd, SchemaBankCD);
            }
            else if (type2 == SearchType.Type2)
            {
                // 自行データ
                strInternalExchange = GetOwnBankQueryIC(bankCd, SchemaBankCD);
            }

            string strSQL = "";
            strSQL += " SELECT DISTINCT {0} ";
            strSQL += " FROM ";
            strSQL += "     " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSQL += "     INNER JOIN " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " IMG ";
            strSQL += "         ON  IMG.GYM_ID = MEI.GYM_ID ";
            strSQL += "         AND IMG.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "         AND IMG.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "         AND IMG.BAT_ID = MEI.BAT_ID ";
            strSQL += "         AND IMG.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "         AND IMG.IMG_KBN = " + TrMeiImg.ImgKbn.表 + " ";
            strSQL += "     INNER JOIN " + TBL_TRFUWATARI.TABLE_NAME(SchemaBankCD) + " FUWA ";
            strSQL += "         ON  MEI.GYM_ID = FUWA.GYM_ID ";
            strSQL += "         AND MEI.OPERATION_DATE = FUWA.OPERATION_DATE ";
            strSQL += "         AND MEI.SCAN_TERM = FUWA.SCAN_TERM ";
            strSQL += "         AND MEI.BAT_ID = FUWA.BAT_ID ";
            strSQL += "         AND MEI.DETAILS_NO = FUWA.DETAILS_NO ";
            strSQL += "     LEFT JOIN " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " BKCD ";
            strSQL += "         ON  MEI.GYM_ID = BKCD.GYM_ID ";
            strSQL += "         AND MEI.OPERATION_DATE = BKCD.OPERATION_DATE ";
            strSQL += "         AND MEI.SCAN_TERM = BKCD.SCAN_TERM ";
            strSQL += "         AND MEI.BAT_ID = BKCD.BAT_ID ";
            strSQL += "         AND MEI.DETAILS_NO = BKCD.DETAILS_NO ";
            strSQL += "         AND BKCD.ITEM_ID = " + DspItem.ItemId.持帰銀行コード;
            strSQL += " WHERE ";
            strSQL += "         MEI.GYM_ID = " + GymParam.GymId.持帰 + " ";
            //strSQL += "     AND MEI.DELETE_FLG = 0 ";
            // 持出取消での削除明細 & 持帰訂正での削除明細は対象外
            strSQL += "     AND NOT ( ";
            strSQL += "             ( MEI.DELETE_FLG = 1 AND MEI.BCA_DATE <> 0 ) ";
            strSQL += "          OR ( MEI.DELETE_FLG = 1 AND MEI.GMA_STS = " + TrMei.Sts.結果正常 + " AND NVL(BKCD.END_DATA,' ') <> '" + bankCd.ToString("D4") + "' ) ";
            strSQL += "         ) ";
            strSQL += strDataType;
            strSQL += strInternalExchange;
            strSQL += " ORDER BY ";
            strSQL += "     1,2,3,4,5 ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0307　不渡返還テキスト作成
        /// 取得内容：作成対象データ（明細：持出）
        /// </summary>
        /// <param name="fileNameList"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetFuwatariTextOCMeiListSelect(List<string> fileNameList, int SchemaBankCD)
        {
            string strSQLFileList = "";
            string strSQLSub = "";
            foreach (string fileName in fileNameList)
            {
                strSQLFileList += string.Format(" {0} OCIMG.IMG_FLNM ='{1}' ", strSQLSub, fileName);
                strSQLSub = "OR";
            }

            string strSQL = "";
            strSQL += " SELECT DISTINCT {0} ";
            strSQL += " FROM ";
            strSQL += "     " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " OCIMG ";
            strSQL += "     INNER JOIN " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " OCMEI ";
            strSQL += "         ON  OCIMG.GYM_ID = OCMEI.GYM_ID ";
            strSQL += "         AND OCIMG.OPERATION_DATE = OCMEI.OPERATION_DATE ";
            strSQL += "         AND OCIMG.SCAN_TERM = OCMEI.SCAN_TERM ";
            strSQL += "         AND OCIMG.BAT_ID = OCMEI.BAT_ID ";
            strSQL += "         AND OCIMG.DETAILS_NO = OCMEI.DETAILS_NO ";
            strSQL += " WHERE ";
            strSQL += "         OCIMG.GYM_ID = " + GymParam.GymId.持出 + " ";
            strSQL += "     AND OCIMG.IMG_KBN = " + TrMeiImg.ImgKbn.表 + " ";
            strSQL += "     AND (" + strSQLFileList + ") ";
            //strSQL += "     AND OCMEI.DELETE_FLG = 0 "; // 削除データも対象
            strSQL += " ORDER BY ";
            strSQL += "     1,2,3,4,5 ";

            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0307　不渡返還テキスト作成
        /// 登録内容：作成対象データ（明細トランザクション）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="mei"></param>
        /// <returns></returns>
        public static string GetFuwatariTextTrMeiInsertAll(string tmpTableName, TBL_TRMEI mei)
        {
            // マルチテーブルインサート用のクエリ
            string strSQL = "";
            strSQL += " INTO " + tmpTableName + " ";
            strSQL += "     ( ";
            // キー
            strSQL += "          GYM_ID, ";
            strSQL += "          OPERATION_DATE, ";
            strSQL += "          SCAN_TERM, ";
            strSQL += "          BAT_ID, ";
            strSQL += "          DETAILS_NO, ";
            // データ
            strSQL += "          GRA_DATE, ";
            strSQL += "          GRA_STS, ";
            strSQL += "          GRA_CONFIRMDATE, ";
            strSQL += "          DELETE_DATE, ";
            strSQL += "          DELETE_FLG ";

            strSQL += "     ) VALUES ( ";
            // キー
            strSQL += "           " + mei._GYM_ID + ", ";
            strSQL += "           " + mei._OPERATION_DATE + ", ";
            strSQL += "          '" + mei._SCAN_TERM + "', ";
            strSQL += "           " + mei._BAT_ID + ", ";
            strSQL += "           " + mei._DETAILS_NO + ", ";
            // データ
            strSQL += "           " + mei.m_GRA_DATE + ", ";
            strSQL += "           " + mei.m_GRA_STS + ", ";
            strSQL += "           " + mei.m_GRA_CONFIRMDATE + ", ";
            strSQL += "           " + mei.m_DELETE_DATE + ", ";
            strSQL += "           " + mei.m_DELETE_FLG + " ";
            strSQL += "     ) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0307　不渡返還テキスト作成
        /// 更新内容：作成対象データ（明細トランザクション）
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetFuwatariTextMeiUpdateAll(string tmpTableName, int SchemaBankCD)
        {
            // 一時テーブルのデータを実テーブルに反映する（一発UPDATE）
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSQL += " SET ";
            strSQL += "   ( ";
            strSQL += "       MEI.GRA_DATE ";
            strSQL += "      ,MEI.GRA_STS ";
            strSQL += "      ,MEI.GRA_CONFIRMDATE ";
            strSQL += "      ,MEI.DELETE_DATE ";
            strSQL += "      ,MEI.DELETE_FLG ";

            strSQL += "   ) = ( ";
            strSQL += "     SELECT ";
            strSQL += "         TMP.GRA_DATE ";
            strSQL += "        ,TMP.GRA_STS ";
            strSQL += "        ,TMP.GRA_CONFIRMDATE ";
            strSQL += "        ,TMP.DELETE_DATE ";
            strSQL += "        ,TMP.DELETE_FLG ";
            strSQL += "     FROM ";
            strSQL += "       " + tmpTableName + " TMP ";
            strSQL += "     WHERE ";
            strSQL += "           TMP.GYM_ID = MEI.GYM_ID ";
            strSQL += "       AND TMP.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "       AND TMP.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "       AND TMP.BAT_ID = MEI.BAT_ID ";
            strSQL += "       AND TMP.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "   ) ";
            strSQL += " WHERE ";
            strSQL += "     EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + tmpTableName + " WKMEI ";
            strSQL += "         WHERE ";
            strSQL += "               WKMEI.GYM_ID = MEI.GYM_ID ";
            strSQL += "           AND WKMEI.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "           AND WKMEI.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "           AND WKMEI.BAT_ID = MEI.BAT_ID ";
            strSQL += "           AND WKMEI.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "     ) ";
            return strSQL;
        }
        #endregion

    }
}
