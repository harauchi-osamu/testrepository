using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR
{
    // *******************************************************************
    // HULFT関連の個別定義(セクション別の定義)
    // *******************************************************************
    internal class Hulft
    {
        internal static string IniPath { get; set; }

        /// <summary>HULFTLogセクション名</summary>
        internal static string HULFTLogSectionName { get; set; } = string.Empty;

        static Hulft()
        {
        }

        // *******************************************************************
        // HULFT
        // *******************************************************************
        /// <summary>
        /// HULFT関連電子交換パッケージプログラムログフォルダ（他端末参照用）
        /// </summary>
        internal static string HulftEXELogRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, HULFTLogSectionName, "HulftEXELogRoot"); }
        }
        /// <summary>
        /// HULFT配信フォルダ（他端末参照用）
        /// </summary>
        internal static string SendRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, HULFTLogSectionName, "SendRoot"); }
        }
        /// <summary>
        /// HULFT集信フォルダ（他端末参照用）
        /// </summary>
        internal static string ReceiveRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, HULFTLogSectionName, "ReceiveRoot"); }
        }
        /// <summary>
        /// HULFT集信退避フォルダ
        /// </summary>
        internal static string BackupRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, HULFTLogSectionName, "BackupRoot"); }
        }
        /// <summary>
        /// HULFT集配信ログフォルダ
        /// </summary>
        internal static string LogRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, HULFTLogSectionName, "LogRoot"); }
        }
        /// <summary>
        /// HULFT集配信エラーログフォルダ
        /// </summary>
        internal static string LogErrRoot
        {
            get { return IniFileAccess.GetKeyByString(IniPath, HULFTLogSectionName, "LogErrRoot"); }
        }
        /// <summary>
        /// HULFT集信ログファイル名
        /// </summary>
        internal static string LogRecvFileName
        {
            get { return IniFileAccess.GetKeyByString(IniPath, HULFTLogSectionName, "LogRecvFileName"); }
        }
        /// <summary>
        /// HULFT集信エラーログファイル名
        /// </summary>
        internal static string LogErrRecvFileName
        {
            get { return IniFileAccess.GetKeyByString(IniPath, HULFTLogSectionName, "LogErrRecvFileName"); }
        }
        /// <summary>
        /// HULFT配信ログファイル名
        /// </summary>
        internal static string LogSendFileName
        {
            get { return IniFileAccess.GetKeyByString(IniPath, HULFTLogSectionName, "LogSendFileName"); }
        }
        /// <summary>
        /// HULFT配信エラーログファイル名
        /// </summary>
        internal static string LogErrSendFileName
        {
            get { return IniFileAccess.GetKeyByString(IniPath, HULFTLogSectionName, "LogErrSendFileName"); }
        }
    }
}
