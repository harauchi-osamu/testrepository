using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using CommonTable.DB;

namespace CommonClass
{
    public class CommonFunc
    {

        #region メンバー

        // Graphicsを扱うためにとりあえず何か必要なのでlabelを用意
        private static System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();

        #endregion

        #region 列挙型

        public enum TextType
        {
            A, K, N, R, S, T, C, J, D, V, W
        }

        #endregion

        /// <summary>
        /// 「バーコード、バーコード、号機、バッチ番号、連番、仕向元、件数」が印字情報として空白区切りで渡されている
        /// そのうち、「号機、バッチ番号、連番、仕向元」を抜き出す
        /// </summary>
        /// <param name="str">印字情報</param>
        /// <returns></returns>
        public static string GetDispNumbering(string str)
        {
            string[] arr = str.Replace("  ", " ").Split(' ');

            if (arr.Length < 3) { return str.Replace("  ", " "); }

            string res = arr[2];
            for (int i = 3; i < arr.Length - 1; i++)
            {
                res += " " + arr[i];
            }

            return res;
        }

        #region 画面コントロール関係

        /// <summary>
        /// 与えられたControlの左上±??%にあるか
        /// </summary>
        /// <param name="cnt">対象のコントロール</param>
        /// <param name="x">x座標（フォーム絶対座標）</param>
        /// <param name="y">y座標（フォーム絶対座標）</param>
        /// <param name="percent">前後左右の許容パーセント(0-100)</param>
        /// <returns></returns>
        public static bool OnLeftTopCorner(Control cnt, int x, int y, int percent)
        {
            // コントロール左上の絶対座標
            Point absCntPos = cnt.PointToScreen(new Point(0, 0));

            if (absCntPos.X * (100 - percent) / 100f < x && absCntPos.X * (100 + percent) / 100f > x &&
                absCntPos.Y * (100 - percent) / 100f < y && absCntPos.Y * (100 + percent) / 100f > y)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 与えられたControlの右下±??%にあるか
        /// </summary>
        /// <param name="cnt">対象のコントロール</param>
        /// <param name="x">x座標（フォーム絶対座標）</param>
        /// <param name="y">y座標（フォーム絶対座標）</param>
        /// <param name="percent">前後左右の許容パーセント(0-100)</param>
        /// <returns></returns>
        public static bool OnRightBottomCorner(Control cnt, int x, int y, int percent)
        {
            // コントロール右下の絶対座標
            Point absCntPos = cnt.PointToScreen(new Point(cnt.Width, cnt.Height));

            if (absCntPos.X * (100 - percent) / 100f < x && absCntPos.X * (100 + percent) / 100f > x &&
                absCntPos.Y * (100 - percent) / 100f < y && absCntPos.Y * (100 + percent) / 100f > y)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region 文字・フォント関係

        /// <summary>
        /// 外字が入っているかどうかのチェック
        /// </summary>
        /// <param name="str">チェック対象文字列</param>
        /// <returns></returns>
        public static bool CheckKanjiCode(string str)
        {
            int code;
            for (int i=0;i<str.Length;i++)
            {
                // ２バイト文字
                if (Encoding.GetEncoding("shift_jis").GetByteCount(str.Substring(i)) == 2)
                {
                    code = str[i];
                    if (0x889F <= code && code <= 0x989E) { continue; }
                    if (0x989F <= code && code <= 0x9FFC) { continue; }
                    if (0x8140 <= code && code <= 0x84BE) { continue; }
                    if (0xE040 <= code && code <= 0xEAFC) { continue; }
                    if (0xED40 <= code && code <= 0xEEFC) { continue; }
                    if (0xFA40 <= code && code <= 0xFA4B) { continue; }
                }
                else { return false; }
            }

            return true;
        }

        /// <summary>
        /// フォントの各文字の最大幅・最大高さを返す
        /// </summary>
        /// <param name="fontsize">フォントサイズ</param>
        /// <param name="texttype">判定する文字種類</param>
        public static SizeF GetMaxCharSize(int fontsize, TextType texttype)
        {
            // VB6のGetTextExtentPoint32(WINAPI)とGraphic.MeasureStringが対応しているはずだけど違う値が返ってくる？
            // VB6と.Netで座標の計算が違うからだと思われる。ほぼ、VB6の15Twips=.NETの1Pixelにあたる。厳密には計算が必要。
            Graphics grfx = lbl.CreateGraphics();
            SizeF sizef;
            float maxWidth = 0, maxHeight = 0;
            Font font = new Font(AplInfo.FontName, (float)fontsize);

            switch (texttype)
            {
                case TextType.A:   // 英数字（スペース含む）
                case TextType.K:   // 英数字とカナ
                    for (int i = 0x30; i < 0x80; i++)
                    {
                        if (!Char.IsLetterOrDigit(Convert.ToChar(i))) { continue; }
                        sizef = grfx.MeasureString(Convert.ToString(Convert.ToChar(i)), font);
                        maxWidth = Math.Max(maxWidth, sizef.Width);
                        maxHeight = Math.Max(maxHeight, sizef.Height);
                    }
                    if (texttype.Equals(DspItem.ItemType.K))
                    {
                        for (int i = 0x30; i < 0x80; i++)
                        {
                            if (!Char.IsLetterOrDigit(Convert.ToChar(i))) { continue; }
                            sizef = grfx.MeasureString(Convert.ToString(Convert.ToChar(i)), font);
                            maxWidth = Math.Max(maxWidth, sizef.Width);
                            maxHeight = Math.Max(maxHeight, sizef.Height);
                        }
                        for (int i = 0xA6; i < 0xE0; i++)
                        {
                            sizef = grfx.MeasureString(Convert.ToString(Convert.ToChar(i)), font);
                            maxWidth = Math.Max(maxWidth, sizef.Width);
                            maxHeight = Math.Max(maxHeight, sizef.Height);
                        }
                        foreach (string str in new string[] { "(", ")", "/", "-", ",", ".", " " })
                        {
                            sizef = grfx.MeasureString(str, font);
                            maxWidth = Math.Max(maxWidth, sizef.Width);
                            maxHeight = Math.Max(maxHeight, sizef.Height);
                        }
                    }
                    break;
                case TextType.N:   // 数字のみ
                case TextType.R:   // 数字と" "
                case TextType.S:   // 数字と"-"
                case TextType.T:   // 数字と"-"と" "
                    for (int i = 0x30; i < 0x39; i++)
                    {
                        if (!Char.IsDigit(Convert.ToChar(i))) { continue; }
                        sizef = grfx.MeasureString(Convert.ToString(Convert.ToChar(i)), font);
                        maxWidth = Math.Max(maxWidth, sizef.Width);
                        maxHeight = Math.Max(maxHeight, sizef.Height);
                    }
                    if (texttype.Equals(TextType.R) || texttype.Equals(TextType.T))
                    {
                        sizef = grfx.MeasureString(" ", font);
                        maxWidth = Math.Max(maxWidth, sizef.Width);
                        maxHeight = Math.Max(maxHeight, sizef.Height);
                    }
                    if (texttype.Equals(TextType.S) || texttype.Equals(TextType.T))
                    {
                        sizef = grfx.MeasureString("-", font);
                        maxWidth = Math.Max(maxWidth, sizef.Width);
                        maxHeight = Math.Max(maxHeight, sizef.Height);
                    }
                    break;
                case TextType.C: // 基準がよく分からない漢字たち
                case TextType.J: // 基準がよく分からない漢字たち
                    for (int i = 0x8140; i < 0xFA4C; i++)
                    {
                        if ((0x8140 <= i && i <= 0x84BE)
                            || (0x889F <= i && i <= 0x9FFC)
                            || (0xE040 <= i && i <= 0xEAFC)
                            || (0xED40 <= i && i <= 0xEEFC)
                            || (0xFA40 <= i && i <= 0xFA4B))
                        {
                            sizef = grfx.MeasureString(Convert.ToString(Convert.ToChar(i)), font);
                            maxWidth = Math.Max(maxWidth, sizef.Width);
                            maxHeight = Math.Max(maxHeight, sizef.Height);
                        }
                    }
                    maxWidth = maxWidth / 2;
                    break;
                default:
                    break;
            }

            return new SizeF(maxWidth, maxHeight);
        }

        /// <summary>
        /// フォントの各文字の最大幅・最大高さを返す
        /// </summary>
        /// <param name="fontsize">フォントサイズ</param>
        /// <param name="texttype">判定する文字種類(A, K, N, R, S, T, C, J, D, V, W)</param>
        public static SizeF GetMaxCharSize(int fontsize, string texttype)
        {
            SizeF ret = new SizeF(0, 0);
            switch (texttype)
            {
                case DspItem.ItemType.A:
                    ret = GetMaxCharSize(fontsize, TextType.A);
                    break;
                case DspItem.ItemType.K:
                    ret = GetMaxCharSize(fontsize, TextType.K);
                    break;
                case DspItem.ItemType.N:
                    ret = GetMaxCharSize(fontsize, TextType.N);
                    break;
                case DspItem.ItemType.R:
                    ret = GetMaxCharSize(fontsize, TextType.R);
                    break;
                case DspItem.ItemType.S:
                    ret = GetMaxCharSize(fontsize, TextType.S);
                    break;
                case DspItem.ItemType.T:
                    ret = GetMaxCharSize(fontsize, TextType.T);
                    break;
                case DspItem.ItemType.C:
                    ret = GetMaxCharSize(fontsize, TextType.C);
                    break;
                case DspItem.ItemType.J:
                    ret = GetMaxCharSize(fontsize, TextType.J);
                    break;
                case DspItem.ItemType.D:
                    ret = GetMaxCharSize(fontsize, TextType.D);
                    break;
                case DspItem.ItemType.V:
                    ret = GetMaxCharSize(fontsize, TextType.V);
                    break;
                case DspItem.ItemType.W:
                    ret = GetMaxCharSize(fontsize, TextType.W);
                    break;
                default:
                    break;
            }

            return ret;
        }

        public static string GetWideString(string value)
        {
            return Strings.StrConv(value, VbStrConv.Wide, 0);
        }

        public static string GetNarrowString(string value)
        {
            return Strings.StrConv(value, VbStrConv.Narrow, 0);
        }

        public static string GetWideUpperString(string value)
        {
            return Strings.StrConv(Strings.StrConv(value, VbStrConv.Wide, 0), VbStrConv.Uppercase, 0);
        }

        public static string GetWideLowerString(string value)
        {
            return Strings.StrConv(Strings.StrConv(value, VbStrConv.Wide, 0), VbStrConv.Lowercase, 0);
        }

        public static string GetNarrowUpperString(string value)
        {
            return Strings.StrConv(Strings.StrConv(value, VbStrConv.Narrow, 0), VbStrConv.Uppercase, 0);
        }

        public static string GetNarrowLowerString(string value)
        {
            return Strings.StrConv(Strings.StrConv(value, VbStrConv.Narrow, 0), VbStrConv.Lowercase, 0);
        }
        #endregion

        #region 日付関連

        /// <summary>
        /// 西暦を和暦に変換
        /// fmtで和暦の書式を指定
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="fmt"></param>
        /// <returns></returns>
        public static string SeirekiToWareki(int year, int month, int day, int fmt)
        {
            //2019.01.21_元号改元対応_個別
            DateTime date = new DateTime(year, month, day);
            string strDate = date.ToString("yyyyMMdd");
            iBicsCalendar ibc = new iBicsCalendar();
            string wareki = ibc.getWareki(Convert.ToInt32(strDate)).ToString().PadLeft(6, '0');
            string gengou = ibc.getGengo(Convert.ToInt32(strDate));

            string wdate = "";

            switch (fmt)
            {
                case 0: // 和暦年月日（年ゼロサプレスなし）
                    wdate = wareki.TrimStart('0');
                    break;
                case 1: // 和暦年月日（年ゼロサプレスあり）
                    wdate = wareki;
                    break;
                case 2: // 和暦年-月-日（年ゼロサプレスなし）
                    wdate = wareki.Substring(0, 2).TrimStart('0') + "-" + wareki.Substring(2, 2) + "-" + wareki.Substring(4, 2);
                    break;
                case 3: // 和暦年-月-日（年ゼロサプレスあり）
                    wdate = wareki.Substring(0, 2) + "-" + wareki.Substring(2, 2) + "-" + wareki.Substring(4, 2);
                    break;
                case 4: // 和暦年-月-日（ゼロサプレスなし）
                    wdate = wareki.Substring(0, 2).TrimStart('0') + "." + wareki.Substring(2, 2).TrimStart('0') + "." + wareki.Substring(4, 2).TrimStart('0');
                    break;
                case 5: // 和暦年-月-日（年ゼロサプレスあり）
                    wdate = wareki.Substring(0, 2) + "." + wareki.Substring(2, 2) + "." + wareki.Substring(4, 2);
                    break;
                case 6: // 元号和暦年"年"月"月"日"日"（年ゼロサプレスなし）
                    wdate = gengou + wareki.Substring(0, 2).TrimStart('0') + "年" + wareki.Substring(2, 2).TrimStart('0') + "月" + wareki.Substring(4, 2).TrimStart('0') + "日";
                    break;
                case 7: // 元号和暦年"年"月"月"日"日"（年ゼロサプレスあり）
                    wdate = gengou + wareki.Substring(0, 2) + "年" + wareki.Substring(2, 2) + "月" + wareki.Substring(4, 2) + "日";
                    break;
                case 8: // 和暦年/月/日（年ゼロサプレスあり）
                    wdate = wareki.Substring(0, 2) + "/" + wareki.Substring(2, 2) + "/" + wareki.Substring(4, 2);
                    break;
                case 9: // 和暦年-月-日（年空白プレスあり）                    
                    string strdate = "";
                    wdate = wareki.Substring(0, 2).TrimStart('0') + "." + wareki.Substring(2, 2).TrimStart('0') + "." + wareki.Substring(4, 2).TrimStart('0');
                    foreach (string str in wdate.Split('.'))
                    {
                        if (str.Length < 2) { strdate = strdate + " " + str + "."; }
                        else { strdate += str + "."; }
                    }
                    wdate = strdate.Substring(0, strdate.Length - 1);
                    break;
                default:
                    break;
            }

            return wdate;
        }

        #endregion

        # region 数値判定

        /// <summary>
        /// 数値判定
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static bool IsNumeric(object objValue)
        {
            string strValue = "";

            try
            {
                strValue = objValue.ToString();
            }
            catch
            {
                return false;
            }

            if (strValue.Length > 0)
            {
                for (int i = 0; i <= strValue.Length - 1; i++)
                {
                    if (Char.IsDigit(strValue, i) == false)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        # endregion

    }
}
