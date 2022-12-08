using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Reflection;
using Common;

namespace NCR.Reporting
{
    /// <summary>
    /// ＷＭＩ管理クラス
    /// </summary>
    public class WMIManager
    {
        /// <summary>
        /// ＷＭＩプリンターが存在するか（プリンター名指定）
        /// </summary>
        /// <param name="printerName">プリンタ名</param>
        /// <returns>
        /// true  : ＷＭＩプリンターは存在する
        /// false : ＷＭＩプリンターは存在しない
        /// </returns>
        public static bool IsExistPrinter(string printerName)
        {
            try
            {
                System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher("Select * From Win32_Printer");
                System.Management.ManagementObjectCollection moc = mos.Get();

                foreach (System.Management.ManagementObject mo in moc)
                {
                    if (((string)mo["Name"]).ToUpper() == printerName.ToUpper())
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), e.ToString(), 3);
				return false;
            }
        }

        /// <summary>
        /// ＷＭＩプリンターを取得する（プリンター名指定）
        /// </summary>
        /// <param name="printerName">プリンタ名</param>
        /// <param name="printer">Win32_Printerクラス</param>
        /// <returns>
        /// true  : 取得成功
        /// false : 取得失敗
        /// </returns>
        public static bool GetWin32_Printer(string printerName, out Win32_Printer printer)
        {
            printer = null;
            try
            {
                System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher("Select * From Win32_Printer");
                System.Management.ManagementObjectCollection moc = mos.Get();

                foreach (System.Management.ManagementObject mo in moc)
                {
                    if (((string)mo["Name"]).ToUpper() == printerName.ToUpper())
                    {
                        printer = new Win32_Printer(mo);
                        return true;
                    }
                }
                return false;
            }
            catch(Exception e)
            {
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), e.ToString(), 3);
				return false;
            }
        }

        /// <summary>
        /// ＷＭＩプリンターを取得する（全て）
        /// </summary>
        /// <returns>プリンター情報を格納したコレクション</returns>
        public static System.Collections.Hashtable GetWin32_Printers()
        {
            System.Collections.Hashtable printers = new System.Collections.Hashtable();
            try
            {
                System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher("Select * From Win32_Printer");
                System.Management.ManagementObjectCollection moc = mos.Get();

                foreach (System.Management.ManagementObject mo in moc)
                {
                    printers.Add(Convert.ToString(mo["Name"]), new Win32_Printer(mo));
                }
                return printers;
            }
            catch (Exception e)
            {
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), e.ToString(), 3);
				return new System.Collections.Hashtable();
            }
        }

        /// <summary>
        /// ＷＭＩプリントジョブを取得する（プリンター指定）
        /// </summary>
        /// <param name="printerName">プリンタ名</param>
        /// <returns>プリントジョブ情報を格納したコレクション</returns>
        public static System.Collections.Hashtable GetWin32_PrintJobs(string printerName)
        {
            System.Collections.Hashtable jobs = new System.Collections.Hashtable();
            string wkName = "";
            try
            {
                System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher("Select * From Win32_PrintJob");
                System.Management.ManagementObjectCollection moc = mos.Get();

                foreach (System.Management.ManagementObject mo in moc)
                {
                    wkName = Convert.ToString(mo["Name"]);
                    wkName = wkName.Substring(0, wkName.IndexOf(','));

                    if (wkName.ToUpper() == printerName.ToUpper())
                    {
                        jobs.Add(Convert.ToInt32(mo["JobId"]), new Win32_PrintJob(mo));
                    }
                }
                return jobs;
            }
            catch(Exception e)
            {
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), e.ToString(), 3);
				return new System.Collections.Hashtable();
            }
        }

        /// <summary>
        /// デフォルトプリンターを設定する
        /// </summary>
        /// <param name="printerName">プリンタ名</param>
        /// <returns>
        /// true  : 設定成功
        /// false : 設定失敗
        /// </returns>
        public static bool SetDefaultPrinter(string printerName)
        {
            try
            {
                System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher("Select * From Win32_Printer");
                System.Management.ManagementObjectCollection moc = mos.Get();

                foreach (System.Management.ManagementObject mo in moc)
                {
                    if (((string)mo["Name"]).ToUpper() == printerName.ToUpper())
                    {
                        System.Management.ManagementBaseObject mbo = mo.InvokeMethod("SetDefaultPrinter", null, null);
                        if (((uint)mbo["returnValue"]) != 0)
                        {
                            return false;
                        }
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), e.ToString(), 3);
				return false;
            }
        }

        /// <summary>
        /// デフォルトプリンター名を取得する
        /// </summary>
        /// <returns>
        /// printerName=プリンタ名
        /// </returns>
        public static string GetDefaultPrinter()
        {
            string printerName = "";
            try
            {
                System.Management.ManagementObjectSearcher mos = new System.Management.ManagementObjectSearcher("Select * From Win32_Printer");
                System.Management.ManagementObjectCollection moc = mos.Get();

                foreach (System.Management.ManagementObject mo in moc)
                {
                    if ((((uint)mo["Attributes"]) & 4) == 4)
                    {
                        printerName = mo["Name"].ToString();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), e.ToString(), 3);
			}
			return printerName;
        }

    }
}
