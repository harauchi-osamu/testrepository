using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows.Forms;
using IIPCommonClass.DB;

namespace IIPCommonClass
{
    public class AplInfo
    {
        #region メンバー

        private static bool _detaillog = true;
        private static TBL_GYM_SETTING _gym_setting = null;
        private static V_GYM_SETTING _v_gym_setting = null;
        private static iBicsCalendar ibc = new iBicsCalendar();
        #endregion

        #region プロパティ

        #region システム関連

        #region システム設定
        /// <summary>
        /// 実行アプリケーション名
        /// </summary>
        public static string Aplname
        {
            get { return System.IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath); }
        }

        /// <summary>
        /// ログ出力先パス
        /// </summary>
        public static string LogFolderPath
        {
            get { return ConfigAccess.Instance.LogFolderPath; }
        }

        /// <summary>
        /// エラーログパス
        /// </summary>
        public static string ErrLogFolderPath
        {
            get { return ConfigAccess.Instance.ErrLogFolderPath; }
        }

        /// <summary>
        /// エラーログ表示日数
        /// </summary>
        public static string ErrLogDays
        {
            get { return ConfigAccess.Instance.ErrLogDays; }
        }

        /// <summary>
        /// Informationレベルのログを出力するか
        /// </summary>
        public static bool DetailLog
        {
            get { return _detaillog; }
            set { _detaillog = value; }
        }

        /// <summary>
        /// フォント名
        /// </summary>
        public static string FontName
        {
            get { return ConfigAccess.Instance.FontName; }
        }

        /// <summary>
        /// 選択可能なフォントサイズの配列
        /// </summary>
        public static string[] FontSizes
        {
            get { return new string[] { "10", "11", "12", "14" }; }
        }

        #endregion


        #region INIファイル情報
        public static string MsgIni
		{
			get { return ConfigAccess.Instance.MsgINI; }
		}

        #endregion

        #endregion

        #region 接続情報
        public static string DB_SERVER
        {
            get { return ConfigurationManager.AppSettings["DB_SQL_ID"]; }
        }
        public static string DB_USER
        {
            get { return ConfigurationManager.AppSettings["DB_SQL_USER"]; }
        }

        public static string DB_PASS
        {
            get { return ConfigurationManager.AppSettings["DB_SQL_PASS"]; }
        }
        #endregion

        #region IFファイル情報
        /// <summary>
        /// IFファイル格納先フォルダ
        /// </summary>
        public static string IFFile_FileCopySendPath
        {
            get { return ConfigurationManager.AppSettings["IFFile_FileCopySendPath"]; }
        }

        /// <summary>
        /// HULFT（伝送）格納先フォルダ
        /// </summary>
        public static string Koukin_HULFTSendPath
        {
            get { return ConfigurationManager.AppSettings["Koukin_HULFTSendPath"]; }
        }

        #endregion

        #region ファイル関連
        //進捗管理・オペ稼働データ格納パス
        public static string ProgDataPath
        {
            get { return ConfigurationManager.AppSettings["ProgDataPath"]; }
        }

        //登録済の進捗管理・オペ稼働データ格納パス
        public static string ProgDataSavePath
        {
            get { return ConfigurationManager.AppSettings["ProgDataSavePath"]; }
        }

        #region ファイルパス

        /// <summary>
        /// レポートファイルがあるフォルダのパス
        /// </summary>
        public static string ReportFilePath
        {
            get { return ConfigurationManager.AppSettings["ReportFilePath"]; }
        }

        /// <summary>
        /// イメージファイルがあるフォルダのパス
        /// </summary>
        public static string ImgFilePath
        {
            get { return ConfigurationManager.AppSettings["ImgFilePath"]; }
        }

        /// <summary>
        /// OCRイメージファイルがあるフォルダのパス
        /// </summary>
        public static string OCRFilePath
        {
            get { return ConfigurationManager.AppSettings["OCRFilePath"]; }
        }

        /// <summary>
        /// 照合のイメージファイル格納パス
        /// </summary>
        public static string ShogouImgFilePath
        {
            get { return ConfigurationManager.AppSettings["ShogouImgFilePath"]; }
        }

        /// <summary>
        /// バックアップがあるフォルダ(batファイル名込)
        /// </summary>
        public static string BackUpBatch
        {
            get { return ConfigurationManager.AppSettings["BackUpBatch"]; }
        }

		/// <summary>
		/// 2次ソート用ファイルパス（1号機）
		/// </summary>
		public static string SecSortFilePath01
		{
			get { return ConfigurationManager.AppSettings["SecSortFilePath01"]; }
		}
		/// <summary>
		/// 2次ソート用ファイルパス（2号機）
		/// </summary>
		public static string SecSortFilePath02
		{
			get { return ConfigurationManager.AppSettings["SecSortFilePath02"]; }
		}

        /// <summary>
        /// 帳票サンプルイメージファイルがあるフォルダのパス
        /// </summary>
        public static string SampleFilePath
        {
            get { return ConfigurationManager.AppSettings["SampleFilePath"]; }
        }

        #endregion

        #endregion

        #region 業務関連

        /// <summary>
        /// 業務設定
        /// </summary>
        public static TBL_GYM_SETTING GYM_SETTING
        {
            get { return _gym_setting; }
        }

