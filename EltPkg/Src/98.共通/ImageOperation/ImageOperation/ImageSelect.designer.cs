namespace ImageOperation
{
    partial class ImageSelect
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFileList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lvImageList = new System.Windows.Forms.ListView();
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.contentsPanel.Controls.Add(this.lvImageList);
            this.contentsPanel.Controls.Add(this.cmbFileList);
            this.contentsPanel.Controls.Add(this.label3);
            this.contentsPanel.Controls.Add(this.label5);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label5, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label3, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbFileList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lvImageList, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(121, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 19);
            this.label1.TabIndex = 100003;
            this.label1.Text = "差替イメージ選択";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 19);
            this.label3.TabIndex = 100004;
            this.label3.Text = "差替基イメージフォルダ";
            // 
            // cmbFileList
            // 
            this.cmbFileList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFileList.FormattingEnabled = true;
            this.cmbFileList.Location = new System.Drawing.Point(308, 34);
            this.cmbFileList.Name = "cmbFileList";
            this.cmbFileList.Size = new System.Drawing.Size(500, 27);
            this.cmbFileList.TabIndex = 100005;
            this.cmbFileList.SelectedIndexChanged += new System.EventHandler(this.cmbFileList_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(121, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 19);
            this.label5.TabIndex = 100003;
            this.label5.Text = "イメージ一覧";
            // 
            // lvImageList
            // 
            this.lvImageList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvImageList.BackColor = System.Drawing.SystemColors.Window;
            this.lvImageList.FullRowSelect = true;
            this.lvImageList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvImageList.HideSelection = false;
            this.lvImageList.Location = new System.Drawing.Point(125, 102);
            this.lvImageList.MultiSelect = false;
            this.lvImageList.Name = "lvImageList";
            this.lvImageList.Size = new System.Drawing.Size(1000, 720);
            this.lvImageList.TabIndex = 100006;
            this.lvImageList.TabStop = false;
            this.lvImageList.UseCompatibleStateImageBehavior = false;
            this.lvImageList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.lvImageList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvBatList_MouseDoubleClick);
            // 
            // ImageSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "ImageSelect";
            this.Load += new System.EventHandler(this.Form_Load);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbFileList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ListView lvImageList;
    }
}
