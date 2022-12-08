namespace ParamMaint
{
	partial class ItemMstDialog
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
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Size = new System.Drawing.Size(0, 0);
            this.lblStatus.Visible = false;
            // 
            // clKey
            // 
            clKey.Text = "clKey";
            clKey.Width = 0;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnOK.Location = new System.Drawing.Point(311, 442);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 50);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "確定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(6, 442);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 50);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "終了";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lvBatList
            // 
            this.lvBatList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.clHeadCd,
            this.clHeadKanji});
            this.lvBatList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvBatList.FullRowSelect = true;
            this.lvBatList.GridLines = true;
            this.lvBatList.HideSelection = false;
            this.lvBatList.Location = new System.Drawing.Point(6, 7);
            this.lvBatList.MultiSelect = false;
            this.lvBatList.Name = "lvBatList";
            this.lvBatList.Size = new System.Drawing.Size(405, 430);
            this.lvBatList.TabIndex = 1;
            this.lvBatList.TabStop = false;
            this.lvBatList.UseCompatibleStateImageBehavior = false;
            this.lvBatList.View = System.Windows.Forms.View.Details;
            this.lvBatList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.lvBatList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvBatList_MouseDoubleClick);
            // 
            // clHeadCd
            // 
            this.clHeadCd.Text = "項目番号";
            this.clHeadCd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clHeadCd.Width = 80;
            // 
            // clHeadKanji
            // 
            this.clHeadKanji.Text = "項目名称";
            this.clHeadKanji.Width = 300;
            // 
            // ItemMstDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 498);
            this.Controls.Add(this.lvBatList);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClose);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ItemMstDialog";
            this.Text = " 項目選択";
            this.Shown += new System.EventHandler(this.SearchDialog_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.Controls.SetChildIndex(this.lvBatList, 0);
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListView lvBatList;
        private System.Windows.Forms.ColumnHeader clHeadCd;
        private System.Windows.Forms.ColumnHeader clHeadKanji;
    }
}