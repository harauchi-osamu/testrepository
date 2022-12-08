using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using NCR.GeneralMainte;

namespace MainMenu
{
    class InitialParameter : NCR.GeneralMainte.InitialParameter, NCR.GeneralMainte.IInitialParameter
    {
        /// <summary>
        /// マスタの抽出条件
        /// </summary>
        /// <returns></returns>
        public override string GetMasterQueryCondition()
        {
            if (ProcAplInfo.GymNo != 0)
            {
                string XMLFileName = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Condition_" + ProcAplInfo.GymNo.ToString().PadLeft(2, '0') + ".xml");

                //存在確認
                if (File.Exists(XMLFileName))
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ConformanceLevel = ConformanceLevel.Fragment;
                    settings.IgnoreWhitespace = true;
                    settings.IgnoreComments = true;

                    XmlReader xRead = XmlReader.Create(XMLFileName, settings);

                    string curElement = "";

                    while (xRead.Read())
                    {
                        switch (xRead.NodeType)
                        {
                            case XmlNodeType.Element:
                                curElement = xRead.Name;
                                break;
                            case XmlNodeType.Text:
                                if (curElement == ProcAplInfo.TableName)
                                {
                                    return " " + xRead.Value;
                                }
                                break;
                            case XmlNodeType.EndElement:
                                curElement = "";
                                break;
                        }
                    }
                }
            }
            return "" ;
        }       
    }
}
