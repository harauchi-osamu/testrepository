using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryClass;
using EntryCommon;
using ImageController;
using static CorrectInput.EntryController;

namespace CorrectInput
{
    /// <summary>
    /// 補正入力画面（共通）
    /// </summary>
    public partial class EntryFormBase : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        protected EntryController _econ { get { return _itemMgr.EntController; } }
        protected EntryDspControl _dcon { get { return _itemMgr.DspControl; } }
        protected EntryImageHandler eiHandler { get { return _itemMgr.ImageHandler; } }
        protected EntryInputChecker eiChecker { get { return _itemMgr.Checker; } }
        protected EntryDataUpdater edUpdater { get { return _itemMgr.Updater; } }
        protected MeisaiInfo _curMei { get { return _itemMgr.CurBat.CurMei; } }

        protected bool _isInitedShown = false;
        protected bool _isFormClosing = false;
        protected bool _vfyChkFlg = false;
        protected int _LeaveTextItemid = 0;
        private bool _isShowDialog = false;

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

        protected bool CanMovePrev { get { return true; } }
        protected bool CanMoveNext { get { return true; } }

        public enum ProcType
        {
            明細確定,
            明細移動,
            自動実行,
        }

        public enum ExecMode
        {
            明細確定,
            明細移動,
            //バッチ終了, バッチ終了と明細確定は差がないため廃止
        }

        public enum FuncType
        {
            未実行,
            終了,
            保留,
            確定,
        }

        public enum DataType
        {
            Cur,
            Next,
            End,
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EntryFormBase()
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
            _ctlb = ctl;

            _masterMgr = ctl.MasterMgr;
            _itemMgr = (ItemManager)ctl.ItemMgr;

            // 処理日
            SetOpDateLabel(0);

            // 画面名設定
            SetDispName1("");
            SetDispName2("");

            // 画面ID
            SetGamenID();

            // デザイナで登録すると何故か子クラスのデザイナ表示でエラーになるのでここに記述
            this.Shown += new System.EventHandler(this.EntryFormBase_Shown);
        }

        /// <summary>
        /// 処理開始
        /// </summary>
        public virtual void StartControl(EntryController econ, MeisaiInfo meiNext)
        {
            // エントリーコントローラー初期化
            _itemMgr.EntController = econ;

            // エントリ開始
            ExecControl(meiNext);
        }

