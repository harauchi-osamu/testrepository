namespace CTRHulftRireki
{
	partial class DetailForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.headerControl = new CTRHulftRireki.HeaderControl();
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.txtLog);
            this.contentsPanel.Controls.Add(this.lblTitle);
            this.contentsPanel.Controls.Add(this.headerControl);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.headerControl, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblTitle, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtLog, 0);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTitle.Location = new System.Drawing.Point(13, 55);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(86, 19);
            this.lblTitle.TabIndex = 100011;
            this.lblTitle.Text = "エラーログ";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.txtLog.Location = new System.Drawing.Point(33, 93);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(1201, 710);
            this.txtLog.TabIndex = 100012;
            this.txtLog.TabStop = false;
            // 
            // headerControl
            // 
            this.headerControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.headerControl.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.headerControl.Location = new System.Drawing.Point(5, 1);
            this.headerControl.Margin = new System.Windows.Forms.Padding(5);
            this.headerControl.Name = "headerControl";
            this.headerControl.Size = new System.Drawing.Size(1260, 40);
            this.headerControl.TabIndex = 100002;
            // 
            // DetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "DetailForm";
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

        #endregion

        private HeaderControl headerControl;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtLog;
    }
}
