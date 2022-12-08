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
	public abstract class DTextBox1 : iBicsTextBox
	{

		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal DTextBox1() : base()
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

		override protected void OnKeyPress(KeyPressEventArgs e)
		{

			switch (e.KeyChar)
			{
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case (char)0x8:
					base.OnKeyPress(e);
					break;
				default:
					e.Handled = true;
					break;
			}

		}

		override protected void OnKeyDown(KeyEventArgs e)
		{

			//			base.OnKeyDown(e);		
			switch (e.KeyCode)
			{
				case Keys.D0:
				case Keys.D1:
				case Keys.D2:
				case Keys.D3:
				case Keys.D4:
				case Keys.D5:
				case Keys.D6:
				case Keys.D7:
				case Keys.D8:
				case Keys.D9:
				case Keys.NumPad0:
				case Keys.NumPad1:
				case Keys.NumPad2:
				case Keys.NumPad3:
				case Keys.NumPad4:
				case Keys.NumPad5:
				case Keys.NumPad6:
				case Keys.NumPad7:
				case Keys.NumPad8:
				case Keys.NumPad9:
				case Keys.F1:
				case Keys.F2:
				case Keys.F3:
				case Keys.F4:
				case Keys.F5:
				case Keys.F6:
				case Keys.F7:
				case Keys.F8:
				case Keys.F9:
				case Keys.F10:
				case Keys.F11:
				case Keys.F12:
				case Keys.Left:
				case Keys.Right:
				case Keys.Insert:
				case Keys.Delete:
				case Keys.Home:
				case Keys.End:
				case Keys.PageUp:
				case Keys.PageDown:
				case Keys.Back:
				case Keys.Alt:
				case Keys.ShiftKey:
				case Keys.ControlKey:
				case Keys.Escape:
				case Keys.PrintScreen:
				case Keys.Space:
				case Keys.Enter:
				case Keys.Up:
				case Keys.Down:
				case Keys.Decimal:
					base.OnKeyDown(e);
					break;
				default:
					e.Handled = true;
					Win32.iBicsBeep();
					break;
			}
		}

		override protected void OnEnter(EventArgs e)
		{

			this.ImeMode = ImeMode.Disable;         /*コンストラクタから移動*/

			if (this.Text.Length != 0 && this.AllSelect)
			{
				this.SelectionStart = 0;
				this.SelectionLength = this.Text.Length;
				this.SelectAll();
			}
			base.OnEnter(e);

		}

		internal protected string trimChar(string var, char c)
		{
			string str = var;
			int i;
			if (var.Length != 0)
			{
				while (true)
				{
					i = str.IndexOf(c);
					if (i == -1)
					{
						break;
					}
					else
					{
						str = str.Remove(i, 1);
					}
				}
			}
			return str;
		}

		#region 最大桁入力後の自動ジャンプ制御有無

		protected bool m_IsMaxInputSelectNext = false;

		/// <summary>
		/// 最大桁入力後にジャンプするかどうかどうかを示します。（未実装）
		/// </summary>
		[Category("動作")]
		[Description("最大桁入力後にジャンプするかどうかどうかを示します。（未実装）")]
		[DefaultValue(false)]
		public bool IsMaxInputSelectNext
		{
			get
			{
				return m_IsMaxInputSelectNext;
			}
			set
			{
				m_IsMaxInputSelectNext = value;
			}
		}

		#endregion
	}
}
