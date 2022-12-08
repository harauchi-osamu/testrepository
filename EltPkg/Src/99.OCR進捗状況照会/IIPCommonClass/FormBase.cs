using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using IIPCommonClass.Log;
using IIPCommonClass.DB;

namespace IIPCommonClass
{
    /// <summary>
    /// 各フォームの外枠になるフォーム
    /// </summary>
    public partial class FormBase : UnclosableForm
    {


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormBase()
        {
            InitializeComponent();

            btnFunc = new Dictionary<int, Button>();
            btnFunc.Add(1, btnFunc01);
            btnFunc.Add(2, btnFunc02);
            btnFunc.Add(3, btnFunc03);
            btnFunc.Add(4, btnFunc04);
            btnFunc.Add(5, btnFunc05);
            btnFunc.Add(6, btnFunc06);
            btnFunc.Add(7, btnFunc07);
            btnFunc.Add(8, btnFunc08);
            btnFunc.Add(9, btnFunc09);
            btnFunc.Add(10, btnFunc10);
            btnFunc.Add(11, btnFunc11);
            btnFunc.Add(12, btnFunc12);

            statusLabel = new Dictionary<int, Label>();
            statusLabel.Add(0, statusLabel0);
            statusLabel.Add(1, statusLabel1);
            statusLabel.Add(2, statusLabel2);
            statusLabel.Add(3, statusLabel3);
            statusLabel.Add(4, statusLabel4);

            // 初期ではステータスラベルは0のみ
            SetStatusLabelFull(true);
        }

        #region メンバ
        /// <summary>
        /// ファンクションボタンリスト
        /// </summary>
        protected Dictionary<int, Button> btnFunc;

        /// <summary>
        /// ステータスラベルリスト
        /// </summary>
        protected Dictionary<int, Label> statusLabel;

        /// <summary>
        /// 業務番号
        /// </summary>
        protected int _gym_id;
        #endregion

        #region イベント

        /// <summary>
        /// フォームロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormBase_Load(object sender, EventArgs e)
        {
            initFormHeader();

            int[] rgbs = new int[3];
            int idx = 0;

            if (AplInfo.V_GYM_SETTING != null)
            {
                foreach (string str in AplInfo.V_GYM_SETTING._BACK_COLOR.Split(','))
                {
                    rgbs[idx] = Convert.ToInt16(str);
                    idx++;
                }
                if (rgbs.Length >= 3)
                {
                    this.BackColor = Color.FromArgb(rgbs[0], rgbs[1], rgbs[2]);
                }
            }
        }

        /// <summary>
        /// フォームクローズイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProcessLog.getInstance().writeLog(((Form)sender).Text + "画面終了");
        }

        /// <summary>
        /// 各パネルのMouseMoveイベントを集約したもの。overrideして各自定義。
        /// </summary>
        protected virtual void Panels_MouseMove(object sender, MouseEventArgs e) { }

        /// <summary>
        /// 各パネルのMouseUpイベントを集約したもの。overrideして各自定義。
        /// </summary>
        protected virtual void Panels_MouseUp(object sender, MouseEventArgs e) { }

        /// <summary>
        /// 各パネルのMouseDownイベントを集約したもの。overrideして各自定義。
        /// </summary>
        protected virtual void Panels_MouseDown(object sender, MouseEventArgs e) { }

        /// <summary>
        /// フォーム基盤_キーダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormBase_KeyDown(object sender, KeyEventArgs e)
        {
            
            switch (e.KeyCode)
            {
                case Keys.F1:
                    if (btnFunc01.Enabled) btnFunc01_Click(null, null);
                    break;
                case Keys.F2:
                    if (btnFunc02.Enabled) btnFunc02_Click(null, null);
                    break;
                case Keys.F3:
                    if (btnFunc03.Enabled) btnFunc03_Click(null, null);
                    break;
                case Keys.F4:
                    if (btnFunc04.Enabled) btnFunc04_Click(null, null);
                    break;
                case Keys.F5:
                    if (btnFunc05.Enabled) btnFunc05_Click(null, null);
                    break;
                case Keys.F6:
                    if (btnFunc06.Enabled) btnFunc06_Click(null, null);
                    break;
                case Keys.F7:
                    if (btnFunc07.Enabled) btnFunc07_Click(null, null);
                    break;
                case Keys.F8:
                    if (btnFunc08.Enabled) btnFunc08_Click(null, null);
                    break;
                case Keys.F9:
                    if (btnFunc09.Enabled) btnFunc09_Click(null, null);
                    break;
                case Keys.F10:
                    e.Handled = true;
                    if (btnFunc10.Enabled) btnFunc10_Click(null, null);
                    break;
                case Keys.F11:
                    if (btnFunc11.Enabled) btnFunc11_Click(null, null);
                    break;
                case Keys.F12:
                    if (btnFunc12.Enabled) btnFunc12_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        #region 各ファンクションボタンイベント

        protected virtual void btnFunc01_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            ProcessLog.getInstance().writeLog("F1:" + btnFunc01.Name + "ボタン押下");
        }

        protected virtual void btnFunc02_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            ProcessLog.getInstance().writeLog("F2:" + btnFunc02.Name + "ボタン押下");
        }

