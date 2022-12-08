using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using CommonClass;
using CommonTable.DB;

namespace EntryClass
{
    public class EntryCommonFunc
    {
        // Graphicsを扱うためにとりあえず何か必要なのでlabelを用意
        private static System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();

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
                if (Encoding.GetEncoding("shift_jis").GetByteCount(str.Substring(i,1)) == 2)
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
        /// 全角かどうかチェックする
        /// </summary>
        /// <param name="texttype"></param>
        /// <returns></returns>
        public static bool IsZenkaku(string texttype)
        {
            switch (texttype)
            {
                case DspItem.ItemType.A:
                case DspItem.ItemType.N:
                case DspItem.ItemType.S:
                case DspItem.ItemType.R:
                case DspItem.ItemType.T:
                case DspItem.ItemType.K:
                case DspItem.ItemType.C:
                case DspItem.ItemType.D:
                    // 半角
                    return false;
                case DspItem.ItemType.J:
                case DspItem.ItemType.V:
                case DspItem.ItemType.W:
                    // 全角
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 文字数を取得する
        /// </summary>
        /// <param name="texttype"></param>
        /// <param name="itemlen"></param>
        /// <returns></returns>
        public static int GetCharLength(string texttype, int itemlen)
        {
            int retLen = itemlen;
            if (IsZenkaku(texttype))
            {
                retLen = (int)Math.Floor((float)itemlen / 2.0F);
            }
            return retLen;
        }

        /// <summary>
        /// フォントの各文字の最大幅・最大高さを返す
        /// </summary>
        /// <param name="fontsize">フォントサイズ</param>
        /// <param name="texttype">判定する文字種類</param>
        public static SizeF GetMaxCharSize(int fontsize, CommonFunc.TextType texttype, ref int top)
        {
            Graphics grfx = lbl.CreateGraphics();
            Font font = new Font(AplInfo.FontName, (float)fontsize);

            // 桁数はバイト数なので全角も半角も「半角」とみなして算出する
            SizeF sizef = grfx.MeasureString(Convert.ToString("a"), font);
            int sub = 0;
            int height = 0;

            // 等角フォントの文字サイズによってテキストボックスの幅を調整する
            switch (texttype)
            {
                case CommonFunc.TextType.A:    // 英数字（スペース含む）
                case CommonFunc.TextType.N:    // 数字のみ
                case CommonFunc.TextType.S:    // 数字と"-"
                case CommonFunc.TextType.R:    // 数字と" "
                case CommonFunc.TextType.T:    // 数字と"-"と" "
                case CommonFunc.TextType.K:    // 英数字とカナ
                case CommonFunc.TextType.C:    // 定数
                case CommonFunc.TextType.D:    // ダミー
                    // 半角フォントで算出
                    AdjustSize(fontsize, ref top, ref sub, ref height, true);
                    break;

                case CommonFunc.TextType.J:    // 漢字
                case CommonFunc.TextType.V:    // 読取専用
                case CommonFunc.TextType.W:    // 読取専用(右揃え)
                    // 全角フォントで算出
                    AdjustSize(fontsize, ref top, ref sub, ref height, false);
                    break;

                default:
                    break;
            }
            return new SizeF(sizef.Width - (float)sub, sizef.Height);
        }

        /// <summary>
        /// 幅調整
        /// </summary>
        /// <param name="fontSize"></param>
        /// <param name="sub"></param>
        /// <param name="top"></param>
        /// <param name="height"></param>
        /// <param name="isHalf"></param>
        public static void AdjustSize(int fontSize, ref int top, ref int sub, ref int height, bool isHalf = true)
        {
            top = 0;
            sub = 0;
            height = 0;
            switch (fontSize)
            {
                case 10:
                    top = isHalf ? 7 : 4;
                    sub = 3;
                    height = 19;
                    break;
                case 11:
                    top = isHalf ? 8 : 4;
                    sub = 3;
                    height = 22;
                    break;
                case 12:
                    top = isHalf ? 9 : 4;
                    sub = 4;
                    height = 24;
                    break;
                case 14:
                    top = isHalf ? 10 : 4;
                    sub = 4;
                    height = 28;
                    break;
                default:
                    top = 0;
                    sub = 0;
                    height = 0;
                    break;
            }
        }

        /// <summary>
        /// フォントの各文字の最大幅・最大高さを返す
        /// </summary>
        /// <param name="fontsize">フォントサイズ</param>
        /// <param name="texttype">判定する文字種類(A, K, N, R, S, T, C, J)</param>
        public static SizeF GetMaxCharSize(int fontsize, string texttype, ref int top)
        {
            SizeF ret = new SizeF(0, 0);
            switch (texttype)
            {
                case DspItem.ItemType.A:
                    ret = GetMaxCharSize(fontsize, CommonFunc.TextType.A, ref top);
                    break;
                case DspItem.ItemType.K:
                    ret = GetMaxCharSize(fontsize, CommonFunc.TextType.K, ref top);
                    break;
                case DspItem.ItemType.N:
                    ret = GetMaxCharSize(fontsize, CommonFunc.TextType.N, ref top);
                    break;
                case DspItem.ItemType.R:
                    ret = GetMaxCharSize(fontsize, CommonFunc.TextType.R, ref top);
                    break;
                case DspItem.ItemType.S:
                    ret = GetMaxCharSize(fontsize, CommonFunc.TextType.S, ref top);
                    break;
                case DspItem.ItemType.T:
                    ret = GetMaxCharSize(fontsize, CommonFunc.TextType.T, ref top);
                    break;
                case DspItem.ItemType.C:
                    ret = GetMaxCharSize(fontsize, CommonFunc.TextType.C, ref top);
                    break;
                case DspItem.ItemType.J:
                    ret = GetMaxCharSize(fontsize, CommonFunc.TextType.J, ref top);
                    break;
                case DspItem.ItemType.D:
                    ret = GetMaxCharSize(fontsize, CommonFunc.TextType.D, ref top);
                    break;
                case DspItem.ItemType.V:
                    ret = GetMaxCharSize(fontsize, CommonFunc.TextType.V, ref top);
                    break;
                case DspItem.ItemType.W:
                    ret = GetMaxCharSize(fontsize, CommonFunc.TextType.W, ref top);
                    break;
                case DspItem.ItemType.AST:
					//「*」の場合は37(固定)を返す
					ret = GetMaxCharSize(fontsize, CommonFunc.TextType.C, ref top);
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

    }
}
