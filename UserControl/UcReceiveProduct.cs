using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPOS_Online
{
    public partial class UcReceiveProduct : UserControl
    {
        public UcReceiveProduct()
        {
            InitializeComponent();
        }

        private void UcReceiveProduct_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1);
        }
    }
}
