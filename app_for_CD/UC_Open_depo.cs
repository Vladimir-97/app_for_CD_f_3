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
            dataGridView1.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            dataGridView1.ForeColor = Color.White;
        }
        OracleConnection con = null;
        string date, kzl, kzl_nm, fio, num;
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
            cmd.CommandText = "select distinct * from open_change_depo order by id";

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
            data[data.Count - 1][0] = dr[0].ToString(); ///////////Номер поряжковый
            data[data.Count - 1][1] = parse_date(check_null(dr[1].ToString()  )  );      ///////////Номер договора
            data[data.Count - 1][2] = check_null(dr[2].ToString());   /////////// Серия договора
            data[data.Count - 1][3] = dr[4].ToString() ;
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
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(89, 89, 89);
                    else
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(128, 128, 128);
                i++;
            }
        }
        string check_null(string str)
        {
            if (str.Length == 0)
                return "";
            return str;
        }

        private void panel1_Enter(object sender, EventArgs e)
        {
            updatePanel2();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           print(kzl,kzl_nm, date, num);
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
       
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentRow.Index;
            if (dataGridView1.SelectedCells.Count > 1)
            {
                num = dataGridView1.Rows[row].Cells[0].Value.ToString();
                date = dataGridView1.Rows[row].Cells[1].Value.ToString();
                kzl = dataGridView1.Rows[row].Cells[2].Value.ToString();
                kzl_nm = dataGridView1.Rows[row].Cells[3].Value.ToString();
                fio = dataGridView1.Rows[row].Cells[4].Value.ToString();
                button4.Enabled = true;
            }
            //else
            //{
            //    button4.Enabled = false;
            //}
            //MessageBox.Show(kzl_nm);
        }

        private void print(string kzl, string kzl_nm, string date, string id)
        {
            FileInfo fi;
            string excel_path;
            string path = ExecuteCommand("echo %cd%");
            string pathD = path.Substring(0, path.Length - 2);
            if (is_phys(kzl))
            {
                ExecuteCommand("copy report_ph.xls report1.xls");
                excel_path = pathD + "\\report1.xls";
                fi = new FileInfo(excel_path);
            }
            else
            {
                ExecuteCommand("copy report_yur.xls report1.xls");
                excel_path = pathD + "\\report1.xls";
                fi = new FileInfo(excel_path);
            }


            Excel.Application oXL;
            Excel.Workbooks oWBs;
            Excel.Workbook oWB;
            Excel.Worksheet oSheet;
            if (fi.Exists)
            {

                oXL = new Excel.Application();
                oXL.Visible = true;
                //Получаем набор ссылок на объекты Workbook
                oWBs = oXL.Workbooks;
                oXL.Workbooks.Open(excel_path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                oWB = oWBs[1];
                oSheet = oWB.ActiveSheet;
                //Выбираем лист 1
                oSheet.Name = "Распечатка";
                
                //////////////////////////////////////////////////////////////////////////////////////////    ТЕПЕРЬ ЗАПОЛНЯЕМ  /////////////////////////////////////////////////////
                string date_str1 = date.ToString();
                string date_str = date_str1.Substring(0, 2);
                date_str += ".";
                date_str += date_str1.Substring(3, 2);
                date_str += ".";
                date_str += date_str1.Substring(6, 4);
                oSheet.Cells[3, 10] = date_str;
                if (is_phys(kzl))
                {
                    OracleCommand cmd = con.CreateCommand();
                    cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = kzl;
                    cmd.Parameters.Add("ID", id );
                    //                             0        1         2          3       4          5             6          7   8      9     10             11           
                    cmd.CommandText = "Select * from open_change_depo_his where crp_cd = :KZL and id = :ID";
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        oSheet.Cells[44, 10] = dr[0].ToString() ;
                        oSheet.Cells[8, 5] = dr[1].ToString() ;
                        oSheet.Cells[10, 5] = dr[3].ToString() ;
                        oSheet.Cells[12, 3] = dr[4].ToString() ;
                        oSheet.Cells[12, 10] = dr[5].ToString() ;
                        oSheet.Cells[14, 5] = dr[6].ToString();
                        oSheet.Cells[14, 10] = dr[7].ToString();
                        oSheet.Cells[16, 5] = dr[8].ToString();
                        oSheet.Cells[16, 10] = dr[9].ToString();
                        oSheet.Cells[18, 5] = dr[10].ToString();
                        oSheet.Cells[20, 5] = dr[11].ToString();
                        oSheet.Cells[20, 10] = dr[12].ToString();
                        oSheet.Cells[22, 5] = dr[13].ToString();
                        oSheet.Cells[24, 5] = dr[14].ToString();
                        oSheet.Cells[24, 10] = dr[15].ToString(); 
                        oSheet.Cells[26, 5] = dr[16].ToString();
                        oSheet.Cells[28, 5] = dr[17].ToString();
                        oSheet.Cells[30,5] = dr[18].ToString();
                        oSheet.Cells[32,5] = dr[19].ToString();
                        oSheet.Cells[34,5] = dr[20].ToString();
                        oSheet.Cells[36,5] = dr[21].ToString();
                        oSheet.Cells[38,5] = dr[22].ToString();
                        oSheet.Cells[38,10] = dr[23].ToString();
                        oSheet.Cells[40,5] = dr[24].ToString();
                        oSheet.Cells[40, 10] = dr[25].ToString();
                        oSheet.Cells[42, 5] = dr[26].ToString();
                        oSheet.Cells[44, 5] = dr[27].ToString();
                        oSheet.Cells[44, 10] = id.ToString();
                        oSheet.Cells[48, 9] = fio;
                        oSheet.Cells[12, 5] = dr[30].ToString();


                    }
                    else
                    {
                        oXL.DisplayAlerts = false;
                        oWB.Close(false);
                        oXL.Quit();
                        oXL.DisplayAlerts = true;
                        oSheet = null;
                        oWB = null;
                        oWBs = null;
                        oXL = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        MessageBox.Show("Нет данных о клиенте");

                    }
                }
                else
                {
                    OracleCommand cmd = con.CreateCommand();
                    cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = kzl;
                    cmd.Parameters.Add("ID", id);
                    //                             0        1         2          3       4          5             6          7   8      9     10             11           
                    cmd.CommandText = "Select * from open_change_depo_his where crp_cd = :KZL and id = :ID";
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        oSheet.Cells[44, 10] = dr[0].ToString();
                        oSheet.Cells[8, 5] = dr[1].ToString();
                        oSheet.Cells[10, 5] = dr[3].ToString();
                      //  oSheet.Cells[12, 3] = dr[4].ToString();
                       // oSheet.Cells[12, 10] = dr[5].ToString();
                        oSheet.Cells[14, 5] = dr[6].ToString();
                        oSheet.Cells[12, 10] = dr[7].ToString();
                    //    oSheet.Cells[16, 5] = dr[8].ToString();
                        oSheet.Cells[16, 10] = dr[9].ToString();
                        oSheet.Cells[16, 5] = dr[10].ToString();
                        oSheet.Cells[18, 5] = dr[11].ToString();
                        oSheet.Cells[18, 10] = dr[12].ToString();
                        oSheet.Cells[20, 5] = dr[13].ToString();
                        oSheet.Cells[22, 5] = dr[14].ToString();
                        oSheet.Cells[22, 10] = dr[15].ToString();
                        oSheet.Cells[24, 5] = dr[16].ToString();
                        oSheet.Cells[26, 5] = dr[17].ToString();
                        oSheet.Cells[28, 5] = dr[18].ToString();
                        oSheet.Cells[30, 5] = dr[19].ToString();
                        oSheet.Cells[32, 5] = dr[20].ToString();
                        oSheet.Cells[34, 5] = dr[21].ToString();
                        oSheet.Cells[36, 5] = dr[22].ToString();
                        oSheet.Cells[36, 10] = dr[23].ToString();
                        //oSheet.Cells[40, 5] = dr[24].ToString();
                        //oSheet.Cells[40, 10] = dr[25].ToString();
                        oSheet.Cells[40, 5] = dr[26].ToString();
                        oSheet.Cells[46, 5] = dr[27].ToString();
                        oSheet.Cells[46, 10] = id.ToString();
                        oSheet.Cells[49, 9] = fio;
                        oSheet.Cells[12, 5] = "\t" +dr[30].ToString();
                        oSheet.Cells[42, 5] = dr[30].ToString();
                        oSheet.Cells[44, 5] = dr[31].ToString();

                    }
                    else
                    {
                        oXL.DisplayAlerts = false;
                        oWB.Close(false);
                        oXL.Quit();
                        oXL.DisplayAlerts = true;
                        oSheet = null;
                        oWB = null;
                        oWBs = null;
                        oXL = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        MessageBox.Show("Нет данных о клиенте");
                        //ExecuteCommand("del report1.xls");
                    }
                }
            }
            else
            {
                MessageBox.Show("Обратитесь к Тимуру");   ////////////возможно они удалили файл
            }

        }
        string find_nm_bk(string tmp)
        {
            if (tmp == "")
                return "";
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("MFO", OracleDbType.Varchar2).Value = tmp;
            //                             0        1         2          3       4          5             6          7   8      9     10             11           
            cmd.CommandText = "Select bk_nm from tbcb_bk_info where mfo_cd = :MFO ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return dr[0].ToString();
            }
            else return "";
        }
        string change_upd(string str)
        {
            if (str == "")
                return "";
            string tmp = "";
            tmp += str.Substring(0, 10);

            return tmp;
        }
        private string parse_date(string str)
        {
            if (str == "99991231" || str == "")
                return "-";
            string tmp = "";
            tmp += str.Substring(6, 2);
            tmp += ".";
            tmp += str.Substring(4, 2);
            tmp += ".";
            tmp += str.Substring(0, 4);
            return tmp;
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
                oSheet.Cells[1, 2] = "Дата создания";
                oSheet.Cells[1, 3] = "КЗЛ";
                oSheet.Cells[1, 4] = "Наименование клиента";
                oSheet.Cells[1, 5] = "Ф.И.О. исполнителя";
                oSheet.Cells[1].ColumnWidth = 5;  //номер
                oSheet.Cells[2].ColumnWidth = 15;   //номер дог
                oSheet.Cells[3].ColumnWidth = 20;   //сер дог
                oSheet.Cells[4].ColumnWidth = 100;   //дата договора
                oSheet.Cells[5].ColumnWidth = 80;   //статус договора
                int i;
                // Create an array to multiple values at once.
                string[,] saNames = new string[101, 15];

                for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        saNames[i, j] = "\t" + check_null(dataGridView1.Rows[i].Cells[j].Value.ToString());
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

        private void button1_Click(object sender, EventArgs e)
        {
            filter_open_depo fod = new filter_open_depo();
            fod.ShowDialog();

            if (Data.f_n == true || Data.f_CRP == true || Data.f_d == true ||  Data.f_fio == true)
            {
                string request = "";
                string name_cl = "";

                OracleCommand cmd = con.CreateCommand();
                if (Data.f_d == true)
                {
                    request += $" AND crte_dt  >= '{Data.st_date_orig}'  AND crte_dt <= '{Data.end_date_orig}' ";
                    Data.f_d = false;
                }
                if (Data.f_CRP == true)
                {
                    request += request + $" AND CRP_CD = {Data.number_ser} ";
                    Data.f_CRP = false;

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
                    request += request + $" AND CRP_NM LIKE '%{name_cl}%' ";
                }
                if (Data.f_fio == true)
                {
                    request += $" AND  fio = '{Data.filter_fio}' ";
                    Data.f_fio = false;

                }
                cmd.CommandText = "SELECT * from open_change_depo where rownum <100  " + request + "order by id";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                List<string[]> data = new List<string[]>();

                while (dr.Read() == true)
                {
                    fill_data(data, dr);
                }

                print_data(data);
            }

        }

        private void UC_Open_depo_Enter(object sender, EventArgs e)
        {
            updatePanel2();
        }

        string change_exp(string str)
        {
            string tmp = "";
            tmp += str.Substring(2, 2);
            tmp += "/20";
            tmp += str.Substring(0, 2);
            return tmp;
        }
        bool is_phys(string str)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", str));
            cmd.CommandText = "select crp_type_cd from tbcb_crp_info where crp_cd = :KZL  ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr[0].ToString() == "8000")
                    return true;
            }


            return false;
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

            process = Process.Start(processInfo);
            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();

            process.Close();
            return output;
        }
        int find_max_id()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select max(id) from open_change_depo";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                try
                {
                    if (dr.HasRows)
                    {
                        return Int32.Parse(dr[0].ToString());
                    }
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }
    }
}
