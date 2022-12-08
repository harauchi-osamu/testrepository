using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryClass;
using EntryCommon;
using ImageController;
using SubRoutineApi;

namespace CorrectInput
{
    public class EntryController : IDisposable
    {
        protected Controller _ctl;
        protected ItemManager _itemMgr = null;
        public EntryCommonFormBase _parent { get; private set; }

        protected EntryController _econ { get { return _itemMgr.EntController; } private set { _itemMgr.EntController = value; } }
        protected EntryDspControl _dcon { get { return _itemMgr.DspControl; } private set { _itemMgr.DspControl = value; } }
        protected EntryImageHandler eiHandler { get { return _itemMgr.ImageHandler; } private set { _itemMgr.ImageHandler = value; } }
        protected EntryInputChecker eiChecker { get { return _itemMgr.Checker; } private set { _itemMgr.Checker = value; } }
        protected EntryDataUpdater edUpdater { get { return _itemMgr.Updater; } private set { _itemMgr.Updater = value; } }
        protected MeisaiInfo _curMei { get { return _itemMgr.CurBat.CurMei; } }

        // エントリ画面
        private EntryFormBase _form = null;

        public bool KeydownFlg { get; set; } = false;
        public bool IsForward { get; set; } = false;

        /// <summary>自動エントリ結果</summary>
        public enum AutoResult
        {
            NextAuto,
            NextDsp,
            End,
        }


        // *******************************************************************
        // プロパティ
        // *******************************************************************

        public bool IsStopTextChanged { get; set; } = false;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EntryController(Controller ctl, EntryCommonFormBase form)
        {
            _ctl = ctl;
            _itemMgr = (ItemManager)_ctl.ItemMgr;
            _parent = form;

            _dcon = new EntryDspControl(_ctl);
            eiHandler = new EntryImageHandler(_ctl);
            eiChecker = new EntryInputChecker(_ctl);
            edUpdater = new EntryDataUpdater(_ctl);
        }

        /// <summary>
        /// リソース開放
        /// </summary>
        public void Dispose()
        {
            if (_form != null)
            {
                _form.Dispose();
                _form = null;
            }
        }


        // *******************************************************************
        // 内部メソッド（起動処理）
        // *******************************************************************

        /// <summary>
        /// エントリ画面を設定する
        /// </summary>
        public void SetEntryForm(BatchListForm.ExecMode execMode)
        {
            switch (execMode)
            {
                case BatchListForm.ExecMode.FuncEnt:
                    _form = new EntryForm();
                    break;
                case BatchListForm.ExecMode.FuncVfy:
                    _form = new VerifyForm();
                    break;
                case BatchListForm.ExecMode.Teisei:
                    _form = new TeiseiForm();
                    break;
            }
            _form.InitializeForm(_ctl);
        }

        /// <summary>
        /// 処理開始
        /// </summary>
        public virtual void StartControl(MeisaiInfo mei)
        {
            try
            {
                if (_parent != null) { _parent.Cursor = Cursors.WaitCursor; }

                // フォーム起動
                _form.StartControl(this, mei);
            }
            catch (Exception ex)
            {
                ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                ParentSetStatusMessage(string.Format(ComMessageMgr.E00004, ex.Message), Color.Salmon);
            }
            finally
            {
                if (_parent != null) { _parent.Cursor = Cursors.Default; }
            }
        }

        /// <summary>
        /// モデル作成
        /// </summary>
        /// <param name="meiNext"></param>
        /// <returns></returns>
        public bool MakeModel(MeisaiInfo meiNext)
        {
            // 明細シーケンス設定
            SetNextSequence(meiNext);

            // アイテム取得
            _itemMgr.FetchTrItems(meiNext);

            // 画面コントロール作成
            _dcon.CreateDspItems();

            // 既存のデータをセット
            _dcon.SetFormData(meiNext);

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了", 1);
            return true;
        }

        /// <summary>
        /// 次の明細のシーケンス番号を設定する
        /// </summary>
        /// <param name="meiNext"></param>
        public void SetNextSequence(MeisaiInfo meiNext)
        {
            // 依頼書切替を検出
            string nextBatKey = CommonUtil.GenerateKey(meiNext._GYM_ID, meiNext._OPERATION_DATE, meiNext._SCAN_TERM, meiNext._BAT_ID);
            string nextMeiKey = CommonUtil.GenerateKey(meiNext._GYM_ID, meiNext._OPERATION_DATE, meiNext._SCAN_TERM, meiNext._BAT_ID, meiNext._DETAILS_NO);
            if (!_itemMgr.DspParams.PrevBatKey.Equals(nextBatKey))
            {
                // バッチ切替
                // フラグクリア
                _itemMgr.EntParams.SetNewBatch();
                _itemMgr.DspParams.PrevBatKey = nextBatKey;
                _itemMgr.DspParams.PrevMeiKey = nextMeiKey;
            }
            else if (!_itemMgr.DspParams.PrevMeiKey.Equals(nextMeiKey))
            {
                // 明細切替
                // フラグクリア
                _itemMgr.EntParams.SetNewMeisai();
                _itemMgr.DspParams.PrevMeiKey = nextMeiKey;
            }
            //_itemMgr.CurBat.CurMeiSeq = meiNext.Seq;
        }

        /// <summary>
        /// 画面コントロール描画
        /// </summary>
        /// <returns></returns>
        public bool MakeView(MeisaiInfo meiNext)
        {
            // コントロールを設定する（フォームリセット前）
            _ctl.SetControlForm(_ctl, _form);

            // コントロール描画中断
            _form.SuspendLayout();

            // 画像イメージ情報取得
            if (!_itemMgr.EntParams.IsInitImage)
            {
                // イメージコントロール作成
                eiHandler.InitializeImagePanel();
                if (!eiHandler.CreateImageControl(meiNext, _curMei.CurImg._IMG_KBN))
                {
                    return false;
                }
            }

            // フォームリセット
            _form.ResetForm();

            // コントロール描画再開
            _form.ResumeLayout();

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了", 1);
            return true;
        }

