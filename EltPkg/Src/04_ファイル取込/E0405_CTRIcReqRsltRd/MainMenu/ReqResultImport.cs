using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using CommonTable.DB;
using CommonClass;
using EntryCommon;
using IFFileOperation;
using IFImportCommon;

namespace MainMenu
{
    class ReqResultImport
    {
        #region クラス変数
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        #endregion

        // XMLPath
        private string XMLPath { get { return Path.Combine(System.Windows.Forms.Application.StartupPath, "Resources/IF207Load.xml"); } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ReqResultImport(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 持帰要求結果テキスト取込処理
        /// </summary>
        public bool TextImport()
        {
            // 「IO集信フォルダ(銀行別)」フォルダにコピー
            File.Copy(Path.Combine(_itemMgr.HULFTReceiveRoot(), _itemMgr._TargetFilename), Path.Combine(_itemMgr.IOReceiveRoot(), _itemMgr._TargetFilename), true);

            // ファイル集配信管理テーブル更新
            if (!FileCtlStart()) return false;

            bool ImportFlg = false;
            try
            {
                // ファイル読み込み整合性確認
                IFFileDataLoad LoadFile = ChkFile();

                // 持帰要求結果テキスト処理
                AppInfo.Setting.SetGymId(GymParam.GymId.持帰);
                ReqResultImportCommon TxtImp = new ReqResultImportCommon(_ctl, LoadFile);

                // 持帰要求管理存在チェック・更新処理
                if (!TxtImp.UpdateReqCtl())
                {
                    throw new Exception("対象ファイルに対する持帰要求情報を取得できませんでした");
                }

                // 登録処理
                List<TBL_ICREQRET_CTL> ctlList;
                using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
                using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
                {
                    // 取込処理
                    if (!TextImportReqRet(TxtImp, dbp, Tran))
                    {
                        return false;
                    }
                    //コミット
                    Tran.Trans.Commit();

                    // 持帰要求結果管理データ取得
                    if (!_itemMgr.GetReqRetCtl(out ctlList, dbp))
                    {
                        return false;
                    }
                }

                // ダウンロードファイル個別処理実行
                foreach(TBL_ICREQRET_CTL ctl in ctlList)
                {
                    if (!DownLoadFileExec(ctl))
                    {
                        return false;
                    }
                }

                // 処理成功
                ImportFlg = true;

                // HULFT集信フォルダのファイル削除
                File.Delete(Path.Combine(_itemMgr.HULFTReceiveRoot(), _itemMgr._TargetFilename));
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString() + "(" + _itemMgr._TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ex.Message, _itemMgr._TargetFilename, 3);

                ImportFlg = false;
            }
            finally
            {
                // ファイル集配信管理テーブル更新
                _itemMgr.FileCtlUpdate((ImportFlg == true) ? 10 : 9);
                AppInfo.Setting.SetGymId(GymParam.GymId.共通);
            }

            return ImportFlg;
        }

        // *******************************************************************
        // 非公開メソッド
        // *******************************************************************

        #region ファイル集配信管理テーブル

        /// <summary>
        /// 処理開始時のファイル集配信管理更新
        /// </summary>
        private bool FileCtlStart()
        {
            string fileID = string.Empty;

            switch (_itemMgr._file_divid)
            {
                case "GDA":
                    // 持帰要求結果テキスト
                    fileID = "IF203";
                    break;
                default:
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "ファイル識別区分が不正です" + "(" + _itemMgr._TargetFilename + ")", 3);
                    LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), "ファイル識別区分が不正です", _itemMgr._TargetFilename, 3);
                    return false;
            }

            string SearchFileName = _itemMgr._TargetFilename;
            // FileID置き換え
            SearchFileName = SearchFileName.Remove(0, 5).Insert(0, fileID);

