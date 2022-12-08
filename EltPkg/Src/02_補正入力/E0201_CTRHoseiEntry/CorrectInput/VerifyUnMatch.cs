using System;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;

namespace CorrectInput
{
    /// <summary>
    /// ベリファイアンマッチ画面
    /// </summary>
    internal partial class VerifyUnMatch : UnclosableForm
    {
        private string _entdata;
        private string _vfydata;
        private string _itemname;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="entdata"></param>
        /// <param name="vfydata"></param>
        /// <param name="itemname"></param>
        internal VerifyUnMatch( string entdata, string vfydata, string itemname)
        {
            InitializeComponent();

            _entdata = entdata;
            _vfydata = vfydata;
            _itemname = itemname;
        }

        /// <summary>
        /// [フォーム] Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerifyUnMatch_Load(object sender, EventArgs e)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), this.Text + "画面表示", 1);

            lblEntryValue.AutoSize = true;
            lblVerifyValue.AutoSize = true;
            lblParamName.Text = _itemname;
            lblEntryValue.Text = _entdata;
            lblVerifyValue.Text = _vfydata;

            // Entry・Verifyの幅が大きいサイズに合わせる
            int MaxWidth = lblEntryValue.Width > lblVerifyValue.Width ? lblEntryValue.Width : lblVerifyValue.Width;
            lblEntryValue.AutoSize = false;
            lblEntryValue.Width = MaxWidth;
            lblVerifyValue.AutoSize = false;
            lblVerifyValue.Width = MaxWidth;
        }

        /// <summary>
        /// [フォーム] KeyUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerifyUnMatch_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    btnBack_Click(sender, e);
                    break;
                case Keys.F12:
                    btnFixed_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// [F1:戻る]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// [F12:確定]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFixed_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
