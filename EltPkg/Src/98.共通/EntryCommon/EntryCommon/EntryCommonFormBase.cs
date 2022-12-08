using System;
using System.Drawing;
using System.Windows.Forms;

namespace EntryCommon
{
    public partial class EntryCommonFormBase : CommonClass.FormBase
    {
        protected ControllerBase _ctlb = null;

        /// <summary>Shiftキーが押されているかどうか</summary>
        protected bool IsPressShiftKey { get { return (((Control.ModifierKeys & Keys.Shift) == Keys.Shift)); } }

        /// <summary>Ctrlキーが押されているかどうか</summary>
        protected bool IsPressCtrlKey { get { return (((Control.ModifierKeys & Keys.Control) == Keys.Control)); } }

        /// <summary>Altキーが押されているかどうか</summary>
        protected bool IsPressAltKey { get { return ((Control.ModifierKeys & Keys.Alt) == Keys.Alt); } }

        /// <summary>Ctrl, Shift, Altキーが押されていないかどうか</summary>
        protected bool IsNotPressCSAKey { get { return (!IsPressShiftKey && !IsPressCtrlKey && !IsPressAltKey); } }

        /// <summary>ファンクション制御無効</summary>
        protected bool IsDisableFunction { get; private set; } = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EntryCommonFormBase()
        {
            InitializeComponent();
        }


        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// フォームを初期化する
        /// </summary>
        public virtual void InitializeForm(ControllerBase ctl)
		{
			_ctlb = ctl;

			// 処理日
			SetOpDateLabel(0);

			// 画面名設定
			SetDispName1("");
			SetDispName2("");

			// ファンクションキー初期化
			InitializeFunction();

			// 画面ID
			SetGamenID();

			// 画面描画
			ResetForm();
		}


		// *******************************************************************
		// 継承メソッド
		// *******************************************************************

		/// <summary>
		/// 画面名1を設定する
		/// </summary>
		/// <param name="dispName"></param>
		protected virtual void SetDispName1(string dispName)
		{
			this.lblDispName1.Text = dispName;
		}

		/// <summary>
		/// 画面名2を設定する
		/// </summary>
		/// <param name="dispName"></param>
		protected virtual void SetDispName2(string dispName)
		{
			this.lblDispName2.Text = dispName;
		}

		/// <summary>
		/// 画面を設定する
		/// </summary>
		/// <param name="ID"></param>
		protected void SetGamenID()
		{
			base.SetGamen(_ctlb.MenuNumber);
		}

