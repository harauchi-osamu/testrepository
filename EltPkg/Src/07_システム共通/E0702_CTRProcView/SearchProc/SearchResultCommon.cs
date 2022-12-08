using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Common;
using CommonClass;
using CommonTable.DB;
using EntryCommon;

namespace SearchProc
{
    /// <summary>
    /// 共通処理クラス
    /// </summary>
    public class SearchResultCommon
    {

        /// <summary>
        /// 画面表示データ整形
        /// </summary>
        public static string DispDataFormat(string Data, string Format)
        {
            if (!long.TryParse(Data, out long ChgData))
            {
                return Data;
            }

            return DispDataFormat(ChgData, Format);
        }

        /// <summary>
        /// 画面表示データ整形
        /// </summary>
        public static string DispDataFormat(long Data, string Format)
        {
            return Data.ToString(Format);
        }


    }
}
