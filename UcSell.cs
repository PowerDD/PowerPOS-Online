﻿using System;
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
    public partial class UcSell : UserControl
    {
        public UcSell()
        {
            InitializeComponent();
        }

        private void UcSell_Load(object sender, EventArgs e)
        {
            Util.InitialTable(table1);
        }
    }
}
