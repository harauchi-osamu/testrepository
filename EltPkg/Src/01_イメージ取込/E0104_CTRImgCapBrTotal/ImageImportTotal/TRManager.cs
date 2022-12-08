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


namespace ImageImportTotal
{
    /// <summary>
    /// トランザクションデータ登録処理管理クラス
    /// </summary>
    public class TRManager
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private string _ImgFolderPath = "";

        /// <summary>合計票インプット表示データ</summary>
        private TBL_BR_TOTAL InputTotalData
        { 
            get { return _itemMgr.InputTotalData; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TRManager(Controller ctl, string ImgFolderPath)
        {
            _ctl = ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;

            _ImgFolderPath = ImgFolderPath;
        }

        /// <summary>
        /// イメージ移動・データ登録処理
        /// </summary>
        public bool TRDataInput()
        {
            Dictionary<string, ImportFileAccess.DetailCtl> RenameList = new Dictionary<string, ImportFileAccess.DetailCtl>();   //リネームリスト

            //端末IPアドレスの取得
            string TermIPAddress = ImportFileAccess.GetTermIPAddress().Replace(".", "_");

            // 格納先フォルダ名の算出
            string TotalFolderPath = string.Format(NCR.Server.BankTotalImageRoot, InputTotalData.m_BK_NO);

            // 登録処理
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    //支店別合計票の全データロック
                    if (!_itemMgr.GetLockAllBrTotal(out List<TBL_BR_TOTAL> Totals, dbp, Tran))
                    {
                        // 取得した行ロックを解除するためロールバック
                        // メッセージボックス表示前に実施
                        Tran.Trans.Rollback();
                        //エラーを表示して画面に戻る。
                        CommonClass.ComMessageMgr.MessageWarning("他端末で処理中のため確定処理が行えません。しばらくお待ちください");
                        return false;
                    }

                    // 「持出銀行・持出支店・スキャン日」単位での重複チェック(STATUSが完了のみ)
                    IEnumerable<TBL_BR_TOTAL> ie = Totals.Where(x => x.m_STATUS == (int)TBL_BR_TOTAL.enumStatus.Complete &&
                                                                     x.m_BK_NO == InputTotalData.m_BK_NO &&
                                                                     x.m_BR_NO == InputTotalData.m_BR_NO &&
                                                                     x.m_SCAN_DATE == InputTotalData.m_SCAN_DATE);
                    if (ie.Count() > 0)
                    {
                        // 取得した行ロックを解除するためロールバック
                        // メッセージボックス表示前に実施
                        Tran.Trans.Rollback();
                        //エラーを表示して画面に戻る。
                        CommonClass.ComMessageMgr.MessageWarning("持出銀行・持出支店・スキャン日が同じ合計票が登録済です。\n登録内容を確認してください");
                        return false;
                    }

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

                    ImportFileAccess.DetailCtl TotalCtl = new ImportFileAccess.DetailCtl();
                    RenameList.Add(_itemMgr.InputParams.TargetFileName, TotalCtl);
                    //合計票のデータ設定枠を作成
                    TotalCtl.FileList.Add(new ImportFileAccess.FileCtl(ImportFileAccess.FileCtl.ImgKbn.Omote, _itemMgr.InputParams.TargetFileName));

                    // 合計票イメージのリネーム
                    ImportFileAccess.DetailFileRename(_ImgFolderPath, TermIPAddress, TotalCtl, InputTotalData.m_BK_NO, InputTotalData.m_BR_NO,
                                                      _ctl.SettingData.UniqueCodeSleepTime, dbp, Tran);

                    // 合計票(銀行別)にイメージファイルの移動
                    ImportFileAccess.DetailFileMoveBank(_ImgFolderPath, RenameList, TotalFolderPath);

                    //ステータス等の変更
                    _itemMgr.InputTotalData.m_IMPORT_IMG_FLNM = TotalCtl.FileList.First().NewFileName;
                    _itemMgr.InputTotalData.m_STATUS = (int)TBL_BR_TOTAL.enumStatus.Complete;
                    _itemMgr.InputTotalData.m_LOCK_TERM = string.Empty;
                    //更新処理実行
                    if (_itemMgr.UpdateStatusComplete(dbp, Tran) == 0)
                    {
                        //catch でリネームの戻し作業等を行う
                        throw new Exception("ステータスの更新に失敗しました");
                    }

                    // コミット
                    Tran.Trans.Commit();

                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("確定処理終了 対象ファイル:{0}", _itemMgr.InputParams.TargetFileName), 1);
                }
                catch (Exception ex)
                {
                    // ロールバック
                    Tran.Trans.Rollback();
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    // エラー時のファイル名戻し作業
                    ImportFileAccess.RenameImageErrBack(_ImgFolderPath, RenameList, TotalFolderPath);
                    //エラーを表示して画面に戻る。
                    CommonClass.ComMessageMgr.MessageError(CommonClass.ComMessageMgr.E00004, ex.Message);
                    return false;
                }
            }

            return true;
        }

    }
}
