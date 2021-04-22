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

namespace app_for_CD
{
    public partial class registration_of_an_invoice : Form
    {
        ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(registration_of_an_invoice));
        OracleConnection con = null;
        int count_of_label = 1, count_of_comboBox = 1, count_of_Button = 1, count_of_TextBox = 1;
        int count_of_label_u = 0;
        int row = 0;
        //List<Label> cur_label = new List<Label>();
        List<ComboBox> comboBoxes = new List<ComboBox>();
        
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
            tableLayoutPanel_main.AutoScroll = false;
            tableLayoutPanel_main.HorizontalScroll.Enabled = false;
            tableLayoutPanel_main.HorizontalScroll.Visible = false;
            tableLayoutPanel_main.HorizontalScroll.Maximum = 0;
            tableLayoutPanel_main.RowCount = 2;
            tableLayoutPanel_main.RowStyles.Clear();
            tableLayoutPanel_main.AutoScroll = true;
        }

        private Label CreateLabel(string name) {
            Label cur_label = new Label();
            cur_label.AutoSize = true;
            cur_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cur_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            cur_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            cur_label.Name = "Label_"+count_of_label.ToString();
            cur_label.Size = new System.Drawing.Size(93, 24);
            cur_label.Text = name;
            count_of_label++;
            return cur_label;
        }

        private ComboBox CreateComboBox(int x, int y) {
            ComboBox combo = new ComboBox();
            combo.FormattingEnabled = true;
            combo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            combo.Name = "ComboBox_" + count_of_comboBox.ToString();
            combo.Size = new System.Drawing.Size(x,y);
            count_of_comboBox++;
            return combo;
        }

        private void search_for_CRP_Click(object sender, EventArgs e)
        {
            this.SetConnection();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CRP_CD FROM TBCB_CRP_INFO where rownum <=1000";


            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox_CRP.Items.Add(dr[0].ToString());
            }
        }

        private Button searchButton() {
            Button but = new Button();
            but.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            but.Location = new System.Drawing.Point(169, 3);
            but.Name = "button_"+count_of_Button.ToString();
            //MessageBox.Show(but.Name);
            but.Size = new System.Drawing.Size(23, 21);
            but.UseVisualStyleBackColor = true;
            count_of_Button++;
            return but;
        }

        private void comboBox_CRP_SelectedValueChanged(object sender, EventArgs e)
        {
            string crp = comboBox_CRP.SelectedItem.ToString();
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = crp;
            cmd.CommandText = "SELECT CRP_NM FROM TBCB_CRP_INFO where CRP_CD = :KZL";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox_CRP.Text = dr[0].ToString();
            }
        }


        private TextBox Create_TextBox(int x, int y, bool grey)
        {
            TextBox textBox = new TextBox();
            if( grey == true )
                textBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            textBox.Location = new System.Drawing.Point(4, 3);
            textBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox.Name = "textBox_"+count_of_TextBox;
            textBox.Size = new System.Drawing.Size(x, y);
            count_of_TextBox++;
            return textBox;
        }
        private void FlowLayoutPanel_Create_double(byte choice) {
            FlowLayoutPanel flws = new FlowLayoutPanel();
            if (choice == 1)
            {
                flws.Controls.Add(CreateLabel($"Услуга - {count_of_label_u + 2}:"));
                flws.Size = new System.Drawing.Size(205, 26);
                count_of_label_u++;
                tableLayoutPanel_main.Controls.Add(flws, 0, row);
            }
            else if (choice == 2)
            {
                flws.Controls.Add(CreateComboBox(158, 21));
                flws.Controls.Add(searchButton());
                flws.Size = new System.Drawing.Size(205, 30);
                tableLayoutPanel_main.Controls.Add(flws, 1, row);
            }
            else if (choice == 3)
            {
                flws.Controls.Add(Create_TextBox(696, 20, true));
                flws.Size = new System.Drawing.Size(700, 26);
                tableLayoutPanel_main.Controls.Add(flws, 2, row);
            }
            else if (choice == 4) {
                flws.Controls.Add(CreateLabel("Сумма:"));
                flws.Size = new System.Drawing.Size(85, 41);
                tableLayoutPanel_main.Controls.Add(flws, 0, row + 1);
            }
            else if (choice == 5)
            {
                flws.Controls.Add(Create_TextBox(158, 20, false));
                flws.Size = new System.Drawing.Size(205, 41);
                tableLayoutPanel_main.Controls.Add(flws, 1, row + 1);
            }
            else if (choice == 6)
            {
                flws.Controls.Add(CreateLabel("Валюта:"));
                flws.Controls.Add(CreateComboBox(121, 21));
                flws.Controls.Add(searchButton());
                flws.Size = new System.Drawing.Size(264, 41);
                tableLayoutPanel_main.Controls.Add(flws, 2, row + 1);
            }

        }
        private void MyCreateButton_Click(object sender, EventArgs e)
        {
            row += 2;
            for (byte i = 1; i <= 6; i++)
            {
                FlowLayoutPanel_Create_double(i);
            }
        }
    }
}
