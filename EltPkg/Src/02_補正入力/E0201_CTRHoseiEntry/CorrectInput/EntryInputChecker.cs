using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryClass;
using EntryCommon;
using SubRoutineApi;

namespace CorrectInput
{
    public class EntryInputChecker
    {
        protected Controller _ctl;
        protected ItemManager _itemMgr = null;
        public EntryCommonFormBase form { get; set; } = null;

        private EntryReplacer _er = null;

        protected EntryController _econ { get { return _itemMgr.EntController; } }
        protected EntryDspControl _dcon { get { return _itemMgr.DspControl; } }
        protected EntryImageHandler eiHandler { get { return _itemMgr.ImageHandler; } }
        protected EntryInputChecker eiChecker { get { return _itemMgr.Checker; } }
        protected EntryDataUpdater edUpdater { get { return _itemMgr.Updater; } }
        protected MeisaiInfo _curMei { get { return _itemMgr.CurBat.CurMei; } }

        public int ErrItemId { get; set; }
        public string ErrMsg { get; private set; }

        public const int DEF_ERRID = -1;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EntryInputChecker(Controller ctl)
        {
            _ctl = ctl;
            _itemMgr = (ItemManager)ctl.ItemMgr;

            _er = _ctl.GetEntReplacer();

            ClearError();
        }

        /// <summary>
        /// エラーをクリアする
        /// </summary>
        public void ClearError()
        {
            ErrItemId = DEF_ERRID;
            ErrMsg = "";
        }

        /// <summary>
        /// ベリファイチェックを行う
        /// </summary>
        /// <param name="mei"></param>
        /// <param name="itemid"></param>
        /// <param name="isAllVerify"></param>
        /// <returns></returns>
        public bool VeifyCheck(MeisaiInfo mei, int itemid, bool isAllVerify = false)
        {
            // ベリファイ対象外
            if (_econ.IsNotInputItem(itemid)) { return true; }
            if (mei.tritems.Count == 0) { return true; }

            // 完了訂正の未完了データはベリファイ対象外
            if (_ctl.IsReadOnlyItemId(itemid)) { return true; }

            _dcon.AddComma(mei, itemid);

            string prvdata = "";
            string curdata = "";
            string orgdata = "";
            int vfyItemid = itemid;
            if (itemid == DspItem.ItemId.入力交換希望日)
            {
                // 「入力交換希望日」は営業日を加味した「交換日」でベリファイチェックする
                TBL_DSP_ITEM diKokanBi = mei.CurDsp.dsp_items[vfyItemid];
                SetMasterName(diKokanBi);
                vfyItemid = DspItem.ItemId.交換日;
            }

            TBL_DSP_ITEM di = mei.CurDsp.dsp_items[vfyItemid];
            TBL_TRITEM org_tritem = mei.tritems_org[vfyItemid];
            TBL_TRITEM tritem = mei.tritems[vfyItemid];

            // チェックは常にエントリー値を比較するよう修正
            prvdata = CommonUtil.EditDspItem(tritem.m_ENT_DATA, di);
            orgdata = CommonUtil.EditDspItem(org_tritem.m_ENT_DATA, di);
            // 現在入力値はDB格納値に変換→画面表示内容にして設定
            curdata = CommonUtil.EditTrDataItem(_dcon.tbDspItems[vfyItemid].Text, di);
            curdata = CommonUtil.EditDspItem(curdata, di);

            if (prvdata.Equals(curdata))
            {
                // ベリファイＯＫ
                return true;
            }

            if (isAllVerify && orgdata.Equals(curdata))
            {
                // 確定処理 AND
                // すでにアンマッチ画面OKとした項目はベリファイアンマッチ画面除外
                return true;
            }

            // 画面表示データ編集
            string prvdatacomma = prvdata;
            if (!string.IsNullOrEmpty(prvdata))
            {
                prvdatacomma = _dcon.AddComma(prvdata, di);
            }
            string curdatacomma = curdata;
            if (!string.IsNullOrEmpty(curdata))
            {
                curdatacomma = _dcon.AddComma(curdata, di);
            }

            // ベリファイアンマッチ画面
            VerifyUnMatch form = new VerifyUnMatch(prvdatacomma, curdatacomma, di.m_ITEM_DISPNAME);
            form.ShowDialog();
            if (form.DialogResult != DialogResult.OK)
            {
                // ベリファイＮＧ
                return false;
            }

            // 確定処理で引っかからないように比較元を設定する
            // org_tritem はＤＢ更新しない
            org_tritem.m_ENT_DATA = CommonUtil.EditTrDataItem(curdata, di);
            return true;
        }

