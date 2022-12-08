namespace CTRIcRequestMk
{
	partial class FileCreateForm
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
            this.lblClearingDate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMochikaeriKbn = new System.Windows.Forms.Label();
            this.lblImageYouhi = new System.Windows.Forms.Label();
            this.cmbClearingType = new System.Windows.Forms.ComboBox();
            this.txtMochikaeriKbn = new CommonClass.NumTextBox2();
            this.txtImageYouhi = new CommonClass.NumTextBox2();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtClearingDateTo = new CommonClass.DTextBox2();
            this.txtClearingDateFrom = new CommonClass.DTextBox2();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.lblFileName);
            this.contentsPanel.Controls.Add(this.label3);
            this.contentsPanel.Controls.Add(this.lblTitle);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.Add(this.txtClearingDateFrom);
            this.contentsPanel.Controls.Add(this.txtClearingDateTo);
            this.contentsPanel.Controls.Add(this.label9);
            this.contentsPanel.Controls.Add(this.label8);
            this.contentsPanel.Controls.Add(this.label7);
            this.contentsPanel.Controls.Add(this.txtImageYouhi);
            this.contentsPanel.Controls.Add(this.txtMochikaeriKbn);
            this.contentsPanel.Controls.Add(this.cmbClearingType);
            this.contentsPanel.Controls.Add(this.lblImageYouhi);
            this.contentsPanel.Controls.Add(this.lblMochikaeriKbn);
            this.contentsPanel.Controls.Add(this.lblClearingDate);
            this.contentsPanel.Controls.SetChildIndex(this.lblClearingDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblMochikaeriKbn, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblImageYouhi, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbClearingType, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtMochikaeriKbn, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtImageYouhi, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label7, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label8, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label9, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtClearingDateTo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtClearingDateFrom, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblTitle, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label3, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblFileName, 0);
            // 
            // lblClearingDate
            // 
            this.lblClearingDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblClearingDate.Location = new System.Drawing.Point(400, 275);
            this.lblClearingDate.Name = "lblClearingDate";
            this.lblClearingDate.Size = new System.Drawing.Size(158, 26);
            this.lblClearingDate.TabIndex = 100005;
            this.lblClearingDate.Text = "交換希望日";
            this.lblClearingDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(400, 300);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 27);
            this.label1.TabIndex = 100005;
            this.label1.Text = "交換証券種類";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMochikaeriKbn
            // 
            this.lblMochikaeriKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMochikaeriKbn.Location = new System.Drawing.Point(400, 326);
            this.lblMochikaeriKbn.Name = "lblMochikaeriKbn";
            this.lblMochikaeriKbn.Size = new System.Drawing.Size(158, 26);
            this.lblMochikaeriKbn.TabIndex = 100005;
            this.lblMochikaeriKbn.Text = "持帰対象区分";
            this.lblMochikaeriKbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblImageYouhi
            // 
            this.lblImageYouhi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblImageYouhi.Location = new System.Drawing.Point(400, 351);
            this.lblImageYouhi.Name = "lblImageYouhi";
            this.lblImageYouhi.Size = new System.Drawing.Size(158, 26);
            this.lblImageYouhi.TabIndex = 100005;
            this.lblImageYouhi.Text = "証券イメージ要否";
            this.lblImageYouhi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbClearingType
            // 
            this.cmbClearingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClearingType.FormattingEnabled = true;
            this.cmbClearingType.Location = new System.Drawing.Point(557, 300);
            this.cmbClearingType.Name = "cmbClearingType";
            this.cmbClearingType.Size = new System.Drawing.Size(276, 27);
            this.cmbClearingType.TabIndex = 3;
            this.cmbClearingType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // txtMochikaeriKbn
            // 
            this.txtMochikaeriKbn.KeyControl = true;
            this.txtMochikaeriKbn.Location = new System.Drawing.Point(557, 326);
            this.txtMochikaeriKbn.MaxLength = 1;
            this.txtMochikaeriKbn.Name = "txtMochikaeriKbn";
            this.txtMochikaeriKbn.Size = new System.Drawing.Size(30, 26);
            this.txtMochikaeriKbn.TabIndex = 4;
            this.txtMochikaeriKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMochikaeriKbn.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            // 
            // txtImageYouhi
            // 
            this.txtImageYouhi.KeyControl = true;
            this.txtImageYouhi.Location = new System.Drawing.Point(557, 351);
            this.txtImageYouhi.MaxLength = 1;
            this.txtImageYouhi.Name = "txtImageYouhi";
            this.txtImageYouhi.Size = new System.Drawing.Size(30, 26);
            this.txtImageYouhi.TabIndex = 5;
            this.txtImageYouhi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtImageYouhi.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(593, 330);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(177, 19);
            this.label7.TabIndex = 100009;
            this.label7.Text = "（0：全量　1：未持帰）";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(593, 355);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 19);
            this.label8.TabIndex = 100009;
            this.label8.Text = "（0：否　1：要）";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(659, 278);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 19);
            this.label9.TabIndex = 100009;
            this.label9.Text = "～";
            // 
            // txtClearingDateTo
            // 
            this.txtClearingDateTo.KeyControl = true;
            this.txtClearingDateTo.Location = new System.Drawing.Point(687, 275);
            this.txtClearingDateTo.MaxLength = 8;
            this.txtClearingDateTo.Name = "txtClearingDateTo";
            this.txtClearingDateTo.OnEnterSeparatorCut = true;
            this.txtClearingDateTo.Size = new System.Drawing.Size(100, 26);
            this.txtClearingDateTo.TabIndex = 2;
            this.txtClearingDateTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClearingDateTo.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            // 
            // txtClearingDateFrom
            // 
            this.txtClearingDateFrom.KeyControl = true;
            this.txtClearingDateFrom.Location = new System.Drawing.Point(557, 275);
            this.txtClearingDateFrom.MaxLength = 8;
            this.txtClearingDateFrom.Name = "txtClearingDateFrom";
            this.txtClearingDateFrom.OnEnterSeparatorCut = true;
            this.txtClearingDateFrom.Size = new System.Drawing.Size(100, 26);
            this.txtClearingDateFrom.TabIndex = 1;
            this.txtClearingDateFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClearingDateFrom.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTitle.Location = new System.Drawing.Point(396, 245);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(89, 19);
            this.lblTitle.TabIndex = 100010;
            this.lblTitle.Text = "当日交換";
            // 
            // lblFileName
            // 
            this.lblFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFileName.Location = new System.Drawing.Point(395, 433);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(438, 26);
            this.lblFileName.TabIndex = 100012;
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(396, 408);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 19);
            this.label3.TabIndex = 100011;
            this.label3.Text = "（作成ファイル）";
            // 
            // FileCreateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "FileCreateForm";
            this.Load += new System.EventHandler(this.Form_Load);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.Label lblImageYouhi;
        private System.Windows.Forms.Label lblMochikaeriKbn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblClearingDate;
        private System.Windows.Forms.ComboBox cmbClearingType;
        private CommonClass.NumTextBox2 txtImageYouhi;
        private CommonClass.NumTextBox2 txtMochikaeriKbn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private CommonClass.DTextBox2 txtClearingDateFrom;
        private CommonClass.DTextBox2 txtClearingDateTo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label label3;
    }
}
