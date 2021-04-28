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
    public partial class registration_of_an_invoice : Form
    {
        ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(registration_of_an_invoice));
        OracleConnection con = null;
        int count_of_label = 1, count_of_comboBox = 2, count_of_Button = 2, count_of_TextBox = 1;
        int count_of_label_u = 0;
        int row = 2;

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

        private void Save_Click(object sender, EventArgs e)
        {
            #region Получение всей информации
            List<string> values = new List<string>();
            string crp = comboBox_CRP_INN.Text;
            string Date = dateTimePicker_invoice_data.Value.ToString("yyyyMMdd");
            string num_series = Docu_num_ser.Text;
            string ground = ground_textBox.Text;
            string comment = comment_textBox.Text;
            
            int cur_row = 0;
            bool flag = false;
            FlowLayoutPanel flp;
            Panel panel;
            for (int i = 0; i < tableLayoutPanel_main.Controls.Count - 4; i++)
            {

                if ((i % 2 == 1) && flag == false)
                {
                    flp = ((FlowLayoutPanel)tableLayoutPanel_main.GetControlFromPosition(1, cur_row));
                    values.Add( ((ComboBox)flp.Controls[0]).Text );
                    flag = true;
                    cur_row++;
                }
                else if ((i % 2 == 1) && flag == true)
                {
                    panel = (Panel)(tableLayoutPanel_main.GetControlFromPosition(1, cur_row));
                    values.Add(((Panel)panel.Controls[0]).Text);
                    values.Add(((ComboBox)panel.Controls[2]).Text);
                    flag = false;
                    cur_row++;
                }
            }
            #endregion

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
                    NDS_PINFL.Visible = true;
                    NDS_PINFL_comboBox.Visible = true;
                }
                else {
                    NDS_PINFL.Text = "Код НДС";
                    NDS_PINFL.Visible = true;
                    NDS_PINFL_comboBox.Visible = true;
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
        private void move(int final) {
            tableLayoutPanel_main.Controls.Add(flowLayoutPanel_basis_for_label, 0, 4);
            tableLayoutPanel_main.SetRow(flowLayoutPanel_basis_for_label, final / 2);
            tableLayoutPanel_main.SetColumn(flowLayoutPanel_basis_for_label, 0);
            tableLayoutPanel_main.SetRow(flowLayoutPanel_basis_for_textBox, final / 2);
            tableLayoutPanel_main.SetColumn(flowLayoutPanel_basis_for_textBox, 1);
            tableLayoutPanel_main.SetRow(flowLayoutPanel_comment_for_label, (final / 2) + 1);
            tableLayoutPanel_main.SetColumn(flowLayoutPanel_comment_for_label, 0);
            tableLayoutPanel_main.SetRow(flowLayoutPanel_comment_for_TextBox, (final / 2) + 1);
            tableLayoutPanel_main.SetColumn(flowLayoutPanel_comment_for_TextBox, 1);
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
