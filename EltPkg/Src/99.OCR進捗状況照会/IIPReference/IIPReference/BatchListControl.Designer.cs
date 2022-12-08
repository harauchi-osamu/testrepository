namespace IIPReference
{
    partial class BatchListControl
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.lstBatch = new System.Windows.Forms.ListView();
            this.colNull = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStartTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGoki = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBatchName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOcr1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOcr2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSeikei = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRenkei = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colComplete = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colError = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbOpeDate = new System.Windows.Forms.TextBox();
            this.lblOpeDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstBatch
            // 
            this.lstBatch.BackColor = System.Drawing.SystemColors.Window;
            this.lstBatch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNull,
            this.colStartTime,
            this.colGoki,
            this.colBatchName,
            this.colCount,
            this.colOcr1,
            this.colOcr2,
            this.colSeikei,
            this.colRenkei,
            this.colComplete,
            this.colError});
            this.lstBatch.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lstBatch.FullRowSelect = true;
            this.lstBatch.GridLines = true;
            this.lstBatch.HideSelection = false;
            this.lstBatch.Location = new System.Drawing.Point(20, 60);
            this.lstBatch.Margin = new System.Windows.Forms.Padding(20, 60, 20, 20);
            this.lstBatch.MultiSelect = false;
            this.lstBatch.Name = "lstBatch";
            this.lstBatch.Size = new System.Drawing.Size(1226, 763);
            this.lstBatch.TabIndex = 0;
            this.lstBatch.UseCompatibleStateImageBehavior = false;
            this.lstBatch.View = System.Windows.Forms.View.Details;
            this.lstBatch.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstBatch_ColumnClick);
            this.lstBatch.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lstBatch_DrawColumnHeader);
            this.lstBatch.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lstBatch_DrawItem);
            this.lstBatch.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lstBatch_DrawSubItem);
            // 
            // colNull
            // 
            this.colNull.DisplayIndex = 10;
            this.colNull.Text = "";
            this.colNull.Width = 0;
            // 
            // colStartTime
            // 
            this.colStartTime.DisplayIndex = 0;
            this.colStartTime.Text = "スキャン時刻";
            this.colStartTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colStartTime.Width = 108;
            // 
            // colGoki
            // 
            this.colGoki.DisplayIndex = 1;
            this.colGoki.Text = "号機";
            this.colGoki.Width = 80;
            // 
            // colBatchName
            // 
            this.colBatchName.DisplayIndex = 2;
            this.colBatchName.Text = "バッチ名";
            this.colBatchName.Width = 244;
            // 
            // colCount
            // 
            this.colCount.DisplayIndex = 3;
            this.colCount.Text = "読込枚数";
            this.colCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colCount.Width = 100;
            // 
            // colOcr1
            // 
            this.colOcr1.DisplayIndex = 4;
            this.colOcr1.Text = "汎用OCR";
            this.colOcr1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colOcr1.Width = 100;
            // 
            // colOcr2
            // 
            this.colOcr2.DisplayIndex = 5;
            this.colOcr2.Text = "手形OCR";
            this.colOcr2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colOcr2.Width = 100;
            // 
            // colSeikei
            // 
            this.colSeikei.DisplayIndex = 6;
            this.colSeikei.Text = "成型";
            this.colSeikei.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colSeikei.Width = 100;
            // 
            // colRenkei
            // 
            this.colRenkei.DisplayIndex = 7;
            this.colRenkei.Text = "連携";
            this.colRenkei.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colRenkei.Width = 100;
            // 
            // colComplete
            // 
            this.colComplete.DisplayIndex = 8;
            this.colComplete.Text = "処理済";
            this.colComplete.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colComplete.Width = 100;
            // 
            // colError
            // 
            this.colError.DisplayIndex = 9;
            this.colError.Text = "エラー";
            this.colError.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbOpeDate
            // 
            this.tbOpeDate.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbOpeDate.Location = new System.Drawing.Point(113, 18);
            this.tbOpeDate.MaxLength = 8;
            this.tbOpeDate.Name = "tbOpeDate";
            this.tbOpeDate.Size = new System.Drawing.Size(129, 26);
            this.tbOpeDate.TabIndex = 1;
            this.tbOpeDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbOpeDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbOpeDate_KeyDown);
            // 
            // lblOpeDate
            // 
            this.lblOpeDate.AutoSize = true;
            this.lblOpeDate.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOpeDate.Location = new System.Drawing.Point(31, 21);
            this.lblOpeDate.Name = "lblOpeDate";
            this.lblOpeDate.Size = new System.Drawing.Size(76, 19);
            this.lblOpeDate.TabIndex = 2;
            this.lblOpeDate.Text = "処理日：";
            // 
            // BatchListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblOpeDate);
            this.Controls.Add(this.tbOpeDate);
            this.Controls.Add(this.lstBatch);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "BatchListControl";
            this.Size = new System.Drawing.Size(1266, 856);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstBatch;
        private System.Windows.Forms.ColumnHeader colStartTime;
        private System.Windows.Forms.ColumnHeader colBatchName;
        private System.Windows.Forms.ColumnHeader colCount;
        private System.Windows.Forms.ColumnHeader colOcr1;
        private System.Windows.Forms.ColumnHeader colOcr2;
        private System.Windows.Forms.ColumnHeader colSeikei;
        private System.Windows.Forms.ColumnHeader colRenkei;
        private System.Windows.Forms.ColumnHeader colComplete;
        private System.Windows.Forms.ColumnHeader colError;
        private System.Windows.Forms.Label lblOpeDate;
        private System.Windows.Forms.ColumnHeader colNull;
        protected System.Windows.Forms.TextBox tbOpeDate;
        private System.Windows.Forms.ColumnHeader colGoki;
    }
}
