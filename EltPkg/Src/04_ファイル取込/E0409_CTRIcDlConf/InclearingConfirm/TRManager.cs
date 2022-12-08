using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using CommonTable.DB;
using CommonClass;
using CommonClass.DB;
using EntryCommon;
using System.IO;
using IFImportCommon;

namespace InclearingConfirm
{
    /// <summary>
    /// トランザクションデータ登録処理管理クラス
    /// </summary>
    public class TRManager
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private int _BatchNumber = 0;
        private ImportTRAccessDownLoad _TRAccess = null;
        private CTRTxtRd _txtRd = null;
        private CTRPkgRd _pkgRd = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TRManager(Controller ctl)
        {
            _ctl = ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;
            _BatchNumber = 1;
            // 読取クラス作成
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                _txtRd = new CTRTxtRd(AppInfo.Setting.SchemaBankCD, dbp, 2);
                _pkgRd = new CTRPkgRd(AppInfo.Setting.SchemaBankCD, dbp);
            }
            // DB登録クラス作成
            _TRAccess = new ImportTRAccessDownLoad(_ctl.SettingData, 1, _itemMgr._chgdspidMF, _itemMgr._itemMF, new EntryReplacer(), _itemMgr._hoseidispitemMF, _itemMgr._dspitemMF);
        }

        /// <summary>
        /// イメージ移動・データ登録処理
        /// </summary>
        public void TRDataInput(IGrouping<string, ItemManager.ConfirmData> FrontNameGrp, ItemManager.ImportResult result)
        {
            List<InclearingFileAccess.FileCtl> MoveList = new List<InclearingFileAccess.FileCtl>();   //移動リスト

            string BachtFolderPath = string.Empty;
            bool UnDeleteFlg = false;

            // 登録処理
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    List<string> wkLogMsg = new List<string>();

                    // 登録対象データの抽出
                    GetInputData(FrontNameGrp, out IEnumerable<ItemManager.ConfirmData> NotAvailableGrp, out IEnumerable<ItemManager.ConfirmData> AvailableGrp, out ItemManager.OverRideType DetailOverRide);

                    // 表面イメージから対象証券の登録チェック
                    if (!_itemMgr.ChkItemDataFileName(FrontNameGrp.Key, out int DetailInput, out TBL_TRMEI TRMei, dbp))
                    {
                        // 処理失敗
                        throw new Exception(string.Format("証券登録済チェックエラー{0}", FrontNameGrp.Key));
                    }

                    // 対象データ未登録時のチェック
                    if ((DetailInput == 0 || DetailOverRide == ItemManager.OverRideType.DeleteInsert || DetailOverRide == ItemManager.OverRideType.UnDelete) &&
                        !(NotAvailableGrp.Where(x => x.LineData[TBL_ICREQRET_BILLMEITXT.IMG_KBN] == TrMeiImg.ImgKbn.表.ToString()).Count() == 1 &&
                          NotAvailableGrp.Where(x => x.LineData[TBL_ICREQRET_BILLMEITXT.IMG_KBN] == TrMeiImg.ImgKbn.裏.ToString()).Count() == 1))
                    {
                        // 未登録で「表・裏」が存在していない場合 
                        // （登録済で上書き対象・削除解除対象の場合も対象）
                        throw new Exception(string.Format("表と裏のイメージが存在していません{0}", FrontNameGrp.Key));
                    }

                    //登録済TRMEIデータの削除
                    if (DetailOverRide == ItemManager.OverRideType.DeleteInsert)
                    {
                        // 明細が上書き対象の場合は対象明細をすべて削除
                        if(!_itemMgr.DeleteTRMEIAllData(TRMei, dbp, Tran))
                        {
                            // 処理失敗
                            throw new Exception(string.Format("明細削除エラー{0}", FrontNameGrp.Key));
                        }
                        wkLogMsg.Add(string.Format("登録済明細削除 処理日:{0} バッチ番号:{1} 明細番号:{2}", TRMei._OPERATION_DATE, TRMei._BAT_ID.ToString("D8"), TRMei._DETAILS_NO.ToString("D8")));

                        // 登録状況を初期化して新規明細として取り込む
                        DetailInput = 0;
                        TRMei = new TBL_TRMEI(AppInfo.Setting.SchemaBankCD);
                    }
                    else if (DetailOverRide == ItemManager.OverRideType.UnDelete)
                    {
                        // 明細が削除解除対象の場合は対象明細を削除解除
                        if (!_itemMgr.UnDeleteTRMEIAllData(TRMei, dbp, Tran))
                        {
                            // 処理失敗
                            throw new Exception(string.Format("明細削除解除エラー{0}", FrontNameGrp.Key));
                        }
                        wkLogMsg.Add(string.Format("明細削除解除 処理日:{0} バッチ番号:{1} 明細番号:{2}", TRMei._OPERATION_DATE, TRMei._BAT_ID.ToString("D8"), TRMei._DETAILS_NO.ToString("D8")));
                        // 削除フラグのクリア
                        TRMei.m_DELETE_FLG = 0;
                        UnDeleteFlg = true;
                    }

                    // 明細番号算出
                    int DetailsNo = 0;
                    int OpeDate = 0;
                    string ScanTerm = string.Empty;
                    if (DetailInput == 0)
                    {
                        // 明細未登録 OR 登録済で上書き対象

                        if (!_itemMgr.GetItemDetailNo(FrontNameGrp.Key, out DetailsNo, dbp))
                        {
                            // 処理失敗
                            throw new Exception(string.Format("明細番号取得エラー{0}", FrontNameGrp.Key));
                        }

                        //未登録時、取得した明細番号の+1を登録の明細番号とする
                        DetailsNo++;

                        //端末IPアドレスの取得
                        OpeDate = AplInfo.OpDate();
                        ScanTerm = ImportFileAccess.GetTermIPAddress().Replace(".", "_");

                        // バッチフォルダ名の算出
                        BachtFolderPath = Path.Combine(_itemMgr.BankConfirmImageRoot(), AppInfo.Setting.GymId.ToString("D3") + OpeDate.ToString("D8") + _BatchNumber.ToString("D8"));
                    }
                    else
                    {
                        //取得した明細番号を明細番号とする
                        DetailsNo = TRMei._DETAILS_NO;

                        //端末IPアドレスの取得
                        OpeDate = TRMei._OPERATION_DATE;
                        ScanTerm = TRMei._SCAN_TERM;

                        // バッチフォルダ名の算出
                        BachtFolderPath = Path.Combine(_itemMgr.BankConfirmImageRoot(), AppInfo.Setting.GymId.ToString("D3") + OpeDate.ToString("D8") + _BatchNumber.ToString("D8"));
                    }

                    IEnumerable<ItemManager.ConfirmData> FrontData = NotAvailableGrp.Where(x => x.LineData[TBL_ICREQRET_BILLMEITXT.IMG_KBN] == TrMeiImg.ImgKbn.表.ToString());

                    // 登録済TRITEMの削除ケースなし
                    //if (DetailInput > 0 && FrontData.Count() == 1)
                    //{
                    //    // 登録済で「表」が存在している場合
                    //    if (!_itemMgr.DeleteTRITEMData(OpeDate, _BatchNumber, DetailsNo, dbp, Tran))
                    //    {
                    //        // 処理失敗
                    //        throw new Exception(string.Format("データ削除エラー{0}", FrontNameGrp.Key));
                    //    }
                    //    wkLogMsg.Add(string.Format("登録済ITEM削除 処理日:{0} バッチ番号:{1} 明細番号:{2}", OpeDate, _BatchNumber.ToString("D8"), DetailsNo.ToString("D8")));
                    //}

                    // 登録済のイメージ削除(想定ケースはないが削除フラグが立っている場合削除 ※ 削除解除ケースは対象外)
                    foreach (ItemManager.ConfirmData Data in NotAvailableGrp.Where(x => x.LineData["IMGFLG"] == "1" && DetailOverRide != ItemManager.OverRideType.UnDelete))
                    {
                        if (!_itemMgr.DeleteTRMEIIMGData(OpeDate, _BatchNumber, DetailsNo, Data.LineData[TBL_ICREQRET_BILLMEITXT.IMG_KBN], dbp, Tran))
                        {
                            // 処理失敗
                            throw new Exception(string.Format("データ削除エラー{0}", FrontNameGrp.Key));
                        }
                    }

                    // 明細イメージトランザクションの登録
                    foreach (ItemManager.ConfirmData Data in NotAvailableGrp)
                    {
                        string RetReqTxtName = Data.LineData[TBL_ICREQRET_CTL.RET_REQ_TXT_NAME];
                        string MeiTxtName = Data.LineData[TBL_ICREQRET_BILLMEITXT.MEI_TXT_NAME];
                        string CapKbn = Data.LineData[TBL_ICREQRET_BILLMEITXT.CAP_KBN];
                        string ImgName = Data.LineData[TBL_ICREQRET_BILLMEITXT.IMG_NAME];
                        string ImgKbn = Data.LineData[TBL_ICREQRET_BILLMEITXT.IMG_KBN];
                        string ArchName = Data.LineData[TBL_ICREQRET_BILLMEITXT.IMG_ARCH_NAME];

                        // 削除解除で登録済のデータは登録除外
                        bool insFlg = true;
                        if (DetailOverRide == ItemManager.OverRideType.UnDelete && Data.LineData["IMGFLG"] != "-1")
                        {
                            insFlg = false;
                        }
                        if (insFlg)
                        {
                            if (!_TRAccess.InsTRMeiImage(OpeDate, ScanTerm, DetailsNo, int.Parse(ImgKbn), ImgName, ArchName, dbp, Tran))
                            {
                                // 処理失敗
                                throw new Exception(string.Format("明細イメージトランザクション登録エラー{0}", FrontNameGrp.Key));
                            }
                        }
                        string outExtensionName = Path.GetFileNameWithoutExtension(ArchName);
                        MoveList.Add(new InclearingFileAccess.FileCtl(RetReqTxtName, MeiTxtName, CapKbn, ImgName, outExtensionName));
                    }

                    if (DetailInput == 0 && FrontData.Count() == 1)
                    {
                        // 新規明細で「表」が存在している場合、TRITEM登録用のデータを取得

                        //ファイル名の持出銀行取得
                        string FILE_OCBKNo = FrontData.First().LineData[TBL_ICREQRET_BILLMEITXT.FILE_OC_BK_NO];

                        int DspID = 0;
                        List<TBL_OCR_DATA> OutOcrList = new List<TBL_OCR_DATA>();
                        if (FILE_OCBKNo == AppInfo.Setting.SchemaBankCD.ToString("D4"))
                        {
                            //自行の場合、持出側の情報取得
                            _itemMgr.GetOutclearingOCRInfo(FrontNameGrp.Key, out int InputRoute, out string FolderName, out string oldFileName, out DspID, dbp);

                            // 持出OCRデータの取得
                            OutOcrList = _itemMgr.GetMeiOCRData(InputRoute, FolderName, oldFileName, dbp);
                        }

                        // 持帰OCRデータの取得
                        List<TBL_OCR_DATA> InOcrList = _itemMgr.GetMeiOCRDataFileName(FrontNameGrp.Key, dbp);

                        // 自行情報(交換証券種類コード)
                        string BillCode = _txtRd.GetText(FrontData.First().LineData, "交換証券種類コード");

                        // 通知テキストでの更新値を取得
                        GetTsuchiTxtChgData(FrontNameGrp.Key, FrontData.First(),
                                            out string OCBKNo, out string OldOCBKNo, out int MRBDate, out int BUBDate, out int BCADate, out int DelDate, out int DelFlg,
                                            dbp, Tran);

                        int dspID;
                        // 明細トランザクションの登録
                        if (!_TRAccess.InsTRMei(OpeDate, ScanTerm, DetailsNo, BillCode,
                                                OCBKNo, OldOCBKNo, MRBDate, BUBDate, BCADate, DelDate, DelFlg,
                                                out dspID, dbp, Tran))
                        {
                            // 処理失敗
                            throw new Exception(string.Format("明細トランザクション登録エラー{0}", FrontNameGrp.Key));
                        }
                        //int dspID;
                        //// 明細トランザクションの登録
                        //if (!_TRAccess.InsTRMei(OpeDate, ScanTerm, DetailsNo, BillCode, FILE_OCBKNo, out dspID, dbp, Tran))
                        //{
                        //    // 処理失敗
                        //    throw new Exception(string.Format("明細トランザクション登録エラー{0}", FrontNameGrp.Key));
                        //}
                        //if (DetailInput == 0)
                        //{
                        //    // 新規データの場合
                        //    // 明細トランザクションの登録
                        //    if (!_TRAccess.InsTRMei(OpeDate, ScanTerm, DetailsNo, BillCode, FILE_OCBKNo, out dspID, dbp, Tran))
                        //    {
                        //        // 処理失敗
                        //        throw new Exception(string.Format("明細トランザクション登録エラー{0}", FrontNameGrp.Key));
                        //    }
                        //}
                        //else
                        //{
                        //    // データが存在する場合、DSPIDを更新
                        //    if (!_TRAccess.UpdTRMei(OpeDate, ScanTerm, DetailsNo, BillCode, out dspID, dbp, Tran))
                        //    {
                        //        // 処理失敗
                        //        throw new Exception(string.Format("明細トランザクション更新エラー{0}", FrontNameGrp.Key));
                        //    }
                        //}

                        // 補正ステータスの登録
                        if (!_TRAccess.InsTRHoseiData(OpeDate, ScanTerm, DetailsNo, dbp, Tran))
                        {
                            // 処理失敗
                            throw new Exception(string.Format("補正ステータス登録エラー{0}", FrontNameGrp.Key));
                        }

                        // 項目トランザクションの登録
                        if (!_TRAccess.InsTRItemData(OpeDate, ScanTerm, DetailsNo, FrontData.First(), dspID, OutOcrList, InOcrList, _txtRd, _pkgRd, dbp, Tran))
                        {
                            // 処理失敗
                            throw new Exception(string.Format("項目トランザクション登録エラー{0}", FrontNameGrp.Key));
                        }
                    }

                    // 登録済イメージデータの処理
                    foreach (ItemManager.ConfirmData Data in AvailableGrp)
                    {
                        // 登録済のイメージでもイメージファイルは最新取得ファイルで上書きコピー
                        string RetReqTxtName = Data.LineData[TBL_ICREQRET_CTL.RET_REQ_TXT_NAME];
                        string MeiTxtName = Data.LineData[TBL_ICREQRET_BILLMEITXT.MEI_TXT_NAME];
                        string CapKbn = Data.LineData[TBL_ICREQRET_BILLMEITXT.CAP_KBN];
                        string ImgName = Data.LineData[TBL_ICREQRET_BILLMEITXT.IMG_NAME];
                        string ArchName = Data.LineData[TBL_ICREQRET_BILLMEITXT.IMG_ARCH_NAME];

                        string outExtensionName = Path.GetFileNameWithoutExtension(ArchName);
                        MoveList.Add(new InclearingFileAccess.FileCtl(RetReqTxtName, MeiTxtName, CapKbn, ImgName, outExtensionName));

                        wkLogMsg.Add(string.Format("登録済イメージ処理 処理日:{0} バッチ番号:{1} 明細番号:{2} イメージ:{3}", OpeDate, _BatchNumber.ToString("D8"), DetailsNo.ToString("D8"), ImgName));
                    }

                    // 持帰要求結果証券明細テキストの確定フラグ更新
                    foreach (var data in MoveList.GroupBy(x => new { x.RetReqTxtName, x.MeiTxtName, x.CapKbn }))
                    {
                        if(!_itemMgr.UpdateReqRetCtlSts(data.Key.RetReqTxtName, data.Key.MeiTxtName, data.Key.CapKbn, data.Select(x => x.FileName).ToList(), dbp, Tran))
                        {
                            // 処理失敗
                            throw new Exception(string.Format("持帰要求結果更新処理エラー{0}", FrontNameGrp.Key));
                        }
                    }

                    // イメージの移動
                    InclearingFileAccess.DetailFileMove(_itemMgr.BankCheckImageRoot(), MoveList, BachtFolderPath);

                    //コミット
                    Tran.Trans.Commit();

                    //ログ出力
                    foreach(string msg in wkLogMsg)
                    {
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), msg, 1);
                    }
                    if (NotAvailableGrp.Count() > 0)
                    {
                        // 追加データがあれば出力
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("登録処理終了 処理日:{0} バッチ番号:{1} 明細番号:{2}", OpeDate, _BatchNumber.ToString("D8"), DetailsNo.ToString("D8")), 1);
                    }

                    //結果反映
                    if (UnDeleteFlg) result.UnDeleteDetailSuccess++;
                    if (NotAvailableGrp.Count() > 0) result.DetailImportSuccess++;
                    result.DetailImgImportSuccess += NotAvailableGrp.Count();
                }
                catch (Exception ex)
                {
                    // ロールバック
                    Tran.Trans.Rollback();
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    // エラー時の戻し作業
                    InclearingFileAccess.MoveImageErrBack(_itemMgr.BankCheckImageRoot(), MoveList, BachtFolderPath);
                    result.DetailImportFail++;
                    return;
                }
            }
        }

        /// <summary>
        /// 登録対象データの抽出
        /// </summary>
        /// <param name="Grp"></param>
        /// <param name="NotAvailableGrp">未登録データ</param>
        /// <param name="AvailableGrp">登録済データ</param>
        /// <param name="DetailOverRide">上書き対象かどうか</param>
        public void GetInputData(IEnumerable<ItemManager.ConfirmData> Grp, 
                                 out IEnumerable<ItemManager.ConfirmData> NotAvailableGrp, out IEnumerable<ItemManager.ConfirmData> AvailableGrp,
                                 out ItemManager.OverRideType DetailOverRide)
        {
            // 上書きチェック
            DetailOverRide = _itemMgr.ChkOverRideData(Grp.First().LineData, Grp);

            // Grpから登録対象のデータを抽出した一覧
            NotAvailableGrp = Grp.Where(x => !_itemMgr.ChkSkipConfirmData(x.LineData, Grp)).Distinct(new ItemManager.ConfirmDataImgKbnComparer());
            // Grpから登録対象データを除外した一覧
            AvailableGrp = Grp.Except(NotAvailableGrp, new ItemManager.ConfirmDataKeyComparer());
        }

        /// <summary>
        /// 通知テキストでの更新値を取得
        /// </summary>
        private void GetTsuchiTxtChgData(string FrontFileName, ItemManager.ConfirmData FrontData,
                                         out string OCBKNo, out string OldOCBKNo, out int MRBDate, out int BUBDate, out int BCADate, out int DelDate, out int DelFlg,
                                         AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            // ファイル名持出銀行
            string FILE_OCBKNo = FrontData.LineData[TBL_ICREQRET_BILLMEITXT.FILE_OC_BK_NO];
            // 取込区分
            string CAPKBN = FrontData.LineData[TBL_ICREQRET_BILLMEITXT.CAP_KBN];

            if (CAPKBN == "0")
            {
                // 電子交換所

                // 対象証券の通知テキストを取得
                if (!_itemMgr.GetInclearingTsuchiTxt(FrontFileName, out List<ItemManager.TsuchiTxtData> tsuchiTxtDatas, dbp, Tran))
                {
                    // 処理失敗
                    throw new Exception(string.Format("対象証券の通知テキスト取得エラー{0}", FrontFileName));
                }

                ItemManager.TsuchiTxtData TxtData = null;

                // 二重持出処理
                BUBDate = 0;
                if (GetFileDividTsuchiTxt(FileParam.FileKbn.BUB, tsuchiTxtDatas, ref TxtData))
                {
                    // データがある場合は二重持出通知日(持帰)を設定
                    BUBDate = TxtData.MAKE_DATE;
                }

                // 金融機関読替情報変更通知処理(持出変更・持帰銀行向け)
                OCBKNo = FILE_OCBKNo;
                OldOCBKNo = "-1";
                MRBDate = 0;
                if (GetFileDividTsuchiTxt(FileParam.FileKbn.MRB, tsuchiTxtDatas, ref TxtData))
                {
                    // データがある場合は金融機関読替情報変更通知日(持出銀行コード変更・持帰銀行向け)(持帰)を設定
                    MRBDate = TxtData.MAKE_DATE;

                    if (TxtData.TsuchiTxt.m_BK_NO_TEISEI_FLG == "2" && TxtData.FILE_DIVID == FileParam.FileKbn.MRB)
                    {
                        // 変更あり
                        if (!int.TryParse(TxtData.TsuchiTxt.m_TEISEI_AFT_BK_NO, out int intAftdata))
                        {
                            throw new Exception(string.Format("通知テキスト訂正後銀行コード取得エラー{0}", FrontFileName));
                        }
                        OldOCBKNo = FILE_OCBKNo;
                        OCBKNo = TxtData.TsuchiTxt.m_TEISEI_AFT_BK_NO;
                    }
                }

                // 持出取消処理
                BCADate = 0;
                DelDate = 0;
                DelFlg = 0;
                if (GetFileDividTsuchiTxt(FileParam.FileKbn.BCA, tsuchiTxtDatas, ref TxtData))
                {
                    // データがある場合は持出取消通知日(持帰)を設定
                    BCADate = TxtData.MAKE_DATE;
                    DelDate = AplInfo.OpDate();
                    DelFlg = 1;
                }
            }
            else
            {
                // 行内連携

                // 二重持出・金融機関読替情報変更通知関連はそのまま
                BUBDate = 0;
                OCBKNo = FILE_OCBKNo;
                OldOCBKNo = "-1";
                MRBDate = 0;

                // 持出の明細データを取得
                TBL_TRMEI ocMei = _itemMgr.GetOutclearingMeiData(FrontFileName, dbp, Tran);

                // 持出明細が削除・アップロード済の場合は削除状態で登録
                BCADate = 0;
                DelDate = 0;
                DelFlg = 0;
                if (ocMei.m_DELETE_FLG == 1 && ocMei.m_BCA_STS == TrMei.Sts.結果正常)
                {
                    BCADate = AplInfo.OpDate();
                    DelDate = AplInfo.OpDate();
                    DelFlg = 1;
                }
            }
        }

        /// <summary>
        /// 指定ファイル識別区分の通知テキストを取得
        /// </summary>
        /// <remarks>戻り値TrueでTxtData設定なしは不可</remarks>
        private bool GetFileDividTsuchiTxt(string FileDivid, List<ItemManager.TsuchiTxtData> tsuchiTxtDatas,
                                           ref ItemManager.TsuchiTxtData TxtData)
        {
            var Datas = tsuchiTxtDatas.Where(x => x.FILE_DIVID == FileDivid);
            if (Datas.Count() == 0) return false;
            TxtData = Datas.OrderByDescending(x => x.RECV_DATE).ThenByDescending(x => x.RECV_TIME).First();
            return true;
        }

    }
}
