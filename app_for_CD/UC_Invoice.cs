using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace app_for_CD
{
    public partial class UC_Invoice : UserControl
    {
        OracleConnection con = null;
        public UC_Invoice()
        {
            InitializeComponent();
            SetConnection();
            LoadData();
        }
        private void SetConnection()
        {
            string ConnectionString = "USER ID=GGUZDR_APP;PASSWORD=gguzdr_app;DATA SOURCE=10.1.50.12:1521/GDBDRCT1";
            con = new OracleConnection(ConnectionString);
            try
            {
                con.Open();
            }
            catch (Exception e)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, e.Message);

                MessageBox.Show(errorMessage, "Error");
            }
        }

        int find_id()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT max(id) FROM Users_cd ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            return Int32.Parse(dr[0].ToString());

        }

        private void LoadData()
        {
            
            dataGridView_invoice.Rows.Clear();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from registration_of_invoice order by ID, num_of_ser";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            List<string[]> data = new List<string[]>();
            int i = 0;
            int priv = -1;
            while(dr.Read()) {
                
                if (Convert.ToInt32(dr[0]) != priv)
                {
                    dataGridView_invoice.Rows.Add();
                    dataGridView_invoice.Rows[i].Cells[0].Value = dr[0] + " от " + dr[9];
                    dataGridView_invoice.Rows[i].Cells[1].Value = dr[2] + " от " + dr[13]; 
                    dataGridView_invoice.Rows[i].Cells[2].Value = dr[1];
                    dataGridView_invoice.Rows[i].Cells[3].Value = dr[10];
                    if (dr[11] == null || dr[11].ToString() == "")
                    {
                        dataGridView_invoice.Rows[i].Cells[4].Value = "-";
                    }
                    else {
                        dataGridView_invoice.Rows[i].Cells[4].Value = dr[11];
                    }
                    if (dr[12].ToString() == "0")
                    {
                        dataGridView_invoice.Rows[i].Cells[5].Value = dr[8];
                        dataGridView_invoice.Rows[i].Cells[6].Value = "-";
                    }
                    else {
                        dataGridView_invoice.Rows[i].Cells[5].Value = "-";
                        dataGridView_invoice.Rows[i].Cells[6].Value = dr[8];
                    }
                    dataGridView_invoice.Rows[i].Cells[7].Value = dr[3].ToString();
                    dataGridView_invoice.Rows[i].Cells[8].Value = dr[4].ToString();
                    if (dr[15].ToString() == "1")
                    {
                        dataGridView_invoice.Rows[i].Cells[9].Value = "Активный";
                    }
                    else {
                        dataGridView_invoice.Rows[i].Cells[9].Value = "Неактивный";
                    }
                    dataGridView_invoice.Rows[i].Cells[10].Value = dr[14];
                    i++;
                }
                else{

                    dataGridView_invoice.Rows[i - 1].Cells[7].Value = dataGridView_invoice.Rows[i - 1].Cells[7].Value.ToString() + '\n' + dr[3].ToString();
                    dataGridView_invoice.Rows[i - 1].Cells[8].Value = (dataGridView_invoice.Rows[i - 1].Cells[8].Value).ToString() + '\n' + (Convert.ToDouble(dr[4])).ToString();
                }
                
                priv = Convert.ToInt32(dr[0]);

            }
            
            for (int row = 0; row <= dataGridView_invoice.Rows.Count - 1; row++)
            {
                ((DataGridViewImageCell)dataGridView_invoice.Rows[row].Cells[11]).Value = Properties.Resources.change;
            }
            
            dr.Close();
        }

        private void add_Click(object sender, EventArgs e)
        {
            registration_of_an_invoice r = new registration_of_an_invoice();
            r.StartPosition = FormStartPosition.CenterParent;
            r.ShowDialog();
        }
        
        private void update_Click(object sender, EventArgs e)
        {
            LoadData();
        }


        private void dataGridView_invoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string num = "";
            
            var senderGrid = (DataGridView)sender;
            try
            {
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
                {
                    string str = dataGridView_invoice.Rows[e.RowIndex].Cells[0].Value.ToString();
                    for (int i = 0; i < str.Count(); i++)
                    {
                        if (str[i] != ' ')
                        {
                            num = num + str[i];
                        }
                        else
                        {
                            break;
                        }
                    }
                    registration_of_an_invoice r = new registration_of_an_invoice(num);
                    r.StartPosition = FormStartPosition.CenterParent;
                    r.ShowDialog();
                }
            }
            catch { 
            
            }
        }
    }
}
