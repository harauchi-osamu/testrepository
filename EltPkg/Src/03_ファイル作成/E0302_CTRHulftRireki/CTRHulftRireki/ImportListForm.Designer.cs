namespace CTRHulftRireki
{
	partial class ImportListForm
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.lstImportList = new System.Windows.Forms.ListView();
            this.clFileId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clFileKbn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel5 = new System.Windows.Forms.Panel();
            this.chkNG = new System.Windows.Forms.CheckBox();
            this.chkHoryu = new System.Windows.Forms.CheckBox();
            this.chkNon = new System.Windows.Forms.CheckBox();
            this.chkOK = new System.Windows.Forms.CheckBox();
            this.cmbFile = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.headerControl = new CTRHulftRireki.HeaderControl();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.panel3);
            this.contentsPanel.Controls.Add(this.headerControl);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.headerControl, 0);
            this.contentsPanel.Controls.SetChildIndex(this.panel3, 0);
            // 
            // clKey
            // 
            clKey.Text = "clKey";
            clKey.Width = 0;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lstImportList);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.cmbFile);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.lblTitle);
            this.panel3.Location = new System.Drawing.Point(5, 46);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1258, 788);
            this.panel3.TabIndex = 100002;
            // 
            // lstImportList
            // 
            this.lstImportList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.clFileId,
            this.clFileKbn,
            this.clStatus,
            this.clDateTime,
            this.clFileName});
            this.lstImportList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lstImportList.FullRowSelect = true;
            this.lstImportList.GridLines = true;
            this.lstImportList.HideSelection = false;
            this.lstImportList.Location = new System.Drawing.Point(27, 130);
            this.lstImportList.MultiSelect = false;
            this.lstImportList.Name = "lstImportList";
            this.lstImportList.Size = new System.Drawing.Size(1201, 629);
            this.lstImportList.TabIndex = 100013;
            this.lstImportList.TabStop = false;
            this.lstImportList.UseCompatibleStateImageBehavior = false;
            this.lstImportList.View = System.Windows.Forms.View.Details;
            this.lstImportList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lstImportList_ColumnWidthChanging);
            // 
            // clFileId
            // 
            this.clFileId.Text = "ファイル種類";
            this.clFileId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clFileId.Width = 90;
            // 
            // clFileKbn
            // 
            this.clFileKbn.Text = "ファイル種類名称";
            this.clFileKbn.Width = 400;
            // 
            // clStatus
            // 
            this.clStatus.Text = "状況";
            this.clStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clStatus.Width = 110;
            // 
            // clDateTime
            // 
            this.clDateTime.Text = "取込完了時刻";
            this.clDateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clDateTime.Width = 170;
            // 
            // clFileName
            // 
            this.clFileName.Text = "ファイル名";
            this.clFileName.Width = 400;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.chkNG);
            this.panel5.Controls.Add(this.chkHoryu);
            this.panel5.Controls.Add(this.chkNon);
            this.panel5.Controls.Add(this.chkOK);
            this.panel5.Location = new System.Drawing.Point(266, 65);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(500, 27);
            this.panel5.TabIndex = 100011;
            // 
            // chkNG
            // 
            this.chkNG.AutoSize = true;
            this.chkNG.Checked = true;
            this.chkNG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNG.Location = new System.Drawing.Point(235, 2);
            this.chkNG.Name = "chkNG";
            this.chkNG.Size = new System.Drawing.Size(66, 23);
            this.chkNG.TabIndex = 0;
            this.chkNG.Text = "異常";
            this.chkNG.UseVisualStyleBackColor = true;
            this.chkNG.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkHoryu
            // 
            this.chkHoryu.AutoSize = true;
            this.chkHoryu.Checked = true;
            this.chkHoryu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHoryu.Location = new System.Drawing.Point(163, 2);
            this.chkHoryu.Name = "chkHoryu";
            this.chkHoryu.Size = new System.Drawing.Size(66, 23);
            this.chkHoryu.TabIndex = 0;
            this.chkHoryu.Text = "保留";
            this.chkHoryu.UseVisualStyleBackColor = true;
            this.chkHoryu.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkNon
            // 
            this.chkNon.AutoSize = true;
            this.chkNon.Checked = true;
            this.chkNon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNon.Location = new System.Drawing.Point(6, 2);
            this.chkNon.Name = "chkNon";
            this.chkNon.Size = new System.Drawing.Size(85, 23);
            this.chkNon.TabIndex = 0;
            this.chkNon.Text = "未取込";
            this.chkNon.UseVisualStyleBackColor = true;
            this.chkNon.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkOK
            // 
            this.chkOK.AutoSize = true;
            this.chkOK.Checked = true;
            this.chkOK.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOK.Location = new System.Drawing.Point(91, 2);
            this.chkOK.Name = "chkOK";
            this.chkOK.Size = new System.Drawing.Size(66, 23);
            this.chkOK.TabIndex = 0;
            this.chkOK.Text = "正常";
            this.chkOK.UseVisualStyleBackColor = true;
            this.chkOK.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // cmbFile
            // 
            this.cmbFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFile.FormattingEnabled = true;
            this.cmbFile.Location = new System.Drawing.Point(266, 39);
            this.cmbFile.Name = "cmbFile";
            this.cmbFile.Size = new System.Drawing.Size(500, 27);
            this.cmbFile.TabIndex = 100010;
            this.cmbFile.SelectedIndexChanged += new System.EventHandler(this.cmbFile_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(117, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 27);
            this.label6.TabIndex = 100009;
            this.label6.Text = "ステータス";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(117, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(150, 27);
            this.label9.TabIndex = 100009;
            this.label9.Text = "ファイル種類名称";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(26, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "取込状況一覧";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(26, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "絞込条件";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTitle.Location = new System.Drawing.Point(7, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(89, 19);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "取込状況";
            // 
            // headerControl
            // 
            this.headerControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.headerControl.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.headerControl.Location = new System.Drawing.Point(5, 1);
            this.headerControl.Margin = new System.Windows.Forms.Padding(5);
            this.headerControl.Name = "headerControl";
            this.headerControl.Size = new System.Drawing.Size(1260, 40);
            this.headerControl.TabIndex = 100001;
            // 
            // ImportListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "ImportListForm";
            this.Load += new System.EventHandler(this.ImportListForm_Load);
            this.contentsPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
        private HeaderControl headerControl;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox chkHoryu;
        private System.Windows.Forms.CheckBox chkOK;
        private System.Windows.Forms.ComboBox cmbFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.CheckBox chkNG;
        private System.Windows.Forms.ListView lstImportList;
        private System.Windows.Forms.ColumnHeader clFileId;
        private System.Windows.Forms.ColumnHeader clFileKbn;
        private System.Windows.Forms.ColumnHeader clStatus;
        private System.Windows.Forms.ColumnHeader clFileName;
        private System.Windows.Forms.ColumnHeader clDateTime;
        private System.Windows.Forms.CheckBox chkNon;
    }
}
