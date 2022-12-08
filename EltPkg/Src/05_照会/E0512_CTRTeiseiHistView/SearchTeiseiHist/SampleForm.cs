using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using EntryCommon;

namespace SearchTeiseiRireki
{
    /// <summary>
    /// ＸＸＸＸＸ画面
    /// </summary>
    public partial class SampleForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;


        private const int F1_ = 1;
        private const int F2_ = 2;
        private const int F3_ = 3;
        private const int F4_ = 4;
        private const int F5_ = 5;
        private const int F6_ = 6;
        private const int F7_ = 7;
        private const int F8_ = 8;
        private const int F9_ = 9;
        private const int F10_ = 10;
        private const int F11_ = 11;
        private const int F12_ = 12;

        private const int SF1_ = 1;
        private const int SF2_ = 2;
        private const int SF3_ = 3;
        private const int SF4_ = 4;
        private const int SF5_ = 5;
        private const int SF6_ = 6;
        private const int SF7_ = 7;
        private const int SF8_ = 8;
        private const int SF9_ = 9;
        private const int SF10_ = 10;
        private const int SF11_ = 11;
        private const int SF12_ = 12;

        private const int CF1_ = 1;
        private const int CF2_ = 2;
        private const int CF3_ = 3;
        private const int CF4_ = 4;
        private const int CF5_ = 5;
        private const int CF6_ = 6;
        private const int CF7_ = 7;
        private const int CF8_ = 8;
        private const int CF9_ = 9;
        private const int CF10_ = 10;
        private const int CF11_ = 11;
        private const int CF12_ = 12;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SampleForm()
        {
            
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

            // コントロール初期化
            InitializeControl();

            base.InitializeForm(ctl);
        }


        // *******************************************************************
        // 継承メソッド
        // *******************************************************************

