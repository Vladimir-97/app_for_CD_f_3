using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app_for_CD
{
    public partial class SaveForSer : Form
    {
        public SaveForSer()
        {
            InitializeComponent();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Data.yes = true;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Data.yes = false;
            this.Close();
        }

    }
}
