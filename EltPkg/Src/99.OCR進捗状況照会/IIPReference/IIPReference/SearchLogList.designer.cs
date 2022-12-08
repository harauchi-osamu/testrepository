namespace IIPReference
{
    partial class SearchLogList
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
            this.btnBack = new System.Windows.Forms.Button();
            this.btnKakutei = new System.Windows.Forms.Button();
            this.lstLogList = new System.Windows.Forms.ListView();
            this.clFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clFilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clWriteTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstLogText = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBack.Location = new System.Drawing.Point(864, 755);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 36);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "F1：戻る";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnBack.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchLogList_KeyDown);
            // 
            // btnKakutei
            // 
            this.btnKakutei.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKakutei.Location = new System.Drawing.Point(970, 755);
            this.btnKakutei.Name = "btnKakutei";
            this.btnKakutei.Size = new System.Drawing.Size(100, 36);
            this.btnKakutei.TabIndex = 4;
            this.btnKakutei.Text = "F12：表示";
            this.btnKakutei.UseVisualStyleBackColor = true;
            this.btnKakutei.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // lstLogList
            // 
            this.lstLogList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clFileName,
            this.clFilePath,
            this.clWriteTime});
            this.lstLogList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lstLogList.FullRowSelect = true;
            this.lstLogList.GridLines = true;
            this.lstLogList.HideSelection = false;
            this.lstLogList.Location = new System.Drawing.Point(13, 13);
            this.lstLogList.Margin = new System.Windows.Forms.Padding(4);
            this.lstLogList.MultiSelect = false;
            this.lstLogList.Name = "lstLogList";
            this.lstLogList.Size = new System.Drawing.Size(1060, 306);
            this.lstLogList.TabIndex = 1;
            this.lstLogList.UseCompatibleStateImageBehavior = false;
            this.lstLogList.View = System.Windows.Forms.View.Details;
            this.lstLogList.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lstLogList_DrawColumnHeader);
            this.lstLogList.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lstLogList_DrawItem);
            this.lstLogList.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lstLogList_DrawSubItem);
            this.lstLogList.DoubleClick += new System.EventHandler(this.lstLogList_DoubleClick);
            this.lstLogList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstLogList_KeyDown);
            // 
            // clFileName
            // 
            this.clFileName.Text = "エラーログ";
            this.clFileName.Width = 280;
            // 
            // clFilePath
            // 
            this.clFilePath.Text = "ファイルパス";
            this.clFilePath.Width = 280;
            // 
            // clWriteTime
            // 
            this.clWriteTime.Text = "更新日時";
            this.clWriteTime.Width = 200;
            // 
            // lstLogText
            // 
            this.lstLogText.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstLogText.HideSelection = false;
            this.lstLogText.Location = new System.Drawing.Point(13, 326);
            this.lstLogText.Margin = new System.Windows.Forms.Padding(4);
            this.lstLogText.Name = "lstLogText";
            this.lstLogText.Size = new System.Drawing.Size(1060, 423);
            this.lstLogText.TabIndex = 2;
            this.lstLogText.UseCompatibleStateImageBehavior = false;
            this.lstLogText.View = System.Windows.Forms.View.Details;
            this.lstLogText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstLogText_KeyDown);
            // 
            // SearchLogList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1085, 795);
            this.Controls.Add(this.lstLogText);
            this.Controls.Add(this.btnKakutei);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lstLogList);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchLogList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SearchLogList_FormClosed);
            this.Load += new System.EventHandler(this.SearchLogList_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lstLogList;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ColumnHeader clFileName;
        private System.Windows.Forms.Button btnKakutei;
        private System.Windows.Forms.ColumnHeader clFilePath;
        private System.Windows.Forms.ColumnHeader clWriteTime;
        private System.Windows.Forms.ListView lstLogText;
        //private EntryClass.NumTextBox2 ntbAccountNo;
    }
}
