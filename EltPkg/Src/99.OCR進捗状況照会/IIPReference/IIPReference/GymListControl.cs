using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IIPCommonClass;
using IIPCommonClass.DB;
using IIPCommonClass.Log;

namespace IIPReference
{
    public partial class GymListControl : UserControl
    {
        List<DBManager> _dBs;
        int _serverCnt;

        public GymListControl()
        {
            InitializeComponent();
        }

        #region メンバー

        ListViewItemComparer _listViewItemSorter;

        #endregion

        #region 初期化
        public void InitializeControl(List<DBManager> dB, int serCnt) {
            _dBs = dB;
            _serverCnt = serCnt;
        this.SetDispItem();
            ResetControl();
        }

        public void ResetControl()
        {
            try
            {
                this.SuspendLayout();
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            }
            finally {
                this.Cursor = System.Windows.Forms.Cursors.Default;
                this.ResumeLayout(true);
            }
        }

        private void InitializeListView()
        {
            this.lstGym.ListViewItemSorter = null;
            this.lstGym.Items.Clear();
            this.lstGym.OwnerDraw = true;
        }
        #endregion

        #region イベント

        public void ShowBatchList()
        {
            AppSubForm thefrm = null;

            try
            {
                if (this.lstGym.SelectedItems.Count > 0)
                {
                    int _Selectted = this.lstGym.FocusedItem.Index;
                    thefrm = new AppSubForm(this.lstGym.FocusedItem, _dBs, _serverCnt);
                    if (thefrm.InitializeForm())
                    {
                        thefrm.ShowDialog();

						// データ更新、画面再描画
						this.SetDispItem();
                    }
                    else
                    {
                        if (this.lstGym.Items.Count > 0)
                        {
                            this.lstGym.Items[_Selectted].Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        private void lstGym_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            _listViewItemSorter.Column = e.Column;
            lstGym.Sort();
        }

        private void lstGym_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ShowBatchList();

        }

        private void lstGym_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowBatchList();
        }

        private void lstGym_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.Bounds);
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            sf.FormatFlags = StringFormatFlags.NoWrap;
            e.Graphics.DrawString(e.Header.Text, lstGym.Font, Brushes.Black, e.Bounds, sf);
        }

        private void lstGym_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lstGym_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }
        #endregion

        #region 画面表示処理

