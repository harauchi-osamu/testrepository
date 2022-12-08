using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using NCR;

namespace CTROcImgArcMk
{
    /// <summary>
    /// 業務ロジッククラス
    /// </summary>
    public class Controller : ControllerBase
    {
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private EntryCommonFormBase _form = null;

        /// <summary>設定ファイル情報</summary>
        public SettingData SettingData { get; private set; } = new SettingData();
        public bool IsIniErr { get { return (!string.IsNullOrEmpty(this.SettingData.CheckParamMsg) || !this.SettingData.ChkServerIni); } }

        /// <summary>アーカイブフォルダ最大サイズ（MB）</summary>
        private int ARCHIVE_SIZE_MAX = 470 * 1024 * 1024;

        /// <summary>ユーザー情報ファイル</summary>
        private const string FILE_NAME_USERID = "USERID.txt";


        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// 管理クラスを設定する
        /// </summary>
        /// <param name="mst"></param>
        /// <param name="item"></param>
        public override void SetManager(MasterManager mst, ManagerBase item)
		{
			base.SetManager(mst, item);
			_masterMgr = MasterMgr;
			_itemMgr = (ItemManager)ItemMgr;
		}

        /// <summary>
        /// フォームを設定する
        /// </summary>
        /// <param name="form"></param>
        public void SetForm(EntryCommonFormBase form)
        {
            _form = form;
        }

		/// <summary>
		/// 引数を設定する
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public override bool SetArgs(string[] args)
        {
			string MenuNumber = args[0];
			base.MenuNumber = MenuNumber;

            return true;
        }

        /// <summary>
        /// Operator.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckOperatorIni()
        {
            SettingData.ChkParam(NCR.Operator.UserID, "ユーザーID");
            SettingData.ChkParam(NCR.Operator.UserName, "ユーザー名");
            SettingData.ChkParam(NCR.Operator.BankCD, "銀行コード");
            return true;
        }

        /// <summary>
        /// Term.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckTermIni()
        {
            SettingData.ChkParam(NCR.Terminal.Number, "端末番号");
            SettingData.ChkParam(NCR.Terminal.ServeriniPath, "CtrServer.iniパス");
            return true;
        }

        /// <summary>
        /// Server.ini 設定チェック
        /// </summary>
        /// <returns></returns>
        protected override bool CheckServerIni()
        {
            // ServerIniファイル存在チェック
            if (!SettingData.ServerIniExists())
            {
                return false;
            }

            SettingData.ChkParam(NCR.Server.IOSendRoot, "IO配信フォルダ(銀行別)");
            SettingData.ChkParam(NCR.Server.BankCheckImageRoot, "持帰ダウンロード確定前イメージルート(銀行別)");
            SettingData.ChkParam(NCR.Server.BankNormalImageRoot, "通常バッチルート情報(銀行別)");
            SettingData.ChkParam(NCR.Server.BankFutaiImageRoot, "付帯バッチルート情報(銀行別)");
            SettingData.ChkParam(NCR.Server.BankInventoryImageRoot, "期日管理バッチルート情報(銀行別)");
            SettingData.ChkParam(NCR.Server.BankConfirmImageRoot, "持帰ダウンロード確定イメージルート(銀行別)");
            SettingData.ChkParam(NCR.Server.ScanImageICOCRRoot, "持帰OCR取込用ルートフォルダ");
            SettingData.ChkParam(NCR.Server.ICOCRCopyUnitCount, "持帰OCRコピー件数単位");
            // OCRオプション導入はZeroOK

            return true;
        }

        /// <summary>
        /// exe.config 設定チェック
        /// </summary>
        /// <returns></returns>
        public override bool CheckAppConfig()
        {
            GetAppSettingsInt("UniqueCodeSleepTime", "スリープタイム（ミリ秒）");
            GetAppSettingsInt("MaxArchiveFileSize", "アーカイブファイルサイズ（MB）");
            return true;
        }

        /// <summary>
        /// configの取得
        /// </summary>
        private int GetAppSettingsInt(string Key, string ItemName, bool EmptyChk = true)
        {
            int iWork = -1;

            try
            {
                string sKeyData = ConfigurationManager.AppSettings[Key];
                if (string.IsNullOrWhiteSpace(sKeyData) && !EmptyChk)
                {
                    sKeyData = "0";
                }
                if (!int.TryParse(sKeyData, out iWork))
                {
                    throw new Exception("Error");
                }
            }
            catch
            {
                SetCheckParamMsg(ItemName);
            }

            return iWork;
        }

