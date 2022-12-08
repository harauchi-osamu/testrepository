namespace CorrectInput
{
    partial class ProofForm
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
            this.Label1 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblKensu = new System.Windows.Forms.Label();
            this.lblKingaku = new System.Windows.Forms.Label();
            this.lblBatCnt = new System.Windows.Forms.Label();
            this.lblBatKingaku = new System.Windows.Forms.Label();
            this.lblMeiKingaku = new System.Windows.Forms.Label();
            this.lblMeiCnt = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.lblDiffKingaku = new System.Windows.Forms.Label();
            this.lblDiffCnt = new System.Windows.Forms.Label();
            this.btnMList = new System.Windows.Forms.Button();
            this.lblBatId = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblLine1 = new System.Windows.Forms.Label();
            this.lblSai = new System.Windows.Forms.Label();
            this.lblMeisai = new System.Windows.Forms.Label();
            this.lblSyokei = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblLine2 = new System.Windows.Forms.Label();
            this.lblLine3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label1.Location = new System.Drawing.Point(14, 46);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(56, 16);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "不照合";
            // 
            // btnBack
            // 
            this.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBack.Location = new System.Drawing.Point(339, 199);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 50);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "F1：中断";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnBack.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ProofForm_KeyUp);
            // 
            // lblKensu
            // 
            this.lblKensu.AutoSize = true;
            this.lblKensu.Location = new System.Drawing.Point(11, 10);
            this.lblKensu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKensu.Name = "lblKensu";
            this.lblKensu.Size = new System.Drawing.Size(40, 16);
            this.lblKensu.TabIndex = 6;
            this.lblKensu.Text = "件数";
            // 
            // lblKingaku
            // 
            this.lblKingaku.AutoSize = true;
            this.lblKingaku.Location = new System.Drawing.Point(11, 10);
            this.lblKingaku.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKingaku.Name = "lblKingaku";
            this.lblKingaku.Size = new System.Drawing.Size(40, 16);
            this.lblKingaku.TabIndex = 9;
            this.lblKingaku.Text = "金額";
            // 
            // lblBatCnt
            // 
            this.lblBatCnt.Location = new System.Drawing.Point(70, 10);
            this.lblBatCnt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatCnt.Name = "lblBatCnt";
            this.lblBatCnt.Size = new System.Drawing.Size(140, 16);
            this.lblBatCnt.TabIndex = 11;
            this.lblBatCnt.Text = "99,999";
            this.lblBatCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBatKingaku
            // 
            this.lblBatKingaku.Location = new System.Drawing.Point(70, 10);
            this.lblBatKingaku.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatKingaku.Name = "lblBatKingaku";
            this.lblBatKingaku.Size = new System.Drawing.Size(140, 16);
            this.lblBatKingaku.TabIndex = 12;
            this.lblBatKingaku.Text = "999,999,999,999,999";
            this.lblBatKingaku.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMeiKingaku
            // 
            this.lblMeiKingaku.Location = new System.Drawing.Point(226, 10);
            this.lblMeiKingaku.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMeiKingaku.Name = "lblMeiKingaku";
            this.lblMeiKingaku.Size = new System.Drawing.Size(140, 16);
            this.lblMeiKingaku.TabIndex = 15;
            this.lblMeiKingaku.Text = "999,999,999,999,999";
            this.lblMeiKingaku.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMeiCnt
            // 
            this.lblMeiCnt.Location = new System.Drawing.Point(226, 10);
            this.lblMeiCnt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMeiCnt.Name = "lblMeiCnt";
            this.lblMeiCnt.Size = new System.Drawing.Size(140, 16);
            this.lblMeiCnt.TabIndex = 14;
            this.lblMeiCnt.Text = "99,999";
            this.lblMeiCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label2.Location = new System.Drawing.Point(15, 13);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(100, 16);
            this.Label2.TabIndex = 16;
            this.Label2.Text = "バッチ番号 　：";
            // 
            // lblDiffKingaku
            // 
            this.lblDiffKingaku.Location = new System.Drawing.Point(388, 10);
            this.lblDiffKingaku.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiffKingaku.Name = "lblDiffKingaku";
            this.lblDiffKingaku.Size = new System.Drawing.Size(140, 16);
            this.lblDiffKingaku.TabIndex = 19;
            this.lblDiffKingaku.Text = "999,999,999,999,999";
            this.lblDiffKingaku.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiffCnt
            // 
            this.lblDiffCnt.Location = new System.Drawing.Point(388, 10);
            this.lblDiffCnt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiffCnt.Name = "lblDiffCnt";
            this.lblDiffCnt.Size = new System.Drawing.Size(140, 16);
            this.lblDiffCnt.TabIndex = 18;
            this.lblDiffCnt.Text = "99,999";
            this.lblDiffCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnMList
            // 
            this.btnMList.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnMList.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMList.Location = new System.Drawing.Point(452, 199);
            this.btnMList.Name = "btnMList";
            this.btnMList.Size = new System.Drawing.Size(100, 50);
            this.btnMList.TabIndex = 0;
            this.btnMList.Text = "F12：継続";
            this.btnMList.UseVisualStyleBackColor = true;
            this.btnMList.Click += new System.EventHandler(this.btnMList_Click);
            this.btnMList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ProofForm_KeyUp);
            // 
            // lblBatId
            // 
            this.lblBatId.AutoSize = true;
            this.lblBatId.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBatId.Location = new System.Drawing.Point(114, 13);
            this.lblBatId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatId.Name = "lblBatId";
            this.lblBatId.Size = new System.Drawing.Size(56, 16);
            this.lblBatId.TabIndex = 16;
            this.lblBatId.Text = "999999";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblKensu);
            this.panel1.Controls.Add(this.lblBatCnt);
            this.panel1.Controls.Add(this.lblDiffCnt);
            this.panel1.Controls.Add(this.lblMeiCnt);
            this.panel1.Location = new System.Drawing.Point(14, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(538, 37);
            this.panel1.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblKingaku);
            this.panel2.Controls.Add(this.lblBatKingaku);
            this.panel2.Controls.Add(this.lblMeiKingaku);
            this.panel2.Controls.Add(this.lblDiffKingaku);
            this.panel2.Location = new System.Drawing.Point(14, 147);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(538, 37);
            this.panel2.TabIndex = 20;
            // 
            // lblLine1
            // 
            this.lblLine1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLine1.Location = new System.Drawing.Point(76, 74);
            this.lblLine1.Name = "lblLine1";
            this.lblLine1.Size = new System.Drawing.Size(1, 109);
            this.lblLine1.TabIndex = 21;
            // 
            // lblSai
            // 
            this.lblSai.AutoSize = true;
            this.lblSai.Location = new System.Drawing.Point(409, 10);
            this.lblSai.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSai.Name = "lblSai";
            this.lblSai.Size = new System.Drawing.Size(94, 16);
            this.lblSai.TabIndex = 17;
            this.lblSai.Text = "バッチ - 明細";
            // 
            // lblMeisai
            // 
            this.lblMeisai.AutoSize = true;
            this.lblMeisai.Location = new System.Drawing.Point(278, 10);
            this.lblMeisai.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMeisai.Name = "lblMeisai";
            this.lblMeisai.Size = new System.Drawing.Size(40, 16);
            this.lblMeisai.TabIndex = 13;
            this.lblMeisai.Text = "明細";
            // 
            // lblSyokei
            // 
            this.lblSyokei.AutoSize = true;
            this.lblSyokei.Location = new System.Drawing.Point(118, 10);
            this.lblSyokei.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSyokei.Name = "lblSyokei";
            this.lblSyokei.Size = new System.Drawing.Size(44, 16);
            this.lblSyokei.TabIndex = 10;
            this.lblSyokei.Text = "バッチ";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblSyokei);
            this.panel3.Controls.Add(this.lblMeisai);
            this.panel3.Controls.Add(this.lblSai);
            this.panel3.Location = new System.Drawing.Point(14, 75);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(538, 37);
            this.panel3.TabIndex = 20;
            // 
            // lblLine2
            // 
            this.lblLine2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLine2.Location = new System.Drawing.Point(230, 74);
            this.lblLine2.Name = "lblLine2";
            this.lblLine2.Size = new System.Drawing.Size(1, 109);
            this.lblLine2.TabIndex = 21;
            // 
            // lblLine3
            // 
            this.lblLine3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLine3.Location = new System.Drawing.Point(388, 75);
            this.lblLine3.Name = "lblLine3";
            this.lblLine3.Size = new System.Drawing.Size(1, 109);
            this.lblLine3.TabIndex = 21;
            // 
            // ProofForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.ClientSize = new System.Drawing.Size(564, 260);
            this.ControlBox = false;
            this.Controls.Add(this.lblLine3);
            this.Controls.Add(this.lblLine2);
            this.Controls.Add(this.lblLine1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnMList);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.lblBatId);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.Label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Location = new System.Drawing.Point(500, 400);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProofForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.PrfResult_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ProofForm_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblKensu;
        private System.Windows.Forms.Label lblKingaku;
        private System.Windows.Forms.Label lblBatCnt;
        private System.Windows.Forms.Label lblBatKingaku;
        private System.Windows.Forms.Label lblMeiKingaku;
        private System.Windows.Forms.Label lblMeiCnt;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label lblDiffKingaku;
        private System.Windows.Forms.Label lblDiffCnt;
        private System.Windows.Forms.Button btnMList;
        private System.Windows.Forms.Label lblBatId;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblLine1;
        private System.Windows.Forms.Label lblSai;
        private System.Windows.Forms.Label lblMeisai;
        private System.Windows.Forms.Label lblSyokei;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblLine2;
        private System.Windows.Forms.Label lblLine3;
    }
}
