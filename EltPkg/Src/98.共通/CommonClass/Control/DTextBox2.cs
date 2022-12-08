using System;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;

namespace CommonClass
{
	/// <summary>
	/// 数字入力コントロール
	/// </summary>
	public class DTextBox2 : NumTextBox1
	{
		private const int ConstMaxLength = 8;

		/// <summary>
		/// 日付の区切り文字を示します。
		/// </summary>
		[Category("表示")]
		[Description("日付の区切り文字を示します。")]
		[DefaultValue('.')]
		public char DateSeparator { get; set; } = '.';

		/// <summary>
		/// OnEnter時に区切り文字を除外するか設定します
		/// </summary>
		[Category("表示")]
		[Description("OnEnter時に区切り文字を除外するか設定します")]
		[DefaultValue(false)]
		public bool OnEnterSeparatorCut { get; set; } = false;

		/// <summary>
		/// MaxLength設定
		/// </summary>
		override public int MaxLength
        {
            set
            {
                if (ConstMaxLength != value)
                {
                    MessageBox.Show(String.Format("MaxLengthは{0}のみ有効", ConstMaxLength));
                }
			}
        }

		public DTextBox2() : base(KEYDOWNMODE.DATE)
		{
			base.MaxLength = ConstMaxLength;
		}

		override public string Text
		{
			set { base.Text = ConvText(value); }
			get
			{
				if (base.Text.Length == 0) return "";
				return base.Text;
			}
		}

		public void setText(int var)
		{
			this.Text = ConvText(var.ToString());
		}
		public void setText(long var)
		{
			this.Text = ConvText(var.ToString());
		}

		public void setText(string var)
		{
			this.Text = ConvText(var.ToString());
		}

		override protected void OnEnter(EventArgs e)
		{
			if (OnEnterSeparatorCut) base.Text = ToString();
			base.OnEnter(e);
		}

		override protected void OnLeave(EventArgs e)
		{
			this.Text = ConvText(this.Text.ToString());

			base.OnLeave(e);
		}

		private string ConvText(string var)
		{
			if (var.Length != 0)
			{
				//数値とSeparatorのみ有効
				for (int i = 0; i < var.Length; i++)
				{
					if (var[i] < '0' || var[i] > '9')
					{
						if (var[i] != DateSeparator)
						{
							var = "";
							break;
						}
					}
				}
			}
			plusDateSeparator(ref var);
			return var;
		}

		private void plusDateSeparator(ref string var)
		{
			string str = String.Copy(var);

			if (str.Length != 0)
			{
				string strWk = trimChar(str, DateSeparator);

				try
				{
					long lRtn = 0;
                    long.TryParse(strWk, out lRtn);
                    strWk = lRtn.ToString("00000000");
                    strWk = strWk.Insert(6, DateSeparator.ToString());
					strWk = strWk.Insert(4, DateSeparator.ToString());
				}
				catch (Exception)
				{
					strWk = "";
				}
				finally
				{
					var = strWk;
				}
			}
		}

		public long getLong()
		{
			long lRtn = 0;
			if (base.Text.Length != 0)
			{
				string str = trimChar(base.Text, DateSeparator);
                long.TryParse(str, out lRtn);
            }
            return lRtn;
		}

		public int getInt()
		{
			int iRtn = 0;
			if (base.Text.Length != 0)
			{
				string str = trimChar(base.Text, DateSeparator);
                int.TryParse(str, out iRtn);
            }
            return iRtn;
		}

        override public string ToString()
        {
            string strRet;
            string strTrim;
            strTrim = base.Text.Trim();

            if (strTrim == "" || strTrim == null)
            {
                strRet = strTrim;
            }
            else
            {
                long l = getLong();
                strRet = l.ToString();
            }
            return strRet;
        }

	}
}
