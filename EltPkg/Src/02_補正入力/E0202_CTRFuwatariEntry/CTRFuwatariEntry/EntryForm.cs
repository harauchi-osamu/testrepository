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
using EntryCommon;

namespace CTRFuwatariEntryForm
{
    /// <summary>
    /// 不渡返還登録画面
    /// </summary>
    public partial class EntryForm : EntryCommonFormBase
    {
        private Controller _ctl = null;
        private MasterManager _masterMgr = null;
        private ItemManager _itemMgr = null;

        private ImageHandler _imgHandler = null;
        private ItemManager.ImageInfo _curImage = null;
        private DateTime _startTime;
        private RegistStatus _registStatus = RegistStatus.未登録;
        private BankStatus _bankStatus =  BankStatus.自行;
        private RegistErrStatus _regErrStatus = RegistErrStatus.編集可;

        private List<ItemSet> _kbnList1 = null;
        private List<ItemSet> _kbnList2 = null;
        private List<ItemSet> _kbnList3 = null;
        private List<ItemSet> _kbnList4 = null;
        private List<ItemSet> _kbnList5 = null;

        private List<ItemSet> _jiyuList1 = null;
        private List<ItemSet> _jiyuList2 = null;
        private List<ItemSet> _jiyuList3 = null;
        private List<ItemSet> _jiyuList4 = null;
        private List<ItemSet> _jiyuList5 = null;

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

        private const int FUBI_KBN_1 = 1;
        private const int FUBI_KBN_2 = 2;
        private const int FUBI_KBN_3 = 3;
        private const int FUBI_KBN_4 = 4;
        private const int FUBI_KBN_5 = 5;

        public enum RegistStatus
        {
            未登録,
            編集不可,
            登録取消可,
            登録のみ可,
            取消のみ可,
        }

        public enum OpeType
        {
            取消,
            登録
        }

        public enum MethodType
        {
            登録,
            更新,
            削除
        }

        public enum BankStatus
        {
            自行,
            他行,
        }

        public enum RegistErrStatus
        {
            編集可,
            不渡編集不可,
            //持出取消編集不可,
            //持帰訂正編集不可,
        }

        /// <summary>
        /// 銀行ステータス
        /// </summary>
        public BankStatus bankStatus { get { return _bankStatus; } }
        /// <summary>
        /// 銀行ステータス
        /// </summary>
        public RegistErrStatus regErrStatus { get { return _regErrStatus; } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EntryForm()
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
            _imgHandler = new ImageHandler(_ctl);

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
            base.SetDispName2("不渡返還登録");
        }

        /// <summary>
        /// ファンクションキーを設定する
        /// </summary>
        protected override void InitializeFunction()
        {
            // 通常状態
            SetFunctionName(F1_, "終了");
            SetFunctionName(F2_, string.Empty);
            SetFunctionName(F3_, string.Empty);
            SetFunctionName(F4_, "クリア");
            SetFunctionName(F5_, "拡大");
            SetFunctionName(F6_, "縮小");
            SetFunctionName(F7_, "回転\n（左回り）", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F8_, "回転\n（右回り）", true, Const.FONT_SIZE_FUNC_LOW);
            SetFunctionName(F9_, "取消", false);
            SetFunctionName(F10_, string.Empty);
            SetFunctionName(F11_, string.Empty);
            SetFunctionName(F12_, "登録", false);
        }

        /// <summary>
        /// ファンクションキー状態を設定する
        /// </summary>
        protected override void SetFunctionState()
        {
            // Validation抑制
            this.ChangeFunctionCausesValidation(false);

            // 設定ファイル読み込みでエラー発生時はF1以外Disable
            if (this._ctl.SettingData.ChkServerIni == false || !string.IsNullOrEmpty(this._ctl.SettingData.CheckParamMsg))
            {
                DisableAllFunctionState(true);
                return;
            }
            if (_itemMgr.IsMeisaiNull)
            {
                DisableAllFunctionState(true);
                return;
            }

            // クリア処理
            bool canEdit = !(_registStatus == RegistStatus.編集不可 || _registStatus == RegistStatus.取消のみ可);
            SetFunctionState(F4_, canEdit);

            // 取消処理
            bool canDel = (_registStatus == RegistStatus.登録取消可 || _registStatus == RegistStatus.取消のみ可);
            SetFunctionState(F9_, canDel);

            // 登録処理
            bool canRegist = (_registStatus == RegistStatus.未登録 || _registStatus == RegistStatus.登録取消可 || _registStatus == RegistStatus.登録のみ可);
            SetFunctionState(F12_, canRegist);
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
            if (_ctl.IsIniErr) { return; }
            if (_itemMgr.IsMeisaiNull) { return; }

            _kbnList1 = GetKbnList();
            _kbnList2 = GetKbnList();
            _kbnList3 = GetKbnList();
            _kbnList4 = GetKbnList();
            _kbnList5 = GetKbnList();
            _jiyuList1 = GetJiyuList(string.Empty);
            _jiyuList2 = GetJiyuList(string.Empty);
            _jiyuList3 = GetJiyuList(string.Empty);
            _jiyuList4 = GetJiyuList(string.Empty);
            _jiyuList5 = GetJiyuList(string.Empty);

            cmbKbn1.DataSource = _kbnList1;
            cmbKbn1.DisplayMember = "ItemDisp";
            cmbKbn1.ValueMember = "ItemValue";
            cmbKbn2.DataSource = _kbnList2;
            cmbKbn2.DisplayMember = "ItemDisp";
            cmbKbn2.ValueMember = "ItemValue";
            cmbKbn3.DataSource = _kbnList3;
            cmbKbn3.DisplayMember = "ItemDisp";
            cmbKbn3.ValueMember = "ItemValue";
            cmbKbn4.DataSource = _kbnList4;
            cmbKbn4.DisplayMember = "ItemDisp";
            cmbKbn4.ValueMember = "ItemValue";
            cmbKbn5.DataSource = _kbnList5;
            cmbKbn5.DisplayMember = "ItemDisp";
            cmbKbn5.ValueMember = "ItemValue";

            cmbJiyu1.DataSource = _jiyuList1;
            cmbJiyu1.DisplayMember = "ItemDisp";
            cmbJiyu1.ValueMember = "ItemValue";
            cmbJiyu2.DataSource = _jiyuList2;
            cmbJiyu2.DisplayMember = "ItemDisp";
            cmbJiyu2.ValueMember = "ItemValue";
            cmbJiyu3.DataSource = _jiyuList3;
            cmbJiyu3.DisplayMember = "ItemDisp";
            cmbJiyu3.ValueMember = "ItemValue";
            cmbJiyu4.DataSource = _jiyuList4;
            cmbJiyu4.DisplayMember = "ItemDisp";
            cmbJiyu4.ValueMember = "ItemValue";
            cmbJiyu5.DataSource = _jiyuList5;
            cmbJiyu5.DisplayMember = "ItemDisp";
            cmbJiyu5.ValueMember = "ItemValue";
        }

