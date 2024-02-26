using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            
        }

        private void ucWBFilterPanel1_TextChanged(object sender, EventArgs e)
        {
        var ddd =     ucWBFilterPanel1.EndDate ;
        }
    }
}
