using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTable.DB
{
    public class SQLImageImport
    {

        /// <summary>
        /// イメージファイルの一意キー算出時のSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string Get_ImageFileUniqueCodeQuery()
        {
            string strSql = " SELECT TO_CHAR(SYSTIMESTAMP, 'YYMMDDHH24MISSFF') As CODE FROM dual";
            return strSql;
        }

        /// <summary>
        /// 期日管理取込で対象データが登録済か確認するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string Get_KijituImportChk(int gymid, int gymdate, string folderName, string fileName, int Schemabankcd)
        {
            string strSql = 
                " SELECT " +
                "    TRMEI.* " +
                "  , TRMEIIMG.BUA_STS " +
                " FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                "          INNER JOIN " +
                "      " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                "           ON TRMEIIMG.GYM_ID = TRMEI.GYM_ID " +
                "          AND TRMEIIMG.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "          AND TRMEIIMG.SCAN_TERM = TRMEI.SCAN_TERM " +
                "          AND TRMEIIMG.BAT_ID = TRMEI.BAT_ID " +
                "          AND TRMEIIMG.DETAILS_NO = TRMEI.DETAILS_NO " +
                "          AND TRMEIIMG.IMG_KBN = " + TrMeiImg.ImgKbn.表 + " " +
                " WHERE " +
                "     TRMEI.GYM_ID = " + gymid + " " +
                " AND TRMEI.OPERATION_DATE = " + gymdate + " " +
                " AND TRMEIIMG.IMG_FLNM_OLD = '" + fileName + "' " +
                " AND EXISTS ( " +
                "  SELECT 1 FROM " + TBL_TRBATCH.TABLE_NAME(Schemabankcd) + " TRBAT " +
                "  WHERE " +
                "       TRBAT.GYM_ID  = TRMEI.GYM_ID " +
                "   AND TRBAT.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "   AND TRBAT.SCAN_TERM = TRMEI.SCAN_TERM " +
                "   AND TRBAT.BAT_ID = TRMEI.BAT_ID " +
                "   AND TRBAT.INPUT_ROUTE = 3" +
                " ) " +
                " AND EXISTS ( " +
                "  SELECT 1 FROM " + TBL_TRBATCHIMG.TABLE_NAME(Schemabankcd) + " TRBATIMG " +
                "  WHERE " +
                "       TRBATIMG.GYM_ID = TRMEI.GYM_ID " +
                "   AND TRBATIMG.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "   AND TRBATIMG.SCAN_TERM = TRMEI.SCAN_TERM " +
                "   AND TRBATIMG.BAT_ID = TRMEI.BAT_ID " +
                "   AND TRBATIMG.SCAN_BATCH_FOLDER_NAME = '" + folderName + "' " +
                " ) ";

            return strSql;
        }

        /// <summary>
        /// 期日管理取込で登録済データ（対象フォルダ単位）を取得するSELECT文を作成します
        /// </summary>
        /// <returns></returns>
        public static string Get_KijituImportData(int gymid, int gymdate, string folderName, int Schemabankcd)
        {
            string strSql =
                " SELECT " +
                "    TRBAT.SCAN_TERM " +
                "  , TRBAT.BAT_ID " +
                "  , MAX(TRMEI.DETAILS_NO) DETAILS_NO " +
                " FROM " + TBL_TRBATCH.TABLE_NAME(Schemabankcd) + " TRBAT " +
                "          INNER JOIN " +
                "      " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                "           ON TRMEI.GYM_ID = TRBAT.GYM_ID " +
                "          AND TRMEI.OPERATION_DATE = TRBAT.OPERATION_DATE " +
                "          AND TRMEI.SCAN_TERM = TRBAT.SCAN_TERM " +
                "          AND TRMEI.BAT_ID = TRBAT.BAT_ID " +
                " WHERE " +
                "     TRBAT.GYM_ID = " + gymid + " " +
                " AND TRBAT.OPERATION_DATE = " + gymdate + " " +
                " AND TRBAT.INPUT_ROUTE = 3" +
                " AND EXISTS ( " +
                "  SELECT 1 FROM " + TBL_TRBATCHIMG.TABLE_NAME(Schemabankcd) + " TRBATIMG " +
                "  WHERE " +
                "       TRBATIMG.GYM_ID = TRBAT.GYM_ID " +
                "   AND TRBATIMG.OPERATION_DATE = TRBAT.OPERATION_DATE " +
                "   AND TRBATIMG.SCAN_TERM = TRBAT.SCAN_TERM " +
                "   AND TRBATIMG.BAT_ID = TRBAT.BAT_ID " +
                "   AND TRBATIMG.SCAN_BATCH_FOLDER_NAME = '" + folderName + "' " +
                " ) " +
                " GROUP BY TRBAT.SCAN_TERM, TRBAT.BAT_ID ";

            return strSql;
        }

    }
}