        /// <summary>
        /// テキストボックスからフォーカスが外れたとき
        /// </summary>
        /// <param name="itemid"></param>
        public void DspItemLeave(int itemid)
        {
            if (itemid < 0) { return; }

            TBL_DSP_ITEM di = _econ.GetDispItem(itemid);
            if (di == null) { return; }

            // 完了訂正の未完了データはチェック対象外
            if (_ctl.IsReadOnlyItemId(itemid)) { return; }

            // 読替処理
            SetMasterName(di);

            // パイプ("|")区切りで複数サブルーチンを検証
            if (string.IsNullOrEmpty(di.m_ITEM_SUBRTN)) { return; }
            foreach (string rtn in di.m_ITEM_SUBRTN.Split('|'))
            {
                // 半角空白区切りでサブルーチンの引数を取得する
                string[] args = rtn.Split(' ');
                string rtnName = args[0].Trim();

                // 区分=2のみサブルーチン対象（入力項目読替）
                if (!CheckSubRtnExists(SubRtn.SubKbn.ItemReplace, rtnName)) { continue; }
                SubRoutine sr = new SubRoutine(_itemMgr.mst_banks, _itemMgr.mst_branches, _itemMgr.mst_syuruimfs, 
                                               _dcon.tbDspItems, _itemMgr.CurBat.CurMei.trmei, _itemMgr.CurBat.CurMei.tritems, _itemMgr.CurBat.CurMei.CurDsp.dsp_items);
                string res = sr.ReplaceBySubRoutine(itemid, rtnName);
                if (!string.IsNullOrEmpty(res))
                {
                    _dcon.tbDspItems[itemid].Text = res;
                }
            }

            // 入力フィールド読替
            TextBox tb = _econ.GetTextBox(itemid);
            if (!string.IsNullOrEmpty(tb.Text))
            {
                return;
            }

            // フル桁自動フォーカス抑止
            _econ.IsStopTextChanged = true;

            switch (di.m_ITEM_TYPE)
            {
                case DspItem.ItemType.N:
                    // 空打ちで0を入力する(前ゼロはコントロール側で実施)
                    // tb.Text = CommonUtil.EditTrDataItem(tb.Text, di);
                    tb.Text = "0";
                    break;
                case DspItem.ItemType.A:
                case DspItem.ItemType.R:
                    break;
                case DspItem.ItemType.K:
                case DspItem.ItemType.J:
                    break;
                default:
                    break;
            }

            // フル桁自動フォーカス抑止解除
            _econ.IsStopTextChanged = false;
        }

