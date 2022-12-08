using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using EntryCommon;

namespace HulftIO
{
    /// <summary>
    /// HULFT伝送画面
    /// </summary>
	public partial class HulftIOForm : EntryCommonDialogBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

		private string TransName
        {
            get { return (_itemMgr.DispParams.TransType == ItemManager.TransParams.集信) ? "集信" : "配信"; }
        }

        /// <summary>HULFT 処理結果</summary>
        public int Result { get; set; } = 0;
        /// <summary>HULFT エラーメッセージ</summary>
        public string ErrMsg { get; set; } = "";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ctl"></param>
        public HulftIOForm()
		{
			InitializeComponent();
        }

        // *******************************************************************
        // 公開メソッド
        // *******************************************************************

        /// <summary>
        /// フォームを初期化する
        /// </summary>
        public override void InitializeForm(ControllerBase ctl)
        {
            _ctl = (Controller)ctl;
            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;

            base.InitializeForm(ctl);
        }

        /// <summary>
        /// HULFT 処理実行
        /// </summary>
        public void RunHulftProcess()
        {
			// 画面項目取得
			GetDisplayParams();

			// HULFT 実行
			int res = 0;
			if (_itemMgr.DispParams.TransType == ItemManager.TransParams.集信)
			{
				// 集信
				res = _ctl.RecvHulft(_itemMgr.TransInfo.RHulftID);
			}
			else // if (_itemMgr.DispParams.TransType == ItemManager.TransParams.配信)
			{
				// 配信
				res = _ctl.SendHulft(_itemMgr.TransInfo.SHulftID);
			}

            if (res != ItemManager.TransParams.RESULT_SUCCESS)
            {
                this.Result = res;
                this.ErrMsg = _itemMgr.TransInfo.ErrMsg;
                return;
            }

            // 完了メッセージ
            if (!_itemMgr.DispParams.IsAutoExec)
            {
                MessageBox.Show(string.Format("HULFT{0}が完了しました。", this.TransName), "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // *******************************************************************
        // 継承メソッド
        // *******************************************************************

        /// <summary>
        /// フォームを再描画する
        /// </summary>
        public override void ResetForm()
        {
            // 画面表示データ更新
            RefreshDisplayData();

            // 画面表示状態更新
            RefreshDisplayState();
        }

        /// <summary>
        /// 画面表示データ更新（初期値設定）
        /// </summary>
        protected override void RefreshDisplayData()
        {
            // 画面項目設定
            SetDisplayParams();
        }

        /// <summary>
        /// 画面表示状態更新
        /// </summary>
        protected override void RefreshDisplayState()
        {
            // 自動実行する場合はフォーム非表示
            if (_itemMgr.DispParams.IsAutoExec)
            {
                this.Visible = false;
            }
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            this.Result = ItemManager.TransParams.RESULT_SUCCESS;
            this.ErrMsg = "";
            _itemMgr.TransInfo.Clear();
            _itemMgr.TransInfo.SHulftID = _itemMgr.DispParams.SHulftID;
            _itemMgr.TransInfo.RHulftID = _itemMgr.DispParams.RHulftID;
            _itemMgr.TransInfo.FileName = _itemMgr.DispParams.FileName;
            _itemMgr.TransInfo.SendDirPath = _itemMgr.DispParams.SendDirPath;
            _itemMgr.TransInfo.RecvDirPath = _itemMgr.DispParams.RecvDirPath;
            return true;
        }

        // *******************************************************************
        // イベント
        // *******************************************************************

        /// <summary>
		/// [フォーム] ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SampleDialog_Load(object sender, EventArgs e)
        {
            try
            {
                // 自動実行
                if (_itemMgr.DispParams.IsAutoExec)
                {
                    btnStart_Click(sender, e);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.Close();
            }
        }

        /// <summary>
        /// [開始]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                // HULFT 処理実行
                RunHulftProcess();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.Result = ItemManager.TransParams.RESULT_HULFT_ERR;
                this.ErrMsg = ex.Message;
            }
        }

        /// <summary>
        /// [終了]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnd_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            }
        }


        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

    }
}
