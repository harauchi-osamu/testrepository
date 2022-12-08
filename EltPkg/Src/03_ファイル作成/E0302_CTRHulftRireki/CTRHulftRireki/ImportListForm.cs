using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;
using HulftIO;

namespace CTRHulftRireki
{
    /// <summary>
    /// 取込状況一覧
    /// </summary>
    public partial class ImportListForm : EntryCommonFormBase
	{
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private List<ItemSet> _sendFileIdList = null;
        private List<ItemSet> _recvFileIdList = null;

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

        private const int COL_KEY = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ImportListForm()
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

            headerControl.InitializeForm(_ctl, this);
            base.InitializeForm(ctl);
            InitializeControl();
        }


        // *******************************************************************
        // 継承メソッド
        // *******************************************************************

        /// <summary>
        /// 画面名を設定する
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
            base.SetDispName2("取込状況一覧");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
		{
            // 通常状態
            SetFunctionName(F1_, "HULFT\n  画面", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F2_, string.Empty);
            SetFunctionName(F3_, string.Empty);
            SetFunctionName(F4_, string.Empty);
            SetFunctionName(F5_, "最新表示", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F6_, string.Empty);
            SetFunctionName(F7_, string.Empty);
            SetFunctionName(F8_, string.Empty);
            SetFunctionName(F9_, string.Empty);
            SetFunctionName(F10_, string.Empty);
            SetFunctionName(F11_, string.Empty);
            SetFunctionName(F12_, "エラーログ\n画面", true, Const.FONT_SIZE_FUNC_LOW);
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
		{
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
            // 画面表示データ更新
            RefreshDisplayData();
        }

        /// <summary>
        /// コントロール初期化
        /// </summary>
        protected void InitializeControl()
        {
            if (_itemMgr.DispParams.ProcType == ItemManager.ProcTypes.配信)
            {
                lblTitle.Text = "取込状況 （配信）";
                SetSendFileIdList();
            }
            else if (_itemMgr.DispParams.ProcType == ItemManager.ProcTypes.集信)
            {
                lblTitle.Text = "取込状況 （集信）";
                SetRecvFileIdList();
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
            // ヘッダー
            headerControl.RefreshDisplayData();

            // ファイル集配信管理取得
            _itemMgr.Fetch_file_ctls();

            // 集配信ログ取得
            HulftLog hlog = _itemMgr.DispParams.HulftLogs;

            // プルダウン取得
            string cmbFileKey = (cmbFile.SelectedIndex < 0) ? "" : ((ItemSet)cmbFile.SelectedItem).ItemValue;

            if (_itemMgr.DispParams.ProcType == ItemManager.ProcTypes.配信)
            {
                // 件数集計
                int rowCnt = 0;
                foreach (HulftLog.RecordInfo rec in hlog.SendLogRecords.Values)
                {
                    TBL_FILE_CTL fctl = rec.fctl;
                    string fileKey = rec.FGen.FileId + rec.FGen.FileDivid;

                    // 絞り込み
                    if (!CheckFilter(fctl, fileKey, cmbFileKey)) { continue; }
                    rowCnt++;
                }

                // 一覧表示
                int cnt = 0;
                List<string> listItem = new List<string>();
                ListViewItem[] listView = new ListViewItem[rowCnt];
                foreach (HulftLog.RecordInfo rec in hlog.SendLogRecords.Values)
                {
                    // ファイル集配信管理取得
                    TBL_FILE_CTL fctl = rec.fctl;
                    string key = rec.No.ToString();
                    string fileKey = rec.FGen.FileId + rec.FGen.FileDivid;
                    string fileKbnName = _itemMgr.SendNameList.ContainsKey(fileKey) ? (_itemMgr.SendNameList[fileKey]) : "";
                    string fileSts = (fctl == null) ? "" : FileCtl.CapSts.GetName(fctl.m_CAP_STS);
                    string compDateTime = (fctl == null) ? "" : CommonUtil.ConvDateMiliTimeToDateTimeFormat(fctl.m_CAP_DATE, fctl.m_CAP_TIME);

                    // 絞り込み
                    if (!CheckFilter(fctl, fileKey, cmbFileKey)) { continue; }

                    // 配信の場合は取込ファイル名を表示する
                    listItem.Clear();
                    listItem.Add(key);                  // 隠しキー
                    listItem.Add(fileKey);              // ファイル種類
                    listItem.Add(fileKbnName);          // ファイル種類名称
                    listItem.Add(fileSts);              // 状況
                    listItem.Add(compDateTime);         // 取込完了時刻
                    listItem.Add(fctl._CAP_FILE_NAME);  // ファイル名
                    listView[cnt] = new ListViewItem(listItem.ToArray());
                    cnt++;
                }
                this.lstImportList.BeginUpdate();
                this.lstImportList.Items.Clear();
                this.lstImportList.Items.AddRange(listView);
            }
            else if (_itemMgr.DispParams.ProcType == ItemManager.ProcTypes.集信)
            {
                // 件数集計(集信はRecvLogRecordsとFCtlRecvLogRecordsの合計を使用)
                int rowCnt = 0;
                foreach (HulftLog.RecordInfo rec in hlog.AllRecvLogRecords.Values)
                {
                    // ファイル集配信管理取得
                    TBL_FILE_CTL fctl = rec.fctl;
                    string fileKey = rec.FGen.FileId + rec.FGen.FileDivid;

                    // 絞り込み
                    if (!CheckFilter(fctl, fileKey, cmbFileKey)) { continue; }
                    rowCnt++;
                }

                // 一覧表示(集信はRecvLogRecordsとFCtlRecvLogRecordsの合計を使用)
                int cnt = 0;
                List<string> listItem = new List<string>();
                ListViewItem[] listView = new ListViewItem[rowCnt];
                foreach (HulftLog.RecordInfo rec in hlog.AllRecvLogRecords.Values)
                {
                    // ファイル集配信管理取得
                    TBL_FILE_CTL fctl = rec.fctl;
                    string key = rec.No.ToString();
                    string fileKey = rec.FGen.FileId + rec.FGen.FileDivid;
                    string fileKbnName = _itemMgr.RecvNameList.ContainsKey(fileKey) ? (_itemMgr.RecvNameList[fileKey]) : "";
                    string fileSts = (fctl == null) ? "" : FileCtl.CapSts.GetName(fctl.m_CAP_STS);
                    string compDateTime = (fctl == null) ? "" : CommonUtil.ConvDateMiliTimeToDateTimeFormat(fctl.m_CAP_DATE, fctl.m_CAP_TIME);

                    // 絞り込み
                    if (!CheckFilter(fctl, fileKey, cmbFileKey)) { continue; }

                    listItem.Clear();
                    listItem.Add(key);                  // 隠しキー
                    listItem.Add(fileKey);              // ファイル種類
                    listItem.Add(fileKbnName);          // ファイル種類名称
                    listItem.Add(fileSts);              // 状況
                    listItem.Add(compDateTime);         // 取込完了時刻
                    listItem.Add(rec.FGen.FileName);    // ファイル名
                    listView[cnt] = new ListViewItem(listItem.ToArray());
                    cnt++;
                }
                this.lstImportList.BeginUpdate();
                this.lstImportList.Items.Clear();
                this.lstImportList.Items.AddRange(listView);
            }
            this.lstImportList.Enabled = true;
            this.lstImportList.Refresh();

            //初期表示位置調整(最下部表示)
            SetListDefPositon(lstImportList);

            this.lstImportList.EndUpdate();
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            // 選択行のバッチ情報を取得する
            if (this.lstImportList.SelectedIndices.Count < 1)
            {
                ComMessageMgr.MessageWarning("対象行が選択されていません。");
                return false;
            }

            // 選択行取得
            _itemMgr.DispParams.LogRecord = null;
            int key = DBConvert.ToIntNull(this.lstImportList.SelectedItems[0].SubItems[COL_KEY].Text);
            if (_itemMgr.DispParams.ProcType == ItemManager.ProcTypes.配信)
            {
                if (_itemMgr.DispParams.HulftLogs.SendLogRecords.ContainsKey(key))
                {
                    _itemMgr.DispParams.LogRecord = _itemMgr.DispParams.HulftLogs.SendLogRecords[key];
                }
            }
            else // if (_itemMgr.DispParams.ProcType == ItemManager.ProcTypes.集信)
            {
                // 集信はRecvLogRecordsとFCtlRecvLogRecordsの合計を使用
                if (_itemMgr.DispParams.HulftLogs.AllRecvLogRecords.ContainsKey(key))
                {
                    _itemMgr.DispParams.LogRecord = _itemMgr.DispParams.HulftLogs.AllRecvLogRecords[key];
                }
            }

            // エラーチェック
            if (_itemMgr.DispParams.LogRecord == null || _itemMgr.DispParams.LogRecord.fctl.m_CAP_STS != FileCtl.CapSts.取込エラー)
            {
                ComMessageMgr.MessageInformation("対象データにエラーはありません。");
                return false;
            }
            return true;
        }


        // *******************************************************************
        // イベント
        // *******************************************************************

        /// <summary>
        /// [フォーム] Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportListForm_Load(object sender, EventArgs e)
        {
            // 初回表示時の初期表示位置調整(最下部表示)
            // 初回表示ではここでも実施する必要がある。
            SetListDefPositon(lstImportList);
        }

        /// <summary>
        /// [絞込]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilter_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 画面表示データ更新
                RefreshDisplayData();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [チェックボックス] CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 画面表示データ更新
                RefreshDisplayData();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [コンボボックス] SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // 画面表示データ更新
                RefreshDisplayData();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// 列幅変更不可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstImportList_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lstImportList.Columns[e.ColumnIndex].Width;
        }


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
        /// F5：最新表示
        /// </summary>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                // 最新表示
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "最新表示", 1);

