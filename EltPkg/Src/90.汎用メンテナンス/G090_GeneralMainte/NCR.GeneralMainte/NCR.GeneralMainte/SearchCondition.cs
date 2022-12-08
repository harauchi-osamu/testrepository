using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NCR.GeneralMainte
{
    public partial class SearchCondition : Form
    {
        private mainForm mmf;
        public SearchCondition(mainForm mf)
        {
            InitializeComponent();
            mmf = mf;
            mmf.Enabled = false;
            this.fillDataSourse();
            
        }

        private void fillDataSourse()
        {
            this.ColumnName1.DataSource = ArrayFromColumns(mmf.dataset.Tables["全体"].Columns);
            this.ColumnName2.DataSource = ArrayFromColumns(mmf.dataset.Tables["全体"].Columns);
            this.ColumnName3.DataSource = ArrayFromColumns(mmf.dataset.Tables["全体"].Columns);
            this.ColumnName4.DataSource = ArrayFromColumns(mmf.dataset.Tables["全体"].Columns);
        }

        private ArrayList  ArrayFromColumns(DataColumnCollection Columns)
        {
            ArrayList arr = new ArrayList();            
            foreach (DataColumn  c in Columns)
            {
                arr.Add(c.ColumnName);
            }
            return arr;
        }

        private void SearchStartButton_Click(object sender, EventArgs e)
        {
            mmf.Enabled = true;
            DataTable dt = mmf.dataset.Tables["検索条件"];
            dt.Rows.Clear();
            if (this.Condition1IsEffective())
            {
                DataRow dr = dt.NewRow();
                dr["列名"] = this.ColumnName1.SelectedItem.ToString();
                dr["演算子"] = this.Condition1.SelectedItem.ToString();
                dr["値"] = this.SearchValue1.Text;
                dt.Rows.Add(dr);
            }
            if (this.Condition2IsEffective())
            {
                DataRow dr = dt.NewRow();
                dr["列名"] = this.ColumnName2.SelectedItem.ToString();
                dr["演算子"] = this.Condition2.SelectedItem.ToString();
                dr["値"] = this.SearchValue2.Text;
                dt.Rows.Add(dr);
            }
            if (this.Condition3IsEffective())
            {
                DataRow dr = dt.NewRow();
                dr["列名"] = this.ColumnName3.SelectedItem.ToString();
                dr["演算子"] = this.Condition3.SelectedItem.ToString();
                dr["値"] = this.SearchValue3.Text;
                dt.Rows.Add(dr);
            }
            if (this.Condition4IsEffective())
            {
                DataRow dr = dt.NewRow();
                dr["列名"] = this.ColumnName4.SelectedItem.ToString();
                dr["演算子"] = this.Condition4.SelectedItem.ToString();
                dr["値"] = this.SearchValue4.Text;
                dt.Rows.Add(dr);
            }

            if (mmf.ShowSelectedData(this.ReturnRowState()))
            {
                //this.Dispose();   //NM140313 DEL
                ReturnMainForm();   //NM140313 ADD
            }
            else 
            {
                //this.Dispose();   //NM140313 DEL
                ReturnMainForm();   //NM140313 ADD
            }
        }

        private DataViewRowState ReturnRowState()
        {
                return DataViewRowState.CurrentRows; 
        }

        private bool Condition1IsEffective()
        {
            if (this.ColumnName1.SelectedItem  == null
                ||  this.Condition1.SelectedItem   ==null 
                || this.SearchValue1.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool Condition2IsEffective()
        {
            if (this.ColumnName2.SelectedItem == null
                || this.Condition2.SelectedItem == null
                || this.SearchValue2.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool Condition3IsEffective()
        {
            if (this.ColumnName3.SelectedItem == null
                || this.Condition3.SelectedItem == null
                || this.SearchValue3.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool Condition4IsEffective()
        {
            if (this.ColumnName4.SelectedItem == null
                || this.Condition4.SelectedItem == null
                || this.SearchValue4.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            //this.Dispose();   //NM140313 DEL
            ReturnMainForm();   //NM140313 ADD
        }

        private void SearchCondition_Deactivate(object sender, EventArgs e)
        {
            //<!--NM140313 DEL
            //mmf.Enabled = true;
            //mmf.TopMost = true;
            //mmf.TopLevel = true;            
            //mmf.Focus();
            //--!>
        }

        private void SearchCondition_Load(object sender, EventArgs e)
        {

        }

        //<!--NM140313 ADD
        private void ReturnMainForm()
        {
            mmf.Show();
            mmf.Enabled = true;
            mmf.TopLevel = true;
            mmf.Focus();

            this.Dispose();
        }
        //--!>

        private void SearchCondition_Leave(object sender, EventArgs e)
        {
            //mmf.Show();   //NM140313 DEL
            
        }
    }
}
