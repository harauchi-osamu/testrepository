using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;

namespace SubRoutineApi
{
    public class SubRoutine
    {
        protected ItemManager _itemMgr = null;

        private SortedList<int, TextBox> _dspTextBoxes;
        private TBL_TRMEI _trmei;
        private SortedList<int, TBL_TRITEM> _tritems;
        private SortedList<int, TBL_DSP_ITEM> _dsp_items;

        string _itemsub = "";
        private Encoding _enc;
        private CheckValue _valtype;

        public enum CheckValue
        {
            TEXT_BOX,
            ENT_DATA,
            VFY_DATA,
            OCR_ENT_DATA,
            OCR_VFY_DATA
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SubRoutine(SortedDictionary<int, TBL_BANKMF> banks, SortedDictionary<int, TBL_BRANCHMF> branches, SortedDictionary<int, TBL_SYURUIMF> syuruimfs, 
                          SortedList<int, TextBox> dspTextBoxes, TBL_TRMEI trmei, SortedList<int, TBL_TRITEM> tritems, SortedList<int, TBL_DSP_ITEM> dsp_items, CheckValue valtype = CheckValue.TEXT_BOX)
        {
            MasterManager mst = new MasterManager();
            _itemMgr = new ItemManager(mst);

            _dspTextBoxes = dspTextBoxes;
            _trmei = trmei;
            _tritems = tritems;
            _dsp_items = dsp_items;
            _enc = Encoding.GetEncoding("Shift_JIS", EncoderFallback.ExceptionFallback, DecoderFallback.ReplacementFallback);
            _valtype = valtype;

            // 休日カレンダー取得
            iBicsCalendar cal = new iBicsCalendar();
            cal.SetHolidays();
            // マスタデータの設定
            _itemMgr.FetchAllData(banks, branches, syuruimfs);
        }

        /// <summary>
        /// 入力値読み替えサブルーチン
        /// </summary>
        /// <param name="itemid">チェックをするtbBoxのkey</param>
        /// <param name="subroutine">サブルーチン名</param>
        /// <returns></returns>
        public string ReplaceBySubRoutine(int itemid, string subroutine)
        {
            string res = "";
            if (!_dspTextBoxes.ContainsKey(itemid)) { return ""; }

            // 読み替えサブルーチンを定義したらこのリストに追加して、メソッドを作成する
            switch (subroutine)
            {
                case "SMT": // 前スペース右詰にする
                    res = SMT(itemid);
                    break;
                case "TMS":
                    res = TMS(itemid);
                    break;
                case "NENDO": // 4月以外の場合、当年の和暦年を返す
                    res = GetWarekiYear(_dspTextBoxes[itemid].Text.Trim());
                    break;
                case "CIF": // ミニCIFによる本人確認マスタ取得
                    res = CIF(subroutine, itemid);
                    break;
                case "SYO": // 書類区分による書類マスタ取得
                    res = SYO(subroutine, itemid);
                    break;
                case "STN": // 店番による支店マスタ取得
                    res = STN(subroutine, itemid);
                    break;
                // 2021.01.21 仕様変更：委託者、支店チェック k_harakawa add start
                case "STN2": // 店番による支店マスタ取得
                    res = STN2(subroutine, itemid);
                    break;
                // 2021.01.21 仕様変更：委託者、支店チェック k_harakawa add end
                case "ITK": // 委託者コードによる委託者マスタ取得
                    res = ITK(subroutine, itemid);
                    break;
                case "KMK": // 科目コードによる科目マスタ取得
                    res = KMK(subroutine, itemid);
                    break;
                default:
                    break;
            }
            return res;
        }

        /// <summary>
        /// サブルーチン入力チェック
        /// </summary>
        /// <param name="itemid">チェックをするtbBoxのkey</param>
        /// <param name="subroutine">サブルーチン名</param>
        /// <returns>エラーがあるときはエラーメッセージが返る</returns>
        public string CheckSubRoutine(int itemid, string subroutine, string itemsub)
        {
            string res = "";
            if (!_dspTextBoxes.ContainsKey(itemid)) { return ""; }
            _itemsub = itemsub;
            string chkStr = _dspTextBoxes[itemid].Text.Trim();

            // 入力チェックのサブルーチンを定義したらこのリストに追加して、メソッドを作成する
            switch (subroutine)
            {
                case "NCT": // 入力値を'*'に変更する
                    break;
                case "ADC": // 西暦暦日チェック
                    res = ADC(itemid);
                    break;
                case "ADD": // 西暦営業日チェック
                    res = ADD(itemid);
                    break;
                case "ANO": // 文字未入力チェック
                    res = ANO(_dspTextBoxes[itemid].Text);
                    break;
                case "CD1": // 取組番号チェック
                    break;
                case "CD2": // 依頼人コードチェック
                    break;
                case "CD3": // OCRコードチェック
                    break;
                case "CD4": // 郵便振替ＭＴサービス払込取扱票のＣＤチェック
                    break;
                case "NNO": // 0禁止チェック
                    res = NNO(_dspTextBoxes[itemid].Text.Trim());
                    break;
                case "SCK": // 店番の存在チェック(支店)
                    break;
                case "BRK": // 空白スペースのみ許容
                    res = BRK(_dspTextBoxes[itemid].Text);
                    break;
                case "ACK": // 文字チェック
                    res = ACK(subroutine, itemid);
                    break;
                case "FLW": // 指定項目フォローチェックルーチン
                    break;
                case "STG": // 指定月12チェックルーチン
                    break;
                case "KOZ": // 口座番号チェックサブルーチン
                    break;
                case "YUK": // 預金種目チェックサブルーチン
                    break;
                case "MCK": // 銀行名カナ曖昧検索
                    break;
                case "STM": // 支店名カナ曖昧検索
                    break;
                case "YMD": // 和暦暦日チェック
                    res = YMD(subroutine, itemid);
                    break;
                case "EMD": // 和暦営業日チェック
                    res = EMD(subroutine, itemid);
                    break;
                case "KEI": // １画面内合計チェック(先頭項目が合計値)
                    break;
                case "MST": // 名称マスタ存在チェック
                    break;
                case "NCK": // 数字チェック
                    res = NCK(_dspTextBoxes[itemid].Text.Replace(",", ""));
                    break;
                case "SUM": // １画面内合計チェック
                    res = SUM(subroutine, itemid);
                    break;
                case "TCK": // 営業店・本部かどうかチェック
                    break;
                case "CDVNULL": //チェックデジット実施／未実施判定+チェックデジットを返す。
                    //画面上のＣＤＶ未処理有無にチェック有の場合は、ＣＤＶを行わない
                    break;
                case "ADK": // 西暦営業日チェック（基準日、月の範囲指定）
                case "AMD": // 西暦営業日チェック（月の範囲指定）
                case "BNKANK":  // 許容文字チェック
                case "ECK": // 和暦営業日チェック（基準日、月の範囲指定）
                case "NEN": // 年度チェック
                case "PRF": // プルーフ金額単位指定
                case "RGT": // 文字の右詰
                    break;
                case "NENCMP": // 年度比較(1:>,2:≦,3:<,4:≧）
                    res = NENCMP(itemid);
                    break;
                case "NENCMP2": // 年度比較(1:>,2:≦,3:<,4:≧）
                    // 引数４(0:加算／0以外:減算)
                    break;
                case "NENACADE": // 年度比較
                    res = NENACADE(itemid);
                    break;
                case "CKT1": // 1項目を使用してのチェックテーブル存在チェック
                    res = CKT1(itemid);
                    break;
                case "CKT2": // 2項目を使用してのチェックテーブル存在チェック
                    res = CKT2(itemid);
                    break;
                case "CKT3": // 1項目を使用してのチェックテーブル２存在チェック
                    res = CKT3(itemid);
                    break;
                case "YMACADE": // 年月チェック１：当月(和暦)までかチェック
                    res = YMACADE(itemid);
                    break;
                case "YM": // 年月チェック２：暦日チェックを行う
                    res = YM(itemid);
                    break;
                case "YMZERO": // 年月チェック３：暦日チェック（未入力可）を行う
                    res = YMZERO(itemid);
                    break;
                case "YMDACADE": // 日付チェック１：前営業日までかチェックを行う
                    res = YMDACADE(itemid);
                    break;
                case "MD": // 日付チェック３：暦日（月日）チェックを行う
                    res = MD(itemid);
                    break;
                case "YMACADE2": // 日付チェック４：当月迄か、前営業日までかチェックを行う
                    res = YMACADE2(subroutine, itemid);
                    break;
                case "WANENACADE": // (和暦)年度比較
                    res = WANENACADE(itemid);
                    break;
                case "KANACK":    // カナ入力チェック
                    res = KANACHK(itemid);
                    break;
                case "CKT2B": // 2項目を使用してのチェックテーブル存在チェックCKT2の逆版
                    res = CKT2B(itemid);
                    break;
                case "YMDCOMP": //前営業日、前々営業日、当日との日付チェック
                    res = YMDCOMP(itemid);
                    break;
                case "NUMCHK":  //数値チェック（数値以外の文字はエラー）
                    res = NUMCHK(itemid);
                    break;
                case "NUMCMP": // 年度比較(1:>,2:≦,3:<,4:≧）
                    res = NUMCMP(itemid);
                    break;
                case "ZENGIN": // 全銀カナ文字チェック
                    res = ZENGIN(_dspTextBoxes[itemid].Text.Trim());
                    break;
                case "BUNRUI": // 分類名称チェック
                    break;
                case "ANCK":     // 英数字チェック(空白は許容しない)
                    res = ANCK(_dspTextBoxes[itemid].Text);
                    break;
                case "AaNCK":    // 英(大小)数字チェック(空白は許容しない)
                    res = AaNCK(_dspTextBoxes[itemid].Text);
                    break;
                case "EMB":    // 外字チェック
                    res = EMB(itemid);
                    break;
                case "PCK":
                    // PCKは定義のみ（ここで処理せずにデータ作成時）
                    break;
                case "ITKCK":
                    // 委託者マスタチェック
                    res = ITKCK(itemid);
                    break;
                case "STNCK":
                    // 支店マスタチェック
                    res = STNCK(itemid);
                    break;
                case "BANK":
                    // 銀行チェック
                    res = BANK(itemid);
                    break;
                case "KIJITSU":
                    // 期日チェック
                    res = KIJITSU(itemid);
                    break;
                case "AMOUNT":
                    // 金額チェック
                    res = AMOUNT(itemid);
                    break;
                case "SYURUI":
                    // 証券種類チェック
                    res = SYURUI(itemid, itemsub);
                    break;
                case "BRANCH":
                    // 支店チェック
                    res = BRANCH(itemid);
                    break;
                case "ACCD":
                    // 口座番号チェックデジットチェック
                    res = ACCD(itemid, itemsub);
                    break;
                case "PAYER":
                    // 支払人チェック
                    res = PAYER(subroutine, itemid);
                    break;
                case "BANK2":
                    // 銀行チェック(読替項目)
                    // チェック前に読替処理は実施しておくこと
                    res = BANK2(itemid);
                    break;
                default:
                    res = "サブルーチン名「" + subroutine + "」が存在しません";
                    break;
            }

            return res;
        }

