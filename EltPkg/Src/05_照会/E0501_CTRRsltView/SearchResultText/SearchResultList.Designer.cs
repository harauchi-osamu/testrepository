namespace SearchResultText
{
	partial class SearchResultList
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
            this.dgFILE_DIVID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dgFILE_NAME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dgBK_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dgFILE_CHK_CODE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dgRECORD_COUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dgRECV_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dgRECV_TIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblFILE_DIVID = new System.Windows.Forms.Label();
            this.lblErrFlg = new System.Windows.Forms.Label();
            this.cmbFILE_DIVID = new System.Windows.Forms.ComboBox();
            this.cmbErrFlg = new System.Windows.Forms.ComboBox();
            this.dtRecvDate = new CommonClass.DTextBox2();
            this.lblRecvDate = new System.Windows.Forms.Label();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.contentsPanel.Controls.Add(this.dtRecvDate);
            this.contentsPanel.Controls.Add(this.lblRecvDate);
            this.contentsPanel.Controls.Add(this.cmbErrFlg);
            this.contentsPanel.Controls.Add(this.cmbFILE_DIVID);
            this.contentsPanel.Controls.Add(this.lblErrFlg);
            this.contentsPanel.Controls.Add(this.lblFILE_DIVID);
            this.contentsPanel.Controls.Add(this.label13);
            this.contentsPanel.Controls.Add(this.label11);
            this.contentsPanel.Controls.Add(this.lvResultList);
            this.contentsPanel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.contentsPanel.Size = new System.Drawing.Size(1266, 844);
            this.contentsPanel.Controls.SetChildIndex(this.lvResultList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label11, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label13, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblFILE_DIVID, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblErrFlg, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbFILE_DIVID, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbErrFlg, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblRecvDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtRecvDate, 0);
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
            this.dgFILE_DIVID,
            this.dgFILE_NAME,
            this.dgBK_NO,
            this.dgFILE_CHK_CODE,
            this.dgRECORD_COUNT,
            this.dgRECV_DATE,
            this.dgRECV_TIME});
            this.lvResultList.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lvResultList.FullRowSelect = true;
            this.lvResultList.GridLines = true;
            this.lvResultList.HideSelection = false;
            this.lvResultList.Location = new System.Drawing.Point(16, 126);
            this.lvResultList.MultiSelect = false;
            this.lvResultList.Name = "lvResultList";
            this.lvResultList.Size = new System.Drawing.Size(1229, 690);
            this.lvResultList.TabIndex = 3;
            this.lvResultList.TabStop = false;
            this.lvResultList.UseCompatibleStateImageBehavior = false;
            this.lvResultList.View = System.Windows.Forms.View.Details;
            this.lvResultList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lv_ColumnWidthChanging);
            this.lvResultList.DoubleClick += new System.EventHandler(this.lv_DoubleClick);
            this.lvResultList.Enter += new System.EventHandler(this.List_Enter);
            this.lvResultList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lv_KeyDown);
            // 
            // dgFILE_DIVID
            // 
            this.dgFILE_DIVID.Text = "ファイル識別区分";
            this.dgFILE_DIVID.Width = 235;
            // 
            // dgFILE_NAME
            // 
            this.dgFILE_NAME.Text = "ファイル名";
            this.dgFILE_NAME.Width = 295;
            // 
            // dgBK_NO
            // 
            this.dgBK_NO.Text = "銀行コード";
            this.dgBK_NO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dgBK_NO.Width = 90;
            // 
            // dgFILE_CHK_CODE
            // 
            this.dgFILE_CHK_CODE.Text = "ファイルチェック結果コード";
            this.dgFILE_CHK_CODE.Width = 325;
            // 
            // dgRECORD_COUNT
            // 
            this.dgRECORD_COUNT.Text = "件数";
            this.dgRECORD_COUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.dgRECORD_COUNT.Width = 100;
            // 
            // dgRECV_DATE
            // 
            this.dgRECV_DATE.Text = "取込日";
            this.dgRECV_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dgRECV_DATE.Width = 82;
            // 
            // dgRECV_TIME
            // 
            this.dgRECV_TIME.Text = "取込時刻";
            this.dgRECV_TIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dgRECV_TIME.Width = 80;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(10, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(151, 16);
            this.label11.TabIndex = 6;
            this.label11.Text = "ファイル絞り込み条件";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(10, 107);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(76, 16);
            this.label13.TabIndex = 6;
            this.label13.Text = "結果一覧";
            // 
            // lblFILE_DIVID
            // 
            this.lblFILE_DIVID.AutoSize = true;
            this.lblFILE_DIVID.Cursor = System.Windows.Forms.Cursors.No;
            this.lblFILE_DIVID.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblFILE_DIVID.Location = new System.Drawing.Point(20, 45);
            this.lblFILE_DIVID.Name = "lblFILE_DIVID";
            this.lblFILE_DIVID.Size = new System.Drawing.Size(117, 16);
            this.lblFILE_DIVID.TabIndex = 100003;
            this.lblFILE_DIVID.Text = "ファイル識別区分";
            this.lblFILE_DIVID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblErrFlg
            // 
            this.lblErrFlg.AutoSize = true;
            this.lblErrFlg.Cursor = System.Windows.Forms.Cursors.No;
            this.lblErrFlg.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblErrFlg.Location = new System.Drawing.Point(807, 45);
            this.lblErrFlg.Name = "lblErrFlg";
            this.lblErrFlg.Size = new System.Drawing.Size(44, 16);
            this.lblErrFlg.TabIndex = 100003;
            this.lblErrFlg.Text = "エラー";
            this.lblErrFlg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbFILE_DIVID
            // 
            this.cmbFILE_DIVID.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFILE_DIVID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFILE_DIVID.FormattingEnabled = true;
            this.cmbFILE_DIVID.Location = new System.Drawing.Point(143, 41);
            this.cmbFILE_DIVID.Name = "cmbFILE_DIVID";
            this.cmbFILE_DIVID.Size = new System.Drawing.Size(407, 24);
            this.cmbFILE_DIVID.TabIndex = 1;
            this.cmbFILE_DIVID.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbFILE_DIVID.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbFILE_DIVID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // cmbErrFlg
            // 
            this.cmbErrFlg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbErrFlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbErrFlg.FormattingEnabled = true;
            this.cmbErrFlg.Location = new System.Drawing.Point(862, 41);
            this.cmbErrFlg.Name = "cmbErrFlg";
            this.cmbErrFlg.Size = new System.Drawing.Size(134, 24);
            this.cmbErrFlg.TabIndex = 3;
            this.cmbErrFlg.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbErrFlg.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbErrFlg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // dtRecvDate
            // 
            this.dtRecvDate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtRecvDate.KeyControl = true;
            this.dtRecvDate.Location = new System.Drawing.Point(641, 41);
            this.dtRecvDate.MaxLength = 8;
            this.dtRecvDate.Name = "dtRecvDate";
            this.dtRecvDate.OnEnterSeparatorCut = true;
            this.dtRecvDate.Size = new System.Drawing.Size(145, 23);
            this.dtRecvDate.TabIndex = 2;
            this.dtRecvDate.TabKeyControl = true;
            this.dtRecvDate.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtRecvDate.Enter += new System.EventHandler(this.txt_Enter);
            this.dtRecvDate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblRecvDate
            // 
            this.lblRecvDate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblRecvDate.Location = new System.Drawing.Point(565, 43);
            this.lblRecvDate.Name = "lblRecvDate";
            this.lblRecvDate.Size = new System.Drawing.Size(68, 18);
            this.lblRecvDate.TabIndex = 100065;
            this.lblRecvDate.Text = "取込日";
            this.lblRecvDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchResultList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SearchResultList";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvResultList;
		private System.Windows.Forms.ColumnHeader dgFILE_NAME;
		private System.Windows.Forms.ColumnHeader dgFILE_CHK_CODE;
        private System.Windows.Forms.ColumnHeader dgBK_NO;
        private System.Windows.Forms.ColumnHeader dgFILE_DIVID;
        private System.Windows.Forms.ColumnHeader dgRECORD_COUNT;
        private System.Windows.Forms.ColumnHeader dgRECV_DATE;
        private System.Windows.Forms.ColumnHeader dgRECV_TIME;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblErrFlg;
        private System.Windows.Forms.Label lblFILE_DIVID;
        private System.Windows.Forms.ComboBox cmbFILE_DIVID;
        private System.Windows.Forms.ComboBox cmbErrFlg;
        private CommonClass.DTextBox2 dtRecvDate;
        private System.Windows.Forms.Label lblRecvDate;
    }
}
