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
    public class IFFileWrite
    {
        private string IFFilePath;
        private string WriteSettingPath;

        /// <summary>読み込みデータ</summary>
        public List<IFData> WriteData { get; private set; }

        /// <summary>書き込みエラー</summary>
        public WriteErrorType WriteError { get; private set; }

        #region 書き込みエラー定義
        public enum WriteErrorType
        {
            None = 0,
            Settingllegal = 1,
            KBNIllegal = 2,
            IFFilellegal = 3,
            DataIllegal = 11,
            OtherIllegal = 99,
        }
        #endregion

        #region 書き込み設定定義
        public enum PaddingType
        {
            BackSpace = 1,  // 後スペース埋め
            PreSpace = 2,   // 前スペース埋め
            BackZero = 3,   // 後Zero埋め
            PreZero = 4,    // 前Zero埋め
        }
        #endregion 

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="iffilepath">書き込みIFファイルパス</param>
        /// <param name="settingpath">設定ファイルパス</param>
        /// <param name="WriteData">書き込みデータ</param>
        public IFFileWrite(string iffilepath, string settingpath, List<IFData> WriteData)
        {
            this.IFFilePath = iffilepath;
            this.WriteSettingPath = settingpath;
            this.WriteData = WriteData;
            WriteError = WriteErrorType.None;
        }

        /// <summary>
        /// データ書き込み
        /// </summary>
        public bool IFDataWrite(bool overWrite)
        {
            //初期化
            WriteError = WriteErrorType.None;

            try
            {
                //対象ファイルの存在チェック
                if (!File.Exists(WriteSettingPath)) throw new WriteException(WriteErrorType.Settingllegal, "Load File Not Exists");

                if (!overWrite && !File.Exists(IFFilePath))
                {
                    // overWriteがFalseで上書きは不可
                    throw new WriteException(WriteErrorType.IFFilellegal, "IFFile Exists");
                }

                //IFファイル書き込み設定取得
                IFWriteXMLData XMLData = LoadXmlFile(WriteSettingPath);

                //書き込みデータの作成
                List<string> lines = new List<string>();
                foreach (IFData Data in WriteData)
                {
                    //レコード区分チェック
                    IEnumerable<RecordKBN> KBNData = XMLData.RecordKBN.Where(x => x.KBN == Data.KBN);
                    if (KBNData.Count() != 1) throw new WriteException(WriteErrorType.KBNIllegal, "RecordKBN Illegal");

                    // 対象レコード区分のSize合計の設定枠を作成
                    string WriteLine = new string(' ', KBNData.First().ItemData.Sum(x => x.Size));

                    //各項目の値設定
                    foreach (ItemData Item in KBNData.First().ItemData)
                    {
                        // 対象項目存在チェック
                        if (!Data.LineData.ContainsKey(Item.Name)) throw new WriteException(WriteErrorType.DataIllegal, "DataItem Not Found");

                        // 対象データ取得
                        string ItemData = Data.LineData[Item.Name];

                        //PaddingType制御
                        switch (Item.PaddingType)
                        {
                            case (int)PaddingType.PreZero:
                                // 前Zero埋め
                                ItemData = ItemData.PadLeft(Item.Size, '0');

                                break;
                            case (int)PaddingType.BackZero:
                                // 後Zero埋め
                                ItemData = ItemData.PadRight(Item.Size, '0');

                                break;
                            case (int)PaddingType.PreSpace:
                                // 前スペース埋め
                                ItemData = ItemData.PadLeft(Item.Size);

                                break;
                            case (int)PaddingType.BackSpace:
                                // 後スペース埋め
                                ItemData = ItemData.PadRight(Item.Size);

                                break;
                            default:
                                throw new WriteException(WriteErrorType.Settingllegal, "PaddingType Illegal");
                        }

                        //対象箇所置換
                        WriteLine = WriteLine.Remove(Item.StartPos - 1, Item.Size).Insert(Item.StartPos - 1, ItemData);
                    }

                    // 書き込みデータに追加
                    lines.Add(WriteLine);
                }

                //ファイル書き込み
                using (StreamWriter writer = new StreamWriter(IFFilePath, false, new System.Text.UTF8Encoding(false)))
                {
                    foreach (string line in lines)
                    {
                        writer.WriteLine(line);
                    }
                }

                return true;
            }
            catch (WriteException ex)
            {
                WriteError = ex.WriteError;
                LogWriter.writeLog(System.Reflection.MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
            catch (Exception ex)
            {
                WriteError = WriteErrorType.OtherIllegal;
                LogWriter.writeLog(System.Reflection.MethodBase.GetCurrentMethod(), ex.ToString(), 3);
                return false;
            }
        }

        /// <summary>
        /// IFファイル読込設定の取得
        /// </summary>
        private IFWriteXMLData LoadXmlFile(string FilePath)
        {
            IFWriteXMLData xmlData = new IFWriteXMLData();

            // 対象データの取得
            string xmlText;
            using (StreamReader sr = new StreamReader(FilePath, new System.Text.UTF8Encoding(false)))
            {
                xmlText = sr.ReadToEnd();
            }
            if (string.IsNullOrEmpty(xmlText)) throw new WriteException(WriteErrorType.Settingllegal, "XmlFile NoData");

            XmlSerializer deserializer = new XmlSerializer(typeof(IFWriteXMLData));
            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmlText)))
            {
                xmlData = deserializer.Deserialize(stream) as IFWriteXMLData;
            }

            // 区分の重複チェック
            if (xmlData.RecordKBN.GroupBy(x => x.KBN).Where(x => x.Count() > 1).Count() >= 1)
            {
                throw new WriteException(WriteErrorType.Settingllegal, "RecordKBN Illegal");
            }

            return xmlData;
        }

        #region XMLクラス

        /// <summary>
        /// IFファイル書き込み設定XMLクラス
        /// </summary>
        public class IFWriteXMLData
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
            [XmlAttribute] public int Size { get; set; }
            [XmlAttribute] public int StartPos { get; set; }
            [XmlAttribute] public int PaddingType { get; set; }
        }

        #endregion 

        #region LoadExceptionクラス

        /// <summary>
        /// WriteExceptionクラス
        /// </summary>
        private class WriteException : Exception
        {
            /// <summary>読み込みエラー</summary>
            public WriteErrorType WriteError { get; set; }

            public WriteException(WriteErrorType Type, string message) : base(message)
            {
                WriteError = Type;
            }
            public WriteException(WriteErrorType Type, string message, Exception innerException) : base(message, innerException)
            {
                WriteError = Type;
            }
        }
        #endregion 

    }
}
