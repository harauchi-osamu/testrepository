using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using System.Reflection;
using System.Linq;
using System.IO;
using NCR;

namespace StartDayOperation
{
    /// <summary>
    /// 更新処理クラス
    /// </summary>
    public class StartDayUpdate
    {
        #region クラス変数

        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        #endregion

        /// <summary>
        /// 初期化
        /// </summary>
        public StartDayUpdate(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;
        }

        #region 日初処理

        /// <summary>
        /// 日初処理
        /// </summary>
        public bool StartProc()
        {
            try
            {
                //iBicsCalendar cal = new iBicsCalendar();
                //cal.SetHolidays();

                //日初処理
                using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
                using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
                {
                    //業務日付更新
                    if (!_itemMgr.UpdateGymDate(dbp, Tran))
                    {
                        return false;
                    }

                    // DBデータ削除処理
                    foreach (SettingData.ConfDBData data in _ctl.SettingData.DBData.Where(x => x.TBLType == 0).OrderBy(x => x.Order))
                    {
                        // 共通スキーマの処理

                        // 基準日付の算出
                        //int BaseDate = cal.getBusinessday(_itemMgr.DispParams.UpdateGymDate, data.DiffDate);
                        int BaseDate = GetDBDeleteBaseDate(data.DiffDate);

                        //削除処理
                        string Schema = DBConvert.TABLE_SCHEMA_DBCTR;
                        int Ret = _itemMgr.DeleteTRData(data.SQL, Schema, BaseDate, dbp, Tran);
                        if (Ret < 0)
                        {
                            return false;
                        }
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("データ削除 {0}.{1} : {2}", Schema, data.TBLName, Ret), 3);
                    }

                    foreach (string BankCd in NCR.Server.HandlingBankCdList.Split(','))
                    {
                        foreach (SettingData.ConfDBData data in _ctl.SettingData.DBData.Where(x => x.TBLType == 1).OrderBy(x => x.Order))
                        {
                            // 銀行別スキーマの処理

                            // 基準日付の算出
                            //int BaseDate = cal.getBusinessday(_itemMgr.DispParams.UpdateGymDate, data.DiffDate);
                            int BaseDate = GetDBDeleteBaseDate(data.DiffDate);

                            //削除処理
                            string Schema = string.Format(DBConvert.TABLE_SCHEMA_DBCTR_BANK, BankCd);
                            int Ret = _itemMgr.DeleteTRData(data.SQL, Schema, BaseDate, dbp, Tran);
                            if (Ret < 0)
                            {
                                return false;
                            }
                            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("データ削除 {0}.{1} : {2}", Schema, data.TBLName, Ret), 3);
                        }
                    }

                    // ファイルデータ削除処理
                    foreach (SettingData.ConfFileParam data in _ctl.SettingData.FileParam)
                    {
                        //削除基準日取得
                        DateTime DeleteBaseDate = GetDeleteBaseDate(data.DiffDate);
                        //ルートフォルダ取得
                        string RootFolderPath = GetDeleteRootPath(data.ProcessType, data.iniSectionName, data.iniKeyName, data.FolderPath);

                        switch (data.ProcessType)
                        {
                            case 1:
                                //銀行別
                                foreach (string BankCd in NCR.Server.HandlingBankCdList.Split(','))
                                {
                                    //ファイル削除
                                    FileDeleteProc(string.Format(RootFolderPath, BankCd), DeleteBaseDate, false);
                                }

                                break;
                            case 0:
                            case 2:
                                //その他

                                //ファイル削除
                                FileDeleteProc(RootFolderPath, DeleteBaseDate, false);
                                break;
                        }
                    }

                    // イメージファイル削除
                    if (_ctl.SettingData.UnMatchTRImgDelete)
                    {
                        // イメージファイル削除設定あり
                        foreach (string BankCd in NCR.Server.HandlingBankCdList.Split(','))
                        {
                            // 銀行毎に削除
                            ImgFileDelete(BankCd, dbp, Tran);
                        }
                    }

                    //コミット
                    Tran.Trans.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
        }

        #endregion

        #region DB削除関連

        /// <summary>
        /// DBデータ削除基準日算出
        /// </summary>
        private int GetDBDeleteBaseDate(int DiffDate)
        {
            int yyyymmdd = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int diffdays = Math.Abs(DiffDate);
            bool isAdd = true;
            if (DiffDate < 0)
            {
                isAdd = false;
            }

            return CommonUtil.GetAddDate(yyyymmdd, diffdays, isAdd);
        }

        #endregion

        #region ファイル削除関連

        /// <summary>
        /// ファイル削除処理
        /// </summary>
        private void FileDeleteProc(string FolderPath, DateTime DeleteBaseDate, bool FolderDelete)
        {
            // サブフォルダの検索
            IEnumerable<string> Directories = Directory.EnumerateDirectories(FolderPath);
            foreach (string SubFolderPath in Directories)
            {
                // サブフォルダを再帰処理で削除処理実行
                FileDeleteProc(SubFolderPath, DeleteBaseDate, true);
            }

            // ファイルの検索
            int DeleteCount = 0;
            IEnumerable<string> Files = Directory.EnumerateFiles(FolderPath, "*.*");
            foreach (string FilePath in Files)
            {
                // ファイル更新日が削除基準日以下の場合、削除実施
                if (DeleteBaseDate >= File.GetLastWriteTime(FilePath))
                {
                    CommonUtil.DeleteFile(FilePath);
                    DeleteCount++;
                }
            }
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル削除 フォルダ:{0} 削除基準日:{1} 削除件数:{2}", FolderPath, DeleteBaseDate.ToString("yyyyMMdd"), DeleteCount), 3);

            if (FolderDelete)
            {
                // 空の場合フォルダ削除
                CommonUtil.DeleteEmptyDirectory(FolderPath);
                if (!Directory.Exists(FolderPath)) LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("フォルダ削除:{0}", FolderPath), 3);
            }
        }

