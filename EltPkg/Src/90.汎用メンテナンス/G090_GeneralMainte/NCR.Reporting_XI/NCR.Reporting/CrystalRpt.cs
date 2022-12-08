using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using CommonClass;

namespace NCR.Reporting
{
    /// <summary>
    /// クリスタルレポート管理クラス
    /// </summary>
    public class CrystalRpt : IAPReport
    {

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CrystalRpt()
        {
            this.m_ReportDocment = new ReportDocument();
        }

        #endregion

        #region メンバ

        protected CrystalDecisions.CrystalReports.Engine.ReportDocument m_ReportDocment;
        protected string m_ReportID = "";
        protected string m_FileName = "";
        protected string m_OutFileName = "";

        #endregion

        #region プロパティ

        /// <summary>
        /// レポートタイプ
        /// </summary>
        public string Type
        {
            get { return "rpt"; }
        }

        /// <summary>
        /// レポートドキュメント
        /// </summary>
        public ReportDocument theReportDocument
        {
            get { return this.m_ReportDocment; }
        }

        /// <summary>
        /// レポートＩＤ
        /// </summary>
        public string ReportID
        {
            get { return this.m_ReportID; }
        }

        /// <summary>
        /// ファイル名（ﾌﾙﾊﾟｽ）
        /// </summary>
        public string FileName
        {
            get { return this.m_FileName; }
        }

        /// <summary>
        /// 出力ファイル名（ﾌﾙﾊﾟｽ）
        /// </summary>
        public string OutFileName
        {
            get { return this.m_OutFileName; }
        }

        #endregion

        #region 初期化

        /// <summary>
        /// レポートを初期化します。（共通）
        /// </summary>
        /// <param name="fileName">レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        public bool Initialize(string fileName)
        {
            return this.Initialize(fileName, "");
        }

        /// <summary>
        /// レポートを初期化します。（共通）
        /// </summary>
        /// <param name="fileName">レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        /// <param name="outFileName">出力レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        public bool Initialize(string fileName, string outFileName)
        {
            // レポートＩＤの設定
            string rptID = System.IO.Path.GetFileNameWithoutExtension(fileName);
            if (rptID.Length > 11)
            {
                this.m_ReportID = rptID.Substring(0, 11);
            }
            else
            {
                this.m_ReportID = rptID;
            }

            // 入力(出力)rptファイル名の設定
            this.m_FileName = fileName;

            // 出力rptファイル名の設定
            this.m_OutFileName = outFileName.Trim();

            //レポートファイルをロード
            this.m_ReportDocment.Load(fileName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByTempCopy);

            return true;
        }

        /// <summary>
        /// レポートを初期化します。（DB使用）
        /// </summary>
        /// <param name="server">接続サーバー</param>
        /// <param name="user">接続ユーザー</param>
        /// <param name="password">接続パスワード</param>
        /// <param name="fileName">レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        public bool InitializeForDB(string server, string user, string password, string fileName)
        {
            return this.InitializeForDB(server, user, password, fileName, "");
        }

