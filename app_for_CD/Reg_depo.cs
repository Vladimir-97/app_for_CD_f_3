using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using app_for_CD.Properties;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;

namespace app_for_CD
{
    public partial class Reg_depo : Form
    {
        public Reg_depo()
        {
            InitializeComponent();
            label4.Visible = false;
            comboBox4.MaxLength = 12;
            this.SetConnection();
        }
        OracleConnection con = null;

        private void button4_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CRP_CD FROM TBCB_CRP_INFO where rownum <=1000";


            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr[0].ToString());
            }
        }
        private void comboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            string crp = comboBox4.SelectedItem.ToString();
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = crp;
            cmd.CommandText = "SELECT CRP_NM FROM TBCB_CRP_INFO where CRP_CD = :KZL";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox2.Text = dr[0].ToString();
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            comboBox4.Text = "";
            textBox2.Text = "";
            label4.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////////////IT PART which will add info into DB  //////////////////////////////////////////////////
            //OracleCommand cmd = con.CreateCommand();
            int id = find_max_id() + 1;
            //cmd.Parameters.Add(new OracleParameter("ID", id));
            //cmd.Parameters.Add(new OracleParameter("DAT", dateTimePicker1.Value.ToString("yyyyMMdd")  )  );
            //cmd.Parameters.Add(new OracleParameter("CRP", comboBox4.Text ));
            //cmd.Parameters.Add(new OracleParameter("FIO", Data.get_fio));


            //cmd.CommandText = "insert into open_change_depo (id, crte_dt, crp_cd, fio) values (:ID, :DAT, :CRP, :FIO)  ";
            //cmd.CommandType = CommandType.Text;
            //if (cmd.ExecuteNonQuery() == 1)
            //{
            //    label4.Visible = true;
            //}
            //////////////////////////////////////////////////////////////////////////// //////////////////////////////////////////////////////////////////////////////
            FileInfo fi;
            string excel_path;
            string path = ExecuteCommand("echo %cd%");
            string pathD = path.Substring(0, path.Length -2);
            if (is_phys(comboBox4.Text))
            {
                MessageBox.Show(pathD);
                ExecuteCommand("copy report_ph.xls report1.xls");
                excel_path = pathD + "\\report1.xls";
                fi = new FileInfo(excel_path);
            }
            else
            {
                MessageBox.Show(pathD);
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
                DateTime date = DateTime.Now;
                string date_str1 = date.ToString();
                string date_str = date_str1.Substring(0, 2);
                date_str += "/";
                date_str += date_str1.Substring(3, 2);
                date_str += "/";
                date_str += date_str1.Substring(6, 4);
                MessageBox.Show(date_str);
                oSheet.Cells[3, 9] = "Дата печати:       " + date_str;
                if (is_phys(comboBox4.Text))
                {
                    OracleCommand cmd = con.CreateCommand();
                    cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = comboBox4.Text;
                    //                             0        1         2          3       4          5             6          7   8      9     10             11           
                    cmd.CommandText = "Select crp_nm, dist_id, birth_dd, dist_id_2, reg_post_no, reg_addr_cont, tel_no, email, fax_no, homp, rsdt_post_no, rsdt_addr_cont, upd_dt  from tbcb_crp_info where crp_cd = :KZL";
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        oSheet.Cells[8, 5] = comboBox4.Text;
                        oSheet.Cells[10, 5] = "\t"+dr[0].ToString();
                        oSheet.Cells[12, 5] = "\t" + dr[1].ToString();
                        oSheet.Cells[14, 5] = parse_date(dr[2].ToString());
                        oSheet.Cells[14, 10] = dr[3].ToString();
                        // oSheet.Cells[16, 5] = "1";
                        oSheet.Cells[20, 10] = dr[4].ToString();
                        oSheet.Cells[22, 5] = dr[5].ToString();
                        oSheet.Cells[28, 5] = dr[6].ToString();
                        oSheet.Cells[32, 5] = dr[7].ToString();
                        oSheet.Cells[30, 5] = dr[8].ToString();
                        oSheet.Cells[34, 5] = dr[9].ToString();
                        oSheet.Cells[24, 10] = dr[10].ToString();
                        oSheet.Cells[26, 5] = dr[11].ToString();
                        oSheet.Cells[44, 5] = change_upd(dr[12].ToString());
                        oSheet.Cells[48, 9] = Data.get_fio;
                        oSheet.Cells[44, 10] = id;

                        //                           0          1       2       3          
                        cmd.CommandText = "Select bk_acnt_no, mfo_cd, card_no, exp_ym from tbcb_crp_bk where crp_cd = :KZL";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            oSheet.Cells[38, 5] = dr[0].ToString();
                            oSheet.Cells[38, 10] = dr[1].ToString();
                            oSheet.Cells[40, 5] = dr[2].ToString();
                            oSheet.Cells[40, 10] = change_exp(dr[3].ToString());
                            oSheet.Cells[36, 5] = find_nm_bk(dr[1].ToString());
                            cmd.CommandText = "select cd_nm from tbcb_cd where cd = (select reg_cntry_cd from tbcb_crp_info where crp_cd = :KZL) and cd_grp_no = '000033' and lang_cd = 'UZ' ";
                            cmd.CommandType = CommandType.Text;
                            dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                oSheet.Cells[20, 5] = dr[0].ToString();

                            }
                            cmd.CommandText = "select cd_nm from tbcb_cd where cd = (select rsdt_cntry_cd from tbcb_crp_info where crp_cd = :KZL) and cd_grp_no = '000033' and lang_cd = 'UZ' ";
                            cmd.CommandType = CommandType.Text;
                            dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                oSheet.Cells[24, 5] = dr[0].ToString();

                            }
                        }
                        else
                        {
                            MessageBox.Show("Нет данных о банке");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Нет данных о клиенте");
                    }
                }
                else
                {
                    oSheet.Cells[10, 5] = "123";
                }
            }
            else
            {
                MessageBox.Show("Обратитесь к Тимуру");   ////////////возможно они удалили файл
            }


        }
        string find_nm_bk(string tmp)
        {
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
            string tmp = "";
            tmp += str.Substring(0, 6);
            tmp += "20";
            tmp += str.Substring(6, 2);
            return tmp;
        }
        string change_exp(string str)
        {
            string tmp = "";
            tmp += str.Substring(2, 2);
            tmp += "/20";
            tmp += str.Substring(0, 2);
            return tmp;
        }
        private string parse_date(string str)
        {
            string tmp = "";
            tmp += str.Substring(6, 2);
            tmp += ".";
            tmp += str.Substring(4, 2);
            tmp += ".";
            tmp += str.Substring(0, 4);
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

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string crp = comboBox4.SelectedItem.ToString();
            if (crp.Length == 12)
            {
                OracleCommand cmd = con.CreateCommand();
                cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = crp;
                cmd.CommandText = "SELECT CRP_NM FROM TBCB_CRP_INFO where CRP_CD = :KZL";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    textBox2.Text = dr[0].ToString();
                }
            }
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            string crp = comboBox4.Text;
            if (crp.Length == 12)
            {
                OracleCommand cmd = con.CreateCommand();
                cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = crp;
                cmd.CommandText = "SELECT CRP_NM FROM TBCB_CRP_INFO where CRP_CD = :KZL";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    textBox2.Text = dr[0].ToString();
                }
            }
        }
    }
}
