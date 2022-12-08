namespace SearchIcMeiView
{
    partial class SearchIcMeiList
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
            this.clOPE_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clOCBKNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clOCBKNM = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDETAILS_NO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCLEARING_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAMOUNT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBillCD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clSyuruiCD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clICBrCD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clICBrNm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAccount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTegata = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDelete = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clTeisei = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clFuwatari = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBMASts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBRASts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clPayKbn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clBUB = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clICBKInptSts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCDATEInptSts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAMTInptSts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clKoukanjiriInptSts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clKoukanjiriTMNO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clJikouInptSts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clJikouTMNO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clICBKEOpe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clICBKVOpe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCDateEOpe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clCDateVOpe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAmountEOpe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAmountVOpe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clJikouEOpe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clJikouVOpe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ntAmountTo = new CommonClass.NumTextBox2();
            this.ntAmountFrom = new CommonClass.NumTextBox2();
            this.dtClearingDate = new CommonClass.DTextBox2();
            this.dtRdate = new CommonClass.DTextBox2();
            this.label8 = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblClearingDate = new System.Windows.Forms.Label();
            this.lblRdate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVeriOpe = new CommonClass.KanaTextBox();
            this.lblstatus = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblTeiseiFlg = new System.Windows.Forms.Label();
            this.lblImgFLNm = new System.Windows.Forms.Label();
            this.txtImgFLNm = new CommonClass.KanaTextBox();
            this.rdoImgOpt1 = new System.Windows.Forms.RadioButton();
            this.rdoImgOpt2 = new System.Windows.Forms.RadioButton();
            this.rdoImgOpt3 = new System.Windows.Forms.RadioButton();
            this.lblOCNo = new System.Windows.Forms.Label();
            this.ntOCNo = new CommonClass.NumTextBox2();
            this.lblVeriOpe = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbTeiseiFlg = new System.Windows.Forms.ComboBox();
            this.cmbFuwatariFlg = new System.Windows.Forms.ComboBox();
            this.cmbGMASts = new System.Windows.Forms.ComboBox();
            this.pnlImgOpt = new System.Windows.Forms.Panel();
            this.cmbGRASts = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ntBillFrom = new CommonClass.NumTextBox2();
            this.lblBill = new System.Windows.Forms.Label();
            this.ntSyuri = new CommonClass.NumTextBox2();
            this.lblSyuri = new System.Windows.Forms.Label();
            this.ntAccount = new CommonClass.NumTextBox2();
            this.lblAccount = new System.Windows.Forms.Label();
            this.ntBrNo = new CommonClass.NumTextBox2();
            this.lblBrNo = new System.Windows.Forms.Label();
            this.ntTegata = new CommonClass.NumTextBox2();
            this.lblTegata = new System.Windows.Forms.Label();
            this.txtEntOpe = new CommonClass.KanaTextBox();
            this.lblEntOpe = new System.Windows.Forms.Label();
            this.lblSearchSortMode = new System.Windows.Forms.Label();
            this.cmbDelete = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbPayKbn = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ntBillTo = new CommonClass.NumTextBox2();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbBUB = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            clKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentsPanel.SuspendLayout();
            this.pnlImgOpt.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contentsPanel.Controls.Add(this.cmbBUB);
            this.contentsPanel.Controls.Add(this.label7);
            this.contentsPanel.Controls.Add(this.label6);
            this.contentsPanel.Controls.Add(this.ntBillTo);
            this.contentsPanel.Controls.Add(this.cmbPayKbn);
            this.contentsPanel.Controls.Add(this.label5);
            this.contentsPanel.Controls.Add(this.cmbDelete);
            this.contentsPanel.Controls.Add(this.label23);
            this.contentsPanel.Controls.Add(this.lblSearchSortMode);
            this.contentsPanel.Controls.Add(this.txtEntOpe);
            this.contentsPanel.Controls.Add(this.lblEntOpe);
            this.contentsPanel.Controls.Add(this.ntTegata);
            this.contentsPanel.Controls.Add(this.lblTegata);
            this.contentsPanel.Controls.Add(this.ntAccount);
            this.contentsPanel.Controls.Add(this.lblAccount);
            this.contentsPanel.Controls.Add(this.ntBrNo);
            this.contentsPanel.Controls.Add(this.lblBrNo);
            this.contentsPanel.Controls.Add(this.ntSyuri);
            this.contentsPanel.Controls.Add(this.lblSyuri);
            this.contentsPanel.Controls.Add(this.ntBillFrom);
            this.contentsPanel.Controls.Add(this.lblBill);
            this.contentsPanel.Controls.Add(this.cmbGRASts);
            this.contentsPanel.Controls.Add(this.label3);
            this.contentsPanel.Controls.Add(this.pnlImgOpt);
            this.contentsPanel.Controls.Add(this.cmbGMASts);
            this.contentsPanel.Controls.Add(this.cmbFuwatariFlg);
            this.contentsPanel.Controls.Add(this.cmbTeiseiFlg);
            this.contentsPanel.Controls.Add(this.cmbStatus);
            this.contentsPanel.Controls.Add(this.txtImgFLNm);
            this.contentsPanel.Controls.Add(this.label19);
            this.contentsPanel.Controls.Add(this.label10);
            this.contentsPanel.Controls.Add(this.lblTeiseiFlg);
            this.contentsPanel.Controls.Add(this.lblImgFLNm);
            this.contentsPanel.Controls.Add(this.txtVeriOpe);
            this.contentsPanel.Controls.Add(this.lblstatus);
            this.contentsPanel.Controls.Add(this.ntAmountTo);
            this.contentsPanel.Controls.Add(this.ntAmountFrom);
            this.contentsPanel.Controls.Add(this.ntOCNo);
            this.contentsPanel.Controls.Add(this.dtClearingDate);
            this.contentsPanel.Controls.Add(this.dtRdate);
            this.contentsPanel.Controls.Add(this.label8);
            this.contentsPanel.Controls.Add(this.lblAmount);
            this.contentsPanel.Controls.Add(this.lblOCNo);
            this.contentsPanel.Controls.Add(this.lblClearingDate);
            this.contentsPanel.Controls.Add(this.lblVeriOpe);
            this.contentsPanel.Controls.Add(this.lblRdate);
            this.contentsPanel.Controls.Add(this.label15);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.Add(this.lvResultList);
            this.contentsPanel.Controls.SetChildIndex(this.lvResultList, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label15, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblRdate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblVeriOpe, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblClearingDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblOCNo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblAmount, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label8, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtRdate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dtClearingDate, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntOCNo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntAmountFrom, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntAmountTo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblstatus, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtVeriOpe, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblImgFLNm, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblTeiseiFlg, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label10, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label19, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtImgFLNm, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbStatus, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbTeiseiFlg, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbFuwatariFlg, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbGMASts, 0);
            this.contentsPanel.Controls.SetChildIndex(this.pnlImgOpt, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label3, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbGRASts, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblBill, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntBillFrom, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblSyuri, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntSyuri, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblBrNo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntBrNo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblAccount, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntAccount, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblTegata, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntTegata, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblEntOpe, 0);
            this.contentsPanel.Controls.SetChildIndex(this.txtEntOpe, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblSearchSortMode, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label23, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbDelete, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label5, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbPayKbn, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ntBillTo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label6, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label7, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbBUB, 0);
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
            this.clOPE_DATE,
            this.clOCBKNo,
            this.clOCBKNM,
            this.clDETAILS_NO,
            this.clCLEARING_DATE,
            this.clAMOUNT,
            this.clBillCD,
            this.clSyuruiCD,
            this.clICBrCD,
            this.clICBrNm,
            this.clAccount,
            this.clTegata,
            this.clDelete,
            this.clTeisei,
            this.clFuwatari,
            this.clBMASts,
            this.clBRASts,
            this.clPayKbn,
            this.clBUB,
            this.clICBKInptSts,
            this.clCDATEInptSts,
            this.clAMTInptSts,
            this.clKoukanjiriInptSts,
            this.clKoukanjiriTMNO,
            this.clJikouInptSts,
            this.clJikouTMNO,
            this.clICBKEOpe,
            this.clICBKVOpe,
            this.clCDateEOpe,
            this.clCDateVOpe,
            this.clAmountEOpe,
            this.clAmountVOpe,
            this.clJikouEOpe,
            this.clJikouVOpe});
            this.lvResultList.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvResultList.FullRowSelect = true;
            this.lvResultList.GridLines = true;
            this.lvResultList.HideSelection = false;
            this.lvResultList.Location = new System.Drawing.Point(20, 223);
            this.lvResultList.MultiSelect = false;
            this.lvResultList.Name = "lvResultList";
            this.lvResultList.Size = new System.Drawing.Size(1231, 612);
            this.lvResultList.TabIndex = 28;
            this.lvResultList.TabStop = false;
            this.lvResultList.UseCompatibleStateImageBehavior = false;
            this.lvResultList.View = System.Windows.Forms.View.Details;
            this.lvResultList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lv_ColumnWidthChanging);
            this.lvResultList.DoubleClick += new System.EventHandler(this.lv_DoubleClick);
            this.lvResultList.Enter += new System.EventHandler(this.List_Enter);
            this.lvResultList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lv_KeyDown);
            // 
            // clOPE_DATE
            // 
            this.clOPE_DATE.Text = "取込日";
            this.clOPE_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clOPE_DATE.Width = 90;
            // 
            // clOCBKNo
            // 
            this.clOCBKNo.Text = "持出銀行コード";
            this.clOCBKNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clOCBKNo.Width = 110;
            // 
            // clOCBKNM
            // 
            this.clOCBKNM.Text = "持出銀行名";
            this.clOCBKNM.Width = 200;
            // 
            // clDETAILS_NO
            // 
            this.clDETAILS_NO.Text = "明細連番";
            this.clDETAILS_NO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clDETAILS_NO.Width = 80;
            // 
            // clCLEARING_DATE
            // 
            this.clCLEARING_DATE.Text = "交換日";
            this.clCLEARING_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clCLEARING_DATE.Width = 90;
            // 
            // clAMOUNT
            // 
            this.clAMOUNT.Text = "金額";
            this.clAMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clAMOUNT.Width = 140;
            // 
            // clBillCD
            // 
            this.clBillCD.Text = "証券種類";
            this.clBillCD.Width = 180;
            // 
            // clSyuruiCD
            // 
            this.clSyuruiCD.Text = "手形種類";
            this.clSyuruiCD.Width = 180;
            // 
            // clICBrCD
            // 
            this.clICBrCD.Text = "支店番号";
            this.clICBrCD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clICBrCD.Width = 110;
            // 
            // clICBrNm
            // 
            this.clICBrNm.Text = "支店名";
            this.clICBrNm.Width = 200;
            // 
            // clAccount
            // 
            this.clAccount.Text = "口座番号";
            this.clAccount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clAccount.Width = 100;
            // 
            // clTegata
            // 
            this.clTegata.Text = "手形番号";
            this.clTegata.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clTegata.Width = 100;
            // 
            // clDelete
            // 
            this.clDelete.Text = "削除状態";
            this.clDelete.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clDelete.Width = 120;
            // 
            // clTeisei
            // 
            this.clTeisei.Text = "訂正入力";
            this.clTeisei.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clTeisei.Width = 90;
            // 
            // clFuwatari
            // 
            this.clFuwatari.Text = "不渡入力";
            this.clFuwatari.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clFuwatari.Width = 120;
            // 
            // clBMASts
            // 
            this.clBMASts.Text = "訂正結果";
            this.clBMASts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clBMASts.Width = 120;
            // 
            // clBRASts
            // 
            this.clBRASts.Text = "不渡返還結果";
            this.clBRASts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clBRASts.Width = 120;
            // 
            // clPayKbn
            // 
            this.clPayKbn.Text = "決済対象区分";
            this.clPayKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clPayKbn.Width = 100;
            // 
            // clBUB
            // 
            this.clBUB.Text = "二重持出日";
            this.clBUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clBUB.Width = 110;
            // 
            // clICBKInptSts
            // 
            this.clICBKInptSts.Text = "入力状態(持帰銀行)";
            this.clICBKInptSts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clICBKInptSts.Width = 155;
            // 
            // clCDATEInptSts
            // 
            this.clCDATEInptSts.Text = "入力状態(交換希望日)";
            this.clCDATEInptSts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clCDATEInptSts.Width = 155;
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
            // clJikouInptSts
            // 
            this.clJikouInptSts.Text = "入力状態(自行情報)";
            this.clJikouInptSts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clJikouInptSts.Width = 155;
            // 
            // clJikouTMNO
            // 
            this.clJikouTMNO.Text = "ロック端末(自行情報)";
            this.clJikouTMNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clJikouTMNO.Width = 155;
            // 
            // clICBKEOpe
            // 
            this.clICBKEOpe.Text = "エントリーオペレーター[持帰銀行]";
            this.clICBKEOpe.Width = 300;
            // 
            // clICBKVOpe
            // 
            this.clICBKVOpe.Text = "ベリファイオペレーター[持帰銀行]";
            this.clICBKVOpe.Width = 300;
            // 
            // clCDateEOpe
            // 
            this.clCDateEOpe.Text = "エントリーオペレーター[交換希望日]";
            this.clCDateEOpe.Width = 300;
            // 
            // clCDateVOpe
            // 
            this.clCDateVOpe.Text = "ベリファイオペレーター[交換希望日]";
            this.clCDateVOpe.Width = 300;
            // 
            // clAmountEOpe
            // 
            this.clAmountEOpe.Text = "エントリーオペレーター[金額]";
            this.clAmountEOpe.Width = 300;
            // 
            // clAmountVOpe
            // 
            this.clAmountVOpe.Text = "ベリファイオペレーター[金額]";
            this.clAmountVOpe.Width = 300;
            // 
            // clJikouEOpe
            // 
            this.clJikouEOpe.Text = "エントリーオペレーター[自行情報]";
            this.clJikouEOpe.Width = 300;
            // 
            // clJikouVOpe
            // 
            this.clJikouVOpe.Text = "ベリファイオペレーター[自行情報]";
            this.clJikouVOpe.Width = 300;
            // 
            // ntAmountTo
            // 
            this.ntAmountTo.Comma = true;
            this.ntAmountTo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntAmountTo.KeyControl = true;
            this.ntAmountTo.Location = new System.Drawing.Point(1110, 42);
            this.ntAmountTo.MaxLength = 12;
            this.ntAmountTo.Name = "ntAmountTo";
            this.ntAmountTo.OnEnterSeparatorCut = true;
            this.ntAmountTo.Size = new System.Drawing.Size(125, 20);
            this.ntAmountTo.TabIndex = 5;
            this.ntAmountTo.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntAmountTo.Enter += new System.EventHandler(this.txt_Enter);
            this.ntAmountTo.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // ntAmountFrom
            // 
            this.ntAmountFrom.Comma = true;
            this.ntAmountFrom.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntAmountFrom.KeyControl = true;
            this.ntAmountFrom.Location = new System.Drawing.Point(950, 42);
            this.ntAmountFrom.MaxLength = 12;
            this.ntAmountFrom.Name = "ntAmountFrom";
            this.ntAmountFrom.OnEnterSeparatorCut = true;
            this.ntAmountFrom.Size = new System.Drawing.Size(125, 20);
            this.ntAmountFrom.TabIndex = 4;
            this.ntAmountFrom.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntAmountFrom.Enter += new System.EventHandler(this.txt_Enter);
            this.ntAmountFrom.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // dtClearingDate
            // 
            this.dtClearingDate.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtClearingDate.KeyControl = true;
            this.dtClearingDate.Location = new System.Drawing.Point(697, 42);
            this.dtClearingDate.MaxLength = 8;
            this.dtClearingDate.Name = "dtClearingDate";
            this.dtClearingDate.OnEnterSeparatorCut = true;
            this.dtClearingDate.Size = new System.Drawing.Size(125, 20);
            this.dtClearingDate.TabIndex = 3;
            this.dtClearingDate.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.dtClearingDate.Enter += new System.EventHandler(this.txt_Enter);
            this.dtClearingDate.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // dtRdate
            // 
            this.dtRdate.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtRdate.KeyControl = true;
            this.dtRdate.Location = new System.Drawing.Point(128, 42);
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
            this.label8.Location = new System.Drawing.Point(1083, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 20);
            this.label8.TabIndex = 100008;
            this.label8.Text = "～";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAmount
            // 
            this.lblAmount.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblAmount.Location = new System.Drawing.Point(847, 42);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(98, 20);
            this.lblAmount.TabIndex = 100009;
            this.lblAmount.Text = "金額";
            this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblClearingDate
            // 
            this.lblClearingDate.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblClearingDate.Location = new System.Drawing.Point(594, 42);
            this.lblClearingDate.Name = "lblClearingDate";
            this.lblClearingDate.Size = new System.Drawing.Size(98, 20);
            this.lblClearingDate.TabIndex = 100011;
            this.lblClearingDate.Text = "交換日";
            this.lblClearingDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRdate
            // 
            this.lblRdate.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblRdate.Location = new System.Drawing.Point(24, 42);
            this.lblRdate.Name = "lblRdate";
            this.lblRdate.Size = new System.Drawing.Size(98, 20);
            this.lblRdate.TabIndex = 100012;
            this.lblRdate.Text = "取込日";
            this.lblRdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(18, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 100007;
            this.label1.Text = "検索条件";
            // 
            // txtVeriOpe
            // 
            this.txtVeriOpe.EntryMode = CommonClass.ENTRYMODE.IMEOFF_KANA;
            this.txtVeriOpe.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtVeriOpe.KanaLock = false;
            this.txtVeriOpe.KeyControl = true;
            this.txtVeriOpe.Location = new System.Drawing.Point(697, 109);
            this.txtVeriOpe.MaxLength = 10;
            this.txtVeriOpe.Name = "txtVeriOpe";
            this.txtVeriOpe.Size = new System.Drawing.Size(125, 20);
            this.txtVeriOpe.TabIndex = 15;
            this.txtVeriOpe.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.txtVeriOpe.Enter += new System.EventHandler(this.txt_Enter);
            this.txtVeriOpe.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblstatus
            // 
            this.lblstatus.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblstatus.Location = new System.Drawing.Point(297, 86);
            this.lblstatus.Name = "lblstatus";
            this.lblstatus.Size = new System.Drawing.Size(98, 20);
            this.lblstatus.TabIndex = 100021;
            this.lblstatus.Text = "状態";
            this.lblstatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label19.Location = new System.Drawing.Point(297, 155);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(98, 20);
            this.label19.TabIndex = 100035;
            this.label19.Text = "不渡入力";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTeiseiFlg
            // 
            this.lblTeiseiFlg.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTeiseiFlg.Location = new System.Drawing.Point(24, 155);
            this.lblTeiseiFlg.Name = "lblTeiseiFlg";
            this.lblTeiseiFlg.Size = new System.Drawing.Size(98, 20);
            this.lblTeiseiFlg.TabIndex = 100036;
            this.lblTeiseiFlg.Text = "訂正入力";
            this.lblTeiseiFlg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblImgFLNm
            // 
            this.lblImgFLNm.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblImgFLNm.Location = new System.Drawing.Point(24, 132);
            this.lblImgFLNm.Name = "lblImgFLNm";
            this.lblImgFLNm.Size = new System.Drawing.Size(98, 20);
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
            this.txtImgFLNm.Location = new System.Drawing.Point(128, 132);
            this.txtImgFLNm.MaxLength = 62;
            this.txtImgFLNm.Name = "txtImgFLNm";
            this.txtImgFLNm.Size = new System.Drawing.Size(727, 20);
            this.txtImgFLNm.TabIndex = 16;
            this.txtImgFLNm.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.txtImgFLNm.Enter += new System.EventHandler(this.txt_Enter);
            this.txtImgFLNm.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // rdoImgOpt1
            // 
            this.rdoImgOpt1.AutoSize = true;
            this.rdoImgOpt1.Checked = true;
            this.rdoImgOpt1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rdoImgOpt1.Location = new System.Drawing.Point(3, 2);
            this.rdoImgOpt1.Name = "rdoImgOpt1";
            this.rdoImgOpt1.Size = new System.Drawing.Size(77, 17);
            this.rdoImgOpt1.TabIndex = 17;
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
            this.rdoImgOpt2.Location = new System.Drawing.Point(99, 2);
            this.rdoImgOpt2.Name = "rdoImgOpt2";
            this.rdoImgOpt2.Size = new System.Drawing.Size(77, 17);
            this.rdoImgOpt2.TabIndex = 18;
            this.rdoImgOpt2.Text = "後方一致";
            this.rdoImgOpt2.UseVisualStyleBackColor = true;
            this.rdoImgOpt2.Enter += new System.EventHandler(this.cmb_Enter);
            this.rdoImgOpt2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // rdoImgOpt3
            // 
            this.rdoImgOpt3.AutoSize = true;
            this.rdoImgOpt3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rdoImgOpt3.Location = new System.Drawing.Point(195, 2);
            this.rdoImgOpt3.Name = "rdoImgOpt3";
            this.rdoImgOpt3.Size = new System.Drawing.Size(77, 17);
            this.rdoImgOpt3.TabIndex = 19;
            this.rdoImgOpt3.Text = "完全一致";
            this.rdoImgOpt3.UseVisualStyleBackColor = true;
            this.rdoImgOpt3.Enter += new System.EventHandler(this.cmb_Enter);
            this.rdoImgOpt3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // lblOCNo
            // 
            this.lblOCNo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOCNo.Location = new System.Drawing.Point(297, 42);
            this.lblOCNo.Name = "lblOCNo";
            this.lblOCNo.Size = new System.Drawing.Size(98, 20);
            this.lblOCNo.TabIndex = 100010;
            this.lblOCNo.Text = "持出銀行コード";
            this.lblOCNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ntOCNo
            // 
            this.ntOCNo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntOCNo.KeyControl = true;
            this.ntOCNo.Location = new System.Drawing.Point(401, 42);
            this.ntOCNo.MaxLength = 4;
            this.ntOCNo.Name = "ntOCNo";
            this.ntOCNo.OnEnterSeparatorCut = true;
            this.ntOCNo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.ntOCNo.Size = new System.Drawing.Size(125, 20);
            this.ntOCNo.TabIndex = 2;
            this.ntOCNo.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntOCNo.Enter += new System.EventHandler(this.txt_Enter);
            this.ntOCNo.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblVeriOpe
            // 
            this.lblVeriOpe.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblVeriOpe.Location = new System.Drawing.Point(594, 109);
            this.lblVeriOpe.Name = "lblVeriOpe";
            this.lblVeriOpe.Size = new System.Drawing.Size(98, 20);
            this.lblVeriOpe.TabIndex = 100012;
            this.lblVeriOpe.Text = "ﾍﾞﾘﾌｧｲｵﾍﾟID";
            this.lblVeriOpe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(594, 155);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 20);
            this.label10.TabIndex = 100036;
            this.label10.Text = "訂正結果";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label15.Location = new System.Drawing.Point(15, 207);
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
            this.cmbStatus.Location = new System.Drawing.Point(401, 86);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(139, 21);
            this.cmbStatus.TabIndex = 12;
            this.cmbStatus.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbStatus.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // cmbTeiseiFlg
            // 
            this.cmbTeiseiFlg.BackColor = System.Drawing.SystemColors.Window;
            this.cmbTeiseiFlg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTeiseiFlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeiseiFlg.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbTeiseiFlg.FormattingEnabled = true;
            this.cmbTeiseiFlg.Location = new System.Drawing.Point(128, 155);
            this.cmbTeiseiFlg.Name = "cmbTeiseiFlg";
            this.cmbTeiseiFlg.Size = new System.Drawing.Size(161, 21);
            this.cmbTeiseiFlg.TabIndex = 21;
            this.cmbTeiseiFlg.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbTeiseiFlg.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbTeiseiFlg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // cmbFuwatariFlg
            // 
            this.cmbFuwatariFlg.BackColor = System.Drawing.SystemColors.Window;
            this.cmbFuwatariFlg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFuwatariFlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFuwatariFlg.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbFuwatariFlg.FormattingEnabled = true;
            this.cmbFuwatariFlg.Location = new System.Drawing.Point(401, 155);
            this.cmbFuwatariFlg.Name = "cmbFuwatariFlg";
            this.cmbFuwatariFlg.Size = new System.Drawing.Size(125, 21);
            this.cmbFuwatariFlg.TabIndex = 22;
            this.cmbFuwatariFlg.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbFuwatariFlg.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            this.cmbFuwatariFlg.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbFuwatariFlg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // cmbGMASts
            // 
            this.cmbGMASts.BackColor = System.Drawing.SystemColors.Window;
            this.cmbGMASts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGMASts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGMASts.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbGMASts.FormattingEnabled = true;
            this.cmbGMASts.Location = new System.Drawing.Point(697, 155);
            this.cmbGMASts.Name = "cmbGMASts";
            this.cmbGMASts.Size = new System.Drawing.Size(125, 21);
            this.cmbGMASts.TabIndex = 23;
            this.cmbGMASts.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbGMASts.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbGMASts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // pnlImgOpt
            // 
            this.pnlImgOpt.Controls.Add(this.rdoImgOpt1);
            this.pnlImgOpt.Controls.Add(this.rdoImgOpt2);
            this.pnlImgOpt.Controls.Add(this.rdoImgOpt3);
            this.pnlImgOpt.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pnlImgOpt.Location = new System.Drawing.Point(880, 132);
            this.pnlImgOpt.Name = "pnlImgOpt";
            this.pnlImgOpt.Size = new System.Drawing.Size(286, 20);
            this.pnlImgOpt.TabIndex = 20;
            // 
            // cmbGRASts
            // 
            this.cmbGRASts.BackColor = System.Drawing.SystemColors.Window;
            this.cmbGRASts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGRASts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGRASts.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbGRASts.FormattingEnabled = true;
            this.cmbGRASts.Location = new System.Drawing.Point(128, 179);
            this.cmbGRASts.Name = "cmbGRASts";
            this.cmbGRASts.Size = new System.Drawing.Size(125, 21);
            this.cmbGRASts.TabIndex = 24;
            this.cmbGRASts.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbGRASts.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbGRASts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(24, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 20);
            this.label3.TabIndex = 100039;
            this.label3.Text = "不渡返還結果";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ntBillFrom
            // 
            this.ntBillFrom.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntBillFrom.KeyControl = true;
            this.ntBillFrom.Location = new System.Drawing.Point(128, 64);
            this.ntBillFrom.MaxLength = 3;
            this.ntBillFrom.Name = "ntBillFrom";
            this.ntBillFrom.OnEnterSeparatorCut = true;
            this.ntBillFrom.Size = new System.Drawing.Size(50, 20);
            this.ntBillFrom.TabIndex = 6;
            this.ntBillFrom.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntBillFrom.Enter += new System.EventHandler(this.txt_Enter);
            this.ntBillFrom.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblBill
            // 
            this.lblBill.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBill.Location = new System.Drawing.Point(24, 64);
            this.lblBill.Name = "lblBill";
            this.lblBill.Size = new System.Drawing.Size(98, 20);
            this.lblBill.TabIndex = 100041;
            this.lblBill.Text = "証券種類";
            this.lblBill.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ntSyuri
            // 
            this.ntSyuri.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntSyuri.KeyControl = true;
            this.ntSyuri.Location = new System.Drawing.Point(401, 64);
            this.ntSyuri.MaxLength = 3;
            this.ntSyuri.Name = "ntSyuri";
            this.ntSyuri.OnEnterSeparatorCut = true;
            this.ntSyuri.Size = new System.Drawing.Size(125, 20);
            this.ntSyuri.TabIndex = 8;
            this.ntSyuri.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntSyuri.Enter += new System.EventHandler(this.txt_Enter);
            this.ntSyuri.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblSyuri
            // 
            this.lblSyuri.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSyuri.Location = new System.Drawing.Point(297, 64);
            this.lblSyuri.Name = "lblSyuri";
            this.lblSyuri.Size = new System.Drawing.Size(98, 20);
            this.lblSyuri.TabIndex = 100043;
            this.lblSyuri.Text = "手形種類";
            this.lblSyuri.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ntAccount
            // 
            this.ntAccount.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntAccount.KeyControl = true;
            this.ntAccount.Location = new System.Drawing.Point(950, 64);
            this.ntAccount.MaxLength = 10;
            this.ntAccount.Name = "ntAccount";
            this.ntAccount.OnEnterSeparatorCut = true;
            this.ntAccount.Size = new System.Drawing.Size(125, 20);
            this.ntAccount.TabIndex = 10;
            this.ntAccount.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntAccount.Enter += new System.EventHandler(this.txt_Enter);
            this.ntAccount.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblAccount
            // 
            this.lblAccount.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblAccount.Location = new System.Drawing.Point(847, 64);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(98, 20);
            this.lblAccount.TabIndex = 100047;
            this.lblAccount.Text = "口座番号";
            this.lblAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ntBrNo
            // 
            this.ntBrNo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntBrNo.KeyControl = true;
            this.ntBrNo.Location = new System.Drawing.Point(697, 64);
            this.ntBrNo.MaxLength = 4;
            this.ntBrNo.Name = "ntBrNo";
            this.ntBrNo.OnEnterSeparatorCut = true;
            this.ntBrNo.Size = new System.Drawing.Size(125, 20);
            this.ntBrNo.TabIndex = 9;
            this.ntBrNo.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntBrNo.Enter += new System.EventHandler(this.txt_Enter);
            this.ntBrNo.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblBrNo
            // 
            this.lblBrNo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBrNo.Location = new System.Drawing.Point(594, 64);
            this.lblBrNo.Name = "lblBrNo";
            this.lblBrNo.Size = new System.Drawing.Size(98, 20);
            this.lblBrNo.TabIndex = 100045;
            this.lblBrNo.Text = "支店番号";
            this.lblBrNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ntTegata
            // 
            this.ntTegata.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntTegata.KeyControl = true;
            this.ntTegata.Location = new System.Drawing.Point(128, 86);
            this.ntTegata.MaxLength = 10;
            this.ntTegata.Name = "ntTegata";
            this.ntTegata.OnEnterSeparatorCut = true;
            this.ntTegata.Size = new System.Drawing.Size(125, 20);
            this.ntTegata.TabIndex = 11;
            this.ntTegata.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntTegata.Enter += new System.EventHandler(this.txt_Enter);
            this.ntTegata.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblTegata
            // 
            this.lblTegata.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTegata.Location = new System.Drawing.Point(24, 86);
            this.lblTegata.Name = "lblTegata";
            this.lblTegata.Size = new System.Drawing.Size(98, 20);
            this.lblTegata.TabIndex = 100049;
            this.lblTegata.Text = "手形番号";
            this.lblTegata.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEntOpe
            // 
            this.txtEntOpe.EntryMode = CommonClass.ENTRYMODE.IMEOFF_KANA;
            this.txtEntOpe.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtEntOpe.KanaLock = false;
            this.txtEntOpe.KeyControl = true;
            this.txtEntOpe.Location = new System.Drawing.Point(401, 109);
            this.txtEntOpe.MaxLength = 10;
            this.txtEntOpe.Name = "txtEntOpe";
            this.txtEntOpe.Size = new System.Drawing.Size(125, 20);
            this.txtEntOpe.TabIndex = 14;
            this.txtEntOpe.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.txtEntOpe.Enter += new System.EventHandler(this.txt_Enter);
            this.txtEntOpe.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblEntOpe
            // 
            this.lblEntOpe.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblEntOpe.Location = new System.Drawing.Point(297, 109);
            this.lblEntOpe.Name = "lblEntOpe";
            this.lblEntOpe.Size = new System.Drawing.Size(98, 20);
            this.lblEntOpe.TabIndex = 100051;
            this.lblEntOpe.Text = "ｴﾝﾄﾘｰｵﾍﾟID";
            this.lblEntOpe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSearchSortMode
            // 
            this.lblSearchSortMode.BackColor = System.Drawing.Color.Linen;
            this.lblSearchSortMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchSortMode.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSearchSortMode.Location = new System.Drawing.Point(7, 1);
            this.lblSearchSortMode.Name = "lblSearchSortMode";
            this.lblSearchSortMode.Size = new System.Drawing.Size(329, 25);
            this.lblSearchSortMode.TabIndex = 100052;
            this.lblSearchSortMode.Text = "検索モード";
            this.lblSearchSortMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDelete
            // 
            this.cmbDelete.BackColor = System.Drawing.SystemColors.Window;
            this.cmbDelete.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDelete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDelete.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbDelete.FormattingEnabled = true;
            this.cmbDelete.Location = new System.Drawing.Point(128, 109);
            this.cmbDelete.Name = "cmbDelete";
            this.cmbDelete.Size = new System.Drawing.Size(125, 21);
            this.cmbDelete.TabIndex = 13;
            this.cmbDelete.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbDelete.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label23.Location = new System.Drawing.Point(24, 109);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(98, 20);
            this.label23.TabIndex = 100054;
            this.label23.Text = "削除データ";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbPayKbn
            // 
            this.cmbPayKbn.BackColor = System.Drawing.SystemColors.Window;
            this.cmbPayKbn.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPayKbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayKbn.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbPayKbn.FormattingEnabled = true;
            this.cmbPayKbn.Location = new System.Drawing.Point(401, 179);
            this.cmbPayKbn.Name = "cmbPayKbn";
            this.cmbPayKbn.Size = new System.Drawing.Size(183, 21);
            this.cmbPayKbn.TabIndex = 25;
            this.cmbPayKbn.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbPayKbn.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbPayKbn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(297, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 20);
            this.label5.TabIndex = 100056;
            this.label5.Text = "決済対象区分";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ntBillTo
            // 
            this.ntBillTo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntBillTo.KeyControl = true;
            this.ntBillTo.Location = new System.Drawing.Point(203, 64);
            this.ntBillTo.MaxLength = 3;
            this.ntBillTo.Name = "ntBillTo";
            this.ntBillTo.OnEnterSeparatorCut = true;
            this.ntBillTo.Size = new System.Drawing.Size(50, 20);
            this.ntBillTo.TabIndex = 7;
            this.ntBillTo.I_Validating += new System.ComponentModel.CancelEventHandler(this.txtBox_IValidating);
            this.ntBillTo.Enter += new System.EventHandler(this.txt_Enter);
            this.ntBillTo.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(182, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 20);
            this.label6.TabIndex = 100058;
            this.label6.Text = "～";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbBUB
            // 
            this.cmbBUB.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBUB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBUB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBUB.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbBUB.FormattingEnabled = true;
            this.cmbBUB.Location = new System.Drawing.Point(697, 179);
            this.cmbBUB.Name = "cmbBUB";
            this.cmbBUB.Size = new System.Drawing.Size(125, 21);
            this.cmbBUB.TabIndex = 26;
            this.cmbBUB.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbBUB.Enter += new System.EventHandler(this.cmb_Enter);
            this.cmbBUB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_KeyDown);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(594, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 20);
            this.label7.TabIndex = 100060;
            this.label7.Text = "二重持出";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchIcMeiList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "SearchIcMeiList";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.root_KeyUp);
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            this.pnlImgOpt.ResumeLayout(false);
            this.pnlImgOpt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvResultList;
        private System.Windows.Forms.ColumnHeader clCLEARING_DATE;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblTeiseiFlg;
        private CommonClass.KanaTextBox txtVeriOpe;
        private System.Windows.Forms.Label lblstatus;
        private CommonClass.NumTextBox2 ntAmountTo;
        private CommonClass.NumTextBox2 ntAmountFrom;
        private CommonClass.DTextBox2 dtClearingDate;
        private CommonClass.DTextBox2 dtRdate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblClearingDate;
        private System.Windows.Forms.Label lblRdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoImgOpt3;
        private System.Windows.Forms.RadioButton rdoImgOpt2;
        private System.Windows.Forms.RadioButton rdoImgOpt1;
        private CommonClass.KanaTextBox txtImgFLNm;
        private System.Windows.Forms.Label lblImgFLNm;
        private CommonClass.NumTextBox2 ntOCNo;
        private System.Windows.Forms.Label lblOCNo;
        private System.Windows.Forms.Label lblVeriOpe;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ColumnHeader clDETAILS_NO;
        private System.Windows.Forms.ColumnHeader clBillCD;
        private System.Windows.Forms.ColumnHeader clAMOUNT;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbTeiseiFlg;
        private System.Windows.Forms.ComboBox cmbFuwatariFlg;
        private System.Windows.Forms.ComboBox cmbGMASts;
        private System.Windows.Forms.Panel pnlImgOpt;
        private System.Windows.Forms.ColumnHeader clSyuruiCD;
        private System.Windows.Forms.ColumnHeader clICBrCD;
        private System.Windows.Forms.ColumnHeader clICBrNm;
        private System.Windows.Forms.ColumnHeader clAccount;
        private System.Windows.Forms.ColumnHeader clTegata;
        private System.Windows.Forms.ColumnHeader clTeisei;
        private System.Windows.Forms.ColumnHeader clFuwatari;
        private System.Windows.Forms.ColumnHeader clBMASts;
        private System.Windows.Forms.ColumnHeader clBRASts;
        private System.Windows.Forms.ColumnHeader clICBKInptSts;
        private System.Windows.Forms.ComboBox cmbGRASts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBill;
        private CommonClass.NumTextBox2 ntBillFrom;
        private CommonClass.NumTextBox2 ntSyuri;
        private System.Windows.Forms.Label lblSyuri;
        private System.Windows.Forms.Label lblTegata;
        private CommonClass.NumTextBox2 ntAccount;
        private System.Windows.Forms.Label lblAccount;
        private CommonClass.NumTextBox2 ntBrNo;
        private System.Windows.Forms.Label lblBrNo;
        private CommonClass.NumTextBox2 ntTegata;
        private CommonClass.KanaTextBox txtEntOpe;
        private System.Windows.Forms.Label lblEntOpe;
        private System.Windows.Forms.ColumnHeader clOCBKNo;
        private System.Windows.Forms.ColumnHeader clOCBKNM;
        private System.Windows.Forms.ColumnHeader clCDATEInptSts;
        private System.Windows.Forms.ColumnHeader clAMTInptSts;
        private System.Windows.Forms.ColumnHeader clKoukanjiriInptSts;
        private System.Windows.Forms.ColumnHeader clKoukanjiriTMNO;
        private System.Windows.Forms.ColumnHeader clJikouInptSts;
        private System.Windows.Forms.ColumnHeader clJikouTMNO;
        private System.Windows.Forms.ColumnHeader clICBKEOpe;
        private System.Windows.Forms.ColumnHeader clICBKVOpe;
        private System.Windows.Forms.ColumnHeader clCDateEOpe;
        private System.Windows.Forms.ColumnHeader clCDateVOpe;
        private System.Windows.Forms.ColumnHeader clAmountEOpe;
        private System.Windows.Forms.ColumnHeader clAmountVOpe;
        private System.Windows.Forms.ColumnHeader clJikouEOpe;
        private System.Windows.Forms.ColumnHeader clJikouVOpe;
        private System.Windows.Forms.Label lblSearchSortMode;
        private System.Windows.Forms.ComboBox cmbDelete;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ColumnHeader clDelete;
        private System.Windows.Forms.ColumnHeader clOPE_DATE;
        private System.Windows.Forms.ColumnHeader clPayKbn;
        private System.Windows.Forms.ComboBox cmbPayKbn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private CommonClass.NumTextBox2 ntBillTo;
        private System.Windows.Forms.ColumnHeader clBUB;
        private System.Windows.Forms.ComboBox cmbBUB;
        private System.Windows.Forms.Label label7;
    }
}
