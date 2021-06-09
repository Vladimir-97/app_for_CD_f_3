using app_for_CD.Properties;
using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace app_for_CD
{
    public partial class Auth : Form
    {
        public Auth()
        {

            InitializeComponent();
            // this.Location = new Point((SystemInformation.PrimaryMonitorSize.Width - this.Width) / 2, (SystemInformation.PrimaryMonitorSize.Height - this.Height) / 2);
            SetConnection();
            check_table();
            textBox1.Text = Settings.Default["LogName"].ToString();
        }
        static string GetHash(string plaintext)
        {
            var sha = new SHA1Managed();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(plaintext));
            return Convert.ToBase64String(hash);
        }
        private void fill_data(OracleDataReader dr)
        {
            Data.login = 1;
            string tmp_str;
            tmp_str = dr[3].ToString();
            Data.role = Int32.Parse(tmp_str);
            Data.status_t = Int16.Parse(dr[4].ToString());

        }

        private void get_fio()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("LOGIN", textBox1.Text);
            cmd.CommandText = "Select fio from users_cd where login = :LOGIN";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Data.get_fio = dr[0].ToString();
            }

        }


        OracleConnection con = null;
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


        private void button2_Click(object sender, EventArgs e)
        {
            Data.exit = false;
            this.Close();
        }

        //private void textBox1_Click(object sender, EventArgs e)
        //{
        //    if (textBox1.Text == "  Имя пользователя")
        //    {
        //        textBox1.Text = "";
        //    }
        //}

        //private void textBox2_Click(object sender, EventArgs e)
        //{
        //    textBox2.Text = "";
        //    textBox2.PasswordChar = '*';
        //}

        private void button1_Click_1(object sender, EventArgs e)
        {
            Data.exit = true;
            string name = textBox1.Text;
            string pass = textBox2.Text;
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("LOGIN", name));
            if (name == "admin")
            {
                cmd.Parameters.Add(new OracleParameter("PASSW", pass));
            }
            else
            {
                cmd.Parameters.Add(new OracleParameter("PASSW", GetHash(pass)));
            }
            cmd.CommandText = "select * from users_cd where login = :LOGIN and PASSWORD = :PASSW";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read() == true)
                {
                    fill_data(dr);
                }
                Settings.Default["LogName"] = textBox1.Text;
                Settings.Default.Save();


                if (Data.login == 1)
                {
                    if (Data.role == 0 && Data.status_t == 1)
                    {
                        //UC_Contract uc_menu = new UC_Contract();
                        //TC_Menu tc_menu = new TC_Menu();
                        //tc_menu.Show();
                        get_fio();
                        CloseConnection();
                        this.Close();
                    }
                    else if (Data.status_t == 2)
                    {
                        MessageBox.Show("Пользователь заблокирован");
                        //incorrect_pass();

                    }
                    else if (Data.role == 1 && Data.status != 2)
                    {
                        //UC_Contract uc_menu = new UC_Contract();
                        //TC_Menu tc_menu = new TC_Menu();
                        //tc_menu.Show();
                        get_fio();
                        CloseConnection();
                        this.Close();
                    }

                }
                else
                {


                    if (Data.login == 0 && Data.exit == true && Data.status_t == 1)
                    {
                        //incorrect_pass();
                    }
                    else if (Data.status_t == 2)
                    {
                        MessageBox.Show("Пользователь заблокирован");
                        //incorrect_pass();
                    }
                    else if (Data.status_t == 0)
                    {
                        MessageBox.Show("Неправильный пароль");
                        //incorrect_pass();
                    }
                }
            }
            else
            {
                MessageBox.Show("Неправильный логин/пароль или пользователь заблокирован");
            }
        }


        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.PasswordChar = '*';
        }
        private void check_table()
        {
            OracleCommand cmd = con.CreateCommand();
          //  check_users_cd_test(cmd);
            check_users_cd(cmd);
            check_table_for_docu(cmd);
            check_agrmnt_table(cmd);
            check_open_change_depo(cmd);
            check_open_change_depo_his(cmd);
            check_table_billing(cmd);
            check_new_tbcb(cmd);
            check_registration_of_invoice(cmd);
            check_tbcb_cd(cmd);   //////actived nds need to add in table tbcb_cd
        }
        //private void check_users_cd_test(OracleCommand cmd)
        //{
        //    try
        //    {
        //        cmd.CommandText = "Select * from users_cd_test";
        //        cmd.CommandType = CommandType.Text;
        //        cmd.ExecuteReader();
        //    }
        //    catch
        //    {
        //        cmd.CommandText = "CREATE TABLE USERS_CD_test (  ID VARCHAR2(5 BYTE) NOT NULL , LOGIN VARCHAR2(20 BYTE) NOT NULL, PASSWORD VARCHAR2(40 BYTE) NOT NULL , ROLE VARCHAR2(1 BYTE) NOT NULL , STATUS VARCHAR2(1 BYTE) NOT NULL , FIO VARCHAR2(100 BYTE) , POSITION VARCHAR2(60 BYTE) NOT NULL ) ";
        //        cmd.CommandType = CommandType.Text;
        //        cmd.ExecuteNonQuery();
        //        cmd.CommandText = "insert into users_cd_test(id, login, password, role, status, fio, position) values('1', 'admin', 'admin', '1', '1', 'admin adminnovich', 'главный')";
        //        cmd.CommandType = CommandType.Text;
        //        cmd.ExecuteNonQuery();
        //    }
        //}
        private void check_users_cd(OracleCommand cmd)
        {
            try
            {
                cmd.CommandText = "Select * from users_cd";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();
            }
            catch
            {
                cmd.CommandText = "CREATE TABLE USERS_CD (  ID VARCHAR2(5 BYTE) NOT NULL , LOGIN VARCHAR2(20 BYTE) NOT NULL, PASSWORD VARCHAR2(40 BYTE) NOT NULL , ROLE VARCHAR2(1 BYTE) NOT NULL , STATUS VARCHAR2(1 BYTE) NOT NULL , FIO VARCHAR2(100 BYTE) , POSITION VARCHAR2(60 BYTE) NOT NULL ) ";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "insert into users_cd(id, login, password, role, status, fio, position) values('1', 'admin', 'admin', '1', '1', 'admin adminnovich', 'главный')";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
        private void check_table_for_docu(OracleCommand cmd)
        {
            try
            {
                cmd.CommandText = "Select * from table_for_docu";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();
            }
            catch
            {
                cmd.CommandText = "CREATE TABLE TABLE_FOR_DOCU(CRP_CD VARCHAR2(12 BYTE) NOT NULL, SEQ VARCHAR2(6 BYTE) NOT NULL, DOCU_NO VARCHAR2(10 BYTE), DOCU_SRES VARCHAR2(10 BYTE), DOCU_ISSU_DD VARCHAR2(8 BYTE), DOCU_EXP_DD VARCHAR2(8 BYTE), REMARK VARCHAR2(150 BYTE), REMARK_2 VARCHAR2(150 BYTE), DOCU_STAT_CD VARCHAR2(2 BYTE)) ";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
        }
        private void check_agrmnt_table(OracleCommand cmd)
        {
            try
            {
                cmd.CommandText = "Select * from agrmnt_table";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();
            }
            catch
            {
                cmd.CommandText = "CREATE TABLE AGRMNT_TABLE (  CRP_CD VARCHAR2(12 BYTE) NOT NULL , DOCU_SRES VARCHAR2(20 BYTE) , DOCU_NO VARCHAR2(20 BYTE) NOT NULL , AGRMNT_NO VARCHAR2(10 BYTE) , AGRMNT_ISSU_DD VARCHAR2(8 BYTE) ) ";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
        }
        private void check_open_change_depo(OracleCommand cmd)
        {
            try
            {
                cmd.CommandText = "Select * from open_change_depo";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();
            }
            catch
            {
                cmd.CommandText = "CREATE TABLE OPEN_CHANGE_DEPO (  ID NUMBER NOT NULL , CRTE_DT VARCHAR2(8 BYTE) , CRP_CD VARCHAR2(12 BYTE) , FIO VARCHAR2(50 BYTE) , CRP_NM VARCHAR2(200 BYTE) )  ";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
        }
        private void check_open_change_depo_his(OracleCommand cmd)
        {
            try
            {
                cmd.CommandText = "Select * from OPEN_CHANGE_DEPO_HIS ";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();
            }
            catch
            {
                cmd.CommandText = "CREATE TABLE OPEN_CHANGE_DEPO_HIS (  ID NUMBER NOT NULL , CRP_CD VARCHAR2(12 BYTE) NOT NULL , CRP_TYPE_CD VARCHAR2(5 BYTE) NOT NULL , CRP_NM VARCHAR2(200 BYTE) , DIST_ID_TYPE_CD VARCHAR2(20 BYTE), PINFL VARCHAR2(14 BYTE) , BIRTH_DD VARCHAR2(10 BYTE) , DIST_ID_2 VARCHAR2(15 BYTE) NOT NULL , DOCU_ISSU_DD VARCHAR2(10 BYTE) , DOCU_EXP_DD VARCHAR2(10 BYTE) , REMARK VARCHAR2(1000 BYTE) , REGR_CNTRY VARCHAR2(50 BYTE) , REGR_INDEX VARCHAR2(10 BYTE) , REGR_ADDR VARCHAR2(500 BYTE) , RSDT_CNTRY VARCHAR2(50 BYTE) , RSDT_INDEX VARCHAR2(10 BYTE) , RSDT_ADDR VARCHAR2(500 BYTE) , TEL_NO VARCHAR2(18 BYTE) , FAX_NO VARCHAR2(18 BYTE) , EMAIL VARCHAR2(50 BYTE) , WEB VARCHAR2(50 BYTE) , BANK VARCHAR2(200 BYTE) , TR_BILL VARCHAR2(30 BYTE) , MFO VARCHAR2(15 BYTE) , CARD_NO VARCHAR2(20 BYTE) , CARD_ISSU VARCHAR2(10 BYTE) , RASP_BILL VARCHAR2(100 BYTE), UPD_DT VARCHAR2(10 BYTE) , USER_NM VARCHAR2(50 BYTE) , DOCU_NO VARCHAR2(15 BYTE) , RASP_SECOND VARCHAR2(200 BYTE) , RASP_THIRD VARCHAR2(500 BYTE) )  ";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
        }
        private void check_table_billing(OracleCommand cmd)
        {
            try
            {
                cmd.CommandText = "Select * from TABLE_BILLING ";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();
            }
            catch
            {
                cmd.CommandText = "CREATE TABLE TABLE_BILLING (  NUM_OF_BILL VARCHAR2(25 BYTE) , DATE_OF_BILL VARCHAR2(8 BYTE) , NUM_AGGR VARCHAR2(20 BYTE) , SRES_AGGR VARCHAR2(10 BYTE) , DATE_AGGR VARCHAR2(10 BYTE) , CRP_CD VARCHAR2(12 BYTE) , CRP_NM VARCHAR2(200 BYTE) , DIST_ID_2 VARCHAR2(10 BYTE) , NDS VARCHAR2(20 BYTE) , PINFL VARCHAR2(14 BYTE) , TYPE_SRES VARCHAR2(250 BYTE) , COST_DELIV VARCHAR2(25 BYTE) , STATE VARCHAR2(1 BYTE) , PROCESS VARCHAR2(100 BYTE) , PAYMENT_AMOUNT VARCHAR2(20 BYTE) , FIO VARCHAR2(50 BYTE) , BASE VARCHAR2(500 BYTE) , REMARK VARCHAR2(500 BYTE) , CURR VARCHAR2(20 BYTE) , COLUMN20 VARCHAR2(20 BYTE) )  ";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
        }
        private void check_new_tbcb(OracleCommand cmd)
        {
            try
            {
                cmd.CommandText = "Select * from new_tbcb";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();
            }
            catch
            {
                cmd.CommandText = "CREATE TABLE NEW_TBCB (  CRP_CD VARCHAR2(12 BYTE) NOT NULL , DOCU_PRICE VARCHAR2(150 BYTE) , GET_DD VARCHAR2(8 BYTE) , DOCU_NO VARCHAR2(10 BYTE) , ARCHV VARCHAR2(500 BYTE) , DESCRPT VARCHAR2(500 BYTE) , ESTM_CD VARCHAR2(10 BYTE) , ESTM_NM VARCHAR2(100 BYTE) , CURRENCY VARCHAR2(20 BYTE) , BLOCK_DATE VARCHAR2(8 BYTE) , REGISTERED VARCHAR2(20 BYTE) , FIO VARCHAR2(100 BYTE) NOT NULL , DOCU_SRES VARCHAR2(20 BYTE) ) ";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
        }
        private void check_registration_of_invoice(OracleCommand cmd)
        {
            try
            {
                cmd.CommandText = "Select * from REGISTRATION_OF_INVOICE ";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();
            }
            catch
            {
                cmd.CommandText = "CREATE TABLE REGISTRATION_OF_INVOICE (  ID NUMBER NOT NULL , CRP VARCHAR2(12 BYTE) NOT NULL , SER VARCHAR2(20 BYTE) NOT NULL , SERVICE_T VARCHAR2(100 BYTE) , SUM_T NUMBER , CURRENCY VARCHAR2(30 BYTE) , BASIS VARCHAR2(300 BYTE) , COMMENT_T VARCHAR2(300 BYTE) , NDS_PINFL VARCHAR2(14 BYTE) , DATE_T VARCHAR2(8 BYTE) , CRP_NM VARCHAR2(300 BYTE) , INN VARCHAR2(50 BYTE) , IF_FIZ VARCHAR2(2 BYTE) , DATE_CON VARCHAR2(8 BYTE) , FIO VARCHAR2(100 BYTE) , STATUS VARCHAR2(2 BYTE) , NUM_OF_SER VARCHAR2(10 BYTE) , PROCESS VARCHAR2(3 BYTE) , SUM_PAID NUMBER ) ";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
        }
        private void check_tbcb_cd(OracleCommand cmd)
        {
            try
            {
                cmd.CommandText = "Select nds from tbcb_cd ";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteReader();
            }
            catch
            {
                cmd.CommandText = "alter table users_cd_test add (NDS VARCHAR2(10 BYTE) DEFAULT 0 , ACTIVED VARCHAR2(20 BYTE) DEFAULT 1)";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
        }
    }
}