        /// <summary>
        /// 自動エントリ
        /// </summary>
        /// <returns></returns>
        public AutoResult AutoEntry(EntryFormBase form, MeisaiInfo curMei, EntryFormBase.DataType type)
        {
            // AutoSkip確認
            // (自動エントリ確認箇所を別メソッド化)
            AutoResult ChkAuto = ChkAutoEntry(curMei, type);
            if (ChkAuto == AutoResult.NextDsp)
            {
                // AutoSkip対象外
                return AutoResult.NextDsp;
            }

            // AutoSkip処理実施
            try
            {
                // 親フォームにAutoSkip処理中メッセージ表示
                // Application.DoEvents()を使用するため、親フォームのDisable制御は必須
                string AutoSkipMsg = string.Empty;
                switch (curMei._GYM_ID)
                {
                    case GymParam.GymId.持出:
                        AutoSkipMsg = string.Format("明細自動確定中．．． (取込日:{0} バッチ番号:{1} 明細番号:{2})",
                                                     CommonUtil.ConvToDateFormat(curMei._OPERATION_DATE, 3), curMei._BAT_ID.ToString(Const.BAT_ID_LEN_STR), curMei._DETAILS_NO.ToString(Const.DETAILS_NO_LEN_STR));
                        break;
                    case GymParam.GymId.持帰:
                        AutoSkipMsg = string.Format("明細自動確定中．．． (取込日:{0} 明細番号:{1})",
                                                     CommonUtil.ConvToDateFormat(curMei._OPERATION_DATE, 3), curMei._DETAILS_NO.ToString(Const.DETAILS_NO_LEN_STR));
                        break;
                }
                ParentSetStatusMessage(AutoSkipMsg, SystemColors.Control, true);

                // 自動エントリ時はフォームを非表示にする
                //form.Visible = false;

                // 自動データ検証
                if (_itemMgr.DspParams.IsEntryExec)
                {
                    // エントリ
                    if (!AutoEntrySetENT(curMei))
                    {
                        // エントリ画面表示（自動エントリＮＧ）
                        // 自動エントリをOFFにして手入力モードにする
                        _itemMgr.EntParams.IsAutoEntryExec = false;
                        return AutoResult.NextDsp;
                    }
                }
                else if (_itemMgr.DspParams.IsVerifyExec)
                {
                    // ベリファイ
                    if (!AutoEntrySetVFY(curMei))
                    {
                        // エントリ画面表示（自動ベリファイＮＧ）
                        // 自動エントリをOFFにして手入力モードにする
                        _itemMgr.EntParams.IsAutoVerifyExec = false;
                        return AutoResult.NextDsp;
                    }
                }

                // 自動エントリ成功時はフォームを非表示にする
                if (form.Visible)
                {
                    // エントリー画面表示時のみ非表示設定
                    form.Visible = false;
                    // 親画面が裏に隠れるため前面表示
                    if (_parent != null)
                    {
                        _parent.TopMost = true;
                        _parent.TopMost = false;
                    }
                }

                // 明細確定
                _itemMgr.EntParams.ExecFunc = EntryFormBase.FuncType.確定;
                if (!MeisaiAutoFix(curMei))
                {
                    // 処理終了
                    return AutoResult.End;
                }

                // プルーフＮＧ
                if (_itemMgr.EntParams.IsBatchEnd)
                {
                    // 処理終了(プルーフ画面で中断)
                    return AutoResult.End;
                }

                // 自動配信しない場合は終了
                if (!_itemMgr.DspParams.IsAutoReceiveBatch)
                {
                    ComMessageMgr.MessageInformation("選択明細の補正入力が完了しました。");
                    return AutoResult.End;
                }

                // 次明細を処理（自動エンベリＯＫ）
                return AutoResult.NextAuto;
            }
            finally
            {
                // 親フォームのメッセージクリア表示
                // (ClearStatusMessageがprotectedで使用できないので空文字設定)
                ParentSetStatusMessage(string.Empty, SystemColors.Control);
            }
        }

        /// <summary>
        /// 自動エントリ確認
        /// </summary>
        /// <returns>
        /// 現明細がAutoSkip対象の場合、NextAuto
        /// AutoSkip対象ではない場合、NextDsp
        /// </returns>
        public AutoResult ChkAutoEntry(MeisaiInfo curMei, EntryFormBase.DataType type)
        {
            _itemMgr.EntParams.IsAutoEntryExec = false;
            _itemMgr.EntParams.IsAutoVerifyExec = false;

            if (type == EntryFormBase.DataType.Cur)
            {
                // 画面再描画の場合、自動エントリー処理無効(画面種類選択での画面再描画等)
                return AutoResult.NextDsp;
            }

            // 自動エントリ実施判定
            bool canAutoEntry = false;
            if (_ctl.IsKanryouTeisei)
            {
                // 完了訂正は自動エントリしない
                canAutoEntry = false;
            }
            else if (_itemMgr.DspParams.IsEntryExec && DBConvert.ToBoolNull(curMei.CurDsp.hosei_param.m_AUTO_SKIP_MODE_ENT))
            {
                // 自動エントリする
                canAutoEntry = true;
                _itemMgr.EntParams.IsAutoEntryExec = true;
            }
            else if (_itemMgr.DspParams.IsVerifyExec && DBConvert.ToBoolNull(curMei.CurDsp.hosei_param.m_AUTO_SKIP_MODE_VFY))
            {
                // 自動ベリファイする
                canAutoEntry = true;
                _itemMgr.EntParams.IsAutoVerifyExec = true;
            }
            if (!canAutoEntry)
            {
                // AutoSkip対象外の場合
                return AutoResult.NextDsp;
            }

            // AutoSkip対象
            return AutoResult.NextAuto;
        }

