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
    public partial class TC_Menu : Form
    {
        public TC_Menu()
        {
            InitializeComponent();

            if (Data.role != 1)
            {
                tabControl1.TabPages.Remove(tabPage6);
                tabControl1.TabPages.Remove(tabPage7);
                this.tabControl1.ItemSize = new Size((this.tabControl1.Width / 5 - 1), 0);
            }
            else {
                this.tabControl1.ItemSize = new Size((this.tabControl1.Width / 7 - 1), 0);
            }
            

        }
        private void TC_Menu_Resize(object sender, EventArgs e)
        {
            if (Data.role != 1)
            {
                this.tabControl1.ItemSize = new Size((this.tabControl1.Width / 5 - 1), 0);
            }
            else
            {
                this.tabControl1.ItemSize = new Size((this.tabControl1.Width / 7 - 1), 0);
            }
        }
    }
}
