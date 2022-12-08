using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;
using EntryCommon;

using System.Data;
using System.Data.OracleClient;
using System.Drawing;

namespace SearchResultText
{
    /// <summary>
    /// 検索結果一覧画面
    /// </summary>
    public partial class SearchResultDetail : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SearchResultDetail()
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


        // *******************************************************************
        // 継承メソッド
        // *******************************************************************

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName1(string dispName)
        {
            base.SetDispName1("業務共通");
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("結果照会");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            if (!IsPressShiftKey && !IsPressCtrlKey)
            {
                // 通常状態
                SetFunctionName(1, "戻る");
                SetFunctionName(2, "イメージ\n   表示", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(3, string.Empty);
                SetFunctionName(4, string.Empty);
                SetFunctionName(5, string.Empty);
                SetFunctionName(6, string.Empty);
                SetFunctionName(7, string.Empty);
                SetFunctionName(8, string.Empty);
                SetFunctionName(9, "再送登録", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(10, string.Empty);
                SetFunctionName(11, string.Empty);
                SetFunctionName(12, string.Empty);
            }
            else
            {
                // Shiftキー押下
                SetFunctionName(1, string.Empty);
                SetFunctionName(2, string.Empty);
                SetFunctionName(3, string.Empty);
                SetFunctionName(4, string.Empty);
                SetFunctionName(5, string.Empty);
                SetFunctionName(6, string.Empty);
                SetFunctionName(7, string.Empty);
                SetFunctionName(8, string.Empty);
                SetFunctionName(9, string.Empty);
                SetFunctionName(10, string.Empty);
                SetFunctionName(11, string.Empty);
                SetFunctionName(12, string.Empty);
            }
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
            if (_itemMgr.RecordListDispParams.TargetFileDicid == "YCA" || 
                _itemMgr.RecordListDispParams.TargetFileDicid == "BUB")
            {
                base.SetFunctionState(12, false);
            }
            else
            {
                base.SetFunctionState(12, true);
            }
        }

        /// <summary>
        /// フォームを再描画する
        /// </summary>
        public override void ResetForm()
        {
            // 画面表示データ更新
            InitializeDisplayData();
            // 画面表示データ更新
            RefreshDisplayData();

            // 画面表示状態更新
            RefreshDisplayState();
        }
        /// <summary>
        /// コントロール初期化
        /// </summary>
        protected void InitializeControl()
        {
        }

        /// <summary>
        /// 画面表示データ初期化
        /// </summary>
        protected void InitializeDisplayData()
        {

        }

        /// <summary>
        /// 画面表示データ更新
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
            // ファンクションキー状態を設定
            SetFunctionState();
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>

        protected override void SetDisplayParams()
        {
            // ファイル情報欄
            SearchResultCommon.UpdatepnlInfo(_itemMgr, pnlInfo);

            //一覧更新
            if (!_itemMgr.FetchResultText())
            {
                return;
            }

            //名称一覧取得
            List<string> name = SearchResultCommon.GetDispNameData();
            //値一覧取得
            List<string> item = SearchResultCommon.GetDispValueData(_itemMgr, _itemMgr.ResultTxt);

            ListViewItem[] listView = new ListViewItem[name.Count];
            for (int i = 0; i < name.Count; i++)
            {
                List<string> DspData = new List<string>();
                DspData.Add(name[i]);
                DspData.Add(item[i]);
                listView[i] = new ListViewItem(DspData.ToArray());
            }
            this.lvDataList.Items.Clear();
            this.lvDataList.Items.AddRange(listView);
            this.lvDataList.Enabled = true;
            this.lvDataList.Refresh();
            this.lvDataList.Select();

            // 列幅自動調整
            this.lvDataList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            // 値欄の調整
            this.lvDataList.Columns[1].Width =
                 Math.Max(this.lvDataList.Columns[1].Width, this.lvDataList.Width - this.lvDataList.Columns[0].Width - 24);
            if (this.lvDataList.Items.Count > 0)
            {
                this.lvDataList.Items[0].Selected = true;
                this.lvDataList.Items[0].Focused = true;
            }
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            return true;
        }

        // *******************************************************************
        // イベント
        // *******************************************************************

        /// <summary>
        /// [画面項目] KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyDown(object sender, KeyEventArgs e)
        {
            //this.ClearStatusMessage();
            if (ChangeFunction(e)) SetFunctionState(); return;
        }

        /// <summary>
        /// [画面項目] KeyUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyUp(object sender, KeyEventArgs e)
        {
            if (ChangeFunction(e)) SetFunctionState(); return;
        }

        /// <summary>
        /// 列幅変更不可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lv_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = ((ListView)sender).Columns[e.ColumnIndex].Width;
        }

        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        /// <summary>
        /// F2：イメージ表示
        /// </summary>
        protected override void btnFunc02_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "イメージ表示", 1);
                // 証券イメージ画面表示
                SearchResultDetailImage form = new SearchResultDetailImage();
                form.InitializeForm(_ctl);
                form.ResetForm();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F9：再送登録
        /// </summary>
        protected override void btnFunc09_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();

            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "再送登録", 1);
                //確認メッセージ表示
                if ((ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "再送登録を行いますがよろしいですか？") == DialogResult.No))
                {
                    return;
                }

                bool flg = false;
                switch (_itemMgr.RecordListDispParams.TargetFileDicid)
                {
                    case "BCA":
                        // 持出取消
                        flg = ResendBCA();
                        break;
                    case "GMA":
                        // 証券データ訂正
                        flg = ResendGMA();
                        break;
                    case "GRA":
                        // 不渡返還
                        flg = ResendGRA();
                        break;
                    default:
                        ComMessageMgr.MessageInformation("再送登録対象外データです。");
                        break;
                }

                if (flg)
                {
                    ComMessageMgr.MessageInformation("再送登録を行いました。");
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        ///再送登録(BUB)
        /// </summary>
        private bool ResendBUB()
        {
            try
            {
                string filename = _itemMgr.ResultTxt.m_RECEPTION;

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("再送登録(BUB)：{0}", filename), 3);

                // 対象データの取得
                if (!_itemMgr.GetBUBData(filename, out TBL_TRMEIIMG Data, false, out int InputRoute, this))
                {
                    ComMessageMgr.MessageError("対象データに対する明細情報が存在しませんでした。");
                    return false;
                }
                // ステータス確認
                if (Data.m_BUA_STS != 19)
                {
                    ComMessageMgr.MessageWarning("対象データは再送不可の明細です。");
                    return false;
                }
                // 更新処理
                if (!_itemMgr.UpdateBUBData(filename, this))
                {
                    ComMessageMgr.MessageError("更新処理に失敗しました。");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                return false;
            }
        }

        /// <summary>
        ///再送登録(BCA)
        /// </summary>
        private bool ResendBCA()
        {
            try
            {
                if (_itemMgr.ResultTxt.m_RECEPTION.Length < 63 )
                {
                    ComMessageMgr.MessageError("明細情報の取得に失敗しました。");
                    return false;
                }
                string filename = _itemMgr.ResultTxt.m_RECEPTION.Substring(1,62).Trim();

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("再送登録(BCA)：{0}", filename), 3);

                // 対象データの取得
                if (!_itemMgr.GetBCAData(filename, out TBL_TRMEI Data, false, out int InputRoute, this))
                {
                    ComMessageMgr.MessageError("対象データに対する明細情報が存在しませんでした。");
                    return false;
                }
                // ステータス確認
                if (Data.m_BCA_STS != 19)
                {
                    ComMessageMgr.MessageWarning("対象データは再送不可の明細です。");
                    return false;
                }
                // 更新処理
                if (!_itemMgr.UpdateBCAData(filename, this))
                {
                    ComMessageMgr.MessageError("更新処理に失敗しました。");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                return false;
            }
        }

        /// <summary>
        ///再送登録(GMA)
        /// </summary>
        private bool ResendGMA()
        {
            try
            {
                if (_itemMgr.ResultTxt.m_RECEPTION.Length < 63)
                {
                    ComMessageMgr.MessageError("明細情報の取得に失敗しました。");
                    return false;
                }
                string filename = _itemMgr.ResultTxt.m_RECEPTION.Substring(1, 62).Trim();

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("再送登録(GMA)：{0}", filename), 3);

                // 対象データの取得
                if (!_itemMgr.GetGMAData(filename, out TBL_TRMEI Data, this))
                {
                    ComMessageMgr.MessageError("対象データに対する明細情報が存在しませんでした。");
                    return false;
                }
                // ステータス確認
                if (Data.m_GMA_STS != 19)
                {
                    ComMessageMgr.MessageWarning("対象データは再送不可の明細です。");
                    return false;
                }
                // 更新処理
                if (!_itemMgr.UpdateGMAData(filename, this))
                {
                    ComMessageMgr.MessageError("更新処理に失敗しました。");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                return false;
            }
        }

        /// <summary>
        ///再送登録(GRA)
        /// </summary>
        private bool ResendGRA()
        {
            try
            {
                if (_itemMgr.ResultTxt.m_RECEPTION.Length < 64)
                {
                    ComMessageMgr.MessageError("明細情報の取得に失敗しました。");
                    return false;
                }
                string filename = _itemMgr.ResultTxt.m_RECEPTION.Substring(2, 62).Trim();

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("再送登録(GRA)：{0}", filename), 3);

                // 対象データの取得
                if (!_itemMgr.GetGRAData(filename, out TBL_TRMEI Data, this))
                {
                    ComMessageMgr.MessageError("対象データに対する明細情報が存在しませんでした。");
                    return false;
                }
                // ステータス確認
                if (Data.m_GRA_STS != 19)
                {
                    ComMessageMgr.MessageWarning("対象データは再送不可の明細です。");
                    return false;
                }
                // 更新処理
                if (!_itemMgr.UpdateGRAData(filename, this))
                {
                    ComMessageMgr.MessageError("更新処理に失敗しました。");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
                return false;
            }
        }

    }
}
