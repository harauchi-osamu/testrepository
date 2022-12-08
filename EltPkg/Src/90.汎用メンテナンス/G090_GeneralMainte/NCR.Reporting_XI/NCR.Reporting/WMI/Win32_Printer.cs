using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Reflection;
using Common;

namespace NCR.Reporting
{
    /// <summary>
    /// ＷＭＩプリンター情報を格納するクラス
    /// </summary>
    public class Win32_Printer
    {
        //class Win32_Printer : CIM_Printer
        //{
        //  uint32 Attributes;
        //  uint16 Availability;
        //  string AvailableJobSheets[];
        //  uint32 AveragePagesPerMinute;
        //  uint16 Capabilities[];
        //  string CapabilityDescriptions[];
        //  string Caption;
        //  string CharSetsSupported[];
        //  string Comment;
        //  uint32 ConfigManagerErrorCode;
        //  boolean ConfigManagerUserConfig;
        //  string CreationClassName;
        //  uint16 CurrentCapabilities[];
        //  string CurrentCharSet;
        //  uint16 CurrentLanguage;
        //  string CurrentMimeType;
        //  string CurrentNaturalLanguage;
        //  string CurrentPaperType;
        //  boolean Default;
        //  uint16 DefaultCapabilities[];
        //  uint32 DefaultCopies;
        //  uint16 DefaultLanguage;
        //  string DefaultMimeType;
        //  uint32 DefaultNumberUp;
        //  string DefaultPaperType;
        //  uint32 DefaultPriority;
        //  string Description;
        //  uint16 DetectedErrorState;
        //  string DeviceID;
        //  boolean Direct;
        //  boolean DoCompleteFirst;
        //  string DriverName;
        //  boolean EnableBIDI;
        //  boolean EnableDevQueryPrint;
        //  boolean ErrorCleared;
        //  string ErrorDescription;
        //  string ErrorInformation[];
        //  uint16 ExtendedDetectedErrorState;
        //  uint16 ExtendedPrinterStatus;
        //  boolean Hidden;
        //  uint32 HorizontalResolution;
        //  datetime InstallDate;
        //  uint32 JobCountSinceLastReset;
        //  boolean KeepPrintedJobs;
        //  uint16 LanguagesSupported[];
        //  uint32 LastErrorCode;
        //  boolean Local;
        //  string Location;
        //  uint16 MarkingTechnology;
        //  uint32 MaxCopies;
        //  uint32 MaxNumberUp;
        //  uint32 MaxSizeSupported;
        //  string MimeTypesSupported[];
        //  string Name;
        //  string NaturalLanguagesSupported[];
        //  boolean Network;
        //  uint16 PaperSizesSupported[];
        //  string PaperTypesAvailable[];
        //  string Parameters;
        //  string PNPDeviceID;
        //  string PortName;
        //  uint16 PowerManagementCapabilities[];
        //  boolean PowerManagementSupported;
        //  string PrinterPaperNames[];
        //  uint32 PrinterState;
        //  uint16 PrinterStatus;
        //  string PrintJobDataType;
        //  string PrintProcessor;
        //  uint32 Priority;
        //  boolean Published;
        //  boolean Queued;
        //  boolean RawOnly;
        //  string SeparatorFile;
        //  string ServerName;
        //  boolean Shared;
        //  string ShareName;
        //  boolean SpoolEnabled;
        //  datetime StartTime;
        //  string Status;
        //  uint16 StatusInfo;
        //  string SystemCreationClassName;
        //  string SystemName;
        //  datetime TimeOfLastReset;
        //  datetime UntilTime;
        //  uint32 VerticalResolution;
        //  boolean WorkOffline;
        //};
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mo">ＷＭＩインスタンス</param>
        public Win32_Printer(System.Management.ManagementObject mo)
        {
            m_MO = mo;
        }

        private System.Management.ManagementObject m_MO;

        #region プロパティ
        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get
            {
                return getMOValue("Name");
            }
        }
        /// <summary>
        /// DetectedErrorState
        /// </summary>
        public UInt16 DetectedErrorState
        {
            get
            {
                return Convert.ToUInt16(getMOValue("DetectedErrorState"));
            }
        }
        /// <summary>
        /// Status
        /// </summary>
        public string Status
        {
            get
            {
                return getMOValue("Status");
            }
        }

        public const string ERRORSTATE_NOERROR = "準備完了";
        public const string ERRORSTATE_NOPAPER = "用紙切れ";
        public const string ERRORSTATE_NOTONNER = "トナー切れ";
        public const string ERRORSTATE_OFFLINE = "オフライン";
        public const string ERRORSTATE_ETC = "その他";

        public const string STATUS_OK = "OK";
        public const string STATUS_ERROR = "Error";

        #endregion

        #region パブリックメソッド
        /// <summary>
        /// エラー状態かどうかを示す値を取得する
        /// </summary>
        /// <returns>
        /// true  : エラー
        /// false : エラーではない
        /// </returns>
        public bool IsError()
        {
            string errorState = "";
            if (this.Status == STATUS_OK)
            {
                errorState = this.GetDispDetectedErrorState();
                if (errorState == ERRORSTATE_NOERROR)
                {
                    // エラーではない
                    return false;
                }
                return true;
            }
            else
            {
                errorState = this.GetDispDetectedErrorState();
                if (errorState == ERRORSTATE_NOERROR)
                {
                    // エラーではない
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// エラー状態を取得する
        /// </summary>
        /// <returns>エラー状態</returns>
        public string GetDispDetectedErrorState()
        {
            string error = "";
            switch (Convert.ToUInt32(this.DetectedErrorState))
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 6:
                case 8:
                    error = ERRORSTATE_NOERROR;
                    break;
                case 5:
                    //20100325 機種によっては想定どおりのエラーにならない為、NERRORとする
                    //error = ERRORSTATE_NOPAPER;
                    error = ERRORSTATE_NOERROR;
                    break;
                case 7:
                    //20100325 機種によっては想定どおりのエラーにならない為、NERRORとする
                    //error = ERRORSTATE_NOTONNER;
                    error = ERRORSTATE_NOERROR;
                    break;
                case 9:
                    //20100325 機種によっては想定どおりのエラーにならない為、NERRORとする
                    //error = ERRORSTATE_OFFLINE;
                    error = ERRORSTATE_NOERROR;
                    break;
                default:    // その他
                    error = ERRORSTATE_ETC;
                    break;
            }

            return error;
        }

        /// <summary>
        /// 全てのプリントジョブをキャンセルする
        /// </summary>
        /// <returns>
        /// true  : キャンセル成功
        /// false : キャンセル失敗
        /// </returns>
        public bool CancelAllJobs()
        {
            try
            {
                System.Management.ManagementBaseObject mbo = this.m_MO.InvokeMethod("CancelAllJobs", null, null);

                if (((uint)mbo["returnValue"]) != 0)
                {
					LogWriter.writeLog(MethodBase.GetCurrentMethod(), "プリントジョブ削除エラー\nプリンター名=[" + this.Name + "]", 2);
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
				LogWriter.writeLog(MethodBase.GetCurrentMethod(), e.ToString(), 3);
				return false;
            }
        }

        #endregion

        #region プライベートメソッド

        /// <summary>
        /// ManagementObjectよりデータ取得
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private string getMOValue(string propertyName)
        {
            try
            {
                return Convert.ToString(m_MO[propertyName]);
            }
            catch
            {
                return "";
            }
        }

        #endregion

    }
}
