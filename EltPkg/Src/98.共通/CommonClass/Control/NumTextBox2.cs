using System;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;

namespace CommonClass
{
	/// <summary>
	/// 数字入力コントロール
	/// </summary>
    public class NumTextBox2 : NumTextBox1
    {

		private const long MAXVALUE = 9223372036854775807L;
		private bool m_bComma;
		private bool m_LeftZero;
        private bool m_AutoZeroSet;

        /// <summary>
        /// 数字の桁にコンマを自動挿入するかどうかを示します。
        /// </summary>
        [Category("表示")]
        [Description("数字の桁にコンマを自動挿入するかどうかを示します。")]
        [DefaultValue(false)]
		public bool Comma {
			set { m_bComma = value; }
			get { return m_bComma; }
		}

        /// <summary>
        /// 最大桁まで左に0を埋めるかどうかを示します。
        /// </summary>
        [Category("表示")]
        [Description("最大桁まで左に0を埋めるかどうかを示します。")]
        [DefaultValue(false)]
        public bool LeftZero
        {
			set { m_LeftZero = value; }
			get { return m_LeftZero; }
		}

        /// <summary>
        /// 入力が空欄のとき、自動的に0に置き換えるかどうかを示します。
        /// </summary>
        [Category("表示")]
        [Description("入力が空欄のとき、自動的に0に置き換えるかどうかを示します。")]
        [DefaultValue(false)]
        public bool AutoZeroSet
        {
            set { m_AutoZeroSet = value; }
            get { return m_AutoZeroSet; }
        }

		/// <summary>
		/// OnEnter時にCommaを除外するか設定します
		/// </summary>
		[Category("表示")]
		[Description("OnEnter時にCommaを除外するか設定します")]
		[DefaultValue(false)]
		public bool OnEnterSeparatorCut { get; set; } = false;

		public NumTextBox2() : base(KEYDOWNMODE.NUMBER)
        {
		}
		
		override public string Text {
			set {
				string fugo = "";
				if ( value.Length != 0 ) {
					if( value.Substring(0,1) == "-" && value.Length != 1 ){
						fugo = "-";
						value = value.Substring(1,value.Length-1);
					}
					for (int i=0; i<value.Length; i++ ) {						
						if ( value[i] < '0' || value[i] > '9' ) {
							if ( (value[i] != ',') && value[i] != '.') {
								value = "";
								break;
							}
						}
					}
					value = fugo + value;
				}
				if ( value.Length != 0 ) {
					string str = value;
                    str = trimChar(str, ',');
                    str = trimChar(str, '.');
                    long lng = Convert.ToInt64(str);
					if ( lng > MAXVALUE || lng < -(MAXVALUE) ) {
						value = "";
					} else {
						plusComma( ref value );
						plusZero( ref value);
					}
				}
				base.Text = value;
			}
            get {
                if ((base.Text.Length == 0) && this.Enabled && m_AutoZeroSet)
                {
                    return "0";
                }
                return base.Text;
            }

		}
		public void setText( int var ){
			string str = var.ToString();
			plusComma( ref str );
			plusZero( ref str );
			this.Text = str;		
		}
		public void setText( long var ) {
			string str = var.ToString();
			plusComma( ref str );
			plusZero( ref str );
			this.Text = str;		
		}
		
		public void setText( string var ) {
			try {
				if ( var.Length > 0 ){
					long l = Convert.ToInt64(var);
					setText(l);
				}else{
					this.Text = "";
				}
			} catch(System.FormatException ) {
			}
		}

		override protected void OnEnter(EventArgs e)
		{
			if (OnEnterSeparatorCut) base.Text = ToString();
			base.OnEnter(e);
		}

		override protected void OnLeave( EventArgs e) {
			
			string str = this.Text;

			plusComma( ref str );
			
			plusZero( ref str );

			this.Text = str;
			
			base.OnLeave(e);
		}

		private void plusComma( ref string var ) {

			string str = String.Copy(var);

			long iVal = 0;
			if ( (str.Length != 0) && m_bComma ) {

				var = trimChar( str, ',' );
				CultureInfo CultInfo = new CultureInfo("ja-JP");
				NumberFormatInfo nfi = CultInfo.NumberFormat;
				nfi.NumberDecimalDigits = 0;
				try {
					iVal = long.Parse(str, NumberStyles.Number, nfi);
				} catch ( OverflowException  ) {
					iVal = 0;
				} catch ( FormatException ) {
					iVal = 0;
				} finally {
					var = iVal.ToString( "N", nfi);
				}
			}
		}

		private void plusZero( ref string var ){
			
			if ( this.m_LeftZero ) {
				for(int i=var.Length;i<this.MaxLength;i++){
					var = "0" + var;
				}
			}

		}

		public long getLong() {
			long lRtn = 0;
			if ( base.Text.Length != 0 ) {
				string str	= trimChar( base.Text, ',' );
				lRtn = long.Parse(str);
			}
			return lRtn;
		}

		public int getInt() {
			int iRtn = 0;
			if ( base.Text.Length != 0 ) {
				string str	= trimChar( base.Text, ',' );
				iRtn = int.Parse(str);
			}
			return iRtn;
		}

		override public string ToString() {
			string strRet;
			string strTrim;
			strTrim = base.Text.Trim();

			if ( strTrim == "" || strTrim == null ){
				strRet = strTrim;
			}else{
				long l = getLong();
				strRet = l.ToString();
			}
			return strRet;
		}
	}
}
