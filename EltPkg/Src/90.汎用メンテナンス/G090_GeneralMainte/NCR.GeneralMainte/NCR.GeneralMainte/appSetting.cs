using System;

namespace NCR.GeneralMainte
{
	/// <summary>
	/// appSetting の概要の説明です。
	/// </summary>
	public class appSetting
	{
		private string schemaName;
		private string tableName;
		private bool intransaction;
		private int inputstart;
		private int inputmode;
		private int inputcolorderwidth;
		private int inputcolnamewidth;
		private int inputcolvaluewidth;
		private int inputcoldefaultvaluewidth;
		private string currentdata;
		private int gridmode;
		private bool aftermenu;
		private string connectionstring;
		private string titlefont;
		private float titlefontsize;
		private string bodyfont;
		private float bodyfontsize;
		private int reportfielddefaultsize;

		private string[] mHistory = new string[5];

		public appSetting()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		public string[] History{
			get{return mHistory;}
			set{mHistory = value;}
		
		}

		/// <summary>
		/// メンテナンス対象となるスキーマ名
		/// </summary>
		public string SchemaName
		{
			get
			{
				return schemaName;
			}
			set
			{
				schemaName = value;
			}
		}

		/// <summary>
		/// メンテナンス対象となるテーブル名
		/// </summary>
		public string TableName
		{
			get
			{
				return tableName;
			}
			set
			{
				tableName = value;
			}
		}

        public string SchemaTableName { get; set; }

        /// <summary>
        /// トランザクション実行中
        /// </summary>
        public bool InTransaction
		{
			get
			{
				return intransaction;
			}
			set
			{
				intransaction = value;
			}
		}

		/// <summary>
		/// 入力フォーム開始行。検索条件、並替条件の開始行は固定
		/// </summary>
		public int InputStart
		{
			get
			{
				return inputstart;
			}
			set
			{
				inputstart = value;
			}
		}

		/// <summary>
		/// 入力モード
		/// </summary>
		public int InputMode
		{
			get
			{
				return inputmode;
			}
			set
			{
				inputmode = value;
			}
		}

		/// <summary>
		/// 入力フォーム列順項目の幅
		/// </summary>
		public int InputColOrderWidth
		{
			get
			{
				return inputcolorderwidth;
			}
			set
			{
				inputcolorderwidth = value;
			}
		}

		/// <summary>
		/// 入力フォーム列名項目の幅
		/// </summary>
		public int InputColNameWidth
		{
			get
			{
				return inputcolnamewidth;
			}
			set
			{
				inputcolnamewidth = value;
			}
		}
	
		/// <summary>
		/// 入力フォーム値項目の幅
		/// </summary>
		public int InputColValueWidth
		{
			get
			{
				return inputcolvaluewidth;
			}
			set
			{
				inputcolvaluewidth = value;
			}
		}
		/// <summary>
		/// 入力フォーム既定値項目の幅
		/// </summary>
		public int InputColDefaultValueWidth
		{
			get
			{
				return inputcoldefaultvaluewidth;
			}
			set
			{
				inputcoldefaultvaluewidth = value;
			}
		}

		/// <summary>
		/// 現在の編集中のデータ
		/// </summary>
		public string CurrentData
		{
			get
			{
				return currentdata;
			}
			set
			{
				currentdata = value;
			}
		}

		/// <summary>
		/// dataGridの表示モード
		/// </summary>
		public int GridMode
		{
			get
			{
				return gridmode;
			}
			set
			{
				gridmode = value;
			}
		}

		/// <summary>
		/// メニュー操作後
		/// </summary>
		public bool AfterMenu
		{
			get
			{
				return aftermenu;
			}
			set
			{
				aftermenu = value;
			}
		}

		/// <summary>
		/// コネクションオブジェクトの接続文字列
		/// </summary>
		public string ConnectionString
		{
			get
			{
				return connectionstring;
			}
			set
			{
				connectionstring = value;
			}
		}

		/// <summary>
		/// レポートタイトルのフォント名
		/// </summary>
		public string TitleFont
		{
			get
			{
				return titlefont;
			}
			set
			{
				titlefont = value;
			}
		}

		/// <summary>
		/// レポートタイトルのフォントサイズ
		/// </summary>
		public float TitleFontSize
		{
			get
			{
				return titlefontsize;
			}
			set
			{
				titlefontsize = value;
			}
		}

		/// <summary>
		/// レポート本文のフォント名
		/// </summary>
		public string BodyFont
		{
			get
			{
				return bodyfont;
			}
			set
			{
				bodyfont = value;
			}
		}

		/// <summary>
		/// レポート本文のフォントサイズ
		/// </summary>
		public float BodyFontSize
		{
			get
			{
				return bodyfontsize;
			}
			set
			{
				bodyfontsize = value;
			}
		}

		/// <summary>
		/// レポート列既定サイズ
		/// </summary>
		public int ReportFieldDefaultSize
		{
			get
			{
				return reportfielddefaultsize;
			}
			set
			{
				reportfielddefaultsize = value;
			}
		}
	}
}
