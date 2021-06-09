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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Printing;

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
            cmd.CommandText = "SELECT * from table_billing order by num_of_bill desc";

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
            data[data.Count - 1][0] = check_null(dr[0].ToString())+ " от " + parse_date(check_null(dr[1].ToString())); /////
            data[data.Count - 1][1] = check_null(dr[1].ToString());      ////  относительно договора
            data[data.Count - 1][2] = check_null(dr[5].ToString());  //
            data[data.Count - 1][3] = check_null(dr[6].ToString());  //
            data[data.Count - 1][4] = check_null(dr[7].ToString());   //
            data[data.Count - 1][5] = check_null(dr[8].ToString());   //
            data[data.Count - 1][6] = check_null(dr[9].ToString());  

            data[data.Count - 1][7] = check_null(dr[10].ToString());  //
            data[data.Count - 1][8] = check_null(dr[11].ToString());  //
            //data[data.Count - 1][9] = check_st(check_null(dr[12].ToString())); //
            //data[data.Count - 1][10] = check_null(dr[13].ToString());   ///////процесс
            //dataGridView_invoice.Rows[i].Cells[10].Value = (dataGridView_invoice.Rows[i].Cells[10] as DataGridViewComboBoxCell).Items[Convert.ToInt32(dr[17])];
            ////     data[data.Count - 1][11] = check_null(dr[12].ToString());    ///////сумма оплаты
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
                        if (str[i+1] != 'о' && str[i+2] != 'т')
                        {
                            num = num + str[i];
                        }
                        else
                        {
                            break;
                        }
                    }
                 //   MessageBox.Show(num);
                    reg_bill rb = new reg_bill(num);
                    rb.ShowDialog();
                }
            }

            catch
            {
                //MessageBox.Show("Ошибка");
            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updatePanel2();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reg_bill rb = new reg_bill();
            rb.ShowDialog();
        }
        static string ExecuteCommand(string command)   ///////////////для копирования excel файла
        {
            int exitCode;
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            process = System.Diagnostics.Process.Start(processInfo);
            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();

            process.Close();
            return output;
        }


        internal static string SelectPrinter()
        {
            PrintDocument printDocument = new PrintDocument();
            // printDocument.PrintPage += PrintPageHandler;

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDialog.Document.Print();

            return "";
        }

        internal static string GetPort(string printerName)
        {
            var devices = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Devices"); //Read-accessible even when using a locked-down account
            try
            {

                foreach (string name in devices.GetValueNames())
                {
                    if (name == printerName)
                    {
                        var value = (String)devices.GetValue(name);
                        var port = Regex.Match(value, @"(,\w+:)", RegexOptions.IgnoreCase).Value;
                        port = port.Replace(",", "");
                        return port;
                    }
                }
            }
            catch
            {
                throw;
            }
            return "";
        }

        internal static string GetActivePrinter()
        {
            string printer = SelectPrinter();
            if (printer != "")
            {
                string port = GetPort(printer);
                if (port != "")
                    return printer + " (" + port + ")";
                else
                    return "";
            }
            else
                return "";
        }

        internal static void PrintExcelSheet(Excel.Application app, Excel.Worksheet sheet, String activePrinter)
        {
            try
            {
                app.ActivePrinter = activePrinter;
                sheet.PrintOutEx();
            }
            catch (Exception e)
            {
                Console.WriteLine("Print error:\r\n" + e.Message);
            }
        }






        private void button1_Click(object sender, EventArgs e)
        {
            FileInfo fi;
            string excel_path;
            string path = ExecuteCommand("echo %cd%");
            string pathD = path.Substring(0, path.Length - 2);
            ExecuteCommand("copy report_bill.xls report1.xls");
            excel_path = pathD + "\\report1.xls";
            fi = new FileInfo(excel_path);



            Excel.Application oXL;
            Excel.Workbooks oWBs;
            Excel.Workbook oWB;
            Excel.Worksheet oSheet;
            if (fi.Exists)
            {


                oXL = new Excel.Application();
             //   oXL.Visible = true;
                //Получаем набор ссылок на объекты Workbook
                oWBs = oXL.Workbooks;
                oXL.Workbooks.Open(excel_path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                oWB = oWBs[1];
                oSheet = oWB.ActiveSheet;
                //Выбираем лист 1
                oSheet.Name = "Распечатка";


                string printerName = GetActivePrinter();
                if (printerName != "")
                {
                    Console.WriteLine("Menu send to printer: " + printerName);
                    PrintExcelSheet(oXL, oSheet, printerName);
                }


                //        DateTime date = DateTime.Now;
                //        string date_str1 = date.ToString();
                //        string date_str = date_str1.Substring(0, 2);
                //        date_str += "/";
                //        date_str += date_str1.Substring(3, 2);
                //        date_str += "/";
                //        date_str += date_str1.Substring(6, 4);


                //        oSheet.Cells[3, 35] = date_str;
                //        // oSheet.Cells[44, 10] = dr[0].ToString();
                //        oSheet.Cells[14, 2] = kzl_otch;
                //        oSheet.Cells[14, 32] = kzl_pol;
                //        oSheet.Cells[14, 11] = count_cb;
                //        oSheet.Cells[14, 17] = sum_one_cb;
                //        oSheet.Cells[14, 23] = "\t" + sum_agr;
                //        oSheet.Cells[25, 2] = type_agr;
                //        oSheet.Cells[25, 17] = num_agr;
                //        oSheet.Cells[25, 23] = date_agr;
                //        oSheet.Cells[19, 2] = code_cb;
                //        oSheet.Cells[19, 7] = name_cb;
                //        oSheet.Cells[15, 2] = name_otch;
                //        oSheet.Cells[15, 32] = name_pol;

                //        oSheet.Cells[19, 34] = "\t" + pval();    //////////ном стоимость
                //        oSheet.Cells[19, 37] = "\t" + total_agst();      //////// % от УФ
                //        oSheet.Cells[19, 26] = "\t" + st_cb();    //////////ном стоимость


                //        oSheet.Cells[28, 12] = date_agr;



                //        oSheet.Cells[36, 2] = " Оператор Депозитария    ______________________ " + Data.get_fio;

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            filter_bill f = new filter_bill();
            f.ShowDialog();

            if (Data_bill.date_from != false || Data_bill.ser_num != false || Data_bill.ser_aggr != false || Data_bill.crp != false || Data_bill.inn != false || Data_bill.pinfl != false || Data_bill.code_nds != false || Data_bill.name != false || Data_bill.serv != false || Data_bill.status != false || Data_bill.fio != false)
            {
                string request = "";
                string name_cl = "";

                OracleCommand cmd = con.CreateCommand();
                if (Data_bill.date_from == true)
                {
                    request = $" AND date_of_bill  >= '{Data_bill.s_date_from}'  AND date_of_bill <= '{Data_bill.s_date_to}' ";
                }
                if (Data_bill.crp == true)
                {
                    request = request + $" AND CRP_CD = {Data_bill.s_crp} ";
                }
                if (Data_bill.name == true)
                {
                    for (int i = 0; i < Data_bill.s_name.Length; i++)
                    {
                        if (Data_bill.s_name[i] == '%')
                        {
                            name_cl += '_';
                        }
                        else
                        {
                            name_cl += Data_bill.s_name[i];
                        }
                    }
                    request = request + $" AND CRP_NM LIKE '%{name_cl}%' ";
                }
                if (Data_bill.inn == true)
                {
                    request = request + $" AND dist_id_2 = '{Data_bill.s_inn}'";
                }
                if (Data_bill.pinfl == true)
                {
                    request = request + $" AND pinfl = '{Data_bill.s_pinfl}'";
                }
                if (Data_bill.code_nds == true)
                {
                    request = request + $" and nds = '{Data_bill.s_code_nds}'";
                }
                if (Data_bill.serv == true)
                {
                    request = request + $" AND type_sres = '{Data_bill.s_serv}'"; 
                }
                if (Data_bill.fio == true)
                {
                    request = request + $" AND fio = '{Data_bill.s_fio}'";
                }
                if (Data_bill.status == true)
                {
                    request = request + $" AND  state = '{Data_bill.s_status}'";
                }

                cmd.CommandText = "SELECT * from table_billing where 1 = 1 " + request + "order by num_of_bill desc ";

                bool find_val = false;
                cmd.CommandType = CommandType.Text;
                try
                {
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
                    Data_bill.clear();
                }
                catch
                {
                    Data_bill.clear();
                }
            }
        }
    }
}

