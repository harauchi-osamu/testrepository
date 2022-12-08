namespace CorrectInput
{
	partial class SearchDialog
    {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.ColumnHeader clKey;
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lvBatList = new System.Windows.Forms.ListView();
            this.clHeadCd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clHeadKanji = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clHeadKana = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblDspName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKeyWord = new System.Windows.Forms.TextBox();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(5, 578);
            this.lblStatus.Size = new System.Drawing.Size(530, 31);
            // 
            // clKey
            // 
            clKey.Text = "clKey";
            clKey.Width = 0;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnOK.Location = new System.Drawing.Point(424, 521);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 50);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "F12：確定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(14, 521);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 50);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "F1：戻る";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lvBatList
            // 
            this.lvBatList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.clHeadCd,
            this.clHeadKanji,
            this.clHeadKana});
            this.lvBatList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvBatList.FullRowSelect = true;
            this.lvBatList.GridLines = true;
            this.lvBatList.HideSelection = false;
            this.lvBatList.Location = new System.Drawing.Point(14, 78);
            this.lvBatList.MultiSelect = false;
            this.lvBatList.Name = "lvBatList";
            this.lvBatList.Size = new System.Drawing.Size(510, 437);
            this.lvBatList.TabIndex = 2;
            this.lvBatList.TabStop = false;
            this.lvBatList.UseCompatibleStateImageBehavior = false;
            this.lvBatList.View = System.Windows.Forms.View.Details;
            this.lvBatList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            // 
            // clHeadCd
            // 
            this.clHeadCd.Text = "コード";
            this.clHeadCd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clHeadCd.Width = 80;
            // 
            // clHeadKanji
            // 
            this.clHeadKanji.Text = "漢字名称";
            this.clHeadKanji.Width = 200;
            // 
            // clHeadKana
            // 
            this.clHeadKana.Text = "カナ名称";
            this.clHeadKana.Width = 200;
            // 
            // lblDspName
            // 
            this.lblDspName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDspName.Location = new System.Drawing.Point(5, 5);
            this.lblDspName.Name = "lblDspName";
            this.lblDspName.Size = new System.Drawing.Size(530, 30);
            this.lblDspName.TabIndex = 8;
            this.lblDspName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "名称";
            // 
            // txtKeyWord
            // 
            this.txtKeyWord.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtKeyWord.Location = new System.Drawing.Point(63, 46);
            this.txtKeyWord.MaxLength = 15;
            this.txtKeyWord.Name = "txtKeyWord";
            this.txtKeyWord.Size = new System.Drawing.Size(461, 26);
            this.txtKeyWord.TabIndex = 1;
            this.txtKeyWord.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            // 
            // SearchDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 613);
            this.Controls.Add(this.txtKeyWord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDspName);
            this.Controls.Add(this.lvBatList);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClose);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SearchDialog";
            this.Text = " ";
            this.Shown += new System.EventHandler(this.SearchDialog_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.Controls.SetChildIndex(this.lvBatList, 0);
            this.Controls.SetChildIndex(this.lblDspName, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtKeyWord, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListView lvBatList;
        private System.Windows.Forms.ColumnHeader clHeadCd;
        private System.Windows.Forms.ColumnHeader clHeadKanji;
        private System.Windows.Forms.ColumnHeader clHeadKana;
        private System.Windows.Forms.Label lblDspName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKeyWord;
    }
}