        /// <summary>
        /// 画面表示データ初期化
        /// </summary>
        protected void InitializeDisplayData()
        {
            if (_ctl.IsIniErr) { return; }
            if (_itemMgr.IsMeisaiNull) { return; }

            if (GetItemStrVal(DspItem.ItemId.持帰銀行コード) == NCR.Operator.BankCD.ToString(Const.BANK_NO_LEN_STR))
            {
                // 持帰銀行が自行の場合
                _bankStatus = BankStatus.自行;
            }
            else
            {
                // 持帰銀行が自行以外の場合
                _bankStatus = BankStatus.他行;
                // 他行でも後続処理は一応実施する
            }

            switch (_itemMgr.trmei.m_DELETE_FLG)
            {
                case 1:
                    // 削除

                    if (_itemMgr.trmei.m_GRA_STS == TrMei.Sts.結果正常)
                    {
                        // 明細削除で不渡変更アップロードが結果正常
                        _regErrStatus = RegistErrStatus.編集可;
                    }
                    else
                    {
                        // 上記以外
                        _regErrStatus = RegistErrStatus.不渡編集不可;
                    }
                    //if (_itemMgr.trmei.m_BCA_DATE != 0)
                    //{
                    //    // 持出取消での削除ケース
                    //    _regErrStatus = RegistErrStatus.持出取消編集不可;
                    //}
                    //else if (_bankStatus == BankStatus.他行 && _itemMgr.trmei.m_GMA_STS == TrMei.Sts.結果正常)
                    //{
                    //    // 持帰訂正での削除ケース(持帰銀行訂正)
                    //    _regErrStatus = RegistErrStatus.持帰訂正編集不可;
                    //}
                    //else
                    //{
                    //    // 上記以外
                    //    _regErrStatus = RegistErrStatus.編集可;
                    //}

                    break;
                default:
                    // 削除以外

                    if (_itemMgr.trmei.m_GRA_STS == TrMei.Sts.未作成)
                    {
                        // 明細未削除で不渡変更アップロードが未作成
                        _regErrStatus = RegistErrStatus.編集可;
                    }
                    else if (_itemMgr.trmei.m_GRA_STS == TrMei.Sts.結果正常)
                    {
                        // 明細未削除で不渡変更アップロードが結果正常(取消成功)
                        _regErrStatus = RegistErrStatus.編集可;
                    }
                    else
                    {
                        // 上記以外
                        _regErrStatus = RegistErrStatus.不渡編集不可;
                    }
                    break;
            }
            // 編集不可でも後続処理は一応実施する

            // 不渡トランザクション登録
            if (_itemMgr.fuwatari == null)
            {
                TBL_TRMEI mei = _itemMgr.trmei;
                _itemMgr.fuwatari = new TBL_TRFUWATARI(mei._GYM_ID, mei._OPERATION_DATE, mei._SCAN_TERM, mei._BAT_ID, mei._DETAILS_NO, AppInfo.Setting.SchemaBankCD);
                _registStatus = RegistStatus.未登録;
            }
            else
            {
                // 登録状態判定
                if (!DBConvert.ToBoolNull(_itemMgr.trmei.m_DELETE_FLG))
                {
                    // 明細未削除
                    switch (_itemMgr.trmei.m_GRA_STS)
                    {
                        case TrMei.Sts.未作成:
                            _registStatus = RegistStatus.登録取消可;
                            break;
                        case TrMei.Sts.結果正常:
                            _registStatus = RegistStatus.登録のみ可;
                            break;
                        case TrMei.Sts.再作成対象:
                        case TrMei.Sts.結果エラー:
                        case TrMei.Sts.ファイル作成:
                        case TrMei.Sts.アップロード:
                        default:
                            _registStatus = RegistStatus.編集不可;
                            break;
                    }

                    //switch (_itemMgr.trmei.m_GRA_STS)
                    //{
                    //    case TrMei.Sts.未作成:
                    //    case TrMei.Sts.結果正常:
                    //        _registStatus = RegistStatus.登録取消可;
                    //        break;
                    //    case TrMei.Sts.再作成対象:
                    //    case TrMei.Sts.結果エラー:
                    //        if (_itemMgr.trmei.m_GRA_CONFIRMDATE != 0)
                    //        {
                    //            _registStatus = RegistStatus.登録のみ可;
                    //        }
                    //        else
                    //        {
                    //            _registStatus = RegistStatus.登録取消可;
                    //        }
                    //        break;
                    //    case TrMei.Sts.ファイル作成:
                    //    case TrMei.Sts.アップロード:
                    //        _registStatus = RegistStatus.編集不可;
                    //        break;
                    //    default:
                    //        _registStatus = RegistStatus.登録取消可;
                    //        break;
                    //}
                }
                else
                {
                    // 明細削除
                    switch (_itemMgr.trmei.m_GRA_STS)
                    {
                        case TrMei.Sts.結果正常:
                            _registStatus = RegistStatus.取消のみ可;
                            break;
                        case TrMei.Sts.未作成:
                        case TrMei.Sts.再作成対象:
                        case TrMei.Sts.結果エラー:
                        case TrMei.Sts.ファイル作成:
                        case TrMei.Sts.アップロード:
                        default:
                            _registStatus = RegistStatus.編集不可;
                            break;
                    }

                    //switch (_itemMgr.trmei.m_GRA_STS)
                    //{
                    //    case TrMei.Sts.再作成対象:
                    //    case TrMei.Sts.結果エラー:
                    //        _registStatus = RegistStatus.取消のみ可;
                    //        break;
                    //    case TrMei.Sts.結果正常:
                    //        _registStatus = RegistStatus.登録のみ可;
                    //        break;
                    //    case TrMei.Sts.ファイル作成:
                    //    case TrMei.Sts.アップロード:
                    //        _registStatus = RegistStatus.編集不可;
                    //        break;
                    //    default:
                    //        _registStatus = RegistStatus.登録取消可;
                    //        break;
                    //}
                    //if (_itemMgr.trmei.m_GRA_CONFIRMDATE == 0)
                    //{
                    //    //ないとは思うが取消済でアップロード確定日がなしのケース
                    //    _registStatus = RegistStatus.登録取消可;
                    //}
                }
            }

            if (bankStatus == BankStatus.他行 || regErrStatus == RegistErrStatus.不渡編集不可)
            {
                // 持帰銀行が他行、不渡編集不可の場合、編集不可
                _registStatus = RegistStatus.編集不可;
            }

            // イメージ
            if (_itemMgr.ImageInfos[TrMeiImg.ImgKbn.表再送分].HasImage)
            {
                // 表再送分イメージを優先して表示する
                _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表再送分];
            }
            else
            {
                _curImage = _itemMgr.ImageInfos[TrMeiImg.ImgKbn.表];
            }

