using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryClass;
using EntryCommon;

namespace CorrectInput
{
    public class EntryDspControl
    {
        protected Controller _ctl;
        protected ItemManager _itemMgr = null;
        public EntryCommonFormBase form { get; set; } = null;

        protected EntryController _econ { get { return _itemMgr.EntController; } }
        protected EntryDspControl _dcon { get { return _itemMgr.DspControl; } }
        protected EntryImageHandler eiHandler { get { return _itemMgr.ImageHandler; } }
        protected EntryInputChecker eiChecker { get { return _itemMgr.Checker; } }
        protected EntryDataUpdater edUpdater { get { return _itemMgr.Updater; } }
        protected MeisaiInfo _curMei { get { return _itemMgr.CurBat.CurMei; } }

        public SortedList<int, TextBox> tbDspItems { get; protected set; } = new SortedList<int, TextBox>();
        public SortedList<int, Label> lblDspItems { get; protected set; } = new SortedList<int, Label>();

        public SortedList<int, DupDspItem> dup_tbDspItems { get; protected set; } = null;    // DUP用
        public InputDspItems input_DspItems { get; protected set; } = null;    // 入力中項目復元用
        public Color ReadOnlyColor { get { return Color.FromArgb(240, 240, 240); } }

        public int FocusedItemId { get; set; } = 1;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EntryDspControl(Controller ctl)
        {
            _ctl = ctl;
            _itemMgr = (ItemManager)ctl.ItemMgr;
        }

