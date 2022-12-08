namespace ImageImportNormal
{
	partial class SearchListForm
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
            this.clInputRoute = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBatch_Folder_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clOc_Bk_No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clOc_Br_No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clScan_Br_No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clScan_Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clClearing_Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clScan_Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cltotal_count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTotal_Amount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clImage_Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clStatusText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clLock_Sts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblScandate = new System.Windows.Forms.Label();
            this.txtScandate = new CommonClass.DTextBox2();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.txtScandate);
            this.contentsPanel.Controls.Add(this.lblScandate);
            this.contentsPanel.Controls.Add(this.lvBatList);
            this.contentsPanel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.contentsPanel.Size = new System.Drawing.Size(1266, 804);
            this.contentsPanel.Controls.SetChildIndex(this.lvBatList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblScandate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtScandate, 0);
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
            this.clInputRoute,
            this.clBatch_Folder_Name,
            this.clOc_Bk_No,
            this.clOc_Br_No,
            this.clScan_Br_No,
            this.clScan_Date,
            this.clClearing_Date,
            this.clScan_Count,
            this.cltotal_count,
            this.clTotal_Amount,
            this.clImage_Count,
            this.clStatus,
            this.clStatusText,
            this.clLock_Sts});
            this.lvBatList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvBatList.FullRowSelect = true;
            this.lvBatList.GridLines = true;
            this.lvBatList.HideSelection = false;
            this.lvBatList.Location = new System.Drawing.Point(10, 34);
            this.lvBatList.MultiSelect = false;
            this.lvBatList.Name = "lvBatList";
            this.lvBatList.Size = new System.Drawing.Size(1249, 768);
            this.lvBatList.TabIndex = 2;
            this.lvBatList.TabStop = false;
            this.lvBatList.UseCompatibleStateImageBehavior = false;
            this.lvBatList.View = System.Windows.Forms.View.Details;
            this.lvBatList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lv_ColumnWidthChanging);
            this.lvBatList.DoubleClick += new System.EventHandler(this.lv_DoubleClick);
            this.lvBatList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lv_KeyDown);
            // 
            // clInputRoute
            // 
            this.clInputRoute.Width = 0;
            // 
            // clBatch_Folder_Name
            // 
            this.clBatch_Folder_Name.Text = "バッチフォルダ名";
            this.clBatch_Folder_Name.Width = 177;
            // 
            // clOc_Bk_No
            // 
            this.clOc_Bk_No.Text = "持出銀行";
            this.clOc_Bk_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clOc_Bk_No.Width = 80;
            // 
            // clOc_Br_No
            // 
            this.clOc_Br_No.Text = "持出支店";
            this.clOc_Br_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clOc_Br_No.Width = 80;
            // 
            // clScan_Br_No
            // 
            this.clScan_Br_No.Text = "スキャン支店";
            this.clScan_Br_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clScan_Br_No.Width = 95;
            // 
            // clScan_Date
            // 
            this.clScan_Date.Text = "スキャン日";
            this.clScan_Date.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clScan_Date.Width = 85;
            // 
            // clClearing_Date
            // 
            this.clClearing_Date.Text = "交換希望日";
            this.clClearing_Date.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clClearing_Date.Width = 95;
            // 
            // clScan_Count
            // 
            this.clScan_Count.Text = "スキャン枚数";
            this.clScan_Count.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clScan_Count.Width = 95;
            // 
            // cltotal_count
            // 
            this.cltotal_count.Text = "合計枚数";
            this.cltotal_count.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cltotal_count.Width = 85;
            // 
            // clTotal_Amount
            // 
            this.clTotal_Amount.Text = "合計金額";
            this.clTotal_Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clTotal_Amount.Width = 175;
            // 
            // clImage_Count
            // 
            this.clImage_Count.Text = "イメージ数";
            this.clImage_Count.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clImage_Count.Width = 85;
            // 
            // clStatus
            // 
            this.clStatus.Text = "状態コード";
            this.clStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clStatus.Width = 0;
            // 
            // clStatusText
            // 
            this.clStatusText.Text = "状態";
            this.clStatusText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clStatusText.Width = 75;
            // 
            // clLock_Sts
            // 
            this.clLock_Sts.Text = "ロック状態";
            this.clLock_Sts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clLock_Sts.Width = 100;
            // 
            // lblScandate
            // 
            this.lblScandate.AutoSize = true;
            this.lblScandate.Font = new System.Drawing.Font("MS UI Gothic", 14.25F);
            this.lblScandate.Location = new System.Drawing.Point(10, 4);
            this.lblScandate.Name = "lblScandate";
            this.lblScandate.Size = new System.Drawing.Size(86, 19);
            this.lblScandate.TabIndex = 6;
            this.lblScandate.Text = "スキャン日";
            // 
            // txtScandate
            // 
            this.txtScandate.Font = new System.Drawing.Font("MS UI Gothic", 14.25F);
            this.txtScandate.Location = new System.Drawing.Point(99, 1);
            this.txtScandate.MaxLength = 8;
            this.txtScandate.Name = "txtScandate";
            this.txtScandate.OnEnterSeparatorCut = true;
            this.txtScandate.Size = new System.Drawing.Size(101, 26);
            this.txtScandate.TabIndex = 1;
            this.txtScandate.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.txtScandate.Enter += new System.EventHandler(this.txt_Enter);
            this.txtScandate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScandate_KeyDown);
            this.txtScandate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // SearchListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SearchListForm";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvBatList;
		private System.Windows.Forms.ColumnHeader clBatch_Folder_Name;
		private System.Windows.Forms.ColumnHeader clScan_Br_No;
        private System.Windows.Forms.ColumnHeader clOc_Bk_No;
        private System.Windows.Forms.ColumnHeader clOc_Br_No;
        private System.Windows.Forms.Label lblScandate;
        private System.Windows.Forms.ColumnHeader clScan_Date;
        private System.Windows.Forms.ColumnHeader clClearing_Date;
        private System.Windows.Forms.ColumnHeader clScan_Count;
        private System.Windows.Forms.ColumnHeader clTotal_Amount;
        private System.Windows.Forms.ColumnHeader clImage_Count;
        private System.Windows.Forms.ColumnHeader clStatus;
        private System.Windows.Forms.ColumnHeader clLock_Sts;
        private System.Windows.Forms.ColumnHeader clInputRoute;
        private System.Windows.Forms.ColumnHeader cltotal_count;
        private CommonClass.DTextBox2 txtScandate;
        private System.Windows.Forms.ColumnHeader clStatusText;
    }
}