            // 画面コントロール描画
            MakeView(_curImage);

            // 処理開始時刻
            _startTime = DateTime.Now;
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

            if (_ctl.IsIniErr) { return; }
            if (_itemMgr.IsMeisaiNull) { return; }
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

            // 編集不可
            bool canEdit = !(_registStatus == RegistStatus.編集不可 || _registStatus == RegistStatus.取消のみ可);
            rdbFuwa0.Enabled = canEdit;
            rdbFuwa1.Enabled = canEdit;
            rdbFuwa2.Enabled = canEdit;
            cmbKbn1.Enabled = canEdit && rdbFuwa0.Checked;
            cmbKbn2.Enabled = canEdit && rdbFuwa0.Checked;
            cmbKbn3.Enabled = canEdit && rdbFuwa0.Checked;
            cmbKbn4.Enabled = canEdit && rdbFuwa0.Checked;
            cmbKbn5.Enabled = canEdit && rdbFuwa0.Checked;
            cmbJiyu1.Enabled = canEdit && rdbFuwa0.Checked;
            cmbJiyu2.Enabled = canEdit && rdbFuwa0.Checked;
            cmbJiyu3.Enabled = canEdit && rdbFuwa0.Checked;
            cmbJiyu4.Enabled = canEdit && rdbFuwa0.Checked;
            cmbJiyu5.Enabled = canEdit && rdbFuwa0.Checked;
        }

        /// <summary>
        /// 画面項目設定
        /// </summary>
        protected override void SetDisplayParams()
        {
            if (_ctl.IsIniErr) { return; }
            if (_itemMgr.IsMeisaiNull) { return; }

            // 対象明細が存在しない
            if (_itemMgr.trmei == null)
            {
                ComMessageMgr.MessageWarning(ComMessageMgr.W00001);
                return;
            }

            // 明細情報
            int syuruiCd = GetItemIntVal(DspItem.ItemId.交換証券種類コード);
            int shitenCd = GetItemIntVal(DspItem.ItemId.持帰支店コード);
            lblKoukanKibouBi.Text = GetItemDateValue(DspItem.ItemId.交換日);                                       // 交換日
            lblSyuruiCd.Text = GetItemPadNumberDspItem(DspItem.ItemId.交換証券種類コード);                         // 証券種類
            lblSyuruiName.Text = _ctl.GetBillName(syuruiCd);                                                       // 証券種類名
            lblShitenCd.Text = GetItemPadNumber(DspItem.ItemId.持帰支店コード, Const.BR_NO_LEN_STR);               // 支店番号
            lblShitenName.Text = _ctl.GetBranchName(shitenCd);                                                     // 支店名
            lblKozaNo.Text = GetItemPadNumberDspItem(DspItem.ItemId.口座番号);                                     // 口座番号
            lblTegataNo.Text = GetItemPadNumberDspItem(DspItem.ItemId.手形番号);                                   // 手形番号
            lblKingaku.Text = GetItemMoney(DspItem.ItemId.金額);                                                   // 金額
            lblFuwatariBi.Text = GetFuwatariBi();                                                                  // 不渡返還登録日
            lblCancelDate.Text = GetCancelDate();                                                                  // 取消登録日
            lblCancelFlg.Text = GetCancelFlg();                                                                    // 取消
            lblUploadSts.Text = TrMei.Sts.GetName(_itemMgr.trmei.m_GRA_STS);                                       // アップロード状態
            lblBatId.Text = _itemMgr.trmei._BAT_ID.ToString(Const.BAT_ID_LEN_STR);                                 // バッチ番号
            lblDetailsNo.Text = _itemMgr.trmei._DETAILS_NO.ToString(Const.DETAILS_NO_LEN_STR);                     // 明細番号
            lblImgName.Text = GetImgFileName(TrMeiImg.ImgKbn.表);                                                  // イメージファイル名（表）

            // 登録情報            
            TBL_TRFUWATARI fuwa = _itemMgr.fuwatari;
            int[] fubiKbn = new int[] { fuwa.m_FUBI_KBN_01, fuwa.m_FUBI_KBN_02, fuwa.m_FUBI_KBN_03, fuwa.m_FUBI_KBN_04, fuwa.m_FUBI_KBN_05 };
            for (int i = 0; i < fubiKbn.Length; i++)
            {
                if (fubiKbn[i] == TrFuwatari.FubiKbn.不渡0号)
                {
                    rdbFuwa0.Checked = true;
                }
                if (fubiKbn[i] == TrFuwatari.FubiKbn.不渡1号)
                {
                    rdbFuwa1.Checked = true;
                }
                if (fubiKbn[i] == TrFuwatari.FubiKbn.不渡2号)
                {
                    rdbFuwa2.Checked = true;
                }
            }

            // 0号不渡事由
            cmbKbn1.SelectedValue = GetFuwatariKbnJiyu(1, FUBI_KBN_1);
            cmbKbn2.SelectedValue = GetFuwatariKbnJiyu(1, FUBI_KBN_2);
            cmbKbn3.SelectedValue = GetFuwatariKbnJiyu(1, FUBI_KBN_3);
            cmbKbn4.SelectedValue = GetFuwatariKbnJiyu(1, FUBI_KBN_4);
            cmbKbn5.SelectedValue = GetFuwatariKbnJiyu(1, FUBI_KBN_5);
            cmbJiyu1.SelectedValue = GetFuwatariKbnJiyu(2, FUBI_KBN_1);
            cmbJiyu2.SelectedValue = GetFuwatariKbnJiyu(2, FUBI_KBN_2);
            cmbJiyu3.SelectedValue = GetFuwatariKbnJiyu(2, FUBI_KBN_3);
            cmbJiyu4.SelectedValue = GetFuwatariKbnJiyu(2, FUBI_KBN_4);
            cmbJiyu5.SelectedValue = GetFuwatariKbnJiyu(2, FUBI_KBN_5);
        }