        protected virtual void btnFunc03_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            ProcessLog.getInstance().writeLog("F3:" + btnFunc03.Name + "ボタン押下");
        }

        protected virtual void btnFunc04_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            ProcessLog.getInstance().writeLog("F4:" + btnFunc04.Name + "ボタン押下");
        }

        protected virtual void btnFunc05_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            ProcessLog.getInstance().writeLog("F5:" + btnFunc05.Name + "ボタン押下");
        }

        protected virtual void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            ProcessLog.getInstance().writeLog("F6:" + btnFunc06.Name + "ボタン押下");
        }

        protected virtual void btnFunc07_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            ProcessLog.getInstance().writeLog("F7:" + btnFunc07.Name + "ボタン押下");
        }

        protected virtual void btnFunc08_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            ProcessLog.getInstance().writeLog("F8:" + btnFunc08.Name + "ボタン押下");
        }

        protected virtual void btnFunc09_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            ProcessLog.getInstance().writeLog("F9:" + btnFunc09.Name + "ボタン押下");
        }

        protected virtual void btnFunc10_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            ProcessLog.getInstance().writeLog("F10:" + btnFunc10.Name + "ボタン押下");
        }

        protected virtual void btnFunc11_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            ProcessLog.getInstance().writeLog("F11:" + btnFunc11.Name + "ボタン押下");
        }

        protected virtual void btnFunc12_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            ProcessLog.getInstance().writeLog("F12:" + btnFunc12.Name + "ボタン押下");
        }
        #endregion

        #endregion

        #region イベント処理詳細
        /// <summary>
        /// ヘッダー内容のセットと表示位置調整
        /// </summary>
        protected void initFormHeader()
        {
            lblDB.Text = "接続：" ;
            lblDate.Text = "処理日：" ;
        }


        protected void SetServerName(string  serName)
        {
            lblDB.Text = "接続：" + serName;
        }


        /// <summary>
        /// 処理日ラベルに業務番号から処理日をセット
        /// </summary>
        /// <param name="gymno"></param>
        protected void SetOpDateLabel(int _opeDate)
        {
            lblDate.Text = "処理日：" + String.Format("{0:D4}/{1:D2}/{2:D2}", _opeDate / 10000, _opeDate % 10000 / 100, _opeDate % 10000 % 100);
        }

  //      /// <summary>
  //      /// 日初処理後の処理日ラベルに再描画(処理日をセット)
  //      /// </summary>
  //      /// <param name="gymno"></param>
  //      protected void Refresh_OpDateLabel(int gymno, int date)
  //      {
  //          lblDate.Text = "処理日：" + String.Format("{0:D4}/{1:D2}/{2:D2}", Convert.ToInt32(date) / 10000, Convert.ToInt32(date) % 10000 / 100, Convert.ToInt32(date) % 10000 % 100);
  //      }

		///// <summary>
		///// 処理日ラベルに業務番号から処理日をセット
		///// </summary>
		///// <param name="gymno"></param>
		//protected void ClearOpDateLabel()
		//{
		//	lblDate.Text = "処理日：                ";
		//}

        /// <summary>
        /// ステータスバー全体にわたるラベルにするか、４つのラベルにするか
        /// </summary>
        /// <param name="val">全体でひとつのラベルのときtrue</param>
        protected void SetStatusLabelFull(bool val)
        {
            statusLabel[0].Visible = val;
            statusLabel[1].Visible = !val;
            statusLabel[2].Visible = !val;
            statusLabel[3].Visible = !val;
            statusLabel[4].Visible = !val;
        }

        /// <summary>
        /// ステータスバーのラベルに出力
        /// </summary>
        /// <param name="no">ラベル番号</param>
        /// <param name="msg">出力テキスト</param>
        /// <param name="clr">ラベル背景色</param>
        public void SetStatusMessage(int no, string msg, Color clr)
        {
            if (0 <= no && no < statusLabel.Count)
            {
                statusLabel[no].Text = msg;
                statusLabel[no].BackColor = clr;
            }
        }

        /// <summary>
        /// ステータスバーのラベルに出力
        /// </summary>
        /// <param name="msg">出力テキスト</param>
        public void SetStatusMessage(string msg, Color clr)
        {
            if (statusLabel0.Visible) { SetStatusMessage(0, msg, clr); }
            else { SetStatusMessage(1, msg, clr); }
        }

        /// <summary>
        /// ステータスバーのラベルに出力（背景色Color.Salmon）
        /// </summary>
        /// <param name="msg">出力テキスト</param>
        public void SetStatusMessage(string msg)
        {
            if (statusLabel0.Visible) { SetStatusMessage(0, msg, Color.Salmon); }
            else { SetStatusMessage(1, msg, Color.Salmon); }
        }

        /// <summary>
        /// ステータスバーのラベルをクリア
        /// </summary>
        /// <param name="no">ラベル番号</param>
        protected void ClearStatusMessage(int no)
        {
            if (0 <= no && no < statusLabel.Count)
            {
                statusLabel[no].Text = "";
                statusLabel[no].BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// ステータスバーのラベルをクリア
        /// 消すのは0と1
        /// </summary>
        protected void ClearStatusMessage()
        {
            ClearStatusMessage(0);
            ClearStatusMessage(1);
        }
        #endregion

        #region 画面共通処理
        /// <summary>
		/// 画面初期化処理
		/// </summary>
		protected void DspInit()
		{
			// 画面名称設定
			this.SetDspName();
			// ステータスバークリア
			this.ClearStatusMessage();
			// 画面アイテム設定
			this.SetDspItem();
			// ボタンの初期設定
			this.DefaultEnabledButtons();
			// 画面毎のボタン設定
			this.EnabledButtons(0);

		}

		/// <summary>
		/// 画面更新処理
		/// </summary>
		protected void DspUpdate()
		{
            // 画面更新処理
            this.DspInit();
		}

		/// <summary>
		/// ボタン制御（DEFAULT設定）
		/// </summary>
		protected void DefaultEnabledButtons()
		{
			// シフトキーチェック
			if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
			{
				this.SetFunctionButtonName(true);
			}
			else
			{
				this.SetFunctionButtonName(false);
			}

			foreach (Button bt in btnFunc.Values)
			{
				bt.Enabled = false;
			}

			// 画面終了
			btnFunc[1].Enabled = true;
			// 画面更新
			//btnFunc[4].Enabled = true;
		}

		/// <summary>
		/// メッセージボックス表示
		/// </summary>
		/// <param name="msg_id"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		protected bool ShowMessageBox(string msg_id, string msg)
		{
			if (msg.Equals(""))
			{
				if (IniFileAccess.ShowMsgBox(msg_id) == DialogResult.Yes)
				{
					return true;
				}
			}
			else
			{
				if (IniFileAccess.ShowMsgBox(msg_id, msg) == DialogResult.Yes)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// ボタン名変更処理
		/// </summary>
		/// <param name="e"></param>
		/// <param name="isPress"></param>
		protected void ChangeButtonName(KeyEventArgs e, bool isPress)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					break;
				case Keys.ShiftKey:
					// ボタン名設定
					this.SetFunctionButtonName(isPress);
					break;
				default:
					break;
			}

			// ボタン初期表示
			this.DefaultEnabledButtons();

			// ボタン制御
			this.EnabledButtons(0);
		}

		#region virtual_method

		/// <summary>
		/// 画面名称設定
		/// </summary>
		/// <param name="_gym_id"></param>
		protected virtual void SetDspName() { }

		/// <summary>
		/// 画面アイテム設定
		/// </summary>
		protected virtual void SetDspItem()
        {
            
        }

		/// <summary>
		/// FUNCTIONボタン名設定
		/// </summary>
		/// <param name="is_press"></param>
		protected virtual void SetFunctionButtonName(bool is_press) { }

		/// <summary>
		/// 画面毎のボタン設定
		/// </summary>
		/// <param name="status"></param>
		protected virtual void EnabledButtons(int status) { }

		#endregion

        /// <summary>
        /// メッセージ出力
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="btn"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public bool ShowBox(string text, string caption, MessageBoxButtons btn, MessageBoxIcon icon, DialogResult dr)
        {
            if (MessageBox.Show(this, text, caption, btn, icon) == dr)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
