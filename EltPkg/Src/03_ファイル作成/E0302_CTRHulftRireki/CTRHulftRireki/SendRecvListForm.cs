using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
    /// HULFT集配信状況一覧
    /// </summary>
    public partial class SendRecvListForm : EntryCommonFormBase
	{
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private List<ItemSet> _sendFileIdList = null;
        private List<ItemSet> _recvFileIdList = null;

        #region クラス定数

        private const int KEY = 0;
        private const int FILEKEY = 1;

        #endregion

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
        public SendRecvListForm()
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
            base.SetDispName2("HULFT集配信状況一覧");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
		{
            // 通常状態
            SetFunctionName(F1_, "終了");
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
            SetFunctionName(F12_, "取込画面", true, Const.FONT_SIZE_FUNC_LOW);
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
		{
            // 設定ファイル読み込みでエラー発生時はF1以外Disable
            if (this._ctl.SettingData.ChkServerIni == false || !string.IsNullOrEmpty(this._ctl.SettingData.CheckParamMsg))
            {
                DisableAllFunctionState(true);
            }
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
            if (_ctl.IsIniErr) { return; }
            SetSendFileIdList();
            SetRecvFileIdList();
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
            if (_ctl.IsIniErr) { return; }

            // ヘッダー
            headerControl.RefreshDisplayData();

            // ファイル集配信管理取得
            _itemMgr.Fetch_file_ctls();

            // 集配信ログ取得（HeaderControl で読込済み）
            HulftLog hlog = _itemMgr.DispParams.HulftLogs;

            // プルダウン取得
            string cmbSendFileKey = (cmbSendFile.SelectedIndex < 0) ? "" : ((ItemSet)cmbSendFile.SelectedItem).ItemValue;
            string cmbRecvFileKey = (cmbRecvFile.SelectedIndex < 0) ? "" : ((ItemSet)cmbRecvFile.SelectedItem).ItemValue;

            // 配信履歴
            {
                // 件数集計
                int rowCnt = 0;
                foreach (HulftLog.RecordInfo rec in hlog.SendLogRecords.Values)
                {
                    string fileKey = rec.FGen.FileId + rec.FGen.FileDivid;
                    if (!CheckFilterSend(rec, fileKey, cmbSendFileKey)) { continue; }
                    rowCnt++;
                }

                // 一覧表示
                int cnt = 0;
                int SelectItem = -1;
                List<string> listItem = new List<string>();
                ListViewItem[] listView = new ListViewItem[rowCnt];
                foreach (HulftLog.RecordInfo rec in hlog.SendLogRecords.Values)
                {
                    string key = rec.No.ToString();
                    string fileKey = rec.FGen.FileId + rec.FGen.FileDivid;
                    string fileKbnName = _itemMgr.SendNameList.ContainsKey(fileKey) ? (_itemMgr.SendNameList[fileKey]) : "";
                    string recCnt = string.Format("{0:###,##0}", rec.レコード件数);
                    string dataSize = string.Format("{0:###,##0}", rec.データサイズ);

                    // 絞り込み
                    if (!CheckFilterSend(rec, fileKey, cmbSendFileKey)) { continue; }

                    listItem.Clear();
                    listItem.Add(key);                  // 隠しキー
                    listItem.Add(fileKey);              // ファイル種類
                    listItem.Add(fileKbnName);          // ファイル種類名称
                    listItem.Add(rec.集配信開始日);     // 開始日
                    listItem.Add(rec.集配信開始時刻);   // 開始時刻
                    listItem.Add(rec.集配信終了時刻);   // 終了時刻
                    listItem.Add(recCnt);               // レコード件数
                    listItem.Add(dataSize);             // データサイズ
                    listItem.Add(rec.ステータス);       // ステータス
                    listItem.Add(rec.FGen.FileName);    // ファイル名
                    listView[cnt] = new ListViewItem(listItem.ToArray());

                    // 前選択状態の復元
                    if (key == _itemMgr.DispParams.SendSelectItemKey &&
                        fileKey == _itemMgr.DispParams.SendSelectItemFileKey)
                    {
                        SelectItem = cnt;
                    }

                    cnt++;
                }
                this.lstSendList.BeginUpdate();
                this.lstSendList.Items.Clear();
                this.lstSendList.Items.AddRange(listView);
                this.lstSendList.Enabled = true;
                this.lstSendList.Refresh();

                //初期表示位置調整(最下部表示)
                SetListDefPositon(lstSendList);

                //if (SelectItem >= 0)
                //{
                //    this.lstSendList.Items[SelectItem].Selected = true;
                //    this.lstSendList.Items[SelectItem].Focused = true;
                //    //初期表示位置調整
                //    SetListDefPositon(lstSendList);
                //}
                this.lstSendList.EndUpdate();
            }

            // 集信履歴
            {
                // 件数集計
                int rowCnt = 0;
                int SelectItem = -1;
                foreach (HulftLog.RecordInfo rec in hlog.RecvLogRecords.Values)
                {
                    string fileKey = rec.FGen.FileId + rec.FGen.FileDivid;
                    if (!CheckFilterRecv(rec, fileKey, cmbRecvFileKey)) { continue; }
                    rowCnt++;
                }

                // 一覧表示
                int cnt = 0;
                List<string> listItem = new List<string>();
                ListViewItem[] listView = new ListViewItem[rowCnt];
                foreach (HulftLog.RecordInfo rec in hlog.RecvLogRecords.Values)
                {
                    string key = rec.No.ToString();
                    string fileKey = rec.FGen.FileId + rec.FGen.FileDivid;
                    string fileKbnName = _itemMgr.RecvNameList.ContainsKey(fileKey) ? (_itemMgr.RecvNameList[fileKey]) : "";
                    string recCnt = string.Format("{0:###,##0}", rec.レコード件数);
                    string dataSize = string.Format("{0:###,##0}", rec.データサイズ);

                    // 絞り込み
                    if (!CheckFilterRecv(rec, fileKey, cmbRecvFileKey)) { continue; }

                    listItem.Clear();
                    listItem.Add(key);                  // 隠しキー
                    listItem.Add(fileKey);              // ファイル種類
                    listItem.Add(fileKbnName);          // ファイル種類名称
                    listItem.Add(rec.集配信開始日);     // 開始日
                    listItem.Add(rec.集配信開始時刻);   // 開始時刻
                    listItem.Add(rec.集配信終了時刻);   // 終了時刻
                    listItem.Add(recCnt);               // レコード件数
                    listItem.Add(dataSize);             // データサイズ
                    listItem.Add(rec.ステータス);       // ステータス
                    listItem.Add(rec.FGen.FileName);    // ファイル名
                    listView[cnt] = new ListViewItem(listItem.ToArray());

                    // 前選択状態の復元
                    if (key == _itemMgr.DispParams.RecvSelectItemKey &&
                        fileKey == _itemMgr.DispParams.RecvSelectItemFileKey)
                    {
                        SelectItem = cnt;
                    }

                    cnt++;
                }
                this.lstRecvList.BeginUpdate();
                this.lstRecvList.Items.Clear();
                this.lstRecvList.Items.AddRange(listView);
                this.lstRecvList.Enabled = true;
                this.lstRecvList.Refresh();

                //初期表示位置調整(最下部表示)
                SetListDefPositon(lstRecvList);

                //if (SelectItem >= 0)
                //{
                //    this.lstRecvList.Items[SelectItem].Selected = true;
                //    this.lstRecvList.Items[SelectItem].Focused = true;
                //    //初期表示位置調整
                //    SetListDefPositon(lstRecvList);
                //}
                this.lstRecvList.EndUpdate();
            }
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            if (_itemMgr.DispParams.ProcType == ItemManager.ProcTypes.配信)
            {
                _itemMgr.DispParams.SendItemValue = (cmbSendFile.SelectedIndex < 0) ? "" : ((ItemSet)cmbSendFile.SelectedItem).ItemValue;
                _itemMgr.DispParams.RecvItemValue = "";
            }
            else if (_itemMgr.DispParams.ProcType == ItemManager.ProcTypes.集信)
            {
                _itemMgr.DispParams.SendItemValue = "";
                _itemMgr.DispParams.RecvItemValue = (cmbRecvFile.SelectedIndex < 0) ? "" : ((ItemSet)cmbRecvFile.SelectedItem).ItemValue;
           }

            //選択行情報の取得
            _itemMgr.DispParams.SendSelectItemKey = "";
            _itemMgr.DispParams.SendSelectItemFileKey = "";
            _itemMgr.DispParams.RecvSelectItemKey = "";
            _itemMgr.DispParams.RecvSelectItemFileKey = "";
            if (this.lstSendList.SelectedIndices.Count > 0)
            {
                _itemMgr.DispParams.SendSelectItemKey = this.lstSendList.SelectedItems[0].SubItems[KEY].Text;
                _itemMgr.DispParams.SendSelectItemFileKey = this.lstSendList.SelectedItems[0].SubItems[FILEKEY].Text;
            }
            if (this.lstRecvList.SelectedIndices.Count > 0)
            {
                _itemMgr.DispParams.RecvSelectItemKey = this.lstRecvList.SelectedItems[0].SubItems[KEY].Text;
                _itemMgr.DispParams.RecvSelectItemFileKey = this.lstRecvList.SelectedItems[0].SubItems[FILEKEY].Text;
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
        private void SendRecvListForm_Load(object sender, EventArgs e)
        {
            // 設定ファイル読み込みでエラー発生時
            if (this._ctl.SettingData.ChkServerIni == false)
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00003));
                return;
            }
            if (!string.IsNullOrEmpty(this._ctl.SettingData.CheckParamMsg))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E01001, this._ctl.SettingData.CheckParamMsg));
                return;
            }

            // 初回表示時の初期表示位置調整(最下部表示)
            // 初回表示ではここでも実施する必要がある。
            SetListDefPositon(lstSendList);
            SetListDefPositon(lstRecvList);
        }

        /// <summary>
        /// [チェックボックス] CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_ctl.IsIniErr) { return; }
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
        private void Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ctl.IsIniErr) { return; }
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

        ///// <summary>
        ///// [リストビュー] MouseClick
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void lstList_MouseClick(object sender, MouseEventArgs e)
        //{
        //    if (_ctl.IsIniErr) { return; }
        //    if (((Control)sender).Name.Equals("lstSendList"))
        //    {
        //        _itemMgr.DispParams.ProcType = ItemManager.ProcTypes.配信;
        //    }
        //    else if (((Control)sender).Name.Equals("lstRecvList"))
        //    {
        //        _itemMgr.DispParams.ProcType = ItemManager.ProcTypes.集信;
        //    }
        //}

        /// <summary>
        /// 列幅変更不可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstSendList_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lstSendList.Columns[e.ColumnIndex].Width;
        }

        /// <summary>
        /// 列幅変更不可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstRecvList_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lstRecvList.Columns[e.ColumnIndex].Width;
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
        /// F12：取込
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                // 取込
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "取込", 1);

                // 画面項目取得
                _itemMgr.DispParams.ProcType = ItemManager.ProcTypes.集信;
                GetDisplayParams();

                // 取込状況一覧(Timerが有効の場合、メモリに残るためDisposeさせる)
                using (ImportListForm form = new ImportListForm())
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
            cmbSendFile.DataSource = _sendFileIdList;
            cmbSendFile.DisplayMember = "ItemDisp";
            cmbSendFile.ValueMember = "ItemValue";
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
            cmbRecvFile.DataSource = _recvFileIdList;
            cmbRecvFile.DisplayMember = "ItemDisp";
            cmbRecvFile.ValueMember = "ItemValue";
        }

        /// <summary>
        /// 入力条件によってフィルタリングを行う（配信）
        /// </summary>
        /// <param name="rec"></param>
        /// <param name="fileKey"></param>
        /// <param name="cmbFileKey"></param>
        /// <returns></returns>
        private bool CheckFilterSend(HulftLog.RecordInfo rec, string fileKey, string cmbFileKey)
        {
            if (!chkSendOK.Checked && !rec.IsErr) { return false; }
            if (!chkSendNG.Checked && rec.IsErr) { return false; }
            if (!string.IsNullOrEmpty(cmbFileKey) && !cmbFileKey.Equals(fileKey)) { return false; }
            return true;
        }

        /// <summary>
        /// 入力条件によってフィルタリングを行う（集信）
        /// </summary>
        /// <param name="rec"></param>
        /// <param name="fileKey"></param>
        /// <param name="cmbFileKey"></param>
        /// <returns></returns>
        private bool CheckFilterRecv(HulftLog.RecordInfo rec, string fileKey, string cmbFileKey)
        {
            if (!chkRecvOK.Checked && !rec.IsErr) { return false; }
            if (!chkRecvNG.Checked && rec.IsErr) { return false; }
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
