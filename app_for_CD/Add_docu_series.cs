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
    public partial class Add_docu_series : Form
    {
        OracleConnection con = null;
        int type_of_button = 0;
        public Add_docu_series()
        {
            InitializeComponent();
            SetConnection();
            label3.Visible = false;
        }
        public Add_docu_series(int i, string ser, string val)
        {
            InitializeComponent();
            SetConnection();
            label3.Visible = false;
            type_of_button = i;
            //if (i == 0)
            //{
            //    button2.Text = "Сохранить";
            //}
            //else
            //{
            //    button2.Text = "Изменить";
            //    textBox2.Enabled = false;
            //}
            textBox2.Text = ser;
            textBox1.Text = val;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = con.CreateCommand();
            if (type_of_button == 0 )
            {
                if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
                {
                    cmd.Parameters.Add(new OracleParameter("SRES", textBox2.Text));
                    cmd.Parameters.Add(new OracleParameter("VALUE", textBox1.Text));

                    cmd.CommandText = "insert into series_of_docu (docu_sres, value_of_sres) values (:SRES, :VALUE) ";
                    cmd.CommandType = CommandType.Text;   ///issu_dd yyyymmdd        reg_docu dd.mm.yyyy
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        label3.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                }
            }
            else
            {
                if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
                {
                    cmd.Parameters.Add(new OracleParameter("VALUE", textBox1.Text));
                    cmd.Parameters.Add(new OracleParameter("SRES", textBox2.Text));

                    cmd.CommandText = "update series_of_docu set value_of_sres = :VALUE where docu_sres = :SRES ";
                    cmd.CommandType = CommandType.Text;   ///issu_dd yyyymmdd        reg_docu dd.mm.yyyy
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        label3.Visible = true;
                    }
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


    }
}