        /// <summary>
        /// 画面表示
        /// </summary>
        public void SetDispItem()
        {
            this.lstGym.Enabled = false;
            this.InitializeListView();

            DataRow[] drs;
            string strsql;
            strsql = this.GetSqlString();
            DataTable MainTable = new DataTable();

            string[] _mainTableRows = new string[] {"業務名", "Processing", "Complete", "Error"};
            for (int i = 0; i < _mainTableRows.Length; i++)
            {
                MainTable.Columns.Add(_mainTableRows[i]);
            }

            drs = _dBs[0].ExecuteRows(strsql, "GYM_PARAM");
            for (int k = 0; k < drs.Count(); k++)
            {
                MainTable.ImportRow(drs[k]);
            }

            for (int s = 1; s < _dBs.Count(); s++)
            {
                strsql = this.GetSqlString();
                drs = _dBs[s].ExecuteRows(strsql, "GYM_PARAM");


                for (int i = 0; i < drs.Count(); i++)
                {
                    int addNumber = Convert.ToInt32(MainTable.Rows[i]["Processing"]) + Convert.ToInt32(drs[i]["Processing"]);
                    MainTable.Rows[i]["Processing"] = addNumber;
                    addNumber = Convert.ToInt32(MainTable.Rows[i]["Complete"]) + Convert.ToInt32(drs[i]["Complete"]);
                    MainTable.Rows[i]["Complete"] = addNumber;

                    if (MainTable.Rows[i]["Error"].ToString() != "●" && drs[i]["Error"].ToString() == "●")
                    {
                        MainTable.Rows[i]["Error"] = "●";
                    }
                }

            }
            


            ListViewItem lvi = new ListViewItem();


            for (int i = 0; i < MainTable.Rows.Count; i++)
            {
                DataRow dr = MainTable.Rows[i];
                lvi = lstGym.Items.Add("");

                for (int j = 0; j < dr.ItemArray.Length ; j++)
                {
                    lvi.UseItemStyleForSubItems = false;
                    switch (j)
                    {
                        case 0:
                            lvi.SubItems.Add(dr[j].ToString());
                            break;
                        case 4:
                            if (dr[j].ToString() == "●")
                            {
                                lvi.SubItems.Add(dr[j].ToString(), Color.Red, Color.White, lvi.Font);
                            }
                            else
                            {
                                lvi.SubItems.Add(dr[j].ToString(), Color.Silver, Color.White, lvi.Font);
                            }
                            break;
                        case 5:
                            lvi.SubItems.Add(dr[j].ToString());
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
                }
            }

            _listViewItemSorter = new ListViewItemComparer();
            _listViewItemSorter.ColumnModes = new ListViewItemComparer.ComparerMode[]
            {
                ListViewItemComparer.ComparerMode.String
                ,ListViewItemComparer.ComparerMode.String
                ,ListViewItemComparer.ComparerMode.Integer
                ,ListViewItemComparer.ComparerMode.Integer
                ,ListViewItemComparer.ComparerMode.String
            };
            this.lstGym.ListViewItemSorter = _listViewItemSorter;

            this.lstGym.Enabled = true;

            if (this.lstGym.Items.Count > 0)
            {
                this.lstGym.Items[0].Selected = true;
				this.lstGym.FocusedItem = this.lstGym.Items[0];
            }
        }

        private string GetSqlString()
        {

            int _syoriDate = int.Parse(AppInfo.OpeDate());

            string _buftable = "";
            if(Properties.Settings.Default.IsVisibleNORMAL)
            {
                if(_buftable.Length!=0)
                {
                    _buftable = _buftable + "union all";
                }
                _buftable = _buftable + "select '001' as 業務ID, '持出　通常' as 業務名 from dual";
            }
            if (Properties.Settings.Default.IsVisibleADDITION)
            {
                if (_buftable.Length != 0)
                {
                    _buftable = _buftable + " union all ";
                }
                _buftable = _buftable + "select '002' as 業務ID, '持出　付帯' as 業務名 from dual ";
            }
            if (Properties.Settings.Default.IsVisibleTOTAL)
            {
                if (_buftable.Length != 0)
                {
                    _buftable = _buftable+ " union all ";
                }
                _buftable = _buftable + " select '003' as 業務ID, '持出　合計票' as 業務名 from dual ";
            }
            if (Properties.Settings.Default.IsVisibleINCLEARING)
            {
                if (_buftable.Length != 0)
                {
                    _buftable = _buftable + " union all ";
                }
                _buftable = _buftable + " select '004' as 業務ID, '持帰' as 業務名 from dual ";
            }
            if (Properties.Settings.Default.IsVisibleINVENTORY)
            {
                if (_buftable.Length != 0)
                {
                    _buftable = _buftable + " union all ";
                }
                _buftable = _buftable + " select '005' as 業務ID, '期日管理　キャプチャー' as 業務名 from dual ";
            }
            return $@"
            select
				d2.業務名
	            ,sum(case when d2.Complete = 0 and d2.batch_name is not null and DateToday = '{_syoriDate}' then 1 else 0 end) as Processing
	            ,sum(case when d2.Complete > 0 and DateToday = '{_syoriDate}' then 1 else 0 end) as Complete
	            ,case when sum(case when d2.Error > 0 and DateToday = '{_syoriDate}' then 1 else 0 end) > 0 then '●' else '-' end as Error
            from (
                    select
						Main.業務ID
						,Main.業務名
						,bt.BatchName as batch_name
			            ,case when (bt.Status not in (3020)) then 1 else 0 end as Processing
			            ,case when (bt.Status in (3020) ) then 1 else 0 end as Complete
			            ,case when ((bt.Status in (2998, 3008, 3018)) or (dt.DocStatus in (108, 118, 208, 218) )) then 1 else 0 end as Error
						,to_char(bt.DateCreated, 'YYYYMMDD') as DateToday
	            from 
					(" + _buftable + $@")  Main
							left outer join dbiip.batchtable bt
								on Main.業務ID = bt.JobDescription
                                AND to_char(bt.DateCreated, 'YYYYMMDD')='{_syoriDate}'
							left outer join dbiip.doctable dt
								on bt.BatchID = dt.BatchID
                                and bt.TERM = dt.TERM
                                AND bt.STATUS<>0	
		            where 1 = 1
                    				
            ) d2
            group by d2.業務ID, d2.業務名
            order by d2.業務ID
            ";
    //明細単位出力
    //        return $@"
    //        select
				//d2.業務名
	   //         ,sum(case when d2.Complete = 0 and d2.batch_name is not null and DateToday = '{_syoriDate}' then 1 else 0 end) as Processing
	   //         ,sum(case when d2.Complete > 0 and DateToday = '{_syoriDate}' then 1 else 0 end) as Complete
	   //         ,case when sum(case when d2.Error > 0 and DateToday = '{_syoriDate}' then 1 else 0 end) > 0 then '●' else '-' end as Error
    //        from (select 業務ID,業務名,batch_name ,Processing,Complete,DateToday,SUM(Error) AS Error
    //              from(
    //                select
				//		Main.業務ID
				//		,Main.業務名
				//		,bt.BatchName as batch_name
			 //           ,case when (bt.Status not in (3020)) then 1 else 0 end as Processing
			 //           ,case when (bt.Status in (3020) ) then 1 else 0 end as Complete
			 //           ,case when ((bt.Status in (2998, 2008, 2018)) or (dt.DocStatus in (108, 208, 218) )) then 1 else 0 end as Error
				//		,to_char(bt.DateCreated, 'YYYYMMDD') as DateToday
	   //         from 
				//	(" + _buftable + $@")  Main
				//			left outer join dbiip.batchtable bt
				//				on Main.業務ID = bt.JobDescription
    //                            AND to_char(bt.DateCreated, 'YYYYMMDD')='{_syoriDate}'
				//			left outer join dbiip.doctable dt
				//				on bt.BatchID = dt.BatchID
    //                            and bt.TERM = dt.TERM
		  //          where 1 = 1
    //            )
    //            group BY 業務ID,業務名,batch_name ,Processing,Complete,DateToday
					
    //        ) d2

    //        group by d2.業務ID, d2.業務名
    //        order by d2.業務ID
    //        ";
        }
        #endregion

        private void GymListControl_Load(object sender, EventArgs e)
        {

        }
    }
}
