using System;
using System.Threading;
using System.Globalization;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using IIPCommonClass.DB;

namespace IIPCommonClass
{
    /// <summary>
    /// 
    /// </summary>
    public class iBicsCalendar : System.Globalization.JapaneseCalendar
    {
        private static string[] mHolidays;
        private static string[] mGengoList;
        private static string[] mRGengoList;
        private static DataRow[] mDrGengo;

        //英字、英略字格納
        private static string[] mGengoEijiList;
        private static string[] mRGengoEijiList;

        private static string GTABLE_NAME = "Era";
        private static string GTABLE_DBUSER = "DBSYS";

        private const string GM_NO = "SEQ";
        private const string GM_GENGO_KANJI = "KANJI";
        private const string GM_GENGO_SKANJI = "SHORTKANJI"; 
        private const string GM_GENGO_ROMAN = "ROMAN";
        private const string GM_GENGO_SROMAN = "SHORTROMAN";
        private const string GM_CHANGE_VALUE = "TOWAREKIYEAR";
        private const string GM_SDATE = "STARTDATE";
        private const string GM_EDATE = "ENDDATE";

        private static JapaneseCalendar mJCal = new JapaneseCalendar();

        public iBicsCalendar()
        {

        }
        public iBicsCalendar(string _GTABLE_NAME, string _GTABLE_DBUSER)
        {
            GTABLE_NAME = _GTABLE_NAME;
            GTABLE_DBUSER = _GTABLE_DBUSER;
        }

        /// <summary>
        /// 休日リスト
        /// </summary>
        public string[] Holidays
        {
            set { mHolidays = value; }
            get { return mHolidays; }
        }

        /// <summary>
        /// HOLIDAYテーブル取得
        /// </summary>
        /// <returns></returns>
        public void SetHolidays(List<DBManager> listDb)
        {
            DataRow[] drs;
            //DBSYS.HOLIDAYテーブルよりデータ取得
            string _strQuery = "SELECT * FROM DBSYS.HOLIDAY";
            for (int i = 0; i < listDb.Count; i++)
            {
                 drs = listDb[i].ExecuteRows(_strQuery, "HOLIDAY");
                 mHolidays = new string[drs.Length];
                for (int r = 0; i < drs.Length; i++)
                {
                    mHolidays.SetValue(drs[r]["DAY"].ToString(), i);
                }

            }
            
           
            //DBSYS.ERAテーブルよりデータ取得
            _strQuery = "SELECT * FROM " + GTABLE_DBUSER + "." + GTABLE_NAME + " ORDER BY " + GM_EDATE + " DESC";

            for (int s = 0; s < listDb.Count; s++)
            {
                mDrGengo = listDb[s].ExecuteRows(_strQuery, GTABLE_NAME);

                mGengoList = new string[mDrGengo.Length];
                mRGengoList = new string[mDrGengo.Length];
                mGengoEijiList = new string[mDrGengo.Length];
                mRGengoEijiList = new string[mDrGengo.Length];

                for (int i = 0; i < mDrGengo.Length; i++)
                {
                    mGengoList.SetValue(mDrGengo[i][GM_GENGO_KANJI].ToString(), i);
                    mRGengoList.SetValue(mDrGengo[i][GM_GENGO_SKANJI].ToString(), i);
                    mGengoEijiList.SetValue(mDrGengo[i][GM_GENGO_ROMAN].ToString(), i);
                    mRGengoEijiList.SetValue(mDrGengo[i][GM_GENGO_SROMAN].ToString(), i);
                }

            }

        }