        /// <summary>
        /// 画面項目取得
        /// </summary>
        protected override bool GetDisplayParams()
        {
            _itemMgr.DispParams.Clear();

            // 必須チェック
            if (!rdbFuwa0.Checked && !rdbFuwa1.Checked && !rdbFuwa2.Checked)
            {
                ComMessageMgr.MessageWarning("登録情報を1つ以上選択してください。");
                return false;
            }

            // 0号不渡
            if (!GetFuwatari0())
            {
                return false;
            }
            // 1号不渡
            _itemMgr.DispParams.Fuwa1 = rdbFuwa1.Checked;
            // 2号不渡
            _itemMgr.DispParams.Fuwa2 = rdbFuwa2.Checked;

            // 登録可能上限チェック
            int chkCount = _itemMgr.DispParams.Fuwa0JiyuCdList.Count;
            chkCount += rdbFuwa1.Checked ? 1 : 0;
            chkCount += rdbFuwa2.Checked ? 1 : 0;
            if (chkCount > 5)
            {
                ComMessageMgr.MessageWarning("登録できる不渡事由は5つまでです。");
                return false;
            }

            // 画面項目取得
            TBL_TRFUWATARI fuwa = _itemMgr.fuwatari;
            fuwa.m_FUBI_KBN_01 = -1;
            fuwa.m_FUBI_KBN_02 = -1;
            fuwa.m_FUBI_KBN_03 = -1;
            fuwa.m_FUBI_KBN_04 = -1;
            fuwa.m_FUBI_KBN_05 = -1;
            fuwa.m_ZERO_FUBINO_01 = -1;
            fuwa.m_ZERO_FUBINO_02 = -1;
            fuwa.m_ZERO_FUBINO_03 = -1;
            fuwa.m_ZERO_FUBINO_04 = -1;
            fuwa.m_ZERO_FUBINO_05 = -1;
            if (rdbFuwa0.Checked)
            {
                foreach (string jiyuCd in _itemMgr.DispParams.Fuwa0JiyuCdList)
                {
                    SetFubiKbn(fuwa, TrFuwatari.FubiKbn.不渡0号, DBConvert.ToIntNull(jiyuCd));
                }
            }
            if (rdbFuwa1.Checked)
            {
                SetFubiKbn(fuwa, TrFuwatari.FubiKbn.不渡1号, -1);
            }
            if (rdbFuwa2.Checked)
            {
                SetFubiKbn(fuwa, TrFuwatari.FubiKbn.不渡2号, -1);
            }
            return true;
        }

