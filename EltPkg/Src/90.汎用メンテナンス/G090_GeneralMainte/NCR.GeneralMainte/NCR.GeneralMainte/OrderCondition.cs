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
    public partial class OrderCondition : Form
    {
        private mainForm mmf;
        public OrderCondition(mainForm mf)
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

            arr.Add("");
            foreach (DataColumn  c in Columns)
            {
                arr.Add(c.ColumnName);
            }
            return arr;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            mmf.Enabled = true;
            DataTable dt = mmf.dataset.Tables["並替条件"];
            dt.Rows.Clear();

            AddOrderRow(this.ColumnName1, this.AscDescString(this.radioDESC1), dt);
            AddOrderRow(this.ColumnName2, this.AscDescString(this.radioDESC2), dt);
            AddOrderRow(this.ColumnName3, this.AscDescString(this.radioDESC3), dt);
            AddOrderRow(this.ColumnName4, this.AscDescString(this.radioDESC4), dt);

            mmf.ShowSelectedData(DataViewRowState.CurrentRows);

            ReturnMainForm();
        }

        private void AddOrderRow(ComboBox cmb, string ascdesc, DataTable dt)
        {
            if (cmb.SelectedItem.ToString().Length == 0) return;

            DataRow dr = dt.NewRow();
            dr["順番"] = dt.Rows.Count + 1;
            dr["列名"] = cmb.SelectedItem.ToString();
            dr["昇降順"] = ascdesc;
            
            dt.Rows.Add(dr);
        }

        private string AscDescString(RadioButton rdesc)
        {
            if (rdesc.Checked) return "降順";

            //デフォルトは昇順とする
            return "昇順";
        }

        private void ReturnMainForm()
        {
            mmf.Show();
            mmf.Enabled = true;
            mmf.TopLevel = true;
            mmf.Focus();

            this.Dispose();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            ReturnMainForm();
        }

        private void OrderCondition_FormClosed(object sender, FormClosedEventArgs e)
        {
            //×ボタンを押した場合
            ReturnMainForm();
        }
    }
}