        /// <summary>
        /// 結果メッセージへの設定
        /// </summary>
        private void SetCheckParamMsg(string Msg)
        {
            if (string.IsNullOrEmpty(this.SettingData.CheckParamMsg))
            {
                this.SettingData.CheckParamMsg = Msg;
            }
            else
            {
                this.SettingData.CheckParamMsg += "," + Msg;
            }
        }

        /// <summary>
        /// 証券イメージアーカイブ作成処理
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public bool MakeArchive(EntryCommonFormBase form)
        {
            _form = form;
            _itemMgr.MeisaiList1 = new SortedDictionary<string, ItemManager.MeisaiInfos>();
            _itemMgr.MeisaiList2 = new SortedDictionary<string, ItemManager.MeisaiInfos>();
            _itemMgr.ArchiveList = new SortedDictionary<int, ItemManager.ArchiveInfos>();
            ARCHIVE_SIZE_MAX = AppConfig.MaxArchiveFileSize * 1024 * 1024;

            // UPDATE 実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction non = new AdoNonCommitTransaction(dbp))
            {
                try
                {
                    // 一時テーブル削除＆作成
                    _itemMgr.DropTmpTable(dbp, non);
                    _itemMgr.CreateTmpTable(dbp, non);

                    // アーカイブファイル作成
                    if (!DoArchive(dbp, non))
                    {
                        // ロールバック
                        RollbackTerminate(dbp, non);
                        return false;
                    }

                    // 行内交換連携
                    if (!DoExchange(dbp, non))
                    {
                        // ロールバック
                        RollbackTerminate(dbp, non);
                        return false;
                    }

                    // 一時テーブル削除
                    _itemMgr.DropTmpTable(dbp, non);
                    
                    // コミット
                    CommitTerminate(dbp, non);
                }
                catch (Exception ex)
                {
                    // 取得した行ロックを解除するためロールバック
                    // メッセージボックス表示前に実施
                    RollbackTerminate(dbp, non);
                    // メッセージ表示
                    if (ex.Message.IndexOf(Const.ORACLE_ERR_LOCK) != -1)
                    {
                        // ロック中
                        ComMessageMgr.MessageWarning(ComMessageMgr.E01003);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                        _form.SetStatusMessage(ComMessageMgr.E01003);
                    }
                    else
                    {
                        ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                        _form.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                    }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// アーカイブを作成する
        /// </summary>
        /// <returns></returns>
        public bool DoArchive(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // 行内交換連携実施判定
            ItemManager.ArchiveType datatype;
            if (ServerIni.Setting.InternalExchange)
            {
                // 行内交換する場合は他行データのみアーカイブ作成する
                datatype = ItemManager.ArchiveType.他行データ;
            }
            else
            {
                // 行内交換しない場合は全データをアーカイブ作成する
                datatype = ItemManager.ArchiveType.全データ;
            }

            // 作成対象データ取得（他行）
            if (!_itemMgr.FetchTrMei(datatype, _form))
            {
                // 異常終了
                return false;
            }

            // 作成対象データ取得（自行）
            if (ServerIni.Setting.InternalExchange)
            {
                if (!_itemMgr.FetchTrMei(ItemManager.ArchiveType.自行データ, _form))
                {
                    // 異常終了
                    return false;
                }
            }

            // 件数チェック
            SortedDictionary<int, ItemManager.CreateData> dataList1 = _itemMgr.CalcCreateDate(_itemMgr.MeisaiList1);
            SortedDictionary<int, ItemManager.CreateData> dataList2 = _itemMgr.CalcCreateDate(_itemMgr.MeisaiList2);
            int eltMeiCnt = dataList1.Values.Sum(p => p.MeisaiCount);
            int eltImgCnt = dataList1.Values.Sum(p => p.ImageCount);
            int ownMeiCnt = dataList2.Values.Sum(p => p.MeisaiCount);
            int ownImgCnt = dataList2.Values.Sum(p => p.ImageCount);
            int meiTotal = eltMeiCnt + ownMeiCnt;
            int imgTotal = eltImgCnt + ownImgCnt;
            if ((meiTotal != _itemMgr.DispParams.YoteiMeiCnt) || (imgTotal != _itemMgr.DispParams.YoteiImgCnt))
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("予定件数相違 電子交換 明細={0}件、イメージ={1}件", eltMeiCnt, eltImgCnt), 1);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("予定件数相違 行内交換 明細={0}件、イメージ={1}件", ownMeiCnt, ownImgCnt), 1);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("予定件数相違 合計 明細={0}件、イメージ={1}件", meiTotal, imgTotal), 1);

                string msg = "予定件数（合計）に変更がありました。\n内容を確認し再度実行してください。";
                ComMessageMgr.MessageWarning(msg);
                _form.SetStatusMessage(msg, System.Drawing.Color.Transparent);
                return false;
            }
            if (meiTotal < 1)
            {
                string msg = "作成対象データがありません。";
                ComMessageMgr.MessageInformation(msg);
                _form.SetStatusMessage(msg, System.Drawing.Color.Transparent);
                return false;
            }

