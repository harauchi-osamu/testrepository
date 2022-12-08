using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;

namespace SearchTxtView
{
    /// <summary>
    /// ヘッダーコントロール
    /// </summary>
    public partial class HeaderControl : UserControl
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private SearchTxtViewList _parent1 = null;

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
        public void InitializeForm(ControllerBase ctl, SearchTxtViewList form)
        {
            _parent1 = form;
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
            chkAutoRefresh.Checked = _itemMgr.HeaderInfo.IsAutoRefresh;
        }

        /// <summary>
        /// [自動更新]チェックボックス CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
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
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

    }
}
