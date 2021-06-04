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
    public partial class Add_service : Form
    {
        public Add_service()
        {
            InitializeComponent();
        }
        public Add_service(string series)
        {
            InitializeComponent();
            this.SetConnection();
            label3.Visible = false;
            button2.Visible = false;
            textBox2.Text = series;
            textBox2.Enabled = false;

        }
        public Add_service(string nds, string service,string series)
        {
            InitializeComponent();
            this.SetConnection();
            textBox1.Text = service;
            textBox1.Enabled = false;
            textBox2.Text = series;
            textBox1.Enabled = false;
            textBox3.Text = nds;
            label3.Visible = false;
            button3.Visible = false;
        }
       // string series, service = "";
        OracleConnection con = null;
        private void button2_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = con.CreateCommand();
            if (textBox3.Text.Length > 0 )
            {
                cmd.Parameters.Add(new OracleParameter("NDS", textBox3.Text));
                cmd.Parameters.Add(new OracleParameter("USED", parse_svc(comboBox1.Text)));
              
                
                cmd.Parameters.Add(new OracleParameter("SERV", textBox1.Text));

                cmd.CommandText = "update tbcb_cd set nds = :NDS, Actived = :USED where cd_NM = :SERV";
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

        private void button3_Click(object sender, EventArgs e)
        {
            save_service();
        }
        private void save_service()
        {
            int max_ser = find_max();
            ++max_ser;
            string cd = textBox2.Text + max_ser.ToString();
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("SERS", cd));
            cmd.Parameters.Add(new OracleParameter("SERV1", textBox1.Text));
            cmd.Parameters.Add(new OracleParameter("SERV2", textBox1.Text));

            cmd.Parameters.Add(new OracleParameter("NDS", textBox3.Text));
            cmd.Parameters.Add(new OracleParameter("USED", parse_svc(comboBox1.Text)));



            cmd.CommandText = "insert into tbcb_cd (cd_grp_no, cd, lang_cd, cd_nm, cd_shrt_nm, nds, actived) values('000037', :SERS, 'UZ', :SERV1, :SERV2, :NDS, :USED ) ";
            cmd.CommandType = CommandType.Text;   ///issu_dd yyyymmdd        reg_docu dd.mm.yyyy
            if (cmd.ExecuteNonQuery() == 1)
            {
                label3.Visible = true;
            }
        }
        int find_max()
        {
            OracleCommand cmd = con.CreateCommand();
            string tmp = textBox2.Text;
            tmp += "%";
            cmd.Parameters.Add(new OracleParameter("SERS", tmp));

            cmd.CommandText = "select count(*) from tbcb_cd where cd like :SERS";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                if (dr.HasRows)
                {
                    tmp = dr[0].ToString();
                    return Int16.Parse(dr[0].ToString());
                }
            return 0;
        }
        private string parse_svc(string tmp)
        {
            if (tmp == "Активна")
                return "1";
            else
            return "0";
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
        private void CloseConnection()
        {
            con.Close();
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
            if (e.KeyChar == DS && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
            else
                e.Handled = !(Char.IsDigit(e.KeyChar) || ((e.KeyChar == DS) && (DS_Count(((TextBox)sender).Text) < 1)) || e.KeyChar == 8);
        }
    }
}