        /// <summary>
        /// 歳入金における年度読み換え処理
        /// 交換日(収納日)の和暦年を取得
        /// </summary>
        /// <returns></returns>
        private string GetWarekiYear(string val)
        {
            //int kodate = ProcAplInfo.KoDate();
            //DateTime procdate = new DateTime(kodate / 10000, kodate % 10000 / 100, kodate % 10000 % 100);
            //if (!val.Equals("")) { return ""; }
            //CultureInfo culture = new CultureInfo("ja-JP", true);
            //culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            //return procdate.ToString("yy", culture);
            return "";
        }

        /// <summary>
        /// 歳入金における出納整理期間チェック
        /// 整理期間（4,5月）は、必ず年度入力必須、空白NG。
        /// 期間以外は空欄であること
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool NENDOCK(string str)
        {
            //int kodate = ProcAplInfo.KoDate();
            //DateTime procdate = new DateTime(kodate / 10000, kodate % 10000 / 100, kodate % 10000 % 100);
            //DateTime proc1date = procdate.AddYears(-1); // 一年前

            ////  ４、５月のみ対象
            //if (procdate.Month != 4 && procdate.Month != 5)
            //{
            //    return true;
            //}
            //else
            //{
            //    CultureInfo culture = new CultureInfo("ja-JP", true);
            //    culture.DateTimeFormat.Calendar = new JapaneseCalendar();

            //    // 当年か前年のみ許容
            //    if (str.Equals(procdate.ToString("yy", culture)) || str.Equals(proc1date.ToString("yy", culture)))
            //    {
            //        return true;
            //    }
            //    return false;
            //}
            return false;
        }

        /// <summary>
        /// 歳入金における出納整理期間チェック
        /// 整理期間も、年度入力可、空白OK。
        /// 期間以外は空欄であること
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool NENDOCKSPC(string str)
        {
            //int kodate = ProcAplInfo.KoDate();
            //DateTime procdate = new DateTime(kodate / 10000, kodate % 10000 / 100, kodate % 10000 % 100);
            //DateTime proc1date = procdate.AddYears(-1); // 一年前

            ////  ４、５月のみ対象
            //if (procdate.Month != 4 && procdate.Month != 5)
            //{
            //    return true;
            //}
            //else
            //{
            //    CultureInfo culture = new CultureInfo("ja-JP", true);
            //    culture.DateTimeFormat.Calendar = new JapaneseCalendar();

            //    // 当年か前年のみ許容
            //    if (str.Equals(procdate.ToString("yy", culture)) || str.Equals(proc1date.ToString("yy", culture)))
            //    {
            //        return true;
            //    }
            //    // 空白入力可
            //    else if (str.Equals(""))
            //    {
            //        return true;
            //    }
            //    return false;
            //}
            return false;
        }

        private string NNO(string str)
        {
            int num;
            if (str.Equals("")) { return "入力値がありません。"; }
            if (Int32.TryParse(str, out num) && num == 0) { return "ゼロは入力できません。"; }
            return "";
        }

        private string BRK(string str)
        {
            if (str.Equals("")) { return "ブランクを入力してください。"; }
            if (!str.Trim().Equals("")) { return "ブランク入力だけが許可されています。"; }
            return "";
        }

        private string ANO(string str)
        {
            if (str == null) { return "未入力です。"; }
            if (str.Length == 0) { return "未入力です。"; }
            return "";
        }

        private string SMT(int itemid)
        {
            // 前方半角空白埋め
            string str = _dspTextBoxes[itemid].Text.Trim();
            TBL_DSP_ITEM di = _dsp_items[itemid];
            return str.PadLeft(di.m_ITEM_LEN, ' ');
        }

        private string TMS(int itemid)
        {
            // 前方ゼロ埋め
            string str = _dspTextBoxes[itemid].Text.Trim();
            TBL_DSP_ITEM di = _dsp_items[itemid];
            return str.PadLeft(di.m_ITEM_LEN, '0');
        }

        /// <summary>
        /// コントロールのTextBoxのなかから金額合計と金額内訳を探して合算する。
        /// 合算結果が0より大きいときtrue
        /// </summary>
        /// <returns></returns>
        private bool IsSumAmountOverZero()
        {
            decimal sum = 0;
            //foreach (TextBox tb in _dspTextBox.Values)
            //{
            //    foreach (string str in ProcAplInfo.Gokei_Word)
            //    {
            //        if (tb.Name.Contains(str)) { sum += DBConvert.ToLongNull(tb.Text.Replace(",", "")); }
            //    }
            //    foreach (string str in ProcAplInfo.Utiwake_Word)
            //    {
            //        if (tb.Name.Contains(str)) { sum += DBConvert.ToLongNull(tb.Text.Replace(",", "")); }
            //    }
            //}

            return (sum > 0) ? true : false;
        }

        /// <summary>
        /// コントロールのTextBoxのなかから金額合計と金額内訳を探して、内訳の合計と合計金額が違うか判定。
        /// 合算結果が一致しているときtrue
        /// </summary>
        /// <returns></returns>
        private bool IsSumSame()
        {
            //decimal gsum = 0;
            //decimal usum = 0;
            //string gBuff = "";
            //string uBuff = "";
            //bool IsSummerized;

            //foreach (TextBox tb in _dspTextBox.Values)
            //{
            //    IsSummerized = false;
            //    foreach (string str in ProcAplInfo.Gokei_Word)
            //    {
            //        if (tb.Name.Contains(str))
            //        {
            //            gBuff = tb.Text.Replace(",", "");
            //            gsum += DBConvert.ToLongNull(tb.Text.Replace(",", ""));
            //            IsSummerized = true;
            //            break;
            //        }
            //    }
            //    if (IsSummerized) { continue; }
            //    foreach (string str in ProcAplInfo.Utiwake_Word)
            //    {
            //        if (tb.Name.Contains(str))
            //        {
            //            uBuff += tb.Text.Replace(",", "");
            //            usum += DBConvert.ToLongNull(tb.Text.Replace(",", ""));
            //            break;
            //        }
            //    }
            //}

            //if (gBuff.Length == 0) { return true; }
            //if (uBuff.Length == 0) { return true; }
            //if (gsum == usum) { return true; }

            return false;
        }

        /// <summary>
        /// コントロールのTextBoxのなかから金額合計と金額内訳を探して、内訳の合計と合計金額が違うか判定。
        /// ただし報奨金は合計の際にマイナスする。
        /// 合算結果が一致しているときtrue
        /// </summary>
        /// <returns></returns>
        private bool IsHosyoSumSame()
        {
            //decimal gsum = 0;
            //decimal usum = 0;
            //string gBuff = "";
            //string uBuff = "";
            //bool IsSummerized;

            //foreach (TextBox tb in _dspTextBox.Values)
            //{
            //    IsSummerized = false;
            //    foreach (string str in ProcAplInfo.Gokei_Word)
            //    {
            //        if (tb.Name.Contains(str))
            //        {
            //            gBuff = tb.Text.Replace(",", "");
            //            gsum += DBConvert.ToLongNull(tb.Text.Replace(",", ""));
            //            IsSummerized = true;
            //            break;
            //        }
            //    }
            //    if (IsSummerized) { continue; }
            //    foreach (string str in ProcAplInfo.Utiwake_Word)
            //    {
            //        if (tb.Name.Contains(str))
            //        {
            //            uBuff += tb.Text.Replace(",", "");
            //            if (tb.Name.Contains("報奨金")) { usum = usum - DBConvert.ToLongNull(tb.Text.Replace(",", "")); break; }
            //            else { usum += DBConvert.ToLongNull(tb.Text.Replace(",", "")); break; }
            //        }
            //    }
            //}

            //if (gBuff.Length == 0) { return true; }
            //if (uBuff.Length == 0) { return true; }
            //if (gsum != usum) { return false; }

            return true;
        }

