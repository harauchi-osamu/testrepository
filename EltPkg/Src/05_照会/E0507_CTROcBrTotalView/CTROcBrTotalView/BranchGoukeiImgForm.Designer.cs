namespace CTROcBrTotalView
{
	partial class BranchGoukeiImgForm
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
            this.txtSCAN_BR_NO = new CommonClass.NumTextBox2();
            this.txtOC_BR_NO = new CommonClass.NumTextBox2();
            this.txtOC_BK_NO = new CommonClass.NumTextBox2();
            this.txtSCAN_DATE = new CommonClass.DTextBox2();
            this.txtFix = new CommonClass.NumTextBox2();
            this.txtTOTAL_AMOUNT = new CommonClass.NumTextBox2();
            this.txtTOTAL_COUNT = new CommonClass.NumTextBox2();
            this.lblOC_BK_NO = new System.Windows.Forms.Label();
            this.lblOC_BR_NO = new System.Windows.Forms.Label();
            this.lblSCAN_BR_NO = new System.Windows.Forms.Label();
            this.lblSCAN_DATE = new System.Windows.Forms.Label();
            this.lblTOTAL_COUNT = new System.Windows.Forms.Label();
            this.lblTOTAL_AMOUNT = new System.Windows.Forms.Label();
            this.lblOC_BK_NM = new System.Windows.Forms.Label();
            this.lblOC_BR_NM = new System.Windows.Forms.Label();
            this.lblSCAN_BR_NM = new System.Windows.Forms.Label();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.pnlImage);
            this.contentsPanel.Controls.Add(this.lblSCAN_BR_NM);
            this.contentsPanel.Controls.Add(this.lblOC_BR_NM);
            this.contentsPanel.Controls.Add(this.lblOC_BK_NM);
            this.contentsPanel.Controls.Add(this.lblSCAN_BR_NO);
            this.contentsPanel.Controls.Add(this.lblOC_BR_NO);
            this.contentsPanel.Controls.Add(this.lblTOTAL_AMOUNT);
            this.contentsPanel.Controls.Add(this.lblTOTAL_COUNT);
            this.contentsPanel.Controls.Add(this.lblSCAN_DATE);
            this.contentsPanel.Controls.Add(this.lblOC_BK_NO);
            this.contentsPanel.Controls.Add(this.txtFix);
            this.contentsPanel.Controls.Add(this.txtTOTAL_AMOUNT);
            this.contentsPanel.Controls.Add(this.txtTOTAL_COUNT);
            this.contentsPanel.Controls.Add(this.txtSCAN_DATE);
            this.contentsPanel.Controls.Add(this.txtSCAN_BR_NO);
            this.contentsPanel.Controls.Add(this.txtOC_BR_NO);
            this.contentsPanel.Controls.Add(this.txtOC_BK_NO);
            this.contentsPanel.Controls.SetChildIndex(this.txtOC_BK_NO, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtOC_BR_NO, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtSCAN_BR_NO, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtSCAN_DATE, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtTOTAL_COUNT, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtTOTAL_AMOUNT, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtFix, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblOC_BK_NO, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblSCAN_DATE, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblTOTAL_COUNT, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblTOTAL_AMOUNT, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblOC_BR_NO, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblSCAN_BR_NO, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblOC_BK_NM, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblOC_BR_NM, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblSCAN_BR_NM, 0);
            this.contentsPanel.Controls.SetChildIndex(this.pnlImage, 0);
            // 
            // txtSCAN_BR_NO
            // 
            this.txtSCAN_BR_NO.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSCAN_BR_NO.KeyControl = true;
            this.txtSCAN_BR_NO.LeftZero = true;
            this.txtSCAN_BR_NO.Location = new System.Drawing.Point(176, 763);
            this.txtSCAN_BR_NO.MaxLength = 4;
            this.txtSCAN_BR_NO.Name = "txtSCAN_BR_NO";
            this.txtSCAN_BR_NO.Size = new System.Drawing.Size(100, 23);
            this.txtSCAN_BR_NO.TabIndex = 3;
            this.txtSCAN_BR_NO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSCAN_BR_NO.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            // 
            // txtOC_BR_NO
            // 
            this.txtOC_BR_NO.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtOC_BR_NO.KeyControl = true;
            this.txtOC_BR_NO.LeftZero = true;
            this.txtOC_BR_NO.Location = new System.Drawing.Point(176, 725);
            this.txtOC_BR_NO.MaxLength = 4;
            this.txtOC_BR_NO.Name = "txtOC_BR_NO";
            this.txtOC_BR_NO.Size = new System.Drawing.Size(100, 23);
            this.txtOC_BR_NO.TabIndex = 2;
            this.txtOC_BR_NO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOC_BR_NO.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            // 
            // txtOC_BK_NO
            // 
            this.txtOC_BK_NO.BackColor = System.Drawing.SystemColors.Window;
            this.txtOC_BK_NO.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtOC_BK_NO.KeyControl = true;
            this.txtOC_BK_NO.LeftZero = true;
            this.txtOC_BK_NO.Location = new System.Drawing.Point(176, 688);
            this.txtOC_BK_NO.MaxLength = 4;
            this.txtOC_BK_NO.Name = "txtOC_BK_NO";
            this.txtOC_BK_NO.ReadOnly = true;
            this.txtOC_BK_NO.Size = new System.Drawing.Size(100, 23);
            this.txtOC_BK_NO.TabIndex = 1;
            this.txtOC_BK_NO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOC_BK_NO.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            this.txtOC_BK_NO.Enter += new System.EventHandler(this.txt_Enter);
            this.txtOC_BK_NO.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // txtSCAN_DATE
            // 
            this.txtSCAN_DATE.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSCAN_DATE.KeyControl = true;
            this.txtSCAN_DATE.Location = new System.Drawing.Point(599, 688);
            this.txtSCAN_DATE.MaxLength = 8;
            this.txtSCAN_DATE.Name = "txtSCAN_DATE";
            this.txtSCAN_DATE.OnEnterSeparatorCut = true;
            this.txtSCAN_DATE.Size = new System.Drawing.Size(120, 23);
            this.txtSCAN_DATE.TabIndex = 4;
            this.txtSCAN_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSCAN_DATE.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            this.txtSCAN_DATE.Enter += new System.EventHandler(this.txt_Enter);
            this.txtSCAN_DATE.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // txtFix
            // 
            this.txtFix.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtFix.Location = new System.Drawing.Point(813, 762);
            this.txtFix.MaxLength = 1;
            this.txtFix.Name = "txtFix";
            this.txtFix.RaiseIValidating = false;
            this.txtFix.Size = new System.Drawing.Size(20, 23);
            this.txtFix.TabIndex = 7;
            this.txtFix.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFix.Enter += new System.EventHandler(this.txt_Enter);
            this.txtFix.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // txtTOTAL_AMOUNT
            // 
            this.txtTOTAL_AMOUNT.Comma = true;
            this.txtTOTAL_AMOUNT.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTOTAL_AMOUNT.KeyControl = true;
            this.txtTOTAL_AMOUNT.Location = new System.Drawing.Point(599, 763);
            this.txtTOTAL_AMOUNT.MaxLength = 15;
            this.txtTOTAL_AMOUNT.Name = "txtTOTAL_AMOUNT";
            this.txtTOTAL_AMOUNT.OnEnterSeparatorCut = true;
            this.txtTOTAL_AMOUNT.Size = new System.Drawing.Size(172, 23);
            this.txtTOTAL_AMOUNT.TabIndex = 6;
            this.txtTOTAL_AMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTOTAL_AMOUNT.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            this.txtTOTAL_AMOUNT.Enter += new System.EventHandler(this.txt_Enter);
            this.txtTOTAL_AMOUNT.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // txtTOTAL_COUNT
            // 
            this.txtTOTAL_COUNT.Comma = true;
            this.txtTOTAL_COUNT.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTOTAL_COUNT.KeyControl = true;
            this.txtTOTAL_COUNT.Location = new System.Drawing.Point(599, 725);
            this.txtTOTAL_COUNT.MaxLength = 6;
            this.txtTOTAL_COUNT.Name = "txtTOTAL_COUNT";
            this.txtTOTAL_COUNT.OnEnterSeparatorCut = true;
            this.txtTOTAL_COUNT.Size = new System.Drawing.Size(120, 23);
            this.txtTOTAL_COUNT.TabIndex = 5;
            this.txtTOTAL_COUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTOTAL_COUNT.I_Validating += new System.ComponentModel.CancelEventHandler(this.root_I_Validating);
            // 
            // lblOC_BK_NO
            // 
            this.lblOC_BK_NO.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOC_BK_NO.Location = new System.Drawing.Point(77, 688);
            this.lblOC_BK_NO.Name = "lblOC_BK_NO";
            this.lblOC_BK_NO.Size = new System.Drawing.Size(95, 23);
            this.lblOC_BK_NO.TabIndex = 100014;
            this.lblOC_BK_NO.Text = "持出銀行";
            this.lblOC_BK_NO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOC_BR_NO
            // 
            this.lblOC_BR_NO.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOC_BR_NO.Location = new System.Drawing.Point(77, 725);
            this.lblOC_BR_NO.Name = "lblOC_BR_NO";
            this.lblOC_BR_NO.Size = new System.Drawing.Size(95, 23);
            this.lblOC_BR_NO.TabIndex = 100014;
            this.lblOC_BR_NO.Text = "持出支店";
            this.lblOC_BR_NO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSCAN_BR_NO
            // 
            this.lblSCAN_BR_NO.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSCAN_BR_NO.Location = new System.Drawing.Point(77, 763);
            this.lblSCAN_BR_NO.Name = "lblSCAN_BR_NO";
            this.lblSCAN_BR_NO.Size = new System.Drawing.Size(95, 23);
            this.lblSCAN_BR_NO.TabIndex = 100014;
            this.lblSCAN_BR_NO.Text = "スキャン支店";
            this.lblSCAN_BR_NO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSCAN_DATE
            // 
            this.lblSCAN_DATE.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSCAN_DATE.Location = new System.Drawing.Point(500, 688);
            this.lblSCAN_DATE.Name = "lblSCAN_DATE";
            this.lblSCAN_DATE.Size = new System.Drawing.Size(92, 23);
            this.lblSCAN_DATE.TabIndex = 100014;
            this.lblSCAN_DATE.Text = "スキャン日";
            this.lblSCAN_DATE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTOTAL_COUNT
            // 
            this.lblTOTAL_COUNT.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTOTAL_COUNT.Location = new System.Drawing.Point(500, 725);
            this.lblTOTAL_COUNT.Name = "lblTOTAL_COUNT";
            this.lblTOTAL_COUNT.Size = new System.Drawing.Size(74, 23);
            this.lblTOTAL_COUNT.TabIndex = 100014;
            this.lblTOTAL_COUNT.Text = "総枚数";
            this.lblTOTAL_COUNT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTOTAL_AMOUNT
            // 
            this.lblTOTAL_AMOUNT.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTOTAL_AMOUNT.Location = new System.Drawing.Point(500, 763);
            this.lblTOTAL_AMOUNT.Name = "lblTOTAL_AMOUNT";
            this.lblTOTAL_AMOUNT.Size = new System.Drawing.Size(92, 23);
            this.lblTOTAL_AMOUNT.TabIndex = 100014;
            this.lblTOTAL_AMOUNT.Text = "総金額";
            this.lblTOTAL_AMOUNT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOC_BK_NM
            // 
            this.lblOC_BK_NM.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOC_BK_NM.Location = new System.Drawing.Point(285, 691);
            this.lblOC_BK_NM.Name = "lblOC_BK_NM";
            this.lblOC_BK_NM.Size = new System.Drawing.Size(190, 19);
            this.lblOC_BK_NM.TabIndex = 100015;
            this.lblOC_BK_NM.Text = "XXXX";
            // 
            // lblOC_BR_NM
            // 
            this.lblOC_BR_NM.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOC_BR_NM.Location = new System.Drawing.Point(285, 728);
            this.lblOC_BR_NM.Name = "lblOC_BR_NM";
            this.lblOC_BR_NM.Size = new System.Drawing.Size(190, 19);
            this.lblOC_BR_NM.TabIndex = 100015;
            this.lblOC_BR_NM.Text = "XXXX";
            // 
            // lblSCAN_BR_NM
            // 
            this.lblSCAN_BR_NM.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSCAN_BR_NM.Location = new System.Drawing.Point(285, 766);
            this.lblSCAN_BR_NM.Name = "lblSCAN_BR_NM";
            this.lblSCAN_BR_NM.Size = new System.Drawing.Size(190, 19);
            this.lblSCAN_BR_NM.TabIndex = 100015;
            this.lblSCAN_BR_NM.Text = "XXXX";
            // 
            // pnlImage
            // 
            this.pnlImage.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pnlImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlImage.Location = new System.Drawing.Point(10, 35);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(1246, 600);
            this.pnlImage.TabIndex = 100034;
            // 
            // BranchGoukeiImgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "BranchGoukeiImgForm";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

        #endregion
        private CommonClass.NumTextBox2 txtFix;
        private CommonClass.NumTextBox2 txtTOTAL_AMOUNT;
        private CommonClass.NumTextBox2 txtTOTAL_COUNT;
        private CommonClass.DTextBox2 txtSCAN_DATE;
        private CommonClass.NumTextBox2 txtSCAN_BR_NO;
        private CommonClass.NumTextBox2 txtOC_BR_NO;
        private CommonClass.NumTextBox2 txtOC_BK_NO;
        private System.Windows.Forms.Label lblOC_BK_NO;
        private System.Windows.Forms.Label lblOC_BR_NO;
        private System.Windows.Forms.Label lblSCAN_BR_NO;
        private System.Windows.Forms.Label lblSCAN_DATE;
        private System.Windows.Forms.Label lblTOTAL_COUNT;
        private System.Windows.Forms.Label lblOC_BK_NM;
        private System.Windows.Forms.Label lblSCAN_BR_NM;
        private System.Windows.Forms.Label lblOC_BR_NM;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.Label lblTOTAL_AMOUNT;
    }
}
