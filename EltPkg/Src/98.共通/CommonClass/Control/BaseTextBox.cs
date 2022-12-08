using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace CommonClass
{
	/// <summary>
	/// UserControl1 の概要の説明です。
	/// </summary>
	public abstract class BaseTextBox : iBicsTextBox
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 直前に押されたキー情報
		/// </summary>
		private KeyEventArgs PreKey = null;

		[Category("制御")]
		[Description("Enter・Down・Upで項目遷移制御を行いたい場合trueを設定します。")]
		[DefaultValue(false)]
		public bool KeyControl { get; set; } = false;

		[Category("制御")]
		[Description("TabキーKey_Downイベントで検知させたい場合trueを設定します。KeyControlがtrueの場合は常にtrueと同じ動作となります。")]
		[DefaultValue(false)]
		public bool TabKeyControl { get; set; } = false;

		[Category("制御")]
		[Description("I_Validatingを発生させたい場合trueを設定します。")]
		[DefaultValue(true)]
		public bool RaiseIValidating { get; set; } = true;

		internal BaseTextBox() : base()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
		/// コード］エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion

		override protected void OnKeyDown(KeyEventArgs e)
		{
			if (!KeyControl)
			{
				// KeyControlがFalseの場合は通常処理
				base.OnKeyDown(e);
				return;
			}

			PreKey = e;
			switch (e.KeyCode)
			{
				case Keys.Enter:
				case Keys.Down:
					// Enter・Downは次コントロールに移動
					//SendKeys.Send("{TAB}");
					this.NextControl(true);
					e.Handled = true;
                    e.SuppressKeyPress = true;

                    break;
				case Keys.Up:
					// Upは前コントロールに移動
					// +({TAB})で[Shift+TAB]の意味になる
					//SendKeys.Send("+({TAB})");
					// +({TAB})だと親フォームで[Shift]が反応するため別の方針に変更
					this.NextControl(false);
					e.Handled = true;
					e.SuppressKeyPress = true;
					break;
				case Keys.Tab:
					bool forward = true;
					if (e.Shift) forward = false;
					this.NextControl(forward);
					e.Handled = true;
					e.SuppressKeyPress = true;
					break;
				default:
					base.OnKeyDown(e);
					break;
			}
			PreKey = null;
		}

		protected override bool IsInputKey(Keys keyData)
		{
			if (!KeyControl && !TabKeyControl)
			{
				// KeyControl・TabKeyControlがFalseの場合は通常処理
				return base.IsInputKey(keyData);
			}

			// Tab・Shift+Tab を KeyDownイベントで検知できるようにする
			switch (keyData)
			{
				case Keys.Tab:
				case Keys.Tab | Keys.Shift:
					break;
				default:
					return base.IsInputKey(keyData);
			}
			return true;
		}

		private void NextControl(bool forward)
        {
			this.Parent.SelectNextControl(this, forward, true, true, false);
		}

		#region カスタムイベント

		public event CancelEventHandler I_Validating;

		public bool RaiseI_Validating()
		{
			if (!RaiseIValidating)
			{
				return false;
			}
			CancelEventArgs e = new CancelEventArgs();
			I_Validating?.Invoke(this, e);

			return e.Cancel;
		}

		protected override void OnValidating(CancelEventArgs e)
		{
			if (!RaiseIValidating)
			{
				base.OnValidating(e);
				return;
			}

			if (PreKey != null)
            {
				switch (PreKey.KeyCode)
				{
					// Up・Shift+Tabはカスタムイベント発生なし
					case Keys.Up:
						break;
					case Keys.Tab:
						if (PreKey.Shift) break;
						I_Validating?.Invoke(this, e);
						break;
					default:
						I_Validating?.Invoke(this, e);
                        break;
				}
			}
            else
            {
				I_Validating?.Invoke(this, e);
			}

			base.OnValidating(e);
		}

		#endregion
	}
}