        /// <summary>
        /// 数字チェック
        /// 指定の範囲内又は指定の値であれば、true
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string NCK(string str)
        {
            long num = 0;
            string itemsub = _itemsub.Replace("NCK ", "");
            string[] chkparams = new string[itemsub.Split(',').Length];
            int cnt = 0;
            long startnum = 0;
            long endnum = 0;

            foreach (string param in itemsub.Split(','))
            {
                chkparams.SetValue(param, cnt);
                cnt++;
            }

            for (int i = 0; i < chkparams.Length; i++)
            {
                // 引数がNULLで、入力値が空白であれば、OK
                if (chkparams[i].ToUpperInvariant() == "NULL" && str == "") { return ""; }
                else if (chkparams[i].ToUpperInvariant() != "NULL")
                {

                    num = DBConvert.ToLongNull(str);
                    // 引数に「:」が含まれていれば、範囲内か比較
                    // 含まれていなければ、一致するか比較
                    if (chkparams[i].IndexOf(":") != -1)
                    {
                        startnum = DBConvert.ToLongNull(chkparams[i].Split(':')[0]);
                        endnum = DBConvert.ToLongNull(chkparams[i].Split(':')[1]);

                        if ((num >= startnum) && (num <= endnum)) { return ""; }
                    }
                    else
                    {
                        if (DBConvert.ToLongNull(str) == DBConvert.ToLongNull(chkparams[i])) { return ""; }
                    }
                }
            }
            return "範囲指定エラー " + _itemsub.Replace("NCK ", "");
        }

        /// <summary>
        /// 年度項目の比較。(入力項目Ａと入力項目Ｂのパターン）
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public string NENCMP(int key)
        {
            string cd = "";
            int idx = 0;

            // "NENCMP △,±XX"から"NENCMP"及びスペースを削除する。
            cd = _itemsub.Replace("NENCMP ", "").Trim();

            // カンマ区切りのデータを変数に格納する。
            string[] str = cd.Split(',');

            if (str.Length != 2)
            {
                //MSG内容変更(2015.12.16)
                //return "マスタ定義がおかしい！！";
                return "入力チェック仕様に誤りがあります。";
            }

            //カンマ区切りの１つ目をパターン
            //       ２つ目を比較先項目
            string pattern = str[0];
            idx = Convert.ToInt32(str[1]);

            if (_dspTextBoxes[key].Text.Trim() == "")
            {
                return "値が設定されていません";
            }
            if (_dspTextBoxes[key + idx].Text.Trim() == "")
            {
                return "値が設定されていません";
            }

            int NendoCMP01 = Convert.ToInt32(_dspTextBoxes[key].Text.Trim());
            int NendoCMP02 = Convert.ToInt32(_dspTextBoxes[key + idx].Text.Trim());

            if (pattern.Equals("1"))
            {
                //比較元>自分
                if (NendoCMP02 > NendoCMP01)
                {
                    return "";
                }
            }
            else if (pattern.Equals("2"))
            {
                //比較元≦自分自身
                if (NendoCMP02 <= NendoCMP01)
                {
                    return "";
                }
            }
            else if (pattern.Equals("3"))
            {
                //比較元<自分自身
                if (NendoCMP02 < NendoCMP01)
                {
                    return "";
                }
            }
            else if (pattern.Equals("4"))
            {
                //比較元≧自分自身
                if (NendoCMP02 >= NendoCMP01)
                {
                    return "";
                }
            }

            //対象項目（年度or年月日）によってエラーメッセージ変更
            if (NendoCMP01.ToString().Length == 6)
            {
                return "年月日項目比較エラーが発生しました";
            }
            else
            {
                return "年度項目比較エラーが発生しました";
            }
        }

        /// <summary>
        /// 年度項目の比較。(当年度）
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public string NENACADE(int key)
        {
            return "";
        }

        /// <summary>
        /// 入力された値がチェックテーブルに存在しているか判定。
        /// （１項目を使用してチェックする処理）
        /// 存在しなければ、エラーメッセージを表示する。
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public string CKT1(int key)
        {
            //チェックテーブルを参照する際の区分格納エリア 
            string kbn = "";

            //チェックテーブルを参照する際のコード格納エリア 
            string cd = _dspTextBoxes[key].Text.Trim(); ;

            if (cd == "") { return "値が入力されていません"; }


            // "CKT1 XXX"から"CKT1"及びスペースを削除する。
            kbn = _itemsub.Replace("CKT1 ", "").Trim();

            if (!ChkCheckTbl(kbn, cd)) { return "チェックテーブル未登録エラー"; }

            return "";
        }

        /// <summary>
        /// 入力された値がチェックテーブルに存在しているか判定。
        /// （２項目を使用してチェックする処理）
        /// 存在しなければ、エラーメッセージを表示する。
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public string CKT2(int key)
        {
            //チェックテーブルを参照する際の区分格納エリア 
            string kbnTmp = "";
            string kbn = "";

            //チェックテーブル参照用キーの設定
            string cd = _dspTextBoxes[key].Text.Trim();

            if (cd == "") { return "値が入力されていません"; }

            // "CKT2 14,-n"から"CKT2"及びスペースを削除する。
            kbnTmp = _itemsub.Replace("CKT2 ", "").Trim();

            // カンマ区切りのデータを変数に格納する。
            // "14,-n"を14と-nに分ける。
            string[] param = kbnTmp.Split(',');

            if (param.Length != 2)
            {
                return "マスタ定義がおかしい！！";
            }

            kbn = param[0] + _dspTextBoxes[key + DBConvert.ToIntNull(param[1])].Text;

            if (!ChkCheckTbl(kbn, cd)) { return "チェックテーブル未登録エラー"; }

            return "";
        }

        /// <summary>
        /// 入力された値がチェックテーブルに存在しているか判定。
        /// （２項目を使用してチェックする処理）
        /// 存在しなければ、エラーメッセージを表示する。
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public string CKT2B(int key)
        {
            //チェックテーブルを参照する際の区分格納エリア 
            string kbnTmp = "";
            string kbn = "";

            //チェックテーブル参照用キーの設定
            string kbn2 = _dspTextBoxes[key].Text.Trim();

            if (kbn2 == "") { return "値が入力されていません"; }

            // "CKT2 14,-n"から"CKT2"及びスペースを削除する。
            kbnTmp = _itemsub.Replace("CKT2B ", "").Trim();

            // カンマ区切りのデータを変数に格納する。
            // "14,-n"を14と-nに分ける。
            string[] param = kbnTmp.Split(',');

            if (param.Length != 2)
            {
                return "マスタ定義がおかしい！！";
            }
            kbn = param[0] + kbn2;
            string cd = _dspTextBoxes[key + DBConvert.ToIntNull(param[1])].Text;

            if (!ChkCheckTbl(kbn, cd)) { return "チェックテーブル未登録エラー"; }

            return "";
        }

        /// <summary>
        /// 入力された値がチェックテーブルに存在しているか判定。
        /// （１項目を使用してチェックする処理）
        /// 存在しなければ、エラーメッセージを表示する。
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public string CKT3(int key)
        {
            return "";
        }

        /// <summary>
        /// チェックテーブルに存在するかどうか判定。
        /// </summary>
        /// <returns></returns>
        private bool ChkCheckTbl(string Kbn, string Cd)
        {
            return false;
        }

        /// <summary>
        /// チェックテーブル２に存在するかどうか判定。
        /// </summary>
        /// <returns></returns>
        private bool ChkCheckTbl2(string Kbn, string Cd)
        {
            return false;
        }

        /// <summary>
        /// 年月（和暦）の比較。(当月までの範囲であるかをチェック）
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public string YMACADE(int key)
        {
            return "";
        }

        /// <summary>
        /// 年月日（和暦）の比較。(前営業日までの範囲であるかをチェック）
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public string YMDACADE(int key)
        {
            return "";
        }

        /// <summary>
        /// 年月（和暦）の暦日チェック。
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public string YM(int key)
        {
            int Nen = 0;          //入力値（年部分)
            int Tsuki = 0;        //入力値（月部分） 

            if (_dspTextBoxes[key].Text.Trim() == "")
            {
                return "値が設定されていません";
            }

            Nen = Convert.ToInt32(_dspTextBoxes[key].Text.Trim()) / 100;
            Tsuki = Convert.ToInt32(_dspTextBoxes[key].Text.Trim()) % 100;

            if (!((Tsuki >= 1) & (Tsuki <= 12)))
            {
                return "年月に暦日でない値が設定されています(月)";
            }

            return "";
        }