        /// <summary>
        /// 読替処理
        /// </summary>
        private void SetMasterName(TBL_DSP_ITEM di)
        {
            TextBox tbSrc = _econ.GetTextBox(di._ITEM_ID);
            if (tbSrc == null) { return; }
            string sDspVal = _dcon.DelCommnaItemType(di, tbSrc.Text);

            // 表示項目の読替処理
            int KouzaLength = 0;
            int BknoLength = 0;
            int BrnoLength = 0;
            TBL_DSP_ITEM KouzaItem = null;
            TBL_DSP_ITEM BknoItem = null;
            TBL_DSP_ITEM BrnoItem = null;
            TextBox tbDst1 = null;
            TextBox tbDst2 = null;
            TextBox tbDst3 = null;
            TextBox tbDst4 = null;
            switch (di._ITEM_ID)
            {
                case DspItem.ItemId.券面持帰銀行コード:
                    tbDst1 = _econ.GetTextBox(DspItem.ItemId.持帰銀行コード);
                    tbDst2 = _econ.GetTextBox(DspItem.ItemId.持帰銀行名);

                    // 持帰銀行の長さ取得
                    BknoLength = 0;
                    BknoItem = _econ.GetDispItem(DspItem.ItemId.持帰銀行コード);
                    if (BknoItem != null) BknoLength = BknoItem.m_ITEM_LEN;

                    _er.ReplaceBankCd(sDspVal, BknoLength, tbDst1, tbDst2);
                    break;
                case DspItem.ItemId.交換証券種類コード:
                    tbDst1 = _econ.GetTextBox(DspItem.ItemId.交換証券種類名);
                    _er.ReplaceBillName(sDspVal, tbDst1);
                    break;
                case DspItem.ItemId.手形種類コード:
                    tbDst1 = _econ.GetTextBox(DspItem.ItemId.手形種類名);
                    _er.ReplaceTegataName(sDspVal, tbDst1);
                    break;
                case DspItem.ItemId.持帰支店コード:
                    tbDst1 = _econ.GetTextBox(DspItem.ItemId.持帰支店名);
                    _er.ReplaceBrName(sDspVal, tbDst1);
                    break;
                case DspItem.ItemId.入力交換希望日:
                    tbDst1 = _econ.GetTextBox(DspItem.ItemId.和暦交換希望日);
                    tbDst2 = _econ.GetTextBox(DspItem.ItemId.交換日);
                    _er.ReplaceClearingDate(sDspVal, tbDst1, tbDst2);
                    break;
                case DspItem.ItemId.券面持帰支店コード:
                    tbDst1 = _econ.GetTextBox(DspItem.ItemId.持帰支店コード);
                    tbDst2 = _econ.GetTextBox(DspItem.ItemId.持帰支店名);
                    tbDst3 = _econ.GetTextBox(DspItem.ItemId.口座番号);
                    tbDst4 = _econ.GetTextBox(DspItem.ItemId.支払人名);

                    // 券面口座番号を取得
                    string sDspKozaNo = GetDspValue(DspItem.ItemId.券面口座番号);

                    // 口座番号の長さ取得
                    KouzaLength = 0;
                    KouzaItem = _econ.GetDispItem(DspItem.ItemId.口座番号);
                    if(KouzaItem != null) KouzaLength = KouzaItem.m_ITEM_LEN;
                    // 持帰支店の長さ取得
                    BrnoLength = 0;
                    BrnoItem = _econ.GetDispItem(DspItem.ItemId.持帰支店コード);
                    if (BrnoItem != null) BrnoLength = BrnoItem.m_ITEM_LEN;

                    // DspItemに定義の長さで設定
                    _er.ReplaceBrName(sDspVal, sDspKozaNo, KouzaLength, BrnoLength, tbDst1, tbDst2, tbDst3, tbDst4);
                    break;
                case DspItem.ItemId.券面口座番号:
                    tbDst1 = _econ.GetTextBox(DspItem.ItemId.持帰支店コード);
                    tbDst2 = _econ.GetTextBox(DspItem.ItemId.持帰支店名);
                    tbDst3 = _econ.GetTextBox(DspItem.ItemId.口座番号);
                    tbDst4 = _econ.GetTextBox(DspItem.ItemId.支払人名);

                    // 券面持帰支店コードを取得
                    string sDspBrNo = GetDspValue(DspItem.ItemId.券面持帰支店コード);

                    // 口座番号の長さ取得
                    KouzaLength = 0;
                    KouzaItem = _econ.GetDispItem(DspItem.ItemId.口座番号);
                    if (KouzaItem != null) KouzaLength = KouzaItem.m_ITEM_LEN;
                    // 持帰支店の長さ取得
                    BrnoLength = 0;
                    BrnoItem = _econ.GetDispItem(DspItem.ItemId.持帰支店コード);
                    if (BrnoItem != null) BrnoLength = BrnoItem.m_ITEM_LEN;

                    // DspItemに定義の長さで設定
                    _er.ReplaceBrName(sDspBrNo, sDspVal, KouzaLength, BrnoLength, tbDst1, tbDst2, tbDst3, tbDst4);
                    break;
            }
        }

