using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NCR.GeneralMainte
{
    public partial class CrystalForm : Form
    {
        public CrystalForm()
        {
            InitializeComponent();
        }

        public int DefaultZoom
        {
            get { return 85; }
        }
    }
}