            //編集中チェック
            if(_itemMgr.MeisaiList1.Where(x => x.Value.trmei.m_EDIT_FLG == 1).Count() > 0)
            {
                // 編集中の明細データがある場合はエラー終了
                string msg = "編集中の明細が存在するため処理を行えません。\nしばらく待って再度実行してください。";
                ComMessageMgr.MessageWarning(msg);
                _form.SetStatusMessage(msg, System.Drawing.Color.Transparent);
                return false;
            }
            // 作成対象データ取得（自行）
            if (ServerIni.Setting.InternalExchange)
            {
                if (_itemMgr.MeisaiList2.Where(x => x.Value.trmei.m_EDIT_FLG == 1).Count() > 0)
                {
                    // 編集中の明細データがある場合はエラー終了
                    string msg = "編集中の明細が存在するため処理を行えません。\nしばらく待って再度実行してください。";
                    ComMessageMgr.MessageWarning(msg);
                    _form.SetStatusMessage(msg, System.Drawing.Color.Transparent);
                    return false;
                }
            }

            // 確認メッセージ
            DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, "証券イメージアーカイブ作成処理を開始します。\nよろしいですか？");
            if (res == DialogResult.Cancel)
            {
                return false;
            }

            // レコードロック
            if (!_itemMgr.LockTables(datatype, dbp, non))
            {
                // データなし（正常終了）
                return true;
            }

            // イメージファイル名生成
            if (!CreateImageFileNames(_itemMgr.MeisaiList1, dbp, non))
            {
                // 他ユーザー更新中（異常終了）
                return false;
            }

            // アーカイブファイル名生成
            CreateArchiveFileNames1(dbp, non);

            // ファイル移動処理は最後にまとめて実施する
            {
                // イメージファイルをリネーム
                RenameImageFiles(_itemMgr.MeisaiList1, dbp, non);

                // アーカイブファイル作成
                CreateArchiveFiles1(dbp, non);
            }

            // ＤＢ更新
            _itemMgr.RegistImageArchives(dbp, non);

