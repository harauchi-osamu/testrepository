namespace CTROcImgArcMk
{
	partial class MeisaiListForm
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
            this.lblClearinte = new System.Windows.Forms.Label();
            this.txtClearinte = new CommonClass.DTextBox2();
            this.txtGymId = new CommonClass.NumTextBox2();
            this.lblGymId = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lvBatList = new System.Windows.Forms.ListView();
            this.clClearingDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDetailCnt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clImageCnt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtImageFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblYoteiMeiCnt = new System.Windows.Forms.Label();
            this.lblYoteiImgCnt = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblResEltMeiCnt = new System.Windows.Forms.Label();
            this.lblResEltImgCnt = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblResInnerMeiCnt = new System.Windows.Forms.Label();
            this.lblResInnerImgCnt = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblResTotalMeiCnt = new System.Windows.Forms.Label();
            this.lblResTotalImgCnt = new System.Windows.Forms.Label();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.lblResTotalImgCnt);
            this.contentsPanel.Controls.Add(this.lblResInnerImgCnt);
            this.contentsPanel.Controls.Add(this.lblResEltImgCnt);
            this.contentsPanel.Controls.Add(this.lblYoteiImgCnt);
            this.contentsPanel.Controls.Add(this.lblResTotalMeiCnt);
            this.contentsPanel.Controls.Add(this.lblResInnerMeiCnt);
            this.contentsPanel.Controls.Add(this.lblResEltMeiCnt);
            this.contentsPanel.Controls.Add(this.lblYoteiMeiCnt);
            this.contentsPanel.Controls.Add(this.label16);
            this.contentsPanel.Controls.Add(this.label13);
            this.contentsPanel.Controls.Add(this.label9);
            this.contentsPanel.Controls.Add(this.label5);
            this.contentsPanel.Controls.Add(this.txtImageFile);
            this.contentsPanel.Controls.Add(this.lvBatList);
            this.contentsPanel.Controls.Add(this.groupBox1);
            this.contentsPanel.Controls.Add(this.label3);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.Add(this.label6);
            this.contentsPanel.Controls.SetChildIndex(this.label6, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label3, 0);
            this.contentsPanel.Controls.SetChildIndex(this.groupBox1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lvBatList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtImageFile, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label5, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label9, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label13, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label16, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblYoteiMeiCnt, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblResEltMeiCnt, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblResInnerMeiCnt, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblResTotalMeiCnt, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblYoteiImgCnt, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblResEltImgCnt, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblResInnerImgCnt, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblResTotalImgCnt, 0);
            // 
            // clKey
            // 
            clKey.Text = "clKey";
            clKey.Width = 0;
            // 
            // lblClearinte
            // 
            this.lblClearinte.AutoSize = true;
            this.lblClearinte.Location = new System.Drawing.Point(6, 28);
            this.lblClearinte.Name = "lblClearinte";
            this.lblClearinte.Size = new System.Drawing.Size(104, 19);
            this.lblClearinte.TabIndex = 100001;
            this.lblClearinte.Text = "交換希望日";
            // 
            // txtClearinte
            // 
            this.txtClearinte.KeyControl = true;
            this.txtClearinte.Location = new System.Drawing.Point(116, 25);
            this.txtClearinte.MaxLength = 8;
            this.txtClearinte.Name = "txtClearinte";
            this.txtClearinte.OnEnterSeparatorCut = true;
            this.txtClearinte.Size = new System.Drawing.Size(100, 26);
            this.txtClearinte.TabIndex = 1;
            this.txtClearinte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClearinte.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            // 
            // txtGymId
            // 
            this.txtGymId.KeyControl = true;
            this.txtGymId.Location = new System.Drawing.Point(294, 25);
            this.txtGymId.MaxLength = 1;
            this.txtGymId.Name = "txtGymId";
            this.txtGymId.Size = new System.Drawing.Size(40, 26);
            this.txtGymId.TabIndex = 2;
            this.txtGymId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtGymId.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.txtGymId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // lblGymId
            // 
            this.lblGymId.AutoSize = true;
            this.lblGymId.Location = new System.Drawing.Point(241, 28);
            this.lblGymId.Name = "lblGymId";
            this.lblGymId.Size = new System.Drawing.Size(47, 19);
            this.lblGymId.TabIndex = 100001;
            this.lblGymId.Text = "業務";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(340, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(363, 19);
            this.label7.TabIndex = 100001;
            this.label7.Text = "（空欄：指定なし　1：交換持出　2：期日管理）";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblClearinte);
            this.groupBox1.Controls.Add(this.txtGymId);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtClearinte);
            this.groupBox1.Controls.Add(this.lblGymId);
            this.groupBox1.Location = new System.Drawing.Point(285, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(708, 63);
            this.groupBox1.TabIndex = 100002;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "絞り込み条件";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(296, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 19);
            this.label6.TabIndex = 100001;
            this.label6.Text = "（予定件数）";
            // 
            // lvBatList
            // 
            this.lvBatList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.clClearingDate,
            this.clDetailCnt,
            this.clImageCnt});
            this.lvBatList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvBatList.FullRowSelect = true;
            this.lvBatList.GridLines = true;
            this.lvBatList.HideSelection = false;
            this.lvBatList.Location = new System.Drawing.Point(295, 155);
            this.lvBatList.MultiSelect = false;
            this.lvBatList.Name = "lvBatList";
            this.lvBatList.Size = new System.Drawing.Size(481, 168);
            this.lvBatList.TabIndex = 100003;
            this.lvBatList.TabStop = false;
            this.lvBatList.UseCompatibleStateImageBehavior = false;
            this.lvBatList.View = System.Windows.Forms.View.Details;
            this.lvBatList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvBatList_ColumnWidthChanging);
            // 
            // clClearingDate
            // 
            this.clClearingDate.Text = "交換希望日";
            this.clClearingDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clClearingDate.Width = 155;
            // 
            // clDetailCnt
            // 
            this.clDetailCnt.Text = "明細数";
            this.clDetailCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clDetailCnt.Width = 150;
            // 
            // clImageCnt
            // 
            this.clImageCnt.Text = "イメージファイル数";
            this.clImageCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clImageCnt.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(296, 375);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 19);
            this.label1.TabIndex = 100001;
            this.label1.Text = "（結果件数）";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(296, 514);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 19);
            this.label3.TabIndex = 100001;
            this.label3.Text = "（作成ファイル）";
            // 
            // txtImageFile
            // 
            this.txtImageFile.Location = new System.Drawing.Point(295, 539);
            this.txtImageFile.Multiline = true;
            this.txtImageFile.Name = "txtImageFile";
            this.txtImageFile.ReadOnly = true;
            this.txtImageFile.Size = new System.Drawing.Size(459, 178);
            this.txtImageFile.TabIndex = 100004;
            this.txtImageFile.TabStop = false;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(295, 322);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 26);
            this.label5.TabIndex = 100005;
            this.label5.Text = "合計";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblYoteiMeiCnt
            // 
            this.lblYoteiMeiCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblYoteiMeiCnt.Location = new System.Drawing.Point(452, 322);
            this.lblYoteiMeiCnt.Name = "lblYoteiMeiCnt";
            this.lblYoteiMeiCnt.Size = new System.Drawing.Size(150, 26);
            this.lblYoteiMeiCnt.TabIndex = 100005;
            this.lblYoteiMeiCnt.Text = "0";
            this.lblYoteiMeiCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblYoteiImgCnt
            // 
            this.lblYoteiImgCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblYoteiImgCnt.Location = new System.Drawing.Point(601, 322);
            this.lblYoteiImgCnt.Name = "lblYoteiImgCnt";
            this.lblYoteiImgCnt.Size = new System.Drawing.Size(153, 26);
            this.lblYoteiImgCnt.TabIndex = 100005;
            this.lblYoteiImgCnt.Text = "0";
            this.lblYoteiImgCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(295, 400);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(158, 26);
            this.label9.TabIndex = 100005;
            this.label9.Text = "電子交換所連携";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblResEltMeiCnt
            // 
            this.lblResEltMeiCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResEltMeiCnt.Location = new System.Drawing.Point(452, 400);
            this.lblResEltMeiCnt.Name = "lblResEltMeiCnt";
            this.lblResEltMeiCnt.Size = new System.Drawing.Size(150, 26);
            this.lblResEltMeiCnt.TabIndex = 100005;
            this.lblResEltMeiCnt.Text = "0";
            this.lblResEltMeiCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblResEltImgCnt
            // 
            this.lblResEltImgCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResEltImgCnt.Location = new System.Drawing.Point(601, 400);
            this.lblResEltImgCnt.Name = "lblResEltImgCnt";
            this.lblResEltImgCnt.Size = new System.Drawing.Size(153, 26);
            this.lblResEltImgCnt.TabIndex = 100005;
            this.lblResEltImgCnt.Text = "0";
            this.lblResEltImgCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Location = new System.Drawing.Point(295, 425);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(158, 26);
            this.label13.TabIndex = 100005;
            this.label13.Text = "行内連携";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblResInnerMeiCnt
            // 
            this.lblResInnerMeiCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResInnerMeiCnt.Location = new System.Drawing.Point(452, 425);
            this.lblResInnerMeiCnt.Name = "lblResInnerMeiCnt";
            this.lblResInnerMeiCnt.Size = new System.Drawing.Size(150, 26);
            this.lblResInnerMeiCnt.TabIndex = 100005;
            this.lblResInnerMeiCnt.Text = "0";
            this.lblResInnerMeiCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblResInnerImgCnt
            // 
            this.lblResInnerImgCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResInnerImgCnt.Location = new System.Drawing.Point(601, 425);
            this.lblResInnerImgCnt.Name = "lblResInnerImgCnt";
            this.lblResInnerImgCnt.Size = new System.Drawing.Size(153, 26);
            this.lblResInnerImgCnt.TabIndex = 100005;
            this.lblResInnerImgCnt.Text = "0";
            this.lblResInnerImgCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Location = new System.Drawing.Point(295, 450);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(158, 26);
            this.label16.TabIndex = 100005;
            this.label16.Text = "合計";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblResTotalMeiCnt
            // 
            this.lblResTotalMeiCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResTotalMeiCnt.Location = new System.Drawing.Point(452, 450);
            this.lblResTotalMeiCnt.Name = "lblResTotalMeiCnt";
            this.lblResTotalMeiCnt.Size = new System.Drawing.Size(150, 26);
            this.lblResTotalMeiCnt.TabIndex = 100005;
            this.lblResTotalMeiCnt.Text = "0";
            this.lblResTotalMeiCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblResTotalImgCnt
            // 
            this.lblResTotalImgCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResTotalImgCnt.Location = new System.Drawing.Point(601, 450);
            this.lblResTotalImgCnt.Name = "lblResTotalImgCnt";
            this.lblResTotalImgCnt.Size = new System.Drawing.Size(153, 26);
            this.lblResTotalImgCnt.TabIndex = 100005;
            this.lblResTotalImgCnt.Text = "0";
            this.lblResTotalImgCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MeisaiListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "MeisaiListForm";
            this.Shown += new System.EventHandler(this.MeisaiListForm_Shown);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

        #endregion
        private CommonClass.NumTextBox2 txtGymId;
        private CommonClass.DTextBox2 txtClearinte;
        private System.Windows.Forms.Label lblGymId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblClearinte;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblYoteiMeiCnt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtImageFile;
        private System.Windows.Forms.ListView lvBatList;
        private System.Windows.Forms.ColumnHeader clClearingDate;
        private System.Windows.Forms.ColumnHeader clDetailCnt;
        private System.Windows.Forms.ColumnHeader clImageCnt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblYoteiImgCnt;
        private System.Windows.Forms.Label lblResInnerImgCnt;
        private System.Windows.Forms.Label lblResEltImgCnt;
        private System.Windows.Forms.Label lblResInnerMeiCnt;
        private System.Windows.Forms.Label lblResEltMeiCnt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblResTotalImgCnt;
        private System.Windows.Forms.Label lblResTotalMeiCnt;
        private System.Windows.Forms.Label label16;
    }
}
