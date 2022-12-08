namespace EntryCommon
{
    partial class EntryCommonFormBase
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
            this.lblDispName1 = new System.Windows.Forms.Label();
            this.lblDispName2 = new System.Windows.Forms.Label();
            this.headerInPanel2.SuspendLayout();
            this.headerInPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerInPanel2
            // 
            this.headerInPanel2.Controls.Add(this.lblDispName2);
            // 
            // headerInPanel1
            // 
            this.headerInPanel1.Controls.Add(this.lblDispName1);
            // 
            // lblDispName1
            // 
            this.lblDispName1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDispName1.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDispName1.Location = new System.Drawing.Point(0, 0);
            this.lblDispName1.Name = "lblDispName1";
            this.lblDispName1.Size = new System.Drawing.Size(557, 30);
            this.lblDispName1.TabIndex = 0;
            this.lblDispName1.Text = "画面名";
            this.lblDispName1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDispName2
            // 
            this.lblDispName2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDispName2.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDispName2.Location = new System.Drawing.Point(0, 0);
            this.lblDispName2.Name = "lblDispName2";
            this.lblDispName2.Size = new System.Drawing.Size(557, 30);
            this.lblDispName2.TabIndex = 1;
            this.lblDispName2.Text = "画面名";
            this.lblDispName2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EntryCommonFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "EntryCommonFormBase";
            this.headerInPanel2.ResumeLayout(false);
            this.headerInPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

		#endregion

        private System.Windows.Forms.Label lblDispName1;
        private System.Windows.Forms.Label lblDispName2;
    }
}