        /// <summary>
        /// 業務名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName1(string dispName)
        {
            base.SetDispName1("サンプル業務");
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("サンプル画面");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            // ファンクションを切り替えない場合（1段しかない場合）は、if-else ブロックを削除して処理を記述してOK

            if (IsNotPressCSAKey)
            {
                // 通常状態
                SetFunctionName(F1_, "終了");
                SetFunctionName(F2_, string.Empty);
                SetFunctionName(F3_, "検索条件\n切替", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F4_, "並び替え", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F5_, "合計");
                SetFunctionName(F6_, string.Empty);
                SetFunctionName(F7_, string.Empty);
                SetFunctionName(F8_, "詳細");
                SetFunctionName(F9_, string.Empty);
                SetFunctionName(F10_, string.Empty);
                SetFunctionName(F11_, string.Empty);
                SetFunctionName(F12_, "更新");
            }
            else if (IsPressShiftKey)
            {
                // Shiftキー押下
                SetFunctionName(SF1_, string.Empty);
                SetFunctionName(SF2_, "前へ");
                SetFunctionName(SF3_, "次へ");
                SetFunctionName(SF4_, string.Empty);
                SetFunctionName(SF5_, string.Empty);
                SetFunctionName(SF6_, string.Empty);
                SetFunctionName(SF7_, string.Empty);
                SetFunctionName(SF8_, "検索");
                SetFunctionName(SF9_, string.Empty);
                SetFunctionName(SF10_, string.Empty);
                SetFunctionName(SF11_, string.Empty);
                SetFunctionName(SF12_, "印刷");
            }
            else if (IsPressCtrlKey)
            {
                // Ctrlキー押下
                SetFunctionName(CF1_, string.Empty);
                SetFunctionName(CF2_, string.Empty);
                SetFunctionName(CF3_, string.Empty);
                SetFunctionName(CF4_, "明細一覧", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(CF5_, string.Empty);
                SetFunctionName(CF6_, string.Empty);
                SetFunctionName(CF7_, string.Empty);
                SetFunctionName(CF8_, string.Empty);
                SetFunctionName(CF9_, string.Empty);
                SetFunctionName(CF10_, string.Empty);
                SetFunctionName(CF11_, string.Empty);
                SetFunctionName(CF12_, string.Empty);
            }
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
            // 権限制御しないのであればここは空実装でOK

            if (!IsPressShiftKey)
            {
                // 通常状態
                if (AppInfo.Setting.IsAuth)
                {
                    // 権限あり
                    SetFunctionState(F4_, true);
                    SetFunctionState(F5_, true);
                }
                else
                {
                    // 権限なし
                    SetFunctionState(SF4_, false);
                    SetFunctionState(SF5_, false);
                }
            }
            else
            {
                // Shiftキー押下
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
            // 初期値設定
           
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
            // 活性・非活性処理

            // ファンクションキー状態を設定
            SetFunctionState();
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
            // 画面項目を設定する処理はまとめてここに実装してこのメソッドを呼ぶ
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            // 画面項目取得する処理はここに記載する
            // 入力チェックもここで行う

            // 機能（ボタン押下）によって入力チェックの内容が異なる場合は適当にメソッド追加してOK


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
        private void SampleForm_Load(object sender, EventArgs e)
        {
            // フォーカス初期位置設定
           
        }

        /// <summary>
        /// [画面項目] KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Down:
                    e.SuppressKeyPress = true;
                    SendKeys.Send("{TAB}");
                    break;
                case Keys.Up:
                    // +({TAB})で[Shift+TAB]の意味になる
                    e.SuppressKeyPress = true;
                    SendKeys.Send("+({TAB})");
                    break;
                case Keys.ShiftKey:
                case Keys.ControlKey:
                    if (ChangeFunction(e)) return;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// [画面項目] KeyUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyUp(object sender, KeyEventArgs e)
        {
            if (ChangeFunction(e)) return;
        }

        /// <summary>
        /// [テキストボックス] Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        /// <summary>
        /// [登録]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGymSearch_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }


        // *******************************************************************
        // イベント（ファンクションキー）
        // *******************************************************************

        /// <summary>
        /// F2：通常処理
        /// SF2：Shift押下処理
        /// CF2：Ctrl押下処理
        /// </summary>
        protected override void btnFunc02_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // ファンクションを切り替えない場合（1段しかない場合）は、if-else ブロックを削除して処理を記述してOK

                //TODO: ここに処理を記述
                if (IsNotPressCSAKey)
                {
                    //TODO: 通常処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "通常処理", 1);
                }
                else if (IsPressShiftKey)
                {
                    //TODO: Shift押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Shift押下処理", 1);
                }
                else if (IsPressCtrlKey)
                {
                    //TODO: Ctrl押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Ctrl押下処理", 1);
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
            }
        }

        /// <summary>
        /// F3：
        /// SF3：
        /// CF3：
        /// </summary>
        protected override void btnFunc03_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // ファンクションを切り替えない場合（1段しかない場合）は、if-else ブロックを削除して処理を記述してOK

                //TODO: ここに処理を記述
                if (IsNotPressCSAKey)
                {
                    //TODO: 通常処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "通常処理", 1);
                }
                else if (IsPressShiftKey)
                {
                    //TODO: Shift押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Shift押下処理", 1);
                }
                else if (IsPressCtrlKey)
                {
                    //TODO: Ctrl押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Ctrl押下処理", 1);
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
            }
        }

        /// <summary>
        /// F4：
        /// SF4：
        /// CF4：
        /// </summary>
        protected override void btnFunc04_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // ファンクションを切り替えない場合（1段しかない場合）は、if-else ブロックを削除して処理を記述してOK

                //TODO: ここに処理を記述
                if (IsNotPressCSAKey)
                {
                    //TODO: 通常処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "通常処理", 1);
                }
                else if (IsPressShiftKey)
                {
                    //TODO: Shift押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Shift押下処理", 1);
                }
                else if (IsPressCtrlKey)
                {
                    //TODO: Ctrl押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Ctrl押下処理", 1);
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
            }
        }

        /// <summary>
        /// F5：
        /// SF5：
        /// CF5：
        /// </summary>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // ファンクションを切り替えない場合（1段しかない場合）は、if-else ブロックを削除して処理を記述してOK

