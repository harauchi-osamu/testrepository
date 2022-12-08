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

namespace SearchIcMeiView
{
    /// <summary>
    /// 明細画面
    /// </summary>
    public partial class SearchIcMeiDetail : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;
        private ImageHandler _imgHandler = null;
        private ItemManager.ImageInfo _curImage = null;
        private enumInfo _curInfo = enumInfo.Info1;

        float FilenameDefSize = 12;

        #region 情報タブ定義

        public enum enumInfo
        {
            Info1 = 1,
            Info2 = 2,
            Info3 = 3,
        }

        #endregion

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
        public SearchIcMeiDetail()
        {
            InitializeComponent();
            // ファイル名の初期フォントサイズ取得
            FilenameDefSize = lblfilename.Font.Size;
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
            base.SetDispName1("交換持帰");
        }

        /// <summary>
        /// 画面名を設定する
        /// </summary>
        /// <param name="dispName"></param>
        protected override void SetDispName2(string dispName)
        {
            base.SetDispName2("持帰明細照会");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            if (IsNotPressCSAKey)
            {
                // 通常状態
                SetFunctionName(F1_, "一覧");
                SetFunctionName(F2_, "印刷");
                SetFunctionName(F3_, "PDF出力", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F4_, "不渡\n エントリ", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F5_, "拡大");
                SetFunctionName(F6_, "縮小");
                SetFunctionName(F7_, "回転\n(右回り)", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F8_, "回転\n(左回り)", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F9_, "前明細", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F10_, "次明細", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F11_, "交換尻\n 訂正", true, Const.FONT_SIZE_FUNC_LOW);
                SetFunctionName(F12_, "自行情報\n 訂正", true, Const.FONT_SIZE_FUNC_LOW);
            }
            else if (IsPressShiftKey)
            {
                // Shiftキー押下
                SetFunctionName(SF1_, string.Empty);
                SetFunctionName(SF2_, string.Empty);
                SetFunctionName(SF3_, string.Empty);
                SetFunctionName(SF4_, string.Empty);
                SetFunctionName(SF5_, string.Empty);
                SetFunctionName(SF6_, string.Empty);
                SetFunctionName(SF7_, string.Empty);
                SetFunctionName(SF8_, string.Empty);
                SetFunctionName(SF9_, string.Empty);
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
                    //SetFunctionState(F4_, false); // 不渡は削除でも使用可能
                    SetFunctionState(F11_, false);
                    SetFunctionState(F12_, false);
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

            //イメージタブ
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

            //情報タブ
            SetInfoTabControl(lblInfo1, pnlInfo1, enumInfo.Info1);
            SetInfoTabControl(lblInfo2, pnlInfo2, enumInfo.Info2);
            SetInfoTabControl(lblInfo3, pnlInfo3, enumInfo.Info3);
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
            //検索モード表示切り替え
            IcMeiListCommon.DispSearchSortMode(lblSearchSortMode, _itemMgr, _ctl);

            // イメージ取得
            _itemMgr.Fetch_meiimges();

            //【共通】
            //取込日
            lblreaddate.Text = IcMeiListCommon.DispDate(CurDetail.OpeDate, "");
            //バッチ番号
            lblbatchno.Text = CurDetail.BatID.ToString("D6");
            //持出銀行ｺｰﾄﾞ
            lblOCBkNo.Text = CurDetail.OCBKNo.ToString("D4");
            //持出銀行名
            lblOCBkName.Text = _itemMgr.GetBank(CurDetail.OCBKNo);
            //明細番号
            lblDetailNo.Text = CurDetail.DetailNo.ToString("D6");
            //イメージファイル名(表名）
            string FLNM = string.Empty;
            if (_itemMgr.ImageInfos[TrMeiImg.ImgKbn.表].HasImage)
            {
                FLNM = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表].MeiImage.m_IMG_FLNM;
            }
            lblfilename.Text = FLNM;
            //決済対象区分
            lblpaykbn.Text = IcMeiListCommon.DispPayKbnFormat(CurDetail);

            //【Info1】
            //交換希望日
            lblclearingdate.Text = IcMeiListCommon.DispDate(CurDetail.ClearingDate, "");
            //持帰銀行コード
            lblbankcode.Text = CurDetail.ICBKNo;
            //金額
            lblamount.Text = IcMeiListCommon.DispFormat(CurDetail.Amount, "#,##0");
            //証券種類
            lblBill.Text = CurDetail.BillCD;
            //証券種類名
            lblBillName.Text = _itemMgr.GeBill(CurDetail.BillCD);
            //手形種類
            lblRyurui.Text = CurDetail.SyuruiCD;
            //手形種類名
            lblRyuruiName.Text = _itemMgr.GetSyurui(CurDetail.SyuruiCD);
            //支店番号
            lblbrno.Text = CurDetail.ICBrCD;
            //支店名
            lblbrName.Text = _itemMgr.GeBranch(CurDetail.ICBrCD);
            //口座番号
            lblAccount.Text = CurDetail.Account;
            //手形番号
            lblTegata.Text = CurDetail.Tegata;
            //状態
            lblSts.Text = IcMeiListCommon.DispInputStsFormat(CurDetail);
            //削除状態
            lblDelete.Text = IcMeiListCommon.DispDeleteFlgFormat(CurDetail);
            //訂正入力
            lblTeisei.Text = IcMeiListCommon.DispTeiseiFlgFormat(CurDetail);
            //不渡入力
            lblFuwatari.Text = IcMeiListCommon.DispFuwatariFlgFormat(CurDetail);
            //訂正結果
            lblBMASts.Text = TrMei.Sts.GetName(CurDetail.BMASts);
            //不渡返還結果
            lblBRASts.Text = TrMei.Sts.GetName(CurDetail.BRASts);
            //二重持出通知日
            lblBUB.Text = IcMeiListCommon.DispDate(CurDetail.BUBDate, "警告なし");

            //【Info2】
            //交換希望日
            lblCDateCTR.Text = IcMeiListCommon.DispDate(CurDetail.InptCTRClearingDate, "");
            lblCDateINP.Text = IcMeiListCommon.DispDate(CurDetail.InptClearingDate, "");
            lblCDateEND.Text = IcMeiListCommon.DispDate(CurDetail.ClearingDate, "");
            //持帰銀行
            lblICBknoCTR.Text = CurDetail.InptCTRICBKNo;
            lblICBknoINP.Text = CurDetail.InptICBKNo;
            lblICBknoEND.Text = CurDetail.ICBKNo;
            //金額
            lblAmtCTR.Text = IcMeiListCommon.DispFormat(CurDetail.CTRAmount, "#,##0");
            lblAmtINP.Text = IcMeiListCommon.DispFormat(CurDetail.Amount, "#,##0");
            lblAmtEND.Text = IcMeiListCommon.DispFormat(CurDetail.Amount, "#,##0");
            //証券種類
            lblBillCTR.Text = CurDetail.CTRBillCD;
            lblBillINPT.Text = CurDetail.BillCD;
            lblBillEND.Text = CurDetail.BillCD;
            //手形種類
            lblRyuruiCTR.Text = CurDetail.CTRSyuruiCD;
            lblRyuruiINPT.Text = CurDetail.SyuruiCD;
            lblRyuruiEND.Text = CurDetail.SyuruiCD;
            //支店番号
            lblbrnoCTR.Text = CurDetail.InptCTRICBrCD;
            lblbrnoINPT.Text = CurDetail.InptICBrCD;
            lblbrnoEND.Text = CurDetail.ICBrCD;
            //口座番号
            lblAccountCTR.Text = CurDetail.InptCTRAccount;
            lblAccountINPT.Text = CurDetail.InptAccount;
            lblAccountEND.Text = CurDetail.Account;
            //手形番号
            lblTegataCTR.Text = CurDetail.CTRTegata;
            lblTegataINPT.Text = CurDetail.Tegata;
            lblTegataEND.Text = CurDetail.Tegata;

            //【Info3】
            //[持帰銀行] エントリー
            lblICBkEnt.Text = IcMeiListCommon.DispOperatorFormat(CurDetail.ICBKEOpe, _itemMgr);
            //[持帰銀行] ベリファイ
            lblICBkVeri.Text = IcMeiListCommon.DispOperatorFormat(CurDetail.ICBKVOpe, _itemMgr);
            //[交換希望日] エントリー
            lblCDateEnt.Text = IcMeiListCommon.DispOperatorFormat(CurDetail.CDateEOpe, _itemMgr);
            //[交換希望日] ベリファイ
            lblCDateVeri.Text = IcMeiListCommon.DispOperatorFormat(CurDetail.CDateVOpe, _itemMgr);
            //[金額] エントリー
            lblAmtEnt.Text = IcMeiListCommon.DispOperatorFormat(CurDetail.AmountEOpe, _itemMgr);
            //[金額] ベリファイ
            lblAmtVeri.Text = IcMeiListCommon.DispOperatorFormat(CurDetail.AmountVOpe, _itemMgr);
            //[自行情報] エントリー
            lblJikouEnt.Text = IcMeiListCommon.DispOperatorFormat(CurDetail.JikouEOpe, _itemMgr);
            //[自行情報] ベリファイ
            lblJikouVeri.Text = IcMeiListCommon.DispOperatorFormat(CurDetail.JikouVOpe, _itemMgr);

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
                ItemManager.ImageInfo orgImage = _curImage;
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

                if (!_curImage.HasImage)
                {
                    _curImage = orgImage;
                    return;
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

        /// <summary>
        /// [情報タブ]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Infotab_MouseClick(object sender, MouseEventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                switch (((Control)sender).Name)
                {
                    case "lblInfo1":
                        _curInfo = enumInfo.Info1;
                        break;
                    case "lblInfo2":
                        _curInfo = enumInfo.Info2;
                        break;
                    case "lblInfo3":
                        _curInfo = enumInfo.Info3;
                        break;
                }

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

        /// <summary>
        /// ラベルTextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_TextChanged(object sender, EventArgs e)
        {
            if (ReferenceEquals(sender, lblfilename))
            {
                // ラベル：ファイル名
                // フォントサイズを適時変更
                using (Graphics g = this.CreateGraphics())
                {
                    CommonUtil.FitLabelFontSize(lblfilename, FilenameDefSize, g);
                }
            }
        }

        // *******************************************************************
        // イベント（ファンクションキー）       
        // *******************************************************************

        /// <summary>
        /// F02：印刷
        /// </summary>
        protected override void btnFunc02_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "印刷", 1);
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
                    string Argument = string.Format("{0} {1}", GymParam.GymId.持帰, 1);
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
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F03：PDF出力
        /// </summary>
        protected override void btnFunc03_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "PDF出力", 1);
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
                    string Argument = string.Format("{0} {1}", GymParam.GymId.持帰, 2);
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
        /// F04：不渡エントリ
        /// </summary>
        protected override void btnFunc04_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "不渡エントリ", 1);
                //不渡エントリ処理実施
                string GKey = CommonUtil.GenerateKey(CurDetail.OpeDate, CurDetail.ScanTerm, CurDetail.BatID, CurDetail.DetailNo);
                string Argument = string.Format("{0} {1}", _ctl.MenuNumber, GKey);
                if (!_itemMgr.RunProcess("CTRFuwatariEntry.exe", Argument, this))
                {
                    ComMessageMgr.MessageWarning("不渡エントリの起動に失敗しました");
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
        /// F07：回転（右回り）
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
        /// F09：前明細
        /// CF09：強制削除/強制削除復活
        /// </summary>
        protected override void btnFunc09_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (IsNotPressCSAKey)
                {
                    // 通常状態

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
        /// F10：次明細
        /// </summary>
        protected override void btnFunc10_Click(object sender, EventArgs e)
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
        /// F11：交換尻訂正
        /// </summary>
        protected override void btnFunc11_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "交換尻訂正", 1);
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

        /// <summary>
        /// F12：自行情報訂正
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "自行情報訂正", 1);
                //訂正処理実施
                string GKey = CommonUtil.GenerateKey(CurDetail.OpeDate, CurDetail.ScanTerm, CurDetail.BatID, CurDetail.DetailNo);
                string Argument = string.Format("{0} {1} {2} {3} {4}", _ctl.MenuNumber, 1, CurDetail.GymID, 6, GKey);
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
            if (imgInfo.HasImage)
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
                    lblTab.BackColor = Color.Ivory;
                    lblTab.BorderStyle = BorderStyle.FixedSingle;
                }
            }
            else
            {
                // 使用不可
                lblTab.BackColor = SystemColors.ScrollBar;
                lblTab.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        /// <summary>
        /// 情報タブボタンの表示を切り替える
        /// </summary>
        /// <param name="lblTab"></param>
        /// <param name="imgInfo"></param>
        private void SetInfoTabControl(Label lblTab, Panel pnlTab, enumInfo Info)
        {
            if (_curInfo == Info)
            {
                // 選択済み
                lblTab.BackColor = Color.LightGreen;
                lblTab.BorderStyle = BorderStyle.FixedSingle;
                pnlTab.Visible = true;
            }
            else
            {
                // 未選択
                lblTab.BackColor = SystemColors.Control;
                lblTab.BorderStyle = BorderStyle.FixedSingle;
                pnlTab.Visible = false;
            }
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
