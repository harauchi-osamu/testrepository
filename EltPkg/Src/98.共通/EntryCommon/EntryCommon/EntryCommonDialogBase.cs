using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;

namespace EntryCommon
{
    public partial class EntryCommonDialogBase : UnclosableForm
    {
        protected ControllerBase _ctlb = null;

        /// <summary>Shiftキーが押されているかどうか</summary>
        protected bool IsPressShiftKey { get { return (((Control.ModifierKeys & Keys.Shift) == Keys.Shift)); } }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EntryCommonDialogBase()
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

            // 画面描画
            ResetForm();
        }

        /// <summary>
        /// ステータスバーのラベルに出力（背景色Color.Salmon）
        /// </summary>
        /// <param name="msg">出力テキスト</param>
        protected void SetStatusMessage(string msg)
        {
            lblStatus.Text = msg;
            lblStatus.BackColor = Color.Salmon;
        }

        /// <summary>
        /// ステータスバーのラベルに出力（背景色Color.Salmon）
        /// </summary>
        /// <param name="msg">出力テキスト</param>
        protected void SetStatusMessage(string msg, Color backColor)
        {
            lblStatus.Text = msg;
            lblStatus.BackColor = backColor;
        }

        /// <summary>
        /// ステータスバーのラベルをクリア
        /// </summary>
        /// <param name="no">ラベル番号</param>
        protected void ClearStatusMessage()
        {
            lblStatus.Text = string.Empty;
            lblStatus.BackColor = SystemColors.Control;
        }


        // *******************************************************************
        // 継承メソッド
        // *******************************************************************

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
        /// メッセージを表示する
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="icon"></param>
        protected void ShowDialogMessage(string msg, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            switch (icon)
            {
                case MessageBoxIcon.Information:
                    MessageBox.Show(msg, "情報", MessageBoxButtons.OK, icon);
                    break;
                case MessageBoxIcon.Warning:
                    MessageBox.Show(msg, "通知", MessageBoxButtons.OK, icon);
                    break;
                case MessageBoxIcon.Error:
                    MessageBox.Show(msg, "エラー", MessageBoxButtons.OK, icon);
                    break;
            }
        }


        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

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

    }
}