        /// <summary>
        /// 元号リストを取得
        /// </summary>
        /// <returns></returns>
        public  object[] getGengoList()
        {
            CultureInfo cl = Thread.CurrentThread.CurrentCulture;
            object[] obj = null;
            try
            {
                if (mGengoList == null)
                {
                    CultureInfo jp = new CultureInfo("ja-JP");
                    Thread.CurrentThread.CurrentCulture = jp;
                    jp.DateTimeFormat.Calendar = new JapaneseCalendar();

                    obj = new object[mJCal.Eras.GetLength(0)];
                    int i = 0;
                    foreach (int ec in mJCal.Eras)
                    {
                        obj.SetValue((object)jp.DateTimeFormat.GetEraName(ec), i);
                        i++;
                    }
                }
                else
                {
                    obj = (object[])mGengoList;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = cl;
            }
            return obj;
        }
        /// <summary>
        /// 元号略称リストを取得
        /// </summary>
        /// <returns></returns>
        public object[] getRGengoList()
        {
            return mGengoList;
        }

        /// <summary>
        /// 西暦をもらって元号名漢字を返す
        /// </summary>
        /// <param name="time">西暦のセットされたDateTime object</param>
        /// 
        /// <returns>元号漢字を返す
        /// 例外  -1:桁エラー 
        ///       -2:日付型エラー
        ///       -3:範囲エラー        
        ///</returns>

        public string getGengo(int idt)
        {

            string strDate = Convert.ToString(idt);
            string strGen;
            if (strDate.Length != 8)
            {
                return "-1";	// 桁エラー
            }
            try
            {
                foreach (DataRow dr in mDrGengo)
                {
                    //開始日-終了日の間
                    if (Convert.ToInt32(dr[GM_SDATE]) <= idt &&
                        Convert.ToInt32(dr[GM_EDATE]) >= idt)
                    {
                        return dr[GM_GENGO_KANJI].ToString();
                    }
                }
                strGen = "-3";　 //eraが暦の有効な時代年号を表していません
            }
            catch (FormatException)
            {
                strGen = "-2";
            }
            catch (ArgumentOutOfRangeException)
            {
                strGen = "-3";　 //eraが暦の有効な時代年号を表していません
            }
            finally
            {
            }
            return strGen;
        }
        /// <summary>
        /// 西暦をもらって元号名略称漢字を返す
        /// </summary>
        /// <param name="time">西暦のセットされたDateTime object</param>
        /// 
        /// <returns>元号漢字を返す
        /// 例外  -1:桁エラー 
        ///       -2:日付型エラー
        ///       -3:範囲エラー        
        ///</returns>

