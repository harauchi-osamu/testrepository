using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;

namespace NCR.GeneralMainte
{
    public partial class DataManipulationForm : Form
    {
        private mainForm mmf;
        private Int16 mmode;

        public DataManipulationForm(mainForm mf, Int16 Mode)
        {
            mmf = mf;
            mmf.Enabled = false;
            
            InitializeComponent();
            mmode = Mode;
            this.Text = getCaptionName();
            //RestrictByEachMode();
        }

        private const int COL_NO = 0;
        private const int COL_JP = 1;
        private const int COL_VAL = 2;
        private const int COL_HOJO = 3;
        private const int COL_NAME = 4;


        private void SetColSize()
        {
            this.dataGridView.Columns[COL_NO].Width = 37;
            this.dataGridView.Columns[COL_JP].Width = 150;
            this.dataGridView.Columns[COL_VAL].Width = 222;
            this.dataGridView.Columns[COL_HOJO].Width = 290;
            this.dataGridView.Columns[COL_NAME].Width = 0;
        }

        private void RestrictByEachMode()
        {
            switch (mmode)
            {
                case (int)DataManipulationForm.Mode.ADD:
                    this.dataGridView.Columns[COL_NO].ReadOnly = true;
                    this.dataGridView.Columns[COL_JP].ReadOnly = true;
                    this.dataGridView.Columns[COL_HOJO].ReadOnly = true;
                    this.dataGridView.Columns[COL_NAME].ReadOnly = true;
                    break;
                case (int)DataManipulationForm.Mode.MODIFY:
                    this.dataGridView.Columns[COL_NO].ReadOnly = true;
                    this.dataGridView.Columns[COL_JP].ReadOnly = true;
                    this.dataGridView.Columns[COL_HOJO].ReadOnly = true;
                    this.dataGridView.Columns[COL_NAME].ReadOnly = true;

                    break;
                case (int)DataManipulationForm.Mode.DELETE:
                    this.dataGridView.ReadOnly = true;
                    break;
            }
        }

        private string getCaptionName()
        {
            switch(mmode)
            {
                case (int)DataManipulationForm.Mode.ADD: { return "追加"; }
                case (int)DataManipulationForm.Mode.MODIFY: { return "変更"; }
                case (int)DataManipulationForm.Mode.DELETE: { return "削除"; }
            }
            return "";
        }

        private void setResizableOff()
        {

            this.dataGridView.Columns[COL_NO].Resizable = DataGridViewTriState.False;
            this.dataGridView.Columns[COL_JP].Resizable = DataGridViewTriState.False;
            this.dataGridView.Columns[COL_VAL].Resizable = DataGridViewTriState.True;
            this.dataGridView.Columns[COL_HOJO].Resizable = DataGridViewTriState.True;
            this.dataGridView.Columns[COL_NAME].Resizable = DataGridViewTriState.False;
        }

        public enum Mode
        {
            ADD = 1,
            MODIFY = 2,
            DELETE = 3
        }

        private void ItemFinalize_Click(object sender, EventArgs e)
        {
            mmf.Enabled = true; //NM121125 ADD form Enableした状態でないとDatagridスクロールバーが無効
            bool bl = true;
            switch (mmode)
            {
                case (int)DataManipulationForm.Mode.ADD:
                    bl=this.callItemFinalize();
                    break;
                case (int)DataManipulationForm.Mode.MODIFY:
                    bl=this.callItemFinalize();
                    this.mmf.Enabled = false;
                    break;
                case (int)DataManipulationForm.Mode.DELETE:
                    mmf.delStart();
                    break;
            }
            if (bl)
            {
                //this.Dispose();   //NM140313 DEL
                ReturnMainForm();   //NM140313 ADD
            }
            else
            {
                this.Activate();
            }
        }

        private bool callItemFinalize()
        {
            try
            {
                mmf.ItemFinalize();
            }
            catch (OdbcException oe)
            {
                mmf.Enabled = false;
                MessageBox.Show(this, oe.Message, "入力確定", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            catch (Exception ex)
            {
                mmf.Enabled = false;    //NM121125 ADD
                string[] mes = ex.Message.Split('\n');
                if (mes.Length > 1)
                {
                    string msg = mes[mes.Length - 1];
                    int pos = msg.IndexOf(")");
                    MessageBox.Show(this, msg.Substring(pos + 1), "入力確定", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(this, mes[0], "入力確定", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Dispose();   //NM140313 DEL
            ReturnMainForm();   //NM140313 ADD
        }

        private void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.RestrictByEachMode();
            this.SetColSize();
            this.setResizableOff();
            for (int i = 0; i < this.dataGridView.RowCount; i++)
            {
                this.dataGridView[COL_NAME, i].Style.ForeColor = Color.White;
            }
        }

        private void DataManipulationForm_Deactivate(object sender, EventArgs e)
        {
                this.mmf.Enabled = true;
                //this.mmf.Show();  //NM140313 DEL
        }

        /// <summary>
        /// NM121125 ADD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataManipulationForm_Activated(object sender, EventArgs e)
        {
            this.mmf.Enabled = false;
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


        //NM140124 ADD
        private void DataManipulationForm_Leave(object sender, EventArgs e)
        {
            //this.mmf.Show();  //NM140313 DEL


        }
    }
}