        /// <summary>
        /// 自動エントリを実施する
        /// </summary>
        /// <returns></returns>
        private bool AutoEntrySetENT(MeisaiInfo curMei)
        {
            // 明細開始処理（処理時間開始）
            edUpdater.UpdateMeisaiToStart();

            // OCR項目の読替処理のため各種設定値を取得
            string icBankCd = "";
            string billCd = "";
            string tegataCd = "";
            string clearingDate = "";
            string kenIcBrCd = "";
            string kenKozaNo = "";
            foreach (TBL_TRITEM item in curMei.tritems.Values)
            {
                TBL_DSP_ITEM di = _econ.GetDispItem(item._ITEM_ID);
                if (di == null) { continue; }
                string chkVal = item.m_OCR_ENT_DATA;

                // 表示項目の読替処理
                switch (di._ITEM_ID)
                {
                    case DspItem.ItemId.券面持帰銀行コード:
                        icBankCd = chkVal;
                        break;
                    case DspItem.ItemId.交換証券種類コード:
                        billCd = chkVal;
                        break;
                    case DspItem.ItemId.手形種類コード:
                        tegataCd = chkVal;
                        break;
                    case DspItem.ItemId.入力交換希望日:
                        clearingDate = chkVal;
                        break;
                    case DspItem.ItemId.券面持帰支店コード:
                        kenIcBrCd = chkVal;
                        break;
                    case DspItem.ItemId.券面口座番号:
                        kenKozaNo = chkVal;
                        break;
                }
            }

            int BknoLength = CommonUtil.GetDBItemLength(DspItem.ItemId.持帰銀行コード, Const.BANK_NO_LEN);
            int KouzaLength = CommonUtil.GetDBItemLength(DspItem.ItemId.口座番号, Const.KOZA_NO_LEN); ;
            int BrnoLength = CommonUtil.GetDBItemLength(DspItem.ItemId.持帰支店コード, Const.BR_NO_LEN);
            // OCR項目の読替処理(自動チェック向けにOCR箇所の読替を実施)
            // DB格納の長さで設定
            EntryReplacer er = _ctl.GetEntReplacer();
            {
                // 券面持帰銀行コード
                string sBkCd = "";
                string sBkName = "";
                if (!string.IsNullOrEmpty(icBankCd))
                {
                    er.ReplaceBankCd(icBankCd, BknoLength, ref sBkCd, ref sBkName);
                    SetItemOCRENT(curMei, DspItem.ItemId.持帰銀行コード, sBkCd);
                    SetItemOCRENT(curMei, DspItem.ItemId.持帰銀行名, sBkName);
                }

                // 交換証券種類名
                string sBillName = er.GetBillName(DBConvert.ToIntNull(billCd));
                SetItemOCRENT(curMei, DspItem.ItemId.交換証券種類名, sBillName);

                // 手形種類名
                string sTegataName = er.GetTegataName(DBConvert.ToIntNull(tegataCd));
                SetItemOCRENT(curMei, DspItem.ItemId.手形種類名, sTegataName);

                // 入力交換希望日
                string sWarekiDate = "";
                string sBusinessDate = "";
                er.ReplaceClearingDateOrg(clearingDate, ref sWarekiDate, ref sBusinessDate);
                SetItemOCRENT(curMei, DspItem.ItemId.和暦交換希望日, sWarekiDate);
                SetItemOCRENT(curMei, DspItem.ItemId.交換日, sBusinessDate);

                // 券面持帰支店コード
                string sIcBrCd = "";
                string sIcBrName = "";
                string sKozaNo = "";
                string sPayer = "";
                er.ReplaceBrName(kenIcBrCd, kenKozaNo, KouzaLength, BrnoLength, ref sIcBrCd, ref sIcBrName, ref sKozaNo, ref sPayer);
                SetItemOCRENT(curMei, DspItem.ItemId.持帰支店コード, sIcBrCd);
                SetItemOCRENT(curMei, DspItem.ItemId.持帰支店名, sIcBrName);
                SetItemOCRENT(curMei, DspItem.ItemId.口座番号, sKozaNo);
                SetItemOCRENT(curMei, DspItem.ItemId.支払人名, sPayer);
            }

            // エントリ値のデータ検証を行う
            foreach (TBL_TRITEM item in curMei.tritems.Values)
            {
                TBL_DSP_ITEM di = _econ.GetDispItem(item._ITEM_ID);
                if (di == null) { continue; }
                if (_econ.IsNotRegistItem(item._ITEM_ID)) { continue; }

                // ITEM_TYPE 検証(画面表示桁数に変換して確認)
                string chkVal = CommonUtil.EditDspItem(item.m_OCR_ENT_DATA, di);
                if (!eiChecker.ValidateItemType(di, chkVal))
                {
                    // 検証NG
                    return false;
                }

                // サブルーチン検証
                if (!eiChecker.ValidateSubRoutine(di, curMei, SubRoutine.CheckValue.OCR_ENT_DATA))
                {
                    // 検証NG
                    return false;
                }

                // データ加工
                item.m_ENT_DATA = CommonUtil.EditTrDataItem(chkVal, di);

                // 最終工程
                if (_itemMgr.IsLastProcess)
                {
                    item.m_END_DATA = item.m_ENT_DATA;
                }
            }

            // 表示項目の読替処理
            {
                //EntryReplacer er = new EntryReplacer();

                // 券面持帰銀行コード
                string sBkCd = "";
                string sBkName = "";
                er.ReplaceBankCd(icBankCd, BknoLength, ref sBkCd, ref sBkName);
                SetItemENT(curMei, DspItem.ItemId.持帰銀行コード, sBkCd);
                SetItemENT(curMei, DspItem.ItemId.持帰銀行名, sBkName);

                // 交換証券種類名
                string sBillName = er.GetBillName(DBConvert.ToIntNull(billCd));
                SetItemENT(curMei, DspItem.ItemId.交換証券種類名, sBillName);

                // 手形種類名
                string sTegataName = er.GetTegataName(DBConvert.ToIntNull(tegataCd));
                SetItemENT(curMei, DspItem.ItemId.手形種類名, sTegataName);

                // 入力交換希望日
                string sWarekiDate = "";
                string sBusinessDate = "";
                er.ReplaceClearingDateOrg(clearingDate, ref sWarekiDate, ref sBusinessDate);
                SetItemENT(curMei, DspItem.ItemId.和暦交換希望日, sWarekiDate);
                SetItemENT(curMei, DspItem.ItemId.交換日, sBusinessDate);

                // 券面持帰支店コード
                string sIcBrCd = "";
                string sIcBrName = "";
                string sKozaNo = "";
                string sPayer = "";
                er.ReplaceBrName(kenIcBrCd, kenKozaNo, KouzaLength, BrnoLength, ref sIcBrCd, ref sIcBrName, ref sKozaNo, ref sPayer);
                SetItemENT(curMei, DspItem.ItemId.持帰支店コード, sIcBrCd);
                SetItemENT(curMei, DspItem.ItemId.持帰支店名, sIcBrName);
                SetItemENT(curMei, DspItem.ItemId.口座番号, sKozaNo);
                SetItemENT(curMei, DspItem.ItemId.支払人名, sPayer);
            }

            // 検証OK
            return true;
        }