        public string getRGengo(int idt)
        {

            string strDate = Convert.ToString(idt);
            string strGen;
            if (strDate.Length != 8)
            {
                return "-1";	// 桁エラー
            }
            try
            {
                foreach (DataRow dr in mDrGengo)
                {
                    //開始日-終了日の間
                    if (Convert.ToInt32(dr[GM_SDATE]) <= idt &&
                        Convert.ToInt32(dr[GM_EDATE]) >= idt)
                    {
                        return dr[GM_GENGO_SKANJI].ToString();
                    }
                }
                strGen = "-3";　 //eraが暦の有効な時代年号を表していません
            }
            catch (FormatException)
            {
                strGen = "-2";
            }
            catch (ArgumentOutOfRangeException)
            {
                strGen = "-3";　 //eraが暦の有効な時代年号を表していません
            }
            finally
            {
            }
            return strGen;
        }
        /// <summary>
        /// 西暦を和暦(int 6桁)に変換する。
        /// </summary>
        /// <param name="dt">西暦のセットされたDateTime object</param>
        /// <returns>6桁和暦年月日
        /// 例外  -1:桁エラー 
        ///       -2:日付型エラー
        ///       -3:範囲エラー       
        ///</returns>
        public int getWareki(int idt)
        {

            string strDate;
            int iWdate;
            strDate = Convert.ToString(idt);
            if (strDate.Length != 8)
            {
                return -1;	// 桁エラー
            }
            else if (!isDate(strDate))
            {
                return -2;  //日付型エラー
            }

            try
            {
                foreach (DataRow dr in mDrGengo)
                {
                    //開始日-終了日の間
                    if (Convert.ToInt32(dr[GM_SDATE]) <= idt &&
                        Convert.ToInt32(dr[GM_EDATE]) >= idt)
                    {
                        return idt - Convert.ToInt16(dr[GM_CHANGE_VALUE]) * 10000;
                    }
                }
                iWdate = -3;　 //eraが暦の有効な時代年号を表していません
            }
            catch (FormatException)
            {
                iWdate = -2;
            }
            catch (ArgumentOutOfRangeException)
            {
                iWdate = -3;　 //eraが暦の有効な時代年号を表していません
            }
            finally
            {

            }
            return iWdate;
        }
        /// <summary>
        /// 西暦年(int 4桁)を和暦年(int 2桁)に変換する。
        /// </summary>
        /// <param name="iyear">4桁西暦年のセットされたint object</param>
        /// <returns>2桁和暦年
        /// 例外  -1:桁エラー 
        ///       -2:日付型エラー
        ///       -3:範囲エラー       
        ///</returns>
        public static int getWarekiYear(int iyear)
        {

            string strYear;
            strYear = Convert.ToString(iyear);
            if (strYear.Length != 4)
            {
                return -1;	// 桁エラー
            }
            int iWyear = 0;

            try
            {

                //入力年が明治以前の場合はエラーを設定
                if (iyear > 1867)
                {

                    //西暦変換値で和暦に変換
                    for (int i = mDrGengo.Length; i > 0; i--)
                    {
                        if (iyear > Convert.ToInt32(mDrGengo[i - 1][GM_CHANGE_VALUE]))
                        {
                            iWyear = iyear - Convert.ToInt32(mDrGengo[i - 1][GM_CHANGE_VALUE]);
                        }
                    }
                }
                else
                {
                    iWyear = -3;
                }
            }
            catch (FormatException)
            {
                iWyear = -2;
            }
            catch (ArgumentOutOfRangeException)
            {
                iWyear = -3;　 //eraが暦の有効な時代年号を表していません
            }
            finally
            {

            }
            return iWyear;
        }
        /// <summary>
        /// 西暦年(int 4桁)を旧和暦年(int 2桁)に変換する。
        /// </summary>
        /// <param name="iyear">4桁西暦年のセットされたint object</param>
        /// <returns>2桁旧和暦年
        /// 例外  -1:桁エラー 
        ///       -2:日付型エラー
        ///       -3:範囲エラー       
        ///</returns>
        public static int getWarekiYearPrevEra(int iyear)
        {

            string strYear;
            strYear = Convert.ToString(iyear);
            if (strYear.Length != 4)
            {
                return -1;	// 桁エラー
            }
            int iWyear = 0;

            try
            {

                //入力年が明治以前の場合はエラーを設定
                if (iyear > 1867)
                {

                    //西暦変換値で和暦に変換
                    for (int i = mDrGengo.Length; i > 1; i--)
                    {
                        if (iyear > Convert.ToInt32(mDrGengo[i - 1][GM_CHANGE_VALUE]))
                        {
                            iWyear = iyear - Convert.ToInt32(mDrGengo[i - 1][GM_CHANGE_VALUE]);
                        }
                    }
                }
                else
                {
                    iWyear = -3;
                }
            }
            catch (FormatException)
            {
                iWyear = -2;
            }
            catch (ArgumentOutOfRangeException)
            {
                iWyear = -3;　 //eraが暦の有効な時代年号を表していません
            }
            finally
            {

            }
            return iWyear;
        }
        /// <summary>
        /// 和暦と元号→西暦
        /// </summary>
        /// <param name="idt">和暦（6桁数字）</param>
        /// <returns>西暦（8桁数字)  
        /// 例外  -1:桁エラー 
        ///       -2:日付型エラー
        ///       -3:範囲エラー       
        ///</returns>
        public int getSeireki(int idt)
        {

            string strgen = Convert.ToString(getGengoList()[0]); //現在の元号
            return getSeireki(idt, strgen);

        }
        /// <summary>
        /// 和暦と元号→西暦
        /// </summary>
        /// <param name="idt">和暦（6桁数字）</param>
        /// <param name="strgen">元号（漢字）</param>
        /// <returns>西暦（8桁数字)  
        /// 例外  -1:桁エラー 
        ///       -2:日付型エラー
        ///       -3:範囲エラー       
        ///</returns>
        public int getSeireki(int idt, string strgen)
        {
            int iSdate;

            string strDate = Convert.ToString(idt);
            if (strDate.Length != 5 && strDate.Length != 6)
            {
                return -1;	// 桁エラー
            }
            try
            {
                foreach (DataRow dr in mDrGengo)
                {
                    //開始日-終了日の間
                    if (dr[GM_GENGO_KANJI].ToString() == strgen)
                    {
                        iSdate = idt + Convert.ToInt16(dr[GM_CHANGE_VALUE]) * 10000;
                        if (isDate(iSdate.ToString()))
                        {
                            return iSdate;
                        }
                        else
                        {
                            return -2;  //日付型エラー
                        }
                    }
                }
                iSdate = -3;	//日付妥当性エラー
            }
            catch (FormatException)
            {
                iSdate = -2;
            }
            catch (ArgumentOutOfRangeException)
            {

                iSdate = -3;	//日付妥当性エラー
            }
            return iSdate;
        }

