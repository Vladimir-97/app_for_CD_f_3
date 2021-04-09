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
using System.Security.Cryptography;
using app_for_CD.Properties;

namespace app_for_CD
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
            SetConnection();
            textBox1.Text = Settings.Default["LogName"].ToString();
        }
        static string GetHash(string plaintext)
        {
            var sha = new SHA1Managed();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(plaintext));
            return Convert.ToBase64String(hash);
        }
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    Data.exit = true;
        //    string name = textBox1.Text;
        //    string pass = textBox2.Text;
        //    OracleCommand cmd = con.CreateCommand();
        //    cmd.Parameters.Add(new OracleParameter("LOGIN", name));
        //    if (name == "admin")
        //    {
        //        cmd.Parameters.Add(new OracleParameter("PASSW", pass));
        //    }
        //    else
        //    {
        //        cmd.Parameters.Add(new OracleParameter("PASSW", GetHash(pass)));
        //    }
        //    cmd.CommandText = "select * from users_cd where login = :LOGIN and PASSWORD = :PASSW";
        //    cmd.CommandType = CommandType.Text;
        //    OracleDataReader dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        while (dr.Read() == true)
        //        {
        //            fill_data(dr);
        //        }
        //        Settings.Default["LogName"] = textBox1.Text;
        //        Settings.Default.Save();
        //    }

        //    CloseConnection();
        //    this.Close();
        //}
        private void fill_data(OracleDataReader dr)
        {
            Data.login = 1;
            string tmp_str;
            tmp_str = dr[3].ToString();
            Data.role = Int32.Parse(tmp_str);
            Data.status_t = Int16.Parse(dr[4].ToString());

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
            Data.exit = false;
            this.Close();
        }

        //private void textBox1_Click(object sender, EventArgs e)
        //{
        //    if (textBox1.Text == "  Имя пользователя")
        //    {
        //        textBox1.Text = "";
        //    }
        //}

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.PasswordChar = '*';
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Data.exit = true;
            string name = textBox1.Text;
            string pass = textBox2.Text;
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("LOGIN", name));
            if (name == "admin")
            {
                cmd.Parameters.Add(new OracleParameter("PASSW", pass));
            }
            else
            {
                cmd.Parameters.Add(new OracleParameter("PASSW", GetHash(pass)));
            }
            cmd.CommandText = "select * from users_cd where login = :LOGIN and PASSWORD = :PASSW";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read() == true)
                {
                    fill_data(dr);
                }
                Settings.Default["LogName"] = textBox1.Text;
                Settings.Default.Save();
            }

            CloseConnection();
            this.Close();
        }
    }
}
