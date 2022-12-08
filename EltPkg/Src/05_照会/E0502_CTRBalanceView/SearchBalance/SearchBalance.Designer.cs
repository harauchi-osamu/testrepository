namespace SearchBalance
{
    partial class SearchBalance
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
            System.Windows.Forms.ColumnHeader clKey;
            this.lvDataList = new System.Windows.Forms.ListView();
            this.clBK_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBK_NM = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDivid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clPAY_AMOUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTytle = new System.Windows.Forms.Label();
            this.lblBKNO = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblOpDate = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.lblOpDate);
            this.contentsPanel.Controls.Add(this.label6);
            this.contentsPanel.Controls.Add(this.lblBKNO);
            this.contentsPanel.Controls.Add(this.label11);
            this.contentsPanel.Controls.Add(this.lblTytle);
            this.contentsPanel.Controls.Add(this.lvDataList);
            this.contentsPanel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.contentsPanel.Controls.SetChildIndex(this.lvDataList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblTytle, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label11, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblBKNO, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label6, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblOpDate, 0);
            // 
            // clKey
            // 
            clKey.Text = "clKey";
            clKey.Width = 0;
            // 
            // lvDataList
            // 
            this.lvDataList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.clBK_NO,
            this.clBK_NM,
            this.clDivid,
            this.clPAY_AMOUNT});
            this.lvDataList.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lvDataList.FullRowSelect = true;
            this.lvDataList.GridLines = true;
            this.lvDataList.HideSelection = false;
            this.lvDataList.Location = new System.Drawing.Point(265, 106);
            this.lvDataList.MultiSelect = false;
            this.lvDataList.Name = "lvDataList";
            this.lvDataList.Size = new System.Drawing.Size(739, 689);
            this.lvDataList.TabIndex = 5;
            this.lvDataList.TabStop = false;
            this.lvDataList.UseCompatibleStateImageBehavior = false;
            this.lvDataList.View = System.Windows.Forms.View.Details;
            this.lvDataList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lv_ColumnWidthChanging);
            // 
            // clBK_NO
            // 
            this.clBK_NO.Text = "銀行コード";
            this.clBK_NO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clBK_NO.Width = 124;
            // 
            // clBK_NM
            // 
            this.clBK_NM.Text = "銀行名漢字";
            this.clBK_NM.Width = 260;
            // 
            // clDivid
            // 
            this.clDivid.Text = "借貸区分";
            this.clDivid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clDivid.Width = 118;
            // 
            // clPAY_AMOUNT
            // 
            this.clPAY_AMOUNT.Text = "決済金額";
            this.clPAY_AMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clPAY_AMOUNT.Width = 215;
            // 
            // lblTytle
            // 
            this.lblTytle.AutoSize = true;
            this.lblTytle.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTytle.Location = new System.Drawing.Point(519, 24);
            this.lblTytle.Name = "lblTytle";
            this.lblTytle.Size = new System.Drawing.Size(231, 19);
            this.lblTytle.TabIndex = 100000;
            this.lblTytle.Text = "交換尻情報（参加銀行用）";
            // 
            // lblBKNO
            // 
            this.lblBKNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBKNO.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblBKNO.Location = new System.Drawing.Point(532, 61);
            this.lblBKNO.Margin = new System.Windows.Forms.Padding(0);
            this.lblBKNO.Name = "lblBKNO";
            this.lblBKNO.Size = new System.Drawing.Size(102, 31);
            this.lblBKNO.TabIndex = 100027;
            this.lblBKNO.Text = "XXXX";
            this.lblBKNO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label11.Location = new System.Drawing.Point(446, 61);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 31);
            this.label11.TabIndex = 100026;
            this.label11.Text = "銀行コード";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOpDate
            // 
            this.lblOpDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOpDate.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblOpDate.Location = new System.Drawing.Point(732, 61);
            this.lblOpDate.Margin = new System.Windows.Forms.Padding(0);
            this.lblOpDate.Name = "lblOpDate";
            this.lblOpDate.Size = new System.Drawing.Size(102, 31);
            this.lblOpDate.TabIndex = 100029;
            this.lblOpDate.Text = "XXXX";
            this.lblOpDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label6.Location = new System.Drawing.Point(646, 61);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 31);
            this.label6.TabIndex = 100028;
            this.label6.Text = "交換日";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SearchBalance";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvDataList;
        private System.Windows.Forms.ColumnHeader clBK_NO;
        private System.Windows.Forms.ColumnHeader clPAY_AMOUNT;
        private System.Windows.Forms.ColumnHeader clBK_NM;
        private System.Windows.Forms.ColumnHeader clDivid;
        private System.Windows.Forms.Label lblTytle;
        private System.Windows.Forms.Label lblOpDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblBKNO;
        private System.Windows.Forms.Label label11;
    }
}
