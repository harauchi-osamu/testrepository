namespace StartDayOperation
{
	partial class StartDayOperation
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
            this.lblRecvDate = new System.Windows.Forms.Label();
            this.dtUpdateGymDate = new CommonClass.DTextBox2();
            this.lblUpdateGymDate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblGymDate = new System.Windows.Forms.Label();
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.contentsPanel.Controls.Add(this.lblGymDate);
            this.contentsPanel.Controls.Add(this.label3);
            this.contentsPanel.Controls.Add(this.dtUpdateGymDate);
            this.contentsPanel.Controls.Add(this.lblUpdateGymDate);
            this.contentsPanel.Controls.Add(this.lblRecvDate);
            this.contentsPanel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.contentsPanel.Size = new System.Drawing.Size(1266, 844);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblRecvDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblUpdateGymDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtUpdateGymDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label3, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblGymDate, 0);
            // 
            // lblRecvDate
            // 
            this.lblRecvDate.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblRecvDate.Location = new System.Drawing.Point(484, 354);
            this.lblRecvDate.Name = "lblRecvDate";
            this.lblRecvDate.Size = new System.Drawing.Size(141, 28);
            this.lblRecvDate.TabIndex = 100065;
            this.lblRecvDate.Text = "現在の処理日";
            this.lblRecvDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtUpdateGymDate
            // 
            this.dtUpdateGymDate.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtUpdateGymDate.KeyControl = true;
            this.dtUpdateGymDate.Location = new System.Drawing.Point(656, 460);
            this.dtUpdateGymDate.MaxLength = 8;
            this.dtUpdateGymDate.Name = "dtUpdateGymDate";
            this.dtUpdateGymDate.OnEnterSeparatorCut = true;
            this.dtUpdateGymDate.Size = new System.Drawing.Size(115, 28);
            this.dtUpdateGymDate.TabIndex = 1;
            this.dtUpdateGymDate.TabKeyControl = true;
            this.dtUpdateGymDate.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtUpdateGymDate.Enter += new System.EventHandler(this.txt_Enter);
            this.dtUpdateGymDate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblUpdateGymDate
            // 
            this.lblUpdateGymDate.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblUpdateGymDate.Location = new System.Drawing.Point(484, 460);
            this.lblUpdateGymDate.Name = "lblUpdateGymDate";
            this.lblUpdateGymDate.Size = new System.Drawing.Size(141, 28);
            this.lblUpdateGymDate.TabIndex = 100067;
            this.lblUpdateGymDate.Text = "処理日更新";
            this.lblUpdateGymDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(545, 403);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(256, 43);
            this.label3.TabIndex = 100068;
            this.label3.Text = "↓";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGymDate
            // 
            this.lblGymDate.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblGymDate.Location = new System.Drawing.Point(656, 354);
            this.lblGymDate.Name = "lblGymDate";
            this.lblGymDate.Size = new System.Drawing.Size(115, 28);
            this.lblGymDate.TabIndex = 100069;
            this.lblGymDate.Text = "yyyy.MM.dd";
            this.lblGymDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StartDayOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "StartDayOperation";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
        private System.Windows.Forms.Label lblRecvDate;
        private CommonClass.DTextBox2 dtUpdateGymDate;
        private System.Windows.Forms.Label lblUpdateGymDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblGymDate;
    }
}
