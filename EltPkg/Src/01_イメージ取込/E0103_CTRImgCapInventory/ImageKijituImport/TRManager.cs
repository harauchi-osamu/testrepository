using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Common;
using CommonTable.DB;
using CommonClass;
using CommonClass.DB;
using EntryCommon;
using System.IO;
using System.Text.RegularExpressions;

namespace ImageKijituImport
{
    /// <summary>
    /// トランザクションデータ登録処理管理クラス
    /// </summary>
    public class TRManager
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private int _GymDate = 0;
        private int _BankCode = 0;
        private int _BatchNumber = 0;
        private string _ScanTerm = string.Empty;
        private ItemManager.ImportData _impData = null;
        private Gymdata _GymData = null;
        private bool _BatchProcessedFlg = false;
        private int _Renban = 0;
        private ImportTRAccessComp _TRAccess = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TRManager(Controller ctl, int GymDate, int BankCode, int BatchNumber, string ScanTerm, ItemManager.ImportData impData, Gymdata gymdata, bool BatchProcessedFlg, int Renban)
        {
            _ctl = ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;

            _GymDate = GymDate;
            _BankCode = BankCode;
            _BatchNumber = BatchNumber;
            _ScanTerm = ScanTerm;
            _impData = impData;
            _GymData = gymdata;
            _BatchProcessedFlg = BatchProcessedFlg;
            _Renban = Renban;

            // DB登録クラス作成
            _TRAccess = new ImportTRAccessComp(GymDate, BatchNumber, impData.BatchFolderName, BankCode,
                                               NCR.Terminal.Number, NCR.Operator.UserID, 
                                               _itemMgr._chgdspidMF, GetItemMF(BankCode), GetDspItemMF(BankCode), new EntryReplacer());
        }

