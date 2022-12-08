using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;

namespace ParamMaint
{
    /// <summary>
    /// 業務選択画面
    /// </summary>
    public partial class SearchGymDialog : UnclosableForm
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        public int GymId { get; private set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gymid"></param>
        public SearchGymDialog(int gymid, ControllerBase ctl)
        {
            InitializeComponent();

            GymId = gymid;
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;

            this.txtGymKana.EntryMode = (AplInfo.OP_ROMAN) ? CommonClass.ENTRYMODE.IMEON_ROMAN_HANKAKU_KANA : ENTRYMODE.IMEON_HANKAKU_KANA;
        }


        // *******************************************************************
        // イベント
        // *******************************************************************

        /// <summary>
        /// [フォーム] ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGymno_Load(object sender, EventArgs e)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), this.Text + "画面表示", 1);
            
            if (GymId != 0) { SearchGymId(); }
            txtGymId.Text = (GymId == 0) ? "" : GymId.ToString(Const.GYM_ID_LEN_STR);
        }

        /// <summary>
        /// [フォーム] クローズ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGymno_FormClosed(object sender, FormClosedEventArgs e)
        {
            GymId = DBConvert.ToIntNull(txtGymId.Text.Trim());
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), this.Text + "画面終了, 選択業務番号: " + GymId, 1);
        }

        /// <summary>
        /// [業務番号]テキスト KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ntbGymno_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Down:
                    txtGymKana.Text = "";
                    livGymList.Items.Clear();
                    SearchGymId();
					txtGymKana.Focus();
					break;
                case Keys.Escape:
                    txtGymId.Text = "";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// [業務カナ]テキスト KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ktbGymkana_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Down:
					if (!string.IsNullOrEmpty(txtGymKana.Text))
					{
						txtGymId.Text = "";
						livGymList.Items.Clear();
						SearchGymKana();
					}
					livGymList.Focus();
                    if (livGymList.Items.Count > 0)
                    {
                        livGymList.Items[0].Selected = true;
                        livGymList.Items[0].Focused = true;
                    }
                    break;
                case Keys.Up:
                    txtGymId.Focus();
                    break;
                case Keys.Escape:
                    txtGymKana.Text = "";
                    break;
                default:
                    break;
            }
        }

		/// <summary>
		/// [確定]ボタン クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFixed_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/// <summary>
		/// [キャンセル]ボタン クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnBack_Click(object sender, EventArgs e)
        {
			this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// [業務一覧]リスト ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvGym_DoubleClick(object sender, EventArgs e)
        {
			this.DialogResult = DialogResult.OK;
			this.Close();
        }

		/// <summary>
		/// [業務一覧] KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SearchGymno_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					this.DialogResult = DialogResult.OK;
					this.Close();
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// [業務一覧]リスト ItemSelectionChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvGym_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (livGymList.SelectedItems.Count == 0) { return; }
            txtGymId.Text = DBConvert.ToStringNull(livGymList.SelectedItems[0].SubItems[0].Text).PadLeft(Const.GYM_ID_LEN_5,'0');
            txtGymKana.Text = DBConvert.ToStringNull(livGymList.SelectedItems[0].SubItems[2].Text);
        }


        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 業務ＩＤ検索
        /// </summary>
        private void SearchGymId()
        {
            GymId = DBConvert.ToIntNull(txtGymId.Text.Trim());

			SortedList<int, TBL_GYM_PARAM> gym_params = GetGymDataId();
            if (gym_params.Count < 1)
            {
                ComMessageMgr.MessageWarning("業務番号：{0} が存在しません。", GymId.ToString(Const.GYM_ID_LEN_STR));
                txtGymId.Focus();
                return;
            }

            int cnt = 0;
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[gym_params.Count];
			foreach (TBL_GYM_PARAM gym in gym_params.Values)
			{
                listItem.Clear();
                listItem.Add(DBConvert.ToStringNull(gym._GYM_ID).PadLeft(Const.GYM_ID_LEN_5, '0'));
                listItem.Add(DBConvert.ToStringNull(gym.m_GYM_KANJI));
                listItem.Add(DBConvert.ToStringNull(gym.m_GYM_KANA));
                listView[cnt] = new ListViewItem(listItem.ToArray());
                cnt++;
            }
            this.livGymList.Items.Clear();
            this.livGymList.Items.AddRange(listView);
        }

        /// <summary>
        /// 業務カナ検索
        /// </summary>
        private void SearchGymKana()
        {
			SortedList<int, TBL_GYM_PARAM> gym_params = GetGymDataKana();
			if (gym_params.Count < 1)
			{
                ComMessageMgr.MessageWarning("業務番号：{0} が存在しません。", txtGymKana.Text);
                txtGymKana.Focus();
                return;
            }

			int cnt = 0;
            List<string> listItem = new List<string>();
            ListViewItem[] listView = new ListViewItem[gym_params.Count];
			foreach (TBL_GYM_PARAM gym in gym_params.Values)
			{
                listItem.Clear();
				listItem.Add(DBConvert.ToStringNull(gym._GYM_ID).PadLeft(Const.GYM_ID_LEN_5, '0'));
				listItem.Add(DBConvert.ToStringNull(gym.m_GYM_KANJI));
				listItem.Add(DBConvert.ToStringNull(gym.m_GYM_KANA));
				listView[cnt] = new ListViewItem(listItem.ToArray());
                cnt++;
            }
            this.livGymList.Items.Clear();
            this.livGymList.Items.AddRange(listView);
        }

		/// <summary>
		/// 検索（業務番号）
		/// </summary>
		private SortedList<int, TBL_GYM_PARAM> GetGymDataId()
        {
            // SELECT実行
            SortedList<int, TBL_GYM_PARAM> gym_params = new SortedList<int, TBL_GYM_PARAM>();
			using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
			{
				string strSQL = "";
				try
				{
					// SQL生成
					strSQL = "SELECT * FROM " + TBL_GYM_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
					if (GymId != 0)
					{
						strSQL += " WHERE " + TBL_GYM_PARAM.GYM_ID + "=" + GymId;
					}
					strSQL += " ORDER BY " + TBL_GYM_PARAM.GYM_ID;

					// 実行
					DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
					foreach (DataRow row in tbl.Rows)
					{
						TBL_GYM_PARAM gym = new TBL_GYM_PARAM(row, AppInfo.Setting.SchemaBankCD);
						gym_params.Add(gym._GYM_ID, gym);
					}
				}
				catch (Exception ex)
				{
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
			return gym_params;
		}

		/// <summary>
		/// 検索（業務名カナ）
		/// </summary>
		private SortedList<int, TBL_GYM_PARAM> GetGymDataKana()
		{
			// SELECT実行
			SortedList<int, TBL_GYM_PARAM> gym_params = new SortedList<int, TBL_GYM_PARAM>();
			using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
			{
				string strSQL = "";
				try
				{
					// SQL生成
					strSQL = "SELECT * FROM " + TBL_GYM_PARAM.TABLE_NAME(AppInfo.Setting.SchemaBankCD);
                    if (!txtGymKana.Text.Equals(""))
					{
						strSQL += " WHERE " + TBL_GYM_PARAM.GYM_KANA + " LIKE '%" + txtGymKana.Text + "%'";
					}
					strSQL += " ORDER BY " + TBL_GYM_PARAM.GYM_ID;

					// 実行
					DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
					foreach (DataRow row in tbl.Rows)
					{
						TBL_GYM_PARAM gym = new TBL_GYM_PARAM(row, AppInfo.Setting.SchemaBankCD);
						gym_params.Add(gym._GYM_ID, gym);
					}
				}
				catch (Exception ex)
				{
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
			return gym_params;
		}
	}
}