        /// <summary>
        /// エントリ開始
        /// </summary>
        /// <returns>
        /// true :画面表示あり
        /// false:画面表示なし
        /// </returns>
        protected virtual bool ExecControl(MeisaiInfo meiNext, EntryFormBase.DataType type = DataType.Next)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), string.Format("開始 GYM={0}, OPE={1}, SCAN={2}, BAT={3}, DET={4}", meiNext._GYM_ID, meiNext._OPERATION_DATE, meiNext._SCAN_TERM, meiNext._BAT_ID, meiNext._DETAILS_NO), 1);

            // 処理明細のキーを作成
            string ExecMeisaiKey = CommonUtil.GenerateKey(meiNext._GYM_ID, meiNext._OPERATION_DATE, meiNext._SCAN_TERM, meiNext._BAT_ID, meiNext._DETAILS_NO);

            // エントリー前準備（排他制御、ステータス更新）
            if (type == DataType.Next)
            {
                if (!_ctl.PrepareEntryStatus(meiNext.hosei_status, _itemMgr.DspParams.ExecMode))
                {
                    // ステータス更新失敗(ステータスチェック・ユーザーチェックエラー)
                    if (_itemMgr.DspParams.IsAutoReceiveBatch)
                    {
                        // 自動配信

                        // 現明細のAutoSkipチェック
                        EntryController.AutoResult ChkAuto = _econ.ChkAutoEntry(meiNext, type);
                        // 次明細の処理を実施
                        //// (現在の明細を除外リストに追加して次明細を取得)
                        //_itemMgr.IgnoreMeisaiList.Add(ExecMeisaiKey);
                        int curDspId = _curMei._DSP_ID;
                        meiNext = GetAutoReceiveBatch();
                        if (meiNext == null)
                        {
                            // 次明細なし
                            if (ChkAuto == AutoResult.NextAuto)
                            {
                                // 現明細がAutoSkip対象の場合、メッセージを表示して終了
                                string proc = _itemMgr.DspParams.IsEntryExec ? "エントリー" : "ベリファイ";
                                ComMessageMgr.MessageInformation("自動{0}処理が完了しました。", proc);
                            }
                            return false;
                        }

                        // 画面IDが変わったらイメージ＆コントロール再描画
                        if (curDspId != meiNext._DSP_ID)
                        {
                            RefreshAll();
                        }

                        // コントロール削除
                        RemoveControls();
                        RemoveImgControl();

                        // テキストボックスクリア
                        _dcon.ClearDspItems();

                        // 次明細あり
                        // 次明細処理
                        return ExecControl(meiNext);
                        //return true;
                    }
                    else
                    {
                        // 自動配信しない場合は終了
                        this.Visible = false;
                        return false;
                    }
                }
            }

            // 最後に処理した明細を保持する(ステータス更新成功後に設定)
            _itemMgr.SelectedInfo.Key = ExecMeisaiKey;

            // フォーム初期化
            this.InitializeForm(_ctl);

            // モデル作成
            if (!_econ.MakeModel(meiNext))
            {
                // 現在この箇所に来ることはないが、画面を非表示設定
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了 MakeModel()", 1);
                this.Visible = false;
                return false;
            }

            // 自動エントリ
            EntryController.AutoResult autoRes = _econ.AutoEntry(this, meiNext, type);
            if (autoRes == EntryController.AutoResult.End)
            {
                // AutoSkipがOK & 確定処理エラー・プルーフ画面で中断・選択配信
                // 画面を非表示設定
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了 AutoEntry()", 1);
                this.Visible = false;
                return false;
            }
            else if (autoRes == EntryController.AutoResult.NextAuto)
            {
                // 次明細取得
                int curDspId = _curMei._DSP_ID;
                meiNext = GetAutoReceiveBatch();
                if (meiNext == null)
                {
                    // 明細なし
                    string proc = _itemMgr.DspParams.IsEntryExec ? "エントリー" : "ベリファイ";
                    ComMessageMgr.MessageInformation("自動{0}処理が完了しました。", proc);
                    return false;
                }

                // 画面IDが変わったらイメージ＆コントロール再描画
                if (curDspId != meiNext._DSP_ID)
                {
                    RefreshAll();
                }

                // コントロール削除
                RemoveControls();
                RemoveImgControl();

                // テキストボックスクリア
                _dcon.ClearDspItems();

                // 明細あり
                // 次明細処理
                return ExecControl(meiNext);
                ////Form_Shown();
                //return true;
            }

            // ビュー作成
            if (!_econ.MakeView(meiNext))
            {
                // 現在この箇所に来ることはないが、画面を非表示設定
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了 MakeView()", 1);
                this.Visible = false;
                return false;
            }

            // 明細開始処理（処理時間開始）
            if (type == DataType.Next)
            {
                edUpdater.UpdateMeisaiToStart();
            }

            // 明細入力画面表示
            if (!this.Visible)
            {
                if (_isShowDialog)
                {
                    this.Show();
                }
                else
                {
                    _isShowDialog = true;
                    this.ShowDialog();
                }
            }

            return true;
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
            base.SetDispName1(dispName);
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2(dispName);
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            // 通常状態
            SetFunctionName(F1_, "終了");
            SetFunctionName(F2_, "拡大");
            SetFunctionName(F3_, "縮小");
            SetFunctionName(F4_, "イメージ\n切替", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F5_, string.Empty);
            SetFunctionName(F6_, string.Empty);
            SetFunctionName(F7_, "左表示\n回転", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F8_, "右表示\n回転", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F9_, "証券種類\n選択", false, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F10_, "検索", false);
            SetFunctionName(F11_, "保留", false);
            SetFunctionName(F12_, "確定", false);
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
            bool btnEnable = !_ctl.IsDspReadOnly;
            if (_ctl.GymId == GymParam.GymId.持帰 && 
                _ctl.IsKanryouTeisei && 
                _ctl.HoseiInputMode == HoseiStatus.HoseiInputMode.交換尻)
            {
                //交換尻完了訂正は証券種類選択使用不可
                SetFunctionState(F9_, false);
            }
            else
            {
                SetFunctionState(F9_, btnEnable);
            }
            SetFunctionState(F10_, btnEnable);
            SetFunctionState(F11_, btnEnable);
            SetFunctionState(F12_, btnEnable);
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
        protected virtual void InitializeControl()
        {
        }

        /// <summary>
        /// 画面表示データ初期化
        /// </summary>
        protected virtual void InitializeDisplayData()
        {
            lblImgKbnName.Text = TrMeiImg.ImgKbn.GetName(_curMei.CurImg._IMG_KBN);
        }

        /// <summary>
        /// 画面表示データ更新
        /// </summary>
        protected override void RefreshDisplayData()
        {
            // コントロール削除
            RemoveControls();
            RemoveImgControl();

            // ボタン可否を設定
            InitializeFunction();

            // フォームヘッダー設定
            SetFormLabels();

            // コントロール、コントロールのイベント設定
            PutDspControlEvent();
            PutImgControl();
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
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            return true;
        }

        /// <summary>
        /// フォーム上部のラベルを設定
        /// </summary>
        protected void SetFormLabels()
        {
            // 取込日
            lblOpeDate.Text = CommonUtil.ConvToDateFormat(_curMei._OPERATION_DATE, 3);

            lblBatId.Text = _curMei._BAT_ID.ToString(Const.BAT_ID_LEN_STR);
            lblDetailsNo.Text = _curMei._DETAILS_NO.ToString(Const.DETAILS_NO_LEN_STR);

            // 証券種類
            lblDspName.Text = string.Format("{0} {1}", _curMei.CurDsp._DSP_ID, _curMei.CurDsp.dsp_param.m_DSP_NAME);

            // 交換希望日
            TBL_TRITEM tritem = _itemMgr.GetTritem(_curMei._GYM_ID, _curMei._OPERATION_DATE, _curMei._SCAN_TERM, _curMei._BAT_ID, _curMei._DETAILS_NO, DspItem.ItemId.交換日);
            string clearingDate = "";
            if (tritem != null)
            {
                clearingDate = string.IsNullOrEmpty(clearingDate) ? tritem.m_END_DATA : clearingDate;
                clearingDate = string.IsNullOrEmpty(clearingDate) ? tritem.m_ENT_DATA : clearingDate;
                clearingDate = string.IsNullOrEmpty(clearingDate) ? tritem.m_CTR_DATA : clearingDate;
            }
            lblClearingDate.Text = CommonUtil.ConvToDateFormat(clearingDate, 3);
        }

        /// <summary>
        /// 画面コントロールからテキストボックスを削除する
        /// </summary>
        /// <param name="pn"></param>
        public void RemoveControls()
        {
            if (_itemMgr.EntParams.IsInitControl) { return; }

            foreach (TextBox tb in _dcon.tbDspItems.Values)
            {
                tb.LostFocus -= new EventHandler(DspItem_LostFocus);
                tb.GotFocus -= new EventHandler(DspItem_GotFocus);
                tb.PreviewKeyDown -= new PreviewKeyDownEventHandler(DspItem_PreviewKeyDown);
                tb.KeyDown -= new KeyEventHandler(DspItem_KeyDown);
                tb.KeyUp -= new KeyEventHandler(DspItem_KeyUp);
                tb.Leave -= new EventHandler(DspItem_Leave);
                tb.Validating -= new CancelEventHandler(DspItem_Validate);
                tb.KeyPress -= new KeyPressEventHandler(DspItem_KePress);
                tb.Click -= new EventHandler(DspItem_Click);
                tb.MouseDown -= new MouseEventHandler(DspItem_MouseDown);
                tb.TextChanged -= new EventHandler(DspItem_TextChanged);
                contentsPanel.Controls.Remove(tb);
            }
            foreach (Label lb in _dcon.lblDspItems.Values) { contentsPanel.Controls.Remove(lb); }
            ClearStatusMessage();
        }

        /// <summary>
        /// 画面コントロールにテキストボックスを追加する
        /// </summary>
        /// <param name="edCont"></param>
        protected virtual void PutDspControlEvent()
        {
            if (_itemMgr.EntParams.IsInitControl) { return; }
            _itemMgr.EntParams.IsInitControl = true;

            foreach (TextBox tb in _dcon.tbDspItems.Values)
            {
                tb.LostFocus += new EventHandler(DspItem_LostFocus);
                tb.GotFocus += new EventHandler(DspItem_GotFocus);
                tb.PreviewKeyDown += new PreviewKeyDownEventHandler(DspItem_PreviewKeyDown);
                tb.KeyDown += new KeyEventHandler(DspItem_KeyDown);
                tb.KeyUp += new KeyEventHandler(DspItem_KeyUp);
                tb.Leave += new EventHandler(DspItem_Leave);
                tb.Validating += new CancelEventHandler(DspItem_Validate);
                tb.KeyPress += new KeyPressEventHandler(DspItem_KePress);
                tb.Click += new EventHandler(DspItem_Click);
                tb.MouseDown += new MouseEventHandler(DspItem_MouseDown);
                tb.TextChanged += new EventHandler(DspItem_TextChanged);
                contentsPanel.Controls.Add(tb);
            }
            foreach (Label lb in _dcon.lblDspItems.Values) { contentsPanel.Controls.Add(lb); }
        }

        /// <summary>
        /// 画面コントロールから画像イメージを削除する
        /// </summary>
        /// <param name="eiHandler"></param>
        public void RemoveImgControl()
        {
            if (eiHandler == null) { return; }

            // イメージ削除しない
            if (!_itemMgr.EntParams.IsDeleteImage) { return; }

            contentsPanel.Controls.Remove(eiHandler.pcPanel);
            contentsPanel.Controls.Remove(eiHandler.pcPanelLine);
            eiHandler.ClearImage();
        }

        /// <summary>
        /// 画面コントロールに画像イメージを追加する
        /// </summary>
        /// <param name="eiHandler"></param>
        protected virtual void PutImgControl()
        {
            if (eiHandler == null) { return; }

            // イメージ未初期化時のみ追加する
            if (_itemMgr.EntParams.IsInitImage) { return; }

            _itemMgr.EntParams.IsInitImage = true;
            contentsPanel.Controls.Add(eiHandler.pcPanel);
            contentsPanel.Controls.Add(eiHandler.pcPanelLine);
        }


        // *******************************************************************
        // イベント
        // *******************************************************************

        /// <summary>
        /// [フォーム] Shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EntryFormBase_Shown(object sender, EventArgs e)
        {
            if (_isInitedShown) { return; }
            try
            {
                Form_Shown();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [画面項目]テキスト GotFocus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void DspItem_GotFocus(object sender, EventArgs e)
        {
            // 参照モードは処理不要
            if (_ctl.IsDspReadOnly) { return; }
            if (_isFormClosing) { return; }
            try
            {
                int itemid = GetTextBoxIndex(sender);

                _econ.KeydownFlg = false;

                // フォーカスがあたったら背景を緑色にする
                TextBox tb = ((TextBox)sender);
                if (!tb.ReadOnly)
                {
                    // ステータスクリア
                    if (itemid != eiChecker.ErrItemId)
                    {
                        this.SetStatusMessage("", SystemColors.Control);
                    }
                    SetFocusBackColor(tb);
                }

                // 数字項目はフォーカスが得た際にカンマを外す
                _dcon.DelComma(_curMei, itemid);

                // 認識領域の描画
                eiHandler.SetImageRegion(itemid);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [画面項目]テキスト LostFocus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void DspItem_LostFocus(object sender, EventArgs e)
        {
            // 参照モードは処理不要
            if (_ctl.IsDspReadOnly) { return; }
            if (_isFormClosing) { return; }
            try
            {
                int itemid = GetTextBoxIndex(sender);

                // フォーカスが外れたら背景を白色にする
                TextBox tb = ((TextBox)sender);
                SetNormalColor(tb);

                // 数字項目はフォーカスが外れたらカンマをつける
                _dcon.AddComma(_curMei, itemid);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [画面項目]テキスト Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DspItem_Leave(object sender, EventArgs e)
        {
            // 参照モードは処理不要
            if (_ctl.IsDspReadOnly) { return; }
            try
            {
                int itemid = GetTextBoxIndex(sender);
                _LeaveTextItemid = itemid;

                // フォーカスが外れたときの処理（サブルーチン：入力項目読替）
                eiChecker.DspItemLeave(itemid);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [画面項目]テキスト Validate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DspItem_Validate(object sender, CancelEventArgs e)
        {
            // 参照モードは処理不要
            if (_ctl.IsDspReadOnly) { return; }
            try
            {
                int itemid = GetTextBoxIndex(sender);

                if (!_econ.IsForward) { return; }
                if (!_econ.KeydownFlg) { return; }

                // 入力検証
                if (!ItemValidate(itemid))
                {
                    // 検証NG
                    return;
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [画面項目]テキスト PreviewKeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DspItem_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // 参照モードは処理不要
            if (_ctl.IsDspReadOnly) { return; }
            try
            {
                int itemid = GetTextBoxIndex(sender);

                // Tabキーが押されてもフォーカスが移動しないようにして KeyDown イベントを発生させる
                if (e.KeyCode == Keys.Tab) { e.IsInputKey = true; }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [画面項目]テキスト KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void DspItem_KeyDown(object sender, KeyEventArgs e)
        {
            // 参照モードは処理不要
            if (_ctl.IsDspReadOnly) { return; }
            if (!_isInitedShown) { return; }
            try
            {
                int itemid = GetTextBoxIndex(sender);
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        _econ.KeydownFlg = true;
                        e.SuppressKeyPress = true;
                        _econ.IsForward = true;

                        // 単項目ベリファイ
                        if (!VerifyItem(itemid)) { return; }
                        GoNextItem(itemid);
                        break;

                    case Keys.Down:
                        if (IsPressAltKey)
                        {
                            // Altキー押下処理（画像移動）
                            _econ.MoveImage(ImageCanvas.MoveType.Up);
                        }
                        break;

                    case Keys.Tab:
                        if (IsNotPressCSAKey)
                        {
                            // 通常処理
                            _econ.KeydownFlg = true;
                            e.SuppressKeyPress = true;
                            _econ.IsForward = true;

                            // 単項目ベリファイ
                            if (!VerifyItem(itemid)) { return; }
                            GoNextItem(itemid);
                        }
                        else if (IsPressShiftKey)
                        {
                            // Shiftキー押下処理
                            _econ.KeydownFlg = true;
                            e.SuppressKeyPress = true;
                            _econ.IsForward = false;

                            // 前アイテムに戻る
                            _econ.FocusPreviousControl();
                        }
                        break;

                    case Keys.Up:
                        if (IsNotPressCSAKey)
                        {
                            // 通常処理
                            _econ.KeydownFlg = true;
                            e.SuppressKeyPress = true;
                            _econ.IsForward = false;

                            // 前アイテムに戻る
                            _econ.FocusPreviousControl();
                        }
                        else if (IsPressAltKey)
                        {
                            // Altキー押下処理（画像移動）
                            _econ.MoveImage(ImageCanvas.MoveType.Down);
                        }
                        break;

                    case Keys.Escape:
                        //2022.12.01 SP.harauchi 不具合対応No150
                        //持帰の交換証券種類コード、名称でEscキーを押下して、F12（確定）を行った場合に発生する
                        //_dcon.tbDspItems[itemid].Text = "";
                        break;

                    case Keys.ShiftKey:
                    case Keys.ControlKey:
                        // ChangeFunctionで制御するように統一 
                        //InitializeFunction();
                        if (ChangeFunction(e)) return;
                        break;

                    case Keys.PageUp:
                        //MovePreviousPage();
                        break;

                    case Keys.PageDown:
                        //MoveNextPage();
                        break;

                    case Keys.Right:
                        if (IsPressAltKey)
                        {
                            // Altキー押下処理（画像移動）
                            _econ.MoveImage(ImageCanvas.MoveType.Left);
                        }
                        break;

                    case Keys.Left:
                        if (IsPressAltKey)
                        {
                            // Altキー押下処理（画像移動）
                            _econ.MoveImage(ImageCanvas.MoveType.Right);
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [画面項目]テキスト KeyUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void DspItem_KeyUp(object sender, KeyEventArgs e)
        {
            // 参照モードは処理不要
            if (_ctl.IsDspReadOnly) { return; }
            try
            {
                int itemid = GetTextBoxIndex(sender);

                switch (e.KeyCode)
                {
                    case Keys.ShiftKey:
                    case Keys.ControlKey:
                        // ボタン制御
                        // ChangeFunctionで制御するように統一 
                        //InitializeFunction();
                        if (ChangeFunction(e)) return;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [画面項目]テキスト Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DspItem_Click(object sender, EventArgs e)
        {
            // 参照モードは処理不要
            if (_ctl.IsDspReadOnly) { return; }
            try
            {
                int itemid = GetTextBoxIndex(sender);
                _dcon.tbDspItems[itemid].SelectAll();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [画面項目]テキスト TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DspItem_TextChanged(object sender, EventArgs e)
        {
            // 参照モードは処理不要
            if (_ctl.IsDspReadOnly) { return; }
            try
            {
                if (_econ.IsStopTextChanged) { return; }

                int itemid = GetTextBoxIndex(sender);
                if (!_dcon.tbDspItems.ContainsKey(itemid))
                {
                    return;
                }
                TextBox tb = _dcon.tbDspItems[itemid];

                // 自動入力がONのとき、フル桁入力で次のコントロールへ移る
                if (DBConvert.ToBoolNull(_curMei.CurDsp.dsp_items[itemid].m_AUTO_INPUT) &&
                    tb.Text.Length == tb.MaxLength)
                {
                    if (_itemMgr.DspParams.IsVerifyExec)
                    {
                        if (!_vfyChkFlg)
                        {
                            // 単項目ベリファイ
                            if (!VerifyItem(itemid)) { return; }
                            GoNextItem(itemid);
                        }
                    }
                    else
                    {
                        GoNextItem(itemid);
                    }
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [画面項目]テキスト KePress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DspItem_KePress(object sender, KeyPressEventArgs e)
        {
            // 参照モードは処理不要
            if (_ctl.IsDspReadOnly) { return; }
            try
            {
                int itemid = GetTextBoxIndex(sender);

                // 数字項目の場合、数字及びマイナス以外の入力は許可しない。
                _dcon.InputCheck(_curMei, itemid, e);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [画面項目]テキスト MouseDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DspItem_MouseDown(object sender, MouseEventArgs e)
        {
            // 参照モードは処理不要
            if (_ctl.IsDspReadOnly) { return; }
            try
            {
                // マウスでクリックされたらフォーカス位置を更新
                int itemid = GetTextBoxIndex(sender);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + " 開始 " + itemid, 1);
                _dcon.FocusedItemId = itemid;
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [マウスファンクション押下後] KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFocusDummey_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.ShiftKey:
                    case Keys.ControlKey:
                        // ボタン制御
                        // ChangeFunctionで制御するように統一 
                        //InitializeFunction();
                        if (ChangeFunction(e)) return;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// [マウスファンクション押下後] KeyUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFocusDummey_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.ShiftKey:
                    case Keys.ControlKey:
                        // ボタン制御
                        // ChangeFunctionで制御するように統一 
                        //InitializeFunction();
                        if (ChangeFunction(e)) return;
                        break;
                    default:
                        break;
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
        // イベント（ファンクションキー）
        // *******************************************************************

        /// <summary>
        /// F1：終了
        /// </summary>
        protected override void btnFunc01_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了", 1);

                if (_ctl.HoseiInputMode == HoseiStatus.HoseiInputMode.自行情報 && _ctl.IsKanryouTeisei &&
                    _itemMgr.EntParams.IsChangeDsp)
                {
                    // 自行の完了訂正で証券種類変更あり
                    ComMessageMgr.MessageWarning("証券種類の変更を行った場合は、確定または保留で終了する\n必要があります。");
                    return;
                }

                // 終了処理
                _itemMgr.EntParams.ExecFunc = FuncType.終了;

                // 明細ステータス更新
                _ctl.TerminateEntryStatus(_curMei, true);

                // バッチ処理終了
                _econ.SetBatchEnd();

                // 画面終了
                RefreshAll();
                CloseForm(DataType.End);
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
        /// F2：拡大
        /// </summary>
        protected override void btnFunc02_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "拡大", 1);

                if (!eiHandler.HasImage) { return; }

                // 拡大
                _econ.SizeChangeImage(Const.IMAGE_ZOOM_IN, _dcon.FocusedItemId);

                // 認識領域の描画
                _itemMgr.ImageHandler.SetImageRegion(_dcon.FocusedItemId);
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
        /// F3：縮小
        /// </summary>
        protected override void btnFunc03_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "縮小", 1);

                if (!eiHandler.HasImage) { return; }

                // 縮小
                _econ.SizeChangeImage(Const.IMAGE_ZOOM_OUT, _dcon.FocusedItemId);

                // 認識領域の描画
                _itemMgr.ImageHandler.SetImageRegion(_dcon.FocusedItemId);
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
        /// F4：イメージ切替
        /// </summary>
        protected override void btnFunc04_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "イメージ切替", 1);

                // 次のイメージを表示する
                _curMei.SetNextImageSeq();
                lblImgKbnName.Text = TrMeiImg.ImgKbn.GetName(_curMei.CurImg._IMG_KBN);

                //入力中項目情報の保存
                _dcon.SaveInputData();

                // 画面終了
                RefreshImage();
                CloseForm(DataType.Cur);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                //入力中項目情報のクリア
                _dcon.ClearInputData();
                InitializeFunction();
            }
        }

        /// <summary>
        /// F7：左表示回転
        /// </summary>
        protected override void btnFunc07_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "左表示回転", 1);

                if (!eiHandler.HasImage) { return; }

                // 左回転
                _econ.RotateImage(1);

                // 認識領域の描画
                _itemMgr.ImageHandler.SetImageRegion(_dcon.FocusedItemId);
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
        /// F8：右表示回転
        /// </summary>
        protected override void btnFunc08_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "右表示回転", 1);

                if (!eiHandler.HasImage) { return; }

                // 右回転
                _econ.RotateImage(0);

                // 認識領域の描画
                _itemMgr.ImageHandler.SetImageRegion(_dcon.FocusedItemId);
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
        /// F9：画面種類選択
        /// </summary>
        protected override void btnFunc09_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "画面種類選択", 1);

                // 画面種類選択画面
                SelectDspForm form = new SelectDspForm();
                form.InitializeForm(_ctl);
                DialogResult res = form.ShowDialog();
                if (res != DialogResult.OK)
                {
                    return;
                }
                if (form.DspId == _curMei._DSP_ID)
                {
                    return;
                }

                // フォーカスを外してLostFocus/Leaveイベントを発生させる
                // (この方法だとValidatingイベントは発生しないよう)
                this.ActiveControl = null;
                //入力中項目情報の保存(フォーカス情報は保存なし)
                _dcon.SaveInputData(false);

                // 画面切替
                if (!_econ.ChangeDsp(_curMei, form.DspId, form.BillCode, form.BillName))
                {
                    return;
                }

                // 切り替え項目削除
                _dcon.DeleteInputData(DspItem.ItemId.交換証券種類コード);
                _dcon.DeleteInputData(DspItem.ItemId.交換証券種類名);

                // 画面終了（画面切替）
                RefreshControl();
                CloseForm(DataType.Cur);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
            finally
            {
                //入力中項目情報のクリア
                _dcon.ClearInputData();
                InitializeFunction();
            }
        }

        /// <summary>
        /// F10：検索
        /// </summary>
        protected override void btnFunc10_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "検索", 1);

                // 検索種別判定
                TBL_DSP_ITEM di = _econ.GetDispItem(_dcon.FocusedItemId);
                TBL_DSP_ITEM diSub = null;
                if (di == null) { return; }
                SearchDialog.SearchType type;
                string searchKey = string.Empty;
                switch (di._ITEM_ID)
                {
                    case DspItem.ItemId.券面持帰銀行コード:
                        type = SearchDialog.SearchType.Bank;
                        diSub = _econ.GetDispItem(DspItem.ItemId.持帰銀行コード);
                        break;
                    case DspItem.ItemId.券面持帰支店コード:
                        type = SearchDialog.SearchType.Branch;
                        break;
                    case DspItem.ItemId.券面口座番号:
                        type = SearchDialog.SearchType.Kouza;

                        //支店コードを取得
                        TextBox tb = _econ.GetTextBox(DspItem.ItemId.券面持帰支店コード);
                        if (tb != null)
                        {
                            searchKey = _dcon.DelCommnaItemType(di, tb.Text);
                        }
                        break;
                    default:
                        return;
                }

                // 検索画面
                SearchDialog form = new SearchDialog(type, searchKey);
                form.InitializeForm(_ctl);
                DialogResult res = form.ShowDialog();
                if (res != DialogResult.OK)
                {
                    return;
                }

                TextBox tb1, tb2, tb3;
                switch (di._ITEM_ID)
                {
                    case DspItem.ItemId.券面持帰銀行コード:
                        tb1 = _econ.GetTextBox(DspItem.ItemId.券面持帰銀行コード);
                        tb2 = _econ.GetTextBox(DspItem.ItemId.持帰銀行コード);
                        tb3 = _econ.GetTextBox(DspItem.ItemId.持帰銀行名);
                        // DspItemに定義の長さで設定
                        tb1.Text = CommonUtil.PadLeft(form.ResultCd.ToString(), di.m_ITEM_LEN, "0");
                        tb2.Text = CommonUtil.PadLeft(form.ResultCd.ToString(), diSub.m_ITEM_LEN, "0");
                        tb3.Text = form.ResultName;
                        SetTextFocus(tb1);
                        break;
                    case DspItem.ItemId.券面持帰支店コード:
                        tb1 = _econ.GetTextBox(DspItem.ItemId.券面持帰支店コード);
                        // DspItemに定義の長さで設定
                        tb1.Text = CommonUtil.PadLeft(form.ResultCd.ToString(), di.m_ITEM_LEN, "0");
                        SetTextFocus(tb1);
                        break;
                    case DspItem.ItemId.券面口座番号:
                        tb1 = _econ.GetTextBox(DspItem.ItemId.券面口座番号);
                        // DspItemに定義の長さで設定
                        tb1.Text = CommonUtil.PadLeft(form.ResultCd.ToString(), di.m_ITEM_LEN, "0");
                        SetTextFocus(tb1);
                        break;
                    default:
                        return;
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
        /// F11：保留
        /// </summary>
        protected override void btnFunc11_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "保留", 1);

                // 保留は入力チェックせずにステータスのみ保留にする
                if (!MeisaiFix(FuncType.保留))
                {
                    return;
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
        /// F12：確定
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "確定", 1);

                // 最終項目は Validate イベント発生しないので自分で実施する
                if (_dcon.FocusedItemId == _econ.GetLastItemId())
                {
                    // 入力検証
                    if (!ItemValidate(_dcon.FocusedItemId)) { return; }

                    // 単項目ベリファイ
                    if (!VerifyItem(_dcon.FocusedItemId)) { return; }
                }
                else
                {
                    // 最終項目以外の場合はフォーカスが外れたときと同じ処理を実行（サブルーチン：入力項目読替）
                    // 読替を行うため
                    eiChecker.DspItemLeave(_dcon.FocusedItemId);
                }

                // 確定処理
                if (!MeisaiFix(FuncType.確定))
                {
                    return;
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

        /// <summary>
        /// 画面表示
        /// </summary>
        protected virtual void Form_Shown()
        {
            _isInitedShown = false;

            // 初期フォーカス
            if (!_ctl.IsDspReadOnly)
            {
                // 入力中のフォーカス情報があればそちらを優先
                if (!_dcon.GetInputFocusItemId(out int FocusedId))
                {
                    // フォーカス情報がない場合は先頭項目
                    FocusedId = _econ.GetFirstItemId();
                }
                _dcon.FocusedItemId = FocusedId;

                // 数字項目はフォーカスが得た際にカンマを外す
                _dcon.DelComma(_curMei, _dcon.FocusedItemId);

                _econ.FocusNextControl(_dcon.FocusedItemId);
                TextBox tb = _econ.GetTextBox(_dcon.FocusedItemId);
                SetFocusBackColor(tb);
            }

            // 認識領域の描画
            _itemMgr.ImageHandler.SetImageRegion(_dcon.FocusedItemId);

            _isInitedShown = true;
            _isFormClosing = false;
        }

        /// <summary>
        /// DspItemのTextBoxの特定
        /// DspItemで該当なしは-1を返す
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        protected int GetTextBoxIndex(object sender)
        {
            if (sender is TextBox)
            {
                TextBox txtSender = (TextBox)sender;
                var tbs = _dcon.tbDspItems.Where(p => p.Value.Name.Equals(txtSender.Name));
                if (tbs.Count() > 0)
                {
                    return tbs.First().Key;
                }
            }
            return -1;
        }

        /// <summary>
        /// 単項目ベリファイ
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        protected bool VerifyItem(int itemid)
        {
            if (itemid < 0) { return false; }

            // ベリファイエントリーの場合はベリファイチェックを行う。
            if (_itemMgr.DspParams.IsVerifyExec)
            {
                // 入力検証
                if (!ItemValidate(_dcon.FocusedItemId)) { return false; }

                _vfyChkFlg = true;
                if (!eiChecker.VeifyCheck(_curMei, itemid))
                {
                    _econ.SetItemFocus(itemid);
                    _vfyChkFlg = false;
                    return false;
                }
                _vfyChkFlg = false;
            }
            return true;
        }

        /// <summary>
        /// 全項目ベリファイ
        /// </summary>
        /// <returns></returns>
        protected bool VerfyItemAll()
        {
            // ベリファイエントリーの場合はベリファイチェックを行う。
            if (_itemMgr.DspParams.IsVerifyExec)
            {
                _vfyChkFlg = true;
                foreach (int itemid in _dcon.tbDspItems.Keys)
                {
                    if (!eiChecker.VeifyCheck(_curMei, itemid, true))
                    {
                        _econ.SetItemFocus(itemid);
                        _vfyChkFlg = false;
                        return false;
                    }
                }
                _vfyChkFlg = false;
            }
            return true;
        }

        /// <summary>
        /// 入力検証
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns>true:検証OK、false:検証NG</returns>
        protected bool ItemValidate(int itemid)
        {
            if (_econ.IsValidValue(itemid))
            {
                // 検証OK
                TextBox tb = _econ.GetTextBox(itemid);
                SetNormalColor(tb);
                return true;
            }
            // 検証NG
            return false;
        }

        /// <summary>
        /// 背景色を通常色を設定する
        /// </summary>
        /// <param name="tb"></param>
        protected void SetNormalColor(TextBox tb)
        {
            if (tb == null) { return; }

            // 読取専用は背景色を変更しない
            if (tb.ReadOnly) { return; }

            // Yellow だったら、ベリファイ相違なので背景色を変更しない
            if (tb.BackColor == Color.Yellow) { return; }

            tb.BackColor = SystemColors.Window;
        }

        /// <summary>
        /// 次の項目に移動する
        /// </summary>
        /// <param name="curItemid"></param>
        protected virtual void GoNextItem(int curItemid)
        {
            int lastItemId = _econ.GetLastItemId();
            if ((lastItemId != curItemid))
            {
                // 次の項目にフォーカスする
                _econ.FocusForwardControl();
            }
            else
            {
                // 最終項目は Validate イベント発生しないので自分で実施する
                if (!ItemValidate(lastItemId)) { return; }

                // 最後のテキストボックスは確定処理
                MeisaiFix(FuncType.確定);
            }
        }

        /// <summary>
        /// 明細確定処理
        /// </summary>
        protected virtual bool MeisaiFix(FuncType type)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "明細確定", 1);

            _itemMgr.EntParams.ExecFunc = type;
            if (type == FuncType.確定)
            {

                // 全項目入力チェック
                if (!_econ.IsAllValidValue())
                {
                    return false;
                }

                // 全項目ベリファイ
                if (!VerfyItemAll())
                {
                    return false;
                }

                // DB更新
                if (!edUpdater.UpdateTransation(_curMei))
                {
                    return false;
                }

                //// 更新したので再取得
                //_itemMgr.SetCurrentTrdataItem();
            }

            // 明細終了
            if (!_econ.GoNextDetail(_curMei, ProcType.明細確定))
            {
                return false;
            }

            // 画面終了
            RefreshImage();
            CloseForm(DataType.Next);
            return true;
        }

        /// <summary>
        /// フォーム終了
        /// </summary>
        protected virtual void CloseForm(EntryFormBase.DataType type)
        {
            _isInitedShown = false;
            _isFormClosing = true;

            // 明細一覧に戻る
            if (type != DataType.Cur)
            {
                if (!_itemMgr.DspParams.IsAutoReceiveBatch ||
                    _itemMgr.EntParams.IsBatchEnd)
                {
                    // 自動配信しない場合は無条件にバッチ終了
                    // DataType.Next でもバッチ終了と判定された場合はバッチ終了
                    type = DataType.End;
                }
            }

            // 画面切替時は次明細を取得せずに再表示する
            MeisaiInfo meiNext = _curMei;
            if (type == DataType.Next)
            {
                // 次明細取得
                int curDspId = _curMei._DSP_ID;
                meiNext = GetAutoReceiveBatch();
                if (meiNext == null) { return; }

                // 画面IDが変わったらイメージ＆コントロール再描画
                if (curDspId != meiNext._DSP_ID)
                {
                    RefreshAll();
                }
            }
            else if (type == DataType.End)
            {
                // 明細なし or 保留 or 終了
                this.Close();
                return;
            }
            else // if (type == DataType.Cur)
            {
                // 現在の明細を再表示
            }

            // コントロール削除
            RemoveControls();
            RemoveImgControl();

            // テキストボックスクリア
            _dcon.ClearDspItems();

            // 明細あり
            // 次明細表示
            if (!ExecControl(meiNext, type))
            {
                // 表示明細なし
                return;
            }
            // 画面初期処理(現在の明細を再表示 や 自動配信で次の明細を表示するケースで必要)
            Form_Shown();
        }

        /// <summary>
        /// 次の明細を取得する
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 画面IDが変わった場合、後続でイメージ＆コントロール再描画処理が必要
        /// RefreshAll() / RemoveControls() / RemoveImgControl() / _dcon.ClearDspItems()
        /// </remarks>
        private MeisaiInfo GetAutoReceiveBatch()
        {
            MeisaiInfo meiNext = null;

            //  明細取得（自動配信）
            TBL_HOSEI_STATUS sts = _ctl.GetAutoReceiveBatch(_itemMgr.DspParams.ExecMode, _ctl.GymId);
            if (sts == null)
            {
                // 明細なし
                this.Close();
                return null;
            }

            // エントリデータ取得
            _itemMgr.DspParams.PrevBatKey = CommonUtil.GenerateKey(_curMei._GYM_ID, _curMei._OPERATION_DATE, _curMei._SCAN_TERM, _curMei._BAT_ID);
            _itemMgr.DspParams.PrevMeiKey = CommonUtil.GenerateKey(_curMei._GYM_ID, _curMei._OPERATION_DATE, _curMei._SCAN_TERM, _curMei._BAT_ID, _curMei._DETAILS_NO);
            meiNext = _ctl.GetNextEntryData(sts);
            if (meiNext == null)
            {
                // 明細なし
                this.Close();
                return null;
            }

            // 明細あり
            return meiNext;
        }

        /// <summary>
        /// イメージそのままでコントロール再描画する
        /// </summary>
        private void RefreshControl()
        {
            _itemMgr.EntParams.IsInitControl = false;
            _itemMgr.EntParams.IsInitImage = true;
            _itemMgr.EntParams.IsDeleteImage = false;
        }

        /// <summary>
        /// コントロールはそのままでイメージ再描画する
        /// </summary>
        private void RefreshImage()
        {
            _itemMgr.EntParams.IsInitControl = true;
            _itemMgr.EntParams.IsInitImage = false;
            _itemMgr.EntParams.IsDeleteImage = true;
        }

        /// <summary>
        /// すべて再描画する
        /// </summary>
        private void RefreshAll()
        {
            _itemMgr.EntParams.IsInitControl = false;
            _itemMgr.EntParams.IsInitImage = false;
            _itemMgr.EntParams.IsDeleteImage = true;
        }

    }
}
