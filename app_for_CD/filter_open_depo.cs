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
    public partial class filter_open_depo : Form
    {
        public filter_open_depo()
        {
            InitializeComponent();
            this.SetConnection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.SetConnection();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CRP_CD FROM TBCB_CRP_INFO where rownum <=1000";


            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
        }
        OracleConnection con = null;

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
        private void CloseConnection()
        {
            con.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.f_d = true;
            }
            else
            {
                Data.f_d = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.f_CRP = true;
            }
            else
            {
                Data.f_CRP = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {

                Data.f_n = true;
            }
            else
            {
                Data.f_n = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {

                Data.f_fio = true;
            }
            else
            {
                Data.f_fio = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data.it_ok = true;
            // for create new
            if (Data.f_d == true)
            {
                DateTime thisDate_st = dateTimePicker_st.Value;
                DateTime thisDate_end = dateTimePicker_end.Value;
                Data.st_date_orig = thisDate_st.ToString("yyyyMMdd");
                Data.end_date_orig = thisDate_end.ToString("yyyyMMdd");
            }
            if (Data.f_CRP == true)
            {
                Data.number_ser = comboBox1.Text.ToString();
            }
            if (Data.f_n == true)
            {
                Data.name_cl = textBox3.Text;
            }

            if (Data.f_fio == true)
            {
                Data.filter_fio = textBox4.Text;
            }
            this.Close();
        }
    }
}