        /// <summary>
        /// 年月（和暦）の暦日チェック。（未入力可版）
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public string YMZERO(int key)
        {
            int Nen = 0;          //入力値（年部分)
            int Tsuki = 0;        //入力値（月部分） 

            if (_dspTextBoxes[key].Text.Trim() == "")
            {
                return "";
            }

            Nen = Convert.ToInt32(_dspTextBoxes[key].Text.Trim()) / 100;
            Tsuki = Convert.ToInt32(_dspTextBoxes[key].Text.Trim()) % 100;

            if ((Nen == 0) & (Tsuki == 0))
            {
                return "";
            }

            if (!((Tsuki >= 1) & (Tsuki <= 12)))
            {
                return "年月に暦日でない値が設定されています(月)";
            }
            return "";
        }

        /// <summary>
        /// 月日の暦日チェック。
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public string MD(int key)
        {
            int Tsuki = 0;          //入力値（月部分)
            int Hi = 0;             //入力値（日部分） 

            if (_dspTextBoxes[key].Text.Trim() == "")
            {
                return "値が設定されていません";
            }

            Tsuki = Convert.ToInt32(_dspTextBoxes[key].Text.Trim()) / 100;
            Hi = Convert.ToInt32(_dspTextBoxes[key].Text.Trim()) % 100;

            if (!((Tsuki >= 1) & (Tsuki <= 12)))
            {
                return "月日に暦日でない値が設定されています";
            }

            if (!((Hi >= 1) & (Hi <= 31)))
            {
                return "月日に暦日でない値が設定されています";
            }

            if ((Tsuki == 4) || (Tsuki == 6) || (Tsuki == 9) || (Tsuki == 11))
            {
                if (Hi >= 31)
                {
                    return "月日に暦日でない値が設定されています";
                }
            }
            if (Tsuki == 2)
            {
                if (Hi >= 30)
                {
                    return "月日に暦日でない値が設定されています";
                }
            }
            return "";
        }

        /// <summary>
        /// 年月日（和暦）の暦日。又は、年月の当月迄チェック。
        /// </summary>00
        /// <returns>エラーメッセージ</returns>
        public string YMACADE2(string rtnName, int itemid)
        {
            int Nentsukihi = 0;     //入力値
            string res = "";

            if (_dspTextBoxes[itemid].Text.Trim() == "")
            {
                return "値が設定されていません";
            }

            Nentsukihi = Convert.ToInt32(_dspTextBoxes[itemid].Text.Trim());

            if (Nentsukihi < 9999)
            {
                //4桁入力の場合、当月までかチェックを行う
                res = YMACADE(itemid);
            }
            else
            {
                //6桁入力の場合、暦日かチェックを行う
                res = YMD(rtnName, itemid);
            }
            return res;
        }

        /// <summary>
        /// 年度項目の比較。(当年度）
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public string WANENACADE(int key)
        {
            return "";
        }

        /// <summary>
        /// 入力された値がカナ判定文字かどうか。
        /// 一致しなければ、エラーメッセージを表示する。
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public string KANACHK(int key)
        {
            // カナ変換OKの場合(正常)
            return "";
        }

        /// <summary>
        /// 年月日（和暦）の暦日チェック（ＮＴＴ電話料、九電）
        /// 前営業日、前々営業日かのチェック
        /// </summary>
        /// <param name="key">取得データ位置</param>
        /// <returns>エラーメッセージ</returns>
        public string YMDCOMP(int key)
        {
            return "";
        }

        /// <summary>
        /// 数値チェック
        /// 入力文字が数値のみであれば、true
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string NUMCHK(int key)
        {
            // 判定文字取得
            string Moji = _dspTextBoxes[key].Text.Trim();

            if (Moji.Trim() == "")
            {
                //未入力の場合は、スルー
                return "";
            }

            //文字列が数値かどうかチェックする
            long l_data;
            if (!long.TryParse(Moji, out l_data))
            {
                return "数値以外の文字は入力できません。";
            }

            return "";
        }

        /// <summary>
        /// 項目間（数値）の比較。(入力項目Ａと入力項目Ｂのパターン）
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public string NUMCMP(int key)
        {
            string cd = "";
            int idx = 0;

            // "NUMCMP △,±XX"から"NUMCMP"及びスペースを削除する。
            cd = _itemsub.Replace("NUMCMP ", "").Trim();

            // カンマ区切りのデータを変数に格納する。
            string[] str = cd.Split(',');

            if (str.Length != 2)
            {
                //MSG内容変更
                return "入力チェック仕様に誤りがあります。";
            }

            //カンマ区切りの１つ目をパターン
            //２つ目を比較先項目
            string pattern = str[0];
            idx = Convert.ToInt32(str[1]);

            if (_dspTextBoxes[key].Text.Trim() == "")
            {
                return "値が設定されていません";
            }
            if (_dspTextBoxes[key + idx].Text.Trim() == "")
            {
                return "値が設定されていません";
            }

            long NumCMP01 = Convert.ToInt64(_dspTextBoxes[key].Text.Replace(",", "").Trim());         //比較元(自分自身)
            long NumCMP02 = Convert.ToInt64(_dspTextBoxes[key + idx].Text.Replace(",", "").Trim());   //比較先

            if (pattern.Equals("1"))
            {
                //比較元>自分自身
                if (NumCMP02 > NumCMP01)
                {
                    return "";
                }
            }
            else if (pattern.Equals("2"))
            {
                //比較元≦自分自身
                if (NumCMP02 <= NumCMP01)
                {
                    return "";
                }
            }
            else if (pattern.Equals("3"))
            {
                //比較元<自分自身
                if (NumCMP02 < NumCMP01)
                {
                    return "";
                }
            }
            else if (pattern.Equals("4"))
            {
                //比較元≧自分自身
                if (NumCMP02 >= NumCMP01)
                {
                    return "";
                }
            }

            //ここに処理が流れる事はエラー！！
            return "入力した値（数値）に誤りがあります。";
        }

        /// <summary>
        /// 全銀文字チェック
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string ZENGIN(string str)
        {
            if (str == null) { return "未入力です。"; }
            if (str.Length == 0) { return "未入力です。"; }

            string msg = "";

            for (int i = 0; i < str.Length; i++)   //文字数分ループ
            {
                if (str[i] == 'ﾟ' || str[i] == 'ﾞ')   //濁点・半濁点の場合
                {
                    if (i == 0)  //先頭の場合はエラー
                    {
                        msg = "全銀文字チェックエラー。";
                        break;
                    }
                    if (!isKanaRightChar(str[i], str[i - 1]))  //濁点・半濁点の整合性チェック
                    {
                        msg = "全銀文字チェックエラー。";
                        break;
                    }
                }
                else if (str[i] == ' ')  //スペースの場合
                {
                    if (i == 0 || i == (str.Length - 1))  //先頭と最後であればエラー
                    {
                        msg = "全銀文字チェックエラー。";
                        break;
                    }
                }
                else
                {
                    if (!isZenginChar(str[i]))
                    {
                        msg = "全銀文字チェックエラー。";
                        break;
                    }
                }
            }

            return msg;
        }

        /// <summary>
        /// 濁点・半濁点の整合性チェック
        /// </summary>
        /// <param name="e"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        protected bool isKanaRightChar(char e, char target)
        {
            bool bRtn = false;

            if (e == 'ﾞ')
            {
                switch (target)
                {
                    case 'ｳ':
                    case 'ｶ':
                    case 'ｷ':
                    case 'ｸ':
                    case 'ｹ':
                    case 'ｺ':
                    case 'ｻ':
                    case 'ｼ':
                    case 'ｽ':
                    case 'ｾ':
                    case 'ｿ':
                    case 'ﾀ':
                    case 'ﾁ':
                    case 'ﾂ':
                    case 'ﾃ':
                    case 'ﾄ':
                    case 'ﾊ':
                    case 'ﾋ':
                    case 'ﾌ':
                    case 'ﾍ':
                    case 'ﾎ':
                        bRtn = true;
                        break;
                    default:
                        break;
                }
            }
            else if (e == 'ﾟ')
            {
                switch (target)
                {
                    case 'ﾊ':
                    case 'ﾋ':
                    case 'ﾌ':
                    case 'ﾍ':
                    case 'ﾎ':
                        bRtn = true;
                        break;
                    default:
                        break;
                }
            }

            return bRtn;
        }

        /// <summary>
        /// 全銀文字チェック
        /// </summary>
        /// <param name="e"></param>
        /// <returns>True/False</returns>
        private bool isZenginChar(char e)
        {
            //数字
            if ((0x30 <= e) && (e <= 0x39))
            {
                return true;
            }

            //アルファベット
            if ((0x41 <= e) && (e <= 0x5a))
            {
                return true;
            }

            //カナ文字 (ｱ-ﾝ  ※ｦ(0xff66)は含まない)
            if ((0xff71 <= e) && (e <= 0xff9d))
            {
                return true;
            }

            //濁点・半濁点
            if ((e == 0xff9e) || (e == 0xff9f))
            {
                return true;
            }

            //記号等 (() - . ｽﾍﾟｰｽ)
            if ((e == 0x28) || (e == 0x29) || (e == 0x2d) || (e == 0x2e) || (e == 0x20))
            {
                return true;
            }

            //最後まできたらNG
            return false;
        }