        /// <summary>
        /// 自動ベリファイを実施する
        /// </summary>
        /// <returns></returns>
        private bool AutoEntrySetVFY(MeisaiInfo curMei)
        {
            // 明細開始処理（処理時間開始）
            edUpdater.UpdateMeisaiToStart();

            // OCR項目の読替処理のため各種設定値を取得
            string icBankCd = "";
            string billCd = "";
            string tegataCd = "";
            string clearingDate = "";
            string kenIcBrCd = "";
            string kenKozaNo = "";
            foreach (TBL_TRITEM item in curMei.tritems.Values)
            {
                TBL_DSP_ITEM di = _econ.GetDispItem(item._ITEM_ID);
                if (di == null) { continue; }
                string chkVal = item.m_OCR_VFY_DATA;

                // 表示項目の読替処理
                switch (di._ITEM_ID)
                {
                    case DspItem.ItemId.券面持帰銀行コード:
                        icBankCd = chkVal;
                        break;
                    case DspItem.ItemId.交換証券種類コード:
                        billCd = chkVal;
                        break;
                    case DspItem.ItemId.手形種類コード:
                        tegataCd = chkVal;
                        break;
                    case DspItem.ItemId.入力交換希望日:
                        clearingDate = chkVal;
                        break;
                    case DspItem.ItemId.券面持帰支店コード:
                        kenIcBrCd = chkVal;
                        break;
                    case DspItem.ItemId.券面口座番号:
                        kenKozaNo = chkVal;
                        break;
                }
            }

            int BknoLength = CommonUtil.GetDBItemLength(DspItem.ItemId.持帰銀行コード, Const.BANK_NO_LEN);
            int KouzaLength = CommonUtil.GetDBItemLength(DspItem.ItemId.口座番号, Const.KOZA_NO_LEN); ;
            int BrnoLength = CommonUtil.GetDBItemLength(DspItem.ItemId.持帰支店コード, Const.BR_NO_LEN);
            // OCR項目の読替処理(自動チェック向けにOCR箇所の読替を実施)
            // DB格納の長さで設定
            EntryReplacer er = _ctl.GetEntReplacer();
            {
                // 券面持帰銀行コード
                string sBkCd = "";
                string sBkName = "";
                if (!string.IsNullOrEmpty(icBankCd))
                {
                    er.ReplaceBankCd(icBankCd, BknoLength, ref sBkCd, ref sBkName);
                    SetItemOCRVFY(curMei, DspItem.ItemId.持帰銀行コード, sBkCd);
                    SetItemOCRVFY(curMei, DspItem.ItemId.持帰銀行名, sBkName);
                }

                // 交換証券種類名
                string sBillName = er.GetBillName(DBConvert.ToIntNull(billCd));
                SetItemOCRVFY(curMei, DspItem.ItemId.交換証券種類名, sBillName);

                // 手形種類名
                string sTegataName = er.GetTegataName(DBConvert.ToIntNull(tegataCd));
                SetItemOCRVFY(curMei, DspItem.ItemId.手形種類名, sTegataName);

                // 入力交換希望日
                string sWarekiDate = "";
                string sBusinessDate = "";
                er.ReplaceClearingDateOrg(clearingDate, ref sWarekiDate, ref sBusinessDate);
                SetItemOCRVFY(curMei, DspItem.ItemId.和暦交換希望日, sWarekiDate);
                SetItemOCRVFY(curMei, DspItem.ItemId.交換日, sBusinessDate);

                // 券面持帰支店コード
                string sIcBrCd = "";
                string sIcBrName = "";
                string sKozaNo = "";
                string sPayer = "";
                er.ReplaceBrName(kenIcBrCd, kenKozaNo, KouzaLength, BrnoLength, ref sIcBrCd, ref sIcBrName, ref sKozaNo, ref sPayer);
                SetItemOCRVFY(curMei, DspItem.ItemId.持帰支店コード, sIcBrCd);
                SetItemOCRVFY(curMei, DspItem.ItemId.持帰支店名, sIcBrName);
                SetItemOCRVFY(curMei, DspItem.ItemId.口座番号, sKozaNo);
                SetItemOCRVFY(curMei, DspItem.ItemId.支払人名, sPayer);
            }

            // ベリファイ値のデータ検証を行う
            foreach (TBL_TRITEM item in curMei.tritems.Values)
            {
                TBL_DSP_ITEM di = _econ.GetDispItem(item._ITEM_ID);
                if (di == null) { continue; }
                if (_econ.IsNotRegistItem(item._ITEM_ID)) { continue; }

                // ITEM_TYPE 検証(画面表示桁数に変換して確認)
                string chkVal = CommonUtil.EditDspItem(item.m_OCR_VFY_DATA, di);
                if (!eiChecker.ValidateItemType(di, chkVal))
                {
                    // 検証NG
                    return false;
                }

                // サブルーチン検証
                if (!eiChecker.ValidateSubRoutine(di, curMei, SubRoutine.CheckValue.OCR_VFY_DATA))
                {
                    // 検証NG
                    return false;
                }

                // エントリ値との比較（入力項目のみ）
                // ここはDB格納値で比較
                if (!IsNotInputItem(di._ITEM_ID))
                {
                    string entval = CommonUtil.EditTrDataItem(item.m_ENT_DATA, di);
                    string vfyval = CommonUtil.EditTrDataItem(item.m_OCR_VFY_DATA, di);
                    if (!entval.Equals(vfyval))
                    {
                        // 検証NG
                        return false;
                    }
                }

                // データ加工
                item.m_VFY_DATA = CommonUtil.EditTrDataItem(chkVal, di);
                item.m_END_DATA = item.m_VFY_DATA;
            }

            // 表示項目の読替処理
            {
                //EntryReplacer er = new EntryReplacer();

                // 券面持帰銀行コード
                string sBkCd = "";
                string sBkName = "";
                er.ReplaceBankCd(icBankCd, BknoLength, ref sBkCd, ref sBkName);
                SetItemVFY(curMei, DspItem.ItemId.持帰銀行コード, sBkCd);
                SetItemVFY(curMei, DspItem.ItemId.持帰銀行名, sBkName);

                // 交換証券種類名
                string sBillName = er.GetBillName(DBConvert.ToIntNull(billCd));
                SetItemVFY(curMei, DspItem.ItemId.交換証券種類名, sBillName);

                // 手形種類名
                string sTegataName = er.GetTegataName(DBConvert.ToIntNull(tegataCd));
                SetItemVFY(curMei, DspItem.ItemId.手形種類名, sTegataName);

                // 入力交換希望日
                string sWarekiDate = "";
                string sBusinessDate = "";
                er.ReplaceClearingDateOrg(clearingDate, ref sWarekiDate, ref sBusinessDate);
                SetItemVFY(curMei, DspItem.ItemId.和暦交換希望日, sWarekiDate);
                SetItemVFY(curMei, DspItem.ItemId.交換日, sBusinessDate);

                // 券面持帰支店コード
                string sIcBrCd = "";
                string sIcBrName = "";
                string sKozaNo = "";
                string sPayer = "";
                er.ReplaceBrName(kenIcBrCd, kenKozaNo, KouzaLength, BrnoLength, ref sIcBrCd, ref sIcBrName, ref sKozaNo, ref sPayer);
                SetItemVFY(curMei, DspItem.ItemId.持帰支店コード, sIcBrCd);
                SetItemVFY(curMei, DspItem.ItemId.持帰支店名, sIcBrName);
                SetItemVFY(curMei, DspItem.ItemId.口座番号, sKozaNo);
                SetItemVFY(curMei, DspItem.ItemId.支払人名, sPayer);
            }

            // 検証OK
            return true;
        }

