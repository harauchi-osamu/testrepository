using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using HulftIO;

namespace CTRHulftRireki
{
    /// <summary>
    /// ヘッダーコントロール
    /// </summary>
    public partial class HeaderControl : UserControl
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private SendRecvListForm _parent1 = null;
        private ImportListForm _parent2 = null;
        private DetailForm _parent3 = null;

        ///// <summary>画面リフレッシュ時間間隔[秒]</summary>
        //private const int MONITOR_INTERVAL = 60 * 1000;

        /// <summary>
        /// 画面リフレッシュ時間間隔[ミリ秒]
        /// </summary>
        private int MONITOR_INTERVAL
        {
            get
            {
                if (_ctl == null || _ctl.SettingData.AutoRefreshInterval < 0) return 60 * 1000;
                return _ctl.SettingData.AutoRefreshInterval * 1000;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HeaderControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームを初期化する
        /// </summary>
        public void InitializeForm(ControllerBase ctl, SendRecvListForm form)
        {
            _parent1 = form;
            InitializeForm(ctl);
        }

        /// <summary>
        /// フォームを初期化する
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="form"></param>
        public void InitializeForm(ControllerBase ctl, ImportListForm form)
        {
            _parent2 = form;
            InitializeForm(ctl);
        }

        /// <summary>
        /// フォームを初期化する
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="form"></param>
        public void InitializeForm(ControllerBase ctl, DetailForm form)
        {
            _parent3 = form;
            InitializeForm(ctl);
        }

        /// <summary>
        /// フォームを初期化する
        /// </summary>
        private void InitializeForm(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;

            //タイマー設定
            this.timerUpdateList.Interval = MONITOR_INTERVAL;
            this.timerUpdateList.Tick += timerUpdateList_Tick;

            // 画面表示データ更新
            RefreshDisplayData();
        }

        /// <summary>
        /// 画面表示データ更新
        /// </summary>
        public void RefreshDisplayData()
        {
            if (_ctl.IsIniErr) { return; }

            // ログ情報は全画面共通なのでヘッダーで最新取得して保持する
            _itemMgr.Fetch_file_ctls();

            // 集配信ログ取得
            HulftLog hlog = new HulftLog(_ctl.SendLogFilePath, _ctl.RecvLogFilePath, _ctl.SendErrLogFilePath, _ctl.RecvErrLogFilePath);
            _itemMgr.DispParams.HulftLogs = hlog;
            hlog.ReadSendLog();
            hlog.ReadRecvLog();
            hlog.ReadSendErrLog();
            hlog.ReadRecvErrLog();

            foreach (HulftLog.RecordInfo rec in hlog.SendLogRecords.Values)
            {
                rec.fctl = GetFileCtrSend(rec);
            }
            foreach (HulftLog.RecordInfo rec in hlog.RecvLogRecords.Values)
            {
                rec.fctl = GetFileCtrRecv(rec);
            }

            // FILE_CTLに存在してログファイルにない集信ログを読み込む
            hlog.ReadFCtlRecvLog(_itemMgr.tbl_file_ctls);

            _itemMgr.HeaderInfo.MiHaishinCnt = GetDirectoryFileCount(ServerIni.Setting.IOSendRoot);
            _itemMgr.HeaderInfo.MiTorikomiCnt = GetDirectoryFileCount(ServerIni.Setting.RecvRoot);

            lblMiHaishinCnt.Text = string.Format("{0:###,##0}", _itemMgr.HeaderInfo.MiHaishinCnt);
            lblMiTorikomiCnt.Text = string.Format("{0:###,##0}", _itemMgr.HeaderInfo.MiTorikomiCnt);
            lblSendErr.Text = hlog.IsSendErr ? "●" : "";
            lblRecvErr.Text = hlog.IsRecvErr ? "●" : "";
            btnDelSendErr.Enabled = hlog.IsSendErr ? true : false;
            btnDelRecvErr.Enabled = hlog.IsRecvErr ? true : false;
            lblImportErr.Text = IsImportError() ? "●" : "";
            chkAutoRefresh.Checked = _itemMgr.HeaderInfo.IsAutoRefresh;
        }

        /// <summary>
        /// 指定したログ履歴のファイル集配信管理を取得する
        /// </summary>
        /// <param name="rec"></param>
        /// <param name="FileId"></param>
        /// <param name="FileDivid"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private TBL_FILE_CTL GetFileCtr(HulftLog.RecordInfo rec, string filter)
        {
            TBL_FILE_CTL fctl = null;
            DataRow[] rows = _itemMgr.tbl_file_ctls.Select(filter);
            if (rows.Length > 0)
            {
                fctl = new TBL_FILE_CTL(rows[0], AppInfo.Setting.SchemaBankCD);
            }
            else
            {
                fctl = new TBL_FILE_CTL(rec.FGen.FileId, rec.FGen.FileDivid, rec.FGen.FileName, "", AppInfo.Setting.SchemaBankCD);
                fctl.m_CAP_STS = FileCtl.CapSts.未取込;
            }
            return fctl;
        }

        /// <summary>
        /// 指定したログ履歴のファイル集配信管理を取得する(配信)
        /// </summary>
        /// <param name="rec"></param>
        /// <param name="FileId"></param>
        /// <param name="FileDivid"></param>
        /// <returns></returns>
        private TBL_FILE_CTL GetFileCtrSend(HulftLog.RecordInfo rec)
        {
            //フィルター設定
            string filter = string.Format("FILE_ID='{0}' AND FILE_DIVID='{1}' AND SEND_FILE_NAME='{2}'", rec.FGen.FileId, rec.FGen.FileDivid, rec.FGen.FileName);
            return GetFileCtr(rec, filter);
        }

        /// <summary>
        /// 指定したログ履歴のファイル集配信管理を取得する(集信)
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        private TBL_FILE_CTL GetFileCtrRecv(HulftLog.RecordInfo rec)
        {
            // FileId・FileDividを一部置き換えて実行
            string FileId = rec.FGen.FileId;
            string FileDivid = rec.FGen.FileDivid;

            if (rec.FGen.FileId == FileParam.FileId.IF207)
            {
                // 持帰要求結果
                switch (rec.FGen.FileDivid)
                {
                    case FileParam.FileKbn.GDA:
                        // 持帰要求結果テキスト
                        FileId = FileParam.FileId.IF203;
                        break;
                }
            }

            if (rec.FGen.FileId == FileParam.FileId.IF206)
            {
                // 結果テキスト
                switch (rec.FGen.FileDivid)
                {
                    case FileParam.FileKbn.BUB:
                        // 持出アップロード
                        FileId = FileParam.FileId.IF101;
                        break;
                    case FileParam.FileKbn.BCA:
                        // 持出取消
                        FileId = FileParam.FileId.IF202;
                        break;
                    case FileParam.FileKbn.GMA:
                        // 証券データ訂正
                        FileId = FileParam.FileId.IF204;
                        break;
                    case FileParam.FileKbn.GRA:
                        // 不渡返還
                        FileId = FileParam.FileId.IF205;
                        break;
                }
            }

            //フィルター設定
            string filter = string.Format("FILE_ID='{0}' AND FILE_DIVID='{1}' AND CAP_FILE_NAME='{2}'", FileId, FileDivid, rec.FGen.FileName);

            return GetFileCtr(rec, filter);
        }


        /// <summary>
        /// 取込エラー有無を判別する
        /// </summary>
        private bool IsImportError()
        {
            string filter = string.Format("CAP_STS={0}", FileCtl.CapSts.取込エラー);
            DataRow[] rows = _itemMgr.tbl_file_ctls.Select(filter);
            return (rows.Length > 0);
        }

        /// <summary>
        /// [配信：エラー消込] ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelSendErr_Click(object sender, EventArgs e)
        {
            if (_ctl.IsIniErr) { return; }
            try
            {
                // 配信エラーログ削除

                // 確認メッセージ
                if (ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, "配信新規エラーの消込を行います。\nよろしいですか？") == DialogResult.Cancel)
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("配信エラーログ削除 [{0}]", _ctl.SendErrLogFilePath), 1);
                _itemMgr.DispParams.HulftLogs.DeleteSendErrLog();

                // 画面表示データ更新
                RefreshDisplayData();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// [集信：エラー消込] ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelRecvErr_Click(object sender, EventArgs e)
        {
            if (_ctl.IsIniErr) { return; }
            try
            {
                // 集信エラーログ削除

                // 確認メッセージ
                if (ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, "集信新規エラーの消込を行います。\nよろしいですか？") == DialogResult.Cancel)
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("集信エラーログ削除 [{0}]", _ctl.RecvErrLogFilePath), 1);
                _itemMgr.DispParams.HulftLogs.DeleteRecvErrLog();

                // 画面表示データ更新
                RefreshDisplayData();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// [自動更新]チェックボックス CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (_ctl.IsIniErr) { return; }
            try
            {
                if (chkAutoRefresh.Checked)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "画面自動更新 ON", 1);
                    timerUpdateList.Start();
                }
                else
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "画面自動更新 OFF", 1);
                    timerUpdateList.Stop();
                }
                _itemMgr.HeaderInfo.IsAutoRefresh = chkAutoRefresh.Checked;
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// 一定間隔ごとにデータの再集計
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerUpdateList_Tick(object sender, EventArgs e)
        {
            try
            {
                // 画面表示データ更新
                if (_parent1 != null)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("自動更新:{0}", _parent1.Name), 1);
                    _parent1.RefreshForm();
                }
                if (_parent2 != null)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("自動更新:{0}", _parent2.Name), 1);
                    _parent2.RefreshForm();
                }
                if (_parent3 != null)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("自動更新:{0}", _parent3.Name), 1);
                    _parent3.RefreshForm();
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// 対象ディレクトリのファイル数取得
        /// </summary>
        private int GetDirectoryFileCount(string DirectoryPath)
        {
            if (!Directory.Exists(DirectoryPath))
            {
                return 0;
            }
            return Directory.GetFiles(DirectoryPath).Length;
        }

    }
}
