namespace SearchResultText
{
	partial class SearchResultDetail
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
            this.label1 = new System.Windows.Forms.Label();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblFILE_DIVID = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblFILE_NAME = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblBK_NO = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblFILE_CHK_CODE = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblRECORD_COUNT = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRECV_DATE = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblRECV_TIME = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lvDataList = new System.Windows.Forms.ListView();
            this.dgFIELDNAME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dgVALUE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.lvDataList);
            this.contentsPanel.Controls.Add(this.pnlInfo);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.contentsPanel.Size = new System.Drawing.Size(1266, 844);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.pnlInfo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lvDataList, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(16, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 100000;
            this.label1.Text = "詳細";
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.lblFILE_DIVID);
            this.pnlInfo.Controls.Add(this.label11);
            this.pnlInfo.Controls.Add(this.lblFILE_NAME);
            this.pnlInfo.Controls.Add(this.label6);
            this.pnlInfo.Controls.Add(this.lblBK_NO);
            this.pnlInfo.Controls.Add(this.label7);
            this.pnlInfo.Controls.Add(this.lblFILE_CHK_CODE);
            this.pnlInfo.Controls.Add(this.label8);
            this.pnlInfo.Controls.Add(this.lblRECORD_COUNT);
            this.pnlInfo.Controls.Add(this.label9);
            this.pnlInfo.Controls.Add(this.lblRECV_DATE);
            this.pnlInfo.Controls.Add(this.label10);
            this.pnlInfo.Controls.Add(this.lblRECV_TIME);
            this.pnlInfo.Controls.Add(this.label13);
            this.pnlInfo.Location = new System.Drawing.Point(19, 33);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(1222, 73);
            this.pnlInfo.TabIndex = 100009;
            // 
            // lblFILE_DIVID
            // 
            this.lblFILE_DIVID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFILE_DIVID.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblFILE_DIVID.Location = new System.Drawing.Point(0, 30);
            this.lblFILE_DIVID.Margin = new System.Windows.Forms.Padding(0);
            this.lblFILE_DIVID.Name = "lblFILE_DIVID";
            this.lblFILE_DIVID.Size = new System.Drawing.Size(261, 34);
            this.lblFILE_DIVID.TabIndex = 100025;
            this.lblFILE_DIVID.Text = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            this.lblFILE_DIVID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(261, 31);
            this.label11.TabIndex = 100018;
            this.label11.Text = "ファイル識別区分";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFILE_NAME
            // 
            this.lblFILE_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFILE_NAME.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblFILE_NAME.Location = new System.Drawing.Point(260, 30);
            this.lblFILE_NAME.Margin = new System.Windows.Forms.Padding(0);
            this.lblFILE_NAME.Name = "lblFILE_NAME";
            this.lblFILE_NAME.Size = new System.Drawing.Size(235, 34);
            this.lblFILE_NAME.TabIndex = 100026;
            this.lblFILE_NAME.Text = "XXXXXXXXXXXXXXX";
            this.lblFILE_NAME.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label6.Location = new System.Drawing.Point(260, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(235, 31);
            this.label6.TabIndex = 100019;
            this.label6.Text = "ファイル名";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBK_NO
            // 
            this.lblBK_NO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBK_NO.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblBK_NO.Location = new System.Drawing.Point(494, 30);
            this.lblBK_NO.Margin = new System.Windows.Forms.Padding(0);
            this.lblBK_NO.Name = "lblBK_NO";
            this.lblBK_NO.Size = new System.Drawing.Size(114, 34);
            this.lblBK_NO.TabIndex = 100027;
            this.lblBK_NO.Text = "XXXX";
            this.lblBK_NO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label7.Location = new System.Drawing.Point(494, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 31);
            this.label7.TabIndex = 100020;
            this.label7.Text = "銀行コード";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFILE_CHK_CODE
            // 
            this.lblFILE_CHK_CODE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFILE_CHK_CODE.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblFILE_CHK_CODE.Location = new System.Drawing.Point(607, 30);
            this.lblFILE_CHK_CODE.Margin = new System.Windows.Forms.Padding(0);
            this.lblFILE_CHK_CODE.Name = "lblFILE_CHK_CODE";
            this.lblFILE_CHK_CODE.Size = new System.Drawing.Size(283, 34);
            this.lblFILE_CHK_CODE.TabIndex = 100028;
            this.lblFILE_CHK_CODE.Text = "XXXX";
            this.lblFILE_CHK_CODE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label8.Location = new System.Drawing.Point(607, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(283, 31);
            this.label8.TabIndex = 100021;
            this.label8.Text = "ファイルチェック結果コード";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRECORD_COUNT
            // 
            this.lblRECORD_COUNT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRECORD_COUNT.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblRECORD_COUNT.Location = new System.Drawing.Point(889, 30);
            this.lblRECORD_COUNT.Margin = new System.Windows.Forms.Padding(0);
            this.lblRECORD_COUNT.Name = "lblRECORD_COUNT";
            this.lblRECORD_COUNT.Size = new System.Drawing.Size(153, 34);
            this.lblRECORD_COUNT.TabIndex = 100029;
            this.lblRECORD_COUNT.Text = "XXXX";
            this.lblRECORD_COUNT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label9.Location = new System.Drawing.Point(889, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(153, 31);
            this.label9.TabIndex = 100022;
            this.label9.Text = "件数";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRECV_DATE
            // 
            this.lblRECV_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRECV_DATE.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblRECV_DATE.Location = new System.Drawing.Point(1041, 30);
            this.lblRECV_DATE.Margin = new System.Windows.Forms.Padding(0);
            this.lblRECV_DATE.Name = "lblRECV_DATE";
            this.lblRECV_DATE.Size = new System.Drawing.Size(86, 34);
            this.lblRECV_DATE.TabIndex = 100030;
            this.lblRECV_DATE.Text = "XXXX";
            this.lblRECV_DATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label10.Location = new System.Drawing.Point(1041, 0);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 31);
            this.label10.TabIndex = 100023;
            this.label10.Text = "取込日";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRECV_TIME
            // 
            this.lblRECV_TIME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRECV_TIME.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblRECV_TIME.Location = new System.Drawing.Point(1126, 30);
            this.lblRECV_TIME.Margin = new System.Windows.Forms.Padding(0);
            this.lblRECV_TIME.Name = "lblRECV_TIME";
            this.lblRECV_TIME.Size = new System.Drawing.Size(86, 34);
            this.lblRECV_TIME.TabIndex = 100031;
            this.lblRECV_TIME.Text = "XXXX";
            this.lblRECV_TIME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label13.Location = new System.Drawing.Point(1126, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 31);
            this.label13.TabIndex = 100024;
            this.label13.Text = "取込時刻";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvDataList
            // 
            this.lvDataList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dgFIELDNAME,
            this.dgVALUE});
            this.lvDataList.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lvDataList.FullRowSelect = true;
            this.lvDataList.GridLines = true;
            this.lvDataList.HideSelection = false;
            this.lvDataList.Location = new System.Drawing.Point(19, 122);
            this.lvDataList.Margin = new System.Windows.Forms.Padding(0);
            this.lvDataList.MultiSelect = false;
            this.lvDataList.Name = "lvDataList";
            this.lvDataList.Size = new System.Drawing.Size(1232, 698);
            this.lvDataList.TabIndex = 100010;
            this.lvDataList.TabStop = false;
            this.lvDataList.UseCompatibleStateImageBehavior = false;
            this.lvDataList.View = System.Windows.Forms.View.Details;
            this.lvDataList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lv_ColumnWidthChanging);
            // 
            // dgFIELDNAME
            // 
            this.dgFIELDNAME.Text = "項目名";
            this.dgFIELDNAME.Width = 250;
            // 
            // dgVALUE
            // 
            this.dgVALUE.Text = "値";
            this.dgVALUE.Width = 330;
            // 
            // SearchResultDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SearchResultDetail";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblFILE_DIVID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblFILE_NAME;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblBK_NO;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblFILE_CHK_CODE;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblRECORD_COUNT;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblRECV_DATE;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblRECV_TIME;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListView lvDataList;
        private System.Windows.Forms.ColumnHeader dgFIELDNAME;
        private System.Windows.Forms.ColumnHeader dgVALUE;
    }
}