        /// <summary>
        /// OCRエントリー値を設定する
        /// </summary>
        /// <param name="curMei"></param>
        /// <param name="itemid"></param>
        /// <param name="val"></param>
        private void SetItemOCRENT(MeisaiInfo curMei, int itemid, string val)
        {
            TBL_TRITEM tritem = _econ.GetTrItem(curMei, itemid);
            if (tritem == null) { return; }
            tritem.m_OCR_ENT_DATA = val;
        }

        /// <summary>
        /// OCRベリファイ値を設定する
        /// </summary>
        /// <param name="curMei"></param>
        /// <param name="itemid"></param>
        /// <param name="val"></param>
        private void SetItemOCRVFY(MeisaiInfo curMei, int itemid, string val)
        {
            TBL_TRITEM tritem = _econ.GetTrItem(curMei, itemid);
            if (tritem == null) { return; }
            tritem.m_OCR_VFY_DATA = val;
        }

        /// <summary>
        /// エントリー値を設定する
        /// </summary>
        /// <param name="curMei"></param>
        /// <param name="itemid"></param>
        /// <param name="val"></param>
        private void SetItemENT(MeisaiInfo curMei, int itemid, string val)
        {
            TBL_TRITEM tritem = _econ.GetTrItem(curMei, itemid);
            if (tritem == null) { return; }
            tritem.m_ENT_DATA = val;
            if (_itemMgr.IsLastProcess)
            {
                tritem.m_END_DATA = val;
            }
        }

        /// <summary>
        /// ベリファイ値を設定する
        /// </summary>
        /// <param name="curMei"></param>
        /// <param name="itemid"></param>
        /// <param name="val"></param>
        private void SetItemVFY(MeisaiInfo curMei, int itemid, string val)
        {
            TBL_TRITEM tritem = _econ.GetTrItem(curMei, itemid);
            if (tritem == null) { return; }
            tritem.m_VFY_DATA = val;
            tritem.m_END_DATA = val;
        }

        /// <summary>
        /// 明細確定処理
        /// </summary>
        protected virtual bool MeisaiAutoFix(MeisaiInfo curMei)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "明細確定（自動）", 1);

            // DB更新
            if (!edUpdater.UpdateTrItems(curMei))
            {
                return false;
            }

            //// 更新したので再取得
            //_itemMgr.SetCurrentTrdataItem();