                // 画面表示データ更新
                RefreshDisplayData();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F12：エラー確認
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                // エラー確認
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "エラー確認", 1);

                // 画面項目取得
                if (!GetDisplayParams())
                {
                    return;
                }

                // 取込エラーログ(Timerが有効の場合、メモリに残るためDisposeさせる)
                using (DetailForm form = new DetailForm())
                {
                    form.InitializeForm(_ctl);
                    form.ShowDialog();
                }

                // 画面表示データ更新
                RefreshDisplayData();
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
        /// 一覧初期位置設定
        /// </summary>
        private void SetListDefPositon(ListView list)
        {
            if (list.Items.Count == 0) return;

            // 最下部表示
            list.Items[list.Items.Count - 1].EnsureVisible();
        }

        /// <summary>
        /// 配信ファイルプルダウン
        /// </summary>
        /// <returns></returns>
        private void SetSendFileIdList()
        {
            List<ItemSet> list = new List<ItemSet>();
            string filter = string.Format("FILE_COURSE=0");
            string sort = "FILE_ID";
            DataRow[] rows = _itemMgr.tbl_file_params.Select(filter, sort);

            Dictionary<string, string> idList = new Dictionary<string, string>();
            foreach (DataRow row in rows)
            {
                TBL_FILE_PARAM data = new TBL_FILE_PARAM(row, AppInfo.Setting.SchemaBankCD);
                string key = data._FILE_ID + data._FILE_DIVID;
                if (idList.ContainsKey(key)) { continue; }
                idList.Add(key, _itemMgr.SendNameList[key]);
            }

            list.Add(new ItemSet("", ""));
            foreach (var keyVal in idList)
            {
                ItemSet item = new ItemSet(keyVal.Key, keyVal.Value);
                list.Add(item);
            }

            _sendFileIdList = list;
            cmbFile.DataSource = _sendFileIdList;
            cmbFile.DisplayMember = "ItemDisp";
            cmbFile.ValueMember = "ItemValue";
            cmbFile.SelectedValue = _itemMgr.DispParams.SendItemValue;
        }

        /// <summary>
        /// 集信ファイルプルダウン
        /// </summary>
        /// <returns></returns>
        private void SetRecvFileIdList()
        {
            List<ItemSet> list = new List<ItemSet>();
            string filter = string.Format("FILE_COURSE=1");
            string sort = "FILE_ID";
            DataRow[] rows = _itemMgr.tbl_file_params.Select(filter, sort);

            Dictionary<string, string> idList = new Dictionary<string, string>();
            foreach (DataRow row in rows)
            {
                TBL_FILE_PARAM data = new TBL_FILE_PARAM(row, AppInfo.Setting.SchemaBankCD);
                string key = data._FILE_ID + data._FILE_DIVID;
                if (idList.ContainsKey(key)) { continue; }
                idList.Add(key, _itemMgr.RecvNameList[key]);
            }

            list.Add(new ItemSet("", ""));
            foreach (var keyVal in idList)
            {
                ItemSet item = new ItemSet(keyVal.Key, keyVal.Value);
                list.Add(item);
            }

            _recvFileIdList = list;
            cmbFile.DataSource = _recvFileIdList;
            cmbFile.DisplayMember = "ItemDisp";
            cmbFile.ValueMember = "ItemValue";
            cmbFile.SelectedValue = _itemMgr.DispParams.RecvItemValue;
        }

        /// <summary>
        /// 入力条件によってフィルタリングを行う
        /// </summary>
        /// <param name="fctl"></param>
        /// <param name="fileKey"></param>
        /// <param name="cmbFileKey"></param>
        /// <returns></returns>
        private bool CheckFilter(TBL_FILE_CTL fctl, string fileKey, string cmbFileKey)
        {
            if (!chkNon.Checked && ((fctl.m_CAP_STS == FileCtl.CapSts.未取込) || (fctl.m_CAP_STS == FileCtl.CapSts.取込中))) { return false; }
            if (!chkOK.Checked && (fctl.m_CAP_STS == FileCtl.CapSts.取込完了)) { return false; }
            if (!chkHoryu.Checked && (fctl.m_CAP_STS == FileCtl.CapSts.取込保留)) { return false; }
            if (!chkNG.Checked && (fctl.m_CAP_STS == FileCtl.CapSts.取込エラー)) { return false; }
            if (!string.IsNullOrEmpty(cmbFileKey) && !cmbFileKey.Equals(fileKey)) { return false; }
            return true;
        }


        public class ItemSet
        {
            public string ItemValue { get; set; }
            public string ItemDisp { get; set; }
            public ItemSet(string value, string disp)
            {
                ItemValue = value;
                ItemDisp = disp;
            }
        }
    }
}
