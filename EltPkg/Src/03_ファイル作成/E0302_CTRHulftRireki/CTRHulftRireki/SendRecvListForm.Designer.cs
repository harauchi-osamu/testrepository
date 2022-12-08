namespace CTRHulftRireki
{
	partial class SendRecvListForm
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
            this.lstSendList = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.chkSendNG = new System.Windows.Forms.CheckBox();
            this.chkSendOK = new System.Windows.Forms.CheckBox();
            this.cmbSendFile = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lstRecvList = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel6 = new System.Windows.Forms.Panel();
            this.chkRecvNG = new System.Windows.Forms.CheckBox();
            this.chkRecvOK = new System.Windows.Forms.CheckBox();
            this.cmbRecvFile = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.headerControl = new CTRHulftRireki.HeaderControl();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.panel4);
            this.contentsPanel.Controls.Add(this.panel3);
            this.contentsPanel.Controls.Add(this.headerControl);
            this.contentsPanel.Location = new System.Drawing.Point(0, 57);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.headerControl, 0);
            this.contentsPanel.Controls.SetChildIndex(this.panel3, 0);
            this.contentsPanel.Controls.SetChildIndex(this.panel4, 0);
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
            // lstSendList
            // 
            this.lstSendList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14});
            this.lstSendList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lstSendList.FullRowSelect = true;
            this.lstSendList.GridLines = true;
            this.lstSendList.HideSelection = false;
            this.lstSendList.Location = new System.Drawing.Point(27, 130);
            this.lstSendList.MultiSelect = false;
            this.lstSendList.Name = "lstSendList";
            this.lstSendList.Size = new System.Drawing.Size(1201, 248);
            this.lstSendList.TabIndex = 5;
            this.lstSendList.TabStop = false;
            this.lstSendList.UseCompatibleStateImageBehavior = false;
            this.lstSendList.View = System.Windows.Forms.View.Details;
            this.lstSendList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lstSendList_ColumnWidthChanging);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "ファイル種類";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 90;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "ファイル種類名称";
            this.columnHeader7.Width = 260;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "開始日";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 100;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "開始時刻";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 80;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "終了時刻";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader10.Width = 80;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "レコード件数";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader11.Width = 95;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "データサイズ";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader12.Width = 90;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "ステータス";
            this.columnHeader13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader13.Width = 110;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "ファイル名";
            this.columnHeader14.Width = 270;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.cmbSendFile);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.lstSendList);
            this.panel3.Location = new System.Drawing.Point(5, 46);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1258, 390);
            this.panel3.TabIndex = 100001;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.chkSendNG);
            this.panel5.Controls.Add(this.chkSendOK);
            this.panel5.Location = new System.Drawing.Point(266, 65);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(500, 27);
            this.panel5.TabIndex = 100011;
            // 
            // chkSendNG
            // 
            this.chkSendNG.AutoSize = true;
            this.chkSendNG.Checked = true;
            this.chkSendNG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSendNG.Location = new System.Drawing.Point(84, 1);
            this.chkSendNG.Name = "chkSendNG";
            this.chkSendNG.Size = new System.Drawing.Size(66, 23);
            this.chkSendNG.TabIndex = 0;
            this.chkSendNG.Text = "異常";
            this.chkSendNG.UseVisualStyleBackColor = true;
            this.chkSendNG.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkSendOK
            // 
            this.chkSendOK.AutoSize = true;
            this.chkSendOK.Checked = true;
            this.chkSendOK.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSendOK.Location = new System.Drawing.Point(12, 1);
            this.chkSendOK.Name = "chkSendOK";
            this.chkSendOK.Size = new System.Drawing.Size(66, 23);
            this.chkSendOK.TabIndex = 0;
            this.chkSendOK.Text = "正常";
            this.chkSendOK.UseVisualStyleBackColor = true;
            this.chkSendOK.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // cmbSendFile
            // 
            this.cmbSendFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSendFile.FormattingEnabled = true;
            this.cmbSendFile.Location = new System.Drawing.Point(266, 39);
            this.cmbSendFile.Name = "cmbSendFile";
            this.cmbSendFile.Size = new System.Drawing.Size(500, 27);
            this.cmbSendFile.TabIndex = 100010;
            this.cmbSendFile.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedIndexChanged);
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
            this.label3.Text = "配信状況一覧";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "配信状況";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lstRecvList);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.cmbRecvFile);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Location = new System.Drawing.Point(5, 435);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1258, 390);
            this.panel4.TabIndex = 100002;
            // 
            // lstRecvList
            // 
            this.lstRecvList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader19});
            this.lstRecvList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lstRecvList.FullRowSelect = true;
            this.lstRecvList.GridLines = true;
            this.lstRecvList.HideSelection = false;
            this.lstRecvList.Location = new System.Drawing.Point(27, 130);
            this.lstRecvList.MultiSelect = false;
            this.lstRecvList.Name = "lstRecvList";
            this.lstRecvList.Size = new System.Drawing.Size(1201, 248);
            this.lstRecvList.TabIndex = 100022;
            this.lstRecvList.TabStop = false;
            this.lstRecvList.UseCompatibleStateImageBehavior = false;
            this.lstRecvList.View = System.Windows.Forms.View.Details;
            this.lstRecvList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lstRecvList_ColumnWidthChanging);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ファイル種類";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ファイル種類名称";
            this.columnHeader3.Width = 260;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "開始日";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "開始時刻";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 80;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "終了時刻";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader15.Width = 80;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "レコード件数";
            this.columnHeader16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader16.Width = 95;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "データサイズ";
            this.columnHeader17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader17.Width = 90;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "ステータス";
            this.columnHeader18.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader18.Width = 110;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "ファイル名";
            this.columnHeader19.Width = 270;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.chkRecvNG);
            this.panel6.Controls.Add(this.chkRecvOK);
            this.panel6.Location = new System.Drawing.Point(266, 65);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(500, 27);
            this.panel6.TabIndex = 100020;
            // 
            // chkRecvNG
            // 
            this.chkRecvNG.AutoSize = true;
            this.chkRecvNG.Checked = true;
            this.chkRecvNG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRecvNG.Location = new System.Drawing.Point(84, 1);
            this.chkRecvNG.Name = "chkRecvNG";
            this.chkRecvNG.Size = new System.Drawing.Size(66, 23);
            this.chkRecvNG.TabIndex = 0;
            this.chkRecvNG.Text = "異常";
            this.chkRecvNG.UseVisualStyleBackColor = true;
            this.chkRecvNG.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // chkRecvOK
            // 
            this.chkRecvOK.AutoSize = true;
            this.chkRecvOK.Checked = true;
            this.chkRecvOK.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRecvOK.Location = new System.Drawing.Point(12, 1);
            this.chkRecvOK.Name = "chkRecvOK";
            this.chkRecvOK.Size = new System.Drawing.Size(66, 23);
            this.chkRecvOK.TabIndex = 0;
            this.chkRecvOK.Text = "正常";
            this.chkRecvOK.UseVisualStyleBackColor = true;
            this.chkRecvOK.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // cmbRecvFile
            // 
            this.cmbRecvFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecvFile.FormattingEnabled = true;
            this.cmbRecvFile.Location = new System.Drawing.Point(266, 39);
            this.cmbRecvFile.Name = "cmbRecvFile";
            this.cmbRecvFile.Size = new System.Drawing.Size(500, 27);
            this.cmbRecvFile.TabIndex = 100019;
            this.cmbRecvFile.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Location = new System.Drawing.Point(117, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 27);
            this.label7.TabIndex = 100017;
            this.label7.Text = "ステータス";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(117, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 27);
            this.label8.TabIndex = 100018;
            this.label8.Text = "ファイル種類名称";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(26, 105);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 19);
            this.label10.TabIndex = 100014;
            this.label10.Text = "集信状況一覧";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(26, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 19);
            this.label11.TabIndex = 100015;
            this.label11.Text = "絞込条件";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.Location = new System.Drawing.Point(7, 8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 19);
            this.label12.TabIndex = 100016;
            this.label12.Text = "集信状況";
            // 
            // headerControl
            // 
            this.headerControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.headerControl.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.headerControl.Location = new System.Drawing.Point(5, 1);
            this.headerControl.Margin = new System.Windows.Forms.Padding(5);
            this.headerControl.Name = "headerControl";
            this.headerControl.Size = new System.Drawing.Size(1260, 40);
            this.headerControl.TabIndex = 100000;
            // 
            // SendRecvListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SendRecvListForm";
            this.Load += new System.EventHandler(this.SendRecvListForm_Load);
            this.contentsPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lstSendList;
        private HeaderControl headerControl;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbSendFile;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox chkSendNG;
        private System.Windows.Forms.CheckBox chkSendOK;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.CheckBox chkRecvNG;
        private System.Windows.Forms.CheckBox chkRecvOK;
        private System.Windows.Forms.ComboBox cmbRecvFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ListView lstRecvList;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ColumnHeader columnHeader19;
    }
}
