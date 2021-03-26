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
using Oracle.DataAccess.Types;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace app_for_CD
{
    public partial class Add_user : Form
    {
        OracleConnection con = null;
        public Add_user()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            Set_Connection();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT login FROM Users_cd";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while( dr.Read() )
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            this.CloseConnection();
        }

        private void Set_Connection()
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
            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Пароли не совпадают");
            }
            else
            {
                Set_Connection();
                OracleCommand cmd = con.CreateCommand();
                cmd.Parameters.Add(new OracleParameter("LOGIN", comboBox1.SelectedItem));

                cmd.CommandText = "SELECT * FROM Users_cd where login = :LOGIN";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OracleParameter("PASS", textBox2.Text));
                    cmd.Parameters.Add(new OracleParameter("LOGIN", comboBox1.SelectedItem));
                    cmd.CommandText = "update users_cd set password = :PASS where login = :LOGIN " ;
                    cmd.CommandType = CommandType.Text;
                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        MessageBox.Show("Успешно");
                    }
                }
                else
                {
                    int id = find_id();
                    id++;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OracleParameter("PASS", textBox2.Text));
                    cmd.Parameters.Add(new OracleParameter("LOGIN", comboBox1.Text));
                    cmd.Parameters.Add(new OracleParameter("ID", id));
                    cmd.CommandText = "insert into users_cd (password, login, id) values (:PASS, :LOGIN, :ID)";
                    cmd.CommandType = CommandType.Text;
                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        MessageBox.Show("Успешно");
                    }
                }
                this.CloseConnection();
            } 
        }
        int find_id()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT max(id) FROM Users_cd ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            return Int32.Parse( dr[0].ToString() );

        }

    }
}
