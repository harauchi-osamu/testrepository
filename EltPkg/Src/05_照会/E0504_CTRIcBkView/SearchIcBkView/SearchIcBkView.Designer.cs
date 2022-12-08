namespace SearchIcBkView
{
	partial class SearchIcBkView
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
            this.clBk_CO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBkName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTotalCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTotalMount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblRdate = new System.Windows.Forms.Label();
            this.dtRdate = new CommonClass.DTextBox2();
            this.lblOC_BK_NO = new System.Windows.Forms.Label();
            this.lblClearingDate = new System.Windows.Forms.Label();
            this.dtClearingDate = new CommonClass.DTextBox2();
            this.nOC_BK_NO = new CommonClass.NumTextBox2();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.label7);
            this.contentsPanel.Controls.Add(this.label6);
            this.contentsPanel.Controls.Add(this.nOC_BK_NO);
            this.contentsPanel.Controls.Add(this.dtClearingDate);
            this.contentsPanel.Controls.Add(this.dtRdate);
            this.contentsPanel.Controls.Add(this.lblClearingDate);
            this.contentsPanel.Controls.Add(this.lblOC_BK_NO);
            this.contentsPanel.Controls.Add(this.lblRdate);
            this.contentsPanel.Controls.Add(this.lvResultList);
            this.contentsPanel.Controls.SetChildIndex(this.lvResultList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblRdate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblOC_BK_NO, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblClearingDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtRdate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtClearingDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.nOC_BK_NO, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label6, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label7, 0);
            // 
            // clKey
            // 
            clKey.Text = "clKey";
            clKey.Width = 0;
            // 
            // lvResultList
            // 
            this.lvResultList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvResultList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.clBk_CO,
            this.clBkName,
            this.clDate,
            this.clTotalCount,
            this.clTotalMount});
            this.lvResultList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvResultList.FullRowSelect = true;
            this.lvResultList.GridLines = true;
            this.lvResultList.HideSelection = false;
            this.lvResultList.Location = new System.Drawing.Point(30, 110);
            this.lvResultList.MultiSelect = false;
            this.lvResultList.Name = "lvResultList";
            this.lvResultList.Size = new System.Drawing.Size(861, 689);
            this.lvResultList.TabIndex = 5;
            this.lvResultList.TabStop = false;
            this.lvResultList.UseCompatibleStateImageBehavior = false;
            this.lvResultList.View = System.Windows.Forms.View.Details;
            this.lvResultList.Enter += new System.EventHandler(this.List_Enter);
            // 
            // clBk_CO
            // 
            this.clBk_CO.Text = "持出銀行コード";
            this.clBk_CO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clBk_CO.Width = 124;
            // 
            // clBkName
            // 
            this.clBkName.Text = "持出銀行名";
            this.clBkName.Width = 250;
            // 
            // clDate
            // 
            this.clDate.Text = "交換日";
            this.clDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clDate.Width = 143;
            // 
            // clTotalCount
            // 
            this.clTotalCount.Text = "明細総枚数";
            this.clTotalCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clTotalCount.Width = 112;
            // 
            // clTotalMount
            // 
            this.clTotalMount.Text = "明細総金額";
            this.clTotalMount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clTotalMount.Width = 214;
            // 
            // lblRdate
            // 
            this.lblRdate.AutoSize = true;
            this.lblRdate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblRdate.Location = new System.Drawing.Point(30, 45);
            this.lblRdate.Name = "lblRdate";
            this.lblRdate.Size = new System.Drawing.Size(56, 16);
            this.lblRdate.TabIndex = 100000;
            this.lblRdate.Text = "取込日";
            // 
            // dtRdate
            // 
            this.dtRdate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtRdate.KeyControl = true;
            this.dtRdate.Location = new System.Drawing.Point(92, 42);
            this.dtRdate.MaxLength = 8;
            this.dtRdate.Name = "dtRdate";
            this.dtRdate.OnEnterSeparatorCut = true;
            this.dtRdate.Size = new System.Drawing.Size(100, 23);
            this.dtRdate.TabIndex = 1;
            this.dtRdate.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtRdate.Enter += new System.EventHandler(this.txt_Enter);
            this.dtRdate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblOC_BK_NO
            // 
            this.lblOC_BK_NO.AutoSize = true;
            this.lblOC_BK_NO.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOC_BK_NO.Location = new System.Drawing.Point(198, 45);
            this.lblOC_BK_NO.Name = "lblOC_BK_NO";
            this.lblOC_BK_NO.Size = new System.Drawing.Size(108, 16);
            this.lblOC_BK_NO.TabIndex = 100000;
            this.lblOC_BK_NO.Text = "持出銀行コード";
            // 
            // lblClearingDate
            // 
            this.lblClearingDate.AutoSize = true;
            this.lblClearingDate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblClearingDate.Location = new System.Drawing.Point(421, 45);
            this.lblClearingDate.Name = "lblClearingDate";
            this.lblClearingDate.Size = new System.Drawing.Size(56, 16);
            this.lblClearingDate.TabIndex = 100000;
            this.lblClearingDate.Text = "交換日";
            // 
            // dtClearingDate
            // 
            this.dtClearingDate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtClearingDate.KeyControl = true;
            this.dtClearingDate.Location = new System.Drawing.Point(483, 42);
            this.dtClearingDate.MaxLength = 8;
            this.dtClearingDate.Name = "dtClearingDate";
            this.dtClearingDate.OnEnterSeparatorCut = true;
            this.dtClearingDate.Size = new System.Drawing.Size(100, 23);
            this.dtClearingDate.TabIndex = 3;
            this.dtClearingDate.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtClearingDate.Enter += new System.EventHandler(this.txt_Enter);
            this.dtClearingDate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // nOC_BK_NO
            // 
            this.nOC_BK_NO.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nOC_BK_NO.KeyControl = true;
            this.nOC_BK_NO.Location = new System.Drawing.Point(312, 42);
            this.nOC_BK_NO.MaxLength = 4;
            this.nOC_BK_NO.Name = "nOC_BK_NO";
            this.nOC_BK_NO.Size = new System.Drawing.Size(100, 23);
            this.nOC_BK_NO.TabIndex = 2;
            this.nOC_BK_NO.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.nOC_BK_NO.Enter += new System.EventHandler(this.txt_Enter);
            this.nOC_BK_NO.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(27, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 100001;
            this.label6.Text = "検索結果";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(27, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 16);
            this.label7.TabIndex = 100001;
            this.label7.Text = "検索条件";
            // 
            // SearchIcBkView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SearchIcBkView";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvResultList;
		private System.Windows.Forms.ColumnHeader clTotalCount;
        private System.Windows.Forms.ColumnHeader clBkName;
        private System.Windows.Forms.ColumnHeader clDate;
        private CommonClass.DTextBox2 dtClearingDate;
        private CommonClass.DTextBox2 dtRdate;
        private System.Windows.Forms.Label lblClearingDate;
        private System.Windows.Forms.Label lblOC_BK_NO;
        private System.Windows.Forms.Label lblRdate;
        private System.Windows.Forms.ColumnHeader clTotalMount;
        private CommonClass.NumTextBox2 nOC_BK_NO;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColumnHeader clBk_CO;
    }
}
