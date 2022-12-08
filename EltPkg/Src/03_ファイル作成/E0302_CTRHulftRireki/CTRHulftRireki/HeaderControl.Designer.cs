namespace CTRHulftRireki
{
    partial class HeaderControl
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
            this.components = new System.ComponentModel.Container();
            this.lblMiHaishinCnt = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMiTorikomiCnt = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSendErr = new System.Windows.Forms.Label();
            this.btnDelSendErr = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblRecvErr = new System.Windows.Forms.Label();
            this.btnDelRecvErr = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblImportErr = new System.Windows.Forms.Label();
            this.chkAutoRefresh = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timerUpdateList = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMiHaishinCnt
            // 
            this.lblMiHaishinCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMiHaishinCnt.Location = new System.Drawing.Point(87, 6);
            this.lblMiHaishinCnt.Name = "lblMiHaishinCnt";
            this.lblMiHaishinCnt.Size = new System.Drawing.Size(90, 26);
            this.lblMiHaishinCnt.TabIndex = 100006;
            this.lblMiHaishinCnt.Text = "0";
            this.lblMiHaishinCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(8, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 26);
            this.label9.TabIndex = 100007;
            this.label9.Text = "未配信";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(176, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 26);
            this.label1.TabIndex = 100007;
            this.label1.Text = "未取込";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMiTorikomiCnt
            // 
            this.lblMiTorikomiCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMiTorikomiCnt.Location = new System.Drawing.Point(255, 6);
            this.lblMiTorikomiCnt.Name = "lblMiTorikomiCnt";
            this.lblMiTorikomiCnt.Size = new System.Drawing.Size(90, 26);
            this.lblMiTorikomiCnt.TabIndex = 100006;
            this.lblMiTorikomiCnt.Text = "0";
            this.lblMiTorikomiCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(363, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 26);
            this.label3.TabIndex = 100007;
            this.label3.Text = "配信新規エラー";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSendErr
            // 
            this.lblSendErr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSendErr.Location = new System.Drawing.Point(492, 6);
            this.lblSendErr.Name = "lblSendErr";
            this.lblSendErr.Size = new System.Drawing.Size(30, 26);
            this.lblSendErr.TabIndex = 100007;
            this.lblSendErr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDelSendErr
            // 
            this.btnDelSendErr.BackColor = System.Drawing.Color.PeachPuff;
            this.btnDelSendErr.Location = new System.Drawing.Point(528, 4);
            this.btnDelSendErr.Name = "btnDelSendErr";
            this.btnDelSendErr.Size = new System.Drawing.Size(100, 30);
            this.btnDelSendErr.TabIndex = 1;
            this.btnDelSendErr.TabStop = false;
            this.btnDelSendErr.Text = "エラー消込";
            this.btnDelSendErr.UseVisualStyleBackColor = false;
            this.btnDelSendErr.Click += new System.EventHandler(this.btnDelSendErr_Click);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(645, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 26);
            this.label6.TabIndex = 100007;
            this.label6.Text = "集信新規エラー";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecvErr
            // 
            this.lblRecvErr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRecvErr.Location = new System.Drawing.Point(774, 6);
            this.lblRecvErr.Name = "lblRecvErr";
            this.lblRecvErr.Size = new System.Drawing.Size(30, 26);
            this.lblRecvErr.TabIndex = 100007;
            this.lblRecvErr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDelRecvErr
            // 
            this.btnDelRecvErr.BackColor = System.Drawing.Color.PeachPuff;
            this.btnDelRecvErr.Location = new System.Drawing.Point(810, 4);
            this.btnDelRecvErr.Name = "btnDelRecvErr";
            this.btnDelRecvErr.Size = new System.Drawing.Size(100, 30);
            this.btnDelRecvErr.TabIndex = 2;
            this.btnDelRecvErr.TabStop = false;
            this.btnDelRecvErr.Text = "エラー消込";
            this.btnDelRecvErr.UseVisualStyleBackColor = false;
            this.btnDelRecvErr.Click += new System.EventHandler(this.btnDelRecvErr_Click);
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(926, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 26);
            this.label5.TabIndex = 100007;
            this.label5.Text = "取込エラー";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblImportErr
            // 
            this.lblImportErr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblImportErr.Location = new System.Drawing.Point(1025, 6);
            this.lblImportErr.Name = "lblImportErr";
            this.lblImportErr.Size = new System.Drawing.Size(30, 26);
            this.lblImportErr.TabIndex = 100007;
            this.lblImportErr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkAutoRefresh
            // 
            this.chkAutoRefresh.AutoSize = true;
            this.chkAutoRefresh.Location = new System.Drawing.Point(12, 1);
            this.chkAutoRefresh.Name = "chkAutoRefresh";
            this.chkAutoRefresh.Size = new System.Drawing.Size(180, 23);
            this.chkAutoRefresh.TabIndex = 0;
            this.chkAutoRefresh.Text = "一覧表示自動更新";
            this.chkAutoRefresh.UseVisualStyleBackColor = true;
            this.chkAutoRefresh.CheckedChanged += new System.EventHandler(this.chkAutoRefresh_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chkAutoRefresh);
            this.panel1.Location = new System.Drawing.Point(1054, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 26);
            this.panel1.TabIndex = 100009;
            // 
            // HeaderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnDelRecvErr);
            this.Controls.Add(this.btnDelSendErr);
            this.Controls.Add(this.lblMiTorikomiCnt);
            this.Controls.Add(this.lblMiHaishinCnt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblRecvErr);
            this.Controls.Add(this.lblImportErr);
            this.Controls.Add(this.lblSendErr);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "HeaderControl";
            this.Size = new System.Drawing.Size(1260, 40);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMiHaishinCnt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMiTorikomiCnt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSendErr;
        private System.Windows.Forms.Button btnDelSendErr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRecvErr;
        private System.Windows.Forms.Button btnDelRecvErr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblImportErr;
        private System.Windows.Forms.CheckBox chkAutoRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timerUpdateList;
    }
}
