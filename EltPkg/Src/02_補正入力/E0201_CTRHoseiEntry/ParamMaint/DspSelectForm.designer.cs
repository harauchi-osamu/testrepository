namespace ParamMaint
{
    partial class DspSelectForm
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
            this.lblDestName = new System.Windows.Forms.Label();
            this.lblSourceName = new System.Windows.Forms.Label();
            this.ntbDest = new CommonClass.NumTextBox2();
            this.ntbSource = new CommonClass.NumTextBox2();
            this.lblDest = new System.Windows.Forms.Label();
            this.lblArrow = new System.Windows.Forms.Label();
            this.lblSource = new System.Windows.Forms.Label();
            this.lvBatList = new System.Windows.Forms.ListView();
            this.colDspId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDspName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblGymName = new System.Windows.Forms.Label();
            this.lblGymNo = new System.Windows.Forms.Label();
            this.colKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.lblDestName);
            this.contentsPanel.Controls.Add(this.lblSourceName);
            this.contentsPanel.Controls.Add(this.ntbDest);
            this.contentsPanel.Controls.Add(this.ntbSource);
            this.contentsPanel.Controls.Add(this.lblDest);
            this.contentsPanel.Controls.Add(this.lblArrow);
            this.contentsPanel.Controls.Add(this.lblSource);
            this.contentsPanel.Controls.Add(this.lvBatList);
            this.contentsPanel.Controls.Add(this.lblGymName);
            this.contentsPanel.Controls.Add(this.lblGymNo);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblGymNo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblGymName, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lvBatList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblSource, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblArrow, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblDest, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntbSource, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntbDest, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblSourceName, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblDestName, 0);
            // 
            // lblDestName
            // 
            this.lblDestName.Location = new System.Drawing.Point(591, 773);
            this.lblDestName.Name = "lblDestName";
            this.lblDestName.Size = new System.Drawing.Size(500, 23);
            this.lblDestName.TabIndex = 100009;
            this.lblDestName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSourceName
            // 
            this.lblSourceName.Location = new System.Drawing.Point(591, 680);
            this.lblSourceName.Name = "lblSourceName";
            this.lblSourceName.Size = new System.Drawing.Size(500, 23);
            this.lblSourceName.TabIndex = 100008;
            this.lblSourceName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ntbDest
            // 
            this.ntbDest.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.ntbDest.Location = new System.Drawing.Point(505, 772);
            this.ntbDest.MaxLength = 5;
            this.ntbDest.Name = "ntbDest";
            this.ntbDest.Size = new System.Drawing.Size(80, 26);
            this.ntbDest.TabIndex = 100007;
            this.ntbDest.Text = "00000";
            this.ntbDest.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ntbDest.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ntbDest_KeyDown);
            this.ntbDest.Leave += new System.EventHandler(this.ntbDest_Leave);
            // 
            // ntbSource
            // 
            this.ntbSource.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.ntbSource.Location = new System.Drawing.Point(505, 679);
            this.ntbSource.MaxLength = 5;
            this.ntbSource.Name = "ntbSource";
            this.ntbSource.Size = new System.Drawing.Size(80, 26);
            this.ntbSource.TabIndex = 100006;
            this.ntbSource.Text = "00000";
            this.ntbSource.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ntbSource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ntbSource_KeyDown);
            this.ntbSource.Leave += new System.EventHandler(this.ntbSource_Leave);
            // 
            // lblDest
            // 
            this.lblDest.AutoSize = true;
            this.lblDest.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblDest.Location = new System.Drawing.Point(306, 775);
            this.lblDest.Name = "lblDest";
            this.lblDest.Size = new System.Drawing.Size(181, 19);
            this.lblDest.TabIndex = 100005;
            this.lblDest.Text = "メンテナンス画面番号";
            // 
            // lblArrow
            // 
            this.lblArrow.AutoSize = true;
            this.lblArrow.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblArrow.Location = new System.Drawing.Point(527, 726);
            this.lblArrow.Name = "lblArrow";
            this.lblArrow.Size = new System.Drawing.Size(35, 24);
            this.lblArrow.TabIndex = 100004;
            this.lblArrow.Text = "↓";
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblSource.Location = new System.Drawing.Point(330, 682);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(155, 19);
            this.lblSource.TabIndex = 100003;
            this.lblSource.Text = "コピー元画面番号";
            // 
            // lvBatList
            // 
            this.lvBatList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colKey,
            this.colDspId,
            this.colDspName});
            this.lvBatList.FullRowSelect = true;
            this.lvBatList.GridLines = true;
            this.lvBatList.HideSelection = false;
            this.lvBatList.Location = new System.Drawing.Point(179, 91);
            this.lvBatList.MultiSelect = false;
            this.lvBatList.Name = "lvBatList";
            this.lvBatList.Size = new System.Drawing.Size(754, 556);
            this.lvBatList.TabIndex = 100002;
            this.lvBatList.TabStop = false;
            this.lvBatList.UseCompatibleStateImageBehavior = false;
            this.lvBatList.View = System.Windows.Forms.View.Details;
            this.lvBatList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.DisplayList_ItemSelectionChanged);
            this.lvBatList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // colDspId
            // 
            this.colDspId.Text = "画面番号";
            this.colDspId.Width = 120;
            // 
            // colDspName
            // 
            this.colDspName.Text = "画面名";
            this.colDspName.Width = 614;
            // 
            // lblGymName
            // 
            this.lblGymName.Location = new System.Drawing.Point(269, 38);
            this.lblGymName.Name = "lblGymName";
            this.lblGymName.Size = new System.Drawing.Size(664, 19);
            this.lblGymName.TabIndex = 100001;
            this.lblGymName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGymNo
            // 
            this.lblGymNo.AutoSize = true;
            this.lblGymNo.Location = new System.Drawing.Point(175, 38);
            this.lblGymNo.Name = "lblGymNo";
            this.lblGymNo.Size = new System.Drawing.Size(85, 19);
            this.lblGymNo.TabIndex = 100000;
            this.lblGymNo.Text = "業務番号";
            // 
            // colKey
            // 
            this.colKey.Text = "";
            this.colKey.Width = 0;
            // 
            // DspSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "DspSelectForm";
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDestName;
        private System.Windows.Forms.Label lblSourceName;
        private CommonClass.NumTextBox2 ntbDest;
        private CommonClass.NumTextBox2 ntbSource;
        private System.Windows.Forms.Label lblDest;
        private System.Windows.Forms.Label lblArrow;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.ListView lvBatList;
        private System.Windows.Forms.ColumnHeader colDspId;
        private System.Windows.Forms.ColumnHeader colDspName;
        private System.Windows.Forms.Label lblGymName;
        private System.Windows.Forms.Label lblGymNo;
        private System.Windows.Forms.ColumnHeader colKey;
    }
}
