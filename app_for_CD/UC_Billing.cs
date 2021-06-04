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
            SetConnection();
        }
        OracleConnection con = null;


        private void UC_Billing_Load(object sender, EventArgs e)
        {
            updatePanel2();
        }
        private void updatePanel2()
        {
            OracleCommand cmd = con.CreateCommand();
            //cmd.CommandText = "SELECT B.DOCU_NO, B.DOCU_SRES, B.DOCU_ISSU_DD, B.DOCU_STAT_CD, A.CRP_CD, A.CRP_NM, DIST_ID_2 FROM TBCB_CRP_INFO A INNER JOIN table_for_docu B ON A.CRP_CD = B.CRP_CD where rownum <= 50";
            //cmd.CommandText = "SELECT DISTINCT c.*, Y.DOCU_PRICE, Y.GET_DD FROM(SELECT B.DOCU_NO, B.DOCU_SRES, B.DOCU_ISSU_DD, B.DOCU_STAT_CD, A.CRP_CD, A.CRP_NM, A.DIST_ID_2, B.CRTE_DT FROM TBCB_CRP_INFO A INNER JOIN table_for_docu B ON A.CRP_CD = B.CRP_CD) c , NEW_TBCB y where c.docu_no = y.docu_no AND C.CRP_CD = Y.CRP_CD and rownum<=100 order by C.DOCU_ISSU_DD";
            cmd.CommandText = "SELECT * from table_billing";

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
            data[data.Count - 1][0] = check_null(dr[0].ToString())+ "/" + parse_date(check_null(dr[1].ToString())); /////
          //  data[data.Count - 1][1] = check_null(dr[1].ToString());      ////  относительно договора
            data[data.Count - 1][2] = check_null(dr[5].ToString());  //
            data[data.Count - 1][3] = check_null(dr[6].ToString());  //
            data[data.Count - 1][4] = check_null(dr[7].ToString());   //
            data[data.Count - 1][5] = check_null(dr[8].ToString());   //
//            data[data.Count - 1][6] = check_null(dr[10].ToString());  

            data[data.Count - 1][7] = check_null(dr[10].ToString());  //
            data[data.Count - 1][8] = check_null(dr[11].ToString());  //
            data[data.Count - 1][9] = check_st(check_null(dr[12].ToString())); //
            data[data.Count - 1][10] = check_null(dr[13].ToString());   ///////процесс
       //     data[data.Count - 1][11] = check_null(dr[12].ToString());    ///////сумма оплаты
            data[data.Count - 1][12] = check_null(dr[15].ToString());  ////



        }

        void print_data(List<string[]> data)
        {
            int i = 0;
            dataGridView1.Rows.Clear();

            foreach (string[] s in data)
            {
                dataGridView1.Rows.Add(s);
                for (int j = 0; j < 13; j++)
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
        string check_st(string str)
        {
            if (str == "1")
                return "Активен";
            return "Неактивный";
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

        private void button3_Click(object sender, EventArgs e)
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

                for (i = 0; i < dataGridView1.Rows.Count ; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        try
                        {
                            saNames[i, j] = "\t" + check_null(dataGridView1.Rows[i].Cells[j].Value.ToString());
                            oSheet.Cells[i + 2, j + 1] = saNames[i, j];
                        }
                        catch (Exception ex)
                        {
                            saNames[i, j] = "";
                        }
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
        string parse_date(string str)
        {
            string new_str = "";
            new_str = str.Substring(6, 2) + ".";
            new_str += str.Substring(4, 2) + ".";
            new_str += str.Substring(0,4) ;
            return new_str;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            string num = "";

            var senderGrid = (DataGridView)sender;
            try
            {
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
                {
                    string str = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    for (int i = 0; i < str.Count(); i++)
                    {
                        if (str[i] != '/')
                        {
                            num = num + str[i];
                        }
                        else
                        {
                            break;
                        }
                    }
                    MessageBox.Show(num);
                    registration_of_an_invoice r = new registration_of_an_invoice(num);
                    r.StartPosition = FormStartPosition.CenterParent;
                    r.ShowDialog();
                }
            }

            catch
            {

            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updatePanel2();
        }
    }
}

