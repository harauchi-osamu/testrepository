namespace SearchICReqResult
{
	partial class SearchReqResultList
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
            System.Windows.Forms.ColumnHeader clKey;
            this.lvBatList = new System.Windows.Forms.ListView();
            this.clREQ_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clREQ_TIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clREQ_TXT_NAME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clRET_FILE_CHK = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clRET_PROC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clRET_MAKE_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clRET_MAKE_TIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clRET_COUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.dtREQ_DATE = new CommonClass.DTextBox2();
            this.lblOperatedate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.dtREQ_DATE);
            this.contentsPanel.Controls.Add(this.lblOperatedate);
            this.contentsPanel.Controls.Add(this.label3);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.Add(this.lvBatList);
            this.contentsPanel.Controls.SetChildIndex(this.lvBatList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label3, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblOperatedate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtREQ_DATE, 0);
            // 
            // clKey
            // 
            clKey.Text = "clKey";
            clKey.Width = 0;
            // 
            // lvBatList
            // 
            this.lvBatList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvBatList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.clREQ_DATE,
            this.clREQ_TIME,
            this.clREQ_TXT_NAME,
            this.clRET_FILE_CHK,
            this.clRET_PROC,
            this.clRET_MAKE_DATE,
            this.clRET_MAKE_TIME,
            this.clRET_COUNT});
            this.lvBatList.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvBatList.FullRowSelect = true;
            this.lvBatList.GridLines = true;
            this.lvBatList.HideSelection = false;
            this.lvBatList.Location = new System.Drawing.Point(30, 113);
            this.lvBatList.MultiSelect = false;
            this.lvBatList.Name = "lvBatList";
            this.lvBatList.Size = new System.Drawing.Size(1214, 687);
            this.lvBatList.TabIndex = 5;
            this.lvBatList.TabStop = false;
            this.lvBatList.UseCompatibleStateImageBehavior = false;
            this.lvBatList.View = System.Windows.Forms.View.Details;
            this.lvBatList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lv_ColumnWidthChanging);
            this.lvBatList.DoubleClick += new System.EventHandler(this.lv_DoubleClick);
            this.lvBatList.Enter += new System.EventHandler(this.List_Enter);
            this.lvBatList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lv_KeyDown);
            // 
            // clREQ_DATE
            // 
            this.clREQ_DATE.Text = "要求日";
            this.clREQ_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clREQ_DATE.Width = 82;
            // 
            // clREQ_TIME
            // 
            this.clREQ_TIME.Text = "要求時間";
            this.clREQ_TIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clREQ_TIME.Width = 73;
            // 
            // clREQ_TXT_NAME
            // 
            this.clREQ_TXT_NAME.Text = "持帰要求テキストファイル名";
            this.clREQ_TXT_NAME.Width = 260;
            // 
            // clRET_FILE_CHK
            // 
            this.clRET_FILE_CHK.Text = "ファイルチェック結果";
            this.clRET_FILE_CHK.Width = 232;
            // 
            // clRET_PROC
            // 
            this.clRET_PROC.Text = "処理結果";
            this.clRET_PROC.Width = 232;
            // 
            // clRET_MAKE_DATE
            // 
            this.clRET_MAKE_DATE.Text = "結果取込日";
            this.clRET_MAKE_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clRET_MAKE_DATE.Width = 95;
            // 
            // clRET_MAKE_TIME
            // 
            this.clRET_MAKE_TIME.Text = "結果取込時間";
            this.clRET_MAKE_TIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clRET_MAKE_TIME.Width = 103;
            // 
            // clRET_COUNT
            // 
            this.clRET_COUNT.Text = "対象レコード件数";
            this.clRET_COUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clRET_COUNT.Width = 116;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(32, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 16);
            this.label1.TabIndex = 100000;
            this.label1.Text = "持帰要求結果一覧";
            // 
            // dtREQ_DATE
            // 
            this.dtREQ_DATE.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtREQ_DATE.KeyControl = true;
            this.dtREQ_DATE.Location = new System.Drawing.Point(108, 48);
            this.dtREQ_DATE.MaxLength = 8;
            this.dtREQ_DATE.Name = "dtREQ_DATE";
            this.dtREQ_DATE.OnEnterSeparatorCut = true;
            this.dtREQ_DATE.Size = new System.Drawing.Size(145, 23);
            this.dtREQ_DATE.TabIndex = 100061;
            this.dtREQ_DATE.TabKeyControl = true;
            this.dtREQ_DATE.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtREQ_DATE.Enter += new System.EventHandler(this.txt_Enter);
            this.dtREQ_DATE.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblOperatedate
            // 
            this.lblOperatedate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOperatedate.Location = new System.Drawing.Point(32, 50);
            this.lblOperatedate.Name = "lblOperatedate";
            this.lblOperatedate.Size = new System.Drawing.Size(68, 18);
            this.lblOperatedate.TabIndex = 100063;
            this.lblOperatedate.Text = "要求日";
            this.lblOperatedate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(32, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 16);
            this.label3.TabIndex = 100000;
            this.label3.Text = "絞り込み条件";
            // 
            // SearchReqResultList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SearchReqResultList";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvBatList;
		private System.Windows.Forms.ColumnHeader clREQ_DATE;
		private System.Windows.Forms.ColumnHeader clRET_FILE_CHK;
        private System.Windows.Forms.ColumnHeader clREQ_TIME;
        private System.Windows.Forms.ColumnHeader clREQ_TXT_NAME;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader clRET_PROC;
        private System.Windows.Forms.ColumnHeader clRET_MAKE_DATE;
        private System.Windows.Forms.ColumnHeader clRET_MAKE_TIME;
        private System.Windows.Forms.ColumnHeader clRET_COUNT;
        private CommonClass.DTextBox2 dtREQ_DATE;
        private System.Windows.Forms.Label lblOperatedate;
        private System.Windows.Forms.Label label3;
    }
}
