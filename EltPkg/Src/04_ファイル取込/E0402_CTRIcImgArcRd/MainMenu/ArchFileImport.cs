using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using CommonTable.DB;
using EntryCommon;
using CommonClass;

namespace MainMenu
{
    class ArchFileImport
    {
        #region クラス変数
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private bool CreateFolderFlg = false;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ArchFileImport(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// アーカイブ取込処理
        /// </summary>
        public bool ARCHImport()
        {
            // 「IO集信フォルダ(銀行別)」フォルダにコピー
            File.Copy(Path.Combine(_itemMgr.HULFTReceiveRoot(), _itemMgr._TargetFilename), Path.Combine(_itemMgr.IOReceiveRoot(), _itemMgr._TargetFilename), true);

            // ファイル集配信管理テーブルに登録
            _itemMgr.SetFileCtlKey(_itemMgr._file_id, _itemMgr._file_divid, new string('Z', 32), _itemMgr._TargetFilename);
            if (!_itemMgr.FileCtlInsert()) return false;

            bool DeployFlg = false;
            try
            {
                //展開処理
                using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
                using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
                {
                    DeployFlg = FileDeploy(dbp, Tran);
                    
                    if (DeployFlg)
                    {
                        // コミット
                        Tran.Trans.Commit();
                        // HULFT集信フォルダのファイル削除
                        File.Delete(Path.Combine(_itemMgr.HULFTReceiveRoot(), _itemMgr._TargetFilename));
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _itemMgr._TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _itemMgr._TargetFilename, 3);
                DeployFlg = false;
                // エラー時の削除処理
                ErrBack();
            }
            finally
            {
                // ファイル集配信管理テーブル更新
                _itemMgr.FileCtlUpdate((DeployFlg == true) ? 10 : 9);
            }

            return DeployFlg;
        }

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

        /// <summary>
        /// 展開処理
        /// </summary>
        private bool FileDeploy(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル展開処理：{0}", _itemMgr._TargetFilename), 3);

            // 「持帰要求結果管理」テーブルのイメージアーカイブ取込状態を更新
            if (_itemMgr.UpdateICREQRETCtl(5, dbp, Tran) == 0)
            {
                throw new Exception("持帰要求結果管理テーブルに対象アーカイブファイルの定義がありませんでした");
            }

            // 展開フォルダの作成
            string ArchName = Path.GetFileNameWithoutExtension(_itemMgr._TargetFilename);
            string FolderName = Path.Combine(_itemMgr.BankCheckImageRoot(), ArchName);
            if (Directory.Exists(FolderName))
            {
                // 存在している場合はエラー
                throw new Exception("アーカイブファイルの展開フォルダが既に存在しています");
            }
            Directory.CreateDirectory(FolderName);
            CreateFolderFlg = true;

            // ファイルの展開
            if (!CommonClass.TarArchive.UnPackTarImg(Path.Combine(_itemMgr.HULFTReceiveRoot(), _itemMgr._TargetFilename), FolderName))
            {
                throw new Exception("アーカイブファイルの展開に失敗しました");
            }

            if (NCR.Server.OCROption == 1)
            {
                // OCROptionありの場合
                // 持帰OCR取込用ルートにファイルコピー
                if(!InclearingFileAccess.ImageICOCRCopy(ArchName, FolderName, NCR.Server.ScanImageICOCRRoot, NCR.Server.ICOCRCopyUnitCount))
                {
                    throw new Exception("持帰OCR取込用フォルダへの展開に失敗しました");
                }
            }

            return true;
        }

        /// <summary>
        /// 戻し作業
        /// </summary>
        private void ErrBack()
        {
            try
            {
                // 展開フォルダの削除
                if (CreateFolderFlg)
                {
                    string FolderName = Path.Combine(_itemMgr.BankCheckImageRoot(), Path.GetFileNameWithoutExtension(_itemMgr._TargetFilename));
                    Directory.Delete(FolderName, true);
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _itemMgr._TargetFilename + ")", 3);
            }
        }
    }
}
