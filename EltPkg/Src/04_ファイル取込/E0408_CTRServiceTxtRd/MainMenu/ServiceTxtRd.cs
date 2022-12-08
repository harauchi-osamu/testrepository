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
    class ServiceTxtRd
    {
        #region クラス変数
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ServiceTxtRd(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// サービス通知取込
        /// </summary>
        public bool TextImport()
        {
            // 「IO集信フォルダ(銀行別)」フォルダにコピー
            File.Copy(Path.Combine(_itemMgr.HULFTReceiveRoot(), _itemMgr._TargetFilename), Path.Combine(_itemMgr.IOReceiveRoot(), _itemMgr._TargetFilename), true);

            // ファイル集配信管理テーブルに登録
            _itemMgr.SetFileCtlKey(_itemMgr._file_id, _itemMgr._file_divid, new string('Z', 32), _itemMgr._TargetFilename);
            if (!_itemMgr.FileCtlInsert()) return false;

            bool ImportFlg = false;
            try
            {
                // 登録処理
                using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
                using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
                {
                    switch (_itemMgr._file_divid)
                    {
                        case "YSA":
                            // サービス開始通知
                            ImportFlg = TextImportServiceStart(dbp, Tran);
                            break;
                        case "YSB":
                            // サービス終了通知
                            ImportFlg = true;
                            break;
                        case "YSC":
                            // 障害復旧通知
                            ImportFlg = true;
                            break;
                        default:
                            break;
                    }

                    if (ImportFlg)
                    {
                        //コミット
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

        #region サービス開始通知関連

        /// <summary>
        /// サービス開始通知取込処理
        /// </summary>
        private bool TextImportServiceStart(AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            if (AplInfo.OpDate() == _itemMgr._MkDate)
            {
                // 処理日とファイル名作成日が同じ場合
                string ErrMsg = string.Format("日初処理実行対象外エラー（{0}）", "処理日とファイル名作成日が同じ");
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ErrMsg, 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ErrMsg, _itemMgr._TargetFilename, 3);

                return false;
            }

            // システム日付取得
            int SystemDate = int.Parse(System.DateTime.Now.ToString("yyyyMMdd"));
            if (AplInfo.OpDate() == SystemDate)
            {
                // 処理日とシステム日付が同じ場合
                string ErrMsg = string.Format("日初処理実行対象外エラー（{0}）", "処理日とシステム日付が同じ");
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ErrMsg, 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ErrMsg, _itemMgr._TargetFilename, 3);

                return false;
            }

            //日初処理実施
            string ExeName = "CTRSod.exe";
            string Argument = string.Format("{0} {1} \"{2}\" {3}", 0, 1, NCR.Server.IniPath, _itemMgr._MkDate);
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("プログラム起動：{0} 引数：{1}", ExeName, Argument), 3);
            int Rtn = _itemMgr.RunProcess(ExeName, Argument);
            if (Rtn != 0)
            {
                string ErrMsg = string.Format("実行処理でエラーが発生しました{0} 戻り値：{1}", ExeName, Rtn);
                ErrMsg += "\n日初処理異常終了（日初処理強制日付変更モードで日付を変更後に集配信履歴管理 取込エラーログ画面よりファイル退避を実行してください）" +
                          "\n※日初処理強制日付変更モードで日付を変更後に「ファイル退避」を実行しない場合は、誤った業務日付で後続処理が行われます";
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ErrMsg, 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ErrMsg, _itemMgr._TargetFilename, 3);
                return false;
            }

            // 業務日付再取得
            if (!AplInfo.GetGymSetting(AppInfo.Setting.GymId, AppInfo.Setting.SchemaBankCD))
            {
                string ErrMsg = "業務設定取得エラー ";
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ErrMsg, 3);
                LogWriterFileImport.writeLog(MethodBase.GetCurrentMethod(), ErrMsg, _itemMgr._TargetFilename, 3);
                return false;
            }

            return true;
        }

        #endregion

    }
}
