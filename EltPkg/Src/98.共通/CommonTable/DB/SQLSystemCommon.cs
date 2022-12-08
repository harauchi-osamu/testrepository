using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTable.DB
{
    public class SQLSystemCommon
    {

        /// <summary>
        /// 進捗状況でのイメージ取込状況を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetProcViewImgImportBatCtl(int OpeDate)
        {
            //2022.11.08 SP.harauchi No.152 表示速度改善対応
            string strSql = "";
            strSql += " SELECT COUNT(*) AS 未処理件数 " +
                      " FROM " + TBL_SCAN_BATCH_CTL.TABLE_NAME + 
                      " WHERE STATUS NOT IN ( " + Convert.ToInt32(TBL_SCAN_BATCH_CTL.enumStatus.Complete) + " ," +
                      Convert.ToInt32(TBL_SCAN_BATCH_CTL.enumStatus.Delete) + ")" +
                      " AND (CLEARING_DATE >= " + OpeDate + " OR CLEARING_DATE = 0) "
                      ;
            return strSql;
        }

        /// <summary>
        /// 進捗状況でのイメージ取込状況を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetProcViewImgImportBat(int Schemabankcd, int OpeDate, int AfterDate)
        {
            //2022.11.08 SP.harauchi No.152 表示速度改善対応
            string strSql = "";
            strSql += "SELECT " +
                      " NVL(SUM(CASE WHEN TRBATCH.CLEARING_DATE = " + OpeDate + " THEN 1 ELSE 0 END),0) AS Today_Batch " +
                      " , NVL(SUM(CASE WHEN TRBATCH.CLEARING_DATE = " + AfterDate + " THEN 1 ELSE 0 END),0) AS Nextday_Batch " +
                      " , NVL(SUM(CASE WHEN TRBATCH.CLEARING_DATE > " + AfterDate + " THEN 1 ELSE 0 END),0) AS Otherday_Batch " +
                      "FROM " + TBL_TRBATCH.TABLE_NAME(Schemabankcd) + " TRBATCH " +
                      "WHERE " +
                      " TRBATCH.GYM_ID = " + GymParam.GymId.持出 +
                      " AND TRBATCH.DELETE_FLG = 0 " +
                      " AND EXISTS ( " +
                      " SELECT 1 FROM " + TBL_TRBATCHIMG.TABLE_NAME(Schemabankcd) + " TRBATCHIMG " +
                      " WHERE " +
                      " TRBATCHIMG.GYM_ID = TRBATCH.GYM_ID " +
                      " AND TRBATCHIMG.OPERATION_DATE = TRBATCH.OPERATION_DATE " +
                      " AND TRBATCHIMG.SCAN_TERM = TRBATCH.SCAN_TERM " +
                      " AND TRBATCHIMG.BAT_ID = TRBATCH.BAT_ID " +
                      " AND TRBATCHIMG.SCAN_BATCH_FOLDER_NAME IN " +
                      " ( " +
                      " SELECT BATCH_FOLDER_NAME " +
                      " FROM DBCTR.SCAN_BATCH_CTL " +
                      " WHERE STATUS <> " + Convert.ToInt32(TBL_SCAN_BATCH_CTL.enumStatus.Delete) +
                      " AND (CLEARING_DATE >= " + OpeDate + " OR CLEARING_DATE = 0) " +
                      " ) " +
                      " ) "
                      ;

            return strSql;
        }

        /// <summary>
        /// 進捗状況でのイメージ取込状況を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetProcViewImgImportMei(int Schemabankcd, int OpeDate, int AfterDate)
        {
            //2022.11.08 SP.harauchi No.152 表示速度改善対応
            string strSql = "";
            strSql += "SELECT " +
                      "     NVL(SUM(CASE WHEN TRITEM_ID5.END_DATA = " + OpeDate + " THEN 1 ELSE 0 END),0) AS Today_Mei " +
                      "   , NVL(SUM(CASE WHEN TRITEM_ID5.END_DATA = " + AfterDate + " THEN 1 ELSE 0 END),0) AS Nextday_Mei " +
                      "   , NVL(SUM(CASE WHEN TRITEM_ID5.END_DATA > " + AfterDate + " THEN 1 ELSE 0 END),0) AS Otherday_Mei " +
                      " FROM " + TBL_TRBATCH.TABLE_NAME(Schemabankcd) + " TRBATCH " +
                      " INNER JOIN " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                      "      ON TRMEI.GYM_ID = TRBATCH.GYM_ID " +
                      "     AND TRMEI.OPERATION_DATE = TRBATCH.OPERATION_DATE " +
                      "     AND TRMEI.SCAN_TERM = TRBATCH.SCAN_TERM " +
                      "     AND TRMEI.BAT_ID = TRBATCH.BAT_ID " +
                      "     AND TRMEI.DELETE_FLG = 0 " +
                      " LEFT JOIN " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                      "      ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                      "     AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "     AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "     AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                      "     AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "     AND TRITEM_ID5.ITEM_ID = " + DspItem.ItemId.交換日 +
                      " WHERE " +
                      "     TRBATCH.GYM_ID = " + GymParam.GymId.持出 +
                      " AND TRBATCH.DELETE_FLG = 0 " +
                      " AND EXISTS ( " +
                      "            SELECT 1 " +
                      "            FROM " + TBL_TRBATCHIMG.TABLE_NAME(Schemabankcd) + " TRBATCHIMG " +
                      "            WHERE " +
                      "                 TRBATCHIMG.GYM_ID = TRBATCH.GYM_ID " +
                      "            AND TRBATCHIMG.OPERATION_DATE = TRBATCH.OPERATION_DATE " +
                      "            AND TRBATCHIMG.SCAN_TERM = TRBATCH.SCAN_TERM " +
                      "            AND TRBATCHIMG.BAT_ID = TRBATCH.BAT_ID " +
                      "            AND TRBATCHIMG.SCAN_BATCH_FOLDER_NAME IN " +
                      "                ( " +
                      "                 SELECT BATCH_FOLDER_NAME " +
                      "                 FROM DBCTR.SCAN_BATCH_CTL " +
                      "                WHERE STATUS <> " + Convert.ToInt32(TBL_SCAN_BATCH_CTL.enumStatus.Delete) +
                      "                AND (CLEARING_DATE >= " + OpeDate + " OR CLEARING_DATE = 0) " +
                      "                ) " +
                      " ) "
                      ;

            return strSql;
        }

        /// <summary>
        /// 進捗状況での持出入力状況を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetProcViewOCInputData(int Schemabankcd)
        {
            string strSql = "";
            strSql += " SELECT " +
                      "    HOSEI_STATUS.GYM_ID " +
                      "  , HOSEI_STATUS.HOSEI_INPTMODE " +
                      "  , HOSEI_STATUS.INPT_STS  " +
                      "  , TRITEM_ID5.END_DATA CLEARING_DATE  " +
                      "  , COUNT(*) CNT  " +
                      " FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEI_STATUS " +
                      "     INNER JOIN " +
                      "     " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                      "      ON TRMEI.GYM_ID = HOSEI_STATUS.GYM_ID " +
                      "     AND TRMEI.OPERATION_DATE = HOSEI_STATUS.OPERATION_DATE " +
                      "     AND TRMEI.SCAN_TERM = HOSEI_STATUS.SCAN_TERM " +
                      "     AND TRMEI.BAT_ID = HOSEI_STATUS.BAT_ID " +
                      "     AND TRMEI.DETAILS_NO = HOSEI_STATUS.DETAILS_NO " +
                      "     AND TRMEI.DELETE_FLG = 0 " +
                      "     LEFT JOIN " +
                      "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                      "      ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                      "     AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "     AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "     AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                      "     AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "     AND TRITEM_ID5.ITEM_ID = 5 " +
                      " WHERE HOSEI_STATUS.GYM_ID = 1 " +
                      " GROUP BY HOSEI_STATUS.GYM_ID " +
                      "        , HOSEI_STATUS.HOSEI_INPTMODE " +
                      "        , HOSEI_STATUS.INPT_STS " +
                      "        , TRITEM_ID5.END_DATA ";

            return strSql;
        }

        /// <summary>
        /// 進捗状況での持出アップロード状況を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetProcViewOCUpLoadData(int Schemabankcd)
        {
            string strSql = "";
            strSql += " SELECT " +
                      "    TRMEI.GYM_ID " +
                      "  , TRITEM_ID5.END_DATA CLEARING_DATE " +
                      "  , TRMEIIMG.BUA_STS " +
                      //"  , HOSEI_STATUS.INPT_STS TEISEIINPTSTS " +
                      "  , COUNT(*) CNT " +
                      " FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                      "     INNER JOIN " +
                      "     " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                      "      ON TRMEI.GYM_ID = TRMEIIMG.GYM_ID " +
                      "     AND TRMEI.OPERATION_DATE = TRMEIIMG.OPERATION_DATE " +
                      "     AND TRMEI.SCAN_TERM = TRMEIIMG.SCAN_TERM " +
                      "     AND TRMEI.BAT_ID = TRMEIIMG.BAT_ID " +
                      "     AND TRMEI.DETAILS_NO = TRMEIIMG.DETAILS_NO " +
                      "     AND TRMEI.DELETE_FLG = 0 " +
                      "     LEFT JOIN " +
                      "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                      "      ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                      "     AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "     AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "     AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                      "     AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "     AND TRITEM_ID5.ITEM_ID = 5 " +
                      //"     LEFT JOIN " +
                      //"     " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEI_STATUS " +
                      //"      ON HOSEI_STATUS.GYM_ID = TRMEI.GYM_ID " +
                      //"     AND HOSEI_STATUS.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      //"     AND HOSEI_STATUS.SCAN_TERM = TRMEI.SCAN_TERM " +
                      //"     AND HOSEI_STATUS.BAT_ID = TRMEI.BAT_ID " +
                      //"     AND HOSEI_STATUS.DETAILS_NO = TRMEI.DETAILS_NO " +
                      //"     AND HOSEI_STATUS.HOSEI_INPTMODE = 5 " +
                      " WHERE TRMEIIMG.GYM_ID = 1 " +
                      "   AND TRMEIIMG.DELETE_FLG = 0 " +
                      "   AND EXISTS (  " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEI_STATUS " +
                      "        WHERE HOSEI_STATUS.GYM_ID = TRMEI.GYM_ID " +
                      "          AND HOSEI_STATUS.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "          AND HOSEI_STATUS.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "          AND HOSEI_STATUS.BAT_ID = TRMEI.BAT_ID " +
                      "          AND HOSEI_STATUS.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "          AND HOSEI_STATUS.HOSEI_INPTMODE = 1 " +
                      "          AND HOSEI_STATUS.INPT_STS = 3000 " +
                      "       ) " +
                      "   AND EXISTS (  " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEI_STATUS " +
                      "        WHERE HOSEI_STATUS.GYM_ID = TRMEI.GYM_ID " +
                      "          AND HOSEI_STATUS.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "          AND HOSEI_STATUS.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "          AND HOSEI_STATUS.BAT_ID = TRMEI.BAT_ID " +
                      "          AND HOSEI_STATUS.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "          AND HOSEI_STATUS.HOSEI_INPTMODE = 2 " +
                      "          AND HOSEI_STATUS.INPT_STS = 3000 " +
                      "       ) " +
                      "   AND EXISTS (  " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEI_STATUS " +
                      "        WHERE HOSEI_STATUS.GYM_ID = TRMEI.GYM_ID " +
                      "          AND HOSEI_STATUS.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "          AND HOSEI_STATUS.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "          AND HOSEI_STATUS.BAT_ID = TRMEI.BAT_ID " +
                      "          AND HOSEI_STATUS.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "          AND HOSEI_STATUS.HOSEI_INPTMODE = 3 " +
                      "          AND HOSEI_STATUS.INPT_STS = 3000 " +
                      "       ) " +
                      " GROUP BY TRMEI.GYM_ID " +
                      "        , TRITEM_ID5.END_DATA " +
                      "        , TRMEIIMG.BUA_STS ";
                      //"        , HOSEI_STATUS.INPT_STS ";

            return strSql;
        }

        /// <summary>
        /// 進捗状況での持出取消アップロード状況を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetProcViewOCDeleteUpLoadData(int Schemabankcd)
        {
            string strSql = "";
            strSql += " SELECT " +
                      "    TRMEI.GYM_ID " +
                      "  , TRMEI.BCA_STS " +
                      "  , TRITEM_ID5.END_DATA CLEARING_DATE " +
                      "  , COUNT(*) CNT  " +
                      " FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                      "     LEFT JOIN " +
                      "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                      "      ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                      "     AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "     AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "     AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                      "     AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "     AND TRITEM_ID5.ITEM_ID = 5 " +
                      " WHERE TRMEI.GYM_ID = 1 " +
                      //"   AND TRMEI.DELETE_FLG = 1 " +
                      //"   AND NOT( TRMEI.GRA_DATE > 0 ) " + // 不渡返還通知での削除データは対象外
                      // 持出取消アップロード状態が「作成対象」以上 & 表面がアップロード済
                      "   AND TRMEI.BCA_STS >= " + TrMei.Sts.作成対象 + " " + 
                      "   AND EXISTS ( " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                      "        WHERE TRMEIIMG.GYM_ID = TRMEI.GYM_ID " +
                      "          AND TRMEIIMG.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "          AND TRMEIIMG.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "          AND TRMEIIMG.BAT_ID = TRMEI.BAT_ID " +
                      "          AND TRMEIIMG.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "          AND TRMEIIMG.BUA_STS = 20 " +
                      "       ) " +
                      " GROUP BY TRMEI.GYM_ID " +
                      "        , TRMEI.BCA_STS " +
                      "        , TRITEM_ID5.END_DATA ";

            return strSql;
        }

        /// <summary>
        /// 進捗状況での持帰ダウンロード状況を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetProcViewICDownLoadData(int Schemabankcd)
        {
            string strSqlWK = "";
            strSqlWK += " SELECT " +
                      "    CASE " +
                      "     WHEN NVL(ICREQRET_BILLMEITXT.TEISEI_CLEARING_DATE, 'ZZZZZZZZ') <> 'ZZZZZZZZ' THEN ICREQRET_BILLMEITXT.TEISEI_CLEARING_DATE " +
                      "     ELSE ICREQRET_BILLMEITXT.OC_CLEARING_DATE " +
                      "    END CLEARING_DATE " +
                      "  , ICREQRET_BILLMEITXT.IMG_NAME " +
                      "  , ICREQRET_BILLMEITXT.KAKUTEI_FLG " +
                      " FROM " + TBL_ICREQRET_BILLMEITXT.TABLE_NAME(Schemabankcd) + " ICREQRET_BILLMEITXT " +
                      " WHERE ICREQRET_BILLMEITXT.KAKUTEI_FLG = 0 " +
                      "   AND ICREQRET_BILLMEITXT.IMG_KBN = " + TrMeiImg.ImgKbn.表 + " " +

                      " UNION ALL " +

                      " SELECT DISTINCT" +
                      "    CASE " +
                      "     WHEN NVL(ICREQRET_BILLMEITXT.TEISEI_CLEARING_DATE, 'ZZZZZZZZ') <> 'ZZZZZZZZ' THEN ICREQRET_BILLMEITXT.TEISEI_CLEARING_DATE " +
                      "     ELSE ICREQRET_BILLMEITXT.OC_CLEARING_DATE " +
                      "    END CLEARING_DATE " +
                      "  , ICREQRET_BILLMEITXT.IMG_NAME " +
                      "  , ICREQRET_BILLMEITXT.KAKUTEI_FLG " +
                      " FROM " + TBL_ICREQRET_BILLMEITXT.TABLE_NAME(Schemabankcd) + " ICREQRET_BILLMEITXT " +
                      " WHERE ICREQRET_BILLMEITXT.KAKUTEI_FLG = 1 " +
                      "   AND ICREQRET_BILLMEITXT.IMG_KBN = " + TrMeiImg.ImgKbn.表 + " ";

            string strSql = "";
            strSql += " SELECT " +
                      "    WK.CLEARING_DATE " +
                      //"  , WK.IMG_NAME " +
                      "  , WK.KAKUTEI_FLG " +
                      "  , COUNT(*) CNT  " +
                      " FROM ( " + strSqlWK + " ) WK " +
                      " GROUP BY WK.CLEARING_DATE " +
                      //"        , WK.IMG_NAME " +
                      "        , WK.KAKUTEI_FLG ";

            return strSql;
        }

        /// <summary>
        /// 進捗状況での持帰入力状況を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetProcViewICInputData(int Schemabankcd)
        {
            string strSql = "";
            strSql += " SELECT " +
                      "    HOSEI_STATUS.GYM_ID " +
                      "  , HOSEI_STATUS.HOSEI_INPTMODE " +
                      "  , HOSEI_STATUS.INPT_STS  " +
                      "  , NVL(TRITEM_ID5.END_DATA, TRITEM_ID5.CTR_DATA) CLEARING_DATE  " +
                      "  , COUNT(*) CNT  " +
                      " FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEI_STATUS " +
                      "     INNER JOIN " +
                      "     " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                      "      ON TRMEI.GYM_ID = HOSEI_STATUS.GYM_ID " +
                      "     AND TRMEI.OPERATION_DATE = HOSEI_STATUS.OPERATION_DATE " +
                      "     AND TRMEI.SCAN_TERM = HOSEI_STATUS.SCAN_TERM " +
                      "     AND TRMEI.BAT_ID = HOSEI_STATUS.BAT_ID " +
                      "     AND TRMEI.DETAILS_NO = HOSEI_STATUS.DETAILS_NO " +
                      "     AND TRMEI.DELETE_FLG = 0 " +
                      "     LEFT JOIN " +
                      "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                      "      ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                      "     AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "     AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "     AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                      "     AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "     AND TRITEM_ID5.ITEM_ID = 5 " +
                      " WHERE HOSEI_STATUS.GYM_ID = 2 " +
                      " GROUP BY HOSEI_STATUS.GYM_ID " +
                      "        , HOSEI_STATUS.HOSEI_INPTMODE " +
                      "        , HOSEI_STATUS.INPT_STS " +
                      "        , NVL(TRITEM_ID5.END_DATA, TRITEM_ID5.CTR_DATA) ";
            return strSql;
        }

        /// <summary>
        /// 進捗状況での持帰不渡アップロード状況を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetProcViewICFuwatariData(int Schemabankcd)
        {
            string strSql = "";
            strSql += " SELECT " +
                      "    TRMEI.GYM_ID " +
                      "  , TRMEI.GRA_STS " +
                      "  , NVL(TRITEM_ID5.END_DATA, TRITEM_ID5.CTR_DATA) CLEARING_DATE  " +
                      "  , TRFUWATARI.E_YMD " +
                      "  , TRFUWATARI.DELETE_DATE " +
                      "  , TRFUWATARI.DELETE_FLG " +
                      "  , COUNT(*) CNT  " +
                      " FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                      "     LEFT JOIN " +
                      "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                      "      ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                      "     AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "     AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "     AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                      "     AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "     AND TRITEM_ID5.ITEM_ID = 5 " +
                      "     INNER JOIN " +
                      "     " + TBL_TRFUWATARI.TABLE_NAME(Schemabankcd) + " TRFUWATARI " +
                      "      ON TRFUWATARI.GYM_ID = TRMEI.GYM_ID " +
                      "     AND TRFUWATARI.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "     AND TRFUWATARI.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "     AND TRFUWATARI.BAT_ID = TRMEI.BAT_ID " +
                      "     AND TRFUWATARI.DETAILS_NO = TRMEI.DETAILS_NO " +
                      " WHERE TRMEI.GYM_ID = 2 " +
                      //"   AND TRMEI.DELETE_FLG = 0 " +
                      // 作成待以外
                      "   AND TRMEI.GRA_STS IN ( " + TrMei.Sts.ファイル作成 + "," + TrMei.Sts.アップロード + "," + TrMei.Sts.結果エラー + "," + TrMei.Sts.結果正常 + ") " +
                      //"   AND EXISTS ( " +
                      //"        SELECT 1 " +
                      //"        FROM " + TBL_TRFUWATARI.TABLE_NAME(Schemabankcd) + " TRFUWATARI " +
                      //"        WHERE TRFUWATARI.GYM_ID = TRMEI.GYM_ID " +
                      //"          AND TRFUWATARI.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      //"          AND TRFUWATARI.SCAN_TERM = TRMEI.SCAN_TERM " +
                      //"          AND TRFUWATARI.BAT_ID = TRMEI.BAT_ID " +
                      //"          AND TRFUWATARI.DETAILS_NO = TRMEI.DETAILS_NO " +
                      //"       ) " +
                      " GROUP BY TRMEI.GYM_ID " +
                      "        , TRMEI.GRA_STS " +
                      "        , NVL(TRITEM_ID5.END_DATA, TRITEM_ID5.CTR_DATA) " +
                      "        , TRFUWATARI.E_YMD " +
                      "        , TRFUWATARI.DELETE_DATE " +
                      "        , TRFUWATARI.DELETE_FLG " +
                      // ↑作成待以外すべて

                      " UNION ALL " +

                      // 作成待のみ
                      " SELECT " +
                      "    TRMEI.GYM_ID " +
                      "  , TRMEI.GRA_STS " +
                      "  , NVL(TRITEM_ID5.END_DATA, TRITEM_ID5.CTR_DATA) CLEARING_DATE  " +
                      "  , TRFUWATARI.E_YMD " +
                      "  , TRFUWATARI.DELETE_DATE " +
                      "  , TRFUWATARI.DELETE_FLG " +
                      "  , COUNT(*) CNT  " +
                      " FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                      "     LEFT JOIN " +
                      "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                      "      ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                      "     AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "     AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "     AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                      "     AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "     AND TRITEM_ID5.ITEM_ID = 5 " +
                      "     LEFT JOIN " +
                      "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID1 " +
                      "      ON TRITEM_ID1.GYM_ID = TRMEI.GYM_ID " +
                      "     AND TRITEM_ID1.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "     AND TRITEM_ID1.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "     AND TRITEM_ID1.BAT_ID = TRMEI.BAT_ID " +
                      "     AND TRITEM_ID1.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "     AND TRITEM_ID1.ITEM_ID = 1 " +
                      "     INNER JOIN " +
                      "     " + TBL_TRFUWATARI.TABLE_NAME(Schemabankcd) + " TRFUWATARI " +
                      "      ON TRFUWATARI.GYM_ID = TRMEI.GYM_ID " +
                      "     AND TRFUWATARI.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "     AND TRFUWATARI.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "     AND TRFUWATARI.BAT_ID = TRMEI.BAT_ID " +
                      "     AND TRFUWATARI.DETAILS_NO = TRMEI.DETAILS_NO " +
                      " WHERE TRMEI.GYM_ID = 2 " +
                      //"   AND TRMEI.DELETE_FLG = 0 " +
                      // 作成待
                      "   AND TRMEI.GRA_STS IN ( " + TrMei.Sts.未作成 + "," + TrMei.Sts.再作成対象 + ") " +
                      // 持出取消での削除明細 & 持帰訂正での削除明細は対象外
                      "   AND NOT ( " +
                      "           ( TRMEI.DELETE_FLG = 1 AND TRMEI.BCA_DATE <> 0 ) " +
                      "        OR ( TRMEI.DELETE_FLG = 1 AND TRMEI.GMA_STS = " + TrMei.Sts.結果正常 + " AND NVL(TRITEM_ID1.END_DATA,' ') <> '" + Schemabankcd.ToString("D4") + "' ) " +
                      "       ) " +
                      //"   AND EXISTS ( " +
                      //"        SELECT 1 " +
                      //"        FROM " + TBL_TRFUWATARI.TABLE_NAME(Schemabankcd) + " TRFUWATARI " +
                      //"        WHERE TRFUWATARI.GYM_ID = TRMEI.GYM_ID " +
                      //"          AND TRFUWATARI.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      //"          AND TRFUWATARI.SCAN_TERM = TRMEI.SCAN_TERM " +
                      //"          AND TRFUWATARI.BAT_ID = TRMEI.BAT_ID " +
                      //"          AND TRFUWATARI.DETAILS_NO = TRMEI.DETAILS_NO " +
                      //"       ) " +
                      " GROUP BY TRMEI.GYM_ID " +
                      "        , TRMEI.GRA_STS " +
                      "        , NVL(TRITEM_ID5.END_DATA, TRITEM_ID5.CTR_DATA) " +
                      "        , TRFUWATARI.E_YMD " +
                      "        , TRFUWATARI.DELETE_DATE " +
                      "        , TRFUWATARI.DELETE_FLG ";
            return strSql;
        }

        /// <summary>
        /// 進捗状況での持帰削除状況を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetProcViewICDeleteData(int Schemabankcd)
        {
            string strSql = "";

            strSql += " SELECT " +
                      "    TRMEI.GYM_ID " +
                      "  , NVL(TRITEM_ID5.END_DATA, TRITEM_ID5.CTR_DATA) CLEARING_DATE  " +
                      "  , COUNT(*) CNT  " +
                      " FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                      "     LEFT JOIN " +
                      "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                      "      ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                      "     AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "     AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "     AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                      "     AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "     AND TRITEM_ID5.ITEM_ID = 5 " +
                      " WHERE TRMEI.GYM_ID = 2 " +
                      "   AND TRMEI.DELETE_FLG = 1 " +
                      " GROUP BY TRMEI.GYM_ID " +
                      "        , NVL(TRITEM_ID5.END_DATA, TRITEM_ID5.CTR_DATA) ";
            return strSql;
        }

        /// <summary>
        /// 進捗状況での持帰訂正アップロード状況(作成待以外)を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetProcViewICTeiseiUpLoadData(int Schemabankcd)
        {
            string strSql = "";
            strSql += " SELECT " +
                      "    TRMEI.GYM_ID " +
                      "  , TRMEI.GMA_STS " +
                      "  , NVL(TRITEM_ID5.END_DATA, TRITEM_ID5.CTR_DATA) CLEARING_DATE  " +
                      "  , COUNT(*) CNT  " +
                      " FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                      "     LEFT JOIN " +
                      "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                      "      ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                      "     AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "     AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "     AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                      "     AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "     AND TRITEM_ID5.ITEM_ID = 5 " +
                      " WHERE TRMEI.GYM_ID = 2 " +
                      // ここは削除データも対象（結果正常の件数取得向け）
                      //"   AND TRMEI.DELETE_FLG = 0 " +
                      " GROUP BY TRMEI.GYM_ID " +
                      "        , TRMEI.GMA_STS " +
                      "        , NVL(TRITEM_ID5.END_DATA, TRITEM_ID5.CTR_DATA) ";
            return strSql;
        }

        /// <summary>
        /// 進捗状況での持帰訂正アップロード状況(作成待)を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetProcViewICTeiseiUpLoadDataWait(int Schemabankcd)
        {
            string strSql = "";
            // 訂正データ
            strSql += " SELECT " +
                      "    TRMEI.GYM_ID " +
                      "  , TRMEI.OPERATION_DATE " +
                      "  , TRMEI.SCAN_TERM " +
                      "  , TRMEI.BAT_ID " +
                      "  , TRMEI.DETAILS_NO " +
                      "  , TRMEI.GMA_STS " +
                      "  , NVL(TRITEM_ID5.END_DATA, TRITEM_ID5.CTR_DATA) CLEARING_DATE  " +
                      " FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                      "     LEFT JOIN " +
                      "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                      "      ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                      "     AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "     AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "     AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                      "     AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "     AND TRITEM_ID5.ITEM_ID = 5 " +
                      " WHERE TRMEI.GYM_ID = 2 " +
                      "   AND TRMEI.DELETE_FLG = 0 " +
                      "   AND TRMEI.GMA_STS IN (" + TrMei.Sts.未作成 + "," + TrMei.Sts.再作成対象 + "," + TrMei.Sts.結果正常 + ") " +
                      "   AND EXISTS ( " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " WKTRITEM " +
                      "        WHERE WKTRITEM.GYM_ID = TRMEI.GYM_ID " +
                      "          AND WKTRITEM.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "          AND WKTRITEM.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "          AND WKTRITEM.BAT_ID = TRMEI.BAT_ID " +
                      "          AND WKTRITEM.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "          AND WKTRITEM.ITEM_ID IN(" + DspItem.ItemId.持帰銀行コード + ", " + DspItem.ItemId.交換日 + ", " + DspItem.ItemId.金額 + ") " +
                      "          AND (   ( NVL(WKTRITEM.CTR_DATA, ' ') <> NVL(WKTRITEM.END_DATA, ' ') ) " +
                      "              AND ( NVL(WKTRITEM.ICTEISEI_DATA, ' ') <> NVL(WKTRITEM.END_DATA, ' ') ) " +
                      "              ) " +
                      "       ) " +
                      "   AND EXISTS ( " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " STS1 " +
                      "        WHERE STS1.GYM_ID = TRMEI.GYM_ID " +
                      "          AND STS1.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "          AND STS1.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "          AND STS1.BAT_ID = TRMEI.BAT_ID " +
                      "          AND STS1.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "          AND STS1.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.持帰銀行 + " " +
                      "          AND STS1.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                      "       ) " +
                      "   AND EXISTS ( " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " STS2 " +
                      "        WHERE STS2.GYM_ID = TRMEI.GYM_ID " +
                      "          AND STS2.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "          AND STS2.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "          AND STS2.BAT_ID = TRMEI.BAT_ID " +
                      "          AND STS2.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "          AND STS2.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.交換希望日 + " " +
                      "          AND STS2.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                      "       ) " +
                      "   AND EXISTS ( " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " STS3 " +
                      "        WHERE STS3.GYM_ID = TRMEI.GYM_ID " +
                      "          AND STS3.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "          AND STS3.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "          AND STS3.BAT_ID = TRMEI.BAT_ID " +
                      "          AND STS3.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "          AND STS3.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.金額 + " " +
                      "          AND STS3.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                      "       ) " +
                      "   AND EXISTS ( " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " STS5 " +
                      "        WHERE STS5.GYM_ID = TRMEI.GYM_ID " +
                      "          AND STS5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "          AND STS5.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "          AND STS5.BAT_ID = TRMEI.BAT_ID " +
                      "          AND STS5.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "          AND STS5.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.交換尻 + " " +
                      "          AND STS5.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                      "       ) " +

                      " UNION " +

                      // 訂正取消データ(訂正と訂正取消で重複した場合を考慮してUNION)
                      " SELECT " +
                      "    TRMEI.GYM_ID " +
                      "  , TRMEI.OPERATION_DATE " +
                      "  , TRMEI.SCAN_TERM " +
                      "  , TRMEI.BAT_ID " +
                      "  , TRMEI.DETAILS_NO " +
                      "  , TRMEI.GMA_STS " +
                      "  , NVL(TRITEM_ID5.END_DATA, TRITEM_ID5.CTR_DATA) CLEARING_DATE  " +
                      " FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                      "     LEFT JOIN " +
                      "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                      "      ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                      "     AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "     AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "     AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                      "     AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "     AND TRITEM_ID5.ITEM_ID = 5 " +
                      " WHERE TRMEI.GYM_ID = 2 " +
                      "   AND TRMEI.DELETE_FLG = 0 " +
                      "   AND TRMEI.GMA_STS IN (" + TrMei.Sts.再作成対象 + "," + TrMei.Sts.結果正常 + ") " +
                      "   AND EXISTS ( " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " WKTRITEM " +
                      "        WHERE WKTRITEM.GYM_ID = TRMEI.GYM_ID " +
                      "          AND WKTRITEM.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "          AND WKTRITEM.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "          AND WKTRITEM.BAT_ID = TRMEI.BAT_ID " +
                      "          AND WKTRITEM.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "          AND WKTRITEM.ITEM_ID IN(" + DspItem.ItemId.持帰銀行コード + ", " + DspItem.ItemId.交換日 + ", " + DspItem.ItemId.金額 + ") " +
                      "          AND (   ( NVL(WKTRITEM.CTR_DATA, ' ') = NVL(WKTRITEM.END_DATA, ' ') ) " +
                      "              AND ( NVL(WKTRITEM.ICTEISEI_DATA, ' ') <> NVL(WKTRITEM.END_DATA, ' ') ) " +
                      "              AND ( WKTRITEM.ICTEISEI_DATA IS NOT NULL ) " +
                      "              ) " +
                      "       ) " + 
                      "   AND EXISTS ( " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " STS1 " +
                      "        WHERE STS1.GYM_ID = TRMEI.GYM_ID " +
                      "          AND STS1.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "          AND STS1.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "          AND STS1.BAT_ID = TRMEI.BAT_ID " +
                      "          AND STS1.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "          AND STS1.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.持帰銀行 + " " +
                      "          AND STS1.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                      "       ) " +
                      "   AND EXISTS ( " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " STS2 " +
                      "        WHERE STS2.GYM_ID = TRMEI.GYM_ID " +
                      "          AND STS2.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "          AND STS2.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "          AND STS2.BAT_ID = TRMEI.BAT_ID " +
                      "          AND STS2.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "          AND STS2.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.交換希望日 + " " +
                      "          AND STS2.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                      "       ) " +
                      "   AND EXISTS ( " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " STS3 " +
                      "        WHERE STS3.GYM_ID = TRMEI.GYM_ID " +
                      "          AND STS3.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "          AND STS3.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "          AND STS3.BAT_ID = TRMEI.BAT_ID " +
                      "          AND STS3.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "          AND STS3.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.金額 + " " +
                      "          AND STS3.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                      "       ) " +
                      "   AND EXISTS ( " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " STS5 " +
                      "        WHERE STS5.GYM_ID = TRMEI.GYM_ID " +
                      "          AND STS5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                      "          AND STS5.SCAN_TERM = TRMEI.SCAN_TERM " +
                      "          AND STS5.BAT_ID = TRMEI.BAT_ID " +
                      "          AND STS5.DETAILS_NO = TRMEI.DETAILS_NO " +
                      "          AND STS5.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.交換尻 + " " +
                      "          AND STS5.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                      "       ) ";

            strSql = " SELECT " +
                     "    WK.GYM_ID " +
                     "  , WK.GMA_STS " +
                     "  , WK.CLEARING_DATE " +
                     "  , COUNT(*) CNT  " +
                     " FROM ( " + strSql + " ) WK " +
                     " GROUP BY WK.GYM_ID " +
                     "        , WK.GMA_STS " +
                     "        , WK.CLEARING_DATE ";
            return strSql;
        }

    }
}
