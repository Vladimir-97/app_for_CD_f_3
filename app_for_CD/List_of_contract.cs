using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace app_for_CD
{
    public partial class List_of_contract : Form
    {
        
        public List_of_contract()
        {
            InitializeComponent();
            this.SetConnection();
            button2.FlatAppearance.BorderColor = Color.White;
            button2.FlatAppearance.BorderSize = 1;
            button3.FlatAppearance.BorderColor = Color.White;
            button3.FlatAppearance.BorderSize = 1;
            dataGridView1.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            button2.Enabled = false;
            button4.Enabled = false;

        }
        OracleConnection con = null;
        string ser="", val="";
        bool changed = false;
        private void button1_Click(object sender, EventArgs e)
        {
            this.CloseConnection();
            this.Close();
        }
        private void List_of_contract_Load(object sender, EventArgs e)
        {
            load_contract();
        }
        private void load_contract()
        {
            button2.Enabled = false;
            button4.Enabled = false;
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from series_of_docu";

            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            List<string[]> data = new List<string[]>();

            while (dr.Read() == true)
            {
                fill_data(data, dr);
            }
            
            print_data(data);
        }
        void fill_data(List<string[]> data, OracleDataReader dr)
        {

            data.Add(new string[3]);
            data[data.Count - 1][0] = data.Count.ToString(); ///////////Номер поряжковый
            data[data.Count - 1][1] = dr[0].ToString();      /////////// Серия договора
            data[data.Count - 1][2] = dr[1].ToString();   /////////// Значение в серии
        }
        void print_data(List<string[]> data)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            foreach (string[] s in data)
            {
                if (app_for_CD.Properties.Settings.Default["Theme"].ToString() != "False")
                {

                    dataGridView1.Rows.Add(s);
                    for (int j = 0; j < 3; j++)
                        if (i % 2 == 0)
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(89, 89, 89);
                        else
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(128, 128, 128);
                    i++;
                }
                else
                {
                    dataGridView1.ForeColor = Color.Black;
                    dataGridView1.Rows.Add(s);

                }
            }
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
                string errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, e.Message);
            }
        }
        private void CloseConnection()
        {
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add_docu_series add = new Add_docu_series(1, ser, val);
            add.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Add_docu_series add = new Add_docu_series(0, ser, val);
            add.Show();
        }

        private void List_of_contract_Activated(object sender, EventArgs e)
        {
            load_contract();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List_of_services los = new List_of_services(ser,val);
            los.Show();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentRow.Index;
            if (dataGridView1.SelectedCells.Count > 1)
            {
                ser = dataGridView1.Rows[row].Cells[1].Value.ToString();
                val = dataGridView1.Rows[row].Cells[2].Value.ToString();
                //changed = true;
                button2.Enabled = true;
                button4.Enabled = true;
            }
        }
    }
}