        /// <summary>
        /// ファイル削除基準日算出
        /// </summary>
        private DateTime GetDeleteBaseDate(int DiffDate)
        {
            return DateTime.Now.Date.AddDays((DiffDate - 1) * -1).AddMilliseconds(-1);
        }

        /// <summary>
        /// ルートパス取得
        /// </summary>
        private string GetDeleteRootPath(int ProcessType, string iniSectionName, string iniKeyName, string FolderPath)
        {
            string RootPath = string.Empty;
            switch (ProcessType)
            {
                case 0:
                case 1:
                    // iniから取得
                    RootPath = IniFileAccess.GetKeyByString(Server.IniPath, iniSectionName, iniKeyName);
                    break;
                case 2:
                    // 指定のフォルダパス
                    RootPath = FolderPath;
                    break;
            }
            if (string.IsNullOrEmpty(RootPath))
            {
                throw new Exception("RootPath定義エラー");
            }

            return RootPath;
        }

        #endregion

        #region イメージファイル削除関連

        /// <summary>
        /// 対象銀行のイメージファイル削除処理
        /// </summary>
        private void ImgFileDelete(string sBankCd, AdoDatabaseProvider dbp, AdoNonCommitTransaction Tran)
        {
            if(!int.TryParse(sBankCd, out int iBankCd))
            {
                throw new Exception("銀行コード定義エラー");
            }

            // イメージデータ一覧を取得
            if (!_itemMgr.GetImgList(iBankCd, out List<ItemManager.ImgFileData> ImgList, dbp, Tran))
            {
                throw new Exception("イメージデータ取得エラー");
            }

            // 持出 : 通常バッチイメージファイル削除処理
            ImgFileDeleteRoot(_itemMgr.BankNormalImageRoot(iBankCd), GymParam.GymId.持出, ImgList, true);

            // 持出 : 付帯バッチイメージファイル削除処理
            ImgFileDeleteRoot(_itemMgr.BankFutaiImageRoot(iBankCd), GymParam.GymId.持出, ImgList, true);

            // 持出 : 期日管理バッチイメージファイル削除処理
            ImgFileDeleteRoot(_itemMgr.BankInventoryImageRoot(iBankCd), GymParam.GymId.持出, ImgList, true);

            // 持帰 : 持帰ダウンロード確定バッチイメージファイル削除処理
            ImgFileDeleteRoot(_itemMgr.BankConfirmImageRoot(iBankCd), GymParam.GymId.持帰, ImgList, true);
        }

        /// <summary>
        /// イメージファイル削除処理
        /// ルートフォルダ
        /// </summary>
        private void ImgFileDeleteRoot(string RootFolderPath, int GymID, List<ItemManager.ImgFileData> ImgList, bool FolderDelete)
        {
            if (!Directory.Exists(RootFolderPath))
            {
                // ないとは思うがルートフォルダがない場合
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("イメージファイル削除ルートフォルダなし フォルダ:{0} ", RootFolderPath), 1);
                return;
            }

            // ルート配下のバッチフォルダの検索
            IEnumerable<string> Directories = Directory.EnumerateDirectories(RootFolderPath);
            foreach (string BatFolderPath in Directories)
            {
                // バッチフォルダ配下のイメージファイルをテーブルデータと突き合わせて削除
                ImgFileDeleteProc(BatFolderPath, GymID, ImgList, FolderDelete);
            }
        }

        /// <summary>
        /// イメージファイル削除処理
        /// バッチフォルダ
        /// </summary>
        private void ImgFileDeleteProc(string FolderPath, int GymID, List<ItemManager.ImgFileData> ImgList, bool FolderDelete)
        {
            // 対象バッチフォルダのキー情報取得
            _itemMgr.GetBatFolderKey(FolderPath, out int FolderGymID, out int FolderOpeDate, out int FolderBatID);
            if (GymID != FolderGymID)
            {
                // 引数の業務IDとフォルダーの業務IDが違う場合、すべて削除対象とする
                FolderGymID = -99;
            }

            // 対象フォルダのjpgファイル一覧取得
            IEnumerable<string> FileList = Directory.EnumerateFiles(FolderPath, "*.jpg").Select(name => Path.GetFileName(name));

            // テーブルに格納しているイメージファイル一覧を算出(バッチフォルダが同じ)
            IEnumerable<string> ImgDatas = ImgList.Where(x => x.GymID == FolderGymID && x.OpeDate == FolderOpeDate && x.BatID == FolderBatID).Select(x => x.ImgFLNM);

            // 対象ファイル一覧からテーブルにないファイルを差集合で算出
            List<string> DeleteFileList = FileList.Except<string>(ImgDatas).ToList();

            // ファイル削除
            foreach (string DeleteFile in DeleteFileList)
            {
                CommonUtil.DeleteFile(Path.Combine(FolderPath, DeleteFile));
            }
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("イメージファイル削除 フォルダ:{0} 削除件数:{1}", FolderPath, DeleteFileList.Count()), 1);

            if (FolderDelete)
            {
                // 空の場合フォルダ削除
                CommonUtil.DeleteEmptyDirectory(FolderPath);
                if (!Directory.Exists(FolderPath)) LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("フォルダ削除:{0}", FolderPath), 3);
            }
        }

        #endregion

    }
}
