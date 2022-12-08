namespace CommonClass
{
    partial class FormBase
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
            this.headerPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblGamen = new System.Windows.Forms.Label();
            this.lblOperator = new System.Windows.Forms.Label();
            this.lblTanmatu = new System.Windows.Forms.Label();
            this.headerInPanel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblKankyo = new System.Windows.Forms.Label();
            this.lblBankName = new System.Windows.Forms.Label();
            this.headerInPanel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.contentsPanel = new System.Windows.Forms.Panel();
            this.btnFocusDummey = new System.Windows.Forms.Button();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.btnFunc12 = new System.Windows.Forms.Button();
            this.btnFunc11 = new System.Windows.Forms.Button();
            this.btnFunc10 = new System.Windows.Forms.Button();
            this.btnFunc09 = new System.Windows.Forms.Button();
            this.btnFunc08 = new System.Windows.Forms.Button();
            this.btnFunc07 = new System.Windows.Forms.Button();
            this.btnFunc06 = new System.Windows.Forms.Button();
            this.btnFunc05 = new System.Windows.Forms.Button();
            this.btnFunc04 = new System.Windows.Forms.Button();
            this.btnFunc03 = new System.Windows.Forms.Button();
            this.btnFunc02 = new System.Windows.Forms.Button();
            this.btnFunc01 = new System.Windows.Forms.Button();
            this.statusBarPanel = new System.Windows.Forms.Panel();
            this.statusLabel4 = new System.Windows.Forms.Label();
            this.statusLabel3 = new System.Windows.Forms.Label();
            this.statusLabel2 = new System.Windows.Forms.Label();
            this.statusLabel1 = new System.Windows.Forms.Label();
            this.statusLabel0 = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contentsPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.statusBarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.headerPanel.Controls.Add(this.panel2);
            this.headerPanel.Controls.Add(this.panel1);
            this.headerPanel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1270, 60);
            this.headerPanel.TabIndex = 0;
            this.headerPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.headerPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.headerPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblGamen);
            this.panel2.Controls.Add(this.lblOperator);
            this.panel2.Controls.Add(this.lblTanmatu);
            this.panel2.Controls.Add(this.headerInPanel2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel2.Location = new System.Drawing.Point(0, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1266, 30);
            this.panel2.TabIndex = 5;
            // 
            // lblGamen
            // 
            this.lblGamen.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblGamen.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblGamen.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblGamen.Location = new System.Drawing.Point(237, 0);
            this.lblGamen.Name = "lblGamen";
            this.lblGamen.Size = new System.Drawing.Size(123, 30);
            this.lblGamen.TabIndex = 0;
            this.lblGamen.Text = "画面：xxxxxxxxx";
            this.lblGamen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOperator
            // 
            this.lblOperator.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblOperator.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOperator.Location = new System.Drawing.Point(1014, 0);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(252, 30);
            this.lblOperator.TabIndex = 0;
            this.lblOperator.Text = "オペレータ：9999999(ＸＸＸＸＸＸＸＸ)";
            this.lblOperator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblOperator.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.lblOperator.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.lblOperator.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // lblTanmatu
            // 
            this.lblTanmatu.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTanmatu.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTanmatu.Location = new System.Drawing.Point(0, 0);
            this.lblTanmatu.Name = "lblTanmatu";
            this.lblTanmatu.Size = new System.Drawing.Size(237, 30);
            this.lblTanmatu.TabIndex = 0;
            this.lblTanmatu.Text = "端末：xxx";
            this.lblTanmatu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // headerInPanel2
            // 
            this.headerInPanel2.Location = new System.Drawing.Point(356, 0);
            this.headerInPanel2.Name = "headerInPanel2";
            this.headerInPanel2.Size = new System.Drawing.Size(557, 30);
            this.headerInPanel2.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(1037, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(232, 37);
            this.label4.TabIndex = 2;
            this.label4.Text = "オペレータ：9999999(ＸＸＸＸＸＸＸＸ)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblKankyo);
            this.panel1.Controls.Add(this.lblBankName);
            this.panel1.Controls.Add(this.headerInPanel1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1266, 30);
            this.panel1.TabIndex = 4;
            // 
            // lblKankyo
            // 
            this.lblKankyo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblKankyo.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKankyo.Location = new System.Drawing.Point(237, 0);
            this.lblKankyo.Name = "lblKankyo";
            this.lblKankyo.Size = new System.Drawing.Size(123, 30);
            this.lblKankyo.TabIndex = 0;
            this.lblKankyo.Text = "環境：xxxxxxxxx";
            this.lblKankyo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBankName
            // 
            this.lblBankName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBankName.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBankName.Location = new System.Drawing.Point(0, 0);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(237, 30);
            this.lblBankName.TabIndex = 0;
            this.lblBankName.Text = "XXXX銀行";
            this.lblBankName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBankName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.lblBankName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.lblBankName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // headerInPanel1
            // 
            this.headerInPanel1.Location = new System.Drawing.Point(356, 0);
            this.headerInPanel1.Name = "headerInPanel1";
            this.headerInPanel1.Size = new System.Drawing.Size(557, 30);
            this.headerInPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(1037, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 37);
            this.label2.TabIndex = 2;
            this.label2.Text = "オペレータ：9999999(ＸＸＸＸＸＸＸＸ)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDate
            // 
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDate.Location = new System.Drawing.Point(1018, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(248, 30);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "処理日：9999/99/99";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.lblDate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.lblDate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // contentsPanel
            // 
            this.contentsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.contentsPanel.Controls.Add(this.btnFocusDummey);
            this.contentsPanel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.contentsPanel.Location = new System.Drawing.Point(0, 58);
            this.contentsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.contentsPanel.Name = "contentsPanel";
            this.contentsPanel.Size = new System.Drawing.Size(1270, 841);
            this.contentsPanel.TabIndex = 1;
            this.contentsPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.contentsPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.contentsPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // btnFocusDummey
            // 
            this.btnFocusDummey.Location = new System.Drawing.Point(0, 0);
            this.btnFocusDummey.Name = "btnFocusDummey";
            this.btnFocusDummey.Size = new System.Drawing.Size(0, 0);
            this.btnFocusDummey.TabIndex = 99999;
            this.btnFocusDummey.TabStop = false;
            this.btnFocusDummey.UseVisualStyleBackColor = true;
            // 
            // footerPanel
            // 
            this.footerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.footerPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.footerPanel.Controls.Add(this.btnFunc12);
            this.footerPanel.Controls.Add(this.btnFunc11);
            this.footerPanel.Controls.Add(this.btnFunc10);
            this.footerPanel.Controls.Add(this.btnFunc09);
            this.footerPanel.Controls.Add(this.btnFunc08);
            this.footerPanel.Controls.Add(this.btnFunc07);
            this.footerPanel.Controls.Add(this.btnFunc06);
            this.footerPanel.Controls.Add(this.btnFunc05);
            this.footerPanel.Controls.Add(this.btnFunc04);
            this.footerPanel.Controls.Add(this.btnFunc03);
            this.footerPanel.Controls.Add(this.btnFunc02);
            this.footerPanel.Controls.Add(this.btnFunc01);
            this.footerPanel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.footerPanel.Location = new System.Drawing.Point(0, 938);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(1270, 50);
            this.footerPanel.TabIndex = 1;
            this.footerPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.footerPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.footerPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // btnFunc12
            // 
            this.btnFunc12.Enabled = false;
            this.btnFunc12.Location = new System.Drawing.Point(1166, 0);
            this.btnFunc12.Name = "btnFunc12";
            this.btnFunc12.Size = new System.Drawing.Size(100, 45);
            this.btnFunc12.TabIndex = 11;
            this.btnFunc12.TabStop = false;
            this.btnFunc12.UseVisualStyleBackColor = true;
            this.btnFunc12.Click += new System.EventHandler(this.btnFunc12_Click);
            this.btnFunc12.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.btnFunc12.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.btnFunc12.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // btnFunc11
            // 
            this.btnFunc11.Enabled = false;
            this.btnFunc11.Location = new System.Drawing.Point(1066, 0);
            this.btnFunc11.Name = "btnFunc11";
            this.btnFunc11.Size = new System.Drawing.Size(100, 45);
            this.btnFunc11.TabIndex = 10;
            this.btnFunc11.TabStop = false;
            this.btnFunc11.UseVisualStyleBackColor = true;
            this.btnFunc11.Click += new System.EventHandler(this.btnFunc11_Click);
            this.btnFunc11.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.btnFunc11.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.btnFunc11.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // btnFunc10
            // 
            this.btnFunc10.Enabled = false;
            this.btnFunc10.Location = new System.Drawing.Point(966, 0);
            this.btnFunc10.Name = "btnFunc10";
            this.btnFunc10.Size = new System.Drawing.Size(100, 45);
            this.btnFunc10.TabIndex = 9;
            this.btnFunc10.TabStop = false;
            this.btnFunc10.UseVisualStyleBackColor = true;
            this.btnFunc10.Click += new System.EventHandler(this.btnFunc10_Click);
            this.btnFunc10.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.btnFunc10.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.btnFunc10.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // btnFunc09
            // 
            this.btnFunc09.Enabled = false;
            this.btnFunc09.Location = new System.Drawing.Point(866, 0);
            this.btnFunc09.Name = "btnFunc09";
            this.btnFunc09.Size = new System.Drawing.Size(100, 45);
            this.btnFunc09.TabIndex = 8;
            this.btnFunc09.TabStop = false;
            this.btnFunc09.UseVisualStyleBackColor = true;
            this.btnFunc09.Click += new System.EventHandler(this.btnFunc09_Click);
            this.btnFunc09.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.btnFunc09.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.btnFunc09.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // btnFunc08
            // 
            this.btnFunc08.Enabled = false;
            this.btnFunc08.Location = new System.Drawing.Point(733, 0);
            this.btnFunc08.Name = "btnFunc08";
            this.btnFunc08.Size = new System.Drawing.Size(100, 45);
            this.btnFunc08.TabIndex = 7;
            this.btnFunc08.TabStop = false;
            this.btnFunc08.UseVisualStyleBackColor = true;
            this.btnFunc08.Click += new System.EventHandler(this.btnFunc08_Click);
            this.btnFunc08.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.btnFunc08.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.btnFunc08.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // btnFunc07
            // 
            this.btnFunc07.Enabled = false;
            this.btnFunc07.Location = new System.Drawing.Point(633, 0);
            this.btnFunc07.Name = "btnFunc07";
            this.btnFunc07.Size = new System.Drawing.Size(100, 45);
            this.btnFunc07.TabIndex = 6;
            this.btnFunc07.TabStop = false;
            this.btnFunc07.UseVisualStyleBackColor = true;
            this.btnFunc07.Click += new System.EventHandler(this.btnFunc07_Click);
            this.btnFunc07.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.btnFunc07.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.btnFunc07.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // btnFunc06
            // 
            this.btnFunc06.Enabled = false;
            this.btnFunc06.Location = new System.Drawing.Point(533, 0);
            this.btnFunc06.Name = "btnFunc06";
            this.btnFunc06.Size = new System.Drawing.Size(100, 45);
            this.btnFunc06.TabIndex = 5;
            this.btnFunc06.TabStop = false;
            this.btnFunc06.UseVisualStyleBackColor = true;
            this.btnFunc06.Click += new System.EventHandler(this.btnFunc06_Click);
            this.btnFunc06.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.btnFunc06.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.btnFunc06.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // btnFunc05
            // 
            this.btnFunc05.Enabled = false;
            this.btnFunc05.Location = new System.Drawing.Point(433, 0);
            this.btnFunc05.Name = "btnFunc05";
            this.btnFunc05.Size = new System.Drawing.Size(100, 45);
            this.btnFunc05.TabIndex = 4;
            this.btnFunc05.TabStop = false;
            this.btnFunc05.UseVisualStyleBackColor = true;
            this.btnFunc05.Click += new System.EventHandler(this.btnFunc05_Click);
            this.btnFunc05.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.btnFunc05.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.btnFunc05.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // btnFunc04
            // 
            this.btnFunc04.Enabled = false;
            this.btnFunc04.Location = new System.Drawing.Point(300, 0);
            this.btnFunc04.Name = "btnFunc04";
            this.btnFunc04.Size = new System.Drawing.Size(100, 45);
            this.btnFunc04.TabIndex = 3;
            this.btnFunc04.TabStop = false;
            this.btnFunc04.UseVisualStyleBackColor = true;
            this.btnFunc04.Click += new System.EventHandler(this.btnFunc04_Click);
            this.btnFunc04.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.btnFunc04.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.btnFunc04.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // btnFunc03
            // 
            this.btnFunc03.Enabled = false;
            this.btnFunc03.Location = new System.Drawing.Point(200, 0);
            this.btnFunc03.Name = "btnFunc03";
            this.btnFunc03.Size = new System.Drawing.Size(100, 45);
            this.btnFunc03.TabIndex = 2;
            this.btnFunc03.TabStop = false;
            this.btnFunc03.UseVisualStyleBackColor = true;
            this.btnFunc03.Click += new System.EventHandler(this.btnFunc03_Click);
            this.btnFunc03.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.btnFunc03.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.btnFunc03.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // btnFunc02
            // 
            this.btnFunc02.Enabled = false;
            this.btnFunc02.Location = new System.Drawing.Point(100, 0);
            this.btnFunc02.Name = "btnFunc02";
            this.btnFunc02.Size = new System.Drawing.Size(100, 45);
            this.btnFunc02.TabIndex = 1;
            this.btnFunc02.TabStop = false;
            this.btnFunc02.UseVisualStyleBackColor = true;
            this.btnFunc02.Click += new System.EventHandler(this.btnFunc02_Click);
            this.btnFunc02.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.btnFunc02.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.btnFunc02.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // btnFunc01
            // 
            this.btnFunc01.Enabled = false;
            this.btnFunc01.Location = new System.Drawing.Point(0, 0);
            this.btnFunc01.Name = "btnFunc01";
            this.btnFunc01.Size = new System.Drawing.Size(100, 45);
            this.btnFunc01.TabIndex = 0;
            this.btnFunc01.TabStop = false;
            this.btnFunc01.UseVisualStyleBackColor = true;
            this.btnFunc01.Click += new System.EventHandler(this.btnFunc01_Click);
            this.btnFunc01.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.btnFunc01.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.btnFunc01.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // statusBarPanel
            // 
            this.statusBarPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusBarPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.statusBarPanel.Controls.Add(this.statusLabel4);
            this.statusBarPanel.Controls.Add(this.statusLabel3);
            this.statusBarPanel.Controls.Add(this.statusLabel2);
            this.statusBarPanel.Controls.Add(this.statusLabel1);
            this.statusBarPanel.Controls.Add(this.statusLabel0);
            this.statusBarPanel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.statusBarPanel.Location = new System.Drawing.Point(0, 900);
            this.statusBarPanel.Name = "statusBarPanel";
            this.statusBarPanel.Size = new System.Drawing.Size(1268, 35);
            this.statusBarPanel.TabIndex = 0;
            this.statusBarPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.statusBarPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.statusBarPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // statusLabel4
            // 
            this.statusLabel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.statusLabel4.Location = new System.Drawing.Point(1066, 0);
            this.statusLabel4.Name = "statusLabel4";
            this.statusLabel4.Size = new System.Drawing.Size(200, 33);
            this.statusLabel4.TabIndex = 3;
            this.statusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.statusLabel4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.statusLabel4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // statusLabel3
            // 
            this.statusLabel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.statusLabel3.Location = new System.Drawing.Point(866, 0);
            this.statusLabel3.Name = "statusLabel3";
            this.statusLabel3.Size = new System.Drawing.Size(200, 33);
            this.statusLabel3.TabIndex = 2;
            this.statusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.statusLabel3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.statusLabel3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // statusLabel2
            // 
            this.statusLabel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.statusLabel2.Location = new System.Drawing.Point(433, 0);
            this.statusLabel2.Name = "statusLabel2";
            this.statusLabel2.Size = new System.Drawing.Size(427, 33);
            this.statusLabel2.TabIndex = 1;
            this.statusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.statusLabel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.statusLabel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // statusLabel1
            // 
            this.statusLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.statusLabel1.Location = new System.Drawing.Point(0, 0);
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Size = new System.Drawing.Size(427, 33);
            this.statusLabel1.TabIndex = 0;
            this.statusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.statusLabel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.statusLabel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // statusLabel0
            // 
            this.statusLabel0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.statusLabel0.Location = new System.Drawing.Point(0, 0);
            this.statusLabel0.Name = "statusLabel0";
            this.statusLabel0.Size = new System.Drawing.Size(1268, 33);
            this.statusLabel0.TabIndex = 4;
            this.statusLabel0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel0.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.statusLabel0.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.statusLabel0.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            // 
            // FormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.contentsPanel);
            this.Controls.Add(this.statusBarPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBase";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormBase_FormClosed);
            this.Load += new System.EventHandler(this.FormBase_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormBase_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormBase_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panels_MouseUp);
            this.headerPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.contentsPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.statusBarPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        protected System.Windows.Forms.Panel contentsPanel;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.Button btnFunc12;
        private System.Windows.Forms.Button btnFunc11;
        private System.Windows.Forms.Button btnFunc10;
        private System.Windows.Forms.Button btnFunc09;
        private System.Windows.Forms.Button btnFunc08;
        private System.Windows.Forms.Button btnFunc07;
        private System.Windows.Forms.Button btnFunc06;
        private System.Windows.Forms.Button btnFunc05;
        private System.Windows.Forms.Button btnFunc04;
        private System.Windows.Forms.Button btnFunc03;
        private System.Windows.Forms.Button btnFunc02;
        private System.Windows.Forms.Button btnFunc01;
        private System.Windows.Forms.Panel statusBarPanel;
        private System.Windows.Forms.Label statusLabel4;
        private System.Windows.Forms.Label statusLabel3;
        private System.Windows.Forms.Label statusLabel2;
        private System.Windows.Forms.Label statusLabel1;
        private System.Windows.Forms.Label statusLabel0;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTanmatu;
        private System.Windows.Forms.Label lblGamen;
        protected System.Windows.Forms.Panel headerInPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblKankyo;
        protected System.Windows.Forms.Panel headerInPanel1;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Button btnFocusDummey;
    }
}
