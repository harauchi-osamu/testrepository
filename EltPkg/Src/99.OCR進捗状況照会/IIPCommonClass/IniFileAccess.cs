using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using IIPCommonClass.DB;

namespace IIPCommonClass
{
    public class IniFileAccess
    {
        private static string msg;
        private static string msgflg;

        #region 設定

        /// <summary>
        /// キーと値を設定する
        /// </summary>
        /// <param name="iniFileName"></param>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        public static void SetValue(string iniFileName, string sectionName, string keyName, string value)
        {
            IniFileHandler.WritePrivateProfileString(sectionName, keyName, value, iniFileName);
        }

        #endregion

        #region 取得

        /// <summary>
        /// 文字列を読み出す
        /// </summary>
        /// <returns></returns>
        public static string GetKeyByString(string iniFileName, string sectionName, string keyName)
        {
            return GetKeyByString(iniFileName, sectionName, keyName, "");
        }

        /// <summary>
        /// 文字列を読み出す
        /// </summary>
        /// <returns></returns>
        public static string GetKeyByString(string iniFileName, string sectionName, string keyName, string defaultValue)
        {
            try
            {
                StringBuilder sb = new StringBuilder(1024);
                IniFileHandler.GetPrivateProfileString(sectionName, keyName, defaultValue, sb, (uint)sb.Capacity, iniFileName);
                return sb.ToString();
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 整数値を読み出す
        /// </summary>
        /// <returns></returns>
        public static int GetKeyByInt(string iniFileName, string sectionName, string keyName)
        {
            return GetKeyByInt(iniFileName, sectionName, keyName, 0);
        }

        /// <summary>
        /// 整数値を読み出す
        /// </summary>
        /// <returns></returns>
        public static int GetKeyByInt(string iniFileName, string sectionName, string keyName, int defaultValue)
        {
            try
            {
                uint resultValue = IniFileHandler.GetPrivateProfileInt(sectionName, keyName, defaultValue, iniFileName);
                return 99;
                    //dBManager.ToIntNull(resultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 指定セクションのキーの一覧を得る
        /// </summary>
        /// <returns></returns>
        public static string[] GetKeysByArray(string iniFileName, string sectionName)
        {
            return GetKeysByArray(iniFileName, sectionName, "");
        }

        /// <summary>
        /// 指定セクションのキーの一覧を得る
        /// </summary>
        /// <returns></returns>
        public static string[] GetKeysByArray(string iniFileName, string sectionName, string defaultValue)
        {
		    byte [] ar = new byte[1024];
            uint resultSize = IniFileHandler.GetPrivateProfileStringByByteArray(sectionName, null, defaultValue, ar, (uint)ar.Length, iniFileName);
		    string result = System.Text.Encoding.Default.GetString(ar, 0, (int)resultSize-1);
		    string [] keys = result.Split('\0');
            return keys;
        }

        /// <summary>
        /// 指定ファイルのセクションの一覧を得る
        /// </summary>
        /// <returns></returns>
        public static string[] GetSectionsByArray(string iniFileName)
        {
            return GetSectionsByArray(iniFileName, "");
        }

        /// <summary>
        /// 指定ファイルのセクションの一覧を得る
        /// </summary>
        /// <returns></returns>
        public static string[] GetSectionsByArray(string iniFileName, string defaultValue)
        {
            try
            {
                byte[] ar = new byte[1024];
                uint resultSize = IniFileHandler.GetPrivateProfileStringByByteArray(null, null, defaultValue, ar, (uint)ar.Length, iniFileName);
                string result = System.Text.Encoding.Default.GetString(ar, 0, (int)resultSize - 1);
                string[] keys = result.Split('\0');
                return keys;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 削除

        /// <summary>
        /// １つのキーと値のペアを削除する
        /// </summary>
        /// <returns></returns>
        public static void RemoveKey(string iniFileName, string sectionName, string key)
        {
            try
            {
                IniFileHandler.WritePrivateProfileString(sectionName, key, null, iniFileName);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 指定セクション内の全てのキーと値のペアを削除する
        /// </summary>
        /// <returns></returns>
        public static void RemoveSection(string iniFileName, string sectionName)
        {
            try
            {
                IniFileHandler.WritePrivateProfileString(sectionName, null, null, iniFileName);
            }
            catch
            {
            }
        }

        #endregion

        #region メッセージ表示

        /// <summary>
        /// メッセージのINIファイルからの取得と表示
        /// </summary>
        /// <param name="msgno">メッセージ番号</param>
        /// <param name="rep1">置換用文字列1</param>
        /// <returns>メッセージへの応答結果</returns>
        public static DialogResult ShowMsgBox(string msgno)
        {
            return ShowMsgBox(msgno, "", "", "", "");
        }

        /// <summary>
        /// メッセージのINIファイルからの取得と表示
        /// </summary>
        /// <param name="msgno">メッセージ番号</param>
        /// <param name="rep1">置換用文字列1</param>
        /// <returns>メッセージへの応答結果</returns>
        public static DialogResult ShowMsgBox(string msgno, object rep1)
        {
            return ShowMsgBox(msgno, rep1.ToString(), "", "", "");
        }

        /// <summary>
        /// メッセージのINIファイルからの取得と表示
        /// </summary>
        /// <param name="msgno">メッセージ番号</param>
        /// <param name="rep1">置換用文字列1</param>
        /// <param name="rep2">置換用文字列2</param>
        /// <returns>メッセージへの応答結果</returns>
        public static DialogResult ShowMsgBox(string msgno, object rep1, object rep2)
        {
            return ShowMsgBox(msgno, rep1.ToString(), rep2.ToString(), "", "");
        }

        /// <summary>
        /// メッセージのINIファイルからの取得と表示
        /// </summary>
        /// <param name="msgno">メッセージ番号</param>
        /// <param name="rep1">置換用文字列1</param>
        /// <param name="rep2">置換用文字列2</param>
        /// <param name="rep3">置換用文字列3</param>
        /// <returns>メッセージへの応答結果</returns>
        public static DialogResult ShowMsgBox(string msgno, object rep1, object rep2, object rep3)
        {
            return ShowMsgBox(msgno, rep1.ToString(), rep2.ToString(), rep3.ToString(), "");
        }

        /// <summary>
        /// メッセージのINIファイルからの取得と表示
        /// </summary>
        /// <param name="msgno">メッセージ番号</param>
        /// <param name="rep1">置換用文字列1</param>
        /// <param name="rep2">置換用文字列2</param>
        /// <param name="rep3">置換用文字列3</param>
        /// <param name="rep4">置換用文字列4</param>
        /// <returns>メッセージへの応答結果</returns>
        public static DialogResult ShowMsgBox(string msgno, object rep1, object rep2, object rep3, object rep4)
        {
            return ShowMsgBox(msgno, rep1.ToString(), rep2.ToString(), rep3.ToString(), rep4.ToString());
        }

        /// <summary>
        /// メッセージのINIファイルからの取得と表示
        /// </summary>
        /// <param name="msgno">メッセージ番号</param>
        /// <param name="rep1">置換用文字列1</param>
        /// <param name="rep2">置換用文字列2</param>
        /// <param name="rep3">置換用文字列3</param>
        /// <returns>メッセージへの応答結果</returns>
        public static DialogResult ShowMsgBox(string msgno, string rep1, string rep2, string rep3, string rep4)
        {
            DialogResult dr = DialogResult.None;

            try
            {
                GetMsg(msgno, rep1, rep2, rep3, rep4);
            }
            catch
            {
            }

            if (msg.Equals(""))
            {
                MessageBox.Show("メッセージ" + msgno + "を取得できません", "メッセージ取得失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dr;
            }

            switch (msgflg)
            {
                case "E":
                    dr = MessageBox.Show(msg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "W":
                    dr = MessageBox.Show(msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case "I":
                    dr = MessageBox.Show(msg, "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "Q":
                    dr = MessageBox.Show(msg, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
                case "L":
                    dr = MessageBox.Show(msg, "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    break;
                default:
                    dr = MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.None);
                    break;
            }

            return dr;
        }

        private static void GetMsg(string msgno, string rep1, string rep2, string rep3, string rep4)
        {
            msg = GetKeyByString(AplInfo.MsgIni, "Msg", msgno);

            if (!msg.Equals(""))
            {
                msgflg = (msg.Split(','))[0];
                msg = (msg.Split(','))[1];

                // iniファイル中の"\n"はエスケープされて"\\n"として入ってくる
                msg = msg.Replace("\\n", System.Environment.NewLine);
                if (!rep1.Equals("")) { msg = msg.Replace("{1}", rep1); }
                if (!rep2.Equals("")) { msg = msg.Replace("{2}", rep2); }
                if (!rep3.Equals("")) { msg = msg.Replace("{3}", rep3); }
                if (!rep4.Equals("")) { msg = msg.Replace("{4}", rep4); }
            }

            return;
        }

        #endregion

        #region ステータスバー用メッセージ取得

        /// <summary>
        /// メッセージのINIファイルからの取得（改行文字消去）
        /// </summary>
        /// <param name="msgno">メッセージ番号</param>
        /// <param name="rep1">置換用文字列1</param>
        /// <param name="rep2">置換用文字列2</param>
        /// <param name="rep3">置換用文字列3</param>
        /// <returns>メッセージ文字列</returns>
        public static string GetStatusMsg(string msgno, string rep1, string rep2, string rep3)
        {
            msg = GetKeyByString(AplInfo.MsgIni, "Msg", msgno);

            if (!msg.Equals(""))
            {
                msgflg = (msg.Split(','))[0];
                msg = (msg.Split(','))[1];

                // iniファイル中の"\n"はエスケープされて"\\n"として入ってくる
                msg = msg.Replace("\\n", "");
                if (!rep1.Equals("")) { msg = msg.Replace("{1}", rep1); }
                if (!rep2.Equals("")) { msg = msg.Replace("{2}", rep2); }
                if (!rep3.Equals("")) { msg = msg.Replace("{3}", rep3); }
            }

            return msg;
        }

        /// <summary>
        /// メッセージのINIファイルからの取得（改行文字消去）
        /// </summary>
        /// <param name="msgno">メッセージ番号</param>
        /// <param name="rep1">置換用文字列1</param>
        /// <param name="rep2">置換用文字列2</param>
        /// <param name="rep3">置換用文字列3</param>
        /// <returns>メッセージ文字列</returns>
        public static string GetStatusMsg(string msgno, object rep1, object rep2, object rep3)
        {
            return GetStatusMsg(msgno, rep1.ToString(), rep2.ToString(), rep3.ToString());
        }

        /// <summary>
        /// メッセージのINIファイルからの取得（改行文字消去）
        /// </summary>
        /// <param name="msgno">メッセージ番号</param>
        /// <param name="rep1">置換用文字列1</param>
        /// <param name="rep2">置換用文字列2</param>
        /// <returns>メッセージ文字列</returns>
        public static string GetStatusMsg(string msgno, object rep1, object rep2)
        {
            return GetStatusMsg(msgno, rep1.ToString(), rep2.ToString(), "");
        }

        /// <summary>
        /// メッセージのINIファイルからの取得（改行文字消去）
        /// </summary>
        /// <param name="msgno">メッセージ番号</param>
        /// <param name="rep1">置換用文字列1</param>
        /// <returns>メッセージ文字列</returns>
        public static string GetStatusMsg(string msgno, object rep1)
        {
            return GetStatusMsg(msgno, rep1.ToString(), "", "");
        }

        /// <summary>
        /// メッセージのINIファイルからの取得（改行文字消去）
        /// </summary>
        /// <param name="msgno">メッセージ番号</param>
        /// <returns>メッセージ文字列</returns>
        public static string GetStatusMsg(string msgno)
        {
            return GetStatusMsg(msgno, "", "", "");
        }

        #endregion
    }
}
