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
using RSDN;
using System.Drawing.Printing;

namespace app_for_CD
{
    public partial class UC_Billing : UserControl
    {
        public UC_Billing()
        {
            InitializeComponent();
            SetConnection();
            location_y = Size.Height -  panel5.Location.Y;
        }
        OracleConnection con = null;
        string ID = "";
        int location_y;

        private void UC_Billing_Load(object sender, EventArgs e)
        {
            LoadData("select * from table_billing order by num_of_bill desc");
        }

        
        private string check_pro(string str)
        {
            if (str == "Выставлена")
                return "0";
            else if (str == "Оплачена")
                return "2";
            else
                return "1";

        }
        private void LoadData(string str)
        {

            dataGridView1.Rows.Clear();
            OracleCommand cmd = con.CreateCommand();
            
            cmd.CommandText = str;
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            List<string[]> data = new List<string[]>();
            int i = 0;
            int priv = -1;
            while (dr.Read())
            {

                if (Convert.ToInt32(dr[0]) != priv)
                {


                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = dr[0] + " от " + dr[1];
                    dataGridView1.Rows[i].Cells[1].Value = dr[2]+ "/" + dr[3] + " от " + dr[4];
                    dataGridView1.Rows[i].Cells[2].Value = dr[5];
                    dataGridView1.Rows[i].Cells[3].Value = dr[6];
                    if (dr[7] == null || dr[7].ToString() == "")
                    {
                        dataGridView1.Rows[i].Cells[4].Value = "-";

                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[4].Value = dr[7];
                    }
                    if (dr[8].ToString() == "" || dr[8].ToString() == null)
                    {
                        dataGridView1.Rows[i].Cells[6].Value = dr[9];
                        dataGridView1.Rows[i].Cells[5].Value = "-";
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[6].Value = "-";
                        dataGridView1.Rows[i].Cells[5].Value = dr[8];
                    }
                    dataGridView1.Rows[i].Cells[7].Value = dr[10].ToString();  //вид товара
                    dataGridView1.Rows[i].Cells[8].Value = dr[11].ToString();  //стоиимость поставки
                    if (dr[12].ToString() == "1")   
                    {
                        dataGridView1.Rows[i].Cells[9].Value = "Активный";
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[9].Value = "Неактивный";
                    }
                    dataGridView1.Rows[i].Cells[10].Value = (dataGridView1.Rows[i].Cells[10] as DataGridViewComboBoxCell).Items[Convert.ToInt32(dr[13])];   // проц
                // dataGridView1.Rows[i].Cells[11].Value = dr[18];
                    dataGridView1.Rows[i].Cells[11].Value = dr[14];
                    dataGridView1.Rows[i].Cells[12].Value = dr[15];
                    i++;

                }
                else
                {

                    dataGridView1.Rows[i - 1].Cells[7].Value = dataGridView1.Rows[i - 1].Cells[7].Value.ToString() + '\n' + dr[3].ToString();
                    dataGridView1.Rows[i - 1].Cells[8].Value = (dataGridView1.Rows[i - 1].Cells[8].Value).ToString() + '\n' + (Convert.ToDouble(dr[4])).ToString();
                }

                priv = Convert.ToInt32(dr[0]);
                for (int j = 0; j < 14; j++)
                    if (i % 2 == 0)
                    dataGridView1.Rows[i-1].Cells[j].Style.BackColor = Color.FromArgb(89, 89, 89);
                    else
                    dataGridView1.Rows[i-1].Cells[j].Style.BackColor = Color.FromArgb(128, 128, 128);
            }

            for (int row = 0; row <= dataGridView1.Rows.Count - 1; row++)
            {
                ((DataGridViewImageCell)dataGridView1.Rows[row].Cells[13]).Value = Properties.Resources.change;
            }

            dr.Close();
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
                return "Активеый";
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
                oSheet.Name = "Информация по счету на оплату";
                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "Номер и дата счета на оплату";
                oSheet.Cells[1, 2] = "Номер, серия и дата договора";
                oSheet.Cells[1, 3] = "КЗЛ";
                oSheet.Cells[1, 4] = "Наименование клиента";
                oSheet.Cells[1, 5] = "ИНН";
                oSheet.Cells[1, 6] = "Код НДС";
                oSheet.Cells[1, 7] = "ПИНФЛ";
                oSheet.Cells[1, 8] = "Вид товара(услуг)";
                oSheet.Cells[1, 9] = "Стоимость поставки";
                oSheet.Cells[1, 10] = "Статус";
                oSheet.Cells[1, 11] = "Процесс";
                oSheet.Cells[1, 12] = "Сумма оплаты";
                oSheet.Cells[1, 13] = "Ф.И.О. исполнителя";

                oSheet.Cells[1].ColumnWidth = 25;
                oSheet.Cells[2].ColumnWidth = 28;
                oSheet.Cells[3].ColumnWidth = 13;
                oSheet.Cells[4].ColumnWidth = 40;
                oSheet.Cells[5].ColumnWidth = 10;
                oSheet.Cells[6].ColumnWidth = 16;
                oSheet.Cells[7].ColumnWidth = 16;
                oSheet.Cells[8].ColumnWidth = 40;
                oSheet.Cells[9].ColumnWidth = 23;
                oSheet.Cells[10].ColumnWidth = 13;
                oSheet.Cells[11].ColumnWidth = 13;
                oSheet.Cells[12].ColumnWidth = 23;
                oSheet.Cells[13].ColumnWidth = 35;
                int i;
                // Create an array to multiple values at once.
                string[,] saNames = new string[101, 15];

                for (i = 0; i < dataGridView1.Rows.Count ; i++)
                {
                    for (int j = 0; j < 13; j++)
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
        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {
            var currentcell = dataGridView1.CurrentCellAddress;
            string num_date_invoice = dataGridView1.Rows[currentcell.Y].Cells[0].Value.ToString();
            string previous_value = dataGridView1.Rows[currentcell.Y].Cells[10].Value.ToString();
            int num;
            string ID1 = "";
            int i = 0;

            while (num_date_invoice[i] != ' ')
            {
                ID1 += num_date_invoice[i];
                i++;
            }

            OracleCommand cmd;

            string sum = "";
            if (dataGridView1.Rows[currentcell.Y].Cells[currentcell.X].EditedFormattedValue.ToString() == "Выставлена")
                num = 0;
            else if (dataGridView1.Rows[currentcell.Y].Cells[currentcell.X].EditedFormattedValue.ToString() == "Часть оплаты")
                num = 1;
            else
            {
                //123
                cmd = con.CreateCommand();
                cmd.CommandText = $"select cost_deliv from table_billing where num_of_bill = {ID1}";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                sum = dr[0].ToString();
                dr.Close();
                num = 2;
            }

            if (num == 0 && previous_value != dataGridView1.Rows[currentcell.Y].Cells[currentcell.X].EditedFormattedValue.ToString())
            {
                cmd = con.CreateCommand();
                dataGridView1.Rows[currentcell.Y].Cells[11].ReadOnly = true;
                cmd.CommandText = $"UPDATE table_billing SET PROCESS = {num}, payment_amount = 0 where num_of_bill = {ID1}";
                cmd.ExecuteNonQuery();

                LoadData("select * from table_billing order by num_of_bill desc");

            }
            else if (num == 1 && previous_value != dataGridView1.Rows[currentcell.Y].Cells[currentcell.X].EditedFormattedValue.ToString())
            {
                sum_for_pay r = new sum_for_pay(ID1,2);
                r.StartPosition = FormStartPosition.CenterParent;
                r.ShowDialog();
                if (Data.yes == true)
                {
                    cmd = con.CreateCommand();
                    dataGridView1.Rows[currentcell.Y].Cells[11].ReadOnly = false;
                    cmd.CommandText = $"UPDATE table_billing SET PROCESS = {num} where num_of_bill = {ID1}";
                    cmd.ExecuteNonQuery();
                    LoadData("select * from table_billing order by num_of_bill desc");
                }
                Data.yes = false;
            }
            else if (num == 2 && previous_value != dataGridView1.Rows[currentcell.Y].Cells[currentcell.X].EditedFormattedValue.ToString())    /////x = 10
            {
                cmd = con.CreateCommand();
                dataGridView1.Rows[currentcell.Y].Cells[11].ReadOnly = true;
                cmd.CommandText = $"UPDATE table_billing SET PROCESS = {num}, payment_amount = {sum} where num_of_bill = {ID1}";
                cmd.ExecuteNonQuery();
                LoadData("select * from table_billing order by num_of_bill desc");
            }

        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (dataGridView1.CurrentCell.ColumnIndex == 10 && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged -= LastColumnComboSelectionChanged;
                comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 11)
            {
                var currentcell = dataGridView1.CurrentCellAddress;
                System.Windows.Forms.TextBox textBox = e.Control as System.Windows.Forms.TextBox;
                if (dataGridView1.Rows[currentcell.Y].Cells[11].ReadOnly == false)
                {
                    string ID1 = "";
                    string num_date_invoice = dataGridView1.Rows[currentcell.Y].Cells[0].Value.ToString();
                    int i = 0;
                    while (num_date_invoice[i] != ' ')
                    {
                        ID1 += num_date_invoice[i];
                        i++;
                    }
                    sum_for_pay r = new sum_for_pay(ID1, 2);
                    r.StartPosition = FormStartPosition.CenterParent;
                    r.ShowDialog();
                    LoadData("select * from table_billing order by num_of_bill desc");
                }
            }
            e.CellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            LoadData("select * from table_billing order by num_of_bill desc");
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
        public string ExcelFilePath
        {
            get { return excelFilePath; }
            set { excelFilePath = value; }
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
        private string excelFilePath = string.Empty;
        Excel.Application myExcelApplication;
        Excel.Workbook myExcelWorkbook;
        Excel.Worksheet myExcelWorkSheet;
        Excel.Workbooks workbooks;
        public void openExcel()
        {
            myExcelApplication = null;
            myExcelApplication = new Excel.Application(); // create Excell App
            myExcelApplication.DisplayAlerts = false; // turn off alerts
            workbooks = myExcelApplication.Workbooks;

            myExcelWorkbook = workbooks.Open(excelFilePath, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing); // open the existing excel file

            myExcelWorkSheet = myExcelWorkbook.Worksheets[1]; // define in which worksheet, do you want to add data
            myExcelWorkSheet.Name = "WorkSheet 1"; // define a name for the worksheet (optinal)

        }
        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        private void DoExcelThings()
        {
            string old_ser, ser = "";
            OracleCommand cmd1;
            OracleDataReader dr1;
            string date_bill, ch_date;
            excelFilePath = Path.GetFullPath("invoice_t.xlsx");

            openExcel();
                
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"select * from table_billing where num_of_bill = {ID}";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            date_bill = dr[1].ToString();
            ch_date = date_bill[6].ToString() + date_bill[7].ToString() + '.';
            ch_date = ch_date + date_bill[4].ToString() + date_bill[5].ToString() + '.';
            ch_date = ch_date + date_bill[0].ToString() + date_bill[1].ToString() + date_bill[2].ToString() + date_bill[3].ToString();


            myExcelWorkSheet.Cells[1, 2].Value = "СЧЕТ НА ОПЛАТУ";

            myExcelWorkSheet.Cells[2, "B"].Value = $"№ {ID} от {ch_date}";

            date_bill = dr[4].ToString();
            ch_date = date_bill[6].ToString() + date_bill[7].ToString() + '.';
            ch_date = ch_date + date_bill[4].ToString() + date_bill[5].ToString() + '.';
            ch_date = ch_date + date_bill[0].ToString() + date_bill[1].ToString() + date_bill[2].ToString() + date_bill[3].ToString();
            myExcelWorkSheet.Cells[3, "B"].Value = $"к договору {dr[0].ToString()} от {ch_date}" ;

            cmd1 = con.CreateCommand();
            cmd1.CommandText = $"select CRP_NM, REG_ADDR_CONT from tbcb_crp_info where CRP_CD = '{dr[5]}'";
            cmd1.CommandType = CommandType.Text;

            dr1 = cmd1.ExecuteReader();

            dr1.Read();
            myExcelWorkSheet.Cells[6, "AX"].Value = dr1[0].ToString();
            myExcelWorkSheet.Cells[8, "AX"].Value = dr1[1].ToString();
            dr1.Close();

            myExcelWorkSheet.Cells[10, "AX"].Value = $"{dr[7]}";
            if (dr[8].ToString() == "")
            myExcelWorkSheet.Cells[12, "AX"].Value = "\t" + dr[9].ToString();
            else
            {
                myExcelWorkSheet.Cells[12, "AX"].Value = "\t" + dr[8].ToString();
            }
            cmd1 = con.CreateCommand();
            cmd1.CommandText = $"Select bk_acnt_no, mfo_cd from tbcb_crp_bk where crp_cd = '{dr[5]}'";
            cmd1.CommandType = CommandType.Text;

            dr1 = cmd1.ExecuteReader();
            dr1.Read();

            myExcelWorkSheet.Cells[14, "AX"].Value = $"{dr1[0]}";
            myExcelWorkSheet.Cells[16, "AX"].Value = $"{dr1[1]}";

            string num_double = dr[11].ToString();
            bool flag = false;
            var DS = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            string val = "", frac = ""; ;

            for (int i = 0; i < num_double.Count(); i++)
            {
                if (num_double[i] != DS && flag == false)
                {
                    val += num_double[i];
                }
                else if (num_double[i] != DS && flag == true)
                {
                    frac += num_double[i];
                }
                else if (num_double[i] == DS)
                {
                    flag = true;
                }
            }

            if (flag == false)
            {
                frac = "00";
            }


            myExcelWorkSheet.Cells[27, "J"].Value = $"{dr[16]}";// комментарий 
            myExcelWorkSheet.Cells[29, "J"].Value = $"{dr[17]}";// основание

            myExcelWorkSheet.Cells[37, "AE"].Value = $"{dr[15]}";

            dr1.Close();
            ser = dr[3].ToString();
            //old_ser = dr[2].ToString();
            //flag = false;
            //for (int i = 0; i < old_ser.Count(); i++)
            //{
            //    if (old_ser[i] == '/')
            //    {
            //        flag = true;
            //    }
            //    else if (old_ser[i] != '/' && flag == true)
            //    {
            //        ser += old_ser[i];
            //    }
            //}

            cmd1 = con.CreateCommand();
            cmd1.CommandText = $"select NDS from tbcb_cd where CD like '{ser}%' AND CD_NM = '{dr[10].ToString()}'";
            cmd1.CommandType = CommandType.Text;
            dr1 = cmd1.ExecuteReader();
            dr1.Read();

            double percent = double.Parse(dr1[0].ToString());
            dr1.Close();
            myExcelWorkSheet.Cells[22, "AE"].Value = $"{dr[18]}";   ///валюта
            myExcelWorkSheet.Cells[22, "D"].Value = $"{dr[10]}";   //услуга
            myExcelWorkSheet.Cells[22, "AI"].Value = double.Parse(dr[11].ToString());
            myExcelWorkSheet.Cells[22, "BF"].Value = double.Parse(dr[11].ToString());
            double sum_without_NDS;
            sum_without_NDS = double.Parse(dr[11].ToString()) / (1 + percent / 100);
            MessageBox.Show(sum_without_NDS.ToString());
            myExcelWorkSheet.Cells[22, "AO"].Value = sum_without_NDS;
            if (percent == 0)
            {
                myExcelWorkSheet.Cells[22, "AZ"].Value = "БЕЗ НДС";
                myExcelWorkSheet.Cells[22, "AW"].Value = "БЕЗ НДС";
                myExcelWorkSheet.Cells[25, "B"].Value = "Всего к оплате:          " + RusNumber.Str(Int32.Parse(val)) + dr[18].ToString() + " " + frac + " тийин";
            }
            else
            {
                myExcelWorkSheet.Cells[22, "AZ"].Value = double.Parse(dr[11].ToString()) - sum_without_NDS;
                myExcelWorkSheet.Cells[22, "AW"].Value = $"{percent}%"; // процент
                myExcelWorkSheet.Cells[25, "B"].Value = "Всего к оплате:          " + RusNumber.Str(Int32.Parse(val)) + dr[18].ToString() + " " + frac + " тийин, в.т.ч. НДС: " + Math.Round(myExcelWorkSheet.Cells[22, "AZ"].Value, 2) + " " + dr[18].ToString();

            }

            dr.Close();
            myExcelApplication.Visible = true; // true will open Excel
            myExcelWorkSheet.PrintPreview();
            myExcelApplication.Visible = false; // hides excel file when user closes preview

        }

        public void closeExcel()
        {

            myExcelWorkbook.SaveAs(excelFilePath, Type.Missing, Type.Missing, Type.Missing,
                                              Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
                                              Type.Missing, Type.Missing, Type.Missing,
                                              Type.Missing, Type.Missing); // Save data in excel

            myExcelWorkbook.Close(true, excelFilePath, Type.Missing); // close the worksheet
            myExcelApplication.Quit(); // close the excel application



            myExcelApplication = null;
            myExcelWorkbook = null;
            myExcelWorkSheet = null;
            workbooks = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            DoExcelThings();
        //    closeExcel();
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
                if (Data_bill.ser_num == true)
                {
                    request = request + $" AND num_aggr = {Data_bill.s_ser_num} ";
                }
                if (Data_bill.ser_aggr == true)
                {
                    request = request + $" AND sres_aggr = '{Data_bill.s_ser_aggr}' ";
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
                if (Data_bill.its_ok)
                {
                    string str = "SELECT * from table_billing where 1 = 1 " + request + "order by num_of_bill desc ";
                    cmd.CommandText = str;
                    bool find_val = false;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    try
                    {

                        if (dr.Read())
                        {
                            LoadData(str);
                        }
                        else
                        {
                            MessageBox.Show("Не найдено по данному запросу!");
                        }
                    }
                    catch
                    {
                    }
                }
                Data_bill.clear();
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentRow.Index;
            string str_tmp = "";
            if (dataGridView1.SelectedCells.Count > 1)
            {
                string str = dataGridView1.Rows[row].Cells[0].Value.ToString();
                for (int i =0; i< str.Length; i++)
                {
                    if (str[i+1] == 'о')
                    {
                        break;
                    }
                    str_tmp += str[i];
                }
                ID = str_tmp;
            }
        }

        private void UC_Billing_SizeChanged(object sender, EventArgs e)
        {
            panel5.Location =  new Point(16, Size.Height - location_y);
        }
    }
}

