namespace CTROcBatchView
{
	partial class SearchBatchListForm
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
            this.clINPUT_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBRANCH_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clSCAN_BR_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clSCAN_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCLEARING_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBR_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBR_COUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBR_AMOUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clMEI_COUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clMEI_AMOUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDIFFER_COUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDIFFER_AMOUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clSTATUS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clNOTE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtImportDate = new CommonClass.DTextBox2();
            this.label6 = new System.Windows.Forms.Label();
            this.txtScanBrCd = new CommonClass.NumTextBox2();
            this.label7 = new System.Windows.Forms.Label();
            this.txtScanDate = new CommonClass.DTextBox2();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOcBrCd = new CommonClass.NumTextBox2();
            this.label9 = new System.Windows.Forms.Label();
            this.txtClearlingDate = new CommonClass.DTextBox2();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBatCnt = new CommonClass.NumTextBox2();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBatKingaku = new CommonClass.NumTextBox2();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbSaMaisu = new System.Windows.Forms.ComboBox();
            this.cmbSaKingaku = new System.Windows.Forms.ComboBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.cmbStatus);
            this.contentsPanel.Controls.Add(this.cmbSaKingaku);
            this.contentsPanel.Controls.Add(this.cmbSaMaisu);
            this.contentsPanel.Controls.Add(this.txtOcBrCd);
            this.contentsPanel.Controls.Add(this.txtBatKingaku);
            this.contentsPanel.Controls.Add(this.txtBatCnt);
            this.contentsPanel.Controls.Add(this.txtScanBrCd);
            this.contentsPanel.Controls.Add(this.txtClearlingDate);
            this.contentsPanel.Controls.Add(this.txtScanDate);
            this.contentsPanel.Controls.Add(this.txtImportDate);
            this.contentsPanel.Controls.Add(this.label8);
            this.contentsPanel.Controls.Add(this.label14);
            this.contentsPanel.Controls.Add(this.label11);
            this.contentsPanel.Controls.Add(this.label12);
            this.contentsPanel.Controls.Add(this.label13);
            this.contentsPanel.Controls.Add(this.label10);
            this.contentsPanel.Controls.Add(this.label6);
            this.contentsPanel.Controls.Add(this.label9);
            this.contentsPanel.Controls.Add(this.label7);
            this.contentsPanel.Controls.Add(this.label5);
            this.contentsPanel.Controls.Add(this.label3);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.Add(this.lvBatList);
            this.contentsPanel.Controls.SetChildIndex(this.lvBatList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label3, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label5, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label7, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label9, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label6, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label10, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label13, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label12, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label11, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label14, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label8, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtImportDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtScanDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtClearlingDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtScanBrCd, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtBatCnt, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtBatKingaku, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtOcBrCd, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbSaMaisu, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbSaKingaku, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbStatus, 0);
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
            this.clINPUT_DATE,
            this.clBRANCH_NO,
            this.clSCAN_BR_NO,
            this.clSCAN_DATE,
            this.clCLEARING_DATE,
            this.clBR_NO,
            this.clBR_COUNT,
            this.clBR_AMOUNT,
            this.clMEI_COUNT,
            this.clMEI_AMOUNT,
            this.clDIFFER_COUNT,
            this.clDIFFER_AMOUNT,
            this.clSTATUS,
            this.clNOTE});
            this.lvBatList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvBatList.FullRowSelect = true;
            this.lvBatList.GridLines = true;
            this.lvBatList.HideSelection = false;
            this.lvBatList.Location = new System.Drawing.Point(12, 159);
            this.lvBatList.MultiSelect = false;
            this.lvBatList.Name = "lvBatList";
            this.lvBatList.Size = new System.Drawing.Size(1246, 670);
            this.lvBatList.TabIndex = 11;
            this.lvBatList.TabStop = false;
            this.lvBatList.UseCompatibleStateImageBehavior = false;
            this.lvBatList.View = System.Windows.Forms.View.Details;
            this.lvBatList.Enter += new System.EventHandler(this.List_Enter);
            this.lvBatList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.lvBatList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvBatList_MouseDoubleClick);
            // 
            // clINPUT_DATE
            // 
            this.clINPUT_DATE.Text = "取込日";
            this.clINPUT_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clINPUT_DATE.Width = 82;
            // 
            // clBRANCH_NO
            // 
            this.clBRANCH_NO.Text = "バッチ番号";
            this.clBRANCH_NO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clBRANCH_NO.Width = 80;
            // 
            // clSCAN_BR_NO
            // 
            this.clSCAN_BR_NO.Text = "スキャン支店";
            this.clSCAN_BR_NO.Width = 92;
            // 
            // clSCAN_DATE
            // 
            this.clSCAN_DATE.Text = "スキャン日";
            this.clSCAN_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clSCAN_DATE.Width = 82;
            // 
            // clCLEARING_DATE
            // 
            this.clCLEARING_DATE.Text = "交換希望日";
            this.clCLEARING_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clCLEARING_DATE.Width = 92;
            // 
            // clBR_NO
            // 
            this.clBR_NO.Text = "持出支店";
            this.clBR_NO.Width = 92;
            // 
            // clBR_COUNT
            // 
            this.clBR_COUNT.Text = "バッチ枚数";
            this.clBR_COUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clBR_COUNT.Width = 80;
            // 
            // clBR_AMOUNT
            // 
            this.clBR_AMOUNT.Text = "バッチ金額";
            this.clBR_AMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clBR_AMOUNT.Width = 115;
            // 
            // clMEI_COUNT
            // 
            this.clMEI_COUNT.Text = "明細枚数";
            this.clMEI_COUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clMEI_COUNT.Width = 80;
            // 
            // clMEI_AMOUNT
            // 
            this.clMEI_AMOUNT.Text = "明細金額";
            this.clMEI_AMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clMEI_AMOUNT.Width = 115;
            // 
            // clDIFFER_COUNT
            // 
            this.clDIFFER_COUNT.Text = "差枚数";
            this.clDIFFER_COUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clDIFFER_COUNT.Width = 80;
            // 
            // clDIFFER_AMOUNT
            // 
            this.clDIFFER_AMOUNT.Text = "差金額";
            this.clDIFFER_AMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clDIFFER_AMOUNT.Width = 115;
            // 
            // clSTATUS
            // 
            this.clSTATUS.Text = "状態";
            this.clSTATUS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clSTATUS.Width = 76;
            // 
            // clNOTE
            // 
            this.clNOTE.Text = "照合";
            this.clNOTE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clNOTE.Width = 44;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 100000;
            this.label1.Text = "検索条件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(10, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 100000;
            this.label3.Text = "検索結果";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(9, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 100001;
            this.label5.Text = "取込日";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtImportDate
            // 
            this.txtImportDate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtImportDate.KeyControl = true;
            this.txtImportDate.Location = new System.Drawing.Point(118, 28);
            this.txtImportDate.MaxLength = 8;
            this.txtImportDate.Name = "txtImportDate";
            this.txtImportDate.OnEnterSeparatorCut = true;
            this.txtImportDate.Size = new System.Drawing.Size(110, 23);
            this.txtImportDate.TabIndex = 1;
            this.txtImportDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtImportDate.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            this.txtImportDate.Enter += new System.EventHandler(this.txt_Enter);
            this.txtImportDate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(248, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 100001;
            this.label6.Text = "スキャン支店";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtScanBrCd
            // 
            this.txtScanBrCd.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtScanBrCd.KeyControl = true;
            this.txtScanBrCd.Location = new System.Drawing.Point(357, 28);
            this.txtScanBrCd.MaxLength = 4;
            this.txtScanBrCd.Name = "txtScanBrCd";
            this.txtScanBrCd.Size = new System.Drawing.Size(110, 23);
            this.txtScanBrCd.TabIndex = 2;
            this.txtScanBrCd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtScanBrCd.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            this.txtScanBrCd.Enter += new System.EventHandler(this.txt_Enter);
            this.txtScanBrCd.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(489, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 100001;
            this.label7.Text = "スキャン日";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtScanDate
            // 
            this.txtScanDate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtScanDate.KeyControl = true;
            this.txtScanDate.Location = new System.Drawing.Point(598, 28);
            this.txtScanDate.MaxLength = 8;
            this.txtScanDate.Name = "txtScanDate";
            this.txtScanDate.OnEnterSeparatorCut = true;
            this.txtScanDate.Size = new System.Drawing.Size(110, 23);
            this.txtScanDate.TabIndex = 3;
            this.txtScanDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtScanDate.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            this.txtScanDate.Enter += new System.EventHandler(this.txt_Enter);
            this.txtScanDate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(9, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 16);
            this.label8.TabIndex = 100001;
            this.label8.Text = "持出支店コード";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOcBrCd
            // 
            this.txtOcBrCd.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtOcBrCd.KeyControl = true;
            this.txtOcBrCd.Location = new System.Drawing.Point(118, 52);
            this.txtOcBrCd.MaxLength = 4;
            this.txtOcBrCd.Name = "txtOcBrCd";
            this.txtOcBrCd.Size = new System.Drawing.Size(110, 23);
            this.txtOcBrCd.TabIndex = 4;
            this.txtOcBrCd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOcBrCd.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            this.txtOcBrCd.Enter += new System.EventHandler(this.txt_Enter);
            this.txtOcBrCd.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(9, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 16);
            this.label9.TabIndex = 100001;
            this.label9.Text = "交換希望日";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtClearlingDate
            // 
            this.txtClearlingDate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtClearlingDate.KeyControl = true;
            this.txtClearlingDate.Location = new System.Drawing.Point(118, 76);
            this.txtClearlingDate.MaxLength = 8;
            this.txtClearlingDate.Name = "txtClearlingDate";
            this.txtClearlingDate.OnEnterSeparatorCut = true;
            this.txtClearlingDate.Size = new System.Drawing.Size(110, 23);
            this.txtClearlingDate.TabIndex = 5;
            this.txtClearlingDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtClearlingDate.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            this.txtClearlingDate.Enter += new System.EventHandler(this.txt_Enter);
            this.txtClearlingDate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(248, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 16);
            this.label10.TabIndex = 100001;
            this.label10.Text = "バッチ枚数";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBatCnt
            // 
            this.txtBatCnt.Comma = true;
            this.txtBatCnt.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBatCnt.KeyControl = true;
            this.txtBatCnt.Location = new System.Drawing.Point(357, 76);
            this.txtBatCnt.MaxLength = 6;
            this.txtBatCnt.Name = "txtBatCnt";
            this.txtBatCnt.OnEnterSeparatorCut = true;
            this.txtBatCnt.Size = new System.Drawing.Size(110, 23);
            this.txtBatCnt.TabIndex = 6;
            this.txtBatCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBatCnt.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            this.txtBatCnt.Enter += new System.EventHandler(this.txt_Enter);
            this.txtBatCnt.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(489, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 16);
            this.label11.TabIndex = 100001;
            this.label11.Text = "バッチ金額";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBatKingaku
            // 
            this.txtBatKingaku.Comma = true;
            this.txtBatKingaku.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBatKingaku.KeyControl = true;
            this.txtBatKingaku.Location = new System.Drawing.Point(598, 76);
            this.txtBatKingaku.MaxLength = 15;
            this.txtBatKingaku.Name = "txtBatKingaku";
            this.txtBatKingaku.OnEnterSeparatorCut = true;
            this.txtBatKingaku.Size = new System.Drawing.Size(168, 23);
            this.txtBatKingaku.TabIndex = 7;
            this.txtBatKingaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBatKingaku.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            this.txtBatKingaku.Enter += new System.EventHandler(this.txt_Enter);
            this.txtBatKingaku.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.Location = new System.Drawing.Point(9, 105);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 16);
            this.label12.TabIndex = 100001;
            this.label12.Text = "差枚数";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.Location = new System.Drawing.Point(248, 105);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 16);
            this.label13.TabIndex = 100001;
            this.label13.Text = "差金額";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.Location = new System.Drawing.Point(489, 105);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 16);
            this.label14.TabIndex = 100001;
            this.label14.Text = "状態";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbSaMaisu
            // 
            this.cmbSaMaisu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSaMaisu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSaMaisu.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.cmbSaMaisu.FormattingEnabled = true;
            this.cmbSaMaisu.Location = new System.Drawing.Point(118, 100);
            this.cmbSaMaisu.Name = "cmbSaMaisu";
            this.cmbSaMaisu.Size = new System.Drawing.Size(110, 24);
            this.cmbSaMaisu.TabIndex = 8;
            this.cmbSaMaisu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbSaMaisu.Enter += new System.EventHandler(this.combo_Enter);
            this.cmbSaMaisu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // cmbSaKingaku
            // 
            this.cmbSaKingaku.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSaKingaku.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSaKingaku.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.cmbSaKingaku.FormattingEnabled = true;
            this.cmbSaKingaku.Location = new System.Drawing.Point(357, 100);
            this.cmbSaKingaku.Name = "cmbSaKingaku";
            this.cmbSaKingaku.Size = new System.Drawing.Size(110, 24);
            this.cmbSaKingaku.TabIndex = 9;
            this.cmbSaKingaku.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbSaKingaku.Enter += new System.EventHandler(this.combo_Enter);
            this.cmbSaKingaku.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(598, 100);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(110, 24);
            this.cmbStatus.TabIndex = 10;
            this.cmbStatus.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbStatus.Enter += new System.EventHandler(this.combo_Enter);
            this.cmbStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // SearchBatchListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SearchBatchListForm";
            this.Load += new System.EventHandler(this.SearchBatchListForm_Load);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvBatList;
		private System.Windows.Forms.ColumnHeader clINPUT_DATE;
		private System.Windows.Forms.ColumnHeader clSCAN_DATE;
        private System.Windows.Forms.ColumnHeader clBRANCH_NO;
        private System.Windows.Forms.ColumnHeader clSCAN_BR_NO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private CommonClass.DTextBox2 txtImportDate;
        private System.Windows.Forms.Label label5;
        private CommonClass.NumTextBox2 txtScanBrCd;
        private CommonClass.DTextBox2 txtScanDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private CommonClass.NumTextBox2 txtOcBrCd;
        private System.Windows.Forms.Label label8;
        private CommonClass.DTextBox2 txtClearlingDate;
        private System.Windows.Forms.Label label9;
        private CommonClass.NumTextBox2 txtBatCnt;
        private System.Windows.Forms.Label label10;
        private CommonClass.NumTextBox2 txtBatKingaku;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ColumnHeader clCLEARING_DATE;
        private System.Windows.Forms.ColumnHeader clBR_NO;
        private System.Windows.Forms.ColumnHeader clBR_COUNT;
        private System.Windows.Forms.ColumnHeader clBR_AMOUNT;
        private System.Windows.Forms.ColumnHeader clMEI_COUNT;
        private System.Windows.Forms.ColumnHeader clMEI_AMOUNT;
        private System.Windows.Forms.ColumnHeader clDIFFER_COUNT;
        private System.Windows.Forms.ColumnHeader clDIFFER_AMOUNT;
        private System.Windows.Forms.ColumnHeader clSTATUS;
        private System.Windows.Forms.ColumnHeader clNOTE;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbSaKingaku;
        private System.Windows.Forms.ComboBox cmbSaMaisu;
    }
}
