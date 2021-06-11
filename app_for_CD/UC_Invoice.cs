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
using Microsoft.Office.Interop.Excel;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using RSDN;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Reflection;

namespace app_for_CD
{
    public partial class UC_Invoice : UserControl
    {
        OracleConnection con = null;
        string ID;
        public UC_Invoice()
        {
            InitializeComponent();
            SetConnection();
            LoadData();
        }
        #region Для подключения excel

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

        public string ExcelFilePath
        {
            get { return excelFilePath; }
            set { excelFilePath = value; }
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
        #endregion
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

        int find_id()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT max(id) FROM Users_cd ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            return Int32.Parse(dr[0].ToString());
        }

        private void LoadData()
        {
            
            dataGridView_invoice.Rows.Clear();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from registration_of_invoice order by ID, num_of_ser";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            List<string[]> data = new List<string[]>();
            int i = 0;
            int priv = -1;
            while(dr.Read()) {
                
                if (Convert.ToInt32(dr[0]) != priv)
                {
                    dataGridView_invoice.Rows.Add();
                    dataGridView_invoice.Rows[i].Cells[0].Value = dr[0] + " от " + dr[9];
                    dataGridView_invoice.Rows[i].Cells[1].Value = dr[2] + " от " + dr[13]; 
                    dataGridView_invoice.Rows[i].Cells[2].Value = dr[1];
                    dataGridView_invoice.Rows[i].Cells[3].Value = dr[10];
                    if (dr[11] == null || dr[11].ToString() == "")
                    {
                        dataGridView_invoice.Rows[i].Cells[4].Value = "-";
                    }
                    else {
                        dataGridView_invoice.Rows[i].Cells[4].Value = dr[11];
                    }
                    if (dr[12].ToString() == "0")
                    {
                        dataGridView_invoice.Rows[i].Cells[5].Value = dr[8];
                        dataGridView_invoice.Rows[i].Cells[6].Value = "-";
                    }
                    else {
                        dataGridView_invoice.Rows[i].Cells[5].Value = "-";
                        dataGridView_invoice.Rows[i].Cells[6].Value = dr[8];
                    }
                    dataGridView_invoice.Rows[i].Cells[7].Value = dr[3].ToString();
                    dataGridView_invoice.Rows[i].Cells[8].Value = dr[4].ToString();
                    if (dr[15].ToString() == "1")
                    {
                        dataGridView_invoice.Rows[i].Cells[9].Value = "Активный";
                    }
                    else
                    {
                        dataGridView_invoice.Rows[i].Cells[9].Value = "Неактивный";
                    }
                    dataGridView_invoice.Rows[i].Cells[10].Value = (dataGridView_invoice.Rows[i].Cells[10] as DataGridViewComboBoxCell).Items[Convert.ToInt32(dr[17])];
                    dataGridView_invoice.Rows[i].Cells[11].Value = dr[18];
                    dataGridView_invoice.Rows[i].Cells[12].Value = dr[14];
                    i++;
                }
                else{

                    dataGridView_invoice.Rows[i - 1].Cells[7].Value = dataGridView_invoice.Rows[i - 1].Cells[7].Value.ToString() + '\n' + dr[3].ToString();
                    dataGridView_invoice.Rows[i - 1].Cells[8].Value = (dataGridView_invoice.Rows[i - 1].Cells[8].Value).ToString() + '\n' + (Convert.ToDouble(dr[4])).ToString();
                }
                
                priv = Convert.ToInt32(dr[0]);

            }
            
            for (int row = 0; row <= dataGridView_invoice.Rows.Count - 1; row++)
            {
                ((DataGridViewImageCell)dataGridView_invoice.Rows[row].Cells[13]).Value = Properties.Resources.change;
            }
            
            dr.Close();
        }

        private void add_Click(object sender, EventArgs e)
        {
            registration_of_an_invoice r = new registration_of_an_invoice();
            r.StartPosition = FormStartPosition.CenterParent;
            r.ShowDialog();
        }
        
        private void update_Click(object sender, EventArgs e)
        {
            LoadData();
        }


        private void dataGridView_invoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string num = "";
            
            var senderGrid = (DataGridView)sender;
            try
            {
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
                {
                    string str = dataGridView_invoice.Rows[e.RowIndex].Cells[0].Value.ToString();
                    for (int i = 0; i < str.Count(); i++)
                    {
                        if (str[i] != ' ')
                        {
                            num = num + str[i];
                        }
                        else
                        {
                            break;
                        }
                    }
                    registration_of_an_invoice r = new registration_of_an_invoice(num);
                    r.StartPosition = FormStartPosition.CenterParent;
                    r.ShowDialog();
                }
            }
            catch { 
            
            }
        }

