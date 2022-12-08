using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTable.DB;

namespace CommonTable.DB
{
    public class SQLEntry
    {
        /// <summary>検索モード</summary>
        public enum SearchType
        {
            Type1,
            Type2,
        }

        /// <summary>
        /// 呼出画面：明細一覧
        /// 取得内容：明細一覧（画面検索）
        /// </summary>
        /// <param name="type">Type1:残表示、Type2:全表示</param>
        /// <param name="gymid"></param>
        /// <param name="hoseiInputMode"></param>
        /// <param name="opdate"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetBatchListSelect(SearchType type, int gymid, int hoseiInputMode, int opdate, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " SELECT ";
            // キー項目
            strSQL += "      MEI.GYM_ID ";
            strSQL += "     ,MEI.OPERATION_DATE ";
            strSQL += "     ,MEI.SCAN_TERM ";
            strSQL += "     ,MEI.BAT_ID ";
            strSQL += "     ,MEI.DETAILS_NO ";
            // 画面項目（持出）
            strSQL += "     ,BAT.OC_BR_NO ";                                                                                           // 持出支店
            strSQL += "     , " + GetTrItem("END_DATA", "ENT_DATA", DspItem.ItemId.持帰銀行コード, "IC_BK_NO", SchemaBankCD);          // 持帰銀行
            strSQL += "     , " + GetTrItem("END_DATA", "ENT_DATA", DspItem.ItemId.持帰銀行名, "IC_BK_NAME", SchemaBankCD);            // 持帰銀行名
            strSQL += "     , " + GetTrItem("END_DATA", "ENT_DATA", DspItem.ItemId.入力交換希望日, "ITM_CLEARING_DATE", SchemaBankCD); // 交換希望日
            strSQL += "     , " + GetTrItem("END_DATA", "ENT_DATA", DspItem.ItemId.金額, "AMOUNT", SchemaBankCD);                      // 金額
            // 画面項目（持帰）
            strSQL += "     , " + GetTrItem("END_DATA", "ENT_DATA", DspItem.ItemId.交換証券種類コード, "BILL_CD", SchemaBankCD);       // 証券種類コード
            strSQL += "     , " + GetTrItem("END_DATA", "ENT_DATA", DspItem.ItemId.交換証券種類名, "BILL_NAME", SchemaBankCD);         // 証券種類名称
            strSQL += "     ,MEI.IC_OC_BK_NO ";                                                                                        // 持出銀行
            strSQL += "     , " + GetTrItem("END_DATA", "ENT_DATA", DspItem.ItemId.持帰支店コード, "IC_BR_NO", SchemaBankCD);          // 持帰支店
            strSQL += "     , " + GetTrItem("CTR_DATA", "CTR_DATA", DspItem.ItemId.入力交換希望日, "CTR_CLEARING_DATE", SchemaBankCD); // 電子交換所交換希望日
            strSQL += "     ,STS.TMNO ";
            strSQL += "     ,STS.INPT_STS ";
            // 持帰は TRBATCH がないので TRMEI を主テーブルにする
            strSQL += " FROM " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSQL += "     LEFT OUTER JOIN " + TBL_TRBATCH.TABLE_NAME(SchemaBankCD) + " BAT ";
            strSQL += "         ON  BAT.GYM_ID = MEI.GYM_ID ";
            strSQL += "         AND BAT.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "         AND BAT.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "         AND BAT.BAT_ID = MEI.BAT_ID ";
            strSQL += "         AND BAT.DELETE_FLG = 0 ";
            strSQL += "     INNER JOIN " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS ";
            strSQL += "         ON  MEI.GYM_ID = STS.GYM_ID ";
            strSQL += "         AND MEI.OPERATION_DATE = STS.OPERATION_DATE ";
            strSQL += "         AND MEI.SCAN_TERM = STS.SCAN_TERM ";
            strSQL += "         AND MEI.BAT_ID = STS.BAT_ID ";
            strSQL += "         AND MEI.DETAILS_NO = STS.DETAILS_NO ";
            strSQL += " WHERE ";
            strSQL += "         MEI.GYM_ID = " + gymid + " ";
            strSQL += "     AND MEI.DELETE_FLG = 0 ";
            strSQL += "     AND STS.HOSEI_INPTMODE = " + hoseiInputMode + " ";

            string strJikouSts = "";
            if (hoseiInputMode == HoseiStatus.HoseiInputMode.自行情報)
            {
                // 自行情報(交換尻完了のみ対象)
                strJikouSts += "     AND EXISTS ( ";
                strJikouSts += "         SELECT 1 FROM " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS_TMP1 ";
                strJikouSts += "         WHERE ";
                strJikouSts += "                 MEI.GYM_ID = STS_TMP1.GYM_ID ";
                strJikouSts += "             AND MEI.OPERATION_DATE = STS_TMP1.OPERATION_DATE ";
                strJikouSts += "             AND MEI.SCAN_TERM = STS_TMP1.SCAN_TERM ";
                strJikouSts += "             AND MEI.BAT_ID = STS_TMP1.BAT_ID ";
                strJikouSts += "             AND MEI.DETAILS_NO = STS_TMP1.DETAILS_NO ";
                strJikouSts += "             AND STS_TMP1.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.持帰銀行 + " ";
                strJikouSts += "             AND STS_TMP1.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ";
                strJikouSts += "     ) ";
                strJikouSts += "     AND EXISTS ( ";
                strJikouSts += "         SELECT 1 FROM " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS_TMP2 ";
                strJikouSts += "         WHERE ";
                strJikouSts += "                 MEI.GYM_ID = STS_TMP2.GYM_ID ";
                strJikouSts += "             AND MEI.OPERATION_DATE = STS_TMP2.OPERATION_DATE ";
                strJikouSts += "             AND MEI.SCAN_TERM = STS_TMP2.SCAN_TERM ";
                strJikouSts += "             AND MEI.BAT_ID = STS_TMP2.BAT_ID ";
                strJikouSts += "             AND MEI.DETAILS_NO = STS_TMP2.DETAILS_NO ";
                strJikouSts += "             AND STS_TMP2.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.交換希望日 + " ";
                strJikouSts += "             AND STS_TMP2.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ";
                strJikouSts += "     ) ";
                strJikouSts += "     AND EXISTS ( ";
                strJikouSts += "         SELECT 1 FROM " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS_TMP3 ";
                strJikouSts += "         WHERE ";
                strJikouSts += "                 MEI.GYM_ID = STS_TMP3.GYM_ID ";
                strJikouSts += "             AND MEI.OPERATION_DATE = STS_TMP3.OPERATION_DATE ";
                strJikouSts += "             AND MEI.SCAN_TERM = STS_TMP3.SCAN_TERM ";
                strJikouSts += "             AND MEI.BAT_ID = STS_TMP3.BAT_ID ";
                strJikouSts += "             AND MEI.DETAILS_NO = STS_TMP3.DETAILS_NO ";
                strJikouSts += "             AND STS_TMP3.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.金額 + " ";
                strJikouSts += "             AND STS_TMP3.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ";
                strJikouSts += "     ) ";
            }

