using System;
using System.Windows;
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
    public partial class UC_Contract : UserControl
    {
        public UC_Contract()
        {
            this.SetConnection();
            InitializeComponent();
            panel2.Width = 3000;
            if (Data.role == 0)
            {
                button3.Visible = false;
                button1.Visible = false;
            }
            dataGridView1.Font = new Font("Times New Roman", 10, FontStyle.Bold);
           // MessageBox.Show(app_for_CD.Properties.Settings.Default["Theme"].ToString());

        }
        void button_enabled()
        {
            button5.Enabled = true;
            // button3.Enabled = true;
        }
        void button_disabled()
        {
            button5.Enabled = false;
            //button3.Enabled = false;
        }
        OracleConnection con = null;
        string doc_num, ser_num, date_agg, state, kzl;

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentRow.Index;
            if (dataGridView1.SelectedCells.Count > 1)
            {
                doc_num = dataGridView1.Rows[row].Cells[1].Value.ToString();
                ser_num = dataGridView1.Rows[row].Cells[2].Value.ToString();
                date_agg = dataGridView1.Rows[row].Cells[3].Value.ToString();
                state = dataGridView1.Rows[row].Cells[4].Value.ToString();
                kzl = dataGridView1.Rows[row].Cells[5].Value.ToString();
                // crp_cd = dataGridView1.Rows[row].Cells[6].Value.ToString();
                // dataGridView1.ForeColor = Color.Red;
                // dataGridView1.GridColor = Color.Green;
                button_enabled();
            }
            else
            {
                button_disabled();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Add add = new Add(doc_num, ser_num, date_agg, state, kzl, 2);
            add.Show();
        }
        string check_stat(string tmp_str)
        {
            if (tmp_str == "1")
            {
                return "действующий документ";
            }
            if (tmp_str == "2")
            {
                return "недействительный документ";
            }
            if (tmp_str == "3")
            {
                return "формируется";
            }
            if (tmp_str == "4")
            {
                return "блокированный документ";
            }
            if (tmp_str == "5")
            {
                return "нераспознанный документ";
            }
            return "";
        }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {


        }
        string check_ser_num(string tmp_str)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("SER", tmp_str);

            cmd.CommandText = "Select value_of_sres from series_of_docu where docu_sres = :SER";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                return dr[0].ToString();
            }
            return "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int is_delete = query_delete_from_docu_info();
            int is_delete1 = query_delete_from_NEW_TBCB();

            if (is_delete > 0 && is_delete1 > 0)
            {
                MessageBox.Show("Запись удалена");
                updatePanel2();
            }
            else
                MessageBox.Show("Запись не удалена, проверьте соединение с БД");

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
        /// ///////////////////////////////////////////////////////// Конец удаления записи ///////////////////////////////
        void fill_data(List<string[]> data, OracleDataReader dr)
        {

            data.Add(new string[13]);
            data[data.Count - 1][0] = data.Count.ToString(); ///////////Номер поряжковый
            data[data.Count - 1][1] = check_null(dr[0].ToString());      ///////////Номер договора
            data[data.Count - 1][2] = check_null(dr[1].ToString());   /////////// Серия договора

            if (dr[2].ToString() != "")
            {
                data[data.Count - 1][3] = parse_date(dr[2].ToString()); ;         /////////////// Дата договора
            }
            data[data.Count - 1][4] = check_stat(dr[3].ToString());  ////////////////////Статус
            data[data.Count - 1][5] = check_null(dr[4].ToString());     /////KZL
            data[data.Count - 1][7] = check_null(dr[5].ToString());     /////Наименование клиента
            data[data.Count - 1][6] = check_null(dr[6].ToString());     /////ИНН

            data[data.Count - 1][8] = check_null(check_ser_num(dr[1].ToString()));
            data[data.Count - 1][9] = check_null(dr[7].ToString());
            data[data.Count - 1][10] = check_null(dr[8].ToString());
            data[data.Count - 1][11] = check_null(dr[9].ToString());
            data[data.Count - 1][12] = "";



        }
        //////// 9 значений //////////////////////
        /// <summary>
        /// ///////////////////////////////////////////////// ПЕЧАТЬ НА ЭКРАН  //////////////////////////////////////////////

        void print_data(List<string[]> data)
        {
            int i = 0;
            dataGridView1.Rows.Clear();

            foreach (string[] s in data)
            {
                if (app_for_CD.Properties.Settings.Default["Theme"].ToString() != "False")
                {
                    
                    dataGridView1.Rows.Add(s);
                    for (int j = 0; j < 12; j++)
                        if (i % 2 == 0)
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(89, 89, 89);
                        else
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(128, 128, 128);
                    i++;
                }
                else
                {
                    dataGridView1.ForeColor = Color.Black;
                    dataGridView1.Rows.Add(s);

                }
            }
        }



        /// ///////////////////////////////////////////////// КОНЕЦ  ПЕЧАТИ НА ЭКРАН  //////////////////////////////////////////////

        private void Form_agreement_Load(object sender, EventArgs e)
        {
            updatePanel2();
            button_disabled();
        }
        private void updatePanel2()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT c.*, Y.DOCU_PRICE, Y.ESTM_NM, Y.FIO FROM(SELECT B.DOCU_NO, B.DOCU_SRES, B.DOCU_ISSU_DD, B.DOCU_STAT_CD, A.CRP_CD, A.CRP_NM, A.DIST_ID_2 FROM TBCB_CRP_INFO A INNER JOIN table_for_docu B ON A.CRP_CD = B.CRP_CD) c , NEW_TBCB y where c.docu_no = y.docu_no AND y.docu_sres = c.docu_sres AND C.CRP_CD = Y.CRP_CD and rownum<=100 order by C.DOCU_ISSU_DD";

            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            List<string[]> data = new List<string[]>();

            while (dr.Read() == true)
            {
                fill_data(data, dr);
            }

            print_data(data);

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

        private void button2_Click(object sender, EventArgs e)
        {
            Add add = new Add("", "", "", "", "", 1);
            add.Show();
            updatePanel2();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            updatePanel2();
        }


        int query_delete_from_docu_info()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", kzl));
            cmd.Parameters.Add(new OracleParameter("NUM_DOCU", doc_num));
            cmd.Parameters.Add(new OracleParameter("SER_DOCU", ser_num));

            cmd.CommandText = "DELETE from table_for_docu WHERE crp_cd = :KZL AND DOCU_NO = :NUM_DOCU AND DOCU_SRES = :SER_DOCU";
            cmd.CommandType = CommandType.Text;
            return cmd.ExecuteNonQuery();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            filter f = new filter();
            f.ShowDialog();

            if (Data.f_n == true || Data.f_CRP == true || Data.f_d == true || Data.f_p == true || Data.f_i == true || Data.f_inn == true || Data.f_ser == true || Data.f_status == true)
            {
                string request = "";
                string name_cl = "";

                OracleCommand cmd = con.CreateCommand();
                if (Data.f_d == true)
                {
                    request = $" AND DOCU_ISSU_DD  >= '{Data.st_date_orig}'  AND DOCU_ISSU_DD <= '{Data.end_date_orig}' ";
                }
                if (Data.f_CRP == true)
                {
                    request = request + $" AND C.CRP_CD = {Data.number_ser} ";
                }
                if (Data.f_n == true)
                {
                    for (int i = 0; i < Data.name_cl.Length; i++)
                    {
                        if (Data.name_cl[i] == '%')
                        {
                            name_cl += '_';
                        }
                        else
                        {
                            name_cl += Data.name_cl[i];
                        }
                    }
                    request = request + $" AND C.CRP_NM LIKE '%{name_cl}%' ";
                }
                if (Data.f_p == true)
                {
                    request = request + $" AND y.DOCU_PRICE = '{Data.price}' AND y.CURRENCY = '{Data.val}'";
                }
                if (Data.f_i == true)
                {
                    request = request + $" AND y.ESTM_NM = '{Data.isch}'";
                }
                if (Data.f_inn == true)
                {
                    request = request + $" AND AND c.DIST_ID_2 = '{Data.INN}'";
                }
                if (Data.f_ser == true)
                {
                    request = request + $" AND c.DOCU_SRES = '{Data.ser}'";
                }
                if (Data.f_status == true)
                {
                    request = request + $" AND c.DOCU_STAT_CD = '{Data.status}'";
                }

                cmd.CommandText = "SELECT DISTINCT c.*, Y.DOCU_PRICE, Y.ESTM_NM, Y.FIO FROM(SELECT B.DOCU_NO, B.DOCU_SRES, B.DOCU_ISSU_DD, B.DOCU_STAT_CD, A.CRP_CD, A.CRP_NM, A.DIST_ID_2 FROM TBCB_CRP_INFO A INNER JOIN table_for_docu B ON A.CRP_CD = B.CRP_CD) c , NEW_TBCB y where c.docu_no = y.docu_no AND y.docu_sres = c.docu_sres AND C.CRP_CD = Y.CRP_CD  and rownum <=100" + request + "order by C.DOCU_ISSU_DD ";

                bool find_val = false;
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                List<string[]> data = new List<string[]>();
                while (dr.Read())
                {
                    find_val = true;
                    fill_data(data, dr);
                }
                if (find_val)
                {
                    MessageBox.Show("Найдено!");
                }
                else
                {
                    MessageBox.Show("Не найдено по данному запросу!");
                }
                print_data(data);
                
            }


            Data.f_n = false;
            Data.f_CRP = false;
            Data.f_d = false;
            Data.f_p = false;
            Data.f_i = false;
            Data.f_inn = false;
            Data.f_ser = false;
            Data.f_status = false;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            registration_of_an_invoice f = new registration_of_an_invoice();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List_of_contract lof = new List_of_contract();
            lof.Show();
        }
        int query_delete_from_NEW_TBCB()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", kzl));
            cmd.Parameters.Add(new OracleParameter("NUM_DOCU", doc_num));
            cmd.CommandText = "DELETE from NEW_TBCB WHERE crp_cd = :KZL AND DOCU_NO = :NUM_DOCU";
            cmd.CommandType = CommandType.Text;
            return cmd.ExecuteNonQuery();
        }



        void reset()
        {
            updatePanel2();
        }



        string check_null(string str) 
        {
            if (str.Length == 0)
                return "";
            return str;
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;

            try
            {
                //Start Excel and get Application object.
                oXL = new Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                oSheet.Name = "Информация по договорам";
                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "№";
                oSheet.Cells[1, 2] = "Номер договора";
                oSheet.Cells[1, 3] = "Серия договора";
                oSheet.Cells[1, 4] = "Дата договора";
                oSheet.Cells[1, 5] = "Статус";
                oSheet.Cells[1, 6] = "КЗЛ";
                oSheet.Cells[1, 7] = "ИНН";
                oSheet.Cells[1, 8] = "Наименование клиента";
                oSheet.Cells[1, 9] = "Вид услуги";
                oSheet.Cells[1, 10] = "Цена договора";
                oSheet.Cells[1, 11] = "Исчисление";
                oSheet.Cells[1, 12] = "Ф.И.О. исполнителя";

                oSheet.Cells[1].ColumnWidth = 5;  //номер
                oSheet.Cells[2].ColumnWidth = 15;   //номер дог
                oSheet.Cells[3].ColumnWidth = 15;   //сер дог
                oSheet.Cells[4].ColumnWidth = 14;   //дата договора
                oSheet.Cells[5].ColumnWidth = 28;   //статус договора
                oSheet.Cells[6].ColumnWidth = 14;   //кзл
                oSheet.Cells[7].ColumnWidth = 10;   //инн
                oSheet.Cells[8].ColumnWidth = 40;   //наименование клиента
                oSheet.Cells[9].ColumnWidth = 67;   //наименование договора
                oSheet.Cells[10].ColumnWidth = 15;  //цена договора
                
                oSheet.Cells[11].ColumnWidth = 15;  //исчисление
                oSheet.Cells[12].ColumnWidth = 40;  //фио
                int i;
                // Create an array to multiple values at once.
                string[,] saNames = new string[101, 15];

                for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if (j == 5)
                        {
                            saNames[i, j] = "\t" + check_null(dataGridView1.Rows[i].Cells[j].Value.ToString());

                        }
                        else
                        saNames[i, j] = check_null(dataGridView1.Rows[i].Cells[j].Value.ToString());
                        oSheet.Cells[i + 2, j + 1] = saNames[i, j];
                    }
                }

            }

            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line:  = ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Error");
            }
        }
    }
}
