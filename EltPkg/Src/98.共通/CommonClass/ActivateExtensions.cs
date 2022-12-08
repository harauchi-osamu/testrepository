using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonClass
{
    public static class ActivateExtensions
    {
        /// <summary>
        /// 処理中のときはtrue
        /// </summary>
        private static bool isProcessing = false;

        /// <summary>
        /// 処理中かどうかを検出できるようにする
        /// </summary>
        /// <param name="self"></param>
        public static void ActivateProcessingDetection(this Form self)
        {
            Application.Idle += Application_Idle;

            // イベントハンドラは自動解除されないので、Close時に解除する。
            self.FormClosed += ActivateProcessing_FormClosed;
        }

        /// <summary>
        /// Application.Idleイベント
        /// </summary>
        static void Application_Idle(object sender, EventArgs e)
        {
            // Idle状態になったらfalse設定
            isProcessing = false;
        }

        /// <summary>
        /// FormClosedイベント
        /// </summary>
        static void ActivateProcessing_FormClosed(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 0)
            {
                // 開いているフォームがない場合はイベント解除
                Application.Idle -= Application_Idle;
            }
        }

        /// <summary>
        /// 処理が実行中かどうかを取得
        /// </summary>
        /// <param name="self"></param>
        /// <returns>処理が実行中のときはtrue、実行されていないときはfalse。</returns>
        public static bool IsProcessing(this Form self)
        {
            if (isProcessing) return true;
            // 処理中設定
            isProcessing = true;
            return false;
        }
    }

}
