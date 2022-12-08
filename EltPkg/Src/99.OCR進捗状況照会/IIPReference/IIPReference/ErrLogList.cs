using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonClass;

namespace IIPReference
{
    public partial class ErrLogList : UnclosableForm
    {
        public ErrLogList()
        {
            InitializeComponent();
            ListViewSet();
        }

        private void ListViewSet()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.InitialDirectory = 
        }
    }
}