        /// <summary>
        /// レポートを初期化します。（DB使用）
        /// </summary>
        /// <param name="server">接続サーバー</param>
        /// <param name="user">接続ユーザー</param>
        /// <param name="password">接続パスワード</param>
        /// <param name="fileName">レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        /// <param name="outFileName">出力レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        public bool InitializeForDB(string server, string user, string password, string fileName, string outFileName)
        {
            //初期化（共通）
            this.Initialize(fileName, outFileName);

            //接続情報の設定
            if (server != "" && user != "")
            {
                for (int i = 0; i < this.m_ReportDocment.DataSourceConnections.Count; i++)
                {
                    this.m_ReportDocment.DataSourceConnections[i].SetConnection(server, "", user, password);
                }
            }

            //テーブル位置を明示的に設定
            //別のスキーマに同じ名前のテーブルがある場合に参照がおかしくなるのを防ぐ。
            string tableLocation = "";
            if (server != "" && user != "")
            {
                System.Collections.Generic.List<TableLogOnInfo> tls = new System.Collections.Generic.List<TableLogOnInfo>();
                for (int i = 0; i < this.m_ReportDocment.Database.Tables.Count; i++)
                {
                    tls.Add(this.m_ReportDocment.Database.Tables[i].LogOnInfo);
                }

                for (int i = 0; i < this.m_ReportDocment.Database.Tables.Count; i++)
                {
                    tableLocation = this.m_ReportDocment.Database.Tables[i].Location;
                    if (tableLocation.Contains("."))
                    {
                        try
                        {
                            // 既にプレフィックスがある場合は置き換える
                            this.m_ReportDocment.Database.Tables[i].Location = tableLocation.Replace(tableLocation.Substring(0, tableLocation.IndexOf(".")), user);
                        }
                        catch
                        {
                            // エラー回避。テーブルの状態によってはエラーになる場合があるので、ログオン情報を再セットしてリランする
                            this.m_ReportDocment.Database.Tables[i].ApplyLogOnInfo(tls[i]);
                            this.m_ReportDocment.Database.Tables[i].Location = tableLocation.Replace(tableLocation.Substring(0, tableLocation.IndexOf(".")), user);
                        }
                    }
                    else
                    {
                        try
                        {
                            // プレフィックスが無い場合は追加する
                            this.m_ReportDocment.Database.Tables[i].Location = user + "." + tableLocation;
                        }
                        catch
                        {
                            // エラー回避。テーブルの状態によってはエラーになる場合があるので、ログオン情報を再セットしてリランする
                            this.m_ReportDocment.Database.Tables[i].ApplyLogOnInfo(tls[i]);
                            this.m_ReportDocment.Database.Tables[i].Location = user + "." + tableLocation;
                        }
                    }
                }
            }

            //// クエリ渡しの場合
            //// 方法不明の為、実装せず。

            // リフレッシュ
            this.m_ReportDocment.Refresh();

            // データベースデータを自動的に保存
            this.m_ReportDocment.ReportOptions.EnableSaveDataWithReport = true;

            return true;
        }

        /// <summary>
        /// レポートを初期化します。（MDB用）
        /// </summary>
        /// <param name="server">MDBファイル名(ﾌﾙﾊﾟｽ)</param>
        /// <param name="fileName">レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        /// <param name="outFileName">出力レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        public bool InitializeForMDB(string mdbFileName, string fileName)
        {
            return InitializeForMDB(mdbFileName, fileName, "");
        }

        /// <summary>
        /// レポートを初期化します。（MDB用）
        /// </summary>
        /// <param name="server">MDBファイル名(ﾌﾙﾊﾟｽ)</param>
        /// <param name="fileName">レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        /// <param name="outFileName">出力レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        public bool InitializeForMDB(string mdbFileName, string fileName, string outFileName)
        {
            //初期化（共通）
            this.Initialize(fileName, outFileName);

            //接続情報の設定
            if (mdbFileName != "")
            {
                for (int i = 0; i < this.m_ReportDocment.DataSourceConnections.Count; i++) {
                    this.m_ReportDocment.DataSourceConnections[i].SetConnection(mdbFileName, "", "", "");
                }
            }

            // リフレッシュ
            this.m_ReportDocment.Refresh();

            // データベースデータを自動的に保存
            this.m_ReportDocment.ReportOptions.EnableSaveDataWithReport = true;

            return true;
        }

