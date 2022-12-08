using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Reflection;
using Common;
using CommonTable.DB;
using CommonClass;
using CommonClass.DB;
using EntryCommon;

namespace CommonClass
{
    /// <summary>
    /// イメージ取込トランザクション操作共通
    /// </summary>
    public class ImportTRAccessCommon
    {
        #region 定義

        /// <summary>金額桁数</summary>
        public const string AMT_LEN = Const.AMOUNT_NO_LEN_STR;

        #endregion

        #region DBアクセス

        #region DB登録

        /// <summary>
        /// バッチデータの登録処理
        /// </summary>
        /// <returns></returns>
        public static bool InsertTRBatchData(TBL_TRBATCH InsBatchData,
                                             AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // バッチデータ登録処理
                string strSQL = InsBatchData.GetInsertQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// バッチデータの更新処理
        /// </summary>
        /// <returns></returns>
        public static bool UpdateTRBatchData(TBL_TRBATCH InsBatchData,
                                             AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // バッチデータ登録処理
                //string strSQL = InsBatchData.GetUpdateQueryNumber();
                string strSQL = InsBatchData.GetUpdateQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// バッチデータの登録処理
        /// </summary>
        /// <returns></returns>
        public static bool InsertTRBatchImageData(TBL_TRBATCHIMG InsIMGData,
                                                  AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // イメージ登録処理
                string strSQL = InsIMGData.GetInsertQuery();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 明細データの登録処理
        /// </summary>
        /// <returns></returns>
        public static bool InsertTRMeiData(int Type, TBL_TRMEI InsMeiData,
                                           AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // 明細トランザクション登録処理
                string strSQL = string.Empty;
                switch (Type)
                {
                    case 1:
                        strSQL = InsMeiData.GetInsertQueryImageImport();
                        break;
                    case 2:
                        strSQL = InsMeiData.GetInsertQueryFileImport();
                        break;
                    default:
                        break;
                }
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 明細データの更新処理
        /// </summary>
        /// <returns></returns>
        public static bool UpdateTRMeiData(TBL_TRMEI UpdMeiData, int Schemabankcd, 
                                           AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // 明細トランザクション更新処理
                string strSQL = SQLTextImport.GetUpdateTrMeiDspId(UpdMeiData._GYM_ID, UpdMeiData._OPERATION_DATE, UpdMeiData._SCAN_TERM, 
                                                                  UpdMeiData._BAT_ID, UpdMeiData._DETAILS_NO, UpdMeiData.m_DSP_ID, Schemabankcd);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 明細イメージデータの登録処理
        /// </summary>
        /// <returns></returns>
        public static bool InsertTRMeiImageData(int Type, TBL_TRMEIIMG InsMeiIMGData,
                                                AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // 明細トランザクション登録処理
                string strSQL = string.Empty;
                switch (Type)
                {
                    case 1:
                        strSQL = InsMeiIMGData.GetInsertQueryImageImport();
                        break;
                    case 2:
                        strSQL = InsMeiIMGData.GetInsertQueryFileImport();
                        break;
                    default:
                        break;
                }
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 補正ステータスの登録処理
        /// </summary>
        /// <returns></returns>
        public static bool InsertTRHoseiData(TBL_HOSEI_STATUS InsHoseiData,
                                             AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // 補正ステータス登録処理
                string strSQL = InsHoseiData.GetInsertQueryImageImport();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 項目トランザクションの登録処理
        /// </summary>
        /// <returns></returns>
        public static bool InsertTRItemData(int Type ,TBL_TRITEM InsTRItemData,
                                            AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // 項目トランザクション登録処理
                string strSQL = string.Empty;
                switch (Type)
                {
                    case 1:
                        strSQL = InsTRItemData.GetInsertQueryImageImport1();
                        break;
                    case 2:
                        strSQL = InsTRItemData.GetInsertQueryImageImport2();
                        break;
                    case 3:
                        strSQL = InsTRItemData.GetInsertQueryImageImport3();
                        break;
                    case 4:
                        strSQL = InsTRItemData.GetInsertQueryImageImport4();
                        break;
                    case 5:
                        strSQL = InsTRItemData.GetInsertQueryImageImport5();
                        break;
                    default:
                        break;
                }
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 項目トランザクションの登録処理
        /// </summary>
        /// <returns></returns>
        public static bool InsertTRItemData(int Type, TBL_TRITEM InsTRItemData, int dspID, List<TBL_DSP_ITEM> dspitemMF,
                                            AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // DSP_ITEMになければ登録外
            if (dspitemMF.Where(x => x._GYM_ID == InsTRItemData._GYM_ID && x._DSP_ID == dspID && x._ITEM_ID == InsTRItemData._ITEM_ID).Count() <= 0)
            {
                return true;
            }

            return InsertTRItemData(Type, InsTRItemData, dbp, Tran);
        }

        /// <summary>
        /// OCRデータの登録処理(期日管理)
        /// </summary>
        /// <returns></returns>
        public static bool InsertOCRData(TBL_OCR_DATA InsOCRData,
                                         AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                // OCRデータ登録処理
                string strSQL = InsOCRData.GetInsertQueryKijitu();
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        #endregion

        #region DB削除

        /// <summary>
        /// 明細データの削除処理
        /// </summary>
        /// <returns></returns>
        public static bool DeleteTRMeiData(int GymID, int OpeDate, string ScanTerm, int BatID, int Details, int Schemabankcd,
                                           AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                string strSQL = string.Empty;

                // 明細トランザクション削除処理
                strSQL = TBL_TRMEI.GetDeleteQuery(GymID, OpeDate, ScanTerm, BatID, Details, Schemabankcd);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);

                // 明細トランザクション履歴削除処理
                strSQL = TBL_TRMEI_HIST.GetDeleteQueryDetails(GymID, OpeDate, ScanTerm, BatID, Details, Schemabankcd);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 明細イメージデータの削除処理
        /// </summary>
        /// <returns></returns>
        public static bool DeleteTRMeiImageData(int GymID, int OpeDate, string ScanTerm, int BatID, int Details, int Schemabankcd,
                                                AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                string strSQL = string.Empty;

                // 明細イメージトランザクション削除処理
                strSQL = TBL_TRMEIIMG.GetDeleteQueryDetails(GymID, OpeDate, ScanTerm, BatID, Details, Schemabankcd);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);

                // 明細イメージトランザクション履歴削除処理
                strSQL = TBL_TRMEIIMG_HIST.GetDeleteQueryDetails(GymID, OpeDate, ScanTerm, BatID, Details, Schemabankcd);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 補正ステータスの削除処理
        /// </summary>
        /// <returns></returns>
        public static bool DeleteTRHoseiData(int GymID, int OpeDate, string ScanTerm, int BatID, int Details, int Schemabankcd,
                                             AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                string strSQL = string.Empty;

                // 補正ステータス削除処理
                strSQL = TBL_HOSEI_STATUS.GetDeleteQueryDetails(GymID, OpeDate, ScanTerm, BatID, Details, Schemabankcd);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 項目トランザクションの削除処理
        /// </summary>
        /// <returns></returns>
        public static bool DeleteTRItemData(int GymID, int OpeDate, string ScanTerm, int BatID, int Details, int Schemabankcd,
                                            AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                string strSQL = string.Empty;

                // 項目トランザクション削除処理
                strSQL = TBL_TRITEM.GetDeleteQueryDetails(GymID, OpeDate, ScanTerm, BatID, Details, Schemabankcd);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);

                // 項目トランザクション履歴削除処理
                strSQL = TBL_TRITEM_HIST.GetDeleteQueryDetails(GymID, OpeDate, ScanTerm, BatID, Details, Schemabankcd);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        /// <summary>
        /// OCRデータの削除処理(期日管理)
        /// </summary>
        /// <returns></returns>
        public static bool DeleteOCRData(int GymID, int InputRoute, int OpeDate, string ImgDirName, string ImgFileName,
                                         AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            try
            {
                string strSQL = string.Empty;

                // OCRデータ削除処理
                strSQL = TBL_OCR_DATA.GetDeleteQueryKijitu(GymID, InputRoute, OpeDate, ImgDirName, ImgFileName);
                dbp.CommandRun(strSQL, new List<IDbDataParameter>(), Tran.Trans);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            return true;
        }

        #endregion

        /// <summary>
        /// 画面IDの取得
        /// </summary>
        public static int GetDSPID(int billcode, List<TBL_CHANGE_DSPIDMF> chgdspidMF)
        {
            IEnumerable<TBL_CHANGE_DSPIDMF> Data = chgdspidMF.Where(x => x._BILL_CODE == billcode);
            if (Data.Count() == 0) return -1;
            return Data.First().m_DSP_ID;
        }

        /// <summary>
        /// 項目名取得
        /// </summary>
        public static string GetTRItemName(int ItemID, List<TBL_ITEM_MASTER> ItemMF)
        {
            IEnumerable<TBL_ITEM_MASTER> Data = ItemMF.Where(x => x._ITEM_ID == ItemID);
            if (Data.Count() == 0) return string.Empty;

            return Data.First().m_ITEM_NAME;
        }

        /// <summary>
        /// 項目定義桁数取得
        /// </summary>
        public static int GetDSPItemLen(int GymId, int DspID, int ItemID, List<TBL_DSP_ITEM> DspItemMF)
        {
            IEnumerable<TBL_DSP_ITEM> Data = DspItemMF.Where(x => x._GYM_ID == GymId && x._DSP_ID == DspID && x._ITEM_ID == ItemID);
            if (Data.Count() == 0) return 0;

            return Data.First().m_ITEM_LEN;
        }

        #endregion

        #region 変換処理

        /// <summary>
        /// 和暦変換
        /// </summary>
        public static string ConvWareki(int date)
        {
            // 和暦算出
            iBicsCalendar cal = new iBicsCalendar();
            string gengo = cal.getGengo(date);
            if (DBConvert.ToIntNull(gengo) >= 0)
            {
                string wareki = iBicsCalendar.datePlanetoDisp3(DBConvert.ToStringNull(cal.getWareki(date)));
               return string.Format("{0} {1}", gengo, wareki);
            }
            return string.Empty;
        }

        #endregion

        #region クラス

        /// <summary>
        /// 設定情報管理クラス
        /// </summary>
        public class OCRSettingData
        {
            /// <summary>
            /// exe.config 画面ID設定（交換証券種類から変換できない場合）
            /// </summary>
            public int DspIDDefault { get; private set; } = 0;

            /// <summary>
            /// exe.config OCR信頼度閾値（補正済とする閾値）
            /// </summary>
            public int OCRLevel { get; private set; } = 999;

            /// <summary>
            /// exe.config OCR読取フィールド名設定（持出銀行）
            /// </summary>
            public string OC_BK_NO { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（持出支店）
            /// </summary>
            public string OC_BR_NO { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（持帰銀行）
            /// </summary>
            public string IC_BK_NO { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（スキャン支店）
            /// </summary>
            public string SCAN_BR_NO { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（スキャン日）
            /// </summary>
            public string SCAN_DATE { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（交換希望日）
            /// </summary>
            public string CLEARING_DATE { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（スキャン枚数）
            /// </summary>
            public string SCAN_COUNT { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（合計枚数）
            /// </summary>
            public string TOTAL_COUNT { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（合計金額）
            /// </summary>
            public string TOTAL_AMOUNT { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（金額）
            /// </summary>
            public string AMOUNT { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（交換証券種類）
            /// </summary>
            public string BILL { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（メモ情報）
            /// </summary>
            public string MEMO { get; private set; } = "";

            public OCRSettingData(int DspIDDef, int OcrLevel, string OC_BKNo, string OC_BRNo, string IC_BKNo, string Scan_BRNo, string ScanDate, string ClearingDate, 
                                  string ScanCount, string TotalCount, string TotalAmt, string Amt, string Bill,string Memo)
            {
                DspIDDefault = DspIDDef;
                OCRLevel = OcrLevel;
                OC_BK_NO = OC_BKNo;
                OC_BR_NO = OC_BRNo;
                IC_BK_NO = IC_BKNo;
                SCAN_BR_NO = Scan_BRNo;
                SCAN_DATE = ScanDate;
                CLEARING_DATE = ClearingDate;
                SCAN_COUNT = ScanCount;
                TOTAL_COUNT = TotalCount;
                TOTAL_AMOUNT = TotalAmt;
                AMOUNT = Amt;
                BILL = Bill;
                MEMO = Memo;
            }
        }


        /// <summary>
        /// 設定情報管理クラス(期日)
        /// </summary>
        public class OCRSettingDataKijitu
        {
            /// <summary>
            /// exe.config 画面ID設定（交換証券種類から変換できない場合）
            /// </summary>
            public int DspIDDefault { get; private set; } = 0;

            /// <summary>
            /// exe.config OCR読取フィールド名設定（持帰銀行コード）
            /// </summary>
            public string IC_BK_NO { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（金額）
            /// </summary>
            public string AMOUNT { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（手形期日）
            /// </summary>
            public string TEGATAKIJITU { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（交換希望日）
            /// </summary>
            public string CLEARING_DATE { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（手形種類）
            /// </summary>
            public string TEGATA { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（持帰支店コード）
            /// </summary>
            public string IC_BR_NO { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（口座番号）
            /// </summary>
            public string KOUZANUMBER { get; private set; } = "";

            /// <summary>
            /// exe.config OCR読取フィールド名設定（手形番号）
            /// </summary>
            public string TEGATANUMBER { get; private set; } = "";

            public OCRSettingDataKijitu(int DspIDDef, string IC_BkNo, string Amt, string Tegatakijitu, string ClearingDate, string Tegata,
                                  　   string IC_BrNo, string KouzaNum, string TegataNum)
            {
                DspIDDefault = DspIDDef;
                IC_BK_NO = IC_BkNo;
                AMOUNT = Amt;
                TEGATAKIJITU = Tegatakijitu;
                CLEARING_DATE = ClearingDate;
                TEGATA = Tegata;
                IC_BR_NO = IC_BrNo;
                KOUZANUMBER = KouzaNum;
                TEGATANUMBER = TegataNum;
            }
        }

        #endregion
    }
}
