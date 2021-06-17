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
    public partial class sum_for_pay : Form
    {
        OracleConnection con = null;
        String cur_id;
        double sum = 1;
        public sum_for_pay(string ID)
        {
            InitializeComponent();
            SetConnection();
            cur_id = ID;
            OracleCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = $"select SUM_T from REGISTRATION_OF_INVOICE where id = {cur_id}";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            sum = double.Parse(dr[0].ToString());
            dr.Close();
        }
        public int DS_Count(string s)
        {
            string substr = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0].ToString();
            int count = (s.Length - s.Replace(substr, "").Length) / substr.Length;
            return count;
        }
        private void textBox_Sum_KeyPress(object sender, KeyPressEventArgs e)
        {
            var DS = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            var minus = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NegativeSign[0];
            if (e.KeyChar == DS && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
            else if(e.KeyChar == minus && ((TextBox)sender).Text.Length == 0)
                e.Handled = false;
            else
                e.Handled = !(Char.IsDigit(e.KeyChar) || ((e.KeyChar == DS) && (DS_Count(((TextBox)sender).Text) < 1)) || e.KeyChar == 8);
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
        private void CloseConnection()
        {
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            double num_input = double.Parse(textBox1.Text);
            if (textBox1.Text == null || textBox1.Text == "")  {
                MessageBox.Show("Введите сумму!");
            }
            else if (textBox1.Text == "0")
            {
                MessageBox.Show("Недопустимое значение!");
            }
            else if (num_input < sum)
            {
                OracleCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = $"UPDATE REGISTRATION_OF_INVOICE SET SUM_PAID = {num_input.ToString(nfi)} where id = {cur_id}";
                cmd.ExecuteNonQuery();
                Data.yes = true;
                CloseConnection();
                this.Close();
            }
            else
            {
                MessageBox.Show("Сумма больше либо равна чем выставленна!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Data.yes = false;
            this.Close();
        }
    }
}
