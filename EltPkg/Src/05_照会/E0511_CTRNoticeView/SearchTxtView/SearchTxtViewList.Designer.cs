namespace SearchTxtView
{
	partial class SearchTxtViewList
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.ColumnHeader clKey;
            this.lvResultList = new System.Windows.Forms.ListView();
            this.clFILE_DIVID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clFILE_NAME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clRECORD_COUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clMAKE_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clMAKE_TIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dtRdate = new CommonClass.DTextBox2();
            this.lblRdate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbFILE_DIVID = new System.Windows.Forms.ComboBox();
            this.lblFILE_DIVID = new System.Windows.Forms.Label();
            this.headerControl = new SearchTxtView.HeaderControl();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.headerControl);
            this.contentsPanel.Controls.Add(this.cmbFILE_DIVID);
            this.contentsPanel.Controls.Add(this.lblFILE_DIVID);
            this.contentsPanel.Controls.Add(this.dtRdate);
            this.contentsPanel.Controls.Add(this.lblRdate);
            this.contentsPanel.Controls.Add(this.label5);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.Add(this.lvResultList);
            this.contentsPanel.Controls.SetChildIndex(this.lvResultList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label5, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblRdate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtRdate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblFILE_DIVID, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbFILE_DIVID, 0);
            this.contentsPanel.Controls.SetChildIndex(this.headerControl, 0);
            // 
            // clKey
            // 
            clKey.Text = "clKey";
            clKey.Width = 0;
            // 
            // lvResultList
            // 
            this.lvResultList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.clFILE_DIVID,
            this.clFILE_NAME,
            this.clRECORD_COUNT,
            this.clMAKE_DATE,
            this.clMAKE_TIME});
            this.lvResultList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvResultList.FullRowSelect = true;
            this.lvResultList.GridLines = true;
            this.lvResultList.HideSelection = false;
            this.lvResultList.Location = new System.Drawing.Point(36, 112);
            this.lvResultList.MultiSelect = false;
            this.lvResultList.Name = "lvResultList";
            this.lvResultList.Size = new System.Drawing.Size(997, 689);
            this.lvResultList.TabIndex = 5;
            this.lvResultList.TabStop = false;
            this.lvResultList.UseCompatibleStateImageBehavior = false;
            this.lvResultList.View = System.Windows.Forms.View.Details;
            this.lvResultList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lv_ColumnWidthChanging);
            this.lvResultList.DoubleClick += new System.EventHandler(this.lv_DoubleClick);
            this.lvResultList.Enter += new System.EventHandler(this.List_Enter);
            this.lvResultList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lv_KeyDown);
            // 
            // clFILE_DIVID
            // 
            this.clFILE_DIVID.Text = "ファイル識別区分";
            this.clFILE_DIVID.Width = 400;
            // 
            // clFILE_NAME
            // 
            this.clFILE_NAME.Text = "ファイル名";
            this.clFILE_NAME.Width = 280;
            // 
            // clRECORD_COUNT
            // 
            this.clRECORD_COUNT.Text = "件数";
            this.clRECORD_COUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clRECORD_COUNT.Width = 100;
            // 
            // clMAKE_DATE
            // 
            this.clMAKE_DATE.Text = "取込日";
            this.clMAKE_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clMAKE_DATE.Width = 105;
            // 
            // clMAKE_TIME
            // 
            this.clMAKE_TIME.Text = "取込時刻";
            this.clMAKE_TIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clMAKE_TIME.Width = 90;
            // 
            // dtRdate
            // 
            this.dtRdate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtRdate.KeyControl = true;
            this.dtRdate.Location = new System.Drawing.Point(655, 41);
            this.dtRdate.MaxLength = 8;
            this.dtRdate.Name = "dtRdate";
            this.dtRdate.OnEnterSeparatorCut = true;
            this.dtRdate.Size = new System.Drawing.Size(145, 23);
            this.dtRdate.TabIndex = 2;
            this.dtRdate.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtRdate.Enter += new System.EventHandler(this.txt_Enter);
            this.dtRdate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblRdate
            // 
            this.lblRdate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblRdate.Location = new System.Drawing.Point(579, 41);
            this.lblRdate.Name = "lblRdate";
            this.lblRdate.Size = new System.Drawing.Size(70, 23);
            this.lblRdate.TabIndex = 100015;
            this.lblRdate.Text = "取込日";
            this.lblRdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 16);
            this.label1.TabIndex = 100014;
            this.label1.Text = "絞り込み条件";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(15, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 16);
            this.label5.TabIndex = 100014;
            this.label5.Text = "通知一覧";
            // 
            // cmbFILE_DIVID
            // 
            this.cmbFILE_DIVID.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFILE_DIVID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFILE_DIVID.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbFILE_DIVID.FormattingEnabled = true;
            this.cmbFILE_DIVID.Location = new System.Drawing.Point(156, 41);
            this.cmbFILE_DIVID.Name = "cmbFILE_DIVID";
            this.cmbFILE_DIVID.Size = new System.Drawing.Size(407, 24);
            this.cmbFILE_DIVID.TabIndex = 1;
            this.cmbFILE_DIVID.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbFILE_DIVID.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbFILE_DIVID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // lblFILE_DIVID
            // 
            this.lblFILE_DIVID.AutoSize = true;
            this.lblFILE_DIVID.Cursor = System.Windows.Forms.Cursors.No;
            this.lblFILE_DIVID.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblFILE_DIVID.Location = new System.Drawing.Point(33, 45);
            this.lblFILE_DIVID.Name = "lblFILE_DIVID";
            this.lblFILE_DIVID.Size = new System.Drawing.Size(117, 16);
            this.lblFILE_DIVID.TabIndex = 100017;
            this.lblFILE_DIVID.Text = "ファイル識別区分";
            this.lblFILE_DIVID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // headerControl
            // 
            this.headerControl.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.headerControl.Location = new System.Drawing.Point(1089, 0);
            this.headerControl.Margin = new System.Windows.Forms.Padding(5);
            this.headerControl.Name = "headerControl";
            this.headerControl.Size = new System.Drawing.Size(177, 43);
            this.headerControl.TabIndex = 100018;
            this.headerControl.TabStop = false;
            // 
            // SearchTxtViewList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SearchTxtViewList";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvResultList;
		private System.Windows.Forms.ColumnHeader clFILE_DIVID;
		private System.Windows.Forms.ColumnHeader clMAKE_DATE;
        private System.Windows.Forms.ColumnHeader clFILE_NAME;
        private System.Windows.Forms.ColumnHeader clRECORD_COUNT;
        private CommonClass.DTextBox2 dtRdate;
        private System.Windows.Forms.Label lblRdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader clMAKE_TIME;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbFILE_DIVID;
        private System.Windows.Forms.Label lblFILE_DIVID;
        private HeaderControl headerControl;
    }
}
