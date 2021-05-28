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
using System.Reflection;


namespace app_for_CD
{
    public partial class UC_Billing : UserControl
    {
        public UC_Billing()
        {
            InitializeComponent();
        }
        OracleConnection con = null;


        private void UC_Billing_Load(object sender, EventArgs e)
        {
           // updatePanel2();
        }
        private void updatePanel2()
        {
            OracleCommand cmd = con.CreateCommand();
            //cmd.CommandText = "SELECT B.DOCU_NO, B.DOCU_SRES, B.DOCU_ISSU_DD, B.DOCU_STAT_CD, A.CRP_CD, A.CRP_NM, DIST_ID_2 FROM TBCB_CRP_INFO A INNER JOIN table_for_docu B ON A.CRP_CD = B.CRP_CD where rownum <= 50";
            //cmd.CommandText = "SELECT DISTINCT c.*, Y.DOCU_PRICE, Y.GET_DD FROM(SELECT B.DOCU_NO, B.DOCU_SRES, B.DOCU_ISSU_DD, B.DOCU_STAT_CD, A.CRP_CD, A.CRP_NM, A.DIST_ID_2, B.CRTE_DT FROM TBCB_CRP_INFO A INNER JOIN table_for_docu B ON A.CRP_CD = B.CRP_CD) c , NEW_TBCB y where c.docu_no = y.docu_no AND C.CRP_CD = Y.CRP_CD and rownum<=100 order by C.DOCU_ISSU_DD";
            cmd.CommandText = "SELECT DISTINCT c.*, Y.DOCU_PRICE, Y.ESTM_NM, Y.FIO FROM(SELECT B.DOCU_NO, B.DOCU_SRES, B.DOCU_ISSU_DD, B.DOCU_STAT_CD, A.CRP_CD, A.CRP_NM, A.DIST_ID_2 FROM TBCB_CRP_INFO A INNER JOIN table_for_docu B ON A.CRP_CD = B.CRP_CD) c , NEW_TBCB y where c.docu_no = y.docu_no AND y.docu_sres = c.docu_sres AND C.CRP_CD = Y.CRP_CD and rownum<=100 order by C.DOCU_ISSU_DD";

            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            List<string[]> data = new List<string[]>();

            //while (dr.Read() == true)
            //{
            //    fill_data(data, dr);
            //}

            //print_data(data);

        }
        void fill_data(List<string[]> data, OracleDataReader dr)
        {

            data.Add(new string[13]);
            data[data.Count - 1][0] = data.Count.ToString(); ///////////Номер поряжковый
            data[data.Count - 1][1] = check_null(dr[0].ToString());      ///////////Номер договора
            data[data.Count - 1][2] = check_null(dr[1].ToString());   /////////// Серия договора

            //if (dr[2].ToString() != "")
            //{
            //    data[data.Count - 1][3] = parse_date(dr[2].ToString()); ;         /////////////// Дата договора
            //}
            //data[data.Count - 1][4] = check_stat(dr[3].ToString());  ////////////////////Статус
            //data[data.Count - 1][5] = check_null(dr[4].ToString());     /////KZL
            //data[data.Count - 1][7] = check_null(dr[5].ToString());     /////Наименование клиента
            //data[data.Count - 1][6] = check_null(dr[6].ToString());     /////ИНН

            //data[data.Count - 1][8] = check_null(check_ser_num(dr[1].ToString()));
            data[data.Count - 1][9] = check_null(dr[7].ToString());
            data[data.Count - 1][10] = check_null(dr[8].ToString());
            data[data.Count - 1][11] = check_null(dr[9].ToString());
            data[data.Count - 1][12] = "";



        }

        //void print_data(List<string[]> data)
        //{
        //    int i = 0;
        //    dataGridView1.Rows.Clear();

        //    foreach (string[] s in data)
        //    {
        //        dataGridView1.Rows.Add(s);
        //        for (int j = 0; j < 12; j++)
        //            if (i % 2 == 0)
        //                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.DarkGray;
        //            else
        //                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Gray;
        //        i++;
        //    }
        //}
        string check_null(string str)
        {
            if (str.Length == 0)
                return "";
            return str;
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

    }
}