        /// <summary>
        /// 画面入力項目生成
        /// </summary>
        public void CreateDspItems()
        {
            if (_itemMgr.EntParams.IsInitControl) { return; }

            float Lblfontsize;
            tbDspItems = new SortedList<int, TextBox>();
            lblDspItems = new SortedList<int, Label>();
            TextBox tb = new TextBox();

            DspInfo dsp = _itemMgr.CurBat.CurMei.CurDsp;
            foreach (TBL_DSP_ITEM di in dsp.dsp_items.Values)
            {
                TBL_HOSEIMODE_DSP_ITEM hdi = dsp.GetHoseiDspItem(di._ITEM_ID);
                if (hdi == null) { continue; }

                // テキストボックス生成
                if ((hdi.m_INPUT_POS_TOP != 0 || hdi.m_INPUT_POS_LEFT != 0) && (!di.m_ITEM_TYPE.Equals(DspItem.ItemType.C)))
                {
                    tb = GetTextBoxItemType(di.m_ITEM_TYPE, di);

                    tb.TabStop = false;
                    tb.Left = hdi.m_INPUT_POS_LEFT;
                    tb.Top = hdi.m_INPUT_POS_TOP;
                    tb.Width = hdi.m_INPUT_WIDTH;
                    tb.Height = hdi.m_INPUT_HEIGHT;
                    tb.Font = new Font(AppConfig.TextFontName, dsp.dsp_param.m_FONT_SIZE, FontStyle.Bold);
                    tb.MaxLength = di.m_ITEM_LEN;
                    tb.Name = di.m_ITEM_DISPNAME;
                    switch (di._ITEM_ID)
                    {
                        case DspItem.ItemId.入力交換希望日:
                            tb.TextAlign = HorizontalAlignment.Left;
                            break;
                    }
                    tbDspItems.Add(hdi._ITEM_ID, tb);
                }

                // ラベル生成
                if (hdi.m_NAME_POS_TOP != 0 || hdi.m_NAME_POS_LEFT != 0)
                {
                    lblDspItems.Add(hdi._ITEM_ID, new Label());
                    lblDspItems[hdi._ITEM_ID].Left = hdi.m_NAME_POS_LEFT;
                    lblDspItems[hdi._ITEM_ID].Top = hdi.m_NAME_POS_TOP;
                    lblDspItems[hdi._ITEM_ID].AutoSize = true;
                    lblDspItems[hdi._ITEM_ID].Text = di.m_ITEM_DISPNAME;
                    lblDspItems[hdi._ITEM_ID].Tag = "";
                    Lblfontsize = dsp.dsp_param.m_FONT_SIZE;
                    lblDspItems[hdi._ITEM_ID].Font = new Font(AplInfo.LabelFontName, Lblfontsize);

                    // 項目タイプが「＊」であればラベルは表示しない
                    if (di.m_ITEM_TYPE.Equals(DspItem.ItemType.AST))
                    {
                        if (lblDspItems[hdi._ITEM_ID].Text.Contains("="))
                        {
                            lblDspItems[hdi._ITEM_ID].Text = di.m_ITEM_DISPNAME.Split('=')[0];
                            tbDspItems[hdi._ITEM_ID].Text = di.m_ITEM_DISPNAME.Split('=')[1];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// テキストボックスを取得する（ITEM_TYPE判別）
        /// </summary>
        /// <param name="itemtype"></param>
        /// <param name="tdi"></param>
        /// <returns></returns>
        protected TextBox GetTextBoxItemType(string itemtype, TBL_DSP_ITEM tdi)
        {
            TextBox tb = null;
            switch (tdi.m_ITEM_TYPE)
            {
                case DspItem.ItemType.A:   // 英数字項目
                    tb = new KanaTextBox();
                    ((KanaTextBox)tb).EntryMode = ENTRYMODE.IMEOFF_ALPHA;
                    break;
                case DspItem.ItemType.N:   // 数字項目
                    tb = new NumTextBox2();
                    tb.TextAlign = HorizontalAlignment.Right;
                    break;
                case DspItem.ItemType.S:   // 符号付数字項目
                    tb = new NumTextBox2();
                    tb.TextAlign = HorizontalAlignment.Right;
                    break;
                case DspItem.ItemType.R:   // 空白許可数字項目
                    tb = new NumTextBox2();
                    tb.TextAlign = HorizontalAlignment.Right;
                    break;
                case DspItem.ItemType.T:   // 空白許可符号付数字項目
                    tb = new NumTextBox2();
                    tb.TextAlign = HorizontalAlignment.Right;
                    break;
                case DspItem.ItemType.K:   // カナ、英数字項目
                    tb = new KanaTextBox();
                    ((KanaTextBox)tb).EntryMode = (AplInfo.OP_ROMAN) ? ENTRYMODE.IMEON_ROMAN_HANKAKU_KANA : ENTRYMODE.IMEON_HANKAKU_KANA;
                    break;
                case DspItem.ItemType.J:   // 漢字項目
                    tb = new KanaTextBox();
                    ((KanaTextBox)tb).EntryMode = (AplInfo.OP_ROMAN) ? ENTRYMODE.IMEON_ROMAN_ZENKAKU_HIRAGANA : ENTRYMODE.IMEON_ZENKAKU_HIRAGANA;
                    break;
                case DspItem.ItemType.D:   // ダミーテキストボックス
                    tb = new NumTextBox2();
                    tb.TextAlign = HorizontalAlignment.Right;
                    break;
                case DspItem.ItemType.V:   // 読取専用テキストボックス
                    tb = new TextBox();
                    tb.ReadOnly = true;
                    break;
                case DspItem.ItemType.W:   // 読取専用テキストボックス(右揃え)
                    tb = new TextBox();
                    tb.TextAlign = HorizontalAlignment.Right;
                    tb.ReadOnly = true;
                    break;
                case DspItem.ItemType.AST:
                    tb = new KanaTextBox();
                    tb.Enabled = false;
                    tb.TextAlign = HorizontalAlignment.Right;
                    break;
                default:
                    tb = new TextBox();
                    break;
            }

            // 参照モード
            tb.ReadOnly |= _ctl.IsReadOnlyItemId(tdi._ITEM_ID);
            tb.ReadOnly |= _ctl.IsDspReadOnly;
            return tb;
        }

        /// <summary>
        /// 既存のデータをセット
        /// </summary>
        /// <returns></returns>
        public void SetFormData(MeisaiInfo mei)
        {
            // テキストボックスにデータを設定する
            foreach (TBL_TRITEM item in mei.tritems.Values)
            {
                if (!tbDspItems.ContainsKey(item._ITEM_ID)) { continue; }

                SetFormItem(mei, item);
                AddComma(mei, item._ITEM_ID);
            }
            return;
        }

        /// <summary>
        /// 既存のデータをセット
        /// </summary>
        /// <param name="manageSts"></param>
        /// <param name="item"></param>
        protected virtual void SetFormItem(MeisaiInfo mei, TBL_TRITEM item)
        {
            TBL_DSP_ITEM di = mei.CurDsp.GetDspItem(item._ITEM_ID);
            if (di == null) { return; }
            tbDspItems[item._ITEM_ID].Text = "";
            tbDspItems[item._ITEM_ID].Tag = "";

            if (_ctl.IsKanryouTeisei)
            {
                // 完了訂正
                // 画面表示桁数に変換して設定
                string DspValue = CommonUtil.EditDspItem(item.m_END_DATA, di);
                tbDspItems[item._ITEM_ID].Text = DspValue;
                tbDspItems[item._ITEM_ID].Tag = DspValue;
            }
            else
            {
                // 分散エントリ
                string cd = "";
                string val = "";
                switch (mei.hosei_status.m_INPT_STS)
                {
                    case HoseiStatus.InputStatus.エントリ中:
                        // 画面表示桁数に変換して設定
                        val = CommonUtil.EditDspItem(CommonUtil.GetOcrValue(di, item.m_OCR_ENT_DATA), di);
                        break;
                    case HoseiStatus.InputStatus.ベリファイ中:
                        // 画面表示桁数に変換して設定
                        val = CommonUtil.EditDspItem(CommonUtil.GetOcrValue(di, item.m_OCR_VFY_DATA), di);
                        break;
                }

                // OCR値から名称を取得して表示する
                switch (di._ITEM_ID)
                {
                    case DspItem.ItemId.持帰銀行名:
                        cd = _econ.GetTextBox(DspItem.ItemId.持帰銀行コード).Text;
                        if (cd.Length != 0) { val = _ctl.GetBankName(DBConvert.ToIntNull(cd)); }
                        break;
                    case DspItem.ItemId.交換証券種類名:
                        cd = _econ.GetTextBox(DspItem.ItemId.交換証券種類コード).Text;
                        val = _ctl.GetBillName(DBConvert.ToIntNull(cd));
                        break;
                    case DspItem.ItemId.手形種類名:
                        cd = _econ.GetTextBox(DspItem.ItemId.手形種類コード).Text;
                        val = _ctl.GetShuruiName(DBConvert.ToIntNull(cd));
                        break;
                    case DspItem.ItemId.持帰支店名:
                        cd = _econ.GetTextBox(DspItem.ItemId.持帰支店コード).Text;
                        val = _ctl.GetBranchName(DBConvert.ToIntNull(cd));
                        break;

                    case DspItem.ItemId.和暦交換希望日:
                        cd = _econ.GetTextBox(DspItem.ItemId.入力交換希望日).Text;

                        // 和暦算出
                        iBicsCalendar cal = new iBicsCalendar();
                        int yyyymmdd = DBConvert.ToIntNull(cd);
                        string gengo = cal.getGengo(yyyymmdd);
                        if (DBConvert.ToIntNull(gengo) >= 0)
                        {
                            string wareki = iBicsCalendar.datePlanetoDisp3(DBConvert.ToStringNull(cal.getWareki(yyyymmdd)));
                            val = string.Format("{0} {1}", gengo, wareki);
                        }
                        break;
                    case DspItem.ItemId.交換日:
                        cd = _econ.GetTextBox(DspItem.ItemId.入力交換希望日).Text;

                        // 営業日算出
                        int bizDate = Calendar.GetSettleDay(DBConvert.ToIntNull(cd));
                        if (bizDate >= 0)
                        {
                            val = CommonUtil.ConvToDateFormat(bizDate, 3);
                        }
                        break;
                }
                tbDspItems[item._ITEM_ID].Text = val;
                tbDspItems[item._ITEM_ID].Tag = val;
            }

            // 入力中の情報があればその内容を設定(Tagの設定内容はそのまま)
            if (GetInputItemText(item._ITEM_ID, out string inputval))
            {
                tbDspItems[item._ITEM_ID].Text = inputval;
            }
        }

        /// <summary>
        /// カンマの追加
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="ed"></param>
        public void AddComma(MeisaiInfo mei, int itemid)
        {
            string tbdata = tbDspItems[itemid].Text.Replace(",", "");
            if (tbdata.IndexOf('?') != -1) { return; }
            if (mei.CurDsp.dsp_items.ContainsKey(itemid))
            {
                tbdata = AddCommaItemType(mei.CurDsp.dsp_items[itemid], tbdata);
            }
            tbDspItems[itemid].Text = tbdata;
            return;
        }

        /// <summary>
        /// カンマの追加(編集結果を返還)
        /// </summary>
        /// <param name="tbdata"></param>
        /// <param name="di"></param>
        public string AddComma(string tbdata, TBL_DSP_ITEM di)
        {
            if (tbdata.IndexOf('?') != -1) { return tbdata; }
            tbdata = tbdata.Replace(",", "");
            tbdata = AddCommaItemType(di, tbdata);
            return tbdata;
        }

        /// <summary>
        /// カンマの削除
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="ed"></param>
        public void DelComma(MeisaiInfo mei, int itemid)
        {
            if (mei.CurDsp.dsp_items == null) { return; }
            string tbdata = tbDspItems[itemid].Text.Replace(",", "");
            if (tbdata.IndexOf('?') != -1) { return; }
            if (mei.CurDsp.dsp_items.ContainsKey(itemid))
            {
                tbdata = DelCommnaItemType(mei.CurDsp.dsp_items[itemid], tbdata);
            }
            tbDspItems[itemid].Text = tbdata;
            return;
        }

        /// <summary>
        /// カンマの追加（ITEM_TYPE判別）
        /// </summary>
        /// <param name="di"></param>
        /// <param name="tbdata"></param>
        /// <returns></returns>
        protected string AddCommaItemType(TBL_DSP_ITEM di, string tbdata)
        {
            // カンマを削除
            string retVal = DelCommnaItemType(di, tbdata);
            bool isReplace = false;
            switch (di._ITEM_ID)
            {
                case DspItem.ItemId.金額:
                case DspItem.ItemId.入力交換希望日:
                case DspItem.ItemId.交換日:
                    isReplace = true;
                    break;
            }

            switch (di.m_ITEM_TYPE)
            {
                case DspItem.ItemType.N:   // 数字項目
                    if (isReplace)
                    {
                        // 特殊
                        retVal = AddCommaItemTypeCustom(di, retVal, true);
                    }
                    else
                    {
                        // 通常
                        retVal = DBConvert.ToLongNull(retVal).ToString("D" + DBConvert.ToStringNull(di.m_ITEM_LEN));
                    }
                    break;
                case DspItem.ItemType.S:   // 符号付数字項目
                    if (!DBConvert.ToLongNull(retVal).Equals(0) && !retVal.Equals(""))
                    {
                        // 0・空以外
                        if (isReplace)
                        {
                            // 特殊
                            retVal = AddCommaItemTypeCustom(di, retVal, true);
                        }
                        else
                        {
                            // 通常
                            retVal = DBConvert.ToLongNull(retVal).ToString("#,###");
                        }
                    }
                    else
                    {
                        retVal = "0";
                    }
                    break;
                case DspItem.ItemType.R:   // 空白許可数字項目
                    if (!retVal.Equals(""))
                    {
                        // 空以外
                        if (isReplace)
                        {
                            // 特殊
                            retVal = AddCommaItemTypeCustom(di, retVal, false);
                        }
                        else
                        {
                            // 通常
                            retVal = DBConvert.ToLongNull(retVal).ToString("D" + DBConvert.ToStringNull(di.m_ITEM_LEN));
                        }
                    }
                    break;
                case DspItem.ItemType.T:   // 空白許可符号付数字項目
                    if (!DBConvert.ToLongNull(retVal).Equals(0) && !retVal.Equals(""))
                    {
                        // 0・空以外
                        if (isReplace)
                        {
                            // 特殊
                            retVal = AddCommaItemTypeCustom(di, retVal, false);
                        }
                        else
                        {
                            // 通常
                            retVal = DBConvert.ToLongNull(retVal).ToString("#,###");
                        }
                    }
                    break;
                default:
                    if (isReplace)
                    {
                        // 特殊
                        retVal = AddCommaItemTypeCustom(di, retVal, false);
                    }
                    break;
            }
            return retVal;
        }

        /// <summary>
        /// カンマの追加（ITEM_TYPE判別）
        /// 特殊処理
        /// </summary>
        /// <param name="di"></param>
        /// <param name="tbdata"></param>
        /// <param name="isZeroAdd">空文字の場合0を設定するかどうか</param>
        /// <returns></returns>
        private string AddCommaItemTypeCustom(TBL_DSP_ITEM di, string tbdata, bool isZeroAdd)
        {
            if (!isZeroAdd && string.IsNullOrEmpty(tbdata))
            {
                // 0設定なしで空の場合
                return tbdata;
            }

            string retVal = tbdata;
            switch (di._ITEM_ID)
            {
                case DspItem.ItemId.金額:
                    retVal = DBConvert.ToLongNull(retVal).ToString("#,##0");
                    break;
                case DspItem.ItemId.入力交換希望日:
                case DspItem.ItemId.交換日:
                    // 数値変換してから再度付加する
                    retVal = DelCommnaItemType(di, retVal);
                    retVal = CommonUtil.ConvToDateFormat(DBConvert.ToIntNull(retVal), 3);
                    break;
                default:
                    break;
            }
            return retVal;
        }

        /// <summary>
        /// カンマ削除（ITEM_TYPE判別）
        /// </summary>
        /// <param name="itemtype"></param>
        /// <param name="tbdata"></param>
        /// <returns></returns>
        public string DelCommnaItemType(TBL_DSP_ITEM di, string tbdata)
        {
            string retVal = tbdata;
            //bool isReplace = false;
            switch (di._ITEM_ID)
            {
                case DspItem.ItemId.金額:
                    retVal = tbdata.Replace(",", "");
                    //isReplace = true;
                    break;
                case DspItem.ItemId.入力交換希望日:
                case DspItem.ItemId.交換日:
                    retVal = tbdata.Replace(".", "");
                    //isReplace = true;
                    break;
            }

            //if (isReplace)
            //{
            //    return retVal;
            //}
            switch (di.m_ITEM_TYPE)
            {
                case DspItem.ItemType.N:   // 数字項目
                    break;
                case DspItem.ItemType.S:   // 符号付数字項目
                    retVal = retVal.Replace(",", "");
                    if (!DBConvert.ToLongNull(retVal).Equals(0))
                    {
                        retVal = retVal.TrimStart('0');
                    }
                    break;
                case DspItem.ItemType.R:   // 空白許可数字項目
                    break;
                case DspItem.ItemType.T:   // 空白許可符号付数字項目
                    retVal = retVal.Replace(",", "");
                    if (!DBConvert.ToLongNull(retVal).Equals(0))
                    {
                        retVal = retVal.TrimStart('0');
                    }
                    break;
                default:
                    break;
            }
            return retVal;
        }

        /// <summary>
        /// テキストボックスクリア
        /// </summary>
        public void ClearDspItems()
        {
            // コントロールは削除しないで次の画面も使いまわす
            if (_itemMgr.EntParams.IsInitControl) { return; }

            tbDspItems.Clear();
            //tbDspItems = null;

            lblDspItems.Clear();
            //lblDspItems = null;
        }

        /// <summary>
        /// 数字項目の入力チェック
        /// </summary>
        /// <param name="itemid">画面項目番号</param>
        /// <param name="ed"></param>
        /// <param name="e"></param>
        public void InputCheck(MeisaiInfo mei, int itemid, KeyPressEventArgs e)
        {
            if (mei.CurDsp.dsp_items == null) { return; }
            if (mei.CurDsp.dsp_items.ContainsKey(itemid))
            {
                InputCheckItemType(mei.CurDsp.dsp_items[itemid].m_ITEM_TYPE, e);
            }
        }

        /// <summary>
        /// 数字項目の入力チェック（ITEM_TYPE判別）
        /// </summary>
        /// <param name="itemtype"></param>
        /// <param name="e"></param>
        protected void InputCheckItemType(string itemtype, KeyPressEventArgs e)
        {
            switch (itemtype)
            {
                case DspItem.ItemType.N:   // 数字項目
                case DspItem.ItemType.R:   // 空白許可数字項目
                    switch (e.KeyChar)
                    {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                        case (char)0x8:
                            break;
                        case '.':
                            e.Handled = true;
                            SendKeys.Send("000");
                            break;
                        default:
                            e.Handled = true;
                            break;
                    }
                    break;
                case DspItem.ItemType.S:   // 符号付数字項目
                case DspItem.ItemType.T:   // 空白許可符号付数字項目
                    switch (e.KeyChar)
                    {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                        case '-':
                        case (char)0x8:
                            break;
                        case '.':
                            e.Handled = true;
                            SendKeys.Send("000");
                            break;
                        default:
                            e.Handled = true;
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// DUP用データ保存
        /// </summary>
        public void SaveDupData()
        {
            // 初回は作成して終わり
            if (dup_tbDspItems == null)
            {
                dup_tbDspItems = CreateDupList(tbDspItems);
                return;
            }

            // 現在のDUPを退避
            SortedList<int, DupDspItem> old_dup_tbDspItems = CreateDupList(tbDspItems);
            dup_tbDspItems.Clear();

            // 次画面のDUP作成
            SortedList<int, DupDspItem> new_dup_tbDspItems = CreateDupList(tbDspItems);

            // 同一項目名の入力値は新しいエントリー値で上書きする
            foreach (DupDspItem newDupItem in new_dup_tbDspItems.Values)
            {
                DupDspItem oldItem = GetDupDspItem(old_dup_tbDspItems, newDupItem.ItemName);
                if (oldItem != null)
                {
                    newDupItem.DupTextBox.Text = string.IsNullOrEmpty(newDupItem.DupTextBox.Text) ? oldItem.Text : newDupItem.DupTextBox.Text;
                }
                dup_tbDspItems.Add(newDupItem.ItemId, newDupItem);
            }
        }

        /// <summary>
        /// DUPデータを作成する
        /// </summary>
        /// <param name="dspItems"></param>
        /// <returns></returns>
        private SortedList<int, DupDspItem> CreateDupList(SortedList<int, TextBox> dspItems)
        {
            SortedList<int, DupDspItem> dup_items = new SortedList<int, DupDspItem>();
            foreach (KeyValuePair<int, TextBox> keyVal in dspItems)
            {
                int itemid = keyVal.Key;
                string itemname = keyVal.Value.Name;
                TextBox tbDup = new TextBox();
                tbDup.Name = keyVal.Value.Name;
                tbDup.Text = keyVal.Value.Text;
                tbDup.Tag = keyVal.Value.Tag;
                tbDup.ReadOnly = keyVal.Value.ReadOnly;
                tbDup.Enabled = keyVal.Value.Enabled;
                tbDup.Font = keyVal.Value.Font;
                tbDup.Size = keyVal.Value.Size;
                tbDup.MaxLength = keyVal.Value.MaxLength;
                dup_items.Add(itemid, new DupDspItem(_curMei.trmei.m_DSP_ID, itemid, itemname, tbDup));
            }
            return dup_items;
        }

        /// <summary>
        /// DUP用データクリア
        /// </summary>
        public void ClearDupData()
        {
            dup_tbDspItems.Clear();
            dup_tbDspItems = null;
        }

        /// <summary>
        /// 指定した ITEM_NAME の DUP 情報を取得する
        /// </summary>
        /// <param name="dupDspItems"></param>
        /// <param name="itemname"></param>
        /// <returns></returns>
        public DupDspItem GetDupDspItem(SortedList<int, DupDspItem> dupDspItems, string itemname)
        {
            foreach (KeyValuePair<int, DupDspItem> keyVal in dupDspItems)
            {
                if (keyVal.Value.ItemName.Equals(itemname)) { return keyVal.Value; }
            }
            return null;
        }

        /// <summary>
        /// 入力中項目復元用データ保存
        /// </summary>
        public void SaveInputData(bool FocusSave = true)
        {
            // クリア
            ClearInputData();

            //　現在の項目内容を保存
            input_DspItems = new InputDspItems();
            // focus情報を保存
            input_DspItems.FocusItemId = -1;
            if (FocusSave)
            {
                // 保存時のみ
                input_DspItems.FocusItemId = this.FocusedItemId;
            }
            // 項目情報を保存
            foreach (KeyValuePair<int, TextBox> keyVal in tbDspItems)
            {
                int itemid = keyVal.Key;
                string itemname = keyVal.Value.Name;
                TextBox tbDup = new TextBox();
                tbDup.Name = keyVal.Value.Name;
                tbDup.Text = keyVal.Value.Text;
                tbDup.Tag = keyVal.Value.Tag;
                tbDup.ReadOnly = keyVal.Value.ReadOnly;
                tbDup.Enabled = keyVal.Value.Enabled;
                input_DspItems.tbDspItems.Add(itemid, new DupDspItem(_curMei.trmei.m_DSP_ID, itemid, itemname, tbDup));
            }
        }

        /// <summary>
        /// 入力中項目情報指定削除
        /// </summary>
        public void DeleteInputData(int ItemID)
        {
            if (input_DspItems == null)
            {
                // なければ終了
                return;
            }

            if (input_DspItems.tbDspItems.ContainsKey(ItemID))
            {
                input_DspItems.tbDspItems.Remove(ItemID);
            }
        }

        /// <summary>
        /// 入力中項目復元用データクリア
        /// </summary>
        public void ClearInputData()
        {
            if (input_DspItems == null)
            {
                // クリア済であれば終了
                return;
            }
            input_DspItems.Clear();
            input_DspItems = null;
        }

        /// <summary>
        /// 指定した ITEM_ID の 入力中情報を取得する
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool GetInputItemText(int itemid, out string Value)
        {
            // 初期化
            Value = string.Empty;

            if (input_DspItems == null)
            {
                // クリア済であれば終了
                return false;
            }

            if (!input_DspItems.tbDspItems.ContainsKey(itemid))
            {
                // 対象のItemIDがなければ終了
                return false;
            }

            // 項目内容を設定
            Value = input_DspItems.tbDspItems[itemid].Text;
            return true;
        }

        /// <summary>
        /// 指定した ITEM_ID の 入力中情報を取得する
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool GetInputFocusItemId(out int itemid)
        {
            // 初期化
            itemid = -1;

            if (input_DspItems == null)
            {
                // クリアされていれば終了
                return false;
            }

            if (!input_DspItems.tbDspItems.ContainsKey(input_DspItems.FocusItemId))
            {
                // 対象のItemIDがなければ終了
                return false;
            }

            // 最新の表示項目と比較
            if (!tbDspItems.ContainsKey(input_DspItems.FocusItemId))
            {
                // 対象のItemIDがなければ終了
                return false;
            }

            // 入力中項目情報を設定
            itemid = input_DspItems.FocusItemId;
            return true;
        }

        // *******************************************************************
        // 内部クラス
        // *******************************************************************

        /// <summary>
        /// DUP情報
        /// </summary>
        public class DupDspItem
        {
            public int DspId { get; set; }
            public int ItemId { get; set; }
            public string ItemName { get; set; }
            public TextBox DupTextBox { get; set; }
            public string Text { get { return DupTextBox.Text; } }

            public DupDspItem(int dspid, int itemid, string itemname, TextBox tb)
            {
                this.DspId = dspid;
                this.ItemId = itemid;
                this.ItemName = itemname;
                this.DupTextBox = tb;
            }
        }

        /// <summary>
        /// 入力中情報
        /// </summary>
        public class InputDspItems
        {
            /// <summary>選択中項目</summary>
            public int FocusItemId { get; set; }
            /// <summary>入力中項目一覧(DUP情報クラスを流用)</summary>
            public SortedList<int, DupDspItem> tbDspItems { get; set; }

            public InputDspItems()
            {
                this.FocusItemId = -1;
                this.tbDspItems = new SortedList<int, DupDspItem>();
            }

            public void Clear()
            {
                this.FocusItemId = -1;
                this.tbDspItems.Clear();
            }
        }

    }
}
