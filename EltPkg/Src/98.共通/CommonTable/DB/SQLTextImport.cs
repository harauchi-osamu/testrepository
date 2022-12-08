using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTable.DB
{
    public class SQLTextImport
    {

        /// <summary>
        /// イメージファイル名からTRITEMを取得するSELECT文を作成します
        /// 証券明細テキスト取込処理
        /// </summary>
        /// <returns></returns>
        public static string GetTRItemDatafileName(string fileName, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM " +
                " WHERE " +
                " EXISTS ( " +
                "  SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                "  WHERE " +
                "       TRMEIIMG.GYM_ID  = TRITEM.GYM_ID " +
                "   AND TRMEIIMG.OPERATION_DATE = TRITEM.OPERATION_DATE " +
                "   AND TRMEIIMG.SCAN_TERM = TRITEM.SCAN_TERM " +
                "   AND TRMEIIMG.BAT_ID = TRITEM.BAT_ID " +
                "   AND TRMEIIMG.DETAILS_NO = TRITEM.DETAILS_NO " +
                "   AND TRMEIIMG.IMG_FLNM = '" + fileName + "' " + 
                " ) ";
            return strSql;
        }

        /// <summary>
        /// イメージファイル名からTRMEIを取得するSELECT文を作成します
        /// 結果テキスト取込処理
        /// </summary>
        /// <returns></returns>
        public static string GetTRMeiDatafileName(int GymID, string fileName, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                " WHERE " +
                " TRMEI.GYM_ID = " + GymID + " " +
                " AND EXISTS ( " +
                "  SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                "  WHERE " +
                "       TRMEIIMG.GYM_ID  = TRMEI.GYM_ID " +
                "   AND TRMEIIMG.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "   AND TRMEIIMG.SCAN_TERM = TRMEI.SCAN_TERM " +
                "   AND TRMEIIMG.BAT_ID = TRMEI.BAT_ID " +
                "   AND TRMEIIMG.DETAILS_NO = TRMEI.DETAILS_NO " +
                "   AND TRMEIIMG.IMG_FLNM = '" + fileName + "' " +
                " ) ";
            return strSql;
        }

        /// <summary>
        /// イメージファイル名からTRITEMを取得するSELECT文を作成します
        /// 結果テキスト取込処理
        /// </summary>
        /// <returns></returns>
        public static string GetTRItemDatafileName(int GymID, string fileName, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM " +
                " WHERE " +
                " TRITEM.GYM_ID = " + GymID + " " +
                " AND EXISTS ( " +
                "  SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                "  WHERE " +
                "       TRMEIIMG.GYM_ID  = TRITEM.GYM_ID " +
                "   AND TRMEIIMG.OPERATION_DATE = TRITEM.OPERATION_DATE " +
                "   AND TRMEIIMG.SCAN_TERM = TRITEM.SCAN_TERM " +
                "   AND TRMEIIMG.BAT_ID = TRITEM.BAT_ID " +
                "   AND TRMEIIMG.DETAILS_NO = TRITEM.DETAILS_NO " +
                "   AND TRMEIIMG.IMG_FLNM = '" + fileName + "' " +
                " ) ";
            return strSql;
        }

        /// <summary>
        /// イメージファイル名からTRIMEIの項目を更新するUPDATE文を作成します
        /// 結果テキスト取込処理
        /// 通知テキスト取込処理
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRMEIFileName(int GymID, string FileName, Dictionary<string,int> UpdateData, int Schemabankcd)
        {
            string SET = string.Empty;
            foreach(string Key in UpdateData.Keys)
            {
                if (!string.IsNullOrEmpty(SET)) SET += ",";
                SET += "  TRMEI." + Key + "=" + UpdateData[Key] + " ";
            }
            string strSql = "UPDATE " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                " SET " + SET +
                " WHERE " +
                " TRMEI.GYM_ID = " + GymID + " " +
                " AND EXISTS ( " +
                "  SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                "  WHERE " +
                "       TRMEIIMG.GYM_ID  = TRMEI.GYM_ID " +
                "   AND TRMEIIMG.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "   AND TRMEIIMG.SCAN_TERM = TRMEI.SCAN_TERM " +
                "   AND TRMEIIMG.BAT_ID = TRMEI.BAT_ID " +
                "   AND TRMEIIMG.DETAILS_NO = TRMEI.DETAILS_NO " +
                "   AND TRMEIIMG.IMG_FLNM = '" + FileName + "' " +
                " ) ";
            return strSql;
        }

        /// <summary>
        /// イメージファイル名からTRIMEIIMGの項目を更新するUPDATE文を作成します
        /// 結果テキスト取込処理
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRMEIIMGFileName(int GymID, string FileName, Dictionary<string, int> UpdateData, int Schemabankcd)
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
                " AND TRMEIIMG.IMG_FLNM = '" + FileName + "' ";
            return strSql;
        }

        /// <summary>
        /// 対象イメージファイル名(表面を想定)から対象証券のTRIMEIIMGの項目を一括更新するUPDATE文を作成します
        /// 結果テキスト取込処理
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRMEIIMGFrontFileName(int GymID, string FrontFileName, Dictionary<string, int> UpdateData, int Schemabankcd)
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
                " TRMEIIMG.GYM_ID = " + GymID + " " +
                " AND EXISTS ( " +
                "  SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " SUB " +
                "  WHERE " +
                "       SUB.GYM_ID  = TRMEIIMG.GYM_ID " +
                "   AND SUB.OPERATION_DATE = TRMEIIMG.OPERATION_DATE " +
                "   AND SUB.SCAN_TERM = TRMEIIMG.SCAN_TERM " +
                "   AND SUB.BAT_ID = TRMEIIMG.BAT_ID " +
                "   AND SUB.DETAILS_NO = TRMEIIMG.DETAILS_NO " +
                "   AND SUB.IMG_FLNM = '" + FrontFileName + "' " +
                " ) ";
            return strSql;
        }

        /// <summary>
        /// OCR_DATAから対象イメージファイル名のデータを削除するDELETE文を作成します
        /// 結果テキスト取込処理(持帰訂正)
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteOCRDATAFileName(int GymID, List<string> DeleteFiles)
        {
            string DEL = string.Empty;
            foreach (string file in DeleteFiles)
            {
                if (!string.IsNullOrEmpty(DEL)) DEL += " OR ";
                DEL += TBL_OCR_DATA.IMG_NAME + " = '" + file + "' ";
            }
            if (!string.IsNullOrEmpty(DEL)) DEL = " AND (" + DEL + ") ";

            string strSql = "DELETE FROM " + TBL_OCR_DATA.TABLE_NAME +
                  " WHERE " + TBL_OCR_DATA.GYM_ID + "=" + GymID +
                  " " + DEL;
            return strSql;
        }

        /// <summary>
        /// IC_OCR_FINISHから対象イメージファイル名のデータを削除するDELETE文を作成します
        /// 結果テキスト取込処理(持帰訂正)
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteICOCRFINISHFileName(List<string> DeleteFiles)
        {
            string DEL = string.Empty;
            foreach (string file in DeleteFiles)
            {
                if (!string.IsNullOrEmpty(DEL)) DEL += " OR ";
                DEL += TBL_IC_OCR_FINISH.FRONT_IMG_NAME + " = '" + file + "' ";
            }
            if (!string.IsNullOrEmpty(DEL)) DEL = " (" + DEL + ") ";

            string strSql = "DELETE FROM " + TBL_IC_OCR_FINISH.TABLE_NAME +
                  " WHERE " + DEL;
            return strSql;
        }

        /// <summary>
        /// 証券明細テキスト取込処理
        /// SPASFA処理での削除対象を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetIcTxtDeleteDataSPASFA(string fileid, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_BILLMEITXT_CTL.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                        TBL_BILLMEITXT_CTL.FILE_ID + " = '" + fileid + "' AND " + 
                        TBL_BILLMEITXT_CTL.FILE_DIVID + " IN('SPA') ";
            return strSql;
        }

        /// <summary>
        /// 証券明細テキスト取込処理
        /// SPBSFB処理での削除対象を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetIcTxtDeleteDataSPBSFB(string fileid, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_BILLMEITXT_CTL.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                        TBL_BILLMEITXT_CTL.FILE_ID + " = '" + fileid + "' AND " +
                        TBL_BILLMEITXT_CTL.FILE_DIVID + " IN('SPB') ";
            return strSql;
        }

        /// <summary>
        /// 証券明細テキスト取込処理
        /// 当日持出明細で使用する対象証券の通知テキストを取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetIcTxtTsuchiTxt(string filename, int Schemabankcd)
        {
            string strSQL =
                " SELECT " +
                "    TXT.* " +
                "  , CTL.FILE_ID " +
                "  , CTL.FILE_DIVID " +
                "  , CTL.MAKE_DATE " +
                "  , CTL.RECV_DATE " +
                "  , CTL.RECV_TIME " +
                " FROM " + TBL_TSUCHITXT.TABLE_NAME(Schemabankcd) + " TXT " +
                "          INNER JOIN " +
                "      " + TBL_TSUCHITXT_CTL.TABLE_NAME(Schemabankcd) + " CTL " +
                "          ON TXT.FILE_NAME = CTL.FILE_NAME " +
                "         AND CTL.FILE_ID = '" + FileParam.FileId.IF208 + "' " +
                "         AND CTL.FILE_DIVID IN('" + FileParam.FileKbn.BUA + "', '" + FileParam.FileKbn.GRA + "') " +
                " WHERE " +
                "     TXT.IMG_NAME = '" + filename + "' ";
            return strSQL;
        }

        /// <summary>
        /// 交換尻取込処理
        /// 削除対象を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetBalanceTxtDeleteData(string fileid, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_BALANCETXT_CTL.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                        TBL_BALANCETXT_CTL.FILE_ID + " = '" + fileid + "' AND " +
                        TBL_BALANCETXT_CTL.FILE_DIVID + " IN('SPA') ";
            return strSql;
        }

        /// <summary>
        /// 結果テキスト取込処理
        /// 持帰訂正確定値を更新するUPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRItemTeiseiData(int GymID, string FileName, List<int> UpdItemID, bool SetTeiseiNull, string TRIGGER, int Schemabankcd)
        {
            string strSql = "UPDATE " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM " +
                " SET " +
                "  TRITEM." + TBL_TRITEM.ICTEISEI_DATA + " = " + (SetTeiseiNull ? "NULL" : "TRITEM." + TBL_TRITEM.END_DATA) + ", " +
                "  TRITEM.FIX_TRIGGER" + " = '" + TRIGGER + "' " +
                " WHERE " +
                " TRITEM.GYM_ID = " + GymID + " " +
                " AND TRITEM.ITEM_ID IN(" + string.Join(",", UpdItemID) + ")" +
                " AND EXISTS ( " +
                "  SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                "  WHERE " +
                "       TRMEIIMG.GYM_ID  = TRITEM.GYM_ID " +
                "   AND TRMEIIMG.OPERATION_DATE = TRITEM.OPERATION_DATE " +
                "   AND TRMEIIMG.SCAN_TERM = TRITEM.SCAN_TERM " +
                "   AND TRMEIIMG.BAT_ID = TRITEM.BAT_ID " +
                "   AND TRMEIIMG.DETAILS_NO = TRITEM.DETAILS_NO " +
                "   AND TRMEIIMG.IMG_FLNM = '" + FileName + "' " +
                " ) ";
            return strSql;
        }

        /// <summary>
        /// 明細データを削除するUPDATE文を作成します
        /// 結果テキスト取込
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRMEIDelete(int gymid, int operationdate, string scanterm, int batid, int detailsno, int opedate, int Schemabankcd)
        {
            return SQLSearch.GetUpdateTRMEIDelete(gymid, operationdate, scanterm, batid, detailsno, opedate, Schemabankcd);
        }

        /// <summary>
        /// 明細イメージデータを明細単位で削除するUPDATE文を作成します
        /// 結果テキスト取込
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRMEIIMGDetailDelete(int gymid, int operationdate, string scanterm, int batid, int detailsno, int opedate, int Schemabankcd)
        {
            return SQLSearch.GetUpdateTRMEIIMGDetailDelete(gymid, operationdate, scanterm, batid, detailsno, opedate, Schemabankcd);
        }

        /// <summary>
        /// 結果テキスト取込処理
        /// BUBでの項目を更新するUPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRItemBUB(int GymID, string FileName, int ItemID, string UpdateData, string TRIGGER, int Schemabankcd)
        {
            string strSql = "UPDATE " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM " +
                " SET " +
                "  TRITEM.BUA_DATA" + " = TRITEM.END_DATA, " +
                "  TRITEM.CTR_DATA" + " = '" + UpdateData + "', " +
                "  TRITEM.END_DATA" + " = '" + UpdateData + "', " +
                "  TRITEM.FIX_TRIGGER" + " = '" + TRIGGER + "' " +
                " WHERE " +
                "     TRITEM.GYM_ID = " + GymID + " " +
                " AND TRITEM.ITEM_ID =  " + ItemID + " " + 
                " AND EXISTS ( " +
                "  SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                "  WHERE " +
                "       TRMEIIMG.GYM_ID  = TRITEM.GYM_ID " +
                "   AND TRMEIIMG.OPERATION_DATE = TRITEM.OPERATION_DATE " +
                "   AND TRMEIIMG.SCAN_TERM = TRITEM.SCAN_TERM " +
                "   AND TRMEIIMG.BAT_ID = TRITEM.BAT_ID " +
                "   AND TRMEIIMG.DETAILS_NO = TRITEM.DETAILS_NO " +
                "   AND TRMEIIMG.IMG_FLNM = '" + FileName + "' " +
                "   AND TRMEIIMG.BUA_STS = 20 " +
                "   AND TRMEIIMG.IMG_KBN = 1 " +
                " ) ";
            return strSql;
        }

        /// <summary>
        /// 通知テキスト取込処理
        /// TRITEMの項目を更新するUPDATE文を作成します(訂正)
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRItemNoticeTeisei(int GymID, int ItemID, string fileName, string UpdateData, string TRIGGER, int Schemabankcd)
        {
            string strSql = "UPDATE " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM " +
                " SET " +
                "  TRITEM.ICTEISEI_DATA = '" + UpdateData + "', " +
                "  TRITEM.END_DATA" + " = '" + UpdateData + "', " +
                "  TRITEM.FIX_TRIGGER" + " = '" + TRIGGER + "' " +
                " WHERE " +
                "     TRITEM.GYM_ID = " + GymID + " " +
                " AND TRITEM.ITEM_ID =  " + ItemID + " " +
                " AND EXISTS ( " +
                "  SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                "  WHERE " +
                "       TRMEIIMG.GYM_ID  = TRITEM.GYM_ID " +
                "   AND TRMEIIMG.OPERATION_DATE = TRITEM.OPERATION_DATE " +
                "   AND TRMEIIMG.SCAN_TERM = TRITEM.SCAN_TERM " +
                "   AND TRMEIIMG.BAT_ID = TRITEM.BAT_ID " +
                "   AND TRMEIIMG.DETAILS_NO = TRITEM.DETAILS_NO " +
                "   AND TRMEIIMG.IMG_FLNM = '" + fileName + "' " +
                " ) ";
            return strSql;
        }

        /// <summary>
        /// 通知テキスト取込処理
        /// TRITEMの項目を更新するUPDATE文を作成します(訂正取消)
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRItemNoticeCancel(int GymID, int ItemID, string fileName, string TRIGGER, int Schemabankcd)
        {
            string strSql = "UPDATE " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM " +
                " SET " +
                "  TRITEM.END_DATA = TRITEM.CTR_DATA, " +
                "  TRITEM.FIX_TRIGGER" + " = '" + TRIGGER + "' " +
                " WHERE " +
                "     TRITEM.GYM_ID = " + GymID + " " +
                " AND TRITEM.ITEM_ID =  " + ItemID + " " +
                " AND EXISTS ( " +
                "  SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                "  WHERE " +
                "       TRMEIIMG.GYM_ID  = TRITEM.GYM_ID " +
                "   AND TRMEIIMG.OPERATION_DATE = TRITEM.OPERATION_DATE " +
                "   AND TRMEIIMG.SCAN_TERM = TRITEM.SCAN_TERM " +
                "   AND TRMEIIMG.BAT_ID = TRITEM.BAT_ID " +
                "   AND TRMEIIMG.DETAILS_NO = TRITEM.DETAILS_NO " +
                "   AND TRMEIIMG.IMG_FLNM = '" + fileName + "' " +
                " ) ";
            return strSql;
        }

        /// <summary>
        /// 通知テキスト取込処理
        /// TRITEMの項目を更新するUPDATE文を作成します(読替)
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRItemNoticeYomikae(int GymID, int ItemID, string fileName, string UpdateData, string TRIGGER, int Schemabankcd)
        {
            string strSql = "UPDATE " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM " +
                " SET " +
                "  TRITEM.MRC_CHG_BEFDATA = TRITEM.END_DATA, " +
                "  TRITEM.END_DATA" + " = '" + UpdateData + "', " +
                "  TRITEM.FIX_TRIGGER" + " = '" + TRIGGER + "' " +
                " WHERE " +
                "     TRITEM.GYM_ID = " + GymID + " " +
                " AND TRITEM.ITEM_ID =  " + ItemID + " " +
                " AND EXISTS ( " +
                "  SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                "  WHERE " +
                "       TRMEIIMG.GYM_ID  = TRITEM.GYM_ID " +
                "   AND TRMEIIMG.OPERATION_DATE = TRITEM.OPERATION_DATE " +
                "   AND TRMEIIMG.SCAN_TERM = TRITEM.SCAN_TERM " +
                "   AND TRMEIIMG.BAT_ID = TRITEM.BAT_ID " +
                "   AND TRMEIIMG.DETAILS_NO = TRITEM.DETAILS_NO " +
                "   AND TRMEIIMG.IMG_FLNM = '" + fileName + "' " +
                " ) ";
            return strSql;
        }

        /// <summary>
        /// 通知テキスト取込処理
        /// TRMEIの項目を更新するUPDATE文を作成します(訂正)
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRMeiNoticeTeisei(int GymID, string fileName, int UpdateData, int Schemabankcd)
        {
            string strSql = "UPDATE " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                " SET " +
                "  TRMEI.IC_OLD_OC_BK_NO = TRMEI.IC_OC_BK_NO, " +
                "  TRMEI.IC_OC_BK_NO" + " = " + UpdateData + " " +
                " WHERE " +
                "     TRMEI.GYM_ID = " + GymID + " " +
                " AND EXISTS ( " +
                "  SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                "  WHERE " +
                "       TRMEIIMG.GYM_ID  = TRMEI.GYM_ID " +
                "   AND TRMEIIMG.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "   AND TRMEIIMG.SCAN_TERM = TRMEI.SCAN_TERM " +
                "   AND TRMEIIMG.BAT_ID = TRMEI.BAT_ID " +
                "   AND TRMEIIMG.DETAILS_NO = TRMEI.DETAILS_NO " +
                "   AND TRMEIIMG.IMG_FLNM = '" + fileName + "' " +
                " ) ";
            return strSql;
        }

        /// <summary>
        /// 持帰ダウンロード確定
        /// 確定対象データを取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetInclearingConfirmData(int GymID, int Schemabankcd)
        {
            string strSql =
                "SELECT " +
                "  TXT.*, " +
                "  CTL.RET_REQ_TXT_NAME, " +
                "  CTL.CAP_KBN, " +
                "  CTL.CAP_STS, " +
                "  CTL.IMG_ARCH_CAP_STS, " +
                "  CASE " +
                "   WHEN CTL.RET_REQ_TXT_NAME IS NULL THEN 0 " +
                "   ELSE 1 " +
                "  END CTLFLG, " +
                "  NVL(MEI.DELETE_FLG, -1) MEIFLG, " +
                "  NVL(IMG.DELETE_FLG, -1) IMGFLG, " +
                "  NVL(MEI.BCA_DATE, -1) BCA_DATE, " +
                "  NVL(MEI.GMA_STS, -1) GMA_STS, " +
                "  BKCD.END_DATA BKNO " +
                " FROM " + TBL_ICREQRET_BILLMEITXT.TABLE_NAME(Schemabankcd) + " TXT " +
                "  LEFT JOIN " + TBL_ICREQRET_CTL.TABLE_NAME(Schemabankcd) + " CTL " +
                "   ON TXT.MEI_TXT_NAME = CTL.MEI_TXT_NAME " +
                "  LEFT JOIN " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " IMG " +
                "   ON TXT.IMG_NAME = IMG.IMG_FLNM " +
                "  AND IMG.GYM_ID = " + GymID + " " +
                //"  AND IMG.DELETE_FLG = 0 " +
                "  LEFT JOIN " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " IMG_F " +
                "   ON TXT.FRONT_IMG_NAME = IMG_F.IMG_FLNM " +
                "  AND IMG_F.GYM_ID = " + GymID + " " +
                "  LEFT JOIN " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " MEI " +
                "   ON MEI.GYM_ID  = IMG_F.GYM_ID " +
                "  AND MEI.OPERATION_DATE = IMG_F.OPERATION_DATE " +
                "  AND MEI.SCAN_TERM = IMG_F.SCAN_TERM " +
                "  AND MEI.BAT_ID = IMG_F.BAT_ID " +
                "  AND MEI.DETAILS_NO = IMG_F.DETAILS_NO " +
                "  LEFT JOIN " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " BKCD " +
                "   ON MEI.GYM_ID = BKCD.GYM_ID " +
                "  AND MEI.OPERATION_DATE = BKCD.OPERATION_DATE " +
                "  AND MEI.SCAN_TERM = BKCD.SCAN_TERM " +
                "  AND MEI.BAT_ID = BKCD.BAT_ID " +
                "  AND MEI.DETAILS_NO = BKCD.DETAILS_NO " +
                "  AND BKCD.ITEM_ID = " + DspItem.ItemId.持帰銀行コード + " " +
                " WHERE " +
                "   TXT.KAKUTEI_FLG = 0 " +
                " AND ( (   CTL.RET_REQ_TXT_NAME IS NOT NULL " +
                "       AND CTL.CAP_STS = 5 " +
                "       AND CTL.IMG_ARCH_CAP_STS IN (-1, 5) " +
                "       ) " +
                "     OR CTL.RET_REQ_TXT_NAME IS NULL ) " +
                " ORDER BY TXT.MEI_TXT_NAME, TXT.FRONT_IMG_NAME, TXT.IMG_KBN";
            return strSql;
        }

        /// <summary>
        /// 持帰ダウンロード確定
        /// 未取込を条件とするSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetInclearingNotImport(int Schemabankcd)
        {
            string strSQL = "SELECT COUNT(*) CNT FROM " + TBL_ICREQRET_CTL.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_ICREQRET_CTL.CAP_STS + " = 0 OR " +
                TBL_ICREQRET_CTL.IMG_ARCH_CAP_STS + " = 0 ";
            return strSQL;
        }

        /// <summary>
        /// 持帰ダウンロード確定
        /// ファイル名から対象明細データを取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetInclearingDetailImgFile(int GymID, int Batid, string Imgflnm, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                " WHERE " +
                "     TRMEI.GYM_ID = " + GymID + " " +
                " AND TRMEI.BAT_ID = " + Batid + " " +
                " AND EXISTS ( " +
                "      SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                "      WHERE " +
                "           TRMEIIMG.GYM_ID  = TRMEI.GYM_ID " +
                "       AND TRMEIIMG.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "       AND TRMEIIMG.SCAN_TERM = TRMEI.SCAN_TERM " +
                "       AND TRMEIIMG.BAT_ID = TRMEI.BAT_ID " +
                "       AND TRMEIIMG.DETAILS_NO = TRMEI.DETAILS_NO " +
                "       AND TRMEIIMG.IMG_FLNM = '" + Imgflnm + "' " +
                "     ) ";
            return strSql;
        }

        /// <summary>
        /// 持帰ダウンロード確定
        /// 登録済の明細番号最大値を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetInclearingMaxDetailNo(int GymID, int opeDate, int BatID, int Schemabankcd)
        {
            string strSQL = "SELECT MAX(" + TBL_TRMEI.DETAILS_NO + ") " + TBL_TRMEI.DETAILS_NO + " FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_TRMEI.GYM_ID + " = " + GymID + " AND " +
                TBL_TRMEI.BAT_ID + " = " + BatID + " AND " +
                TBL_TRMEI.OPERATION_DATE + " = " + opeDate + " ";
            return strSQL;
        }

        /// <summary>
        /// 持帰ダウンロード確定
        /// 持出のOCRキー情報を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetOutclearingOCRInfo(int GymID, string filename, int Schemabankcd)
        {
            string strSQL = "SELECT BAT.INPUT_ROUTE, BATIMG.SCAN_BATCH_FOLDER_NAME, MEIIMG.IMG_FLNM_OLD, MEI.DSP_ID " +
                " FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " MEIIMG " +
                           " INNER JOIN " +
                           TBL_TRMEI.TABLE_NAME(Schemabankcd) + " MEI " +
                           " ON MEIIMG.GYM_ID = MEI.GYM_ID " +
                           "AND MEIIMG.OPERATION_DATE = MEI.OPERATION_DATE " +
                           "AND MEIIMG.SCAN_TERM = MEI.SCAN_TERM " +
                           "AND MEIIMG.BAT_ID = MEI.BAT_ID " +
                           "AND MEIIMG.DETAILS_NO = MEI.DETAILS_NO " +
                           " INNER JOIN " +
                           TBL_TRBATCH.TABLE_NAME(Schemabankcd) + " BAT " +
                           " ON MEIIMG.GYM_ID = BAT.GYM_ID " +
                           "AND MEIIMG.OPERATION_DATE = BAT.OPERATION_DATE " +
                           "AND MEIIMG.SCAN_TERM = BAT.SCAN_TERM " +
                           "AND MEIIMG.BAT_ID = BAT.BAT_ID " +
                           " INNER JOIN " +
                           TBL_TRBATCHIMG.TABLE_NAME(Schemabankcd) + " BATIMG " +
                           " ON MEIIMG.GYM_ID = BATIMG.GYM_ID " +
                           "AND MEIIMG.OPERATION_DATE = BATIMG.OPERATION_DATE " +
                           "AND MEIIMG.SCAN_TERM = BATIMG.SCAN_TERM " +
                           "AND MEIIMG.BAT_ID = BATIMG.BAT_ID " +
                           "AND BATIMG.IMG_KBN = 1 " +
                " WHERE " +
                " MEIIMG.GYM_ID = " + GymID + " AND " +
                " MEIIMG.IMG_FLNM = '" + filename + "' ";
            return strSQL;
        }

        /// <summary>
        /// 持帰ダウンロード確定
        /// 持帰要求結果証券明細テキストの確定フラグを更新するUPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateBillMeiTxtKakuteiFlg(int opedate, string opeid, string meitxt, string capkbn, List<string> imglist, int Schemabankcd)
        {
            string strSQL = "UPDATE " + TBL_ICREQRET_BILLMEITXT.TABLE_NAME(Schemabankcd) +
                " SET " +
                TBL_ICREQRET_BILLMEITXT.KAKUTEI_FLG + " = 1, " +
                TBL_ICREQRET_BILLMEITXT.KAKUTEI_DATE + " = " + opedate + ", " +
                TBL_ICREQRET_BILLMEITXT.KAKUTEI_TIME + " = " + int.Parse(DateTime.Now.ToString("HHmmss")) + ", " +
                TBL_ICREQRET_BILLMEITXT.KAKUTEI_OPE + " = '" + opeid + "' " +
                " WHERE " +
                TBL_ICREQRET_BILLMEITXT.MEI_TXT_NAME + " = '" + meitxt + "' AND " +
                TBL_ICREQRET_BILLMEITXT.CAP_KBN + " = " + capkbn + " AND " +
                TBL_ICREQRET_BILLMEITXT.IMG_NAME + " IN(" + "'" + string.Join("','", imglist) + "'" + ") ";
            return strSQL;
        }

        /// <summary>
        /// 持帰ダウンロード確定
        /// 持帰要求結果証券明細テキストの未確定データ件数を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetBillMeiTxtNotKautei(string meitxt, string capkbn, int Schemabankcd)
        {
            string strSQL = "SELECT COUNT(*) CNT FROM " + TBL_ICREQRET_BILLMEITXT.TABLE_NAME(Schemabankcd) +
                " WHERE " +
                TBL_ICREQRET_BILLMEITXT.MEI_TXT_NAME + " = '" + meitxt + "' AND " +
                TBL_ICREQRET_BILLMEITXT.CAP_KBN + " = " + capkbn + " AND " +
                TBL_ICREQRET_BILLMEITXT.KAKUTEI_FLG + " = 0 ";
            return strSQL;
        }

        /// <summary>
        /// 持帰ダウンロード確定
        /// 持帰要求結果証券明細テキストの確定フラグを更新するUPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateReqRetCtlSTS(string reqrettxtname, string meitxt, string capkbn, int Schemabankcd)
        {
            string strSQL = "UPDATE " + TBL_ICREQRET_CTL.TABLE_NAME(Schemabankcd) +
                " SET " +
                TBL_ICREQRET_CTL.CAP_STS + " = 10, " +
                TBL_ICREQRET_CTL.IMG_ARCH_CAP_STS + " = 10 " +
                " WHERE " +
                TBL_ICREQRET_CTL.RET_REQ_TXT_NAME + " = '" + reqrettxtname + "' AND " +
                TBL_ICREQRET_CTL.MEI_TXT_NAME + " = '" + meitxt + "' AND " +
                TBL_ICREQRET_CTL.CAP_KBN + " = " + capkbn + " ";
            return strSQL;
        }

        /// <summary>
        /// 持帰ダウンロード確定
        /// DSPIDを更新するUPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTrMeiDspId(int GymID, int OpeDate, string ScanTerm, int BatID, int Details, int DspID, int Schemabankcd)
        {
            return SQLEntry.GetEntryDspIdUpdate(GymID, OpeDate, ScanTerm, BatID, Details, DspID, Schemabankcd);
        }

        /// <summary>
        /// 持帰ダウンロード確定
        /// 持帰で使用する対象証券の通知テキストを取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetInclearingTsuchiTxt(string filename, int Schemabankcd)
        {
            string strSQL =
                " SELECT " +
                "    TXT.* " +
                "  , CTL.FILE_ID " +
                "  , CTL.FILE_DIVID " +
                "  , CTL.MAKE_DATE " +
                "  , CTL.RECV_DATE " +
                "  , CTL.RECV_TIME " +
                " FROM " + TBL_TSUCHITXT.TABLE_NAME(Schemabankcd) + " TXT " +
                "          INNER JOIN " +
                "      " + TBL_TSUCHITXT_CTL.TABLE_NAME(Schemabankcd) + " CTL " +
                "          ON TXT.FILE_NAME = CTL.FILE_NAME " +
                "         AND CTL.FILE_ID = '" + FileParam.FileId.IF208 + "' " +
                "         AND CTL.FILE_DIVID IN('" + FileParam.FileKbn.BUB + "', '" + FileParam.FileKbn.BCA + "', '" + FileParam.FileKbn.MRB + "') " +
                " WHERE " +
                "     TXT.IMG_NAME = '" + filename + "' ";
            return strSQL;
        }

        /// <summary>
        /// 持帰ダウンロード確定
        /// 持出の明細情報を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetOutclearingMeiData(int GymID, string filename, int Schemabankcd)
        {
            string strSQL = 
                " SELECT " +
                "    TRMEI.* " +
                " FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                " WHERE " +
                "     TRMEI.GYM_ID = " + GymID + " " +
                " AND EXISTS ( " +
                "      SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                "      WHERE " +
                "           TRMEIIMG.GYM_ID  = TRMEI.GYM_ID " +
                "       AND TRMEIIMG.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "       AND TRMEIIMG.SCAN_TERM = TRMEI.SCAN_TERM " +
                "       AND TRMEIIMG.BAT_ID = TRMEI.BAT_ID " +
                "       AND TRMEIIMG.DETAILS_NO = TRMEI.DETAILS_NO " +
                "       AND TRMEIIMG.IMG_FLNM = '" + filename + "' " +
                "     ) ";
            return strSQL;
        }

        /// <summary>
        /// 持帰ダウンロード確定
        /// 対象証券単位でのTRIMEIIMGの項目を一括更新するUPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateDetailTRMEIIMG(int gymid, int operationdate, string scanterm, int batid, int detailsno, Dictionary<string, int> UpdateData, int Schemabankcd)
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
                TBL_TRMEIIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEIIMG.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEIIMG.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEIIMG.DETAILS_NO + "=" + detailsno + " ";
            return strSql;
        }

        /// <summary>
        /// 持帰ダウンロード確定
        /// 明細データの項目を更新するUPDATE文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRMEI(int gymid, int operationdate, string scanterm, int batid, int detailsno, Dictionary<string, int> UpdateData, int Schemabankcd)
        {
            return SQLSearch.GetUpdateTRMEI(gymid, operationdate, scanterm, batid, detailsno, UpdateData, Schemabankcd);
        }

    }
}
