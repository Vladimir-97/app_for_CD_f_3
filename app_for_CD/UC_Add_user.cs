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
using System.Security.Cryptography;

namespace app_for_CD
{
    public partial class UC_Add_user : UserControl
    {
        int count_row = 0;
        int count_of_table_row = 8;
        string FIO, Position, Status, Login;
        public UC_Add_user()
        {
            InitializeComponent();
            SetConnection();
            LoadData();
            
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
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, e.Message);

                MessageBox.Show(errorMessage, "Error");
            }
        }
        private void LoadData()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from users_cd";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (dr.Read())
            {
                data.Add(new string[4]);

                data[data.Count - 1][0] = dr[5].ToString();
                data[data.Count - 1][1] = dr[6].ToString();
                if (dr[4].ToString() == "1")
                {
                    data[data.Count - 1][2] = "Активен";
                }
                else
                {
                    data[data.Count - 1][2] = "Заблокирован";
                }
                data[data.Count - 1][3] = dr[1].ToString();
            }

            dataGridView1.Rows.Clear();
            foreach (string[] s in data)
            {
                dataGridView1.Rows.Add(s);
            }

            dr.Close();
        }
        private Label CreateLabel(string name)
        {
            
            Label cur_label = new Label();
            cur_label.AutoSize = true;
            cur_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cur_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            cur_label.Size = new System.Drawing.Size(200, 25);
            cur_label.Text = name;
            
            return cur_label;
            
        }
        private TextBox CreateTextBox(int cur_textBox, int x, int y)
        {
            TextBox textBox = new TextBox();
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Location = new System.Drawing.Point(210, 3);
            textBox.Name = $"{count_row}";
            textBox.Size = new System.Drawing.Size(x,y);
            if(cur_textBox == 1)
            {
                textBox.Text = "Логин";
                textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            }
            else if (cur_textBox == 2)
            {
                textBox.Text = "Пароль";
                textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            }
            textBox.Enter += new System.EventHandler(this.textBox_Enter);
            textBox.Click += new System.EventHandler(this.Click_on_tc);
            return textBox;
        }
        private ComboBox CreateComboBox(int cur_combo, int x, int y)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            comboBox.FormattingEnabled = true;
            if (cur_combo == 1) {
                comboBox.Items.AddRange(new object[] {
                "Активен",
                "Заблокирован"});
            }
            comboBox.Location = new System.Drawing.Point(512, 3);
            comboBox.Name = $"{count_row}";
            comboBox.Size = new System.Drawing.Size(x, y);
            comboBox.Click += new System.EventHandler(this.Click_on_tc);
            return comboBox;
        }
        private void insert() {
            tableLayoutPanel1.Controls.Add(CreateLabel("Ф.И.О сотрудника:"));
            tableLayoutPanel1.Controls.Add(CreateTextBox(0, 227, 20));
            tableLayoutPanel1.Controls.Add(CreateLabel("Должность:"));
            tableLayoutPanel1.Controls.Add(CreateComboBox(0, 169, 21));
            tableLayoutPanel1.Controls.Add(CreateLabel("Статус:"));
            tableLayoutPanel1.Controls.Add(CreateComboBox(1, 142, 21));
            tableLayoutPanel1.Controls.Add(CreateTextBox(1, 168, 21));
            tableLayoutPanel1.Controls.Add(CreateTextBox(2, 171, 20));
        }
        private void Add_Click(object sender, EventArgs e)
        {
            for (int i=0; i<1; i++) {
                try
                {
                    insert();
                }
                catch {
                    MessageBox.Show(i.ToString());
                }
                count_row++;
            }
        }

        static string GetHash(string plaintext)
        {
            var sha = new SHA1Managed();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(plaintext));
            return Convert.ToBase64String(hash);
        }
        int find_id()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT max(id) FROM Users_cd ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            return Int32.Parse(dr[0].ToString());

        }
        private bool check() {
            bool f = false;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is TextBox || tableLayoutPanel1.Controls[i] is ComboBox)
                {
                    if (tableLayoutPanel1.Controls[i].Text == null || tableLayoutPanel1.Controls[i].Text == "" || tableLayoutPanel1.Controls[i].Text == "Пароль" || tableLayoutPanel1.Controls[i].Text == "Логин")
                    {
                        tableLayoutPanel1.Controls[i].BackColor = System.Drawing.Color.Red;
                        f = true;
                    }
                }
            }
            if (tableLayoutPanel1.Controls.Count == 0) {
                this.information.Visible = true;
                this.information.Text = "Нажмите кнопку добавить, чтобы ввести данные!";
            }
            if (f == true) {
                return false;
            }
            return true;
        }
        private void Remove()
        {
            for (int i = tableLayoutPanel1.Controls.Count - 1; i >= 0; i--)
            {
                tableLayoutPanel1.Controls[i].Dispose();
            }
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            if ( (sender as TextBox).Text == "Пароль" || (sender as TextBox).Text == "Логин") {
                (sender as TextBox).Text = "";
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void Click_on_tc(object sender, EventArgs e)
        {
            if (sender is TextBox && (sender as TextBox).BackColor == System.Drawing.Color.Red) { 
                (sender as TextBox).BackColor = System.Drawing.Color.White;
            }
            if (sender is ComboBox && (sender as ComboBox).BackColor == System.Drawing.Color.Red)
            {
                (sender as ComboBox).BackColor = System.Drawing.Color.White;
            }
        }
        //1
        private void Save_Click(object sender, EventArgs e)
        {
            int id;
            OracleCommand cmd;
            if (check() == false)
            {
                this.information.Visible = true;
                this.information.Text = "Заполните все поля!";
            }
            else
            {
                for (int i = 0; i < tableLayoutPanel1.Controls.Count / count_of_table_row; i++)
                {
                    cmd = con.CreateCommand();
                    cmd.Parameters.Add(new OracleParameter("LOGIN", tableLayoutPanel1.Controls[count_of_table_row * i+6].Text));
                    cmd.CommandText = "SELECT * FROM Users_cd where login = :LOGIN";
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    cmd.Parameters.Clear();
                    if (dr.HasRows)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "update users_cd set ";
                        cmd.Parameters.Add(new OracleParameter("PASS", GetHash(tableLayoutPanel1.Controls[count_of_table_row * i + 7].Text)));
                        cmd.CommandText += " password = :PASS, ";

                        if (tableLayoutPanel1.Controls[count_of_table_row * i + 5].Text == "Активен")
                        {
                            cmd.Parameters.Add(new OracleParameter("STATUS", 1));
                        }
                        else
                        {
                            cmd.Parameters.Add(new OracleParameter("STATUS", 2));
                        }
                        cmd.CommandText += " status = :STATUS";

                        cmd.Parameters.Add(new OracleParameter("FIO", tableLayoutPanel1.Controls[count_of_table_row * i + 1].Text));

                        cmd.CommandText += " ,fio = :FIO";

                        cmd.Parameters.Add(new OracleParameter("POSITION", tableLayoutPanel1.Controls[count_of_table_row * i + 3].Text));
                        cmd.CommandText += " ,POSITION = :POSITION";

                        cmd.Parameters.Add(new OracleParameter("LOGIN", tableLayoutPanel1.Controls[count_of_table_row * i + 6].Text));
                        cmd.CommandText += " where login = :LOGIN ";

                        cmd.CommandType = CommandType.Text;
                        if (cmd.ExecuteNonQuery() != 0)
                        {
                            information.Visible = true;
                            information.ForeColor = System.Drawing.Color.Green;
                            information.Text = "Успешно!";
                        }
                    }
                    
                    else
                    {
                        cmd = con.CreateCommand();

                        cmd.Parameters.Add(new OracleParameter("LOGIN", tableLayoutPanel1.Controls[count_of_table_row * i + 6].Text));
                        cmd.Parameters.Add(new OracleParameter("PASS", GetHash(tableLayoutPanel1.Controls[count_of_table_row * i + 7].Text)));
                        if (tableLayoutPanel1.Controls[count_of_table_row * i + 5].Text == "Активен")
                        {
                            cmd.Parameters.Add(new OracleParameter("STATUS", 1));
                        }
                        else
                        {

                            cmd.Parameters.Add(new OracleParameter("STATUS", 2));
                        }
                        cmd.Parameters.Add(new OracleParameter("FIO", tableLayoutPanel1.Controls[count_of_table_row * i + 1].Text));
                        cmd.Parameters.Add(new OracleParameter("POSITION", tableLayoutPanel1.Controls[count_of_table_row * i+3].Text));

                        cmd.CommandText = $"insert into users_cd (id , login, password, role, status, fio, position) values ({find_id()+1}, :LOGIN, :PASS, {0}, :STATUS, :FIO, :POSITION)";
                        cmd.CommandType = CommandType.Text;

                        if (cmd.ExecuteNonQuery() != 0)
                        {
                            information.Visible = true;
                            information.ForeColor = System.Drawing.Color.Green;
                            information.Text = "Успешно!";
                        }
                    }
                }
                LoadData();
                Remove();
            }
        }
        
        private void change_Click(object sender, EventArgs e)
        {
            Remove();
            insert();
            tableLayoutPanel1.Controls[1].Text = FIO;
            tableLayoutPanel1.Controls[3].Text = Position;
            tableLayoutPanel1.Controls[5].Text = Status;
            tableLayoutPanel1.Controls[6].Text = Login;

        }
        
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentRow.Index;
            if (dataGridView1.SelectedCells.Count > 1)
            {
                FIO = dataGridView1.Rows[row].Cells[0].Value.ToString();
                Position = dataGridView1.Rows[row].Cells[1].Value.ToString();
                Status = dataGridView1.Rows[row].Cells[2].Value.ToString();
                Login = dataGridView1.Rows[row].Cells[3].Value.ToString();
     
                change.Enabled = true;
            }
            else
            {
                change.Enabled = false;
            }
        }
    }
}
