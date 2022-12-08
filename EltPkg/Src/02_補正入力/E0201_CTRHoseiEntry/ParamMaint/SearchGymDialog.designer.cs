namespace ParamMaint
{
    partial class SearchGymDialog
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
            this.lblGymno = new System.Windows.Forms.Label();
            this.lblGymkana = new System.Windows.Forms.Label();
            this.txtGymId = new CommonClass.NumTextBox2();
            this.txtGymKana = new CommonClass.KanaTextBox();
            this.livGymList = new System.Windows.Forms.ListView();
            this.clHeaderNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clHeaderKanji = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clHeaderKana = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnBack = new System.Windows.Forms.Button();
            this.btnFixed = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblGymno
            // 
            this.lblGymno.AutoSize = true;
            this.lblGymno.Location = new System.Drawing.Point(16, 15);
            this.lblGymno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGymno.Name = "lblGymno";
            this.lblGymno.Size = new System.Drawing.Size(72, 16);
            this.lblGymno.TabIndex = 0;
            this.lblGymno.Text = "業務番号";
            // 
            // lblGymkana
            // 
            this.lblGymkana.AutoSize = true;
            this.lblGymkana.Location = new System.Drawing.Point(16, 54);
            this.lblGymkana.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGymkana.Name = "lblGymkana";
            this.lblGymkana.Size = new System.Drawing.Size(80, 16);
            this.lblGymkana.TabIndex = 1;
            this.lblGymkana.Text = "業務カナ名";
            // 
            // txtGymId
            // 
            this.txtGymId.Location = new System.Drawing.Point(128, 12);
            this.txtGymId.Margin = new System.Windows.Forms.Padding(4);
            this.txtGymId.MaxLength = 5;
            this.txtGymId.Name = "txtGymId";
            this.txtGymId.Size = new System.Drawing.Size(60, 23);
            this.txtGymId.TabIndex = 2;
            this.txtGymId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtGymId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ntbGymno_KeyDown);
            // 
            // txtGymKana
            // 
            this.txtGymKana.EntryMode = CommonClass.ENTRYMODE.IMEON_ROMAN_HANKAKU_KANA;
            this.txtGymKana.KanaLock = false;
            this.txtGymKana.Location = new System.Drawing.Point(128, 47);
            this.txtGymKana.Margin = new System.Windows.Forms.Padding(4);
            this.txtGymKana.MaxLength = 80;
            this.txtGymKana.Name = "txtGymKana";
            this.txtGymKana.Size = new System.Drawing.Size(431, 23);
            this.txtGymKana.TabIndex = 3;
            this.txtGymKana.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ktbGymkana_KeyDown);
            // 
            // livGymList
            // 
            this.livGymList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clHeaderNo,
            this.clHeaderKanji,
            this.clHeaderKana});
            this.livGymList.FullRowSelect = true;
            this.livGymList.GridLines = true;
            this.livGymList.HideSelection = false;
            this.livGymList.Location = new System.Drawing.Point(19, 88);
            this.livGymList.Margin = new System.Windows.Forms.Padding(4);
            this.livGymList.MultiSelect = false;
            this.livGymList.Name = "livGymList";
            this.livGymList.Size = new System.Drawing.Size(540, 328);
            this.livGymList.TabIndex = 4;
            this.livGymList.UseCompatibleStateImageBehavior = false;
            this.livGymList.View = System.Windows.Forms.View.Details;
            this.livGymList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvGym_ItemSelectionChanged);
            this.livGymList.DoubleClick += new System.EventHandler(this.lvGym_DoubleClick);
            this.livGymList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchGymno_KeyDown);
            // 
            // clHeaderNo
            // 
            this.clHeaderNo.Text = "業務番号";
            this.clHeaderNo.Width = 80;
            // 
            // clHeaderKanji
            // 
            this.clHeaderKanji.Text = "業務名";
            this.clHeaderKanji.Width = 439;
            // 
            // clHeaderKana
            // 
            this.clHeaderKana.Text = "業務名カナ";
            this.clHeaderKana.Width = 0;
            // 
            // btnBack
            // 
            this.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBack.Location = new System.Drawing.Point(572, 366);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 50);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "キャンセル";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnFixed
            // 
            this.btnFixed.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnFixed.Location = new System.Drawing.Point(572, 310);
            this.btnFixed.Name = "btnFixed";
            this.btnFixed.Size = new System.Drawing.Size(100, 50);
            this.btnFixed.TabIndex = 5;
            this.btnFixed.Text = "確定";
            this.btnFixed.UseVisualStyleBackColor = true;
            this.btnFixed.Click += new System.EventHandler(this.btnFixed_Click);
            // 
            // SearchGymDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnBack;
            this.ClientSize = new System.Drawing.Size(684, 429);
            this.Controls.Add(this.btnFixed);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.livGymList);
            this.Controls.Add(this.txtGymKana);
            this.Controls.Add(this.txtGymId);
            this.Controls.Add(this.lblGymkana);
            this.Controls.Add(this.lblGymno);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchGymDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "業務検索";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SearchGymno_FormClosed);
            this.Load += new System.EventHandler(this.SearchGymno_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchGymno_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGymno;
        private System.Windows.Forms.Label lblGymkana;
        private CommonClass.NumTextBox2 txtGymId;
        private CommonClass.KanaTextBox txtGymKana;
        private System.Windows.Forms.ListView livGymList;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ColumnHeader clHeaderNo;
        private System.Windows.Forms.ColumnHeader clHeaderKanji;
        private System.Windows.Forms.ColumnHeader clHeaderKana;
		private System.Windows.Forms.Button btnFixed;
	}
}