            return true;
        }

        /// <summary>
        /// 行内交換連携を実施する
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        /// <returns></returns>
        public bool DoExchange(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // 作成対象データ取得
            ItemManager.ArchiveType datatype;
            if (ServerIni.Setting.InternalExchange)
            {
                // 行内交換する場合は自行データのみデータ更新する
                datatype = ItemManager.ArchiveType.自行データ;
            }
            else
            {
                // 行内交換しない場合は処理しない（正常終了）
                return true;
            }

            // 作成対象データ取得
            if (!_itemMgr.FetchTrMei(datatype, _form))
            {
                // 異常終了
                return false;
            }

            // レコードロック
            if (!_itemMgr.LockTables(datatype, dbp, non))
            {
                // データなし（正常終了）
                return true;
            }

            // イメージファイル名生成
            if (!CreateImageFileNames(_itemMgr.MeisaiList2, dbp, non))
            {
                // 他ユーザー更新中（異常終了）
                return false;
            }

            // ファイル一連番号採番
            int fileSeq = ImportFileAccess.GetFileSeq(FileParam.FileId.IF101, FileParam.FileKbn.BUB, 1, dbp, non);
            // 持帰要求結果テキストファイル
            FileGenerator if207 = new FileGenerator(fileSeq, FileParam.FileId.IF207, FileParam.FileKbn.GDA, Operator.BankCD, ".txt");
            // 証券明細テキストファイル
            FileGenerator if201 = new FileGenerator(fileSeq, FileParam.FileId.IF201, FileParam.FileKbn.GDA, Operator.BankCD, ".txt");
            // アーカイブファイル
            FileGenerator if101 = new FileGenerator(fileSeq, FileParam.FileId.IF101, FileParam.FileKbn.BUB, Operator.BankCD, ".tar");

            // アーカイブファイル名設定
            CreateArchiveFileNames2(if101, dbp, non);

            // ファイル移動処理は最後にまとめて実施する
            {
                // イメージファイルをリネーム
                RenameImageFiles(_itemMgr.MeisaiList2, dbp, non);

                // アーカイブファイル作成
                if(!CreateArchiveFiles2(if101, dbp, non))
                {
                    // 異常終了
                    return false;
                }
            }

            // ＤＢ更新
            _itemMgr.RegistInternalExchanges(if207, if201, if101, dbp, non);

            return true;
        }

        /// <summary>
        /// アップロード用のイメージファイル名を生成する
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        /// <returns></returns>
        private bool CreateImageFileNames(SortedDictionary<string, ItemManager.MeisaiInfos> meisaiList, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            try
            {
                // 明細件数（ファイル名生成）
                foreach (ItemManager.MeisaiInfos meiinfo in meisaiList.Values)
                {
                    // イメージ件数(IMG_KBN順に処理「表は優先」)
                    foreach (ItemManager.ImageInfos imginfo in meiinfo.images.Values.OrderBy(x => x.trimg._IMG_KBN))
                    {
                        TBL_TRMEIIMG img = imginfo.trimg;
                        if ((img.m_BUA_STS != TrMei.Sts.未作成) &&
                            (img.m_BUA_STS != TrMei.Sts.再作成対象))
                        {
                            string msg = "他のユーザーによって更新された可能性があります。画面を更新して再度実行してください。";
                            ComMessageMgr.MessageWarning(msg);
                            _form.SetStatusMessage(msg, System.Drawing.Color.Transparent);
                            return false;
                        }

                        // イメージファイル名生成
                        CreateFileName(meiinfo, imginfo);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                throw new Exception("イメージファイル名の生成に失敗しました。");
            }
            return true;
        }

        /// <summary>
        /// アップロード用のイメージファイル名を生成する
        /// </summary>
        /// <returns></returns>
        /// <remarks>表イメージを優先して処理されていることが前提</remarks>
        private bool CreateFileName(ItemManager.MeisaiInfos mei, ItemManager.ImageInfos imginfo)
        {
            TBL_TRMEIIMG img = imginfo.trimg;

            if (imginfo.trimg._IMG_KBN == TrMeiImg.ImgKbn.表)
            {
                // 表イメージの場合、補正値からファイルデータ設定

                // 項目トランザクション設定
                img.m_OC_OC_DATE = AplInfo.OpDate().ToString("D8"); //持出日
                img.m_OC_IC_BK_NO = GetFileItemName(mei.tritems[DspItem.ItemId.持帰銀行コード].m_END_DATA, 4); //持帰銀行
                img.m_OC_CLEARING_DATE = GetFileItemName(mei.tritems[DspItem.ItemId.入力交換希望日].m_END_DATA, 8); //交換希望日
                img.m_OC_AMOUNT = GetFileItemName(mei.tritems[DspItem.ItemId.金額].m_END_DATA, 12); //金額
                img.m_PAY_KBN = GetFileItemName(mei.tritems[DspItem.ItemId.決済フラグ].m_END_DATA, 1); //決済フラグ

                // 既に設定済みのはずだけど念のためパディングしとく
                img.m_OC_OC_BK_NO = CommonUtil.PadLeft(img.m_OC_OC_BK_NO, 4, "0");
                img.m_OC_OC_BR_NO = CommonUtil.PadLeft(img.m_OC_OC_BR_NO, 4, "0");
                img.m_UNIQUE_CODE = CommonUtil.PadLeft(img.m_UNIQUE_CODE, 15, "0");
            }
            else
            {
                // 表イメージ以外の場合、表イメージからファイルデータ設定
                TBL_TRMEIIMG Front = mei.fronttrimg;

                // 表面から設定
                img.m_OC_OC_DATE = Front.m_OC_OC_DATE; //持出日
                img.m_OC_IC_BK_NO = Front.m_OC_IC_BK_NO; //持帰銀行
                img.m_OC_CLEARING_DATE = Front.m_OC_CLEARING_DATE; //交換希望日
                img.m_OC_AMOUNT = Front.m_OC_AMOUNT; //金額
                img.m_PAY_KBN = Front.m_PAY_KBN; //決済フラグ
                img.m_OC_OC_BK_NO = Front.m_OC_OC_BK_NO;
                img.m_OC_OC_BR_NO = Front.m_OC_OC_BR_NO;
                img.m_UNIQUE_CODE = Front.m_UNIQUE_CODE;
            }

            // ファイル管理情報生成
            ImportFileAccess.FileCtl fctl = CreateFileControl(mei, img);

            // File.Exists() はえらい時間かかるのでやらない
            // 基本的にファイルがあるものとして処理し、存在しない場合はそのまま例外にする

            // ファイルサイズ取得
            string filePath = Path.Combine(mei.fileCtl.ImageDirPath, fctl.OldFileName);
            FileInfo file = new FileInfo(filePath);
            fctl.FileSize = file.Length;

            // ファイルサイズ集計
            mei.fileCtl.TotalFileSize += fctl.FileSize;
            mei.fileCtl.FileList.Add(fctl);

            // ファイル名
            img.m_IMG_FLNM = fctl.NewFileName;
            imginfo.NEW_IMG_FLNM = fctl.NewFileName;

            return true;
        }

        /// <summary>
        /// ファイル管理情報を生成する
        /// </summary>
        /// <param name="mei"></param>
        /// <param name="img"></param>
        private ImportFileAccess.FileCtl CreateFileControl(ItemManager.MeisaiInfos mei, TBL_TRMEIIMG img)
        {
            ImportFileAccess.FileCtl.ImgKbn kbn = ImportFileAccess.FileCtl.ImgKbn.Omote;
            switch (img._IMG_KBN)
            {
                case TrMeiImg.ImgKbn.表:
                    kbn = ImportFileAccess.FileCtl.ImgKbn.Omote;
                    break;
                case TrMeiImg.ImgKbn.裏:
                    kbn = ImportFileAccess.FileCtl.ImgKbn.Ura;
                    break;
                case TrMeiImg.ImgKbn.補箋:
                    kbn = ImportFileAccess.FileCtl.ImgKbn.Hosen;
                    break;
                case TrMeiImg.ImgKbn.付箋:
                    kbn = ImportFileAccess.FileCtl.ImgKbn.Fusen;
                    break;
                case TrMeiImg.ImgKbn.入金証明:
                    kbn = ImportFileAccess.FileCtl.ImgKbn.Nyukin;
                    break;
                case TrMeiImg.ImgKbn.表再送分:
                    kbn = ImportFileAccess.FileCtl.ImgKbn.SaiOmote;
                    break;
                case TrMeiImg.ImgKbn.裏再送分:
                    kbn = ImportFileAccess.FileCtl.ImgKbn.SaiUra;
                    break;
                case TrMeiImg.ImgKbn.その他1:
                    kbn = ImportFileAccess.FileCtl.ImgKbn.Other1;
                    break;
                case TrMeiImg.ImgKbn.その他2:
                    kbn = ImportFileAccess.FileCtl.ImgKbn.Other2;
                    break;
                case TrMeiImg.ImgKbn.その他3:
                    kbn = ImportFileAccess.FileCtl.ImgKbn.Other3;
                    break;
                case TrMeiImg.ImgKbn.予備1:
                    kbn = ImportFileAccess.FileCtl.ImgKbn.Yobi1;
                    break;
                case TrMeiImg.ImgKbn.予備2:
                    kbn = ImportFileAccess.FileCtl.ImgKbn.Yobi2;
                    break;
                case TrMeiImg.ImgKbn.予備3:
                    kbn = ImportFileAccess.FileCtl.ImgKbn.Yobi3;
                    break;
            }
            ImportFileAccess.FileCtl fctl = new ImportFileAccess.FileCtl(kbn, mei.images[img._IMG_KBN].OLD_IMG_FLNM);
            fctl.OC_BKNO = img.m_OC_OC_BK_NO;
            fctl.OC_BRNO = img.m_OC_OC_BR_NO;
            fctl.IC_BKNO = img.m_OC_IC_BK_NO;
            fctl.OC_DATE = img.m_OC_OC_DATE;
            fctl.CLEARING_DATE = img.m_OC_CLEARING_DATE;
            fctl.AMOUNT = img.m_OC_AMOUNT;
            fctl.PAY_KBN = img.m_PAY_KBN;
            fctl.UNIQUECODE = img.m_UNIQUE_CODE;
            fctl.IMG_KBN = CommonUtil.PadLeft(img._IMG_KBN.ToString(), 2, "0");
            fctl.EXTENSION = img.m_FILE_EXTENSION;
            fctl.NewFileName = fctl.GetRenameFileName();
            fctl.Rename = false;
            fctl.Move = false;
            return fctl;
        }

        /// <summary>
        /// アーカイブファイル名を生成する（他行）
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        /// <returns></returns>
        private bool CreateArchiveFileNames1(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            try
            {
                // 明細件数
                int seq = 1;
                foreach (ItemManager.MeisaiInfos meiinfo in _itemMgr.MeisaiList1.Values)
                {
                    // 自行データ抽出（アーカイブ対象外）
                    if (ServerIni.Setting.InternalExchange)
                    {
                        // 行内交換連携を実施する場合
                        if (DBConvert.ToIntNull(meiinfo.tritems[DspItem.ItemId.持帰銀行コード].m_END_DATA) == Operator.BankCD)
                        {
                            continue;
                        }
                    }

                    // 他行データ（アーカイブ対象）
                    ItemManager.ArchiveInfos archive = null;
                    var archives = _itemMgr.ArchiveList.Values.Where(p => !p.IsFileFull);
                    if (archives.Count() > 0)
                    {
                        archive = archives.First();
                        if ((archive.TotalFileSize + meiinfo.fileCtl.TotalFileSize) <= ARCHIVE_SIZE_MAX)
                        {
                            // 500MB未満だったらアーカイブに追加する
                            archive.MeisaiList.Add(meiinfo.Key, meiinfo);
                            archive.TotalFileSize += meiinfo.fileCtl.TotalFileSize;
                            continue;
                        }
                        // 500MB超えていたら終わり
                        archive.IsFileFull = true;
                    }

                    // 新しいアーカイブを生成する
                    ItemManager.ArchiveInfos newArchive = new ItemManager.ArchiveInfos();
                    newArchive.MeisaiList.Add(meiinfo.Key, meiinfo);
                    newArchive.TotalFileSize += meiinfo.fileCtl.TotalFileSize;
                    _itemMgr.ArchiveList.Add(seq, newArchive);
                    seq++;
                }

                // アーカイブフォルダ名生成
                int edSeq = ImportFileAccess.GetFileSeq(FileParam.FileId.IF101, FileParam.FileKbn.BUB, _itemMgr.ArchiveList.Count, dbp, non);
                int stSeq = edSeq - (_itemMgr.ArchiveList.Count - 1);
                seq = stSeq;
                foreach (ItemManager.ArchiveInfos archive in _itemMgr.ArchiveList.Values)
                {
                    archive.if101 = new FileGenerator(seq, FileParam.FileId.IF101, FileParam.FileKbn.BUB, Operator.BankCD, ".tar");

                    // 明細件数（アーカイブファイル名を設定）
                    foreach (ItemManager.MeisaiInfos meiinfo in archive.MeisaiList.Values)
                    {
                        // イメージ件数
                        foreach (ItemManager.ImageInfos imginfo in meiinfo.images.Values)
                        {
                            TBL_TRMEIIMG img = imginfo.trimg;
                            img.m_BUA_STS = TrMei.Sts.ファイル作成;
                            img.m_BUA_DATE = AplInfo.OpDate();
                            img.m_BUA_TIME = DBConvert.ToIntNull(DateTime.Now.ToString("HHmmss"));
                            img.m_IMG_ARCH_NAME = archive.if101.FileName;
                        }
                    }
                    seq++;
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                throw new Exception("アーカイブファイル名の生成に失敗しました。");
            }
            return true;
        }

        /// <summary>
        /// アーカイブファイル名を設定する（自行）
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        /// <returns></returns>
        private bool CreateArchiveFileNames2(FileGenerator if101, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            try
            {
                // 明細件数
                foreach (ItemManager.MeisaiInfos meiinfo in _itemMgr.MeisaiList2.Values)
                {
                    // イメージ件数
                    foreach (ItemManager.ImageInfos imginfo in meiinfo.images.Values)
                    {
                        TBL_TRMEIIMG img = imginfo.trimg;
                        img.m_BUA_STS = TrMei.Sts.結果正常;
                        img.m_BUA_DATE = AplInfo.OpDate();
                        img.m_BUB_CONFIRMDATE = AplInfo.OpDate();
                        img.m_BUA_TIME = DBConvert.ToIntNull(DateTime.Now.ToString("HHmmss"));
                        img.m_IMG_ARCH_NAME = if101.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                throw new Exception("アーカイブファイル名の生成に失敗しました。");
            }
            return true;
        }

        /// <summary>
        /// アーカイブファイルを生成する（他行）
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        /// <returns></returns>
        private bool CreateArchiveFiles1(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // アーカイブ件数
            foreach (ItemManager.ArchiveInfos archive in _itemMgr.ArchiveList.Values)
            {
                // アーカイブフォルダ作成
                string archivePath = Path.Combine(archive.if101.ArchiveOutputPath, archive.if101.ArchiveDirName);
                CommonUtil.DeleteDirectories(archive.if101.ArchiveOutputPath, true);
                Directory.CreateDirectory(archivePath);

                // ユーザー情報ファイル作成
                CreateUserIdText(archive, archivePath, dbp, non);

                // アーカイブファイル作成
                CreateArchiveFile(archive, archivePath);
            }
            return true;
        }

        /// <summary>
        /// アーカイブファイルを生成する（自行）
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        /// <returns></returns>
        private bool CreateArchiveFiles2(FileGenerator if101, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // アーカイブフォルダ作成
            string dstDirPath = Path.Combine(ServerIni.Setting.BankCheckImageRoot, if101.ArchiveDirName);
            CommonUtil.DeleteDirectories(if101.ArchiveOutputPath, true);
            Directory.CreateDirectory(dstDirPath);

            // フォルダパスを保持する
            _itemMgr.OwnBankInfo = new ItemManager.OwnBankInfos();
            _itemMgr.OwnBankInfo.ArchiveFilePath = dstDirPath;

            // 明細件数
            foreach (ItemManager.MeisaiInfos meisai in _itemMgr.MeisaiList2.Values)
            {
                // イメージファイル格納フォルダ
                string srcDirPath = meisai.fileCtl.ImageDirPath;

                // イメージ件数
                foreach (ItemManager.ImageInfos image in meisai.images.Values)
                {
                    // ファイルコピー
                    string srcFilePath = Path.Combine(srcDirPath, image.NEW_IMG_FLNM);
                    string dstFilePath = Path.Combine(dstDirPath, image.NEW_IMG_FLNM);

                    // 持帰ダウンロード確定前イメージルート(銀行別)
                    File.Copy(srcFilePath, dstFilePath, true);
                }
            }

            if (NCR.Server.OCROption == 1)
            {
                // OCROptionありの場合
                // 持帰OCR取込用ルートにファイルコピー
                if (!InclearingFileAccess.ImageICOCRCopy(if101.ArchiveDirName, dstDirPath, NCR.Server.ScanImageICOCRRoot, NCR.Server.ICOCRCopyUnitCount))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// イメージファイル名を変更する
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        /// <returns></returns>
        private bool RenameImageFiles(SortedDictionary<string, ItemManager.MeisaiInfos> meisaiList, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            try
            {
                // 端末IPアドレス
                string TermIPAddress = ImportFileAccess.GetTermIPAddress();

                // 明細件数（ファイル移動）
                foreach (ItemManager.MeisaiInfos meiinfo in meisaiList.Values)
                {
                    TBL_TRMEIIMG imgFirst = meiinfo.images.Values.First().trimg;
                    ImportFileAccess.DetailFileRenameArchive(
                        meiinfo.fileCtl.ImageDirPath,
                        TermIPAddress,
                        meiinfo.fileCtl,
                        imgFirst.m_OC_OC_BK_NO,
                        imgFirst.m_OC_OC_BR_NO,
                        imgFirst.m_OC_IC_BK_NO,
                        imgFirst.m_OC_OC_DATE,
                        imgFirst.m_OC_CLEARING_DATE,
                        imgFirst.m_OC_AMOUNT,
                        imgFirst.m_PAY_KBN,
                        AppConfig.UniqueCodeSleepTime,
                        dbp, non);
                }
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                throw new Exception("イメージファイル名のリネームに失敗しました。");
            }
            return true;
        }

        /// <summary>
        /// ユーザー情報ファイルを作成する
        /// </summary>
        /// <param name="archive"></param>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        /// <returns></returns>
        private bool CreateUserIdText(ItemManager.ArchiveInfos archive, string archivePath, AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            try
            {
                // ファイル名
                string filePath = Path.Combine(archivePath, FILE_NAME_USERID);
                TBL_CTRUSERINFO userinfo = _masterMgr.GetCtrUserInfo(dbp, non);
                string userId = CommonUtil.PadRight(userinfo._USERID, Const.CTRUSER_USERID_LEN, " ");
                string password = CommonUtil.PadRight(userinfo.m_PASSWORD, Const.CTRUSER_PASSWORD_LEN, " ");

                // データ
                StringBuilder sb = new StringBuilder();
                sb.Append(userId);
                sb.Append(password);
                sb.Append(FileGenerator.CRLF);
                FileGenerator.WriteAllTextStream(filePath, sb.ToString());
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                throw new Exception("ユーザー情報ファイルの作成に失敗しました。");
            }
            return true;
        }

        /// <summary>
        /// アーカイブファイルを作成する
        /// </summary>
        /// <param name="archive"></param>
        /// <returns></returns>
        private bool CreateArchiveFile(ItemManager.ArchiveInfos archive, string archivePath)
        {
            try
            {
                // アーカイブフォルダパス
                string dstDirPath = archivePath;

                // ユーザー情報ファイルをtarに含める
                List<string> cmpFileNameList = new List<string>();
                cmpFileNameList.Add(FILE_NAME_USERID);

                // イメージファイルをマイドキュメントに作成したアーカイブフォルダにコピーする
                foreach (ItemManager.MeisaiInfos meisai in archive.MeisaiList.Values)
                {
                    // イメージファイル格納フォルダ
                    string srcDirPath = meisai.fileCtl.ImageDirPath;
                    foreach (ItemManager.ImageInfos image in meisai.images.Values)
                    {
                        // ファイルコピー
                        string srcFilePath = Path.Combine(srcDirPath, image.NEW_IMG_FLNM);
                        string dstFilePath = Path.Combine(dstDirPath, image.NEW_IMG_FLNM);
                        File.Copy(srcFilePath, dstFilePath, true);

                        // アーカイブファイルをtarに含める
                        cmpFileNameList.Add(image.NEW_IMG_FLNM);
                    }
                }

                // tar圧縮する
                TarArchive.PackTarImg(archive.if101.ArchiveFilePath, dstDirPath, cmpFileNameList);

                // tarをHULFT送信フォルダにコピー
                string sendFilePath = Path.Combine(ServerIni.Setting.IOSendRoot, archive.if101.FileName);
                File.Copy(archive.if101.ArchiveFilePath, sendFilePath, true);

                // ファイルサイズ算出
                archive.if101.SetFileSize(sendFilePath);

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("ファイル保存=[{0}]", sendFilePath), 1);
            }
            catch (Exception ex)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                throw new Exception("アーカイブファイルの作成に失敗しました。");
            }
            return false;
        }

        /// <summary>
        /// アップロード用の項目名を生成する
        /// </summary>
        /// <returns></returns>
        private string GetFileItemName(string EndData, int Length)
        {
            if (string.IsNullOrEmpty(EndData))
            {
                // 補正未実施の場合
                return new string('Z', Length);
            }
            return DBConvert.ToLongNull(EndData).ToString("D" + Length.ToString());
        }

        /// <summary>
        /// ファイル名を元に戻す
        /// </summary>
        private void RenameImageErrBack(SortedDictionary<string, ItemManager.MeisaiInfos> meisaiList)
        {
            // 明細件数（ファイル移動）
            foreach (ItemManager.MeisaiInfos meiinfo in meisaiList.Values)
            {
                ImportFileAccess.RenameImageErrBack(meiinfo.fileCtl);
            }
        }

        /// <summary>
        /// ロールバックと後処理を行う（異常終了時）
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        private void RollbackTerminate(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // ロールバック前にテーブル削除すると、先にコミットされてしまうので注意

            // ロールバック
            non.Trans.Rollback();
            // 一時テーブル削除
            _itemMgr.DropTmpTable(dbp, non);
            // ファイル名を元に戻す（他行）
            RenameImageErrBack(_itemMgr.MeisaiList1);
            // ファイル名を元に戻す（自行）
            RenameImageErrBack(_itemMgr.MeisaiList2);

            // アーカイブフォルダ（他行）
            foreach (ItemManager.ArchiveInfos archive in _itemMgr.ArchiveList.Values)
            {
                // アーカイブフォルダ削除（マイドキュメント）
                CommonUtil.DeleteDirectories(archive.if101.ArchiveOutputPath, true);

                // アーカイブファイル削除（HULFT送信フォルダ）
                string sendFilePath = Path.Combine(ServerIni.Setting.IOSendRoot, archive.if101.FileName);
                CommonUtil.DeleteFile(sendFilePath);
            }

            // アーカイブフォルダ（自行）
            // アーカイブフォルダ削除（持帰ダウンロード確定前イメージルート）
            CommonUtil.DeleteDirectories(_itemMgr.OwnBankInfo.ArchiveFilePath, true);
        }

        /// <summary>
        /// コミットと後処理を行う（正常終了時）
        /// </summary>
        /// <param name="dbp"></param>
        /// <param name="non"></param>
        private void CommitTerminate(AdoDatabaseProvider dbp, AdoNonCommitTransaction non)
        {
            // コミット
            non.Trans.Commit();

            // 一時テーブル削除
            _itemMgr.DropTmpTable(dbp, non);

            // アーカイブフォルダ（他行）
            foreach (ItemManager.ArchiveInfos archive in _itemMgr.ArchiveList.Values)
            {
                // アーカイブフォルダ削除（マイドキュメント）
                CommonUtil.DeleteDirectories(archive.if101.ArchiveOutputPath, true);
            }
        }
    }
}
