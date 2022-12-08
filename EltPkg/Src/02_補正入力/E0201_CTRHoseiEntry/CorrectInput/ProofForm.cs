using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using EntryCommon;

namespace CorrectInput
{
    /// <summary>
    /// プルーフ画面
    /// </summary>
    internal partial class ProofForm : UnclosableForm
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private ItemManager.ProofInfo _pi = null;

        public ProofResult Result { get; private set; }

        public enum ProofResult
        {
            中断,
            継続,
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ctl"></param>
        public ProofForm(ControllerBase ctl, ItemManager.ProofInfo pi)
        {
            InitializeComponent();

            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;

            _pi = pi;
        }

        // *******************************************************************
        // イベント
        // *******************************************************************

        /// <summary>
        /// [フォーム] ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrfResult_Load(object sender, EventArgs e)
        {
            if (_itemMgr.DspParams.IsAutoReceiveBatch)
            {
                // 自動配信
                btnBack.Enabled = true;
                btnBack.Visible = true;
                btnMList.Enabled = true;
                btnMList.Visible = true;
                btnMList.Text = "F12：継続";
            }
            else
            {
                // 単一選択
                btnBack.Enabled = false;
                btnBack.Visible = false;
                btnMList.Enabled = true;
                btnMList.Visible = true;
                btnMList.Text = "F12：OK";
            }

            lblBatId.Text = _pi.BatId.ToString("D6");
            lblBatCnt.Text = string.Format("{0:#,##0}", _pi.BatCount);
            lblBatKingaku.Text = string.Format("{0:#,##0}", _pi.BatAmount);
            lblMeiCnt.Text = string.Format("{0:#,##0}", _pi.MeiCount);
            lblMeiKingaku.Text = string.Format("{0:#,##0}", _pi.MeiAmount);
            lblDiffCnt.Text = string.Format("{0:#,##0}", (_pi.BatCount -_pi.MeiCount));
            lblDiffKingaku.Text = string.Format("{0:#,##0}", (_pi.BatAmount - _pi.MeiAmount));
        }

        /// <summary>
        /// [F1：中断]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            Result = ProofResult.中断;
            this.Close();
        }

        /// <summary>
        /// [F12：継続]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMList_Click(object sender, EventArgs e)
        {
            Result = ProofResult.継続;
            this.Close();
        }

        /// <summary>
        /// [フォーム] KeyUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProofForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    if (btnBack.Enabled) btnBack_Click(sender, e);
                    break;
                case Keys.F12:
                    if (btnMList.Enabled) btnMList_Click(sender, e);
                    break;
                default:
                    break;
            }
        }
    }
}
