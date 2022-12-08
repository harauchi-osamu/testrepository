using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using Common;

namespace IFFileOperation
{
    /// <summary>
    /// IFファイル読込クラス
    /// </summary>
    public class IFFileDataLoad
    {
        private string IFFilePath;
        private string LoadSettingPath;

        /// <summary>読み込みデータ</summary>
        public List<IFData> LoadData { get; private set; }

        /// <summary>読み込みエラー</summary>
        public LoadErrorType LoadError { get; private set; }

        #region 読み込みエラー定義
        public enum LoadErrorType
        {
            None = 0,
            Settingllegal = 1,
            KBNIllegal = 2,
            DataIllegal = 11,
            OtherIllegal = 99,
        }
        #endregion 

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="iffilepath">読み込みIFファイルパス</param>
        /// <param name="settingpath">設定ファイルパス</param>
        public IFFileDataLoad(string iffilepath, string settingpath)
        {
            IFFilePath = iffilepath;
            LoadSettingPath = settingpath;
            LoadData = new List<IFData>();
            LoadError = LoadErrorType.None;
        }

        /// <summary>
        /// ファイルサイズチェック
        /// </summary>
        public bool ChkFileSize(long size)
        {
            if ((new FileInfo(IFFilePath)).Length % size != 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// データ読み込み
        /// </summary>
        public bool IFDataLoad()
        {
            //初期化
            LoadData = new List<IFData>();
            LoadError = LoadErrorType.None;

            try
            {
                //対象ファイルの存在チェック
                if (!File.Exists(IFFilePath) || !File.Exists(LoadSettingPath)) throw new LoadException(LoadErrorType.Settingllegal, "Load File Not Exists");

                //IFファイル読込設定取得
                IFLoadXMLData XMLData = LoadXmlFile(LoadSettingPath);

                //IFデータの取得
                IEnumerable<string> lines = File.ReadLines(IFFilePath, new System.Text.UTF8Encoding(false));
                foreach (string FilelineData in lines)
                {
                    if (string.IsNullOrEmpty(FilelineData)) continue;

                    // 先頭文字チェック
                    IEnumerable<RecordKBN> KBNData = XMLData.RecordKBN.Where(x => x.KBN == FilelineData.First().ToString());
                    if (KBNData.Count() != 1) throw new LoadException(LoadErrorType.KBNIllegal, "RecordKBN Illegal");

                    Dictionary<string, string> IFLineData = new Dictionary<string, string>();
                    foreach (ItemData Item in KBNData.First().ItemData)
                    {
                        string CutData = FilelineData.Substring(Item.StartPos - 1, Item.Size);
                        if (Item.Attr == "N" && (CutData != new string('Z', Item.Size) && !long.TryParse(CutData, out long chgData)))
                        {
                            // 属性"N"で「全て"Z"」以外の場合、数値変換できないとエラー
                            throw new LoadException(LoadErrorType.DataIllegal, "Data Illegal");
                        }

                        //対象箇所登録
                        IFLineData.Add(Item.Name, CutData);
                    }

                    // 行データ登録
                    LoadData.Add(new IFData(KBNData.First().KBN, IFLineData));
                }

                return true;
            }
            catch (LoadException ex)
            {
                LoadError = ex.LoadError;
                LogWriter.writeLog(System.Reflection.MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            catch (Exception ex)
            {
                LoadError = LoadErrorType.OtherIllegal;
                LogWriter.writeLog(System.Reflection.MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
        }

        /// <summary>
        /// IFファイル読込設定の取得
        /// </summary>
        private IFLoadXMLData LoadXmlFile(string FilePath)
        {
            IFLoadXMLData xmlData = new IFLoadXMLData();

            // 対象データの取得
            string xmlText;
            using (StreamReader sr = new StreamReader(FilePath, new System.Text.UTF8Encoding(false)))
            {
                xmlText = sr.ReadToEnd();
            }
            if (string.IsNullOrEmpty(xmlText)) throw new LoadException(LoadErrorType.Settingllegal, "XmlFile NoData");

            XmlSerializer deserializer = new XmlSerializer(typeof(IFLoadXMLData));
            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmlText)))
            {
                xmlData = deserializer.Deserialize(stream) as IFLoadXMLData;
            }

            // 区分の重複チェック
            if (xmlData.RecordKBN.GroupBy(x => x.KBN).Where(x => x.Count() > 1).Count() >= 1)
            {
                throw new LoadException(LoadErrorType.Settingllegal, "RecordKBN Illegal");
            } 

            return xmlData;
        }

        #region XMLクラス

        /// <summary>
        /// IFファイル読込設定XMLクラス
        /// </summary>
        public class IFLoadXMLData
        {
            [XmlElement("RecordKBN")] public List<RecordKBN> RecordKBN { get; set; }
        }
        public class RecordKBN
        {
            [XmlAttribute] public string KBN { get; set; }
            [XmlElement("Item")] public List<ItemData> ItemData { get; set; }
        }

        public class ItemData
        {
            [XmlAttribute] public string Name { get; set; }
            [XmlAttribute] public string Attr { get; set; }
            [XmlAttribute] public int Size { get; set; }
            [XmlAttribute] public int StartPos { get; set; }
        }

        #endregion 

        #region LoadExceptionクラス

        /// <summary>
        /// LoadExceptionクラス
        /// </summary>
        private class LoadException : Exception 
        {
            /// <summary>読み込みエラー</summary>
            public LoadErrorType LoadError { get; set; }

            public LoadException(LoadErrorType Type, string message) : base(message)
            {
                LoadError = Type;
            }
            public LoadException(LoadErrorType Type, string message, Exception innerException) : base(message, innerException)
            {
                LoadError = Type;
            }
        }
        #endregion 

    }
}
