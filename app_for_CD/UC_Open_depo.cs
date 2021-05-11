using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Excel = Microsoft.Office.Interop.Excel;
/// <summary>
/// ///////////////////open_change_depo
/// </summary>

namespace app_for_CD
{
    public partial class UC_Open_depo : UserControl
    {
        public UC_Open_depo()
        {
            InitializeComponent();
            SetConnection();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Reg_depo rg = new Reg_depo();
            rg.Show();
        }

        private void UC_Open_depo_Load(object sender, EventArgs e)
        {
            updatePanel2();


        }
        private void updatePanel2()
        {
            
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select distinct * from open_change_depo";

            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            List<string[]> data = new List<string[]>();

            while (dr.Read() == true)
            {
                fill_data(data, dr);
            }

            print_data(data);

        }
        void fill_data(List<string[]> data, OracleDataReader dr)
        {

            data.Add(new string[13]);
            data[data.Count - 1][0] = data.Count.ToString(); ///////////Номер поряжковый
            data[data.Count - 1][1] = parse_date(check_null(dr[1].ToString()  )  );      ///////////Номер договора
            data[data.Count - 1][2] = check_null(dr[2].ToString());   /////////// Серия договора
            data[data.Count - 1][3] = find_company(dr[2].ToString()  );
            data[data.Count - 1][4] = check_null(dr[3].ToString());   /////////// Серия договора
        }
        void print_data(List<string[]> data)
        {
            int i = 0;
            dataGridView1.Rows.Clear();

            foreach (string[] s in data)
            {
                dataGridView1.Rows.Add(s);
                for (int j = 0; j < 5; j++)
                    if (i % 2 == 0)
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.DarkGray;
                    else
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Gray;
                i++;
            }
        }
        string check_null(string str)
        {
            if (str.Length == 0)
                return "";
            return str;
        }
        string parse_date(string tmp)
        {
            string norm_vid_date = "";
            if (tmp == "")
                return "";
            norm_vid_date = tmp[6].ToString() + tmp[7] + ".";
            norm_vid_date += tmp[4].ToString() + tmp[5] + ".";
            norm_vid_date += tmp[0].ToString() + tmp[1] + tmp[2] + tmp[3];
            return norm_vid_date;
        }

        string find_company(string tmp_str)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", tmp_str));

            cmd.CommandText = "SELECT CRP_NM FROM TBCB_CRP_INFO where crp_cd = :KZL";


            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return dr[0].ToString();
            }
            return "";
        }
    }
}