                //TODO: ここに処理を記述
                if (IsNotPressCSAKey)
                {
                    //TODO: 通常処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "通常処理", 1);
                }
                else if (IsPressShiftKey)
                {
                    //TODO: Shift押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Shift押下処理", 1);
                }
                else if (IsPressCtrlKey)
                {
                    //TODO: Ctrl押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Ctrl押下処理", 1);
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
            }
        }

        /// <summary>
        /// F6：
        /// SF6：
        /// CF6：
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // ファンクションを切り替えない場合（1段しかない場合）は、if-else ブロックを削除して処理を記述してOK

                //TODO: ここに処理を記述
                if (IsNotPressCSAKey)
                {
                    //TODO: 通常処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "通常処理", 1);
                }
                else if (IsPressShiftKey)
                {
                    //TODO: Shift押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Shift押下処理", 1);
                }
                else if (IsPressCtrlKey)
                {
                    //TODO: Ctrl押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Ctrl押下処理", 1);
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
            }
        }

        /// <summary>
        /// F7：
        /// SF7：
        /// CF7：
        /// </summary>
        protected override void btnFunc07_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // ファンクションを切り替えない場合（1段しかない場合）は、if-else ブロックを削除して処理を記述してOK

                //TODO: ここに処理を記述
                if (IsNotPressCSAKey)
                {
                    //TODO: 通常処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "通常処理", 1);
                }
                else if (IsPressShiftKey)
                {
                    //TODO: Shift押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Shift押下処理", 1);
                }
                else if (IsPressCtrlKey)
                {
                    //TODO: Ctrl押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Ctrl押下処理", 1);
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
            }
        }

        /// <summary>
        /// F8：
        /// SF8：
        /// CF8：
        /// </summary>
        protected override void btnFunc08_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // ファンクションを切り替えない場合（1段しかない場合）は、if-else ブロックを削除して処理を記述してOK

                //TODO: ここに処理を記述
                if (IsNotPressCSAKey)
                {
                    //TODO: 通常処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "通常処理", 1);
                }
                else if (IsPressShiftKey)
                {
                    //TODO: Shift押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Shift押下処理", 1);
                }
                else if (IsPressCtrlKey)
                {
                    //TODO: Ctrl押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Ctrl押下処理", 1);
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
            }
        }

        /// <summary>
        /// F9：
        /// SF9：
        /// CF9：
        /// </summary>
        protected override void btnFunc09_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // ファンクションを切り替えない場合（1段しかない場合）は、if-else ブロックを削除して処理を記述してOK

                //TODO: ここに処理を記述
                if (IsNotPressCSAKey)
                {
                    //TODO: 通常処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "通常処理", 1);
                }
                else if (IsPressShiftKey)
                {
                    //TODO: Shift押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Shift押下処理", 1);
                }
                else if (IsPressCtrlKey)
                {
                    //TODO: Ctrl押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Ctrl押下処理", 1);
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
            }
        }

        /// <summary>
        /// F10：
        /// SF10：
        /// CF10：
        /// </summary>
        protected override void btnFunc10_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // ファンクションを切り替えない場合（1段しかない場合）は、if-else ブロックを削除して処理を記述してOK

                //TODO: ここに処理を記述
                if (IsNotPressCSAKey)
                {
                    //TODO: 通常処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "通常処理", 1);
                }
                else if (IsPressShiftKey)
                {
                    //TODO: Shift押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Shift押下処理", 1);
                }
                else if (IsPressCtrlKey)
                {
                    //TODO: Ctrl押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Ctrl押下処理", 1);
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
            }
        }

        /// <summary>
        /// F11：
        /// SF11：
        /// CF11：
        /// </summary>
        protected override void btnFunc11_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // ファンクションを切り替えない場合（1段しかない場合）は、if-else ブロックを削除して処理を記述してOK

                //TODO: ここに処理を記述
                if (IsNotPressCSAKey)
                {
                    //TODO: 通常処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "通常処理", 1);
                }
                else if (IsPressShiftKey)
                {
                    //TODO: Shift押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Shift押下処理", 1);
                }
                else if (IsPressCtrlKey)
                {
                    //TODO: Ctrl押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Ctrl押下処理", 1);
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
            }
        }

        /// <summary>
        /// F12：
        /// SF12：
        /// CF12：
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // ファンクションを切り替えない場合（1段しかない場合）は、if-else ブロックを削除して処理を記述してOK

                //TODO: ここに処理を記述
                if (IsNotPressCSAKey)
                {
                    //TODO: 通常処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "通常処理", 1);

                    // 画面項目取得
                    if (!GetDisplayParams())
                    {
                        return;
                    }

                    // 検索結果詳細画面
                    SampleForm form = new SampleForm();
                    form.InitializeForm(_ctl);
                    form.ShowDialog();
                }
                else if (IsPressShiftKey)
                {
                    //TODO: Shift押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Shift押下処理", 1);
                }
                else if (IsPressCtrlKey)
                {
                    //TODO: Ctrl押下処理
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "Ctrl押下処理", 1);
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                InitializeFunction();
            }
        }


        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

    }
}
