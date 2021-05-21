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
    public partial class UC_Move_stocks : UserControl
    {
        public UC_Move_stocks()
        {
            InitializeComponent();
            SetConnection();
            button5.Enabled = false;
        }
        OracleConnection con = null;
        private void UC_Move_stocks_Load(object sender, EventArgs e)
        {
            update_panel();
            button5.Enabled = false;

        }
        void fill_data(List<string[]> data, OracleDataReader dr)
        {
            data.Add(new string[15]);
            data[data.Count - 1][0] = data.Count.ToString(); ///////////Номер поряжковый
            data[data.Count - 1][1] = check_null(dr[0].ToString());      ///////////Номер поручения
            data[data.Count - 1][2] = check_null(parse_date(dr[1].ToString()));   /////////// Дата поручения

            if (dr[2].ToString() != "")
            {
                data[data.Count - 1][3] = dr[2].ToString(); ;         /////////////// КЗЛ отчуждателя
            }
            data[data.Count - 1][4] = dr[3].ToString();  ////////////////////Наименование отчуждателя
            data[data.Count - 1][5] = check_null(dr[4].ToString());     /////КОД ЦБ
            
            data[data.Count - 1][6] = find_stk(check_null(dr[4].ToString()));     /////Наименование ЦБ

            data[data.Count - 1][7] = check_null(dr[6].ToString());     /////Наименование  эмитента и ценной бумаги

            data[data.Count - 1][8] = check_null(dr[7].ToString());  ////количство ЦБ
            data[data.Count - 1][9] = check_null(dr[8].ToString());   ////цена одной ЦБ
            data[data.Count - 1][10] = parse_type(check_null(dr[9].ToString()));  //// Сумма сделки
            data[data.Count - 1][11] = check_null(dr[10].ToString());  //// Вид сделки
            data[data.Count - 1][12] = check_null(dr[11].ToString()); ////КЗЛ получателя
                                                                      //    data[data.Count - 1][13] = check_null(dr[12].ToString()); ////Наименоание получателя
                                                                      //data[data.Count - 1][14] = check_null(dr[13].ToString()); ////ФИО исполнителя

        }
        void print_data(List<string[]> data)
        {
            int i = 0;
            dataGridView1.Rows.Clear();

            foreach (string[] s in data)
            {
                dataGridView1.Rows.Add(s);
                for (int j = 0; j < 14; j++)
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
        private void update_panel()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select distinct dl_reg_no, dl_reg_dd, pldgr_crp_cd, pldgr_nm, isu_cd, issr_nm,plg_prov_qty, cors,sec_val, tr_type_cd,  pldge_crp_cd, pldge_nm from tbsr_stk_plg_reg order by dl_reg_dd desc ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            List<string[]> data = new List<string[]>();
            int i = 0;
            while (dr.Read() == true && i < 100)
            {
                i++;
                fill_data(data, dr);
            }

            print_data(data);
        }
        private string parse_type(string tmp)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("CD", tmp);
            cmd.CommandText = "select cd_nm from tbcb_cd where cd_grp_no = '100051' and Lang_cd = 'UZ' and cd = :CD";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                return dr[0].ToString();
            }
            return "";
        }
        private string find_stk(string tmp)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("ISU_CD", tmp);
            cmd.CommandText = "select ISU_NM from tbcb_stk where isu_cd = :ISU_CD and rownum <500";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                return dr[0].ToString();
            }
            return "";
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

        private void button7_Click(object sender, EventArgs e)
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
                oSheet.Name = "Движение ценных бумаг";
                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "№";
                oSheet.Cells[1, 2] = "Номер поручения";
                oSheet.Cells[1, 3] = "Дата поручения";
                oSheet.Cells[1, 4] = "КЗЛ отчуждателя ";
                oSheet.Cells[1, 5] = "Наименование отчуждателя";
                oSheet.Cells[1, 6] = "Код ЦБ";
                oSheet.Cells[1, 7] = "Наименование ЦБ";
                oSheet.Cells[1, 8] = "Кол-во ЦБ";
                oSheet.Cells[1, 9] = "Цена одной ЦБ";
                oSheet.Cells[1, 10] = "Сумма сделки";
                oSheet.Cells[1, 11] = "Вид сделки";
                oSheet.Cells[1, 12] = "КЗЛ получателя";
                oSheet.Cells[1, 13] = "Наименование получателя";

                oSheet.Cells[1].ColumnWidth = 5;  //номер
                oSheet.Cells[2].ColumnWidth = 15;   //номер дог
                oSheet.Cells[3].ColumnWidth = 15;   //сер дог
                oSheet.Cells[4].ColumnWidth = 14;   //дата договора
                oSheet.Cells[5].ColumnWidth = 28;   //статус договора
                oSheet.Cells[6].ColumnWidth = 14;   //кзл
                oSheet.Cells[7].ColumnWidth = 10;   //инн
                oSheet.Cells[8].ColumnWidth = 15;   //наименование клиента
                oSheet.Cells[9].ColumnWidth = 15;   //наименование договора
                oSheet.Cells[10].ColumnWidth = 15;  //цена договора

                oSheet.Cells[11].ColumnWidth = 15;  //исчисление
                oSheet.Cells[12].ColumnWidth = 40;  //фио
                int i;
                // Create an array to multiple values at once.
                string[,] saNames = new string[101, 15];

                for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        //if (j == 3 || j == 11)
                        //{
                        saNames[i, j] = "\t" + check_null(dataGridView1.Rows[i].Cells[j].Value.ToString());

                        //}
                        //else
                        //    saNames[i, j] = check_null(dataGridView1.Rows[i].Cells[j].Value.ToString());
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

        private void button6_Click(object sender, EventArgs e)
        {
            filter_stocks f = new filter_stocks();
            f.ShowDialog();

            if (Data.fil_date == true || Data.fil_crp1 == true || Data.fil_client1 || Data.fil_crp2 == true || Data.fil_client2 || Data.fil_name_stocks == true)
            {
                string request = "where 1 = 1";
               // string name_cl = "";

                OracleCommand cmd = con.CreateCommand();
                if (Data.fil_date == true)
                {
                    request += $" AND dl_reg_dd  >= '{Data.st_date_orig}'  AND dl_reg_dd <= '{Data.end_date_orig}' ";
                }
                if (Data.fil_crp1 == true )
                {
                    request += $" AND pldgr_crp_cd like '%{Data.crp_str1}%' ";
                }
                if (Data.fil_client1 == true)
                {
                    request += $" AND pldgr_nm like '%{Data.client_str1}%' ";
                }
                if (Data.fil_crp2 == true)
                {
                    request += $" AND pldge_crp_cd like '%{Data.crp_str2}%' ";
                }
                if (Data.fil_client2 == true)
                {
                    request += $" AND pldge_nm like '%{Data.client_str2}%' ";
                }
                //if (Data.fil_code_stocks == true)
                //{
                //    request += $" AND pldge_nm = {Data.client_str1} ";
                //}
                if (Data.fil_name_stocks == true)
                {
                    request += $" AND issr_nm = '%{Data.client_str1}%' ";
                }
                cmd.CommandText = "select distinct dl_reg_no, dl_reg_dd, pldgr_crp_cd, pldgr_nm, isu_cd, issr_nm,plg_prov_qty, cors,sec_val, tr_type_cd,  pldge_crp_cd, pldge_nm from tbsr_stk_plg_reg " + request + " and rownum < 100 order by dl_reg_dd desc";

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
                Data.fil_date = false;
                Data.fil_crp1 = false;
                Data.fil_client1 = false;
                Data.fil_crp2 = false;
                Data.fil_client2 = false; 
                Data.fil_name_stocks = false;
                Data.fil_code_stocks = false;
            }
        }
        string kzl_pol, name_pol, kzl_otch, name_otch, code_cb, count_cb, type_agr;

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentRow.Index;
            if (dataGridView1.SelectedCells.Count > 1)
            {
                kzl_otch = dataGridView1.Rows[row].Cells[3].Value.ToString();
                name_otch = dataGridView1.Rows[row].Cells[4].Value.ToString();
                kzl_pol = dataGridView1.Rows[row].Cells[11].Value.ToString();
                name_pol = dataGridView1.Rows[row].Cells[12].Value.ToString();
                code_cb = dataGridView1.Rows[row].Cells[5].Value.ToString();
                count_cb = dataGridView1.Rows[row].Cells[7].Value.ToString();
                type_agr = dataGridView1.Rows[row].Cells[10].Value.ToString();   /////тип сделки
                MessageBox.Show("kzl_otch = " + kzl_otch);
                MessageBox.Show("kzl_pol = " + kzl_pol);
                MessageBox.Show("code_cd = " + code_cb);
                MessageBox.Show("count_cb = " + count_cb);
                MessageBox.Show("type_agr = " + type_agr);
                button5.Enabled = true;
                // crp_cd = dataGridView1.Rows[row].Cells[6].Value.ToString();
                // dataGridView1.ForeColor = Color.Red;
                // dataGridView1.GridColor = Color.Green;
            }
            else
            {
            }
        }
    }
}
