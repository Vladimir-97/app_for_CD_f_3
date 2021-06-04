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
//2
namespace app_for_CD
{
    public partial class Add : Form
    {
        string doc_num_, ser_num_, date_agg_, state_, kzl_;
        OracleConnection con = null;
        int count_row = 0, interval = 30;

        public Add()
        {
            InitializeComponent();
            this.SetConnection();
            label20.Visible = false;
        }
        public Add(string doc_num, string ser_num, string date_agg, string state, string kzl, int but)
        {
            InitializeComponent();
            SetConnection();
            label20.Visible = false;
            this.doc_num_ = doc_num;
            this.ser_num_ = ser_num;
            this.date_agg_ = date_agg;
            this.state_ = state;
            this.kzl_ = kzl;
            Name_company.Enabled = false;
            //textBox1.Enabled = false;
            comboBox4.MaxLength = 12;
            dateTimePicker3.Visible = false;
            label19.Visible = false;
            button6.Enabled = false;
            //   textBox6.Enabled = false;
            load_sres();
            load_currency();
            //     Name_company.Enabled = false;
            if (but == 1)   //добавить
            {
                button1.Visible = false;
                comboBox3.Text = "формируется";
                comboBox3.Enabled = false;
                comboBox6.Text = "UZS";
                dateTimePicker4.Enabled = false;
                textBox6.Enabled = false;
            }
            if (but == 2) ////просмотр/изменение
            {
                //  SetConnection();
                button4.Visible = false;
                button5.Visible = false;
                //button6.Visible = false;
                //   button1.Visible = false;
                //   button2.Visible = false;
                button3.Visible = false;
                inverse_parse_date("20801231", dateTimePicker3);
                show_values();
            }
            if (but == 3)
            {
                // SetConnection();
                button4.Visible = false;
                button5.Visible = false;
                button3.Visible = false;
                button2.Visible = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                dateTimePicker3.Visible = true;
                //textBox6.Enabled = false;
                //inverse_parse_date("20801231", dateTimePicker3);

                update_values();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.SetConnection();
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

            // button6.Enabled = false;
            string crp = comboBox4.SelectedItem.ToString();
            if (crp.Length < 12)
            {
                button6.Enabled = false;
            }
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = crp;
            cmd.CommandText = "SELECT CRP_NM FROM TBCB_CRP_INFO where CRP_CD = :KZL";
            kzl_ = crp;
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox6.Text = dr[0].ToString();
                button6.Enabled = true;
            }
            comboBox5.Items.Clear();
            ///////////////////////////////
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.SetConnection();
            OracleCommand cmd = con.CreateCommand();
            if (comboBox4.Text != "")
            {
                cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = comboBox4.Text;
                cmd.CommandText = "SELECT DOCU_NO, DOCU_SRES FROM table_for_docu where CRP_CD = :KZL AND rownum <=1000  ";


                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                // List<string[]> data = new List<string[]>();
                while (dr.Read())
                {
                    comboBox5.Items.Add(dr[0].ToString()); /*+ " " + dr[1].ToString())*/
                }
            }
        }
        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            string crp = comboBox4.Text.ToString();
            if (crp.Length < 12)
            {
                button6.Enabled = false;
            }
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 12).Value = crp;
            cmd.CommandText = "SELECT CRP_NM FROM TBCB_CRP_INFO where CRP_CD = :KZL";
            kzl_ = crp;
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox6.Text = dr[0].ToString();
                button6.Enabled = true;
            }
            comboBox5.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox5.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox1.SelectedItem = "";
            //check_value();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;
            dateTimePicker5.Value = DateTime.Now;
            //label20.Visible = false;
            //Add_docu_series tmp = new Add_docu_series();
            //tmp.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //if (check_empty_str())
            //{
            SetConnection();
            if (button6.Enabled && count_row > 0)
            {
                for (int i = 1; i <= Data.was_count; i++)
                {
                    query_update_agrmnt_table(i);
                }
                for (int i = Data.was_count + 1; i <= count_row; i++)
                    query_insert_agrmnt_table(i);
            }

            query_insert_docu_info();
            query_insert_tbcb_new();

            // }

        }

        /// <summary>
        /// ///////////////////////////////////////////////////2 ЗАПРОСА РАБОТАЮТ!!!!!!!!!!!!!! ///////////////////////////
        /// </summary>
        /// <returns></returns>
        bool check_empty_str()
        {
            if (comboBox4.Text == "" || textBox6.Text == "" || comboBox5.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || Name_company.Text == "" || comboBox3.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Введите все необходимые поля!");
                return false;
            }
            return true;
        }
        private int find_max_seq()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", comboBox4.Text));
            cmd.CommandText = "select max(seq) from table_for_docu where crp_cd = :KZL";
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
        void query_insert_docu_info()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", comboBox4.Text));
            int seq = find_max_seq() + 1;
            cmd.Parameters.Add(new OracleParameter("SEQ", seq));

            cmd.Parameters.Add(new OracleParameter("NUM_DOCU", comboBox5.Text));
            cmd.Parameters.Add(new OracleParameter("SER_DOCU", comboBox1.Text));
            string st_date = dateTimePicker1.Value.ToString("yyyyMMdd");
            cmd.Parameters.Add(new OracleParameter("DOCU_ISSU", st_date));
            string end_date = dateTimePicker2.Value.ToString("yyyyMMdd");
            cmd.Parameters.Add(new OracleParameter("EXP_DOCU", end_date));
            cmd.Parameters.Add(new OracleParameter("REM1", textBox7.Text));
            cmd.Parameters.Add(new OracleParameter("REM2", textBox8.Text));
            cmd.Parameters.Add(new OracleParameter("STAT", reverse_check_stat(comboBox3.Text)));
            //string reg_date = parse_date(dateTimePicker5.Value.ToString(), 1);
            //cmd.Parameters.Add(new OracleParameter("REG_DOCU", reg_date));


            cmd.CommandText = "insert into table_for_docu(crp_cd, SEQ, docu_no, docu_sres, docu_issu_dd, docu_exp_dd, remark, remark_2, docu_stat_cd)values(:KZL, :SEQ, :NUM_DOCU, :SER_DOCU, :DOCU_ISSU, :EXP_DOCU , :REM1, :REM2, :STAT) ";

            cmd.CommandType = CommandType.Text;   ///issu_dd yyyymmdd        reg_docu dd.mm.yyyy
            cmd.ExecuteNonQuery();

        }

        void query_insert_tbcb_new()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", comboBox4.Text));

            var price = textBox3.Text.ToString();
            cmd.Parameters.Add(new OracleParameter("DOCU_PR", price));

            var date = dateTimePicker4.Value.ToString("yyyyMMdd");
            cmd.Parameters.Add(new OracleParameter("REC", date));
            string docu_num = comboBox5.Text.ToString();
            cmd.Parameters.Add(new OracleParameter("DOCU_NO", docu_num));

            cmd.Parameters.Add(new OracleParameter("ARCHEE", textBox2.Text));
            cmd.Parameters.Add(new OracleParameter("DESCR", textBox4.Text));
            var tmp_int = fun_ischis(comboBox2.Text);
            cmd.Parameters.Add(new OracleParameter("ISCHIS", tmp_int));
            cmd.Parameters.Add(new OracleParameter("PAR_ISCHIS", comboBox2.Text));
            cmd.Parameters.Add(new OracleParameter("CURRENCY", comboBox6.Text));
            cmd.Parameters.Add(new OracleParameter("BLOCK", "20801231"));
            cmd.Parameters.Add(new OracleParameter("REG", dateTimePicker5.Value.ToString("yyyyMMdd")));
            cmd.Parameters.Add(new OracleParameter("FIO", Data.get_fio));
            cmd.Parameters.Add(new OracleParameter("SRES", comboBox1.Text));


            cmd.CommandText = "insert into new_tbcb (crp_cd,docu_price,get_dd,docu_no, archv, descrpt, estm_cd, estm_nm, currency, block_date,registered, fio, docu_sres)  values(:KZL,:DOCU_PR, :REC, :DOCU_NO, :ARCHEE, :DESCR, :ISCHIS, :PAR_ISCHIS, :CURRENCY, :BLOCK, :REG, :FIO,:SRES)";
            cmd.CommandType = CommandType.Text;
            if (cmd.ExecuteNonQuery() == 1)
                label20.Visible = true;
            else
                MessageBox.Show("Не получилось добавить");
        }
        private void query_update_agrmnt_table(int row)
        {
            OracleCommand cmd = con.CreateCommand();

            var cultureInfo = new System.Globalization.CultureInfo("ru-Ru");
            string dateString = tableLayoutPanel1.Controls[row * 5 + 3].Text;
            var dateTime = DateTime.Parse(dateString, cultureInfo);
            ////MessageBox.Show(dateTime.ToString("yyyyMMdd"));
            cmd.Parameters.Add("ISSU", dateTime.ToString("yyyyMMdd"));

            cmd.Parameters.Add("KZL", comboBox4.Text);
            cmd.Parameters.Add("DOCU_SRES", comboBox1.Text);
            cmd.Parameters.Add("DOCU_NO", comboBox5.Text);
            cmd.Parameters.Add("ADD_C", tableLayoutPanel1.Controls[row * 5 + 1].Text);
            cmd.CommandText = "update agrmnt_table set agrmnt_issu_dd = :ISSU where crp_cd = :KZL and docu_sres = :DOCU_SRES and docu_no = :DOCU_NO and agrmnt_no = :ADD_C";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

        }
        private void query_insert_agrmnt_table(int row)
        {
            OracleCommand cmd = con.CreateCommand();

            cmd.Parameters.Add("KZL", comboBox4.Text);
            cmd.Parameters.Add("DOCU_SRES", comboBox1.Text);
            cmd.Parameters.Add("DOCU_NO", comboBox5.Text);
            cmd.Parameters.Add("ADD_C", tableLayoutPanel1.Controls[row * 5 + 1].Text);
            //  dtp = tableLayoutPanel1.Controls[row * 5 + 3].AccessibilityObject;


            var cultureInfo = new System.Globalization.CultureInfo("ru-Ru");
            string dateString = tableLayoutPanel1.Controls[row * 5 + 3].Text;
            var dateTime = DateTime.Parse(dateString, cultureInfo);
            ////MessageBox.Show(dateTime.ToString("yyyyMMdd"));
            cmd.Parameters.Add("ISSU", dateTime.ToString("yyyyMMdd"));
            cmd.CommandText = "insert into agrmnt_table (crp_cd, docu_sres, docu_no, agrmnt_no, agrmnt_issu_dd) values (:KZL, :DOCU_SRES,:DOCU_NO, :ADD_C, :ISSU)";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

        }
        int fun_ischis(string str)
        {
            if (str == "Сумма")
                return 1;
            return 2;
        }
        string parse(string tmp)
        {
            string date = "";
            date = tmp.Substring(6, 9);
            date += tmp.Substring(3, 4);
            date += tmp.Substring(0, 1);
            return date;
        }
        void inverse_parse_date(string str, DateTimePicker dateTimePicker_tmp)
        {
            string tmp_yy = str[0].ToString() + str[1].ToString() + str[2].ToString() + str[3].ToString();
            int yy = Int16.Parse(tmp_yy);
            string tmp_mm = str[4].ToString() + str[5].ToString();
            int mm = Int16.Parse(tmp_mm);
            string tmp_dd = str[6].ToString() + str[7].ToString();
            int dd = Int16.Parse(tmp_dd);
            int x = Convert.ToInt32(yy);
            int y = Convert.ToInt32(mm);
            int z = Convert.ToInt32(dd);

            dateTimePicker_tmp.Value = new DateTime(x, y, z);

        }
        void show_values()
        {
            has_values();

        }
        void has_values()
        {
            comboBox4.Text = kzl_;
            comboBox5.Text = doc_num_;
            comboBox1.Text = ser_num_;
            show_from_new_tbcb();
            show_from_crp_docu_info();
            show_from_crp_info();
            show_from_agrmnt_table();
            textBox6.Enabled = false;
            comboBox4.Enabled = false;

        }
        void show_from_agrmnt_table()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", comboBox4.Text));
            cmd.Parameters.Add(new OracleParameter("DOCU_NO", comboBox5.Text));
            cmd.Parameters.Add(new OracleParameter("DOCU_SRES", comboBox1.Text));
            cmd.CommandText = "select * from agrmnt_table where crp_cd = :KZL and docu_no = :DOCU_NO and docu_sres = :DOCU_SRES order by agrmnt_no";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Data.was_count++;
                button6_Click(Data.was_count, dr[3].ToString(), dr[4].ToString());
                //tableLayoutPanel1.Controls[Data.was_count + 1].Text = dr[3].ToString();
                //tableLayoutPanel1.Controls[Data.was_count + 3].Text = ;
                ///////////////////////////////////////////////////////Дописать ///////////////////////////////////////////////////////////

            }
            count_row = Data.was_count;

            // Data.was_count++;
        }
        void show_from_new_tbcb()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", kzl_));
            cmd.Parameters.Add(new OracleParameter("DOCU_NO", doc_num_));
            comboBox5.Text = doc_num_;
            cmd.CommandText = "Select * from new_tbcb where crp_cd = :KZL AND docu_no = :DOCU_NO";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    textBox3.Text = dr[1].ToString();
                    inverse_parse_date(dr[2].ToString(), dateTimePicker4);
                    textBox2.Text = check_null(dr[4].ToString());
                    textBox4.Text = check_null(dr[5].ToString());
                    comboBox2.Text = check_null(dr[7].ToString());
                    comboBox6.Text = check_null(dr[8].ToString());
                    if (dr[9].ToString() != "")
                        inverse_parse_date(dr[9].ToString(), dateTimePicker3);
                    inverse_parse_date(dr[10].ToString(), dateTimePicker5);

                }
            }


        }
        void show_from_crp_docu_info()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", kzl_));
            cmd.Parameters.Add(new OracleParameter("DOCU_NO", doc_num_));
            cmd.Parameters.Add(new OracleParameter("DOC_SER", ser_num_));

            cmd.CommandText = "Select remark, remark_2, docu_issu_dd, docu_exp_dd, docu_stat_cd from table_for_docu where crp_cd = :KZL AND docu_no = :DOCU_NO and DOCU_SRES = :DOC_SER";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    textBox7.Text = check_null(dr[0].ToString());
                    textBox8.Text = check_null(dr[1].ToString());
                    inverse_parse_date(dr[2].ToString(), dateTimePicker1);
                    inverse_parse_date(dr[3].ToString(), dateTimePicker2);
                    comboBox3.Text = check_stat(dr[4].ToString());
                }
            }

        }
        void show_from_crp_info()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", kzl_));

            cmd.CommandText = "Select crp_stat_cd from tbcb_crp_info where crp_cd = :KZL";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string tmp_str = dr[0].ToString();
                    //comboBox3.Text = check_stat(dr[0].ToString());
                    if (dr[0].ToString() == "4")
                    {
                        dateTimePicker3.Visible = true;
                        label19.Visible = true;
                    }
                    else
                    {
                        dateTimePicker3.Visible = false;
                        label19.Visible = false;
                    }
                    //  inverse_parse_date(dr[1].ToString(), dateTimePicker5);
                }
            }
        }
        void update_values()
        {
            this.SetConnection();
            comboBox4.Text = kzl_;
            comboBox5.Text = doc_num_;
            comboBox1.Text = ser_num_;
            show_from_new_tbcb();
            show_from_crp_info();
            show_from_crp_docu_info();
            comboBox5.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.SetConnection();
            // update_crp_info();
            if (button6.Enabled && count_row > 0)
            {
                for (int i = 1; i <= Data.was_count; i++)
                {
                    query_update_agrmnt_table(i);
                }
                for (int i = Data.was_count + 1; i <= count_row; i++)
                    query_insert_agrmnt_table(i);
            }
            update_crp_docu_info();
            update_new_tbcb();
        }

        void update_crp_docu_info()
        {
            OracleCommand cmd = con.CreateCommand();

            string st_date = dateTimePicker1.Value.ToString("yyyyMMdd");
            cmd.Parameters.Add(new OracleParameter("DOCU_ISSU", st_date));
            string end_date = dateTimePicker2.Value.ToString("yyyyMMdd");
            cmd.Parameters.Add(new OracleParameter("EXP_DOCU", end_date));
            cmd.Parameters.Add(new OracleParameter("REM1", textBox7.Text));
            cmd.Parameters.Add(new OracleParameter("REM2", textBox8.Text));
            cmd.Parameters.Add(new OracleParameter("STAT", reverse_check_stat(comboBox3.Text)));

            //string reg_date = parse_date(dateTimePicker5.Value.ToString(), 1);
            //cmd.Parameters.Add(new OracleParameter("REG_DOCU", reg_date));

            cmd.Parameters.Add(new OracleParameter("KZL", comboBox4.Text));
            cmd.Parameters.Add(new OracleParameter("NUM_DOCU", comboBox5.Text));

            cmd.Parameters.Add(new OracleParameter("SER_DOCU", comboBox1.Text));

            cmd.CommandText = "update table_for_docu set docu_issu_dd=:DOCU_ISSU, docu_exp_dd = :EXP_DOCU, remark = :REM1, remark_2= :REM2, DOCU_STAT_CD = :STAT where crp_cd = :KZL and DOCU_NO = :NUM_DOCU and docu_sres = :SER_DOCU";

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        void update_new_tbcb()
        {
            OracleCommand cmd = con.CreateCommand();

            var price = textBox3.Text.ToString();
            cmd.Parameters.Add(new OracleParameter("DOCU_PR", price));
            var date = dateTimePicker4.Value.ToString("yyyyMMdd");
            cmd.Parameters.Add(new OracleParameter("REC", date));
            cmd.Parameters.Add(new OracleParameter("ARCHEE", textBox2.Text));
            cmd.Parameters.Add(new OracleParameter("DESCR", textBox4.Text));
            var tmp_int = fun_ischis(comboBox2.Text);
            cmd.Parameters.Add(new OracleParameter("ISCHIS", tmp_int));
            cmd.Parameters.Add(new OracleParameter("PAR_ISCHIS", comboBox2.Text));
            cmd.Parameters.Add(new OracleParameter("CURRENCY", comboBox6.Text));
            cmd.Parameters.Add(new OracleParameter("BLOCK", dateTimePicker3.Value.ToString("yyyyMMdd")));
            cmd.Parameters.Add(new OracleParameter("REG", dateTimePicker5.Value.ToString("yyyyMMdd")));

            cmd.Parameters.Add(new OracleParameter("KZL", comboBox4.Text));
            string docu_num = comboBox5.Text.ToString();
            cmd.Parameters.Add(new OracleParameter("DOCU_NO", docu_num));
            cmd.Parameters.Add(new OracleParameter("SRES", comboBox1.Text));



            cmd.CommandText = "update new_tbcb set docu_price = :DOCU_PR , get_dd = :REC, archv = :ARCHEE, descrpt = :DESCR, estm_cd =:ISCHIS, estm_nm = :PAR_ISCHIS, currency = :CURRENCY, block_date = :BLOCK, registered = :REG where crp_cd = :KZL and docu_no = :DOCU_NO and docu_sres = :SRES";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            if (cmd.ExecuteNonQuery() > 0)
                label20.Visible = true;
            else
                MessageBox.Show("Не получилось изменить");

        }

        string check_stat(string tmp_str)
        {
            if (tmp_str == "1")
            {
                return "действующий документ";
            }
            if (tmp_str == "2")
            {
                return "недействительный документ";
            }
            if (tmp_str == "3")
            {
                return "формируется";
            }
            if (tmp_str == "4")
            {

                return "блокированный документ";
            }
            if (tmp_str == "5")
            {
                return "нераспознанный документ";
            }
            return "";
        }
        string reverse_check_stat(string tmp_str)
        {
            if (tmp_str == "действующий документ")
            {
                return "1";
            }
            if (tmp_str == "недействительный документ")
            {
                return "2";
            }
            if (tmp_str == "формируется")
            {
                return "3";
            }
            if (tmp_str == "блокированный документ")
            {
                return "4";
            }
            if (tmp_str == "нераспознанный документ")
            {
                return "5";
            }
            return "";
        }

        private void comboBox4_Enter(object sender, EventArgs e)
        {
            this.SetConnection();
        }

        private void comboBox4_Leave(object sender, EventArgs e)
        {
            this.CloseConnection();
        }


        string check_null(string str)
        {
            if (str.Length == 0)
                return "";
            else return str;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reverse_check_stat(comboBox3.Text) == "4")
            {
                dateTimePicker3.Visible = true;
                label19.Visible = true;
            }
            else
            {
                dateTimePicker3.Visible = false;
                label19.Visible = false;
            }
        }




        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //    check_value();
            int tmp = comboBox1.SelectedIndex;
            Name_company.SelectedItem = Name_company.Items[tmp];
            find_contract();
        }

        bool is_empty_str(string str)
        {
            if (str == "")
                return true;
            else return false;
        }
        void find_contract()
        {
            SetConnection();
            OracleCommand cmd = con.CreateCommand();
            // cmd.Parameters.Add(new OracleParameter("NUM_DOCU", comboBox5.Text));
            cmd.Parameters.Add(new OracleParameter("SER_DOCU", comboBox1.Text));
            cmd.CommandText = "select max(DOCU_NO) from table_for_docu where docu_sres = :SER_DOCU ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                long tmp;
                if (!is_empty_str(dr[0].ToString()))
                {
                    tmp = Int64.Parse(dr[0].ToString());

                }
                else
                {
                    tmp = 0;
                }
                tmp += 1;
                comboBox5.Text = tmp.ToString();
            }

        }
        DateTimePicker dtp;
        TextBox textBox;
        private void button6_Click(object sender, EventArgs e)
        {
            //this.Size = new Size(1204, 618 + count_row * interval);
            //panel2.Size = new Size( 1192, 268 + count_row * interval);
            //panel3.Location = new Point(1, 355 + count_row * interval);
            //panel2.Controls.Add(CreateLabel("label_num", "№ соглашения", 15,114, 16));  ////номер соглашения
            //panel2.Controls.Add(CreateLabel("st_from", "Действует с", 416, 99, 16));    //действует с
            //panel2.Controls.Add(CreateTextBox("KO",1,391,784));   //Доп соглашение
            //panel2.Controls.Add(CreateTextBox("value_contract",2, 160,154));
            //panel2.Controls.Add(CreateDateTime());
            tableLayoutPanel1.Controls.Add(CreateLabel("№ соглашения", 3));
            tableLayoutPanel1.Controls.Add(CreateTextBox("value_contract", 2, 160, 154));
            tableLayoutPanel1.Controls.Add(CreateLabel("Действует с", 3));
            tableLayoutPanel1.Controls.Add(CreateDateTime());
            tableLayoutPanel1.Controls.Add(CreateTextBox("KO", 1, 391, 784));
            this.Size = new Size(1204, count_row * 26 + 618);
            //panel2.Size = new Size(1192, 268 + count_row * 26);
            // tableLayoutPanel1.Size = new Size(1192 ,count_row*30 + 268);
            //  tableLayoutPanel1.Location = new Point(15,212);
            panel3.Location = new Point(1, 355 + count_row * 26);
            count_row++;

        }
        private void button6_Click(int tmp, string num, string date)
        {
            tableLayoutPanel1.Controls.Add(CreateLabel("№ соглашения", 3));
            tableLayoutPanel1.Controls.Add(CreateTextBox("value_contract", 2, 160, 154, num));
            tableLayoutPanel1.Controls.Add(CreateLabel("Действует с", 3));
            tableLayoutPanel1.Controls.Add(CreateDateTime(date));
            tableLayoutPanel1.Controls.Add(CreateTextBox("KO", 1, 391, 784));
            this.Size = new Size(1204, tmp * 26 + 618);
            //panel2.Size = new Size(1192, 268 + count_row * 26);
            // tableLayoutPanel1.Size = new Size(1192 ,count_row*30 + 268);
            //  tableLayoutPanel1.Location = new Point(15,212);
            panel3.Location = new Point(1, 355 + tmp * 26);



        }
        private DateTimePicker CreateDateTime()
        {
            string tmp_DTP = "dtp" + count_row.ToString();
            dtp = new DateTimePicker();
            dtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtp.Size = new Size(200, 20);
            dtp.Location = new Point(554, 211 + count_row * interval);
            dtp.Name = tmp_DTP;
            dtp.CustomFormat = "dd.MM.yyyy";
            return dtp;
        }
        private DateTimePicker CreateDateTime(string date)
        {
            string tmp_DTP = "dtp" + count_row.ToString();
            dtp = new DateTimePicker();
            dtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtp.Size = new Size(200, 20);
            dtp.Location = new Point(554, 211 + count_row * interval);
            dtp.Name = tmp_DTP;
            inverse_parse_date(date, dtp);
            dtp.CustomFormat = "dd.MM.yyyy";
            return dtp;
        }
        private Label CreateLabel(string name, int loc_y)
        {

            Label cur_label = new Label();
            cur_label.AutoSize = true;
            //if (name == "№ соглашения")
            //label.Location = new Point(3, loc_y);
            //else 
            //label.Location = new Point(403, loc_y);
            cur_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cur_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            cur_label.Size = new System.Drawing.Size(200, 25);
            cur_label.Text = name;

            return cur_label;

        }

        private void Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.was_count = 0;
        }

        private TextBox CreateTextBox(string name, int tmp, int s_x, int loc_x, string num)
        {
            string tmp_textbox = name + count_row.ToString();
            textBox = new TextBox();
            textBox.Name = tmp_textbox;
            textBox.Size = new Size(s_x, 20);
            textBox.Location = new System.Drawing.Point(loc_x, 211 + count_row * interval);
            if (tmp == 1)
                textBox.Text = "КО - Дополнительное соглашение к договору";
            else if (tmp == 2)
            {
                textBox.Text = num;
            }
            textBox.Enabled = false;

            return textBox;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private TextBox CreateTextBox(string name, int tmp, int s_x, int loc_x)
        {
            string tmp_textbox = name + count_row.ToString();
            textBox = new TextBox();
            textBox.Name = tmp_textbox;
            textBox.Size = new Size(s_x, 20);
            textBox.Location = new System.Drawing.Point(loc_x, 211 + count_row * interval);
            if (tmp == 1)
                textBox.Text = "КО - Дополнительное соглашение к договору";
            else if (tmp == 2)
            {
                textBox.Text = (count_row + 1).ToString();
            }
            textBox.Enabled = false;

            return textBox;
        }
        void load_sres()
        {
            SetConnection();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from series_of_docu ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            comboBox1.Items.Clear();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
                Name_company.Items.Add(dr[1].ToString());

            }
            comboBox1.Items.Add("");
            Name_company.Items.Add("");
        }
        void load_currency()
        {
            //    SetConnection();
            //    OracleCommand cmd = con.CreateCommand();
            //    cmd.CommandText = "select cd from tbcb_cd where cd_grp_no = '000069' ";
            //    cmd.CommandType = CommandType.Text;
            //    OracleDataReader dr = cmd.ExecuteReader();
            //    int tmp = 0;
            //    comboBox1.Items.Clear();
            //    while (dr.Read())
            //    {
            //        if ((tmp++)%2 == 1)
            //        comboBox6.Items.Add(dr[0].ToString());
            //    }
            comboBox6.Items.Add("");
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
    }
}