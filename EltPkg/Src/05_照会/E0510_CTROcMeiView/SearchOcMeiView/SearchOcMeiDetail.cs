using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SearchOcMeiView
{
    /// <summary>
    /// 明細画面
    /// </summary>
    public partial class SearchOcMeiDetail : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private ImageHandler _imgHandler = null;
        private ItemManager.ImageInfo _curImage = null;

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
        /// 対象明細データ
        /// </summary>
        public ItemManager.DetailData CurDetail
        {
            get 
            {
                return _itemMgr.MeiDetails[_itemMgr.DetailParams.Key]; 
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SearchOcMeiDetail()
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
            base.SetDispName1("交換持出");
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("持出明細照会");
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
                SetFunctionName(F1_, "一覧");
                SetFunctionName(F2_, "印刷");
                SetFunctionName(F3_, "イメージ\n 切出", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F4_, "イメージ\n 差替/追加", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F5_, "拡大");
                SetFunctionName(F6_, "縮小");
                SetFunctionName(F7_, "左表示\n回転", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F8_, "右表示\n回転", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F9_, string.Empty);
                SetFunctionName(F10_, "前明細", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F11_, "次明細", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F12_, "訂正");
            }
            else if (IsPressShiftKey)
            {
                // Shiftキー押下
                SetFunctionName(SF1_, string.Empty);
                SetFunctionName(SF2_, "PDF出力", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(SF3_, string.Empty);
                SetFunctionName(SF4_, "イメージ\n 削除", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(SF5_, string.Empty);
                SetFunctionName(SF6_, string.Empty);
                SetFunctionName(SF7_, string.Empty);
                SetFunctionName(SF8_, string.Empty);
                if (CurDetail.BCASts == TrMei.Sts.作成対象)
                {
                    // 持出取消が作成
                    SetFunctionName(SF9_, "取消\n ｷｬﾝｾﾙ", true, Const.FONT_SIZE_FUNC_LOW);
                }
                else
                {
                    // 上記以外
                    SetFunctionName(SF9_, "明細削除", true, Const.FONT_SIZE_FUNC_LOW);
                }
                SetFunctionName(SF10_, string.Empty);
                SetFunctionName(SF11_, string.Empty);
                SetFunctionName(SF12_, string.Empty);
            }
            else if (IsPressCtrlKey)
            {
                // Ctrlキー押下
                SetFunctionName(CF1_, string.Empty);
                SetFunctionName(CF2_, string.Empty);
                SetFunctionName(CF3_, string.Empty);
                SetFunctionName(CF4_, string.Empty);
                SetFunctionName(CF5_, string.Empty);
                SetFunctionName(CF6_, string.Empty);
                SetFunctionName(CF7_, string.Empty);
                SetFunctionName(CF8_, string.Empty);
                if (CurDetail.MeiDelete == 1)
                {
                    // 削除データ
                    SetFunctionName(CF9_, "強制削除復活", true, Const.FONT_SIZE_FUNC_LOW);
                }
                else
                {
                    // 未削除
                    SetFunctionName(CF9_, "強制削除", true, Const.FONT_SIZE_FUNC_LOW);
                }
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
            if (CurDetail.MeiDelete == 1)
            {
                // 削除データの場合
                if (IsNotPressCSAKey)
                {
                    // 通常状態
                    SetFunctionState(F2_, false);
                    SetFunctionState(F3_, false);
                    SetFunctionState(F4_, false);
                    SetFunctionState(F12_, false);
                }
                else if (IsPressShiftKey)
                {
                    // Shiftキー押下
                    SetFunctionState(SF2_, false);
                    SetFunctionState(SF4_, false);
                    if (CurDetail.BCASts != TrMei.Sts.作成対象)
                    {
                        // 持出取消が作成(取消ｷｬﾝｾﾙ)以外
                        // 明細削除 & 持出取消予約中のケースはキャンセルできても問題ないため、使用可とする
                        SetFunctionState(SF9_, false);
                    }
                }
            }
            else if (CurDetail.BCASts >= TrMei.Sts.作成対象)
            {
                // 持出取消が作成以上
                if (IsNotPressCSAKey)
                {
                    // 通常状態
                    SetFunctionState(F3_, false);
                    SetFunctionState(F4_, false);
                }
                else if (IsPressShiftKey)
                {
                    // Shiftキー押下
                    SetFunctionState(SF4_, false);
                    if (CurDetail.BCASts != TrMei.Sts.作成対象)
                    {
                        // 持出取消が作成(取消ｷｬﾝｾﾙ)以外
                        SetFunctionState(SF9_, false);
                    }
                }
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
            // ファンクションキー状態を設定
            SetFunctionState();
            SetTabControl(lblImg1, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表]);
            SetTabControl(lblImg2, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.裏]);
            SetTabControl(lblImg3, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.補箋]);
            SetTabControl(lblImg4, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.付箋]);
            SetTabControl(lblImg5, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.入金証明]);
            SetTabControl(lblImg6, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表再送分]);
            SetTabControl(lblImg7, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.裏再送分]);
            SetTabControl(lblImg8, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.その他1]);
            SetTabControl(lblImg9, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.その他2]);
            SetTabControl(lblImg10, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.その他3]);
            SetTabControl(lblImg11, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.予備1]);
            SetTabControl(lblImg12, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.予備2]);
            SetTabControl(lblImg13, _itemMgr.ImageInfos[TrMeiImg.ImgKbn.予備3]);
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
            SetDisplayParams(true);
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected void SetDisplayParams(bool isImageRefresh)
        {
            // イメージ取得
            _itemMgr.Fetch_meiimges();

            //取込日
            lblreaddate.Text = OcMeiListCommon.DispDate(CurDetail.OpeDate, "");
            //バッチ番号
            lblbatchno.Text = CurDetail.BatID.ToString("D6");
            //明細番号
            lblDetailNo.Text = CurDetail.DetailNo.ToString("D6");
            //交換希望日
            lblclearingdate.Text = OcMeiListCommon.DispDate(CurDetail.ClearingDate, "");
            //スキャン支店コード
            lblscancode.Text = CurDetail.ScanBRNo.ToString("D4");
            //スキャン支店名
            lblscanname.Text = _itemMgr.GeScanBranch(CurDetail.ScanBRNo);
            //スキャン日
            lblscandate.Text = OcMeiListCommon.DispDate(CurDetail.ScanDate, "");
            //持出支店コード
            lblbrno.Text = CurDetail.OCBRNo.ToString("D4");
            //持出支店名
            lblbrname.Text = _itemMgr.GeBranch(CurDetail.OCBRNo);
            //金額
            lblamount.Text = OcMeiListCommon.DispFormat(CurDetail.Amount, "#,##0");
            //持帰銀行コード
            lblbankcode.Text = CurDetail.ICBKNo;
            //持帰銀行名
            lblbkname.Text = _itemMgr.GetBank(CurDetail.ICBKNo);
            //決済対象区分
            lblGXA.Text = CurDetail.PayKbn;
            //エントリー状態
            lblentstatus.Text = OcMeiListCommon.DispEntryStsFormat(CurDetail);
            //持帰銀行オペレーター
            lblicbkoperate.Text = OcMeiListCommon.DispOperatorFormat(CurDetail.ICBKOpeNo, _itemMgr);
            //金額オペレーター
            lblamtoperate.Text = OcMeiListCommon.DispOperatorFormat(CurDetail.AmountOpeNo, _itemMgr);
            // アップロード状況
            lblupload.Text = TrMei.Sts.GetName(CurDetail.ImgUploadSts);
            //二重持出日
            lblbua.Text = OcMeiListCommon.DispDate(CurDetail.BUADate, "警告なし");
            //持帰訂正日
            lblgma.Text = OcMeiListCommon.DispDate(CurDetail.GMADate, "訂正なし");
            //決済後訂正日
            lblgxadate.Text = OcMeiListCommon.DispDate(CurDetail.GXADate, "訂正なし");
            //メモ
            txtmemo.Text = CurDetail.Memo;
            //削除状態
            lblDelete.Text = OcMeiListCommon.DispDeleteFlgFormat(CurDetail);
            //持出取消結果
            lblbcasts.Text = TrMei.Sts.GetNameOCCancel(CurDetail.BCASts);
            //不渡返還日
            lblGRADate.Text = OcMeiListCommon.DispDate(CurDetail.GRADate, "なし");
            //イメージファイル名(表名）
            string FLNM = string.Empty;
            if (_itemMgr.ImageInfos[TrMeiImg.ImgKbn.表].HasImage)
            {
                FLNM = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表].MeiImage.m_IMG_FLNM;
            }
            lblfilename.Text = FLNM;

            // イメージ描画
            if (isImageRefresh)
            {
                if (_curImage != null)
                {
                    // 以前の設定があり
                    if (!_itemMgr.ImageInfos[_curImage.ImgKbn].HasImage || _curImage.ImgKbn == TrMeiImg.ImgKbn.表)
                    {
                        // 対象イメージがない OR 表イメージ場合、Nullを設定して初期表示と同じ処理を行う
                        _curImage = null;
                    }
                }

                if (_curImage == null)
                {
                    //初期表示の場合
                    _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表];
                    if (_itemMgr.ImageInfos[TrMeiImg.ImgKbn.表再送分].HasImage)
                    {
                        //表再送分があれば優先
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表再送分];
                    }
                }
                else
                {
                    // 以前の設定優先
                    int kbn = _curImage.ImgKbn;
                    _curImage = _itemMgr.ImageInfos[kbn];
                }
                MakeView(_curImage);
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
        /// [フォーム] ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// [画面項目] KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void root_KeyDown(object sender, KeyEventArgs e)
        {
            //KeyDownClearStatusMessage(e);
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
        /// [タブ]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tab_MouseClick(object sender, MouseEventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                switch (((Control)sender).Name)
                {
                    case "lblImg1":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表];
                        break;
                    case "lblImg2":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.裏];
                        break;
                    case "lblImg3":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.補箋];
                        break;
                    case "lblImg4":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.付箋];
                        break;
                    case "lblImg5":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.入金証明];
                        break;
                    case "lblImg6":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表再送分];
                        break;
                    case "lblImg7":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.裏再送分];
                        break;
                    case "lblImg8":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.その他1];
                        break;
                    case "lblImg9":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.その他2];
                        break;
                    case "lblImg10":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.その他3];
                        break;
                    case "lblImg11":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.予備1];
                        break;
                    case "lblImg12":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.予備2];
                        break;
                    case "lblImg13":
                        _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.予備3];
                        break;
                }

                // 画面コントロール描画
                MakeView(_curImage);

                // 画面表示状態更新
                RefreshDisplayState();
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
        /// F02：印刷
        /// SF02：PDF出力
        /// </summary>
        protected override void btnFunc02_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                if (IsNotPressCSAKey)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "印刷", 1);
                    // 印刷
                    if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "表示している明細を印刷しますが、よろしいですか？") == DialogResult.No)
                    {
                        return;
                    }

                    //対象データを明細イメージリストに登録
                    if (!_itemMgr.InsertWKImgList(_itemMgr.DetailParams.Key, _itemMgr.MeiDetails[_itemMgr.DetailParams.Key], this))
                    {
                        return;
                    }

                    try
                    {
                        //メッセージ設定
                        Processing(CommonClass.ComMessageMgr.I00003);

                        //印刷処理実施
                        string Argument = string.Format("{0} {1}", GymParam.GymId.持出, 1);
                        if (!_itemMgr.RunProcess("CTRMeiList.exe", Argument, this))
                        {
                            ComMessageMgr.MessageWarning("印刷処理に失敗しました");
                            return;
                        }
                    }
                    finally
                    {
                        //メッセージ初期化
                        EndProcessing(CommonClass.ComMessageMgr.I00003);
                    }

                    ComMessageMgr.MessageInformation("印刷処理が終了しました");
                }
                else if (IsPressShiftKey)
                {
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "PDF出力", 1);
                    // PDF出力
                    if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "表示している明細をPDF出力しますが、よろしいですか？") == DialogResult.No)
                    {
                        return;
                    }

                    //対象データを明細イメージリストに登録
                    if (!_itemMgr.InsertWKImgList(_itemMgr.DetailParams.Key, _itemMgr.MeiDetails[_itemMgr.DetailParams.Key], this))
                    {
                        return;
                    }

                    try
                    {
                        //メッセージ設定
                        Processing(CommonClass.ComMessageMgr.I00004);

                        //PDF印刷処理実施
                        string Argument = string.Format("{0} {1}", GymParam.GymId.持出, 2);
                        if (!_itemMgr.RunProcess("CTRMeiList.exe", Argument, this))
                        {
                            ComMessageMgr.MessageWarning("PDF出力処理に失敗しました");
                            return;
                        }
                    }
                    finally
                    {
                        //メッセージ初期化
                        EndProcessing(CommonClass.ComMessageMgr.I00004);
                    }

                    ComMessageMgr.MessageInformation("PDF出力処理が終了しました");
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
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F03：イメージ切出
        /// </summary>
        protected override void btnFunc03_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "イメージ切出", 1);
                if (!(_imgHandler.HasImage && _curImage.HasImage))
                {
                    ComMessageMgr.MessageWarning("切出イメージがありません");
                    return;
                }

                //イメージ編集チェック
                _itemMgr.getTRMei(CurDetail, out TBL_TRMEI mei, this);
                // 編集フラグ確認
                if (mei.m_EDIT_FLG == 1)
                {
                    ComMessageMgr.MessageWarning("他オペレータが編集中のため、イメージは切出できません");
                    return;
                }

                // 持出取消アップロード状態確認
                if (mei.m_BCA_STS >= TrMei.Sts.作成対象)
                {
                    ComMessageMgr.MessageWarning("持出取消済の明細イメージは切出できません");
                    return;
                }
                //int[] ErrSts = { TrMei.Sts.ファイル作成, TrMei.Sts.アップロード };
                //if (ErrSts.Contains(mei.m_BCA_STS))
                //{
                //    ComMessageMgr.MessageWarning("持出取消アップロード中の明細のイメージは切出できません");
                //    return;
                //}

                // ステータス確認
                int[] ChkErrSts = { TrMei.Sts.ファイル作成, TrMei.Sts.アップロード };
                if (!_itemMgr.ChkTRMeiImgUploadSts(ref _curImage, ChkErrSts, this))
                {
                    ComMessageMgr.MessageWarning("持出アップロード中のイメージは切出できません");
                    return;
                }
                if (_curImage.MeiImage.m_BUA_STS == TrMei.Sts.結果正常)
                {
                    switch (_curImage.MeiImage._IMG_KBN)
                    {
                        case TrMeiImg.ImgKbn.表:
                        case TrMeiImg.ImgKbn.裏:
                        case TrMeiImg.ImgKbn.表再送分:
                        case TrMeiImg.ImgKbn.裏再送分:
                            ComMessageMgr.MessageWarning("表・裏の持出アップロード済のイメージは切出できません");
                            return;
                    }
                }

                //補正中チェック
                if (!_itemMgr.ChkEntryHoseiSts(CurDetail, this))
                {
                    ComMessageMgr.MessageWarning("他オペレータで訂正中のため、イメージ切出できません");
                    return;
                }

                // イメージ切出処理実施
                ImageCut();
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
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F04：イメージ差替/追加
        /// SF04：イメージ削除
        /// </summary>
        protected override void btnFunc04_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                if (IsNotPressCSAKey)
                {
                    // イメージ差替/追加
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "イメージ差替/追加", 1);
                    //イメージ編集チェック
                    _itemMgr.getTRMei(CurDetail, out TBL_TRMEI mei, this);
                    // 編集フラグ確認
                    if (mei.m_EDIT_FLG == 1)
                    {
                        ComMessageMgr.MessageWarning("他オペレータが編集中のため、イメージは差替/追加できません");
                        return;
                    }
                    // 持出取消アップロード状態確認
                    if (mei.m_BCA_STS >= TrMei.Sts.作成対象)
                    {
                        ComMessageMgr.MessageWarning("持出取消済の明細イメージは差替/追加できません");
                        return;
                    }
                    //int[] ErrSts = { TrMei.Sts.ファイル作成, TrMei.Sts.アップロード };
                    //if (ErrSts.Contains(mei.m_BCA_STS))
                    //{
                    //    ComMessageMgr.MessageWarning("持出取消アップロード中の明細のイメージは差替/追加できません");
                    //    return;
                    //}

                    if (_curImage.HasImage)
                    {
                        // 対象イメージが存在している場合

                        // ステータス確認
                        int[] ChkErrSts = { TrMei.Sts.ファイル作成, TrMei.Sts.アップロード };
                        if (!_itemMgr.ChkTRMeiImgUploadSts(ref _curImage, ChkErrSts, this))
                        {
                            ComMessageMgr.MessageWarning("持出アップロード中のイメージは差替/追加できません");
                            return;
                        }

                        if (_curImage.MeiImage.m_BUA_STS == TrMei.Sts.結果正常)
                        {
                            switch (_curImage.MeiImage._IMG_KBN)
                            {
                                case TrMeiImg.ImgKbn.表:
                                case TrMeiImg.ImgKbn.裏:
                                    ComMessageMgr.MessageWarning("表・裏の持出アップロード済のイメージは差替/追加できません");
                                    return;
                            }
                        }
                    }

                    //補正中チェック
                    if (!_itemMgr.ChkEntryHoseiSts(CurDetail, this))
                    {
                        ComMessageMgr.MessageWarning("他オペレータで訂正中のため、イメージは差替/追加できません");
                        return;
                    }

                    // イメージ差替/追加
                    ImageReplace();

                }
                else if (IsPressShiftKey)
                {
                    // イメージ削除
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), "イメージ削除", 1);
                    if (!_curImage.HasImage)
                    {
                        // 対象イメージの設定がない場合
                        ComMessageMgr.MessageWarning("削除イメージがありません");
                        return;
                    }

                    //イメージ編集チェック
                    _itemMgr.getTRMei(CurDetail, out TBL_TRMEI mei, this);
                    // 編集フラグ確認
                    if (mei.m_EDIT_FLG == 1)
                    {
                        ComMessageMgr.MessageWarning("他オペレータが編集中のため、イメージは削除できません");
                        return;
                    }
                    // 持出取消アップロード状態確認
                    if (mei.m_BCA_STS >= TrMei.Sts.作成対象)
                    {
                        ComMessageMgr.MessageWarning("持出取消済の明細イメージは削除できません");
                        return;
                    }
                    //int[] ErrSts = { TrMei.Sts.ファイル作成, TrMei.Sts.アップロード };
                    //if (ErrSts.Contains(mei.m_BCA_STS))
                    //{
                    //    ComMessageMgr.MessageWarning("持出取消アップロード中の明細のイメージは削除できません");
                    //    return;
                    //}

                    // ステータス確認
                    int[] ChkErrSts = { TrMei.Sts.ファイル作成, TrMei.Sts.アップロード, TrMei.Sts.結果正常 };
                    if (!_itemMgr.ChkTRMeiImgUploadSts(ref _curImage, ChkErrSts, this))
                    {
                        ComMessageMgr.MessageWarning("持出アップロード中・済のイメージは削除できません");
                        return;
                    }

                    // 区分チェック
                    switch (_curImage.MeiImage._IMG_KBN)
                    {
                        case TrMeiImg.ImgKbn.表:
                        case TrMeiImg.ImgKbn.裏:
                            ComMessageMgr.MessageWarning("表・裏のイメージは削除できません");
                            return;
                    }

                    //補正中チェック
                    if (!_itemMgr.ChkEntryHoseiSts(CurDetail, this))
                    {
                        ComMessageMgr.MessageWarning("他オペレータで訂正中のため、イメージは削除できません");
                        return;
                    }

                    // 確認メッセージ
                    if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "イメージを削除します。よろしいですか？") == DialogResult.No)
                    {
                        return;
                    }

                    // 削除処理
                    ImageDelete();
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
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F05：拡大
        /// </summary>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (!(_imgHandler.HasImage && _curImage.HasImage))
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "拡大", 1);

                // 拡大
                _imgHandler.SizeChangeImage(Const.IMAGE_ZOOM_IN, pnlImage.Width, pnlImage.Height);
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
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F06：縮小
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (!(_imgHandler.HasImage && _curImage.HasImage))
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "縮小", 1);

                // 縮小
                _imgHandler.SizeChangeImage(Const.IMAGE_ZOOM_OUT, pnlImage.Width, pnlImage.Height);
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
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F07：回転（左回り）
        /// </summary>
        protected override void btnFunc07_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (!(_imgHandler.HasImage && _curImage.HasImage))
                {
                    return;
                }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "回転（左回り）", 1);

                // 左回転
                _imgHandler.RotateImage(1);
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
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F08：回転（右回り）
        /// </summary>
        protected override void btnFunc08_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (!(_imgHandler.HasImage && _curImage.HasImage))
                {
                    return;
                }

                // 回転（右回り）
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "回転（右回り）", 1);

                // 右回転
                _imgHandler.RotateImage(0);
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
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// SF09：明細削除/取消キャンセル
        /// CF09：強制削除/強制削除復活
        /// </summary>
        protected override void btnFunc09_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (IsPressShiftKey)
                {
                    // Shiftキー押下

                    if (CurDetail.BCASts == TrMei.Sts.作成対象)
                    {
                        // 取消キャンセル(持出取消が作成)

                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), "取消キャンセル", 1);
                        //イメージ編集チェック
                        _itemMgr.getTRMei(CurDetail, out TBL_TRMEI mei, this);
                        // 編集フラグ確認
                        if (mei.m_EDIT_FLG == 1)
                        {
                            ComMessageMgr.MessageWarning("他オペレータが編集中のため、対象証券の持出取消登録キャンセルは\n行えません");
                            return;
                        }
                        // 持出取消アップロード状態確認
                        if (mei.m_BCA_STS != TrMei.Sts.作成対象)
                        {
                            ComMessageMgr.MessageWarning("持出取消登録されていないため、キャンセルはできません");
                            return;
                        }

                        // 確認メッセージ
                        if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "持出取消登録をキャンセルします。よろしいですか？") == DialogResult.No)
                        {
                            return;
                        }

                        // 持出取消アップロード状態を未作成に変更
                        if (!_itemMgr.OCCancelDisableTrMei(CurDetail, _itemMgr.DetailParams.Key, this))
                        {
                            ComMessageMgr.MessageWarning("持出取消登録のキャンセルに失敗しました");
                            return;
                        }
                    }
                    else
                    {
                        // 明細削除

                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), "明細削除", 1);
                        //イメージ編集チェック
                        _itemMgr.getTRMei(CurDetail, out TBL_TRMEI mei, this);
                        // 編集フラグ確認
                        if (mei.m_EDIT_FLG == 1)
                        {
                            ComMessageMgr.MessageWarning("他オペレータが編集中のため、対象証券の削除は行えません");
                            return;
                        }
                        // 持出取消アップロード状態確認
                        if (mei.m_BCA_STS >= TrMei.Sts.作成対象)
                        {
                            ComMessageMgr.MessageWarning("持出取消済の明細は削除できません");
                            return;
                        }
                        //int[] ErrSts = { TrMei.Sts.ファイル作成, TrMei.Sts.アップロード };
                        //if (ErrSts.Contains(mei.m_BCA_STS))
                        //{
                        //    ComMessageMgr.MessageWarning("対象証券は持出取消アップロード中のため削除できません");
                        //    return;
                        //}

                        // ステータス確認
                        if (!_itemMgr.ChkTRMEIIMGUploadSts(CurDetail, out List<TBL_TRMEIIMG> ImgList, this))
                        {
                            ComMessageMgr.MessageWarning("対象証券は持出アップロード中のため削除できません");
                            return;
                        }

                        //補正中チェック
                        if (!_itemMgr.ChkEntryHoseiSts(CurDetail, this))
                        {
                            ComMessageMgr.MessageWarning("他オペレータで訂正中のため、明細削除はできません");
                            return;
                        }

                        //交換日チェック
                        if (int.TryParse(CurDetail.ClearingDate, out int iDate))
                        {
                            if (iDate <= AplInfo.OpDate())
                            {
                                ComMessageMgr.MessageWarning("交換日が処理日以前のため、削除できません");
                                return;
                            }
                        }

                        bool ChkUpload = ImgList.Count(x => x.m_BUA_STS == TrMei.Sts.結果正常) > 0;

                        // 確認メッセージ
                        string Msg = "対象証券を削除します。よろしいですか？";
                        if (ChkUpload)
                        {
                            Msg = "対象証券を削除します。よろしいですか？\nアップロード済の証券は持出取消テキスト作成画面より\nテキスト作成処理を行ってください。";
                        }
                        if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, Msg) == DialogResult.No)
                        {
                            return;
                        }

                        if (ChkUpload)
                        {
                            // アップロード済のイメージがある場合は持出取消アップロード状態を作成対象に変更
                            if (!_itemMgr.OCCancelTrMei(CurDetail, _itemMgr.DetailParams.Key, this))
                            {
                                ComMessageMgr.MessageWarning("削除に失敗しました");
                                return;
                            }
                        }
                        else
                        {
                            // すべて未アップロードの場合、削除処理
                            if (!_itemMgr.DeleteTrMei(CurDetail, _itemMgr.DetailParams.Key, this))
                            {
                                ComMessageMgr.MessageWarning("削除に失敗しました");
                                return;
                            }
                        }
                    }

                    //画面データ更新
                    SetDisplayParams(true);
                }
                else if (IsPressCtrlKey)
                {
                    // Ctrlキー押下

                    if (CurDetail.MeiDelete == 1)
                    {
                        // 削除データ
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), "強制削除復活", 1);

                        if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "強制削除復活を行います、よろしいですか？") == DialogResult.No)
                        {
                            return;
                        }

                        // 削除処理
                        if (!_itemMgr.UnDeleteTrMei(CurDetail, _itemMgr.DetailParams.Key, this))
                        {
                            ComMessageMgr.MessageWarning("強制削除復活に失敗しました");
                            return;
                        }
                    }
                    else
                    {
                        // 未削除
                        LogWriter.writeLog(MethodBase.GetCurrentMethod(), "強制削除", 1);

                        if (ComMessageMgr.MessageQuestion(MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2, "強制削除を行います、よろしいですか？") == DialogResult.No)
                        {
                            return;
                        }

                        // 削除処理
                        if (!_itemMgr.DeleteTrMei(CurDetail, _itemMgr.DetailParams.Key, this))
                        {
                            ComMessageMgr.MessageWarning("強制削除に失敗しました");
                            return;
                        }
                    }

                    //画面データ更新
                    SetDisplayParams(true);
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
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F10：前明細
        /// </summary>
        protected override void btnFunc10_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "前明細", 1);
                if (!_itemMgr.CurrentDetailMove(true))
                {
                    // 移動不可
                    ComMessageMgr.MessageWarning("先頭明細のため前明細に移動することはできません");
                    return;
                }

                //画面データ更新
                SetDisplayParams(true);
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
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F11：次明細
        /// </summary>
        protected override void btnFunc11_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "次明細", 1);
                if (!_itemMgr.CurrentDetailMove(false))
                {
                    // 移動不可
                    ComMessageMgr.MessageWarning("最終明細のため次明細に移動することはできません");
                    return;
                }

                //画面データ更新
                SetDisplayParams(true);
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
                RefreshDisplayState();
            }
        }

        /// <summary>
        /// F12：訂正
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "訂正", 1);
                //訂正処理実施
                string GKey = CommonUtil.GenerateKey(CurDetail.OpeDate, CurDetail.ScanTerm, CurDetail.BatID, CurDetail.DetailNo);
                string Argument = string.Format("{0} {1} {2} {3} {4}", _ctl.MenuNumber, 1, CurDetail.GymID, 5, GKey);
                if (!_itemMgr.RunProcess("CTRHoseiEntry.exe", Argument, this))
                {
                    ComMessageMgr.MessageWarning("補正エントリーの起動に失敗しました");
                }

                // 最新データの部分更新
                _itemMgr.UpdateTeiseiData(CurDetail, this);

                //画面データ更新
                SetDisplayParams(false);
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
                RefreshDisplayState();
            }
        }

        // *******************************************************************
        // 内部メソッド（イメージ関連）
        // *******************************************************************

        /// <summary>
        /// 画面コントロール描画
        /// </summary>
        private void MakeView(ItemManager.ImageInfo imgInfo)
        {
            // コントロール描画中断
            this.SuspendLayout();

            // 最初にコントロールを削除する
            this.RemoveImgControl(_imgHandler);

            // TBL_IMG_PARAM は 1 以上の値を入れておく
            TBL_IMG_PARAM imgparam = new TBL_IMG_PARAM(AppInfo.Setting.GymId, 1, AppInfo.Setting.SchemaBankCD);
            imgparam.m_REDUCE_RATE = _ctl.SettingData.ImageReduceRate;
            imgparam.m_IMG_TOP = 1;
            imgparam.m_IMG_LEFT = 1;
            imgparam.m_IMG_HEIGHT = 1;
            imgparam.m_IMG_WIDTH = 1;
            imgparam.m_XSCROLL_VALUE = 0;

            // イメージコントロール作成
            _imgHandler = new ImageHandler(_ctl);
            _imgHandler.InitializePanelSize(pnlImage.Width, pnlImage.Height);
            if (imgInfo.HasImage)
            {
                // 対象イメージがある場合のみ表示処理実施
                _imgHandler.CreateImageControl(imgInfo.MeiImage, imgparam, imgInfo.ImgFolderPath, true);
                // コントロール、コントロールのイベント設定
                this.PutImgControl(_imgHandler);
            }

            // コントロール描画再開
            this.ResumeLayout();
        }

        /// <summary>
        /// 画面コントロールに画像イメージを追加する
        /// </summary>
        /// <param name="eiHandler"></param>
        internal void PutImgControl(ImageHandler eiHandler)
        {
            if (eiHandler == null) { return; }
            contentsPanel.Controls.Add(eiHandler.pcPanel);
            eiHandler.SetImagePosition(pnlImage.Top, pnlImage.Left);
            pnlImage.Visible = false;
        }

        /// <summary>
        /// 画面コントロールから画像イメージを削除する
        /// </summary>
        /// <param name="eiHandler"></param>
        internal void RemoveImgControl(ImageHandler eiHandler)
        {
            if (eiHandler == null) { return; }
            eiHandler.ClearImage();
            contentsPanel.Controls.Remove(eiHandler.pcPanel);
            pnlImage.Visible = true;
        }

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        #region タブ制御

        /// <summary>
        /// タブボタンの表示を切り替える
        /// </summary>
        /// <param name="lblTab"></param>
        /// <param name="imgInfo"></param>
        private void SetTabControl(Label lblTab, ItemManager.ImageInfo imgInfo)
        {
            if (_curImage.ImgKbn == imgInfo.ImgKbn)
            {
                // 選択済み
                lblTab.BackColor = Color.LightGreen;
                lblTab.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                // 未選択
                if (imgInfo.HasImage)
                {
                    // イメージあり
                    lblTab.BackColor = Color.Ivory;
                    lblTab.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    // イメージなし
                    lblTab.BackColor = SystemColors.ScrollBar;
                    lblTab.BorderStyle = BorderStyle.FixedSingle;
                }
            }
        }

        #endregion

        #region Function処理

        /// <summary>
        /// イメージ切り出し処理
        /// </summary>
        private void ImageCut()
        {
            // 編集フラグ更新
            if (!_itemMgr.UpdateTRMEIEditFlg(CurDetail, 1, this))
            {
                ComMessageMgr.MessageWarning("編集フラグの更新に失敗しました");
                return;
            }

            try
            {
                // イメージ切り出し画面表示
                ImageOperation.ImageCut form = new ImageOperation.ImageCut(ImageOperation.ImageCut.CutType.ImageImport, _curImage.ImgFolderPath, _curImage.MeiImage.m_IMG_FLNM,
                                                                           NCR.Server.Tegata, NCR.Server.Kogitte, NCR.Server.DstDpi, NCR.Server.Quality, NCR.Server.ScanImageBackUpRoot);
                form.InitializeForm(_ctl);
                form.ResetForm();
                DialogResult result = form.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }

                // アップロードステータス更新
                if (!(_curImage.MeiImage.m_BUA_STS == TrMei.Sts.未作成))
                {
                    using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
                    using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
                    {
                        Dictionary<string, int> UpdateData = new Dictionary<string, int>();
                        UpdateData.Add(TBL_TRMEIIMG.BUA_STS, TrMei.Sts.再作成対象);
                        if (!_itemMgr.UpdateTRMEIIMGSts(_curImage.MeiImage, UpdateData, dbp, Tran, this))
                        {
                            ComMessageMgr.MessageWarning("ステータスの更新に失敗しました");
                            return;
                        }

                        //処理成功時コミット
                        Tran.Trans.Commit();
                    }
                }

                // 画面コントロール描画
                MakeView(_curImage);
                // 画面表示状態更新
                RefreshDisplayState();
            }
            finally
            {
                // 編集フラグ更新
                if (!_itemMgr.UpdateTRMEIEditFlg(CurDetail, 0, this))
                {
                    ComMessageMgr.MessageWarning("編集フラグの更新に失敗しました");
                }
            }
        }

        /// <summary>
        /// イメージ差替/追加処理
        /// </summary>
        private void ImageReplace()
        {
            // 編集フラグ更新
            if (!_itemMgr.UpdateTRMEIEditFlg(CurDetail, 1, this))
            {
                ComMessageMgr.MessageWarning("編集フラグの更新に失敗しました");
                return;
            }

            try
            {
                // イメージ差替/追加画面表示
                int ImgKbn = _curImage.ImgKbn;
                ImageOperation.ImageSelect form = new ImageOperation.ImageSelect(
                                                        CurDetail.GymID, CurDetail.OpeDate, CurDetail.ScanTerm, CurDetail.BatID, CurDetail.DetailNo, ImgKbn, AppInfo.Setting.SchemaBankCD,
                                                        NCR.Server.ScanImageReplaceRoot, NCR.Server.ScanImageBackUpRoot,
                                                        _itemMgr.BankNormalImageRoot(), _itemMgr.BankFutaiImageRoot(), _itemMgr.BankInventoryImageRoot(),
                                                        NCR.Server.Tegata, NCR.Server.Kogitte, NCR.Server.DstDpi, NCR.Server.Quality,
                                                        NCR.Server.ScanImageBackUpRoot);
                form.InitializeForm(_ctl);
                form.ResetForm();
                DialogResult result = form.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }

                //データの再取得
                _itemMgr.UpdateTeiseiData(CurDetail, this);

                //カレントの再設定
                _curImage = _itemMgr.ImageInfos[ImgKbn];
                // 画面コントロール描画
                MakeView(_curImage);
                // 画面表示状態更新
                RefreshDisplayState();
            }
            finally
            {
                // 編集フラグ更新
                if (!_itemMgr.UpdateTRMEIEditFlg(CurDetail, 0, this))
                {
                    ComMessageMgr.MessageWarning("編集フラグの更新に失敗しました");
                }
            }
        }

        /// <summary>
        /// イメージ削除処理
        /// </summary>
        private void ImageDelete()
        {
            // 更新
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            using (AdoNonCommitTransaction Tran = new AdoNonCommitTransaction(dbp))
            {
                // 削除処理
                Dictionary<string, int> UpdateData = new Dictionary<string, int>();
                UpdateData.Add(TBL_TRMEIIMG.DELETE_FLG, 1);
                UpdateData.Add(TBL_TRMEIIMG.DELETE_DATE, AplInfo.OpDate());
                if (!_itemMgr.UpdateTRMEIIMGSts(_curImage.MeiImage, UpdateData, dbp, Tran, this))
                {
                    ComMessageMgr.MessageWarning("削除に失敗しました");
                    return;
                }

                // 退避処理
                if (!ImportFileAccess.FileBackUp(_curImage.ImgFolderPath, _curImage.MeiImage.m_IMG_FLNM, NCR.Server.ScanImageBackUpRoot))
                {
                    ComMessageMgr.MessageWarning("ファイルの退避に失敗しました。");
                    return;
                }

                //処理成功時コミット
                Tran.Trans.Commit();
            }

            //データの再取得
            _itemMgr.UpdateTeiseiData(CurDetail, this);

            //カレントの再設定
            _curImage = _itemMgr.ImageInfos[_curImage.ImgKbn];
            // 画面コントロール描画
            MakeView(_curImage);
            // 画面表示状態更新
            RefreshDisplayState();

        }

        #endregion 

        #region 処理中設定

        /// <summary>
        /// 処理中状態に設定
        /// </summary>
        private void Processing(string msg)
        {
            // ファンクションDisable
            DisableAllFunctionState(false);

            SetWaitCursor();
            this.SetStatusMessage(msg, System.Drawing.Color.Transparent);
            this.Refresh();
        }

        /// <summary>
        /// 処理中状態を解除する
        /// </summary>
        private void EndProcessing(string msg)
        {
            // Disableにしたファンクションを元に戻す
            InitializeFunction();
            SetFunctionState();

            if (this.GetStatusMessage() == msg)
            {
                //メッセージが同じ場合クリア
                this.ClearStatusMessage();
            }
            ResetCursor();
        }

        #endregion

        #region 入力チェック

        /// <summary>
        /// KeyDown時のクリアメッセージ
        /// </summary>
        private void KeyDownClearStatusMessage(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                case Keys.F2:
                case Keys.F3:
                case Keys.F4:
                case Keys.F5:
                case Keys.F6:
                case Keys.F7:
                case Keys.F8:
                case Keys.F9:
                case Keys.F10:
                case Keys.F11:
                case Keys.F12:
                    break;
                default:
                    this.ClearStatusMessage();
                    break;
            }
        }

        /// <summary>
        /// 全コントロール取得
        /// </summary>
        private static List<Control> AllSubControls(Control control)
        {
            var list = control.Controls.OfType<Control>().ToList();
            var deep = list.Where(o => o.Controls.Count > 0).SelectMany(AllSubControls).ToList();
            list.AddRange(deep);
            return list;
        }

        /// <summary>
        /// 全入力項目チェック
        /// </summary>
        private bool CheckInputAll()
        {
            foreach (Control con in AllSubControls(this).OrderBy(c => c.TabIndex))
            {
                if (con is BaseTextBox)
                {
                    // BaseTextBoxを継承している場合は、チェックイベント強制発生
                    if (((BaseTextBox)con).RaiseI_Validating())
                    {
                        // 項目遷移時、遷移元のValidatingは発生させない
                        this.AutoValidate = AutoValidate.Disable;
                        ((BaseTextBox)con).Select();
                        this.AutoValidate = AutoValidate.EnablePreventFocusChange;

                        return false;
                    }
                }
            }
            return true;
        }


        #endregion

    }
}
