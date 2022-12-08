using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;

namespace CorrectInput
{
    /// <summary>
    /// コード検索画面
    /// </summary>
	public partial class SearchDialog : EntryCommonDialogBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private SearchType _type;
        private string _searchKey;
        private string _searchWord;

        public int ResultCd { get; private set; } = 0;
        public string ResultName { get; private set; } = "";

        private const int COL_CD = 1;
        private const int COL_NAME = 2;

        public enum SearchType
        {
            Bank,
            Branch,
            Kouza
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ctl"></param>
        public SearchDialog(SearchType type, string searchKey)
		{
			InitializeComponent();

            _type = type;
            _searchKey = searchKey;
            _searchWord = "default";
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
            lblDspName.Text = "コード検索";
        }

        /// <summary>
        /// 画面表示データ初期化
        /// </summary>
        protected void InitializeDisplayData()
        {
            // 初期値設定
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
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            string val = txtKeyWord.Text;
            if (string.IsNullOrEmpty(val))
            {
                SetStatusMessage("名称が入力されていません。");
                return false;
            }
            _searchWord = txtKeyWord.Text;
            return true;
        }


        // *******************************************************************
        // イベント
        // *******************************************************************

        /// <summary>
        /// [画面項目] KeyUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (txtKeyWord.Focused)
                    {
                        if (!_searchWord.Equals(txtKeyWord.Text))
                        {
                            SearchList();
                        }
                        else
                        {
                            if (lvBatList.Items.Count > 0)
                            {
                                lvBatList.Focus();
                                lvBatList.Items[0].Selected = true;
                            }
                        }
                    }
                    else if (lvBatList.Focused)
                    {
                        btnOK_Click(sender, e);
                    }
                    break;

                case Keys.F1:
                    btnClose_Click(sender, e);
                    break;

                case Keys.F12:
                    btnOK_Click(sender, e);
                    break;
                default:
                    break;
            }
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
                this.ResultCd = DBConvert.ToIntNull(lvBatList.SelectedItems[0].SubItems[COL_CD].Text);
                this.ResultName = DBConvert.ToStringNull(lvBatList.SelectedItems[0].SubItems[COL_NAME].Text);

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

        private void SearchDialog_Shown(object sender, EventArgs e)
        {
            SetTextFocus(txtKeyWord);
        }


        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 検索を行う
        /// </summary>
        private void SearchList()
        {
            // 入力チェック
            if (!GetDisplayParams())
            {
                return;
            }

            switch (_type)
            {
                case SearchType.Bank:
                    SetDisplayParamsBank();
                    break;

                case SearchType.Branch:
                    SetDisplayParamsBranch();
                    break;

                case SearchType.Kouza:
                    SetDisplayParamsShiharai();
                    break;
            }
        }

        /// <summary>
        /// 持帰銀行コードを検索する
        /// </summary>
        private void SetDisplayParamsBank()
        {
            // TBL_BANKMF
            var rows = _itemMgr.mst_banks.Values.Where(p =>
            {
                bool isHit = p.m_BK_NAME_KANJI.Contains(_searchWord);
                isHit |= p.m_BK_NAME_KANA.Contains(_searchWord);
                return isHit;
            });

            int cnt = 0;
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[rows.Count()];
            foreach (var data in rows)
            {
                int no = cnt + 1;
                listItem.Clear();
                listItem.Add(no.ToString());
                listItem.Add(data._BK_NO.ToString(Const.BANK_NO_LEN_STR));
                listItem.Add(data.m_BK_NAME_KANJI);
                listItem.Add(data.m_BK_NAME_KANA);
                listView[cnt] = new ListViewItem(listItem.ToArray());
                listView[cnt].UseItemStyleForSubItems = false;
                cnt++;
            }
            // リスト表示を初期化
            this.lvBatList.Items.Clear();
            this.lvBatList.Items.AddRange(listView);
            this.lvBatList.Enabled = true;
            this.lvBatList.Refresh();
        }

        /// <summary>
        /// 持帰支店コードを検索する
        /// </summary>
        private void SetDisplayParamsBranch()
        {
            // TBL_BRANCHMF
            var rows = _itemMgr.mst_branches.Values.Where(p =>
            {
                bool isHit = p.m_BR_NAME_KANJI.Contains(_searchWord);
                isHit |= p.m_BR_NAME_KANA.Contains(_searchWord);
                return isHit;
            });

            int cnt = 0;
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[rows.Count()];
            foreach (var data in rows)
            {
                int no = cnt + 1;
                listItem.Clear();
                listItem.Add(no.ToString());
                listItem.Add(data._BR_NO.ToString(Const.BR_NO_LEN_STR));
                listItem.Add(data.m_BR_NAME_KANJI);
                listItem.Add(data.m_BR_NAME_KANA);
                listView[cnt] = new ListViewItem(listItem.ToArray());
                listView[cnt].UseItemStyleForSubItems = false;
                cnt++;
            }
            // リスト表示を初期化
            this.lvBatList.Items.Clear();
            this.lvBatList.Items.AddRange(listView);
            this.lvBatList.Enabled = true;
            this.lvBatList.Refresh();
        }

        /// <summary>
        /// 支払口座を検索する
        /// </summary>
        private void SetDisplayParamsShiharai()
        {
            // 対象の支店から検索
            int SearchBrNo;
            if (!int.TryParse(_searchKey, out SearchBrNo))
            {
                // 数値変換できない場合はエラー(ないケースだが)
                this.SetStatusMessage("支店番号が不正です。");
                return;
            }

            // TBL_PAYERMF
            var rows = _itemMgr.mst_payermf.Values.Where(p =>
            {
                if (p._BR_NO != SearchBrNo)
                {
                    return false;
                }
                bool isHit = p.m_NAME_KANJI.Contains(_searchWord);
                isHit |= p.m_NAME_KANA.Contains(_searchWord);
                return isHit;
            });

            int cnt = 0;
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[rows.Count()];
            foreach (var data in rows.OrderBy(p => p._BR_NO).OrderBy(p => p._ACCOUNT_NO))
            {
                int no = cnt + 1;
                listItem.Clear();
                listItem.Add(no.ToString());
                listItem.Add(data._ACCOUNT_NO.ToString("D7"));
                listItem.Add(data.m_NAME_KANJI);
                listItem.Add(data.m_NAME_KANA);
                listView[cnt] = new ListViewItem(listItem.ToArray());
                listView[cnt].UseItemStyleForSubItems = false;
                cnt++;
            }
            // リスト表示を初期化
            this.lvBatList.Items.Clear();
            this.lvBatList.Items.AddRange(listView);
            this.lvBatList.Enabled = true;
            this.lvBatList.Refresh();
        }

    }
}
