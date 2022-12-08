namespace SearchOutclearingBranch
{
	partial class SearchOutclearingBranchList
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
            this.lvListList = new System.Windows.Forms.ListView();
            this.clOPERATION_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clOC_BR_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCLEARING_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTOTAL_COUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTOTAL_AMOUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label7 = new System.Windows.Forms.Label();
            this.dtOC_BR_NO = new CommonClass.NumTextBox2();
            this.dtClearingDate = new CommonClass.DTextBox2();
            this.dtRdate = new CommonClass.DTextBox2();
            this.lblClearingDate = new System.Windows.Forms.Label();
            this.lblOC_BR_NO = new System.Windows.Forms.Label();
            this.lblRdate = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.label6);
            this.contentsPanel.Controls.Add(this.label7);
            this.contentsPanel.Controls.Add(this.dtOC_BR_NO);
            this.contentsPanel.Controls.Add(this.dtClearingDate);
            this.contentsPanel.Controls.Add(this.dtRdate);
            this.contentsPanel.Controls.Add(this.lblClearingDate);
            this.contentsPanel.Controls.Add(this.lblOC_BR_NO);
            this.contentsPanel.Controls.Add(this.lblRdate);
            this.contentsPanel.Controls.Add(this.lvListList);
            this.contentsPanel.Controls.SetChildIndex(this.lvListList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblRdate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblOC_BR_NO, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblClearingDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtRdate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtClearingDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtOC_BR_NO, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label7, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label6, 0);
            // 
            // clKey
            // 
            clKey.Text = "clKey";
            clKey.Width = 0;
            // 
            // lvListList
            // 
            this.lvListList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.clOPERATION_DATE,
            this.clOC_BR_NO,
            this.clCLEARING_DATE,
            this.clTOTAL_COUNT,
            this.clTOTAL_AMOUNT});
            this.lvListList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvListList.FullRowSelect = true;
            this.lvListList.GridLines = true;
            this.lvListList.HideSelection = false;
            this.lvListList.Location = new System.Drawing.Point(29, 109);
            this.lvListList.MultiSelect = false;
            this.lvListList.Name = "lvListList";
            this.lvListList.Size = new System.Drawing.Size(731, 689);
            this.lvListList.TabIndex = 5;
            this.lvListList.TabStop = false;
            this.lvListList.UseCompatibleStateImageBehavior = false;
            this.lvListList.View = System.Windows.Forms.View.Details;
            this.lvListList.Enter += new System.EventHandler(this.List_Enter);
            // 
            // clOPERATION_DATE
            // 
            this.clOPERATION_DATE.Text = "取込日";
            this.clOPERATION_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clOPERATION_DATE.Width = 115;
            // 
            // clOC_BR_NO
            // 
            this.clOC_BR_NO.Text = "持出支店コード";
            this.clOC_BR_NO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clOC_BR_NO.Width = 124;
            // 
            // clCLEARING_DATE
            // 
            this.clCLEARING_DATE.Text = "交換希望日";
            this.clCLEARING_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clCLEARING_DATE.Width = 143;
            // 
            // clTOTAL_COUNT
            // 
            this.clTOTAL_COUNT.Text = "合計枚数";
            this.clTOTAL_COUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clTOTAL_COUNT.Width = 112;
            // 
            // clTOTAL_AMOUNT
            // 
            this.clTOTAL_AMOUNT.Text = "合計金額";
            this.clTOTAL_AMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clTOTAL_AMOUNT.Width = 215;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(24, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 16);
            this.label7.TabIndex = 100008;
            this.label7.Text = "検索条件";
            // 
            // dtOC_BR_NO
            // 
            this.dtOC_BR_NO.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtOC_BR_NO.KeyControl = true;
            this.dtOC_BR_NO.Location = new System.Drawing.Point(528, 42);
            this.dtOC_BR_NO.MaxLength = 4;
            this.dtOC_BR_NO.Name = "dtOC_BR_NO";
            this.dtOC_BR_NO.OnEnterSeparatorCut = true;
            this.dtOC_BR_NO.Size = new System.Drawing.Size(100, 23);
            this.dtOC_BR_NO.TabIndex = 3;
            this.dtOC_BR_NO.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtOC_BR_NO.Enter += new System.EventHandler(this.txt_Enter);
            this.dtOC_BR_NO.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // dtClearingDate
            // 
            this.dtClearingDate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtClearingDate.KeyControl = true;
            this.dtClearingDate.Location = new System.Drawing.Point(298, 42);
            this.dtClearingDate.MaxLength = 8;
            this.dtClearingDate.Name = "dtClearingDate";
            this.dtClearingDate.OnEnterSeparatorCut = true;
            this.dtClearingDate.Size = new System.Drawing.Size(100, 23);
            this.dtClearingDate.TabIndex = 2;
            this.dtClearingDate.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtClearingDate.Enter += new System.EventHandler(this.txt_Enter);
            this.dtClearingDate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // dtRdate
            // 
            this.dtRdate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtRdate.KeyControl = true;
            this.dtRdate.Location = new System.Drawing.Point(86, 42);
            this.dtRdate.MaxLength = 8;
            this.dtRdate.Name = "dtRdate";
            this.dtRdate.OnEnterSeparatorCut = true;
            this.dtRdate.Size = new System.Drawing.Size(100, 23);
            this.dtRdate.TabIndex = 1;
            this.dtRdate.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtRdate.Enter += new System.EventHandler(this.txt_Enter);
            this.dtRdate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblClearingDate
            // 
            this.lblClearingDate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblClearingDate.Location = new System.Drawing.Point(204, 42);
            this.lblClearingDate.Name = "lblClearingDate";
            this.lblClearingDate.Size = new System.Drawing.Size(88, 23);
            this.lblClearingDate.TabIndex = 100005;
            this.lblClearingDate.Text = "交換希望日";
            this.lblClearingDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOC_BR_NO
            // 
            this.lblOC_BR_NO.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOC_BR_NO.Location = new System.Drawing.Point(414, 42);
            this.lblOC_BR_NO.Name = "lblOC_BR_NO";
            this.lblOC_BR_NO.Size = new System.Drawing.Size(108, 23);
            this.lblOC_BR_NO.TabIndex = 100006;
            this.lblOC_BR_NO.Text = "持出支店コード";
            this.lblOC_BR_NO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRdate
            // 
            this.lblRdate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblRdate.Location = new System.Drawing.Point(24, 42);
            this.lblRdate.Name = "lblRdate";
            this.lblRdate.Size = new System.Drawing.Size(56, 23);
            this.lblRdate.TabIndex = 100007;
            this.lblRdate.Text = "取込日";
            this.lblRdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(26, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 100008;
            this.label6.Text = "検索結果";
            // 
            // SearchOutclearingBranchList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SearchOutclearingBranchList";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvListList;
		private System.Windows.Forms.ColumnHeader clOPERATION_DATE;
		private System.Windows.Forms.ColumnHeader clTOTAL_COUNT;
        private System.Windows.Forms.ColumnHeader clOC_BR_NO;
        private System.Windows.Forms.ColumnHeader clCLEARING_DATE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private CommonClass.NumTextBox2 dtOC_BR_NO;
        private CommonClass.DTextBox2 dtClearingDate;
        private CommonClass.DTextBox2 dtRdate;
        private System.Windows.Forms.Label lblClearingDate;
        private System.Windows.Forms.Label lblOC_BR_NO;
        private System.Windows.Forms.Label lblRdate;
        private System.Windows.Forms.ColumnHeader clTOTAL_AMOUNT;
    }
}
