namespace SearchTeiseiRireki
{
    partial class SampleForm
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
            this.txtGymId = new System.Windows.Forms.TextBox();
            this.btnGymSearch = new System.Windows.Forms.Button();
            this.ktbGymKana = new CommonClass.KanaTextBox();
            this.jtbGymKanji = new CommonClass.KanaTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbGymTyushutu = new System.Windows.Forms.RadioButton();
            this.rdbGymNuryoku = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbDataNotInput = new System.Windows.Forms.RadioButton();
            this.rdbDataNotComp = new System.Windows.Forms.RadioButton();
            this.rdbDataAll = new System.Windows.Forms.RadioButton();
            this.contentsPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.groupBox2);
            this.contentsPanel.Controls.Add(this.groupBox1);
            this.contentsPanel.Controls.Add(this.btnGymSearch);
            this.contentsPanel.Controls.Add(this.jtbGymKanji);
            this.contentsPanel.Controls.Add(this.ktbGymKana);
            this.contentsPanel.Controls.Add(this.txtGymId);
            // 
            // txtGymId
            // 
            this.txtGymId.Location = new System.Drawing.Point(163, 113);
            this.txtGymId.Name = "txtGymId";
            this.txtGymId.Size = new System.Drawing.Size(100, 26);
            this.txtGymId.TabIndex = 1;
            this.txtGymId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.txtGymId.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            // 
            // btnGymSearch
            // 
            this.btnGymSearch.Location = new System.Drawing.Point(336, 103);
            this.btnGymSearch.Name = "btnGymSearch";
            this.btnGymSearch.Size = new System.Drawing.Size(127, 45);
            this.btnGymSearch.TabIndex = 2;
            this.btnGymSearch.Text = "button1";
            this.btnGymSearch.UseVisualStyleBackColor = true;
            this.btnGymSearch.Click += new System.EventHandler(this.btnGymSearch_Click);
            // 
            // ktbGymKana
            // 
            this.ktbGymKana.Location = new System.Drawing.Point(163, 168);
            this.ktbGymKana.Name = "ktbGymKana";
            this.ktbGymKana.Size = new System.Drawing.Size(100, 26);
            this.ktbGymKana.TabIndex = 3;
            this.ktbGymKana.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.ktbGymKana.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            // 
            // jtbGymKanji
            // 
            this.jtbGymKanji.Location = new System.Drawing.Point(163, 225);
            this.jtbGymKanji.Name = "jtbGymKanji";
            this.jtbGymKanji.Size = new System.Drawing.Size(100, 26);
            this.jtbGymKanji.TabIndex = 4;
            this.jtbGymKanji.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.jtbGymKanji.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbGymTyushutu);
            this.groupBox1.Controls.Add(this.rdbGymNuryoku);
            this.groupBox1.Location = new System.Drawing.Point(163, 286);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(397, 99);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // rdbGymTyushutu
            // 
            this.rdbGymTyushutu.AutoSize = true;
            this.rdbGymTyushutu.Location = new System.Drawing.Point(188, 41);
            this.rdbGymTyushutu.Name = "rdbGymTyushutu";
            this.rdbGymTyushutu.Size = new System.Drawing.Size(132, 23);
            this.rdbGymTyushutu.TabIndex = 1;
            this.rdbGymTyushutu.TabStop = true;
            this.rdbGymTyushutu.Text = "radioButton2";
            this.rdbGymTyushutu.UseVisualStyleBackColor = true;
            this.rdbGymTyushutu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.rdbGymTyushutu.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            // 
            // rdbGymNuryoku
            // 
            this.rdbGymNuryoku.AutoSize = true;
            this.rdbGymNuryoku.Checked = true;
            this.rdbGymNuryoku.Location = new System.Drawing.Point(28, 41);
            this.rdbGymNuryoku.Name = "rdbGymNuryoku";
            this.rdbGymNuryoku.Size = new System.Drawing.Size(132, 23);
            this.rdbGymNuryoku.TabIndex = 0;
            this.rdbGymNuryoku.TabStop = true;
            this.rdbGymNuryoku.Text = "radioButton1";
            this.rdbGymNuryoku.UseVisualStyleBackColor = true;
            this.rdbGymNuryoku.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.rdbGymNuryoku.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbDataNotInput);
            this.groupBox2.Controls.Add(this.rdbDataNotComp);
            this.groupBox2.Controls.Add(this.rdbDataAll);
            this.groupBox2.Location = new System.Drawing.Point(163, 391);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(550, 99);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // rdbDataNotInput
            // 
            this.rdbDataNotInput.AutoSize = true;
            this.rdbDataNotInput.Location = new System.Drawing.Point(348, 41);
            this.rdbDataNotInput.Name = "rdbDataNotInput";
            this.rdbDataNotInput.Size = new System.Drawing.Size(132, 23);
            this.rdbDataNotInput.TabIndex = 2;
            this.rdbDataNotInput.TabStop = true;
            this.rdbDataNotInput.Text = "radioButton2";
            this.rdbDataNotInput.UseVisualStyleBackColor = true;
            this.rdbDataNotInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.rdbDataNotInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            // 
            // rdbDataNotComp
            // 
            this.rdbDataNotComp.AutoSize = true;
            this.rdbDataNotComp.Location = new System.Drawing.Point(188, 41);
            this.rdbDataNotComp.Name = "rdbDataNotComp";
            this.rdbDataNotComp.Size = new System.Drawing.Size(132, 23);
            this.rdbDataNotComp.TabIndex = 1;
            this.rdbDataNotComp.TabStop = true;
            this.rdbDataNotComp.Text = "radioButton2";
            this.rdbDataNotComp.UseVisualStyleBackColor = true;
            this.rdbDataNotComp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.rdbDataNotComp.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            // 
            // rdbDataAll
            // 
            this.rdbDataAll.AutoSize = true;
            this.rdbDataAll.Checked = true;
            this.rdbDataAll.Location = new System.Drawing.Point(28, 41);
            this.rdbDataAll.Name = "rdbDataAll";
            this.rdbDataAll.Size = new System.Drawing.Size(132, 23);
            this.rdbDataAll.TabIndex = 0;
            this.rdbDataAll.TabStop = true;
            this.rdbDataAll.Text = "radioButton1";
            this.rdbDataAll.UseVisualStyleBackColor = true;
            this.rdbDataAll.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.rdbDataAll.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            // 
            // SampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SampleForm";
            this.Load += new System.EventHandler(this.SampleForm_Load);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtGymId;
        private System.Windows.Forms.Button btnGymSearch;
        private CommonClass.KanaTextBox jtbGymKanji;
        private CommonClass.KanaTextBox ktbGymKana;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbGymTyushutu;
        private System.Windows.Forms.RadioButton rdbGymNuryoku;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbDataNotInput;
        private System.Windows.Forms.RadioButton rdbDataNotComp;
        private System.Windows.Forms.RadioButton rdbDataAll;
    }
}
