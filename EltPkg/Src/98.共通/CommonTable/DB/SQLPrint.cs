using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTable.DB
{
    public class SQLPrint
    {

        /// <summary>
        /// 明細リスト出力時の印刷データを取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetMeiListPrintData(int opedate, int gymid, string scanterm, int batid, int detail, int Schemabankcd)
        {
            string strSql =
                 "SELECT TRMEIIMG.GYM_ID " +
                 "     , TRMEIIMG.OPERATION_DATE " +
                 "     , TRMEIIMG.SCAN_TERM " +
                 "     , TRMEIIMG.BAT_ID " +
                 "     , TRMEIIMG.DETAILS_NO " +
                 "     , TRMEIIMG.IMG_KBN " +
                 "     , TRMEIIMG.IMG_FLNM " +
                 "     , TRBATCH.INPUT_ROUTE " +
                 "     , TRBATCH.OC_BK_NO " +
                 "     , TRBATCH.OC_BR_NO " +
                 "     , TRMEI.IC_OC_BK_NO " +
                 "     , TRITEM_ID1.END_DATA ICBKKNO " +
                 "     , TRITEM_ID3.END_DATA CLEARING_DATE " +
                 "     , TRITEM_ID6.END_DATA AMT " +
                 "     , TRITEM_ID8.END_DATA BILLCD " +
                 "     , TRITEM_ID10.END_DATA SYURUICD " +
                 "     , TRITEM_ID13.END_DATA ICBRNO " +
                 "     , TRITEM_ID16.END_DATA ACCOUNT " +
                 "     , TRITEM_ID18.END_DATA BILLNO " +
                 " FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                 "     INNER JOIN " +
                 "      " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                 "      ON TRMEIIMG.GYM_ID = TRMEI.GYM_ID " +
                 "     AND TRMEIIMG.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                 "     AND TRMEIIMG.SCAN_TERM = TRMEI.SCAN_TERM " +
                 "     AND TRMEIIMG.BAT_ID = TRMEI.BAT_ID " +
                 "     AND TRMEIIMG.DETAILS_NO = TRMEI.DETAILS_NO " +
                 "     LEFT JOIN " +
                 "      " + TBL_TRBATCH.TABLE_NAME(Schemabankcd) + " TRBATCH " +
                 "      ON TRMEI.GYM_ID = TRBATCH.GYM_ID " +
                 "     AND TRMEI.OPERATION_DATE = TRBATCH.OPERATION_DATE " +
                 "     AND TRMEI.SCAN_TERM = TRBATCH.SCAN_TERM " +
                 "     AND TRMEI.BAT_ID = TRBATCH.BAT_ID " +
                 "     LEFT JOIN " +
                 "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID1 " +
                 "      ON TRMEI.GYM_ID = TRITEM_ID1.GYM_ID " +
                 "     AND TRMEI.OPERATION_DATE = TRITEM_ID1.OPERATION_DATE " +
                 "     AND TRMEI.SCAN_TERM = TRITEM_ID1.SCAN_TERM " +
                 "     AND TRMEI.BAT_ID = TRITEM_ID1.BAT_ID " +
                 "     AND TRMEI.DETAILS_NO = TRITEM_ID1.DETAILS_NO " +
                 "     AND TRITEM_ID1.ITEM_ID = 1 " +
                 "     LEFT JOIN " +
                 "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID3 " +
                 "      ON TRMEI.GYM_ID = TRITEM_ID3.GYM_ID " +
                 "     AND TRMEI.OPERATION_DATE = TRITEM_ID3.OPERATION_DATE " +
                 "     AND TRMEI.SCAN_TERM = TRITEM_ID3.SCAN_TERM " +
                 "     AND TRMEI.BAT_ID = TRITEM_ID3.BAT_ID " +
                 "     AND TRMEI.DETAILS_NO = TRITEM_ID3.DETAILS_NO " +
                 "     AND TRITEM_ID3.ITEM_ID = 3 " +
                 "     LEFT JOIN " +
                 "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID6 " +
                 "      ON TRMEI.GYM_ID = TRITEM_ID6.GYM_ID " +
                 "     AND TRMEI.OPERATION_DATE = TRITEM_ID6.OPERATION_DATE " +
                 "     AND TRMEI.SCAN_TERM = TRITEM_ID6.SCAN_TERM " +
                 "     AND TRMEI.BAT_ID = TRITEM_ID6.BAT_ID " +
                 "     AND TRMEI.DETAILS_NO = TRITEM_ID6.DETAILS_NO " +
                 "     AND TRITEM_ID6.ITEM_ID = 6 " +
                 "     LEFT JOIN " +
                 "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID8 " +
                 "      ON TRMEI.GYM_ID = TRITEM_ID8.GYM_ID " +
                 "     AND TRMEI.OPERATION_DATE = TRITEM_ID8.OPERATION_DATE " +
                 "     AND TRMEI.SCAN_TERM = TRITEM_ID8.SCAN_TERM " +
                 "     AND TRMEI.BAT_ID = TRITEM_ID8.BAT_ID " +
                 "     AND TRMEI.DETAILS_NO = TRITEM_ID8.DETAILS_NO " +
                 "     AND TRITEM_ID8.ITEM_ID = 8 " +
                 "     LEFT JOIN " +
                 "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID10 " +
                 "      ON TRMEI.GYM_ID = TRITEM_ID10.GYM_ID " +
                 "     AND TRMEI.OPERATION_DATE = TRITEM_ID10.OPERATION_DATE " +
                 "     AND TRMEI.SCAN_TERM = TRITEM_ID10.SCAN_TERM " +
                 "     AND TRMEI.BAT_ID = TRITEM_ID10.BAT_ID " +
                 "     AND TRMEI.DETAILS_NO = TRITEM_ID10.DETAILS_NO " +
                 "     AND TRITEM_ID10.ITEM_ID = 10 " +
                 "     LEFT JOIN " +
                 "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID13 " +
                 "      ON TRMEI.GYM_ID = TRITEM_ID13.GYM_ID " +
                 "     AND TRMEI.OPERATION_DATE = TRITEM_ID13.OPERATION_DATE " +
                 "     AND TRMEI.SCAN_TERM = TRITEM_ID13.SCAN_TERM " +
                 "     AND TRMEI.BAT_ID = TRITEM_ID13.BAT_ID " +
                 "     AND TRMEI.DETAILS_NO = TRITEM_ID13.DETAILS_NO " +
                 "     AND TRITEM_ID13.ITEM_ID = 13 " +
                 "     LEFT JOIN " +
                 "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID16 " +
                 "      ON TRMEI.GYM_ID = TRITEM_ID16.GYM_ID " +
                 "     AND TRMEI.OPERATION_DATE = TRITEM_ID16.OPERATION_DATE " +
                 "     AND TRMEI.SCAN_TERM = TRITEM_ID16.SCAN_TERM " +
                 "     AND TRMEI.BAT_ID = TRITEM_ID16.BAT_ID " +
                 "     AND TRMEI.DETAILS_NO = TRITEM_ID16.DETAILS_NO " +
                 "     AND TRITEM_ID16.ITEM_ID = 16 " +
                 "     LEFT JOIN " +
                 "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID18 " +
                 "      ON TRMEI.GYM_ID = TRITEM_ID18.GYM_ID " +
                 "     AND TRMEI.OPERATION_DATE = TRITEM_ID18.OPERATION_DATE " +
                 "     AND TRMEI.SCAN_TERM = TRITEM_ID18.SCAN_TERM " +
                 "     AND TRMEI.BAT_ID = TRITEM_ID18.BAT_ID " +
                 "     AND TRMEI.DETAILS_NO = TRITEM_ID18.DETAILS_NO " +
                 "     AND TRITEM_ID18.ITEM_ID = 18 " +
                 " WHERE TRMEIIMG.DELETE_FLG = 0 " +
                 "   AND TRMEIIMG.OPERATION_DATE = " + opedate + " " +
                 "   AND TRMEIIMG.GYM_ID = " + gymid + " " +
                 "   AND TRMEIIMG.SCAN_TERM = '" + scanterm + "' " +
                 "   AND TRMEIIMG.BAT_ID = " + batid + " " +
                 "   AND TRMEIIMG.DETAILS_NO = " + detail + " ";
            return strSql;
        }

        /// <summary>
        /// 持出バッチ別合計票出力時の印刷データを取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetOcBatchTotalPrintData(int gymid, int date, int Schemabankcd)
        {
            string strSql =
                 "SELECT  TRBATCH.OPERATION_DATE " +
                 "     , TRBATCH.BAT_ID " +
                 "     , TRBATCH.OC_BK_NO " +
                 "     , TRBATCH.OC_BR_NO " +
                 "     , TRBATCH.CLEARING_DATE " +
                 "     , TRBATCH.TOTAL_COUNT " +
                 "     , TRBATCH.TOTAL_AMOUNT " +
                 "     , COUNT(CASE WHEN TRMEI.DELETE_FLG = 0 THEN TRMEI.DETAILS_NO END) CNT " +
                 "     , SUM(CASE WHEN TRMEI.DELETE_FLG = 0 THEN TRITEM_ID6.END_DATA END) AMT " +
                 " FROM " + TBL_TRBATCH.TABLE_NAME(Schemabankcd) + " TRBATCH " +
                 "     LEFT JOIN " +
                 "      " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                 "      ON TRBATCH.GYM_ID = TRMEI.GYM_ID " +
                 "     AND TRBATCH.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                 "     AND TRBATCH.SCAN_TERM = TRMEI.SCAN_TERM " +
                 "     AND TRBATCH.BAT_ID = TRMEI.BAT_ID " +
                 "     LEFT JOIN " +
                 "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID6 " +
                 "      ON TRMEI.GYM_ID = TRITEM_ID6.GYM_ID " +
                 "     AND TRMEI.OPERATION_DATE = TRITEM_ID6.OPERATION_DATE " +
                 "     AND TRMEI.SCAN_TERM = TRITEM_ID6.SCAN_TERM " +
                 "     AND TRMEI.BAT_ID = TRITEM_ID6.BAT_ID " +
                 "     AND TRMEI.DETAILS_NO = TRITEM_ID6.DETAILS_NO " +
                 "     AND TRITEM_ID6.ITEM_ID = 6 " +
                 " WHERE TRBATCH.GYM_ID = " + gymid + " " +
                 "   AND TRBATCH.DELETE_FLG = 0 ";
            if (date > 0)
            {
                strSql +=
                 "   AND TRBATCH.OPERATION_DATE = " + date + " ";
            }
            strSql +=
                 " GROUP BY TRBATCH.OPERATION_DATE " +
                 "        , TRBATCH.BAT_ID " +
                 "        , TRBATCH.OC_BK_NO " +
                 "        , TRBATCH.OC_BR_NO " +
                 "        , TRBATCH.CLEARING_DATE " +
                 "        , TRBATCH.TOTAL_COUNT " +
                 "        , TRBATCH.TOTAL_AMOUNT " +
                 " ORDER BY TRBATCH.OPERATION_DATE, TRBATCH.BAT_ID ";
            return strSql;
        }

        /// <summary>
        /// 持出支店別合計票出力時の印刷データを取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetOcBrTotalPrintData(int gymid, int date, int Schemabankcd)
        {
            string strSql =
                 "SELECT BRANCH.BR_NO " +
                 "     , BRANCH.BR_NAME_KANJI " +
                 "     , SUB.CNT " +
                 "     , SUB.AMT " +
                 " FROM " + TBL_BRANCHMF.TABLE_NAME(Schemabankcd) + " BRANCH " +
                 "     LEFT JOIN " +
                 "     ( SELECT TRBATCH.OC_BR_NO " +
                 "            , COUNT(*) CNT " +
                 "            , SUM(TRITEM_ID6.END_DATA) AMT " +
                 "       FROM " + TBL_TRBATCH.TABLE_NAME(Schemabankcd) + " TRBATCH " +
                 "            INNER JOIN " +
                 "             " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                 "             ON TRBATCH.GYM_ID = TRMEI.GYM_ID " +
                 "            AND TRBATCH.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                 "            AND TRBATCH.SCAN_TERM = TRMEI.SCAN_TERM " +
                 "            AND TRBATCH.BAT_ID = TRMEI.BAT_ID " +
                 "            LEFT JOIN " +
                 "             " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID6 " +
                 "             ON TRMEI.GYM_ID = TRITEM_ID6.GYM_ID " +
                 "            AND TRMEI.OPERATION_DATE = TRITEM_ID6.OPERATION_DATE " +
                 "            AND TRMEI.SCAN_TERM = TRITEM_ID6.SCAN_TERM " +
                 "            AND TRMEI.BAT_ID = TRITEM_ID6.BAT_ID " +
                 "            AND TRMEI.DETAILS_NO = TRITEM_ID6.DETAILS_NO " +
                 "            AND TRITEM_ID6.ITEM_ID = 6 " +
                 "       WHERE TRBATCH.GYM_ID = " + gymid + " " +
                 "         AND TRBATCH.DELETE_FLG = 0 " +
                 "         AND TRMEI.DELETE_FLG = 0 " +
                 "         AND EXISTS ( " +
                 "              SELECT 1  " +
                 "              FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                 "              WHERE TRMEI.GYM_ID = TRITEM_ID5.GYM_ID " +
                 "                AND TRMEI.OPERATION_DATE = TRITEM_ID5.OPERATION_DATE " +
                 "                AND TRMEI.SCAN_TERM = TRITEM_ID5.SCAN_TERM " +
                 "                AND TRMEI.BAT_ID = TRITEM_ID5.BAT_ID " +
                 "                AND TRMEI.DETAILS_NO = TRITEM_ID5.DETAILS_NO " +
                 "                AND TRITEM_ID5.ITEM_ID = 5 " +
                 "                AND TRITEM_ID5.END_DATA = '" + date + "' " +
                 "             ) " +
                 "       GROUP BY TRBATCH.OC_BR_NO " +
                 "     ) SUB " +
                 "      ON BRANCH.BR_NO = SUB.OC_BR_NO " +
                 " ORDER BY BRANCH.BR_NO ";
            return strSql;
        }

        /// <summary>
        /// 持出銀行別合計表(持帰)出力時の印刷データを取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetIcOcBkTotalPrintData(int gymid, int date, int Schemabankcd)
        {
            string strSql =
                 "SELECT TRMEI.IC_OC_BK_NO " +
                 "     , COUNT(*) CNT " +
                 "     , SUM(TRITEM_ID6.END_DATA) AMT " +
                 "FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                 "     LEFT JOIN " +
                 "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID6 " +
                 "      ON TRMEI.GYM_ID = TRITEM_ID6.GYM_ID " +
                 "     AND TRMEI.OPERATION_DATE = TRITEM_ID6.OPERATION_DATE " +
                 "     AND TRMEI.SCAN_TERM = TRITEM_ID6.SCAN_TERM " +
                 "     AND TRMEI.BAT_ID = TRITEM_ID6.BAT_ID " +
                 "     AND TRMEI.DETAILS_NO = TRITEM_ID6.DETAILS_NO " +
                 "     AND TRITEM_ID6.ITEM_ID = 6 " +
                 "     LEFT JOIN " +
                 "     " + TBL_TRFUWATARI.TABLE_NAME(Schemabankcd) + " TRFUWATARI " +
                 "      ON TRMEI.GYM_ID = TRFUWATARI.GYM_ID " +
                 "     AND TRMEI.OPERATION_DATE = TRFUWATARI.OPERATION_DATE " +
                 "     AND TRMEI.SCAN_TERM = TRFUWATARI.SCAN_TERM " +
                 "     AND TRMEI.BAT_ID = TRFUWATARI.BAT_ID " +
                 "     AND TRMEI.DETAILS_NO = TRFUWATARI.DETAILS_NO " +
                 "WHERE TRMEI.GYM_ID = " + gymid + " " +
                 "  AND TRMEI.DELETE_FLG = 0 " +
                 "  AND EXISTS ( " +
                 "       SELECT 1  " +
                 "       FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                 "       WHERE TRMEI.GYM_ID = TRITEM_ID5.GYM_ID " +
                 "         AND TRMEI.OPERATION_DATE = TRITEM_ID5.OPERATION_DATE " +
                 "         AND TRMEI.SCAN_TERM = TRITEM_ID5.SCAN_TERM " +
                 "         AND TRMEI.BAT_ID = TRITEM_ID5.BAT_ID " +
                 "         AND TRMEI.DETAILS_NO = TRITEM_ID5.DETAILS_NO " +
                 "         AND TRITEM_ID5.ITEM_ID = 5 " +
                 "         AND TRITEM_ID5.END_DATA = '" + date + "' " +
                 "       ) " +
                 // 不渡登録なし または 不渡登録が削除のデータが対象
                 "  AND ( TRFUWATARI.GYM_ID IS NULL OR TRFUWATARI.DELETE_FLG <> 0 ) " +
                 "GROUP BY TRMEI.IC_OC_BK_NO " +
                 "ORDER BY TRMEI.IC_OC_BK_NO ";
            return strSql;
        }

        /// <summary>
        /// 持帰店別合計表出力時の印刷データを取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetCTRIcBrTotalPrintData(int gymid, int date, int Schemabankcd)
        {
            string strSql =
                 "SELECT BRANCH.BR_NO " +
                 "     , BRANCH.BR_NAME_KANJI " +
                 "     , SUB.CNT " +
                 "     , SUB.AMT " +
                 " FROM " + TBL_BRANCHMF.TABLE_NAME(Schemabankcd) + " BRANCH " +
                 "     LEFT JOIN " +
                 "     ( SELECT TRITEM_ID13.END_DATA BR_NO " +
                 "            , COUNT(*) CNT " +
                 "            , SUM(TRITEM_ID6.END_DATA) AMT " +
                 "       FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                 "            LEFT JOIN " +
                 "             " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID6 " +
                 "             ON TRMEI.GYM_ID = TRITEM_ID6.GYM_ID " +
                 "            AND TRMEI.OPERATION_DATE = TRITEM_ID6.OPERATION_DATE " +
                 "            AND TRMEI.SCAN_TERM = TRITEM_ID6.SCAN_TERM " +
                 "            AND TRMEI.BAT_ID = TRITEM_ID6.BAT_ID " +
                 "            AND TRMEI.DETAILS_NO = TRITEM_ID6.DETAILS_NO " +
                 "            AND TRITEM_ID6.ITEM_ID = 6 " +
                 "            LEFT JOIN " +
                 "             " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID13 " +
                 "             ON TRMEI.GYM_ID = TRITEM_ID13.GYM_ID " +
                 "            AND TRMEI.OPERATION_DATE = TRITEM_ID13.OPERATION_DATE " +
                 "            AND TRMEI.SCAN_TERM = TRITEM_ID13.SCAN_TERM " +
                 "            AND TRMEI.BAT_ID = TRITEM_ID13.BAT_ID " +
                 "            AND TRMEI.DETAILS_NO = TRITEM_ID13.DETAILS_NO " +
                 "            AND TRITEM_ID13.ITEM_ID = 13 " +
                 "            LEFT JOIN " +
                 "             " + TBL_TRFUWATARI.TABLE_NAME(Schemabankcd) + " TRFUWATARI " +
                 "             ON TRMEI.GYM_ID = TRFUWATARI.GYM_ID " +
                 "            AND TRMEI.OPERATION_DATE = TRFUWATARI.OPERATION_DATE " +
                 "            AND TRMEI.SCAN_TERM = TRFUWATARI.SCAN_TERM " +
                 "            AND TRMEI.BAT_ID = TRFUWATARI.BAT_ID " +
                 "            AND TRMEI.DETAILS_NO = TRFUWATARI.DETAILS_NO " +
                 "       WHERE TRMEI.GYM_ID = " + gymid + " " +
                 "         AND TRMEI.DELETE_FLG = 0 " +
                 "         AND EXISTS ( " +
                 "              SELECT 1  " +
                 "              FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                 "              WHERE TRMEI.GYM_ID = TRITEM_ID5.GYM_ID " +
                 "                AND TRMEI.OPERATION_DATE = TRITEM_ID5.OPERATION_DATE " +
                 "                AND TRMEI.SCAN_TERM = TRITEM_ID5.SCAN_TERM " +
                 "                AND TRMEI.BAT_ID = TRITEM_ID5.BAT_ID " +
                 "                AND TRMEI.DETAILS_NO = TRITEM_ID5.DETAILS_NO " +
                 "                AND TRITEM_ID5.ITEM_ID = 5 " +
                 "                AND TRITEM_ID5.END_DATA = '" + date + "' " +
                 "             ) " +
                 // 不渡登録なし または 不渡登録が削除のデータが対象
                 "         AND ( TRFUWATARI.GYM_ID IS NULL OR TRFUWATARI.DELETE_FLG <> 0 ) " +
                 "       GROUP BY TRITEM_ID13.END_DATA " +
                 "     ) SUB " +
                 "      ON BRANCH.BR_NO = SUB.BR_NO " +
                 " ORDER BY BRANCH.BR_NO ";
            return strSql;
        }

        /// <summary>
        /// オペレータ統計出力時の印刷データを取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetCTROpeListPrintData(int dateFrom, int dateTo, int Schemabankcd)
        {
            string strSql =
                 "SELECT GYM_ID " +
                 "     , ITEM_ID " +
                 "     , MAX(ITEM_NAME) ITEM_NAME " +
                 "     , OPENO " +
                 "     , SUM(E_CNT) E_CNT " +
                 "     , SUM(E_TIME) E_TIME " +
                 "     , SUM(V_CNT) V_CNT " +
                 "     , SUM(V_TIME) V_TIME " +
                 "     , SUM(TEISEICNT) TEISEICNT " +
                 " FROM( " +
                 "  SELECT GYM_ID " +
                 "       , ITEM_ID " +
                 "       , MAX(ITEM_NAME) ITEM_NAME " +
                 "       , E_OPENO OPENO " +
                 "       , COUNT(*) E_CNT " +
                 "       , SUM(E_TIME) E_TIME " +
                 "       , 0 V_CNT " +
                 "       , 0 V_TIME " +
                 "       , SUM(CASE WHEN C_TIME > 0 OR O_TIME > 0 THEN 1 ELSE 0 END ) TEISEICNT " +
                 "  FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " " +
                 "  WHERE E_TIME > 0 " +
                 "    AND E_YMD >= " + dateFrom + " " +
                 "    AND E_YMD <= " + dateTo + " " +
                 "  GROUP BY GYM_ID, ITEM_ID, E_OPENO " +
                 "  UNION ALL " +
                 "  SELECT GYM_ID " +
                 "       , ITEM_ID " +
                 "       , MAX(ITEM_NAME) ITEM_NAME " +
                 "       , V_OPENO OPENO " +
                 "       , 0 E_CNT " +
                 "       , 0 E_TIME " +
                 "       , COUNT(*) V_CNT " +
                 "       , SUM(V_TIME) V_TIME " +
                 "       , SUM(CASE WHEN C_TIME > 0 OR O_TIME > 0 THEN 1 ELSE 0 END ) TEISEICNT " +
                 "  FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " " +
                 "  WHERE V_TIME > 0 " +
                 "    AND V_YMD >= " + dateFrom + " " +
                 "    AND V_YMD <= " + dateTo + " " +
                 "  GROUP BY GYM_ID, ITEM_ID, V_OPENO " +
                 " ) " +
                 " GROUP BY GYM_ID, ITEM_ID, OPENO " +
                 " ORDER BY GYM_ID, ITEM_ID, OPENO ";
            return strSql;
        }

        /// <summary>
        /// 手形種類別集計表出力時の印刷データを取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string GetSyubetuPrintData(int gymid, int date, int Schemabankcd)
        {
            string strSql =
                 "SELECT SYURUI.SYURUI_CODE " +
                 "     , SYURUI.SYURUI_NAME " +
                 "     , SUB.CNT " +
                 "     , SUB.AMT " +
                 " FROM " + TBL_SYURUIMF.TABLE_NAME(Schemabankcd) + " SYURUI " +
                 "     FULL OUTER JOIN " +
                 "     ( SELECT NVL(TRITEM_ID10.END_DATA, 999) SYURUI_CODE " +
                 "            , COUNT(*) CNT " +
                 "            , SUM(TRITEM_ID6.END_DATA) AMT " +
                 "       FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                 "            LEFT JOIN " +
                 "             " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID6 " +
                 "             ON TRMEI.GYM_ID = TRITEM_ID6.GYM_ID " +
                 "            AND TRMEI.OPERATION_DATE = TRITEM_ID6.OPERATION_DATE " +
                 "            AND TRMEI.SCAN_TERM = TRITEM_ID6.SCAN_TERM " +
                 "            AND TRMEI.BAT_ID = TRITEM_ID6.BAT_ID " +
                 "            AND TRMEI.DETAILS_NO = TRITEM_ID6.DETAILS_NO " +
                 "            AND TRITEM_ID6.ITEM_ID = 6 " +
                 "            LEFT JOIN " +
                 "             " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID10 " +
                 "             ON TRMEI.GYM_ID = TRITEM_ID10.GYM_ID " +
                 "            AND TRMEI.OPERATION_DATE = TRITEM_ID10.OPERATION_DATE " +
                 "            AND TRMEI.SCAN_TERM = TRITEM_ID10.SCAN_TERM " +
                 "            AND TRMEI.BAT_ID = TRITEM_ID10.BAT_ID " +
                 "            AND TRMEI.DETAILS_NO = TRITEM_ID10.DETAILS_NO " +
                 "            AND TRITEM_ID10.ITEM_ID = 10 " +
                 "            LEFT JOIN " +
                 "             " + TBL_TRFUWATARI.TABLE_NAME(Schemabankcd) + " TRFUWATARI " +
                 "             ON TRMEI.GYM_ID = TRFUWATARI.GYM_ID " +
                 "            AND TRMEI.OPERATION_DATE = TRFUWATARI.OPERATION_DATE " +
                 "            AND TRMEI.SCAN_TERM = TRFUWATARI.SCAN_TERM " +
                 "            AND TRMEI.BAT_ID = TRFUWATARI.BAT_ID " +
                 "            AND TRMEI.DETAILS_NO = TRFUWATARI.DETAILS_NO " +
                 "       WHERE TRMEI.GYM_ID = " + gymid + " " +
                 "         AND TRMEI.DELETE_FLG = 0 " +
                 "         AND EXISTS ( " +
                 "              SELECT 1  " +
                 "              FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                 "              WHERE TRMEI.GYM_ID = TRITEM_ID5.GYM_ID " +
                 "                AND TRMEI.OPERATION_DATE = TRITEM_ID5.OPERATION_DATE " +
                 "                AND TRMEI.SCAN_TERM = TRITEM_ID5.SCAN_TERM " +
                 "                AND TRMEI.BAT_ID = TRITEM_ID5.BAT_ID " +
                 "                AND TRMEI.DETAILS_NO = TRITEM_ID5.DETAILS_NO " +
                 "                AND TRITEM_ID5.ITEM_ID = 5 " +
                 "                AND TRITEM_ID5.END_DATA = '" + date + "' " +
                 "             ) " +
                 // 不渡登録なし または 不渡登録が削除のデータが対象
                 "         AND ( TRFUWATARI.GYM_ID IS NULL OR TRFUWATARI.DELETE_FLG <> 0 ) " +
                 "       GROUP BY NVL(TRITEM_ID10.END_DATA, 999) " +
                 "     ) SUB " +
                 "      ON SYURUI.SYURUI_CODE = SUB.SYURUI_CODE " +
                 " ORDER BY SYURUI.SYURUI_CODE ";
            return strSql;
        }
    }
}
