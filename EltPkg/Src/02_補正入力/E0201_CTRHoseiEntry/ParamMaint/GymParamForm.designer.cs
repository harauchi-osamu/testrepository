namespace ParamMaint
{
    partial class GymParamForm
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
            this.lblGymID2 = new System.Windows.Forms.Label();
            this.txtGymKanji = new CommonClass.KanaTextBox();
            this.txtGymKana = new CommonClass.KanaTextBox();
            this.lblGymKanji = new System.Windows.Forms.Label();
            this.lblGymKana = new System.Windows.Forms.Label();
            this.lblGymID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSaveDay = new CommonClass.NumTextBox2();
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.txtSaveDay);
            this.contentsPanel.Controls.Add(this.lblGymID2);
            this.contentsPanel.Controls.Add(this.txtGymKanji);
            this.contentsPanel.Controls.Add(this.txtGymKana);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.Add(this.lblGymKanji);
            this.contentsPanel.Controls.Add(this.lblGymKana);
            this.contentsPanel.Controls.Add(this.lblGymID);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblGymID, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblGymKana, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblGymKanji, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtGymKana, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtGymKanji, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblGymID2, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtSaveDay, 0);
            // 
            // lblGymID2
            // 
            this.lblGymID2.Location = new System.Drawing.Point(221, 29);
            this.lblGymID2.Name = "lblGymID2";
            this.lblGymID2.Size = new System.Drawing.Size(100, 19);
            this.lblGymID2.TabIndex = 100040;
            // 
            // txtGymKanji
            // 
            this.txtGymKanji.EntryMode = CommonClass.ENTRYMODE.IMEOFF_KANA;
            this.txtGymKanji.KanaLock = false;
            this.txtGymKanji.Location = new System.Drawing.Point(225, 100);
            this.txtGymKanji.MaxLength = 40;
            this.txtGymKanji.Name = "txtGymKanji";
            this.txtGymKanji.Size = new System.Drawing.Size(390, 26);
            this.txtGymKanji.TabIndex = 100001;
            this.txtGymKanji.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // txtGymKana
            // 
            this.txtGymKana.CharCheck = true;
            this.txtGymKana.EntryMode = CommonClass.ENTRYMODE.IMEOFF_KANA;
            this.txtGymKana.KanaLock = false;
            this.txtGymKana.Location = new System.Drawing.Point(225, 62);
            this.txtGymKana.MaxLength = 40;
            this.txtGymKana.Name = "txtGymKana";
            this.txtGymKana.Size = new System.Drawing.Size(390, 26);
            this.txtGymKana.TabIndex = 100000;
            this.txtGymKana.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // lblGymKanji
            // 
            this.lblGymKanji.AutoSize = true;
            this.lblGymKanji.Location = new System.Drawing.Point(25, 103);
            this.lblGymKanji.Name = "lblGymKanji";
            this.lblGymKanji.Size = new System.Drawing.Size(66, 19);
            this.lblGymKanji.TabIndex = 100023;
            this.lblGymKanji.Text = "業務名";
            // 
            // lblGymKana
            // 
            this.lblGymKana.AutoSize = true;
            this.lblGymKana.Location = new System.Drawing.Point(25, 65);
            this.lblGymKana.Name = "lblGymKana";
            this.lblGymKana.Size = new System.Drawing.Size(95, 19);
            this.lblGymKana.TabIndex = 100022;
            this.lblGymKana.Text = "業務名カナ";
            // 
            // lblGymID
            // 
            this.lblGymID.AutoSize = true;
            this.lblGymID.Location = new System.Drawing.Point(25, 29);
            this.lblGymID.Name = "lblGymID";
            this.lblGymID.Size = new System.Drawing.Size(85, 19);
            this.lblGymID.TabIndex = 100021;
            this.lblGymID.Text = "業務番号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 19);
            this.label1.TabIndex = 100023;
            this.label1.Text = "補正データ保存日数";
            // 
            // txtSaveDay
            // 
            this.txtSaveDay.Location = new System.Drawing.Point(225, 140);
            this.txtSaveDay.MaxLength = 4;
            this.txtSaveDay.Name = "txtSaveDay";
            this.txtSaveDay.Size = new System.Drawing.Size(61, 26);
            this.txtSaveDay.TabIndex = 100041;
            this.txtSaveDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSaveDay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // GymParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "GymParamForm";
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblGymID2;
        private CommonClass.KanaTextBox txtGymKanji;
        private CommonClass.KanaTextBox txtGymKana;
        private System.Windows.Forms.Label lblGymKanji;
        private System.Windows.Forms.Label lblGymKana;
        private System.Windows.Forms.Label lblGymID;
        private CommonClass.NumTextBox2 txtSaveDay;
        private System.Windows.Forms.Label label1;
    }
}
