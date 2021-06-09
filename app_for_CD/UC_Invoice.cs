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
        

        public void openExcel()
        {
            myExcelApplication = null;

            myExcelApplication = new Excel.Application(); // create Excell App
            myExcelApplication.DisplayAlerts = false; // turn off alerts


            myExcelWorkbook = (Excel.Workbook)(myExcelApplication.Workbooks._Open(excelFilePath, System.Reflection.Missing.Value,
               System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
               System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
               System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
               System.Reflection.Missing.Value, System.Reflection.Missing.Value)); // open the existing excel file

            int numberOfWorkbooks = myExcelApplication.Workbooks.Count; // get number of workbooks (optional)

            myExcelWorkSheet = (Excel.Worksheet)myExcelWorkbook.Worksheets[1]; // define in which worksheet, do you want to add data
            myExcelWorkSheet.Name = "WorkSheet 1"; // define a name for the worksheet (optinal)

            int numberOfSheets = myExcelWorkbook.Worksheets.Count; // get number of worksheets (optional)
        }

        public string ExcelFilePath
        {
            get { return excelFilePath; }
            set { excelFilePath = value; }
        }

        public void closeExcel()
        {
            try
            {
                myExcelWorkbook.SaveAs(excelFilePath, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value); // Save data in excel


                myExcelWorkbook.Close(true, excelFilePath, System.Reflection.Missing.Value); // close the worksheet


            }
            finally
            {
                if (myExcelApplication != null)
                {
                    myExcelApplication.Quit(); // close the excel application
                }
            }

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

        private void print_Click(object sender, EventArgs e)
        {
            string nch_date, ch_date; 
            excelFilePath = Path.GetFullPath("invoice_t.xlsx");
            openExcel();
            
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"select * from registration_of_invoice where ID = {ID}";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            nch_date = dr[9].ToString();
            ch_date = nch_date[6].ToString() + nch_date[7].ToString() + '.';
            ch_date = ch_date + nch_date[4].ToString() + nch_date[5].ToString() + '.';
            ch_date = ch_date + nch_date[0].ToString() + nch_date[1].ToString() + nch_date[2].ToString() + nch_date[3].ToString();
            MessageBox.Show(ch_date);
            myExcelWorkSheet.Cells[2, "B"].Value = $"№ {ID} от {ch_date}";
            myExcelWorkSheet.Cells[3, "B"].Value = $"к договору {dr[2].ToString()} от **.**.****";
            
            OracleCommand cmd1 = con.CreateCommand();
            cmd1.CommandText = $"select CRP_NM from tbcb_crp_info where CRP_CD = '{dr[1]}'";
            cmd1.CommandType = CommandType.Text;
            OracleDataReader dr1 = cmd1.ExecuteReader();
            dr1.Read();
            myExcelWorkSheet.Cells[6, "AX"].Value = $"{dr1[0]}";
            dr1.Close();
            dr.Close();
            closeExcel();
            /*
            File.Copy("invoice_template.xlsx", "invoice_template-tmp.xlsx");
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
            */
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
                MessageBox.Show(ID);
                print.Enabled = true;
            }
            else
            {
                print.Enabled = false;
            }
        }

        private void filtr_Click(object sender, EventArgs e)
        {
            reg_bill r = new reg_bill();
            r.StartPosition = FormStartPosition.CenterParent;
            r.ShowDialog();
        }

        private void bottom_tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
