namespace IIPReference
{
    partial class AppMainForm
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
            this._lblMainDispName = new System.Windows.Forms.Label();
            this._theGymListControl = new IIPReference.GymListControl();
            this.contentsPanel.SuspendLayout();
            this.headerInPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this._theGymListControl);
            // 
            // headerInPanel
            // 
            this.headerInPanel.Controls.Add(this._lblMainDispName);
            // 
            // _lblMainDispName
            // 
            this._lblMainDispName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._lblMainDispName.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._lblMainDispName.Location = new System.Drawing.Point(0, 0);
            this._lblMainDispName.Name = "_lblMainDispName";
            this._lblMainDispName.Size = new System.Drawing.Size(687, 37);
            this._lblMainDispName.TabIndex = 1;
            this._lblMainDispName.Text = "\"\"";
            this._lblMainDispName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _theGymListControl
            // 
            this._theGymListControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._theGymListControl.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._theGymListControl.ForeColor = System.Drawing.SystemColors.ControlText;
            this._theGymListControl.Location = new System.Drawing.Point(0, -2);
            this._theGymListControl.Margin = new System.Windows.Forms.Padding(5);
            this._theGymListControl.Name = "_theGymListControl";
            this._theGymListControl.Size = new System.Drawing.Size(1268, 858);
            this._theGymListControl.TabIndex = 0;
            this._theGymListControl.Load += new System.EventHandler(this._theGymListControl_Load);
            // 
            // AppMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 993);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "AppMainForm";
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