        /// <summary>
        /// レポートを初期化します。（XML用）
        /// </summary>
        /// <param name="xmlFileName">データソースとして使用するXMLファイル(ﾌﾙﾊﾟｽ)</param>
        /// <param name="fileName">レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        /// <param name="outFileName">出力レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        public bool InitializeForXML(string xmlFileName, string fileName, string outFileName)
        {
            try
            {
                //初期化（共通）
                this.Initialize(fileName, outFileName);

                //XMLファイルの指定
                foreach (Table tbl in this.m_ReportDocment.Database.Tables)
                {
                    tbl.Location = xmlFileName;
                }

                foreach (ReportDocument sRep in this.m_ReportDocment.Subreports)
                {
                    foreach (Table tbl in this.m_ReportDocment.Database.Tables)
                    {
                        tbl.Location = "";
                    }
                }

                // リフレッシュ
                this.m_ReportDocment.Refresh();

                // データベースデータを自動的に保存
                this.m_ReportDocment.ReportOptions.EnableSaveDataWithReport = true;

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// レポートを初期化します。（DataSet用）
        /// </summary>
        /// <param name="dataSet">データソースとして使用するDataSet</param>
        /// <param name="fileName">レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        /// <param name="outFileName">出力レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        public bool InitializeForDataSet(DataSet dataSet, string fileName, string outFileName)
        {
            try
            {
                //初期化（共通）
                this.Initialize(fileName, outFileName);

                //データソースの設定
                this.m_ReportDocment.SetDataSource(dataSet);

                // リフレッシュ
                this.m_ReportDocment.Refresh();

                // データベースデータを自動的に保存
                this.m_ReportDocment.ReportOptions.EnableSaveDataWithReport = true;

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// レポートを初期化します。（DataTable用）
        /// </summary>
        /// <param name="dataSet">データソースとして使用するDataTable</param>
        /// <param name="fileName">レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        /// <param name="outFileName">出力レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        public bool InitializeForDataTable(DataTable dataTable, string fileName, string outFileName)
        {
            try
            {
                //初期化（共通）
                this.Initialize(fileName, outFileName);

                //データソースの設定
                this.m_ReportDocment.SetDataSource(dataTable);

                // リフレッシュ
                this.m_ReportDocment.Refresh();

                // データベースデータを自動的に保存
                this.m_ReportDocment.ReportOptions.EnableSaveDataWithReport = true;

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 保存

        /// <summary>
        /// レポートを保存します。
        /// </summary>
        public bool Save()
        {
            return this.Save(this.m_OutFileName);
        }        
        
        /// <summary>
        /// レポートを保存します。
        /// </summary>
        /// <param name="outFileName">出力レポートファイル名</param>
        public bool Save(string outFileName)
        {
            if (outFileName == "")
            {
                //出力ファイル名が無い場合はNG
                return false;
            }

            try
            {
                // レポートをエクスポートする
                this.m_ReportDocment.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.CrystalReport, outFileName);
                System.Threading.Thread.Sleep(100);
                System.Windows.Forms.Application.DoEvents();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 印刷

        /// <summary>
        /// 印刷します。
        /// </summary>
        /// <param name="printerName">プリンタ名</param>
        /// <returns>
        /// true : 印刷成功、false : 印刷失敗
        /// </returns>
        public bool PrintOut(string printerName)
        {
            PrintParameter pm = new PrintParameter();
            pm.PrinterName = printerName;
            pm.DriverName = "";
            pm.Port = "";
            return this.PrintOut(pm);
        }

        /// <summary>
        /// 印刷します。
        /// </summary>
        /// <param name="printerName">プリンタ名</param>
        /// <param name="driverName">ドライバ名</param>
        /// <param name="port">ポート</param>
        /// <returns>
        /// true : 印刷成功、false : 印刷失敗
        /// </returns>
        public bool PrintOut(string printerName, string driverName, string port)
        {
            PrintParameter pm = new PrintParameter();
            pm.PrinterName = printerName;
            pm.DriverName = driverName;
            pm.Port = port;
            return this.PrintOut(pm);
        }

        /// <summary>
        /// 印刷します。
        /// </summary>
        /// <param name="printParameter">印刷パラメーター</param>
        /// <returns>
        /// true : 印刷成功、false : 印刷失敗
        /// </returns>
        public bool PrintOut(PrintParameter printParameter)
        {
            // 出力ファイル指定ありの場合
            if (this.m_OutFileName != "")
            {
                // 一旦保存する
                this.Save(this.m_OutFileName);

                // プリント要求=falseの場合は印刷せずに正常終了
                if (!printParameter.PrintReq)
                {
                    return true;
                }

                // レポートファイルのリロード
                ReportDocument report = new ReportDocument();
                report.Load(this.m_OutFileName);

                return this.printOutSub(report, printParameter);
            }
            else
            {
                // プリント要求=falseの場合は出力せずに正常終了
                if (!printParameter.PrintReq)
                {
                    return true;
                }

                return this.printOutSub(this.m_ReportDocment, printParameter);
            }
        }

        /// <summary>
        /// 印刷する（再プリント用）
        /// </summary>
        /// <param name="printerName"></param>
        public bool PrintOutSpool(string fileName, string printerName)
        {
            return PrintOutSpool(fileName, printerName, "", "");
        }

        /// <summary>
        /// 印刷する（再プリント用）
        /// </summary>
        /// <param name="printerName"></param>
        public bool PrintOutSpool(string fileName, string printerName, string driverName, string portName)
        {
            if (!System.IO.File.Exists(fileName))
            {
				MessageBox.Show("プリント再出力エラー\nスプールファイルがありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // レポートファイルのロード
            ReportDocument report = new ReportDocument();
            report.Load(fileName);

            PrintParameter pm = new PrintParameter();
            pm.PrinterName = printerName;
            pm.DriverName = driverName;
            pm.Port = portName;

            // 出力
            return this.printOutSub(report, pm);
        }


        /// <summary>
        /// 印刷処理メイン（.NET版）
        /// </summary>
        /// <param name="report">レポートドキュメント</param>
        /// <param name="printParameter">印刷パラメーター</param>
        /// <returns>
        /// true  : 正常終了
        /// false : 異常終了
        /// </returns>
        protected bool printOutSub(ReportDocument report, PrintParameter printParameter)
        {
            try
            {
                // 用紙設定を保存する
                CrystalDecisions.Shared.PaperSize paperSize = report.PrintOptions.PaperSize;

                // 出力プリンター設定を切替える
                report.PrintOptions.PrinterName = printParameter.PrinterName;

                // 用紙を再設定する
                report.PrintOptions.PaperSize = paperSize;

                // 印刷
                //(Page単位で部数出力されてしまう為、クリスタルレポートに任せない)
                for (int i = 0; i < printParameter.NumberOfCopies; i++)
                {
                    report.PrintToPrinter(1, true, printParameter.StartPageN, printParameter.EndPageN);
                    System.Windows.Forms.Application.DoEvents();
                }

                return true;
            }
            catch (Exception e)
            {
                string msg = e.Message;
                return false;
            }
        }

        #endregion

        #region プレビュー

        /// <summary>
        /// プレビューする
        /// </summary>
        public void Preview()
        {
            Preview("");
        }


        /// <summary>
        /// プレビューする
        /// </summary>
        public void Preview(string printerName)
        {
            if (this.m_OutFileName != "")
            {
                // 保存する
                this.Save(this.m_OutFileName);

                //this.previewSub(this.m_OutFileName);
                FormPreview frm = new FormPreview();
                frm.Preview(this.m_OutFileName, printerName);
            }
            else
            {
                //this.previewSub(this.m_FileName);
                FormPreview frm = new FormPreview();
                frm.Preview(this, printerName);
            }
        }

        /// <summary>
        /// プレビューする（再プリント用）
        /// </summary>
        /// <param name="printerName"></param>
        public void PreviewSpool(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
				MessageBox.Show("プリント再出力エラー\nスプールファイルがありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // レポートファイルのロード
            //this.previewSub(fileName);
            FormPreview frm = new FormPreview();
            frm.Preview(fileName);
        }

        ///// <summary>
        ///// プレビュー
        ///// </summary>
        ///// <param name="filename">ファイル名</param>
        //protected void previewSub(string filename)
        //{
        //    FormPreview frm = new FormPreview();
        //    frm.Preview(filename);
        //}

        #endregion

        #region 付加情報設定

        /// <summary>
        /// 式フィールドを設定します。
        /// </summary>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="value">設定値</param>
        public void SetFormulaField(string fieldName, object value)
        {
            foreach (FormulaFieldDefinition ffd in this.m_ReportDocment.DataDefinition.FormulaFields)
            {
                if( ffd.FormulaName == "{@" + fieldName + "}" )
                {
                    if (CommonFunc.IsNumeric(value))
                    {
                        ffd.Text = value.ToString();
                    }
                    else
                    {
                        ffd.Text = "'" + value.ToString() + "'";
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 式フィールド設定
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <param name="isNumber"></param>
        public void SetFormulaField(string fieldName, object value, bool isNumber)
        {
            foreach (FormulaFieldDefinition ffd in this.m_ReportDocment.DataDefinition.FormulaFields)
            {
                if (ffd.FormulaName == "{@" + fieldName + "}")
                {
                    if (isNumber)
                    {
                        if (CommonFunc.IsNumeric(value))
                        {
                            ffd.Text = value.ToString();
                        }
                        else
                        {
                            ffd.Text = "'" + value.ToString() + "'";
                        }
                    }
                    else
                    {
                        ffd.Text = "'" + value.ToString() + "'";
                    }
                    break;
                }
            }
        }


        /// <summary>
        /// パラメータフィールドを設定します。
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public void SetParameterField(string fieldName, object value)
        {
            //ParameterField pField = new ParameterField();
            //ParameterFields pFields = new ParameterFields();
            //ParameterDiscreteValue pDiscreateValue = new ParameterDiscreteValue();

            //// パラメータフィールド名を指定する
            //pField.Name = fieldName;

            //// パラメータの値を設定する
            //if (NCR.Data.ibConvert.IsNumeric(value))
            //{
            //    pDiscreateValue.Value = value.ToString();
            //}
            //else
            //{
            //    pDiscreateValue.Value = "'" + value.ToString() + "'";
            //}

            //// パラメータフィールドに値を設定する
            //pField.CurrentValues.Add(pDiscreateValue);
            //pFields.Add(pField);

            //2008.05.03 これだけで実装できた。
            this.m_ReportDocment.SetParameterValue(fieldName, value);
        }

        #endregion

        #region 付加情報取得

        /// <summary>
        /// 総ページ数を取得します。
        /// </summary>
        /// <returns></returns>
        public int GetTotalPageCount()
        {
            int total = 0;
            CrystalReportViewer viewer = new CrystalReportViewer();
            try
            {
                viewer.ReportSource = this.theReportDocument;
                viewer.ShowLastPage();
                total = viewer.GetCurrentPageNumber();
            }
            catch
            {
            }
            viewer.Dispose();

            return total;
        }

        /// <summary>
        /// 総ページ数を取得します。(static)
        /// </summary>
        /// <param name="fileName">レポートファイル名(ﾌﾙﾊﾟｽ)</param>
        /// <returns></returns>
        public static int GetTotalPageCount(string fileName)
        {
            int total = 0;
            ReportDocument report = new ReportDocument();
            CrystalReportViewer viewer = new CrystalReportViewer();
            try
            {
                report.Load(fileName);
                viewer.ReportSource = report;
                viewer.ShowLastPage();
                total = viewer.GetCurrentPageNumber();
            }
            catch
            {
            }
            report.Dispose();
            viewer.Dispose();

            return total;
        }

        #endregion

        #region 付加情報設定(SUBレポート)

        /// <summary>
        /// 式フィールドを設定します。
        /// </summary>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="value">設定値</param>
        public void SetSubFormulaField(string subReportName, string fieldName, object value)
        {
            foreach (FormulaFieldDefinition ffd in this.m_ReportDocment.Subreports[subReportName].DataDefinition.FormulaFields)
            {
                if (ffd.FormulaName == "{@" + fieldName + "}")
                {
                    if (CommonFunc.IsNumeric(value))
                    {
                        ffd.Text = value.ToString();
                    }
                    else
                    {
                        ffd.Text = "'" + value.ToString() + "'";
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 式フィールド設定
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <param name="isNumber"></param>
        public void SetSubFormulaField(string subReportName, string fieldName, object value, bool isNumber)
        {
            foreach (FormulaFieldDefinition ffd in this.m_ReportDocment.Subreports[subReportName].DataDefinition.FormulaFields)
            {
                if (ffd.FormulaName == "{@" + fieldName + "}")
                {
                    if (isNumber)
                    {
                        if (CommonFunc.IsNumeric(value))
                        {
                            ffd.Text = value.ToString();
                        }
                        else
                        {
                            ffd.Text = "'" + value.ToString() + "'";
                        }
                    }
                    else
                    {
                        ffd.Text = "'" + value.ToString() + "'";
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// パラメータフィールドを設定します。
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public void SetSubParameterField(string subReportName, string fieldName, object value)
        {
            this.m_ReportDocment.Subreports[subReportName].SetParameterValue(fieldName, value);
        }

        /// <summary>
        /// パラメータフィールドを設定します。
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public string GetSubReportName(int index)
        {
            try
            {
                return this.m_ReportDocment.Subreports[index].Name;
            }
            catch
            {
                return "";
            }
        }

        #endregion

        #region PDF操作

        /// <summary>
        /// PDFとして出力します。
        /// </summary>
        /// <param name="pdfFileName">PDFファイル名(ﾌﾙﾊﾟｽ)</param>
        /// <returns></returns>
        public bool WritePDF(string pdfFileName)
        {
            try
            {
                this.m_ReportDocment.ExportToDisk(ExportFormatType.PortableDocFormat, pdfFileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
