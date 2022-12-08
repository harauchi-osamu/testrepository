using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace NCR.GeneralMainte
{
	/// <summary>
	/// refForm の概要の説明です。
	/// </summary>
	public class refForm : System.Windows.Forms.Form
	{

		private appSetting ast;
		private System.Windows.Forms.DataGrid dataGrid;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public refForm(appSetting appSet)
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			ast = appSet;
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.dataGrid = new System.Windows.Forms.DataGrid();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid
			// 
			this.dataGrid.DataMember = "";
			this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid.Location = new System.Drawing.Point(0, 0);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.PreferredColumnWidth = 280;
			this.dataGrid.ReadOnly = true;
			this.dataGrid.Size = new System.Drawing.Size(320, 438);
			this.dataGrid.TabIndex = 0;
			this.dataGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGrid_MouseDown);
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(56, 448);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(72, 24);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(192, 448);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(72, 24);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "キャンセル";
			// 
			// refForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(320, 486);
			this.ControlBox = false;
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.dataGrid);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.KeyPreview = true;
			this.Name = "refForm";
			this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 48);
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "refForm";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.refForm_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.refForm_KeyUp);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void refForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (ast.AfterMenu)
			{
				ast.AfterMenu = false;
				return;
			}

			if (e.KeyCode == Keys.Enter)
			{
				okButton.PerformClick();
			}
		}

		private void refForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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

		/// <summary>
		/// dataGridのCaptionText
		/// </summary>
		public string CaptionText
		{
			get
			{
				return dataGrid.CaptionText;
			}
			set
			{
				dataGrid.CaptionText = value;
			}
		}

		/// <summary>
		/// dataGridのDataSource
		/// </summary>
		public DataView dataView
		{
			get
			{
				return (DataView)dataGrid.DataSource;
			}
			set
			{
				dataGrid.DataSource = value;
			}
		}

		/// <summary>
		/// 選択されたデータ
		/// </summary>
		public string SelectedText
		{
			get
			{
				string str = null;
				if (dataGrid[dataGrid.CurrentRowIndex, 0] != DBNull.Value)
				{
					str = (string)dataGrid[dataGrid.CurrentRowIndex, 0];
				}
				return str;
			}
		}
	}
}
