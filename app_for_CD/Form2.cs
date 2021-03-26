using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace app_for_CD
{
    /// <summary>
    /// закрой connect
    /// </summary>

    public partial class filter : Form
    {
        OracleConnection con = null;
        public filter()
        {
            InitializeComponent();
            this.SetConnection();
            comboBox4.MaxLength = 12;
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
        bool is_empty_str(string str)
        {
            if (str == "")
                return true;
            else return false;
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            string crp = comboBox4.Text.ToString();
            
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 12).Value = crp;
            cmd.CommandText = "SELECT CRP_NM FROM TBCB_CRP_INFO where CRP_CD = :KZL";
            //kzl_ = crp;
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox6.Text = dr[0].ToString();
            }
            //comboBox5.Items.Clear();
        }
           
        private void CRM_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.f_n = 1;
            }
            else
            {
                Data.f_n = 0;
            }
        }

        private void Серия_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.f_s = 1;
            }
            else
            {
                Data.f_s = 0;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.f_d = 1;
            }
            else
            {
                Data.f_d = 0;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.f_p = 1;
            }
            else
            {
                Data.f_p = 0;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.SetConnection();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CRP_CD FROM TBCB_CRP_DOCU_INFO where rownum <=1000";


            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            // List<string[]> data = new List<string[]>();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr[0].ToString());
            }
        }
        private void Ok_Click(object sender, EventArgs e)
        {
            Data.it_ok = true;
            if (Data.f_d == 1)
            {
                DateTime thisDate_st = dateTimePicker1.Value;
                DateTime thisDate_end = dateTimePicker2.Value;
                Data.st_date_orig = thisDate_st.ToString("yyyyMMdd").ToString();
                Data.end_date_orig = thisDate_end.ToString("yyyyMMdd").ToString();
            }
            if (Data.f_s == 1)
            {
                Data.number_ser = comboBox4.Text.ToString();
            }
            if (Data.f_n == 1)
            {
                Data.name_cl = textBox1.Text;
            }
            if (Data.f_p == 1)
            {
                Data.price = textBox3.Text;
                Data.val = comboBox6.Text.ToString();
            }
            if (Data.f_i == 1)
            {
                Data.isch = comboBox2.Text.ToString();
            }
            this.Close();
        }

        private void CloseConnection()
        {
            con.Close();
        }

        private void filter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Data.it_ok != true)
            {
                Data.f_d = 0;
                Data.f_s = 0;
                Data.f_n = 0;
                Data.f_p = 0;
                Data.f_i = 0;
                CloseConnection();
            }
            if ((Data.it_ok == true)  && ( (Data.name_cl == "" && Data.f_n == 1) || (Data.number_ser == "" && Data.f_s == 1) || (Data.price == "" && Data.name_cl == "" && Data.f_p == 1) || (Data.isch == "" && Data.f_i == 1)))
            {
                e.Cancel = true;
                Data.it_ok = false;
                MessageBox.Show("Заполните все строки!");
            }
            
            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.f_i = 1;
            }
            else
            {
                Data.f_i = 0;
            }
        }
    }
}