        /// <summary>
        /// <summary>
        /// 各TextBox項目の検証
        /// </summary>
        /// <param name="itemid">tbDspItemsの添数</param>
        /// <param name="ed">エントリデータ</param>
        /// <param name="dspmes">エラー時にメッセージを表示するか</param>
        /// <returns>true:検証OK, false:検証NG</returns>
        public bool CheckTextBoxInput(int itemid, SortedList<int, TextBox> text_boxes, bool dspmes)
        {
            // 入力項目でない場合は検証しない
            if (_econ.IsNotInputItem(itemid)) { return true; }

            if (!text_boxes.ContainsKey(itemid)) { return false; }
            TextBox tb = text_boxes[itemid];

            // 読取専用はチェックしない
            if (tb.ReadOnly) { return true; }

            // サブルーチンチェック前に入力項目読替を実施（フォーカスが外れたときの処理）
            eiChecker.DspItemLeave(itemid);

            SubRoutine sr = new SubRoutine(_itemMgr.mst_banks, _itemMgr.mst_branches, _itemMgr.mst_syuruimfs,
                                           text_boxes, _itemMgr.CurBat.CurMei.trmei, _itemMgr.CurBat.CurMei.tritems, _itemMgr.CurBat.CurMei.CurDsp.dsp_items);

            // ITEM_TYPE 入力チェック
            if (!CheckItemType(itemid, tb, text_boxes))
            {
                return false;
            }

            // その他のサブルーチンチェック
            TBL_DSP_ITEM di = _econ.GetDispItem(itemid);
            if (di == null) { return true; }
            if (string.IsNullOrEmpty(di.m_ITEM_SUBRTN)) { return true; }

            // パイプ("|")区切りで複数サブルーチンを検証
            foreach (string rtn in di.m_ITEM_SUBRTN.Split('|'))
            {
                string[] args = rtn.Split(' ');
                string rtnName = args[0].Trim();

                // 区分=1のみサブルーチン対象（入力項目チェック）
                if (!CheckSubRtnExists(SubRtn.SubKbn.ItemCheck, rtnName)) { continue; }

                // サブルーチン実行
                ErrMsg = sr.CheckSubRoutine(itemid, rtnName, rtn);
                if (!ErrMsg.Equals(""))
                {
                    ErrItemId = itemid;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// サブルーチンデータを検索して区分・ルーチン名の有無を返す
        /// </summary>
        /// <param name="kbn">区分</param>
        /// <param name="rtnname">サブルーチン名</param>
        /// <returns></returns>
        public bool CheckSubRtnExists(int kbn, string rtnname)
        {
            foreach (TBL_SUB_RTN subrtn in _itemMgr.sub_rtns)
            {
                if (rtnname.Equals(subrtn.m_SUB_SUB) && (subrtn.m_SUB_KBN == kbn)) { return true; }
            }
            return false;
        }

        /// <summary>
        /// ITEM_TYPE 入力チェック
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="ed"></param>
        /// <param name="tb"></param>
        /// <param name="dspmes"></param>
        /// <returns></returns>
        public bool CheckItemType(int itemid, TextBox tb, SortedList<int, TextBox> text_boxes)
        {
            TBL_DSP_ITEM di = _econ.GetDispItem(itemid);
            if (di == null) { return true; }

            string txtVal = _dcon.DelCommnaItemType(di, tb.Text);
            ClearError();

            // 認識不能文字が残っている
            if (txtVal.Contains("?"))
            {
                ErrMsg = "未入力のデータが存在します。";
                ErrItemId = itemid;
                return false;
            }

            // 文字数チェック
            // テキストボックス（カンマを除く）の桁数が
            // 項目の桁数を超えていればエラー
            int blen = CommonUtil.BLen(txtVal);
            if ((di.m_ITEM_LEN < blen) && (di.m_ITEM_LEN != 0))
            {
                ErrMsg = string.Format("最大桁数を超えています。 最大桁数：{0}  現在桁数：{1}", di.m_ITEM_LEN, blen);
                ErrItemId = itemid;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 明細が更新されたかチェックする（表示項目）
        /// </summary>
        /// <returns></returns>
        /// <remarks>未使用</remarks>
        private bool IsViewItemUpdate()
        {
            foreach (int itemid in _dcon.tbDspItems.Keys)
            {
                if (CheckViewItemChange(itemid))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 表示専用項目が変更されたかチェックする
        /// </summary>
        /// <returns></returns>
        /// <remarks>未使用</remarks>
        private bool CheckViewItemChange(int itemid)
        {
            TextBox tb = _econ.GetTextBox(itemid);
            TBL_DSP_ITEM di = _econ.GetDispItem(itemid);
            if (tb == null) { return false; }
            if (di == null) { return false; }
            //if (di.m_ITEM_TYPE != DspItem.ItemType.V) { return false; }
            string[] ChkType = { DspItem.ItemType.V, DspItem.ItemType.W };
            if (!ChkType.Contains(di.m_ITEM_TYPE)) { return false; }

            // BAT_TRITEM が存在しない場合は新規なので変更あり
            if (!_curMei.tritems_org.ContainsKey(itemid))
            {
                return true;
            }

            // オリジナルデータと比較
            string orgdata = _econ.GetOriginalData(_curMei, itemid);
            string indata = tb.Text;
            orgdata = CommonUtil.EditTrDataItem(orgdata, di);
            indata = CommonUtil.EditTrDataItem(indata, di);

            // 何か変更がある場合
            if (!orgdata.Equals(indata))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ITEM_TYPE によるデータ検証を行う
        /// </summary>
        /// <param name="di"></param>
        /// <param name="val"></param>
        /// <returns>検証OK：true、検証NG：false</returns>
        public bool ValidateItemType(TBL_DSP_ITEM di, string val)
        {
            // 文字数チェック
            switch (di.m_ITEM_TYPE)
            {
                case DspItem.ItemType.A:
                case DspItem.ItemType.N:
                case DspItem.ItemType.S:
                case DspItem.ItemType.R:
                case DspItem.ItemType.T:
                case DspItem.ItemType.K:
                    int blen = CommonUtil.BLen(val);
                    if ((di.m_ITEM_LEN > 0) && (di.m_ITEM_LEN < blen))
                    {
                        return false;
                    }
                    break;
                case DspItem.ItemType.J:
                case DspItem.ItemType.C:
                case DspItem.ItemType.AST:
                case DspItem.ItemType.D:
                case DspItem.ItemType.V:
                case DspItem.ItemType.W:
                    break;
            }

            // 形式チェック
            bool isOK = true;
            switch (di.m_ITEM_TYPE)
            {
                case DspItem.ItemType.A:
                    isOK = IsNumberAlpha(val);
                    break;
                case DspItem.ItemType.N:
                    isOK = IsNumber(val);
                    break;
                case DspItem.ItemType.S:
                    isOK = IsNumberMinus(val);
                    break;
                case DspItem.ItemType.R:
                    isOK = IsNumber(val, true);
                    break;
                case DspItem.ItemType.T:
                    isOK = IsNumberMinus(val, true);
                    break;
                case DspItem.ItemType.K:
                    isOK = IsNumberAlphaKana(val);
                    break;
                case DspItem.ItemType.J:
                case DspItem.ItemType.C:
                case DspItem.ItemType.AST:
                case DspItem.ItemType.D:
                case DspItem.ItemType.V:
                case DspItem.ItemType.W:
                    break;
            }
            return isOK;
        }

        /// <summary>
        /// サブルーチンによるデータ検証を行う
        /// </summary>
        /// <param name="di"></param>
        /// <param name="mei"></param>
        /// <param name="valtype"></param>
        /// <returns></returns>
        public bool ValidateSubRoutine(TBL_DSP_ITEM di, MeisaiInfo mei, SubRoutine.CheckValue valtype)
        {
            if (string.IsNullOrEmpty(di.m_ITEM_SUBRTN)) { return true; }

            SubRoutine sr = new SubRoutine(_itemMgr.mst_banks, _itemMgr.mst_branches, _itemMgr.mst_syuruimfs,
                                           _dcon.tbDspItems, mei.trmei, mei.tritems, mei.CurDsp.dsp_items, valtype);

            // パイプ("|")区切りで複数サブルーチンを検証
            foreach (string rtn in di.m_ITEM_SUBRTN.Split('|'))
            {
                // 半角空白区切りでサブルーチンの引数を取得する
                string[] args = rtn.Split(' ');
                string rtnName = args[0].Trim();

                // 区分=1のみサブルーチン対象（入力項目チェック）
                if (!CheckSubRtnExists(SubRtn.SubKbn.ItemCheck, rtnName)) { continue; }

                // サブルーチン実行
                string errMsg = sr.CheckSubRoutine(di._ITEM_ID, rtnName, rtn);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// データ形式チェック（半角数字）
        /// </summary>
        /// <param name="val">チェック対象文字列</param>
        /// <param name="isBlankOK">空文字ＮＧの場合は false 指定</param>
        /// <returns></returns>
        public bool IsNumber(object val, bool isBlankOK = false)
        {
            if (val == null) { return isBlankOK; }
            string sVal = DBConvert.ToStringNull(val);
            if (string.IsNullOrEmpty(sVal)) { return isBlankOK; }

            // 1桁以上
            // 半角数字 OK
            // 半角英字 NG
            // 半角記号 NG
            // 半角カナ NG
            string len = "1";
            string number = @"0-9";
            string alpha = @"";
            string marks = @"";
            string kana = @"";
            string regex = "^";
            regex += "[" + number + alpha + marks + kana + "]";
            regex += "{" + len + ",}$";
            if (!Regex.IsMatch(sVal, regex))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// データ形式チェック（半角数字、マイナス記号）
        /// </summary>
        /// <param name="val">チェック対象文字列</param>
        /// <param name="isBlankOK">空文字ＮＧの場合は false 指定</param>
        /// <returns></returns>
        public bool IsNumberMinus(object val, bool isBlankOK = false)
        {
            if (val == null) { return isBlankOK; }
            string sVal = DBConvert.ToStringNull(val);
            if (string.IsNullOrEmpty(sVal)) { return isBlankOK; }

            // 1桁以上
            // 半角数字 OK
            // 半角英字 NG
            // マイナス記号 OK
            // 半角カナ NG
            string len = "1";
            string number = @"0-9";
            string alpha = @"";
            string marks = @"\-";
            string kana = @"";
            string regex = "^";
            regex += "[" + number + alpha + marks + kana + "]";
            regex += "{" + len + ",}$";
            if (!Regex.IsMatch(sVal, regex))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// データ形式チェック（半角数字、半角英字）
        /// </summary>
        /// <param name="val">チェック対象文字列</param>
        /// <param name="isBlankOK">空文字ＮＧの場合は false 指定</param>
        /// <returns></returns>
        public bool IsNumberAlpha(object val, bool isBlankOK = true)
        {
            if (val == null) { return isBlankOK; }
            string sVal = DBConvert.ToStringNull(val);
            if (string.IsNullOrEmpty(sVal)) { return isBlankOK; }

            // 1桁以上
            // 半角数字 OK
            // 半角英字 OK
            // 半角記号 NG
            // 半角カナ NG
            string len = "1";
            string number = @"0-9";
            string alpha = @"a-zA-Z";
            string marks = @"";
            string kana = @"";
            string regex = "^";
            regex += "[" + number + alpha + marks + kana + "]";
            regex += "{" + len + ",}$";
            if (!Regex.IsMatch(sVal, regex))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// データ形式チェック（半角数字、半角英字、半角カナ）
        /// </summary>
        /// <param name="val">チェック対象文字列</param>
        /// <param name="isBlankOK">空文字ＮＧの場合は false 指定</param>
        /// <returns></returns>
        public bool IsNumberAlphaKana(object val, bool isBlankOK = true)
        {
            if (val == null) { return isBlankOK; }
            string sVal = DBConvert.ToStringNull(val);
            if (string.IsNullOrEmpty(sVal)) { return isBlankOK; }

            // 1桁以上
            // 半角数字 OK
            // 半角英字 OK
            // 半角記号 NG
            // 半角カナ OK
            string len = "1";
            string number = @"0-9";
            string alpha = @"a-zA-Z";
            string marks = @"";
            string kana = @"ｦ-ﾟ";
            string regex = "^";
            regex += "[" + number + alpha + marks + kana + "]";
            regex += "{" + len + ",}$";
            if (!Regex.IsMatch(sVal, regex))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 歴日かどうかチェックする
        /// </summary>
        /// <param name="val">yyyyMMdd</param>
        /// <returns></returns>
        public bool IsDate(object val)
        {
            string sVal = DBConvert.ToStringNull(val);
            if (string.IsNullOrEmpty(sVal)) { return false; }
            if (sVal.Length < 8) { return false; }
            sVal = CommonUtil.ConvToDateFormat(DBConvert.ToIntNull(sVal));
            DateTime dt;
            return DateTime.TryParse(sVal, out dt);
        }

        /// <summary>
        /// 指定した ITEM_ID のテキストボックス入力値を取得する
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private string GetDspValue(int itemid)
        {
            TBL_DSP_ITEM di = _econ.GetDispItem(itemid);
            TextBox tb = _econ.GetTextBox(itemid);
            if (di == null) { return ""; }
            if (tb == null) { return ""; }
            return _dcon.DelCommnaItemType(di, tb.Text); ;
        }
    }
}
