using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;

namespace ParamMaint
{
    /// <summary>
    /// 項目選択画面
    /// </summary>
	public partial class ItemMstDialog : EntryCommonDialogBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        public int ItemId { get; private set; } = 0;

        private const int COL_CD = 1;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ctl"></param>
        public ItemMstDialog()
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

            base.InitializeForm(ctl);
        }


        // *******************************************************************
        // 継承メソッド
        // *******************************************************************

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
        /// コントロール初期化
        /// </summary>
        protected void InitializeControl()
        {
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
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
            // ITEM_MASTER

            int cnt = 0;
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[_itemMgr.MasterDspParam.item_masters.Rows.Count];
            foreach (DataRow row in _itemMgr.MasterDspParam.item_masters.Rows)
            {
                TBL_ITEM_MASTER itemMst = new TBL_ITEM_MASTER(row, AppInfo.Setting.SchemaBankCD);

                int no = cnt + 1;
                listItem.Clear();
                listItem.Add(no.ToString());
                listItem.Add(itemMst._ITEM_ID.ToString());
                listItem.Add(itemMst.m_ITEM_NAME);
                listView[cnt] = new ListViewItem(listItem.ToArray());
                listView[cnt].UseItemStyleForSubItems = false;
                cnt++;
            }
            // リスト表示を初期化
            this.lvBatList.Items.Clear();
            this.lvBatList.Items.AddRange(listView);
            this.lvBatList.Enabled = true;
            this.lvBatList.Refresh();
            if (this.lvBatList.Items.Count > 0)
            {
                this.lvBatList.Items[0].Selected = true;
                this.lvBatList.Items[0].Focused = true;
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

        /// <summary>
        /// [フォーム] Shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchDialog_Shown(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// [確定]ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (this.lvBatList.SelectedIndices.Count < 1)
                {
                    SetStatusMessage("対象行が選択されていません。");
                    return;
                }

                // 選択行を取得する
                this.ItemId = DBConvert.ToIntNull(lvBatList.SelectedItems[0].SubItems[COL_CD].Text);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// [終了]ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// [画面項目] KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyDown(object sender, KeyEventArgs e)
        {
            this.ClearStatusMessage();
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btnOK_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// [アイテム一覧]リスト MouseDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvBatList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                SendKeys.Send("{Enter}");
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }


        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

    }
}