            // ファイル集配信管理テーブル更新
            _itemMgr.SetFileCtlKey(fileID, _itemMgr._file_divid, SearchFileName, _itemMgr._TargetFilename);
            if (_itemMgr.FileCtlStartUpdate() <= 0)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "対象ファイルに対する配信情報を取得できませんでした" + "(" + _itemMgr._TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), "対象ファイルに対する配信情報を取得できませんでした", _itemMgr._TargetFilename, 3);

                return false;
            }

            return true;
        }

        #endregion

        #region ファイル整合性確認

        /// <summary>
        /// ファイル整合性確認
        /// </summary>
        private IFFileDataLoad ChkFile()
        {
            // ファイル識別区分チェック
            if (!_itemMgr.GetfileParam(out int FileSize))
            {
                throw new Exception("ファイル識別区分が不正です");
            }

            // データ読み込み準備
            IFFileDataLoad LoadFile = new IFFileDataLoad(Path.Combine(_itemMgr.HULFTReceiveRoot(), _itemMgr._TargetFilename), XMLPath);

            // ファイルサイズチェック
            if (!LoadFile.ChkFileSize(FileSize))
            {
                throw new Exception("ファイルサイズが不正です");
            }

            // データ読み込み
            if (!LoadFile.IFDataLoad())
            {
                string ErrMsg = string.Empty;
                switch (LoadFile.LoadError)
                {
                    case IFFileDataLoad.LoadErrorType.KBNIllegal:
                        ErrMsg = "レコード区分が不正です";
                        break;
                    case IFFileDataLoad.LoadErrorType.DataIllegal:
                        ErrMsg = "データが不正です";
                        break;
                    default:
                        ErrMsg = "データ読み込みに失敗しました";
                        break;
                }
                throw new Exception(ErrMsg);
            }

            // レコード区分順序チェック

            // 最低3レコード数チェック
            if (LoadFile.LoadData.Count < 3)
            {
                throw new Exception("データが不正です");
            }

            // ヘッダ・レコードチェック
            if (!(LoadFile.LoadData.Count(x => x.KBN == "1") == 1 && LoadFile.LoadData.First().KBN == "1"))
            {
                throw new Exception("ヘッダ・レコードが不正です");
            }

            // エンド・レコードチェック
            if (!(LoadFile.LoadData.Count(x => x.KBN == "9") == 1 && LoadFile.LoadData.Last().KBN == "9"))
            {
                throw new Exception("エンド・レコードが不正です");
            }

            // トレーラ・レコードチェック
            if (!(LoadFile.LoadData.Count(x => x.KBN == "8") == 1 && LoadFile.LoadData.Skip(LoadFile.LoadData.Count - 2).Take(1).Count(x => x.KBN == "8") == 1))
            {
                throw new Exception("トレーラ・レコードが不正です");
            }

            // トレーラ・データレコード件数チェック
            IEnumerable<IFData> Data = LoadFile.LoadData.Where(x => x.KBN == "8");
            if (!(Data.Count() == 1 &&
                  long.Parse(Data.First().LineData["レコード件数"]) == LoadFile.LoadData.Count(x => x.KBN == "2")))
            {
                throw new Exception("データレコード件数が不正です");
            }

            return LoadFile;
        }

        #endregion

        #region ファイル確認関連

        /// <summary>
        /// ダウンロードファイル確認
        /// </summary>
        private bool ChkDownLoadFile(string FullPathName)
        {
            string fileName = Path.GetFileName(FullPathName);

            // 対象ファイルの確認
            bool Chk = false;
            for (int i = 1; i <= _ctl.RetryCount; i++)
            {
                // 存在チェック・ロック確認
                if (File.Exists(FullPathName) && ChkFileLock(FullPathName))
                {
                    Chk = true;
                    break;
                }

                //指定時間スリープ
                System.Threading.Thread.Sleep(_ctl.SleepTime);
            }
            if (!Chk)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("持帰要求結果に対応するファイルを確認できませんでした{0}", fileName)  + "(" + _itemMgr._TargetFilename + ")", 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), string.Format("持帰要求結果に対応するファイルを確認できませんでした{0}", fileName), _itemMgr._TargetFilename, 3);
                return false;
            }

            return true;
        }

        /// <summary>
        /// ファイルロック確認
        /// </summary>
        /// <returns></returns>
        private bool ChkFileLock(string FullPathName)
        {
            try
            {
                //排他でファイルを開く
                using (FileStream fs = new FileStream(FullPathName, FileMode.Open, FileAccess.Read, FileShare.None)) { }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 持帰要求結果関連

        /// <summary>
        /// 持帰要求結果テキスト取込処理
        /// </summary>
        private bool TextImportReqRet(ReqResultImportCommon TxtImp, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("持帰要求結果テキスト取込処理：{0}", _itemMgr._TargetFilename), 3);

            // 持帰要求結果管理データ取得
            if (!_itemMgr.GetReqRetCtl(out List<TBL_ICREQRET_CTL> ctlList, dbp))
            {
                return false;
            }

            foreach(IFData ifdata in TxtImp._DataRecord)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("持帰要求登録処理：{0},{1},{2}", _itemMgr._TargetFilename, ifdata.LineData["証券明細テキストファイル名"], ifdata.LineData["イメージアーカイブ名"]), 3);

                // 登録済データ確認
                if (ctlList.Count(x => x._RET_REQ_TXT_NAME == _itemMgr._TargetFilename && x._MEI_TXT_NAME == ifdata.LineData["証券明細テキストファイル名"] && x._CAP_KBN == 0 ) > 0)
                {
                    continue;
                }

                // 登録処理
                if (!TxtImp.ImportReqRetCtl(ifdata, dbp, Tran))
                {
                    return false;
                }
            }

            foreach (TBL_ICREQRET_CTL ctl in ctlList)
            {
                // データ確認
                if (TxtImp._DataRecord.Count(x => x.LineData["証券明細テキストファイル名"] == ctl._MEI_TXT_NAME) == 0)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("持帰要求削除処理：{0},{1},{2}", ctl._RET_REQ_TXT_NAME, ctl._MEI_TXT_NAME, ctl.m_IMG_ARCH_NAME), 3);

                    // テーブルに存在してファイルにない箇所は削除処理
                    if (!_itemMgr.DeleteReqRetCtl(ctl._RET_REQ_TXT_NAME, ctl._MEI_TXT_NAME, dbp, Tran))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// ダウンロードファイル個別処理実行
        /// </summary>
        private bool DownLoadFileExec(TBL_ICREQRET_CTL ctl)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("持帰要求ダウンロード個別処理：{0},{1},{2}", ctl._RET_REQ_TXT_NAME, ctl._MEI_TXT_NAME, ctl.m_IMG_ARCH_NAME), 3);

            // 存在チェック・ロック確認

            // 証券明細テキストファイル
            if (ctl.m_CAP_STS == 0)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル存在確認：{0}", ctl._MEI_TXT_NAME), 1);
                if (!ChkDownLoadFile(Path.Combine(_itemMgr.HULFTReceiveRoot(), ctl._MEI_TXT_NAME)))
                {
                    return false;
                }
            }

            // イメージアーカイブファイル
            if (ctl.m_IMG_ARCH_CAP_STS == 0)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル存在確認：{0}", ctl.m_IMG_ARCH_NAME), 1);
                if (!ChkDownLoadFile(Path.Combine(_itemMgr.HULFTReceiveRoot(), ctl.m_IMG_ARCH_NAME)))
                {
                    return false;
                }
            }

            // 取込処理
            string WorkDir = string.Format(NCR.Server.ExePath, _itemMgr._BankCd);

            // 証券明細テキストファイル
            if (ctl.m_CAP_STS == 0)
            {
                string Argument = string.Format("\"{0}\" {1} \"{2}\"", NCR.Server.IniPath, _itemMgr._BankCd, ctl._MEI_TXT_NAME);
                string ExeName = "CTRBillMeiTxtRd.exe";
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("プログラム起動：{0} 引数：{1}", ExeName, Argument), 3);
                int Rtn = ProcessManager.RunProcess(Path.Combine(WorkDir, ExeName), WorkDir, Argument, true, false);
                if (Rtn != 0)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("取込実行処理でエラーが発生しました{0} 戻り値：{1} ({2})", ExeName, Rtn, _itemMgr._TargetFilename), 3);
                    LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), string.Format("取込実行処理でエラーが発生しました{0} 戻り値：{1}", ExeName, Rtn), _itemMgr._TargetFilename, 3);
                    return false;
                }
            }

            // イメージアーカイブファイル
            if (ctl.m_IMG_ARCH_CAP_STS == 0)
            {
                string Argument = string.Format("\"{0}\" {1} \"{2}\"", NCR.Server.IniPath, _itemMgr._BankCd, ctl.m_IMG_ARCH_NAME);
                string ExeName = "CTRIcImgArcRd.exe";
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("プログラム起動：{0} 引数：{1}", ExeName, Argument), 3);
                int Rtn = ProcessManager.RunProcess(Path.Combine(WorkDir, ExeName), WorkDir, Argument, true, false);
                if (Rtn != 0)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("取込実行処理でエラーが発生しました{0} 戻り値：{1} ({2})", ExeName, Rtn, _itemMgr._TargetFilename), 3);
                    LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), string.Format("取込実行処理でエラーが発生しました{0} 戻り値：{1}", ExeName, Rtn), _itemMgr._TargetFilename, 3);
                    return false;
                }
            }

            return true;
        }

        #endregion

    }
}
