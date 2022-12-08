using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.Odbc; 
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using CrystalDecisions.CrystalReports.Engine;
using Common;
using CommonClass.DB;
using CommonTable.DB;
using System.Data.Common;

namespace NCR.GeneralMainte
{
	/// <summary>
	/// Form1 の概要の説明です。
	/// </summary>
	public class mainForm : System.Windows.Forms.Form
	{
		private static string[] args;
		protected appSetting ast;

		protected DbConnection conn;
		protected DbTransaction tran;
		protected DbDataAdapter adapter;

		protected DataSet tableSet;
		private DataRowView inputDrv;

		private Font titleFont;
		private Font bodyFont;
		private Font bodyColumnFont;
		private int reportRec;
		private int nowPage;
		private int sortSeq;
		private int searchSeq;
		private string headerToday;

		private System.Windows.Forms.MainMenu mainFormMenu;
        private System.Windows.Forms.DataGrid dataGrid;
        private IContainer components;
        private Panel panel1;
        private Button DelButton;
        private Button EditButton;
        private Button AddButton;
        private Button PrintButton;
        private Button EndButton;
        private GroupBox groupBox2;
        private Button SearchResultPrintButton;
        private Button InputConditionButton;
        private GroupBox groupBox1;
        private Button FinalizeButton;

        private ArrayList mDelArray;
        private UpdateList updateList1;

        public DataSet PrintWorkObject;
        private string mPrintWorkHeader = "";
        private SearchList searchList1;
        private new Button CancelButton;
        private Panel panel2;
        private bool mhaskey;

        private DataManipulationForm dmf;
        protected AppController m_appController;
        protected ICheck m_ICheck;
        protected string[] m_LocalOrders = null;
        private Button InputOrderButton;
        protected IInitialParameter m_InitialParameter;

        public mainForm()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			mDelArray = new ArrayList();
        }
        public mainForm(string[] a, AppController appController)
            : this()
        {
            args = a;
            m_appController = appController;
        }
        public mainForm(string[] a, AppController appController, string[] _LocalOrders)
            : this()
        {
            args = a;
            m_appController = appController;
            m_LocalOrders = _LocalOrders;
        }
        public mainForm(string[] a, AppController appController, IInitialParameter ip, string[] _LocalOrders)
            : this()
        {
            args = a;
            m_appController = appController;
            m_LocalOrders = _LocalOrders;
            m_InitialParameter = ip;
        }
		public mainForm(string[] a, AppController appController, ICheck ic, IInitialParameter ip):this(){
			args = a;
            m_appController = appController;
            m_ICheck = ic;
            m_InitialParameter = ip;
        }
        public mainForm(string[] a, AppController appController, ICheck ic,  IInitialParameter ip, string[] _LocalOrders)
            : this()
        {
            args = a;
            m_appController = appController;
            m_ICheck = ic;
            m_LocalOrders = _LocalOrders;
            m_InitialParameter = ip;
        }

        public DataSet dataset
        {
            set { this.tableSet = value; }
            get { return this.tableSet; }
        }

        /// <summary>
        /// コントローラー
        /// </summary>
        public AppController theAppController
        {
            get { return m_appController; }
        }

        /// <summary>
        /// ICheck型としてクラスを取得
        /// </summary>
        protected ICheck theICheck
        {
            get { return (ICheck)m_ICheck; }
        }

