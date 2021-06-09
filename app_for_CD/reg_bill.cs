using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace app_for_CD
{

    public partial class reg_bill : Form
    {
        ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(reg_bill));
        OracleConnection con = null;
        int count_of_label = 1, count_of_comboBox = 2, count_of_Button = 2, count_of_TextBox = 1;
        int count_of_label_u = 0;
        int row = 2;
        string cur_INN = "", cur_crp_nm = "";
        bool f = false;
        string Num_of_id = "-1";
        string sres, num_sres;
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
        public reg_bill()
        {
            InitializeComponent();
            SetConnection();
            textBox_number_of_invoice.Text = (find_last_number() + 1).ToString();
            button1.Visible = false;
        }
        public reg_bill(string num)
        {
            InitializeComponent();
            SetConnection();
            textBox_number_of_invoice.Text = num;
            fill_data();
            Save.Visible = false;
        }
        private void fill_data()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("NUM_OF_BILL", textBox_number_of_invoice.Text);
            cmd.CommandText = "Select * from table_billing where num_of_bill = :NUM_OF_BILL";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                comboBox_CRP_INN.Text = dr[5].ToString();
                if (dr[8].ToString() == "")
                    NDS_PINFL_textBox.Text = dr[9].ToString();
                else
                    NDS_PINFL_textBox.Text = dr[8].ToString();
                inverse_parse_date(dr[1].ToString(), dateTimePicker_invoice_data);
                Docu_num_ser.Items.Add(dr[2].ToString() + "/" + dr[3].ToString()  );
                Docu_num_ser.SelectedIndex = 0;
                ComboBox_0.Items.Add(dr[10].ToString()  );
                ComboBox_0.SelectedIndex = 0;
                textBox_Sum.Text = dr[11].ToString();
                comboBox6.Text = dr[18].ToString();
                ground_textBox.Text = dr[16].ToString();
                comment_textBox.Text = dr[17].ToString();
                
            }
        }


        private void Form1_Shown(object sender, EventArgs e)
        {

            tableLayoutPanel_main.AutoScroll = false;
            tableLayoutPanel_main.HorizontalScroll.Enabled = false;
            tableLayoutPanel_main.HorizontalScroll.Visible = false;
            tableLayoutPanel_main.HorizontalScroll.Maximum = 0;
            tableLayoutPanel_main.RowCount = 2;
            tableLayoutPanel_main.RowStyles.Clear();
            tableLayoutPanel_main.AutoScroll = true;
            SetConnection();
            if (Num_of_id != "-1")
                LoadChange(Num_of_id);

        }
        private void LoadChange(string id)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"select * from registration_of_invoice where ID = '{id}' order by num_of_ser";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            int enter = 0;
            status_label.Visible = true;
            status_comboBox.Visible = true;

            int cur_row = 2;
            bool flag = false;

            FlowLayoutPanel flp;
            Panel panel;

            while (dr.Read())
            {
                if (enter == 0)
                {

                    comboBox_CRP_INN.Text = dr[1].ToString();
                    NDS_PINFL_textBox.Text = dr[8].ToString();
                    if (dr[15].ToString() == "1")
                    {
                        status_comboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        status_comboBox.SelectedIndex = 1;
                    }
                    textBox_number_of_invoice.Text = dr[0].ToString();
                    dateTimePicker_invoice_data.Value = DateTime.ParseExact(dr[9].ToString(), "yyyyMMdd", null);

                    search_nom_ser.PerformClick();
                    findItems(Docu_num_ser, dr[2].ToString());

                    SearchSer_0.PerformClick();
                    findItems(ComboBox_0, dr[3].ToString());

                    textBox_Sum.Text = dr[4].ToString();

                    findItems(comboBox6, dr[5].ToString());

                    ground_textBox.Text = dr[6].ToString();
                    comment_textBox.Text = dr[7].ToString();
                }
                else
                {
                    Add.PerformClick();

                    flp = ((FlowLayoutPanel)tableLayoutPanel_main.GetControlFromPosition(1, cur_row));
                    ((Button)flp.Controls[1]).PerformClick();
                    findItems(((ComboBox)flp.Controls[0]), dr[3].ToString());
                    flag = true;
                    cur_row++;
                    panel = (Panel)(tableLayoutPanel_main.GetControlFromPosition(1, cur_row));
                    ((TextBox)panel.Controls[0]).Text = dr[4].ToString();
                    findItems(((ComboBox)panel.Controls[2]), dr[5].ToString());
                    flag = false;
                    cur_row++;

                }
                enter++;
            }

        }

        private void findItems(ComboBox combo, string val)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                if (combo.Items[i].ToString() == val)
                {
                    combo.SelectedIndex = i;
                }
            }
        }

        private Label CreateLabel(string name, int x, int y)
        {
            Label cur_label = new Label();
            cur_label.AutoSize = true;
            cur_label.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            cur_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            cur_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            cur_label.Name = "Label_" + count_of_label.ToString();
            cur_label.Size = new System.Drawing.Size(93, 24);
            cur_label.Text = name;
            if (x != -1 && y != -1)
                cur_label.Location = new System.Drawing.Point(x, y);
            count_of_label++;
            return cur_label;
        }

        private ComboBox CreateComboBox(int w, int h, int x, int y, bool currencyf)
        {
            ComboBox combo = new ComboBox();
            combo.FormattingEnabled = true;
            combo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            if (currencyf)
            {
                combo.Items.AddRange(new object[] {
                "СУМ",
                "Доллар США",});
            }
            combo.Name = "ComboBox_" + count_of_comboBox.ToString();
            combo.Size = new System.Drawing.Size(w, h);
            combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            if (x != -1 && y != -1)
                combo.Location = new System.Drawing.Point(x, y);
            combo.Click += new System.EventHandler(ComboBoxClick);
            count_of_comboBox++;
            return combo;
        }

        private void search_for_CRP_Click(object sender, EventArgs e)
        {

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CRP_CD FROM TBCB_CRP_INFO where rownum <=1000";

            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox_CRP_INN.Items.Add(dr[0].ToString());
            }
        }

        private Button searchButton()
        {
            Button but = new Button();
            but.Image = ((System.Drawing.Image)(resources.GetObject("SearchSer_0.Image")));
            but.Location = new System.Drawing.Point(169, 3);
            but.Name = "SearchSer_" + count_of_Button.ToString();
            but.Size = new System.Drawing.Size(23, 21);
            but.UseVisualStyleBackColor = true;
            but.Click += new System.EventHandler(search_ser_Click);

            count_of_Button = count_of_Button + 2;
            return but;
        }
        int find_id()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT NVL(MAX(ID), 0) FROM REGISTRATION_OF_INVOICE ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            return Int32.Parse(dr[0].ToString());
        }
        bool check()
        {
            bool flag = true;
            if (comboBox_CRP_INN.Text == "" || comboBox_CRP_INN.Text == null)
            {
                comboBox_CRP_INN.BackColor = System.Drawing.Color.Red;
                flag = false;
            }
            if (NDS_PINFL_textBox.Text == "" || NDS_PINFL_textBox.Text == null)
            {
                NDS_PINFL_textBox.BackColor = System.Drawing.Color.Red;
                flag = false;
            }
            if (Docu_num_ser.Text == "" || Docu_num_ser.Text == null)
            {
                Docu_num_ser.BackColor = System.Drawing.Color.Red;
                Docu_num_ser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
                flag = false;
            }
            FlowLayoutPanel flp;
            Panel panel;
            for (int j = 0; j < tableLayoutPanel_main.Controls.Count; j++)
            {
                if (tableLayoutPanel_main.Controls[j] is FlowLayoutPanel)
                {
                    flp = (FlowLayoutPanel)tableLayoutPanel_main.Controls[j];
                    if (flp.Controls[0] is ComboBox)
                    {
                        if (((ComboBox)(flp.Controls[0])).Text == "" || ((ComboBox)(flp.Controls[0])).Text == null)
                        {
                            ((ComboBox)(flp.Controls[0])).BackColor = System.Drawing.Color.Red;
                            ((ComboBox)(flp.Controls[0])).DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
                            flag = false;
                        }
                    }
                }

                else if (tableLayoutPanel_main.Controls[j] is Panel)
                {
                    panel = (Panel)tableLayoutPanel_main.Controls[j];
                    if (((TextBox)(panel.Controls[0])).Text == "" || ((TextBox)(panel.Controls[0])).Text == null)
                    {
                        ((TextBox)(panel.Controls[0])).BackColor = System.Drawing.Color.Red;
                        flag = false;
                    }
                    if (((ComboBox)(panel.Controls[2])).Text == "" || ((ComboBox)(panel.Controls[2])).Text == null)
                    {
                        ((ComboBox)(panel.Controls[2])).BackColor = System.Drawing.Color.Red;
                        ((ComboBox)(panel.Controls[2])).DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
                        flag = false;
                    }
                }


            }

            if (ground_textBox.Text == "" || ground_textBox.Text == null)
            {
                ground_textBox.BackColor = System.Drawing.Color.Red;
                flag = false;
            }
            if (comment_textBox.Text == "" || comment_textBox.Text == null)
            {
                comment_textBox.BackColor = System.Drawing.Color.Red;
                flag = false;
            }

            if (flag == false)
            {
                Report.Text = "Заполните все поля!";
                Report.ForeColor = System.Drawing.Color.Red;
                Report.Visible = true;
            }
            return flag;
        }

        private string find_data(string crp, string num_ser)
        {
            string ser = "", num = "";
            string data = "";
            bool flag = false;
            for (int i = 0; i < num_ser.Count(); i++)
            {
                if (num_ser[i] == '/')
                {
                    flag = true;
                }
                else if (flag == false)
                {
                    num = num + num_ser[i]; ;
                }
                else if (flag == true)
                {
                    ser = ser + num_ser[i];
                }
            }

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"select DOCU_ISSU_DD from table_for_docu  WHERE CRP_CD = '{crp}' AND DOCU_NO = '{num}' AND DOCU_SRES = '{ser}'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                data = dr[0].ToString();
            }

            return data;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            int status;
            byte IF_fiz;
            if (NDS_PINFL.Text == "ПИНФЛ")
            {
                IF_fiz = 1;
            }
            else
            {
                IF_fiz = 0;
            }

            if (status_comboBox.Text == "Активный" || status_comboBox.Visible == false)
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("NUM_BILL", textBox_number_of_invoice.Text);//////
            cmd.Parameters.Add("DATE_BILL", dateTimePicker_invoice_data.Value.ToString("yyyyMMdd")); //
            cmd.Parameters.Add("NUM_AGGR", num_sres);  ///////
            cmd.Parameters.Add("SER_AGGR", sres);      //////////
            cmd.Parameters.Add("DATE_AGGR", "date");
            cmd.Parameters.Add("KZL", comboBox_CRP_INN.Text);   ////////////
            cmd.Parameters.Add("KZL_NM", cur_crp_nm);/////
            cmd.Parameters.Add("INN", cur_INN); ////////////
            if (IF_fiz == 0)
            {
                cmd.Parameters.Add("NDS", NDS_PINFL_textBox.Text); ///////////
                cmd.Parameters.Add("PINFL", ""); ///////////

            }
            else
            {
                cmd.Parameters.Add("NDS", ""); ///////////
                cmd.Parameters.Add("PINFL", NDS_PINFL_textBox.Text); ///////////
            }
            cmd.Parameters.Add("TYPE_SER", ComboBox_0.Text);   /////////////////
            cmd.Parameters.Add("COST_DELIV", float.Parse(textBox_Sum.Text));            ///////////////////////////////////NDS_PINFL
            cmd.Parameters.Add("STATUS", status);   /////////////
            cmd.Parameters.Add("PROCESS", "Выставлен");  //////////////
            cmd.Parameters.Add("SUMMA", "");
            cmd.Parameters.Add("FIO", Data.get_fio);
            cmd.Parameters.Add("BASE", ground_textBox.Text);
            cmd.Parameters.Add("REMARK", comment_textBox.Text);
            cmd.Parameters.Add("CUR", comboBox6.Text);

            cmd.CommandText = "insert into table_billing (num_of_bill, date_of_bill, num_aggr, sres_aggr, date_aggr, crp_cd, crp_nm, dist_id_2, nds, pinfl, type_sres, cost_deliv, state, process, payment_amount, fio, base, remark, curr) values (:NUM_BILL, :DATE_BILL, :NUM_AGGR, :SER_AGGR, :DATE_AGGR, :KZL, :KZL_NM, :INN, :NDS, :PINFL, :TYPE_SER, :COST_DELIV, :STATUS, :PROCESS, :SUMMA, :FIO, :BASE, :REMARK, :CUR)";
            cmd.CommandType = CommandType.Text;
            if (cmd.ExecuteNonQuery() == 1)
            {
                Report.Visible = true;
                textBox_number_of_invoice.Text = (Int32.Parse(textBox_number_of_invoice.Text) + 1).ToString();
            }
            else
            {
                MessageBox.Show("Error. Скажите Тимуру про инвойс");
            }


        }

        private void comboBox_CRP_INN_TextChanged(object sender, EventArgs e)
        {
            string crp = comboBox_CRP_INN.Text.ToString();
            string INN = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = crp;
            cmd.CommandText = "SELECT CRP_NM, DIST_ID_2, CRP_TYPE_CD FROM TBCB_CRP_INFO where CRP_CD = :KZL";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr[2].ToString() == "8000")
                {
                    NDS_PINFL.Text = "ПИНФЛ";
                    NDS_PINFL_textBox.MaxLength = 14;
                    NDS_PINFL.Visible = true;
                    NDS_PINFL_textBox.Visible = true;
                }
                else
                {
                    NDS_PINFL.Text = "Код НДС";
                    NDS_PINFL_textBox.MaxLength = 12;
                    NDS_PINFL.Visible = true;
                    NDS_PINFL_textBox.Visible = true;
                }
                INN = dr[1].ToString();
                if (INN == " " || INN == "" || INN == null)
                {
                    textBox_CRP.Text = dr[0].ToString();
                    cur_crp_nm = dr[0].ToString();
                }
                else
                {
                    textBox_CRP.Text = dr[0].ToString() + $"(ИНН:{INN})";
                    cur_crp_nm = dr[0].ToString();
                    cur_INN = INN;
                }
            }
        }

        private void search_nom_ser_Click(object sender, EventArgs e)
        {
            Docu_num_ser.Items.Clear();
            string crp = comboBox_CRP_INN.Text;
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = crp;
            cmd.CommandText = "SELECT DOCU_NO, DOCU_SRES FROM table_for_docu where CRP_CD = :KZL";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                num_sres = dr[0].ToString();
                sres = dr[1].ToString();
                Docu_num_ser.Items.Add($"{dr[0]}/{dr[1]}");
            }
        }

        private void Docu_num_ser_TextChanged(object sender, EventArgs e)
        {

            string num_ser = Docu_num_ser.Text.ToString();
            string ser = "";
            bool flag = false;
            for (int i = 0; i < num_ser.Count(); i++)
            {
                if (num_ser[i] == '/')
                {
                    flag = true;
                }
                else if (flag == true)
                {
                    ser = ser + num_ser[i];
                }
            }
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("ser", OracleDbType.Varchar2, 13).Value = ser;
            cmd.CommandText = $"SELECT VALUE_OF_SRES FROM series_of_docu where DOCU_SRES = '{ser}'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                textBox_ser_name.Text = dr[0].ToString();
        }

        private void search_ser_Click(object sender, EventArgs e)
        {
            Button but = sender as Button;
            ComboBox near_combo;
            string number = " ";
            for (int i = 10; i < but.Name.Count(); i++)
            {
                number += but.Name[i];
            }
            int inumber = Int32.Parse(number);

            near_combo = ((ComboBox)(((FlowLayoutPanel)(tableLayoutPanel_main.GetControlFromPosition(1, inumber))).Controls[0]));
            near_combo.Items.Clear();
            string num_ser = Docu_num_ser.Text.ToString();
            string num = "", ser = "";
            string ser_num;
            bool flag = false;
            if (num_ser.Count() != 0)
            {

                for (int i = 0; i < num_ser.Count(); i++)
                {
                    if ((num_ser[i] != '/') && (flag == false))
                    {
                        num = num + num_ser[i];
                    }
                    else
                    {
                        flag = true;
                    }
                    if ((num_ser[i] != '/') && flag == true)
                    {
                        ser = ser + num_ser[i];
                    }
                }
                ser_num = ser;
                ser_num = ser_num + num;
                OracleCommand cmd = con.CreateCommand();
                cmd.Parameters.Add("cd", OracleDbType.Varchar2, 13).Value = ser;
                cmd.CommandText = $"SELECT CD_NM FROM tbcb_cd where cd_grp_no = '000037' AND CD like '{ser}%'";

                cmd.CommandType = CommandType.Text;

                OracleDataReader dr = cmd.ExecuteReader();
                //OracleDataReader dr1;

                while (dr.Read())
                {
                    //cmd.CommandText = $"";
                    //cmd.ExecuteReader();
                    near_combo.Items.Add(dr[0].ToString());
                }
            }
        }

        private TextBox Create_TextBox(int w, int h, bool grey, int x, int y)
        {
            TextBox textBox = new TextBox();
            if (grey == true)
                textBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            textBox.Location = new System.Drawing.Point(4, 3);
            textBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox.Name = "textBox_" + count_of_TextBox;
            textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            textBox.Size = new System.Drawing.Size(w, h);
            if (x != -1 && y != -1)
                textBox.Location = new System.Drawing.Point(x, y);
            textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Sum_KeyPress);
            textBox.Click += new System.EventHandler(this.textBoxClick);
            count_of_TextBox++;
            return textBox;
        }
        private Panel Create_Panel()
        {
            Panel panel = new Panel();
            panel.Controls.Add(Create_TextBox(189, 20, false, 4, 5));
            panel.Controls.Add(CreateLabel("Валюта:", 224, 3));
            panel.Controls.Add(CreateComboBox(189, 21, 336, 6, true));
            panel.Dock = System.Windows.Forms.DockStyle.Fill;
            panel.Size = new System.Drawing.Size(926, 30);
            panel.TabIndex = 66;
            return panel;
        }
        public int DS_Count(string s)
        {
            string substr = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0].ToString();
            int count = (s.Length - s.Replace(substr, "").Length) / substr.Length;
            return count;
        }
        private void textBox_Sum_KeyPress(object sender, KeyPressEventArgs e)
        {
            var DS = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            if (e.KeyChar == DS && ((TextBox)sender).Text.Length == 0)
                e.Handled = true;
            else
                e.Handled = !(Char.IsDigit(e.KeyChar) || ((e.KeyChar == DS) && (DS_Count(((TextBox)sender).Text) < 1)) || e.KeyChar == 8);
        }

        private void ComboBoxClick(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo.BackColor == System.Drawing.Color.Red)
            {
                combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                combo.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void comboBox_CRP_INN_Click(object sender, EventArgs e)
        {
            if (comboBox_CRP_INN.BackColor == System.Drawing.Color.Red)
            {
                comboBox_CRP_INN.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void textBoxClick(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.BackColor == System.Drawing.Color.Red)
            {
                textBox.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private void Remove(bool f)
        {
            textBox_number_of_invoice.Text = "";
            int iter = 0;
            if (f == true)
            {
                for (int i = tableLayoutPanel_main.Controls.Count - 1; i >= 8; i--)
                {
                    tableLayoutPanel_main.Controls[i].Dispose();
                    iter++;
                    if (iter % 4 == 0)
                    {
                        row = row - 2;
                        count_of_label_u = count_of_label_u - 1;
                    }

                }
                comboBox_CRP_INN.Text = "";
                comboBox_CRP_INN.BackColor = System.Drawing.SystemColors.Window;
                textBox_CRP.Text = "";
                textBox_CRP.BackColor = System.Drawing.SystemColors.Window;
                NDS_PINFL_textBox.Text = "";
                NDS_PINFL_textBox.BackColor = System.Drawing.SystemColors.Window;
                Docu_num_ser.SelectedIndex = -1;
                Docu_num_ser.BackColor = System.Drawing.SystemColors.Window;
                textBox_ser_name.Text = "";
                textBox_ser_name.BackColor = System.Drawing.SystemColors.Window;
                ComboBox_0.SelectedIndex = -1;
                ComboBox_0.BackColor = System.Drawing.SystemColors.Window;
                textBox_Sum.Text = "";
                textBox_Sum.BackColor = System.Drawing.SystemColors.Window;
                comboBox6.SelectedIndex = -1;
                comboBox6.BackColor = System.Drawing.SystemColors.Window;
                ground_textBox.Text = "";
                ground_textBox.BackColor = System.Drawing.SystemColors.Window;
                comment_textBox.Text = "";
                comment_textBox.BackColor = System.Drawing.SystemColors.Window;
            }
            else if (tableLayoutPanel_main.Controls.Count != 8)
            {
                tableLayoutPanel_main.Controls[tableLayoutPanel_main.Controls.Count - 1].Dispose();
                tableLayoutPanel_main.Controls[tableLayoutPanel_main.Controls.Count - 1].Dispose();
                tableLayoutPanel_main.Controls[tableLayoutPanel_main.Controls.Count - 1].Dispose();
                tableLayoutPanel_main.Controls[tableLayoutPanel_main.Controls.Count - 1].Dispose();
                row = row - 2;
                count_of_label_u = count_of_label_u - 1;
            }
        }


        private void New_Click(object sender, EventArgs e)
        {
            Save s = new Save();
            s.StartPosition = FormStartPosition.CenterParent;
            s.ShowDialog();
            if (Data.yes == true)
            {
                Save.PerformClick();
            }
            else
            {
                Remove(true);
            }
            Data.yes = false;
        }

        private void comboBox_CRP_INN_SelectedValueChanged(object sender, EventArgs e)
        {
            bool flag = false;
            FlowLayoutPanel flp;
            Panel panel;
            int cur_row = 0;

            NDS_PINFL_textBox.Text = "";

            for (int i = 0; i < tableLayoutPanel_main.Controls.Count - 4; i++)
            {
                if ((i % 2 == 1) && flag == false)
                {
                    flp = ((FlowLayoutPanel)tableLayoutPanel_main.GetControlFromPosition(1, cur_row));
                    ((ComboBox)flp.Controls[0]).SelectedItem = null;
                    ((ComboBox)flp.Controls[0]).Items.Clear();
                    flag = true;
                    cur_row++;
                }
                else if ((i % 2 == 1) && flag == true)
                {
                    panel = (Panel)(tableLayoutPanel_main.GetControlFromPosition(1, cur_row));
                    ((TextBox)panel.Controls[0]).Text = "";
                    ((ComboBox)panel.Controls[2]).SelectedItem = null;
                    flag = false;
                    cur_row++;
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel_main.Controls.Count != 8)
            {
                Panel pan = ((Panel)tableLayoutPanel_main.Controls[tableLayoutPanel_main.Controls.Count - 1]);
                FlowLayoutPanel flp = ((FlowLayoutPanel)tableLayoutPanel_main.Controls[tableLayoutPanel_main.Controls.Count - 3]);
                if (((ComboBox)(pan.Controls[2])).SelectedIndex != -1 || ((TextBox)(pan.Controls[0])).Text != "" || ((ComboBox)(flp.Controls[0])).SelectedIndex != -1)
                {
                    SaveForSer s = new SaveForSer();
                    s.StartPosition = FormStartPosition.CenterParent;
                    s.ShowDialog();

                    if (Data.yes == true)
                    {
                        Remove(false);
                    }
                    Data.yes = false;

                }
                else
                {
                    Remove(false);
                }

            }
        }

        private void NDS_PINFL_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int status;
            byte IF_fiz;
            if (NDS_PINFL.Text == "ПИНФЛ")
            {
                IF_fiz = 1;
            }
            else
            {
                IF_fiz = 0;
            }

            if (status_comboBox.Text == "Активный" || status_comboBox.Visible == false)
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("DATE_BILL", dateTimePicker_invoice_data.Value.ToString("yyyyMMdd")); //
            cmd.Parameters.Add("NUM_AGGR", num_sres);  ///////
            cmd.Parameters.Add("SER_AGGR", sres);      //////////
            cmd.Parameters.Add("DATE_AGGR", "date");
            cmd.Parameters.Add("KZL", comboBox_CRP_INN.Text);   ////////////
            cmd.Parameters.Add("KZL_NM", cur_crp_nm);/////
            cmd.Parameters.Add("INN", cur_INN); ////////////
            if (IF_fiz == 0)
            {
                cmd.Parameters.Add("NDS", NDS_PINFL_textBox.Text); ///////////
                cmd.Parameters.Add("PINFL", ""); ///////////

            }
            else
            {
                cmd.Parameters.Add("NDS", ""); ///////////
                cmd.Parameters.Add("PINFL", NDS_PINFL_textBox.Text); ///////////
            }
            cmd.Parameters.Add("TYPE_SER", ComboBox_0.Text);   /////////////////
            cmd.Parameters.Add("COST_DELIV", float.Parse(textBox_Sum.Text));            ///////////////////////////////////NDS_PINFL
            cmd.Parameters.Add("STATUS", status);   /////////////
            cmd.Parameters.Add("PROCESS", "Выставлен");  //////////////
            cmd.Parameters.Add("SUMMA", "");
            cmd.Parameters.Add("FIO", Data.get_fio);
            cmd.Parameters.Add("BASE", ground_textBox.Text);
            cmd.Parameters.Add("REMARK", comment_textBox.Text);
            cmd.Parameters.Add("CUR", comboBox6.Text);
            cmd.Parameters.Add("NUM_BILL", textBox_number_of_invoice.Text);//////

            cmd.CommandText = "update table_billing set date_of_bill = :DATE_BILL, num_aggr = :NUM_AGGR, sres_aggr = :SER_AGGR, date_aggr = :DATE_AGGR, crp_cd= :KZL, crp_nm = :KZL_NM, dist_id_2 = :INN, nds = :NDS, pinfl = :PINFL, type_sres = :TYPE_SER, cost_deliv = :COST_DELIV, state = :STATUS, process = :PROCESS, payment_amount = :SUMMA, fio = :FIO, base = :BASE, remark = :REMARK, curr = :CUR where num_of_bill = :NUM_BILL  ";
            cmd.CommandType = CommandType.Text;
            if (cmd.ExecuteNonQuery() == 1)
            {
                Report.Visible = true;
            }
            else
            {
                MessageBox.Show("Error. Скажите Тимуру про reg_bill");
            }

        }

        private void Docu_num_ser_SelectedValueChanged(object sender, EventArgs e)
        {
            bool flag = false;
            FlowLayoutPanel flp;
            Panel panel;
            int cur_row = 0;
            for (int i = 0; i < tableLayoutPanel_main.Controls.Count - 4; i++)
            {

                if ((i % 2 == 1) && flag == false)
                {
                    flp = ((FlowLayoutPanel)tableLayoutPanel_main.GetControlFromPosition(1, cur_row));
                    ((ComboBox)flp.Controls[0]).SelectedItem = null;
                    ((ComboBox)flp.Controls[0]).Items.Clear();
                    flag = true;
                    cur_row++;
                }
                else if ((i % 2 == 1) && flag == true)
                {
                    panel = (Panel)(tableLayoutPanel_main.GetControlFromPosition(1, cur_row));
                    ((TextBox)panel.Controls[0]).Text = "";
                    ((ComboBox)panel.Controls[2]).SelectedItem = null;
                    flag = false;
                    cur_row++;
                }
            }
        }

        private void CreateElementOnTable(byte choice)
        {
            FlowLayoutPanel flws = new FlowLayoutPanel();
            if (choice == 1)
            {
                flws.Controls.Add(CreateLabel($"Услуга - {count_of_label_u + 2}:", -1, -1));
                flws.Size = new System.Drawing.Size(205, 26);
                count_of_label_u++;
                tableLayoutPanel_main.Controls.Add(flws, 0, row);
            }
            else if (choice == 2)
            {

                flws.Controls.Add(CreateComboBox(873, 21, -1, -1, false));
                flws.Controls.Add(searchButton());
                flws.Size = new System.Drawing.Size(926, 26);
                tableLayoutPanel_main.Controls.Add(flws, 1, row);
                row++;
            }
            else if (choice == 3)
            {
                flws.Controls.Add(CreateLabel("Сумма:", -1, -1));
                flws.Size = new System.Drawing.Size(85, 26);
                tableLayoutPanel_main.Controls.Add(flws, 0, row);
            }
            else if (choice == 4)
            {
                tableLayoutPanel_main.Controls.Add(Create_Panel(), 1, row);
                row++;
            }

        }
        int find_last_number()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select max(num_of_bill) from table_billing ";
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
        private void MyCreateButton_Click(object sender, EventArgs e)
        {
            for (byte i = 1; i <= 4; i++)
            {
                CreateElementOnTable(i);
            }
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
    }
}