            // 明細終了
            return GoNextDetail(curMei, EntryFormBase.ProcType.自動実行);
        }

        /// <summary>
        /// イメージファイルパスを取得する
        /// </summary>
        public string GetImgFilePath(ImageInfo img)
        {
            Dictionary<string, string> pathList = new Dictionary<string, string>();
            pathList.Add("BankNormalImageRoot", ServerIni.Setting.BankNormalImageRoot);
            pathList.Add("BankFutaiImageRoot", ServerIni.Setting.BankFutaiImageRoot);
            pathList.Add("BankKijituImageRoot", ServerIni.Setting.BankKijituImageRoot);
            pathList.Add("BankConfirmImageRoot", ServerIni.Setting.BankConfirmImageRoot);
            return CommonUtil.GetImgFilePath(img.ParentBat.trbatch, img.trimg, pathList);
        }

        // *******************************************************************
        // 内部メソッド（フォーカス制御）
        // *******************************************************************

        /// <summary>
        /// 指定したアイテムにフォーカスする
        /// </summary>
        /// <param name="itemid"></param>
        public void SetItemFocus(int itemid)
        {
            if (!_dcon.tbDspItems.ContainsKey(itemid)) { return; }
            _dcon.tbDspItems[itemid].Focus();
            _dcon.tbDspItems[itemid].SelectAll();
            _dcon.FocusedItemId = itemid;
        }

        /// <summary>
        /// 指定した項目にフォーカスを移動する
        /// </summary>
        /// <param name="itemid"></param>
        public void ChangeFocusedControl(int itemid)
        {
            if (!_dcon.tbDspItems.ContainsKey(itemid)) { return; }
            _dcon.FocusedItemId = itemid;
            FocusNextControl(_dcon.FocusedItemId);
        }

        public void FocusForwardControl()
        {
            _dcon.FocusedItemId = GetNextItemId(_dcon.FocusedItemId);
            FocusNextControl(_dcon.FocusedItemId);
        }

        public void FocusPreviousControl()
        {
            _dcon.FocusedItemId = GetPreviousItemId(_dcon.FocusedItemId);
            FocusNextControl(_dcon.FocusedItemId);
        }

        public void FocusNextControl(int itemid)
        {
            TextBox tb = GetTextBox(itemid);
            if (tb == null) { return; }
            tb.SelectAll();
            tb.Select();
        }

        // *******************************************************************
        // 内部メソッド（アイテム取得）
        // *******************************************************************

        /// <summary>
        /// 画面上の最初の TextBox の ITEM_ID を取得する
        /// </summary>
        /// <returns></returns>
        public int GetFirstItemId()
        {
            var hdis = _curMei.CurDsp.hosei_items.Values.Where(p =>
            {
                return !IsNotFocusItem(p._ITEM_ID);
            }
            ).OrderBy(p => p.m_INPUT_SEQ);
            if (hdis.Count() < 1)
            {
                return 1;
            }
            return hdis.First()._ITEM_ID;
        }

        /// <summary>
        /// フォーカスされている ITEM_ID を取得する
        /// </summary>
        /// <returns></returns>
        public int GetFocusedItemId()
        {
            foreach (KeyValuePair<int, TextBox> keyVal in _dcon.tbDspItems)
            {
                if (keyVal.Value.Focused)
                {
                    return keyVal.Key;
                }
            }
            return -1;
        }

        /// <summary>
        /// 指定した ITEM_ID の次の TextBox の ITEM_ID を取得する
        /// </summary>
        /// <returns></returns>
        public int GetNextItemId(int curid)
        {
            TBL_HOSEIMODE_DSP_ITEM hdi = _itemMgr.CurBat.CurMei.CurDsp.hosei_items[curid];
            var hdis = _curMei.CurDsp.hosei_items.Values.Where(p =>
            {
                if (IsNotFocusItem(p._ITEM_ID)) { return false; }
                return (p.m_INPUT_SEQ > hdi.m_INPUT_SEQ);
            }
            ).OrderBy(p => p.m_INPUT_SEQ);
            if (hdis.Count() < 1)
            {
                return GetLastItemId();
            }
            return hdis.First()._ITEM_ID;
        }

        /// <summary>
        /// 指定した ITEM_ID の前の TextBox の ITEM_ID を取得する
        /// </summary>
        /// <returns></returns>
        public int GetPreviousItemId(int curid)
        {
            TBL_HOSEIMODE_DSP_ITEM hdi = _itemMgr.CurBat.CurMei.CurDsp.hosei_items[curid];
            var hdis = _curMei.CurDsp.hosei_items.Values.Where(p =>
            {
                if (IsNotFocusItem(p._ITEM_ID)) { return false; }
                return (p.m_INPUT_SEQ < hdi.m_INPUT_SEQ);
            }
            ).OrderBy(p => p.m_INPUT_SEQ);
            if (hdis.Count() < 1)
            {
                return GetFirstItemId();
            }
            return hdis.Last()._ITEM_ID;
        }

        /// <summary>
        /// 画面上の最後の TextBox の ITEM_ID を取得する
        /// </summary>
        /// <returns></returns>
        public int GetLastItemId()
        {
            var hdis = _curMei.CurDsp.hosei_items.Values.Where(p =>
            {
                return !IsNotFocusItem(p._ITEM_ID);
            }
            ).OrderBy(p => p.m_INPUT_SEQ);
            if (hdis.Count() < 1)
            {
                return 1;
            }
            return hdis.Last()._ITEM_ID;
        }

        /// <summary>
        /// 指定した ITEM_ID の TBL_DSP_ITEM を取得する
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public TBL_DSP_ITEM GetDispItem(int itemid)
        {
            DspInfo dsp = _curMei.CurDsp;
            if (dsp.dsp_items.ContainsKey(itemid)) { return dsp.dsp_items[itemid]; }
            return null;
        }

        /// <summary>
        /// 指定した ITEM_ID の TBL_DSP_ITEM を取得する
        /// </summary>
        /// <param name="itemname"></param>
        /// <returns></returns>
        public TBL_DSP_ITEM GetDispItem(string itemname)
        {
            DspInfo dsp = _curMei.CurDsp;
            var dis = dsp.dsp_items.Values.Where(p => p.m_ITEM_DISPNAME.Equals(itemname));
            if (dis.Count() > 0) { return dis.First(); }
            return null;
        }

        /// <summary>
        /// 指定した ITEM_ID の TextBox を取得する
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public TextBox GetTextBox(int itemid)
        {
            if (_dcon.tbDspItems.ContainsKey(itemid)) { return _dcon.tbDspItems[itemid]; }
            return null;
        }

        /// <summary>
        /// フォーカス対象外テキストボックス
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns>true：フォーカス対象外、false：フォーカス対象</returns>
        private bool IsNotFocusItem(int itemid)
        {
            TBL_DSP_ITEM di = GetDispItem(itemid);
            if (_ctl.IsReadOnlyItemId(di._ITEM_ID))
            {
                return true;
            }
            return CommonUtil.IsNotFocusItem(di);
        }

        /// <summary>
        /// ＤＢ登録対象外テキストボックス
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns>true：ＤＢ登録対象外、false：ＤＢ登録対象</returns>
        public bool IsNotRegistItem(int itemid)
        {
            TBL_DSP_ITEM di = GetDispItem(itemid);
            return CommonUtil.IsNotRegistItem(di);
        }

        /// <summary>
        /// 手入力対象外テキストボックス
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns>true：手入力項目でない、false：手入力項目である</returns>
        public bool IsNotInputItem(int itemid)
        {
            TBL_DSP_ITEM di = GetDispItem(itemid);
            return CommonUtil.IsNotInputItem(di);
        }

        /// <summary>
        /// 読取専用でないテキストボックス
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns>true：手入力項目でない、false：手入力項目である</returns>
        public bool CanInputItem(int itemid)
        {
            TextBox tb = GetTextBox(itemid);
            return !tb.ReadOnly;
        }

        /// <summary>
        /// ITEM_NAME に合致する BAT_TRITEM を取得する
        /// </summary>
        /// <param name="itemname"></param>
        /// <returns></returns>
        public TBL_TRITEM GetTrItem(MeisaiInfo mei, int itemid)
        {
            if (!mei.tritems.ContainsKey(itemid)) { return null; }
            return mei.tritems[itemid];
        }

        // *******************************************************************
        // 内部メソッド（遷移）
        // *******************************************************************

        /// <summary>
        /// 先頭明細へ
        /// </summary>
        public void GoFirstDetail()
        {
        }

        /// <summary>
        /// 前明細へ
        /// </summary>
        public void SetPrevDetailsNo()
        {
        }

        /// <summary>
        /// 最終明細へ
        /// </summary>
        public void GoLastDetail()
        {
        }

        /// <summary>
        /// 処理済み明細に移動する
        /// </summary>
        /// <param name="detailsno"></param>
        public void ChangeDetail(int imageno, int detailsno)
        {
        }

        // *******************************************************************
        // 内部メソッド（イメージ処理）
        // *******************************************************************

        /// <summary>
        /// 帳票回転
        /// </summary>
        public void RotateImage(int mode)
        {
            // 回転処理
            eiHandler.RotateImage(mode);

            // 時計回り９０度回転
            _itemMgr.EntParams.ChangeRotate();

            ////回転情報を更新
            //TBL_BAT_TRIMAGE bat_trimage = _itemMgr.CurBat.CurImage.bat_trimage;
            //if (mode.Equals(0))
            //{
            //    bat_trimage.ChangeRotate();   // 時計回り
            //}
            //else if (mode.Equals(1))
            //{
            //    bat_trimage.ChangeReverseRotate();    // 反時計回り
            //}
            //// BAT_TRIMAGE 更新
            //edUpdater.UpdateTrImage(bat_trimage);
        }

        /// <summary>
        /// 帳票拡大／縮小
        /// </summary>
        public void SizeChangeImage(int mode, int itemid)
        {
            eiHandler.SizeChangeImage(mode, itemid);
        }

        /// <summary>
        /// 帳票全体表示
        /// </summary>
        public void SizeChangeImageFit(int itemid)
        {
            eiHandler.SizeChangeImageFit(itemid);
        }

        /// <summary>
        /// イメージの移動
        /// </summary>
        public void MoveImage(ImageCanvas.MoveType mode)
        {
            eiHandler.MoveImage(mode);
        }

        // *******************************************************************
        // 内部メソッド
        // *******************************************************************

        /// <summary>
        /// 入力値の検証
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public bool IsValidValue(int itemid)
        {
            if (!eiChecker.CheckTextBoxInput(itemid, _dcon.tbDspItems, false))
            {
                if (!eiChecker.ErrMsg.Equals(""))
                {
                    _form.SetStatusMessage(eiChecker.ErrMsg);
                    if (eiChecker.ErrItemId != EntryInputChecker.DEF_ERRID)
                    {
                        ChangeFocusedControl(eiChecker.ErrItemId);
                    }
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// すべての入力値検証
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public bool IsAllValidValue()
        {
            // すべての入力項目検証（サブルーチン）
            bool isSuccess = true;
            foreach (int itemid in _dcon.tbDspItems.Keys)
            {
                if (!eiChecker.CheckTextBoxInput(itemid, _dcon.tbDspItems, true))
                {
                    isSuccess = false;
                    break;
                }
            }

            // サブルーチンエラー有無
            if (!isSuccess)
            {
                if (!string.IsNullOrEmpty(eiChecker.ErrMsg))
                {
                    _form.SetStatusMessage(eiChecker.ErrMsg);
                    if (eiChecker.ErrItemId != EntryInputChecker.DEF_ERRID)
                    {
                        _econ.ChangeFocusedControl(eiChecker.ErrItemId);
                    }
                }
                return false;
            }

            return true;
        }

        /// <summary>
        /// 処理状況に応じたデータを取得する
        /// </summary>
        /// <param name="mei"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public string GetOriginalData(MeisaiInfo mei, int itemid)
        {
            TextBox tb = _econ.GetTextBox(itemid);
            return DBConvert.ToStringNull(tb.Tag);
        }

        /// <summary>
        /// 明細確定の動作を判別する
        /// </summary>
        /// <param name="mei"></param>
        /// <param name="ftype"></param>
        /// <returns></returns>
        private EntryFormBase.ExecMode GetExecMode(MeisaiInfo mei, EntryFormBase.ProcType ftype)
        {
            EntryFormBase.ExecMode mode = EntryFormBase.ExecMode.明細移動;
            if (_ctl.IsKanryouTeisei)
            {
                // 完了訂正
                mode = EntryFormBase.ExecMode.明細確定;
                //mode = EntryFormBase.ExecMode.バッチ終了;
            }
            else if (ftype != EntryFormBase.ProcType.明細移動)
            {
                mode = EntryFormBase.ExecMode.明細確定;

                //// バッチ終了判定
                //if (_itemMgr.CurBat.IsLastMeisai)
                //{
                //    mode = EntryFormBase.ExecMode.バッチ終了;
                //}
                //else
                //{
                //    mode = EntryFormBase.ExecMode.明細確定;
                //}
            }
            return mode;
        }

        /// <summary>
        /// 次の明細番号を取得して移る
        /// </summary>
        public bool GoNextDetail(MeisaiInfo mei, EntryFormBase.ProcType ftype)
        {
            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "開始", 1);

            EntryFormBase.ExecMode emode = GetExecMode(mei, ftype);
            if (emode == EntryFormBase.ExecMode.明細確定)
            {
                // 明細終了
                edUpdater.UpdateMeisaiToEnd(mei);

                // プルーフチェック
                ExecProof();

                // 明細ステータス更新
                _ctl.TerminateEntryStatus(mei);
            }

            //if (emode == EntryFormBase.ExecMode.バッチ終了)
            //{
            //    // 明細終了
            //    edUpdater.UpdateMeisaiToEnd(mei);

            //    // プルーフチェック
            //    ExecProof();

            //    // DUPデータ削除
            //    //_dcon.ClearDupData();

            //    // 明細ステータス更新
            //    _ctl.TerminateEntryStatus(mei);
            //}
            //else if (emode == EntryFormBase.ExecMode.明細確定)
            //{
            //    // 明細終了
            //    edUpdater.UpdateMeisaiToEnd(mei);

            //    // プルーフチェック
            //    ExecProof();

            //    // DUPデータ保存
            //    //_dcon.SaveDupData();

            //    // 明細ステータス更新
            //    _ctl.TerminateEntryStatus(mei);
            //}

            LogWriter.writeLog(MethodBase.GetCurrentMethod(), "終了", 1);
            return true;
        }

        /// <summary>
        /// 金額プルーフチェックを行う
        /// </summary>
        private void ExecProof()
        {
            // プルーフチェック
            ProofForm prForm = ProofCheck();
            if ((prForm != null) && (prForm.Result == ProofForm.ProofResult.中断))
            {
                // バッチ処理終了
                SetBatchEnd();
            }
        }

        /// <summary>
        /// 金額プルーフチェックを行う
        /// </summary>
        /// <returns></returns>
        private ProofForm ProofCheck()
        {
            // 持帰はプルーフしない
            if (_ctl.GymId != GymParam.GymId.持出) { return null; }

            // プルーフチェック=OFF はプルーフしない
            if (!AppConfig.KingakuProofCheck) { return null; }

            // プルーフ実行可否（金額エントリー）
            bool canProof = (_ctl.HoseiInputMode == HoseiStatus.HoseiInputMode.金額);
            //canProof |= _ctl.IsKanryouTeisei;
            if (!canProof)
            {
                // プルーフしない
                return null;
            }

            //// 完了訂正は状態に関係なく必ずプルーフする
            //if (!_ctl.IsKanryouTeisei)
            //{
            //    // バッチ内の自分自身以外の明細がすべて「3000:完了」以上の場合のみプルーフする
            //    SortedDictionary<int, TBL_HOSEI_STATUS> hosei_statuses = _itemMgr.GetHoseiStatusesByKingaku(
            //        _curMei._GYM_ID, _curMei._OPERATION_DATE, _curMei._SCAN_TERM, _curMei._BAT_ID, HoseiStatus.HoseiInputMode.金額);
            //    var statuses = hosei_statuses.Values.Where(p => (p._DETAILS_NO != _curMei._DETAILS_NO) && (p.m_INPT_STS < HoseiStatus.InputStatus.完了));
            //    if (statuses.Count() > 0)
            //    {
            //        // プルーフしない
            //        return null;
            //    }
            //}

            // バッチ内の自分自身以外の明細がすべて「3000:完了」以上の場合のみプルーフする
            SortedDictionary<int, TBL_HOSEI_STATUS> hosei_statuses = _itemMgr.GetHoseiStatusesByKingaku(
                _curMei._GYM_ID, _curMei._OPERATION_DATE, _curMei._SCAN_TERM, _curMei._BAT_ID, HoseiStatus.HoseiInputMode.金額);
            var statuses = hosei_statuses.Values.Where(p => (p._DETAILS_NO != _curMei._DETAILS_NO) && (p.m_INPT_STS < HoseiStatus.InputStatus.完了));
            if (statuses.Count() > 0)
            {
                // プルーフしない
                return null;
            }

            // 枚数・金額チェック
            bool isProofErr = false;
            ItemManager.ProofInfo pi = _itemMgr.GetProofData(_curMei);
            if (pi.MeiCount != pi.BatCount)
            {
                isProofErr = true;
            }
            else if (pi.MeiAmount != pi.BatAmount)
            {
                isProofErr = true;
            }
            if (!isProofErr)
            {
                // プルーフＯＫ
                return null;
            }

            // プルーフ画面表示
            ProofForm prForm = new ProofForm(_ctl, pi);
            prForm.ShowDialog();
            return prForm;
        }

        /// <summary>
        /// 画面ID切替処理
        /// </summary>
        /// <param name="dspid"></param>
        /// <returns></returns>
        public bool ChangeDsp(MeisaiInfo mei, int dspid, int BillCode, string BillName)
        {
            // DSP_ID が変わったので画面項目数が変わる
            if (!edUpdater.DspUpdate(mei, dspid, BillCode, BillName))
            {
                return false;
            }

            // 証券種類変更フラグを設定
            _itemMgr.EntParams.IsChangeDsp = true;

            // 更新したので明細とアイテムを再取得
            _itemMgr.FetchTrData(mei);
            _itemMgr.FetchTrItems(mei);

            // カーソル座標を再取得
            eiHandler.RefreshRectangleInfo();

            return true;
        }

        /// <summary>
        /// バッチ終了
        /// </summary>
        public void SetBatchEnd()
        {
            _itemMgr.EntParams.IsBatchEnd = true;
        }

        /// <summary>
        /// 親フォームのメッセージ欄設定
        /// </summary>
        /// <remarks>
        /// Application.DoEvents()を使用する場合は、親フォームのDisable制御は留意する必要あり
        /// </remarks>
        private void ParentSetStatusMessage(string msg, Color clr, bool DoEvent = false)
        {
            // 親フォームが未設定の場合は終了
            if (_parent == null) { return; }

            // メッセージ設定
            _parent.SetStatusMessage(msg, clr);

            if (DoEvent)
            {
                // DoEventsで画面表示反映 [Refresh()だと画面がちらつくのでDoEventsで実施]
                Application.DoEvents();
            }
        }

    }
}
