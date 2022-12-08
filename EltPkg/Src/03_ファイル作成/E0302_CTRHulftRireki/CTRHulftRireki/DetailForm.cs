using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using HulftIO;

namespace CTRHulftRireki
{
    /// <summary>
    /// 取込エラーログ
    /// </summary>
    public partial class DetailForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private const int F1_ = 1;
        private const int F2_ = 2;
        private const int F3_ = 3;
        private const int F4_ = 4;
        private const int F5_ = 5;
        private const int F6_ = 6;
        private const int F7_ = 7;
        private const int F8_ = 8;
        private const int F9_ = 9;
        private const int F10_ = 10;
        private const int F11_ = 11;
        private const int F12_ = 12;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DetailForm()
        {
            InitializeComponent();
        }


        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// フォームを初期化する
        /// </summary>
        public override void InitializeForm(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;

            // コントロール初期化
            InitializeControl();

            headerControl.InitializeForm(_ctl, this);
            base.InitializeForm(ctl);
            InitializeControl();
        }


        // *******************************************************************
        // 継承メソッド
        // *******************************************************************

        /// <summary>
        /// 業務名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName1(string dispName)
        {
            base.SetDispName1("集配信履歴照会");
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("取込エラーログ");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            // 通常状態
            SetFunctionName(F1_, "取込画面", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F2_, string.Empty);
            SetFunctionName(F3_, string.Empty);
            SetFunctionName(F4_, string.Empty);
            SetFunctionName(F5_, "ファイル\n退避", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F6_, string.Empty);
            SetFunctionName(F7_, string.Empty);
            SetFunctionName(F8_, string.Empty);
            SetFunctionName(F9_, string.Empty);
            SetFunctionName(F10_, string.Empty);
            SetFunctionName(F11_, string.Empty);
            SetFunctionName(F12_, string.Empty);
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
            bool isRecv = (_itemMgr.DispParams.ProcType == ItemManager.ProcTypes.集信);
            SetFunctionState(F5_, isRecv);
        }

        /// <summary>
        /// フォームを再描画する
        /// </summary>
        public override void ResetForm()
        {
            // 画面表示データ更新
            InitializeDisplayData();

            // 画面表示データ更新
            RefreshDisplayData();

            // 画面表示状態更新
            RefreshDisplayState();
        }

        /// <summary>
        /// フォームを更新する
        /// </summary>
        public void RefreshForm()
        {
        }

        /// <summary>
        /// コントロール初期化
        /// </summary>
        protected void InitializeControl()
        {
            if (_itemMgr.DispParams.ProcType == ItemManager.ProcTypes.配信)
            {
                lblTitle.Text = "エラーログ（配信）";
            }
            else if (_itemMgr.DispParams.ProcType == ItemManager.ProcTypes.集信)
            {
                lblTitle.Text = "エラーログ（集信）";
            }
        }

        /// <summary>
        /// 画面表示データ初期化
        /// </summary>
        protected void InitializeDisplayData()
        {
        }

        /// <summary>
        /// 画面表示データ更新
        /// </summary>
        protected override void RefreshDisplayData()
        {
            // 画面項目設定
            SetDisplayParams();
        }

        /// <summary>
        /// 画面表示状態更新
        /// </summary>
        protected override void RefreshDisplayState()
        {
            // ファンクションキー状態を設定
            SetFunctionState();
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
            HulftLog.RecordInfo rec = _itemMgr.DispParams.LogRecord;

            // ログ取得
            if (_itemMgr.DispParams.ProcType == ItemManager.ProcTypes.配信)
            {
                // 配信の場合は取込ファイル名でログを探す
                FileGenerator capFile = new FileGenerator(rec.fctl._CAP_FILE_NAME);
                string fileKey = capFile.FileId + capFile.FileDivid;
                string logFileName = string.Format("{0}_{1}_E.log", DateTime.Now.ToString("yyyyMMdd"), fileKey);
                string logFilePath = Path.Combine(ServerIni.Setting.HulftEXELogRoot, DateTime.Now.ToString("yyyyMMdd"), logFileName);
                txtLog.Text = ReadErrRecords(logFilePath, capFile.FileName);
            }
            else if (_itemMgr.DispParams.ProcType == ItemManager.ProcTypes.集信)
            {
                string fileKey = rec.FGen.FileId + rec.FGen.FileDivid;
                string logFileName = string.Format("{0}_{1}_E.log", DateTime.Now.ToString("yyyyMMdd"), fileKey);
                string logFilePath = Path.Combine(ServerIni.Setting.HulftEXELogRoot, DateTime.Now.ToString("yyyyMMdd"), logFileName);
                txtLog.Text = ReadErrRecords(logFilePath, rec.FGen.FileName);
            }
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            return true;
        }


        // *******************************************************************
        // イベント
        // *******************************************************************


        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        /// <summary>
        /// F1：終了
        /// </summary>
        protected override void btnFunc01_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 終了
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了", 1);

                this.Close();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F5：ファイル退避
        /// </summary>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                // ファイル退避
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "ファイル退避", 1);

                // ファイル移動
                if (!MoveFiles())
                {
                    return;
                }

                // 完了メッセージ
                ComMessageMgr.MessageInformation(ComMessageMgr.I00001, "ファイルの退避");
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }


        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// ファイル退避
        /// </summary>
        /// <returns></returns>
        private bool MoveFiles()
        {
            try
            {
                HulftLog.RecordInfo rec = _itemMgr.DispParams.LogRecord;
                string srcFilePath = Path.Combine(ServerIni.Setting.RecvRoot, rec.FGen.FileName);
                string dstFilePath = Path.Combine(ServerIni.Setting.BackupRoot, rec.FGen.FileName);
                if (!File.Exists(srcFilePath))
                {
                    string msg = "退避ファイルが見つかりませんでした。";
                    ComMessageMgr.MessageWarning(msg);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("{0} file=[{1}]", msg, srcFilePath), 3);
                    return false;
                }

                // 確認メッセージ
                DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, "ファイル退避を開始します。\nよろしいですか？");
                if (res == DialogResult.Cancel)
                {
                    return false;
                }

                // バックアップディレクトリ
                if (!Directory.Exists(ServerIni.Setting.BackupRoot))
                {
                    Directory.CreateDirectory(ServerIni.Setting.BackupRoot);
                }

                using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
                using (AdoNonCommitTransaction non = new AdoNonCommitTransaction(dbp))
                {
                    // ステータス更新
                    _itemMgr.UpdateFileCtlFileMove(rec, dbp, non);

                    // ファイル退避
                    File.Move(srcFilePath, dstFilePath);
                    // コミット
                    non.Trans.Commit();
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError("ファイル退避に失敗しました。");
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取込エラーログを読み込む
        /// </summary>
        /// <param name="logFilePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string ReadErrRecords(string logFilePath, string fileName)
        {
            if (!File.Exists(logFilePath))
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("取込エラーログが見つかりませんでした。[{0}]", logFilePath), 1);
                return "";
            }

            string[] logs = CommonUtil.ReadTextStream(logFilePath, Encoding.UTF8);
            var lines = logs.Where(line => (line.IndexOf(fileName) != -1));
            if (lines.Count() < 1)
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("取込エラーログに該当行が見つかりませんでした。file=[{0}], name=[{1}]", logFilePath, fileName), 1);
                return "";
            }
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("取込エラーログ file=[{0}], name=[{1}], cnt=[{2}]", logFilePath, fileName, lines.Count()), 1);

            StringBuilder sb = new StringBuilder();
            foreach (string line in lines)
            {
                sb.AppendLine(line);
            }
            return sb.ToString();
        }

    }
}
