namespace CorrectInput
{
    partial class VerifyUnMatch
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
            this.lblVerifyUnMatch = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblEntryMsg = new System.Windows.Forms.Label();
            this.lblVerifyMsg = new System.Windows.Forms.Label();
            this.btnFixed = new System.Windows.Forms.Button();
            this.lblParam = new System.Windows.Forms.Label();
            this.lblEntry = new System.Windows.Forms.Label();
            this.lblVerify = new System.Windows.Forms.Label();
            this.lblEntryValue = new System.Windows.Forms.Label();
            this.lblVerifyValue = new System.Windows.Forms.Label();
            this.lblParamName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblVerifyUnMatch
            // 
            this.lblVerifyUnMatch.AutoSize = true;
            this.lblVerifyUnMatch.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblVerifyUnMatch.Location = new System.Drawing.Point(76, 9);
            this.lblVerifyUnMatch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVerifyUnMatch.Name = "lblVerifyUnMatch";
            this.lblVerifyUnMatch.Size = new System.Drawing.Size(294, 24);
            this.lblVerifyUnMatch.TabIndex = 1;
            this.lblVerifyUnMatch.Text = "ベリファイ相違が発生しました。";
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBack.Location = new System.Drawing.Point(215, 209);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 50);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "F1：戻る";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnBack.KeyUp += new System.Windows.Forms.KeyEventHandler(this.VerifyUnMatch_KeyUp);
            // 
            // lblEntryMsg
            // 
            this.lblEntryMsg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryMsg.AutoSize = true;
            this.lblEntryMsg.Location = new System.Drawing.Point(13, 147);
            this.lblEntryMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEntryMsg.Name = "lblEntryMsg";
            this.lblEntryMsg.Size = new System.Drawing.Size(416, 16);
            this.lblEntryMsg.TabIndex = 6;
            this.lblEntryMsg.Text = "エントリー値が正しい場合は『戻る』を押し再度、入力して下さい。";
            // 
            // lblVerifyMsg
            // 
            this.lblVerifyMsg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVerifyMsg.AutoSize = true;
            this.lblVerifyMsg.Location = new System.Drawing.Point(13, 172);
            this.lblVerifyMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVerifyMsg.Name = "lblVerifyMsg";
            this.lblVerifyMsg.Size = new System.Drawing.Size(330, 16);
            this.lblVerifyMsg.TabIndex = 7;
            this.lblVerifyMsg.Text = "ベリファイ値が正しい場合は『確定』を押して下さい。";
            // 
            // btnFixed
            // 
            this.btnFixed.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnFixed.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFixed.Location = new System.Drawing.Point(321, 209);
            this.btnFixed.Name = "btnFixed";
            this.btnFixed.Size = new System.Drawing.Size(100, 50);
            this.btnFixed.TabIndex = 8;
            this.btnFixed.Text = "F12：確定";
            this.btnFixed.UseVisualStyleBackColor = true;
            this.btnFixed.Click += new System.EventHandler(this.btnFixed_Click);
            this.btnFixed.KeyUp += new System.Windows.Forms.KeyEventHandler(this.VerifyUnMatch_KeyUp);
            // 
            // lblParam
            // 
            this.lblParam.AutoSize = true;
            this.lblParam.Location = new System.Drawing.Point(13, 49);
            this.lblParam.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblParam.Name = "lblParam";
            this.lblParam.Size = new System.Drawing.Size(56, 16);
            this.lblParam.TabIndex = 6;
            this.lblParam.Text = "項目名";
            // 
            // lblEntry
            // 
            this.lblEntry.AutoSize = true;
            this.lblEntry.Location = new System.Drawing.Point(13, 80);
            this.lblEntry.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEntry.Name = "lblEntry";
            this.lblEntry.Size = new System.Drawing.Size(65, 16);
            this.lblEntry.TabIndex = 9;
            this.lblEntry.Text = "エントリー";
            // 
            // lblVerify
            // 
            this.lblVerify.AutoSize = true;
            this.lblVerify.Location = new System.Drawing.Point(13, 110);
            this.lblVerify.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVerify.Name = "lblVerify";
            this.lblVerify.Size = new System.Drawing.Size(62, 16);
            this.lblVerify.TabIndex = 10;
            this.lblVerify.Text = "ベリファイ";
            // 
            // lblEntryValue
            // 
            this.lblEntryValue.AutoSize = true;
            this.lblEntryValue.Location = new System.Drawing.Point(86, 80);
            this.lblEntryValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEntryValue.Name = "lblEntryValue";
            this.lblEntryValue.Size = new System.Drawing.Size(65, 16);
            this.lblEntryValue.TabIndex = 11;
            this.lblEntryValue.Text = "エントリー";
            this.lblEntryValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblVerifyValue
            // 
            this.lblVerifyValue.AutoSize = true;
            this.lblVerifyValue.Location = new System.Drawing.Point(86, 110);
            this.lblVerifyValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVerifyValue.Name = "lblVerifyValue";
            this.lblVerifyValue.Size = new System.Drawing.Size(62, 16);
            this.lblVerifyValue.TabIndex = 12;
            this.lblVerifyValue.Text = "ベリファイ";
            this.lblVerifyValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblParamName
            // 
            this.lblParamName.AutoSize = true;
            this.lblParamName.Location = new System.Drawing.Point(86, 49);
            this.lblParamName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblParamName.Name = "lblParamName";
            this.lblParamName.Size = new System.Drawing.Size(56, 16);
            this.lblParamName.TabIndex = 13;
            this.lblParamName.Text = "項目名";
            // 
            // VerifyUnMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.ClientSize = new System.Drawing.Size(433, 264);
            this.Controls.Add(this.lblParamName);
            this.Controls.Add(this.lblVerifyValue);
            this.Controls.Add(this.lblEntryValue);
            this.Controls.Add(this.lblVerify);
            this.Controls.Add(this.lblEntry);
            this.Controls.Add(this.btnFixed);
            this.Controls.Add(this.lblVerifyMsg);
            this.Controls.Add(this.lblParam);
            this.Controls.Add(this.lblEntryMsg);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblVerifyUnMatch);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Location = new System.Drawing.Point(500, 400);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VerifyUnMatch";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ベリファイ相違";
            this.Load += new System.EventHandler(this.VerifyUnMatch_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.VerifyUnMatch_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVerifyUnMatch;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblEntryMsg;
        private System.Windows.Forms.Label lblVerifyMsg;
        private System.Windows.Forms.Button btnFixed;
        private System.Windows.Forms.Label lblParam;
        private System.Windows.Forms.Label lblEntry;
        private System.Windows.Forms.Label lblVerify;
        private System.Windows.Forms.Label lblEntryValue;
        private System.Windows.Forms.Label lblVerifyValue;
        private System.Windows.Forms.Label lblParamName;
    }
}
