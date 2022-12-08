namespace ParamMaint
{
	partial class DspAdjustForm
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
            this.components = new System.ComponentModel.Container();
            this.picturePanel = new System.Windows.Forms.Panel();
            this.pbCtrl = new System.Windows.Forms.PictureBox();
            this.pbContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripDelRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsPanel.SuspendLayout();
            this.picturePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCtrl)).BeginInit();
            this.pbContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.picturePanel);
            this.contentsPanel.Controls.SetChildIndex(this.picturePanel, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            // 
            // picturePanel
            // 
            this.picturePanel.AutoScroll = true;
            this.picturePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picturePanel.Controls.Add(this.pbCtrl);
            this.picturePanel.Location = new System.Drawing.Point(28, 31);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(1100, 500);
            this.picturePanel.TabIndex = 100000;
            // 
            // pbCtrl
            // 
            this.pbCtrl.ContextMenuStrip = this.pbContextMenu;
            this.pbCtrl.Location = new System.Drawing.Point(0, 0);
            this.pbCtrl.Name = "pbCtrl";
            this.pbCtrl.Size = new System.Drawing.Size(300, 300);
            this.pbCtrl.TabIndex = 0;
            this.pbCtrl.TabStop = false;
            this.pbCtrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCtrl_MouseMove);
            this.pbCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCtrl_MouseUp);
            // 
            // pbContextMenu
            // 
            this.pbContextMenu.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pbContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDelRegion});
            this.pbContextMenu.Name = "pbContextMenu";
            this.pbContextMenu.Size = new System.Drawing.Size(174, 26);
            this.pbContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.pbContextMenu_Opening);
            // 
            // toolStripDelRegion
            // 
            this.toolStripDelRegion.Name = "toolStripDelRegion";
            this.toolStripDelRegion.Size = new System.Drawing.Size(173, 22);
            this.toolStripDelRegion.Text = "領域の削除(&D)";
            this.toolStripDelRegion.Click += new System.EventHandler(this.toolStripDelRegion_Click);
            // 
            // DspAdjustForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "DspAdjustForm";
            this.Controls.SetChildIndex(this.contentsPanel, 0);
            this.contentsPanel.ResumeLayout(false);
            this.picturePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCtrl)).EndInit();
            this.pbContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.Panel picturePanel;
        private System.Windows.Forms.PictureBox pbCtrl;
        private System.Windows.Forms.ContextMenuStrip pbContextMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripDelRegion;
    }
}
