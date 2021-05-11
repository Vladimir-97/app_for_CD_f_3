using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app_for_CD
{
    public partial class UC_Invoice : UserControl
    {
        public UC_Invoice()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, EventArgs e)
        {
            registration_of_an_invoice r = new registration_of_an_invoice();
            r.StartPosition = FormStartPosition.CenterParent;
            r.ShowDialog();
        }

        private void update_Click(object sender, EventArgs e)
        {
            string s = "1!";
            dataGridView_invoice.Rows.Add(s);
            s = "Первый Услуг! \n Второй услуга!";
            dataGridView_invoice.Rows.Add(s);

        }

        private void dataGridView_invoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