        /// <summary>
        /// イメージ移動・データ登録処理
        /// </summary>
        public bool TRDataInput(ref ItemManager.ImportResult result)
        {
            Dictionary<string, ImportFileAccess.DetailCtl> RenameList = new Dictionary<string, ImportFileAccess.DetailCtl>();   //リネームリスト

            ////端末IPアドレスの取得
            //string TermIPAddress = ImportFileAccess.GetTermIPAddress().Replace(".", "_");

            // バッチフォルダ名の算出
            string BachtFolderPath = Path.Combine(string.Format(NCR.Server.BankInventoryImageRoot, _BankCode),
                                                  AppInfo.Setting.GymId.ToString("D3") + _GymDate.ToString("D8") + _BatchNumber.ToString("D8"));

            // 登録処理
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    // 取扱端末ロックの行ロック取得(取込端末のIPアドレスで取得)
                    if (!_itemMgr.GetLockTerm(int.Parse(ImportFileAccess.GetTermIPAddress().Last().ToString()), dbp, Tran))
                    {
                        // 取得した行ロックを解除するためロールバック
                        // メッセージボックス表示前に実施
                        Tran.Trans.Rollback();
                        //エラーを表示して画面に戻る。
                        CommonClass.ComMessageMgr.MessageWarning("他端末で確定処理中です。しばらくお待ちください");
                        return false;
                    }

                    ImportFileAccess.DetailCtl BatchCtl = new ImportFileAccess.DetailCtl();
                    RenameList.Add("0", BatchCtl);

                    //表・裏のデータ設定枠を作成
                    BatchCtl.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Omote, string.Empty));
                    BatchCtl.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Ura, string.Empty));

                    // バッチ登録データの作成
                    // バッチデータ登録データの作成
                    TBL_TRBATCH InsBatchData = new TBL_TRBATCH(AppInfo.Setting.GymId, _GymDate, _ScanTerm, _BatchNumber, _BankCode);
                    InsBatchData.m_STS = 10;    // 状態
                    InsBatchData.m_INPUT_ROUTE = 3;    // スキャン取込ルート
                    InsBatchData.m_OC_BK_NO = _BankCode;    // 持出銀行
                    InsBatchData.m_OC_BR_NO = _GymData.batch[0].batchocr[0].batchocr_ocbranch;    // 持出支店
                    InsBatchData.m_SCAN_BR_NO = _GymData.batch[0].batchocr[0].batchocr_scanbranch;    // スキャン支店
                    InsBatchData.m_SCAN_DATE = _GymDate;    // スキャン日
                    InsBatchData.m_CLEARING_DATE = _GymData.batch[0].batchocr[0].batchocr_batchclearingdate;    // 交換希望日
                    InsBatchData.m_SCAN_COUNT = _GymData.batch[0].batchocr[0].batchocr_scancount;    // スキャン枚数
                    InsBatchData.m_TOTAL_COUNT = _GymData.batch[0].batchocr[0].batchocr_count;    // 合計枚数
                    InsBatchData.m_TOTAL_AMOUNT = _GymData.batch[0].batchocr[0].batchocr_amount;    // 合計金額
                    InsBatchData.m_DELETE_DATE = 0;    // 削除日
                    InsBatchData.m_DELETE_FLG = 0;    // 削除フラグ
                    InsBatchData.m_E_TERM = NCR.Terminal.Number;    // バッチ票入力端末番号
                    InsBatchData.m_E_OPENO = NCR.Operator.UserID;    // バッチ票入力オペレーター番号
                    InsBatchData.m_E_YMD = int.Parse(DateTime.Now.ToString("yyyyMMdd"));    // バッチ票入力日付
                    InsBatchData.m_E_TIME = int.Parse(DateTime.Now.ToString("HHmmss"));    // バッチ票入力時間

                    if (_BatchProcessedFlg)
                    {
                        // バッチデータが登録済の場合

                        //バッチデータの更新
                        if (!_TRAccess.UpdTRBatch(InsBatchData, dbp, Tran))
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("バッチデータの更新に失敗しました");
                        }
                    }
                    else
                    {
                        // バッチデータが未登録の場合

                        //バッチデータの登録
                        if (!_TRAccess.InsTRBatch(InsBatchData, dbp, Tran))
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("バッチデータの登録に失敗しました");
                        }
                        //バッチイメージの登録
                        if (!_TRAccess.InsTRBatchIMG(_ScanTerm, BatchCtl, dbp, Tran))
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("バッチイメージの登録に失敗しました");
                        }
                    }

                    // 証券単位での処理
                    int DetailImportSuccess = 0;
                    long DetailImportTotalAmt = 0;
                    foreach (ItemManager.ImportImgInfo FrontFileInfo in _impData.Detail.OrderBy(info => info.FrontFileName))
                    {
                        // 対象のimgocrを確認
                        if (FrontFileInfo.Ocr.Count ==0)
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("imgocrが存在していません");
                        }

                        // 裏証券イメージ存在チェック
                        if (!ChkFileExists(_impData.FolderName, FrontFileInfo.FrontFileName, _ctl.SettingData.FileNameKbnPosition, _ctl.SettingData.KbnCodeUra, out string ChkFileName))
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("裏イメージが存在していません");
                        }

                        // 指定コード以外の証券イメージ存在チェック
                        if (ChkErrorFileExists(_impData.FolderName, FrontFileInfo.FrontFileName, _ctl.SettingData.FileNameKbnPosition))
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("不正イメージが存在しています");
                        }

                        int DetailsNo = _Renban;
                        if (string.IsNullOrEmpty(FrontFileInfo.ImportKey))
                        {
                            // 新規データの場合ここで連番をカウントアップ
                            _Renban++;
                        }
                        else 
                        {
                            // 登録済データあり
                            if (FrontFileInfo.DeleteFlg && !FrontFileInfo.UploadFlg)
                            {
                                // 明細削除で未アップロードの場合はデータ削除
                                // イメージファイルは日初処理で削除されるためここでは削除は行わない

                                //登録済データキー情報(GYM_ID|OPERATION_DATE|SCAN_TERM|BAT_ID|DETAILS_NO)
                                string[] Keys = CommonUtil.DivideKeys(FrontFileInfo.ImportKey, "|");
                                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("明細データ削除 バッチ番号:{0} 明細番号:{1}", DBConvert.ToIntNull(Keys[3]).ToString("D8"), DBConvert.ToIntNull(Keys[4]).ToString("D5")), 1);

                                // 明細データの削除
                                if (!DeleteDetails(DBConvert.ToIntNull(Keys[0]), DBConvert.ToIntNull(Keys[1]), Keys[2], 
                                                   DBConvert.ToIntNull(Keys[3]), DBConvert.ToIntNull(Keys[4]), _BankCode, dbp, Tran))
                                {
                                    //catch でリネームの戻し作業等を行う
                                    throw new Exception("明細データの削除に失敗しました");
                                }

                                // OCRデータの削除
                                if (!DeleteOCRData(DBConvert.ToIntNull(Keys[0]), DBConvert.ToIntNull(Keys[1]), 
                                                   _impData.BatchFolderName, FrontFileInfo.FrontFileName, dbp, Tran))
                                {
                                    //catch でリネームの戻し作業等を行う
                                    throw new Exception("OCRデータの削除に失敗しました");
                                }

                                // 明細番号は削除したデータの明細番号を設定
                                DetailsNo = DBConvert.ToIntNull(Keys[4]);
                            }
                            else
                            {
                                // 登録済データありで上記以外の場合は次へ（ここまで来る想定ではないが）
                                continue;
                            }
                        }

                        ImportFileAccess.DetailCtl Detail = new ImportFileAccess.DetailCtl();
                        RenameList.Add(DetailsNo.ToString(), Detail);

                        //表証券イメージデータ設定枠を作成
                        Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Omote, FrontFileInfo.FrontFileName));

                        //裏証券イメージデータ設定枠を作成
                        if (ChkFileExists(_impData.FolderName, FrontFileInfo.FrontFileName, _ctl.SettingData.FileNameKbnPosition, _ctl.SettingData.KbnCodeUra, out ChkFileName))
                        {
                            Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Ura, ChkFileName));
                        }

                        //補箋の証券イメージデータ設定枠を作成
                        if (ChkFileExists(_impData.FolderName, FrontFileInfo.FrontFileName, _ctl.SettingData.FileNameKbnPosition, _ctl.SettingData.KbnCodeHosen, out ChkFileName))
                        {
                            Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Hosen, ChkFileName));
                        }

                        //付箋の証券イメージデータ設定枠を作成
                        if (ChkFileExists(_impData.FolderName, FrontFileInfo.FrontFileName, _ctl.SettingData.FileNameKbnPosition, _ctl.SettingData.KbnCodeFusen, out ChkFileName))
                        {
                            Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Fusen, ChkFileName));
                        }

                        //入金証明の証券イメージデータ設定枠を作成
                        if (ChkFileExists(_impData.FolderName, FrontFileInfo.FrontFileName, _ctl.SettingData.FileNameKbnPosition, _ctl.SettingData.KbnCodeNyukin, out ChkFileName))
                        {
                            Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Nyukin, ChkFileName));
                        }

                        // 証券イメージのリネーム(取込端末のIPアドレスでキー取得)
                        ImportFileAccess.DetailFileRename(_impData.FolderName, ImportFileAccess.GetTermIPAddress().Replace(".", "_"), 
                                                          Detail, InsBatchData.m_OC_BK_NO, InsBatchData.m_OC_BR_NO,
                                                          FrontFileInfo.Ocr[0].ocr_icbank, FrontFileInfo.Ocr[0].ocr_clearingdate, FrontFileInfo.Ocr[0].ocr_amount,
                                                          _ctl.SettingData.UniqueCodeSleepTime, dbp, Tran);

                        //明細トランザクションの登録
                        if (!_TRAccess.InsTRMei(_ScanTerm, DetailsNo, FrontFileInfo.Ocr[0].ocr_tegatasyurui, FrontFileInfo.Memo, _ctl.SettingData.OCRSettingData.DspIDDefault, out int DspID, dbp, Tran))
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("明細トランザクションの登録に失敗しました");
                        }

                        //明細イメージトランザクションの登録
                        if (!_TRAccess.InsTRMeiImage(_ScanTerm, DetailsNo, Detail, 0, dbp, Tran))
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("明細イメージトランザクションの登録に失敗しました");
                        }

                        //補正ステータスの登録
                        if (!_TRAccess.InsTRHoseiData(_ScanTerm, DetailsNo, dbp, Tran))
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("補正ステータスの登録に失敗しました");
                        }

                        //項目トランザクションの登録
                        if (!_TRAccess.InsTRItemDataKijitu(_ScanTerm, DetailsNo, FrontFileInfo.Ocr[0].ocr_icbank, FrontFileInfo.Ocr[0].ocr_clearingdate, FrontFileInfo.Ocr[0].ocr_amount, DspID, dbp, Tran))
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("項目トランザクションの登録に失敗しました");
                        }

                        //OCRデータの登録
                        if(!SetDetailOCRData(FrontFileInfo.Ocr[0], _GymDate, _impData.BatchFolderName, FrontFileInfo.FrontFileName, dbp, Tran))
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("OCRデータの登録に失敗しました");
                        }

                        // 成功数更新
                        DetailImportSuccess++;
                        DetailImportTotalAmt += FrontFileInfo.Ocr[0].ocr_amount;
                        //_Renban++;
                    }

                    // 期日管理バッチルート情報(銀行別)にイメージファイルの移動
                    ImportFileAccess.DetailFileMoveBank(_impData.FolderName, RenameList, BachtFolderPath);

                    // コミット
                    Tran.Trans.Commit();
                    result.BatchImportSuccess++;
                    result.DetailImportSuccess += DetailImportSuccess;
                    result.DetailImportTotalAmt += DetailImportTotalAmt;

                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("確定処理 バッチ番号:{0}", _BatchNumber.ToString("D8")), 1);
                }
                catch (Exception ex)
                {
                    // ロールバック
                    Tran.Trans.Rollback();
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    // エラー時のファイル名戻し作業
                    ImportFileAccess.RenameImageErrBack(_impData.FolderName, RenameList, BachtFolderPath);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// イメージ存在チェック
        /// </summary>
        private bool ChkFileExists(string FolderPath, string BaseFileName, string Position, string KbnCode, out string ChkFileName)
        {
            ChkFileName = string.Empty;
            List<string> pos = Position.Split(',').ToList();
            if (pos.Count < 2)
            {
                return false;
            }
            
            //指定箇所を所定文字に置き換えて存在チェックを行う
            ChkFileName = BaseFileName.Remove(int.Parse(pos[0]) - 1, int.Parse(pos[1])).Insert(int.Parse(pos[0]) - 1, KbnCode);
            return File.Exists(Path.Combine(FolderPath, ChkFileName));
        }

        /// <summary>
        /// 指定外のファイルが存在するかチェック
        /// </summary>
        private bool ChkErrorFileExists(string FolderPath, string BaseFileName, string Position)
        {
            List<string> pos = Position.Split(',').ToList();
            if (pos.Count < 2)
            {
                return true;
            }

            string chkstr = "[^" + _ctl.SettingData.KbnCodeOmote + "|" + _ctl.SettingData.KbnCodeUra + "|" + _ctl.SettingData.KbnCodeHosen +
                                "|" + _ctl.SettingData.KbnCodeFusen + "|" + _ctl.SettingData.KbnCodeNyukin + "]";
            //指定箇所を所定文字に置き換えて正規表現チェックを行う
            string ChkFileName = BaseFileName.Remove(int.Parse(pos[0]) - 1, int.Parse(pos[1])).Insert(int.Parse(pos[0]) - 1, chkstr);
            foreach (string name in Directory.EnumerateFiles(FolderPath, "*").Select(name => Path.GetFileName(name)))
            {
                Match mct = Regex.Match(name, ChkFileName);
                if (mct.Success)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// OCRデータ登録
        /// </summary>
        private bool SetDetailOCRData(imgocr ocrData, int GymDate, string FolderPath, string FrontFile,
                                      AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            //持帰銀行コード
            if (!SetOCRData(AppInfo.Setting.GymId, (int)TBL_OCR_DATA.OCRInputRoute.Kijitu, 
                            GymDate, FolderPath, FrontFile, _ctl.SettingData.OCRSettingData.IC_BK_NO,
                            ocrData.ocr_icbank_level, ocrData.ocr_icbank.ToString(), dbp, Tran))
            {
                return false;
            }

            //金額
            if (!SetOCRData(AppInfo.Setting.GymId, (int)TBL_OCR_DATA.OCRInputRoute.Kijitu,
                            GymDate, FolderPath, FrontFile, _ctl.SettingData.OCRSettingData.AMOUNT,
                            ocrData.ocr_amount_level, ocrData.ocr_amount.ToString(), dbp, Tran))
            {
                return false;
            }

            //手形期日
            if (!SetOCRData(AppInfo.Setting.GymId, (int)TBL_OCR_DATA.OCRInputRoute.Kijitu,
                            GymDate, FolderPath, FrontFile, _ctl.SettingData.OCRSettingData.TEGATAKIJITU,
                            ocrData.ocr_billdate_level, ocrData.ocr_billdate.ToString(), dbp, Tran))
            {
                return false;
            }

            //交換希望日
            if (!SetOCRData(AppInfo.Setting.GymId, (int)TBL_OCR_DATA.OCRInputRoute.Kijitu,
                            GymDate, FolderPath, FrontFile, _ctl.SettingData.OCRSettingData.CLEARING_DATE,
                            ocrData.ocr_clearingdate_level, ocrData.ocr_clearingdate.ToString(), dbp, Tran))
            {
                return false;
            }

            //手形種類
            if (!SetOCRData(AppInfo.Setting.GymId, (int)TBL_OCR_DATA.OCRInputRoute.Kijitu,
                            GymDate, FolderPath, FrontFile, _ctl.SettingData.OCRSettingData.TEGATA,
                            ocrData.ocr_tegatasyurui_level, ocrData.ocr_tegatasyurui.ToString(), dbp, Tran))
            {
                return false;
            }

            //持帰支店コード
            if (!SetOCRData(AppInfo.Setting.GymId, (int)TBL_OCR_DATA.OCRInputRoute.Kijitu,
                            GymDate, FolderPath, FrontFile, _ctl.SettingData.OCRSettingData.IC_BR_NO,
                            ocrData.ocr_icbrno_level, ocrData.ocr_icbrno.ToString(), dbp, Tran))
            {
                return false;
            }

            //口座番号
            if (!SetOCRData(AppInfo.Setting.GymId, (int)TBL_OCR_DATA.OCRInputRoute.Kijitu,
                            GymDate, FolderPath, FrontFile, _ctl.SettingData.OCRSettingData.KOUZANUMBER,
                            ocrData.ocr_account_level, ocrData.ocr_account.ToString(), dbp, Tran))
            {
                return false;
            }

            //手形番号
            if (!SetOCRData(AppInfo.Setting.GymId, (int)TBL_OCR_DATA.OCRInputRoute.Kijitu,
                            GymDate, FolderPath, FrontFile, _ctl.SettingData.OCRSettingData.TEGATANUMBER,
                            ocrData.ocr_billno_level, ocrData.ocr_billno.ToString(), dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// OCRデータ登録
        /// </summary>
        private bool SetOCRData(int GymId, int InputRoute, int GymDate, string FolderPath, string FrontFile, string FieldName, int Level, string OCR,
                                AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            //登録データ作成 
            TBL_OCR_DATA data = new TBL_OCR_DATA(GymId, InputRoute, GymDate, FolderPath, FrontFile, FieldName);
            data.m_CONFIDENCE = Level;
            data.m_OCR = OCR;
            return ImportTRAccessCommon.InsertOCRData(data, dbp, Tran);
        }

        /// <summary>
        /// 明細データの削除
        /// </summary>
        private bool DeleteDetails(int GymId, int GymDate, string ScanTerm, int BatId, int Details, int SchemaBankCD,
                                   AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {

            // 明細データ削除
            if (!ImportTRAccessCommon.DeleteTRMeiData(GymId, GymDate, ScanTerm, BatId, Details, SchemaBankCD, dbp, Tran))
            {
                return false;
            }

            // 明細イメージ明細データ削除
            if (!ImportTRAccessCommon.DeleteTRMeiImageData(GymId, GymDate, ScanTerm, BatId, Details, SchemaBankCD, dbp, Tran))
            {
                return false;
            }

            // 補正ステータスデータ削除
            if (!ImportTRAccessCommon.DeleteTRHoseiData(GymId, GymDate, ScanTerm, BatId, Details, SchemaBankCD, dbp, Tran))
            {
                return false;
            }

            // 項目トランザクションデータ削除
            if (!ImportTRAccessCommon.DeleteTRItemData(GymId, GymDate, ScanTerm, BatId, Details, SchemaBankCD, dbp, Tran))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// OCRデータ削除
        /// </summary>
        private bool DeleteOCRData(int GymId, int GymDate, string FolderPath, string FrontFile,
                                   AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // OCRデータ削除
            return ImportTRAccessCommon.DeleteOCRData(GymId, (int)TBL_OCR_DATA.OCRInputRoute.Kijitu, GymDate, FolderPath, FrontFile, dbp, Tran);
        }

        /// <summary>
        /// 項目マスタ取得
        /// </summary>
        private List<TBL_ITEM_MASTER> GetItemMF(int SchemaBankCD)
        {
            List<TBL_ITEM_MASTER> ItemMF = new List<TBL_ITEM_MASTER>();

            string strSQL = TBL_ITEM_MASTER.GetSelectQuery(SchemaBankCD);
            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        ItemMF.Add(new TBL_ITEM_MASTER(tbl.Rows[i], SchemaBankCD));
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
            return ItemMF;
        }

        /// <summary>
        /// 項目定義マスタ取得
        /// </summary>
        private List<TBL_DSP_ITEM> GetDspItemMF(int SchemaBankCD)
        {
            List<TBL_DSP_ITEM> DspItemMF = new List<TBL_DSP_ITEM>();
            string strSQL = TBL_DSP_ITEM.GetSelectQuery(AppInfo.Setting.GymId, SchemaBankCD);
            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        DspItemMF.Add(new TBL_DSP_ITEM(tbl.Rows[i], SchemaBankCD));
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
            return DspItemMF;
        }

    }
}
