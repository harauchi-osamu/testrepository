namespace CTROcBrTotalView
{
	partial class SearchBranchGoukeiForm
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
            this.lvBatList = new System.Windows.Forms.ListView();
            this.clScan_Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBR_NAME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTOTAL_COUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTOTAL_AMOUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clMEI_TOTAL_COUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clMEI_TOTAL_AMOUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDIFFER_COUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDIFFER_AMOUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtScanDate = new CommonClass.DTextBox2();
            this.lblDIFFER_COUNT = new System.Windows.Forms.Label();
            this.lbldtSCAN_DATE = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOcBrCd = new CommonClass.NumTextBox2();
            this.lblBR_NO = new System.Windows.Forms.Label();
            this.lblDIFFER_AMOUNT = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbSaKingaku = new System.Windows.Forms.ComboBox();
            this.cmbSaMaisu = new System.Windows.Forms.ComboBox();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.cmbTotalUmu = new System.Windows.Forms.ComboBox();
            this.lblTotalumu = new System.Windows.Forms.Label();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.cmbTotalUmu);
            this.contentsPanel.Controls.Add(this.lblTotalumu);
            this.contentsPanel.Controls.Add(this.pnlImage);
            this.contentsPanel.Controls.Add(this.cmbSaKingaku);
            this.contentsPanel.Controls.Add(this.cmbSaMaisu);
            this.contentsPanel.Controls.Add(this.txtOcBrCd);
            this.contentsPanel.Controls.Add(this.lblBR_NO);
            this.contentsPanel.Controls.Add(this.lblDIFFER_AMOUNT);
            this.contentsPanel.Controls.Add(this.label6);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.Add(this.txtScanDate);
            this.contentsPanel.Controls.Add(this.lblDIFFER_COUNT);
            this.contentsPanel.Controls.Add(this.lbldtSCAN_DATE);
            this.contentsPanel.Controls.Add(this.lvBatList);
            this.contentsPanel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.contentsPanel.Controls.SetChildIndex(this.lvBatList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lbldtSCAN_DATE, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblDIFFER_COUNT, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtScanDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label6, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblDIFFER_AMOUNT, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblBR_NO, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtOcBrCd, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbSaMaisu, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbSaKingaku, 0);
            this.contentsPanel.Controls.SetChildIndex(this.pnlImage, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblTotalumu, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbTotalUmu, 0);
            // 
            // clKey
            // 
            clKey.Text = "clKey";
            clKey.Width = 0;
            // 
            // lvBatList
            // 
            this.lvBatList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.clScan_Date,
            this.clBR_NAME,
            this.clTOTAL_COUNT,
            this.clTOTAL_AMOUNT,
            this.clMEI_TOTAL_COUNT,
            this.clMEI_TOTAL_AMOUNT,
            this.clDIFFER_COUNT,
            this.clDIFFER_AMOUNT});
            this.lvBatList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvBatList.FullRowSelect = true;
            this.lvBatList.GridLines = true;
            this.lvBatList.HideSelection = false;
            this.lvBatList.Location = new System.Drawing.Point(25, 143);
            this.lvBatList.MultiSelect = false;
            this.lvBatList.Name = "lvBatList";
            this.lvBatList.Size = new System.Drawing.Size(1217, 247);
            this.lvBatList.TabIndex = 5;
            this.lvBatList.TabStop = false;
            this.lvBatList.UseCompatibleStateImageBehavior = false;
            this.lvBatList.View = System.Windows.Forms.View.Details;
            this.lvBatList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvBatList_ColumnWidthChanging);
            this.lvBatList.SelectedIndexChanged += new System.EventHandler(this.lvBatList_SelectedIndexChanged);
            this.lvBatList.Enter += new System.EventHandler(this.List_Enter);
            this.lvBatList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.lvBatList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvBatList_MouseDoubleClick);
            // 
            // clScan_Date
            // 
            this.clScan_Date.Text = "スキャン日";
            this.clScan_Date.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clScan_Date.Width = 105;
            // 
            // clBR_NAME
            // 
            this.clBR_NAME.Text = "持出支店名";
            this.clBR_NAME.Width = 200;
            // 
            // clTOTAL_COUNT
            // 
            this.clTOTAL_COUNT.Text = "合計票枚数";
            this.clTOTAL_COUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clTOTAL_COUNT.Width = 105;
            // 
            // clTOTAL_AMOUNT
            // 
            this.clTOTAL_AMOUNT.Text = "合計票金額";
            this.clTOTAL_AMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clTOTAL_AMOUNT.Width = 190;
            // 
            // clMEI_TOTAL_COUNT
            // 
            this.clMEI_TOTAL_COUNT.Text = "明細総枚数";
            this.clMEI_TOTAL_COUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clMEI_TOTAL_COUNT.Width = 105;
            // 
            // clMEI_TOTAL_AMOUNT
            // 
            this.clMEI_TOTAL_AMOUNT.Text = "明細総金額";
            this.clMEI_TOTAL_AMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clMEI_TOTAL_AMOUNT.Width = 190;
            // 
            // clDIFFER_COUNT
            // 
            this.clDIFFER_COUNT.Text = "差引枚数";
            this.clDIFFER_COUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clDIFFER_COUNT.Width = 105;
            // 
            // clDIFFER_AMOUNT
            // 
            this.clDIFFER_AMOUNT.Text = "差引金額";
            this.clDIFFER_AMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clDIFFER_AMOUNT.Width = 190;
            // 
            // txtScanDate
            // 
            this.txtScanDate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtScanDate.KeyControl = true;
            this.txtScanDate.Location = new System.Drawing.Point(170, 38);
            this.txtScanDate.MaxLength = 8;
            this.txtScanDate.Name = "txtScanDate";
            this.txtScanDate.OnEnterSeparatorCut = true;
            this.txtScanDate.Size = new System.Drawing.Size(100, 23);
            this.txtScanDate.TabIndex = 1;
            this.txtScanDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtScanDate.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            this.txtScanDate.Enter += new System.EventHandler(this.txt_Enter);
            this.txtScanDate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblDIFFER_COUNT
            // 
            this.lblDIFFER_COUNT.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDIFFER_COUNT.Location = new System.Drawing.Point(283, 71);
            this.lblDIFFER_COUNT.Name = "lblDIFFER_COUNT";
            this.lblDIFFER_COUNT.Size = new System.Drawing.Size(95, 23);
            this.lblDIFFER_COUNT.TabIndex = 100004;
            this.lblDIFFER_COUNT.Text = "差枚数";
            this.lblDIFFER_COUNT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbldtSCAN_DATE
            // 
            this.lbldtSCAN_DATE.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbldtSCAN_DATE.Location = new System.Drawing.Point(27, 38);
            this.lbldtSCAN_DATE.Name = "lbldtSCAN_DATE";
            this.lbldtSCAN_DATE.Size = new System.Drawing.Size(137, 23);
            this.lbldtSCAN_DATE.TabIndex = 100005;
            this.lbldtSCAN_DATE.Text = "スキャン日";
            this.lbldtSCAN_DATE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(27, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 100006;
            this.label1.Text = "検索条件";
            // 
            // txtOcBrCd
            // 
            this.txtOcBrCd.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtOcBrCd.KeyControl = true;
            this.txtOcBrCd.Location = new System.Drawing.Point(410, 38);
            this.txtOcBrCd.MaxLength = 4;
            this.txtOcBrCd.Name = "txtOcBrCd";
            this.txtOcBrCd.Size = new System.Drawing.Size(100, 23);
            this.txtOcBrCd.TabIndex = 2;
            this.txtOcBrCd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOcBrCd.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            this.txtOcBrCd.Enter += new System.EventHandler(this.txt_Enter);
            this.txtOcBrCd.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblBR_NO
            // 
            this.lblBR_NO.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBR_NO.Location = new System.Drawing.Point(283, 38);
            this.lblBR_NO.Name = "lblBR_NO";
            this.lblBR_NO.Size = new System.Drawing.Size(108, 23);
            this.lblBR_NO.TabIndex = 100009;
            this.lblBR_NO.Text = "持出支店コード";
            this.lblBR_NO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDIFFER_AMOUNT
            // 
            this.lblDIFFER_AMOUNT.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDIFFER_AMOUNT.Location = new System.Drawing.Point(539, 71);
            this.lblDIFFER_AMOUNT.Name = "lblDIFFER_AMOUNT";
            this.lblDIFFER_AMOUNT.Size = new System.Drawing.Size(56, 23);
            this.lblDIFFER_AMOUNT.TabIndex = 100010;
            this.lblDIFFER_AMOUNT.Text = "差金額";
            this.lblDIFFER_AMOUNT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(27, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 100006;
            this.label6.Text = "検索結果";
            // 
            // cmbSaKingaku
            // 
            this.cmbSaKingaku.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSaKingaku.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSaKingaku.FormattingEnabled = true;
            this.cmbSaKingaku.Location = new System.Drawing.Point(666, 71);
            this.cmbSaKingaku.Name = "cmbSaKingaku";
            this.cmbSaKingaku.Size = new System.Drawing.Size(100, 24);
            this.cmbSaKingaku.TabIndex = 5;
            this.cmbSaKingaku.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbSaKingaku.Enter += new System.EventHandler(this.combo_Enter);
            this.cmbSaKingaku.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // cmbSaMaisu
            // 
            this.cmbSaMaisu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSaMaisu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSaMaisu.FormattingEnabled = true;
            this.cmbSaMaisu.Location = new System.Drawing.Point(410, 71);
            this.cmbSaMaisu.Name = "cmbSaMaisu";
            this.cmbSaMaisu.Size = new System.Drawing.Size(100, 24);
            this.cmbSaMaisu.TabIndex = 4;
            this.cmbSaMaisu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbSaMaisu.Enter += new System.EventHandler(this.combo_Enter);
            this.cmbSaMaisu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // pnlImage
            // 
            this.pnlImage.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pnlImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlImage.Location = new System.Drawing.Point(25, 411);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(900, 410);
            this.pnlImage.TabIndex = 100034;
            // 
            // cmbTotalUmu
            // 
            this.cmbTotalUmu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTotalUmu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTotalUmu.FormattingEnabled = true;
            this.cmbTotalUmu.Location = new System.Drawing.Point(170, 71);
            this.cmbTotalUmu.Name = "cmbTotalUmu";
            this.cmbTotalUmu.Size = new System.Drawing.Size(100, 24);
            this.cmbTotalUmu.TabIndex = 3;
            this.cmbTotalUmu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbTotalUmu.Enter += new System.EventHandler(this.combo_Enter);
            this.cmbTotalUmu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // lblTotalumu
            // 
            this.lblTotalumu.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTotalumu.Location = new System.Drawing.Point(27, 71);
            this.lblTotalumu.Name = "lblTotalumu";
            this.lblTotalumu.Size = new System.Drawing.Size(137, 23);
            this.lblTotalumu.TabIndex = 100036;
            this.lblTotalumu.Text = "支店別合計票有無";
            this.lblTotalumu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchBranchGoukeiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SearchBranchGoukeiForm";
            this.Load += new System.EventHandler(this.SearchBranchGoukeiForm_Load);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvBatList;
		private System.Windows.Forms.ColumnHeader clScan_Date;
		private System.Windows.Forms.ColumnHeader clTOTAL_AMOUNT;
        private System.Windows.Forms.ColumnHeader clBR_NAME;
        private System.Windows.Forms.ColumnHeader clTOTAL_COUNT;
        private CommonClass.NumTextBox2 txtOcBrCd;
        private System.Windows.Forms.Label lblBR_NO;
        private System.Windows.Forms.Label lblDIFFER_AMOUNT;
        private System.Windows.Forms.Label label1;
        private CommonClass.DTextBox2 txtScanDate;
        private System.Windows.Forms.Label lblDIFFER_COUNT;
        private System.Windows.Forms.Label lbldtSCAN_DATE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColumnHeader clMEI_TOTAL_COUNT;
        private System.Windows.Forms.ColumnHeader clMEI_TOTAL_AMOUNT;
        private System.Windows.Forms.ColumnHeader clDIFFER_COUNT;
        private System.Windows.Forms.ColumnHeader clDIFFER_AMOUNT;
        private System.Windows.Forms.ComboBox cmbSaKingaku;
        private System.Windows.Forms.ComboBox cmbSaMaisu;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.ComboBox cmbTotalUmu;
        private System.Windows.Forms.Label lblTotalumu;
    }
}
