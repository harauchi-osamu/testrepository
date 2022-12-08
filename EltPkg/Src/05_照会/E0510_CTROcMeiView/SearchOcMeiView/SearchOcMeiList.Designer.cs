namespace SearchOcMeiView
{
    partial class SearchOcMeiList
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
            this.lvResultList = new System.Windows.Forms.ListView();
            this.clOPERATION_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBATCH_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCLEARING_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clSCAN_BR_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clSCAN_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clOC_BR_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDETAILS_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clIC_OC_BK_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAMOUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDelete = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clUpload = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBCASts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBUA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clGMA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clGRA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clGXA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clICBKInptSts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAMTInptSts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clKoukanjiriInptSts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clKoukanjiriTMNO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clICBKEOpe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAmountEOpe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ntAmountTo = new CommonClass.NumTextBox2();
            this.ntAmountFrom = new CommonClass.NumTextBox2();
            this.ntOCBRNoFrom = new CommonClass.NumTextBox2();
            this.dtClearingDataFrom = new CommonClass.DTextBox2();
            this.dtRdate = new CommonClass.DTextBox2();
            this.label8 = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblOCBRNo = new System.Windows.Forms.Label();
            this.lblClearingData = new System.Windows.Forms.Label();
            this.lblRdate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ntScanBrNo = new CommonClass.NumTextBox2();
            this.txtOpeNumer = new CommonClass.KanaTextBox();
            this.ntBatNo = new CommonClass.NumTextBox2();
            this.lblstatus = new System.Windows.Forms.Label();
            this.lblScanBrNo = new System.Windows.Forms.Label();
            this.lblBatNo = new System.Windows.Forms.Label();
            this.rdoMemoOpt3 = new System.Windows.Forms.RadioButton();
            this.rdoMemoOpt2 = new System.Windows.Forms.RadioButton();
            this.rdoMemoOpt1 = new System.Windows.Forms.RadioButton();
            this.txtMemo = new CommonClass.KanaTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblMemo = new System.Windows.Forms.Label();
            this.lblImgFLNm = new System.Windows.Forms.Label();
            this.txtImgFLNm = new CommonClass.KanaTextBox();
            this.rdoImgOpt1 = new System.Windows.Forms.RadioButton();
            this.rdoImgOpt2 = new System.Windows.Forms.RadioButton();
            this.rdoImgOpt3 = new System.Windows.Forms.RadioButton();
            this.label20 = new System.Windows.Forms.Label();
            this.lblICNo = new System.Windows.Forms.Label();
            this.ntICBKNo = new CommonClass.NumTextBox2();
            this.label22 = new System.Windows.Forms.Label();
            this.dtClearingDataTo = new CommonClass.DTextBox2();
            this.label23 = new System.Windows.Forms.Label();
            this.lblScanDate = new System.Windows.Forms.Label();
            this.dtScanDate = new CommonClass.DTextBox2();
            this.label25 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbDelete = new System.Windows.Forms.ComboBox();
            this.cmbBUASTS = new System.Windows.Forms.ComboBox();
            this.cmbGMB = new System.Windows.Forms.ComboBox();
            this.cmbBCASTS = new System.Windows.Forms.ComboBox();
            this.cmbGRASTS = new System.Windows.Forms.ComboBox();
            this.cmbBUA = new System.Windows.Forms.ComboBox();
            this.cmbGXA = new System.Windows.Forms.ComboBox();
            this.pnlMemoOpt = new System.Windows.Forms.Panel();
            this.pnlImgOpt = new System.Windows.Forms.Panel();
            this.cmbDef = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ntOCBRNoTo = new CommonClass.NumTextBox2();
            this.cmbEditFlg = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.pnlMemoOpt.SuspendLayout();
            this.pnlImgOpt.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.cmbEditFlg);
            this.contentsPanel.Controls.Add(this.label6);
            this.contentsPanel.Controls.Add(this.ntOCBRNoTo);
            this.contentsPanel.Controls.Add(this.label5);
            this.contentsPanel.Controls.Add(this.cmbDef);
            this.contentsPanel.Controls.Add(this.label3);
            this.contentsPanel.Controls.Add(this.pnlImgOpt);
            this.contentsPanel.Controls.Add(this.pnlMemoOpt);
            this.contentsPanel.Controls.Add(this.cmbGXA);
            this.contentsPanel.Controls.Add(this.cmbBUA);
            this.contentsPanel.Controls.Add(this.cmbGRASTS);
            this.contentsPanel.Controls.Add(this.cmbBCASTS);
            this.contentsPanel.Controls.Add(this.cmbGMB);
            this.contentsPanel.Controls.Add(this.cmbBUASTS);
            this.contentsPanel.Controls.Add(this.cmbDelete);
            this.contentsPanel.Controls.Add(this.cmbStatus);
            this.contentsPanel.Controls.Add(this.txtImgFLNm);
            this.contentsPanel.Controls.Add(this.txtMemo);
            this.contentsPanel.Controls.Add(this.label19);
            this.contentsPanel.Controls.Add(this.label10);
            this.contentsPanel.Controls.Add(this.label13);
            this.contentsPanel.Controls.Add(this.label18);
            this.contentsPanel.Controls.Add(this.label20);
            this.contentsPanel.Controls.Add(this.lblImgFLNm);
            this.contentsPanel.Controls.Add(this.lblMemo);
            this.contentsPanel.Controls.Add(this.ntScanBrNo);
            this.contentsPanel.Controls.Add(this.txtOpeNumer);
            this.contentsPanel.Controls.Add(this.ntBatNo);
            this.contentsPanel.Controls.Add(this.lblstatus);
            this.contentsPanel.Controls.Add(this.lblScanBrNo);
            this.contentsPanel.Controls.Add(this.lblBatNo);
            this.contentsPanel.Controls.Add(this.ntAmountTo);
            this.contentsPanel.Controls.Add(this.ntAmountFrom);
            this.contentsPanel.Controls.Add(this.ntICBKNo);
            this.contentsPanel.Controls.Add(this.ntOCBRNoFrom);
            this.contentsPanel.Controls.Add(this.dtClearingDataTo);
            this.contentsPanel.Controls.Add(this.dtClearingDataFrom);
            this.contentsPanel.Controls.Add(this.dtScanDate);
            this.contentsPanel.Controls.Add(this.dtRdate);
            this.contentsPanel.Controls.Add(this.label22);
            this.contentsPanel.Controls.Add(this.label8);
            this.contentsPanel.Controls.Add(this.lblAmount);
            this.contentsPanel.Controls.Add(this.label23);
            this.contentsPanel.Controls.Add(this.label9);
            this.contentsPanel.Controls.Add(this.lblICNo);
            this.contentsPanel.Controls.Add(this.lblOCBRNo);
            this.contentsPanel.Controls.Add(this.lblClearingData);
            this.contentsPanel.Controls.Add(this.label25);
            this.contentsPanel.Controls.Add(this.lblScanDate);
            this.contentsPanel.Controls.Add(this.lblRdate);
            this.contentsPanel.Controls.Add(this.label15);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.Add(this.lvResultList);
            this.contentsPanel.Controls.SetChildIndex(this.lvResultList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label15, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblRdate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblScanDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label25, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblClearingData, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblOCBRNo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblICNo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label9, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label23, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblAmount, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label8, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label22, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtRdate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtScanDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtClearingDataFrom, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtClearingDataTo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntOCBRNoFrom, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntICBKNo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntAmountFrom, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntAmountTo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblBatNo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblScanBrNo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblstatus, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntBatNo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtOpeNumer, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntScanBrNo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblMemo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblImgFLNm, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label20, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label18, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label13, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label10, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label19, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtMemo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtImgFLNm, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbStatus, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbDelete, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbBUASTS, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbGMB, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbBCASTS, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbGRASTS, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbBUA, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbGXA, 0);
            this.contentsPanel.Controls.SetChildIndex(this.pnlMemoOpt, 0);
            this.contentsPanel.Controls.SetChildIndex(this.pnlImgOpt, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label3, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbDef, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label5, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntOCBRNoTo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label6, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbEditFlg, 0);
            // 
            // clKey
            // 
            clKey.Text = "clKey";
            clKey.Width = 0;
            // 
            // lvResultList
            // 
            this.lvResultList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clKey,
            this.clOPERATION_DATE,
            this.clBATCH_NO,
            this.clCLEARING_DATE,
            this.clSCAN_BR_NO,
            this.clSCAN_DATE,
            this.clOC_BR_NO,
            this.clDETAILS_NO,
            this.clIC_OC_BK_NO,
            this.clAMOUNT,
            this.clDelete,
            this.clUpload,
            this.clBCASts,
            this.clBUA,
            this.clGMA,
            this.clGRA,
            this.clGXA,
            this.clICBKInptSts,
            this.clAMTInptSts,
            this.clKoukanjiriInptSts,
            this.clKoukanjiriTMNO,
            this.clICBKEOpe,
            this.clAmountEOpe});
            this.lvResultList.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvResultList.FullRowSelect = true;
            this.lvResultList.GridLines = true;
            this.lvResultList.HideSelection = false;
            this.lvResultList.Location = new System.Drawing.Point(20, 220);
            this.lvResultList.MultiSelect = false;
            this.lvResultList.Name = "lvResultList";
            this.lvResultList.Size = new System.Drawing.Size(1231, 612);
            this.lvResultList.TabIndex = 31;
            this.lvResultList.TabStop = false;
            this.lvResultList.UseCompatibleStateImageBehavior = false;
            this.lvResultList.View = System.Windows.Forms.View.Details;
            this.lvResultList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lv_ColumnWidthChanging);
            this.lvResultList.DoubleClick += new System.EventHandler(this.lv_DoubleClick);
            this.lvResultList.Enter += new System.EventHandler(this.List_Enter);
            this.lvResultList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lv_KeyDown);
            // 
            // clOPERATION_DATE
            // 
            this.clOPERATION_DATE.Text = "取込日";
            this.clOPERATION_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clOPERATION_DATE.Width = 106;
            // 
            // clBATCH_NO
            // 
            this.clBATCH_NO.Text = "バッチ番号";
            this.clBATCH_NO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clBATCH_NO.Width = 103;
            // 
            // clCLEARING_DATE
            // 
            this.clCLEARING_DATE.Text = "交換日";
            this.clCLEARING_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clCLEARING_DATE.Width = 110;
            // 
            // clSCAN_BR_NO
            // 
            this.clSCAN_BR_NO.Text = "スキャン支店";
            this.clSCAN_BR_NO.Width = 184;
            // 
            // clSCAN_DATE
            // 
            this.clSCAN_DATE.Text = "スキャン日";
            this.clSCAN_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clSCAN_DATE.Width = 96;
            // 
            // clOC_BR_NO
            // 
            this.clOC_BR_NO.Text = "持出支店";
            this.clOC_BR_NO.Width = 192;
            // 
            // clDETAILS_NO
            // 
            this.clDETAILS_NO.Text = "明細連番";
            this.clDETAILS_NO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clDETAILS_NO.Width = 99;
            // 
            // clIC_OC_BK_NO
            // 
            this.clIC_OC_BK_NO.Text = "持帰銀行";
            this.clIC_OC_BK_NO.Width = 185;
            // 
            // clAMOUNT
            // 
            this.clAMOUNT.Text = "金額";
            this.clAMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clAMOUNT.Width = 140;
            // 
            // clDelete
            // 
            this.clDelete.Text = "削除状態";
            this.clDelete.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clDelete.Width = 120;
            // 
            // clUpload
            // 
            this.clUpload.Text = "アップロード状況";
            this.clUpload.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clUpload.Width = 140;
            // 
            // clBCASts
            // 
            this.clBCASts.Text = "持出取消結果";
            this.clBCASts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clBCASts.Width = 120;
            // 
            // clBUA
            // 
            this.clBUA.Text = "二重持出日";
            this.clBUA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clBUA.Width = 120;
            // 
            // clGMA
            // 
            this.clGMA.Text = "持帰訂正日";
            this.clGMA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clGMA.Width = 120;
            // 
            // clGRA
            // 
            this.clGRA.Text = "不渡返還日";
            this.clGRA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clGRA.Width = 120;
            // 
            // clGXA
            // 
            this.clGXA.Text = "決済後訂正日";
            this.clGXA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clGXA.Width = 120;
            // 
            // clICBKInptSts
            // 
            this.clICBKInptSts.Text = "入力状態(持帰銀行)";
            this.clICBKInptSts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clICBKInptSts.Width = 155;
            // 
            // clAMTInptSts
            // 
            this.clAMTInptSts.Text = "入力状態(金額)";
            this.clAMTInptSts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clAMTInptSts.Width = 155;
            // 
            // clKoukanjiriInptSts
            // 
            this.clKoukanjiriInptSts.Text = "入力状態(交換尻)";
            this.clKoukanjiriInptSts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clKoukanjiriInptSts.Width = 155;
            // 
            // clKoukanjiriTMNO
            // 
            this.clKoukanjiriTMNO.Text = "ロック端末(交換尻)";
            this.clKoukanjiriTMNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clKoukanjiriTMNO.Width = 155;
            // 
            // clICBKEOpe
            // 
            this.clICBKEOpe.Text = "オペレーター[持帰銀行]";
            this.clICBKEOpe.Width = 300;
            // 
            // clAmountEOpe
            // 
            this.clAmountEOpe.Text = "オペレーター[金額]";
            this.clAmountEOpe.Width = 300;
            // 
            // ntAmountTo
            // 
            this.ntAmountTo.Comma = true;
            this.ntAmountTo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntAmountTo.KeyControl = true;
            this.ntAmountTo.Location = new System.Drawing.Point(879, 65);
            this.ntAmountTo.MaxLength = 12;
            this.ntAmountTo.Name = "ntAmountTo";
            this.ntAmountTo.OnEnterSeparatorCut = true;
            this.ntAmountTo.Size = new System.Drawing.Size(125, 20);
            this.ntAmountTo.TabIndex = 11;
            this.ntAmountTo.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntAmountTo.Enter += new System.EventHandler(this.txt_Enter);
            this.ntAmountTo.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // ntAmountFrom
            // 
            this.ntAmountFrom.Comma = true;
            this.ntAmountFrom.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntAmountFrom.KeyControl = true;
            this.ntAmountFrom.Location = new System.Drawing.Point(719, 65);
            this.ntAmountFrom.MaxLength = 12;
            this.ntAmountFrom.Name = "ntAmountFrom";
            this.ntAmountFrom.OnEnterSeparatorCut = true;
            this.ntAmountFrom.Size = new System.Drawing.Size(125, 20);
            this.ntAmountFrom.TabIndex = 10;
            this.ntAmountFrom.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntAmountFrom.Enter += new System.EventHandler(this.txt_Enter);
            this.ntAmountFrom.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // ntOCBRNoFrom
            // 
            this.ntOCBRNoFrom.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntOCBRNoFrom.KeyControl = true;
            this.ntOCBRNoFrom.Location = new System.Drawing.Point(147, 43);
            this.ntOCBRNoFrom.MaxLength = 4;
            this.ntOCBRNoFrom.Name = "ntOCBRNoFrom";
            this.ntOCBRNoFrom.OnEnterSeparatorCut = true;
            this.ntOCBRNoFrom.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.ntOCBRNoFrom.Size = new System.Drawing.Size(125, 20);
            this.ntOCBRNoFrom.TabIndex = 4;
            this.ntOCBRNoFrom.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntOCBRNoFrom.Enter += new System.EventHandler(this.txt_Enter);
            this.ntOCBRNoFrom.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // dtClearingDataFrom
            // 
            this.dtClearingDataFrom.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtClearingDataFrom.KeyControl = true;
            this.dtClearingDataFrom.Location = new System.Drawing.Point(147, 65);
            this.dtClearingDataFrom.MaxLength = 8;
            this.dtClearingDataFrom.Name = "dtClearingDataFrom";
            this.dtClearingDataFrom.OnEnterSeparatorCut = true;
            this.dtClearingDataFrom.Size = new System.Drawing.Size(125, 20);
            this.dtClearingDataFrom.TabIndex = 8;
            this.dtClearingDataFrom.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtClearingDataFrom.Enter += new System.EventHandler(this.txt_Enter);
            this.dtClearingDataFrom.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // dtRdate
            // 
            this.dtRdate.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtRdate.KeyControl = true;
            this.dtRdate.Location = new System.Drawing.Point(147, 21);
            this.dtRdate.MaxLength = 8;
            this.dtRdate.Name = "dtRdate";
            this.dtRdate.OnEnterSeparatorCut = true;
            this.dtRdate.Size = new System.Drawing.Size(125, 20);
            this.dtRdate.TabIndex = 1;
            this.dtRdate.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtRdate.Enter += new System.EventHandler(this.txt_Enter);
            this.dtRdate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(851, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 23);
            this.label8.TabIndex = 100008;
            this.label8.Text = "～";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAmount
            // 
            this.lblAmount.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblAmount.Location = new System.Drawing.Point(592, 65);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(40, 23);
            this.lblAmount.TabIndex = 100009;
            this.lblAmount.Text = "金額";
            this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOCBRNo
            // 
            this.lblOCBRNo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOCBRNo.Location = new System.Drawing.Point(24, 43);
            this.lblOCBRNo.Name = "lblOCBRNo";
            this.lblOCBRNo.Size = new System.Drawing.Size(108, 23);
            this.lblOCBRNo.TabIndex = 100010;
            this.lblOCBRNo.Text = "持出支店コード";
            this.lblOCBRNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblClearingData
            // 
            this.lblClearingData.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblClearingData.Location = new System.Drawing.Point(24, 65);
            this.lblClearingData.Name = "lblClearingData";
            this.lblClearingData.Size = new System.Drawing.Size(108, 23);
            this.lblClearingData.TabIndex = 100011;
            this.lblClearingData.Text = "交換日";
            this.lblClearingData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRdate
            // 
            this.lblRdate.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblRdate.Location = new System.Drawing.Point(24, 21);
            this.lblRdate.Name = "lblRdate";
            this.lblRdate.Size = new System.Drawing.Size(108, 23);
            this.lblRdate.TabIndex = 100012;
            this.lblRdate.Text = "取込日";
            this.lblRdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(20, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 100007;
            this.label1.Text = "検索条件";
            // 
            // ntScanBrNo
            // 
            this.ntScanBrNo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntScanBrNo.KeyControl = true;
            this.ntScanBrNo.Location = new System.Drawing.Point(433, 21);
            this.ntScanBrNo.MaxLength = 4;
            this.ntScanBrNo.Name = "ntScanBrNo";
            this.ntScanBrNo.OnEnterSeparatorCut = true;
            this.ntScanBrNo.Size = new System.Drawing.Size(125, 20);
            this.ntScanBrNo.TabIndex = 2;
            this.ntScanBrNo.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntScanBrNo.Enter += new System.EventHandler(this.txt_Enter);
            this.ntScanBrNo.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // txtOpeNumer
            // 
            this.txtOpeNumer.EntryMode = CommonClass.ENTRYMODE.IMEOFF_KANA;
            this.txtOpeNumer.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtOpeNumer.KanaLock = false;
            this.txtOpeNumer.KeyControl = true;
            this.txtOpeNumer.Location = new System.Drawing.Point(719, 87);
            this.txtOpeNumer.MaxLength = 10;
            this.txtOpeNumer.Name = "txtOpeNumer";
            this.txtOpeNumer.Size = new System.Drawing.Size(125, 20);
            this.txtOpeNumer.TabIndex = 14;
            this.txtOpeNumer.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.txtOpeNumer.Enter += new System.EventHandler(this.txt_Enter);
            this.txtOpeNumer.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // ntBatNo
            // 
            this.ntBatNo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntBatNo.KeyControl = true;
            this.ntBatNo.Location = new System.Drawing.Point(719, 43);
            this.ntBatNo.MaxLength = 6;
            this.ntBatNo.Name = "ntBatNo";
            this.ntBatNo.OnEnterSeparatorCut = true;
            this.ntBatNo.Size = new System.Drawing.Size(125, 20);
            this.ntBatNo.TabIndex = 6;
            this.ntBatNo.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntBatNo.Enter += new System.EventHandler(this.txt_Enter);
            this.ntBatNo.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblstatus
            // 
            this.lblstatus.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblstatus.Location = new System.Drawing.Point(24, 87);
            this.lblstatus.Name = "lblstatus";
            this.lblstatus.Size = new System.Drawing.Size(108, 23);
            this.lblstatus.TabIndex = 100021;
            this.lblstatus.Text = "エントリー状態";
            this.lblstatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblScanBrNo
            // 
            this.lblScanBrNo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblScanBrNo.Location = new System.Drawing.Point(303, 21);
            this.lblScanBrNo.Name = "lblScanBrNo";
            this.lblScanBrNo.Size = new System.Drawing.Size(108, 23);
            this.lblScanBrNo.TabIndex = 100024;
            this.lblScanBrNo.Text = "スキャン支店";
            this.lblScanBrNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBatNo
            // 
            this.lblBatNo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBatNo.Location = new System.Drawing.Point(592, 43);
            this.lblBatNo.Name = "lblBatNo";
            this.lblBatNo.Size = new System.Drawing.Size(108, 20);
            this.lblBatNo.TabIndex = 100026;
            this.lblBatNo.Text = "バッチ番号";
            this.lblBatNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdoMemoOpt3
            // 
            this.rdoMemoOpt3.AutoSize = true;
            this.rdoMemoOpt3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rdoMemoOpt3.Location = new System.Drawing.Point(195, 3);
            this.rdoMemoOpt3.Name = "rdoMemoOpt3";
            this.rdoMemoOpt3.Size = new System.Drawing.Size(77, 17);
            this.rdoMemoOpt3.TabIndex = 18;
            this.rdoMemoOpt3.Text = "完全一致";
            this.rdoMemoOpt3.UseVisualStyleBackColor = true;
            this.rdoMemoOpt3.Enter += new System.EventHandler(this.cmb_Enter);
            this.rdoMemoOpt3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // rdoMemoOpt2
            // 
            this.rdoMemoOpt2.AutoSize = true;
            this.rdoMemoOpt2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rdoMemoOpt2.Location = new System.Drawing.Point(99, 3);
            this.rdoMemoOpt2.Name = "rdoMemoOpt2";
            this.rdoMemoOpt2.Size = new System.Drawing.Size(77, 17);
            this.rdoMemoOpt2.TabIndex = 17;
            this.rdoMemoOpt2.Text = "後方一致";
            this.rdoMemoOpt2.UseVisualStyleBackColor = true;
            this.rdoMemoOpt2.Enter += new System.EventHandler(this.cmb_Enter);
            this.rdoMemoOpt2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // rdoMemoOpt1
            // 
            this.rdoMemoOpt1.AutoSize = true;
            this.rdoMemoOpt1.Checked = true;
            this.rdoMemoOpt1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rdoMemoOpt1.Location = new System.Drawing.Point(3, 3);
            this.rdoMemoOpt1.Name = "rdoMemoOpt1";
            this.rdoMemoOpt1.Size = new System.Drawing.Size(77, 17);
            this.rdoMemoOpt1.TabIndex = 16;
            this.rdoMemoOpt1.TabStop = true;
            this.rdoMemoOpt1.Text = "前方一致";
            this.rdoMemoOpt1.UseVisualStyleBackColor = true;
            this.rdoMemoOpt1.Enter += new System.EventHandler(this.cmb_Enter);
            this.rdoMemoOpt1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // txtMemo
            // 
            this.txtMemo.EntryMode = CommonClass.ENTRYMODE.IMEON;
            this.txtMemo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtMemo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtMemo.KanaLock = false;
            this.txtMemo.KeyControl = true;
            this.txtMemo.Location = new System.Drawing.Point(147, 110);
            this.txtMemo.MaxLength = 256;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(727, 20);
            this.txtMemo.TabIndex = 15;
            this.txtMemo.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.txtMemo.Enter += new System.EventHandler(this.txt_Enter);
            this.txtMemo.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label19.Location = new System.Drawing.Point(24, 177);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(108, 23);
            this.label19.TabIndex = 100035;
            this.label19.Text = "不渡返還結果";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label18.Location = new System.Drawing.Point(876, 154);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(108, 23);
            this.label18.TabIndex = 100036;
            this.label18.Text = "持帰訂正";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMemo
            // 
            this.lblMemo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMemo.Location = new System.Drawing.Point(24, 110);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(118, 23);
            this.lblMemo.TabIndex = 100037;
            this.lblMemo.Text = "メモ";
            this.lblMemo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblImgFLNm
            // 
            this.lblImgFLNm.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblImgFLNm.Location = new System.Drawing.Point(24, 132);
            this.lblImgFLNm.Name = "lblImgFLNm";
            this.lblImgFLNm.Size = new System.Drawing.Size(118, 23);
            this.lblImgFLNm.TabIndex = 100037;
            this.lblImgFLNm.Text = "イメージファイル名";
            this.lblImgFLNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtImgFLNm
            // 
            this.txtImgFLNm.EntryMode = CommonClass.ENTRYMODE.IMEOFF_KANA;
            this.txtImgFLNm.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtImgFLNm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtImgFLNm.KanaLock = false;
            this.txtImgFLNm.KeyControl = true;
            this.txtImgFLNm.Location = new System.Drawing.Point(147, 132);
            this.txtImgFLNm.MaxLength = 62;
            this.txtImgFLNm.Name = "txtImgFLNm";
            this.txtImgFLNm.Size = new System.Drawing.Size(727, 20);
            this.txtImgFLNm.TabIndex = 19;
            this.txtImgFLNm.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.txtImgFLNm.Enter += new System.EventHandler(this.txt_Enter);
            this.txtImgFLNm.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // rdoImgOpt1
            // 
            this.rdoImgOpt1.AutoSize = true;
            this.rdoImgOpt1.Checked = true;
            this.rdoImgOpt1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rdoImgOpt1.Location = new System.Drawing.Point(3, 3);
            this.rdoImgOpt1.Name = "rdoImgOpt1";
            this.rdoImgOpt1.Size = new System.Drawing.Size(77, 17);
            this.rdoImgOpt1.TabIndex = 20;
            this.rdoImgOpt1.TabStop = true;
            this.rdoImgOpt1.Text = "前方一致";
            this.rdoImgOpt1.UseVisualStyleBackColor = true;
            this.rdoImgOpt1.Enter += new System.EventHandler(this.cmb_Enter);
            this.rdoImgOpt1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // rdoImgOpt2
            // 
            this.rdoImgOpt2.AutoSize = true;
            this.rdoImgOpt2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rdoImgOpt2.Location = new System.Drawing.Point(99, 3);
            this.rdoImgOpt2.Name = "rdoImgOpt2";
            this.rdoImgOpt2.Size = new System.Drawing.Size(77, 17);
            this.rdoImgOpt2.TabIndex = 21;
            this.rdoImgOpt2.Text = "後方一致";
            this.rdoImgOpt2.UseVisualStyleBackColor = true;
            this.rdoImgOpt2.Enter += new System.EventHandler(this.cmb_Enter);
            this.rdoImgOpt2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // rdoImgOpt3
            // 
            this.rdoImgOpt3.AutoSize = true;
            this.rdoImgOpt3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rdoImgOpt3.Location = new System.Drawing.Point(195, 3);
            this.rdoImgOpt3.Name = "rdoImgOpt3";
            this.rdoImgOpt3.Size = new System.Drawing.Size(77, 17);
            this.rdoImgOpt3.TabIndex = 22;
            this.rdoImgOpt3.Text = "完全一致";
            this.rdoImgOpt3.UseVisualStyleBackColor = true;
            this.rdoImgOpt3.Enter += new System.EventHandler(this.cmb_Enter);
            this.rdoImgOpt3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label20.Location = new System.Drawing.Point(24, 154);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(118, 23);
            this.label20.TabIndex = 100037;
            this.label20.Text = "アップロード状況";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblICNo
            // 
            this.lblICNo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblICNo.Location = new System.Drawing.Point(876, 43);
            this.lblICNo.Name = "lblICNo";
            this.lblICNo.Size = new System.Drawing.Size(121, 23);
            this.lblICNo.TabIndex = 100010;
            this.lblICNo.Text = "持帰銀行コード";
            this.lblICNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ntICBKNo
            // 
            this.ntICBKNo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntICBKNo.KeyControl = true;
            this.ntICBKNo.Location = new System.Drawing.Point(1003, 43);
            this.ntICBKNo.MaxLength = 4;
            this.ntICBKNo.Name = "ntICBKNo";
            this.ntICBKNo.OnEnterSeparatorCut = true;
            this.ntICBKNo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.ntICBKNo.Size = new System.Drawing.Size(125, 20);
            this.ntICBKNo.TabIndex = 7;
            this.ntICBKNo.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntICBKNo.Enter += new System.EventHandler(this.txt_Enter);
            this.ntICBKNo.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label22.Location = new System.Drawing.Point(278, 65);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(24, 23);
            this.label22.TabIndex = 100008;
            this.label22.Text = "～";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtClearingDataTo
            // 
            this.dtClearingDataTo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtClearingDataTo.KeyControl = true;
            this.dtClearingDataTo.Location = new System.Drawing.Point(303, 65);
            this.dtClearingDataTo.MaxLength = 8;
            this.dtClearingDataTo.Name = "dtClearingDataTo";
            this.dtClearingDataTo.OnEnterSeparatorCut = true;
            this.dtClearingDataTo.Size = new System.Drawing.Size(125, 20);
            this.dtClearingDataTo.TabIndex = 9;
            this.dtClearingDataTo.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtClearingDataTo.Enter += new System.EventHandler(this.txt_Enter);
            this.dtClearingDataTo.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label23.Location = new System.Drawing.Point(303, 87);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(107, 23);
            this.label23.TabIndex = 100010;
            this.label23.Text = "削除データ";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblScanDate
            // 
            this.lblScanDate.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblScanDate.Location = new System.Drawing.Point(592, 21);
            this.lblScanDate.Name = "lblScanDate";
            this.lblScanDate.Size = new System.Drawing.Size(108, 23);
            this.lblScanDate.TabIndex = 100012;
            this.lblScanDate.Text = "スキャン日";
            this.lblScanDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtScanDate
            // 
            this.dtScanDate.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtScanDate.KeyControl = true;
            this.dtScanDate.Location = new System.Drawing.Point(719, 21);
            this.dtScanDate.MaxLength = 8;
            this.dtScanDate.Name = "dtScanDate";
            this.dtScanDate.OnEnterSeparatorCut = true;
            this.dtScanDate.Size = new System.Drawing.Size(125, 20);
            this.dtScanDate.TabIndex = 3;
            this.dtScanDate.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtScanDate.Enter += new System.EventHandler(this.txt_Enter);
            this.dtScanDate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label25.Location = new System.Drawing.Point(592, 87);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(108, 23);
            this.label25.TabIndex = 100012;
            this.label25.Text = "オペレーターID";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(592, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 23);
            this.label9.TabIndex = 100010;
            this.label9.Text = "二重持出";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(303, 177);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 23);
            this.label10.TabIndex = 100036;
            this.label10.Text = "決済後訂正";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.Location = new System.Drawing.Point(303, 154);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(106, 23);
            this.label13.TabIndex = 100036;
            this.label13.Text = "持出取消結果";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label15.Location = new System.Drawing.Point(15, 204);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 13);
            this.label15.TabIndex = 100007;
            this.label15.Text = "検索結果";
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.SystemColors.Window;
            this.cmbStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(147, 87);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(125, 21);
            this.cmbStatus.TabIndex = 12;
            this.cmbStatus.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbStatus.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // cmbDelete
            // 
            this.cmbDelete.BackColor = System.Drawing.SystemColors.Window;
            this.cmbDelete.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDelete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDelete.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbDelete.FormattingEnabled = true;
            this.cmbDelete.Location = new System.Drawing.Point(433, 87);
            this.cmbDelete.Name = "cmbDelete";
            this.cmbDelete.Size = new System.Drawing.Size(125, 21);
            this.cmbDelete.TabIndex = 13;
            this.cmbDelete.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbDelete.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // cmbBUASTS
            // 
            this.cmbBUASTS.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBUASTS.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBUASTS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBUASTS.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbBUASTS.FormattingEnabled = true;
            this.cmbBUASTS.Location = new System.Drawing.Point(147, 154);
            this.cmbBUASTS.Name = "cmbBUASTS";
            this.cmbBUASTS.Size = new System.Drawing.Size(125, 21);
            this.cmbBUASTS.TabIndex = 23;
            this.cmbBUASTS.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbBUASTS.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbBUASTS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // cmbGMB
            // 
            this.cmbGMB.BackColor = System.Drawing.SystemColors.Window;
            this.cmbGMB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGMB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGMB.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbGMB.FormattingEnabled = true;
            this.cmbGMB.Location = new System.Drawing.Point(1003, 154);
            this.cmbGMB.Name = "cmbGMB";
            this.cmbGMB.Size = new System.Drawing.Size(125, 21);
            this.cmbGMB.TabIndex = 26;
            this.cmbGMB.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbGMB.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbGMB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // cmbBCASTS
            // 
            this.cmbBCASTS.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBCASTS.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBCASTS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBCASTS.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbBCASTS.FormattingEnabled = true;
            this.cmbBCASTS.Location = new System.Drawing.Point(433, 154);
            this.cmbBCASTS.Name = "cmbBCASTS";
            this.cmbBCASTS.Size = new System.Drawing.Size(125, 21);
            this.cmbBCASTS.TabIndex = 24;
            this.cmbBCASTS.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbBCASTS.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            this.cmbBCASTS.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbBCASTS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // cmbGRASTS
            // 
            this.cmbGRASTS.BackColor = System.Drawing.SystemColors.Window;
            this.cmbGRASTS.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGRASTS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGRASTS.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbGRASTS.FormattingEnabled = true;
            this.cmbGRASTS.Location = new System.Drawing.Point(147, 177);
            this.cmbGRASTS.Name = "cmbGRASTS";
            this.cmbGRASTS.Size = new System.Drawing.Size(125, 21);
            this.cmbGRASTS.TabIndex = 27;
            this.cmbGRASTS.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbGRASTS.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            this.cmbGRASTS.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbGRASTS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // cmbBUA
            // 
            this.cmbBUA.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBUA.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBUA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBUA.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbBUA.FormattingEnabled = true;
            this.cmbBUA.Location = new System.Drawing.Point(719, 154);
            this.cmbBUA.Name = "cmbBUA";
            this.cmbBUA.Size = new System.Drawing.Size(125, 21);
            this.cmbBUA.TabIndex = 25;
            this.cmbBUA.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbBUA.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbBUA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // cmbGXA
            // 
            this.cmbGXA.BackColor = System.Drawing.SystemColors.Window;
            this.cmbGXA.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGXA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGXA.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbGXA.FormattingEnabled = true;
            this.cmbGXA.Location = new System.Drawing.Point(433, 177);
            this.cmbGXA.Name = "cmbGXA";
            this.cmbGXA.Size = new System.Drawing.Size(125, 21);
            this.cmbGXA.TabIndex = 28;
            this.cmbGXA.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbGXA.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbGXA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // pnlMemoOpt
            // 
            this.pnlMemoOpt.Controls.Add(this.rdoMemoOpt1);
            this.pnlMemoOpt.Controls.Add(this.rdoMemoOpt2);
            this.pnlMemoOpt.Controls.Add(this.rdoMemoOpt3);
            this.pnlMemoOpt.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pnlMemoOpt.Location = new System.Drawing.Point(880, 110);
            this.pnlMemoOpt.Name = "pnlMemoOpt";
            this.pnlMemoOpt.Size = new System.Drawing.Size(286, 23);
            this.pnlMemoOpt.TabIndex = 16;
            // 
            // pnlImgOpt
            // 
            this.pnlImgOpt.Controls.Add(this.rdoImgOpt1);
            this.pnlImgOpt.Controls.Add(this.rdoImgOpt2);
            this.pnlImgOpt.Controls.Add(this.rdoImgOpt3);
            this.pnlImgOpt.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pnlImgOpt.Location = new System.Drawing.Point(880, 132);
            this.pnlImgOpt.Name = "pnlImgOpt";
            this.pnlImgOpt.Size = new System.Drawing.Size(286, 22);
            this.pnlImgOpt.TabIndex = 20;
            // 
            // cmbDef
            // 
            this.cmbDef.BackColor = System.Drawing.SystemColors.Window;
            this.cmbDef.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDef.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbDef.FormattingEnabled = true;
            this.cmbDef.Location = new System.Drawing.Point(719, 177);
            this.cmbDef.Name = "cmbDef";
            this.cmbDef.Size = new System.Drawing.Size(125, 21);
            this.cmbDef.TabIndex = 29;
            this.cmbDef.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbDef.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbDef.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(592, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 23);
            this.label3.TabIndex = 100039;
            this.label3.Text = "省略値検索";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(278, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 23);
            this.label5.TabIndex = 100041;
            this.label5.Text = "～";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ntOCBRNoTo
            // 
            this.ntOCBRNoTo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntOCBRNoTo.KeyControl = true;
            this.ntOCBRNoTo.Location = new System.Drawing.Point(303, 43);
            this.ntOCBRNoTo.MaxLength = 4;
            this.ntOCBRNoTo.Name = "ntOCBRNoTo";
            this.ntOCBRNoTo.OnEnterSeparatorCut = true;
            this.ntOCBRNoTo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.ntOCBRNoTo.Size = new System.Drawing.Size(125, 20);
            this.ntOCBRNoTo.TabIndex = 5;
            this.ntOCBRNoTo.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntOCBRNoTo.Enter += new System.EventHandler(this.txt_Enter);
            this.ntOCBRNoTo.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // cmbEditFlg
            // 
            this.cmbEditFlg.BackColor = System.Drawing.SystemColors.Window;
            this.cmbEditFlg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEditFlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditFlg.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbEditFlg.FormattingEnabled = true;
            this.cmbEditFlg.Location = new System.Drawing.Point(1003, 177);
            this.cmbEditFlg.Name = "cmbEditFlg";
            this.cmbEditFlg.Size = new System.Drawing.Size(125, 21);
            this.cmbEditFlg.TabIndex = 30;
            this.cmbEditFlg.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbEditFlg.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbEditFlg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(876, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 23);
            this.label6.TabIndex = 100043;
            this.label6.Text = "編集フラグ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchOcMeiList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SearchOcMeiList";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.pnlMemoOpt.ResumeLayout(false);
            this.pnlMemoOpt.PerformLayout();
            this.pnlImgOpt.ResumeLayout(false);
            this.pnlImgOpt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvResultList;
        private System.Windows.Forms.ColumnHeader clOPERATION_DATE;
        private System.Windows.Forms.ColumnHeader clSCAN_BR_NO;
        private System.Windows.Forms.ColumnHeader clBATCH_NO;
        private System.Windows.Forms.ColumnHeader clCLEARING_DATE;
        private System.Windows.Forms.RadioButton rdoMemoOpt3;
        private System.Windows.Forms.RadioButton rdoMemoOpt2;
        private System.Windows.Forms.RadioButton rdoMemoOpt1;
        private CommonClass.KanaTextBox txtMemo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblMemo;
        private CommonClass.NumTextBox2 ntScanBrNo;
        private CommonClass.KanaTextBox txtOpeNumer;
        private CommonClass.NumTextBox2 ntBatNo;
        private System.Windows.Forms.Label lblstatus;
        private System.Windows.Forms.Label lblScanBrNo;
        private System.Windows.Forms.Label lblBatNo;
        private CommonClass.NumTextBox2 ntAmountTo;
        private CommonClass.NumTextBox2 ntAmountFrom;
        private CommonClass.DTextBox2 dtClearingDataFrom;
        private CommonClass.DTextBox2 dtRdate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblOCBRNo;
        private System.Windows.Forms.Label lblClearingData;
        private System.Windows.Forms.Label lblRdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoImgOpt3;
        private System.Windows.Forms.RadioButton rdoImgOpt2;
        private System.Windows.Forms.RadioButton rdoImgOpt1;
        private CommonClass.KanaTextBox txtImgFLNm;
        private System.Windows.Forms.Label lblImgFLNm;
        private System.Windows.Forms.Label label20;
        private CommonClass.NumTextBox2 ntICBKNo;
        private System.Windows.Forms.Label lblICNo;
        private CommonClass.DTextBox2 dtClearingDataTo;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private CommonClass.DTextBox2 dtScanDate;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblScanDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ColumnHeader clSCAN_DATE;
        private System.Windows.Forms.ColumnHeader clOC_BR_NO;
        private System.Windows.Forms.ColumnHeader clDETAILS_NO;
        private System.Windows.Forms.ColumnHeader clIC_OC_BK_NO;
        private System.Windows.Forms.ColumnHeader clAMOUNT;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbDelete;
        private System.Windows.Forms.ComboBox cmbBUASTS;
        private System.Windows.Forms.ComboBox cmbGMB;
        private System.Windows.Forms.ComboBox cmbBCASTS;
        private System.Windows.Forms.ComboBox cmbGRASTS;
        private System.Windows.Forms.ComboBox cmbBUA;
        private System.Windows.Forms.ComboBox cmbGXA;
        private System.Windows.Forms.Panel pnlMemoOpt;
        private System.Windows.Forms.Panel pnlImgOpt;
        private System.Windows.Forms.ColumnHeader clDelete;
        private System.Windows.Forms.ColumnHeader clUpload;
        private System.Windows.Forms.ColumnHeader clBCASts;
        private System.Windows.Forms.ColumnHeader clBUA;
        private System.Windows.Forms.ColumnHeader clGMA;
        private System.Windows.Forms.ColumnHeader clGRA;
        private System.Windows.Forms.ColumnHeader clGXA;
        private System.Windows.Forms.ComboBox cmbDef;
        private System.Windows.Forms.Label label3;
        private CommonClass.NumTextBox2 ntOCBRNoFrom;
        private System.Windows.Forms.ColumnHeader clICBKInptSts;
        private System.Windows.Forms.ColumnHeader clAMTInptSts;
        private System.Windows.Forms.ColumnHeader clKoukanjiriInptSts;
        private System.Windows.Forms.ColumnHeader clKoukanjiriTMNO;
        private System.Windows.Forms.ColumnHeader clICBKEOpe;
        private System.Windows.Forms.ColumnHeader clAmountEOpe;
        private CommonClass.NumTextBox2 ntOCBRNoTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbEditFlg;
        private System.Windows.Forms.Label label6;
    }
}
