using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace app_for_CD
{
    public partial class List_of_services : Form
    {
        public List_of_services()
        {
            InitializeComponent();
            this.SetConnection();
        }
        public List_of_services(string ser, string values)
        {
            InitializeComponent();
            this.SetConnection();
            this.series = ser;
            string tmp = ser;
            tmp += "- (" + values + ")";
            textBox1.Text = tmp;
            textBox1.Enabled = false;
            dataGridView1.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            button2.Enabled = false;
        }
        OracleConnection con = null;
        string series = "", services = "",nds  ="", brv = "";

        private void List_of_services_Load(object sender, EventArgs e)
        {
            load_services();
        }
        private void load_services()
        {
            string tmp_ser = series + "%";
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("SER", tmp_ser);
            cmd.CommandText = "select cd_nm, nds,actived, count_brv from tbcb_cd where cd like :SER";

            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            List<string[]> data = new List<string[]>();

            while (dr.Read() == true)
            {
                fill_data(data, dr);
            }

            print_data(data);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.CloseConnection();
            this.Close();
        }

        void fill_data(List<string[]> data, OracleDataReader dr)
        {

            data.Add(new string[6]);
            data[data.Count - 1][0] = data.Count.ToString(); ///////////Номер поряжковый
            data[data.Count - 1][1] = dr[0].ToString();      /////////// Наименование услуги
            data[data.Count - 1][2] = dr[1].ToString() + "%";   /////////// НДС
            data[data.Count - 1][3] = dr[3].ToString();   /////////// БРВ
            data[data.Count - 1][4] = parse_activ(dr[2].ToString());   /////////// Активно
        }
        void print_data(List<string[]> data)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            foreach (string[] s in data)
            {
                if (app_for_CD.Properties.Settings.Default["Theme"].ToString() != "False")
                {
                    dataGridView1.ForeColor = Color.White;
                    dataGridView1.Rows.Add(s);
                    if (i % 2 == 0)
                        for (int j = 0; j < 5; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(89, 89, 89);
                        }
                    else
                        for (int j = 0; j < 5; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(128, 128, 128);
                        }
                    i++;
                }
                else
                {
                    dataGridView1.ForeColor = Color.Black;
                    dataGridView1.Rows.Add(s);
                }
            }
        }

        string parse_activ(string tmp)
        {
            if (tmp == "1")
            {
                return "V";
            }
            else return "O";
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentRow.Index;
            if (dataGridView1.SelectedCells.Count > 1)
            {
                services = dataGridView1.Rows[row].Cells[1].Value.ToString();
                nds = dataGridView1.Rows[row].Cells[2].Value.ToString();
                nds = nds.Remove(nds.Length - 1);
                brv = dataGridView1.Rows[row].Cells[3].Value.ToString();
                button2.Enabled = true;
                //series = dataGridView1.Rows[row].Cells[0].Value.ToString();
            }
        }

        private void List_of_services_Activated(object sender, EventArgs e)
        {
            button2.Enabled = false;
            load_services();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add_service add_svc = new Add_service(nds,services,series, brv);
            add_svc.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Add_service add_svc = new Add_service(series);
            add_svc.Show();
        }

        private void CloseConnection()
        {
            con.Close();
        }

    }
}
