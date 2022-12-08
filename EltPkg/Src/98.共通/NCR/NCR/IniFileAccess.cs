using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NCR
{
    public class IniFileAccess
    {
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
                return Convert.ToInt32(resultValue);
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
    }
}