        /// <summary>
        /// 営業日取得
        /// （渡された日付が営業日でない場合、翌営業日を取得）
        /// </summary>
        /// <param name="day">日付</param>
        /// <param name="holidays">休日</param>
        /// <returns>営業日</returns>
        public static int GetSettleDay(int day, string[] holidays)
        {
            int iSettleDate = day;
            if (!iBicsCalendar.isBusinessday(day, holidays))
            {
                iSettleDate = iBicsCalendar.getBusinessday(day, 1, holidays);
            }
            return iSettleDate;
        }

        /// <summary>
        /// 営業日取得
        /// </summary>
        /// <param name="day"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public int getBusinessday(int day, int days)
        {
            return getBusinessday(day, days, mHolidays);
        }

        /// <summary>
        /// 営業日取得
        /// </summary>
        /// <param name="day">日付</param>
        /// <param name="days">～営業日後</param>
        /// <param name="holidays">休日</param>
        /// <returns>営業日</returns>
        public static int getBusinessday(int day, int days, string[] holidays)
        {
            int businessday;
            string strDate = Convert.ToString(day);
            if (strDate.Length != 8)
            {
                return -1;	// 桁エラー
            }
            CultureInfo cl = Thread.CurrentThread.CurrentCulture;
            try
            {

                strDate = strDate.Substring(0, 4) + "/" + strDate.Substring(4, 2) + "/" + strDate.Substring(6, 2);
                CultureInfo jp = new CultureInfo("ja-JP");
                Thread.CurrentThread.CurrentCulture = jp;
                jp.DateTimeFormat.Calendar = new JapaneseCalendar();
                DateTime dt = Convert.ToDateTime(strDate, cl);

                DateTime wdt = new DateTime(dt.Ticks);
                string nextday = dt.ToString("yyyyMMdd", cl);
                DayOfWeek we;
                int i;
                // ～営業日前
                if (days < 0)
                {
                    for (i = 0; i > days; i--)
                    {
                        wdt = wdt.AddDays(-1);
                        we = jp.DateTimeFormat.Calendar.GetDayOfWeek(wdt);
                        nextday = wdt.ToString("yyyyMMdd", cl);
                        if (we == DayOfWeek.Saturday || we == DayOfWeek.Sunday)
                        {
                            i++;
                        }
                        else
                        {
                            foreach (string holi in holidays)
                            {
                                if (nextday == holi)
                                {
                                    i++;
                                }
                            }
                        }
                    }
                    //～営業日後
                }
                else
                {
                    for (i = 0; i < days; i++)
                    {
                        wdt = wdt.AddDays(1);
                        we = jp.DateTimeFormat.Calendar.GetDayOfWeek(wdt);
                        nextday = wdt.ToString("yyyyMMdd", cl);
                        if (we == DayOfWeek.Saturday || we == DayOfWeek.Sunday)
                        {
                            i--;
                        }
                        else
                        {
                            foreach (string holi in holidays)
                            {
                                if (nextday == holi)
                                {
                                    i--;
                                }
                            }
                        }
                    }
                }
                businessday = Convert.ToInt32(nextday);
            }
            catch (Exception)
            {
                businessday = -2;
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = cl;
            }
            return businessday;
        }
        /// <summary>
        /// 営業日チェック
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public bool isBusinessday(int day)
        {
            return iBicsCalendar.isBusinessday(day, mHolidays);
        }

