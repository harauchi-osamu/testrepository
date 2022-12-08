namespace IIPReference
{
    partial class AppSubForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this._lblSubDispName = new System.Windows.Forms.Label();
            this._theBatchListControl = new IIPReference.BatchListControl();
            this.contentsPanel.SuspendLayout();
            this.headerInPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this._theBatchListControl);
            // 
            // headerInPanel
            // 
            this.headerInPanel.Controls.Add(this._lblSubDispName);
            // 
            // _lblSubDispName
            // 
            this._lblSubDispName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._lblSubDispName.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._lblSubDispName.Location = new System.Drawing.Point(0, 0);
            this._lblSubDispName.Name = "_lblSubDispName";
            this._lblSubDispName.Size = new System.Drawing.Size(687, 37);
            this._lblSubDispName.TabIndex = 1;
            this._lblSubDispName.Text = "\"\"";
            this._lblSubDispName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _theBatchListControl
            // 
            this._theBatchListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._theBatchListControl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._theBatchListControl.ForeColor = System.Drawing.SystemColors.ControlText;
            this._theBatchListControl.Location = new System.Drawing.Point(0, 0);
            this._theBatchListControl.Margin = new System.Windows.Forms.Padding(5);
            this._theBatchListControl.Name = "_theBatchListControl";
            this._theBatchListControl.Size = new System.Drawing.Size(1268, 856);
            this._theBatchListControl.TabIndex = 0;
            // 
            // AppSubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 993);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "AppSubForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.headerInPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
