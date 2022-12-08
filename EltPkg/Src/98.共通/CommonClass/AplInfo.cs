using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using CommonClass.DB;
using CommonTable.DB;

namespace CommonClass
{
    public class AplInfo
    {
        ///// <summary>共通コンフィグ</summary>
        //private static Configuration _config = null;

        static AplInfo()
		{
            // dll.configは廃止
            //AplInfo ai = new AplInfo();
            //string configPath = ai.GetType().Assembly.Location;
            //_config = ConfigurationManager.OpenExeConfiguration(configPath);
        }

        #region メンバー

        private static bool _detaillog = true;
        private static TBL_OPERATION_DATE _gym_opedate;
        private static TBL_GYM_PARAM _gym_param;
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
        /// 端末番号
        /// </summary>
        public static string TermNo
        {
            get { return NCR.Terminal.Number; }
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
        /// フォント名（ラベル）
        /// </summary>
        public static string LabelFontName
        {
            get { return "MS UI Gothic"; }
        }

        /// <summary>
        /// フォント名（テキストボックス）
        /// </summary>
        public static string FontName
        {
            get { return "ＭＳ ゴシック"; }
        }

        #endregion

        #endregion

        #region 業務関連

        /// <summary>
        /// 業務日付
        /// </summary>
        public static TBL_OPERATION_DATE GYM_OPEDATE
        {
            get { return _gym_opedate; }
        }

        /// <summary>
        /// 業務設定
        /// </summary>
        public static TBL_GYM_PARAM GYM_PARAM
        {
            get { return _gym_param; }
        }

        #region オペレータ情報

        /// <summary>
        /// オペレータＩＤ
        /// </summary>
        public static string OP_ID
        {
            //get { return _op_id.PadLeft(8, '0'); }
            get { return NCR.Operator.UserID; }
            //set { _op_id = value; }
        }

        /// <summary>
        /// オペレータ名
        /// </summary>
        public static string OP_NAME
        {
            //get { return _op_name; }
            get { return NCR.Operator.UserName; }
            //set { _op_name = value; }
        }

        /// <summary>
        /// オペレータ権限
        /// </summary>
        public static int OP_PRIV()
        {
            return NCR.Operator.Priviledge;
        }

        /// <summary>
        /// ローマ字使用かどうか
        /// </summary>
        public static bool OP_ROMAN
        {
            //get { return _op_roman; }
            get { return NCR.Operator.Roman == 1 ? true : false; }
            //set { _op_roman = value; }
        }

        #endregion

        #region 業務設定

        /// <summary>
        /// 業務設定を取得
        /// </summary>
        /// <param name="gymid">業務番号</param>
        /// <returns></returns>
        public static bool GetOperationDate()
        {
            try
            {
                // 業務日付取得
                DataRow operow = DBManager.GetOperationDate();
                if (operow == null)
                {
                    return false;
                }
                _gym_opedate = new TBL_OPERATION_DATE(operow);
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
        /// <param name="gymid">業務番号</param>
        /// <returns></returns>
        public static bool GetGymSetting(int gymid, int Schemabankcd)
        {
            try
            {
                // 業務日付取得
                DataRow operow = DBManager.GetOperationDate();
                if (operow == null)
                {
                    return false;
                }
                _gym_opedate = new TBL_OPERATION_DATE(operow);

                // 業務設定取得
                DataRow row = DBManager.GetGymSetMstData(gymid, Schemabankcd);
                if (row == null)
                {
                    return false;
                }
                _gym_param = new TBL_GYM_PARAM(row, Schemabankcd);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 処理日

        /// <summary>
        /// 処理日を返す
        /// </summary>
        /// <returns></returns>
        public static int OpDate()
        {
            if (GYM_OPEDATE == null)
            {
                throw new Exception("処理日取得エラー");
            }
            else
            {
                return GYM_OPEDATE._OPEDATE;
            }
        }

        #endregion

        #region 和暦変換値
        /// <summary>
        /// 和暦変換値
        /// </summary>
        public static string ChangeWarekiValue
        {
            get { return ConfigurationManager.AppSettings["ChangeWarekiValue"]; }
        }
        #endregion

        #endregion

        #region 2重起動チェック

        /// <summary>
        /// 実行ファイルフルパスでの2重起動チェック
        /// </summary>
        /// <remarks>
        /// 自身が標準ユーザの場合
        /// 管理者権限で実行中のプロセスはMainModule取得箇所でエラーとなるため注意
        /// </remarks>
        public static bool IsDoubleStartup()
        {
            try
            {
                System.Diagnostics.Process curp = System.Diagnostics.Process.GetCurrentProcess();
                foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName(curp.ProcessName))
                {
                    if (p.Id != curp.Id)
                    {
                        if (string.Compare(p.MainModule.FileName, curp.MainModule.FileName, true) == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogWriter.writeLog(System.Reflection.MethodBase.GetCurrentMethod(), "二重起動チェック時に[" + ex.GetType().ToString() + " " + ex.Message + "]が発生したため、起動を中止します", 3);
                return true;
            }

            return false;
        }

        #endregion

        #endregion

        #region 列挙型

        public enum EditType
        {
            NEW, UPDATE, COPY, NONE
        }

        #endregion

    }
}
