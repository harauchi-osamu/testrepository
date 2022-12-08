using System;
using System.Windows.Forms;
using System.Reflection;
using Common;

namespace NCR.GeneralMainte
{
    public class AppController
    {
		protected ProcAplInfo m_AplInfo;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AppController()
        {
        }

        /// <summary>
        /// コントローラ開始
        /// </summary>
        /// <returns></returns>
        public virtual bool Start(string[] args)
        {
            try
            {
                // アプリケーション情報初期化
                this.m_AplInfo = new ProcAplInfo(args);

                // 引数チェック
                if (!this.m_AplInfo.checkArgs())
                {
					LogWriter.writeLog(MethodBase.GetCurrentMethod(), "引数チェックエラー", 2);
                    return false;
                }

                if (ProcAplInfo.OP_ID.Equals("") || ProcAplInfo.OP_NAME.Equals(""))
                {
                    MessageBox.Show("オペレータ情報の取得に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
					LogWriter.writeLog(MethodBase.GetCurrentMethod(), "オペレータ情報の取得に失敗\nOP_ID:"
						+ ProcAplInfo.OP_ID + ", OP_NAME:" + ProcAplInfo.OP_NAME, 3);
					return false;
                }
                return true;
            }
            catch (Exception ex)
            {
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), "コントローラ開始エラー: " + ex.ToString(), 3);
                return false;
            }
        }

        /// <summary>
        /// コントローラ停止
        /// </summary>
        /// <returns></returns>
        public virtual bool Stop()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), "コントローラ停止エラー: " + ex.ToString(), 3);
                return false;
            }
        }

    }
}
