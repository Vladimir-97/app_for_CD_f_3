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
using System.Collections.Generic;
using Oracle.DataAccess.Types;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace app_for_CD
{
    public partial class UC_Move_stocks : UserControl
    {
        public UC_Move_stocks()
        {
            InitializeComponent();
            SetConnection();
          //  button5.Enabled = false;
           // dataGridView1.Font = new Font("Times New Roman", 10, FontStyle.Bold);
        }
        OracleConnection con = null;
        private void UC_Move_stocks_Load(object sender, EventArgs e)
        {
            update_panel();
           // button5.Enabled = false;

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
                if (app_for_CD.Properties.Settings.Default["Theme"].ToString() != "False")
                {

                    dataGridView1.Rows.Add(s);
                    for (int j = 0; j < 14; j++)
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


        private void button5_Click(object sender, EventArgs e)
        {
            FileInfo fi;
            string excel_path;
            string path = ExecuteCommand("echo %cd%");
            string pathD = path.Substring(0, path.Length - 2);

            ExecuteCommand("copy move_stocks.xls report1.xls");
            excel_path = pathD + "\\report1.xls";
            fi = new FileInfo(excel_path);



            Excel.Application oXL;
            Excel.Workbooks oWBs;
            Excel.Workbook oWB;
            Excel.Worksheet oSheet;
            if (fi.Exists)
            {

                oXL = new Excel.Application();
                MessageBox.Show("Ведется подготовка документа");
                //Получаем набор ссылок на объекты Workbook
                oWBs = oXL.Workbooks;
                oXL.Workbooks.Open(excel_path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                oWB = oWBs[1];
                oSheet = oWB.ActiveSheet;
                //Выбираем лист 1
                oSheet.Name = "Распечатка";
                DateTime date = DateTime.Now;
                string date_str1 = date.ToString();
                string date_str = date_str1.Substring(0, 2);
                date_str += "/";
                date_str += date_str1.Substring(3, 2);
                date_str += "/";
                date_str += date_str1.Substring(6, 4);


                oSheet.Cells[3, "BE"] = date_str;
                //// oSheet.Cells[44, 10] = dr[0].ToString();
                //oSheet.Cells[14, 2] = kzl_otch;
                //oSheet.Cells[14, 32] = kzl_pol;
                //oSheet.Cells[14, 11] = count_cb;
                //oSheet.Cells[14, 17] = sum_one_cb;
                //oSheet.Cells[14, 23] = "\t" + sum_agr;
                //oSheet.Cells[25, 2] = type_agr;
                //oSheet.Cells[25, 17] = num_agr;
                //oSheet.Cells[25, 23] = date_agr;
                //oSheet.Cells[19, 2] = code_cb;
                //oSheet.Cells[19, 7] = name_cb;
                //oSheet.Cells[15, 2] = name_otch;
                //oSheet.Cells[15, 32] = name_pol;

                //oSheet.Cells[19, 34] = "\t" + pval();    //////////ном стоимость
                //oSheet.Cells[19, 37] = "\t" + total_agst();      //////// % от УФ
                //oSheet.Cells[19, 26] = "\t" + st_cb();    //////////ном стоимость


                //oSheet.Cells[28, 12] = date_agr;



                oSheet.Cells[10, 2] = " Исполнено депозитарием:  " +  date_str;
                oSheet.Cells[11, 2] = " Исполнитель Депозитария                    ______________________ " + Data.get_fio;
                oSheet.Cells[12, 2] = " Вид операции:     ";

                int i = 0;

                List<DataGridViewRow> rows_with_checked_column = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    rows_with_checked_column.Add(row);
                    if (Convert.ToBoolean(row.Cells["Распечатать"].Value) == true)
                    {
                        if (row.Index == 0)
                        {
                            oSheet.Cells[18, "B"] = dataGridView1.Rows[row.Index].Cells[3].Value.ToString() + " - " + dataGridView1.Rows[row.Index].Cells[4].Value.ToString();
                            //oSheet.Cells[18, 2] = dataGridView1.Rows[row.Index].Cells[5].Value.ToString();
                            oSheet.Cells[18, "N"] = dataGridView1.Rows[row.Index].Cells[6].Value.ToString();
                            oSheet.Cells[18, "AC"] = "\t" + pval(dataGridView1.Rows[row.Index].Cells[5].Value.ToString(), dataGridView1.Rows[row.Index].Cells[3].Value.ToString(), dataGridView1.Rows[row.Index].Cells[11].Value.ToString(), dataGridView1.Rows[row.Index].Cells[7].Value.ToString());    ////Номинал
                            oSheet.Cells[18, "AF"] = dataGridView1.Rows[row.Index].Cells[7].Value.ToString();
                            oSheet.Cells[18,"AK"] = dataGridView1.Rows[row.Index].Cells[8].Value.ToString();   //цена сделка
                            oSheet.Cells[18, "AQ"] = dataGridView1.Rows[row.Index].Cells[9].Value.ToString();     ////сумма сделки
                            oSheet.Cells[18, "AY"] = "\t" + total_agst(dataGridView1.Rows[row.Index].Cells[5].Value.ToString(), dataGridView1.Rows[row.Index].Cells[3].Value.ToString(), dataGridView1.Rows[row.Index].Cells[11].Value.ToString(), dataGridView1.Rows[row.Index].Cells[7].Value.ToString()  );  ////% от УФ
                            oSheet.Cells[18, "BC"] = dataGridView1.Rows[row.Index].Cells[11].Value.ToString() + " - " + dataGridView1.Rows[row.Index].Cells[12].Value.ToString();
                            oSheet.Cells[21, "I"] = dataGridView1.Rows[row.Index].Cells[10].Value.ToString();
                            oSheet.Cells[21, "AJ"] = dataGridView1.Rows[row.Index].Cells[1].Value.ToString();
                            oSheet.Cells[21, "AQ"] = dataGridView1.Rows[row.Index].Cells[2].Value.ToString();
                            oSheet.Cells[21, "AU"] = "\t" + st_cb(dataGridView1.Rows[row.Index].Cells[5].Value.ToString());    //////////state of stock
                            i++;
                        }
                        else
                        {///////////////////////////объединение ячеек

                            change_height(oSheet, i, 2, 8, 17,"КЗЛ и наименование отчуждателя");     /////    i откуда, докуда, что писать
                            change_height(oSheet, i, 9, 13,17, "Код ЦБ");     /////  
                            change_height(oSheet, i, 14, 28,17, "Наименование ценной бумаги и Эмитента");     /////  
                            change_height(oSheet, i, 29, 31,17, "Номинал");     /////  
                            change_height(oSheet, i, 32, 36,17, "Количество ЦБ");     /////  
                            change_height(oSheet, i, 37, 42,17, "Цена сделки");     /////  
                            change_height(oSheet, i, 43, 50,17, "Сумма сделки");     /////  
                            change_height(oSheet, i, 51, 54,17, "% т УФ");     /////  
                            change_height(oSheet, i, 55, 61,17, "КЗЛ и наименование получателя");     /////  


                            change_height_date(oSheet, i, 2, 8,18, dataGridView1.Rows[row.Index].Cells[3].Value.ToString() + " - " + dataGridView1.Rows[row.Index].Cells[4].Value.ToString() );     /////  
                            change_height(oSheet, i, 9, 13, 18, dataGridView1.Rows[row.Index].Cells[5].Value.ToString(), true );     /////  
                            change_height(oSheet, i, 14, 28, 18, dataGridView1.Rows[row.Index].Cells[6].Value.ToString(), true);     /////  
                            change_height(oSheet, i, 29, 31, 18, "\t" + pval(dataGridView1.Rows[row.Index].Cells[5].Value.ToString(), dataGridView1.Rows[row.Index].Cells[3].Value.ToString(), dataGridView1.Rows[row.Index].Cells[11].Value.ToString(), dataGridView1.Rows[row.Index].Cells[7].Value.ToString()) , true);     /////  
                            change_height(oSheet, i, 32, 36, 18, dataGridView1.Rows[row.Index].Cells[7].Value.ToString(), true);     /////  
                            change_height(oSheet, i, 37, 42, 18, dataGridView1.Rows[row.Index].Cells[8].Value.ToString(), true);   //цена сделка 
                            change_height(oSheet, i, 43, 50, 18, "\t" + dataGridView1.Rows[row.Index].Cells[9].Value.ToString(), true);     ////сумма сделки
                            change_height(oSheet, i, 51, 54, 18, "\t" + total_agst(dataGridView1.Rows[row.Index].Cells[5].Value.ToString(), dataGridView1.Rows[row.Index].Cells[3].Value.ToString(), dataGridView1.Rows[row.Index].Cells[11].Value.ToString(), dataGridView1.Rows[row.Index].Cells[7].Value.ToString()), true);  ////% от УФ);     /////  
                            change_height_date(oSheet, i, 55, 61, 18, dataGridView1.Rows[row.Index].Cells[11].Value.ToString() + " - " + dataGridView1.Rows[row.Index].Cells[12].Value.ToString());     /////  

                            change_height(oSheet, i, 9, 35, 20, "Основание для совершения операции: Вид документа ");     /////    i откуда, докуда, что писать
                            change_height(oSheet, i, 36, 42, 20, "№ документа");     /////    i откуда, докуда, что писать
                            change_height(oSheet, i, 43, 46, 20, "Дата сделки");     /////    i откуда, докуда, что писать
                            change_height(oSheet, i, 47, 54, 20, "Статус ЦБ");     /////    i откуда, докуда, что писать

                            change_height(oSheet, i, 9, 35, 21, dataGridView1.Rows[row.Index].Cells[10].Value.ToString(), true);     ///
                            change_height(oSheet, i, 36, 42, 21, dataGridView1.Rows[row.Index].Cells[1].Value.ToString(), true);     ///
                            change_height(oSheet, i, 43, 46, 21, dataGridView1.Rows[row.Index].Cells[2].Value.ToString(), true);     ///
                            change_height(oSheet, i, 47, 54, 21, "\t" + st_cb(dataGridView1.Rows[row.Index].Cells[5].Value.ToString()), true);     ///

                            i++;
                        }   
                    }   
                }
                oSheet.Cells[18 + i * 6 -1, 11].Font.Bold = true;
                oSheet.Cells[19 + i * 6 -1, 8].Font.Bold = true;
                oSheet.Cells[18 + i * 6 -1, 11] = "Оператор Депозитария ______________________ " + Data.get_fio;
                oSheet.Cells[19 + i * 6 -1, 16] = "                                                                 ( П о д п и с ь )       М. П.";
                oXL.Visible = true;
            }
        }
        private void change_height(Excel.Worksheet oSheet, int i, int j, int k, int m, string str, bool is_bold = false)
        {
            oSheet.Range[oSheet.Cells[m + 6 * i, j], oSheet.Cells[m + 6 * i, k]].Merge();
            var SomeCell = (Excel.Range)oSheet.Cells[m + 6 * i, j];
            SomeCell.Value2 = str;
            oSheet.Range[oSheet.Cells[m + 6 * i, j], oSheet.Cells[m + 6 * i, k]].Borders.LineStyle = XlLineStyle.xlContinuous; ;

            if (j ==2)
                SomeCell.RowHeight = 30;
            if (m == 18)
                SomeCell.RowHeight = 60;

            if (is_bold == true)
            {
                oSheet.Cells[m + i * 6, j].Font.Bold = true;
            }
            else
            {
                oSheet.Cells[m + i * 6, j].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(242, 242, 242));
            }
            oSheet.Cells[m + i * 6, j].Font.Size = 10;


        }
        private void change_height_date(Excel.Worksheet oSheet, int i, int j, int k, int m, string str)
        {
            oSheet.Range[oSheet.Cells[m + 6 * i, j], oSheet.Cells[m + 6 * i+3, k]].Merge();
            var SomeCell = (Excel.Range)oSheet.Cells[m + 6 * i, j];
            SomeCell.Value2 = str;
            SomeCell.RowHeight = 70;
            oSheet.Cells[m + i * 6, j].Font.Bold = true;
            oSheet.Cells[m + i * 6, j].Font.Size = 10;
            oSheet.Range[oSheet.Cells[m + 6 * i, j], oSheet.Cells[m + 6 * i + 3, k]].Borders.LineStyle = XlLineStyle.xlContinuous ;

        }
        string pval(string code_cb, string kzl_otch, string kzl_pol, string count_cb)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("CODE_CB", code_cb);
            cmd.Parameters.Add("KZL1", kzl_otch);
            cmd.Parameters.Add("KZL2", kzl_pol);
            cmd.Parameters.Add("COUNT_CB", count_cb);
            cmd.CommandText = "select pval from tbsr_stk_plg_reg where isu_cd = :CODE_CD and pldgr_crp_cd = :KZL1  and pldge_crp_cd = :KZL2 and plg_prov_qty = :COUNT_CB";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return dr[0].ToString();
            }
            return "0,000";
        }
        string st_cb(string code_cb)
        {
            string val = "";
            val = find_cb(code_cb);
            if (val == "")
            val = find_ob(code_cb);
            if (val == "1")
                return "Активный";
            else if (val == "2")
                return "Блокированный";
            else
            return "Аннулированный";
        }
        string find_cb(string code_cb)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("CODE_CB", code_cb);
            cmd.CommandText = "select dl_susp_stat_cd from tbcb_stk where isu_cd = :CODE_CD";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return dr[0].ToString();
            }
            return "";
        }
        string find_ob(string code_cb)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("CODE_CB", code_cb);
            cmd.CommandText = "select dl_susp_stat_cd from tbcb_bnd where isu_cd = :CODE_CD";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return dr[0].ToString();
            }
            return "";
        }
        string total_agst(string code_cb, string kzl_otch, string kzl_pol, string count_cb)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("CODE_CB", code_cb);
            cmd.Parameters.Add("KZL1", kzl_otch);
            cmd.Parameters.Add("KZL2", kzl_pol);
            cmd.Parameters.Add("COUNT_CB", count_cb);
            cmd.CommandText = "select tot_agst_prov_rt from tbsr_stk_plg_reg where isu_cd = :CODE_CD and pldgr_crp_cd = :KZL1 and pldge_crp_cd = :KZL2 and plg_prov_qty = :COUNT_CB ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return dr[0].ToString();
            }
            return "0,0000";
        }
        //private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        //{
        //    int row = dataGridView1.CurrentRow.Index;
        //    if (dataGridView1.SelectedCells.Count > 1)
        //    {
        //        kzl_otch = dataGridView1.Rows[row].Cells[3].Value.ToString();
        //        name_otch = dataGridView1.Rows[row].Cells[4].Value.ToString();
        //        kzl_pol = dataGridView1.Rows[row].Cells[11].Value.ToString();
        //        name_pol = dataGridView1.Rows[row].Cells[12].Value.ToString();
        //        code_cb = dataGridView1.Rows[row].Cells[5].Value.ToString();
        //        name_cb = dataGridView1.Rows[row].Cells[6].Value.ToString();
                
        //        sum_one_cb = dataGridView1.Rows[row].Cells[8].Value.ToString();
        //        count_cb = dataGridView1.Rows[row].Cells[7].Value.ToString();
        //        type_agr = dataGridView1.Rows[row].Cells[10].Value.ToString();   /////тип сделки
        //        num_agr = dataGridView1.Rows[row].Cells[1].Value.ToString();
        //        date_agr = dataGridView1.Rows[row].Cells[2].Value.ToString();
        //        sum_agr = dataGridView1.Rows[row].Cells[9].Value.ToString();
        //       // button5.Enabled = true;
        //        // crp_cd = dataGridView1.Rows[row].Cells[6].Value.ToString();
        //        // dataGridView1.ForeColor = Color.Red;
        //        // dataGridView1.GridColor = Color.Green;
        //    }
        //    else
        //    {
        //    }
        //}
    }
}