        /// <summary>
        /// 英(大)数文字チェック
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string ANCK(string str)
        {
            if (str == null) { return "未入力です。"; }
            if (str.Length == 0) { return "未入力です。"; }

            string msg = "";
            char x;

            for (int i = 0; i < str.Length; i++)   //文字数分ループ
            {
                x = str[i];

                //数字
                if ((0x30 <= x) && (x <= 0x39))
                {
                    continue;  //OK
                }

                //アルファベット(大文字)
                if ((0x41 <= x) && (x <= 0x5a))
                {
                    continue;  //OK
                }

                msg = "英数文字チェックエラー。";
                break;
            }

            return msg;
        }

        /// <summary>
        /// 英(大小)数文字チェック
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string AaNCK(string str)
        {
            if (str == null) { return "未入力です。"; }
            if (str.Length == 0) { return "未入力です。"; }

            string msg = "";
            char x;

            for (int i = 0; i < str.Length; i++)   //文字数分ループ
            {
                x = str[i];

                //数字
                if ((0x30 <= x) && (x <= 0x39))
                {
                    continue;  //OK
                }

                //アルファベット(大文字)
                if ((0x41 <= x) && (x <= 0x5a))
                {
                    continue;  //OK
                }

                //アルファベット(小文字)
                if ((0x61 <= x) && (x <= 0x7a))
                {
                    continue;  //OK
                }

                msg = "英数文字チェックエラー。";
                break;
            }

            return msg;
        }

        /// <summary>
        /// サブルーチンの引数を取得する
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private string[] GetSubrtnArgs(string rtnName, int itemid)
        {
            foreach (string subrtn in _dsp_items[itemid].m_ITEM_SUBRTN.Split('|'))
            {
                if (!subrtn.StartsWith(rtnName)) { continue; }

                // 半角空白で区切ってから、カンマで区切る
                string[] rtns = subrtn.Split(' ');
                if (rtns.Length < 2) { return new string[0]; }
                return rtns[1].Split(',');
            }
            return new string[0];
        }

        /// <summary>
        /// 文字チェック
        /// </summary>
        /// <param name="itemid"></param>
        private string ACK(string rtnName, int itemid)
        {
            string chkVal = _dspTextBoxes[itemid].Text;
            if (string.IsNullOrEmpty(chkVal)) { return string.Empty; }

            string[] args = GetSubrtnArgs(rtnName, itemid);

            bool isExist = false;
            for (int i = 0; i < args.Length; i++)
            {
                string chkStrType = args[i];

                // NULLの場合は空文字OK
                if (chkStrType.Equals("NULL") && string.IsNullOrEmpty(chkVal)) { return string.Empty; }

                // 範囲指定文字を含む
                if (chkStrType.IndexOf(":") != -1)
                {
                    string[] spans = chkStrType.Split(':');
                    if (spans.Length < 2) { continue; }
                    string st = spans[0];
                    string ed = spans[1];

                    // 範囲内一致でOK
                    string regex = string.Format("[{0}-{1}]", st, ed);
                    if (Regex.IsMatch(chkVal, regex)) { return string.Empty; }
                }
                else
                {
                    // 完全一致でOK
                    if (chkVal.Equals(chkStrType)) { return string.Empty; }
                }
            }
            if (!isExist)
            {
                return "範囲指定エラー " + _dsp_items[itemid].m_ITEM_SUBRTN.Replace("ACK ", "");
            }

            // 正常終了
            return string.Empty;
        }

        /// <summary>
        /// １画面内合計チェック
        /// 指定の範囲の合計と一致すれば、true
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string SUM(string rtnName, int itemid)
        {
            string retVal = _dspTextBoxes[itemid].Text;

            // 概要：入力元1の合計と入力元2の値を比較する
            // 形式：計算項目1ITEM_ID,計算項目2ITEM_ID,計算項目3ITEM_ID,…
            // 入力元1：
            //   (0) 計算項目1ITEM_ID
            //   (1) 計算項目2ITEM_ID
            //   (n) 計算項目nITEM_ID
            // 入力元2：
            //   入力値

            string[] itemids = GetSubrtnArgs(rtnName, itemid);
            if (itemids.Length < 1) { return string.Empty; }

            // 入力値
            long inputNum = DBConvert.ToLongNull(retVal);

            // 合計算出
            long itemSum = 0;
            for (int i = 0; i < itemids.Length; i++)
            {
                int id = DBConvert.ToIntNull(itemids[i]);
                long num = DBConvert.ToLongNull(_dspTextBoxes[id].Text);
                itemSum += num;
            }

            // 比較結果
            if (inputNum != itemSum)
            {
                return inputNum + " ≠ " + itemSum + " 合計金額が一致しません";
            }

            // 正常終了
            return string.Empty;
        }

        /// <summary>
        /// 本人確認マスタ取得
        /// </summary>
        /// <param name="itemid"></param>
        private string CIF(string rtnName, int itemid)
        {
            string retVal = _dspTextBoxes[itemid].Text;

            //// 形式：受付日ITEM_ID,カナ氏名ITEM_ID,生年月日ITEM_ID,住所ITEM_ID,漢字氏名ITEM_ID
            //// 入力元：
            ////   (0) 受付日ITEM_ID：HONNIN_KAKUNIN.KAKUNINBI
            ////       入力値(CIF番号)：HONNIN_KAKUNIN.CIFNO
            //// 出力先：
            ////   (1) カナ氏名ITEM_ID：HONNIN_KAKUNIN.KANA_SHIMEI
            ////   (2) 生年月日ITEM_ID：HONNIN_KAKUNIN.BIRTHDAY
            ////   (3) 住所ITEM_ID：HONNIN_KAKUNIN.KANJI_JUSYO
            ////   (4) 漢字氏名ITEM_ID：HONNIN_KAKUNIN.KANJI_SHIMEI
            //const int KAKUNINBI = 0;
            //const int KANA_SHIMEI = 1;
            //const int BIRTHDAY = 2;
            //const int KANJI_JUSYO = 3;
            //const int KANJI_SHIMEI = 4;

            //// ITEM_ID（5つ指定されていないとダメ）
            //string[] itemids = GetSubrtnArgs(rtnName, itemid);
            //if (itemids.Length < 5) { return retVal; }

            //// 画面入力値
            //string kakuninbi = _dspTextBox[DBConvert.ToIntNull(itemids[KAKUNINBI])].Text.Trim();
            //string cifno = _dspTextBox[itemid].Text.Trim();

            //// SELECT実行
            //TBL_HONNIN_KAKUNIN hData = new TBL_HONNIN_KAKUNIN();
            //using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            //{
            //    string strSQL = TBL_HONNIN_KAKUNIN.GetSelectQuery(cifno, kakuninbi);
            //    try
            //    {
            //        // 実行
            //        DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            //        if (tbl.Rows.Count > 0) { hData = new TBL_HONNIN_KAKUNIN(tbl.Rows[0]); }
            //    }
            //    catch (Exception ex)
            //    {
            //        ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
            //        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            //        return retVal;
            //    }
            //}

            //// カナ氏名
            //int idKana = DBConvert.ToIntNull(itemids[KANA_SHIMEI]);
            //if (_dspTextBoxes.ContainsKey(idKana))
            //{
            //    _dspTextBoxes[idKana].Text = DBConvert.ToStringNull(hData.m_KANA_SHIMEI);
            //}

            //// 生年月日
            //int idBirth = DBConvert.ToIntNull(itemids[BIRTHDAY]);
            //if (_dspTextBoxes.ContainsKey(idBirth))
            //{
            //    _dspTextBoxes[idBirth].Text = DBConvert.ToStringNull(hData.m_BIRTHDAY);
            //}

            //// 住所
            //int idAddr = DBConvert.ToIntNull(itemids[KANJI_JUSYO]);
            //if (_dspTextBoxes.ContainsKey(idAddr))
            //{
            //    _dspTextBoxes[idAddr].Text = DBConvert.ToStringNull(hData.m_KANJI_JUSYO);
            //}

            //// 漢字氏名
            //int idKanji = DBConvert.ToIntNull(itemids[KANJI_SHIMEI]);
            //if (_dspTextBoxes.ContainsKey(idKanji))
            //{
            //    _dspTextBoxes[idKanji].Text = DBConvert.ToStringNull(hData.m_KANJI_SHIMEI);
            //}

            // 正常終了
            return retVal;
        }

