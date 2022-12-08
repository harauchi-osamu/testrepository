namespace IIPReference
{
    partial class GymListControl
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
            this.lstGym = new System.Windows.Forms.ListView();
            this.colNull = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGymName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProcessingBatch = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCompleteBatch = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colError = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lstGym
            // 
            this.lstGym.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNull,
            this.colGymName,
            this.colProcessingBatch,
            this.colCompleteBatch,
            this.colError});
            this.lstGym.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lstGym.FullRowSelect = true;
            this.lstGym.GridLines = true;
            this.lstGym.HideSelection = false;
            this.lstGym.Location = new System.Drawing.Point(20, 60);
            this.lstGym.Margin = new System.Windows.Forms.Padding(20, 60, 20, 20);
            this.lstGym.MultiSelect = false;
            this.lstGym.Name = "lstGym";
            this.lstGym.Size = new System.Drawing.Size(1226, 699);
            this.lstGym.TabIndex = 0;
            this.lstGym.UseCompatibleStateImageBehavior = false;
            this.lstGym.View = System.Windows.Forms.View.Details;
            this.lstGym.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstGym_ColumnClick);
            this.lstGym.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lstGym_DrawColumnHeader);
            this.lstGym.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lstGym_DrawItem);
            this.lstGym.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lstGym_DrawSubItem);
            this.lstGym.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstGym_MouseDoubleClick);
            // 
            // colNull
            // 
            this.colNull.Text = "";
            this.colNull.Width = 0;
            // 
            // colGymName
            // 
            this.colGymName.Text = "業務名";
            this.colGymName.Width = 600;
            // 
            // colProcessingBatch
            // 
            this.colProcessingBatch.Text = "処理中枚数";
            this.colProcessingBatch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colProcessingBatch.Width = 120;
            // 
            // colCompleteBatch
            // 
            this.colCompleteBatch.Text = "処理済枚数";
            this.colCompleteBatch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colCompleteBatch.Width = 120;
            // 
            // colError
            // 
            this.colError.Text = "エラー";
            this.colError.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GymListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstGym);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "GymListControl";
            this.Size = new System.Drawing.Size(1266, 856);
            this.Load += new System.EventHandler(this.GymListControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstGym;
        private System.Windows.Forms.ColumnHeader colGymName;
        private System.Windows.Forms.ColumnHeader colProcessingBatch;
        private System.Windows.Forms.ColumnHeader colCompleteBatch;
        private System.Windows.Forms.ColumnHeader colError;
        private System.Windows.Forms.ColumnHeader colNull;
    }
}
