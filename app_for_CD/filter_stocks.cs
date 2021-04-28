using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace app_for_CD
{
    public partial class filter_stocks : Form
    {
        public filter_stocks()
        {
            InitializeComponent();
        }
        OracleConnection con = null;
        private void CRP_search_Click(object sender, EventArgs e)
        {
            this.SetConnection();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CRP_CD FROM TBCB_CRP_INFO where rownum <=1000";


            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            // List<string[]> data = new List<string[]>();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
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
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, e.Message);

                MessageBox.Show(errorMessage, "Error");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.fil_date = true;
            }
            else
            {
                Data.fil_date = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.fil_crp1 = true;
            }
            else
            {
                Data.fil_crp1 = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.fil_client1 = true;
            }
            else
            {
                Data.fil_client1 = false;
            }
        }
        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.fil_crp2 = true;
            }
            else
            {
                Data.fil_crp2 = false;
            }
        }
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.fil_client2 = true;
            }
            else
            {
                Data.fil_client2 = false;
            }
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.fil_emitent = true;
            }
            else
            {
                Data.fil_emitent = false;
            }
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.fil_code_stocks = true;
            }
            else
            {
                Data.fil_code_stocks = false;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.fil_name_stocks = true;
            }
            else
            {
                Data.fil_name_stocks = false;
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            Data.it_ok = true;
            if (Data.fil_date == true)
            {
                DateTime thisDate_st = dateTimePicker1.Value;
                DateTime thisDate_end = dateTimePicker2.Value;
                Data.st_date_orig = thisDate_st.ToString("yyyyMMdd");
                Data.end_date_orig = thisDate_end.ToString("yyyyMMdd");
            }
            if (Data.fil_crp1 == true)
            {
                Data.crp_str1 = comboBox1.Text;
            }
            if (Data.fil_crp2 == true)
            {
                Data.crp_str2 = comboBox2.Text;
            }
            if (Data.fil_client1)
                Data.client_str1 = textBox1.Text;
            if (Data.fil_client2)
                Data.client_str2 = textBox6.Text;
            if (Data.fil_emitent)
                Data.emitent_str = textBox2.Text;
            if (Data.fil_code_stocks)
                Data.code_stock_str = textBox3.Text;
            if (Data.fil_name_stocks)
                Data.name_stock_str = textBox4.Text;

            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
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


        //private void checkBox7_CheckedChanged(object sender, EventArgs e)        ////////////fio 
        //{
        //    CheckBox checkBox = (CheckBox)sender;
        //    if (checkBox.Checked == true)
        //    {
        //        Data.f_d = true;
        //    }
        //    else
        //    {
        //        Data.f_d = false;
        //    }
        //}
    }
}
