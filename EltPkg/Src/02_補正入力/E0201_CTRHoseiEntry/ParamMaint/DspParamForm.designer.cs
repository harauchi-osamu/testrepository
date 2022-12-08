namespace ParamMaint
{
    partial class DspParamForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblDspId = new System.Windows.Forms.Label();
            this.lblDspIdName = new System.Windows.Forms.Label();
            this.cmbFontSize = new System.Windows.Forms.ComboBox();
            this.tbOCR = new System.Windows.Forms.TextBox();
            this.lblOCR = new System.Windows.Forms.Label();
            this.dgvDspItems = new System.Windows.Forms.DataGridView();
            this.ITEM_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_DISPNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_TYPE = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ITEM_LEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DUP = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AUTO_INPUT = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NAME_POS_TOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME_POS_LEFT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INPUT_POS_TOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INPUT_POS_LEFT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INPUT_WIDTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INPUT_HEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INPUT_SEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_TOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_LEFT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_WIDTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_HEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_SUBRTN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbImgFile = new System.Windows.Forms.TextBox();
            this.lblImgFile = new System.Windows.Forms.Label();
            this.lblFontSize = new System.Windows.Forms.Label();
            this.ktbDspName = new CommonClass.KanaTextBox();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.lblGymName = new System.Windows.Forms.Label();
            this.lblGymNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkAutoEnt = new System.Windows.Forms.CheckBox();
            this.chkAutoVfy = new System.Windows.Forms.CheckBox();
            this.chkVfy = new System.Windows.Forms.CheckBox();
            this.lblHoseiItemMode = new System.Windows.Forms.Label();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.contentsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDspItems)).BeginInit();
            this.SuspendLayout();
            // 
            // contentsPanel
            // 
            this.contentsPanel.Controls.Add(this.btnAdd);
            this.contentsPanel.Controls.Add(this.btnDel);
            this.contentsPanel.Controls.Add(this.chkVfy);
            this.contentsPanel.Controls.Add(this.chkAutoVfy);
            this.contentsPanel.Controls.Add(this.chkAutoEnt);
            this.contentsPanel.Controls.Add(this.lblHoseiItemMode);
            this.contentsPanel.Controls.Add(this.lblDspId);
            this.contentsPanel.Controls.Add(this.lblDspIdName);
            this.contentsPanel.Controls.Add(this.cmbFontSize);
            this.contentsPanel.Controls.Add(this.tbOCR);
            this.contentsPanel.Controls.Add(this.lblOCR);
            this.contentsPanel.Controls.Add(this.dgvDspItems);
            this.contentsPanel.Controls.Add(this.tbImgFile);
            this.contentsPanel.Controls.Add(this.lblImgFile);
            this.contentsPanel.Controls.Add(this.lblFontSize);
            this.contentsPanel.Controls.Add(this.ktbDspName);
            this.contentsPanel.Controls.Add(this.label6);
            this.contentsPanel.Controls.Add(this.label5);
            this.contentsPanel.Controls.Add(this.label3);
            this.contentsPanel.Controls.Add(this.label1);
            this.contentsPanel.Controls.Add(this.lblDisplay);
            this.contentsPanel.Controls.Add(this.lblGymName);
            this.contentsPanel.Controls.Add(this.lblGymNo);
            this.contentsPanel.Controls.SetChildIndex(this.lblGymNo, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblGymName, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblDisplay, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label1, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label3, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label5, 0);
            this.contentsPanel.Controls.SetChildIndex(this.label6, 0);
            this.contentsPanel.Controls.SetChildIndex(this.ktbDspName, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblFontSize, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblImgFile, 0);
            this.contentsPanel.Controls.SetChildIndex(this.tbImgFile, 0);
            this.contentsPanel.Controls.SetChildIndex(this.dgvDspItems, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblOCR, 0);
            this.contentsPanel.Controls.SetChildIndex(this.tbOCR, 0);
            this.contentsPanel.Controls.SetChildIndex(this.cmbFontSize, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblDspIdName, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblDspId, 0);
            this.contentsPanel.Controls.SetChildIndex(this.lblHoseiItemMode, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnFocusDummey, 0);
            this.contentsPanel.Controls.SetChildIndex(this.chkAutoEnt, 0);
            this.contentsPanel.Controls.SetChildIndex(this.chkAutoVfy, 0);
            this.contentsPanel.Controls.SetChildIndex(this.chkVfy, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnDel, 0);
            this.contentsPanel.Controls.SetChildIndex(this.btnAdd, 0);
            // 
            // lblDspId
            // 
            this.lblDspId.Location = new System.Drawing.Point(218, 60);
            this.lblDspId.Name = "lblDspId";
            this.lblDspId.Size = new System.Drawing.Size(100, 19);
            this.lblDspId.TabIndex = 100031;
            this.lblDspId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDspIdName
            // 
            this.lblDspIdName.AutoSize = true;
            this.lblDspIdName.Location = new System.Drawing.Point(36, 60);
            this.lblDspIdName.Name = "lblDspIdName";
            this.lblDspIdName.Size = new System.Drawing.Size(85, 19);
            this.lblDspIdName.TabIndex = 100030;
            this.lblDspIdName.Text = "画面番号";
            // 
            // cmbFontSize
            // 
            this.cmbFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFontSize.Location = new System.Drawing.Point(896, 43);
            this.cmbFontSize.Name = "cmbFontSize";
            this.cmbFontSize.Size = new System.Drawing.Size(65, 27);
            this.cmbFontSize.TabIndex = 5;
            this.cmbFontSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // tbOCR
            // 
            this.tbOCR.Location = new System.Drawing.Point(896, 133);
            this.tbOCR.MaxLength = 98;
            this.tbOCR.Multiline = true;
            this.tbOCR.Name = "tbOCR";
            this.tbOCR.ReadOnly = true;
            this.tbOCR.Size = new System.Drawing.Size(299, 142);
            this.tbOCR.TabIndex = 7;
            this.tbOCR.TabStop = false;
            this.tbOCR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // lblOCR
            // 
            this.lblOCR.AutoSize = true;
            this.lblOCR.Location = new System.Drawing.Point(750, 136);
            this.lblOCR.Name = "lblOCR";
            this.lblOCR.Size = new System.Drawing.Size(104, 19);
            this.lblOCR.TabIndex = 100029;
            this.lblOCR.Text = "OCR帳票名";
            // 
            // dgvDspItems
            // 
            this.dgvDspItems.AllowUserToAddRows = false;
            this.dgvDspItems.AllowUserToDeleteRows = false;
            this.dgvDspItems.AllowUserToResizeColumns = false;
            this.dgvDspItems.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDspItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDspItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDspItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ITEM_ID,
            this.ITEM_DISPNAME,
            this.ITEM_TYPE,
            this.ITEM_LEN,
            this.POS,
            this.DUP,
            this.AUTO_INPUT,
            this.NAME_POS_TOP,
            this.NAME_POS_LEFT,
            this.INPUT_POS_TOP,
            this.INPUT_POS_LEFT,
            this.INPUT_WIDTH,
            this.INPUT_HEIGHT,
            this.INPUT_SEQ,
            this.ITEM_TOP,
            this.ITEM_LEFT,
            this.ITEM_WIDTH,
            this.ITEM_HEIGHT,
            this.ITEM_SUBRTN});
            this.dgvDspItems.Location = new System.Drawing.Point(10, 339);
            this.dgvDspItems.MultiSelect = false;
            this.dgvDspItems.Name = "dgvDspItems";
            this.dgvDspItems.RowHeadersVisible = false;
            this.dgvDspItems.RowTemplate.Height = 21;
            this.dgvDspItems.Size = new System.Drawing.Size(1246, 487);
            this.dgvDspItems.TabIndex = 10;
            // 
            // ITEM_ID
            // 
            this.ITEM_ID.HeaderText = "ID";
            this.ITEM_ID.MaxInputLength = 3;
            this.ITEM_ID.Name = "ITEM_ID";
            this.ITEM_ID.ReadOnly = true;
            this.ITEM_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ITEM_ID.Width = 50;
            // 
            // ITEM_DISPNAME
            // 
            this.ITEM_DISPNAME.HeaderText = "項目表示名称";
            this.ITEM_DISPNAME.MaxInputLength = 40;
            this.ITEM_DISPNAME.Name = "ITEM_DISPNAME";
            this.ITEM_DISPNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ITEM_DISPNAME.Width = 300;
            // 
            // ITEM_TYPE
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ITEM_TYPE.DefaultCellStyle = dataGridViewCellStyle2;
            this.ITEM_TYPE.HeaderText = "タイプ";
            this.ITEM_TYPE.Items.AddRange(new object[] {
            "A",
            "N",
            "R",
            "K",
            "J",
            "C",
            "H",
            "T",
            "S",
            "D",
            "V",
            "W",
            "*"});
            this.ITEM_TYPE.MaxDropDownItems = 7;
            this.ITEM_TYPE.Name = "ITEM_TYPE";
            this.ITEM_TYPE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ITEM_TYPE.Width = 55;
            // 
            // ITEM_LEN
            // 
            this.ITEM_LEN.HeaderText = "桁数";
            this.ITEM_LEN.MaxInputLength = 2;
            this.ITEM_LEN.Name = "ITEM_LEN";
            this.ITEM_LEN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ITEM_LEN.Width = 50;
            // 
            // POS
            // 
            this.POS.HeaderText = "位置";
            this.POS.MaxInputLength = 4;
            this.POS.Name = "POS";
            this.POS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.POS.Width = 50;
            // 
            // DUP
            // 
            this.DUP.HeaderText = "DUP";
            this.DUP.Name = "DUP";
            this.DUP.Width = 45;
            // 
            // AUTO_INPUT
            // 
            this.AUTO_INPUT.HeaderText = "自動確定";
            this.AUTO_INPUT.Name = "AUTO_INPUT";
            this.AUTO_INPUT.Width = 95;
            // 
            // NAME_POS_TOP
            // 
            this.NAME_POS_TOP.HeaderText = "名称位置(TOP)";
            this.NAME_POS_TOP.MaxInputLength = 5;
            this.NAME_POS_TOP.Name = "NAME_POS_TOP";
            this.NAME_POS_TOP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NAME_POS_LEFT
            // 
            this.NAME_POS_LEFT.HeaderText = "名称位置(LEFT)";
            this.NAME_POS_LEFT.MaxInputLength = 5;
            this.NAME_POS_LEFT.Name = "NAME_POS_LEFT";
            this.NAME_POS_LEFT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // INPUT_POS_TOP
            // 
            this.INPUT_POS_TOP.HeaderText = "入力位置(TOP)";
            this.INPUT_POS_TOP.MaxInputLength = 5;
            this.INPUT_POS_TOP.Name = "INPUT_POS_TOP";
            this.INPUT_POS_TOP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // INPUT_POS_LEFT
            // 
            this.INPUT_POS_LEFT.HeaderText = "入力位置(LEFT)";
            this.INPUT_POS_LEFT.MaxInputLength = 5;
            this.INPUT_POS_LEFT.Name = "INPUT_POS_LEFT";
            this.INPUT_POS_LEFT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // INPUT_WIDTH
            // 
            this.INPUT_WIDTH.HeaderText = "入力幅";
            this.INPUT_WIDTH.Name = "INPUT_WIDTH";
            this.INPUT_WIDTH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.INPUT_WIDTH.Width = 75;
            // 
            // INPUT_HEIGHT
            // 
            this.INPUT_HEIGHT.HeaderText = "入力高";
            this.INPUT_HEIGHT.Name = "INPUT_HEIGHT";
            this.INPUT_HEIGHT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.INPUT_HEIGHT.Width = 75;
            // 
            // INPUT_SEQ
            // 
            this.INPUT_SEQ.HeaderText = "入力順";
            this.INPUT_SEQ.Name = "INPUT_SEQ";
            this.INPUT_SEQ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.INPUT_SEQ.Width = 75;
            // 
            // ITEM_TOP
            // 
            this.ITEM_TOP.HeaderText = "カーソル(TOP)";
            this.ITEM_TOP.Name = "ITEM_TOP";
            this.ITEM_TOP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ITEM_TOP.Width = 90;
            // 
            // ITEM_LEFT
            // 
            this.ITEM_LEFT.HeaderText = "カーソル(LEFT)";
            this.ITEM_LEFT.Name = "ITEM_LEFT";
            this.ITEM_LEFT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ITEM_LEFT.Width = 90;
            // 
            // ITEM_WIDTH
            // 
            this.ITEM_WIDTH.HeaderText = "カーソル(WIDTH)";
            this.ITEM_WIDTH.Name = "ITEM_WIDTH";
            this.ITEM_WIDTH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ITEM_WIDTH.Width = 90;
            // 
            // ITEM_HEIGHT
            // 
            this.ITEM_HEIGHT.HeaderText = "カーソル(HEIGHT)";
            this.ITEM_HEIGHT.Name = "ITEM_HEIGHT";
            this.ITEM_HEIGHT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ITEM_HEIGHT.Width = 90;
            // 
            // ITEM_SUBRTN
            // 
            this.ITEM_SUBRTN.HeaderText = "サブルーチン";
            this.ITEM_SUBRTN.MaxInputLength = 128;
            this.ITEM_SUBRTN.Name = "ITEM_SUBRTN";
            this.ITEM_SUBRTN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ITEM_SUBRTN.Width = 200;
            // 
            // tbImgFile
            // 
            this.tbImgFile.Location = new System.Drawing.Point(896, 96);
            this.tbImgFile.MaxLength = 30;
            this.tbImgFile.Name = "tbImgFile";
            this.tbImgFile.Size = new System.Drawing.Size(299, 26);
            this.tbImgFile.TabIndex = 6;
            this.tbImgFile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // lblImgFile
            // 
            this.lblImgFile.AutoSize = true;
            this.lblImgFile.Location = new System.Drawing.Point(750, 99);
            this.lblImgFile.Name = "lblImgFile";
            this.lblImgFile.Size = new System.Drawing.Size(122, 19);
            this.lblImgFile.TabIndex = 100028;
            this.lblImgFile.Text = "イメージファイル";
            // 
            // lblFontSize
            // 
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Location = new System.Drawing.Point(750, 46);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(108, 19);
            this.lblFontSize.TabIndex = 100026;
            this.lblFontSize.Text = "フォントサイズ";
            // 
            // ktbDspName
            // 
            this.ktbDspName.EntryMode = CommonClass.ENTRYMODE.IMEOFF_KANA;
            this.ktbDspName.KanaLock = false;
            this.ktbDspName.Location = new System.Drawing.Point(222, 133);
            this.ktbDspName.MaxLength = 60;
            this.ktbDspName.Name = "ktbDspName";
            this.ktbDspName.Size = new System.Drawing.Size(428, 26);
            this.ktbDspName.TabIndex = 1;
            this.ktbDspName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.Location = new System.Drawing.Point(36, 136);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(66, 19);
            this.lblDisplay.TabIndex = 100021;
            this.lblDisplay.Text = "画面名";
            this.lblDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGymName
            // 
            this.lblGymName.Location = new System.Drawing.Point(218, 24);
            this.lblGymName.Name = "lblGymName";
            this.lblGymName.Size = new System.Drawing.Size(432, 19);
            this.lblGymName.TabIndex = 100020;
            this.lblGymName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGymNo
            // 
            this.lblGymNo.AutoSize = true;
            this.lblGymNo.Location = new System.Drawing.Point(36, 24);
            this.lblGymNo.Name = "lblGymNo";
            this.lblGymNo.Size = new System.Drawing.Size(85, 19);
            this.lblGymNo.TabIndex = 100019;
            this.lblGymNo.Text = "業務番号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 19);
            this.label1.TabIndex = 100021;
            this.label1.Text = "補正項目モード";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 19);
            this.label3.TabIndex = 100021;
            this.label3.Text = "オートスキップエントリ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 19);
            this.label5.TabIndex = 100021;
            this.label5.Text = "オートスキップベリファイ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 19);
            this.label6.TabIndex = 100021;
            this.label6.Text = "ベリファイ有無";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkAutoEnt
            // 
            this.chkAutoEnt.AutoSize = true;
            this.chkAutoEnt.Location = new System.Drawing.Point(222, 173);
            this.chkAutoEnt.Name = "chkAutoEnt";
            this.chkAutoEnt.Size = new System.Drawing.Size(56, 23);
            this.chkAutoEnt.TabIndex = 2;
            this.chkAutoEnt.Text = "あり";
            this.chkAutoEnt.UseVisualStyleBackColor = true;
            this.chkAutoEnt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // chkAutoVfy
            // 
            this.chkAutoVfy.AutoSize = true;
            this.chkAutoVfy.Location = new System.Drawing.Point(222, 214);
            this.chkAutoVfy.Name = "chkAutoVfy";
            this.chkAutoVfy.Size = new System.Drawing.Size(56, 23);
            this.chkAutoVfy.TabIndex = 3;
            this.chkAutoVfy.Text = "あり";
            this.chkAutoVfy.UseVisualStyleBackColor = true;
            this.chkAutoVfy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // chkVfy
            // 
            this.chkVfy.AutoSize = true;
            this.chkVfy.Location = new System.Drawing.Point(222, 255);
            this.chkVfy.Name = "chkVfy";
            this.chkVfy.Size = new System.Drawing.Size(56, 23);
            this.chkVfy.TabIndex = 4;
            this.chkVfy.Text = "あり";
            this.chkVfy.UseVisualStyleBackColor = true;
            this.chkVfy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.root_KeyDown);
            // 
            // lblHoseiItemMode
            // 
            this.lblHoseiItemMode.Location = new System.Drawing.Point(218, 99);
            this.lblHoseiItemMode.Name = "lblHoseiItemMode";
            this.lblHoseiItemMode.Size = new System.Drawing.Size(145, 19);
            this.lblHoseiItemMode.TabIndex = 100031;
            this.lblHoseiItemMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(116, 303);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(100, 30);
            this.btnDel.TabIndex = 9;
            this.btnDel.Text = "項目削除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(10, 303);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "項目追加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // DspParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1270, 995);
            this.Name = "DspParamForm";
            this.contentsPanel.ResumeLayout(false);
            this.contentsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDspItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDspId;
        private System.Windows.Forms.Label lblDspIdName;
        private System.Windows.Forms.ComboBox cmbFontSize;
        private System.Windows.Forms.TextBox tbOCR;
        private System.Windows.Forms.Label lblOCR;
        private System.Windows.Forms.DataGridView dgvDspItems;
        private System.Windows.Forms.TextBox tbImgFile;
        private System.Windows.Forms.Label lblImgFile;
        private System.Windows.Forms.Label lblFontSize;
        private CommonClass.KanaTextBox ktbDspName;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.Label lblGymName;
        private System.Windows.Forms.Label lblGymNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkVfy;
        private System.Windows.Forms.CheckBox chkAutoVfy;
        private System.Windows.Forms.CheckBox chkAutoEnt;
        private System.Windows.Forms.Label lblHoseiItemMode;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_DISPNAME;
        private System.Windows.Forms.DataGridViewComboBoxColumn ITEM_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_LEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn POS;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DUP;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AUTO_INPUT;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME_POS_TOP;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME_POS_LEFT;
        private System.Windows.Forms.DataGridViewTextBoxColumn INPUT_POS_TOP;
        private System.Windows.Forms.DataGridViewTextBoxColumn INPUT_POS_LEFT;
        private System.Windows.Forms.DataGridViewTextBoxColumn INPUT_WIDTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn INPUT_HEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn INPUT_SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_TOP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_LEFT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_WIDTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_HEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_SUBRTN;
    }
}
