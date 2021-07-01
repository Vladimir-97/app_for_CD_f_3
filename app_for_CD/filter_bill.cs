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
    public partial class filter_bill : Form
    {
        public filter_bill()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Data_bill.date_from = !Data_bill.date_from;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Data_bill.ser_num = !Data_bill.ser_num;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Data_bill.ser_aggr = !Data_bill.ser_aggr;
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Data_bill.pinfl = !Data_bill.pinfl;
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Data_bill.code_nds = !Data_bill.code_nds;
        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            Data_bill.name = !Data_bill.name;
        } 
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            Data_bill.serv = !Data_bill.serv;
        }
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            Data_bill.inn = !Data_bill.inn;
        }
        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            Data_bill.crp = !Data_bill.crp;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            Data_bill.fio = !Data_bill.fio;
        }
        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            Data_bill.status = !Data_bill.status;
        }
        private void Ok_Click(object sender, EventArgs e)
        {
            Data_bill.its_ok = true;

            if (Data_bill.date_from == true)
            {
                DateTime thisDate_st = dateTimePicker_st.Value;
                DateTime thisDate_end = dateTimePicker_end.Value;
                Data_bill.s_date_from = thisDate_st.ToString("yyyyMMdd");
                Data_bill.s_date_to = thisDate_end.ToString("yyyyMMdd");
            }
            if (Data_bill.ser_num)
            {
                if (textBox8.Text != "")
                    Data_bill.s_ser_num = textBox8.Text;
                else
                {
                    Data_bill.its_ok = false;
                    textBox8.BackColor = Color.Red;
                }
            }
            if (Data_bill.ser_aggr)
            {
                if (textBox1.Text != "")
                    Data_bill.s_ser_aggr = textBox1.Text;
                else
                {
                    Data_bill.its_ok = false;
                    textBox1.BackColor = Color.Red;
                }
            }
            if (Data_bill.crp)
            {
                if (comboBox2.Text != "")
                    Data_bill.s_crp = comboBox2.Text;
                else
                {
                    Data_bill.its_ok = false;
                    comboBox2.BackColor = Color.Red;
                }
            }
            if (Data_bill.inn)
            {
                if (textBox6.Text != "")
                    Data_bill.s_inn = textBox6.Text;
                else
                {
                    Data_bill.its_ok = false;
                    textBox6.BackColor = Color.Red;
                }
            }
            if (Data_bill.pinfl)
            {
                if (textBox2.Text != "")
                    Data_bill.s_pinfl = textBox2.Text;
                else
                {
                    Data_bill.its_ok = false;
                    textBox2.BackColor = Color.Red;
                }
            }
            if (Data_bill.code_nds)
            {
                if (textBox3.Text != "")
                    Data_bill.s_code_nds = textBox3.Text;
                else
                {
                    Data_bill.its_ok = false;
                    textBox3.BackColor = Color.Red;
                }
            }
            if (Data_bill.name)
            {
                if (textBox4.Text != "")
                    Data_bill.s_name = textBox4.Text;
                else
                {
                    Data_bill.its_ok = false;
                    textBox4.BackColor = Color.Red;
                }
            }
            if (Data_bill.serv)
            {
                if (textBox5.Text != "")
                    Data_bill.s_serv = textBox5.Text;
                else
                {
                    Data_bill.its_ok = false;
                    textBox5.BackColor = Color.Red;
                }
            }
            if (Data_bill.fio)
            {
                if (textBox7.Text != "")
                    Data_bill.s_fio = textBox7.Text;
                else
                {
                    Data_bill.its_ok = false;
                    textBox7.BackColor = Color.Red;
                }
            }
            if (Data_bill.status)
            {
                if (comboBox1.Text != "")
                {
                    if (comboBox1.SelectedIndex == 0)
                        Data_bill.s_status = "1";
                    else
                    {
                        Data_bill.s_status = "0";
                        comboBox1.BackColor = Color.Red;
                    }

                }
                else
                {
                    Data_bill.its_ok = false;
                }
            }
            if (Data_bill.its_ok)
            this.Close();
            else
            MessageBox.Show("Неправильно заполнено");

        }

    }
}
