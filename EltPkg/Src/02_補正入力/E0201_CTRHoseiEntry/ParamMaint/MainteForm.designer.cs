namespace ParamMaint
{
    partial class MainteForm
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
            this.lblSourceName = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtGymIdDst = new CommonClass.NumTextBox2();
            this.txtGymIdSrc = new CommonClass.NumTextBox2();
            this.lblDest = new System.Windows.Forms.Label();
            this.lblArrow = new System.Windows.Forms.Label();
            this.lblSource = new System.Windows.Forms.Label();
            this.gymParamReport1 = new ParamMaint.GymParamReport();
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.lblSourceName);
            this.contentsPanel.Controls.Add(this.btnSearch);
            this.contentsPanel.Controls.Add(this.txtGymIdDst);
            this.contentsPanel.Controls.Add(this.txtGymIdSrc);
            this.contentsPanel.Controls.Add(this.lblDest);
            this.contentsPanel.Controls.Add(this.lblArrow);
            this.contentsPanel.Controls.Add(this.lblSource);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblSource, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblArrow, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblDest, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtGymIdSrc, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtGymIdDst, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnSearch, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblSourceName, 0);
            // 
            // lblSourceName
            // 
            this.lblSourceName.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblSourceName.Location = new System.Drawing.Point(594, 314);
            this.lblSourceName.Name = "lblSourceName";
            this.lblSourceName.Size = new System.Drawing.Size(500, 21);
            this.lblSourceName.TabIndex = 100006;
            this.lblSourceName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(469, 299);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 50);
            this.btnSearch.TabIndex = 100003;
            this.btnSearch.Text = "業務検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtGymIdDst
            // 
            this.txtGymIdDst.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtGymIdDst.LeftZero = true;
            this.txtGymIdDst.Location = new System.Drawing.Point(370, 512);
            this.txtGymIdDst.MaxLength = 5;
            this.txtGymIdDst.Name = "txtGymIdDst";
            this.txtGymIdDst.Size = new System.Drawing.Size(80, 26);
            this.txtGymIdDst.TabIndex = 100005;
            this.txtGymIdDst.Text = "00000";
            this.txtGymIdDst.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtGymIdDst.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGymIdDst_KeyDown);
            this.txtGymIdDst.Leave += new System.EventHandler(this.txtGymIdDst_Leave);
            // 
            // txtGymIdSrc
            // 
            this.txtGymIdSrc.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtGymIdSrc.LeftZero = true;
            this.txtGymIdSrc.Location = new System.Drawing.Point(370, 312);
            this.txtGymIdSrc.MaxLength = 5;
            this.txtGymIdSrc.Name = "txtGymIdSrc";
            this.txtGymIdSrc.Size = new System.Drawing.Size(80, 26);
            this.txtGymIdSrc.TabIndex = 100001;
            this.txtGymIdSrc.Text = "00000";
            this.txtGymIdSrc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtGymIdSrc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGymIdSrc_KeyDown);
            this.txtGymIdSrc.Leave += new System.EventHandler(this.txtGymIdSrc_Leave);
            // 
            // lblDest
            // 
            this.lblDest.AutoSize = true;
            this.lblDest.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblDest.Location = new System.Drawing.Point(173, 515);
            this.lblDest.Name = "lblDest";
            this.lblDest.Size = new System.Drawing.Size(181, 19);
            this.lblDest.TabIndex = 100004;
            this.lblDest.Text = "メンテナンス業務番号";
            // 
            // lblArrow
            // 
            this.lblArrow.AutoSize = true;
            this.lblArrow.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblArrow.Location = new System.Drawing.Point(392, 421);
            this.lblArrow.Name = "lblArrow";
            this.lblArrow.Size = new System.Drawing.Size(35, 24);
            this.lblArrow.TabIndex = 100002;
            this.lblArrow.Text = "↓";
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblSource.Location = new System.Drawing.Point(196, 315);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(155, 19);
            this.lblSource.TabIndex = 100000;
            this.lblSource.Text = "コピー元業務番号";
            // 
            // MainteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "MainteForm";
            this.Load += new System.EventHandler(this.Form_Load);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSourceName;
        private System.Windows.Forms.Button btnSearch;
        private CommonClass.NumTextBox2 txtGymIdDst;
        private CommonClass.NumTextBox2 txtGymIdSrc;
        private System.Windows.Forms.Label lblDest;
        private System.Windows.Forms.Label lblArrow;
        private System.Windows.Forms.Label lblSource;
        private GymParamReport gymParamReport1;
    }
}
