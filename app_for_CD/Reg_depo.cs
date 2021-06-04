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
        int id;
        string crp_cd, crp_type_cd, crp_nm, dist_ip_type_cd, pinfl = "", birth_dd, dist_id_2, docu_issu_dd, docu_exp_dd, remark, regr_cntry, regr_index, regr_addr, rsdt_cntry, rsdt_index;
        string rsdt_addr, tel_no, fax_no, email, web, bank, tr_bill, mfo, card_no, card_issu, rasp_bill, udp_dt, user_nm, docu_no , rasp_second = "", rasp_third = "";

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
            OracleCommand cmd = con.CreateCommand();
            id = find_max_id() + 1;
            user_nm = Data.get_fio;
            crp_cd = comboBox4.Text;
            cmd.Parameters.Add(new OracleParameter("ID", id));
            cmd.Parameters.Add(new OracleParameter("DAT", dateTimePicker1.Value.ToString("yyyyMMdd")));
            cmd.Parameters.Add(new OracleParameter("CRP", comboBox4.Text));
            cmd.Parameters.Add(new OracleParameter("FIO", Data.get_fio));
            cmd.Parameters.Add(new OracleParameter("CRP_NM", textBox2.Text));


            cmd.CommandText = "insert into open_change_depo (id, crte_dt, crp_cd, fio,crp_nm) values (:ID, :DAT, :CRP, :FIO, :CRP_NM)  ";
            cmd.CommandType = CommandType.Text;
            if (cmd.ExecuteNonQuery() == 1)
            {
                label4.Visible = true;
            }
            //////////////////////////////////////////////////////////////////////////// //////////////////////////////////////////////////////////////////////////////
            FileInfo fi;
            string excel_path;
            string path = ExecuteCommand("echo %cd%");
            string pathD = path.Substring(0, path.Length -2);
            if (is_phys(comboBox4.Text))
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
                DateTime date = DateTime.Now;
                string date_str1 = date.ToString();
                string date_str = date_str1.Substring(0, 2);
                date_str += "/";
                date_str += date_str1.Substring(3, 2);
                date_str += "/";
                date_str += date_str1.Substring(6, 4);
                oSheet.Cells[3, 10] = date_str;
                if (is_phys(comboBox4.Text))
                {
                    cmd = con.CreateCommand();
                    cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = comboBox4.Text;
                    //                             0        1         2          3       4          5             6          7   8      9     10             11           
                    cmd.CommandText = "Select crp_nm, dist_id, birth_dd, dist_id_2, reg_post_no, reg_addr_cont, tel_no, email, fax_no, homp, rsdt_post_no, rsdt_addr_cont, upd_dt  from tbcb_crp_info where crp_cd = :KZL";
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        oSheet.Cells[8, 5] = comboBox4.Text;
                        oSheet.Cells[10, 5] = "\t"+dr[0].ToString();
                        crp_nm = dr[0].ToString();
                        oSheet.Cells[12, 5] = "\t" + dr[1].ToString();
                        docu_no = dr[1].ToString();
                        oSheet.Cells[14, 5] = parse_date(dr[2].ToString());
                        birth_dd = parse_date(dr[2].ToString());
                        oSheet.Cells[14, 10] = dr[3].ToString();
                        dist_id_2 = dr[3].ToString();
                        // oSheet.Cells[16, 5] = "1";
                        oSheet.Cells[20, 10] = dr[4].ToString();
                        regr_index = dr[4].ToString();
                        oSheet.Cells[28, 5] = dr[6].ToString();
                        tel_no = dr[6].ToString();
                        oSheet.Cells[32, 5] = dr[7].ToString();
                        email = dr[7].ToString();
                        oSheet.Cells[30, 5] = dr[8].ToString();
                        fax_no = dr[8].ToString();
                        oSheet.Cells[34, 5] = dr[9].ToString();
                        web = dr[9].ToString();
                        oSheet.Cells[24, 10] = dr[10].ToString();
                        rsdt_index = dr[10].ToString();
                        oSheet.Cells[44, 5] = change_upd(dr[12].ToString());
                        udp_dt = change_upd(dr[12].ToString());
                        oSheet.Cells[48, 9] = Data.get_fio;
                        oSheet.Cells[44, 10] = id;

                        //                           0          1       2       3          
                        cmd.CommandText = "Select bk_acnt_no, mfo_cd, card_no, exp_ym from tbcb_crp_bk where crp_cd = :KZL";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            oSheet.Cells[38, 5] = dr[0].ToString();
                            tr_bill = dr[0].ToString();
                            oSheet.Cells[38, 10] = dr[1].ToString();
                            mfo = dr[1].ToString();
                            oSheet.Cells[40, 5] = dr[2].ToString();
                            card_no = dr[2].ToString();
                            oSheet.Cells[40, 10] = change_exp(dr[3].ToString());
                            card_issu = change_exp(dr[3].ToString());
                            oSheet.Cells[36, 5] = find_nm_bk(dr[1].ToString());
                            bank = find_nm_bk(dr[1].ToString());

                        }
                        else
                        {
                            oSheet.Cells[38, 5] = "";
                            oSheet.Cells[38, 10] = "";
                            oSheet.Cells[40, 5] = "";
                            oSheet.Cells[40, 10] = "";
                            oSheet.Cells[36, 5] = "";

                        }
                        cmd.CommandText = "select cd_nm from tbcb_cd where cd = (select reg_cntry_cd from tbcb_crp_info where crp_cd = :KZL) and cd_grp_no = '000033' and lang_cd = 'UZ' ";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            oSheet.Cells[20, 5] = dr[0].ToString();
                            regr_cntry = dr[0].ToString();
                        }
                        cmd.CommandText = "select cd_nm from tbcb_cd where cd = (select rsdt_cntry_cd from tbcb_crp_info where crp_cd = :KZL) and cd_grp_no = '000033' and lang_cd = 'UZ' ";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            oSheet.Cells[24, 5] = dr[0].ToString();
                            rsdt_cntry = dr[0].ToString();
                    
                        }
                        cmd.CommandText = "select cd_nm from tbcb_cd where cd = (select dist_id_type_cd from tbcb_crp_docu_info where seq = (select max(seq) from tbcb_crp_docu_info where crp_cd = :KZL) and crp_cd = :KZL) and cd_grp_no = '000035' and lang_cd = 'UZ' ";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            oSheet.Cells[12, 3] = dr[0].ToString();
                            dist_ip_type_cd = dr[0].ToString();
                        }
                        cmd.CommandText = "select docu_issu_dd, docu_exp_dd from tbcb_crp_docu_info where seq = (select max(seq) from tbcb_crp_docu_info where crp_cd = :KZL) and crp_cd = :KZL ";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            oSheet.Cells[16, 5] = parse_date(dr[0].ToString());
                            oSheet.Cells[16, 10] = parse_date(dr[1].ToString());
                            docu_issu_dd = parse_date(dr[0].ToString());
                            docu_exp_dd = parse_date(dr[1].ToString());

                        }
                        cmd.CommandText = "select cd_nm from tbcb_cd where cd = (select regr_cd from tbcb_crp_docu_info where seq = (select max(seq) from tbcb_crp_docu_info where crp_cd = :KZL) and crp_cd = :KZL) and lang_cd = 'UZ' ";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            if (dr[0].ToString() != "ОВД")
                            {
                                oSheet.Cells[18, 5] = dr[0].ToString();
                                remark = dr[0].ToString();
                            }
                            else
                            {
                                cmd.CommandText = "select remark from tbcb_crp_docu_info where seq = 1 and crp_cd = :KZL ";
                                cmd.CommandType = CommandType.Text;
                                dr = cmd.ExecuteReader();
                                if (dr.Read())
                                {
                                    oSheet.Cells[18, 5] = dr[0].ToString();
                                    remark = dr[0].ToString();
                                }
                            }
                        }
                        cmd.CommandText = "select reps_nm from tbcb_crp_reps where crp_cd = :KZL and pow_type = 1";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            oSheet.Cells[42, 5] = dr[0].ToString();
                            rasp_bill = dr[0].ToString();
                        }
                        string addr1 = "", addr2 = "";
                        cmd.CommandText = "select cd_nm from tbcb_cd where cd = (select reg_regn_cd from tbcb_crp_info where crp_cd = :KZL) and cd_grp_no = '100042' and lang_cd = 'UZ'";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            addr1 += dr[0].ToString() + " ";
                        }
                        cmd.CommandText = "select cd_nm from tbcb_cd where cd = (select rsdt_regn_cd from tbcb_crp_info where crp_cd = :KZL) and cd_grp_no = '100042' and lang_cd = 'UZ'";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            addr2 += dr[0].ToString() + " ";
                        }
                        cmd.CommandText = "select soato_nm from tbcb_soato_info where soato_cd = (select reg_soato_cd from tbcb_crp_info where crp_cd = :KZL)";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            addr1 += dr[0].ToString() + " ";
                        }
                        cmd.CommandText = "select soato_nm from tbcb_soato_info where soato_cd = (select rsdt_soato_cd from tbcb_crp_info where crp_cd = :KZL)";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            addr2 += dr[0].ToString() + " ";
                        }
                        cmd.CommandText = "select reg_stre_addr, rsdt_stre_addr from tbcb_crp_info where crp_cd = :KZL";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            addr1 += dr[0].ToString();
                            addr2 += dr[1].ToString();
                            oSheet.Cells[22, 5] = addr1;
                            oSheet.Cells[26, 5] = addr2;
                            regr_addr = addr1;
                            rsdt_addr = addr2;
                            insert_into_depo_his();
                        }
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
                      //  ExecuteCommand("del report1.xls");
                    }
                }
                else       ///////////////////////////////////////////////////////////////////yur lico       //////////////////////////////////////////////////////////////////////////////////////////////
                {
                    
                    cmd = con.CreateCommand();
                    cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = comboBox4.Text;
                    //                             0        1         2          3       4          5             6          7   8      9     10             11           
                    cmd.CommandText = "Select crp_nm, dist_id, birth_dd, dist_id_2, reg_post_no, reg_addr_cont, tel_no, email, fax_no, homp, rsdt_post_no, rsdt_addr_cont, upd_dt  from tbcb_crp_info where crp_cd = :KZL";
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        oSheet.Cells[8, 5] = comboBox4.Text;
                        oSheet.Cells[10, 5] = "\t" + dr[0].ToString();
                        crp_nm = dr[0].ToString();
                        oSheet.Cells[12, 5] = "\t" + dr[1].ToString();
                        docu_no = dr[1].ToString();
                        oSheet.Cells[14, 5] = parse_date(dr[2].ToString());
                        birth_dd = parse_date(dr[2].ToString());
                        oSheet.Cells[12, 10] = dr[3].ToString();
                        dist_id_2 = dr[3].ToString();
                        // oSheet.Cells[16, 5] = "1";
                        oSheet.Cells[18, 10] = dr[4].ToString();
                        regr_index = dr[4].ToString();
                        oSheet.Cells[26, 5] = dr[6].ToString();
                        tel_no = dr[6].ToString();
                        oSheet.Cells[30, 5] = dr[7].ToString();
                        email = dr[7].ToString();
                        oSheet.Cells[28, 5] = dr[8].ToString();
                        fax_no = dr[8].ToString();
                        oSheet.Cells[32, 5] = dr[9].ToString();
                        web = dr[9].ToString();
                        oSheet.Cells[22, 10] = dr[10].ToString();
                        rsdt_index = dr[10].ToString();
                        oSheet.Cells[46, 5] = change_upd(dr[12].ToString());
                        udp_dt = change_upd(dr[12].ToString());
                        oSheet.Cells[49, 9] = Data.get_fio;
                        oSheet.Cells[46, 10] = id;

                        //                           0          1       2       3          
                        cmd.CommandText = "Select bk_acnt_no, mfo_cd from tbcb_crp_bk where crp_cd = :KZL";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            oSheet.Cells[36, 5] = dr[0].ToString();
                            tr_bill = dr[0].ToString();
                            oSheet.Cells[36, 10] = dr[1].ToString();
                            mfo = dr[1].ToString();
                            oSheet.Cells[34, 5] = find_nm_bk(dr[1].ToString());
                            bank = find_nm_bk(dr[1].ToString());

                        }
                        else
                        {
                            oSheet.Cells[36, 5] = "";
                            oSheet.Cells[36, 10] = "";

                        }
                        cmd.CommandText = "select cd_nm from tbcb_cd where cd = (select reg_cntry_cd from tbcb_crp_info where crp_cd = :KZL) and cd_grp_no = '000033' and lang_cd = 'UZ' ";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            oSheet.Cells[18, 5] = dr[0].ToString();
                            regr_cntry = dr[0].ToString();
                        }
                        cmd.CommandText = "select cd_nm from tbcb_cd where cd = (select rsdt_cntry_cd from tbcb_crp_info where crp_cd = :KZL) and cd_grp_no = '000033' and lang_cd = 'UZ' ";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            oSheet.Cells[22, 5] = dr[0].ToString();
                            rsdt_cntry = dr[0].ToString();

                        }
                        //cmd.CommandText = "select cd_nm from tbcb_cd where cd = (select dist_id_type_cd from tbcb_crp_docu_info where seq = (select max(seq) from tbcb_crp_docu_info where crp_cd = :KZL) and crp_cd = :KZL) and cd_grp_no = '000035' and lang_cd = 'UZ' ";
                        //cmd.CommandType = CommandType.Text;
                        //dr = cmd.ExecuteReader();
                        //if (dr.Read())
                        //{
                        //    oSheet.Cells[10, 3] = dr[0].ToString();
                        //    dist_ip_type_cd = dr[0].ToString();
                        //}
                        cmd.CommandText = "select cd_nm from tbcb_cd where cd = (select regr_cd from tbcb_crp_docu_info where seq = (select max(seq) from tbcb_crp_docu_info where crp_cd = :KZL) and crp_cd = :KZL) and lang_cd = 'UZ' ";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            if (dr[0].ToString() != "ОВД")
                            {
                                oSheet.Cells[16, 5] = dr[0].ToString();
                                remark = dr[0].ToString();
                            }
                            else
                            {
                                cmd.CommandText = "select remark from tbcb_crp_docu_info where seq = 1 and crp_cd = :KZL ";
                                cmd.CommandType = CommandType.Text;
                                dr = cmd.ExecuteReader();
                                if (dr.Read())
                                {
                                    oSheet.Cells[16, 5] = dr[0].ToString();
                                    remark = dr[0].ToString();
                                }
                            }
                        }
                        cmd.CommandText = "select eng_reps_nm from tbcb_crp_reps where crp_cd = :KZL and pow_type = 1";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        rasp_bill = "";
                        while (dr.Read())
                        {
                            rasp_bill += dr[0].ToString();
                        }
                        oSheet.Cells[40, 5] = rasp_bill;
                        cmd.CommandText = "select eng_reps_nm from tbcb_crp_reps where crp_cd = :KZL and pow_type = 2";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            rasp_second += dr[0].ToString() + ", ";
                        }
                        oSheet.Cells[42, 5] = rasp_second;
                        cmd.CommandText = "select eng_reps_nm from tbcb_crp_reps where crp_cd = :KZL and pow_type = 3";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            rasp_third += dr[0].ToString() + ", ";
                        }
                        oSheet.Cells[44, 5] = rasp_third;






                        string addr1 = "", addr2 = "";
                        cmd.CommandText = "select cd_nm from tbcb_cd where cd = (select reg_regn_cd from tbcb_crp_info where crp_cd = :KZL) and cd_grp_no = '100042' and lang_cd = 'UZ'";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            addr1 += dr[0].ToString() + " ";
                        }
                        cmd.CommandText = "select cd_nm from tbcb_cd where cd = (select rsdt_regn_cd from tbcb_crp_info where crp_cd = :KZL) and cd_grp_no = '100042' and lang_cd = 'UZ'";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            addr2 += dr[0].ToString() + " ";
                        }
                        cmd.CommandText = "select soato_nm from tbcb_soato_info where soato_cd = (select reg_soato_cd from tbcb_crp_info where crp_cd = :KZL)";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            addr1 += dr[0].ToString() + " ";
                        }
                        cmd.CommandText = "select soato_nm from tbcb_soato_info where soato_cd = (select rsdt_soato_cd from tbcb_crp_info where crp_cd = :KZL)";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            addr2 += dr[0].ToString() + " ";
                        }
                        cmd.CommandText = "select reg_stre_addr, rsdt_stre_addr from tbcb_crp_info where crp_cd = :KZL";
                        cmd.CommandType = CommandType.Text;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            addr1 += dr[0].ToString();
                            addr2 += dr[1].ToString();
                            oSheet.Cells[20, 5] = addr1;
                            oSheet.Cells[24, 5] = addr2;
                            regr_addr = addr1;
                            rsdt_addr = addr2;
                            insert_into_depo_his();
                        }
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
        bool is_phys(string str)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", str));
            cmd.CommandText = "select crp_type_cd from tbcb_crp_info where crp_cd = :KZL  ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                crp_type_cd = dr[0].ToString();
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
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        void insert_into_depo_his()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("ID", id));
            cmd.Parameters.Add(new OracleParameter("CRP_CD", crp_cd));
            cmd.Parameters.Add(new OracleParameter("CRP_TYPE_CD", crp_type_cd));
            cmd.Parameters.Add(new OracleParameter("CRP_NM", crp_nm));
            cmd.Parameters.Add(new OracleParameter("DIST_ID_TYPE_CD", dist_ip_type_cd));
            cmd.Parameters.Add(new OracleParameter("PINFL", pinfl));
            cmd.Parameters.Add(new OracleParameter("BIRTH_DD", birth_dd));
            cmd.Parameters.Add(new OracleParameter("DIST_ID_2", dist_id_2));
            cmd.Parameters.Add(new OracleParameter("DOCU_ISSU_DD", docu_issu_dd));
            cmd.Parameters.Add(new OracleParameter("DOCU_EXP_DD", docu_exp_dd));
            cmd.Parameters.Add(new OracleParameter("REMARK", remark));
            cmd.Parameters.Add(new OracleParameter("REGR_CNTRY", regr_cntry));
            cmd.Parameters.Add(new OracleParameter("REGR_INDEX", regr_index));
            cmd.Parameters.Add(new OracleParameter("REGR_ADDR", regr_addr));
            cmd.Parameters.Add(new OracleParameter("RSDT_CNTRY", rsdt_cntry));
            cmd.Parameters.Add(new OracleParameter("RSDT_INDEX", rsdt_index));
            cmd.Parameters.Add(new OracleParameter("RSDT_ADDR", rsdt_addr));
            cmd.Parameters.Add(new OracleParameter("TEL_NO", tel_no));
            cmd.Parameters.Add(new OracleParameter("FAX_NO", fax_no));
            cmd.Parameters.Add(new OracleParameter("EMAIL", email));
            cmd.Parameters.Add(new OracleParameter("WEB", web));
            cmd.Parameters.Add(new OracleParameter("BANK", bank));
            cmd.Parameters.Add(new OracleParameter("TR_BILL", tr_bill));
            cmd.Parameters.Add(new OracleParameter("MFO", mfo));
            cmd.Parameters.Add(new OracleParameter("CARD_NO", card_no));
            cmd.Parameters.Add(new OracleParameter("CARD_ISSU", card_issu));
            cmd.Parameters.Add(new OracleParameter("RASP_BILL", rasp_bill));
            cmd.Parameters.Add(new OracleParameter("UDP_DT", udp_dt));
            cmd.Parameters.Add(new OracleParameter("USER_NM", user_nm));
            cmd.Parameters.Add(new OracleParameter("DOCU_NO", docu_no));
            cmd.Parameters.Add(new OracleParameter("RASP_SECOND", rasp_second));
            cmd.Parameters.Add(new OracleParameter("RASP_THIRD", rasp_third));
            cmd.CommandText = "insert into open_change_depo_his (id, crp_cd, CRP_TYPE_CD, CRP_NM, DIST_ID_TYPE_CD, PINFL,  BIRTH_DD, DIST_ID_2, DOCU_ISSU_DD, DOCU_EXP_DD, REMARK, REGR_CNTRY, REGR_INDEX, REGR_ADDR, RSDT_CNTRY, RSDT_INDEX, RSDT_ADDR, TEL_NO, FAX_NO, EMAIL, WEB, BANK, TR_BILL, MFO, CARD_NO, CARD_ISSU, RASP_BILL, UPD_DT, USER_NM, docu_no, rasp_second, rasp_third) VALUES (:ID, :CRP_CD, :CRP_TYPE_CD, :CRP_NM, :DIST_ID_TYPE_CD, :PINFL, :BIRTH_DD, :DIST_ID_2, :DOCU_ISSU_DD, :DOCU_EXP_DD, :REMARK, :REGR_CNTRY, :REGR_INDEX, :REGR_ADDR, :RSDT_CNTRY, :RSDT_INDEX, :RSDT_ADDR, :TEL_NO, :FAX_NO, :EMAIL, :WEB, :BANK, :TR_BILL, :MFO, :CARD_NO, :CARD_ISSU, :RASP_BILL, :RASP_BILL,:USER_NM, :DOCU_NO, :RASP_SECOND, :RASP_THIRD)   ";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }

    }
}