            string strInputSts = "";
            if (type == SearchType.Type1)
            {
                // 残表示
                string sts = "";
                sts += string.Format("{0}{1}", "", HoseiStatus.InputStatus.エントリ待);
                sts += string.Format("{0}{1}", ",", HoseiStatus.InputStatus.エントリ保留);
                sts += string.Format("{0}{1}", ",", HoseiStatus.InputStatus.ベリファイ待);
                sts += string.Format("{0}{1}", ",", HoseiStatus.InputStatus.ベリファイ保留);
                // 補正中データも表示対象
                sts += string.Format("{0}{1}", ",", HoseiStatus.InputStatus.エントリ中);
                sts += string.Format("{0}{1}", ",", HoseiStatus.InputStatus.ベリファイ中);
                strInputSts += "     AND STS.INPT_STS IN (" + sts + ") ";
            }
            else if (type == SearchType.Type2)
            {
                // 全表示
                // ( 対象補正モードが未完了 ) OR
                // ( 対象補正モードが完了 AND ( 交換日のEND_DATAが処理日以降 OR 空 ) ) 
                strInputSts += "     AND ( ";
                strInputSts += "           STS.INPT_STS <> " + HoseiStatus.InputStatus.完了 + " ";
                strInputSts += "           OR ( ";
                strInputSts += "                STS.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ";
                strInputSts += "                AND EXISTS ( ";
                strInputSts += "                    SELECT 1 FROM " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " TR_CLEARINGDATE ";
                strInputSts += "                    WHERE ";
                strInputSts += "                            MEI.GYM_ID = TR_CLEARINGDATE.GYM_ID ";
                strInputSts += "                        AND MEI.OPERATION_DATE = TR_CLEARINGDATE.OPERATION_DATE ";
                strInputSts += "                        AND MEI.SCAN_TERM = TR_CLEARINGDATE.SCAN_TERM ";
                strInputSts += "                        AND MEI.BAT_ID = TR_CLEARINGDATE.BAT_ID ";
                strInputSts += "                        AND MEI.DETAILS_NO = TR_CLEARINGDATE.DETAILS_NO ";
                strInputSts += "                        AND TR_CLEARINGDATE.ITEM_ID = " + DspItem.ItemId.交換日 + " ";
                strInputSts += "                        AND ( NVL(TR_CLEARINGDATE.END_DATA, ' ') = ' ' OR TR_CLEARINGDATE.END_DATA >= '" + opdate.ToString("D8") + "' ) ";
                strInputSts += "                ) ";
                strInputSts += "           ) ";
                strInputSts += "         ) ";
            }

            // 並び順
            string strOrder = "";
            if (gymid == GymParam.GymId.持出)
            {
                strOrder += "     ITM_CLEARING_DATE, ";
                strOrder += "     STS.INPT_STS,";
                strOrder += "     MEI.BAT_ID, ";
                strOrder += "     MEI.DETAILS_NO ";
            }
            else //if (gymid == GymParam.GymId.持帰)
            {
                strOrder += "     CTR_CLEARING_DATE, ";
                strOrder += "     STS.INPT_STS,";
                strOrder += "     BILL_CD, ";
                strOrder += "     MEI.BAT_ID, ";
                strOrder += "     MEI.DETAILS_NO ";
            }

            strSQL += strJikouSts;
            strSQL += strInputSts;
            strSQL += " ORDER BY ";
            strSQL += strOrder;
            return strSQL;
        }

        /// <summary>
        /// ベリファイあり業務の補正パラメータを取得する
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetVfyHoseiParam(int gymid, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " SELECT * FROM ";
            strSQL += "     " + TBL_HOSEIMODE_PARAM.TABLE_NAME(SchemaBankCD) + " ";
            strSQL += " WHERE ";
            strSQL += "     GYM_ID = " + gymid + " ";
            strSQL += " AND VERY_MODE = 1 ";
            return strSQL;
        }

