using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using IIPCommonClass;
using IIPCommonClass.DB;
using IIPCommonClass.Log;
using System.Collections.Generic;
using System.Linq;

namespace IIPReference
{
    public partial class BatchListControl : UserControl
    {

        public BatchListControl()
        {
            InitializeComponent();
        }

        List<DBManager> dBs;

        #region メンバー

        ListViewItemComparer _listViewItemSorter;
        string _gym_Id1 = "";

        #endregion

        #region 初期化
        public void InitializeControl(string _gym_id1, List<DBManager> dB)
        {
            this.InitializeControl(_gym_id1,this.tbOpeDate.Text, dB);
        }

        public bool InitializeControl(string _gym_id1, string _ope_date, List<DBManager> dB)
        {
            _gym_Id1 = _gym_id1;
            dBs = dB;

            if(!this.SetDispItem(_gym_id1, _ope_date))
            {
                return false;
            }
            ResetControl();
            return true;
        }

        public void ResetControl()
        {
            try
            {
                this.SuspendLayout();
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
                this.ResumeLayout(true);
            }
        }
        
        private void InitializeListView()
        {
            this.lstBatch.ListViewItemSorter = null;
            this.lstBatch.Items.Clear();
            this.lstBatch.OwnerDraw = true;
        }

        #endregion

        #region イベント

        private void lstBatch_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            _listViewItemSorter.Column = e.Column;
            lstBatch.Sort();
        }

