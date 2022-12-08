namespace SearchTeiseiHist
{
	partial class SearchTeiseiHist
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
            this.lvTeiseiList = new System.Windows.Forms.ListView();
            this.clGymId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clOPERATION_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clSCAN_TERM = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBAT_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDETAILS_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clITEM_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clSEQ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clITEM_NAME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clOCR_ENT_DATA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clOCR_VFY_DATA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clENT_DATA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clVFY_DATA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clEND_DATA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBUA_DATA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCTR_DATA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clICTEISEI_DATA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clMRC_CHG_BEFDATA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clE_TERM = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clE_OPENO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clE_STIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clE_ETIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clE_YMD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clE_TIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clV_TERM = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clV_OPENO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clV_STIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clV_ETIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clV_YMD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clV_TIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clC_TERM = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clC_OPENO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clC_STIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clC_ETIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clC_YMD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clC_TIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clO_TERM = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clO_OPENO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clO_STIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.O_ETIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clO_YMD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clO_TIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clITEM_TOP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clITEM_LEFT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clITEM_WIDTH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clITEM_HEIGHT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clUPDATE_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clUPDATE_TIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clUPDATE_KBN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clFIX_TRIGGER = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rbsame = new System.Windows.Forms.RadioButton();
            this.rbback = new System.Windows.Forms.RadioButton();
            this.rbfront = new System.Windows.Forms.RadioButton();
            this.label17 = new System.Windows.Forms.Label();
            this.ntBAT_ID = new CommonClass.NumTextBox2();
            this.lblUPDATE_TIME = new System.Windows.Forms.Label();
            this.lblBAT_ID = new System.Windows.Forms.Label();
            this.dtUPDATE_DATE = new CommonClass.DTextBox2();
            this.dtOperatedate = new CommonClass.DTextBox2();
            this.lblUPDATE_DATE = new System.Windows.Forms.Label();
            this.lblDETAILS_NO = new System.Windows.Forms.Label();
            this.lblOperatedate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblITEM_NAME = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtUPDATE_TIME = new CommonClass.NumTextBox2();
            this.dtUPDATE_TIME2 = new CommonClass.NumTextBox2();
            this.ntDETAILS_NO = new CommonClass.NumTextBox2();
            this.cmbITEM_NAME = new System.Windows.Forms.ComboBox();
            this.txtIMG_FLNM = new CommonClass.KanaTextBox();
            this.txtITEM_NAME = new CommonClass.KanaTextBox();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.txtITEM_NAME);
            this.contentsPanel.Controls.Add(this.txtIMG_FLNM);
            this.contentsPanel.Controls.Add(this.cmbITEM_NAME);
            this.contentsPanel.Controls.Add(this.rbsame);
            this.contentsPanel.Controls.Add(this.rbback);
            this.contentsPanel.Controls.Add(this.rbfront);
            this.contentsPanel.Controls.Add(this.label17);
            this.contentsPanel.Controls.Add(this.ntDETAILS_NO);
            this.contentsPanel.Controls.Add(this.ntBAT_ID);
            this.contentsPanel.Controls.Add(this.lblUPDATE_TIME);
            this.contentsPanel.Controls.Add(this.lblBAT_ID);
            this.contentsPanel.Controls.Add(this.dtUPDATE_TIME2);
            this.contentsPanel.Controls.Add(this.dtUPDATE_TIME);
            this.contentsPanel.Controls.Add(this.dtUPDATE_DATE);
            this.contentsPanel.Controls.Add(this.dtOperatedate);
            this.contentsPanel.Controls.Add(this.label7);
            this.contentsPanel.Controls.Add(this.lblITEM_NAME);
            this.contentsPanel.Controls.Add(this.lblUPDATE_DATE);
            this.contentsPanel.Controls.Add(this.lblDETAILS_NO);
            this.contentsPanel.Controls.Add(this.lblOperatedate);
            this.contentsPanel.Controls.Add(this.label8);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.Add(this.lvTeiseiList);
            this.contentsPanel.Controls.SetChildIndex(this.lvTeiseiList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label8, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblOperatedate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblDETAILS_NO, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblUPDATE_DATE, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblITEM_NAME, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label7, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtOperatedate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtUPDATE_DATE, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtUPDATE_TIME, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtUPDATE_TIME2, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblBAT_ID, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblUPDATE_TIME, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntBAT_ID, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntDETAILS_NO, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label17, 0);
            this.contentsPanel.Controls.SetChildIndex(this.rbfront, 0);
            this.contentsPanel.Controls.SetChildIndex(this.rbback, 0);
            this.contentsPanel.Controls.SetChildIndex(this.rbsame, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbITEM_NAME, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtIMG_FLNM, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtITEM_NAME, 0);
            // 
            // clKey
            // 
            clKey.Text = "clKey";
            clKey.Width = 0;
            // 
            // lvTeiseiList
            // 
            this.lvTeiseiList.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.lvTeiseiList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.clGymId,
            this.clOPERATION_DATE,
            this.clSCAN_TERM,
            this.clBAT_ID,
            this.clDETAILS_NO,
            this.clITEM_ID,
            this.clSEQ,
            this.clITEM_NAME,
            this.clOCR_ENT_DATA,
            this.clOCR_VFY_DATA,
            this.clENT_DATA,
            this.clVFY_DATA,
            this.clEND_DATA,
            this.clBUA_DATA,
            this.clCTR_DATA,
            this.clICTEISEI_DATA,
            this.clMRC_CHG_BEFDATA,
            this.clE_TERM,
            this.clE_OPENO,
            this.clE_STIME,
            this.clE_ETIME,
            this.clE_YMD,
            this.clE_TIME,
            this.clV_TERM,
            this.clV_OPENO,
            this.clV_STIME,
            this.clV_ETIME,
            this.clV_YMD,
            this.clV_TIME,
            this.clC_TERM,
            this.clC_OPENO,
            this.clC_STIME,
            this.clC_ETIME,
            this.clC_YMD,
            this.clC_TIME,
            this.clO_TERM,
            this.clO_OPENO,
            this.clO_STIME,
            this.O_ETIME,
            this.clO_YMD,
            this.clO_TIME,
            this.clITEM_TOP,
            this.clITEM_LEFT,
            this.clITEM_WIDTH,
            this.clITEM_HEIGHT,
            this.clUPDATE_DATE,
            this.clUPDATE_TIME,
            this.clUPDATE_KBN,
            this.clFIX_TRIGGER});
            this.lvTeiseiList.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvTeiseiList.FullRowSelect = true;
            this.lvTeiseiList.GridLines = true;
            this.lvTeiseiList.HideSelection = false;
            this.lvTeiseiList.Location = new System.Drawing.Point(25, 162);
            this.lvTeiseiList.MultiSelect = false;
            this.lvTeiseiList.Name = "lvTeiseiList";
            this.lvTeiseiList.Size = new System.Drawing.Size(1231, 672);
            this.lvTeiseiList.TabIndex = 5;
            this.lvTeiseiList.TabStop = false;
            this.lvTeiseiList.UseCompatibleStateImageBehavior = false;
            this.lvTeiseiList.View = System.Windows.Forms.View.Details;
            this.lvTeiseiList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lv_ColumnWidthChanging);
            this.lvTeiseiList.Enter += new System.EventHandler(this.List_Enter);
            // 
            // clGymId
            // 
            this.clGymId.Text = "業務番号";
            this.clGymId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clGymId.Width = 134;
            // 
            // clOPERATION_DATE
            // 
            this.clOPERATION_DATE.Text = "処理日";
            this.clOPERATION_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clOPERATION_DATE.Width = 140;
            // 
            // clSCAN_TERM
            // 
            this.clSCAN_TERM.Text = "イメージ取込端末";
            this.clSCAN_TERM.Width = 213;
            // 
            // clBAT_ID
            // 
            this.clBAT_ID.Text = "バッチ番号";
            this.clBAT_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clBAT_ID.Width = 135;
            // 
            // clDETAILS_NO
            // 
            this.clDETAILS_NO.Text = "明細番号";
            this.clDETAILS_NO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clDETAILS_NO.Width = 139;
            // 
            // clITEM_ID
            // 
            this.clITEM_ID.Text = "項目ID";
            this.clITEM_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clITEM_ID.Width = 146;
            // 
            // clSEQ
            // 
            this.clSEQ.Text = "出力連番";
            this.clSEQ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clSEQ.Width = 127;
            // 
            // clITEM_NAME
            // 
            this.clITEM_NAME.Text = "項目名称";
            this.clITEM_NAME.Width = 198;
            // 
            // clOCR_ENT_DATA
            // 
            this.clOCR_ENT_DATA.Text = "ＯＣＲ値（エントリ適用）";
            this.clOCR_ENT_DATA.Width = 123;
            // 
            // clOCR_VFY_DATA
            // 
            this.clOCR_VFY_DATA.Text = "ＯＣＲ値（ベリファイ適用）";
            this.clOCR_VFY_DATA.Width = 120;
            // 
            // clENT_DATA
            // 
            this.clENT_DATA.Text = "エントリー値";
            this.clENT_DATA.Width = 126;
            // 
            // clVFY_DATA
            // 
            this.clVFY_DATA.Text = "ベリファイ値";
            this.clVFY_DATA.Width = 124;
            // 
            // clEND_DATA
            // 
            this.clEND_DATA.Text = "最終確定値";
            this.clEND_DATA.Width = 117;
            // 
            // clBUA_DATA
            // 
            this.clBUA_DATA.Text = "持出アップロード値";
            this.clBUA_DATA.Width = 151;
            // 
            // clCTR_DATA
            // 
            this.clCTR_DATA.Text = "電子交換所結果値";
            // 
            // clICTEISEI_DATA
            // 
            this.clICTEISEI_DATA.Text = "持帰訂正確定値";
            this.clICTEISEI_DATA.Width = 202;
            // 
            // clMRC_CHG_BEFDATA
            // 
            this.clMRC_CHG_BEFDATA.Text = "通知読替前値";
            // 
            // clE_TERM
            // 
            this.clE_TERM.Text = "エントリー端末";
            this.clE_TERM.Width = 149;
            // 
            // clE_OPENO
            // 
            this.clE_OPENO.Text = "エントリーオペレーター番号";
            this.clE_OPENO.Width = 181;
            // 
            // clE_STIME
            // 
            this.clE_STIME.Text = "エントリー開始時刻（ミリ秒）";
            this.clE_STIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clE_STIME.Width = 154;
            // 
            // clE_ETIME
            // 
            this.clE_ETIME.Text = "エントリー終了時刻（ミリ秒）";
            this.clE_ETIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clE_ETIME.Width = 151;
            // 
            // clE_YMD
            // 
            this.clE_YMD.Text = "エントリー処理日";
            this.clE_YMD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clE_YMD.Width = 149;
            // 
            // clE_TIME
            // 
            this.clE_TIME.Text = "エントリー時間（ミリ秒）";
            this.clE_TIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clE_TIME.Width = 141;
            // 
            // clV_TERM
            // 
            this.clV_TERM.Text = "ベリファイ端末";
            this.clV_TERM.Width = 136;
            // 
            // clV_OPENO
            // 
            this.clV_OPENO.Text = "ベリファイオペレーター番号";
            this.clV_OPENO.Width = 179;
            // 
            // clV_STIME
            // 
            this.clV_STIME.Text = "ベリファイ開始時刻（ミリ秒）";
            this.clV_STIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clV_STIME.Width = 151;
            // 
            // clV_ETIME
            // 
            this.clV_ETIME.Text = "ベリファイ終了時刻（ミリ秒）";
            this.clV_ETIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clV_ETIME.Width = 145;
            // 
            // clV_YMD
            // 
            this.clV_YMD.Text = "ベリファイ処理日";
            this.clV_YMD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clV_YMD.Width = 143;
            // 
            // clV_TIME
            // 
            this.clV_TIME.Text = "ベリファイ時間（ミリ秒）";
            this.clV_TIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clV_TIME.Width = 154;
            // 
            // clC_TERM
            // 
            this.clC_TERM.Text = "完了訂正端末";
            this.clC_TERM.Width = 136;
            // 
            // clC_OPENO
            // 
            this.clC_OPENO.Text = "完了訂正オペレーター番号";
            this.clC_OPENO.Width = 193;
            // 
            // clC_STIME
            // 
            this.clC_STIME.Text = "完了訂正開始時刻（ミリ秒）";
            this.clC_STIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clC_STIME.Width = 154;
            // 
            // clC_ETIME
            // 
            this.clC_ETIME.Text = "完了訂正終了時刻（ミリ秒）";
            this.clC_ETIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clC_ETIME.Width = 159;
            // 
            // clC_YMD
            // 
            this.clC_YMD.Text = "完了訂正処理日";
            this.clC_YMD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clC_YMD.Width = 146;
            // 
            // clC_TIME
            // 
            this.clC_TIME.Text = "完了訂正時間（ミリ秒）";
            this.clC_TIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clC_TIME.Width = 153;
            // 
            // clO_TERM
            // 
            this.clO_TERM.Text = "持出エラー訂正端末";
            this.clO_TERM.Width = 185;
            // 
            // clO_OPENO
            // 
            this.clO_OPENO.Text = "持出エラー訂正オペレーター番号";
            this.clO_OPENO.Width = 225;
            // 
            // clO_STIME
            // 
            this.clO_STIME.Text = "持出エラー訂正開始時刻（ミリ秒）";
            this.clO_STIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clO_STIME.Width = 181;
            // 
            // O_ETIME
            // 
            this.O_ETIME.Text = "持出エラー訂正終了時刻（ミリ秒）";
            this.O_ETIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.O_ETIME.Width = 181;
            // 
            // clO_YMD
            // 
            this.clO_YMD.Text = "持出エラー訂正処理日";
            this.clO_YMD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clO_YMD.Width = 170;
            // 
            // clO_TIME
            // 
            this.clO_TIME.Text = "持出エラー訂正時間（ミリ秒）";
            this.clO_TIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clO_TIME.Width = 183;
            // 
            // clITEM_TOP
            // 
            this.clITEM_TOP.Text = "OCR認識項目位置(TOP)";
            this.clITEM_TOP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clITEM_TOP.Width = 187;
            // 
            // clITEM_LEFT
            // 
            this.clITEM_LEFT.Text = "OCR認識項目位置(LEFT)";
            this.clITEM_LEFT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clITEM_LEFT.Width = 189;
            // 
            // clITEM_WIDTH
            // 
            this.clITEM_WIDTH.Text = "OCR認識項目位置(WIDTH)";
            this.clITEM_WIDTH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clITEM_WIDTH.Width = 198;
            // 
            // clITEM_HEIGHT
            // 
            this.clITEM_HEIGHT.Text = "OCR認識項目位置(HEIGHT)";
            this.clITEM_HEIGHT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clITEM_HEIGHT.Width = 207;
            // 
            // clUPDATE_DATE
            // 
            this.clUPDATE_DATE.Text = "更新日";
            this.clUPDATE_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clUPDATE_DATE.Width = 138;
            // 
            // clUPDATE_TIME
            // 
            this.clUPDATE_TIME.Text = "更新時間";
            this.clUPDATE_TIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clUPDATE_TIME.Width = 130;
            // 
            // clUPDATE_KBN
            // 
            this.clUPDATE_KBN.Text = "更新区分";
            this.clUPDATE_KBN.Width = 112;
            // 
            // clFIX_TRIGGER
            // 
            this.clFIX_TRIGGER.Text = "修正トリガー";
            this.clFIX_TRIGGER.Width = 120;
            // 
            // rbsame
            // 
            this.rbsame.AutoSize = true;
            this.rbsame.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rbsame.Location = new System.Drawing.Point(1146, 119);
            this.rbsame.Name = "rbsame";
            this.rbsame.Size = new System.Drawing.Size(85, 19);
            this.rbsame.TabIndex = 12;
            this.rbsame.Text = "完全一致";
            this.rbsame.UseVisualStyleBackColor = true;
            // 
            // rbback
            // 
            this.rbback.AutoSize = true;
            this.rbback.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rbback.Location = new System.Drawing.Point(1050, 119);
            this.rbback.Name = "rbback";
            this.rbback.Size = new System.Drawing.Size(85, 19);
            this.rbback.TabIndex = 11;
            this.rbback.Text = "後方一致";
            this.rbback.UseVisualStyleBackColor = true;
            // 
            // rbfront
            // 
            this.rbfront.AutoSize = true;
            this.rbfront.Checked = true;
            this.rbfront.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rbfront.Location = new System.Drawing.Point(954, 119);
            this.rbfront.Name = "rbfront";
            this.rbfront.Size = new System.Drawing.Size(85, 19);
            this.rbfront.TabIndex = 10;
            this.rbfront.TabStop = true;
            this.rbfront.Text = "前方一致";
            this.rbfront.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label17.Location = new System.Drawing.Point(28, 119);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(118, 18);
            this.label17.TabIndex = 100070;
            this.label17.Text = "イメージファイル名";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ntBAT_ID
            // 
            this.ntBAT_ID.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntBAT_ID.KeyControl = true;
            this.ntBAT_ID.Location = new System.Drawing.Point(404, 18);
            this.ntBAT_ID.MaxLength = 6;
            this.ntBAT_ID.Name = "ntBAT_ID";
            this.ntBAT_ID.OnEnterSeparatorCut = true;
            this.ntBAT_ID.Size = new System.Drawing.Size(120, 22);
            this.ntBAT_ID.TabIndex = 2;
            this.ntBAT_ID.TabKeyControl = true;
            this.ntBAT_ID.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntBAT_ID.Enter += new System.EventHandler(this.txt_Enter);
            this.ntBAT_ID.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblUPDATE_TIME
            // 
            this.lblUPDATE_TIME.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblUPDATE_TIME.Location = new System.Drawing.Point(28, 96);
            this.lblUPDATE_TIME.Name = "lblUPDATE_TIME";
            this.lblUPDATE_TIME.Size = new System.Drawing.Size(118, 18);
            this.lblUPDATE_TIME.TabIndex = 100063;
            this.lblUPDATE_TIME.Text = "更新時刻";
            this.lblUPDATE_TIME.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBAT_ID
            // 
            this.lblBAT_ID.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBAT_ID.Location = new System.Drawing.Point(313, 20);
            this.lblBAT_ID.Name = "lblBAT_ID";
            this.lblBAT_ID.Size = new System.Drawing.Size(91, 18);
            this.lblBAT_ID.TabIndex = 100064;
            this.lblBAT_ID.Text = "バッチ番号";
            this.lblBAT_ID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtUPDATE_DATE
            // 
            this.dtUPDATE_DATE.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtUPDATE_DATE.KeyControl = true;
            this.dtUPDATE_DATE.Location = new System.Drawing.Point(155, 69);
            this.dtUPDATE_DATE.MaxLength = 8;
            this.dtUPDATE_DATE.Name = "dtUPDATE_DATE";
            this.dtUPDATE_DATE.OnEnterSeparatorCut = true;
            this.dtUPDATE_DATE.Size = new System.Drawing.Size(120, 22);
            this.dtUPDATE_DATE.TabIndex = 6;
            this.dtUPDATE_DATE.TabKeyControl = true;
            this.dtUPDATE_DATE.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtUPDATE_DATE.Enter += new System.EventHandler(this.txt_Enter);
            this.dtUPDATE_DATE.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // dtOperatedate
            // 
            this.dtOperatedate.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtOperatedate.KeyControl = true;
            this.dtOperatedate.Location = new System.Drawing.Point(155, 18);
            this.dtOperatedate.MaxLength = 8;
            this.dtOperatedate.Name = "dtOperatedate";
            this.dtOperatedate.OnEnterSeparatorCut = true;
            this.dtOperatedate.Size = new System.Drawing.Size(120, 22);
            this.dtOperatedate.TabIndex = 1;
            this.dtOperatedate.TabKeyControl = true;
            this.dtOperatedate.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtOperatedate.Enter += new System.EventHandler(this.txt_Enter);
            this.dtOperatedate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblUPDATE_DATE
            // 
            this.lblUPDATE_DATE.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblUPDATE_DATE.Location = new System.Drawing.Point(28, 70);
            this.lblUPDATE_DATE.Name = "lblUPDATE_DATE";
            this.lblUPDATE_DATE.Size = new System.Drawing.Size(118, 18);
            this.lblUPDATE_DATE.TabIndex = 100059;
            this.lblUPDATE_DATE.Text = "更新日";
            this.lblUPDATE_DATE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDETAILS_NO
            // 
            this.lblDETAILS_NO.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDETAILS_NO.Location = new System.Drawing.Point(550, 20);
            this.lblDETAILS_NO.Name = "lblDETAILS_NO";
            this.lblDETAILS_NO.Size = new System.Drawing.Size(79, 18);
            this.lblDETAILS_NO.TabIndex = 100061;
            this.lblDETAILS_NO.Text = "明細番号";
            this.lblDETAILS_NO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOperatedate
            // 
            this.lblOperatedate.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOperatedate.Location = new System.Drawing.Point(28, 20);
            this.lblOperatedate.Name = "lblOperatedate";
            this.lblOperatedate.Size = new System.Drawing.Size(118, 18);
            this.lblOperatedate.TabIndex = 100060;
            this.lblOperatedate.Text = "処理日";
            this.lblOperatedate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(22, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 100052;
            this.label1.Text = "検索条件";
            // 
            // lblITEM_NAME
            // 
            this.lblITEM_NAME.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblITEM_NAME.Location = new System.Drawing.Point(28, 44);
            this.lblITEM_NAME.Name = "lblITEM_NAME";
            this.lblITEM_NAME.Size = new System.Drawing.Size(118, 18);
            this.lblITEM_NAME.TabIndex = 100056;
            this.lblITEM_NAME.Text = "項目名称";
            this.lblITEM_NAME.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblITEM_NAME.UseMnemonic = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(281, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 23);
            this.label7.TabIndex = 100054;
            this.label7.Text = "～";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(22, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 100052;
            this.label8.Text = "検索結果";
            // 
            // dtUPDATE_TIME
            // 
            this.dtUPDATE_TIME.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtUPDATE_TIME.KeyControl = true;
            this.dtUPDATE_TIME.Location = new System.Drawing.Point(155, 94);
            this.dtUPDATE_TIME.MaxLength = 6;
            this.dtUPDATE_TIME.Name = "dtUPDATE_TIME";
            this.dtUPDATE_TIME.Size = new System.Drawing.Size(120, 22);
            this.dtUPDATE_TIME.TabIndex = 7;
            this.dtUPDATE_TIME.TabKeyControl = true;
            this.dtUPDATE_TIME.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtUPDATE_TIME.Enter += new System.EventHandler(this.txt_Enter);
            this.dtUPDATE_TIME.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // dtUPDATE_TIME2
            // 
            this.dtUPDATE_TIME2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtUPDATE_TIME2.KeyControl = true;
            this.dtUPDATE_TIME2.Location = new System.Drawing.Point(310, 94);
            this.dtUPDATE_TIME2.MaxLength = 6;
            this.dtUPDATE_TIME2.Name = "dtUPDATE_TIME2";
            this.dtUPDATE_TIME2.Size = new System.Drawing.Size(120, 22);
            this.dtUPDATE_TIME2.TabIndex = 8;
            this.dtUPDATE_TIME2.TabKeyControl = true;
            this.dtUPDATE_TIME2.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtUPDATE_TIME2.Enter += new System.EventHandler(this.txt_Enter);
            this.dtUPDATE_TIME2.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // ntDETAILS_NO
            // 
            this.ntDETAILS_NO.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntDETAILS_NO.KeyControl = true;
            this.ntDETAILS_NO.Location = new System.Drawing.Point(635, 18);
            this.ntDETAILS_NO.MaxLength = 6;
            this.ntDETAILS_NO.Name = "ntDETAILS_NO";
            this.ntDETAILS_NO.OnEnterSeparatorCut = true;
            this.ntDETAILS_NO.Size = new System.Drawing.Size(120, 22);
            this.ntDETAILS_NO.TabIndex = 3;
            this.ntDETAILS_NO.TabKeyControl = true;
            this.ntDETAILS_NO.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntDETAILS_NO.Enter += new System.EventHandler(this.txt_Enter);
            this.ntDETAILS_NO.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // cmbITEM_NAME
            // 
            this.cmbITEM_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.cmbITEM_NAME.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbITEM_NAME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbITEM_NAME.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbITEM_NAME.FormattingEnabled = true;
            this.cmbITEM_NAME.Location = new System.Drawing.Point(155, 43);
            this.cmbITEM_NAME.Name = "cmbITEM_NAME";
            this.cmbITEM_NAME.Size = new System.Drawing.Size(165, 23);
            this.cmbITEM_NAME.TabIndex = 4;
            this.cmbITEM_NAME.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbITEM_NAME.Enter += new System.EventHandler(this.cmbITEM_NAME_Enter);
            this.cmbITEM_NAME.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // txtIMG_FLNM
            // 
            this.txtIMG_FLNM.EntryMode = CommonClass.ENTRYMODE.IMEOFF_KANA;
            this.txtIMG_FLNM.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtIMG_FLNM.KanaLock = false;
            this.txtIMG_FLNM.KeyControl = true;
            this.txtIMG_FLNM.Location = new System.Drawing.Point(155, 119);
            this.txtIMG_FLNM.MaxLength = 62;
            this.txtIMG_FLNM.Name = "txtIMG_FLNM";
            this.txtIMG_FLNM.Size = new System.Drawing.Size(763, 22);
            this.txtIMG_FLNM.TabIndex = 9;
            this.txtIMG_FLNM.TabKeyControl = true;
            this.txtIMG_FLNM.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.txtIMG_FLNM.Enter += new System.EventHandler(this.txt_Enter);
            this.txtIMG_FLNM.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // txtITEM_NAME
            // 
            this.txtITEM_NAME.EntryMode = CommonClass.ENTRYMODE.IMEOFF_KANA;
            this.txtITEM_NAME.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtITEM_NAME.KanaLock = false;
            this.txtITEM_NAME.KeyControl = true;
            this.txtITEM_NAME.Location = new System.Drawing.Point(331, 43);
            this.txtITEM_NAME.MaxLength = 100;
            this.txtITEM_NAME.Name = "txtITEM_NAME";
            this.txtITEM_NAME.Size = new System.Drawing.Size(614, 22);
            this.txtITEM_NAME.TabIndex = 5;
            this.txtITEM_NAME.TabKeyControl = true;
            this.txtITEM_NAME.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.txtITEM_NAME.Enter += new System.EventHandler(this.txt_Enter);
            this.txtITEM_NAME.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // SearchTeiseiHist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SearchTeiseiHist";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvTeiseiList;
		private System.Windows.Forms.ColumnHeader clGymId;
		private System.Windows.Forms.ColumnHeader clBAT_ID;
        private System.Windows.Forms.ColumnHeader clOPERATION_DATE;
        private System.Windows.Forms.ColumnHeader clSCAN_TERM;
        private System.Windows.Forms.RadioButton rbsame;
        private System.Windows.Forms.RadioButton rbback;
        private System.Windows.Forms.RadioButton rbfront;
        private System.Windows.Forms.Label label17;
        private CommonClass.NumTextBox2 ntBAT_ID;
        private System.Windows.Forms.Label lblUPDATE_TIME;
        private System.Windows.Forms.Label lblBAT_ID;
        private CommonClass.DTextBox2 dtUPDATE_DATE;
        private CommonClass.DTextBox2 dtOperatedate;
        private System.Windows.Forms.Label lblUPDATE_DATE;
        private System.Windows.Forms.Label lblDETAILS_NO;
        private System.Windows.Forms.Label lblOperatedate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblITEM_NAME;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ColumnHeader clDETAILS_NO;
        private System.Windows.Forms.ColumnHeader clITEM_ID;
        private System.Windows.Forms.ColumnHeader clSEQ;
        private System.Windows.Forms.ColumnHeader clITEM_NAME;
        private System.Windows.Forms.ColumnHeader clOCR_ENT_DATA;
        private System.Windows.Forms.ColumnHeader clBUA_DATA;
        private CommonClass.NumTextBox2 dtUPDATE_TIME;
        private CommonClass.NumTextBox2 dtUPDATE_TIME2;
        private System.Windows.Forms.ColumnHeader clENT_DATA;
        private System.Windows.Forms.ColumnHeader clVFY_DATA;
        private System.Windows.Forms.ColumnHeader clEND_DATA;
        private System.Windows.Forms.ColumnHeader clICTEISEI_DATA;
        private System.Windows.Forms.ColumnHeader clE_TERM;
        private System.Windows.Forms.ColumnHeader clE_OPENO;
        private System.Windows.Forms.ColumnHeader clE_STIME;
        private System.Windows.Forms.ColumnHeader clE_ETIME;
        private System.Windows.Forms.ColumnHeader clE_YMD;
        private System.Windows.Forms.ColumnHeader clE_TIME;
        private System.Windows.Forms.ColumnHeader clV_TERM;
        private System.Windows.Forms.ColumnHeader clV_OPENO;
        private System.Windows.Forms.ColumnHeader clV_STIME;
        private System.Windows.Forms.ColumnHeader clV_ETIME;
        private System.Windows.Forms.ColumnHeader clV_YMD;
        private System.Windows.Forms.ColumnHeader clV_TIME;
        private System.Windows.Forms.ColumnHeader clC_TERM;
        private System.Windows.Forms.ColumnHeader clC_OPENO;
        private System.Windows.Forms.ColumnHeader clC_STIME;
        private System.Windows.Forms.ColumnHeader clC_ETIME;
        private System.Windows.Forms.ColumnHeader clC_YMD;
        private System.Windows.Forms.ColumnHeader clC_TIME;
        private System.Windows.Forms.ColumnHeader clO_TERM;
        private System.Windows.Forms.ColumnHeader clO_OPENO;
        private System.Windows.Forms.ColumnHeader clO_STIME;
        private System.Windows.Forms.ColumnHeader O_ETIME;
        private System.Windows.Forms.ColumnHeader clO_YMD;
        private System.Windows.Forms.ColumnHeader clO_TIME;
        private System.Windows.Forms.ColumnHeader clITEM_TOP;
        private System.Windows.Forms.ColumnHeader clITEM_LEFT;
        private System.Windows.Forms.ColumnHeader clITEM_WIDTH;
        private System.Windows.Forms.ColumnHeader clITEM_HEIGHT;
        private System.Windows.Forms.ColumnHeader clUPDATE_DATE;
        private System.Windows.Forms.ColumnHeader clUPDATE_TIME;
        private System.Windows.Forms.ColumnHeader clUPDATE_KBN;
        private System.Windows.Forms.ColumnHeader clOCR_VFY_DATA;
        private System.Windows.Forms.ColumnHeader clFIX_TRIGGER;
        private CommonClass.NumTextBox2 ntDETAILS_NO;
        private System.Windows.Forms.ComboBox cmbITEM_NAME;
        private CommonClass.KanaTextBox txtITEM_NAME;
        private CommonClass.KanaTextBox txtIMG_FLNM;
        private System.Windows.Forms.ColumnHeader clCTR_DATA;
        private System.Windows.Forms.ColumnHeader clMRC_CHG_BEFDATA;        
    }
}
