using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Common;
using CommonClass;
using CommonClass.DB;
using CommonTable.DB;

namespace EntryCommon
{
    /// <summary>
    /// 営業日カレンダー
    /// </summary>
	public class Calendar
	{

		private Calendar () { }
		static Calendar()
		{
		}

        /// <summary>
        /// 日付有効性チェック
        /// </summary>
        public static bool IsDate(string date)
        {
            return CommonClass.iBicsCalendar.isDate(date);
        }

        /// <summary>
        /// 営業日かどうかチェックする
        /// </summary>
        /// <param name="opedate">処理日付（yyyymmdd）</param>
        /// <returns></returns>
        public static bool IsBusinessDate(int date)
		{
            iBicsCalendar cal = new iBicsCalendar();
            return cal.isBusinessday(date);
		}

        /// <summary>
        /// 営業日取得
        /// （渡された日付が営業日でない場合、翌営業日を取得）
        /// </summary>
        /// <param name="day">日付</param>
        /// <returns>営業日</returns>
        public static int GetSettleDay(int date)
        {
            iBicsCalendar cal = new iBicsCalendar();
            return iBicsCalendar.GetSettleDay(date, cal.Holidays);
        }

        /// <summary>
        /// 和暦を西暦に変換する
        /// </summary>
        /// <param name="wType"></param>
        /// <param name="WYYMMDD"></param>
        /// <returns></returns>
        public static void FetchEra(ConvCalInfo wCal)
        {
            // SELECT実行
            wCal.EraList = new SortedDictionary<int, TBL_ERA>();
            using (AdoDatabaseProvider dbp = GenDbProviderFactory.CreateAdoProvider1())
            {
                string strSQL = TBL_ERA.GetSelectQuery();
                try
                {
                    DataTable tbl = dbp.SelectTable(strSQL, new List<IDbDataParameter>());
                    foreach (DataRow row in tbl.Rows)
                    {
                        TBL_ERA era = new TBL_ERA(row);
                        wCal.EraList.Add(era._SEQ, era);
                    }
                }
                catch (Exception ex)
                {
                    wCal.ErrMsg = ex.Message;
                    LogWriter.writeLog(MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                    return;
                }
            }
        }

        /// <summary>
        /// 和暦を西暦に変換する
        /// </summary>
        public static void ConvToSeireki(ConvCalInfo wCal)
        {
            if (wCal.EraList.Count < 1)
            {
                wCal.ErrMsg = "和暦変換テーブルがないため変換できません。";
                return;
            }

            // 和暦変換テーブル
            bool isExist = false;
            foreach (TBL_ERA era in wCal.EraList.Values)
            {
                // 元号
                switch (wCal.Type)
                {
                    case WarekiType.SEQ:
                        if (!wCal.W.Equals(era._SEQ.ToString())) { continue; }
                        break;
                    case WarekiType.SHORTKANJI:
                        if (!wCal.W.Equals(era.m_SHORTKANJI)) { continue; }
                        break;
                    case WarekiType.SHORTROMAN:
                        if (!wCal.W.Equals(era.m_SHORTROMAN)) { continue; }
                        break;
                }

                // 西暦年算出
                wCal.YYYY = wCal.YY + era.m_TOWAREKIYEAR;
                isExist = true;
                break;
            }
            if (!isExist)
            {
                wCal.ErrMsg = "元号の指定が正しくありません。";
                return;
            }

            // 日付設定
            wCal.SetDate();
            if (!string.IsNullOrEmpty(wCal.ErrMsg))
            {
                return;
            }
        }

        /// <summary>
        /// 西暦を和暦に変換する
        /// </summary>
        public static void ConvToWareki(ConvCalInfo wCal)
        {
            if (wCal.EraList.Count < 1)
            {
                wCal.ErrMsg = "和暦変換テーブルがないため変換できません。";
                return;
            }

            // 日付設定
            wCal.SetDate();
            if (!string.IsNullOrEmpty(wCal.ErrMsg))
            {
                return;
            }

            // 和暦変換テーブル
            bool isExist = false;
            foreach (TBL_ERA era in wCal.EraList.Values)
            {
                if (wCal.nYYYYMMDD < era.m_STARTDATE)
                {
                    // 和暦年算出
                    wCal.YY = wCal.YYYY - era.m_TOWAREKIYEAR;

                    // 元号
                    switch (wCal.Type)
                    {
                        case WarekiType.SEQ:
                            wCal.W = era._SEQ.ToString();
                            break;
                        case WarekiType.SHORTKANJI:
                            wCal.W = era.m_SHORTKANJI;
                            break;
                        case WarekiType.SHORTROMAN:
                            wCal.W = era.m_SHORTROMAN;
                            break;
                    }
                    isExist = true;
                    break;
                }
            }
            if (!isExist)
            {
                wCal.ErrMsg = "元号の指定が正しくありません。";
                return;
            }
        }

        /// <summary>
        /// 変換カレンダー情報
        /// </summary>
        public class ConvCalInfo
        {
            public int Type { get; set; }

            /// <summary>元号</summary>
            public string W { get; set; } = "";
            /// <summary>西暦年</summary>
            public int YYYY { get; set; } = 0;
            /// <summary>和暦年</summary>
            public int YY { get; set; } = 0;
            /// <summary>月</summary>
            public int MM { get; set; } = 0;
            /// <summary>日</summary>
            public int DD { get; set; } = 0;

            /// <summary>西暦年月日（数値型）</summary>
            public int nYYYYMMDD { get; set; } = 0;
            /// <summary>西暦年月日（文字型）</summary>
            public string sYYYYMMDD { get; set; } = "";
            /// <summary>西暦年月日（日付型）</summary>
            public DateTime dYYYYMMDD { get; set; }
            /// <summary>和暦年月日</summary>
            public string WYYMMDD { get; set; } = "";

            /// <summary>和暦変換テーブル</summary>
            public SortedDictionary<int, TBL_ERA> EraList { get; set; }

            /// <summary>変換結果</summary>
            public string ErrMsg { get; set; } = "";

            /// <summary>
            /// コンストラクタ（西暦→和暦変換用）
            /// </summary>
            /// <param name="yyyymmdd"></param>
            public ConvCalInfo(string yyyymmdd)
            {
                this.sYYYYMMDD = yyyymmdd;
                this.EraList = new SortedDictionary<int, TBL_ERA>();

                // 形式チェック1
                if (string.IsNullOrEmpty(this.sYYYYMMDD))
                {
                    this.ErrMsg = "値が入力されていません。";
                    return;
                }
                if (this.sYYYYMMDD.Length != 8)
                {
                    this.ErrMsg = "入力値の桁数が正しくありません。";
                    return;
                }

                // 形式チェック2
                this.YYYY = DBConvert.ToIntNull(this.sYYYYMMDD.Substring(0, 4));
                this.MM = DBConvert.ToIntNull(this.sYYYYMMDD.Substring(4, 2));
                this.DD = DBConvert.ToIntNull(this.sYYYYMMDD.Substring(6, 2));
                CheckMMDD();
                if (!string.IsNullOrEmpty(this.ErrMsg))
                {
                    return;
                }

                // 形式チェック3
                SetDate();
                if (!string.IsNullOrEmpty(this.ErrMsg))
                {
                    return;
                }
            }

            /// <summary>
            /// コンストラクタ（和暦→西暦変換用）
            /// </summary>
            /// <param name="wType"></param>
            /// <param name="WYYMMDD"></param>
            public ConvCalInfo(int wType, string WYYMMDD)
            {
                this.Type = wType;
                this.WYYMMDD = WYYMMDD;
                this.EraList = new SortedDictionary<int, TBL_ERA>();

                // 形式チェック1
                if (string.IsNullOrEmpty(this.WYYMMDD))
                {
                    this.ErrMsg = "値が入力されていません。";
                    return;
                }
                if (this.WYYMMDD.Length != 7)
                {
                    this.ErrMsg = "入力値の桁数が正しくありません。";
                    return;
                }

                // 形式チェック2
                this.W = DBConvert.ToStringNull(this.WYYMMDD.Substring(0, 1));
                this.YY = DBConvert.ToIntNull(this.WYYMMDD.Substring(1, 2));
                this.MM = DBConvert.ToIntNull(this.WYYMMDD.Substring(3, 2));
                this.DD = DBConvert.ToIntNull(this.WYYMMDD.Substring(5, 2));
                CheckMMDD();
                if (!string.IsNullOrEmpty(this.ErrMsg))
                {
                    return;
                }
            }

            /// <summary>
            /// ありきたりの歴日チェック
            /// </summary>
            private void CheckMMDD()
            {
                // 月チェック
                if (!(1 <= this.MM && this.MM <= 12))
                {
                    this.ErrMsg = "月日に暦日でない値が設定されています。(月)";
                    return;
                }

                // 日チェック
                if (!(1 <= this.DD && this.DD <= 31))
                {
                    this.ErrMsg = "月日に暦日でない値が設定されています。(日)";
                    return;
                }

                // 月日チェック
                if ((this.MM == 4) || (this.MM == 6) || (this.MM == 9) || (this.MM == 11))
                {
                    if (this.DD >= 31)
                    {
                        this.ErrMsg = "月日に暦日でない値が設定されています";
                        return;
                    }
                }
                else if (this.MM == 2)
                {
                    if (this.DD >= 30)
                    {
                        this.ErrMsg = "月日に暦日でない値が設定されています";
                        return;
                    }
                }
            }

            /// <summary>
            /// 日付を設定する
            /// </summary>
            public void SetDate()
            {
                nYYYYMMDD = (YYYY * 10000) + (MM * 100) + DD;
                sYYYYMMDD = nYYYYMMDD.ToString("D8");
                DateTime dt = new DateTime();
                if (!DateTime.TryParse(CommonUtil.ConvToDateFormat(sYYYYMMDD), out dt))
                {
                    ErrMsg = "有効ではない日付が設定されています";
                }
                dYYYYMMDD = dt;
            }
        }

        /// <summary>和暦変換種別</summary>
        public class WarekiType
        {
            /// <summary>番号</summary>
            public const int SEQ = 1;
            /// <summary>元号略称</summary>
            public const int SHORTKANJI = 2;
            /// <summary>元号ローマ略称</summary>
            public const int SHORTROMAN = 3;
        }

    }

}
