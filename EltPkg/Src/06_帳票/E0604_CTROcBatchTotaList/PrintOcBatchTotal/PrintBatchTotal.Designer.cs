namespace PrintOcBatchTotal
{
	partial class PrintBatchTotal
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
            this.lblInputDATE = new System.Windows.Forms.Label();
            this.lblCLEARING_DATE = new System.Windows.Forms.Label();
            this.txtInputDATE = new CommonClass.DTextBox2();
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.contentsPanel.Controls.Add(this.txtInputDATE);
            this.contentsPanel.Controls.Add(this.lblCLEARING_DATE);
            this.contentsPanel.Controls.Add(this.lblInputDATE);
            this.contentsPanel.Controls.SetChildIndex(this.lblInputDATE, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblCLEARING_DATE, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtInputDATE, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            // 
            // lblInputDATE
            // 
            this.lblInputDATE.AutoSize = true;
            this.lblInputDATE.Location = new System.Drawing.Point(559, 369);
            this.lblInputDATE.Name = "lblInputDATE";
            this.lblInputDATE.Size = new System.Drawing.Size(66, 19);
            this.lblInputDATE.TabIndex = 4;
            this.lblInputDATE.Text = "取込日";
            // 
            // lblCLEARING_DATE
            // 
            this.lblCLEARING_DATE.AutoSize = true;
            this.lblCLEARING_DATE.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCLEARING_DATE.Location = new System.Drawing.Point(444, 369);
            this.lblCLEARING_DATE.Name = "lblCLEARING_DATE";
            this.lblCLEARING_DATE.Size = new System.Drawing.Size(89, 19);
            this.lblCLEARING_DATE.TabIndex = 5;
            this.lblCLEARING_DATE.Text = "検索条件";
            // 
            // txtInputDATE
            // 
            this.txtInputDATE.KeyControl = true;
            this.txtInputDATE.Location = new System.Drawing.Point(668, 366);
            this.txtInputDATE.MaxLength = 8;
            this.txtInputDATE.Name = "txtInputDATE";
            this.txtInputDATE.OnEnterSeparatorCut = true;
            this.txtInputDATE.Size = new System.Drawing.Size(153, 26);
            this.txtInputDATE.TabIndex = 4;
            this.txtInputDATE.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.txtInputDATE.Enter += new System.EventHandler(this.txt_Enter);
            this.txtInputDATE.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // PrintBatchTotal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "PrintBatchTotal";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
        private System.Windows.Forms.Label lblCLEARING_DATE;
        private System.Windows.Forms.Label lblInputDATE;
        private CommonClass.DTextBox2 txtInputDATE;
    }
}