        private void dataGridView_invoice_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (dataGridView_invoice.CurrentCell.ColumnIndex == 10 && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged -= LastColumnComboSelectionChanged;
                comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
            }
            e.CellStyle.BackColor = dataGridView_invoice.DefaultCellStyle.BackColor;
        }

        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {
            
            var currentcell = dataGridView_invoice.CurrentCellAddress;
            string num_date_invoice = dataGridView_invoice.Rows[currentcell.Y].Cells[0].Value.ToString();
            int num;
            string ID = "";
            int i = 0;
            var a = sender;
            
            while(num_date_invoice[i] != ' ')
            {
                ID += num_date_invoice[i];
                i++;
            }


            OracleCommand cmd;
            cmd = con.CreateCommand();
            if (dataGridView_invoice.Rows[currentcell.Y].Cells[currentcell.X].EditedFormattedValue.ToString() == "Выставлена")
                num = 0;
            else if (dataGridView_invoice.Rows[currentcell.Y].Cells[currentcell.X].EditedFormattedValue.ToString() == "Часть оплаты")
                num = 1;
            else { 
                num = 2;
            }
            cmd.CommandText = $"UPDATE REGISTRATION_OF_INVOICE SET PROCESS = {num} where id = {ID}";
            cmd.ExecuteNonQuery();
 
        }

        private string ChangeFormatData(string nch_data) {
            string ch_data;
            ch_data = nch_data[6].ToString() + nch_data[7].ToString() + '.';
            ch_data = ch_data + nch_data[4].ToString() + nch_data[5].ToString() + '.';
            ch_data = ch_data + nch_data[0].ToString() + nch_data[1].ToString() + nch_data[2].ToString() + nch_data[3].ToString();
            return ch_data;
        }

        private void DoExcelThings()
        {
            string old_ser, ser = "";
            OracleCommand cmd1;
            OracleDataReader dr1;
            string ch_data;
            excelFilePath = Path.GetFullPath("invoice_t.xlsx");

            openExcel();

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"select * from registration_of_invoice where ID = {ID}";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();

            ch_data = ChangeFormatData(dr[9].ToString());
            
            myExcelWorkSheet.Cells[2, "B"].Value = $"№ {ID} от {ch_data}";
            ch_data = ChangeFormatData(dr[13].ToString());
            myExcelWorkSheet.Cells[3, "B"].Value = $"к договору {dr[2].ToString()} от {ch_data}";

            cmd1 = con.CreateCommand();
            cmd1.CommandText = $"select CRP_NM, REG_ADDR_CONT from tbcb_crp_info where CRP_CD = '{dr[1]}'";
            cmd1.CommandType = CommandType.Text;

            dr1 = cmd1.ExecuteReader();

            dr1.Read();
            myExcelWorkSheet.Cells[6, "AX"].Value = dr1[0].ToString();
            myExcelWorkSheet.Cells[8, "AX"].Value = dr1[1].ToString();
            dr1.Close();

            myExcelWorkSheet.Cells[10, "AX"].Value = $"{dr[11]}";
            myExcelWorkSheet.Cells[12, "AX"].Value = "\t" + dr[8].ToString();

            cmd1 = con.CreateCommand();
            cmd1.CommandText = $"Select bk_acnt_no, mfo_cd from tbcb_crp_bk where crp_cd = '{dr[1]}'";
            cmd1.CommandType = CommandType.Text;

            dr1 = cmd1.ExecuteReader();
            dr1.Read();

            myExcelWorkSheet.Cells[14, "AX"].Value = $"{dr1[0]}";
            myExcelWorkSheet.Cells[16, "AX"].Value = $"{dr1[1]}";

            string num_double = dr[4].ToString();
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


            myExcelWorkSheet.Cells[27, "J"].Value = $"{dr[6]}";// комментарий 
            myExcelWorkSheet.Cells[29, "J"].Value = $"{dr[7]}";// основание

            myExcelWorkSheet.Cells[37, "AE"].Value = $"{dr[14]}";

            dr1.Close();

            old_ser = dr[2].ToString();
            flag = false;
            for (int i = 0; i < old_ser.Count(); i++)
            {
                if (old_ser[i] == '/')
                {
                    flag = true;
                }
                else if (old_ser[i] != '/' && flag == true)
                {
                    ser += old_ser[i];
                }
            }

            cmd1 = con.CreateCommand();
            cmd1.CommandText = $"select NDS from tbcb_cd where CD like '{ser}%' AND CD_NM = '{dr[3].ToString()}'";
            cmd1.CommandType = CommandType.Text;
            dr1 = cmd1.ExecuteReader();
            dr1.Read();

            double percent = double.Parse(dr1[0].ToString());
            dr1.Close();
            myExcelWorkSheet.Cells[22, "AE"].Value = $"{dr[5]}";
            myExcelWorkSheet.Cells[22, "D"].Value = $"{dr[3]}";
            myExcelWorkSheet.Cells[22, "AI"].Value = double.Parse(dr[4].ToString());
            myExcelWorkSheet.Cells[22, "BF"].Value = double.Parse(dr[4].ToString());
            double sum_without_NDS;
            sum_without_NDS = double.Parse(dr[4].ToString()) / (1 + percent / 100);

            myExcelWorkSheet.Cells[22, "AO"].Value = sum_without_NDS;
            
            if (percent == 0)
            {
                myExcelWorkSheet.Cells[22, "AZ"].Value = "БЕЗ НДС";
                myExcelWorkSheet.Cells[22, "AW"].Value = "БЕЗ НДС";
                myExcelWorkSheet.Cells[25, "B"].Value = "Всего к оплате:          " + RusNumber.Str(Int32.Parse(val)) + dr[5].ToString() + " " + frac + " тийин";
            }
            else
            {
                myExcelWorkSheet.Cells[22, "AZ"].Value = double.Parse(dr[4].ToString()) - sum_without_NDS;
                myExcelWorkSheet.Cells[22, "AW"].Value = $"{percent}%"; // процент
                myExcelWorkSheet.Cells[25, "B"].Value = "Всего к оплате:          " + RusNumber.Str(Int32.Parse(val)) + dr[5].ToString() + " " + frac + " тийин, в.т.ч. НДС: " + Math.Round(myExcelWorkSheet.Cells[22, "AZ"].Value, 2) + " " + dr[5].ToString();
            }

            dr.Close();
            myExcelApplication.Visible = true; // true will open Excel
            myExcelWorkSheet.PrintPreview();
            myExcelApplication.Visible = false; // hides excel file when user closes preview

        }
        private void print_Click(object sender, EventArgs e)
        {
            DoExcelThings();
            closeExcel();
        }

        private void dataGridView_invoice_SelectionChanged(object sender, EventArgs e)
        {
            int row = dataGridView_invoice.CurrentRow.Index;
            string str, num = "";
            if (dataGridView_invoice.SelectedCells.Count > 1)
            {
                str = dataGridView_invoice.Rows[row].Cells[0].Value.ToString();
                for (int i = 0; i < str.Count(); i++)
                {
                    if (str[i] != ' ')
                    {
                        num = num + str[i];
                    }
                    else
                    {
                        break;
                    }
                }
                ID = num;
                print.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
                print.Enabled = false;
            }
        }

        private void filtr_Click(object sender, EventArgs e)
        {
            filter_for_invoice r = new filter_for_invoice();
            r.StartPosition = FormStartPosition.CenterParent;
            r.ShowDialog();
        }
        string check_null(string str)
        {
            if (str.Length == 0)
                return "";
            return str;
        }
        private void excel_Click(object sender, EventArgs e)
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
                oSheet.Name = "Информация по счет-фактурам";
                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "Номер и дата счет-фактуры";
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

                for (i = 0; i < dataGridView_invoice.Rows.Count; i++)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        try
                        {
                            oSheet.Cells[i + 2, j + 1] = "\t" + check_null(dataGridView_invoice.Rows[i].Cells[j].Value.ToString());
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
            finally {
                oXL = null;
                oWB = null;
                oSheet = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ID);
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
                oSheet.Name = "Информация по счет-фактурам";
                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "INV_NUM";
                oSheet.Cells[1, 2] = "INV_DATE";
                oSheet.Cells[1, 3] = "CUR_CODE";
                oSheet.Cells[1, 4] = "USERNAME";
                oSheet.Cells[1, 5] = "PL_CODE";
                oSheet.Cells[1, 6] = "PL_NAME";
                oSheet.Cells[1, 7] = "PL_INN";
                oSheet.Cells[1, 8] = "PL_ADDRESS";
                oSheet.Cells[1, 9] = "PL_REGION";
                oSheet.Cells[1, 10] = "PL_NUM_ACC";
                oSheet.Cells[1, 11] = "PL_MFO";
                oSheet.Cells[1, 12] = "PL_BANK";
                oSheet.Cells[1, 13] = "SRV_SUM";
                oSheet.Cells[1, 14] = "SRV_NAME";
                oSheet.Cells[1, 15] = "DOGOVOR";

                oSheet.Cells[1].ColumnWidth = 8.43;
                oSheet.Cells[2].ColumnWidth = 9.43;
                oSheet.Cells[3].ColumnWidth = 12.71;
                oSheet.Cells[4].ColumnWidth = 10.86;
                oSheet.Cells[5].ColumnWidth = 8.86;
                oSheet.Cells[6].ColumnWidth = 21.57;
                oSheet.Cells[7].ColumnWidth = 9.29;
                oSheet.Cells[8].ColumnWidth = 43;
                oSheet.Cells[9].ColumnWidth = 24.29;
                oSheet.Cells[10].ColumnWidth = 20.71;
                oSheet.Cells[11].ColumnWidth = 7.71;
                oSheet.Cells[12].ColumnWidth = 93;
                oSheet.Cells[13].ColumnWidth = 9.86;
                oSheet.Cells[14].ColumnWidth = 48.71;
                oSheet.Cells[15].ColumnWidth = 26;
                int i;
                // Create an array to multiple values at once.
                string[,] saNames = new string[101, 15];

                for (i = 0; i < dataGridView_invoice.Rows.Count; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        try
                        {
                            saNames[i, j] = "\t" + check_null(dataGridView_invoice.Rows[i].Cells[j].Value.ToString());
                            oSheet.Cells[i + 3, j + 1] = saNames[i, j];
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
            finally
            {
                oXL = null;
                oWB = null;
                oSheet = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

        }
    }
}
