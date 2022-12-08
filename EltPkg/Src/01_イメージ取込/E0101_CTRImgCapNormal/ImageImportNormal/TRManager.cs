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

namespace ImageImportNormal
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
        private int _BatchNumber = 0;
        private string _ImgFolderPath = "";
        private int _FolderFileCount = 0;
        private Dictionary<string, ItemManager.BatchInputParams.ImageData> _ImageList;
        private ImportTRAccess _TRAccess = null;

        /// <summary>バッチ票インプット表示データ</summary>
        private TBL_SCAN_BATCH_CTL InputBatchData 
        { 
            get { return _itemMgr.InputBatchData; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TRManager(Controller ctl, int GymDate, int BatchNumber, string ImgFolderPath, int FolderFileCount, Dictionary<string, ItemManager.BatchInputParams.ImageData> ImageList)
        {
            _ctl = ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;

            _GymDate = GymDate;
            _BatchNumber = BatchNumber;
            _ImgFolderPath = ImgFolderPath;
            _FolderFileCount = FolderFileCount;
            _ImageList = ImageList;

            // DB登録クラス作成
            _TRAccess = new ImportTRAccess(_ctl.SettingData.OCRSettingData, _GymDate, _BatchNumber,
                                           _itemMgr.InputParams.TargetRoute, _itemMgr.InputParams.TargetBatchFolderName,
                                           _itemMgr.InputBatchData, NCR.Terminal.Number, NCR.Operator.UserID, NCR.Server.OC_OCRLevel,
                                           _itemMgr._chgdspidMF, GetItemMF(_itemMgr.InputBatchData.m_OC_BK_NO), GetDspItemMF(_itemMgr.InputBatchData.m_OC_BK_NO), new EntryReplacer());
        }

        /// <summary>
        /// イメージ移動・データ登録処理
        /// </summary>
        public bool TRDataInput()
        {
            Dictionary<string, ImportFileAccess.DetailCtl> RenameList = new Dictionary<string, ImportFileAccess.DetailCtl>();   //リネームリスト

            //端末IPアドレスの取得
            string TermIPAddress = ImportFileAccess.GetTermIPAddress().Replace(".", "_");

            // バッチフォルダ名の算出
            string BachtFolderPath = Path.Combine(string.Format(NCR.Server.BankNormalImageRoot, InputBatchData.m_OC_BK_NO), AppInfo.Setting.GymId.ToString("D3") + _GymDate.ToString("D8") + _BatchNumber.ToString("D8"));

            // 登録処理
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    // 取扱端末ロックの行ロック取得
                    if (!_itemMgr.GetLockTerm(int.Parse(TermIPAddress.Last().ToString()), dbp, Tran))
                    {
                        // 取得した行ロックを解除するためロールバック
                        // メッセージボックス表示前に実施
                        Tran.Trans.Rollback();
                        //エラーを表示して画面に戻る。
                        CommonClass.ComMessageMgr.MessageWarning("他端末で確定処理中です。しばらくお待ちください");
                        return false;
                    }

                    ImportFileAccess.DetailCtl BatchCtl = new ImportFileAccess.DetailCtl();
                    RenameList.Add(_ImageList.First().Key, BatchCtl);

                    //表・裏のデータ設定枠を作成
                    BatchCtl.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Omote, _ImageList.First().Value.Front));
                    BatchCtl.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Ura, _ImageList.First().Value.Back));

                    // バッチ票イメージのリネーム
                    ImportFileAccess.DetailFileRename(_ImgFolderPath, TermIPAddress, BatchCtl, InputBatchData.m_OC_BK_NO, InputBatchData.m_OC_BR_NO, InputBatchData.m_CLEARING_DATE,
                                                      _ctl.SettingData.UniqueCodeSleepTime, dbp, Tran);

                    //バッチデータの登録
                    if (!_TRAccess.InsTRBatch(TermIPAddress, dbp, Tran))
                    {
                        //catch でリネームの戻し作業等を行う
                        throw new Exception("バッチデータの登録に失敗しました");
                    }

                    //バッチイメージの登録
                    if (!_TRAccess.InsTRBatchIMG(TermIPAddress, BatchCtl, dbp, Tran))
                    {
                        //catch でリネームの戻し作業等を行う
                        throw new Exception("バッチイメージの登録に失敗しました");
                    }

                    // 証券単位での処理
                    int DetailCnt = 1;
                    foreach (KeyValuePair<string, ItemManager.BatchInputParams.ImageData> Data in _ImageList)
                    {
                        if (Data.Key == _ImageList.First().Key)
                        {
                            //先頭データは除外
                            continue;
                        }

                        //対象ファイルのOCRデータ取得
                        List<TBL_OCR_DATA> OCRList = _itemMgr.GetMeiOCRData(Data.Value.Front, _itemMgr.InputBatchData.m_SCAN_DATE, _itemMgr.InputParams.TargetBatchFolderName, dbp);

                        ImportFileAccess.DetailCtl Detail = new ImportFileAccess.DetailCtl();
                        RenameList.Add(Data.Key, Detail);
                        //表・裏のデータ設定枠を作成
                        Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Omote, Data.Value.Front));
                        Detail.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Ura, Data.Value.Back));

                        // 証券イメージのリネーム
                        ImportFileAccess.DetailFileRename(_ImgFolderPath, TermIPAddress, Detail, InputBatchData.m_OC_BK_NO, InputBatchData.m_OC_BR_NO, InputBatchData.m_CLEARING_DATE,
                                                          _ctl.SettingData.UniqueCodeSleepTime, dbp, Tran);

                        //明細トランザクションの登録
                        if (!_TRAccess.InsTRMei(TermIPAddress, DetailCnt, Data.Value.Front, out int DspID, OCRList, dbp, Tran))
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("明細トランザクションの登録に失敗しました");
                        }

                        //明細イメージトランザクションの登録
                        if (!_TRAccess.InsTRMeiImage(TermIPAddress, DetailCnt, Detail, dbp, Tran))
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("明細イメージトランザクションの登録に失敗しました");
                        }

                        //項目トランザクションの登録
                        if (!_TRAccess.InsTRItemData(TermIPAddress, DetailCnt, Data.Value.Front, DspID, OCRList, out bool BankComp, out bool AmtComp, dbp, Tran))
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("項目トランザクションの登録に失敗しました");
                        }

                        //補正ステータスの登録
                        if (!_TRAccess.InsTRHoseiData(TermIPAddress, DetailCnt, BankComp, AmtComp, dbp, Tran))
                        {
                            //catch でリネームの戻し作業等を行う
                            throw new Exception("補正ステータスの登録に失敗しました");
                        }

                        DetailCnt++;
                    }

                    // 通常バッチルート情報(銀行別)にイメージファイルの移動
                    ImportFileAccess.DetailFileMoveBank(_ImgFolderPath, RenameList, BachtFolderPath);

                    //ステータス等の変更
                    _itemMgr.InputBatchData.m_STATUS = (int)TBL_SCAN_BATCH_CTL.enumStatus.Complete;
                    _itemMgr.InputBatchData.m_LOCK_TERM = string.Empty;
                    _itemMgr.InputBatchData.m_IMAGE_COUNT = _FolderFileCount;
                    //更新処理実行
                    if (_itemMgr.UpdateStatusComplete(dbp, Tran) == 0)
                    {
                        //catch でリネームの戻し作業等を行う
                        throw new Exception("ステータスの更新に失敗しました");
                    }

                    // コミット
                    Tran.Trans.Commit();

                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("確定処理終了 バッチ番号:{0}", _BatchNumber.ToString("D8")), 1);
                }
                catch (Exception ex)
                {
                    // ロールバック
                    Tran.Trans.Rollback();
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    // エラー時のファイル名戻し作業
                    ImportFileAccess.RenameImageErrBack(_ImgFolderPath, RenameList, BachtFolderPath);
                    //エラーを表示して画面に戻る。
                    CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                    return false;
                }
            }

            return true;
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
