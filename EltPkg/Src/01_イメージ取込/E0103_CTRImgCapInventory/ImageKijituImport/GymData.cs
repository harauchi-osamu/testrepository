using System;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;

namespace ImageKijituImport
{

    /// <summary>
    /// 業務連携XMLクラス
    /// </summary>
    public class Gymdata
    {
        [XmlElement("batch")] public List<batch> batch { get; set; } = new List<batch>();
        [XmlElement("img")] public List<img> img { get; set; } = new List<img>();
    }

    public class batch
    {
        [XmlElement] public int batch_gymid { get; set; }
        [XmlElement] public string batch_Frontimgname { get; set; }
        [XmlElement] public string batch_Rearimgname { get; set; }
        [XmlElement("ocr")] public List<batchocr> batchocr { get; set; }
    }

    public class batchocr
    {
        [XmlElement] public int batchocr_priority { get; set; }
        [XmlElement] public int batchocr_ocbank { get; set; }
        [XmlElement] public int batchocr_ocbank_level { get; set; }
        [XmlElement] public int batchocr_ocbranch { get; set; }
        [XmlElement] public int batchocr_ocbranch_level { get; set; }
        [XmlElement] public int batchocr_scanbranch { get; set; }
        [XmlElement] public int batchocr_scanbranch_level { get; set; }
        [XmlElement] public int batchocr_batchclearingdate { get; set; }
        [XmlElement] public int batchocr_batchclearingdate_level { get; set; }
        [XmlElement] public int batchocr_scancount { get; set; }
        [XmlElement] public int batchocr_scancount_level { get; set; }
        [XmlElement] public int batchocr_count { get; set; }
        [XmlElement] public int batchocr_count_level { get; set; }
        [XmlElement] public long batchocr_amount { get; set; }
        [XmlElement] public int batchocr_amount_level { get; set; }
    }
    public class img
    {
        [XmlElement] public string img_FrontName { get; set; }
        [XmlElement] public string img_memo { get; set; }
        [XmlElement("ocr")] public List<imgocr> imgocr { get; set; }
    }

    public class imgocr
    {
        [XmlElement] public int ocr_priority { get; set; }
        [XmlElement] public int ocr_icbank { get; set; }
        [XmlElement] public int ocr_icbank_level { get; set; }
        [XmlElement] public long ocr_amount { get; set; }
        [XmlElement] public int ocr_amount_level { get; set; }
        [XmlElement] public int ocr_billdate { get; set; }
        [XmlElement] public int ocr_billdate_level { get; set; }
        [XmlElement] public int ocr_clearingdate { get; set; }
        [XmlElement] public int ocr_clearingdate_level { get; set; }
        [XmlElement] public int ocr_tegatasyurui { get; set; }
        [XmlElement] public int ocr_tegatasyurui_level { get; set; }
        [XmlElement] public int ocr_icbrno { get; set; }
        [XmlElement] public int ocr_icbrno_level { get; set; }
        [XmlElement] public long ocr_account { get; set; }
        [XmlElement] public int ocr_account_level { get; set; }
        [XmlElement] public long ocr_billno { get; set; }
        [XmlElement] public int ocr_billno_level { get; set; }
    }
}