        /// <summary>
        /// 指定した項目名の TRITEM を取得する
        /// </summary>
        /// <param name="itemCol1"></param>
        /// <param name="itemCol2"></param>
        /// <param name="itemid"></param>
        /// <param name="dspName"></param>
        /// <returns></returns>
        private static string GetTrItem(string itemCol1, string itemCol2, int itemid, string dspName, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " ( ";
            strSQL += " SELECT ";
            strSQL += "     NVL(" + itemCol1 + ", " + itemCol2 + ") ";
            strSQL += " FROM ";
            strSQL += "     " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " IT ";
            strSQL += " WHERE ";
            strSQL += "         IT.GYM_ID = MEI.GYM_ID ";
            strSQL += "     AND IT.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "     AND IT.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "     AND IT.BAT_ID = MEI.BAT_ID ";
            strSQL += "     AND IT.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "     AND IT.ITEM_ID = " + itemid + " ";
            strSQL += " ) AS " + dspName + " ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：明細一覧
        /// 取得内容：明細一覧（自動配信）
        /// </summary>
        /// <param name="type">Type1:エントリ入力、Type2:ベリファイ入力</param>
        /// <param name="gymid"></param>
        /// <param name="hoseiInputMode"></param>
        /// <param name="opid"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetBatchListAutoReceiveSelect(SearchType type, int gymid, int hoseiInputMode, string opid, int ignoreCnt, int SchemaBankCD)
        {
            // 補正状態
            string strInputSts = "";
            if (type == SearchType.Type1)
            {
                // エントリ待のみ取得
                strInputSts += "     AND STS.INPT_STS IN (" + HoseiStatus.InputStatus.エントリ待 + ") ";
            }
            else // if (type == SearchType.Type2)
            {
                // ベリファイ待のみ取得
                // ベリファイはエントリーを行ったユーザ以外で行う
                strInputSts += "     AND STS.INPT_STS IN (" + HoseiStatus.InputStatus.ベリファイ待 + ") ";
                strInputSts += "     AND NVL(STS.E_OPENO, '0') <> '" + opid + "' ";
            }

            string strJikouSts = "";
            if (hoseiInputMode == HoseiStatus.HoseiInputMode.自行情報)
            {
                // 自行情報(交換尻完了のみ対象)
                strJikouSts += "     AND EXISTS ( ";
                strJikouSts += "         SELECT 1 FROM " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS_TMP1 ";
                strJikouSts += "         WHERE ";
                strJikouSts += "                 MEI.GYM_ID = STS_TMP1.GYM_ID ";
                strJikouSts += "             AND MEI.OPERATION_DATE = STS_TMP1.OPERATION_DATE ";
                strJikouSts += "             AND MEI.SCAN_TERM = STS_TMP1.SCAN_TERM ";
                strJikouSts += "             AND MEI.BAT_ID = STS_TMP1.BAT_ID ";
                strJikouSts += "             AND MEI.DETAILS_NO = STS_TMP1.DETAILS_NO ";
                strJikouSts += "             AND STS_TMP1.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.持帰銀行 + " ";
                strJikouSts += "             AND STS_TMP1.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ";
                strJikouSts += "     ) ";
                strJikouSts += "     AND EXISTS ( ";
                strJikouSts += "         SELECT 1 FROM " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS_TMP2 ";
                strJikouSts += "         WHERE ";
                strJikouSts += "                 MEI.GYM_ID = STS_TMP2.GYM_ID ";
                strJikouSts += "             AND MEI.OPERATION_DATE = STS_TMP2.OPERATION_DATE ";
                strJikouSts += "             AND MEI.SCAN_TERM = STS_TMP2.SCAN_TERM ";
                strJikouSts += "             AND MEI.BAT_ID = STS_TMP2.BAT_ID ";
                strJikouSts += "             AND MEI.DETAILS_NO = STS_TMP2.DETAILS_NO ";
                strJikouSts += "             AND STS_TMP2.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.交換希望日 + " ";
                strJikouSts += "             AND STS_TMP2.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ";
                strJikouSts += "     ) ";
                strJikouSts += "     AND EXISTS ( ";
                strJikouSts += "         SELECT 1 FROM " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS_TMP3 ";
                strJikouSts += "         WHERE ";
                strJikouSts += "                 MEI.GYM_ID = STS_TMP3.GYM_ID ";
                strJikouSts += "             AND MEI.OPERATION_DATE = STS_TMP3.OPERATION_DATE ";
                strJikouSts += "             AND MEI.SCAN_TERM = STS_TMP3.SCAN_TERM ";
                strJikouSts += "             AND MEI.BAT_ID = STS_TMP3.BAT_ID ";
                strJikouSts += "             AND MEI.DETAILS_NO = STS_TMP3.DETAILS_NO ";
                strJikouSts += "             AND STS_TMP3.HOSEI_INPTMODE = " + HoseiStatus.HoseiInputMode.金額 + " ";
                strJikouSts += "             AND STS_TMP3.INPT_STS = " + HoseiStatus.InputStatus.完了 + " ";
                strJikouSts += "     ) ";
            }

            // 共通
            string strSELECT = "";
			//（大量データテスト時の改善）
			strSELECT += " SELECT * FROM ("; // 2022/08/03 ADD

            strSELECT += " SELECT ";
            strSELECT += "      {0} ";
            strSELECT += "     , " + GetTrItem("END_DATA", "ENT_DATA", DspItem.ItemId.入力交換希望日, "ITM_CLEARING_DATE", SchemaBankCD); // （並替用）交換希望日
            strSELECT += "     , " + GetTrItem("CTR_DATA", "CTR_DATA", DspItem.ItemId.入力交換希望日, "CTR_CLEARING_DATE", SchemaBankCD); // （並替用）電子交換所交換希望日
            strSELECT += "     , " + GetTrItem("END_DATA", "ENT_DATA", DspItem.ItemId.交換証券種類コード, "BILL_CD", SchemaBankCD);       // （並替用）証券種類コード
            // 持帰は TRBATCH がないので TRMEI を主テーブルにする
            strSELECT += " FROM " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSELECT += "     LEFT OUTER JOIN " + TBL_TRBATCH.TABLE_NAME(SchemaBankCD) + " BAT ";
            strSELECT += "         ON  BAT.GYM_ID = MEI.GYM_ID ";
            strSELECT += "         AND BAT.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSELECT += "         AND BAT.SCAN_TERM = MEI.SCAN_TERM ";
            strSELECT += "         AND BAT.BAT_ID = MEI.BAT_ID ";
            strSELECT += "         AND BAT.DELETE_FLG = 0 ";
            strSELECT += "     INNER JOIN " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS ";
            strSELECT += "         ON  MEI.GYM_ID = STS.GYM_ID ";
            strSELECT += "         AND MEI.OPERATION_DATE = STS.OPERATION_DATE ";
            strSELECT += "         AND MEI.SCAN_TERM = STS.SCAN_TERM ";
            strSELECT += "         AND MEI.BAT_ID = STS.BAT_ID ";
            strSELECT += "         AND MEI.DETAILS_NO = STS.DETAILS_NO ";
            strSELECT += " WHERE ";
            strSELECT += "         MEI.GYM_ID = " + gymid + " ";
            strSELECT += "     AND MEI.DELETE_FLG = 0 ";
            strSELECT += "     AND STS.HOSEI_INPTMODE = " + hoseiInputMode + " ";
            strSELECT += strInputSts;
            strSELECT += strJikouSts;

            // 並び順
            string strOrder = "";
            if (gymid == GymParam.GymId.持出)
            {
                strOrder += "     ITM_CLEARING_DATE, ";
                strOrder += "     STS.INPT_STS,";
                strOrder += "     MEI.BAT_ID, ";
                strOrder += "     MEI.DETAILS_NO ";
            }
            else //if (gymid == GymParam.GymId.持帰)
            {
                strOrder += "     CTR_CLEARING_DATE, ";
                strOrder += "     STS.INPT_STS,";
                strOrder += "     BILL_CD, ";
                strOrder += "     MEI.BAT_ID, ";
                strOrder += "     MEI.DETAILS_NO ";
            }

            // SQL本体
            string strSQL = "";
            //（大量データテスト時の改善）
            //2022.12.01 SP.Harauchi 持帰、自行情報エントリユーザでベリファイ出来てしまう
            //ベリファイを自動配信で行い、途中補正入力のための画面が表示されたタイミングでF1：終了を押す
            //HOSEI_STATUSのSTATUSをベリファイ中からベリファイ待ちに戻すUPDATE文でE_OPENOをNULLにするため
            //エントリユーザでベリファイできてしまう
            //NULL AS E_OPENO→STS.E_OPENOに変更
            //strSQL += string.Format(strSELECT, "STS.GYM_ID, STS.OPERATION_DATE, STS.SCAN_TERM, STS.BAT_ID, STS.DETAILS_NO, STS.HOSEI_INPTMODE, STS.INPT_STS, NULL AS TMNO, NULL AS OPENO, NULL AS E_OPENO");
            strSQL += string.Format(strSELECT, "STS.GYM_ID, STS.OPERATION_DATE, STS.SCAN_TERM, STS.BAT_ID, STS.DETAILS_NO, STS.HOSEI_INPTMODE, STS.INPT_STS, NULL AS TMNO, NULL AS OPENO, STS.E_OPENO");
            strSQL += " ORDER BY ";
            strSQL += strOrder;
			strSQL += " ) "; // 2022/08/03 ADD
			//（大量データテスト時の改善）
			strSQL += " WHERE ROWNUM <= " + ignoreCnt.ToString(); // 2022/08/03 ADD
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：エントリー入力
        /// 取得内容：項目トランザクション
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="opedate"></param>
        /// <param name="scanterm"></param>
        /// <param name="batid"></param>
        /// <param name="detno"></param>
        /// <param name="dspid"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetEntryTrItemsSelect(int gymid, int opedate, string scanterm, int batid, int detno, int dspid, int hoseiItemMode, int SchemaBankCD)
        {
            // SQL本体
            string strSQL = "";
            strSQL += " SELECT ";
            strSQL += "     IT.* ";
            strSQL += " FROM " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSQL += "     INNER JOIN " + TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(SchemaBankCD) + " HDI ";
            strSQL += "         ON HDI.GYM_ID = MEI.GYM_ID ";
            strSQL += "         AND HDI.DSP_ID = MEI.DSP_ID ";
            strSQL += "         AND HDI.HOSEI_ITEMMODE = " + hoseiItemMode + " ";
            strSQL += "     INNER JOIN " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " IT ";
            strSQL += "         ON IT.GYM_ID = MEI.GYM_ID ";
            strSQL += "         AND IT.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "         AND IT.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "         AND IT.BAT_ID = MEI.BAT_ID ";
            strSQL += "         AND IT.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "         AND IT.ITEM_ID = HDI.ITEM_ID ";
            strSQL += " WHERE ";
            strSQL += "         MEI.GYM_ID = " + gymid + " ";
            strSQL += "     AND MEI.OPERATION_DATE = " + opedate + " ";
            strSQL += "     AND MEI.SCAN_TERM = '" + scanterm + "' ";
            strSQL += "     AND MEI.BAT_ID = " + batid + " ";
            strSQL += "     AND MEI.DETAILS_NO = " + detno + " ";
            strSQL += "     AND MEI.DSP_ID = " + dspid + " ";
            strSQL += "     AND MEI.DELETE_FLG = 0 ";
            strSQL += " ORDER BY ";
            strSQL += "     IT.ITEM_ID ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：エントリー入力
        /// 取得内容：集計枚数、集計金額
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="opedate"></param>
        /// <param name="scanterm"></param>
        /// <param name="batid"></param>
        /// <param name="itemCol"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetEntryProofKingaku(int gymid, int opedate, string scanterm, int batid, string itemCol, int SchemaBankCD)
        {
            // SQL本体
            string strSQL = "";
            strSQL += " SELECT ";
            strSQL += "       COUNT(1) AS TOTAL_COUNT ";
            strSQL += "     , SUM(IT." + itemCol + ") AS TOTAL_AMOUNT ";
            strSQL += " FROM ";
            strSQL += "     " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " DT ";
            strSQL += "     INNER JOIN " + TBL_TRITEM.TABLE_NAME(SchemaBankCD) + " IT ";
            strSQL += "         ON  DT.GYM_ID = IT.GYM_ID ";
            strSQL += "         AND DT.OPERATION_DATE = IT.OPERATION_DATE ";
            strSQL += "         AND DT.SCAN_TERM = IT.SCAN_TERM ";
            strSQL += "         AND DT.BAT_ID = IT.BAT_ID ";
            strSQL += "         AND DT.DETAILS_NO = IT.DETAILS_NO ";
            strSQL += " WHERE ";
            strSQL += "         DT.GYM_ID = " + gymid + "  ";
            strSQL += "     AND DT.OPERATION_DATE = " + opedate + " ";
            strSQL += "     AND DT.SCAN_TERM = '" + scanterm + "' ";
            strSQL += "     AND DT.BAT_ID = " + batid + " ";
            strSQL += "     AND DT.DELETE_FLG = 0 ";
            strSQL += "     AND IT.ITEM_ID = " + DspItem.ItemId.金額 + " ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：エントリー入力
        /// 取得内容：補正ステータス
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="opedate"></param>
        /// <param name="scanterm"></param>
        /// <param name="batid"></param>
        /// <param name="detailsno"></param>
        /// <param name="hoseiInputMode"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetEntryHoseiStatusSelect(int gymid, int opedate, string scanterm, int batid, int detailsno, int hoseiInputMode, int SchemaBankCD)
        {
            // FROM句
            string strFROM = "";
            strFROM += " SELECT ";
            strFROM += "     * ";
            strFROM += " FROM ";
            strFROM += "     " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " ";
            strFROM += " WHERE ";
            strFROM += "         GYM_ID = " + gymid + " ";
            strFROM += "     AND OPERATION_DATE = " + opedate + " ";
            strFROM += "     AND SCAN_TERM = '" + scanterm + "' ";
            strFROM += "     AND BAT_ID = " + batid + " ";
            strFROM += "     AND DETAILS_NO = " + detailsno + " ";

            // WHERE句
            string strWHERE = "";
            strWHERE += "     AND HOSEI_INPTMODE <> " + hoseiInputMode + " ";
            strWHERE += "     AND INPT_STS IN ( ";
            strWHERE += "              " + HoseiStatus.InputStatus.エントリ中 + " ";
            strWHERE += "             ," + HoseiStatus.InputStatus.ベリファイ中 + " ";
            strWHERE += "             ," + HoseiStatus.InputStatus.完了訂正中 + " ";
            strWHERE += "         ) ";

            // SQL本体
            string strSQL = "";
            strSQL += strFROM;
            strSQL += strWHERE;

            return strSQL;
        }

        /// <summary>
        /// 呼出画面：エントリー入力
        /// 更新内容：持出アップロード状態
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="opedate"></param>
        /// <param name="scanterm"></param>
        /// <param name="batid"></param>
        /// <param name="detailsno"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetEntryBuaStsUpdate(int gymid, int opedate, string scanterm, int batid, int detailsno, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRMEIIMG.TABLE_NAME(SchemaBankCD) + " ";
            strSQL += " SET ";
            strSQL += "         BUA_STS = " + TrMei.Sts.再作成対象 + " ";
            strSQL += " WHERE ";
            strSQL += "         GYM_ID = " + gymid + " ";
            strSQL += "     AND OPERATION_DATE = " + opedate + " ";
            strSQL += "     AND SCAN_TERM = '" + scanterm + "' ";
            strSQL += "     AND BAT_ID = " + batid + " ";
            strSQL += "     AND DETAILS_NO = " + detailsno + " ";
            strSQL += "     AND BUA_STS <> " + TrMei.Sts.未作成 + " ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：エントリー入力
        /// 更新内容：明細トランザクション（画面ID）
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="opedate"></param>
        /// <param name="scanterm"></param>
        /// <param name="batid"></param>
        /// <param name="detailsno"></param>
        /// <param name="dspid"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns>持帰ダウンロード確定処理でも一部使用</returns>
        public static string GetEntryDspIdUpdate(int gymid, int opedate, string scanterm, int batid, int detailsno, int dspid, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " UPDATE " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " ";
            strSQL += "     SET DSP_ID = " + dspid + " ";
            strSQL += " WHERE ";
            strSQL += "         GYM_ID = " + gymid + " ";
            strSQL += "     AND OPERATION_DATE = " + opedate + " ";
            strSQL += "     AND SCAN_TERM = '" + scanterm + "' ";
            strSQL += "     AND BAT_ID = " + batid + " ";
            strSQL += "     AND DETAILS_NO = " + detailsno + " ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：エントリー入力
        /// 取得内容：補正未完了 ITEM_ID リスト
        /// </summary>
        /// <returns></returns>
        public static string GetReadOnlyItemId(int gymid, int opedate, string scanterm, int batid, int detailsno, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " SELECT DISTINCT ";
            strSQL += "     IM.* ";
            strSQL += " FROM ";
            strSQL += "     " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS ";
            strSQL += "     INNER JOIN " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSQL += "         ON  STS.GYM_ID = MEI.GYM_ID ";
            strSQL += "         AND STS.OPERATION_DATE = MEI.OPERATION_DATE ";
            strSQL += "         AND STS.SCAN_TERM = MEI.SCAN_TERM ";
            strSQL += "         AND STS.BAT_ID = MEI.BAT_ID ";
            strSQL += "         AND STS.DETAILS_NO = MEI.DETAILS_NO ";
            strSQL += "     INNER JOIN " + TBL_HOSEIMODE_DSP_ITEM.TABLE_NAME(SchemaBankCD) + " HDI ";
            strSQL += "         ON  MEI.GYM_ID = HDI.GYM_ID ";
            strSQL += "         AND MEI.DSP_ID = HDI.DSP_ID ";
            strSQL += "         AND STS.HOSEI_INPTMODE = HDI.HOSEI_ITEMMODE ";
            strSQL += "     INNER JOIN " + TBL_DSP_ITEM.TABLE_NAME(SchemaBankCD) + " DI ";
            strSQL += "         ON  HDI.GYM_ID = DI.GYM_ID ";
            strSQL += "         AND HDI.DSP_ID = DI.DSP_ID ";
            strSQL += "         AND HDI.ITEM_ID = DI.ITEM_ID ";
            strSQL += "     INNER JOIN " + TBL_ITEM_MASTER.TABLE_NAME(SchemaBankCD) + " IM ";
            strSQL += "         ON  HDI.ITEM_ID = IM.ITEM_ID ";
            strSQL += " WHERE ";
            strSQL += "         STS.GYM_ID = " + gymid + " ";
            strSQL += "     AND STS.OPERATION_DATE = " + opedate + " ";
            strSQL += "     AND STS.SCAN_TERM = '" + scanterm + "' ";
            strSQL += "     AND STS.BAT_ID = " + batid + " ";
            strSQL += "     AND STS.DETAILS_NO = " + detailsno + " ";
            strSQL += "     AND STS.HOSEI_INPTMODE <> " + HoseiStatus.HoseiInputMode.自行情報 + " ";
            strSQL += "     AND STS.INPT_STS < " + HoseiStatus.InputStatus.完了 + " ";
            strSQL += "     AND DI.ITEM_TYPE NOT IN ('D') ";
            strSQL += " ORDER BY ";
            strSQL += "     IM.ITEM_ID ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：明細イメージ一覧
        /// 取得内容：明細イメージ一覧
        /// </summary>
        /// <param name="term"></param>
        /// <param name="gymid"></param>
        /// <param name="rowFrom"></param>
        /// <param name="rowTo"></param>
        /// <returns></returns>
        public static string GetMeisaiPageList(string term, int gymid, int rowFrom, int rowTo)
        {
            // ROWNUMとORDER BYは同じサブクエリ内に定義してもORDER BYで指定した順番に番号を振らないケースがあるため、ROW_NUMBERを使用して番号を算出
            string strSQL = "";
            strSQL += " SELECT * FROM ";
            strSQL += " ( ";
            strSQL += "     SELECT ";
            strSQL += "            ROW_NUMBER() OVER(ORDER BY MEI.GYM_ID, MEI.OPERATION_DATE, MEI.BAT_ID, MEI.DETAILS_NO) AS ROW_ID ";
            strSQL += "          , MEI.* ";
            strSQL += "     FROM " + TBL_WK_IMGELIST.TABLE_NAME + " MEI ";
            strSQL += "     WHERE ";
            strSQL += "             SEARCH_TERMID = '" + term + "' ";
            strSQL += "         AND GYM_ID = " + gymid + " ";
            strSQL += " ) ";
            strSQL += " WHERE ";
            strSQL += "     ROW_ID BETWEEN " + rowFrom + " AND " + rowTo + " ";
            strSQL += " ORDER BY ";
            strSQL += "     ROW_ID ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：業務メンテナンス
        /// 処理内容：一時テーブル作成クエリ
        /// </summary>
        /// <returns></returns>
        public static string GetCreateDSP_PARAM(string tableName)
        {
            string strSQL = "";
            strSQL += " CREATE GLOBAL TEMPORARY TABLE " + tableName + " ( ";
            strSQL += "     GYM_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     DSP_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     DSP_NAME VARCHAR2(60), ";
            strSQL += "     FONT_SIZE NUMBER(2,0) default 0  NOT NULL, ";
            strSQL += "     DSP_WIDTH NUMBER(5,0) default 0  NOT NULL, ";
            strSQL += "     DSP_HEIGHT NUMBER(5,0) default 0  NOT NULL, ";
            strSQL += "     OCR_NAME VARCHAR2(40), ";
            strSQL += " PRIMARY KEY ( ";
            strSQL += "      GYM_ID ";
            strSQL += "     ,DSP_ID ";
            strSQL += " )) ";
            strSQL += " ON COMMIT DELETE ROWS ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：業務メンテナンス
        /// 処理内容：一時テーブル作成クエリ
        /// </summary>
        /// <returns></returns>
        public static string GetCreateIMG_PARAM(string tableName)
        {
            string strSQL = "";
            strSQL += " CREATE GLOBAL TEMPORARY TABLE " + tableName + " ( ";
            strSQL += "     GYM_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     DSP_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     IMG_FILE VARCHAR2(30), ";
            strSQL += "     REDUCE_RATE NUMBER(4,1) default 0  NOT NULL, ";
            strSQL += "     IMG_TOP NUMBER(10,1) default -1  NOT NULL, ";
            strSQL += "     IMG_LEFT NUMBER(10,1) default -1  NOT NULL, ";
            strSQL += "     IMG_WIDTH NUMBER(10,1) default -1  NOT NULL, ";
            strSQL += "     IMG_HEIGHT NUMBER(10,1) default -1  NOT NULL, ";
            strSQL += "     IMG_BASE_POINT NUMBER(10,1) default -1  NOT NULL, ";
            strSQL += "     XSCROLL_LEFT NUMBER(10,1) default 0  NOT NULL, ";
            strSQL += "     XSCROLL_VALUE NUMBER(4,0) default 0  NOT NULL, ";
            strSQL += "     XSCROLL_RIGHT NUMBER(10,1) default 0  NOT NULL, ";
            strSQL += " PRIMARY KEY ( ";
            strSQL += "      GYM_ID ";
            strSQL += "     ,DSP_ID ";
            strSQL += " )) ";
            strSQL += " ON COMMIT DELETE ROWS ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：業務メンテナンス
        /// 処理内容：一時テーブル作成クエリ
        /// </summary>
        /// <returns></returns>
        public static string GetCreateDSP_ITEM(string tableName)
        {
            string strSQL = "";
            strSQL += " CREATE GLOBAL TEMPORARY TABLE " + tableName + " ( ";
            strSQL += "     GYM_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     DSP_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     ITEM_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     ITEM_DISPNAME VARCHAR2(40), ";
            strSQL += "     ITEM_TYPE VARCHAR2(1), ";
            strSQL += "     ITEM_LEN NUMBER(2,0) default 0  NOT NULL, ";
            strSQL += "     POS NUMBER(4,0) default 0  NOT NULL, ";
            strSQL += "     DUP VARCHAR2(1), ";
            strSQL += "     AUTO_INPUT VARCHAR2(1), ";
            strSQL += "     ITEM_SUBRTN VARCHAR2(150), ";
            strSQL += "     BLANK_FLG VARCHAR2(1), ";
            strSQL += " PRIMARY KEY ( ";
            strSQL += "      GYM_ID ";
            strSQL += "     ,DSP_ID ";
            strSQL += "     ,ITEM_ID ";
            strSQL += " )) ";
            strSQL += " ON COMMIT DELETE ROWS ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：業務メンテナンス
        /// 処理内容：一時テーブル作成クエリ
        /// </summary>
        /// <returns></returns>
        public static string GetCreateIMG_CURSOR_PARAM(string tableName)
        {
            string strSQL = "";
            strSQL += " CREATE GLOBAL TEMPORARY TABLE " + tableName + " ( ";
            strSQL += "     GYM_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     DSP_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     ITEM_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     ITEM_TOP NUMBER(10,1) default 0  NOT NULL, ";
            strSQL += "     ITEM_LEFT NUMBER(10,1) default 0  NOT NULL, ";
            strSQL += "     ITEM_WIDTH NUMBER(10,1) default 0  NOT NULL, ";
            strSQL += "     ITEM_HEIGHT NUMBER(10,1) default 0  NOT NULL, ";
            strSQL += "     LINE_WEIGHT NUMBER(10,1) default 0  NOT NULL, ";
            strSQL += "     LINE_COLOR NUMBER(10,1) default 0  NOT NULL, ";
            strSQL += " PRIMARY KEY ( ";
            strSQL += "      GYM_ID ";
            strSQL += "     ,DSP_ID ";
            strSQL += "     ,ITEM_ID ";
            strSQL += " )) ";
            strSQL += " ON COMMIT DELETE ROWS ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：業務メンテナンス
        /// 処理内容：一時テーブル作成クエリ
        /// </summary>
        /// <returns></returns>
        public static string GetCreateHOSEIMODE_PARAM(string tableName)
        {
            string strSQL = "";
            strSQL += " CREATE GLOBAL TEMPORARY TABLE " + tableName + " ( ";
            strSQL += "     GYM_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     DSP_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     HOSEI_ITEMMODE NUMBER(1,0) NOT NULL, ";
            strSQL += "     AUTO_SKIP_MODE_ENT NUMBER(1,0) default -1  NOT NULL, ";
            strSQL += "     AUTO_SKIP_MODE_VFY NUMBER(1,0) default -1  NOT NULL, ";
            strSQL += "     VERY_MODE NUMBER(1,0) default -1  NOT NULL, ";
            strSQL += " PRIMARY KEY ( ";
            strSQL += "      GYM_ID ";
            strSQL += "     ,DSP_ID ";
            strSQL += "     ,HOSEI_ITEMMODE ";
            strSQL += " )) ";
            strSQL += " ON COMMIT DELETE ROWS ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：業務メンテナンス
        /// 処理内容：一時テーブル作成クエリ
        /// </summary>
        /// <returns></returns>
        public static string GetCreateHOSEIMODE_DSP_ITEM(string tableName)
        {
            string strSQL = "";
            strSQL += " CREATE GLOBAL TEMPORARY TABLE " + tableName + " ( ";
            strSQL += "     GYM_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     DSP_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     HOSEI_ITEMMODE NUMBER(1,0) NOT NULL, ";
            strSQL += "     ITEM_ID NUMBER(3,0) NOT NULL, ";
            strSQL += "     NAME_POS_TOP NUMBER(5,0) default -1  NOT NULL, ";
            strSQL += "     NAME_POS_LEFT NUMBER(5,0) default -1  NOT NULL, ";
            strSQL += "     INPUT_POS_TOP NUMBER(5,0) default -1  NOT NULL, ";
            strSQL += "     INPUT_POS_LEFT NUMBER(5,0) default -1  NOT NULL, ";
            strSQL += "     INPUT_WIDTH NUMBER(5,0) default -1  NOT NULL, ";
            strSQL += "     INPUT_HEIGHT NUMBER(5,0) default -1  NOT NULL, ";
            strSQL += "     INPUT_SEQ NUMBER(2,0) default -1  NOT NULL, ";
            strSQL += " PRIMARY KEY ( ";
            strSQL += "      GYM_ID ";
            strSQL += "     ,DSP_ID ";
            strSQL += "     ,HOSEI_ITEMMODE ";
            strSQL += "     ,ITEM_ID ";
            strSQL += " )) ";
            strSQL += " ON COMMIT DELETE ROWS ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：業務メンテナンス
        /// 処理内容：一時テーブルにコピーする
        /// </summary>
        /// <returns></returns>
        public static string GetInsertDstToTmp(string srcTableName, string dstTableName, string allColumns, int gymid)
        {
            string strSQL = "";
            strSQL += " INSERT INTO " + dstTableName + " ";
            strSQL += "     ( " + allColumns + " ) ";
            strSQL += "     SELECT ";
            strSQL += "          SRC.* ";
            strSQL += "     FROM ";
            strSQL += "         " + srcTableName + " SRC ";
            strSQL += "     WHERE ";
            strSQL += "         SRC.GYM_ID = " + gymid + " ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：業務メンテナンス
        /// 処理内容：一時テーブルのレコードを指定テーブルにINSERTする
        /// 　GYM_ID
        /// 　DSP_ID
        /// </summary>
        /// <returns></returns>
        public static string GetInsertTmpToDst1(string srcTableName, string dstTableName, string allColumns)
        {
            string strSQL = "";
            strSQL += " INSERT INTO " + dstTableName + " ";
            strSQL += "     ( " + allColumns + " ) ";
            strSQL += "     SELECT ";
            strSQL += "          TMP.* ";
            strSQL += "     FROM ";
            strSQL += "         " + srcTableName + " TMP ";
            strSQL += "         LEFT JOIN " + dstTableName + " DST ";
            strSQL += "             ON  TMP.GYM_ID = DST.GYM_ID ";
            strSQL += "             AND TMP.DSP_ID = DST.DSP_ID ";
            strSQL += "     WHERE ";
            strSQL += "         DST.GYM_ID IS NULL ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：業務メンテナンス
        /// 処理内容：一時テーブルのレコードを指定テーブルにINSERTする
        /// 　GYM_ID
        /// 　DSP_ID
        /// 　ITEM_ID
        /// </summary>
        /// <returns></returns>
        public static string GetInsertTmpToDst2(string srcTableName, string dstTableName, string allColumns)
        {
            string strSQL = "";
            strSQL += " INSERT INTO " + dstTableName + " ";
            strSQL += "     ( " + allColumns + " ) ";
            strSQL += "     SELECT ";
            strSQL += "          TMP.* ";
            strSQL += "     FROM ";
            strSQL += "         " + srcTableName + " TMP ";
            strSQL += "         LEFT JOIN " + dstTableName + " DST ";
            strSQL += "             ON  TMP.GYM_ID = DST.GYM_ID ";
            strSQL += "             AND TMP.DSP_ID = DST.DSP_ID ";
            strSQL += "             AND TMP.ITEM_ID = DST.ITEM_ID ";
            strSQL += "     WHERE ";
            strSQL += "         DST.GYM_ID IS NULL ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：業務メンテナンス
        /// 処理内容：一時テーブルのレコードを指定テーブルにINSERTする
        /// 　GYM_ID
        /// 　DSP_ID
        /// 　HOSEI_ITEMMODE
        /// 　ITEM_ID
        /// </summary>
        /// <returns></returns>
        public static string GetInsertTmpToDst4(string srcTableName, string dstTableName, string allColumns)
        {
            string strSQL = "";
            strSQL += " INSERT INTO " + dstTableName + " ";
            strSQL += "     ( " + allColumns + " ) ";
            strSQL += "     SELECT ";
            strSQL += "          TMP.* ";
            strSQL += "     FROM ";
            strSQL += "         " + srcTableName + " TMP ";
            strSQL += "         LEFT JOIN " + dstTableName + " DST ";
            strSQL += "             ON  TMP.GYM_ID = DST.GYM_ID ";
            strSQL += "             AND TMP.DSP_ID = DST.DSP_ID ";
            strSQL += "             AND TMP.HOSEI_ITEMMODE = DST.HOSEI_ITEMMODE ";
            strSQL += "             AND TMP.ITEM_ID = DST.ITEM_ID ";
            strSQL += "     WHERE ";
            strSQL += "         DST.GYM_ID IS NULL ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：業務メンテナンス
        /// 処理内容：一時テーブルのレコードを指定テーブルにINSERTする
        /// 　GYM_ID
        /// 　DSP_ID
        /// 　HOSEI_ITEMMODE
        /// </summary>
        /// <returns></returns>
        public static string GetInsertTmpToDst5(string srcTableName, string dstTableName, string allColumns)
        {
            string strSQL = "";
            strSQL += " INSERT INTO " + dstTableName + " ";
            strSQL += "     ( " + allColumns + " ) ";
            strSQL += "     SELECT ";
            strSQL += "          TMP.* ";
            strSQL += "     FROM ";
            strSQL += "         " + srcTableName + " TMP ";
            strSQL += "         LEFT JOIN " + dstTableName + " DST ";
            strSQL += "             ON  TMP.GYM_ID = DST.GYM_ID ";
            strSQL += "             AND TMP.DSP_ID = DST.DSP_ID ";
            strSQL += "             AND TMP.HOSEI_ITEMMODE = DST.HOSEI_ITEMMODE ";
            strSQL += "     WHERE ";
            strSQL += "         DST.GYM_ID IS NULL ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：業務メンテナンス
        /// 処理内容：一時テーブルに存在しないレコードを削除する
        /// 　GYM_ID
        /// 　DSP_ID
        /// 　ITEM_ID
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteDspQuery2(string srcTableName, string dstTableName, int gymid, int dspid, int itemid)
        {
            string strSQL = "";
            strSQL += " DELETE ";
            strSQL += " FROM ";
            strSQL += "     " + dstTableName + " DST ";
            strSQL += " WHERE ";
            strSQL += "     NOT EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + srcTableName + " TMP ";
            strSQL += "         WHERE ";
            strSQL += "                 DST.GYM_ID = TMP.GYM_ID  ";
            strSQL += "             AND DST.DSP_ID = TMP.DSP_ID ";
            strSQL += "             AND DST.ITEM_ID = TMP.ITEM_ID ";
            strSQL += "     ) ";
            strSQL += "     AND GYM_ID = " + gymid + " ";
            strSQL += "     AND DSP_ID = " + dspid + " ";
            strSQL += "     AND ITEM_ID = " + itemid + " ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：業務メンテナンス
        /// 処理内容：一時テーブルに存在しないレコードを削除する
        /// 　GYM_ID
        /// 　DSP_ID
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteDspQuery3(string srcTableName, string dstTableName, int gymid, int dspid)
        {
            string strSQL = "";
            strSQL += " DELETE ";
            strSQL += " FROM ";
            strSQL += "     " + dstTableName + " DST ";
            strSQL += " WHERE ";
            strSQL += "     NOT EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + srcTableName + " TMP ";
            strSQL += "         WHERE ";
            strSQL += "                 DST.GYM_ID = TMP.GYM_ID  ";
            strSQL += "             AND DST.DSP_ID = TMP.DSP_ID ";
            strSQL += "             AND DST.ITEM_ID = TMP.ITEM_ID ";
            strSQL += "     ) ";
            strSQL += "     AND GYM_ID = " + gymid + " ";
            strSQL += "     AND DSP_ID = " + dspid + " ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：業務メンテナンス
        /// 処理内容：一時テーブルに存在しないレコードを削除する
        /// 　GYM_ID
        /// 　DSP_ID
        /// 　HOSEI_ITEMMODE
        /// </summary>
        /// <returns></returns>
        public static string GetDeleteDspQuery4(string srcTableName, string dstTableName, int gymid, int dspid, int hoseiitemmode)
        {
            string strSQL = "";
            strSQL += " DELETE ";
            strSQL += " FROM ";
            strSQL += "     " + dstTableName + " DST ";
            strSQL += " WHERE ";
            strSQL += "     NOT EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + srcTableName + " TMP ";
            strSQL += "         WHERE ";
            strSQL += "                 DST.GYM_ID = TMP.GYM_ID  ";
            strSQL += "             AND DST.DSP_ID = TMP.DSP_ID ";
            strSQL += "             AND DST.HOSEI_ITEMMODE = TMP.HOSEI_ITEMMODE ";
            strSQL += "             AND DST.ITEM_ID = TMP.ITEM_ID ";
            strSQL += "     ) ";
            strSQL += "     AND GYM_ID = " + gymid + " ";
            strSQL += "     AND DSP_ID = " + dspid + " ";
            strSQL += "     AND HOSEI_ITEMMODE = " + hoseiitemmode + " ";
            return strSQL;
        }

        /// <summary>
        /// 呼出画面：補正状態強制完了更新
        /// 取得内容：処理対象の補正ステータス
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="opedate"></param>
        /// <param name="scanterm"></param>
        /// <param name="batid"></param>
        /// <param name="detailsno"></param>
        /// <param name="hoseiInputMode"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetOcHoseiForceSelect(int gymid, List<int> inptmode, List<int> inptsts, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " SELECT ";
            strSQL += "     STS.* ";
            strSQL += " FROM ";
            strSQL += "     " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS ";
            strSQL += " WHERE ";
            strSQL += "         STS.GYM_ID = " + gymid + " ";
            strSQL += "     AND STS.HOSEI_INPTMODE IN ( " + string.Join(", ", inptmode) + " ) ";
            strSQL += "     AND STS.INPT_STS IN ( " + string.Join(", ", inptsts) + " ) ";
            strSQL += "     AND EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSQL += "         WHERE ";
            strSQL += "                 MEI.GYM_ID = STS.GYM_ID  ";
            strSQL += "             AND MEI.OPERATION_DATE = STS.OPERATION_DATE ";
            strSQL += "             AND MEI.SCAN_TERM = STS.SCAN_TERM ";
            strSQL += "             AND MEI.BAT_ID = STS.BAT_ID ";
            strSQL += "             AND MEI.DETAILS_NO = STS.DETAILS_NO ";
            strSQL += "             AND MEI.DELETE_FLG = 0 ";
            strSQL += "     ) ";

            return strSQL;
        }

        /// <summary>
        /// 呼出画面：補正状態強制完了更新
        /// 更新内容：処理対象の補正ステータス
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="opedate"></param>
        /// <param name="scanterm"></param>
        /// <param name="batid"></param>
        /// <param name="detailsno"></param>
        /// <param name="hoseiInputMode"></param>
        /// <param name="SchemaBankCD"></param>
        /// <returns></returns>
        public static string GetOcHoseiForceUpdate(int gymid, int updatests,　List<int> inptmode, List<int> inptsts, int SchemaBankCD)
        {
            string strSQL = "";
            strSQL += " UPDATE " + TBL_HOSEI_STATUS.TABLE_NAME(SchemaBankCD) + " STS ";
            strSQL += " SET ";
            strSQL += "         STS.INPT_STS = " + updatests + " ";
            strSQL += " WHERE ";
            strSQL += "         STS.GYM_ID = " + gymid + " ";
            strSQL += "     AND STS.HOSEI_INPTMODE IN ( " + string.Join(", ", inptmode) + " ) ";
            strSQL += "     AND STS.INPT_STS IN ( " + string.Join(", ", inptsts) + " ) ";
            strSQL += "     AND EXISTS ( ";
            strSQL += "         SELECT 1 FROM " + TBL_TRMEI.TABLE_NAME(SchemaBankCD) + " MEI ";
            strSQL += "         WHERE ";
            strSQL += "                 MEI.GYM_ID = STS.GYM_ID  ";
            strSQL += "             AND MEI.OPERATION_DATE = STS.OPERATION_DATE ";
            strSQL += "             AND MEI.SCAN_TERM = STS.SCAN_TERM ";
            strSQL += "             AND MEI.BAT_ID = STS.BAT_ID ";
            strSQL += "             AND MEI.DETAILS_NO = STS.DETAILS_NO ";
            strSQL += "             AND MEI.DELETE_FLG = 0 ";
            strSQL += "     ) ";

            return strSQL;
        }


    }
}