		/// <summary>
		/// ファンクションキーを設定する
		/// </summary>
		protected virtual void InitializeFunction()
		{
			// すべてクリア
			for (int i = 0; i < 12; i++)
			{
                SetFunctionName(i + 1, string.Empty);
            }
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected virtual void SetFunctionState()
        {
        }

        /// <summary>
        /// フォームを再描画する
        /// </summary>
        public virtual void ResetForm()
        {
            // 画面表示データ更新
            RefreshDisplayData();

            // 画面表示状態更新
            RefreshDisplayState();
        }

        /// <summary>
        /// 画面表示データ更新
        /// </summary>
        protected virtual void RefreshDisplayData() {; }

        /// <summary>
        /// 画面表示状態更新
        /// </summary>
        protected virtual void RefreshDisplayState() {; }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected virtual void SetDisplayParams() {; }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected virtual bool GetDisplayParams() { return false; }

        /// <summary>
        /// Shift/Ctrlキーでファンクションキーを切り替える
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected virtual bool ChangeFunction(KeyEventArgs e)
        {
            if (IsDisableFunction) return false;

            switch (e.KeyCode)
            {
                case Keys.ShiftKey:
                case Keys.ControlKey:
                    InitializeFunction();
                    return true;
            }
            return false;
        }

        /// <summary>
        /// カーソルを待機状態にする
        /// </summary>
        protected void SetWaitCursor(bool flg = true)
        {
            this.Cursor = Cursors.WaitCursor;
            SetDisableFunction(flg);
        }

        /// <summary>
        /// カーソルを通常状態に戻す
        /// </summary>
        public override void ResetCursor()
        {
            ResetCursor(false);
        }

        /// <summary>
        /// カーソルを通常状態に戻す
        /// </summary>
        public void ResetCursor(bool flg)
        {
            this.Cursor = Cursors.Default;
            SetDisableFunction(flg);
        }

        /// <summary>
        /// 処理中状態に設定する
        /// </summary>
        protected void StartProcess(string msg)
        {
            SetWaitCursor();
            this.SetStatusMessage(msg, System.Drawing.Color.LightGreen);
            this.Refresh();
        }

        /// <summary>
        /// 処理中状態を解除する
        /// </summary>
        protected void EndProcess()
        {
            this.ClearStatusMessage();
            ResetCursor();
        }

        /// <summary>
        /// ファンクション制御無効設定
        /// </summary>
        protected void SetDisableFunction(bool flg)
        {
            IsDisableFunction = flg;
        }

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        /// <param name="no"></param>
        /// <param name="name"></param>
        /// <param name="autoEnabled"></param>
        /// <param name="fontSize"></param>
        protected void SetFunctionName(int no, string name, bool autoEnabled = true, Single fontSize = Const.FONT_SIZE_FUNC_DEF)
        {
            // このメソッド直後に SetFunctionState() を呼ぶ場合は、Shift押下時にボタン描画がちらつくので autoEnabled をfalseに設定する
            bool isEmpty = string.IsNullOrEmpty(name);
            if (isEmpty)
            {
                this.btnFunc[no].Text = string.Empty;
            }
            else
            {
                this.btnFunc[no].Text = string.Format("F{0}: {1}", no, name);
            }
            // ボタン名が設定されたら自動的に活性化する
            if (autoEnabled)
            {
                SetFunctionState(no, !isEmpty);
            }
            this.btnFunc[no].Font = new Font(Const.FONT_NAME_DEF, fontSize);
        }

        /// <summary>
        /// ファンクションキー状態をすべて非活性にする
        /// </summary>
        /// <param name="isF1Enabeled">「F1キー」を活性にするかどうか</param>
        protected void DisableAllFunctionState(bool isF1Enabeled = false)
        {
            for (int i = 0; i < 12; i++)
            {
                // 「F1:終了」は非活性にしない
                if (isF1Enabeled && i == 0)
                {
                    continue;
                }
                SetFunctionState(i + 1, false);
            }
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        /// <param name="no"></param>
        /// <param name="enabled"></param>
        protected void SetFunctionState(int no, bool enabled)
        {
            this.btnFunc[no].Enabled = enabled;
        }

        /// <summary>
        /// フォーカスがあたったら背景を緑色にする
        /// </summary>
        /// <param name="ctrl"></param>
        protected void SetFocusBackColor(Control ctrl)
        {
            ctrl.BackColor = Color.LightGreen;
        }

        /// <summary>
        /// フォーカスが外れたら背景を白色にする
        /// </summary>
        /// <param name="ctrl"></param>
        protected void RemoveFocusBackColor(Control ctrl)
        {
            ctrl.BackColor = SystemColors.Window;
        }

        /// <summary>
        /// 背景を灰色にする
        /// </summary>
        /// <param name="ctrl"></param>
        protected void ReadOnlyBackColor(Control ctrl)
        {
            ctrl.BackColor = SystemColors.Control;
        }

        /// <summary>
        /// 次のコントロールにフォーカスする
        /// </summary>
        /// <param name="curControl"></param>
        /// <param name="nextControl"></param>
        /// <returns></returns>
        protected bool SetNextFocus(Control curControl, Control nextControl)
        {
            if (!curControl.Focused) { return false; }

            // 次のコントロール
            nextControl.Focus();
            if (nextControl is TextBox)
            {
                ((TextBox)nextControl).SelectAll();
            }
            return true;
        }

        /// <summary>
        /// テキストボックスにフォーカスして選択する
        /// </summary>
        /// <param name="tb"></param>
        protected void SetTextFocus(TextBox tb)
        {
            tb.Focus();
            tb.SelectAll();
        }

        /// <summary>
        /// Validationを無効にしてフォーカス遷移
        /// </summary>
        /// <param name="tb"></param>
        protected void SetFocusValidation(Control control)
        {
            Control Active = this.ActiveControl;
            bool org = false;
            if (Active?.CausesValidation != null)
            {
                org = Active.CausesValidation;
                Active.CausesValidation = false;
            }
            control.Focus();
            if (Active?.CausesValidation != null)
            {
                Active.CausesValidation = org;
            }
        }

    }
}
