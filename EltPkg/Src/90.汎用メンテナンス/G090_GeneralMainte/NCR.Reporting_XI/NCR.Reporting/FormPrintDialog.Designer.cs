namespace NCR.Reporting
{
    partial class FormPrintDialog
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
            this.gbPrinter = new System.Windows.Forms.GroupBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cbPrinterName = new System.Windows.Forms.ComboBox();
            this.lblCommentCaption = new System.Windows.Forms.Label();
            this.lblLocationCaption = new System.Windows.Forms.Label();
            this.lblStatusCaption = new System.Windows.Forms.Label();
            this.lblPrintNameCaption = new System.Windows.Forms.Label();
            this.gbPrintRange = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPageTo = new CommonClass.NumTextBox2();
            this.tbPageFrom = new CommonClass.NumTextBox2();
            this.rbCurrentPage = new System.Windows.Forms.RadioButton();
            this.rbPage = new System.Windows.Forms.RadioButton();
            this.rbALL = new System.Windows.Forms.RadioButton();
            this.gbPrintCopy = new System.Windows.Forms.GroupBox();
            this.nudPrintCopy = new System.Windows.Forms.NumericUpDown();
            this.lblPrintCopy = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbPrinter.SuspendLayout();
            this.gbPrintRange.SuspendLayout();
            this.gbPrintCopy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrintCopy)).BeginInit();
            this.SuspendLayout();
            // 
            // gbPrinter
            // 
            this.gbPrinter.Controls.Add(this.lblComment);
            this.gbPrinter.Controls.Add(this.lblLocation);
            this.gbPrinter.Controls.Add(this.lblStatus);
            this.gbPrinter.Controls.Add(this.cbPrinterName);
            this.gbPrinter.Controls.Add(this.lblCommentCaption);
            this.gbPrinter.Controls.Add(this.lblLocationCaption);
            this.gbPrinter.Controls.Add(this.lblStatusCaption);
            this.gbPrinter.Controls.Add(this.lblPrintNameCaption);
            this.gbPrinter.Location = new System.Drawing.Point(8, 8);
            this.gbPrinter.Name = "gbPrinter";
            this.gbPrinter.Size = new System.Drawing.Size(416, 112);
            this.gbPrinter.TabIndex = 0;
            this.gbPrinter.TabStop = false;
            this.gbPrinter.Text = "プリンター";
            // 
            // lblComment
            // 
            this.lblComment.Location = new System.Drawing.Point(96, 84);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(184, 20);
            this.lblComment.TabIndex = 7;
            this.lblComment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLocation
            // 
            this.lblLocation.Location = new System.Drawing.Point(96, 62);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(184, 20);
            this.lblLocation.TabIndex = 6;
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(96, 40);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(184, 20);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbPrinterName
            // 
            this.cbPrinterName.FormattingEnabled = true;
            this.cbPrinterName.Location = new System.Drawing.Point(104, 16);
            this.cbPrinterName.Name = "cbPrinterName";
            this.cbPrinterName.Size = new System.Drawing.Size(264, 20);
            this.cbPrinterName.TabIndex = 1;
            this.cbPrinterName.SelectionChangeCommitted += new System.EventHandler(this.cbPrinterName_SelectionChangeCommitted);
            // 
            // lblCommentCaption
            // 
            this.lblCommentCaption.Location = new System.Drawing.Point(8, 84);
            this.lblCommentCaption.Name = "lblCommentCaption";
            this.lblCommentCaption.Size = new System.Drawing.Size(86, 20);
            this.lblCommentCaption.TabIndex = 3;
            this.lblCommentCaption.Text = "コメント:";
            this.lblCommentCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLocationCaption
            // 
            this.lblLocationCaption.Location = new System.Drawing.Point(8, 62);
            this.lblLocationCaption.Name = "lblLocationCaption";
            this.lblLocationCaption.Size = new System.Drawing.Size(86, 20);
            this.lblLocationCaption.TabIndex = 2;
            this.lblLocationCaption.Text = "場所:";
            this.lblLocationCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatusCaption
            // 
            this.lblStatusCaption.Location = new System.Drawing.Point(8, 40);
            this.lblStatusCaption.Name = "lblStatusCaption";
            this.lblStatusCaption.Size = new System.Drawing.Size(86, 20);
            this.lblStatusCaption.TabIndex = 1;
            this.lblStatusCaption.Text = "状態:";
            this.lblStatusCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPrintNameCaption
            // 
            this.lblPrintNameCaption.Location = new System.Drawing.Point(8, 16);
            this.lblPrintNameCaption.Name = "lblPrintNameCaption";
            this.lblPrintNameCaption.Size = new System.Drawing.Size(86, 23);
            this.lblPrintNameCaption.TabIndex = 0;
            this.lblPrintNameCaption.Text = "プリンター名:";
            this.lblPrintNameCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbPrintRange
            // 
            this.gbPrintRange.Controls.Add(this.label1);
            this.gbPrintRange.Controls.Add(this.label2);
            this.gbPrintRange.Controls.Add(this.tbPageTo);
            this.gbPrintRange.Controls.Add(this.tbPageFrom);
            this.gbPrintRange.Controls.Add(this.rbCurrentPage);
            this.gbPrintRange.Controls.Add(this.rbPage);
            this.gbPrintRange.Controls.Add(this.rbALL);
            this.gbPrintRange.Location = new System.Drawing.Point(8, 128);
            this.gbPrintRange.Name = "gbPrintRange";
            this.gbPrintRange.Size = new System.Drawing.Size(264, 128);
            this.gbPrintRange.TabIndex = 1;
            this.gbPrintRange.TabStop = false;
            this.gbPrintRange.Text = "印刷範囲";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(168, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "ページから";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(168, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 23);
            this.label2.TabIndex = 10;
            this.label2.Text = "ページまで";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbPageTo
            // 
            this.tbPageTo.AllSelect = true;
            this.tbPageTo.AutoZeroSet = false;
            this.tbPageTo.Comma = false;
            this.tbPageTo.IsMaxInputSelectNext = false;
            this.tbPageTo.LeftZero = false;
            this.tbPageTo.Location = new System.Drawing.Point(120, 72);
            this.tbPageTo.MaxLength = 4;
            this.tbPageTo.Name = "tbPageTo";
            this.tbPageTo.Size = new System.Drawing.Size(40, 19);
            this.tbPageTo.TabIndex = 5;
            this.tbPageTo.TextChanged += new System.EventHandler(this.tbPageTo_TextChanged);
            // 
            // tbPageFrom
            // 
            this.tbPageFrom.AllSelect = true;
            this.tbPageFrom.AutoZeroSet = false;
            this.tbPageFrom.Comma = false;
            this.tbPageFrom.IsMaxInputSelectNext = false;
            this.tbPageFrom.LeftZero = false;
            this.tbPageFrom.Location = new System.Drawing.Point(120, 48);
            this.tbPageFrom.MaxLength = 4;
            this.tbPageFrom.Name = "tbPageFrom";
            this.tbPageFrom.Size = new System.Drawing.Size(40, 19);
            this.tbPageFrom.TabIndex = 4;
            this.tbPageFrom.TextChanged += new System.EventHandler(this.tbPageFrom_TextChanged);
            // 
            // rbCurrentPage
            // 
            this.rbCurrentPage.Location = new System.Drawing.Point(16, 96);
            this.rbCurrentPage.Name = "rbCurrentPage";
            this.rbCurrentPage.Size = new System.Drawing.Size(104, 24);
            this.rbCurrentPage.TabIndex = 3;
            this.rbCurrentPage.TabStop = true;
            this.rbCurrentPage.Text = "選択した部分";
            this.rbCurrentPage.UseVisualStyleBackColor = true;
            // 
            // rbPage
            // 
            this.rbPage.Location = new System.Drawing.Point(16, 48);
            this.rbPage.Name = "rbPage";
            this.rbPage.Size = new System.Drawing.Size(104, 24);
            this.rbPage.TabIndex = 2;
            this.rbPage.TabStop = true;
            this.rbPage.Text = "ページ指定";
            this.rbPage.UseVisualStyleBackColor = true;
            // 
            // rbALL
            // 
            this.rbALL.Location = new System.Drawing.Point(16, 24);
            this.rbALL.Name = "rbALL";
            this.rbALL.Size = new System.Drawing.Size(104, 24);
            this.rbALL.TabIndex = 1;
            this.rbALL.TabStop = true;
            this.rbALL.Text = "全て";
            this.rbALL.UseVisualStyleBackColor = true;
            // 
            // gbPrintCopy
            // 
            this.gbPrintCopy.Controls.Add(this.nudPrintCopy);
            this.gbPrintCopy.Controls.Add(this.lblPrintCopy);
            this.gbPrintCopy.Location = new System.Drawing.Point(280, 128);
            this.gbPrintCopy.Name = "gbPrintCopy";
            this.gbPrintCopy.Size = new System.Drawing.Size(144, 128);
            this.gbPrintCopy.TabIndex = 2;
            this.gbPrintCopy.TabStop = false;
            this.gbPrintCopy.Text = "印刷部数";
            // 
            // nudPrintCopy
            // 
            this.nudPrintCopy.Location = new System.Drawing.Point(72, 16);
            this.nudPrintCopy.Name = "nudPrintCopy";
            this.nudPrintCopy.Size = new System.Drawing.Size(56, 19);
            this.nudPrintCopy.TabIndex = 10;
            this.nudPrintCopy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblPrintCopy
            // 
            this.lblPrintCopy.Location = new System.Drawing.Point(8, 16);
            this.lblPrintCopy.Name = "lblPrintCopy";
            this.lblPrintCopy.Size = new System.Drawing.Size(64, 23);
            this.lblPrintCopy.TabIndex = 9;
            this.lblPrintCopy.Text = "部数:";
            this.lblPrintCopy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(216, 264);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(328, 264);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormPrintDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(434, 296);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbPrintCopy);
            this.Controls.Add(this.gbPrintRange);
            this.Controls.Add(this.gbPrinter);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPrintDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "印刷";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.FormPrintDialog_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormPrintDialog_KeyDown);
            this.gbPrinter.ResumeLayout(false);
            this.gbPrintRange.ResumeLayout(false);
            this.gbPrintRange.PerformLayout();
            this.gbPrintCopy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudPrintCopy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPrinter;
        private System.Windows.Forms.Label lblLocationCaption;
        private System.Windows.Forms.Label lblStatusCaption;
        private System.Windows.Forms.Label lblPrintNameCaption;
        private System.Windows.Forms.Label lblCommentCaption;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cbPrinterName;
        private System.Windows.Forms.GroupBox gbPrintRange;
        private System.Windows.Forms.RadioButton rbCurrentPage;
        private System.Windows.Forms.RadioButton rbPage;
        private System.Windows.Forms.RadioButton rbALL;
        private CommonClass.NumTextBox2 tbPageFrom;
        private System.Windows.Forms.Label label2;
        private CommonClass.NumTextBox2 tbPageTo;
        private System.Windows.Forms.GroupBox gbPrintCopy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudPrintCopy;
        private System.Windows.Forms.Label lblPrintCopy;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
