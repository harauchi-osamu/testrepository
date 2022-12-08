namespace CorrectInput
{
	partial class SelectDspForm
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
            this.livDspList = new System.Windows.Forms.ListView();
            this.clHeaderNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pbSample = new System.Windows.Forms.PictureBox();
            this.pbCurrent = new System.Windows.Forms.PictureBox();
            this.contentsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrent)).BeginInit();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.livDspList);
            this.contentsPanel.Controls.Add(this.pbSample);
            this.contentsPanel.Controls.Add(this.pbCurrent);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.pbCurrent, 0);
            this.contentsPanel.Controls.SetChildIndex(this.pbSample, 0);
            this.contentsPanel.Controls.SetChildIndex(this.livDspList, 0);
            // 
            // livDspList
            // 
            this.livDspList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clHeaderNo,
            this.clHeaderName});
            this.livDspList.FullRowSelect = true;
            this.livDspList.GridLines = true;
            this.livDspList.HideSelection = false;
            this.livDspList.Location = new System.Drawing.Point(8, 13);
            this.livDspList.MultiSelect = false;
            this.livDspList.Name = "livDspList";
            this.livDspList.Size = new System.Drawing.Size(429, 810);
            this.livDspList.TabIndex = 1;
            this.livDspList.UseCompatibleStateImageBehavior = false;
            this.livDspList.View = System.Windows.Forms.View.Details;
            this.livDspList.SelectedIndexChanged += new System.EventHandler(this.livDspList_SelectedIndexChanged);
            this.livDspList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.livDspList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            // 
            // clHeaderNo
            // 
            this.clHeaderNo.Text = "コード";
            this.clHeaderNo.Width = 90;
            // 
            // clHeaderName
            // 
            this.clHeaderName.Text = "コード名称";
            this.clHeaderName.Width = 335;
            // 
            // pbSample
            // 
            this.pbSample.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbSample.Location = new System.Drawing.Point(443, 421);
            this.pbSample.Name = "pbSample";
            this.pbSample.Size = new System.Drawing.Size(820, 402);
            this.pbSample.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSample.TabIndex = 100001;
            this.pbSample.TabStop = false;
            this.pbSample.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // pbCurrent
            // 
            this.pbCurrent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbCurrent.Location = new System.Drawing.Point(443, 13);
            this.pbCurrent.Name = "pbCurrent";
            this.pbCurrent.Size = new System.Drawing.Size(820, 402);
            this.pbCurrent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCurrent.TabIndex = 100000;
            this.pbCurrent.TabStop = false;
            this.pbCurrent.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // SelectDspForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SelectDspForm";
            this.Load += new System.EventHandler(this.SelectDspForm_Load);
            this.contentsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrent)).EndInit();
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.ListView livDspList;
        private System.Windows.Forms.ColumnHeader clHeaderNo;
        private System.Windows.Forms.ColumnHeader clHeaderName;
        private System.Windows.Forms.PictureBox pbSample;
        private System.Windows.Forms.PictureBox pbCurrent;
    }
}