        /// <summary>
        /// 業務設定（View）
        /// </summary>
        public static V_GYM_SETTING V_GYM_SETTING
        {
            get { return _v_gym_setting; }
        }

        #region 業務設定

        /// <summary>
        /// 業務設定を取得
        /// </summary>
        /// <param name="gymno">業務番号</param>
        /// <returns></returns>
        public static bool GetGymSetting(int gymkbn, List<DBManager> listDbs)
        {
            try
            {
                for (int i = 0; i < listDbs.Count; i++)
                {
                    _gym_setting = new TBL_GYM_SETTING(listDbs[i].GetGymSetMstData(gymkbn));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 業務設定を取得
        /// </summary>
        /// <param name="gymno">業務番号</param>
        /// <returns></returns>
        public static bool GetGymSettingView(int gymno, List<DBManager> listDbs)
        {
            try
            {
                for (int i = 0; i < listDbs.Count; i++)
                {
                    _v_gym_setting = new V_GYM_SETTING(listDbs[i].GetGymSetMstDataView(gymno));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 処理日

        public static bool AutoDate
        {
            get { return ConfigurationManager.AppSettings["AUTODATE"].Equals("true"); }
        }

        /// <summary>
        /// 処理日を返す
        /// AUTODATE=trueのときは業務設定から取得
        /// </summary>
        /// <returns></returns>
        public static int OpDate()
        {
            try
            {
                if (V_GYM_SETTING == null)
                {
                    return int.Parse(DateTime.Today.ToString("yyyyMMdd"));
                }
                else
                {
                    return GYM_SETTING._OPERATION_DATE;
                }
            }
            catch
            {
                return 0;
            }
        }
        public static int OpAftDate(DBManager listDb)
        {
            try
            {
                if (V_GYM_SETTING == null)
                {
                    return ibc.getBusinessday(listDb.ToIntNull(DateTime.Now.ToString("yyyyMMdd")), 1);
                }
                else
                {
                    return V_GYM_SETTING.SODATE_AFT;
                }
            }
            catch
            {
                return 0;
            }
        }
        public static int OpPreDate(DBManager listDb)
        {
            try
            {
                if (V_GYM_SETTING == null)
                {
                    return ibc.getBusinessday(listDb.ToIntNull(DateTime.Now.ToString("yyyyMMdd")), -1);
                }
                else
                {
                    return V_GYM_SETTING.SODATE_PRE;
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 業務日付

        /// <summary>
        /// 業務日付
        /// 
        /// </summary>
        /// <returns>
        /// App.config[AUTODATE]=true：DBMST.GYM_SETTINGS.KODATE
        /// App.config[AUTODATE]=false:app.configのSTODAYDATE -1
        /// エラー時: 0
        /// </returns>
        public static int BUSINESSDATE(DBManager listDb)
        {
            try
            {
                if (V_GYM_SETTING == null)
                {
                    return listDb.ToIntNull(DateTime.Now.ToString("yyyyMMdd"));
                }
                else
                {
                    return V_GYM_SETTING.BUSINESSDATE;
                }
            }
            catch
            {
                return 0;
            }
        }
        public static int BUSINESS_PreDATE(DBManager listDb)
        {
            try
            {
                if (V_GYM_SETTING == null)
                {
                    return ibc.getBusinessday(listDb.ToIntNull(DateTime.Now.ToString("yyyyMMdd")), -1);
                }
                else
                {
                    return V_GYM_SETTING.BUSINESSDATE_PRE;
                }
            }
            catch
            {
                return 0;
            }
        }
        public static int BUSINESS_AftDATE(DBManager listDb)
        {
            try
            {
                if (V_GYM_SETTING == null)
                {
                    return ibc.getBusinessday(listDb.ToIntNull(DateTime.Now.ToString("yyyyMMdd")), 1);
                }
                else
                {
                    return V_GYM_SETTING.BUSINESSDATE_AFT;
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 自動更新

        public static bool AutoUpdate
        {
            get { return ConfigurationManager.AppSettings["AutoUpdate"].ToUpper().Equals("TRUE"); }
        }

        public static int Interval
        {
            get { return DBConvert.ToIntNull(ConfigurationManager.AppSettings["Interval"]); }
        }

        #endregion

        #region OCRソフトウェア名称
        /// <summary>
        /// OCRソフトウェア名称1
        /// </summary>
        public static string OCR1
        {
            get { return ConfigAccess.Instance.OCR1; }
        }

        /// <summary>
        /// OCRソフトウェア名称2
        /// </summary>
        public static string OCR2
        {
            get { return ConfigAccess.Instance.OCR2; }
        }

        #endregion



        #endregion

        #endregion

        #region 列挙型

        public enum EditType
        {
            NEW, UPDATE, COPY, NONE
        }

        #endregion

        /// <summary>
        /// 特定業務の権限コードを取得する。
        /// </summary>
        /// <param name="gymno"></param>
        /// <param name="reg_value"></param>
        /// <returns></returns>
        public static string OP_AUTH(int gymno, string reg_value, DBManager listDb)
        {
            return listDb.ToIntNull(reg_value).ToString("D8").Substring(gymno - 1, 1);
        }

    }
}
