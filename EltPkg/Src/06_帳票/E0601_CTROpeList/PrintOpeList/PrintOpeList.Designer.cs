namespace PrintOpeList
{
	partial class PrintOpeList
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
            this.txtInputDATEFrom = new CommonClass.DTextBox2();
            this.txtInputDATETo = new CommonClass.DTextBox2();
            this.label1 = new System.Windows.Forms.Label();
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.Add(this.txtInputDATETo);
            this.contentsPanel.Controls.Add(this.txtInputDATEFrom);
            this.contentsPanel.Controls.Add(this.lblCLEARING_DATE);
            this.contentsPanel.Controls.Add(this.lblInputDATE);
            this.contentsPanel.Controls.SetChildIndex(this.lblInputDATE, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblCLEARING_DATE, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtInputDATEFrom, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtInputDATETo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            // 
            // lblInputDATE
            // 
            this.lblInputDATE.AutoSize = true;
            this.lblInputDATE.Location = new System.Drawing.Point(387, 368);
            this.lblInputDATE.Name = "lblInputDATE";
            this.lblInputDATE.Size = new System.Drawing.Size(66, 19);
            this.lblInputDATE.TabIndex = 4;
            this.lblInputDATE.Text = "基準日";
            // 
            // lblCLEARING_DATE
            // 
            this.lblCLEARING_DATE.AutoSize = true;
            this.lblCLEARING_DATE.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCLEARING_DATE.Location = new System.Drawing.Point(352, 334);
            this.lblCLEARING_DATE.Name = "lblCLEARING_DATE";
            this.lblCLEARING_DATE.Size = new System.Drawing.Size(89, 19);
            this.lblCLEARING_DATE.TabIndex = 5;
            this.lblCLEARING_DATE.Text = "検索条件";
            // 
            // txtInputDATEFrom
            // 
            this.txtInputDATEFrom.KeyControl = true;
            this.txtInputDATEFrom.Location = new System.Drawing.Point(496, 365);
            this.txtInputDATEFrom.MaxLength = 8;
            this.txtInputDATEFrom.Name = "txtInputDATEFrom";
            this.txtInputDATEFrom.OnEnterSeparatorCut = true;
            this.txtInputDATEFrom.Size = new System.Drawing.Size(153, 26);
            this.txtInputDATEFrom.TabIndex = 4;
            this.txtInputDATEFrom.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.txtInputDATEFrom.Enter += new System.EventHandler(this.txt_Enter);
            this.txtInputDATEFrom.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // txtInputDATETo
            // 
            this.txtInputDATETo.KeyControl = true;
            this.txtInputDATETo.Location = new System.Drawing.Point(694, 365);
            this.txtInputDATETo.MaxLength = 8;
            this.txtInputDATETo.Name = "txtInputDATETo";
            this.txtInputDATETo.OnEnterSeparatorCut = true;
            this.txtInputDATETo.Size = new System.Drawing.Size(153, 26);
            this.txtInputDATETo.TabIndex = 100000;
            this.txtInputDATETo.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.txtInputDATETo.Enter += new System.EventHandler(this.txt_Enter);
            this.txtInputDATETo.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(657, 368);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 19);
            this.label1.TabIndex = 100001;
            this.label1.Text = "～";
            // 
            // PrintOpeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "PrintOpeList";
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
        private CommonClass.DTextBox2 txtInputDATEFrom;
        private System.Windows.Forms.Label label1;
        private CommonClass.DTextBox2 txtInputDATETo;
    }
}
