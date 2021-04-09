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
        public Add_docu_series()
        {
            InitializeComponent();
            SetConnection();
            label3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("SRES", textBox2.Text) );
            cmd.Parameters.Add(new OracleParameter("VALUE", textBox1.Text) );

            cmd.CommandText = "insert into series_of_docu (docu_sres, value_of_sres) values (:SRES, :VALUE) ";
            cmd.CommandType = CommandType.Text;   ///issu_dd yyyymmdd        reg_docu dd.mm.yyyy
            if (cmd.ExecuteNonQuery() == 1)
            {
                label3.Visible = true;
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
