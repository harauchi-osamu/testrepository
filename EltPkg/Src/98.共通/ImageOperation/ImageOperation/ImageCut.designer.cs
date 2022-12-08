namespace ImageOperation
{
    partial class ImageCut
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
            this.pnlCut = new System.Windows.Forms.Panel();
            this.pbCutImage = new System.Windows.Forms.PictureBox();
            this.contentsPanel.SuspendLayout();
            this.pnlCut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCutImage)).BeginInit();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.contentsPanel.Controls.Add(this.pnlCut);
            this.contentsPanel.Controls.SetChildIndex(this.pnlCut, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            // 
            // pnlCut
            // 
            this.pnlCut.Controls.Add(this.pbCutImage);
            this.pnlCut.Location = new System.Drawing.Point(22, 17);
            this.pnlCut.Name = "pnlCut";
            this.pnlCut.Size = new System.Drawing.Size(1227, 711);
            this.pnlCut.TabIndex = 100000;
            // 
            // pbCutImage
            // 
            this.pbCutImage.BackColor = System.Drawing.SystemColors.Control;
            this.pbCutImage.Location = new System.Drawing.Point(0, 0);
            this.pbCutImage.Name = "pbCutImage";
            this.pbCutImage.Size = new System.Drawing.Size(1227, 441);
            this.pbCutImage.TabIndex = 9;
            this.pbCutImage.TabStop = false;
            this.pbCutImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCut_MouseDown);
            this.pbCutImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCut_MouseMove);
            this.pbCutImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCut_MouseUp);
            // 
            // ImageCut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "ImageCut";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.pnlCut.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCutImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCut;
        private System.Windows.Forms.PictureBox pbCutImage;
    }
}