        /// <summary>
        /// 書類種別マスタ取得
        /// </summary>
        /// <param name="itemid"></param>
        private string SYO(string rtnName, int itemid)
        {
            //// 形式：SYO 書類名ITEM_ID
            //// 出力先：
            ////   書類名ITEM_ID：TBL_SYORUI_MST.m_SYORUI_NAME
            //string retVal = _dspTextBoxes[itemid].Text;

            //// ITEM_ID（1つ指定されていないとダメ）
            //string[] itemids = GetSubrtnArgs(rtnName, itemid);
            //if (itemids.Length < 1) { return retVal; }

            //// 画面入力値
            //string dspValue = _dspTextBoxes[itemid].Text.Trim();
            //dspValue = DBConvert.ToIntNull(dspValue).ToString();

            //// SELECT実行
            //TBL_SYORUI_MST sMst = new TBL_SYORUI_MST();
            //using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            //{
            //    string strSQL = TBL_SYORUI_MST.GetSelectQuery(dspValue);
            //    try
            //    {
            //        // 実行
            //        DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            //        if (tbl.Rows.Count > 0) { sMst = new TBL_SYORUI_MST(tbl.Rows[0]); }
            //    }
            //    catch (Exception ex)
            //    {
            //        ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
            //        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            //        return retVal;
            //    }
            //}

            //// 書類名
            //int idSyoruiName = DBConvert.ToIntNull(itemids[0]);
            //if (_dspTextBoxes.ContainsKey(idSyoruiName))
            //{
            //    _dspTextBoxes[idSyoruiName].Text = DBConvert.ToStringNull(sMst.m_SYORUI_NAME);
            //}
            //if (DBManager.ToBoolNull(sMst.m_DEL_FLG))
            //{
            //    _dspTextBoxes[idSyoruiName].Text = "";
            //}

            //// 正常終了
            //return retVal;
            return "";
        }

        /// <summary>
        /// 支店マスタ取得
        /// </summary>
        /// <param name="itemid"></param>
        private string STN(string rtnName, int itemid)
        {
            // 形式：STN 支店名ITEM_ID
            // 出力先：
            //   支店名ITEM_ID：TBL_BRANCH.m_BR_NAME_KANJI
            string retVal = _dspTextBoxes[itemid].Text;

            // ITEM_ID（1つ指定されていないとダメ）
            string[] itemids = GetSubrtnArgs(rtnName, itemid);
            if (itemids.Length < 1) { return retVal; }

            // 画面入力値
            int dspValue = DBConvert.ToIntNull(_dspTextBoxes[itemid].Text.Trim());

            // SELECT実行
            TBL_BRANCHMF sMst = new TBL_BRANCHMF(AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                string strSQL = TBL_BRANCHMF.GetSelectQuery(dspValue, AppInfo.Setting.SchemaBankCD);
                try
                {
                    // 実行
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0) { sMst = new TBL_BRANCHMF(tbl.Rows[0], AppInfo.Setting.SchemaBankCD); }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return retVal;
                }
            }

            // 支店名
            int idShitenName = DBConvert.ToIntNull(itemids[0]);
            if (_dspTextBoxes.ContainsKey(idShitenName))
            {
                _dspTextBoxes[idShitenName].Text = DBConvert.ToStringNull(sMst.m_BR_NAME_KANJI);
            }

            // 正常終了
            return retVal;
        }

