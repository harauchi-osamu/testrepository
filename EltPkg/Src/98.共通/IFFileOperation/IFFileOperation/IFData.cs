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
    /// IFDataクラス
    /// </summary>
    public class IFData
    {
        /// <summary>レコード区分</summary>
        public string KBN { get; set; }

        public Dictionary<string, string> LineData { get; set; }

        private IFData()
        {
        }

        public IFData(string kbn, Dictionary<string, string> data)
        {
            KBN = kbn;
            LineData = data;
        }
    }
}
