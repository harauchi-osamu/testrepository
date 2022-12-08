using System.Diagnostics;
using System.Collections;
using System.Configuration;
using System.Reflection;
using Common;

namespace NCR.GeneralMainte
{
    /// <summary>
    /// アプリケーション情報
    /// </summary>
    public class ProcAplInfo
    {
		private static string _op_priv_nm = "";
		private static bool _op_roman = true;
		private static System.Collections.SortedList m_Args;    //引数コレクション
		private static string m_OutReportPath;                  //レポート保存パス

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public ProcAplInfo(string[] args)
        {
            m_Args = new System.Collections.SortedList();
            for (int i = 0; i < args.Length; i++)
            {
                m_Args.Add(i, args[i]);
            }
        }

        /// <summary>
        /// アプリケーション引数チェック
        /// </summary>
        /// <returns></returns>
        public bool checkArgs()
        {
            // 引数の数をチェック
            if (ProcAplInfo.Args.Count < 2)
            {
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), "引数個数チェックエラー", 2);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 引数
        /// </summary>
        /// <param name="index">引数番号（１～）</param>
        /// <returns></returns>
        public static string Arg(int index)
        {
            if (m_Args.Contains(index-1))
            {
                return (string)m_Args[index-1];
            }
            else
            {
                return "";
            }
        }

		public static SortedList Args
		{
			get { return m_Args; }
		}

		/// <summary>
		/// テーブル名
		/// </summary>
		public static string TableName
        {
            get { return Arg(1); }
        }

        /// <summary>
        /// スキーマ名
        /// </summary>
        public static string Schema
        {
            get { return Arg(2); }
        }

        /// <summary>
        /// 参照モード（第3引数指定時）
        /// </summary>
        public static bool IsReadOnly
        {
            get { return (m_Args.Count > 2); }
        }

        /// <summary>
        /// 追加ボタン表示フラグ
        /// </summary>
        public static bool IsAdd
        {
            get
            {
                if (Arg(3) == "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 変更ボタン表示フラグ
        /// </summary>
        public static bool IsEdit
        {
            get
            {
                if (Arg(4) == "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 削除ボタン表示フラグ
        /// </summary>
        public static bool IsDel
        {
            get
            {
                if (Arg(5) == "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 業務番号
        /// </summary>
        public static int GymNo
        {
            get
            {
                if (Arg(6) == "")
                {
                    return 0;
                }
                else
                {
                    return int.Parse(Arg(6));
                }
            }
        }

        /// <summary>
        /// プロセスＩＤ
        /// </summary>
        public static int ProcessID
        {
            get { return Process.GetCurrentProcess().Id; }
        }

        /// <summary>
        /// レポート保存パス
        /// </summary>
        public static string OutReportPath
        {
            get { return m_OutReportPath; }
            set { m_OutReportPath = value; }
        }

        /// <summary>
        /// ログファイルのパス
        /// </summary>
        public static string LogFolderPath
        {
            get { return ConfigurationManager.AppSettings["LogFolderPath"]; }
        }

        /// <summary>
        /// オペレータファイルのパス
        /// </summary>
        public static string OpeFilePath
        {
            get { return ConfigurationManager.AppSettings["OpeFilePath"]; }
        }

        /// <summary>
        /// INIファイル情報
        /// </summary>
        public static string TermIni
        {
            get { return ConfigurationManager.AppSettings["TermIniINI"]; }
        }

        /// <summary>
        /// 端末番号
        /// コンピューター名のなかの'0'以降の文字を返す
        /// </summary>
        public static string TermNo
        {
            //get { return _term_no; }
            //set { _term_no = value; }
            get { return Terminal.Number; }
        }

        /// <summary>
        /// オペレータＩＤ
        /// </summary>
        public static string OP_ID
        {
            //get { return _op_id.PadLeft(8, '0'); }
            //set { _op_id = value; }
            get { return Operator.UserID; }
        }

        /// <summary>
        /// オペレータ名
        /// </summary>
        public static string OP_NAME
        {
            //get { return _op_name; }
            //set { _op_name = value; }
            get { return Operator.UserName; }
        }

        /// <summary>
        /// オペレータ権限レベル
        /// </summary>
        public static string OP_PRIV
        {
            //get { return _op_priv; }
            //set
            //{
            //    _op_priv = value;
            //}
            get
            {
                return NCR.Operator.Priviledge.ToString();
            }
        }

        /// <summary>
        /// オペレータ権限名称(OPEL, MANAGER, SYSMANAGER, NULL)
        /// </summary>
        public static string OP_PRIV_NM
        {
            get { return _op_priv_nm; }
            set
            {
                // ActiveDirectoryの権限体系から汎用エントリ系の権限体系への変換
                switch (value)
                {
                    case "0":
                        _op_priv_nm = "NULL";
                        break;
                    case "1":
                        _op_priv_nm = "SYSMANAGER";
                        break;
                    case "2":
                        _op_priv_nm = "MANAGER";
                        break;
                    case "3":
                        _op_priv_nm = "OPEL";
                        break;
                    default:
                        _op_priv_nm = "";
                        break;
                }
            }
        }

        /// <summary>
        /// ローマ字使用かどうか
        /// </summary>
        public static bool OP_ROMAN
        {
            get { return _op_roman; }
            set { _op_roman = value; }
        }




    }
}