        /// <summary>
        /// 交換日チェック
        /// </summary>
        /// <param name="oType"></param>
        /// <returns></returns>
        private bool CheckClearingDate(OpeType oType)
        {
            string msg = (oType == OpeType.登録) ? "登録" : "取消";

            // 交換日チェック
            iBicsCalendar cal = new iBicsCalendar();
            int prevBizDate = cal.getBusinessday(AplInfo.OpDate(), -5);                     // 5営業日前
            int nextBizDate = cal.getBusinessday(AplInfo.OpDate(), 1);                      // 翌営業日
            int clearingDate = DBConvert.ToIntNull(lblKoukanKibouBi.Text.Replace(".", "")); // 交換日
            if ((clearingDate < prevBizDate) || (nextBizDate < clearingDate))
            {
                ComMessageMgr.MessageWarning(string.Format("交換日が処理日の５営業日前～翌営業日以外は{0}できません。", msg));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 0号不渡を取得する
        /// </summary>
        /// <returns></returns>
        private bool GetFuwatari0()
        {
            if (!rdbFuwa0.Checked) { return true; }

            // 必須チェック（0号不渡）
            string jiyuCd = "";
            jiyuCd += lblJiyuCd1.Text;
            jiyuCd += lblJiyuCd2.Text;
            jiyuCd += lblJiyuCd3.Text;
            jiyuCd += lblJiyuCd4.Text;
            jiyuCd += lblJiyuCd5.Text;
            if (string.IsNullOrEmpty(jiyuCd))
            {
                ComMessageMgr.MessageWarning("0号不渡理由が1つも選択されていません。");
                return false;
            }

            // 重複チェック
            string[] jiyuArray = new string[] { lblJiyuCd1.Text, lblJiyuCd2.Text, lblJiyuCd3.Text, lblJiyuCd4.Text, lblJiyuCd5.Text };
            List<string> jiyuList = new List<string>();
            foreach (var jiyu in jiyuArray)
            {
                if (string.IsNullOrEmpty(jiyu))
                {
                    continue;
                }
                if (jiyuList.Contains(jiyu))
                {
                    ComMessageMgr.MessageWarning("0号不渡理由が重複しています。");
                    return false;
                }
                jiyuList.Add(jiyu);
            }
            _itemMgr.DispParams.Fuwa0 = true;
            _itemMgr.DispParams.Fuwa0JiyuCdList = jiyuList;

            return true;
        }

        /// <summary>
        /// 不渡トランザクションの項目を設定する
        /// </summary>
        /// <param name="fuwa"></param>
        /// <param name="fubiKbn"></param>
        /// <param name="jiyuCd"></param>
        private void SetFubiKbn(TBL_TRFUWATARI fuwa, int fubiKbn, int jiyuCd)
        {
            if (fuwa.m_FUBI_KBN_01 == -1)
            {
                fuwa.m_FUBI_KBN_01 = fubiKbn;
                fuwa.m_ZERO_FUBINO_01 = jiyuCd;
            }
            else if (fuwa.m_FUBI_KBN_02 == -1)
            {
                fuwa.m_FUBI_KBN_02 = fubiKbn;
                fuwa.m_ZERO_FUBINO_02 = jiyuCd;
            }
            else if (fuwa.m_FUBI_KBN_03 == -1)
            {
                fuwa.m_FUBI_KBN_03 = fubiKbn;
                fuwa.m_ZERO_FUBINO_03 = jiyuCd;
            }
            else if (fuwa.m_FUBI_KBN_04 == -1)
            {
                fuwa.m_FUBI_KBN_04 = fubiKbn;
                fuwa.m_ZERO_FUBINO_04 = jiyuCd;
            }
            else if (fuwa.m_FUBI_KBN_05 == -1)
            {
                fuwa.m_FUBI_KBN_05 = fubiKbn;
                fuwa.m_ZERO_FUBINO_05 = jiyuCd;
            }
        }


        // *******************************************************************
        // イベント
        // *******************************************************************

        /// <summary>
        /// [フォーム] ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryForm_Load(object sender, EventArgs e)
        {
            // 設定ファイル読み込みでエラー発生時
            if (this._ctl.SettingData.ChkServerIni == false)
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E00003));
                return;
            }
            if (!string.IsNullOrEmpty(this._ctl.SettingData.CheckParamMsg))
            {
                this.SetStatusMessage(string.Format(CommonClass.ComMessageMgr.E01001, this._ctl.SettingData.CheckParamMsg));
                return;
            }
            if (_itemMgr.IsMeisaiNull)
            {
                this.SetStatusMessage(CommonClass.ComMessageMgr.W00001);
                return;
            }
            if (bankStatus == EntryForm.BankStatus.他行)
            {
                // 他行の場合
                this.SetStatusMessage("持帰銀行コードが当行でないため、不渡エントリはできません。");
                return;
            }
            else if (_registStatus == RegistStatus.編集不可)
            {
                // 編集不可の場合
                this.SetStatusMessage("不渡返還登録編集不可のため、不渡エントリはできません。");
                return;
            }
        }

        /// <summary>
        /// [タブ]ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tab_MouseClick(object sender, MouseEventArgs e)
        {
            if (_ctl.IsIniErr) { return; }
            if (_itemMgr.IsMeisaiNull) { return; }

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
                if (!_curImage.HasImage)
                {
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
        /// [区分]リスト SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbKbn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ctl.IsIniErr) { return; }
            if (_itemMgr.IsMeisaiNull) { return; }

            string kbnName = "";
            switch (((Control)sender).Name)
            {
                case "cmbKbn1":
                    kbnName = ((ItemSet)cmbKbn1.SelectedItem).ItemValue;
                    _jiyuList1.Clear();
                    _jiyuList1 = GetJiyuList(kbnName);
                    cmbJiyu1.DataSource = _jiyuList1;
                    cmbJiyu1.DisplayMember = "ItemDisp";
                    cmbJiyu1.ValueMember = "ItemValue";
                    break;
                case "cmbKbn2":
                    kbnName = ((ItemSet)cmbKbn2.SelectedItem).ItemValue;
                    _jiyuList2.Clear();
                    _jiyuList2 = GetJiyuList(kbnName);
                    cmbJiyu2.DataSource = _jiyuList2;
                    cmbJiyu2.DisplayMember = "ItemDisp";
                    cmbJiyu2.ValueMember = "ItemValue";
                    break;
                case "cmbKbn3":
                    kbnName = ((ItemSet)cmbKbn3.SelectedItem).ItemValue;
                    _jiyuList3.Clear();
                    _jiyuList3 = GetJiyuList(kbnName);
                    cmbJiyu3.DataSource = _jiyuList3;
                    cmbJiyu3.DisplayMember = "ItemDisp";
                    cmbJiyu3.ValueMember = "ItemValue";
                    break;
                case "cmbKbn4":
                    kbnName = ((ItemSet)cmbKbn4.SelectedItem).ItemValue;
                    _jiyuList4.Clear();
                    _jiyuList4 = GetJiyuList(kbnName);
                    cmbJiyu4.DataSource = _jiyuList4;
                    cmbJiyu4.DisplayMember = "ItemDisp";
                    cmbJiyu4.ValueMember = "ItemValue";
                    break;
                case "cmbKbn5":
                    kbnName = ((ItemSet)cmbKbn5.SelectedItem).ItemValue;
                    _jiyuList5.Clear();
                    _jiyuList5 = GetJiyuList(kbnName);
                    cmbJiyu5.DataSource = _jiyuList5;
                    cmbJiyu5.DisplayMember = "ItemDisp";
                    cmbJiyu5.ValueMember = "ItemValue";
                    break;
            }
        }

        /// <summary>
        /// [不渡事由]リスト SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbJiyu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ctl.IsIniErr) { return; }
            if (_itemMgr.IsMeisaiNull) { return; }

            int jiyuCd = 0;
            switch (((Control)sender).Name)
            {
                case "cmbJiyu1":
                    jiyuCd = DBConvert.ToIntNull(((ItemSet)cmbJiyu1.SelectedItem).ItemValue);
                    lblJiyuCd1.Text = (jiyuCd == 0) ? "" : jiyuCd.ToString("D2");
                    break;
                case "cmbJiyu2":
                    jiyuCd = DBConvert.ToIntNull(((ItemSet)cmbJiyu2.SelectedItem).ItemValue);
                    lblJiyuCd2.Text = (jiyuCd == 0) ? "" : jiyuCd.ToString("D2");
                    break;
                case "cmbJiyu3":
                    jiyuCd = DBConvert.ToIntNull(((ItemSet)cmbJiyu3.SelectedItem).ItemValue);
                    lblJiyuCd3.Text = (jiyuCd == 0) ? "" : jiyuCd.ToString("D2");
                    break;
                case "cmbJiyu4":
                    jiyuCd = DBConvert.ToIntNull(((ItemSet)cmbJiyu4.SelectedItem).ItemValue);
                    lblJiyuCd4.Text = (jiyuCd == 0) ? "" : jiyuCd.ToString("D2");
                    break;
                case "cmbJiyu5":
                    jiyuCd = DBConvert.ToIntNull(((ItemSet)cmbJiyu5.SelectedItem).ItemValue);
                    lblJiyuCd5.Text = (jiyuCd == 0) ? "" : jiyuCd.ToString("D2");
                    break;
            }
        }

        /// <summary>
        /// [登録情報]ラジオボタン CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdbFuwa_CheckedChanged(object sender, EventArgs e)
        {
            if (_ctl.IsIniErr) { return; }
            if (_itemMgr.IsMeisaiNull) { return; }

            RefreshDisplayState();
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
                // 終了
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了", 1);
                this.Close();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F4：クリア
        /// </summary>
        protected override void btnFunc04_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                // クリア
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "クリア", 1);

                // 画面表示データ更新
                rdbFuwa0.Checked = false;
                cmbKbn1.SelectedValue = "";
                cmbKbn2.SelectedValue = "";
                cmbKbn3.SelectedValue = "";
                cmbKbn4.SelectedValue = "";
                cmbKbn5.SelectedValue = "";
                cmbJiyu1.SelectedValue = "";
                cmbJiyu2.SelectedValue = "";
                cmbJiyu3.SelectedValue = "";
                cmbJiyu4.SelectedValue = "";
                cmbJiyu5.SelectedValue = "";
                rdbFuwa1.Checked = false;
                rdbFuwa2.Checked = false;

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
        /// F5：拡大
        /// </summary>
        protected override void btnFunc05_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (_imgHandler.HasImage == false) { return; }

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
                SetFunctionState();
            }
        }

        /// <summary>
        /// F6：縮小
        /// </summary>
        protected override void btnFunc06_Click(object sender, EventArgs e)
        {
            this.ClearStatusMessage();
            try
            {
                if (_imgHandler.HasImage == false) { return; }

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
                SetFunctionState();
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
                if (_imgHandler.HasImage == false) { return; }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "左表示回転", 1);

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
                SetFunctionState();
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
                if (_imgHandler.HasImage == false) { return; }

                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "右表示回転", 1);

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
                SetFunctionState();
            }
        }

        /// <summary>
        /// F9：取消
        /// </summary>
        protected override void btnFunc09_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                // 取消
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "取消", 1);

                // 不渡トランザクション更新
                if (!SetFuwatariData(OpeType.取消))
                {
                    return;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// F12：登録
        /// </summary>
        protected override void btnFunc12_Click(object sender, EventArgs e)
        {
            // ボタン連打回避
            if (this.IsProcessing()) return;

            this.ClearStatusMessage();
            try
            {
                // 登録
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), "登録", 1);

                // 不渡トランザクション更新
                if (!SetFuwatariData(OpeType.登録))
                {
                    return;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                this.SetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message));
            }
        }

        /// <summary>
        /// 不渡トランザクションを更新する
        /// </summary>
        /// <param name="oType"></param>
        private bool SetFuwatariData(OpeType oType)
        {
            TBL_TRFUWATARI fuwa = _itemMgr.fuwatari;
            TBL_TRMEI trmei = _itemMgr.trmei;
            MethodType mType = MethodType.更新;

            // 入力状態
            switch (oType)
            {
                case OpeType.取消:
                    if (!SetCancelData(oType, ref mType)) { return false; }
                    break;
                case OpeType.登録:
                    if (!SetRegistData(oType, ref mType)) { return false; }
                    break;
            }

            // オペレーター情報設定
            SetOperationData();

            // DB更新
            if (!_itemMgr.RegistTrFuwatari(fuwa, mType))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 不渡返還取消情報を設定する
        /// </summary>
        /// <param name="oType"></param>
        /// <param name="mType"></param>
        /// <returns></returns>
        private bool SetCancelData(OpeType oType, ref MethodType mType)
        {
            TBL_TRFUWATARI fuwa = _itemMgr.fuwatari;
            TBL_TRMEI trmei = _itemMgr.trmei;

            // 交換日チェック
            if (!CheckClearingDate(oType))
            {
                return false;
            }

            // 確認メッセージ
            DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, "不渡を取消してよろしいですか？");
            if (res != DialogResult.OK)
            {
                return false;
            }

            // DELETE or UPDATE
            if (trmei.m_GRA_CONFIRMDATE == 0)
            {
                // 物理削除
                mType = MethodType.削除;
                // 物理削除時はアップロード状態を未作成に設定
                _itemMgr.trmei.m_GRA_STS = TrMei.Sts.未作成;
            }
            else
            {
                // 論理削除（電子交換所へのアップロード対象）
                mType = MethodType.更新;

                // 再作成対象
                if ((_itemMgr.trmei.m_GRA_STS == TrMei.Sts.結果エラー) ||
                    (_itemMgr.trmei.m_GRA_STS == TrMei.Sts.結果正常))
                {
                    _itemMgr.trmei.m_GRA_STS = TrMei.Sts.再作成対象;
                }
            }

            // 入力項目は更新しない
            fuwa.m_DELETE_DATE = AplInfo.OpDate();
            fuwa.m_DELETE_FLG = 1;
            return true;
        }

        /// <summary>
        /// 不渡返還登録情報を設定する
        /// </summary>
        /// <param name="oType"></param>
        /// <param name="mType"></param>
        /// <returns></returns>
        private bool SetRegistData(OpeType oType, ref MethodType mType)
        {
            TBL_TRFUWATARI fuwa = _itemMgr.fuwatari;
            TBL_TRMEI trmei = _itemMgr.trmei;

            // 交換日チェック
            if (!CheckClearingDate(oType))
            {
                return false;
            }

            // 画面項目取得
            if (!GetDisplayParams())
            {
                return false;
            }

            // 確認メッセージ
            DialogResult res = ComMessageMgr.MessageQuestion(MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button2, "不渡を登録してよろしいですか？");
            if (res != DialogResult.OK)
            {
                return false;
            }

            // INSERT or UPDATE
            if (_registStatus == RegistStatus.未登録)
            {
                mType = MethodType.登録;
            }
            else
            {
                mType = MethodType.更新;

                // 再作成対象
                if ((_itemMgr.trmei.m_GRA_STS == TrMei.Sts.結果エラー) ||
                    (_itemMgr.trmei.m_GRA_STS == TrMei.Sts.結果正常))
                {
                    _itemMgr.trmei.m_GRA_STS = TrMei.Sts.再作成対象;
                }
            }

            // 取消されてたら復活する
            fuwa.m_DELETE_DATE = 0;
            fuwa.m_DELETE_FLG = 0;
            return true;
        }

        /// <summary>
        /// オペレーター情報を設定する
        /// </summary>
        private void SetOperationData()
        {
            TBL_TRFUWATARI fuwa = _itemMgr.fuwatari;

            // 入力開始時からの経過時間を計算
            TimeSpan ts = DateTime.Now - _startTime;
            int addMilSecond = DBConvert.ToIntNull(Math.Round(ts.TotalSeconds * 1000));
            if (fuwa.m_E_YMD == 0)
            {
                // 初回のみ初期化
                fuwa.m_E_YMD = AplInfo.OpDate();
            }
            if (!AplInfo.OP_ID.Equals(fuwa.m_E_OPENO))
            {
                // オペレーターが変わったら初期化
                fuwa.m_E_OPENO = AplInfo.OP_ID;
                fuwa.m_E_TIME = addMilSecond;
            }
            else
            {
                // 同一オペレーターは時間加算
                fuwa.m_E_TIME += addMilSecond;
            }
            fuwa.m_E_TERM = AplInfo.TermNo;
        }

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 区分リストを取得する
        /// </summary>
        /// <returns></returns>
        private List<ItemSet> GetKbnList()
        {
            List<ItemSet> kbnList = new List<ItemSet>();
            ItemSet fstItem = new ItemSet("", "");
            kbnList.Add(fstItem);
            foreach (DataRow row in _itemMgr.tblFuwatari.Rows)
            {
                TBL_FUWATARIMF data = new TBL_FUWATARIMF(row);
                var kbns = kbnList.Where(p => p.ItemDisp == data.m_KBN_NAME);
                if (kbns.Count() > 0) { continue; }
                ItemSet item = new ItemSet(data.m_KBN_NAME, data.m_KBN_NAME);
                kbnList.Add(item);
            }
            return kbnList;
        }

        /// <summary>
        /// 不渡事由リストを取得する
        /// </summary>
        /// <returns></returns>
        private List<ItemSet> GetJiyuList(string kbnName)
        {
            List<ItemSet> jiyuList = new List<ItemSet>();
            ItemSet fstItem = new ItemSet("", "");
            jiyuList.Add(fstItem);

            // 区分リストに対応する不渡事由を取得する
            string filter = string.Format("KBN_NAME='{0}'", kbnName);
            string sort = "CODE";
            DataRow[] rows = _itemMgr.tblFuwatari.Select(filter, sort);
            foreach (DataRow row in rows)
            {
                TBL_FUWATARIMF data = new TBL_FUWATARIMF(row);
                ItemSet item = new ItemSet(data._CODE.ToString(), data.m_FUBI_JIYUU);
                jiyuList.Add(item);
            }
            return jiyuList;
        }

        /// <summary>
        /// 不渡トランザクションの値を取得する
        /// </summary>
        /// <param name="col"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        private string GetFuwatariKbnJiyu(int col, int no)
        {
            // col=1 ：FUWATARIMF.KBN_NAME を取得
            // col=2 ：FUWATARIMF.FUBI_JIYUU を取得

            string retVal = "";
            switch (no)
            {
                case FUBI_KBN_1:
                    if (_itemMgr.fuwatari.m_FUBI_KBN_01 == TrFuwatari.FubiKbn.不渡0号)
                    {
                        retVal = GetFuwatariMstValue(col, _itemMgr.fuwatari.m_ZERO_FUBINO_01);
                    }
                    break;
                case FUBI_KBN_2:
                    if (_itemMgr.fuwatari.m_FUBI_KBN_02 == TrFuwatari.FubiKbn.不渡0号)
                    {
                        retVal = GetFuwatariMstValue(col, _itemMgr.fuwatari.m_ZERO_FUBINO_02);
                    }
                    break;
                case FUBI_KBN_3:
                    if (_itemMgr.fuwatari.m_FUBI_KBN_03 == TrFuwatari.FubiKbn.不渡0号)
                    {
                        retVal = GetFuwatariMstValue(col, _itemMgr.fuwatari.m_ZERO_FUBINO_03);
                    }
                    break;
                case FUBI_KBN_4:
                    if (_itemMgr.fuwatari.m_FUBI_KBN_04 == TrFuwatari.FubiKbn.不渡0号)
                    {
                        retVal = GetFuwatariMstValue(col, _itemMgr.fuwatari.m_ZERO_FUBINO_04);
                    }
                    break;
                case FUBI_KBN_5:
                    if (_itemMgr.fuwatari.m_FUBI_KBN_05 == TrFuwatari.FubiKbn.不渡0号)
                    {
                        retVal = GetFuwatariMstValue(col, _itemMgr.fuwatari.m_ZERO_FUBINO_05);
                    }
                    break;
            }
            return retVal;
        }

        /// <summary>
        /// 不渡事由コードマスタの名称を取得する
        /// </summary>
        /// <param name="col"></param>
        /// <param name="cd"></param>
        /// <returns></returns>
        private string GetFuwatariMstValue(int col, int cd)
        {
            string filter = string.Format("CODE={0}", cd);
            DataRow[] rows = _itemMgr.tblFuwatari.Select(filter);
            if (rows.Length > 0)
            {
                TBL_FUWATARIMF data = new TBL_FUWATARIMF(rows[0]);
                switch (col)
                {
                    case 1:
                        return data.m_KBN_NAME;
                    case 2:
                        return data._CODE.ToString();
                }
            }
            return "";
        }

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
        /// 日付取得
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private string GetItemDateValue(int itemid)
        {
            if (!_itemMgr.tritems.ContainsKey(itemid)) { return ""; }
            int nVal = DBConvert.ToIntNull(_itemMgr.tritems[itemid].m_END_DATA);
            if (nVal == 0) { return ""; }
            return CommonUtil.ConvToDateFormat(nVal, 3);
        }

        /// <summary>
        /// 数値取得
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private int GetItemIntVal(int itemid)
        {
            if (!_itemMgr.tritems.ContainsKey(itemid)) { return 0; }
            return DBConvert.ToIntNull(_itemMgr.tritems[itemid].m_END_DATA);
        }

        /// <summary>
        /// 数値取得
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private long GetItemLongVal(int itemid)
        {
            if (!_itemMgr.tritems.ContainsKey(itemid)) { return 0; }
            return DBConvert.ToLongNull(_itemMgr.tritems[itemid].m_END_DATA);
        }

        /// <summary>
        /// 文字列取得
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private string GetItemStrVal(int itemid)
        {
            if (!_itemMgr.tritems.ContainsKey(itemid)) { return ""; }
            return DBConvert.ToStringNull(_itemMgr.tritems[itemid].m_END_DATA);
        }

        /// <summary>
        /// パディング数値取得
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="paddingFormat"></param>
        /// <returns></returns>
        private string GetItemPadNumber(int itemid, string paddingFormat)
        {
            int nVal = GetItemIntVal(itemid);
            if (nVal == 0) { return ""; }
            return nVal.ToString(paddingFormat);
        }

        /// <summary>
        /// パディング数値取得
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        /// <remarks>DSP_ITEMから長さを取得して加工</remarks>
        private string GetItemPadNumberDspItem(int itemid)
        {
            int ItemLength = _ctl.GetDSPItemLen(itemid);
            long nVal = GetItemLongVal(itemid);
            if (nVal == 0) { return ""; }
            return CommonUtil.PadLeft(nVal.ToString(), ItemLength, "0");
        }

        /// <summary>
        /// 金額取得
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private string GetItemMoney(int itemid)
        {
            if (!_itemMgr.tritems.ContainsKey(itemid)) { return "0"; }
            return string.Format("{0:#,##0}", DBConvert.ToLongNull(_itemMgr.tritems[itemid].m_END_DATA));
        }

        /// <summary>
        /// 不渡登録日取得
        /// </summary>
        /// <returns></returns>
        private string GetFuwatariBi()
        {
            if (_itemMgr.fuwatari.m_E_YMD.ToString().Length != 8) { return ""; }
            return CommonUtil.ConvToDateFormat(_itemMgr.fuwatari.m_E_YMD, 3);
        }

        /// <summary>
        /// 取消登録日取得
        /// </summary>
        /// <returns></returns>
        private string GetCancelDate()
        {
            if (_itemMgr.fuwatari.m_DELETE_DATE.ToString().Length != 8) { return ""; }
            return CommonUtil.ConvToDateFormat(_itemMgr.fuwatari.m_DELETE_DATE, 3);
        }

        /// <summary>
        /// 取消取得
        /// </summary>
        /// <returns></returns>
        private string GetCancelFlg()
        {
            return DBConvert.ToBoolNull(_itemMgr.fuwatari.m_DELETE_FLG) ? "●" : "";
        }

        /// <summary>
        /// イメージファイル名を取得
        /// </summary>
        private string GetImgFileName(int ImgKbn)
        {
            if (!_itemMgr.trimges.ContainsKey(ImgKbn)) { return string.Empty; }

            // イメージファイル名を取得
            return _itemMgr.trimges[ImgKbn].m_IMG_FLNM;
        }

        // *******************************************************************
        // 内部メソッド（イメージ関連）
        // *******************************************************************

        /// <summary>
        /// 画面コントロール描画
        /// </summary>
        private void MakeView(ItemManager.ImageInfo imgInfo)
        {
            if (!_itemMgr.trimges.ContainsKey(imgInfo.ImgKbn)) { return; }

            // コントロール描画中断
            this.SuspendLayout();

            // 最初にコントロールを削除する
            this.RemoveImgControl(_imgHandler);

            // イメージコントロール作成
            TBL_TRMEIIMG img = _itemMgr.trimges[imgInfo.ImgKbn];
            _imgHandler = new ImageHandler(_ctl);
            _imgHandler.InitializePanelSize(pnlImage.Width, pnlImage.Height);
            _imgHandler.CreateImageControl(_itemMgr.trbat, img, _itemMgr.imgparam, true);

            // コントロール、コントロールのイベント設定
            this.PutImgControl(_imgHandler);

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

        public class ItemSet
        {
            public string ItemValue { get; set; }
            public string ItemDisp { get; set; }
            public ItemSet(string value, string disp)
            {
                ItemValue = value;
                ItemDisp = disp;
            }
        }
    }
}
