﻿using System;
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

    public partial class registration_of_an_invoice : Form
    {
        ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(registration_of_an_invoice));
        OracleConnection con = null;
        int count_of_label = 1, count_of_comboBox = 2, count_of_Button = 2, count_of_TextBox = 1;
        int count_of_label_u = 0;
        int row = 2;
        bool f = false;
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
        public registration_of_an_invoice()
        {
            InitializeComponent();
            // create
            textBox_number_of_invoice.Text = "Hi, I'm working!";
            tableLayoutPanel_main.AutoScroll = false;
            tableLayoutPanel_main.HorizontalScroll.Enabled = false;
            tableLayoutPanel_main.HorizontalScroll.Visible = false;
            tableLayoutPanel_main.HorizontalScroll.Maximum = 0;
            tableLayoutPanel_main.RowCount = 2;
            tableLayoutPanel_main.RowStyles.Clear();
            tableLayoutPanel_main.AutoScroll = true;
            SetConnection();
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
            if(currencyf)
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
            count_of_comboBox ++;
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

        private void comboBox_CRP_SelectedValueChanged(object sender, EventArgs e)
        {
            string crp = comboBox_CRP_INN.SelectedItem.ToString();
            string INN = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = crp;
            cmd.CommandText = "SELECT CRP_NM, DIST_ID_2 FROM TBCB_CRP_INFO where CRP_CD = :KZL";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                INN = dr[1].ToString();
                if (INN == " " || INN == "" || INN == null)
                    textBox_CRP.Text = dr[0].ToString();
                else
                    textBox_CRP.Text = dr[0].ToString() + $" (ИНН:{INN})";
            }
        }
        int find_id()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT NVL(MAX(ID), 0) FROM REGISTRATION_OF_INVOICE ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            MessageBox.Show(dr.Read().ToString());
            return Int32.Parse(dr[0].ToString());
        }
        bool check()
        {
            bool flag = true;
            if(comboBox_CRP_INN.Text == "" || comboBox_CRP_INN.Text == null) { 
                comboBox_CRP_INN.BackColor = System.Drawing.Color.Red;
                flag = false;
            }
            if (NDS_PINFL_textBox.Text == "" || NDS_PINFL_textBox.Text == null) { 
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
            for (int j=0; j < tableLayoutPanel_main.Controls.Count; j++)
            {
                if (tableLayoutPanel_main.Controls[j] is FlowLayoutPanel) {
                    flp = (FlowLayoutPanel)tableLayoutPanel_main.Controls[j];
                    if (flp.Controls[0] is ComboBox) {
                        if (((ComboBox)(flp.Controls[0])).Text == "" || ((ComboBox)(flp.Controls[0])).Text == null) {
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

            if (flag == false) {
                Report.Text = "Заполните все поля!";
                Report.ForeColor = System.Drawing.Color.Red;
                Report.Visible = true;
            }
            return flag;
        }
        private void Save_Click(object sender, EventArgs e)
        {
            if (check()) {
                #region Получение всей информации
                List<string> values = new List<string>();
                string crp = comboBox_CRP_INN.Text;
                string Date = dateTimePicker_invoice_data.Value.ToString("yyyyMMdd");
                string num_series = Docu_num_ser.Text;
                string ground = ground_textBox.Text;
                string comment = comment_textBox.Text;
                string nds_pinfl = NDS_PINFL_textBox.Text;
                int cur_row = 0;
                bool flag = false;
                FlowLayoutPanel flp;
                Panel panel;
                for (int i = 0; i < tableLayoutPanel_main.Controls.Count - 4; i++)
                {

                    if ((i % 2 == 1) && flag == false)
                    {
                        flp = ((FlowLayoutPanel)tableLayoutPanel_main.GetControlFromPosition(1, cur_row));
                        values.Add(((ComboBox)flp.Controls[0]).Text);
                        flag = true;
                        cur_row++;
                    }
                    else if ((i % 2 == 1) && flag == true)
                    {
                        panel = (Panel)(tableLayoutPanel_main.GetControlFromPosition(1, cur_row));
                        values.Add(((TextBox)panel.Controls[0]).Text);
                        values.Add(((ComboBox)panel.Controls[2]).Text);
                        flag = false;
                        cur_row++;
                    }
                }
                #endregion
                OracleCommand cmd;
                cmd = con.CreateCommand();
                int id;
                id = find_id() + 1;
                for (int i = 0; i < values.Count; i = i + 3) {

                    cmd.CommandText = $"insert into REGISTRATION_OF_INVOICE (id , CRP, SER, SERVICE_T, SUM_T, CURRENCY, BASIS, COMMENT_T, NDS_PINFL, DATE_T) values ({id}, '{crp}', '{num_series}', '{values[i]}', {values[i + 1]}, '{values[i + 2]}', '{ground}', '{comment}', '{nds_pinfl}', '{Date}') ";
                    cmd.ExecuteNonQuery();
                }
            }
            //
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
                if (dr[2].ToString() == "8000") {
                    NDS_PINFL.Text = "ПИНФЛ";
                    NDS_PINFL_textBox.MaxLength = 14;
                    NDS_PINFL.Visible = true;
                    NDS_PINFL_textBox.Visible = true;
                }
                else {
                    NDS_PINFL.Text = "Код НДС";
                    NDS_PINFL_textBox.MaxLength = 12;
                    NDS_PINFL.Visible = true;
                    NDS_PINFL_textBox.Visible = true;
                }
                INN = dr[1].ToString();
                if (INN == " " || INN == "" || INN == null)
                    textBox_CRP.Text = dr[0].ToString();
                else
                    textBox_CRP.Text = dr[0].ToString() + $"(ИНН:{INN})";
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
            for (int i = 10; i < but.Name.Count(); i++) {
                number += but.Name[i];
            }
            int inumber = Int32.Parse(number);

            near_combo = ((ComboBox)(((FlowLayoutPanel)(tableLayoutPanel_main.GetControlFromPosition(1,inumber))).Controls[0]));
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

                while (dr.Read())
                {
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
            if(x!=-1 && y!=-1)
                textBox.Location = new System.Drawing.Point(x, y);
            textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Sum_KeyPress);
            textBox.Click += new System.EventHandler(this.textBoxClick);
            count_of_TextBox++;
            return textBox;
        }
        private Panel Create_Panel() {
            Panel panel = new Panel();
            panel.Controls.Add(Create_TextBox(189, 20, false, 4 , 5));
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
                e.Handled = !(Char.IsDigit(e.KeyChar) || ((e.KeyChar == DS) && (DS_Count(((TextBox)sender).Text) < 1) ) || e.KeyChar == 8 );
        }

        private void ComboBoxClick(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo.BackColor == System.Drawing.Color.Red) {
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

        private void Remove()
        {
            for (int i = tableLayoutPanel_main.Controls.Count - 1; i >= 8; i--)
            {
                tableLayoutPanel_main.Controls[i].Dispose();
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
                Remove();
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
            Data.yes = false;
        }


        private void Docu_num_ser_SelectedValueChanged(object sender, EventArgs e)
        {
            bool flag = false;
            FlowLayoutPanel flp;
            Panel panel;
            int cur_row = 0;
            for (int i = 0; i < tableLayoutPanel_main.Controls.Count - 4; i++) {
                
                if ( (i % 2 == 1) && flag == false ) {
                    flp = ((FlowLayoutPanel)tableLayoutPanel_main.GetControlFromPosition(1,cur_row));
                    ((ComboBox)flp.Controls[0]).SelectedItem = null;
                    ((ComboBox)flp.Controls[0]).Items.Clear();
                    flag = true;
                    cur_row++;
                }
                else if ( (i % 2 == 1) && flag == true) {
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
                tableLayoutPanel_main.Controls.Add(flws,0,row);
            }
            else if (choice == 2)
            {
                
                flws.Controls.Add(CreateComboBox(873, 21, -1, -1,false));
                flws.Controls.Add(searchButton());
                flws.Size = new System.Drawing.Size(926, 26);
                tableLayoutPanel_main.Controls.Add(flws,1,row);
                row++;
            }
            else if (choice == 3)
            {
                flws.Controls.Add(CreateLabel("Сумма:", -1, -1));
                flws.Size = new System.Drawing.Size(85, 26);
                tableLayoutPanel_main.Controls.Add(flws,0,row);
            }
            else if (choice == 4)
            {
                tableLayoutPanel_main.Controls.Add(Create_Panel(), 1, row);
                row ++;
            }

        }

        private void MyCreateButton_Click(object sender, EventArgs e)
        { 
            for (byte i = 1; i <= 4; i++)
            {
                CreateElementOnTable(i);
            }
        }
    }
}
