namespace ParamMaint
{
    partial class ImageParamForm
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
            this.lblScrollPos = new System.Windows.Forms.Label();
            this.lblImgBase = new System.Windows.Forms.Label();
            this.lblImgFile = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblBaseLeft = new System.Windows.Forms.Label();
            this.lblBaseTop = new System.Windows.Forms.Label();
            this.lblReduction = new System.Windows.Forms.Label();
            this.ntbScroolPos = new CommonClass.NumTextBox2();
            this.ntbImgBase = new CommonClass.NumTextBox2();
            this.tbImgFile = new System.Windows.Forms.TextBox();
            this.ntbHeight = new CommonClass.NumTextBox2();
            this.ntbWidth = new CommonClass.NumTextBox2();
            this.ntbBaseLeft = new CommonClass.NumTextBox2();
            this.ntbBaseTop = new CommonClass.NumTextBox2();
            this.ntbReduction = new CommonClass.NumTextBox2();
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.lblScrollPos);
            this.contentsPanel.Controls.Add(this.lblImgBase);
            this.contentsPanel.Controls.Add(this.lblImgFile);
            this.contentsPanel.Controls.Add(this.lblHeight);
            this.contentsPanel.Controls.Add(this.lblWidth);
            this.contentsPanel.Controls.Add(this.lblBaseLeft);
            this.contentsPanel.Controls.Add(this.lblBaseTop);
            this.contentsPanel.Controls.Add(this.lblReduction);
            this.contentsPanel.Controls.Add(this.ntbScroolPos);
            this.contentsPanel.Controls.Add(this.ntbImgBase);
            this.contentsPanel.Controls.Add(this.tbImgFile);
            this.contentsPanel.Controls.Add(this.ntbHeight);
            this.contentsPanel.Controls.Add(this.ntbWidth);
            this.contentsPanel.Controls.Add(this.ntbBaseLeft);
            this.contentsPanel.Controls.Add(this.ntbBaseTop);
            this.contentsPanel.Controls.Add(this.ntbReduction);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntbReduction, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntbBaseTop, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntbBaseLeft, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntbWidth, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntbHeight, 0);
            this.contentsPanel.Controls.SetChildIndex(this.tbImgFile, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntbImgBase, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntbScroolPos, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblReduction, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblBaseTop, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblBaseLeft, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblWidth, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblHeight, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblImgFile, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblImgBase, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblScrollPos, 0);
            // 
            // lblScrollPos
            // 
            this.lblScrollPos.AutoSize = true;
            this.lblScrollPos.Location = new System.Drawing.Point(571, 303);
            this.lblScrollPos.Name = "lblScrollPos";
            this.lblScrollPos.Size = new System.Drawing.Size(121, 19);
            this.lblScrollPos.TabIndex = 100015;
            this.lblScrollPos.Text = "スクロール位置";
            // 
            // lblImgBase
            // 
            this.lblImgBase.AutoSize = true;
            this.lblImgBase.Location = new System.Drawing.Point(571, 233);
            this.lblImgBase.Name = "lblImgBase";
            this.lblImgBase.Size = new System.Drawing.Size(125, 19);
            this.lblImgBase.TabIndex = 100014;
            this.lblImgBase.Text = "イメージ内基点";
            // 
            // lblImgFile
            // 
            this.lblImgFile.AutoSize = true;
            this.lblImgFile.Location = new System.Drawing.Point(571, 163);
            this.lblImgFile.Name = "lblImgFile";
            this.lblImgFile.Size = new System.Drawing.Size(122, 19);
            this.lblImgFile.TabIndex = 100013;
            this.lblImgFile.Text = "イメージファイル";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(171, 443);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(41, 19);
            this.lblHeight.TabIndex = 100012;
            this.lblHeight.Text = "高さ";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(171, 373);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(28, 19);
            this.lblWidth.TabIndex = 100011;
            this.lblWidth.Text = "幅";
            // 
            // lblBaseLeft
            // 
            this.lblBaseLeft.AutoSize = true;
            this.lblBaseLeft.Location = new System.Drawing.Point(171, 303);
            this.lblBaseLeft.Name = "lblBaseLeft";
            this.lblBaseLeft.Size = new System.Drawing.Size(101, 19);
            this.lblBaseLeft.TabIndex = 100010;
            this.lblBaseLeft.Text = "基点(LEFT)";
            // 
            // lblBaseTop
            // 
            this.lblBaseTop.AutoSize = true;
            this.lblBaseTop.Location = new System.Drawing.Point(171, 233);
            this.lblBaseTop.Name = "lblBaseTop";
            this.lblBaseTop.Size = new System.Drawing.Size(95, 19);
            this.lblBaseTop.TabIndex = 100009;
            this.lblBaseTop.Text = "基点(TOP)";
            // 
            // lblReduction
            // 
            this.lblReduction.AutoSize = true;
            this.lblReduction.Location = new System.Drawing.Point(171, 163);
            this.lblReduction.Name = "lblReduction";
            this.lblReduction.Size = new System.Drawing.Size(66, 19);
            this.lblReduction.TabIndex = 100008;
            this.lblReduction.Text = "縮小率";
            // 
            // ntbScroolPos
            // 
            this.ntbScroolPos.Location = new System.Drawing.Point(741, 300);
            this.ntbScroolPos.MaxLength = 4;
            this.ntbScroolPos.Name = "ntbScroolPos";
            this.ntbScroolPos.Size = new System.Drawing.Size(68, 26);
            this.ntbScroolPos.TabIndex = 100007;
            this.ntbScroolPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // ntbImgBase
            // 
            this.ntbImgBase.Location = new System.Drawing.Point(741, 230);
            this.ntbImgBase.MaxLength = 4;
            this.ntbImgBase.Name = "ntbImgBase";
            this.ntbImgBase.Size = new System.Drawing.Size(100, 26);
            this.ntbImgBase.TabIndex = 100006;
            this.ntbImgBase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // tbImgFile
            // 
            this.tbImgFile.Location = new System.Drawing.Point(741, 160);
            this.tbImgFile.MaxLength = 30;
            this.tbImgFile.Name = "tbImgFile";
            this.tbImgFile.Size = new System.Drawing.Size(352, 26);
            this.tbImgFile.TabIndex = 100005;
            this.tbImgFile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // ntbHeight
            // 
            this.ntbHeight.Location = new System.Drawing.Point(301, 440);
            this.ntbHeight.MaxLength = 4;
            this.ntbHeight.Name = "ntbHeight";
            this.ntbHeight.Size = new System.Drawing.Size(100, 26);
            this.ntbHeight.TabIndex = 100004;
            this.ntbHeight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // ntbWidth
            // 
            this.ntbWidth.Location = new System.Drawing.Point(301, 370);
            this.ntbWidth.MaxLength = 4;
            this.ntbWidth.Name = "ntbWidth";
            this.ntbWidth.Size = new System.Drawing.Size(100, 26);
            this.ntbWidth.TabIndex = 100003;
            this.ntbWidth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // ntbBaseLeft
            // 
            this.ntbBaseLeft.Location = new System.Drawing.Point(298, 300);
            this.ntbBaseLeft.MaxLength = 4;
            this.ntbBaseLeft.Name = "ntbBaseLeft";
            this.ntbBaseLeft.Size = new System.Drawing.Size(100, 26);
            this.ntbBaseLeft.TabIndex = 100002;
            this.ntbBaseLeft.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // ntbBaseTop
            // 
            this.ntbBaseTop.Location = new System.Drawing.Point(301, 230);
            this.ntbBaseTop.MaxLength = 4;
            this.ntbBaseTop.Name = "ntbBaseTop";
            this.ntbBaseTop.Size = new System.Drawing.Size(100, 26);
            this.ntbBaseTop.TabIndex = 100001;
            this.ntbBaseTop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // ntbReduction
            // 
            this.ntbReduction.Location = new System.Drawing.Point(301, 160);
            this.ntbReduction.MaxLength = 3;
            this.ntbReduction.Name = "ntbReduction";
            this.ntbReduction.Size = new System.Drawing.Size(39, 26);
            this.ntbReduction.TabIndex = 100000;
            this.ntbReduction.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // ImageParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "ImageParamForm";
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblScrollPos;
        private System.Windows.Forms.Label lblImgBase;
        private System.Windows.Forms.Label lblImgFile;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblBaseLeft;
        private System.Windows.Forms.Label lblBaseTop;
        private System.Windows.Forms.Label lblReduction;
        private CommonClass.NumTextBox2 ntbScroolPos;
        private CommonClass.NumTextBox2 ntbImgBase;
        private System.Windows.Forms.TextBox tbImgFile;
        private CommonClass.NumTextBox2 ntbHeight;
        private CommonClass.NumTextBox2 ntbWidth;
        private CommonClass.NumTextBox2 ntbBaseLeft;
        private CommonClass.NumTextBox2 ntbBaseTop;
        private CommonClass.NumTextBox2 ntbReduction;
    }
}
