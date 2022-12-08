namespace SearchTxtView
{
	partial class SearchTxtViewDetail
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
            this.lvResultData = new System.Windows.Forms.ListView();
            this.clIMG_NAME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBK_NO_TEISEI_FLG = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTEISEI_BEF_BK_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTEISEI_AFT_BK_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCLEARING_TEISEI_FLG = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTEISEI_BEF_CLEARING_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTEISEI_CLEARING_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAMOUNT_TEISEI_FLG = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTEISEI_BEF_AMOUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTEISEI_AMOUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDUPLICATE_IMG_NAME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clFUBI_REG_KBN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clFUBI_KBN_01 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clZERO_FUBINO_01 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clFUBI_KBN_02 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clZRO_FUBINO_02 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clFUBI_KBN_03 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clZRO_FUBINO_03 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clFUBI_KBN_04 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clZRO_FUBINO_04 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clFUBI_KBN_05 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clZRO_FUBINO_05 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clREV_CLEARING_FLG = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblFILE_DIVID = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblFILE_NAME = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblRECORD_COUNT = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblRECV_DATE = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblRECV_TIME = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.pnlInfo);
            this.contentsPanel.Controls.Add(this.label5);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.Add(this.lvResultData);
            this.contentsPanel.Controls.SetChildIndex(this.lvResultData, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label5, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.pnlInfo, 0);
            // 
            // clKey
            // 
            clKey.Text = "clKey";
            clKey.Width = 0;
            // 
            // lvResultData
            // 
            this.lvResultData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.clIMG_NAME,
            this.clBK_NO_TEISEI_FLG,
            this.clTEISEI_BEF_BK_NO,
            this.clTEISEI_AFT_BK_NO,
            this.clCLEARING_TEISEI_FLG,
            this.clTEISEI_BEF_CLEARING_DATE,
            this.clTEISEI_CLEARING_DATE,
            this.clAMOUNT_TEISEI_FLG,
            this.clTEISEI_BEF_AMOUNT,
            this.clTEISEI_AMOUNT,
            this.clDUPLICATE_IMG_NAME,
            this.clFUBI_REG_KBN,
            this.clFUBI_KBN_01,
            this.clZERO_FUBINO_01,
            this.clFUBI_KBN_02,
            this.clZRO_FUBINO_02,
            this.clFUBI_KBN_03,
            this.clZRO_FUBINO_03,
            this.clFUBI_KBN_04,
            this.clZRO_FUBINO_04,
            this.clFUBI_KBN_05,
            this.clZRO_FUBINO_05,
            this.clREV_CLEARING_FLG});
            this.lvResultData.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvResultData.FullRowSelect = true;
            this.lvResultData.GridLines = true;
            this.lvResultData.HideSelection = false;
            this.lvResultData.Location = new System.Drawing.Point(23, 123);
            this.lvResultData.MultiSelect = false;
            this.lvResultData.Name = "lvResultData";
            this.lvResultData.Size = new System.Drawing.Size(1222, 706);
            this.lvResultData.TabIndex = 5;
            this.lvResultData.TabStop = false;
            this.lvResultData.UseCompatibleStateImageBehavior = false;
            this.lvResultData.View = System.Windows.Forms.View.Details;
            this.lvResultData.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lv_ColumnWidthChanging);
            this.lvResultData.DoubleClick += new System.EventHandler(this.lv_DoubleClick);
            this.lvResultData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lv_KeyDown);
            // 
            // clIMG_NAME
            // 
            this.clIMG_NAME.Text = "証券イメージファイル名";
            this.clIMG_NAME.Width = 240;
            // 
            // clBK_NO_TEISEI_FLG
            // 
            this.clBK_NO_TEISEI_FLG.Text = "銀行コード訂正フラグ";
            this.clBK_NO_TEISEI_FLG.Width = 159;
            // 
            // clTEISEI_BEF_BK_NO
            // 
            this.clTEISEI_BEF_BK_NO.Text = "訂正前銀行コード";
            this.clTEISEI_BEF_BK_NO.Width = 139;
            // 
            // clTEISEI_AFT_BK_NO
            // 
            this.clTEISEI_AFT_BK_NO.Text = "訂正後銀行コード";
            this.clTEISEI_AFT_BK_NO.Width = 155;
            // 
            // clCLEARING_TEISEI_FLG
            // 
            this.clCLEARING_TEISEI_FLG.Text = "交換希望日訂正フラグ";
            this.clCLEARING_TEISEI_FLG.Width = 162;
            // 
            // clTEISEI_BEF_CLEARING_DATE
            // 
            this.clTEISEI_BEF_CLEARING_DATE.Text = "訂正前交換希望日";
            this.clTEISEI_BEF_CLEARING_DATE.Width = 153;
            // 
            // clTEISEI_CLEARING_DATE
            // 
            this.clTEISEI_CLEARING_DATE.Text = "訂正後交換希望日";
            this.clTEISEI_CLEARING_DATE.Width = 150;
            // 
            // clAMOUNT_TEISEI_FLG
            // 
            this.clAMOUNT_TEISEI_FLG.Text = "金額訂正フラグ";
            this.clAMOUNT_TEISEI_FLG.Width = 128;
            // 
            // clTEISEI_BEF_AMOUNT
            // 
            this.clTEISEI_BEF_AMOUNT.Text = "訂正前金額";
            this.clTEISEI_BEF_AMOUNT.Width = 126;
            // 
            // clTEISEI_AMOUNT
            // 
            this.clTEISEI_AMOUNT.Text = "訂正後金額";
            this.clTEISEI_AMOUNT.Width = 133;
            // 
            // clDUPLICATE_IMG_NAME
            // 
            this.clDUPLICATE_IMG_NAME.Text = "二重持出イメージファイル名";
            this.clDUPLICATE_IMG_NAME.Width = 194;
            // 
            // clFUBI_REG_KBN
            // 
            this.clFUBI_REG_KBN.Text = "不渡返還登録区分";
            this.clFUBI_REG_KBN.Width = 153;
            // 
            // clFUBI_KBN_01
            // 
            this.clFUBI_KBN_01.Text = "不渡返還区分１";
            this.clFUBI_KBN_01.Width = 127;
            // 
            // clZERO_FUBINO_01
            // 
            this.clZERO_FUBINO_01.Text = "0号不渡事由コード１";
            this.clZERO_FUBINO_01.Width = 163;
            // 
            // clFUBI_KBN_02
            // 
            this.clFUBI_KBN_02.Text = "不渡返還区分２";
            this.clFUBI_KBN_02.Width = 121;
            // 
            // clZRO_FUBINO_02
            // 
            this.clZRO_FUBINO_02.Text = "0号不渡事由コード２";
            this.clZRO_FUBINO_02.Width = 155;
            // 
            // clFUBI_KBN_03
            // 
            this.clFUBI_KBN_03.Text = "不渡返還区分３";
            this.clFUBI_KBN_03.Width = 121;
            // 
            // clZRO_FUBINO_03
            // 
            this.clZRO_FUBINO_03.Text = "0号不渡事由コード３";
            this.clZRO_FUBINO_03.Width = 149;
            // 
            // clFUBI_KBN_04
            // 
            this.clFUBI_KBN_04.Text = "不渡返還区分４";
            this.clFUBI_KBN_04.Width = 121;
            // 
            // clZRO_FUBINO_04
            // 
            this.clZRO_FUBINO_04.Text = "0号不渡事由コード４";
            this.clZRO_FUBINO_04.Width = 147;
            // 
            // clFUBI_KBN_05
            // 
            this.clFUBI_KBN_05.Text = "不渡返還区分５";
            this.clFUBI_KBN_05.Width = 123;
            // 
            // clZRO_FUBINO_05
            // 
            this.clZRO_FUBINO_05.Text = "0号不渡事由コード５";
            this.clZRO_FUBINO_05.Width = 89;
            // 
            // clREV_CLEARING_FLG
            // 
            this.clREV_CLEARING_FLG.Text = "逆交換対象フラグ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(18, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 100014;
            this.label1.Text = "ファイル情報";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(18, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 16);
            this.label5.TabIndex = 100014;
            this.label5.Text = "データレコード内容";
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.lblFILE_DIVID);
            this.pnlInfo.Controls.Add(this.label11);
            this.pnlInfo.Controls.Add(this.lblFILE_NAME);
            this.pnlInfo.Controls.Add(this.label6);
            this.pnlInfo.Controls.Add(this.lblRECORD_COUNT);
            this.pnlInfo.Controls.Add(this.label10);
            this.pnlInfo.Controls.Add(this.lblRECV_DATE);
            this.pnlInfo.Controls.Add(this.label12);
            this.pnlInfo.Controls.Add(this.lblRECV_TIME);
            this.pnlInfo.Controls.Add(this.label13);
            this.pnlInfo.Location = new System.Drawing.Point(26, 32);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(1040, 61);
            this.pnlInfo.TabIndex = 100071;
            // 
            // lblFILE_DIVID
            // 
            this.lblFILE_DIVID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFILE_DIVID.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblFILE_DIVID.Location = new System.Drawing.Point(0, 30);
            this.lblFILE_DIVID.Margin = new System.Windows.Forms.Padding(0);
            this.lblFILE_DIVID.Name = "lblFILE_DIVID";
            this.lblFILE_DIVID.Size = new System.Drawing.Size(408, 31);
            this.lblFILE_DIVID.TabIndex = 100025;
            this.lblFILE_DIVID.Text = "XXXXXXXXXXXX";
            this.lblFILE_DIVID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(408, 31);
            this.label11.TabIndex = 100018;
            this.label11.Text = "ファイル識別区分";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFILE_NAME
            // 
            this.lblFILE_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFILE_NAME.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblFILE_NAME.Location = new System.Drawing.Point(407, 30);
            this.lblFILE_NAME.Margin = new System.Windows.Forms.Padding(0);
            this.lblFILE_NAME.Name = "lblFILE_NAME";
            this.lblFILE_NAME.Size = new System.Drawing.Size(303, 31);
            this.lblFILE_NAME.TabIndex = 100026;
            this.lblFILE_NAME.Text = "XXXXXXXXXXXXXXX";
            this.lblFILE_NAME.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label6.Location = new System.Drawing.Point(407, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(303, 31);
            this.label6.TabIndex = 100019;
            this.label6.Text = "ファイル名";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRECORD_COUNT
            // 
            this.lblRECORD_COUNT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRECORD_COUNT.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblRECORD_COUNT.Location = new System.Drawing.Point(709, 30);
            this.lblRECORD_COUNT.Margin = new System.Windows.Forms.Padding(0);
            this.lblRECORD_COUNT.Name = "lblRECORD_COUNT";
            this.lblRECORD_COUNT.Size = new System.Drawing.Size(153, 31);
            this.lblRECORD_COUNT.TabIndex = 100029;
            this.lblRECORD_COUNT.Text = "XXXX";
            this.lblRECORD_COUNT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label10.Location = new System.Drawing.Point(709, 0);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(153, 31);
            this.label10.TabIndex = 100022;
            this.label10.Text = "件数";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRECV_DATE
            // 
            this.lblRECV_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRECV_DATE.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblRECV_DATE.Location = new System.Drawing.Point(861, 30);
            this.lblRECV_DATE.Margin = new System.Windows.Forms.Padding(0);
            this.lblRECV_DATE.Name = "lblRECV_DATE";
            this.lblRECV_DATE.Size = new System.Drawing.Size(86, 31);
            this.lblRECV_DATE.TabIndex = 100030;
            this.lblRECV_DATE.Text = "XXXX";
            this.lblRECV_DATE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label12.Location = new System.Drawing.Point(861, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 31);
            this.label12.TabIndex = 100023;
            this.label12.Text = "取込日";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRECV_TIME
            // 
            this.lblRECV_TIME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRECV_TIME.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.lblRECV_TIME.Location = new System.Drawing.Point(946, 30);
            this.lblRECV_TIME.Margin = new System.Windows.Forms.Padding(0);
            this.lblRECV_TIME.Name = "lblRECV_TIME";
            this.lblRECV_TIME.Size = new System.Drawing.Size(86, 31);
            this.lblRECV_TIME.TabIndex = 100031;
            this.lblRECV_TIME.Text = "XXXX";
            this.lblRECV_TIME.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label13.Location = new System.Drawing.Point(946, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 31);
            this.label13.TabIndex = 100024;
            this.label13.Text = "取込時刻";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchTxtViewDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SearchTxtViewDetail";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvResultData;
		private System.Windows.Forms.ColumnHeader clIMG_NAME;
		private System.Windows.Forms.ColumnHeader clTEISEI_AFT_BK_NO;
        private System.Windows.Forms.ColumnHeader clBK_NO_TEISEI_FLG;
        private System.Windows.Forms.ColumnHeader clTEISEI_BEF_BK_NO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader clCLEARING_TEISEI_FLG;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader clTEISEI_BEF_CLEARING_DATE;
        private System.Windows.Forms.ColumnHeader clTEISEI_CLEARING_DATE;
        private System.Windows.Forms.ColumnHeader clAMOUNT_TEISEI_FLG;
        private System.Windows.Forms.ColumnHeader clTEISEI_BEF_AMOUNT;
        private System.Windows.Forms.ColumnHeader clTEISEI_AMOUNT;
        private System.Windows.Forms.ColumnHeader clDUPLICATE_IMG_NAME;
        private System.Windows.Forms.ColumnHeader clFUBI_REG_KBN;
        private System.Windows.Forms.ColumnHeader clFUBI_KBN_01;
        private System.Windows.Forms.ColumnHeader clZERO_FUBINO_01;
        private System.Windows.Forms.ColumnHeader clFUBI_KBN_02;
        private System.Windows.Forms.ColumnHeader clZRO_FUBINO_02;
        private System.Windows.Forms.ColumnHeader clFUBI_KBN_03;
        private System.Windows.Forms.ColumnHeader clZRO_FUBINO_03;
        private System.Windows.Forms.ColumnHeader clFUBI_KBN_04;
        private System.Windows.Forms.ColumnHeader clZRO_FUBINO_04;
        private System.Windows.Forms.ColumnHeader clFUBI_KBN_05;
        private System.Windows.Forms.ColumnHeader clZRO_FUBINO_05;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblFILE_DIVID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblFILE_NAME;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRECORD_COUNT;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblRECV_DATE;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblRECV_TIME;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ColumnHeader clREV_CLEARING_FLG;
    }
}