        /// <summary>
        /// 営業日チェック
        /// </summary>
        /// <param name="day">日付</param>
        /// <param name="holidays">祝日</param>
        /// <returns></returns>
        public static bool isBusinessday(int day, string[] holidays)
        {
            bool ret = true;
            string strDate = Convert.ToString(day);
            if (strDate.Length != 8)
            {
                return false;	// 桁エラー
            }
            CultureInfo cl = Thread.CurrentThread.CurrentCulture;
            try
            {
                strDate = strDate.Substring(0, 4) + "/" + strDate.Substring(4, 2) + "/" + strDate.Substring(6, 2);
                CultureInfo jp = new CultureInfo("ja-JP");
                Thread.CurrentThread.CurrentCulture = jp;
                jp.DateTimeFormat.Calendar = new JapaneseCalendar();
                DateTime dt = Convert.ToDateTime(strDate, cl);
                DayOfWeek we = jp.DateTimeFormat.Calendar.GetDayOfWeek(dt);
                if (we == DayOfWeek.Saturday || we == DayOfWeek.Sunday)
                {
                    ret = false;
                }
                else
                {
                    string nextday = dt.ToString("yyyyMMdd", cl);
                    foreach (string holi in holidays)
                    {
                        if (nextday == holi)
                        {
                            ret = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                ret = false;
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = cl;
            }
            return ret;
        }
        /// <summary>
        /// 6桁の日付を8桁の日付に変える
        /// </summary>
        public string date6to8(string date6)
        {
            DateTime date8 = DateTime.Parse(String.Concat(date6.Substring(0, 2), "/", date6.Substring(2, 2), "/", date6.Substring(4, 2)));
            return date8.ToString("yyyyMMdd");
        }
        /// <summary>
        /// 日付５桁６桁８桁を/区切りに編集する
        /// </summary>
        /// <param name="date"></param>
        /// <returns>yy/MM/dd, yyyy/MM/dd</returns>
        public static string datePlanetoDisp(string date)
        {
            string retDate = null;
            if (date.Length == 5)
            {
                date = "0" + date;
                retDate = date.Substring(0, 2) + "/" + date.Substring(2, 2) + "/" + date.Substring(4, 2);
            }
            else if (date.Length == 6)
            {
                retDate = date.Substring(0, 2) + "/" + date.Substring(2, 2) + "/" + date.Substring(4, 2);
            }
            else if (date.Length == 8)
            {
                retDate = date.Substring(0, 4) + "/" + date.Substring(4, 2) + "/" + date.Substring(6, 2);
            }
            return retDate;
        }
        /// <summary>
        /// 日付５桁６桁８桁を年月日区切りに編集する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string datePlanetoDisp2(string date)
        {
            string retDate = null;
            if (date.Length == 5)
            {
                date = "0" + date;
                retDate = date.Substring(0, 2) + "年" + date.Substring(2, 2) + "月" + date.Substring(4, 2) + "日";
            }
            else if (date.Length == 6)
            {
                retDate = date.Substring(0, 2) + "年" + date.Substring(2, 2) + "月" + date.Substring(4, 2) + "日";
            }
            else if (date.Length == 8)
            {
                retDate = date.Substring(0, 4) + "年" + date.Substring(4, 2) + "月" + date.Substring(6, 2) + "日";
            }
            return retDate;
        }
        /// <summary>
        /// 日付５桁６桁８桁を.切りに編集する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string datePlanetoDisp3(string date)
        {
            string retDate = null;
            if (date.Length == 5)
            {
                date = "0" + date;
                retDate = date.Substring(0, 2) + "." + date.Substring(2, 2) + "." + date.Substring(4, 2);
            }
            else if (date.Length == 6)
            {
                retDate = date.Substring(0, 2) + "." + date.Substring(2, 2) + "." + date.Substring(4, 2);
            }
            else if (date.Length == 8)
            {
                retDate = date.Substring(0, 4) + "." + date.Substring(4, 2) + "." + date.Substring(6, 2);
            }
            return retDate;
        }
        /// <summary>
        /// int yyyymmdd string "."を string "元号 yy.mm.dd"に変換（設定したseparaterで区切る）
        /// </summary>
        /// <param name="iStartDateWest"></param>
        /// <param name="strSeparater"></param>
        /// <returns></returns>
        public string ChangeWestToJpPlusEra(int iStartDateWest, string strSeparater)
        {
            string strStartDateJp = getWareki(iStartDateWest).ToString();
            strStartDateJp = strStartDateJp.PadLeft(6, '0').Insert(2, strSeparater);
            strStartDateJp = strStartDateJp.Insert(5, strSeparater);
            strStartDateJp = getGengo(iStartDateWest) + " " + strStartDateJp;
            return (strStartDateJp);
        }
        /// <summary>
        /// int yyyymmdd string "."を string "略称元号 yy.mm.dd"に変換（設定したseparaterで区切る）
        /// </summary>
        /// <param name="iStartDateWest"></param>
        /// <param name="strSeparater"></param>
        /// <returns></returns>
        public string ChangeWestToRJpPlusEra(int iStartDateWest, string strSeparater)
        {
            string strStartDateJp = getWareki(iStartDateWest).ToString();
            strStartDateJp = strStartDateJp.PadLeft(6, '0').Insert(2, strSeparater);
            strStartDateJp = strStartDateJp.Insert(5, strSeparater);
            strStartDateJp = getRGengo(iStartDateWest) + " " + strStartDateJp;
            return (strStartDateJp);
        }
        /// <summary>
        /// int yyyymmdd を string "yy.mm.dd"に変換（設定したseparaterで区切る）
        /// </summary>
        /// <param name="iStartDateWest"></param>
        /// <param name="strSeparater"></param>
        /// <returns></returns>
        public string ChangeWestToJp(int iStartDateWest, string strSeparater)
        {
            string strStartDateJp = getWareki(iStartDateWest).ToString();
            if (Convert.ToInt32(strStartDateJp) > 0)
            {
                strStartDateJp = strStartDateJp.PadLeft(6, '0').Insert(2, strSeparater);
                strStartDateJp = strStartDateJp.Insert(5, strSeparater);
            }
            else
            {
                if (iStartDateWest > 8)
                {
                    strStartDateJp = iStartDateWest.ToString().Substring(0, 8);
                }
                else
                {
                    strStartDateJp = iStartDateWest.ToString();
                }
            }
            return (strStartDateJp);
        }
        /// <summary>
        /// int yyyymmdd の翌日（int "yyyymmdd"）を取得
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static int getNextday(int day)
        {
            int iNextDay;
            string strDate = Convert.ToString(day);
            if (strDate.Length != 8)
            {
                return -1;	// 桁エラー
            }
            CultureInfo cl = Thread.CurrentThread.CurrentCulture;
            try
            {
                strDate = strDate.Substring(0, 4) + "/" + strDate.Substring(4, 2) + "/" + strDate.Substring(6, 2);
                CultureInfo jp = new CultureInfo("ja-JP");
                Thread.CurrentThread.CurrentCulture = jp;
                jp.DateTimeFormat.Calendar = new JapaneseCalendar();
                DateTime dt = Convert.ToDateTime(strDate, cl);
                DateTime wdt = new DateTime(dt.Ticks);
                wdt = wdt.AddDays(1.0);
                string strNextDay = wdt.ToString("yyyyMMdd", cl);
                iNextDay = Convert.ToInt32(strNextDay);
            }
            catch (Exception)
            {
                iNextDay = -2;
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = cl;
            }
            return iNextDay;
        }

        /// <summary>
        /// int yyyymmdd の前日（int "yyyymmdd"）を取得
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static int getPreviousday(int day)
        {
            int iPreviousDay;
            string strDate = Convert.ToString(day);
            if (strDate.Length != 8)
            {
                return -1;	// 桁エラー
            }
            CultureInfo cl = Thread.CurrentThread.CurrentCulture;
            try
            {
                strDate = strDate.Substring(0, 4) + "/" + strDate.Substring(4, 2) + "/" + strDate.Substring(6, 2);
                CultureInfo jp = new CultureInfo("ja-JP");
                Thread.CurrentThread.CurrentCulture = jp;
                jp.DateTimeFormat.Calendar = new JapaneseCalendar();
                DateTime dt = Convert.ToDateTime(strDate, cl);
                DateTime wdt = new DateTime(dt.Ticks);
                wdt = wdt.AddDays(-1.0);
                string strPreviousDay = wdt.ToString("yyyyMMdd", cl);
                iPreviousDay = Convert.ToInt32(strPreviousDay);
            }
            catch (Exception)
            {
                iPreviousDay = -2;
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = cl;
            }
            return iPreviousDay;
        }
        /// <summary>
        /// int yyyymm の翌月（int "yyyymm"）を取得
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static int getNextMonth(int iMonth)
        {
            int iNextiMonth;
            string strDate = Convert.ToString(iMonth) + "01";
            if (strDate.Length != 8)
            {
                return -1;	// 桁エラー
            }
            CultureInfo cl = Thread.CurrentThread.CurrentCulture;
            try
            {
                strDate = strDate.Substring(0, 4) + "/" + strDate.Substring(4, 2) + "/" + strDate.Substring(6, 2);
                CultureInfo jp = new CultureInfo("ja-JP");
                Thread.CurrentThread.CurrentCulture = jp;
                jp.DateTimeFormat.Calendar = new JapaneseCalendar();
                DateTime dt = Convert.ToDateTime(strDate, cl);
                DateTime wdt = new DateTime(dt.Ticks);
                wdt = wdt.AddMonths(1);
                string strNextDay = wdt.ToString("yyyyMMdd", cl);
                iNextiMonth = Convert.ToInt32(strNextDay.Substring(0, 6));
            }
            catch (Exception)
            {
                iNextiMonth = -2;
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = cl;
            }
            return iNextiMonth;
        }
        /// <summary>
        /// int yyyymm の翌月（int "yyyymm"）を取得
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static int getNextMonth(int iMonth, int iaddMonth)
        {
            int iNextiMonth;
            string strDate = Convert.ToString(iMonth) + "01";
            if (strDate.Length != 8)
            {
                return -1;	// 桁エラー
            }
            CultureInfo cl = Thread.CurrentThread.CurrentCulture;
            try
            {
                strDate = strDate.Substring(0, 4) + "/" + strDate.Substring(4, 2) + "/" + strDate.Substring(6, 2);
                CultureInfo jp = new CultureInfo("ja-JP");
                Thread.CurrentThread.CurrentCulture = jp;
                jp.DateTimeFormat.Calendar = new JapaneseCalendar();
                DateTime dt = Convert.ToDateTime(strDate, cl);
                DateTime wdt = new DateTime(dt.Ticks);
                wdt = wdt.AddMonths(iaddMonth);
                string strNextDay = wdt.ToString("yyyyMMdd", cl);
                iNextiMonth = Convert.ToInt32(strNextDay.Substring(0, 6));
            }
            catch (Exception)
            {
                iNextiMonth = -2;
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = cl;
            }
            return iNextiMonth;
        }

        /// <summary>
        /// string yyyymmdd の年度（string "yyyy"）を取得
        /// </summary>
        /// <param name="yyyymmdd"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static string GetNendo(string yyyymmdd, int start)
        {
            // 何か月戻すべきかを計算する。
            int backTo = (start - 1) * -1;

            // yyyymmdd を DateTime に変換する。
            var dt = DateTime.ParseExact(yyyymmdd, "yyyyMMdd", null);

            // 年度を計算して返す
            return dt.AddMonths(backTo).ToString("yyyy");
        }

        /// <summary>
        /// 営業日チェック
        /// </summary>
        /// <param name="day">日付</param>
        /// <returns></returns>
        public static bool isDate(string sday)
        {
            bool ret = true;
            try
            {
                if (sday.Length != 8)
                {
                    return false;
                }
                string buf = sday.Substring(0, 4) + "/" + sday.Substring(4, 2) + "/" + sday.Substring(6, 2);
                DateTime dt;

                ret = DateTime.TryParse(buf, out dt);
            }
            catch (Exception)
            {
                ret = false;
            }
            finally
            {

            }
            return ret;
        }

        /// <summary>
        /// 和暦（元号なし）→西暦
        /// </summary>
        /// <param name="idt">和暦（6桁数字）</param>
        /// <param name="ilmt">旧元号下限値（6桁数字）</param>
        /// <returns>西暦（8桁数字)  
        /// 例外  -1:桁エラー 
        ///       -2:日付型エラー
        ///       -3:範囲エラー       
        ///</returns>
        public static int getGengouWarekiUpperLimit(int idt, int ilmt)
        {

            if (idt.ToString().Length != 5 && idt.ToString().Length != 6)
            {
                return -1;  // 桁エラー
            }

            int iSdate = 0;

            try
            {
                if (idt > ilmt)
                {
                    //閾値を超えたら１つ前の元号で西暦変換
                    iSdate = idt + (10000 * Convert.ToInt32(mDrGengo[1][GM_CHANGE_VALUE]));
                }
                else
                {
                    //最新元号で西暦変換
                    iSdate = idt + (10000 * Convert.ToInt32(mDrGengo[0][GM_CHANGE_VALUE]));
                }

                //8桁が暦日か確認
                string strDate;
                strDate = Convert.ToString(iSdate);
                strDate = strDate.Substring(0, 4) + "/" + strDate.Substring(4, 2) + "/" + strDate.Substring(6, 2);
                DateTime dt = Convert.ToDateTime(strDate);

            }
            catch (FormatException)
            {
                iSdate = -2;
            }
            catch (ArgumentOutOfRangeException)
            {
                iSdate = -3;    //日付妥当性エラー
            }
            return iSdate;
        }

        /// <summary>
        /// 和暦年（元号なし）→西暦
        /// </summary>
        /// <param name="iyear">和暦年（2桁数字）</param>
        /// <param name="ilmt">旧元号下限値（6桁数字）</param>
        /// <returns>西暦年（4桁数字)  
        /// 例外  -1:桁エラー 
        ///       -2:日付型エラー
        ///       -3:範囲エラー       
        ///</returns>
        public static int getGengouWarekiUpperLimitYear(int iyear, int ilmt)
        {

            if (iyear.ToString().Length != 1 && iyear.ToString().Length != 2)
            {
                return -1;  // 桁エラー
            }

            int iSyear = 0;

            try
            {
                if (iyear > Convert.ToInt32(ilmt.ToString().PadLeft(6, '0').Substring(0, 2)))
                {
                    //閾値を超えたら１つ前の元号で西暦変換
                    iSyear = iyear + Convert.ToInt32(mDrGengo[1][GM_CHANGE_VALUE]);
                }
                else
                {
                    //最新元号で西暦変換
                    iSyear = iyear + Convert.ToInt32(mDrGengo[0][GM_CHANGE_VALUE]);
                }

            }
            catch (FormatException)
            {
                iSyear = -2;
            }
            catch (ArgumentOutOfRangeException)
            {
                iSyear = -3;    //日付妥当性エラー
            }
            return iSyear;
        }

    }
}