        /// <summary>
        /// 支店マスタ取得
        /// </summary>
        /// <param name="itemid"></param>
        private string STN2(string rtnName, int itemid)
        {
            // 形式：STN 支店名ITEM_ID
            // 出力先：
            //   支店名ITEM_ID：TBL_BRANCHMF.m_BR_NAME_KANJI
            string retVal = _dspTextBoxes[itemid].Text;

            // ITEM_ID（1つ指定されていないとダメ）
            string[] itemids = GetSubrtnArgs(rtnName, itemid);
            if (itemids.Length < 1) { return retVal; }

            // 画面入力値
            int dspValue = DBConvert.ToIntNull(_dspTextBoxes[itemid].Text.Trim());

            // SELECT実行
            TBL_BRANCHMF sMst = new TBL_BRANCHMF(AppInfo.Setting.SchemaBankCD);
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    string strSQL = "";
                    strSQL += " SELECT * FROM " + TBL_BRANCHMF.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " ";
                    strSQL += " WHERE ";
                    strSQL += "     " + TBL_BRANCHMF.BR_NO + " = " + dspValue + " ";

                    // 実行
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count > 0) { sMst = new TBL_BRANCHMF(tbl.Rows[0], AppInfo.Setting.SchemaBankCD); }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return retVal;
                }
            }

            // 支店名
            int idShitenName = DBConvert.ToIntNull(itemids[0]);
            if (dspValue == 0)
            {
                _dspTextBoxes[idShitenName].Text = "";
            }
            else if (_dspTextBoxes.ContainsKey(idShitenName))
            {
                _dspTextBoxes[idShitenName].Text = DBConvert.ToStringNull(sMst.m_BR_NAME_KANJI);
            }

            // 正常終了
            return retVal;
        }

        /// <summary>
        /// 支店マスタチェック
        /// </summary>
        /// <param name="itemid"></param>
        private string STNCK(int itemid)
        {
            string retVal = "";

            // 画面入力値
            int dspValue = DBConvert.ToIntNull(_dspTextBoxes[itemid].Text.Trim());

            // 0はOKとする
            if (dspValue == 0) { return retVal; }

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    string strSQL = "";
                    strSQL += " SELECT * FROM " + TBL_BRANCHMF.TABLE_NAME(AppInfo.Setting.SchemaBankCD) + " ";
                    strSQL += " WHERE ";
                    strSQL += "     " + TBL_BRANCHMF.BR_NO + " = " + dspValue + " ";

                    // 実行
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count == 0)
                    {
                        retVal = "マスタに登録されていません。";
                    }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return retVal;
                }
            }
            return retVal;
        }

        /// <summary>
        /// 委託者マスタ取得
        /// </summary>
        /// <param name="itemid"></param>
        private string ITK(string rtnName, int itemid)
        {
            //// 形式：ITK 委託者名ITEM_ID
            //// 出力先：
            ////   委託者名ITEM_ID：ITAKUSYA.ITAKU_NAME_KANJI
            //string retVal = _dspTextBoxes[itemid].Text;

            //// ITEM_ID（1つ指定されていないとダメ）
            //string[] itemids = GetSubrtnArgs(rtnName, itemid);
            //if (itemids.Length < 1) { return retVal; }

            //// 画面入力値
            //int dspValue = DBConvert.ToIntNull(_dspTextBoxes[itemid].Text.Trim());

            //// SELECT実行
            //TBL_ITAKUSYA iMst = new TBL_ITAKUSYA();
            //using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            //{
            //    string strSQL = TBL_ITAKUSYA.GetSelectQuery(dspValue);
            //    try
            //    {
            //        // 実行
            //        DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            //        if (tbl.Rows.Count > 0) { iMst = new TBL_ITAKUSYA(tbl.Rows[0]); }
            //    }
            //    catch (Exception ex)
            //    {
            //        ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
            //        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            //        return retVal;
            //    }
            //}

            //// 委託者名
            //int idShitenName = DBConvert.ToIntNull(itemids[0]);
            //if (_dspTextBoxes.ContainsKey(idShitenName))
            //{
            //    _dspTextBoxes[idShitenName].Text = DBConvert.ToStringNull(iMst.m_ITAKU_NAME_KANJI);
            //}

            //// 正常終了
            //return retVal;
            return "";
        }

        /// <summary>
        /// 委託者マスタチェック
        /// </summary>
        /// <param name="itemid"></param>
        private string ITKCK(int itemid)
        {
            //string retVal = "";

            //// 画面入力値
            //int dspValue = DBConvert.ToIntNull(_dspTextBoxes[itemid].Text.Trim());

            //// SELECT実行
            //using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            //{
            //    try
            //    {
            //        string strSQL = TBL_ITAKUSYA.GetSelectQuery(dspValue);

            //        // 実行
            //        DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            //        if (tbl.Rows.Count == 0)
            //        {
            //            retVal = "マスタに登録されていません。";
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
            //        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            //        return retVal;
            //    }
            //}
            //return retVal;
            return "";
        }

        /// <summary>
        /// 科目マスタ取得
        /// </summary>
        /// <param name="itemid"></param>
        private string KMK(string rtnName, int itemid)
        {
            //// 形式：KMK 科目名ITEM_ID
            //// 出力先：
            ////   科目名ITEM_ID：KAMOKU_MST.KAMOKU_NAME
            //string retVal = _dspTextBoxes[itemid].Text;

            //// ITEM_ID（1つ指定されていないとダメ）
            //string[] itemids = GetSubrtnArgs(rtnName, itemid);
            //if (itemids.Length < 1) { return retVal; }

            //// 画面入力値
            //int dspValue = DBConvert.ToIntNull(_dspTextBoxes[itemid].Text.Trim());

            //// SELECT実行
            //TBL_KAMOKU_MST iMst = new TBL_KAMOKU_MST();
            //using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            //{
            //    string strSQL = TBL_KAMOKU_MST.GetSelectQuery(dspValue.ToString());
            //    try
            //    {
            //        // 実行
            //        DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
            //        if (tbl.Rows.Count > 0) { iMst = new TBL_KAMOKU_MST(tbl.Rows[0]); }
            //    }
            //    catch (Exception ex)
            //    {
            //        ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
            //        LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
            //        return retVal;
            //    }
            //}

            //// 科目名
            //int idKamokuName = DBConvert.ToIntNull(itemids[0]);
            //if (_dspTextBoxes.ContainsKey(idKamokuName))
            //{
            //    _dspTextBoxes[idKamokuName].Text = DBConvert.ToStringNull(iMst.m_KAMOKU_NAME);
            //}

            //// 正常終了
            //return retVal;
            return "";
        }

        /// <summary>
        /// 西暦暦日チェック
        /// </summary>
        /// <param name="itemid"></param>
        private string ADC(int itemid)
        {
            string chkVal = _dspTextBoxes[itemid].Text.Trim();
            if (string.IsNullOrEmpty(chkVal)) { return string.Empty; }

            // 西暦カレンダー取得
            EntryCommon.Calendar.ConvCalInfo sCal = GetSeirekiCalendar(chkVal);
            if (!string.IsNullOrEmpty(sCal.ErrMsg))
            {
                return sCal.ErrMsg;
            }

            // 正常終了
            return string.Empty;
        }

        /// <summary>
        /// 西暦営業日チェック
        /// </summary>
        /// <param name="itemid"></param>
        private string ADD(int itemid)
        {
            string chkVal = _dspTextBoxes[itemid].Text.Trim();
            if (string.IsNullOrEmpty(chkVal)) { return string.Empty; }

            // 西暦カレンダー取得
            EntryCommon.Calendar.ConvCalInfo sCal = GetSeirekiCalendar(chkVal);
            if (!string.IsNullOrEmpty(sCal.ErrMsg))
            {
                return sCal.ErrMsg;
            }

            // 営業日チェック
            if (!EntryCommon.Calendar.IsBusinessDate(sCal.nYYYYMMDD))
            {
                return "営業日ではありません。";
            }

            // 正常終了
            return string.Empty;
        }

        /// <summary>
        /// 和暦暦日チェック
        /// 9YYMMDD
        /// </summary>
        /// <param name="itemid"></param>
        private string YMD(string rtnName, int itemid)
        {
            // 形式：YMD [元号タイプ]
            // 元号タイプ：
            //   1：ERA.SEQ
            //   2：ERA.SHORTKANJI
            //   3：ERA.SHORTROMAN
            // 入力形式
            // 元号タイプ
            //   1：9YYMMDD
            //   2：ＮYYMMDD
            //   3：XYYMMDD
            string chkVal = _dspTextBoxes[itemid].Text.Trim();
            if (string.IsNullOrEmpty(chkVal)) { return string.Empty; }

            // 元号タイプ
            string[] args = GetSubrtnArgs(rtnName, itemid);
            if (args.Length < 1) { return chkVal; }
            int wType = DBConvert.ToIntNull(args[0]);

            // 和暦カレンダー取得
            EntryCommon.Calendar.ConvCalInfo wCal = GetWarekiCalendar(wType, chkVal);
            if (!string.IsNullOrEmpty(wCal.ErrMsg))
            {
                return wCal.ErrMsg;
            }

            // 正常終了
            return string.Empty;
        }

        /// <summary>
        /// 和暦営業日チェック
        /// </summary>
        /// <param name="itemid"></param>
        private string EMD(string rtnName, int itemid)
        {
            // 形式：EMD [元号タイプ]
            // 元号タイプ：
            //   1：ERA.SEQ
            //   2：ERA.SHORTKANJI
            //   3：ERA.SHORTROMAN
            // 入力形式
            // 元号タイプ
            //   1：9YYMMDD
            //   2：ＮYYMMDD
            //   3：XYYMMDD
            string chkVal = _dspTextBoxes[itemid].Text.Trim();
            if (string.IsNullOrEmpty(chkVal)) { return string.Empty; }

            // 元号タイプ
            string[] args = GetSubrtnArgs(rtnName, itemid);
            if (args.Length < 1) { return chkVal; }
            int wType = DBConvert.ToIntNull(args[0]);

            // 和暦カレンダー取得
            EntryCommon.Calendar.ConvCalInfo wCal = GetWarekiCalendar(wType, chkVal);
            if (!string.IsNullOrEmpty(wCal.ErrMsg))
            {
                return wCal.ErrMsg;
            }

            // 営業日チェック
            if (!EntryCommon.Calendar.IsBusinessDate(wCal.nYYYYMMDD))
            {
                return "営業日ではありません。";
            }

            // 正常終了
            return string.Empty;
        }

        /// <summary>
        /// 和暦カレンダーを取得する
        /// </summary>
        /// <param name="wType"></param>
        /// <param name="chkVal"></param>
        /// <returns></returns>
        private EntryCommon.Calendar.ConvCalInfo GetWarekiCalendar(int wType, string chkVal)
        {
            // カレンダー初期化
            EntryCommon.Calendar.ConvCalInfo wCal =
                new EntryCommon.Calendar.ConvCalInfo(wType, chkVal);
            if (!string.IsNullOrEmpty(wCal.ErrMsg))
            {
                return wCal;
            }

            // 和暦変換テーブル取得
            EntryCommon.Calendar.FetchEra(wCal);
            if (!string.IsNullOrEmpty(wCal.ErrMsg))
            {
                return wCal;
            }

            // 和暦から西暦に変換できるかチェック
            EntryCommon.Calendar.ConvToSeireki(wCal);
            if (!string.IsNullOrEmpty(wCal.ErrMsg))
            {
                return wCal;
            }
            return wCal;
        }

        /// <summary>
        /// 西暦カレンダーを取得する
        /// </summary>
        /// <param name="chkVal"></param>
        /// <returns></returns>
        private EntryCommon.Calendar.ConvCalInfo GetSeirekiCalendar(string chkVal)
        {
            // カレンダー初期化
            EntryCommon.Calendar.ConvCalInfo wCal =
                new EntryCommon.Calendar.ConvCalInfo(chkVal);
            if (!string.IsNullOrEmpty(wCal.ErrMsg))
            {
                return wCal;
            }
            return wCal;
        }

        /// <summary>
        /// 環境依存文字チェック
        /// </summary>
        /// <param name="itemid"></param>
        private string EMB(int itemid)
        {
            string chkVal = _dspTextBoxes[itemid].Text.Trim();
            if (string.IsNullOrEmpty(chkVal)) { return string.Empty; }

            string errVal = IsSafeChar(chkVal);
            if (!string.IsNullOrEmpty(errVal))
            {
                return "登録できない文字が入力されています。[" + errVal + "]";
            }

            // 正常終了
            return string.Empty;
        }

        /// <summary>
        /// 文字列チェック
        /// </summary>
        /// <param name="chkStr"></param>
        /// <returns></returns>
        public string IsSafeChar(string chkStr)
        {
            for (int i = 0; i < chkStr.Length; i++)
            {
                if (GetBadChar(chkStr[i]) != 0) { return chkStr[i].ToString(); }
            }
            // OK
            return string.Empty;
        }

        /// <summary>
        /// 文字コードチェック
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private int GetBadChar(char c)
        {
            // 半角カナ OK
            // JIS第1水準漢字 OK
            // JIS第2水準漢字 OK
            // 環境依存文字 NG
            // JIS第3水準漢字 NG
            // JIS第4水準漢字 NG
            try
            {
                byte[] bytes = _enc.GetBytes(c.ToString());

                if (bytes.Length == 1) return 0;  // 半角はOK

                if (BitConverter.IsLittleEndian) Array.Reverse(bytes);  // 上位バイトと下位バイトを入れ替える
                int code = BitConverter.ToUInt16(bytes, 0);

                // 文字コードチェック（チェック範囲は下位１バイトが 0x40～0x7e、0x80～0xfc にあるもの）
                if ((code >= 0x81ad && code <= 0x81b7) || (code >= 0x81c0 && code <= 0x81c7) || (code >= 0x81cf && code <= 0x81d9) ||
                    (code >= 0x81e9 && code <= 0x81ef) || (code >= 0x81f8 && code <= 0x81fb) || (code >= 0x81fd && code <= 0x824e) ||
                    (code >= 0x8259 && code <= 0x825f) || (code >= 0x827a && code <= 0x8280) || (code >= 0x829b && code <= 0x829e))
                {
                    return code;  // 記号、桁、ラテン
                }
                if ((code >= 0x82f2 && code <= 0x82ff) || (code >= 0x8397 && code <= 0x839e))
                {
                    return code;  // ひらがな、カタカナ
                }
                if ((code >= 0x83b7 && code <= 0x83be) || (code >= 0x83d7 && code <= 0x83df) || (code >= 0x8461 && code <= 0x846f) ||
                    (code >= 0x8492 && code <= 0x849e) || (code >= 0x84bf && code <= 0x889e))
                {
                    return code;  // ギリシャ文字、キリル文字、特殊文字
                }
                if ((code >= 0x9873 && code <= 0x989e) || (code >= 0xeaa5))
                {
                    return code;  // 漢字
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 指定した ITEM_ID の値を取得する
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private string GetDspValue(int itemid)
        {
            if (_valtype == CheckValue.TEXT_BOX)
            {
                if (_dspTextBoxes == null) { return ""; }
                if (!_dspTextBoxes.ContainsKey(itemid)) { return ""; }
                return _dspTextBoxes[itemid].Text;
            }
            else if (_valtype == CheckValue.ENT_DATA)
            {
                if (!_tritems.ContainsKey(itemid)) { return ""; }
                return _tritems[itemid].m_ENT_DATA;
            }
            else if (_valtype == CheckValue.VFY_DATA)
            {
                if (!_tritems.ContainsKey(itemid)) { return ""; }
                return _tritems[itemid].m_VFY_DATA;
            }
            else if (_valtype == CheckValue.OCR_ENT_DATA)
            {
                if (!_tritems.ContainsKey(itemid)) { return ""; }
                return _tritems[itemid].m_OCR_ENT_DATA;
            }
            else if (_valtype == CheckValue.OCR_VFY_DATA)
            {
                if (!_tritems.ContainsKey(itemid)) { return ""; }
                return _tritems[itemid].m_OCR_VFY_DATA;
            }
            return string.Empty;
        }

        /// <summary>
        /// 銀行チェック
        /// </summary>
        /// <param name="itemid"></param>
        private string BANK(int itemid)
        {
            string errMsg = "銀行マスタに存在しません";

            // 画面入力値
            string dspValue = GetDspValue(itemid);

            // 数字チェック
            if (!IsNumber(dspValue)) { return errMsg; }
            int nDspValue = DBConvert.ToIntNull(dspValue);

            //// 銀行マスタ取得
            //_itemMgr.Fetch_mst_banks();

            // 銀行チェック
            if (!_itemMgr.mst_banks.ContainsKey(nDspValue))
            {
                return errMsg;
            }

            // 加盟フラグ
            if (!DBConvert.ToBoolNull(_itemMgr.mst_banks[nDspValue].m_KAMEI_FLG))
            {
                return "電子交換所未加盟です";
            }
            return string.Empty;
        }

        /// <summary>
        /// 銀行チェック(読替項目)
        /// </summary>
        /// <param name="itemid"></param>
        private string BANK2(int itemid)
        {
            string errMsg = "銀行マスタに存在しません";

            // 画面入力値
            string dspValue = GetDspValue(itemid);

            // 入力項目の数字チェック
            if (!IsNumber(dspValue)) { return errMsg; }

            // "BANK2 X"から"BANK2"及びスペースを削除する。
            string id = _itemsub.Replace("BANK2 ", "").Trim();
            if (!int.TryParse(id, out int itemSubID))
            {
                return "入力チェック仕様に誤りがあります。";
            }

            // パラメータ設定されている項目に対して銀行チェックを実施
            return BANK(itemSubID);
        }

        /// <summary>
        /// 期日チェック
        /// </summary>
        /// <param name="itemid"></param>
        private string KIJITSU(int itemid)
        {
            string errMsg = "入力日付が正しくありません";

            // 画面入力値
            string dspValue = GetDspValue(itemid);
            dspValue = dspValue.Replace("/", "");
            dspValue = dspValue.Replace("-", "");
            dspValue = dspValue.Replace(".", "");

            // 数字チェック
            if (!IsNumber(dspValue)) { return errMsg; }
            int nDspValue = DBConvert.ToIntNull(dspValue);

            // 歴日チェック
            if (!IsDate(dspValue)) { return errMsg; }

            // 営業日換算して後続チェックを行う
            // (インスタンスで休日マスタは取得しているためここでは取得なし)
            int bizDate = Calendar.GetSettleDay(nDspValue);
            if (bizDate <= 0) { return errMsg; }

            // 処理日チェック
            if (AplInfo.OpDate() > bizDate) { return errMsg; }
            return string.Empty;
        }

        /// <summary>
        /// 金額チェック
        /// </summary>
        /// <param name="itemid"></param>
        private string AMOUNT(int itemid)
        {
            string errMsg = "入力値が正しくありません";

            // 画面入力値
            string dspValue = GetDspValue(itemid);
            dspValue = dspValue.Replace(",", "");

            // 数字チェック
            if (!IsNumber(dspValue)) { return errMsg; }
            long nDspValue = DBConvert.ToLongNull(dspValue);

            // 0チェック
            if (nDspValue < 1) { return errMsg; }

            return string.Empty;
        }

        /// <summary>
        /// 種類チェック
        /// </summary>
        /// <param name="itemid"></param>
        private string SYURUI(int itemid, string itemsub)
        {
            string errMsg = "種類マスタに存在しません";

            // 画面入力値
            string dspValue = GetDspValue(itemid);
            // DSPID
            int ndspID = _trmei.m_DSP_ID;

            // 数字チェック
            if (!IsNumber(dspValue)) { return errMsg; }
            int nDspValue = DBConvert.ToIntNull(dspValue);

            //// 種類マスタ取得
            //_itemMgr.Fetch_mst_syuruimfs();

            // 種類存在チェック
            if (!_itemMgr.mst_syuruimfs.ContainsKey(nDspValue))
            {
                return errMsg;
            }

            // DSP_ID毎の種類入力チェック
            errMsg = "入力不可の手形種類です";
            string Key = CommonUtil.GenerateKey("|", nDspValue, ndspID);
            string KeyAll = CommonUtil.GenerateKey("|", nDspValue, string.Empty);
            if (!(_itemMgr.mst_chksyuruimfs.ContainsKey(Key) || _itemMgr.mst_chksyuruimfs.ContainsKey(KeyAll)))
            {
                return errMsg;
            }

            string[] param = itemsub.Split(' ');
			bool errFlag = false;

			if (param.Length > 1)
			{
				string BillCode = GetDspValue(DBConvert.ToIntNull(param[1]));
				
				switch (BillCode)
				{
					case "101":		// 約束手形
						errFlag = nDspValue != 2;
						break;
					case "102":		// 為替手形(手形種類が3(為手)、7(為手(自店処理分)))
						errFlag = nDspValue != 3 && nDspValue != 7;
						break;
					case "201":     // 小切手(手形種類が1(小切手)、4(自己宛小切手)、7(小切手(自店処理分)))
                        errFlag = nDspValue != 1 && nDspValue != 4 && nDspValue != 7;
						break;
                    // 証券種類コードがその他証券、のとき、手形種類コード1:小切手　2:約束手形　3:為替手形　4:自己宛小切手はNG
					default:
                        switch (nDspValue)
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                                errFlag = true;
                                break;
                            default:
                                errFlag = false;
                                break;
                        }
                        break;
				}

				if (errFlag)
				{
					errMsg = string.Format("証券種類コード:{0}に手形種類コード:{1}は対応していません。"
						+ "証券種類コードまたは手形種類コードを見直してください。"
						, BillCode
						, dspValue);
					return errMsg;
				}
            }

            return string.Empty;
        }

        /// <summary>
        /// 支店チェック
        /// </summary>
        /// <param name="itemid"></param>
        private string BRANCH(int itemid)
        {
            string errMsg = "支店マスタに存在しません";

            // 画面入力値
            string dspValue = GetDspValue(itemid);

            // 数字チェック
            if (!IsNumber(dspValue)) { return errMsg; }
            int nDspValue = DBConvert.ToIntNull(dspValue);

            //// 支店マスタ取得
            //_itemMgr.Fetch_mst_branches();

            // 支店チェック
            if (!_itemMgr.mst_branches.ContainsKey(nDspValue))
            {
                return errMsg;
            }
            return string.Empty;
        }

        /// <summary>
        /// 口座番号チェックデジットチェック
        /// </summary>
        /// <param name="itemid"></param>
        private string ACCD(int itemid, string itemsub)
        {
            string errMsg = "チェックデジットエラー";

            string[] param = itemsub.Split(' ');

            if (param.Length > 1)
            {
                // 自己宛小切手はチェックディジット対象外
                if (DBConvert.ToIntNull(GetDspValue(DBConvert.ToIntNull(param[1]))) == 4)
                    return string.Empty;
            }

            // 画面入力値
            int dspValue = DBConvert.ToIntNull(GetDspValue(itemid));

            // 数字チェック
            if (!IsNumber(dspValue)) { return errMsg; }

            // チェックディジットチェック
            if (!CheckDigitApi.CheckDigit.DoCheck(dspValue))
            {
                return errMsg;
            }
            return string.Empty;
        }

        /// <summary>
        /// 支払人チェック
        /// </summary>
        /// <param name="itemid"></param>
        private string PAYER(string rtnName, int itemid)
        {
            string errMsg = "支払人マスタに存在しません";

            // 形式：PAYER 支店ITEM_ID, 口座番号ITEM_ID
            string retVal = _dspTextBoxes[itemid].Text;

            // ITEM_ID（2つ指定されていないとダメ）
            string[] itemids = GetSubrtnArgs(rtnName, itemid);
            if (itemids.Length < 2) { return retVal; }

            // 画面入力値
            string dspValue = GetDspValue(itemid);

            // 数字チェック
            if (!IsNumber(dspValue)) { return errMsg; }

            // 支店と口座番号が両方入力された場合のみチェックする
            int brnoId = DBConvert.ToIntNull(itemids[0]);
            int accountId = DBConvert.ToIntNull(itemids[1]);
            int brno = DBConvert.ToIntNull(GetDspValue(brnoId));
            int accoutno = DBConvert.ToIntNull(GetDspValue(accountId));
            if ((brno == 0) && (accoutno == 0)) { return string.Empty; }

            // 支店フォーカス時は、口座番号を入力した場合のみチェックする
            if (brnoId == itemid)
            {
                if (accoutno == 0) { return string.Empty; }
            }

            // SELECT実行
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                try
                {
                    string strSQL = TBL_PAYERMF.GetSelectQuery(brno, accoutno, AppInfo.Setting.SchemaBankCD);
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    if (tbl.Rows.Count < 1) { return errMsg; }
                }
                catch (Exception ex)
                {
                    ComMessageMgr.MessageError(ComMessageMgr.E00004, ex.Message);
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                }
            }
            return string.Empty;
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
    }
}
