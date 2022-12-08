using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTable.DB
{
    public class SQLSearch
    {
        /// <summary>
        /// 持出店別照会での検索SQL取得
        /// </summary>
        /// <returns></returns>
        public static string Get_SearchOutclearingBranch(int gymid, int opedate, int clearingdate, int brcode, int Schemabankcd, int ListDispLimit)
        {
            string strSql = "SELECT BAT.OPERATION_DATE, BAT.OC_BR_NO, BAT.CLEARING_DATE, COUNT(MEI.DETAILS_NO) MEICOUNT, SUM(ID6.END_DATA) AMT ";
            strSql += "FROM " + TBL_TRBATCH.TABLE_NAME(Schemabankcd) + " BAT ";
            strSql += " INNER JOIN " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " MEI " +
                      "  ON " +
                      "      BAT.GYM_ID = MEI.GYM_ID " +
                      "  AND BAT.OPERATION_DATE = MEI.OPERATION_DATE " +
                      "  AND BAT.SCAN_TERM = MEI.SCAN_TERM " +
                      "  AND BAT.BAT_ID = MEI.BAT_ID ";
            strSql += " LEFT JOIN " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " ID6 " +
                      "  ON " +
                      "      MEI.GYM_ID = ID6.GYM_ID " +
                      "  AND MEI.OPERATION_DATE = ID6.OPERATION_DATE " +
                      "  AND MEI.SCAN_TERM = ID6.SCAN_TERM " +
                      "  AND MEI.BAT_ID = ID6.BAT_ID " +
                      "  AND MEI.DETAILS_NO = ID6.DETAILS_NO " +
                      "  AND ID6.ITEM_ID = 6 ";
            strSql += " WHERE BAT.GYM_ID = " + gymid + " " +
                      "   AND BAT.DELETE_FLG = 0 " +
                      "   AND MEI.DELETE_FLG = 0 ";
            if (clearingdate > -1)
            {
                // 設定がある場合
                strSql += " AND BAT.CLEARING_DATE = " + clearingdate + " ";
            }
            if (opedate > -1)
            {
                // 設定がある場合
                strSql += " AND BAT.OPERATION_DATE = " + opedate + " ";
            }
            if (brcode > -1)
            {
                // 設定がある場合
                strSql += " AND BAT.OC_BR_NO = " + brcode + " ";
            }
            strSql += " GROUP BY BAT.OPERATION_DATE, BAT.OC_BR_NO, BAT.CLEARING_DATE ";
            strSql += " ORDER BY BAT.OPERATION_DATE, BAT.OC_BR_NO, BAT.CLEARING_DATE ";

            //取得件数制御
            strSql = "SELECT * FROM ( " + strSql + " ) WHERE ROWNUM <= " + (ListDispLimit + 1) + " ";

            return strSql;
        }

        /// <summary>
        /// 結果照会での検索SQL取得
        /// </summary>
        /// <returns></returns>
        public static string Get_SearchResultData(int Date, string filedivid, int Schemabankcd, int ErrFlg, int ListDispLimit)
        {
            string strSql = "SELECT CTL.* FROM " + TBL_RESULTTXT_CTL.TABLE_NAME(Schemabankcd) + " CTL ";
            string wk = " WHERE ";
            if (Date > -1)
            {
                // 取込日の設定がある場合
                strSql += wk + " CTL.RECV_DATE = " + Date + " ";
                wk = " AND ";
            }
            if (!string.IsNullOrWhiteSpace(filedivid))
            {
                // 識別区分の設定がある場合
                strSql += wk + " CTL.FILE_DIVID = '" + filedivid + "' ";
                wk = " AND ";
            }
            if (ErrFlg == 0 || ErrFlg == 9)
            {
                // エラー設定がある場合
                string strSQLSub1 = " CTL.FILE_CHK_CODE IN ('M1012000-I','M1052000-I','M2042000-I','M2052000-I') ";
                string strSQLSub2 = " EXISTS ( " +
                                    "  SELECT 1 FROM " + TBL_RESULTTXT.TABLE_NAME(Schemabankcd) + " TXT " +
                                    "  WHERE " +
                                    "       TXT.FILE_NAME = CTL.FILE_NAME " +
                                    "   AND TXT.RET_CODE NOT IN ('M1012000-I','M1052000-I','M2042000-I','M2052001-I','M2052002-I') ) ";
                switch (ErrFlg)
                {
                    case 0:
                        strSql += wk + string.Format(" ( {0} AND NOT {1} ) ", strSQLSub1, strSQLSub2);
                        break;
                    case 9:
                        strSql += wk + string.Format(" ( NOT {0} OR {1} ) ", strSQLSub1, strSQLSub2);
                        break;
                }
                wk = " AND ";
            }

            // ORDER BY 
            strSql += " ORDER BY CTL.RECV_DATE DESC, CTL.RECV_TIME DESC ";

            //取得件数制御
            strSql = "SELECT * FROM ( " + strSql + " ) WHERE ROWNUM <= " + (ListDispLimit + 1) + " ";

            return strSql;
        }

        /// <summary>
        /// 結果照会明細一覧での検索SQL取得
        /// </summary>
        /// <returns></returns>
        public static string Get_SearchResultTxtData(string filename, int ErrFlg, int Schemabankcd)
        {
            string strSql = "SELECT * FROM " + TBL_RESULTTXT.TABLE_NAME(Schemabankcd);
            strSql += " WHERE FILE_NAME = '" + filename + "' ";

            if (ErrFlg == 0 || ErrFlg == 9)
            {
                // エラー設定がある場合
                string strSQLSub1 = " 'M1012000-I','M1052000-I','M2042000-I','M2052001-I','M2052002-I' ";
                switch (ErrFlg)
                {
                    case 0:
                        // エラーなし
                        strSql += string.Format(" AND RET_CODE IN({0}) ", strSQLSub1);
                        break;
                    case 9:
                        // エラーあり
                        strSql += string.Format(" AND RET_CODE NOT IN({0}) ", strSQLSub1);
                        break;
                }
            }

            return strSql;
        }

        /// <summary>
        /// 結果照会での再送対象データ取得SQL
        /// 持出アップロード（結果照会）
        /// </summary>
        /// <returns></returns>
        public static string Get_SearchResultDataBUB(int gymid, string imgfilename, int Schemabankcd, bool route)
        {
            string strSql =
                "SELECT MEIIMG.*";
            if (route)
            {
                strSql += ",BATCH.INPUT_ROUTE ";
            }
            strSql += " FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " MEIIMG ";
            if (route)
            {
                strSql += "  LEFT JOIN " +
                             TBL_TRBATCH.TABLE_NAME(Schemabankcd) + " BATCH " +
                          " ON " +
                          "       MEIIMG.GYM_ID = BATCH.GYM_ID " +
                          "   AND MEIIMG.OPERATION_DATE = BATCH.OPERATION_DATE " +
                          "   AND MEIIMG.SCAN_TERM = BATCH.SCAN_TERM " +
                          "   AND MEIIMG.BAT_ID = BATCH.BAT_ID ";
            }
            strSql += " WHERE " +
                            "MEIIMG.GYM_ID = " + gymid + "" +
                       " AND MEIIMG.IMG_FLNM = '" + imgfilename + "'";
            return strSql;
        }

        /// <summary>
        /// 持帰要求結果一覧SQL取得
        /// </summary>
        /// <returns></returns>
        public static string Get_SearchIcreqCtl(int ReqDate, int Schemabankcd, int ListDispLimit)
        {
            string strSql = "";
            strSql = "SELECT * FROM " + TBL_ICREQ_CTL.TABLE_NAME(Schemabankcd) + " ";
            if (ReqDate > -1)
            {
                strSql += " WHERE REQ_DATE = " + ReqDate;
            }
            // ORDER BY 
            strSql += " ORDER BY REQ_DATE DESC, REQ_TIME DESC";

            //取得件数制御
            strSql = "SELECT * FROM ( " + strSql + " ) WHERE ROWNUM <= " + (ListDispLimit) + " ";

            return strSql;
        }

        /// <summary>
        ///  訂正履歴照会項目名称取得
        /// </summary>
        /// <returns></returns>
        public static string Get_SearchTeiseiHistItemName(int Schemabankcd)
        {
            string strSql = "SELECT ITEM_NAME  FROM " + TBL_TRITEM_HIST.TABLE_NAME(Schemabankcd);
            // ORDER BY 
            strSql += " GROUP BY ITEM_NAME";
            // ORDER BY 
            strSql += " ORDER BY MAX(ITEM_ID)";

            return strSql;
        }

        /// <summary>
        /// 訂正履歴照会一覧取得
        /// </summary>
        /// <returns></returns>
        public static string Get_SearchTeiseiHistDataList(int gymid, int operationdate, int batid, int detailsno, int updatedate, 
                                                          int updatetime, int updatetime2, string itemname, string itemvalue, 
                                                          string imgflnm, int radioselect, int Schemabankcd, int ListDispLimit)
        {
            string strSql1 = "SELECT ITEMHIST.*  FROM " + TBL_TRITEM_HIST.TABLE_NAME(Schemabankcd) + " ITEMHIST ";
            StringBuilder sbSQL = new StringBuilder(strSql1);
            List<string> sqlWhere = new List<string>();
            sqlWhere.Add(" ITEMHIST.GYM_ID = " + gymid);
            if (operationdate > -1)
            {
                sqlWhere.Add(" ITEMHIST.OPERATION_DATE = " + operationdate);
            }
            if (batid > -1)
            {
                sqlWhere.Add(" ITEMHIST.BAT_ID = " + batid);
            }
            if (detailsno > -1)
            {
                sqlWhere.Add(" ITEMHIST.DETAILS_NO = " + detailsno);
            }
            if (updatedate > -1)
            {
                sqlWhere.Add(" ITEMHIST.UPDATE_DATE = " + updatedate);
            }
            if (updatetime > -1)
            {
                sqlWhere.Add(" ITEMHIST.UPDATE_TIME >= " + updatetime*1000);
            }
            if (updatetime2 > -1)
            {
                sqlWhere.Add(" ITEMHIST.UPDATE_TIME <= " + (updatetime2 * 1000 + 999));
            }
            if (itemname != "")
            {
                sqlWhere.Add(" ITEMHIST.ITEM_NAME = '" + itemname + "' ");
                if (itemvalue != "")
                {
                    sqlWhere.Add(" ITEMHIST.END_DATA = '" + itemvalue + "' ");
                }
            }
            if (imgflnm != "")
            {
                sqlWhere.Add(" EXISTS (" +
                    " SELECT 1 FROM " +
                    TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                    " WHERE " +
                    " TRMEIIMG.GYM_ID = " + "ITEMHIST.GYM_ID " + " AND " +
                    " TRMEIIMG.OPERATION_DATE = " + "ITEMHIST.OPERATION_DATE " + " AND " +
                    " TRMEIIMG.SCAN_TERM = " + "ITEMHIST.SCAN_TERM " + " AND " +
                     " TRMEIIMG.BAT_ID = " + "ITEMHIST.BAT_ID " + " AND " +
                    " TRMEIIMG.DETAILS_NO = " + "ITEMHIST.DETAILS_NO ");
                if (radioselect == 2)
                {
                    sqlWhere.Add(" TRMEIIMG.IMG_FLNM  = " + "'" + imgflnm + "'" + " )");
                }
                if (radioselect == 0)
                {
                    sqlWhere.Add(" TRMEIIMG.IMG_FLNM  LIKE " + "'" + imgflnm + "%'" + " )");
                }
                if (radioselect == 1)
                {
                    sqlWhere.Add(" TRMEIIMG.IMG_FLNM  LIKE " + "'%" + imgflnm + "'" + " )");
                }
            }
            if (sqlWhere.Count > 0)
            {
                sbSQL.Append(" WHERE ");
                sbSQL.Append(string.Join(" AND ", sqlWhere));
            }
            string strSql = sbSQL.ToString();

            // ORDER BY 
            strSql += " ORDER BY ITEMHIST.GYM_ID, ITEMHIST.OPERATION_DATE, ITEMHIST.SCAN_TERM, ITEMHIST.BAT_ID, ITEMHIST.DETAILS_NO, ITEMHIST.ITEM_ID, ITEMHIST.SEQ";

            //取得件数制御
            strSql = "SELECT * FROM ( " + strSql + " ) WHERE ROWNUM <= " + (ListDispLimit + 1) + " ";

            return strSql;

        }

        /// <summary>
        /// 結果照会での再送対象データ取得SQL
        /// 持出取消・証券データ訂正・不渡返還（結果照会）
        /// </summary>
        /// <returns></returns>
        public static string Get_SearchResultDataOther(int gymid, string fileName, int Schemabankcd, bool route)
        {
            string strSql =
                "SELECT MEI.*";
            if (route)
            {
                strSql += ",BATCH.INPUT_ROUTE ";
            }
            strSql += " FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " MEI ";
            if (route)
            {
                strSql += "  LEFT JOIN " +
                             TBL_TRBATCH.TABLE_NAME(Schemabankcd) + " BATCH " +
                          " ON " +
                          "       MEI.GYM_ID = BATCH.GYM_ID " +
                          "   AND MEI.OPERATION_DATE = BATCH.OPERATION_DATE " +
                          "   AND MEI.SCAN_TERM = BATCH.SCAN_TERM " +
                          "   AND MEI.BAT_ID = BATCH.BAT_ID ";
            }
            strSql += " WHERE " +
                      "  MEI.GYM_ID = " + gymid + " " +
                      " AND EXISTS ( " +
                      "  SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " MEIIMG " +
                      "  WHERE " +
                      "       MEIIMG.GYM_ID = MEI.GYM_ID " +
                      "   AND MEIIMG.OPERATION_DATE = MEI.OPERATION_DATE " +
                      "   AND MEIIMG.SCAN_TERM = MEI.SCAN_TERM " +
                      "   AND MEIIMG.BAT_ID = MEI.BAT_ID " +
                      "   AND MEIIMG.DETAILS_NO = MEI.DETAILS_NO " +
                      "   AND MEIIMG.IMG_FLNM = '" + fileName + "' " +
                      " ) ";
            return strSql;
        }

        /// <summary>
        /// 再送登録するUPDATE文を作成します
        /// 持出アップロード（結果照会）
        /// </summary>
        /// <returns></returns>
        public static string Get_UpdateSearchResultDataBUB(int gymid, string imgfilename, int Schemabankcd)
        {
            string strSQL = "UPDATE " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " SET " +
                TBL_TRMEIIMG.BUA_STS + "= 1 " +
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG.IMG_FLNM + "='" + imgfilename + "' ";
            return strSQL;
        }

        /// <summary>
        /// 再送登録するUPDATE文を作成します
        /// 持出取消・証券データ訂正・不渡返還（結果照会）
        /// </summary>
        /// <returns></returns>
        public static string Get_UpdateSearchResultDataOther(int gymid, string fileName, int Schemabankcd, string UpdateField)
        {
            string strSQL = "UPDATE " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " MEI " + " SET " + 
                      UpdateField + "= 1 " +
                      " WHERE " +
                      "  MEI.GYM_ID = " + gymid + " " +
                      " AND EXISTS ( " +
                      "  SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " MEIIMG " +
                      "  WHERE " +
                      "       MEIIMG.GYM_ID = MEI.GYM_ID " +
                      "   AND MEIIMG.OPERATION_DATE = MEI.OPERATION_DATE " +
                      "   AND MEIIMG.SCAN_TERM = MEI.SCAN_TERM " +
                      "   AND MEIIMG.BAT_ID = MEI.BAT_ID " +
                      "   AND MEIIMG.DETAILS_NO = MEI.DETAILS_NO " +
                      "   AND MEIIMG.IMG_FLNM = '" + fileName + "' " +
                      " ) ";
            return strSQL;
        }

        /// <summary>
        /// 結果ファイル単位での再送登録するUPDATE文を作成します
        /// 持出アップロード（結果照会）
        /// </summary>
        /// <returns></returns>
        public static string Get_UpdateSearchResultFileBUB(int gymid, string imgarchname, int Schemabankcd)
        {
            string strSQL = "UPDATE " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " SET " +
                TBL_TRMEIIMG.BUA_STS + "= " + TrMei.Sts.再作成対象 + " " +
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG.IMG_ARCH_NAME + "='" + imgarchname + "' AND " +
                TBL_TRMEIIMG.BUA_STS + "=" + TrMei.Sts.アップロード + " ";
            return strSQL;
        }

        /// <summary>
        /// 結果ファイル単位での再送登録するUPDATE文を作成します
        /// 持出取消・証券データ訂正・不渡返還（結果照会）
        /// </summary>
        /// <returns></returns>
        public static string Get_UpdateSearchResultFileOther(int gymid, string procfileName, int Schemabankcd, string UpdateField)
        {
            string strSQL = "UPDATE " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " MEI " + " SET " +
                      UpdateField + "= " + TrMei.Sts.再作成対象 + " " +
                      " WHERE " +
                      "     MEI.GYM_ID = " + gymid + " " +
                      " AND MEI." + UpdateField + " = " + TrMei.Sts.アップロード + " " +
                      " AND EXISTS ( " +
                      "  SELECT 1 FROM " + TBL_SEND_FILE_TRMEI.TABLE_NAME(Schemabankcd) + " SENDMEI " +
                      "  WHERE " +
                      "       SENDMEI.GYM_ID = MEI.GYM_ID " +
                      "   AND SENDMEI.OPERATION_DATE = MEI.OPERATION_DATE " +
                      "   AND SENDMEI.SCAN_TERM = MEI.SCAN_TERM " +
                      "   AND SENDMEI.BAT_ID = MEI.BAT_ID " +
                      "   AND SENDMEI.DETAILS_NO = MEI.DETAILS_NO " +
                      "   AND SENDMEI.SEND_FILE_NAME = '" + procfileName + "' " +
                      " ) ";
            return strSQL;
        }

        /// <summary>
        /// 交換尻照会での検索SQL取得
        /// </summary>
        /// <returns></returns>
        public static string Get_SearchBalanceCtl(string fileid, string bkno, int date, int Schemabankcd)
        {
            string strSql = "SELECT TXT.* FROM " + TBL_BALANCETXT.TABLE_NAME(Schemabankcd) + " TXT " +
                      " WHERE " +
                      "  EXISTS ( " +
                      "   SELECT 1 FROM " + TBL_BALANCETXT_CTL.TABLE_NAME(Schemabankcd) + " CTL " +
                      "   WHERE " +
                      "        CTL.FILE_NAME = TXT.FILE_NAME " +
                      "    AND CTL.FILE_ID = '" + fileid + "' " +
                      "    AND CTL.BK_NO = '" + bkno + "' " +
                      "    AND CTL.CLEARING_DATE = " + date + " " +
                      "  ) " +
                      "  ORDER BY TXT.BK_NO ";
            return strSql;
        }


        // *******************************************************************
        // E0507　持出支店別合計票照会
        // *******************************************************************

        /// <summary>
        /// 呼出画面：E0507　持出支店別合計票照会
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：SELECT句のレコードを一時テーブルに登録する
        /// </summary>
        /// <param name="strSELECT"></param>
        /// <param name="dstTableName"></param>
        /// <param name="allColumns"></param>
        /// <returns></returns>
        public static string GetInsertTmpTable(string strSELECT, string dstTableName, string allColumns)
        {
            string strSQL = "";
            strSQL += " INSERT INTO " + dstTableName + " ";
            strSQL += "     ( " + allColumns + " ) ";
            strSQL += strSELECT;
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0507　持出支店別合計票照会
        /// 処理内容：一時テーブル作成クエリ（BR_TOTAL）
        /// </summary>
        /// <returns></returns>
        public static string GetCreateTMP_BRTOTAL(string tableName)
        {
            string strSQL = "";
            strSQL += " CREATE GLOBAL TEMPORARY TABLE " + tableName + " ( ";
            // BR_TOTAL 項目
            strSQL += "     GYM_ID number(3,0) NOT NULL, ";
            strSQL += "     OPERATION_DATE number(8,0) NOT NULL, ";
            strSQL += "     SCAN_IMG_FLNM varchar2(100) NOT NULL, ";
            strSQL += "     IMPORT_IMG_FLNM varchar2(62), ";
            strSQL += "     BK_NO number(4,0) default -1  NOT NULL, ";
            strSQL += "     BR_NO number(4,0) default -1  NOT NULL, ";
            strSQL += "     SCAN_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     SCAN_BR_NO number(4,0) default -1  NOT NULL, ";
            strSQL += "     TOTAL_COUNT number(6,0), ";
            strSQL += "     TOTAL_AMOUNT number(18,0), ";
            strSQL += "     STATUS number(1,0) default 0  NOT NULL, ";
            strSQL += "     LOCK_TERM varchar2(20), ";
            // 追加項目
            strSQL += "     MEI_COUNT number(38,0) default 0  NOT NULL, ";
            strSQL += "     MEI_AMOUNT number(38,0) default 0  NOT NULL, ";
            strSQL += "     DIFF_COUNT number(38,0) default 0  NOT NULL, ";
            strSQL += "     DIFF_AMOUNT number(38,0) default 0  NOT NULL, ";
            strSQL += "     NOT_TOTAL number(1,0) default 0  NOT NULL, ";
            // 主キー
            strSQL += " PRIMARY KEY ( ";
            strSQL += "      GYM_ID ";
            strSQL += "     ,OPERATION_DATE ";
            strSQL += "     ,SCAN_IMG_FLNM ";
            strSQL += " )) ";
            strSQL += " ON COMMIT DELETE ROWS ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0507　持出支店別合計票照会
        /// 処理内容：支店別合計票取得１
        /// </summary>
        /// <param name="GymId"></param>
        /// <param name="BankCd"></param>
        /// <param name="ScanDate"></param>
        /// <param name="OcBrCd"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string OcBrTotalViewSelectBatList1(int GymId, int BankCd, int ScanDate, int OcBrCd, int SchemaBankCD)
        {
            // スキャン日
            string strScanDate = "";
            if (ScanDate != -1)
            {
                strScanDate = string.Format("    AND BRT.SCAN_DATE = {0} ", ScanDate);
            }

            // 持出支店コード
            string strOcBrCd = "";
            if (OcBrCd != -1)
            {
                strOcBrCd = string.Format("    AND BRT.BR_NO = {0} ", OcBrCd);
            }

            // 抽出条件を指定してBR_TOTAL の全項目を取得する
            string strSQL = "";
            strSQL += " SELECT ";
            strSQL += "      BRT.* ";
            strSQL += " FROM ";
            strSQL += "     " + TBL_BR_TOTAL.TABLE_NAME + " BRT ";
            strSQL += " WHERE ";
            strSQL += "         BRT.GYM_ID = " + GymId + " ";
            strSQL += "     AND BRT.BK_NO = " + BankCd + " ";
            strSQL += "     AND BRT.STATUS = " + (int)TBL_BR_TOTAL.enumStatus.Complete + " ";
            strSQL += strScanDate;
            strSQL += strOcBrCd;

            // ここでは ORDER BY 不要
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0507　持出支店別合計票照会
        /// 処理内容：支店別合計票取得１(支店別合計票支店マスタにない支店データ削除)
        /// </summary>
        /// <param name="GymId"></param>
        /// <param name="BankCd"></param>
        /// <param name="ScanDate"></param>
        /// <param name="OcBrCd"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string OcBrTotalViewDeleteBtBranch(string dstTableName, int SchemaBankCD)
        {
            // BT_BRANCHMFにない支店データを削除
            string strSQL = "";
            strSQL += " DELETE FROM " + dstTableName + " DST ";
            strSQL += " WHERE ";
            strSQL += "     NOT EXISTS ( ";
            strSQL += "          SELECT 1 FROM " + V_BT_BRANCHMF.TABLE_NAME(SchemaBankCD) + " BRANCH ";
            strSQL += "          WHERE ";
            strSQL += "               BRANCH.BR_NO = DST.BR_NO ";
            strSQL += "         ) ";

            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0507　持出支店別合計票照会
        /// 処理内容：支店別合計票取得１(支店別合計票支店マスタデータ)
        /// </summary>
        /// <param name="GymId"></param>
        /// <param name="BankCd"></param>
        /// <param name="ScanDate"></param>
        /// <param name="OcBrCd"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string OcBrTotalViewSelectBatList1BtBranch(string dstTableName, int GymId, int BankCd, int ScanDate, int OcBrCd, int SchemaBankCD)
        {
            // BT_BRANCHMFの支店データを取得
            string strSQL = "";
            strSQL += " SELECT ";
            strSQL += "     " + GymId + " GYM_ID ";
            strSQL += "   , " + ScanDate + " OPERATION_DATE ";
            strSQL += "   , TO_CHAR(BRANCH.BR_NO) SCAN_IMG_FLNM ";
            strSQL += "   , ' ' IMPORT_IMG_FLNM ";
            strSQL += "   , " + BankCd + " BK_NO ";
            strSQL += "   , BRANCH.BR_NO ";
            strSQL += "   , " + ScanDate + " SCAN_DATE ";
            strSQL += "   , BRANCH.BR_NO SCAN_BR_NO ";
            strSQL += "   , NULL TOTAL_COUNT ";
            strSQL += "   , NULL TOTAL_AMOUNT ";
            strSQL += "   , " + (int)TBL_BR_TOTAL.enumStatus.Complete + " STATUS ";
            strSQL += "   , '' LOCK_TERM ";
            strSQL += "   , 1 NOT_TOTAL ";
            strSQL += " FROM ";
            strSQL += "     " + V_BT_BRANCHMF.TABLE_NAME(SchemaBankCD) + " BRANCH ";
            strSQL += " WHERE ";
            strSQL += "     NOT EXISTS ( ";
            strSQL += "          SELECT 1 FROM " + dstTableName + " DST ";
            strSQL += "          WHERE ";
            strSQL += "               DST.BR_NO = BRANCH.BR_NO ";
            strSQL += "         ) ";
            if (OcBrCd != -1)
            {
                strSQL += string.Format("    AND BRANCH.BR_NO = {0} ", OcBrCd);
            }

            // ここでは ORDER BY 不要
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0507　持出支店別合計票照会
        /// 処理内容：支店別合計票取得２
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="TotalUmu"></param>
        /// <param name="SaMaisu"></param>
        /// <param name="SaKingaku"></param>
        /// <param name="ListDispLimit"></param>
        /// <returns></returns>
        public static string OcBrTotalViewSelectBatList2(string tmpTableName, int TotalUmu, int SaMaisu, long SaKingaku, int ListDispLimit)
        {
            // 支店別合計票有無
            string strTOTALUMU = "";
            switch (TotalUmu)
            {
                case TOTAL_ARI:
                    strTOTALUMU = string.Format(" BRT.TOTAL_COUNT IS NOT NULL ");
                    break;
                case TOTAL_NASHI:
                    strTOTALUMU = string.Format(" BRT.TOTAL_COUNT IS NULL ");
                    break;
            }

            // 差枚数
            string strSA_MAISU = "";
            switch (SaMaisu)
            {
                case DIFF_NASHI:
                    strSA_MAISU = string.Format(" BRT.DIFF_COUNT = 0 ");
                    break;
                case DIFF_ARI:
                    strSA_MAISU = string.Format(" BRT.DIFF_COUNT <> 0 ");
                    break;
            }

            // 差金額
            string strSA_KINGAKU = "";
            switch (SaKingaku)
            {
                case DIFF_NASHI:
                    strSA_KINGAKU = string.Format(" BRT.DIFF_AMOUNT = 0 ");
                    break;
                case DIFF_ARI:
                    strSA_KINGAKU = string.Format(" BRT.DIFF_AMOUNT <> 0 ");
                    break;
            }

            // WHERE句
            string sep = "";
            string strWHERE = "";
            if (!string.IsNullOrEmpty(strTOTALUMU))
            {
                strWHERE += sep + strTOTALUMU;
                sep = " AND ";
            }
            if (!string.IsNullOrEmpty(strSA_MAISU))
            {
                strWHERE += sep + strSA_MAISU;
                sep = " AND ";
            }
            if (!string.IsNullOrEmpty(strSA_KINGAKU))
            {
                strWHERE += sep + strSA_KINGAKU;
                sep = " AND ";
            }
            if (!string.IsNullOrEmpty(strWHERE))
            {
                strWHERE = " WHERE " + strWHERE;
            }

            // FROM句
            string strFROM = "";
            strFROM += " SELECT * FROM ";
            strFROM += "    " + tmpTableName + " BRT ";
            strFROM += strWHERE;
            strFROM += " ORDER BY ";
            strFROM += "     BRT.GYM_ID, BRT.SCAN_DATE, BRT.BR_NO ";

            // Oracle は ORDER BY したものに対して ROWNUM で絞る
            string strSQL = "";
            strSQL += " SELECT * FROM (" + strFROM + ")";
            strSQL += " WHERE ";
            strSQL += "     ROWNUM <= " + (ListDispLimit + 1);
            return strFROM;
        }

        /// <summary>
        /// 呼出画面：E0507　持出支店別合計票照会
        /// 処理内容：合計票集計１
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string OcBrTotalViewUpdateTmpBatchList1(string tmpTableName, int SchemaBankCD)
        {
            // 明細枚数
            string strMEI_COUNT = "";
            strMEI_COUNT += " (   SELECT ";
            strMEI_COUNT += "         COUNT(*) ";
            strMEI_COUNT += "     FROM ";
            strMEI_COUNT += "         " + tmpTableName + " TMP ";
            strMEI_COUNT += "         INNER JOIN " + TBL_TRBATCH.TABLE_NAME(SchemaBankCD) + " BAT ";
            strMEI_COUNT += "             ON  BAT.GYM_ID = TMP.GYM_ID ";
            strMEI_COUNT += "             AND BAT.OC_BK_NO = TMP.BK_NO ";
            // 2022.05.04 条件変更 持出店⇒スキャン店
            //strMEI_COUNT += "             AND BAT.OC_BR_NO = TMP.BR_NO ";
            strMEI_COUNT += "             AND BAT.SCAN_BR_NO = TMP.SCAN_BR_NO ";
            strMEI_COUNT += "             AND BAT.SCAN_DATE = TMP.SCAN_DATE ";
            strMEI_COUNT += "             AND BAT.INPUT_ROUTE <> " + TrBatch.InputRoute.期日管理 + " ";
            strMEI_COUNT += "             AND BAT.DELETE_FLG = 0 ";
            strMEI_COUNT += "         INNER JOIN " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strMEI_COUNT += "             ON MEI.GYM_ID = BAT.GYM_ID ";
            strMEI_COUNT += "             AND MEI.OPERATION_DATE = BAT.OPERATION_DATE ";
            strMEI_COUNT += "             AND MEI.SCAN_TERM = BAT.SCAN_TERM ";
            strMEI_COUNT += "             AND MEI.BAT_ID = BAT.BAT_ID ";
            strMEI_COUNT += "             AND MEI.DELETE_FLG = 0 ";
            strMEI_COUNT += "         INNER JOIN " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " ITEM ";
            strMEI_COUNT += "             ON  ITEM.GYM_ID = MEI.GYM_ID ";
            strMEI_COUNT += "             AND ITEM.OPERATION_DATE = MEI.OPERATION_DATE ";
            strMEI_COUNT += "             AND ITEM.SCAN_TERM = MEI.SCAN_TERM ";
            strMEI_COUNT += "             AND ITEM.BAT_ID = MEI.BAT_ID ";
            strMEI_COUNT += "             AND ITEM.DETAILS_NO = MEI.DETAILS_NO ";
            strMEI_COUNT += "             AND ITEM.ITEM_ID = " + DspItem.ItemId.金額 + " ";
            strMEI_COUNT += "     WHERE ";
            strMEI_COUNT += "             TMP.GYM_ID = BRT.GYM_ID ";
            strMEI_COUNT += "         AND TMP.OPERATION_DATE = BRT.OPERATION_DATE ";
            strMEI_COUNT += "         AND TMP.SCAN_IMG_FLNM = BRT.SCAN_IMG_FLNM ";
            strMEI_COUNT += "     GROUP BY ";
            strMEI_COUNT += "          TMP.GYM_ID ";
            strMEI_COUNT += "         ,TMP.BK_NO ";
            strMEI_COUNT += "         ,TMP.BR_NO ";
            strMEI_COUNT += "         ,TMP.SCAN_DATE ";
            strMEI_COUNT += " ) ";

            // 明細金額
            string strMEI_AMOUNT = "";
            strMEI_AMOUNT += " (   SELECT ";
            strMEI_AMOUNT += "         SUM(TO_NUMBER(ITEM.END_DATA)) ";
            strMEI_AMOUNT += "     FROM ";
            strMEI_AMOUNT += "         " + tmpTableName + " TMP ";
            strMEI_AMOUNT += "         INNER JOIN " + TBL_TRBATCH.TABLE_NAME(SchemaBankCD) + " BAT ";
            strMEI_AMOUNT += "             ON  BAT.GYM_ID = TMP.GYM_ID ";
            strMEI_AMOUNT += "             AND BAT.OC_BK_NO = TMP.BK_NO ";
            // 2022.05.04 条件変更 持出店⇒スキャン店
            // strMEI_AMOUNT += "             AND BAT.OC_BR_NO = TMP.BR_NO ";
            strMEI_AMOUNT += "             AND BAT.SCAN_BR_NO = TMP.SCAN_BR_NO ";
            strMEI_AMOUNT += "             AND BAT.SCAN_DATE = TMP.SCAN_DATE ";
            strMEI_AMOUNT += "             AND BAT.INPUT_ROUTE <> " + TrBatch.InputRoute.期日管理 + " ";
            strMEI_AMOUNT += "             AND BAT.DELETE_FLG = 0 ";
            strMEI_AMOUNT += "         INNER JOIN " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strMEI_AMOUNT += "             ON MEI.GYM_ID = BAT.GYM_ID ";
            strMEI_AMOUNT += "             AND MEI.OPERATION_DATE = BAT.OPERATION_DATE ";
            strMEI_AMOUNT += "             AND MEI.SCAN_TERM = BAT.SCAN_TERM ";
            strMEI_AMOUNT += "             AND MEI.BAT_ID = BAT.BAT_ID ";
            strMEI_AMOUNT += "             AND MEI.DELETE_FLG = 0 ";
            strMEI_AMOUNT += "         INNER JOIN " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " ITEM ";
            strMEI_AMOUNT += "             ON  ITEM.GYM_ID = MEI.GYM_ID ";
            strMEI_AMOUNT += "             AND ITEM.OPERATION_DATE = MEI.OPERATION_DATE ";
            strMEI_AMOUNT += "             AND ITEM.SCAN_TERM = MEI.SCAN_TERM ";
            strMEI_AMOUNT += "             AND ITEM.BAT_ID = MEI.BAT_ID ";
            strMEI_AMOUNT += "             AND ITEM.DETAILS_NO = MEI.DETAILS_NO ";
            strMEI_AMOUNT += "             AND ITEM.ITEM_ID = " + DspItem.ItemId.金額 + " ";
            strMEI_AMOUNT += "     WHERE ";
            strMEI_AMOUNT += "             TMP.GYM_ID = BRT.GYM_ID ";
            strMEI_AMOUNT += "         AND TMP.OPERATION_DATE = BRT.OPERATION_DATE ";
            strMEI_AMOUNT += "         AND TMP.SCAN_IMG_FLNM = BRT.SCAN_IMG_FLNM ";
            strMEI_AMOUNT += "     GROUP BY ";
            strMEI_AMOUNT += "          TMP.GYM_ID ";
            strMEI_AMOUNT += "         ,TMP.BK_NO ";
            strMEI_AMOUNT += "         ,TMP.BR_NO ";
            strMEI_AMOUNT += "         ,TMP.SCAN_DATE ";
            strMEI_AMOUNT += " ) ";

            // 一時テーブル更新
            string strSQL = "";
            strSQL += " UPDATE " + tmpTableName + " BRT ";
            strSQL += " SET ";
            strSQL += "      MEI_COUNT = NVL(" + strMEI_COUNT + ",0) ";
            strSQL += "     ,MEI_AMOUNT = NVL(" + strMEI_AMOUNT + ",0) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0507　持出支店別合計票照会
        /// 処理内容：合計票集計２
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <returns></returns>
        public static string OcBrTotalViewUpdateTmpBatchList2(string tmpTableName)
        {
            string strSQL = "";
            strSQL += " UPDATE " + tmpTableName + " BRT ";
            strSQL += " SET ";
            strSQL += "      DIFF_COUNT = (NVL(TOTAL_COUNT,0) - NVL(MEI_COUNT,0)) ";
            strSQL += "     ,DIFF_AMOUNT = (NVL(TOTAL_AMOUNT,0) - NVL(MEI_AMOUNT,0)) ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0507　持出支店別合計票照会
        /// 処理内容：入力完了の合計票を取得する
        /// </summary>
        /// <returns></returns>
        public static string OcBrTotalViewSelectTrBatchQuery(int gymid, int opedate, string imgname)
        {
            string strSQL = "";
            strSQL += " SELECT * FROM " + TBL_BR_TOTAL.TABLE_NAME + " BRT ";
            strSQL += " WHERE ";
            strSQL += "         BRT.GYM_ID = " + gymid + " ";
            strSQL += "     AND BRT.OPERATION_DATE = " + opedate + " ";
            strSQL += "     AND BRT.SCAN_IMG_FLNM = '" + imgname + "' ";
            strSQL += "     AND BRT.STATUS = " + (int)TBL_BR_TOTAL.enumStatus.Complete + " ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0507　持出支店別合計票照会
        /// 処理内容：合計票ステータスを入力完了に更新する
        /// </summary>
        /// <returns></returns>
        public static string OcBrTotalViewUpdateTrBatchStatusCompQuery(TBL_BR_TOTAL brt)
        {
            string strSQL = "";
            strSQL += " UPDATE " + TBL_BR_TOTAL.TABLE_NAME + " BRT ";
            strSQL += " SET ";
            strSQL += "      STATUS = " + (int)TBL_BR_TOTAL.enumStatus.Complete + " ";
            strSQL += "     ,LOCK_TERM = NULL ";
            strSQL += " WHERE ";
            strSQL += "         BRT.GYM_ID = " + brt._GYM_ID + " ";
            strSQL += "     AND BRT.OPERATION_DATE = " + brt._OPERATION_DATE + " ";
            strSQL += "     AND BRT.SCAN_IMG_FLNM = '" + brt._SCAN_IMG_FLNM + "' ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0507　持出支店別合計票照会
        /// 処理内容：BR_TOTAL を削除する
        /// </summary>
        /// <returns></returns>
        public static string OcBrTotalViewDeleteTrBatchQuery(TBL_BR_TOTAL brt)
        {
            // TRBATCH 更新
            string strSQL = "";
            strSQL += " UPDATE " + TBL_BR_TOTAL.TABLE_NAME + " BRT ";
            strSQL += " SET ";
            strSQL += "      STATUS = " + (int)TBL_BR_TOTAL.enumStatus.Delete + " ";
            strSQL += "     ,LOCK_TERM = NULL ";
            strSQL += " WHERE ";
            strSQL += "         BRT.GYM_ID = " + brt._GYM_ID + " ";
            strSQL += "     AND BRT.OPERATION_DATE = " + brt._OPERATION_DATE + " ";
            strSQL += "     AND BRT.SCAN_IMG_FLNM = '" + brt._SCAN_IMG_FLNM + "' ";
            return strSQL;
        }


        // *******************************************************************
        // E0508　持出バッチ照会
        // *******************************************************************
        /// <summary>バッチ補正状態：入力待ち</summary>
        public const int BAT_STS_WAIT = 1;
        /// <summary>バッチ補正状態：入力中</summary>
        public const int BAT_STS_INPUT = 2;
        /// <summary>バッチ補正状態：入力完了</summary>
        public const int BAT_STS_COMP = 10;

        /// <summary>照合：OK</summary>
        public const int DIFF_RESULT_OK = 0;
        /// <summary>照合：NG</summary>
        public const int DIFF_RESULT_NG = 1;

        /// <summary>差分：なし</summary>
        public const int DIFF_NASHI = 0;
        /// <summary>差分：あり</summary>
        public const int DIFF_ARI = 9;

        /// <summary>支店別合計票：あり</summary>
        public const int TOTAL_ARI = 0;
        /// <summary>支店別合計票：なし</summary>
        public const int TOTAL_NASHI = 9;

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：一時テーブル作成クエリ（TRBATCH）
        /// </summary>
        /// <returns></returns>
        public static string GetCreateTMP_TRBATCH(string tableName)
        {
           string strSQL = "";
            strSQL += " CREATE GLOBAL TEMPORARY TABLE " + tableName + " ( ";
            // TRBATCH 項目
            strSQL += "     GYM_ID number(3,0) NOT NULL, ";
            strSQL += "     OPERATION_DATE number(8,0) NOT NULL, ";
            strSQL += "     SCAN_TERM varchar2(20) NOT NULL, ";
            strSQL += "     BAT_ID number(6,0) NOT NULL, ";
            strSQL += "     STS number(2,0) default 0  NOT NULL, ";
            strSQL += "     INPUT_ROUTE number(1,0) default 0  NOT NULL, ";
            strSQL += "     OC_BK_NO number(4,0) default -1  NOT NULL, ";
            strSQL += "     OC_BR_NO number(4,0) default -1  NOT NULL, ";
            strSQL += "     SCAN_BR_NO number(4,0) default -1  NOT NULL, ";
            strSQL += "     SCAN_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     CLEARING_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     SCAN_COUNT number(6,0) default 0  NOT NULL, ";
            strSQL += "     TOTAL_COUNT number(6,0) default 0  NOT NULL, ";
            strSQL += "     TOTAL_AMOUNT number(18,0) default 0  NOT NULL, ";
            strSQL += "     DELETE_DATE number(8,0) default 0  NOT NULL, ";
            strSQL += "     DELETE_FLG number(1,0) default 0  NOT NULL, ";
            strSQL += "     E_TERM varchar2(20), ";
            strSQL += "     E_OPENO varchar2(20), ";
            strSQL += "     E_YMD number(8,0) default 0  NOT NULL, ";
            strSQL += "     E_TIME number(9,0) default 0  NOT NULL, ";
            // 追加項目
            strSQL += "     MEI_COUNT number(38,0) default 0 , ";
            strSQL += "     MEI_AMOUNT number(38,0) default 0 , ";
            strSQL += "     DIFF_COUNT number(38,0) default 0 , ";
            strSQL += "     DIFF_AMOUNT number(38,0) default 0 , ";
            strSQL += "     STS_WAIT_COUNT number(6,0) default 0 , ";
            strSQL += "     STS_COMP_COUNT number(6,0) default 0 , ";
            strSQL += "     STS_INPT_COUNT number(6,0) default 0 , ";
            strSQL += "     TOTAL_STATUS number(4,0) default 0 , ";
            strSQL += "     DIFF_RESULT number(2,0) default 0 , ";
            // 主キー
            strSQL += " PRIMARY KEY ( ";
            strSQL += "      GYM_ID ";
            strSQL += "     ,OPERATION_DATE ";
            strSQL += "     ,SCAN_TERM ";
            strSQL += "     ,BAT_ID ";
            strSQL += " )) ";
            strSQL += " ON COMMIT DELETE ROWS ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：バッチ一覧取得１
        /// </summary>
        /// <param name="ImportDate"></param>
        /// <param name="ScanBrCd"></param>
        /// <param name="ScanDate"></param>
        /// <param name="OcBrCd"></param>
        /// <param name="ClearlingDate"></param>
        /// <param name="BatCnt"></param>
        /// <param name="BatKingaku"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetOcBatchViewSelectBatList1(int ImportDate, int ScanBrCd, int ScanDate, int OcBrCd, int ClearlingDate, int BatCnt, long BatKingaku, int SchemaBankCD)
        {
            // 取込日
            string strImportDate = "";
            if (ImportDate != -1)
            {
                strImportDate = string.Format("    AND BAT.OPERATION_DATE = {0} ", ImportDate);
            }

            // スキャン支店
            string strScanBrCd = "";
            if (ScanBrCd != -1)
            {
                strScanBrCd = string.Format("    AND BAT.SCAN_BR_NO = {0} ", ScanBrCd);
            }

            // スキャン日
            string strScanDate = "";
            if (ScanDate != -1)
            {
                strScanDate = string.Format("    AND BAT.SCAN_DATE = {0} ", ScanDate);
            }

            // 持出支店コード
            string strOcBrCd = "";
            if (OcBrCd != -1)
            {
                strOcBrCd = string.Format("    AND BAT.OC_BR_NO = {0} ", OcBrCd);
            }

            // 交換希望日
            string strClearlingDate = "";
            if (ClearlingDate != -1)
            {
                strClearlingDate = string.Format("    AND BAT.CLEARING_DATE = {0} ", ClearlingDate);
            }

            // バッチ枚数
            string strBatCnt = "";
            if (BatCnt != -1)
            {
                strBatCnt = string.Format("    AND BAT.TOTAL_COUNT = {0} ", BatCnt);
            }

            // バッチ金額
            string strBatKingaku = "";
            if (BatKingaku != -1)
            {
                strBatKingaku = string.Format("    AND BAT.TOTAL_AMOUNT = {0} ", BatKingaku);
            }

            string strSELECT = "";
            strSELECT += " SELECT " + TBL_TRBATCH.ALL_COLUMNS + " FROM ";
            strSELECT += "    " + TBL_TRBATCH.TABLE_NAME(SchemaBankCD) + " BAT ";
            strSELECT += " WHERE ";
            strSELECT += "        BAT.GYM_ID = " + GymParam.GymId.持出 + " ";
            strSELECT += "    AND BAT.DELETE_FLG = 0 ";
            strSELECT += strImportDate;
            strSELECT += strScanBrCd;
            strSELECT += strScanDate;
            strSELECT += strOcBrCd;
            strSELECT += strClearlingDate;
            strSELECT += strBatCnt;
            strSELECT += strBatKingaku;

            // ここでは ORDER BY 不要
            return strSELECT;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：バッチ一覧取得２
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SaMaisu"></param>
        /// <param name="SaKingaku"></param>
        /// <param name="Status"></param>
        /// <param name="ListDispLimit"></param>
        /// <returns></returns>
        public static string GetOcBatchViewSelectBatList2(string tmpTableName, int SaMaisu, long SaKingaku, int Status, int ListDispLimit)
        {
            // 差枚数
            string strSA_MAISU = "";
            switch (SaMaisu)
            {
                case DIFF_NASHI:
                    strSA_MAISU = string.Format(" BAT.DIFF_COUNT = 0 ");
                    break;
                case DIFF_ARI:
                    strSA_MAISU = string.Format(" BAT.DIFF_COUNT <> 0 ");
                    break;
            }

            // 差金額
            string strSA_KINGAKU = "";
            switch (SaKingaku)
            {
                case DIFF_NASHI:
                    strSA_KINGAKU = string.Format(" BAT.DIFF_AMOUNT = 0 ");
                    break;
                case DIFF_ARI:
                    strSA_KINGAKU = string.Format(" BAT.DIFF_AMOUNT <> 0 ");
                    break;
            }

            // 状態
            string strSTATUS = "";
            if (Status != -1)
            {
                strSTATUS = string.Format(" BAT.TOTAL_STATUS = {0} ", Status);
            }

            // WHERE句
            string sep = "";
            string strWHERE = "";
            if (!string.IsNullOrEmpty(strSA_MAISU))
            {
                strWHERE += sep + strSA_MAISU;
                sep = " AND ";
            }
            if (!string.IsNullOrEmpty(strSA_KINGAKU))
            {
                strWHERE += sep + strSA_KINGAKU;
                sep = " AND ";
            }
            if (!string.IsNullOrEmpty(strSTATUS))
            {
                strWHERE += sep + strSTATUS;
                sep = " AND ";
            }
            if (!string.IsNullOrEmpty(strWHERE))
            {
                strWHERE = " WHERE " + strWHERE;
            }

            // FROM句
            string strFROM = "";
            strFROM += " SELECT * FROM ";
            strFROM += "    " + tmpTableName + " BAT ";
            strFROM += strWHERE;
            strFROM += " ORDER BY ";
            strFROM += "     BAT.OPERATION_DATE, BAT.BAT_ID ";

            // Oracle は ORDER BY したものに対して ROWNUM で絞る
            string strSQL = "";
            strSQL += " SELECT * FROM (" + strFROM + ")";
            strSQL += " WHERE ";
            strSQL += "     ROWNUM <= " + (ListDispLimit + 1);
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：バッチデータ集計１
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetOcBatchViewUpdateTmpBatchList1(string tmpTableName, int SchemaBankCD)
        {
            // 明細枚数
            string strMEI_COUNT = "";
            strMEI_COUNT += " (   SELECT ";
            strMEI_COUNT += "         COUNT(1) ";
            strMEI_COUNT += "     FROM ";
            strMEI_COUNT += "         " + tmpTableName + " TMP ";
            strMEI_COUNT += "         INNER JOIN " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strMEI_COUNT += "             ON  MEI.GYM_ID = TMP.GYM_ID ";
            strMEI_COUNT += "             AND MEI.OPERATION_DATE = TMP.OPERATION_DATE ";
            strMEI_COUNT += "             AND MEI.SCAN_TERM = TMP.SCAN_TERM ";
            strMEI_COUNT += "             AND MEI.BAT_ID = TMP.BAT_ID ";
            strMEI_COUNT += "             AND MEI.DELETE_FLG = 0 ";
            strMEI_COUNT += "         INNER JOIN " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " ITEM ";
            strMEI_COUNT += "             ON  ITEM.GYM_ID = MEI.GYM_ID ";
            strMEI_COUNT += "             AND ITEM.OPERATION_DATE = MEI.OPERATION_DATE ";
            strMEI_COUNT += "             AND ITEM.SCAN_TERM = MEI.SCAN_TERM ";
            strMEI_COUNT += "             AND ITEM.BAT_ID = MEI.BAT_ID ";
            strMEI_COUNT += "             AND ITEM.DETAILS_NO = MEI.DETAILS_NO ";
            strMEI_COUNT += "             AND ITEM.ITEM_ID = " + DspItem.ItemId.金額 + " ";
            strMEI_COUNT += "     WHERE  ";
            strMEI_COUNT += "             TMP.GYM_ID = BAT.GYM_ID ";
            strMEI_COUNT += "         AND TMP.OPERATION_DATE = BAT.OPERATION_DATE ";
            strMEI_COUNT += "         AND TMP.SCAN_TERM = BAT.SCAN_TERM ";
            strMEI_COUNT += "         AND TMP.BAT_ID = BAT.BAT_ID ";
            strMEI_COUNT += "     GROUP BY ";
            strMEI_COUNT += "          MEI.GYM_ID ";
            strMEI_COUNT += "         ,MEI.OPERATION_DATE ";
            strMEI_COUNT += "         ,MEI.SCAN_TERM ";
            strMEI_COUNT += "         ,MEI.BAT_ID ";
            strMEI_COUNT += " ) ";

            // 明細金額
            string strMEI_AMOUNT = "";
            strMEI_AMOUNT += " (   SELECT ";
            strMEI_AMOUNT += "         SUM(TO_NUMBER(ITEM.END_DATA)) ";
            strMEI_AMOUNT += "     FROM ";
            strMEI_AMOUNT += "         " + tmpTableName + " TMP ";
            strMEI_AMOUNT += "         INNER JOIN " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strMEI_AMOUNT += "             ON  MEI.GYM_ID = TMP.GYM_ID ";
            strMEI_AMOUNT += "             AND MEI.OPERATION_DATE = TMP.OPERATION_DATE ";
            strMEI_AMOUNT += "             AND MEI.SCAN_TERM = TMP.SCAN_TERM ";
            strMEI_AMOUNT += "             AND MEI.BAT_ID = TMP.BAT_ID ";
            strMEI_AMOUNT += "             AND MEI.DELETE_FLG = 0 ";
            strMEI_AMOUNT += "         INNER JOIN " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " ITEM ";
            strMEI_AMOUNT += "             ON  ITEM.GYM_ID = MEI.GYM_ID ";
            strMEI_AMOUNT += "             AND ITEM.OPERATION_DATE = MEI.OPERATION_DATE ";
            strMEI_AMOUNT += "             AND ITEM.SCAN_TERM = MEI.SCAN_TERM ";
            strMEI_AMOUNT += "             AND ITEM.BAT_ID = MEI.BAT_ID ";
            strMEI_AMOUNT += "             AND ITEM.DETAILS_NO = MEI.DETAILS_NO ";
            strMEI_AMOUNT += "             AND ITEM.ITEM_ID = " + DspItem.ItemId.金額 + " ";
            strMEI_AMOUNT += "     WHERE  ";
            strMEI_AMOUNT += "             TMP.GYM_ID = BAT.GYM_ID ";
            strMEI_AMOUNT += "         AND TMP.OPERATION_DATE = BAT.OPERATION_DATE ";
            strMEI_AMOUNT += "         AND TMP.SCAN_TERM = BAT.SCAN_TERM ";
            strMEI_AMOUNT += "         AND TMP.BAT_ID = BAT.BAT_ID ";
            strMEI_AMOUNT += "     GROUP BY ";
            strMEI_AMOUNT += "          MEI.GYM_ID ";
            strMEI_AMOUNT += "         ,MEI.OPERATION_DATE ";
            strMEI_AMOUNT += "         ,MEI.SCAN_TERM ";
            strMEI_AMOUNT += "         ,MEI.BAT_ID ";
            strMEI_AMOUNT += " ) ";

            // 補正ステータス集計
            string strSTS_WAIT_COUNT = GetOcBatchViewSelectHoseiStatus(SchemaBankCD, " = " + HoseiStatus.InputStatus.エントリ待 + " ");
            string strSTS_COMP_COUNT = GetOcBatchViewSelectHoseiStatus(SchemaBankCD, " >= " + HoseiStatus.InputStatus.完了 + " ");
            string strSTS_INPT_COUNT = GetOcBatchViewSelectHoseiStatus(SchemaBankCD, " IN (" + HoseiStatus.InputStatus.エントリ中 + "," + HoseiStatus.InputStatus.エントリ保留 + ") ");

            // 一時テーブル更新
            string strSQL = "";
            strSQL += " UPDATE " + tmpTableName + " BAT ";
            strSQL += " SET ";
            strSQL += "      MEI_COUNT = NVL(" + strMEI_COUNT + ", 0) ";
            strSQL += "     ,MEI_AMOUNT = NVL(" + strMEI_AMOUNT + ", 0) ";
            strSQL += "     ,STS_WAIT_COUNT = " + strSTS_WAIT_COUNT + " ";
            strSQL += "     ,STS_COMP_COUNT = " + strSTS_COMP_COUNT + " ";
            strSQL += "     ,STS_INPT_COUNT = " + strSTS_INPT_COUNT + " ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：バッチデータ集計２
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <returns></returns>
        public static string GetOcBatchViewUpdateTmpBatchList2(string tmpTableName)
        {
            // 補正状態がすべて「1000:エントリ待」の場合、「1:入力待ち」
            // 補正状態がすべて「3000:完了」以上の場合、「10:入力完了」
            // 配下明細がない場合、「10:入力完了」(待ち・入力中・完了データなし)
            // 上記以外は「2:入力中」

            // 補正ステータス集計
            string strTOTAL_STATUS = "";
            strTOTAL_STATUS += " (   SELECT ";
            strTOTAL_STATUS += "         CASE ";
            strTOTAL_STATUS += "             WHEN STS_INPT_COUNT > 0 THEN " + BAT_STS_INPUT + " ";
            strTOTAL_STATUS += "             WHEN STS_WAIT_COUNT > 0 AND STS_COMP_COUNT = 0 THEN " + BAT_STS_WAIT + " ";
            strTOTAL_STATUS += "             WHEN STS_WAIT_COUNT = 0 AND STS_COMP_COUNT = 0 THEN " + BAT_STS_COMP + " ";
            strTOTAL_STATUS += "             WHEN STS_WAIT_COUNT = 0 AND STS_COMP_COUNT > 0 THEN " + BAT_STS_COMP + " ";
            strTOTAL_STATUS += "         ELSE " + BAT_STS_INPUT + " ";
            strTOTAL_STATUS += "         END TOTAL_STATUS ";
            strTOTAL_STATUS += "     FROM ";
            strTOTAL_STATUS += "         " + tmpTableName + " TMP ";
            strTOTAL_STATUS += "     WHERE ";
            strTOTAL_STATUS += "             TMP.GYM_ID = BAT.GYM_ID ";
            strTOTAL_STATUS += "         AND TMP.OPERATION_DATE = BAT.OPERATION_DATE ";
            strTOTAL_STATUS += "         AND TMP.SCAN_TERM = BAT.SCAN_TERM ";
            strTOTAL_STATUS += "         AND TMP.BAT_ID = BAT.BAT_ID ";
            strTOTAL_STATUS += " ) ";

            string strSQL = "";
            strSQL += " UPDATE " + tmpTableName + " BAT ";
            strSQL += " SET ";
            strSQL += "      DIFF_COUNT = (TOTAL_COUNT - MEI_COUNT) ";
            strSQL += "     ,DIFF_AMOUNT = (TOTAL_AMOUNT - MEI_AMOUNT) ";
            strSQL += "     ,TOTAL_STATUS = " + strTOTAL_STATUS + " ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：バッチデータ集計３
        /// </summary>
        /// <param name="tmpTableName"></param>
        /// <returns></returns>
        public static string GetOcBatchViewUpdateTmpBatchList3(string tmpTableName)
        {
            // 差枚数・差金額が０かつ入力完了のものを照合OK、それ以外をNG
            // 0：照合OK
            // 1：照合NG

            // 照合
            string strDIFF_RESULT = "";
            strDIFF_RESULT += " (   SELECT ";
            strDIFF_RESULT += "         CASE ";
            strDIFF_RESULT += "             WHEN DIFF_COUNT <> 0 THEN " + DIFF_RESULT_NG + " ";
            strDIFF_RESULT += "             WHEN DIFF_AMOUNT <> 0 THEN " + DIFF_RESULT_NG + " ";
            strDIFF_RESULT += "             WHEN TOTAL_STATUS <> " + BAT_STS_COMP + " THEN " + DIFF_RESULT_NG + " ";
            strDIFF_RESULT += "         ELSE " + DIFF_RESULT_OK + " ";
            strDIFF_RESULT += "         END DIFF_RESULT ";
            strDIFF_RESULT += "     FROM ";
            strDIFF_RESULT += "         " + tmpTableName + " TMP ";
            strDIFF_RESULT += "     WHERE ";
            strDIFF_RESULT += "             TMP.GYM_ID = BAT.GYM_ID ";
            strDIFF_RESULT += "         AND TMP.OPERATION_DATE = BAT.OPERATION_DATE ";
            strDIFF_RESULT += "         AND TMP.SCAN_TERM = BAT.SCAN_TERM ";
            strDIFF_RESULT += "         AND TMP.BAT_ID = BAT.BAT_ID ";
            strDIFF_RESULT += " ) ";

            string strSQL = "";
            strSQL += " UPDATE " + tmpTableName + " BAT ";
            strSQL += " SET ";
            strSQL += "      DIFF_RESULT = " + strDIFF_RESULT + " ";
            return strSQL;
        }

        /// <summary>
        /// 補正ステータス集計
        /// </summary>
        /// <param name="ctype"></param>
        /// <returns></returns>
        private static string GetOcBatchViewSelectHoseiStatus(int SchemaBankCD, string condition)
        {
            string inputMode = "";
            inputMode += HoseiStatus.HoseiInputMode.持帰銀行;
            inputMode += ",";
            inputMode += HoseiStatus.HoseiInputMode.交換希望日;
            inputMode += ",";
            inputMode += HoseiStatus.HoseiInputMode.金額;

            // 補正ステータス集計
            string strSTS = "";
            strSTS += " (   SELECT ";
            strSTS += "         COUNT(1) ";
            strSTS += "     FROM ";
            strSTS += "         " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSTS += "         INNER JOIN " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS ";
            strSTS += "             ON  STS.GYM_ID = MEI.GYM_ID ";
            strSTS += "             AND STS.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSTS += "             AND STS.SCAN_TERM = MEI.SCAN_TERM ";
            strSTS += "             AND STS.BAT_ID = MEI.BAT_ID ";
            strSTS += "             AND STS.DETAILS_NO = MEI.DETAILS_NO ";
            strSTS += "             AND STS.HOSEI_INPTMODE IN (" + inputMode + ") ";
            strSTS += "             AND STS.INPT_STS " + condition;
            strSTS += "     WHERE ";
            strSTS += "             MEI.GYM_ID = BAT.GYM_ID ";
            strSTS += "         AND MEI.OPERATION_DATE = BAT.OPERATION_DATE ";
            strSTS += "         AND MEI.SCAN_TERM = BAT.SCAN_TERM ";
            strSTS += "         AND MEI.BAT_ID = BAT.BAT_ID ";
            strSTS += "         AND MEI.DELETE_FLG = 0 ";
            strSTS += " ) ";
            return strSTS;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：バッチが訂正可能かどうか判別する
        /// </summary>
        /// <remarks>未使用</remarks>
        public static string GetOcBatchViewCanBatchEditQuery(int gymid, int opedate, string scanterm, int batid, int SchemaBankCD)
        {
            string buasts = "";
            buasts += TrMei.Sts.未作成;
            buasts += ",";
            buasts += TrMei.Sts.結果エラー;

            string strSQL = "";
            strSQL += " SELECT * FROM " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " IMG ";
            strSQL += " WHERE ";
            strSQL += "         IMG.GYM_ID = " + gymid + " ";
            strSQL += "     AND IMG.OPERATION_DATE = " + opedate + " ";
            strSQL += "     AND IMG.SCAN_TERM = '" + scanterm + "' ";
            strSQL += "     AND IMG.BAT_ID = " + batid + " ";
            strSQL += "     AND IMG.BUA_STS NOT IN (" + buasts + ") ";
            strSQL += "     AND IMG.DELETE_FLG = 0 ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：バッチ配下にアップロード済(アップロード中・結果正常)の証券が存在するか判別する
        /// </summary>
        public static string GetOcBatchViewBatchImgUploadQuery(int gymid, int opedate, string scanterm, int batid, int SchemaBankCD)
        {
            string buasts = "";
            buasts += TrMei.Sts.ファイル作成;
            buasts += ",";
            buasts += TrMei.Sts.アップロード;
            buasts += ",";
            buasts += TrMei.Sts.結果正常;

            string strSQL = "";
            strSQL += " SELECT * FROM " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " IMG ";
            strSQL += " WHERE ";
            strSQL += "         IMG.GYM_ID = " + gymid + " ";
            strSQL += "     AND IMG.OPERATION_DATE = " + opedate + " ";
            strSQL += "     AND IMG.SCAN_TERM = '" + scanterm + "' ";
            strSQL += "     AND IMG.BAT_ID = " + batid + " ";
            strSQL += "     AND IMG.BUA_STS IN (" + buasts + ") ";
            strSQL += "     AND IMG.DELETE_FLG = 0 ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：バッチが削除可能かどうか判別する１
        /// </summary>
        public static string GetOcBatchViewCanBatchDeleteQuery1(int gymid, int opedate, string scanterm, int batid, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " SELECT * FROM " + TBL_TRBATCH.TABLE_NAME(SchemaBankCD) + " BAT ";
            strSQL += " WHERE ";
            strSQL += "         BAT.GYM_ID = " + gymid + " ";
            strSQL += "     AND BAT.OPERATION_DATE = " + opedate + " ";
            strSQL += "     AND BAT.SCAN_TERM = '" + scanterm + "' ";
            strSQL += "     AND BAT.BAT_ID = " + batid + " ";
            strSQL += "     AND BAT.STS = " + TrBatch.Sts.入力完了 + " ";
            strSQL += "     AND BAT.DELETE_FLG = 0 ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：バッチが削除可能かどうか判別する２
        /// </summary>
        public static string GetOcBatchViewCanBatchDeleteQuery2(int gymid, int opedate, string scanterm, int batid, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " SELECT * FROM " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " IMG ";
            strSQL += " WHERE ";
            strSQL += "         IMG.GYM_ID = " + gymid + " ";
            strSQL += "     AND IMG.OPERATION_DATE = " + opedate + " ";
            strSQL += "     AND IMG.SCAN_TERM = '" + scanterm + "' ";
            strSQL += "     AND IMG.BAT_ID = " + batid + " ";
            //strSQL += "     AND IMG.DELETE_FLG = 0 ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：入力完了のバッチ情報を取得する
        /// </summary>
        /// <returns></returns>
        public static string GetOcBatchViewSelectTrBatchQuery(int gymid, int opedate, string scanterm, int batid, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " SELECT * FROM " + TBL_TRBATCH.TABLE_NAME(SchemaBankCD) + " BAT ";
            strSQL += " WHERE ";
            strSQL += "         BAT.GYM_ID = " + gymid + " ";
            strSQL += "     AND BAT.OPERATION_DATE = " + opedate + " ";
            strSQL += "     AND BAT.SCAN_TERM = '" + scanterm + "' ";
            strSQL += "     AND BAT.BAT_ID = " + batid + " ";
            strSQL += "     AND BAT.STS = " + TrBatch.Sts.入力完了 + " ";
            strSQL += "     AND BAT.DELETE_FLG = 0 ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：バッチステータスを入力完了に更新する
        /// </summary>
        /// <returns></returns>
        public static string GetOcBatchViewUpdateTrBatchStatusCompQuery(TBL_TRBATCH bat, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRBATCH.TABLE_NAME(SchemaBankCD) + " BAT ";
            strSQL += " SET ";
            strSQL += "     STS = " + TrBatch.Sts.入力完了 + " ";
            strSQL += " WHERE ";
            strSQL += "         BAT.GYM_ID = " + bat._GYM_ID + " ";
            strSQL += "     AND BAT.OPERATION_DATE = " + bat._OPERATION_DATE + " ";
            strSQL += "     AND BAT.SCAN_TERM = '" + bat._SCAN_TERM + "' ";
            strSQL += "     AND BAT.BAT_ID = " + bat._BAT_ID + " ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：更新対象バッチの項目トランザクションを取得する
        /// </summary>
        /// <returns></returns>
        public static string GetOcBatchViewSelectTrItem(TBL_TRBATCH bat, int BefClearingDate, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " SELECT ";
            strSQL += "     ITEM.* ";
            strSQL += " FROM ";
            strSQL += "     " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSQL += "     INNER JOIN " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " ITEM ";
            strSQL += "         ON  ITEM.GYM_ID = MEI.GYM_ID ";
            strSQL += "         AND ITEM.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "         AND ITEM.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "         AND ITEM.BAT_ID = MEI.BAT_ID ";
            strSQL += "         AND ITEM.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += " WHERE ";
            strSQL += "         MEI.GYM_ID = " + bat._GYM_ID + " ";
            strSQL += "     AND MEI.OPERATION_DATE = " + bat._OPERATION_DATE + " ";
            strSQL += "     AND MEI.SCAN_TERM = '" + bat._SCAN_TERM + "' ";
            strSQL += "     AND MEI.BAT_ID = " + bat._BAT_ID + " ";
            strSQL += "     AND MEI.DELETE_FLG = 0 ";
            strSQL += "     AND EXISTS( ";
            strSQL += "             SELECT 1 FROM " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " WT ";
            strSQL += "             WHERE ";
            strSQL += "                 WT.GYM_ID = MEI.GYM_ID ";
            strSQL += "             AND WT.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "             AND WT.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "             AND WT.BAT_ID = MEI.BAT_ID ";
            strSQL += "             AND WT.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "             AND WT.ITEM_ID = " + DspItem.ItemId.入力交換希望日 + " ";
            strSQL += "             AND TO_NUMBER(NVL(WT.END_DATA,0)) = " + BefClearingDate + " ";
            strSQL += "         ) ";
            // アップロード済のイメージがある明細は除外
            strSQL += "     AND NOT EXISTS( ";
            strSQL += "             SELECT 1 FROM " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " IMG ";
            strSQL += "             WHERE ";
            strSQL += "                 IMG.GYM_ID = MEI.GYM_ID ";
            strSQL += "             AND IMG.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "             AND IMG.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "             AND IMG.BAT_ID = MEI.BAT_ID ";
            strSQL += "             AND IMG.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "             AND IMG.BUA_STS IN (" + TrMei.Sts.ファイル作成 + "," + TrMei.Sts.アップロード + "," + TrMei.Sts.結果正常 + ") ";
            strSQL += "             AND IMG.DELETE_FLG = 0 ";
            strSQL += "         ) ";
            strSQL += " ORDER BY ";
            strSQL += "      ITEM.GYM_ID ";
            strSQL += "     ,ITEM.OPERATION_DATE ";
            strSQL += "     ,ITEM.SCAN_TERM ";
            strSQL += "     ,ITEM.BAT_ID ";
            strSQL += "     ,ITEM.DETAILS_NO ";
            strSQL += "     ,ITEM.ITEM_ID ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：TRBATCH を削除する
        /// </summary>
        /// <returns></returns>
        public static string GetOcBatchViewDeleteTrBatchQuery(TBL_TRBATCH bat, int opedate, int SchemaBankCD)
        {
            // TRBATCH 更新
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRBATCH.TABLE_NAME(SchemaBankCD) + " BAT ";
            strSQL += " SET ";
            strSQL += "      DELETE_DATE = " + opedate + " ";
            strSQL += "     ,DELETE_FLG = 1 ";
            strSQL += " WHERE ";
            strSQL += "         BAT.GYM_ID = " + bat._GYM_ID + " ";
            strSQL += "     AND BAT.OPERATION_DATE = " + bat._OPERATION_DATE + " ";
            strSQL += "     AND BAT.SCAN_TERM = '" + bat._SCAN_TERM + "' ";
            strSQL += "     AND BAT.BAT_ID = " + bat._BAT_ID + " ";
            strSQL += "     AND BAT.STS = " + TrBatch.Sts.入力完了 + " ";
            strSQL += "     AND BAT.DELETE_FLG = 0";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：TRMEI を削除する
        /// </summary>
        /// <returns></returns>
        public static string GetOcBatchViewDeleteTrMeiQuery(TBL_TRBATCH bat, int opedate, int SchemaBankCD)
        {
            // TRMEI 更新
            string strSQL = "";
            strSQL = "";
            strSQL += " UPDATE " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSQL += " SET ";
            strSQL += "      DELETE_DATE = " + opedate + " ";
            strSQL += "     ,DELETE_FLG = 1 ";
            strSQL += " WHERE ";
            strSQL += "         MEI.GYM_ID = " + bat._GYM_ID + " ";
            strSQL += "     AND MEI.OPERATION_DATE = " + bat._OPERATION_DATE + " ";
            strSQL += "     AND MEI.SCAN_TERM = '" + bat._SCAN_TERM + "' ";
            strSQL += "     AND MEI.BAT_ID = " + bat._BAT_ID + " ";
            strSQL += "     AND MEI.DELETE_FLG = 0";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：E0508　持出バッチ照会
        /// 処理内容：TRMEIIMG を削除する
        /// </summary>
        /// <returns></returns>
        public static string GetOcBatchViewDeleteTrImageQuery(TBL_TRBATCH bat, int opedate, int SchemaBankCD)
        {
            // TRMEIIMG 更新
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " IMG ";
            strSQL += " SET ";
            strSQL += "      DELETE_DATE = " + opedate + " ";
            strSQL += "     ,DELETE_FLG = 1 ";
            strSQL += " WHERE ";
            strSQL += "         IMG.GYM_ID = " + bat._GYM_ID + " ";
            strSQL += "     AND IMG.OPERATION_DATE = " + bat._OPERATION_DATE + " ";
            strSQL += "     AND IMG.SCAN_TERM = '" + bat._SCAN_TERM + "' ";
            strSQL += "     AND IMG.BAT_ID = " + bat._BAT_ID + " ";
            strSQL += "     AND IMG.DELETE_FLG = 0";
            return strSQL;
        }

        /// <summary>
        /// 持出明細照会での一覧取得SQL
        /// </summary>
        /// <returns></returns>
        public static string GetOcMeiList(int gymid, int Rdate, int ScanBrNo, int ScanDate, int OCBRNoFrom, int OCBRNoTo, int BatNo, int ICBKNo, 
                                          int ClearingDataFrom, int ClearingDataTo, long AmountFrom, long AmountTo, int Status, 
                                          int Delete, string ntOpeNumer, string Memo, int MemoOpt, string ImgFLNm, int ImgFLNmOpt,
                                          int BUASts, int BCASts, int BUA, int GMB, int GRASts, int GXA, int Def, int EditFlg, 
                                          int Schemabankcd, int ListDispLimit)
        {
            //共通部取得
            string strSql = GetOcMeiListCommon(Schemabankcd);

            //条件設定
            strSql += " WHERE TRMEI.GYM_ID = " + gymid + " ";
            if (Rdate > -1)
            {
                strSql += "  AND TRMEI.OPERATION_DATE = " + Rdate + " ";
            }
            if (ScanBrNo > -1)
            {
                strSql += "  AND TRBATCH.SCAN_BR_NO = " + ScanBrNo + " ";
            }
            if (ScanDate > -1)
            {
                strSql += "  AND TRBATCH.SCAN_DATE = " + ScanDate + " ";
            }
            if (OCBRNoFrom > -1)
            {
                strSql += "  AND TRBATCH.OC_BR_NO >= " + OCBRNoFrom + " ";
            }
            if (OCBRNoTo > -1)
            {
                strSql += "  AND TRBATCH.OC_BR_NO <= " + OCBRNoTo + " ";
            }
            if (BatNo > -1)
            {
                strSql += "  AND TRMEI.BAT_ID = " + BatNo + " ";
            }
            if (ICBKNo > -1)
            {
                strSql += "  AND TRITEM_ID1.END_DATA = '" + ICBKNo.ToString("D4") + "' ";
            }
            if (ClearingDataFrom > -1)
            {
                strSql += "  AND TRITEM_ID5.END_DATA >= '" + ClearingDataFrom.ToString("D8") + "' ";
            }
            if (ClearingDataTo > -1)
            {
                strSql += "  AND TRITEM_ID5.END_DATA <= '" + ClearingDataTo.ToString("D8") + "' ";
            }
            if (AmountFrom > -1)
            {
                strSql += "  AND TRITEM_ID6.END_DATA >= '" + AmountFrom.ToString("D12") + "' ";
            }
            if (AmountTo > -1)
            {
                strSql += "  AND TRITEM_ID6.END_DATA <= '" + AmountTo.ToString("D12") + "' ";
            }
            if (Delete != 9)
            {
                strSql += "  AND TRMEI.DELETE_FLG = 0 ";
            }
            if (!string.IsNullOrEmpty(ntOpeNumer))
            {
                strSql += "  AND ( TRITEM_ID19.E_OPENO = '" + ntOpeNumer + "' OR TRITEM_ID6.E_OPENO = '" + ntOpeNumer + "' ) ";
            }
            if (!string.IsNullOrEmpty(Memo))
            {
                switch (MemoOpt)
                {
                    case 1:
                        strSql += "  AND TRMEI.MEMO LIKE '" + Memo + "%' ";
                        break;
                    case 2:
                        strSql += "  AND TRMEI.MEMO LIKE '%" + Memo + "' ";
                        break;
                    default:
                        strSql += "  AND TRMEI.MEMO = '" + Memo + "' ";
                        break;
                }
            }
            if (BCASts > -1)
            {
                strSql += "  AND TRMEI.BCA_STS = " + BCASts + " ";
            }
            switch (BUA)
            {
                case 0:
                    strSql += "  AND TRMEI.BUA_DATE = 0 ";
                    break;
                case 1:
                    strSql += "  AND TRMEI.BUA_DATE > 0 ";
                    break;
            }
            switch (GMB)
            {
                case 0:
                    strSql += "  AND TRMEI.GMA_DATE = 0 ";
                    break;
                case 1:
                    strSql += "  AND TRMEI.GMA_DATE > 0 ";
                    break;
            }
            switch (GRASts)
            {
                case 0:
                    strSql += "  AND TRMEI.GRA_DATE = 0 ";
                    break;
                case 1:
                    strSql += "  AND TRMEI.GRA_DATE > 0 ";
                    break;
            }
            switch (GXA)
            {
                case 0:
                    strSql += "  AND TRMEI.GXA_DATE = 0 ";
                    break;
                case 1:
                    strSql += "  AND TRMEI.GXA_DATE > 0 ";
                    break;
            }
            if (!string.IsNullOrEmpty(ImgFLNm) || BUASts > -1)
            {
                strSql +=
                    "  AND EXISTS (  " +
                    "       SELECT 1 " +
                    "       FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                    "       WHERE TRMEIIMG.GYM_ID = TRMEI.GYM_ID " +
                    "         AND TRMEIIMG.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                    "         AND TRMEIIMG.SCAN_TERM = TRMEI.SCAN_TERM " +
                    "         AND TRMEIIMG.BAT_ID = TRMEI.BAT_ID " +
                    "         AND TRMEIIMG.DETAILS_NO = TRMEI.DETAILS_NO ";
                if (!string.IsNullOrEmpty(ImgFLNm))
                {
                    switch (ImgFLNmOpt)
                    {
                        case 1:
                            strSql += "  AND TRMEIIMG.IMG_FLNM LIKE '" + ImgFLNm + "%' ";
                            break;
                        case 2:
                            strSql += "  AND TRMEIIMG.IMG_FLNM LIKE '%" + ImgFLNm + "' ";
                            break;
                        default:
                            strSql += "  AND TRMEIIMG.IMG_FLNM = '" + ImgFLNm + "' ";
                            break;
                    }
                }
                if (BUASts > -1)
                {
                    strSql += "  AND TRMEIIMG.BUA_STS = " + BUASts + " ";
                }

                strSql += " ) ";
            }

            if (Def > -1)
            {
                switch (Def)
                {
                    case 0:
                        //ありの場合はどれかが省略値のデータを抽出
                        strSql +=
                            "  AND EXISTS (  " +
                            "       SELECT 1 " +
                            "       FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_DEF " +
                            "       WHERE TRITEM_DEF.GYM_ID = TRMEI.GYM_ID " +
                            "         AND TRITEM_DEF.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                            "         AND TRITEM_DEF.SCAN_TERM = TRMEI.SCAN_TERM " +
                            "         AND TRITEM_DEF.BAT_ID = TRMEI.BAT_ID " +
                            "         AND TRITEM_DEF.DETAILS_NO = TRMEI.DETAILS_NO " +
                            "         AND TRITEM_DEF.ITEM_ID IN(6, 19) " +
                            "         AND TRITEM_DEF.BUA_DATA IS NULL ";
                        break;
                    case 1:
                        //なしの場合はすべてが省略値のデータを抽出
                        strSql +=
                            "  AND ( " +
                            "    EXISTS (  " +
                            "       SELECT 1 " +
                            "       FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_DEF " +
                            "       WHERE TRITEM_DEF.GYM_ID = TRMEI.GYM_ID " +
                            "         AND TRITEM_DEF.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                            "         AND TRITEM_DEF.SCAN_TERM = TRMEI.SCAN_TERM " +
                            "         AND TRITEM_DEF.BAT_ID = TRMEI.BAT_ID " +
                            "         AND TRITEM_DEF.DETAILS_NO = TRMEI.DETAILS_NO " +
                            "         AND TRITEM_DEF.ITEM_ID = 6 " +
                            "         AND TRITEM_DEF.BUA_DATA IS NOT NULL " +
                            "    ) AND " +
                            "    EXISTS (  " +
                            "       SELECT 1 " +
                            "       FROM " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_DEF " +
                            "       WHERE TRITEM_DEF.GYM_ID = TRMEI.GYM_ID " +
                            "         AND TRITEM_DEF.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                            "         AND TRITEM_DEF.SCAN_TERM = TRMEI.SCAN_TERM " +
                            "         AND TRITEM_DEF.BAT_ID = TRMEI.BAT_ID " +
                            "         AND TRITEM_DEF.DETAILS_NO = TRMEI.DETAILS_NO " +
                            "         AND TRITEM_DEF.ITEM_ID = 19 " +
                            "         AND TRITEM_DEF.BUA_DATA IS NOT NULL " +
                            "    ) ";
                        break;
                }

                strSql += " ) ";
            }

            switch (Status)
            {
                case 1:
                    // 入力待
                    strSql += "  AND ( " +
                              "        HOSEIICBK.INPT_STS = " + HoseiStatus.InputStatus.エントリ待 + " " +
                              "    AND HOSEIAMT.INPT_STS = " + HoseiStatus.InputStatus.エントリ待 + " " +
                              "  ) ";
                    break;
                case 2:
                    // 入力中
                    strSql += "  AND ( " +
                              "    NOT ( " +
                              "          HOSEIICBK.INPT_STS = " + HoseiStatus.InputStatus.エントリ待 + " " +
                              "      AND HOSEIAMT.INPT_STS = " + HoseiStatus.InputStatus.エントリ待 + " " +
                              "    ) " +
                              "    AND " +
                              "    NOT ( " +
                              "          HOSEIICBK.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "      AND HOSEICDATE.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "      AND HOSEIAMT.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "      AND HOSEITEISEI.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    ) " +
                              "    AND " +
                              "    ( " +
                              "          HOSEIICBK.INPT_STS <= " + HoseiStatus.InputStatus.完了 + " " +
                              "      AND HOSEIAMT.INPT_STS <= " + HoseiStatus.InputStatus.完了 + " " +
                              "      AND HOSEITEISEI.INPT_STS <= " + HoseiStatus.InputStatus.完了 + " " +
                              "    ) " +
                              "  ) ";
                    break;
                case 10:
                    // 完了
                    strSql += "  AND ( " +
                              "        HOSEIICBK.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEICDATE.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEIAMT.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEITEISEI.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "  ) ";
                    break;
                case 20:
                    // 完了訂正
                    strSql += "  AND ( " +
                              "        HOSEITEISEI.INPT_STS IN (" + HoseiStatus.InputStatus.完了訂正中 + "," + HoseiStatus.InputStatus.完了訂正保留 + ") " +
                              "  ) ";
                    break;
                default:
                    break;
            }
            if (EditFlg > -1)
            {
                strSql += "  AND TRMEI.EDIT_FLG = " + EditFlg + " ";
            }

            // ORDER BY 
            strSql += " ORDER BY TRMEI.OPERATION_DATE, TRMEI.BAT_ID, TRMEI.DETAILS_NO ";
            //取得件数制御
            strSql = "SELECT * FROM ( " + strSql + " ) WHERE ROWNUM <= " + (ListDispLimit + 1) + " ";

            return strSql;
        }

        /// <summary>
        /// 持出明細照会での対象データ取得SQL
        /// </summary>
        /// <returns></returns>
        public static string GetOcMeiListRow(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            //共通部取得
            string strSql = GetOcMeiListCommon(Schemabankcd);
            //条件設定
            strSql += " WHERE TRMEI.GYM_ID = " + gymid + " " +
                      "   AND TRMEI.OPERATION_DATE = " + operationdate + " " +
                      "   AND TRMEI.SCAN_TERM = '" + scanterm + "' " +
                      "   AND TRMEI.BAT_ID = " + batid + " " +
                      "   AND TRMEI.DETAILS_NO = " + detailsno + " ";

            return strSql;
        }

        /// <summary>
        /// 持出明細照会での取得SQL(共通)
        /// </summary>
        /// <returns></returns>
        public static string GetOcMeiListCommon(int Schemabankcd)
        {
            string strSql =
                " SELECT TRMEI.GYM_ID " +
                "      , TRMEI.OPERATION_DATE " +
                "      , TRMEI.SCAN_TERM " +
                "      , TRMEI.BAT_ID " +
                "      , TRMEI.DETAILS_NO " +
                "      , TRBATCH.OC_BK_NO " +
                "      , TRBATCH.OC_BR_NO " +
                "      , TRBATCH.SCAN_BR_NO " +
                "      , TRBATCH.SCAN_DATE " +
                "      , TRBATCH.INPUT_ROUTE " +
                "      , TRITEM_ID1.END_DATA ICBK_NO " +
                "      , TRITEM_ID5.END_DATA CLEARING_DATE " +
                "      , TRITEM_ID6.END_DATA AMT " +
                "      , TRITEM_ID7.END_DATA PAY_KBN " +
                "      , TRITEM_ID19.E_OPENO ICBK_OPENO " +
                "      , TRITEM_ID6.E_OPENO AMT_OPENO " +
                "      , TRMEI.BCA_STS " +
                "      , TRMEI.BUA_DATE " +
                "      , TRMEI.GMA_DATE " +
                "      , TRMEI.GRA_DATE " +
                "      , TRMEI.GXA_DATE " +
                "      , TRMEI.MEMO " +
                "      , TRMEI.DELETE_FLG " +
                "      , HOSEIICBK.INPT_STS ICBKINPTSTS " +
                "      , HOSEICDATE.INPT_STS CDATEINPTSTS " +
                "      , HOSEIAMT.INPT_STS AMTINPTSTS " +
                "      , HOSEITEISEI.INPT_STS TEISEIINPTSTS " +
                "      , HOSEITEISEI.TMNO TEISEITMNO " +
                "      , TRMEI.EDIT_FLG " +
                " FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                "     INNER JOIN " +
                "     " + TBL_TRBATCH.TABLE_NAME(Schemabankcd) + " TRBATCH " +
                "      ON TRBATCH.GYM_ID = TRMEI.GYM_ID " +
                "     AND TRBATCH.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "     AND TRBATCH.SCAN_TERM = TRMEI.SCAN_TERM " +
                "     AND TRBATCH.BAT_ID = TRMEI.BAT_ID " +
                "     LEFT JOIN " +
                "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID1 " +
                "      ON TRITEM_ID1.GYM_ID = TRMEI.GYM_ID " +
                "     AND TRITEM_ID1.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "     AND TRITEM_ID1.SCAN_TERM = TRMEI.SCAN_TERM " +
                "     AND TRITEM_ID1.BAT_ID = TRMEI.BAT_ID " +
                "     AND TRITEM_ID1.DETAILS_NO = TRMEI.DETAILS_NO " +
                "     AND TRITEM_ID1.ITEM_ID = 1 " +
                "     LEFT JOIN " +
                "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                "      ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                "     AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "     AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                "     AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                "     AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                "     AND TRITEM_ID5.ITEM_ID = 5 " +
                "     LEFT JOIN " +
                "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID6 " +
                "      ON TRITEM_ID6.GYM_ID = TRMEI.GYM_ID " +
                "     AND TRITEM_ID6.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "     AND TRITEM_ID6.SCAN_TERM = TRMEI.SCAN_TERM " +
                "     AND TRITEM_ID6.BAT_ID = TRMEI.BAT_ID " +
                "     AND TRITEM_ID6.DETAILS_NO = TRMEI.DETAILS_NO " +
                "     AND TRITEM_ID6.ITEM_ID = 6 " +
                "     LEFT JOIN " +
                "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID7 " +
                "      ON TRITEM_ID7.GYM_ID = TRMEI.GYM_ID " +
                "     AND TRITEM_ID7.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "     AND TRITEM_ID7.SCAN_TERM = TRMEI.SCAN_TERM " +
                "     AND TRITEM_ID7.BAT_ID = TRMEI.BAT_ID " +
                "     AND TRITEM_ID7.DETAILS_NO = TRMEI.DETAILS_NO " +
                "     AND TRITEM_ID7.ITEM_ID = 7 " +
                "     LEFT JOIN " +
                "     " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID19 " +
                "      ON TRITEM_ID19.GYM_ID = TRMEI.GYM_ID " +
                "     AND TRITEM_ID19.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "     AND TRITEM_ID19.SCAN_TERM = TRMEI.SCAN_TERM " +
                "     AND TRITEM_ID19.BAT_ID = TRMEI.BAT_ID " +
                "     AND TRITEM_ID19.DETAILS_NO = TRMEI.DETAILS_NO " +
                "     AND TRITEM_ID19.ITEM_ID = 19 " +
                "     LEFT JOIN " +
                "     " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEIICBK " +
                "      ON HOSEIICBK.GYM_ID = TRMEI.GYM_ID " +
                "     AND HOSEIICBK.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "     AND HOSEIICBK.SCAN_TERM = TRMEI.SCAN_TERM " +
                "     AND HOSEIICBK.BAT_ID = TRMEI.BAT_ID " +
                "     AND HOSEIICBK.DETAILS_NO = TRMEI.DETAILS_NO " +
                "     AND HOSEIICBK.HOSEI_INPTMODE = 1 " +
                "     LEFT JOIN " +
                "     " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEICDATE " +
                "      ON HOSEICDATE.GYM_ID = TRMEI.GYM_ID " +
                "     AND HOSEICDATE.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "     AND HOSEICDATE.SCAN_TERM = TRMEI.SCAN_TERM " +
                "     AND HOSEICDATE.BAT_ID = TRMEI.BAT_ID " +
                "     AND HOSEICDATE.DETAILS_NO = TRMEI.DETAILS_NO " +
                "     AND HOSEICDATE.HOSEI_INPTMODE = 2 " +
                "     LEFT JOIN " +
                "     " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEIAMT " +
                "      ON HOSEIAMT.GYM_ID = TRMEI.GYM_ID " +
                "     AND HOSEIAMT.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "     AND HOSEIAMT.SCAN_TERM = TRMEI.SCAN_TERM " +
                "     AND HOSEIAMT.BAT_ID = TRMEI.BAT_ID " +
                "     AND HOSEIAMT.DETAILS_NO = TRMEI.DETAILS_NO " +
                "     AND HOSEIAMT.HOSEI_INPTMODE = 3 " +
                "     LEFT JOIN " +
                "     " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEITEISEI " +
                "      ON HOSEITEISEI.GYM_ID = TRMEI.GYM_ID " +
                "     AND HOSEITEISEI.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "     AND HOSEITEISEI.SCAN_TERM = TRMEI.SCAN_TERM " +
                "     AND HOSEITEISEI.BAT_ID = TRMEI.BAT_ID " +
                "     AND HOSEITEISEI.DETAILS_NO = TRMEI.DETAILS_NO " +
                "     AND HOSEITEISEI.HOSEI_INPTMODE = 5 ";

            return strSql;
        }

        /// <summary>
        /// 明細データの編集フラグを更新するUPDATE文を作成します
        /// 持出明細照会
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRMEIEditFlg(int gymid, int operationdate, string scanterm, int batid, int detailsno, int EditFlg, int Schemabankcd)
        {
            string strSQL = "UPDATE " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " SET " +
                TBL_TRMEI.EDIT_FLG + "=" + EditFlg + " " +
                " WHERE " +
                TBL_TRMEI.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEI.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEI.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEI.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEI.DETAILS_NO + "=" + detailsno + " ";
            return strSQL;
        }

        /// <summary>
        /// 明細イメージデータの項目を更新するUPDATE文を作成します
        /// 持出明細照会
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRMEIIMG(int gymid, int operationdate, string scanterm, int batid, int detailsno, int imgkbn, Dictionary<string, int> UpdateData, int Schemabankcd)
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
                TBL_TRMEIIMG.DETAILS_NO + "=" + detailsno + " AND " +
                TBL_TRMEIIMG.IMG_KBN + "=" + imgkbn + " ";
            return strSql;
        }

        /// <summary>
        /// 明細データの項目を更新するUPDATE文を作成します
        /// 持出明細照会
        /// 持帰ダウンロード確定でも使用
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRMEI(int gymid, int operationdate, string scanterm, int batid, int detailsno, Dictionary<string, int> UpdateData, int Schemabankcd)
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
                TBL_TRMEI.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEI.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEI.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEI.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEI.DETAILS_NO + "=" + detailsno + " ";
            return strSql;
        }

        /// <summary>
        /// 明細データを削除するUPDATE文を作成します
        /// 持出明細照会
        /// 持帰明細照会
        /// 結果テキスト取込でも使用
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRMEIDelete(int gymid, int operationdate, string scanterm, int batid, int detailsno, int opedate, int Schemabankcd)
        {
            string strSql = "UPDATE " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " " +
                " SET DELETE_DATE = " + opedate + " " +
                "   , DELETE_FLG = 1" +
                " WHERE " +
                TBL_TRMEI.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEI.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEI.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEI.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEI.DETAILS_NO + "=" + detailsno + " ";
            return strSql;
        }

        /// <summary>
        /// 明細イメージデータを明細単位で削除するUPDATE文を作成します
        /// 持出明細照会
        /// 持帰明細照会
        /// 結果テキスト取込でも使用
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRMEIIMGDetailDelete(int gymid, int operationdate, string scanterm, int batid, int detailsno, int opedate, int Schemabankcd)
        {
            string strSql = "UPDATE " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " " +
                " SET DELETE_DATE = " + opedate + " " +
                "   , DELETE_FLG = 1" + 
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEIIMG.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEIIMG.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEIIMG.DETAILS_NO + "=" + detailsno + " ";
            return strSql;
        }

        /// <summary>
        /// 明細データを削除解除するUPDATE文を作成します
        /// 持出明細照会
        /// 持帰明細照会
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRMEIUnDelete(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            string strSql = "UPDATE " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " " +
                " SET DELETE_DATE = 0 " +
                "   , DELETE_FLG = 0 " +
                " WHERE " +
                TBL_TRMEI.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEI.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEI.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEI.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEI.DETAILS_NO + "=" + detailsno + " ";
            return strSql;
        }

        /// <summary>
        /// 明細イメージデータを明細単位で削除解除するUPDATE文を作成します
        /// 持出明細照会
        /// 持帰明細照会
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateTRMEIIMGDetailUnDelete(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            string strSql = "UPDATE " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " " +
                " SET DELETE_DATE = 0 " +
                "   , DELETE_FLG = 0 " +
                " WHERE " +
                TBL_TRMEIIMG.GYM_ID + "=" + gymid + " AND " +
                TBL_TRMEIIMG.OPERATION_DATE + "=" + operationdate + " AND " +
                TBL_TRMEIIMG.SCAN_TERM + "='" + scanterm + "' AND " +
                TBL_TRMEIIMG.BAT_ID + "=" + batid + " AND " +
                TBL_TRMEIIMG.DETAILS_NO + "=" + detailsno + " ";
            return strSql;
        }

        /// <summary>
        /// 持帰明細照会での一覧取得SQL
        /// </summary>
        /// <returns></returns>
        public static string GetIcMeiList(int gymid, int Rdate, int OcBkNo, int ClearingDate, long AmountFrom, long AmountTo, int BillNoFrom, int BillNoTo, int SyuriNo, int BrNo, 
                                          long AccountNo, long TegataNo, int Status, string entOpeNumer, string veriOpeNumer, string ImgFLNm, int ImgFLNmOpt, 
                                          int TeiseiFlg, int FuwatariFlg, int GMASts, int GRASts, int Delete, int PayKbn, int BUB, int Schemabankcd, int ListDispLimit, 
                                          bool DispSortType, List<int> BillNoList)
        {
            //共通部取得
            string strSql = GetIcMeiListCommon(Schemabankcd);
            //条件設定
            strSql += " WHERE TRMEI.GYM_ID = " + gymid + " ";
            if (Rdate > -1)
            {
                strSql += "   AND TRMEI.OPERATION_DATE = " + Rdate + " ";
            }
            if (OcBkNo > -1)
            {
                strSql += "   AND TRMEI.IC_OC_BK_NO = " + OcBkNo + " ";
            }
            if (ClearingDate > -1)
            {
                strSql += "   AND TRITEM_ID5.END_DATA = '" + ClearingDate.ToString("D8") + "' ";
            }
            if (AmountFrom > -1)
            {
                strSql += "  AND TRITEM_ID6.END_DATA >= '" + AmountFrom.ToString("D12") + "' ";
            }
            if (AmountTo > -1)
            {
                strSql += "  AND TRITEM_ID6.END_DATA <= '" + AmountTo.ToString("D12") + "' ";
            }
            if (BillNoFrom > -1)
            {
                strSql += "   AND TRITEM_ID8.END_DATA >= '" + BillNoFrom.ToString("D3") + "' ";
            }
            if (BillNoTo > -1)
            {
                strSql += "   AND TRITEM_ID8.END_DATA <= '" + BillNoTo.ToString("D3") + "' ";
            }
            if (DispSortType)
            {
                // 印鑑照合モード
                if (BillNoList.Count > 0)
                {
                    strSql += "   AND TRITEM_ID8.END_DATA IN('" + string.Join("','", BillNoList.Select(x => x.ToString("D3"))) + "') ";
                }
            }
            if (SyuriNo > -1)
            {
                // 種類については桁数が決まっていないためこの箇所は数値化して比較(パフォーマンスには影響があるが)
                strSql += "   AND TO_NUMBER(TRITEM_ID10.END_DATA) = " + SyuriNo + " ";
            }
            if (BrNo > -1)
            {
                strSql += "   AND TRITEM_ID13.END_DATA = '" + BrNo.ToString("D4") + "' ";
            }
            if (AccountNo > -1)
            {
                // 口座番号については桁数が決まっていないためこの箇所は数値化して比較(パフォーマンスには影響があるが)
                strSql += "   AND TO_NUMBER(TRITEM_ID16.END_DATA) = " + AccountNo + " ";
            }
            if (TegataNo > -1)
            {
                // 手形番号については桁数が決まっていないためこの箇所は数値化して比較(パフォーマンスには影響があるが)
                strSql += "   AND TO_NUMBER(TRITEM_ID18.END_DATA) = " + TegataNo + " ";
            }
            switch (Delete)
            {
                case 8:
                    // 削除のみ
                    strSql += "  AND TRMEI.DELETE_FLG = 1 ";
                    break;
                case 9:
                    // 削除含む
                    break;
                default:
                    strSql += "  AND TRMEI.DELETE_FLG = 0 ";
                    break;
            }
            switch (PayKbn)
            {
                case 0:
                    // 決済対象
                    strSql += "  AND TRITEM_ID7.END_DATA = '0' ";
                    break;
                case 1:
                    // 決済を伴わないｲﾒｰｼﾞ交換
                    strSql += "  AND TRITEM_ID7.END_DATA = '1' ";
                    break;
                default:
                    break;
            }
            switch (BUB)
            {
                case 0:
                    // なし
                    strSql += "  AND TRMEI.BUB_DATE = 0 ";
                    break;
                case 1:
                    // あり
                    strSql += "  AND TRMEI.BUB_DATE > 0 ";
                    break;
            }
            switch (Status)
            {
                case 1:
                    // 交換尻入力待
                    strSql += "  AND ( " +
                              "        HOSEIICBK.INPT_STS = " + HoseiStatus.InputStatus.エントリ待 + " " +
                              "    AND HOSEICDATE.INPT_STS = " + HoseiStatus.InputStatus.エントリ待 + " " +
                              "    AND HOSEIAMT.INPT_STS = " + HoseiStatus.InputStatus.エントリ待 + " " +
                              "  ) ";
                    break;
                case 2:
                    // 交換尻入力中
                    strSql += "  AND ( " +
                              "    NOT ( " +
                              "          HOSEIICBK.INPT_STS = " + HoseiStatus.InputStatus.エントリ待 + " " +
                              "      AND HOSEICDATE.INPT_STS = " + HoseiStatus.InputStatus.エントリ待 + " " +
                              "      AND HOSEIAMT.INPT_STS = " + HoseiStatus.InputStatus.エントリ待 + " " +
                              "    ) " +
                              "    AND " +
                              "    NOT ( " +
                              "          HOSEIICBK.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "      AND HOSEICDATE.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "      AND HOSEIAMT.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "      AND HOSEIKJIRI.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    ) " +
                              "    AND " +
                              "    ( " +
                              "          HOSEIICBK.INPT_STS <= " + HoseiStatus.InputStatus.完了 + " " +
                              "      AND HOSEICDATE.INPT_STS <= " + HoseiStatus.InputStatus.完了 + " " +
                              "      AND HOSEIAMT.INPT_STS <= " + HoseiStatus.InputStatus.完了 + " " +
                              "      AND HOSEIKJIRI.INPT_STS <= " + HoseiStatus.InputStatus.完了 + " " +
                              "    ) " +
                              "  ) ";
                    break;
                case 3:
                    // 自行情報入力待
                    strSql += "  AND ( " +
                              "        HOSEIICBK.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEICDATE.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEIAMT.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEIJIKOU.INPT_STS = " + HoseiStatus.InputStatus.エントリ待 + " " +
                              "  ) ";

                    break;
                case 4:
                    // 自行情報入力中
                    strSql += "  AND ( " +
                              "        HOSEIICBK.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEICDATE.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEIAMT.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND " +
                              "    ( " +
                              "          HOSEIJIKOU.INPT_STS > " + HoseiStatus.InputStatus.エントリ待 + " " +
                              "      AND HOSEIJIKOU.INPT_STS < " + HoseiStatus.InputStatus.完了 + " " +
                              "    ) " +
                              "  ) ";
                    break;
                case 10:
                    // 完了
                    strSql += "  AND ( " +
                              "        HOSEIICBK.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEICDATE.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEIAMT.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEIJIKOU.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEIKJIRI.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "  ) ";
                    break;
                case 20:
                    // 交換尻完了訂正
                    strSql += "  AND ( " +
                              "        HOSEIKJIRI.INPT_STS IN (" + HoseiStatus.InputStatus.完了訂正中 + "," + HoseiStatus.InputStatus.完了訂正保留 + ") " +
                              "  ) ";
                    break;
                case 21:
                    // 自行情報完了訂正
                    strSql += "  AND ( " +
                              "        HOSEIJIKOU.INPT_STS IN (" + HoseiStatus.InputStatus.完了訂正中 + "," + HoseiStatus.InputStatus.完了訂正保留 + ") " +
                              "  ) ";
                    break;
                case 30:
                    // 交換尻完了
                    strSql += "  AND ( " +
                              "        HOSEIICBK.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEICDATE.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEIAMT.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "    AND HOSEIKJIRI.INPT_STS = " + HoseiStatus.InputStatus.完了 + " " +
                              "  ) ";
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(entOpeNumer))
            {
                strSql += "   AND ( TRITEM_ID19.E_OPENO = '" + entOpeNumer + "' OR TRITEM_ID3.E_OPENO = '" + entOpeNumer + "' " +
                          "       OR TRITEM_ID6.E_OPENO = '" + entOpeNumer + "' OR TRITEM_ID10.E_OPENO = '" + entOpeNumer + "' " + " ) ";
            }
            if (!string.IsNullOrEmpty(veriOpeNumer))
            {
                strSql += "   AND ( TRITEM_ID19.V_OPENO = '" + veriOpeNumer + "' OR TRITEM_ID3.V_OPENO = '" + veriOpeNumer + "' " +
                          "       OR TRITEM_ID6.V_OPENO = '" + veriOpeNumer + "' OR TRITEM_ID10.V_OPENO = '" + veriOpeNumer + "' " + " ) ";
            }
            switch (TeiseiFlg)
            {
                case 0:
                    // なし
                    strSql += " AND ( HOSEIICBK.INPT_STS = " + HoseiStatus.InputStatus.完了 + " AND HOSEICDATE.INPT_STS = " + HoseiStatus.InputStatus.完了 + " AND HOSEIAMT.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ) ";
                    strSql += " AND ( NVL(TRITEM_ID1.CTR_DATA, ' ') = NVL(TRITEM_ID1.END_DATA, ' ') AND NVL(TRITEM_ID5.CTR_DATA, ' ') = NVL(TRITEM_ID5.END_DATA, ' ') " +
                              "  AND NVL(TRITEM_ID6.CTR_DATA, ' ') = NVL(TRITEM_ID6.END_DATA, ' ') ) ";
                    break;
                case 1:
                    // あり
                    strSql += " AND ( HOSEIICBK.INPT_STS = " + HoseiStatus.InputStatus.完了 + " AND HOSEICDATE.INPT_STS = " + HoseiStatus.InputStatus.完了 + " AND HOSEIAMT.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ) ";
                    strSql += " AND ( NVL(TRITEM_ID1.CTR_DATA, ' ') <> NVL(TRITEM_ID1.END_DATA, ' ') OR NVL(TRITEM_ID5.CTR_DATA, ' ') <> NVL(TRITEM_ID5.END_DATA, ' ') " +
                              "  OR NVL(TRITEM_ID6.CTR_DATA, ' ') <> NVL(TRITEM_ID6.END_DATA, ' ') ) ";
                    break;
                default:
                    break;
            }
            switch (FuwatariFlg)
            {
                case 0:
                    // なし
                    strSql += " AND TRFUWATARI.GYM_ID IS NULL ";
                    break;
                case 1:
                    // あり(不渡データありで未削除)
                    strSql += " AND ( TRFUWATARI.GYM_ID IS NOT NULL AND TRFUWATARI.DELETE_FLG = 0 ) ";
                    break;
                case 2:
                    // 取消(不渡データありで削除済)
                    strSql += " AND ( TRFUWATARI.GYM_ID IS NOT NULL AND TRFUWATARI.DELETE_FLG = 1 ) ";
                    break;
                default:
                    break;
            }
            if (GMASts > -1)
            {
                strSql += "  AND TRMEI.GMA_STS = " + GMASts + " ";
            }
            if (GRASts > -1)
            {
                strSql += "  AND TRMEI.GRA_STS = " + GRASts + " ";
            }
            if (!string.IsNullOrEmpty(ImgFLNm))
            {
                strSql +=
                    "  AND EXISTS (  " +
                    "       SELECT 1 " +
                    "       FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                    "       WHERE TRMEIIMG.GYM_ID = TRMEI.GYM_ID " +
                    "         AND TRMEIIMG.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                    "         AND TRMEIIMG.SCAN_TERM = TRMEI.SCAN_TERM " +
                    "         AND TRMEIIMG.BAT_ID = TRMEI.BAT_ID " +
                    "         AND TRMEIIMG.DETAILS_NO = TRMEI.DETAILS_NO ";
                switch (ImgFLNmOpt)
                {
                    case 1:
                        strSql += "  AND TRMEIIMG.IMG_FLNM LIKE '" + ImgFLNm + "%' ";
                        break;
                    case 2:
                        strSql += "  AND TRMEIIMG.IMG_FLNM LIKE '%" + ImgFLNm + "' ";
                        break;
                    default:
                        strSql += "  AND TRMEIIMG.IMG_FLNM = '" + ImgFLNm + "' ";
                        break;
                }

                strSql += " ) ";
            }

            // ORDER BY
            if (DispSortType)
            {
                // 印鑑照合モード
                strSql += " ORDER BY ";
                strSql += "  CASE WHEN HOSEIICBK.INPT_STS = " + HoseiStatus.InputStatus.完了 + " AND HOSEICDATE.INPT_STS = " + HoseiStatus.InputStatus.完了 + " AND HOSEIAMT.INPT_STS = " + HoseiStatus.InputStatus.完了 + " AND HOSEIJIKOU.INPT_STS >= " + HoseiStatus.InputStatus.完了 + " THEN 0 ELSE 1 END, ";
                strSql += "  TRITEM_ID5.END_DATA, TRITEM_ID13.END_DATA, TRITEM_ID16.END_DATA, TRITEM_ID18.END_DATA ";
            }
            else
            {
                strSql += " ORDER BY TRMEI.OPERATION_DATE, TRMEI.BAT_ID, TRMEI.DETAILS_NO ";
            }

            //取得件数制御
            strSql = "SELECT * FROM ( " + strSql + " ) WHERE ROWNUM <= " + (ListDispLimit + 1) + " ";

            return strSql;
        }

        /// <summary>
        /// 持帰明細照会での対象データ取得SQL
        /// </summary>
        /// <returns></returns>
        public static string GetIcMeiListRow(int gymid, int operationdate, string scanterm, int batid, int detailsno, int Schemabankcd)
        {
            //共通部取得
            string strSql = GetIcMeiListCommon(Schemabankcd);
            //条件設定
            strSql += " WHERE TRMEI.GYM_ID = " + gymid + " " +
                      "   AND TRMEI.OPERATION_DATE = " + operationdate + " " +
                      "   AND TRMEI.SCAN_TERM = '" + scanterm + "' " +
                      "   AND TRMEI.BAT_ID = " + batid + " " +
                      "   AND TRMEI.DETAILS_NO = " + detailsno + " ";

            return strSql;
        }

        /// <summary>
        /// 持帰明細照会での一覧取得SQL(共通)
        /// </summary>
        /// <returns></returns>
        public static string GetIcMeiListCommon(int Schemabankcd)
        {
            string strSql =
                " SELECT TRMEI.GYM_ID " +
                "      , TRMEI.OPERATION_DATE " +
                "      , TRMEI.SCAN_TERM " +
                "      , TRMEI.BAT_ID " +
                "      , TRMEI.DETAILS_NO " +
                "      , TRMEI.IC_OC_BK_NO " +
                "      , TRMEI.GMA_STS " +
                "      , TRMEI.GRA_STS " +
                "      , TRMEI.BUB_DATE " +
                "      , TRITEM_ID19.CTR_DATA INPT_CTRICBKNO " +
                "      , TRITEM_ID19.END_DATA INPT_ICBKNO " +
                "      , TRITEM_ID1.CTR_DATA CTRICBKNO " +
                "      , TRITEM_ID1.END_DATA ICBKNO " +
                "      , TRITEM_ID3.CTR_DATA INPT_CTRCLEARING_DATE " +
                "      , TRITEM_ID3.END_DATA INPT_CLEARING_DATE " +
                "      , TRITEM_ID5.CTR_DATA CTRCLEARING_DATE " +
                "      , TRITEM_ID5.END_DATA CLEARING_DATE " +
                "      , TRITEM_ID6.CTR_DATA CTRAMT " +
                "      , TRITEM_ID6.END_DATA AMT " +
                "      , TRITEM_ID7.END_DATA PAYKBN " +
                "      , TRITEM_ID8.CTR_DATA CTRBILLCD " +
                "      , TRITEM_ID8.END_DATA BILLCD " +
                "      , TRITEM_ID10.CTR_DATA CTRSYURUICD " +
                "      , TRITEM_ID10.END_DATA SYURUICD " +
                "      , TRITEM_ID12.CTR_DATA INPT_CTRICBRNO " +
                "      , TRITEM_ID12.END_DATA INPT_ICBRNO " +
                "      , TRITEM_ID13.CTR_DATA CTRICBRNO " +
                "      , TRITEM_ID13.END_DATA ICBRNO " +
                "      , TRITEM_ID15.CTR_DATA INPT_CTRACCOUNT " +
                "      , TRITEM_ID15.END_DATA INPT_ACCOUNT " +
                "      , TRITEM_ID16.CTR_DATA CTRACCOUNT " +
                "      , TRITEM_ID16.END_DATA ACCOUNT " +
                "      , TRITEM_ID18.CTR_DATA CTRTEGATA " +
                "      , TRITEM_ID18.END_DATA TEGATA " +
                "      , TRITEM_ID19.E_OPENO ICBK_EOPENO " +
                "      , TRITEM_ID19.V_OPENO ICBK_VOPENO " +
                "      , TRITEM_ID3.E_OPENO CDATE_EOPENO " +
                "      , TRITEM_ID3.V_OPENO CDATE_VOPENO " +
                "      , TRITEM_ID6.E_OPENO AMT_EOPENO " +
                "      , TRITEM_ID6.V_OPENO AMT_VOPENO " +
                "      , TRITEM_ID10.E_OPENO JIKOU_EOPENO " +
                "      , TRITEM_ID10.V_OPENO JIKOU_VOPENO " +
                "      , HOSEIICBK.INPT_STS ICBKINPTSTS " +
                "      , HOSEICDATE.INPT_STS CDATEINPTSTS " +
                "      , HOSEIAMT.INPT_STS AMTINPTSTS " +
                "      , HOSEIJIKOU.INPT_STS JIKOUINPTSTS " +
                "      , HOSEIJIKOU.TMNO JIKOUTMNO " +
                "      , HOSEIKJIRI.INPT_STS KOUKANJIRIINPTSTS " +
                "      , HOSEIKJIRI.TMNO KOUKANJIRITMNO " +
                "      , TRFUWATARI.GYM_ID FUWATARI " +
                "      , TRFUWATARI.DELETE_FLG FUWATARI_DELETE_FLG " +
                "      , TRMEI.DELETE_FLG " +
                " FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID1 " +
                "       ON TRITEM_ID1.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID1.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID1.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID1.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID1.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID1.ITEM_ID = 1 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID3 " +
                "       ON TRITEM_ID3.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID3.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID3.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID3.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID3.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID3.ITEM_ID = 3 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                "       ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID5.ITEM_ID = 5 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID6 " +
                "       ON TRITEM_ID6.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID6.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID6.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID6.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID6.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID6.ITEM_ID = 6 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID7 " +
                "       ON TRITEM_ID7.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID7.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID7.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID7.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID7.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID7.ITEM_ID = 7 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID8 " +
                "       ON TRITEM_ID8.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID8.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID8.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID8.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID8.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID8.ITEM_ID = 8 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID10 " +
                "       ON TRITEM_ID10.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID10.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID10.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID10.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID10.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID10.ITEM_ID = 10 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID12 " +
                "       ON TRITEM_ID12.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID12.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID12.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID12.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID12.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID12.ITEM_ID = 12 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID13 " +
                "       ON TRITEM_ID13.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID13.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID13.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID13.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID13.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID13.ITEM_ID = 13 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID15 " +
                "       ON TRITEM_ID15.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID15.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID15.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID15.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID15.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID15.ITEM_ID = 15 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID16 " +
                "       ON TRITEM_ID16.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID16.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID16.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID16.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID16.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID16.ITEM_ID = 16 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID18 " +
                "       ON TRITEM_ID18.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID18.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID18.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID18.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID18.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID18.ITEM_ID = 18 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID19 " +
                "       ON TRITEM_ID19.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID19.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID19.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID19.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID19.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID19.ITEM_ID = 19 " +
                "      LEFT JOIN " +
                "      " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEIICBK " +
                "       ON HOSEIICBK.GYM_ID = TRMEI.GYM_ID " +
                "      AND HOSEIICBK.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND HOSEIICBK.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND HOSEIICBK.BAT_ID = TRMEI.BAT_ID " +
                "      AND HOSEIICBK.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND HOSEIICBK.HOSEI_INPTMODE = 1 " +
                "      LEFT JOIN " +
                "      " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEICDATE " +
                "       ON HOSEICDATE.GYM_ID = TRMEI.GYM_ID " +
                "      AND HOSEICDATE.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND HOSEICDATE.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND HOSEICDATE.BAT_ID = TRMEI.BAT_ID " +
                "      AND HOSEICDATE.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND HOSEICDATE.HOSEI_INPTMODE = 2 " +
                "      LEFT JOIN " +
                "      " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEIAMT " +
                "       ON HOSEIAMT.GYM_ID = TRMEI.GYM_ID " +
                "      AND HOSEIAMT.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND HOSEIAMT.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND HOSEIAMT.BAT_ID = TRMEI.BAT_ID " +
                "      AND HOSEIAMT.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND HOSEIAMT.HOSEI_INPTMODE = 3 " +
                "      LEFT JOIN " +
                "      " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEIJIKOU " +
                "       ON HOSEIJIKOU.GYM_ID = TRMEI.GYM_ID " +
                "      AND HOSEIJIKOU.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND HOSEIJIKOU.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND HOSEIJIKOU.BAT_ID = TRMEI.BAT_ID " +
                "      AND HOSEIJIKOU.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND HOSEIJIKOU.HOSEI_INPTMODE = 4 " +
                "      LEFT JOIN " +
                "      " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " HOSEIKJIRI " +
                "       ON HOSEIKJIRI.GYM_ID = TRMEI.GYM_ID " +
                "      AND HOSEIKJIRI.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND HOSEIKJIRI.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND HOSEIKJIRI.BAT_ID = TRMEI.BAT_ID " +
                "      AND HOSEIKJIRI.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND HOSEIKJIRI.HOSEI_INPTMODE = 5 " +
                "      LEFT JOIN " +
                "      " + TBL_TRFUWATARI.TABLE_NAME(Schemabankcd) + " TRFUWATARI " +
                "       ON TRFUWATARI.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRFUWATARI.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRFUWATARI.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRFUWATARI.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRFUWATARI.DETAILS_NO = TRMEI.DETAILS_NO ";

            return strSql;
        }

        /// <summary>
        /// 持帰明細照会での出力ファイル情報取得SQL
        /// </summary>
        /// <returns></returns>
        public static string GetICMeiFileOutputData(int Gymid, int OpeDate, string ScanTerm, int BatID, int Detail, int Schemabankcd)
        {
            string strSql =
                " SELECT TRMEI.GYM_ID " +
                "      , TRMEI.OPERATION_DATE " +
                "      , TRMEI.SCAN_TERM " +
                "      , TRMEI.BAT_ID " +
                "      , TRMEI.DETAILS_NO " +
                "      , DSP_ITEM.ITEM_ID " +
                "      , DSP_ITEM.ITEM_DISPNAME " +
                "      , DSP_ITEM.ITEM_TYPE " +
                "      , DSP_ITEM.ITEM_LEN " +
                "      , DSP_ITEM.POS " +
                "      , TRITEM.END_DATA " +
                " FROM " + TBL_DSP_ITEM.TABLE_NAME(Schemabankcd) + " DSP_ITEM " +
                "      INNER JOIN " +
                "      " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                "       ON TRMEI.GYM_ID = DSP_ITEM.GYM_ID " +
                "      AND TRMEI.DSP_ID = DSP_ITEM.DSP_ID " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM " +
                "       ON TRITEM.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM.ITEM_ID = DSP_ITEM.ITEM_ID " +
                " WHERE DSP_ITEM.POS > 0 " +
                "   AND TRMEI.GYM_ID = " + Gymid + " " +
                "   AND TRMEI.OPERATION_DATE = " + OpeDate + " " +
                "   AND TRMEI.SCAN_TERM = '" + ScanTerm + "' " +
                "   AND TRMEI.BAT_ID = " + BatID + " " +
                "   AND TRMEI.DETAILS_NO = " + Detail + " ";
           return strSql;
        }

        /// <summary>
        /// 持帰明細照会での出力ファイル情報取得SQL
        /// </summary>
        /// <returns></returns>
        public static string GetICMeiFileOutputInfo(int Gymid, int Schemabankcd)
        {
            string strSql =
                " SELECT DISTINCT " +
                "        DSP_ITEM.ITEM_ID " +
                "      , DSP_ITEM.ITEM_LEN " +
                "      , DSP_ITEM.POS " +
                " FROM " + TBL_DSP_ITEM.TABLE_NAME(Schemabankcd) + " DSP_ITEM " +
                " WHERE DSP_ITEM.POS > 0 " +
                "   AND DSP_ITEM.GYM_ID = " + Gymid + " " +
                " ORDER BY DSP_ITEM.POS "
                ;
            return strSql;
        }

        /// <summary>
        /// 持出・持帰明細照会でのロック解除更新SQL
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateUnlockIcMei(int Gymid, int OpeDate, string ScanTerm, int BatID, int Detail, int InputMode,
                                                  int InpuSts, int UpdateInpuSts, int Schemabankcd)
        {
            string strSql = "UPDATE " + TBL_HOSEI_STATUS.TABLE_NAME(Schemabankcd) + " SET " +
                TBL_HOSEI_STATUS.INPT_STS + "=" + UpdateInpuSts + ", " +
                TBL_HOSEI_STATUS.TMNO + "='' " +
                " WHERE " +
                TBL_HOSEI_STATUS.GYM_ID + "=" + Gymid + " AND " +
                TBL_HOSEI_STATUS.OPERATION_DATE + "=" + OpeDate + " AND " +
                TBL_HOSEI_STATUS.SCAN_TERM + "='" + ScanTerm + "' AND " +
                TBL_HOSEI_STATUS.BAT_ID + "=" + BatID + " AND " +
                TBL_HOSEI_STATUS.DETAILS_NO + "=" + Detail + " AND " +
                TBL_HOSEI_STATUS.HOSEI_INPTMODE + "=" + InputMode + " AND " +
                TBL_HOSEI_STATUS.INPT_STS + "=" + InpuSts;
            return strSql;
        }

        /// <summary>
        /// 通知照会での通知テキスト管理一覧を取得するSELECT文を作成します
        /// 通知照会
        /// </summary>
        /// <returns></returns>
        public static string GetSearchTsuchiTxtCtl(int RecvDate, string FileDivid, int Schemabankcd, int ListDispLimit)
        {
            string strSql = "SELECT * FROM " + TBL_TSUCHITXT_CTL.TABLE_NAME(Schemabankcd);
            string wk = " WHERE ";
            if (RecvDate > -1)
            {
                strSql += wk + TBL_TSUCHITXT_CTL.RECV_DATE + "=" + RecvDate + " ";
                wk = " AND ";
            }
            if (!string.IsNullOrEmpty(FileDivid))
            {
                strSql += wk + TBL_TSUCHITXT_CTL.FILE_DIVID + "='" + FileDivid + "' ";
                wk = " AND ";
            }
            strSql += " ORDER BY " + TBL_TSUCHITXT_CTL.RECV_DATE + " DESC , " + TBL_TSUCHITXT_CTL.RECV_TIME + " DESC "; ;

            //取得件数制御
            strSql = "SELECT * FROM ( " + strSql + " ) WHERE ROWNUM <= " + (ListDispLimit + 1) + " ";

            return strSql;
        }

        /// <summary>
        /// 通知照会での通知テキストのイメージファイル名からイメージ情報を取得するSELECT文を作成します
        /// 通知照会
        /// </summary>
        /// <returns></returns>
        public static string GetTsuchiTxtImgData(int gymid, string imgfilename, int Schemabankcd)
        {
            string strSql = "SELECT TRMEIIMG.*,BATCH.INPUT_ROUTE ";
            strSql += " FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                      "  LEFT JOIN " +
                         TBL_TRBATCH.TABLE_NAME(Schemabankcd) + " BATCH " +
                      " ON " +
                      "       TRMEIIMG.GYM_ID = BATCH.GYM_ID " +
                      "   AND TRMEIIMG.OPERATION_DATE = BATCH.OPERATION_DATE " +
                      "   AND TRMEIIMG.SCAN_TERM = BATCH.SCAN_TERM " +
                      "   AND TRMEIIMG.BAT_ID = BATCH.BAT_ID " +
                      " WHERE TRMEIIMG.GYM_ID = " + gymid + " " +
                      "   AND EXISTS (  " +
                      "        SELECT 1 " +
                      "        FROM " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " IMG " +
                      "        WHERE IMG.GYM_ID = TRMEIIMG.GYM_ID " +
                      "          AND IMG.OPERATION_DATE = TRMEIIMG.OPERATION_DATE " +
                      "          AND IMG.SCAN_TERM = TRMEIIMG.SCAN_TERM " +
                      "          AND IMG.BAT_ID = TRMEIIMG.BAT_ID " +
                      "          AND IMG.DETAILS_NO = TRMEIIMG.DETAILS_NO " +
                      "          AND IMG.IMG_FLNM = '" + imgfilename + "' " +
                      "       ) ";
            return strSql;
        }

        /// <summary>
        /// 持出銀行別照会(持帰)での検索SQL取得
        /// </summary>
        /// <returns></returns>
        public static string GetSearchIcBkView(int gymid, int opedate, int clearingdate, int bkcode, int Schemabankcd, int ListDispLimit)
        {
            string strSql = "SELECT MEI.IC_OC_BK_NO, ID5.END_DATA CLEARING_DATE, COUNT(MEI.DETAILS_NO) MEICOUNT, SUM(ID6.END_DATA) AMT ";
            strSql += "FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " MEI ";
            strSql += " LEFT JOIN " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " ID5 " +
                      "  ON " +
                      "      MEI.GYM_ID = ID5.GYM_ID " +
                      "  AND MEI.OPERATION_DATE = ID5.OPERATION_DATE " +
                      "  AND MEI.SCAN_TERM = ID5.SCAN_TERM " +
                      "  AND MEI.BAT_ID = ID5.BAT_ID " +
                      "  AND MEI.DETAILS_NO = ID5.DETAILS_NO " +
                      "  AND ID5.ITEM_ID = 5 ";
            strSql += " LEFT JOIN " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " ID6 " +
                      "  ON " +
                      "      MEI.GYM_ID = ID6.GYM_ID " +
                      "  AND MEI.OPERATION_DATE = ID6.OPERATION_DATE " +
                      "  AND MEI.SCAN_TERM = ID6.SCAN_TERM " +
                      "  AND MEI.BAT_ID = ID6.BAT_ID " +
                      "  AND MEI.DETAILS_NO = ID6.DETAILS_NO " +
                      "  AND ID6.ITEM_ID = 6 ";
            strSql += " WHERE MEI.GYM_ID = " + gymid + " " +
                      "   AND MEI.DELETE_FLG = 0 ";
            if (opedate > -1)
            {
                // 設定がある場合
                strSql += " AND MEI.OPERATION_DATE = " + opedate + " ";
            }
            if (bkcode > -1)
            {
                // 設定がある場合
                strSql += " AND MEI.IC_OC_BK_NO = " + bkcode + " ";
            }
            if (clearingdate > -1)
            {
                // 設定がある場合
                strSql += " AND ID5.END_DATA = '" + clearingdate + "' ";
            }
            strSql += " GROUP BY MEI.IC_OC_BK_NO, ID5.END_DATA ";
            strSql += " ORDER BY MEI.IC_OC_BK_NO, ID5.END_DATA ";

            //取得件数制御
            strSql = "SELECT * FROM ( " + strSql + " ) WHERE ROWNUM <= " + (ListDispLimit + 1) + " ";

            return strSql;
        }

        /// <summary>
        /// 交換尻証券テキスト照会での検索SQL取得
        /// </summary>
        /// <returns></returns>
        public static string GetSearchBalanceTxtData(int gymid, string FileDivid, int CtlDate, int OCBKNo, int Date, int ICBKNo, 
                                                     long Amount, int PayKbn, int ICFlg, int Diff, int Fuwatari, 
                                                     string ImgFLNm, int ImgFLNmOpt, int Schemabankcd, int ListDispLimit)
        {

            // 不渡条件設定
            string strSqlJoinAdd = string.Empty; 
            switch (Fuwatari)
            {
                case 1:
                    // 含む
                    strSqlJoinAdd += " AND ( TRMEI.DELETE_FLG = 0 OR (TRMEI.DELETE_FLG = 1 AND (TRMEI.GRA_CONFIRMDATE <> 0 OR TRMEI.GRA_DATE <> 0)) ) ";
                    break;
                default:
                    // 指定なし
                    strSqlJoinAdd += " AND TRMEI.DELETE_FLG = 0 ";
                    break;
            }

            string strSql =
                " SELECT BILLMEITXT.* " +
                "      , CASE " +
                "         WHEN NVL(BILLMEITXT.CHG_OC_BK_NO, 'ZZZZ') <> 'ZZZZ' THEN BILLMEITXT.CHG_OC_BK_NO " +
                "         ELSE BILLMEITXT.FILE_OC_BK_NO " +
                "        END OCBKNO " +
                "      , TRITEM_ID1.END_DATA ITEMICBKNO " +
                "      , TRITEM_ID5.END_DATA ITEMCLEARING_DATE " +
                "      , TRITEM_ID6.END_DATA ITEMAMT " +
                "      , TRMEI.GYM_ID " +
                "      , TRMEI.DELETE_FLG " +
                "      , TRMEI.GRA_CONFIRMDATE " +
                "      , TRMEI.GRA_DATE " +
                " FROM " + TBL_BILLMEITXT.TABLE_NAME(Schemabankcd) + " BILLMEITXT " +
                "      INNER JOIN " +
                "      " + TBL_BILLMEITXT_CTL.TABLE_NAME(Schemabankcd) + " CTL " +
                "        ON CTL.TXTNAME = BILLMEITXT.TXTNAME " +
                "      AND FILE_DIVID IN(" + FileDivid + ") " +
                "      LEFT JOIN " +
                "      " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG " +
                "        ON TRMEIIMG.IMG_FLNM = BILLMEITXT.IMG_NAME " +
                "      AND TRMEIIMG.GYM_ID = " + gymid + " " +
                "      LEFT JOIN " +
                "      " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                "       ON TRMEI.GYM_ID = TRMEIIMG.GYM_ID " +
                "      AND TRMEI.OPERATION_DATE = TRMEIIMG.OPERATION_DATE " +
                "      AND TRMEI.SCAN_TERM = TRMEIIMG.SCAN_TERM " +
                "      AND TRMEI.BAT_ID = TRMEIIMG.BAT_ID " +
                "      AND TRMEI.DETAILS_NO = TRMEIIMG.DETAILS_NO " +
                //"      AND TRMEI.DELETE_FLG = 0 " +
                "     " + strSqlJoinAdd + // 不渡条件指定
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID1 " +
                "       ON TRITEM_ID1.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID1.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID1.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID1.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID1.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID1.ITEM_ID = 1 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                "       ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID5.ITEM_ID = 5 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID6 " +
                "       ON TRITEM_ID6.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID6.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID6.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID6.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID6.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID6.ITEM_ID = 6 " +
                " WHERE BILLMEITXT.IMG_KBN = 1 ";

            if (CtlDate > -1)
            {
                // 作成日
                strSql += "   AND CTL.CREATE_DATE = '" + CtlDate + "' ";
            }
            if (OCBKNo > -1)
            {
                // 持出銀行コード
                strSql += "   AND CASE " +
                          "        WHEN NVL(BILLMEITXT.CHG_OC_BK_NO, 'ZZZZ') <> 'ZZZZ' THEN BILLMEITXT.CHG_OC_BK_NO " +
                          "        ELSE BILLMEITXT.FILE_OC_BK_NO " +
                          "       END = '" + OCBKNo.ToString("D4") + "' ";
            }
            if (Date > -1)
            {
                // 交換日
                strSql += "   AND ( CASE WHEN BILLMEITXT.CLEARING_DATE <> 'ZZZZZZZZ' THEN BILLMEITXT.CLEARING_DATE " +
                          "              WHEN BILLMEITXT.TEISEI_CLEARING_DATE <> 'ZZZZZZZZ' THEN BILLMEITXT.TEISEI_CLEARING_DATE " +
                          "              ELSE BILLMEITXT.OC_CLEARING_DATE " +
                          "         END = '" + Date + "' " + 
                          "       ) ";
            }
            if (ICBKNo > -1)
            {
                // 決済持帰銀行コード
                strSql += "   AND ( CASE WHEN BILLMEITXT.PAYAFT_REV_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.PAYAFT_REV_IC_BK_NO " +
                          "              WHEN BILLMEITXT.PAY_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.PAY_IC_BK_NO " +
                          "              WHEN BILLMEITXT.TEISEI_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.TEISEI_IC_BK_NO " +
                          "              WHEN BILLMEITXT.CHG_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.CHG_IC_BK_NO " +
                          "              WHEN BILLMEITXT.FILE_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.FILE_IC_BK_NO " +
                          "              WHEN BILLMEITXT.QR_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.QR_IC_BK_NO " +
                          "              WHEN BILLMEITXT.OCR_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.OCR_IC_BK_NO " +
                          "              ELSE BILLMEITXT.MICR_IC_BK_NO " +
                          "         END = '" + ICBKNo.ToString("D4") + "' " +
                          "       ) ";
            }
            if (Amount > -1)
            {
                // 決済金額
                strSql += "   AND ( CASE WHEN BILLMEITXT.PAYAFT_REV_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.PAYAFT_REV_AMOUNT " +
                          "              WHEN BILLMEITXT.PAY_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.PAY_AMOUNT " +
                          "              WHEN BILLMEITXT.TEISEI_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.TEISEI_AMOUNT " +
                          "              WHEN BILLMEITXT.FILE_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.FILE_AMOUNT " +
                          "              WHEN BILLMEITXT.QR_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.QR_AMOUNT " +
                          "              WHEN BILLMEITXT.MICR_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.MICR_AMOUNT " +
                          "              ELSE BILLMEITXT.OCR_AMOUNT " +
                          "         END = '" + Amount.ToString("D12") + "' " +
                          "       ) ";
            }
            if (PayKbn > -1)
            {
                // 決済対象区分
                strSql += "   AND BILLMEITXT.PAY_KBN = '" + PayKbn + "' ";
            }
            if (ICFlg > -1)
            {
                // 持帰状況フラグ
                strSql += "   AND BILLMEITXT.IC_FLG = '" + ICFlg + "' ";
            }
            // 差異
            switch (Diff)
            {
                case 0:
                    // 無
                    strSql += "  AND ( " +
                              "           ( CASE WHEN BILLMEITXT.PAYAFT_REV_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.PAYAFT_REV_IC_BK_NO " +
                              "                  WHEN BILLMEITXT.PAY_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.PAY_IC_BK_NO " +
                              "                  WHEN BILLMEITXT.TEISEI_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.TEISEI_IC_BK_NO " +
                              "                  WHEN BILLMEITXT.CHG_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.CHG_IC_BK_NO " +
                              "                  WHEN BILLMEITXT.FILE_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.FILE_IC_BK_NO " +
                              "                  WHEN BILLMEITXT.QR_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.QR_IC_BK_NO " +
                              "                  WHEN BILLMEITXT.OCR_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.OCR_IC_BK_NO " +
                              "                  ELSE BILLMEITXT.MICR_IC_BK_NO " +
                              "             END = NVL(TRITEM_ID1.END_DATA, 'ZZZZ') " +
                              "           ) " +
                              "       AND ( CASE WHEN BILLMEITXT.CLEARING_DATE <> 'ZZZZZZZZ' THEN BILLMEITXT.CLEARING_DATE " +
                              "                  WHEN BILLMEITXT.TEISEI_CLEARING_DATE <> 'ZZZZZZZZ' THEN BILLMEITXT.TEISEI_CLEARING_DATE " +
                              "                  ELSE BILLMEITXT.OC_CLEARING_DATE " +
                              "             END = NVL(TRITEM_ID5.END_DATA, 'ZZZZZZZZ') " +
                              "           ) " +
                              "       AND ( CASE WHEN BILLMEITXT.PAYAFT_REV_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.PAYAFT_REV_AMOUNT " +
                              "                  WHEN BILLMEITXT.PAY_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.PAY_AMOUNT " +
                              "                  WHEN BILLMEITXT.TEISEI_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.TEISEI_AMOUNT " +
                              "                  WHEN BILLMEITXT.FILE_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.FILE_AMOUNT " +
                              "                  WHEN BILLMEITXT.QR_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.QR_AMOUNT " +
                              "                  WHEN BILLMEITXT.MICR_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.MICR_AMOUNT " +
                              "                  ELSE BILLMEITXT.OCR_AMOUNT " +
                              "             END = NVL(TRITEM_ID6.END_DATA, 'ZZZZZZZZZZZZ') " +
                              "           ) " +
                              "      ) ";
                    break;
                case 1:
                    // 有
                    strSql += "  AND ( " +
                              "           ( CASE WHEN BILLMEITXT.PAYAFT_REV_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.PAYAFT_REV_IC_BK_NO " +
                              "                  WHEN BILLMEITXT.PAY_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.PAY_IC_BK_NO " +
                              "                  WHEN BILLMEITXT.TEISEI_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.TEISEI_IC_BK_NO " +
                              "                  WHEN BILLMEITXT.CHG_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.CHG_IC_BK_NO " +
                              "                  WHEN BILLMEITXT.FILE_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.FILE_IC_BK_NO " +
                              "                  WHEN BILLMEITXT.QR_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.QR_IC_BK_NO " +
                              "                  WHEN BILLMEITXT.OCR_IC_BK_NO <> 'ZZZZ' THEN BILLMEITXT.OCR_IC_BK_NO " +
                              "                  ELSE BILLMEITXT.MICR_IC_BK_NO " +
                              "             END <> NVL(TRITEM_ID1.END_DATA, 'ZZZZ') " +
                              "           ) " +
                              "        OR ( CASE WHEN BILLMEITXT.CLEARING_DATE <> 'ZZZZZZZZ' THEN BILLMEITXT.CLEARING_DATE " +
                              "                  WHEN BILLMEITXT.TEISEI_CLEARING_DATE <> 'ZZZZZZZZ' THEN BILLMEITXT.TEISEI_CLEARING_DATE " +
                              "                  ELSE BILLMEITXT.OC_CLEARING_DATE " +
                              "             END <> NVL(TRITEM_ID5.END_DATA, 'ZZZZZZZZ') " +
                              "           ) " +
                              "        OR ( CASE WHEN BILLMEITXT.PAYAFT_REV_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.PAYAFT_REV_AMOUNT " +
                              "                  WHEN BILLMEITXT.PAY_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.PAY_AMOUNT " +
                              "                  WHEN BILLMEITXT.TEISEI_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.TEISEI_AMOUNT " +
                              "                  WHEN BILLMEITXT.FILE_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.FILE_AMOUNT " +
                              "                  WHEN BILLMEITXT.QR_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.QR_AMOUNT " +
                              "                  WHEN BILLMEITXT.MICR_AMOUNT <> 'ZZZZZZZZZZZZ' THEN BILLMEITXT.MICR_AMOUNT " +
                              "                  ELSE BILLMEITXT.OCR_AMOUNT " +
                              "             END <> NVL(TRITEM_ID6.END_DATA, 'ZZZZZZZZZZZZ') " +
                              "           ) " +
                              "      ) ";
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(ImgFLNm))
            {
                // ｲﾒｰｼﾞﾌｧｲﾙ名
                switch (ImgFLNmOpt)
                {
                    case 1:
                        strSql += "  AND BILLMEITXT.IMG_NAME LIKE '" + ImgFLNm + "%' ";
                        break;
                    case 2:
                        strSql += "  AND BILLMEITXT.IMG_NAME LIKE '%" + ImgFLNm + "' ";
                        break;
                    default:
                        strSql += "  AND BILLMEITXT.IMG_NAME = '" + ImgFLNm + "' ";
                        break;
                }
            }

            // ORDER BY 
            strSql += " ORDER BY BILLMEITXT.TXTNAME, BILLMEITXT.IMG_NAME ";

            //取得件数制御
            strSql = "SELECT * FROM ( " + strSql + " ) WHERE ROWNUM <= " + (ListDispLimit + 1) + " ";

            return strSql;
        }

        /// <summary>
        /// 交換尻証券テキスト照会での検索SQL取得(件数取得)
        /// (パッケージのみデータ)
        /// </summary>
        /// <returns></returns>
        public static string GetSearchBalanceTxtPkgOnlyCount(int gymid, string FileDivid, int CtlDate, int InternalExchange,
                                                             string ImgFLNm, int ImgFLNmOpt, int Schemabankcd)
        {
            // 共通部分取得
            string strSql = GetSearchBalanceTxtPkgOnlyCommon(gymid, FileDivid, CtlDate, InternalExchange, ImgFLNm, ImgFLNmOpt, Schemabankcd);

            // 件数取得
            strSql = "SELECT COUNT(*) CNT FROM ( " + strSql + " ) ";

            return strSql;
        }

        /// <summary>
        /// 交換尻証券テキスト照会での検索SQL取得
        /// (パッケージのみデータ)
        /// </summary>
        /// <returns></returns>
        public static string GetSearchBalanceTxtPkgOnlyData(int gymid, string FileDivid, int CtlDate, int InternalExchange,
                                                            string ImgFLNm, int ImgFLNmOpt, int Schemabankcd, int ListDispLimit)
        {
            // 共通部分取得
            string strSql = GetSearchBalanceTxtPkgOnlyCommon(gymid, FileDivid, CtlDate, InternalExchange, ImgFLNm, ImgFLNmOpt, Schemabankcd);

            // ORDER BY 
            strSql += " ORDER BY TRMEIIMG_F.IMG_FLNM ";

            //取得件数制御
            strSql = "SELECT * FROM ( " + strSql + " ) WHERE ROWNUM <= " + (ListDispLimit + 1) + " ";

            return strSql;
        }

        /// <summary>
        /// 交換尻証券テキスト照会での検索SQL取得(共通)
        /// (パッケージのみデータ)
        /// </summary>
        /// <returns></returns>
        private static string GetSearchBalanceTxtPkgOnlyCommon(int gymid, string FileDivid, int CtlDate, int InternalExchange, 
                                                               string ImgFLNm, int ImgFLNmOpt, int Schemabankcd)
        {
            string strSql =
                " SELECT TRMEI.GYM_ID " +
                "      , TRMEI.DELETE_FLG " +
                "      , TRMEI.GRA_CONFIRMDATE " +
                "      , TRMEI.GRA_DATE " +
                "      , TRMEIIMG_F.IMG_FLNM " +
                "      , TRITEM_ID1.END_DATA ITEMICBKNO " +
                "      , TRITEM_ID5.END_DATA ITEMCLEARING_DATE " +
                "      , TRITEM_ID6.END_DATA ITEMAMT " +
                " FROM " + TBL_TRMEI.TABLE_NAME(Schemabankcd) + " TRMEI " +
                "      LEFT JOIN " +
                "      " + TBL_TRMEIIMG.TABLE_NAME(Schemabankcd) + " TRMEIIMG_F " +
                "       ON TRMEIIMG_F.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRMEIIMG_F.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRMEIIMG_F.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRMEIIMG_F.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRMEIIMG_F.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRMEIIMG_F.IMG_KBN = " + TrMeiImg.ImgKbn.表 + " " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID1 " +
                "       ON TRITEM_ID1.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID1.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID1.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID1.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID1.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID1.ITEM_ID = 1 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID5 " +
                "       ON TRITEM_ID5.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID5.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID5.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID5.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID5.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID5.ITEM_ID = 5 " +
                "      LEFT JOIN " +
                "      " + TBL_TRITEM.TABLE_NAME(Schemabankcd) + " TRITEM_ID6 " +
                "       ON TRITEM_ID6.GYM_ID = TRMEI.GYM_ID " +
                "      AND TRITEM_ID6.OPERATION_DATE = TRMEI.OPERATION_DATE " +
                "      AND TRITEM_ID6.SCAN_TERM = TRMEI.SCAN_TERM " +
                "      AND TRITEM_ID6.BAT_ID = TRMEI.BAT_ID " +
                "      AND TRITEM_ID6.DETAILS_NO = TRMEI.DETAILS_NO " +
                "      AND TRITEM_ID6.ITEM_ID = 6 " +
                " WHERE TRMEI.GYM_ID = " + gymid + " " +
                "   AND TRMEI.DELETE_FLG = 0 " +
                // BILLMEITXTにある明細は除外
                "   AND NOT EXISTS ( " +
                "             SELECT 1 " +
                "             FROM " + TBL_BILLMEITXT.TABLE_NAME(Schemabankcd) + " BILLMEITXT " +
                "                  INNER JOIN " +
                "                  " + TBL_BILLMEITXT_CTL.TABLE_NAME(Schemabankcd) + " CTL " +
                "                    ON CTL.TXTNAME = BILLMEITXT.TXTNAME " +
                "                  AND FILE_DIVID IN(" + FileDivid + ") " +
                "             WHERE BILLMEITXT.IMG_KBN = 1 " +
                "               AND BILLMEITXT.IMG_NAME = TRMEIIMG_F.IMG_FLNM " +
                "       ) ";

            // 行内連携
            switch (InternalExchange)
            {
                case 1:
                    // 行内連携有効

                    // 行内連携データは除外
                    if (gymid == GymParam.GymId.持出)
                    {
                        // 持帰銀行が同じデータは除外
                        strSql += "   AND NVL(TRITEM_ID1.END_DATA, ' ') <> '" + Schemabankcd.ToString("D4") + "' ";
                    }
                    else if (gymid == GymParam.GymId.持帰)
                    {
                        // 持出銀行が同じデータは除外
                        strSql += "   AND TRMEI.IC_OC_BK_NO <> " + Schemabankcd + " ";
                    }
                    break;
                default:
                    break;
            }

            if (CtlDate > -1)
            {
                // 作成日(TRITEMの交換日で検索)
                strSql += "   AND TRITEM_ID5.END_DATA = '" + CtlDate + "' ";
            }

            if (!string.IsNullOrEmpty(ImgFLNm))
            {
                // ｲﾒｰｼﾞﾌｧｲﾙ名
                switch (ImgFLNmOpt)
                {
                    case 1:
                        strSql += "  AND TRMEIIMG_F.IMG_FLNM LIKE '" + ImgFLNm + "%' ";
                        break;
                    case 2:
                        strSql += "  AND TRMEIIMG_F.IMG_FLNM LIKE '%" + ImgFLNm + "' ";
                        break;
                    default:
                        strSql += "  AND TRMEIIMG_F.IMG_FLNM = '" + ImgFLNm + "' ";
                        break;
                }
            }

            return strSql;
        }

    }
}
