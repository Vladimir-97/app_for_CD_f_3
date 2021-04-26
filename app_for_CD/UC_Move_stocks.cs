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
        }
        OracleConnection con = null;
        private void UC_Move_stocks_Load(object sender, EventArgs e)
        {
            update_panel();


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
            data[data.Count - 1][6] = check_null(dr[5].ToString());     /////Наименование ЦБ
            data[data.Count - 1][7] = find_stk(check_null(dr[6].ToString()));     /////Наименование  эмитента и ценной бумаги

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
            cmd.CommandText = "select distinct dl_reg_no, dl_reg_dd, pldgr_crp_cd, pldgr_nm, isu_cd, issr_nm,plg_prov_qty, cors,sec_val, tr_type_cd,  pldge_crp_cd, pldge_nm from tbsr_stk_plg_reg where rownum <100 order by dl_reg_dd desc";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            List<string[]> data = new List<string[]>();

            while (dr.Read() == true)
            {
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
        //private string parse_type(string tmp)
        //{
        //    OracleCommand cmd = con.CreateCommand();
        //    cmd.Parameters.Add("CD", tmp);
        //    cmd.CommandText = "select cd_nm from tbcb_cd where cd_grp_no = '100051' and Lang_cd = 'UZ' and cd = :CD";
        //    cmd.CommandType = CommandType.Text;
        //    OracleDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        return dr[0].ToString();
        //    }
        //    return "";
        //}
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
    }
}