        private void tbOpeDate_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.SetDispItem(_gym_Id1);
                    break;
                default:
                    break;
            }
        }
        private void lstBatch_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.Bounds);
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            sf.FormatFlags = StringFormatFlags.NoWrap;
            e.Graphics.DrawString(e.Header.Text, lstBatch.Font, Brushes.Black, e.Bounds, sf);

        }

        private void lstBatch_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lstBatch_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        #endregion

        #region 画面表示処理
        /// <summary>
        /// 画面表示
        /// </summary>
        public void SetDispItem(string gym_id1)
        {
            this.SetDispItem(gym_id1, this.tbOpeDate.Text);
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        public bool SetDispItem(string gym_id1, string ope_date)
       {
            //入力チェック
            int value = 0;
            if (!int.TryParse(ope_date, out value))
            {
                MessageBox.Show("処理日に半角数字を指定してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tbOpeDate.Focus();
                return false;
            }

            this.tbOpeDate.Enabled = false;
            this.lstBatch.Enabled = false;
            this.InitializeListView();

            DataRow[] drs;
            DataTable SubTable = new DataTable();

            //string[] SubTableLow = new string[]{"OpeDate", "BatchName", "DocCnt", "OCR1", "OCR1E", "OCR2", "OCR2E",
            //    "Seikei", "SeikeiE", "Renkei", "RenkeiE", "Complete", "Status", "Error","BATCHDIRECTORY", "BatchID"};
            string[] SubTableLow = new string[]{"OpeDate", "Term", "BatchName", "DocCnt", "OCR1", "OCR1E", "OCR2", "OCR2E",
                "Seikei", "SeikeiE", "Renkei", "RenkeiE", "Complete", "Status", "Error","BATCHDIRECTORY", "BatchID"};
            for (int i = 0; i < SubTableLow.Length; i++)
            {
                SubTable.Columns.Add(SubTableLow[i]);
            }

            for (int i = 0; i < dBs.Count(); i++)
            {
                string strsql = this.GetSqlString(gym_id1, value);
                drs = dBs[i].ExecuteRows(strsql, "SCAN_BATCHTR");
                for (int l = 0; l < drs.Count(); l++)
                {
                    SubTable.ImportRow(drs[l]);
                }
            }
                
            ListViewItem lvi = new ListViewItem();

            colOcr1.Text = AplInfo.OCR1;
            colOcr2.Text = AplInfo.OCR2;

            for (int i = 0; i < SubTable.Rows.Count; i++)
            {
                DataRow dr = SubTable.Rows[i];
                int n = 0;
                lvi = lstBatch.Items.Add("");

                for (int j = 0; j < dr.ItemArray.Length; j++)
                {
                    lvi.UseItemStyleForSubItems = false;
                    //2022.7.20 一覧に号機列追加 S
                    //switch (j)
                    //{
                    //    case 0:
                    //        lvi.SubItems.Add(dr[j].ToString().PadLeft(8, '0'));
                    //        break;
                    //    case 4:
                    //    case 6:
                    //    case 8:
                    //    case 10:
                    //    case 12:
                    //        if (dr[j].ToString() == "R")
                    //        {
                    //            this.lstBatch.Items[i].SubItems[j - n].ForeColor = Color.Red;
                    //            this.lstBatch.Items[i].SubItems[j - n].Font = new Font(lvi.Font, FontStyle.Bold);
                    //            n = n + 1;
                    //        }
                    //        else
                    //        {
                    //            n = n + 1;
                    //        }
                    //        break;
                    //    case 14:
                    //        if (dr[j].ToString() == "●")
                    //        {
                    //            lvi.SubItems.Add(dr[j].ToString(), Color.Red, Color.White, lvi.Font);
                    //        }
                    //        else
                    //        {
                    //            lvi.SubItems.Add(dr[j].ToString(), Color.Silver, Color.White, lvi.Font);
                    //        }
                    //        break;
                    //    default:
                    //        if (int.TryParse(dr[j].ToString(), out int val))
                    //        {
                    //            if (val == 0)
                    //            {
                    //                lvi.SubItems.Add(dr[j].ToString(), Color.Silver, Color.White, lvi.Font);
                    //            }
                    //            else
                    //            {
                    //                lvi.SubItems.Add(String.Format("{0:#,0}", val));
                    //            }
                    //        }
                    //        else
                    //        {
                    //            lvi.SubItems.Add(dr[j].ToString());
                    //        }
                    //        break;
                    //}
                    switch (j)
                    {
                        case 0:
                            lvi.SubItems.Add(dr[j].ToString().PadLeft(8, '0'));
                            break;
                        case 1:
                            if(IIPReference.AppInfo.Goki().ContainsKey(dr[j].ToString()))
                            {
                                lvi.SubItems.Add(IIPReference.AppInfo.Goki()[dr[j].ToString()], Color.Black, Color.White, lvi.Font);
                            }
                            else
                            {
                                lvi.SubItems.Add("その他", Color.Black, Color.White, lvi.Font);
                            }
                            break;
                        case 5:
                        case 7:
                        case 9:
                        case 11:
                        case 13:
                            if (dr[j].ToString() == "R")
                            {
                                this.lstBatch.Items[i].SubItems[j - n].ForeColor = Color.Red;
                                this.lstBatch.Items[i].SubItems[j - n].Font = new Font(lvi.Font, FontStyle.Bold);
                                n = n + 1;
                            }
                            else
                            {
                                n = n + 1;
                            }
                            break;
                        case 15:
                            if (dr[j].ToString() == "●")
                            {
                                lvi.SubItems.Add(dr[j].ToString(), Color.Red, Color.White, lvi.Font);
                            }
                            else
                            {
                                lvi.SubItems.Add(dr[j].ToString(), Color.Silver, Color.White, lvi.Font);
                            }
                            break;
                        default:
                            if (int.TryParse(dr[j].ToString(), out int val))
                            {
                                if (val == 0)
                                {
                                    lvi.SubItems.Add(dr[j].ToString(), Color.Silver, Color.White, lvi.Font);
                                }
                                else
                                {
                                    lvi.SubItems.Add(String.Format("{0:#,0}", val));
                                }
                            }
                            else
                            {
                                lvi.SubItems.Add(dr[j].ToString());
                            }
                            break;
                    }
                    //2022.7.20 一覧に号機列追加 E
                }
            }

            _listViewItemSorter = new ListViewItemComparer();
            _listViewItemSorter.ColumnModes = new ListViewItemComparer.ComparerMode[]
            {
                ListViewItemComparer.ComparerMode.String
                ,ListViewItemComparer.ComparerMode.String
                ,ListViewItemComparer.ComparerMode.String
                ,ListViewItemComparer.ComparerMode.String
                ,ListViewItemComparer.ComparerMode.Integer
                ,ListViewItemComparer.ComparerMode.Integer
                ,ListViewItemComparer.ComparerMode.Integer
                ,ListViewItemComparer.ComparerMode.Integer
                ,ListViewItemComparer.ComparerMode.Integer
                ,ListViewItemComparer.ComparerMode.Integer
                ,ListViewItemComparer.ComparerMode.String
            };
            this.lstBatch.ListViewItemSorter = _listViewItemSorter;

            if (this.tbOpeDate.Text == "")
            {
                this.tbOpeDate.Text = ope_date;
            }

            this.lstBatch.Enabled = true;
            this.tbOpeDate.Enabled = true;
            this.lstBatch.Focus();

            if(this.lstBatch.Items.Count > 0)
            {
                this.lstBatch.Items[0].Selected = true;
            }

            if (lstBatch.Items.Count == 0)
            {
                MessageBox.Show("該当のデータがありません。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private string GetSqlString(string _gym_id1, int _ope_date)
        {
            return $@"
            select 
	            d1.OpeDate
,d1.TERM
	            ,d1.BatchName
	            ,d1.DocCnt
	            ,sum(d1.OCR0 + d1.OCR1) as OCR1
	            ,case when sum(d1.OCR1E) > 0 then 'R' else 'B' end as OCR1E
	            ,sum(d1.OCR2) as OCR2
	            ,case when sum(d1.OCR2E) > 0 then 'R' else 'B' end as OCR2E
	            ,sum(d1.Seikei) as Seikei
	            ,case when sum(d1.SeikeiE) > 0 then 'R' else 'B' end as SeikeiE
	            ,sum(d1.Renkei) as Renkei
	            ,case when sum(d1.RenkeiE) > 0 then 'R' else 'B' end as RenkeiE
	            ,sum(d1.Complete) as Complete
	            ,d1.Status
	            ,case when sum(d1.Error) > 0 then '●' else '-' end as Error
                ,d1.BATCHDIRECTORY
                ,d1.BatchID
            from (
	            select
		            to_char(bt.DateCreated, 'HH24:MI:SS') as OpeDate
,dt.TERM
                    ,bt.BatchID
		            ,bt.BatchName
		            ,bt.DocCnt
		            ,case when(bt.Status > 0 and bt.Status < 3000) 
		                or(bt.Status in (3000) and dt.DocStatus in (0))  then 1 else 0 end as OCR0

		            ,case when(bt.Status in (3000) and dt.DocStatus in (100)) 
                        or (bt.Status in (3000) and dt.DocStatus in (105)) 
                        or (bt.Status in (3000) and dt.DocStatus in (108)) then 1 else 0 end as OCR1
		            ,case when(bt.Status in (3000) and dt.DocStatus in (108)) then 1 else 0 end as OCR1E

		            ,case when(bt.Status in (3000) and dt.DocStatus in (110)) 
                        or (bt.Status in (3000) and dt.DocStatus in (115)) 
                        or (bt.Status in (3000) and dt.DocStatus in (118)) then 1 else 0 end as OCR2
		            ,case when(bt.Status in (3000) and dt.DocStatus in (118)) then 1 else 0 end as OCR2E

		            ,case when(bt.Status in (3000) and dt.DocStatus in (300)) 
                        or (bt.Status in (3005) and dt.DocStatus in (300)) 
                        or (bt.Status in (3008) and dt.DocStatus in (300)) then 1 else 0 end as Seikei
		            ,case when(bt.Status in (3008) and dt.DocStatus in (300)) then 1 else 0 end as SeikeiE

		            ,case when(bt.Status in (3010) and dt.DocStatus in (300)) 
                        or (bt.Status in (3015) and dt.DocStatus in (300)) 
                        or (bt.Status in (3018) and dt.DocStatus in (300)) then 1 else 0 end as Renkei
		            ,case when(bt.Status in (3018) and dt.DocStatus in (300)) then 1 else 0 end as RenkeiE

		            ,case when(bt.Status in (3020) and dt.DocStatus in (300)) then 1 else 0 end as Complete

		            ,case when(bt.Status in (3008, 3018)) 
                        or (dt.DocStatus in (108, 118)) then 1 else 0 end as Error
		            ,bt.Status
                    ,bt.BATCHDIRECTORY
	            from
		            batchtable bt
		            left outer join doctable dt
			            on bt.BatchID = dt.BatchID
                        and bt.TERM = dt.TERM
	            where 1 = 1 
	            and bt.JobDescription = '{_gym_id1}'
	            and to_char(bt.DateCreated, 'YYYYMMDD') = '{_ope_date}'
            ) d1
            group by d1.TERM, d1.BatchName, d1.OpeDate, d1.DocCnt, d1.Status,d1.BATCHDIRECTORY, d1.BatchID
            order by d1.OpeDate,d1.BatchName
            ";

       
        }
        /// <summary>
        /// 画面表示
        /// </summary>
        public void ShowBatchImageView(string gym_id1)
        {
            BatchImageView thefrm = null;

            try
            {
                if (this.lstBatch.SelectedItems.Count == 0)
                {
                    return;
                }
                ListViewItem _item = this.lstBatch.FocusedItem;
                
                int _Selectted = this.lstBatch.FocusedItem.Index;

                // 2022.7.20 一覧に号機列追加 S
                //string b= _item.SubItems[10].Text;
                string b = _item.SubItems[11].Text;
                //if (System.IO.Directory.Exists(_item.SubItems[10].Text))
                if (System.IO.Directory.Exists(_item.SubItems[11].Text))
                {
                    //if (System.IO.Directory.GetFiles(_item.SubItems[10].Text).Length > 0)
                    if (System.IO.Directory.GetFiles(_item.SubItems[11].Text).Length > 0)
                    {
                        //System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(_item.SubItems[10].Text);
                        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(_item.SubItems[11].Text);
                        System.IO.FileInfo[] files = di.GetFiles("*.jpg");
                        if(files.Length>0)
                        {
                            thefrm = new BatchImageView();
                            if (thefrm.InitializeForm(files[0].FullName))
                            {
                                thefrm.ShowDialog();
                            }
                        }
                    }
                }
                // 2022.7.20 一覧に号機列追加 E
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void updateOcrStatus()
        {
            if (MessageBox.Show("選択しているバッチのステータスを強制的に進めますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                return;
            }

            ListViewItem _item = this.lstBatch.FocusedItem;
            string batchId = _item.SubItems[12].Text.Replace(",", "");

            string updBatSql = GetBatchUpdateSqlString(batchId);
            string updDocSql = GetDocUpdateSqlString(batchId);

            for (int i = 0; i < dBs.Count(); i++)
            {
                dBs[i].Execute(updBatSql, "BATCHTABLE");
                dBs[i].Execute(updDocSql, "DOCTABLE");
            }
        }

        private string GetBatchUpdateSqlString(string _batchId)
        {
            return $@"
                update dbiip.batchtable 
                set Status = CASE 
                    when Status in (2995, 2998) then 3000 
                    when Status in (3005, 3008) then 3010 
                    when Status in (3015, 3018) then 3020 else Status END 
                where BatchID = '{_batchId}' 
                and Status in (2995, 2998, 3005, 3008, 3015, 3018) 
            ";
        }

        private string GetDocUpdateSqlString(string _batchId)
        {
            return $@"
                update dbiip.doctable 
                set DocStatus = CASE 
                    when DocStatus in (105, 108) then 110 
                    when DocStatus in (115, 118) then 300 else DocStatus END 
                where BatchID = '{_batchId}' 
                and DocStatus in (105, 108, 115, 118) 
            ";
        }

        #endregion

    }
}