        /// <summary>
        /// IInitialParameter型としてクラスを取得
        /// </summary>
        protected IInitialParameter theInitialParameter
        {
            get { return m_InitialParameter; }
        }

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.mainFormMenu = new System.Windows.Forms.MainMenu(this.components);
            this.dataGrid = new System.Windows.Forms.DataGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.EndButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.InputOrderButton = new System.Windows.Forms.Button();
            this.SearchResultPrintButton = new System.Windows.Forms.Button();
            this.InputConditionButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.FinalizeButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.PrintButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.DelButton = new System.Windows.Forms.Button();
            this.PrintWorkObject = new System.Data.DataSet();
            this.panel2 = new System.Windows.Forms.Panel();
            this.updateList1 = new NCR.GeneralMainte.UpdateList();
            this.searchList1 = new NCR.GeneralMainte.SearchList();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrintWorkObject)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.AllowNavigation = false;
            this.dataGrid.AllowSorting = false;
            this.dataGrid.DataMember = "";
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid.Location = new System.Drawing.Point(0, 0);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.Size = new System.Drawing.Size(1264, 900);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.CurrentCellChanged += new System.EventHandler(this.dataGrid_CurrentCellChanged);
            this.dataGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGrid_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.EndButton);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 906);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 79);
            this.panel1.TabIndex = 1;
            // 
            // EndButton
            // 
            this.EndButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EndButton.Location = new System.Drawing.Point(758, 21);
            this.EndButton.Name = "EndButton";
            this.EndButton.Size = new System.Drawing.Size(70, 47);
            this.EndButton.TabIndex = 9;
            this.EndButton.Text = "終了";
            this.EndButton.UseVisualStyleBackColor = true;
            this.EndButton.Click += new System.EventHandler(this.EndButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.InputOrderButton);
            this.groupBox2.Controls.Add(this.SearchResultPrintButton);
            this.groupBox2.Controls.Add(this.InputConditionButton);
            this.groupBox2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox2.Location = new System.Drawing.Point(3, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 74);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "検索・並替";
            // 
            // InputOrderButton
            // 
            this.InputOrderButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InputOrderButton.Location = new System.Drawing.Point(85, 21);
            this.InputOrderButton.Name = "InputOrderButton";
            this.InputOrderButton.Size = new System.Drawing.Size(70, 47);
            this.InputOrderButton.TabIndex = 9;
            this.InputOrderButton.Text = "並替";
            this.InputOrderButton.UseVisualStyleBackColor = true;
            this.InputOrderButton.Click += new System.EventHandler(this.InputOrderButton_Click);
            // 
            // SearchResultPrintButton
            // 
            this.SearchResultPrintButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SearchResultPrintButton.Location = new System.Drawing.Point(161, 21);
            this.SearchResultPrintButton.Name = "SearchResultPrintButton";
            this.SearchResultPrintButton.Size = new System.Drawing.Size(70, 47);
            this.SearchResultPrintButton.TabIndex = 8;
            this.SearchResultPrintButton.Text = "印刷";
            this.SearchResultPrintButton.UseVisualStyleBackColor = true;
            this.SearchResultPrintButton.Click += new System.EventHandler(this.SearchResultPrintButton_Click);
            // 
            // InputConditionButton
            // 
            this.InputConditionButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InputConditionButton.Location = new System.Drawing.Point(9, 21);
            this.InputConditionButton.Name = "InputConditionButton";
            this.InputConditionButton.Size = new System.Drawing.Size(70, 47);
            this.InputConditionButton.TabIndex = 7;
            this.InputConditionButton.Text = "検索";
            this.InputConditionButton.UseVisualStyleBackColor = true;
            this.InputConditionButton.Click += new System.EventHandler(this.InputConditionButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.CancelButton);
            this.groupBox1.Controls.Add(this.FinalizeButton);
            this.groupBox1.Controls.Add(this.AddButton);
            this.groupBox1.Controls.Add(this.PrintButton);
            this.groupBox1.Controls.Add(this.EditButton);
            this.groupBox1.Controls.Add(this.DelButton);
            this.groupBox1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox1.Location = new System.Drawing.Point(253, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 74);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "編集";
            // 
            // CancelButton
            // 
            this.CancelButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CancelButton.Location = new System.Drawing.Point(234, 21);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(70, 47);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "取消";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // FinalizeButton
            // 
            this.FinalizeButton.Enabled = false;
            this.FinalizeButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FinalizeButton.Location = new System.Drawing.Point(414, 21);
            this.FinalizeButton.Name = "FinalizeButton";
            this.FinalizeButton.Size = new System.Drawing.Size(70, 47);
            this.FinalizeButton.TabIndex = 6;
            this.FinalizeButton.Text = "確定";
            this.FinalizeButton.UseVisualStyleBackColor = true;
            this.FinalizeButton.Click += new System.EventHandler(this.FinalizeButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AddButton.Location = new System.Drawing.Point(9, 21);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(70, 47);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "追加";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // PrintButton
            // 
            this.PrintButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PrintButton.Location = new System.Drawing.Point(339, 21);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(70, 47);
            this.PrintButton.TabIndex = 5;
            this.PrintButton.Text = "印刷";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EditButton.Location = new System.Drawing.Point(84, 21);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(70, 47);
            this.EditButton.TabIndex = 2;
            this.EditButton.Text = "変更";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // DelButton
            // 
            this.DelButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DelButton.Location = new System.Drawing.Point(159, 21);
            this.DelButton.Name = "DelButton";
            this.DelButton.Size = new System.Drawing.Size(70, 47);
            this.DelButton.TabIndex = 3;
            this.DelButton.Text = "削除";
            this.DelButton.UseVisualStyleBackColor = true;
            this.DelButton.Click += new System.EventHandler(this.DelButton_Click);
            // 
            // PrintWorkObject
            // 
            this.PrintWorkObject.DataSetName = "NewDataSet";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 85);
            this.panel2.Size = new System.Drawing.Size(1264, 985);
            this.panel2.TabIndex = 2;
            // 
            // mainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 13);
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KeyPreview = true;
            this.Menu = this.mainFormMenu;
            this.Name = "mainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "メンテナンス";
            this.TopMost = true;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.mainForm_Closing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.MenuComplete += new System.EventHandler(this.mainForm_MenuComplete);
            this.MenuStart += new System.EventHandler(this.mainForm_MenuStart);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mainForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PrintWorkObject)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void mainForm_Load(object sender, System.EventArgs e)
		{
            try
            {
                if (this.DesignMode) return;
                InitializeApplication();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// アプリケーションの初期化
        /// </summary>
        private void InitializeApplication()
        {
            ObtainParameters();
            SetButtons();
            OpenConnection();
            GenerateTable();
            GenerateHelper();
            GenerateDefalutRelation();
            FetchTableDef();
            InitSortCondition();
            GenerateRelation();
            GenerateInputTable();
            GetOperationDate();
            ProcAplInfo.OutReportPath = theInitialParameter.GetOutReportPath();
            ShowWhole(); //最初にテーブルを開くならこちら
        }

		/// <summary>
		/// パラメーター取得
		/// </summary>
		private void ObtainParameters()
		{
			ast = new appSetting();
			ast.TableName = "オペレータ";
			ast.InputStart = 0;
			ast.InputColOrderWidth = 35;
			ast.InputColNameWidth = 140;
			ast.InputColValueWidth = 500;
			ast.InputColDefaultValueWidth = 200;
			ast.TitleFont = "ＭＳ 明朝";
			ast.TitleFontSize = 16;
			ast.BodyFont = "ＭＳ 明朝";
			ast.BodyFontSize = Convert.ToSingle(10.5);
			ast.ReportFieldDefaultSize = 15;
            ast.SchemaName = ProcAplInfo.Schema;
            ast.TableName = ProcAplInfo.TableName;
            ast.SchemaTableName = string.Format("[{0}].[{1}]", ProcAplInfo.Schema, ProcAplInfo.TableName);

            //this.SetConnectionString();
        }

        /// <summary>
        /// コネクションを開く
        /// </summary>
        private void OpenConnection()
		{
            DataBase sqldb = DBManager.dbc;
			conn = sqldb.Conn;
			tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
		}

		/// <summary>
		/// 処理日を取得
		/// </summary>
		private void GetOperationDate()
        {
            headerToday = string.Format(DateTime.Now.ToString(), "yyyy/MM/dd");
        }

		/// <summary>
		/// DataSetをDataGridに表示
		/// </summary>
		private void ShowTable(string tab)
		{
            string currentcaption = dataGrid.CaptionText;
            RefreshButtonEnable();
			dataGrid.DataSource = tableSet.Tables[tab].DefaultView;
			dataGrid.ReadOnly = true;
			//dataGrid.CaptionText = tab;
            dataGrid.CaptionText = currentcaption;
            ast.CurrentData = tab;
			ast.GridMode = 1;
		}

        /// <summary>
        /// ボタン制御
        /// </summary>
        private void SetButtons()
        {
            this.AddButton.Enabled = !ProcAplInfo.IsReadOnly;
            this.EditButton.Enabled = !ProcAplInfo.IsReadOnly;
            this.DelButton.Enabled = !ProcAplInfo.IsReadOnly;
            this.CancelButton.Enabled = !ProcAplInfo.IsReadOnly;
        }

        /// <summary>
        /// テーブル情報の取得
        /// </summary>
        private void FetchTableDef()
		{
			DataGridTextBoxColumn tb;
			DataGridTableStyle dgt;

            //adapter.SelectCommand = conn.CreateCommand();
            //adapter.SelectCommand.CommandText = "SELECT SEARCH_CONDITION AS 入力規則 FROM USER_CONSTRAINTS WHERE CONSTRAINT_TYPE = 'C' AND TABLE_NAME = '" + ast.TableName + "'";
            //adapter.SelectCommand.Transaction = tran;
            //adapter.Fill(tableSet, "入力規則");

            //dgt = new DataGridTableStyle();
            //dgt.MappingName = "入力規則";

            //tb = new DataGridTextBoxColumn();
            //tb.MappingName = "入力規則";
            //tb.HeaderText = "入力規則";
            //tb.Width = ast.InputColValueWidth;
			//dgt.GridColumnStyles.Add(tb);
			
			//dataGrid.TableStyles.Add(dgt);

            string sql = "";
            sql += " SELECT ";
            sql += "      TC.OWNER ";
            sql += "     ,TC.TABLE_NAME ";
            sql += "     ,TC.COLUMN_NAME ";
            sql += "     ,TC.DATA_DEFAULT ";
            sql += "     ,TC.DATA_LENGTH ";
            sql += "     ,TC.DATA_PRECISION ";
            sql += "     ,CASE ";
            sql += "      WHEN NVL(CC.COMMENTS, ' ') = ' ' THEN TC.COLUMN_NAME ";
            sql += "      WHEN INSTR(NVL(CC.COMMENTS, ''), ':') = 0 THEN CC.COMMENTS ";
            sql += "      ELSE SUBSTR(NVL(CC.COMMENTS, ''), 1, INSTR(NVL(CC.COMMENTS, ''), ':') - 1) ";
            sql += "      END AS CAPTION ";
            sql += " FROM ";
            sql += "     ALL_TAB_COLUMNS TC ";
            sql += " LEFT JOIN ALL_COL_COMMENTS CC ";
            sql += "     ON TC.OWNER = CC.OWNER ";
            sql += "    AND TC.TABLE_NAME = CC.TABLE_NAME ";
            sql += "    AND TC.COLUMN_NAME = CC.COLUMN_NAME ";
            sql += " WHERE ";
            sql += "     TC.OWNER = '" + ast.SchemaName.ToUpper() + "' ";
            sql += " AND TC.TABLE_NAME = '" + ast.TableName + "' ";
            sql += " ORDER BY ";
            sql += "     TC.COLUMN_ID ";

            adapter.SelectCommand = conn.CreateCommand();
			adapter.SelectCommand.CommandText = sql;
			adapter.SelectCommand.Transaction = tran;
			adapter.Fill(tableSet,"列定義");

			foreach (DataRow dr in tableSet.Tables["列定義"].Rows)
			{
				DataColumn dc = tableSet.Tables["全体"].Columns[Convert.ToString(dr["COLUMN_NAME"])];

                // 規定値欄がWIDEMEMOとなりうまくとれない
                //if (dr["DATA_DEFAULT"] != DBNull.Value)
                //{
                //	dc.DefaultValue = dr["DATA_DEFAULT"];
                //}

                string caption = "";
                if (dr["CAPTION"] != DBNull.Value)
                {
                    caption = dr["CAPTION"].ToString();
                }

                int len = 0;
				string strLen = "";
				if (dr["DATA_LENGTH"] != DBNull.Value)
				{
					strLen = dr["DATA_LENGTH"].ToString();
					Int32.TryParse(strLen, out len);
					dc.Caption = strLen + "," + caption;
				}
				else if (dr["DATA_PRECISION"] != DBNull.Value)
				{
					strLen = dr["DATA_PRECISION"].ToString();
					Int32.TryParse(strLen, out len);
                    dc.Caption = strLen + "," + caption;
                }
            }

            //adapter.SelectCommand = conn.CreateCommand();
            //adapter.SelectCommand.CommandText = "SELECT * FROM USER_CONSTRAINTS WHERE TABLE_NAME = '" + ast.TableName + "'";
            //adapter.SelectCommand.Transaction = tran;
            //adapter.Fill(tableSet,"制約");

            //adapter.SelectCommand = conn.CreateCommand();
            //adapter.SelectCommand.CommandText = "SELECT * FROM USER_CONS_COLUMNS WHERE TABLE_NAME = '" + ast.TableName + "'";
            //adapter.SelectCommand.Transaction = tran;
            //adapter.Fill(tableSet,"制約列");

            //adapter.SelectCommand = conn.CreateCommand();
            //adapter.SelectCommand.CommandText = "SELECT * FROM USER_INDEXES WHERE TABLE_NAME = '" + ast.TableName + "'";
            //adapter.SelectCommand.Transaction = tran;
            //adapter.Fill(tableSet,"インデックス");

            //adapter.SelectCommand = conn.CreateCommand();
            //adapter.SelectCommand.CommandText = "SELECT * FROM USER_IND_COLUMNS WHERE TABLE_NAME = '" + ast.TableName + "'";
            //adapter.SelectCommand.Transaction = tran;
            //adapter.Fill(tableSet,"インデックス列");

            sql = "";
            sql += " SELECT ";
            sql += "      TC.OWNER ";
            sql += "     ,TC.TABLE_NAME ";
            sql += "     ,TC.COLUMN_NAME ";
            sql += "     ,CASE ";
            sql += "      WHEN NVL(CC.COMMENTS, ' ') = ' ' THEN TC.COLUMN_NAME ";
            sql += "      WHEN INSTR(NVL(CC.COMMENTS, ''), ':') = 0 THEN CC.COMMENTS ";
            sql += "      ELSE SUBSTR(NVL(CC.COMMENTS, ''), 1, INSTR(NVL(CC.COMMENTS, ''), ':') - 1) ";
            sql += "      END AS COMMENTS ";
            sql += " FROM ";
            sql += "     ALL_TAB_COLUMNS TC ";
            sql += " LEFT JOIN ALL_COL_COMMENTS CC ";
            sql += "     ON TC.OWNER = CC.OWNER ";
            sql += "    AND TC.TABLE_NAME = CC.TABLE_NAME ";
            sql += "    AND TC.COLUMN_NAME = CC.COLUMN_NAME ";
            sql += " WHERE ";
            sql += "     TC.OWNER = '" + ast.SchemaName.ToUpper() + "' ";
            sql += " AND TC.TABLE_NAME = '" + ast.TableName + "' ";
            sql += " ORDER BY ";
            sql += "     TC.COLUMN_ID ";

            adapter.SelectCommand = conn.CreateCommand();
			adapter.SelectCommand.CommandText = sql;
			adapter.SelectCommand.Transaction = tran;
			adapter.Fill(tableSet,"列コメント");

			dgt = new DataGridTableStyle();
			dgt.MappingName = "列コメント";

			tb = new DataGridTextBoxColumn();
			tb.MappingName = "TABLE_NAME";
			tb.HeaderText = "テーブル名";
			tb.Width = ast.InputColNameWidth;
			dgt.GridColumnStyles.Add(tb);
			
			tb = new DataGridTextBoxColumn();
			tb.MappingName = "COLUMN_NAME";
			tb.HeaderText = "列名";
			tb.Width = ast.InputColNameWidth;
			dgt.GridColumnStyles.Add(tb);
			
			tb = new DataGridTextBoxColumn();
			tb.MappingName = "COMMENTS";
			tb.HeaderText = "コメント";
			tb.Width = ast.InputColValueWidth;
			dgt.GridColumnStyles.Add(tb);

			dataGrid.TableStyles.Add(dgt);

            // 列名日本語化
            DataGridTableStyle zdgt = new DataGridTableStyle();
            zdgt.MappingName = "全体";
            foreach (DataRow row in tableSet.Tables["列コメント"].Rows)
            {
                DataGridTextBoxColumn ztb = new DataGridTextBoxColumn();                
                ztb.MappingName = row["COLUMN_NAME"].ToString();
                ztb.HeaderText = row["COMMENTS"].ToString() + " (" + ztb.MappingName + ")";
                ztb.Width = 150;
                zdgt.GridColumnStyles.Add(ztb);
            }
            dataGrid.TableStyles.Add(zdgt);

            //adapter.SelectCommand = conn.CreateCommand();
            //adapter.SelectCommand.CommandText = "SELECT * FROM USER_TAB_COMMENTS WHERE TABLE_NAME = '" + ast.TableName + "'";
            //adapter.SelectCommand.Transaction = tran;
            //adapter.Fill(tableSet,"テーブルコメント");

            //dgt = new DataGridTableStyle();
            //dgt.MappingName = "テーブルコメント";

			//tb = new DataGridTextBoxColumn();
			//tb.MappingName = "TABLE_NAME";
			//tb.HeaderText = "テーブル名";
			//tb.Width = ast.InputColNameWidth;
			//dgt.GridColumnStyles.Add(tb);
			
			//tb = new DataGridTextBoxColumn();
			//tb.MappingName = "TABLE_TYPE";
			//tb.HeaderText = "テーブル型";
			//tb.Width = ast.InputColNameWidth;
			//dgt.GridColumnStyles.Add(tb);
			
			//tb = new DataGridTextBoxColumn();
			//tb.MappingName = "COMMENTS";
			//tb.HeaderText = "コメント";
            //tb.Width = ast.InputColValueWidth;
            //dgt.GridColumnStyles.Add(tb);

            //dataGrid.TableStyles.Add(dgt);
        }

        private void menuItemEnd_Click(object sender, System.EventArgs e)
		{
            try
            {
                mainForm.ActiveForm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		/// <summary>
		/// where 句
		/// </summary>
		private string whereClause
		{
			get
			{
				DataRow[] dr = tableSet.Tables["検索条件"].Select("", "順番 ASC");
				string str = null;
				for (int i = 0; i < dr.Length; i++)
				{
					if (i > 0)
					{
						str = str + " " + dr[i]["複合"] + " ";
					}
					if ( dr[i]["演算子"].ToString().Equals("IS NULL") ) {
						str = str + dr[i]["列名"] + " " + dr[i]["演算子"] + " " + dr[i]["値"].ToString().Replace("'","");
					}
                    else if (dr[i]["演算子"].ToString().Equals("LIKE")) {
                        str = str + dr[i]["列名"] + " " + dr[i]["演算子"] + " '*" + dr[i]["値"].ToString().Replace("'", "") + "*'";
                    
                    }
                    else
                    {
                        str = str + dr[i]["列名"] + " " + dr[i]["演算子"] + " '" + dr[i]["値"].ToString().Replace("'", "") + "'";
                    }
				}
				return str;
			}
		}

		/// <summary>
		/// order by 句
		/// </summary>
		private string orderByClause
		{
			get
			{
				DataRow[] dr = tableSet.Tables["並替条件"].Select("", "順番 ASC");
				string str = null;
				for (int i = 0; i < dr.Length; i++)
				{
					if (i > 0)
					{
						str = str + ", ";
					}
					DataRow[] desc = tableSet.Tables["昇降順"].Select("昇降順 = '" + dr[i]["昇降順"] + "'");
					str = str + dr[i]["列名"] + " " + desc[0]["Descend"];
				}				
				return str;
			}
		}

        /// <summary>
        /// order by 句
        /// </summary>
        private string orderByCaption
        {
            get
            {
                DataRow[] dr = tableSet.Tables["並替条件"].Select("", "順番 ASC");
                string str = null;
                for (int i = 0; i < dr.Length; i++)
                {
                    if (i > 0)
                    {
                        str = str + ", ";
                    }
                    DataRow[] desc = tableSet.Tables["昇降順"].Select("昇降順 = '" + dr[i]["昇降順"] + "'");
                    str = str + dr[i]["キャプション"] + " " + desc[0]["Descend"];
                }
                return str;
            }
        }

        private void ShowTableFromMenu(object sender, System.EventArgs e)
		{
            try
            {
                MenuItem mi = (MenuItem)sender;
                string[] tab = mi.Text.Split(' ');

                ShowTable(tab[0]);

                switch (tab[0])
                {
                    case "列名":
                    case "入力規則":
                    case "列定義":
                    case "制約":
                    case "制約列":
                    case "インデックス":
                    case "インデックス列":
                    case "列コメント":
                    case "テーブルコメント":
                        ast.GridMode = 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		private void menuItemQueryClear_Click(object sender, System.EventArgs e)
		{
            try
            {
                DataTable dt = tableSet.Tables["検索条件"];
                dt.Rows.Clear();
                searchSeq = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		private void menuItemSortInit_Click(object sender, System.EventArgs e)
		{
            try
            {
                InitSortCondition();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// 並替条件をテーブルの主キーを使用して初期化
        /// </summary>
        private void InitSortCondition()
		{
			DataTable dt = tableSet.Tables["並替条件"];
			dt.Rows.Clear();
			sortSeq = 0;

			DataView dv = tableSet.Tables["全体"].DefaultView;
			dv.Sort = orderByClause;
            if (m_LocalOrders == null)
            {
                foreach (DataColumn pk in tableSet.Tables["全体"].PrimaryKey)
                {
                    DataRow dr = dt.NewRow();
                    dr["順番"] = sortSeq++;
                    dr["列名"] = pk.ColumnName;
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                for (int i = 0; i < m_LocalOrders.Length;i++ )
                {
                    DataRow dr = dt.NewRow();
                    dr["順番"] = sortSeq++;
                    dr["列名"] = m_LocalOrders[i].ToString();
                    dt.Rows.Add(dr);
                }
            }
		}

		private void menuItemSetting_Click(object sender, System.EventArgs e)
		{
            try
            {
                propertyForm f = new propertyForm();
                f.SelectedObjedt = ast;
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// 全体テーブルのテーブル間リレーションを作成
        /// </summary>
        private void GenerateRelation()
		{
			//foreach (DataRow dr in tableSet.Tables["制約"].Select("CONSTRAINT_TYPE = 'R'"))
			//{
			//	DataColumn[] parentCol;
			//	DataColumn[] childCol;
			//	DataRelation rel;
			//	DataTable dt = new DataTable();

			//	adapter.SelectCommand = conn.CreateCommand();
			//	adapter.SelectCommand.CommandText = "SELECT * FROM USER_CONS_COLUMNS WHERE CONSTRAINT_NAME = '" + dr["R_CONSTRAINT_NAME"] + "' ORDER BY POSITION ASC";
			//	adapter.SelectCommand.Transaction = tran;
			//	adapter.Fill(dt);
			//	string refTable = dt.Rows[0]["TABLE_NAME"].ToString();

			//	if (!tableSet.Tables.Contains(refTable))
			//	{
			//		adapter.SelectCommand = conn.CreateCommand();
			//		adapter.SelectCommand.CommandText = "SELECT * FROM " + refTable + theInitialParameter.GetMasterQueryCondition();
			//		adapter.SelectCommand.Transaction = tran;
			//		adapter.Fill(tableSet, refTable);
			//	}

			//	parentCol = new DataColumn[dt.Rows.Count];
				
			//	DataRow[] childRows = tableSet.Tables["制約列"].Select("CONSTRAINT_NAME = '" + dr["CONSTRAINT_NAME"] + "'", "POSITION ASC");
			//	childCol = new DataColumn[childRows.Length];
			//	for (int i = 0; i < childRows.Length; i++)
			//	{
			//		parentCol[i] = tableSet.Tables[refTable].Columns[Convert.ToString(dt.Rows[i]["COLUMN_NAME"])];
			//		childCol[i] = tableSet.Tables["全体"].Columns[Convert.ToString(childRows[i]["COLUMN_NAME"])];
			//	}
			//	rel = new DataRelation(Convert.ToString(dr["CONSTRAINT_NAME"]), parentCol, childCol);
			//	tableSet.Relations.Add(rel);
			//}
		}

		/// <summary>
		/// アプリ用のテーブル間リレーションを作成
		/// </summary>
		private void GenerateDefalutRelation()
		{
			DataColumn parentCol;
			DataColumn childCol;
			DataRelation rel;

			parentCol = tableSet.Tables["複合"].Columns["複合"];
			childCol = tableSet.Tables["検索条件"].Columns["複合"];
			rel = new DataRelation("複合", parentCol, childCol);
			tableSet.Relations.Add(rel);

			parentCol = tableSet.Tables["列名"].Columns["列名"];
			childCol = tableSet.Tables["検索条件"].Columns["列名"];
			rel = new DataRelation("検索列名", parentCol, childCol);
			tableSet.Relations.Add(rel);

			parentCol = tableSet.Tables["演算子"].Columns["演算子"];
			childCol = tableSet.Tables["検索条件"].Columns["演算子"];
			rel = new DataRelation("演算子", parentCol, childCol);
			tableSet.Relations.Add(rel);

			parentCol = tableSet.Tables["列名"].Columns["列名"];
			childCol = tableSet.Tables["並替条件"].Columns["列名"];
			rel = new DataRelation("並替列名", parentCol, childCol);
			tableSet.Relations.Add(rel);

			parentCol = tableSet.Tables["昇降順"].Columns["昇降順"];
			childCol = tableSet.Tables["並替条件"].Columns["昇降順"];
			rel = new DataRelation("昇降順", parentCol, childCol);
			tableSet.Relations.Add(rel);
		}

        private void AddToDataset(DataSet ds,DataTable dt)
        {
            ds.Tables.Add(dt);
        }

        private DataTable getColumnInfo()
        {
            DataTable dt = SelectStructure();
            DataTable dt_columns= new DataTable("列名");
            dt_columns.Columns.Add("列名");
            dt_columns.Columns.Add("列順");
            int i = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                DataRow dr = dt_columns.NewRow();
                dr["列名"] = dc.ColumnName ;
                dr["列順"] = i;
                dt_columns.Rows.Add(dr);
                i = i + 1;
            }
            return dt_columns;
        }

        private DataTable getConstraintInfo()
        {
            DataTable dt = SelectStructure();
            DataTable dt_constraint = new DataTable("制約");
            dt_constraint.Columns.Add("制約");
            dt_constraint.Columns.Add("制約順");
            int i = 0;
            foreach (Constraint c in dt.Constraints)
            {
                DataRow dr = dt_constraint.NewRow();
                dr["制約名"] = c.ConstraintName  ;
                //dr["列名"] = ;
                dt_constraint.Rows.Add(dr);
                i = i + 1;
            }
            return dt_constraint;
        }

        private DataTable getCommentInfo()
        {
            DataTable dt = SelectStructure();
            DataTable dt_comment = new DataTable("制約名");
            dt_comment.Columns.Add("制約名");
            dt_comment.Columns.Add("制約順");
            int i = 0;
            foreach (Constraint c in dt.Rows)
            {
                DataRow dr = dt_comment.NewRow();
                dr["制約名"] = c.ConstraintName;
                dr["制約順"] = i;
                dt_comment.Rows.Add(dr);
                i = i + 1;
            }
            return dt_comment;
        }

        private DataTable SelectStructure()
        {
            DataTable dt = new DataTable();

			adapter.SelectCommand = conn.CreateCommand();
			adapter.SelectCommand.CommandText = "SELECT * FROM " + ast.SchemaTableName + theInitialParameter.GetMasterQueryCondition();
			adapter.SelectCommand.Transaction = tran;
			adapter.Fill(dt);
            return dt;
        }

		/// <summary>
		/// 入力補助テーブルを作成
		/// </summary>
		private void GenerateHelper()
		{
			DataTable dt;
			DataRow dr;

            AddToDataset(tableSet,this.getColumnInfo());
            
			DataGridTextBoxColumn tb;
			DataGridTableStyle dgt;
			
			dgt = new DataGridTableStyle();
			dgt.MappingName = "列名";

			tb = new DataGridTextBoxColumn();
			tb.MappingName = "列名";
			tb.HeaderText = "列名";
			tb.Width = ast.InputColNameWidth;
			dgt.GridColumnStyles.Add(tb);
			
			tb = new DataGridTextBoxColumn();
			tb.MappingName = "列順";
			tb.HeaderText = "列順";
			tb.Width = ast.InputColOrderWidth;
			dgt.GridColumnStyles.Add(tb);

			dataGrid.TableStyles.Add(dgt);

			dt = tableSet.Tables.Add("複合");
			dt.Columns.Add("複合", System.Type.GetType("System.String"));
			dr = dt.NewRow();
			dr["複合"] = "AND";
			dt.Rows.Add(dr);
			dr = dt.NewRow();
			dr["複合"] = "OR";
			dt.Rows.Add(dr);

			dt = tableSet.Tables.Add("演算子");
			dt.Columns.Add("演算子", System.Type.GetType("System.String"));
			dr = dt.NewRow();
			dr["演算子"] = "<";
			dt.Rows.Add(dr);
			dr = dt.NewRow();
			dr["演算子"] = "<=";
			dt.Rows.Add(dr);
			dr = dt.NewRow();
			dr["演算子"] = "=";
			dt.Rows.Add(dr);
			dr = dt.NewRow();
			dr["演算子"] = ">";
			dt.Rows.Add(dr);
			dr = dt.NewRow();
			dr["演算子"] = ">=";
			dt.Rows.Add(dr);
			dr = dt.NewRow();
			dr["演算子"] = "<>";
			dt.Rows.Add(dr);
			dr = dt.NewRow();
			dr["演算子"] = "LIKE";
			dt.Rows.Add(dr);
			dr = dt.NewRow();
			dr["演算子"] = "IS NULL";
			dt.Rows.Add(dr);

			dt = tableSet.Tables.Add("昇降順");
			dt.Columns.Add("昇降順", System.Type.GetType("System.String"));
			dt.Columns.Add("Descend", System.Type.GetType("System.String"));
			dr = dt.NewRow();
			dr["昇降順"] = "昇順";
			dr["Descend"] = "ASC";
			dt.Rows.Add(dr);
			dr = dt.NewRow();
			dr["昇降順"] = "降順";
			dr["Descend"] = "DESC";
			dt.Rows.Add(dr);
		}

		/// <summary>
		/// 基本テーブルを作成
		/// </summary>
		private void GenerateTable()
		{
			DataTable dt;
			DataColumn dc;

			tableSet = new DataSet(ast.TableName + "テーブル");
			this.Text = this.Text + " " + tableSet.DataSetName;

            adapter = DBManager.dbc.Factory.CreateDataAdapter();
			adapter.SelectCommand = conn.CreateCommand();
			adapter.SelectCommand.CommandText = "SELECT * FROM " + ast.SchemaTableName + theInitialParameter.GetMasterQueryCondition();
			adapter.SelectCommand.Transaction = tran;
			adapter.FillSchema(tableSet,SchemaType.Mapped, "全体");
            adapter.Fill(tableSet, "全体");

            DbCommandBuilder cb = DBManager.dbc.Factory.CreateCommandBuilder();
            cb.DataAdapter = adapter;
            adapter.InsertCommand = cb.GetInsertCommand();
            if (tableSet.Tables["全体"].PrimaryKey.GetLength(0) > 0)
            {
                adapter.UpdateCommand = cb.GetUpdateCommand();
                adapter.DeleteCommand = cb.GetDeleteCommand();
                mhaskey = true;
            }
            else
            {
                //主キーがない場合。行が特定できないのでUPDATE,DELETEメンテ不可だが一応閲覧機能は残す。
                string sql = "UPDATE " + ast.SchemaTableName + " SET ";
                foreach (DataColumn ldc in tableSet.Tables["全体"].Columns)
                {
                    sql= sql + "\"" + ldc.ColumnName + "\"=?,";
                }
                sql = sql.Remove(sql.Length - 1);

				adapter.UpdateCommand = conn.CreateCommand();
				adapter.UpdateCommand.CommandText = sql;
				adapter.UpdateCommand.Transaction = tran;
                adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.Both;
                sql = "DELETE FROM " + ast.SchemaTableName;
				adapter.DeleteCommand = conn.CreateCommand();
				adapter.DeleteCommand.CommandText = sql;
				adapter.DeleteCommand.Transaction = tran;
                adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.Both;
                mhaskey = false;
            }

			foreach (DataColumn col in tableSet.Tables["全体"].Columns)
			{
				Regex re = new Regex("漢字");
				Match ma = re.Match(col.ColumnName);
				if (ma.Success)
				{
					col.ExtendedProperties.Add("ImeMode", ImeMode.Hiragana);
					continue;
				}

				re = new Regex("カナ");
				ma = re.Match(col.ColumnName);
				if (ma.Success)
				{
					col.ExtendedProperties.Add("ImeMode", ImeMode.KatakanaHalf);
					continue;
				}
				col.ExtendedProperties.Add("ImeMode", ImeMode.Off);
			}

			dt = tableSet.Tables.Add("検索条件");
			dc = dt.Columns.Add("順番", System.Type.GetType("System.Int32"));
			dc.ExtendedProperties.Add("ImeMode", ImeMode.Off);
			dc = dt.Columns.Add("複合", System.Type.GetType("System.String"));
			dc.DefaultValue = "AND";
			dc.ExtendedProperties.Add("ImeMode", ImeMode.Off);
			dc = dt.Columns.Add("列名", System.Type.GetType("System.String"));
			dc.ExtendedProperties.Add("ImeMode", ImeMode.Hiragana);
			dc.AllowDBNull = false;
			dc = dt.Columns.Add("演算子", System.Type.GetType("System.String"));
			dc.ExtendedProperties.Add("ImeMode", ImeMode.Off);
			dc.AllowDBNull = false;
			dc = dt.Columns.Add("値", System.Type.GetType("System.String"));
			dc.ExtendedProperties.Add("ImeMode", ImeMode.Off);
			dc.AllowDBNull = false;

			dt = tableSet.Tables.Add("並替条件");
			dc = dt.Columns.Add("順番", System.Type.GetType("System.Int32"));
			dc.ExtendedProperties.Add("ImeMode", ImeMode.Off);
			dc = dt.Columns.Add("列名", System.Type.GetType("System.String"));
			dc.ExtendedProperties.Add("ImeMode", ImeMode.Hiragana);
			dc.AllowDBNull = false;
			dc = dt.Columns.Add("昇降順", System.Type.GetType("System.String"));
			dc.DefaultValue = "昇順";
			dc.ExtendedProperties.Add("ImeMode", ImeMode.Hiragana);

			this.MakeBATable();
		}

		private void MakeBATable(){
			//変更前後テーブル作成
			DataTable dt = new DataTable("メンテナンスログ");

			//列定義情報設定
			DataColumn dc;

			dc = new DataColumn("No");
			dc.DataType = Type.GetType("System.String");
			dc.DefaultValue = "";
			dc.MaxLength = 15;
			dt.Columns.Add(dc);

			DataColumn[] dcs = {dc};
			dt.PrimaryKey = dcs;

			foreach(DataColumn dcol in tableSet.Tables["全体"].Columns){
				DataColumn col = new DataColumn();
				col.DataType = dcol.DataType; 
				col.AllowDBNull = true; 
				col.Caption = dcol.Caption; 
				col.ColumnName = dcol.ColumnName; 
				col.MaxLength = dcol.MaxLength;
				col.DefaultValue = dcol.DefaultValue;
				dt.Columns.Add(col);
			}
			tableSet.Tables.Add(dt);
		}

		/// <summary>
		/// インプットテーブルの作成
		/// </summary>
		private void GenerateInputTable()
		{
			DataGridTextBoxColumn tb;

			DataGridTableStyle dgt = new DataGridTableStyle();
			dgt.MappingName = "入力";

			tb = new DataGridTextBoxColumn();
			tb.MappingName = "列順";
			tb.HeaderText = "列順";
			tb.Width = ast.InputColOrderWidth;
			tb.ReadOnly = true;
			dgt.GridColumnStyles.Add(tb);
			
            tb = new DataGridTextBoxColumn();
            tb.MappingName = "列";
            tb.HeaderText = "列";
            tb.Width = ast.InputColNameWidth;
            tb.ReadOnly = true;
            dgt.GridColumnStyles.Add(tb);

            tb = new DataGridTextBoxColumn();
			tb.MappingName = "値";
			tb.HeaderText = "値";
			tb.Width = ast.InputColValueWidth;
			tb.NullText = "";
			tb.TextBox.TextChanged += new EventHandler(input_TextChanged);
			dgt.GridColumnStyles.Add(tb);

			tb = new DataGridTextBoxColumn();
			tb.MappingName = "入力補助";
			tb.HeaderText = "入力補助";
			tb.Width = ast.InputColDefaultValueWidth;
			tb.NullText = "";
			tb.ReadOnly = true;
			dgt.GridColumnStyles.Add(tb);

            tb = new DataGridTextBoxColumn();
            tb.MappingName = "列名";
            tb.HeaderText = "列名";
            tb.Width = ast.InputColNameWidth;
            tb.ReadOnly = true;
            dgt.GridColumnStyles.Add(tb);

            dataGrid.TableStyles.Add(dgt);

			DataTable dt = tableSet.Tables.Add("入力");
			dt.Columns.Add("列順", System.Type.GetType("System.Int32"));
            dt.Columns.Add("列", System.Type.GetType("System.String"));
            dt.Columns.Add("値", System.Type.GetType("System.String"));
			dt.Columns.Add("入力補助", System.Type.GetType("System.String"));
            dt.Columns.Add("列名", System.Type.GetType("System.String"));

            dt.DefaultView.AllowNew = false;
		}

		private void mainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (ast.InTransaction)
			{
				DialogResult r = MessageBox.Show(this, "更新未済のデータがあります。更新を取消してこのまま終了しますか?","アプリケーションの終了",MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (r == DialogResult.Yes)
				{
					tableSet.Tables["全体"].RejectChanges();
					tran.Rollback();
					ast.InTransaction = false;
				}
				else
				{
					e.Cancel = true;
				}
			} 
			else
			{
				tran.Rollback();
			}
		}

		private void menuItemUpdate_Click(object sender, System.EventArgs e)
		{
            try
            {
                string msg = ItemUpdate();
                if (msg == null)
                {
                    ast.InTransaction = false;
                    tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    dataGrid.CaptionText = "更新完了";
                }
                else
                {
                    MessageBox.Show(this, msg, "更新実行", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        protected string ItemUpdate( ) {
			int cntResult=0;
			try {
                
				cntResult = adapter.Update(tableSet, "全体");

				mDelArray.Clear();
			} 
			catch (OdbcException  oe) {
				return oe.Message;
			} 
			catch (Exception ex) {
				string[] mes = ex.Message.Split('\n');
                if (mes.Length > 1)
                {
                    string msg = mes[mes.Length - 1];
                    int pos = msg.IndexOf(")");
                    return msg.Substring(pos + 1);
                }
                else
                {
                    return mes[0];
                }
			}
			tran.Commit();

			return null;
		}

		private void menuItemCancelUpdate_Click(object sender, System.EventArgs e)
		{
            try
            {
                tableSet.Tables["全体"].RejectChanges();
                mDelArray.Clear();
                ast.InTransaction = false;
                dataGrid.CaptionText = "取消完了";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        private void menuItemAdd_Click(object sender, System.EventArgs e)
		{
            try
            {
                switch (ast.CurrentData)
                {
                    case "検索条件":
                    case "並替条件":
                    case "全体":
                        break;
                    default:
                        return;
                }

                ast.InputMode = 0;
                dataGrid.CaptionText = "追加中";

                PrepareInputTable();
                ShowInputTable((Int16)DataManipulationForm.Mode.ADD);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        private void input_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                DataGridTextBoxColumn dgt = (DataGridTextBoxColumn)dataGrid.TableStyles["入力"].GridColumnStyles["値"];
                switch (ast.CurrentData)
                {
                    case "全体":
                    case "検索条件":
                    case "並替条件":
                        DataTable dt = tableSet.Tables[ast.CurrentData];
                        DataColumn dc = dt.Columns[dataGrid.CurrentRowIndex];
                        do
                        {
                            dgt.TextBox.ImeMode = (ImeMode)dc.ExtendedProperties["ImeMode"];
                        } while (dgt.TextBox.ImeMode != (ImeMode)dc.ExtendedProperties["ImeMode"]);
                        if (ast.CurrentData == "全体")
                        {
                            if (dc.DataType == System.Type.GetType("System.Decimal"))
                            {
                                dgt.TextBox.MaxLength = GetPrecisionScale(dc);
                            }
                            else if (dc.DataType == System.Type.GetType("System.String"))
                            {

                                if (dc.ColumnName.IndexOf("漢字") < 0)
                                {
                                    //if(dc.ColumnName.IndexOf("漢字")<0 && dc.ColumnName.IndexOf("カナ")<0){
                                    dgt.TextBox.MaxLength = dc.MaxLength;
                                }
                                else
                                {
                                    dgt.TextBox.MaxLength = dc.MaxLength / 2;
                                }

                            }
                            else
                            {
                                dgt.TextBox.MaxLength = 0;
                            }
                        }
                        break;
                }

                if (ast.CurrentData != "全体")
                {
                    dgt.TextBox.MaxLength = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		private int GetPrecisionScale(DataColumn dc)
		{
			DataTable dt = tableSet.Tables["列定義"];
			DataRow[] dr = dt.Select("COLUMN_NAME = '" + dc.ColumnName + "'");
			return Convert.ToInt32(dr[0]["DATA_PRECISION"]) + Convert.ToInt32(dr[0]["DATA_SCALE"]);
		}

		private void mainForm_MenuStart(object sender, System.EventArgs e)
		{
            try
            {
                switch (ast.GridMode)
                {
                    case 0:

                        break;
                    case 1:

                        if (ast.CurrentData.Equals("全体") && ast.TableName.StartsWith("MWK_"))
                        {
                            DataView dv = tableSet.Tables[ast.CurrentData].DefaultView;
                            switch (dv.RowStateFilter)
                            {
                                case DataViewRowState.Added:
                                case DataViewRowState.Deleted:
                                case DataViewRowState.ModifiedOriginal:
                                case DataViewRowState.ModifiedCurrent:

                                    break;
                                default:
                                    break;
                            }
                        }

                        break;
                    case 2:

                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        private void menuItemFinalize_Click(object sender, System.EventArgs e)
		{
			try
			{
				FinalizeData();
			} 
			catch (OdbcException  oe)
			{
				MessageBox.Show(this,oe.Message,"入力確定",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
			} 
			catch (Exception ex)
			{
				string[] mes = ex.Message.Split('\n');
				if (mes.Length > 1) 
				{
					string msg = mes[mes.Length -1];
					int pos = msg.IndexOf(")");
					MessageBox.Show(this, msg.Substring(pos + 1),"入力確定",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				} 
				else 
				{
					MessageBox.Show(this, mes[0], "入力確定",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				}
				return;
			}

            try
            {
                if (ast.CurrentData == "全体")
                {
                    if (!ast.InTransaction)
                    {
                        ast.InTransaction = true;
                    }

                    if (ast.InputMode == 0)
                    {
                        ShowEditedData(DataViewRowState.Added);
                    }
                    else if (ast.InputMode == 1)
                    {
                        ShowEditedData(DataViewRowState.ModifiedCurrent);
                    }
                }
                else
                {
                    ShowTable(ast.CurrentData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        /// <summary>
        /// データ追加
        /// </summary>
        private void AddData()
		{
			DataTable dt = tableSet.Tables[ast.CurrentData];
			DataTable inp = tableSet.Tables["入力"];
			DataRow dr = dt.NewRow();
			bool bNull = false;

			if((!ast.CurrentData.Equals("検索条件")) && (!ast.CurrentData.Equals("並替条件")))
			{
				//テーブル制約チェック
				//TableRestCheck();
			}
			for (int i = 0; i < inp.Rows.Count; i++)
			{
                //odbc対応
                //if (dmf.dataGridView[4,i].Value.ToString().Equals("演算子") && dmf.dataGridView[2,i].Value.ToString().Equals("IS NULL") 
                if (dmf.dataGridView[4, i].Value.ToString().Equals("演算子") && dmf.dataGridView[2, i].Value.ToString().Equals("")
                    && ast.CurrentData.Equals("検索条件"))
                {
					bNull = true;
				}
                if (dmf.dataGridView[4, i].Value.ToString().Equals("値") && bNull)
                {
					// エラー回避するためスペースに変換
                    dmf.dataGridView[2, i].Value = " ";
				}
                if (dmf.dataGridView[2, i].Value != DBNull.Value)
				{
					this.CheckInput(i);

                    dr[dmf.dataGridView[4, i].Value.ToString()] = dmf.dataGridView[2, i].FormattedValue.ToString();
				}
                this.OriginalCheckInput(i);
			}
			dt.Rows.Add(dr);
			if(ast.CurrentData.Equals("検索条件"))
			{
				searchSeq++;
			}
			else if(ast.CurrentData.Equals("並替条件"))
			{
				sortSeq++;
			}
		}

		/// <summary>
		/// データ変更
		/// </summary>
		private void ChangeData()
		{
            try
            {
                DataTable inp = tableSet.Tables["入力"];
                DataTable dt = tableSet.Tables[ast.CurrentData].Clone();
                DataRow dr_modify = dt.NewRow();

                inputDrv.BeginEdit();
                bool bNull = false;
                bool IsChanged = false;

                if ((!ast.CurrentData.Equals("検索条件")) && (!ast.CurrentData.Equals("並替条件")))
                {
                    //テーブル制約チェック（口座Table/取引先Table)
                    //TableRestCheck();
                }
                for (int i = 0; i < inp.Rows.Count; i++)
                {
                    if (dmf.dataGridView[4, i].Value.ToString().Equals("演算子") && dmf.dataGridView[2, i].Value.ToString().Equals("IS NULL")
                        && ast.CurrentData.Equals("検索条件"))
                    {
                        bNull = true;
                    }
                    if (dmf.dataGridView[4, i].ToString().Equals("値") && bNull)
                    {
                        // エラー回避するためスペースに変換
                        dmf.dataGridView[2, i].Value = " ";
                    }
                    

                    if (dmf.dataGridView[2, i].Value != DBNull.Value)
                    {
                        this.CheckInput(i);
                        dr_modify[dmf.dataGridView[4, i].Value.ToString()] = dmf.dataGridView[2, i].FormattedValue.ToString();
                       
                        if (inputDrv[dmf.dataGridView[4, i].Value.ToString()] != dmf.dataGridView[2, i].Value)
                        {
                            try
                            {
                                IsChanged = true;
                                inputDrv[dmf.dataGridView[4, i].Value.ToString()] = dmf.dataGridView[2, i].Value;
                            }
                            catch
                            {
                                dataGrid.CurrentCell = new DataGridCell(2, i);
                                throw;
                            }
                        }
                    }
                    this.OriginalCheckInput(i);
                }
                if (!IsChanged)
                {
                    inputDrv.CancelEdit();
                    throw new Exception("何も変更されていません。");
                }
                dt.Rows.Add(dr_modify);
                inputDrv.EndEdit();
            }
            catch 
            { 
                inputDrv.CancelEdit();
                throw;
            }
		}

        /// <summary>
        /// 入力チェック
        /// </summary>
        private void CheckInput(int i)
        {
			string colName = dmf.dataGridView[4,i].Value.ToString();
            string inpValue = dmf.dataGridView[2,i].Value.ToString();

			//列名「～カナ]は全銀半角文字チェック
			Regex re = new Regex("カナ");
			Match ma = re.Match(colName);
			if( ma.Success && KanaCheck(inpValue) == false){
				//dataGrid.CurrentCell = new DataGridCell(i,2);
				throw new Exception("列 "+ colName + " に <"+ inpValue + " > を格納できませんでした。入力可能なデータは全銀半角文字です。");
			}

			//列名「～漢字」は全角文字チェック　
			re = new Regex("漢字");
			ma = re.Match(colName);
			if( ma.Success && KanjiCheck(inpValue) == false){
				dataGrid.CurrentCell = new DataGridCell(i,2);
				throw new Exception("列 "+ colName + " に <"+ inpValue + " > を格納できませんでした。入力可能なデータは全角です。");			
			}
		}

        /// <summary>
        /// 個別入力チェック
        /// </summary>
        private void OriginalCheckInput(int i)
        {
            string colName = dmf.dataGridView[4, i].Value.ToString();
            string inpValue = dmf.dataGridView[2, i].Value.ToString();
            string ErrMsg = "";

            ErrMsg = theICheck.IsOk(colName, inpValue);
            if (ErrMsg.Length != 0)
            {
                throw new Exception(ErrMsg);
            }
        }

		/// <summary>
		/// データ確定
		/// </summary>
		private void FinalizeData()
		{
			if (ast.InputMode == 0)
			{
				AddData();
			}
			else if (ast.InputMode == 1)
			{
				ChangeData();
			}
		}

		private void mainForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (ast.AfterMenu)
			{
				ast.AfterMenu = false;
				return;
			}

			if (ast.GridMode == 2)
			{
				DataView dv = (DataView)dataGrid.DataSource;
				if (dataGrid.CurrentCell.ColumnNumber == 2 && dataGrid.CurrentCell.RowNumber + 1 == dv.Count)
				{
//					DataGridColumnStyle dgc;
//
//					switch (e.KeyCode)
//					{
//						case Keys.Enter:
//							dgc = dataGrid.TableStyles["入力"].GridColumnStyles["値"];
//							dataGrid.EndEdit(dgc, dataGrid.CurrentRowIndex, false);
//							dataGrid.BeginEdit(dgc, dataGrid.CurrentRowIndex);
//							DataGridTextBoxColumn dgt = (DataGridTextBoxColumn)dgc;
//							dgt.TextBox.Select(dgt.TextBox.Text.Length, 0);
//							break;
//						case Keys.Down:
//							dgc = dataGrid.TableStyles["入力"].GridColumnStyles["値"];
//							dataGrid.EndEdit(dgc, dataGrid.CurrentRowIndex, false);
//							dataGrid.BeginEdit(dgc, dataGrid.CurrentRowIndex);
//							break;
//					}
				}
//				if(dataGrid.CurrentCell.ColumnNumber == 2 && e.KeyCode == Keys.Down && dataGrid.CurrentCell.RowNumber + 1 == dv.Count){
//	//				dataGrid.CurrentCell = new DataGridCell(0, 2);
//				}
//				else if(dataGrid.CurrentCell.ColumnNumber == 2 && e.KeyCode == Keys.Up && dataGrid.CurrentCell.RowNumber  == 0){
//	//				dataGrid.CurrentCell = new DataGridCell(dv.Count - 1, 2);			
//				}
			} 
			else if (ast.GridMode == 1)
			{
				if (dataGrid.CurrentRowIndex > -1) 
				{
					if (e.KeyCode == Keys.Enter)
					{
						//StartChangingData();
					}
				}
			}
		}

		private void menuItemShowAdd_Click(object sender, System.EventArgs e)
		{
            try
            {
                ShowEditedData(DataViewRowState.Added);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        private void menuItemShowWhole_Click(object sender, System.EventArgs e)
		{
            try
            {
                ShowWhole();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        private void ShowWhole()
        {
            RefreshButtonEnable();
            DataView dv = ConditionSet();
            ShowTable("全体");
            dataGrid.CaptionText = String.Format("全件データ  {0:#,##0}件  並替条件:{1}", dv.Count, dv.Sort);
        }

		protected DataView ConditionSet(){
			DataView dv = tableSet.Tables["全体"].DefaultView;
			dv.RowStateFilter = DataViewRowState.CurrentRows;
			dv.Sort = orderByClause;
			dv.RowFilter = " ";
			return dv;
		}

		private void menuItemSortClear_Click(object sender, System.EventArgs e)
		{
            try
            {
                DataTable dt = tableSet.Tables["並替条件"];
                dt.Rows.Clear();
                sortSeq = 0;

                DataView dv = tableSet.Tables["全体"].DefaultView;
                dv.Sort = orderByClause;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		private void menuItemShowSelectedData_Click(object sender, System.EventArgs e)
        {
            try
            {
                ShowSelectedData(DataViewRowState.CurrentRows);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		public bool ShowSelectedData(DataViewRowState drs) {
            try
            {
                DataView dv = SelectedConditionSet();
                dv.RowStateFilter = drs;
                ShowTable("全体");
                dataGrid.CaptionText = String.Format("検索データ  {0:#,##0}件   検索条件:{1}     並替条件:{2}", dv.Count, dv.RowFilter, dv.Sort);
                //SaveSelectHistory();
                RefreshButtonEnable();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
		}

		protected DataView SelectedConditionSet(){
			DataView dv = tableSet.Tables["全体"].DefaultView;
			try {
				dv.RowStateFilter = DataViewRowState.CurrentRows;
				dv.Sort = orderByClause;
				dv.RowFilter = whereClause;
			} 
			catch (Exception ex) {
				MessageBox.Show(this,"条件を見直して再度検索してください。" + ex.Message,"検索データ",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                throw ex;
			} 
			return dv;
		}

		/// <summary>
		/// 検索条件と並替条件をXMLで保存
		/// </summary>
		private void SaveSelectHistory(){

			string fileName = "Hist_"+ast.TableName;

			DataSet dts = new DataSet(fileName);

			DataTable dtSearch = this.tableSet.Tables["検索条件"].Copy();
			DataTable dtSort = this.tableSet.Tables["並替条件"].Copy();

			dts.Tables.Add(dtSearch);
			dts.Tables.Add(dtSort);

     		dts.WriteXml(fileName+".xml");
		}

		/// <summary>
		/// 全体件数の取得
		/// </summary>
		private int GetTotalCount()
		{
			DataView dv = new DataView(tableSet.Tables["全体"]);
			dv.RowStateFilter = DataViewRowState.CurrentRows;
			dv.Sort = orderByClause;
			dv.RowFilter = "";
			return dv.Count;
		}

        private void RefreshButtonEnable()
        {
            if (hasData(DataViewRowState.Added) ||
                hasData(DataViewRowState.ModifiedCurrent) ||
                hasData(DataViewRowState.Deleted))
            {
                this.CancelButton.Enabled = true;
                this.PrintButton.Enabled = true;
                this.FinalizeButton.Enabled = true;
            }
            else
            {
                this.CancelButton.Enabled = false;
                this.PrintButton.Enabled = false;
                this.FinalizeButton.Enabled = false;
            }
            if (!mhaskey)
            {
                this.EditButton.Enabled = false;
            }
        }

		/// <summary>
		/// 編集データの表示
		/// </summary>
		private void ShowEditedData(DataViewRowState dvs)
		{
            RefreshButtonEnable();
			DataView dv = tableSet.Tables["全体"].DefaultView;
			dv.RowStateFilter = dvs;
			dv.Sort = orderByClause;
			dv.RowFilter = "";

			ShowTable("全体");

			String title = "";
			switch (dvs)
			{
				case DataViewRowState.Added:
					title = "追加データ";
					break;
				case DataViewRowState.Deleted:
					title = "削除データ";
					break;
				case DataViewRowState.ModifiedCurrent:
					title = "変更データ(変更後)";
					break;
				case DataViewRowState.ModifiedOriginal:
					title = "変更データ(変更前)";
					break;
			}
			dataGrid.CaptionText = String.Format("{0}   {1:#,##0}件  並替条件:{2}", title, dv.Count, dv.Sort);
        }

		private void menuItemDelete_Click(object sender, System.EventArgs e)
		{
            try
            {
                DataView dv;
                if (dataGrid.CurrentRowIndex == -1)
                {
                    return;
                }

                switch (ast.CurrentData)
                {
                    case "検索条件":
                    case "並替条件":
                    case "全体":
                        dv = (DataView)dataGrid.DataSource;
                        if (dv.RowStateFilter != DataViewRowState.CurrentRows)
                        {
                            return;
                        }
                        break;
                    default:
                        return;
                }

                dv = tableSet.Tables[ast.CurrentData].DefaultView;
                DataRowView drv = dv[dataGrid.CurrentRowIndex];
                drv.Delete();
                mDelArray.Add(drv);

                if (ast.CurrentData == "全体")
                {
                    if (!ast.InTransaction)
                    {
                        ast.InTransaction = true;
                    }
                    ShowEditedData(DataViewRowState.Deleted);
                }
                else
                {
                    ShowTable(ast.CurrentData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        private void menuItemShowDel_Click(object sender, System.EventArgs e)
		{
            try
            {
                ShowEditedData(DataViewRowState.Deleted);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        private void menuItemChange_Click(object sender, System.EventArgs e)
		{
            try
            {
                if (dataGrid.CurrentRowIndex == -1)
                {
                    return;
                }

                switch (ast.CurrentData)
                {
                    case "検索条件":
                    case "並替条件":
                    case "全体":
                        DataView dv = (DataView)dataGrid.DataSource;
                        if (dv.RowStateFilter != DataViewRowState.CurrentRows)
                        {
                            return;
                        }
                        break;
                    default:
                        return;
                }
                StartChangingData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		/// <summary>
		/// データ変更開始
		/// </summary>
		private void StartChangingData()
		{
			ast.InputMode = 1;
			//dataGrid.CaptionText = "";

			PrepareInputTable();

			DataTable dt = tableSet.Tables["入力"];
			inputDrv = tableSet.Tables[ast.CurrentData].DefaultView[dataGrid.CurrentRowIndex];

			for (int i=0; i< dt.Rows.Count ; i++)
			{
				DataRow dr = dt.Rows[i];
				dr[2] = inputDrv[i];
			}

            ShowInputTable((Int16)DataManipulationForm.Mode.MODIFY);
		}

        /// <summary>
        /// データ削除開始
        /// </summary>
        private void StartDeletingData()
        {
            ast.InputMode = 1;
            //dataGrid.CaptionText = "";

            PrepareInputTable();

            DataTable dt = tableSet.Tables["入力"];
            inputDrv = tableSet.Tables[ast.CurrentData].DefaultView[dataGrid.CurrentRowIndex];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                dr[2] = inputDrv[i];
            }
            ShowInputTable((Int16)DataManipulationForm.Mode.DELETE);
        }

        private string getHelp(DataColumn dc)
        {
            string rtn="";
            
            if(dc.ColumnName.Contains("カナ"))
            {
                rtn = "半角カナで入力して下さい。";
            }
            else if (dc.ColumnName.Contains("漢字"))
            {
                rtn = "全角漢字で入力して下さい";
            }
            else if (dc.ColumnName.Contains("番号") && dc.DataType.Name.Equals("String"))
            {
                rtn = "半角の数値を入力してください。";
            }
            else 
            {
                switch (dc.DataType.Name)
                {
                    case ("String"):
                        rtn = "半角または全角の文字列を入力してください。";
                        break;
                    case ("Int16"):
                        rtn = "数値で入力してください。";
                        break;
                    case ("Int32"):
                        rtn = "数値で入力してください。";
                        break;
                }
            }
            rtn = rtn + "（最大桁：" + GetCaption(dc, 0) + ")";

            if (dc.AllowDBNull)
            {
                return rtn;
            }
            else 
            {
                rtn = "〈必須〉" + rtn;
                return rtn;
            }
        }

        private string GetCaption(DataColumn dc, int idx)
        {
            string[] captions = dc.Caption.Split(new string[] { "," }, StringSplitOptions.None);
            if (captions.Length < (idx + 1))
            {
                return "";
            }
            return captions[idx];
        }

		/// <summary>
		/// 入力テーブルの準備
		/// </summary>
		private void PrepareInputTable()
		{
			int i = 0;

			DataTable dt = tableSet.Tables["入力"];
			dt.Clear();
			ast.InputStart = 0;

			foreach (DataColumn dc in tableSet.Tables[ast.CurrentData].Columns)
			{
				DataRow dr = dt.NewRow();
				dr["列順"] = i++;
                dr["列名"] = dc.ColumnName;
                dr["列"] = GetCaption(dc, 1);
                dr["入力補助"] = this.getHelp(dc);
                if (dc.ColumnName == "処理区分")
                {
					dr["入力補助"] = "1:新規登録 2:変更登録 3:削除登録";
					if(ast.InputMode == 0){
						ast.InputStart = 1;
						dr["値"] = 1;
					}
				}
				if(dc.ColumnName == "順番") 
				{
					if(ast.CurrentData.Equals("検索条件"))
					{
						dr["値"] = searchSeq;
					}
					else if(ast.CurrentData.Equals("並替条件"))
					{
						dr["値"] = sortSeq;
					}
					dr["入力補助"] = "自動採番";
				}

				if (dc.AutoIncrement)
				{
					dr["入力補助"] = "自動採番";
				} 
				else if (dc.DefaultValue != DBNull.Value)
				{
					dr["入力補助"] = "既定値 [" + dc.DefaultValue + "]";
				}
                //個別入力補助設定
                dr["入力補助"] = theICheck.SetColumnInfo(dc.ColumnName, dr["入力補助"].ToString());
				dt.Rows.Add(dr);
			}
			
			foreach (DataRelation rel in tableSet.Tables[ast.CurrentData].ParentRelations)
			{
				i = -1;
				foreach (DataColumn dc in rel.ChildColumns)
				{
					i = i + 1;
					DataRow[] dr = dt.Select("列名 = '" + dc.ColumnName + "'");
					if (dr[0]["入力補助"] != DBNull.Value)
					{
						dr[0]["入力補助"] = dr[0]["入力補助"] + "  ";
					}
					dr[0]["入力補助"] = dr[0]["入力補助"] + "参照 [" + rel.ParentColumns[i].Table.TableName + "," + rel.ParentColumns[i].ColumnName + "]";
				}
			}
		}

		/// <summary>
		/// 入力テーブルの表示
		/// </summary>
		private void ShowInputTable(Int16 Mode)
		{
            dmf = new DataManipulationForm(this, Mode);
            
			DataTable dt = tableSet.Tables["入力"];

            dmf.dataGridView.DataSource = dt.DefaultView;
            dmf.dataGridView.ReadOnly = false;

			int row = 0;
			if (ast.CurrentData == "検索条件")
			{
				row = 2;
			} 
			else if (ast.CurrentData == "並替条件") 
			{
				row = 1;
			} 
			else 
			{
				if (ast.InputStart > 0 && ast.InputStart < dt.Columns.Count)
				{
					row = ast.InputStart;
				}
			}

            ast.GridMode = 2;
            dmf.Activate();
            dmf.Show();
		}

		private void menuItemShowBefore_Click(object sender, System.EventArgs e)
		{
            try
            {
                ShowEditedData(DataViewRowState.ModifiedOriginal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		private void menuItemShowAfter_Click(object sender, System.EventArgs e)
		{
            try
            {
                ShowEditedData(DataViewRowState.ModifiedCurrent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }
		
		private void menuItemShowBA_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.ShowEditedBAData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		private void menuItemShowAddModifyDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.ShowEditedBAData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		private void ShowEditedBAData(){
			DataRow dr;
			DataTable dt = tableSet.Tables["メンテナンスログ"];
			dt.Rows.Clear();

			DataView dv = tableSet.Tables["全体"].DefaultView;
			dv.Sort = orderByClause;
			dv.RowFilter = "";

			int i = 0;
			
			dv.RowStateFilter = DataViewRowState.Added;
			foreach(DataRowView drv in dv){
				i++;
				dr = dt.NewRow();
				dr["No"] = Convert.ToString(i).PadLeft(3,'0')+"  追加";
				foreach(DataColumn dcol in dv.Table.Columns){
					dr[dcol.ColumnName] = drv.Row[dcol.ColumnName,DataRowVersion.Current];
				}
				dt.Rows.Add(dr);
			}

			dv.RowStateFilter = DataViewRowState.ModifiedOriginal;
			foreach(DataRowView drv in dv){
				i++;
				dr = dt.NewRow();
				dr["No"] = Convert.ToString(i).PadLeft(3,'0')+"  変更前";
				foreach(DataColumn dcol in dv.Table.Columns){
					dr[dcol.ColumnName] = drv.Row[dcol.ColumnName,DataRowVersion.Original];
				}
				dt.Rows.Add(dr);

				dr = dt.NewRow();
				dr["No"] = Convert.ToString(i).PadLeft(3,'0')+"　変更後";
				foreach(DataColumn dcol in dv.Table.Columns){
					dr[dcol.ColumnName] = drv.Row[dcol.ColumnName,DataRowVersion.Current];
				}
				dt.Rows.Add(dr);
			}

			dv.RowStateFilter = DataViewRowState.Deleted;
			foreach(DataRowView drv in dv){
				i++;
				dr = dt.NewRow();
				dr["No"] = Convert.ToString(i).PadLeft(3,'0')+"  削除";
				foreach(DataColumn dcol in dv.Table.Columns){
					dr[dcol.ColumnName] = drv.Row[dcol.ColumnName,DataRowVersion.Original];
				}
				dt.Rows.Add(dr);
			}

			dv = tableSet.Tables["メンテナンスログ"].DefaultView;
			dv.Sort = "No ASC";
			dv.RowStateFilter = DataViewRowState.CurrentRows;
			ShowTable("メンテナンスログ");

			string title = "追加/変更(前後)/削除 データ";

			dataGrid.CaptionText = String.Format("{0}   {1:#,##0}件  並替条件:{2}", title, dv.Count, dv.Sort);
		}

		private void menuItemRef_Click(object sender, System.EventArgs e)
		{
            try
            {
                Regex re = new Regex(@"参照 \[(.+),(.+)\]");
                if (dataGrid[dataGrid.CurrentRowIndex, 3] == DBNull.Value)
                {
                    return;
                }
                Match ma = re.Match((string)dataGrid[dataGrid.CurrentRowIndex, 3]);
                if (!ma.Success)
                {
                    return;
                }

                DataTable dt = new DataTable("参照テーブル");
                dt.Columns.Add("参照値", System.Type.GetType("System.String"));

                foreach (DataRow dr in tableSet.Tables[ma.Groups[1].Value].Rows)
                {
                    DataRow row = dt.NewRow();
                    row["参照値"] = dr[ma.Groups[2].Value].ToString();
                    dt.Rows.Add(row);
                }

                refForm rf = new refForm(ast);
                rf.Text = ma.Groups[1].Value;
                rf.CaptionText = ma.Groups[2].Value;
                dt.DefaultView.AllowNew = false;
                rf.dataView = dt.DefaultView;

                if (rf.ShowDialog(this) == DialogResult.OK)
                {
                    dataGrid[dataGrid.CurrentRowIndex, 2] = rf.SelectedText;
                    DataGridColumnStyle dgc = dataGrid.TableStyles["入力"].GridColumnStyles["値"];
                    dataGrid.EndEdit(dgc, dataGrid.CurrentRowIndex, false);
                    dataGrid.BeginEdit(dgc, dataGrid.CurrentRowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		private void mainForm_MenuComplete(object sender, System.EventArgs e)
		{
            try
            {
                ast.AfterMenu = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		private void mainForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{ 
			if (ast.AfterMenu)
			{
				ast.AfterMenu = false;
			}
		}

		private void dataGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (ast.AfterMenu)
			{
				ast.AfterMenu = false;
			}
		}

		private string reportTitle()
		{
			string title;

			DataView dv = tableSet.Tables[ast.CurrentData].DefaultView;

			if (ast.CurrentData == "全体") {
				title = ast.TableName;
				switch (dv.RowStateFilter) {
					case DataViewRowState.Added:
						title = title + " 追加データ";
						break;
					case DataViewRowState.ModifiedOriginal:
						title = title + " 変更データ(前)";
						break;
					case DataViewRowState.ModifiedCurrent:
						title = title + " 変更データ(後)";
						break;
					case DataViewRowState.Deleted:
						title = title + " 削除データ";
						break;
				}
			}else if(ast.CurrentData == "メンテナンスログ" ){

				title = ast.TableName + " 追加/変更(前後)/削除 データ";
			}
			else {
				title = ast.CurrentData;
			}
			return title;
		}

		private void pd_BeginPrint(object sender, PrintEventArgs ev) {
			titleFont = new Font(ast.TitleFont, ast.TitleFontSize, FontStyle.Bold);
			bodyFont = new Font(ast.BodyFont, ast.BodyFontSize);
			bodyColumnFont = new Font(ast.BodyFont, ast.BodyFontSize, FontStyle.Bold | FontStyle.Underline);

			reportRec = 0;
			nowPage = 0;
		}

		private void pd_PrintPage(object sender, PrintPageEventArgs ev) 
		{
			DataView dv = tableSet.Tables[ast.CurrentData].DefaultView;

			float[] fwidth;
			float lineHeight;
			int numLines;
			SizeF sf;
			float xPos;
			float yPos;

			xPos = ev.MarginBounds.X;
			yPos = ev.MarginBounds.Y;
			sf = ev.Graphics.MeasureString("M", bodyFont);
			lineHeight = sf.Height;

			nowPage ++;

			ev.Graphics.DrawString(reportTitle(), titleFont, Brushes.Black, xPos, yPos);

			System.Text.Encoding ec = System.Text.Encoding.GetEncoding("shift-jis");

			if(dataGrid.CaptionText.IndexOf("検索データ") >= 0){
 
				if (whereClause != null){
					yPos = yPos + lineHeight * 2;

					string whr = "(検索条件 : " + whereClause + ")";
					int whrStart = 0;
					int whrLength = 0;
					xPos += ev.Graphics.MeasureString("MM", bodyFont).Width;
					while(true){
						whrLength ++;
						sf = ev.Graphics.MeasureString(whr.Substring(whrStart,whrLength), bodyFont);
						if(xPos + sf.Width >= ev.MarginBounds.Right ){
							ev.Graphics.DrawString(whr.Substring(whrStart,whrLength - 1), bodyFont, Brushes.Black, xPos, yPos);
							whrStart += whrLength;
							whrLength = 0;
							yPos = yPos + lineHeight;
						}
						if(whrStart + whrLength >= whr.Length){
							ev.Graphics.DrawString(whr.Substring(whrStart,whrLength), bodyFont, Brushes.Black, xPos, yPos);
							break;
						}
					}
					xPos = ev.MarginBounds.X;
				}
			}

			yPos = yPos + lineHeight * 2;

			fwidth = new Single[dv.Table.Columns.Count];
            for (int i = 0; i < dv.Table.Columns.Count; i++)
            {
                int flen;

                DataColumn dc = dv.Table.Columns[i];

                if (dc.DataType == System.Type.GetType("System.Decimal"))
                {
                    flen = GetPrecisionScale(dc);
                }
                else
                {
                    flen = dc.MaxLength;
                }

                int bcount = ec.GetByteCount(dc.ColumnName);
                string fname;

                if (bcount < flen)
                {
                    fname = dc.ColumnName + "".PadLeft(flen - bcount, 'M');
                }
                else
                {
                    fname = dc.ColumnName;
                }
                sf = ev.Graphics.MeasureString(fname, bodyFont);
                fwidth[i] = sf.Width;
            }

			numLines = 1;
			for (int i = 0; i < dv.Table.Columns.Count; i++)
			{
				DataColumn dc = dv.Table.Columns[i];

				if (xPos + fwidth[i] > ev.MarginBounds.Right)
				{
					xPos = ev.MarginBounds.X;
					yPos = yPos + lineHeight;
					numLines++;
					if (yPos > ev.MarginBounds.Bottom)
					{
						return;
					}
				}
				ev.Graphics.DrawString(dc.ColumnName, bodyColumnFont, Brushes.Black, xPos, yPos);
				if (i + 1 == dv.Table.Columns.Count)
				{
					yPos = yPos + lineHeight;
					if (yPos > ev.MarginBounds.Bottom)
					{
						return;
					}
				} 
				else 
				{
					xPos = xPos + fwidth[i];
				}
			}

			yPos = yPos + lineHeight;

			int recordsPerPage = 0;
			int numLinesPlusSpace;

			if(numLines > 5){
				numLinesPlusSpace =  numLines + 2;
			}
			else{
				numLinesPlusSpace =  numLines + 1;
			}
			while(true){
				recordsPerPage ++;
				if( yPos + lineHeight * numLinesPlusSpace * recordsPerPage > ev.MarginBounds.Bottom ){
					recordsPerPage -= 1;
					break;
				}				
			}
			int totalPages = ( dv.Count + recordsPerPage - 1 ) / recordsPerPage;

			int startRec = 0;
			for(int j = reportRec; j < dv.Count; j++)
			{
				DataRowView drv = dv[j];

				if (yPos + lineHeight * numLines > ev.MarginBounds.Bottom)
				{
					ev.HasMorePages = true;
					break;
				}

				xPos = ev.MarginBounds.X;

				for (int i = 0; i < dv.Table.Columns.Count; i++)
				{
					if (xPos + fwidth[i] > ev.MarginBounds.Right)
					{
						xPos = ev.MarginBounds.X;
						yPos = yPos + lineHeight;
						if (yPos > ev.MarginBounds.Bottom)
						{
							break;
						}
					}
					ev.Graphics.DrawString(Convert.ToString(drv[i]), bodyFont, Brushes.Black, xPos, yPos);
					if (i + 1 == dv.Table.Columns.Count)
					{
						yPos = yPos + lineHeight;
						if (yPos > ev.MarginBounds.Bottom)
						{
							break;
						}
					} 
					else 
					{
						xPos = xPos + fwidth[i];
					}
				}
				reportRec++;
				if (startRec == 0)
				{
					startRec = reportRec;
				}
				if(numLines > 5){
					yPos = yPos + lineHeight * 2;
				}
				else{
					yPos = yPos + lineHeight;
				}
			}

			StringFormat fm = new StringFormat();
			fm.Alignment = StringAlignment.Far;
			ev.Graphics.DrawString(String.Format("({0:#,##0}件中 {1:#,##0}-{2:#,##0}件目)      処理日 : {3}    {4}      {5}/{6}頁", dv.Count, startRec, reportRec, headerToday, System.DateTime.Now.ToShortTimeString(), nowPage, totalPages), bodyFont, Brushes.Black, ev.MarginBounds, fm);
			//@@@ 帳票IDをページフッターに出力
			//ev.Graphics.DrawString("( 013-0001 )", bodyFont, Brushes.Black, ev.MarginBounds.X+70  ,ev.MarginBounds.Height+30, fm);
		}

		private bool KanjiCheck( string checkString){

			return this.IsJISX0208(checkString);
		}

		private bool KanaCheck( string checkString){
			for(int loopCounter = 0; loopCounter < checkString.Length; loopCounter ++ ){
				char checkChar = checkString[loopCounter];
				if ( isNotHogeChar(checkChar) == false ){
					return false;
				}
			}
			return true;
		}

		public bool IsJISX0208(string Text) {

			bool ret = true;
			uint iWk1;
			uint iWk2;
			uint iWk3;

			System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("shift-jis");
			byte [] myByte = encoding.GetBytes(Text);

			for (int i=0; i<myByte.Length; i=i+2 ) {
				iWk1 = myByte[i];
				if ( i+1 < myByte.Length ) {
					iWk2 = myByte[i+1];
					iWk3 = iWk1 << 8;
					iWk3 |= iWk2;

					if ( ( (iWk3 < 0x8140) || (iWk3 > 0x84be) )
						&& ( (iWk3 < 0x889f) || (iWk3 > 0x9872) )
						&& ( (iWk3 < 0x989f) || (iWk3 > 0x9ffc) )
						&& ( (iWk3 < 0xe040) || (iWk3 > 0xeaa4) )
						) {
						ret = false;
						break;
					} else if (iWk3==0x0814a || iWk3==0x0814b) {
						ret = false;
						break;
					}else if ( iWk3==0x8140 ){
						if ( i == 0 || i+2==myByte.Length){
							ret = false;
							break;
						}
					}
				} else {
					ret = false;
				}
			}
			return ret;
		}

		private bool isNotHogeChar(char e) {
			bool bRtn = true;
			
			/*0xff71 0xff9d 0xff9e 0x20 0x28 0x29 0x2d 0x30 0x39*/
			/* 0xff67*/
			
			if ( ( (e < 0xff71 ) || (e > 0xff9f) )			/*カナ大文字*/
				&& (e != 0xff66)							/* ｦ */
				&& (e != 0x20 )								/*   */	
				&& (e != 0x28 )								/* ( */	
				&& (e != 0x29 )								/* ) */
				&& (e != 0x2c )								/* , */
				&& (e != 0x2d )								/* - */
				&& (e != 0x2e )								/* . */
				&& (e != 0x2f )								/* / */
				&& ( (e <  0x30 || e > 0x39) )				/*数字*/
				&& ( (e < 0x41 || e > 0x5a ) )				/*英大文字*/
				) {

				bRtn = false;
			}
			return bRtn;
		}

		private void menuItemData_Popup(object sender, System.EventArgs e)
        {
            try
            {
                if (ast.CurrentData == "検索条件" || ast.CurrentData == "並替条件")
                {
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		private void dataGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            try
            {
                //ここで上下キー押した場合の幅が前のセルと同じになる不具合の修正をする？
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        private void menuItemQueryHistory_Click(object sender, System.EventArgs e)
        {
            try
            {
                MenuItem mi = (MenuItem)sender;
                string[] tab = mi.Text.Split(' ');

                ShowTable(tab[0]);

                switch (tab[0])
                {
                    case "列名":
                    case "入力規則":
                    case "列定義":
                    case "制約":
                    case "制約列":
                    case "インデックス":
                    case "インデックス列":
                    case "列コメント":
                    case "テーブルコメント":
                        ast.GridMode = 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		private void menuItemQueryHistory1_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.tableSet.ReadXml("Hist_" + ast.TableName + ".xml", XmlReadMode.Auto);
            }
            catch (System.IO.FileNotFoundException)
            {
                //履歴ファイル無しは続行
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

		private void menuItemDeleteCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataView dv;
				if (dataGrid.CurrentRowIndex == -1)
				{
					return;
				}

				switch(ast.CurrentData)
				{
					case "検索条件":
					case "並替条件":
					case "全体":
						dv = (DataView)dataGrid.DataSource;
						if(dv.RowStateFilter != DataViewRowState.Deleted)
						{
							return;
						}
						//テーブル制約チェック（口座Table)
						//TableRestCheck(true);
						break;
					default:
						return;
				}
	
				CancelDeletedRecord();

				if (ast.CurrentData == "全体") 
				{
					if (!ast.InTransaction)
					{
						ast.InTransaction = true;
					}
					ShowEditedData(DataViewRowState.Deleted);
				} 
				else 
				{
					ShowTable(ast.CurrentData);
				}
			}
			catch (OdbcException oe)
			{
				MessageBox.Show(this, oe.Message,"削除取消", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} 
			catch (Exception ex)
			{
				string[] mes = ex.Message.Split('\n');
				if (mes.Length > 1) 
				{
					string msg = mes[mes.Length -1];
					int pos = msg.IndexOf(")");
					MessageBox.Show(this, msg.Substring(pos + 1), "削除取消", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				} 
				else 
				{
					MessageBox.Show(this, mes[0], "削除取消", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				return;
			}
		}

		private void CancelDeletedRecord()
		{
			DataView dv = tableSet.Tables[ast.CurrentData].DefaultView;
			DataRowView drvCurrent = dv[dataGrid.CurrentRowIndex];
			
			foreach(DataRowView drv in mDelArray)
			{
				if (drvCurrent.Equals(drv))
				{
					drv.Row.RejectChanges();
				}
			}
		}

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                switch (ast.CurrentData)
                {
                    case "検索条件":
                    case "並替条件":
                    case "全体":
                        break;
                    default:
                        return;
                }

                ast.InputMode = 0;

                //CaptionText = "追加中";
                PrepareInputTable();
                ShowInputTable((Int16)DataManipulationForm.Mode.ADD);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGrid.CurrentRowIndex == -1)
                {
                    return;
                }

                switch (ast.CurrentData)
                {
                    case "検索条件":
                    case "並替条件":
                    case "全体":
                        DataView dv = (DataView)dataGrid.DataSource;
                        if (dv.RowStateFilter != DataViewRowState.CurrentRows)
                        {
                            return;
                        }
                        break;
                    default:
                        return;
                }
                StartChangingData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        private void DelButton_Click(object sender, EventArgs e)
        {
            try
            {
                StartDeletingData();
            }
            catch (System.IndexOutOfRangeException)
            {
                MessageBox.Show("削除対象のデータが存在しません。");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        public void delStart()
        {
            DataView dv;
            if (dataGrid.CurrentRowIndex == -1)
            {
                return;
            }

            switch (ast.CurrentData)
            {
                case "検索条件":
                case "並替条件":
                case "全体":
                    dv = (DataView)dataGrid.DataSource;
                    if (dv.RowStateFilter != DataViewRowState.CurrentRows)
                    {
                        return;
                    }
                    break;
                default:
                    return;
            }
            dv = tableSet.Tables[ast.CurrentData].DefaultView;
            DataRowView drv = dv[dataGrid.CurrentRowIndex];
            drv.Delete();
            mDelArray.Add(drv);

            if (ast.CurrentData == "全体")
            {
                if (!ast.InTransaction)
                {
                    ast.InTransaction = true;
                }
                //ShowEditedData(DataViewRowState.Deleted);
                ShowTable(ast.CurrentData);
            }
            else
            {
                ShowTable(ast.CurrentData);
            }
        }

        private string ConvertMultiLineText(string inputStr)
        {
            string outputStr = "";
            string[] ss = inputStr.Split('\n');
            outputStr= "'" + ss[0] + "'";

            int counter = 1;
            foreach (string s in ss)
            {
                if (counter > 1)
                {
                    outputStr = outputStr + '+' + "ChrW(13)" + '+' + "'" + s + "'";
                }
                counter = counter + 1;
            }
            
            return outputStr;
        }

        #region 編集時の印刷

        private void PrintButton_Click(object sender, EventArgs e)
        {
            try
            {
                CrystalForm cf = new CrystalForm();
                cf.Activate();
                cf.Show();

                this.UpdatePrintSub(cf);
                this.FinalizeButton.Enabled = true;
                this.FinalizeButton.Focus();
                cf.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        protected virtual void UpdatePrintSub(CrystalForm cf)
        {
            PrepareData(cf);

            SetReportFormula(updateList1);

            updateList1.SetDataSource(this.PrintWorkObject);
            cf.crystalReportViewer1.ReportSource = updateList1;
            cf.crystalReportViewer1.Zoom(cf.DefaultZoom);
        }

        private void PrepareData(CrystalForm cf)
        {
            List<List<string>> list = ListedData();            
            this.SetOnPrintWork(MakeMaxLengthRow(list,cf),150);  
        }

        private List<List<string>> ListedData()
        {
            List<List<string>> result = new List<List<string>>();
            AppendHeader(result);
            if(hasData(DataViewRowState.Added))
            {
                AppendAddedData(result);
                
                if (hasData(DataViewRowState.ModifiedCurrent)
                 | hasData(DataViewRowState.Deleted))
                {
                
                    AppendSpace(result);
                }
            }
            if (hasData(DataViewRowState.ModifiedCurrent))
            {
                AppendModifiedData(result);
                
                if (hasData(DataViewRowState.Deleted))
                {
                
                AppendSpace(result);
                }
            }
            AppendDeletedData(result);

            return result;
        }

        protected bool hasData(DataViewRowState dvrs)
        {
            DataViewRowState sv = tableSet.Tables["全体"].DefaultView.RowStateFilter;
            DataView dv = tableSet.Tables["全体"].DefaultView;
            dv.RowStateFilter = dvrs;
            if (dv.Count > 0)
            {
                dv.RowStateFilter = sv;
                return true;
            }
            else
            {
                dv.RowStateFilter = sv;
                return false;
            }
        }

        #region データ書き込み
        private void AppendAddedData(List<List<string>> result)
        {
            DataViewRowState sv = tableSet.Tables["全体"].DefaultView.RowStateFilter;
            
            List<string> col = new List<string>();
            DataView dv = tableSet.Tables["全体"].DefaultView;
            dv.RowStateFilter = DataViewRowState.Added;

            foreach (DataRowView r in dv)
            {
                result.Add(FromDataRowToList(r.Row, "追加分    "));
            }
            dv.RowStateFilter = sv;
        }

        private void AppendSpace(List<List<string>> result)
        {
            List<string> col = new List<string>();
            DataView dv = tableSet.Tables["全体"].DefaultView;

            col.Add(" ");
            foreach (DataColumn dc in dv.Table.Columns)
            {
                col.Add(" ");
            }
            result.Add(col);
        }

        private void AppendHeader(List<List<string>> result)
        {
            List<string> col = new List<string>();
            DataView dv = tableSet.Tables["全体"].DefaultView;

            col.Add("　　　    ");
            foreach (DataColumn dc in dv.Table.Columns)
            {
                col.Add(GetCaption(dc, 1) + "   ");
            }
            result.Add(col);
        }

        private void AppendModifiedData(List<List<string>> result)
        {
            DataViewRowState sv = tableSet.Tables["全体"].DefaultView.RowStateFilter;
            
            List<string> col = new List<string>();
            DataView dv = tableSet.Tables["全体"].DefaultView;
            dv.RowStateFilter = DataViewRowState.ModifiedOriginal;
            DataRow dr;
            foreach (DataRowView r in dv)
            {
                dr = r.DataView.Table.NewRow();
                foreach (DataColumn c in r.DataView.Table.Columns)
                {
                    dr[c.ColumnName] = r.Row[c.ColumnName, DataRowVersion.Original];
                }
                result.Add(FromDataRowToList(dr, "変更前    "));
                dr = r.DataView.Table.NewRow();
                foreach (DataColumn c in r.DataView.Table.Columns)
                {
                    dr[c.ColumnName] = r.Row[c.ColumnName, DataRowVersion.Current];
                }
                result.Add(FromDataRowToList(dr, "変更後    "));
            }
            dv.RowStateFilter = sv;
        }

        private void AppendDeletedData(List<List<string>> result)
        {
            DataViewRowState sv = tableSet.Tables["全体"].DefaultView.RowStateFilter;
            List<string> col = new List<string>();
            DataView dv = tableSet.Tables["全体"].DefaultView;
            dv.RowStateFilter = DataViewRowState.Deleted;
            DataRow dr;
            foreach (DataRowView r in dv)
            {
                dr = r.DataView.Table.NewRow();
                foreach (DataColumn c in r.DataView.Table.Columns)
                {
                    dr[c.ColumnName] = r.Row[c.ColumnName, DataRowVersion.Original];
                }
                result.Add(FromDataRowToList(dr, "削除分    "));
            }
            dv.RowStateFilter = sv;
        }
        #endregion

        #endregion

        #region 照会時の印刷
        private void SearchResultPrintButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (tableSet.Tables[ast.CurrentData].DefaultView.Count > 500)
                {
                    this.Enabled = false;
                    System.Windows.Forms.MessageBox.Show("対象件数が" + tableSet.Tables[ast.CurrentData].DefaultView.Count.ToString() + "件です。"
                        + "\n多すぎるので、500件以内になるよう条件を絞ってください。");
                    this.Enabled = true;
                    return;
                }

                CrystalForm cf = new CrystalForm();
                cf.Activate();
                cf.Show();

                this.SearchResultPrintSub(cf);
                ShowTable(ast.CurrentData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        protected virtual void SearchResultPrintSub(CrystalForm cf)
        {
            PrepareSearchResultData(cf);

            SetReportFormula(searchList1);

            searchList1.SetDataSource(this.PrintWorkObject);
            cf.crystalReportViewer1.ReportSource = searchList1;
            cf.crystalReportViewer1.Zoom(cf.DefaultZoom);
        }

        protected virtual void SetReportFormula(ReportClass rpt)
        {
            rpt.DataDefinition.FormulaFields["Condition"].Text = "'" + dataGrid.CaptionText.Replace("'", "") + "'";
            rpt.DataDefinition.FormulaFields["TableName"].Text = "'" + ast.TableName + " '";

            //共通帳票データセット
            rpt.DataDefinition.FormulaFields["titleArea"].Text = ConvertMultiLineText(mPrintWorkHeader);
            rpt.DataDefinition.FormulaFields["s端末No"].Text = "'" + ProcAplInfo.TermNo.PadLeft(3, '0') + "'";
            rpt.DataDefinition.FormulaFields["s担当者"].Text = "'" + ProcAplInfo.OP_NAME + "'";            
        }

        private void PrepareSearchResultData(CrystalForm cf)
        {
            List<List<string>> list = ListedSearchResultData();
            this.SetOnPrintWork(MakeMaxLengthRow(list, cf), 150);
        }   

        private List<List<string>> ListedSearchResultData()
        {
            List<List<string>> result = new List<List<string>>();
            DataView dv = tableSet.Tables[ast.CurrentData].DefaultView;
            List<string> col = new List<string>();
            col.Add(" ");
            foreach (DataColumn dc in dv.Table.Columns)
            {
                col.Add(GetCaption(dc, 1) + "");
            }
            result.Add(col);
            foreach (DataRowView r in dv)
            {
                result.Add(FromDataRowToList(r.Row,""));
            }
            return result;
        }
        #endregion

        #region 印刷処理汎用
        private void SetOnPrintWork(List<List<string>> list, int insertat)
        { 
            PrintWork.PrintWorkDataTable pwkdt = new PrintWork.PrintWorkDataTable();
            int counter=1;
            foreach(List<string> l in list)
            {
                DataRow dr = pwkdt.NewRow();
                
                string s=this.InsertNewLine(ConnectListedString(l),insertat);
                if (counter == 1)
                { 
                    this.mPrintWorkHeader = s; 
                }
                else 
                {
                    dr["Content"] = this.InsertNewLine(ConnectListedString(l), insertat);
                    pwkdt.Rows.Add(dr);
                }
                counter = counter + 1;                
            }
            this.PrintWorkObject.Tables.Clear();
            this.PrintWorkObject.Tables.Add( (DataTable)pwkdt); 
        }

        private List<string> FromDataRowToList(DataRow dr, string rowhead)
        {
            List<string> list = new List<string>();
            list.Add(rowhead);
            foreach (object obj in dr.ItemArray)
            {
                list.Add(obj.ToString());
            }
            return list;
        }

        private string ConnectListedString(List<string> list)
        {
            string result = "";
            foreach (string str in list)
            {
                result = result + str;
            }
            return result;
        }

        private List<List<string>> MakeMaxLengthRow(List<List<string>> list, CrystalForm cf)
        {
            Int32 FormerCount =0;
            Int32 LatterCount=0;
            Int32 SuccessfulCount = 0;
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            Boolean flg = false;
            cf.progressBar1.Maximum = list.Count*list[0].Count+list.Count*list[0].Count/3;
            cf.progressBar1.Value = 0;
            do
            {
                flg=true;
                SuccessfulCount = 0;
                for (Int32 i = 0; i<list.Count-1;i=i+1 )
                {
                    for (Int32 j = 0; j < list[i].Count; j = j + 1)
                    {
                        FormerCount = 0;
                        LatterCount = 0;
                        FormerCount = sjisEnc.GetByteCount(list[i][j].ToString());
                        LatterCount = sjisEnc.GetByteCount(list[i + 1][j].ToString());
                        if (FormerCount < LatterCount)
                        {
                            StringBuilder sb = new StringBuilder(list[i][j], FormerCount + 1);
                            list[i][j] = sb.Append(' ').ToString();
                            flg = false;
                        }
                        else if (FormerCount > LatterCount)
                        {
                            StringBuilder sb = new StringBuilder(list[i + 1][j], LatterCount + 1);
                            list[i + 1][j] = sb.Append(' ').ToString();
                            flg = false;
                        }
                        else
                        {
                            SuccessfulCount = SuccessfulCount + 1;
                        }
                    }   
                }
                cf.progressBar1.Value = SuccessfulCount;
            } while (flg == false);
            list=FillOneByteAfter(list);
            cf.progressBar1.Value = cf.progressBar1.Maximum;
            return list;
        }

        private List<List<string>> FillOneByteAfter(List<List<string>> list)
        {
            List<List<string>> results=new List<List<string>>();
            List<string> result;
            foreach(List<string> l in list)
            {
                result = new List<string>();
                foreach (string str in l)
                {
                    result.Add(str + " ");
                }
                results.Add(result);
            }
            return results;
        }

        /// <summary>
        /// プリントファイル名取得
        /// </summary>
        /// <param name="ReportID"></param>
        /// <returns></returns>
        private string getPrintFileName(string ReportID, string yyyymmdd)
        {
            return "Rpt_" + yyyymmdd + "_" + ReportID + "_" + Environment.MachineName + "_"
                + System.DateTime.Now.ToString("HHmmss") + ProcAplInfo.ProcessID.ToString().PadLeft(5, '0');
        }

        #endregion

        private string InsertNewLine(string basetext, int at)
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            string result="";
            int i = 0;
            int breakcount = 0;
            StringBuilder sb = new StringBuilder(result);
            foreach (char c in basetext)
            {
                sb.Append(c);
                i = sjisEnc.GetByteCount(sb.ToString());
                if (i % at == 0 || i%at==1)
                {
                    if (breakcount < i / at)
                    {
                        sb.Append("\n     "); //改行コード
                        breakcount = breakcount + 1;
                    }
                }
            }
            result = sb.ToString();
            return result;
        }

        private void InputConditionButton_Click(object sender, EventArgs e)
        {
            try
            {
                SearchCondition SC = new SearchCondition(this);
                SC.Activate();
                SC.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        private void EndButton_Click(object sender, EventArgs e)
        {
            try
            {
                mainForm.ActiveForm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        private void FinalizeButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                DialogResult dr = MessageBox.Show("ここまでの入力内容を確定しますか？", "確定処理", MessageBoxButtons.OKCancel);
                this.Enabled = true;
                if (dr == DialogResult.OK)
                {
                    string msg = ItemUpdate();

                    if (msg == null)
                    {
                        ast.InTransaction = false;
                        tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        adapter.UpdateCommand.Transaction = tran;
                        adapter.InsertCommand.Transaction = tran;
                        adapter.DeleteCommand.Transaction = tran;
                        this.Enabled = false;
                        MessageBox.Show("正常に確定処理が完了しました。", "確定処理");
                        this.Enabled = true;
                        dataGrid.CaptionText = "確定完了";
                        RefreshButtonEnable();
                    }
                    else
                    {
                        this.Enabled = false;
                        MessageBox.Show(this, msg, "確定処理実行", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.Enabled = true;
                    }
                    this.FinalizeButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        public void ItemFinalize()
        {
            try
            {
                FinalizeData();
            }
            catch
            {
                throw;
            }

            if (ast.CurrentData == "全体")
            {
                if (!ast.InTransaction)
                {
                    ast.InTransaction = true;
                }

                //if (ast.InputMode == 0)
                //{
                //    ShowEditedData(DataViewRowState.Added);
                //}
                //else if (ast.InputMode == 1)
                //{
                //    ShowEditedData(DataViewRowState.ModifiedCurrent);
                //}
                ShowTable(ast.CurrentData);
            }
            else
            {
                ShowTable(ast.CurrentData);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                DialogResult dr = MessageBox.Show("仮確定中の変更はすべて取り消されます。よろしいですか？", "取消", MessageBoxButtons.OKCancel);
                this.Enabled = true;
                if (dr == DialogResult.OK)
                {
                    tableSet.Tables["全体"].RejectChanges();
                    this.mDelArray.Clear();
                    ast.InTransaction = false;
                    this.RefreshButtonEnable();
                    this.AddButton.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }

        private void InputOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                OrderCondition oc = new OrderCondition(this);
                oc.Show();
                oc.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }
	}
}
