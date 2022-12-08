namespace CorrectInput
{
	partial class BatchListForm
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
            System.Windows.Forms.ColumnHeader columnHeader1;
            this.livBatListOc = new System.Windows.Forms.ListView();
            this.clBatId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDetailsNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clOcBrCd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clOcIcBkCd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clOcIcBkName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clClearingDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clKingaku = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clInputSts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clInputTerm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblGymName = new System.Windows.Forms.Label();
            this.lblInputMode = new System.Windows.Forms.Label();
            this.livBatListIc = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.lblInputMode);
            this.contentsPanel.Controls.Add(this.lblGymName);
            this.contentsPanel.Controls.Add(this.livBatListIc);
            this.contentsPanel.Controls.Add(this.livBatListOc);
            this.contentsPanel.Controls.SetChildIndex(this.livBatListOc, 0);
            this.contentsPanel.Controls.SetChildIndex(this.livBatListIc, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblGymName, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblInputMode, 0);
            // 
            // clKey
            // 
            clKey.Text = "clKey";
            clKey.Width = 0;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "clKey";
            columnHeader1.Width = 0;
            // 
            // livBatListOc
            // 
            this.livBatListOc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.clBatId,
            this.clDetailsNo,
            this.clOcBrCd,
            this.clOcIcBkCd,
            this.clOcIcBkName,
            this.clClearingDate,
            this.clKingaku,
            this.clInputSts,
            this.clInputTerm});
            this.livBatListOc.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.livBatListOc.FullRowSelect = true;
            this.livBatListOc.GridLines = true;
            this.livBatListOc.HideSelection = false;
            this.livBatListOc.Location = new System.Drawing.Point(14, 56);
            this.livBatListOc.MultiSelect = false;
            this.livBatListOc.Name = "livBatListOc";
            this.livBatListOc.Size = new System.Drawing.Size(1239, 748);
            this.livBatListOc.TabIndex = 5;
            this.livBatListOc.TabStop = false;
            this.livBatListOc.UseCompatibleStateImageBehavior = false;
            this.livBatListOc.View = System.Windows.Forms.View.Details;
            this.livBatListOc.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvBatList_ColumnWidthChanging);
            this.livBatListOc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.livBatList_KeyDown);
            this.livBatListOc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.livBatList_MouseDoubleClick);
            // 
            // clBatId
            // 
            this.clBatId.Text = "バッチ番号";
            this.clBatId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clBatId.Width = 88;
            // 
            // clDetailsNo
            // 
            this.clDetailsNo.Text = "明細番号";
            this.clDetailsNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clDetailsNo.Width = 80;
            // 
            // clOcBrCd
            // 
            this.clOcBrCd.Text = "持出支店";
            this.clOcBrCd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clOcBrCd.Width = 120;
            // 
            // clOcIcBkCd
            // 
            this.clOcIcBkCd.Text = "持帰銀行";
            this.clOcIcBkCd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clOcIcBkCd.Width = 120;
            // 
            // clOcIcBkName
            // 
            this.clOcIcBkName.Text = "持帰銀行名";
            this.clOcIcBkName.Width = 200;
            // 
            // clClearingDate
            // 
            this.clClearingDate.Text = "交換希望日";
            this.clClearingDate.Width = 120;
            // 
            // clKingaku
            // 
            this.clKingaku.Text = "金額";
            this.clKingaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clKingaku.Width = 170;
            // 
            // clInputSts
            // 
            this.clInputSts.Text = "入力状態";
            this.clInputSts.Width = 200;
            // 
            // clInputTerm
            // 
            this.clInputTerm.Text = "入力端末";
            this.clInputTerm.Width = 120;
            // 
            // lblGymName
            // 
            this.lblGymName.AutoSize = true;
            this.lblGymName.Location = new System.Drawing.Point(18, 17);
            this.lblGymName.Name = "lblGymName";
            this.lblGymName.Size = new System.Drawing.Size(66, 19);
            this.lblGymName.TabIndex = 100000;
            this.lblGymName.Text = "業務名";
            // 
            // lblInputMode
            // 
            this.lblInputMode.AutoSize = true;
            this.lblInputMode.Location = new System.Drawing.Point(111, 17);
            this.lblInputMode.Name = "lblInputMode";
            this.lblInputMode.Size = new System.Drawing.Size(91, 19);
            this.lblInputMode.TabIndex = 100000;
            this.lblInputMode.Text = "入力モード";
            // 
            // livBatListIc
            // 
            this.livBatListIc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16});
            this.livBatListIc.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.livBatListIc.FullRowSelect = true;
            this.livBatListIc.GridLines = true;
            this.livBatListIc.HideSelection = false;
            this.livBatListIc.Location = new System.Drawing.Point(14, 56);
            this.livBatListIc.MultiSelect = false;
            this.livBatListIc.Name = "livBatListIc";
            this.livBatListIc.Size = new System.Drawing.Size(1239, 748);
            this.livBatListIc.TabIndex = 100001;
            this.livBatListIc.TabStop = false;
            this.livBatListIc.UseCompatibleStateImageBehavior = false;
            this.livBatListIc.View = System.Windows.Forms.View.Details;
            this.livBatListIc.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvBatList_ColumnWidthChanging);
            this.livBatListIc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.livBatList_KeyDown);
            this.livBatListIc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.livBatList_MouseDoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "取込日";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 88;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "明細番号";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 80;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "証券コード";
            this.columnHeader7.Width = 80;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "証券種類名称";
            this.columnHeader8.Width = 110;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "持出銀行";
            this.columnHeader9.Width = 80;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "持帰銀行";
            this.columnHeader10.Width = 80;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "持帰支店";
            this.columnHeader11.Width = 80;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "電子交換所交換希望日";
            this.columnHeader12.Width = 175;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "交換希望日";
            this.columnHeader13.Width = 95;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "金額";
            this.columnHeader14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader14.Width = 120;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "入力状態";
            this.columnHeader15.Width = 150;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "入力端末";
            this.columnHeader16.Width = 80;
            // 
            // BatchListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "BatchListForm";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView livBatListOc;
		private System.Windows.Forms.ColumnHeader clBatId;
		private System.Windows.Forms.ColumnHeader clOcIcBkCd;
        private System.Windows.Forms.ColumnHeader clDetailsNo;
        private System.Windows.Forms.ColumnHeader clOcBrCd;
        private System.Windows.Forms.ColumnHeader clOcIcBkName;
        private System.Windows.Forms.ColumnHeader clClearingDate;
        private System.Windows.Forms.ColumnHeader clKingaku;
        private System.Windows.Forms.ColumnHeader clInputSts;
        private System.Windows.Forms.ColumnHeader clInputTerm;
        private System.Windows.Forms.Label lblInputMode;
        private System.Windows.Forms.Label lblGymName;
        private System.Windows.Forms.ListView livBatListIc;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
    }
}
