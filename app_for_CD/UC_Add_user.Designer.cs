namespace app_for_CD
{
    partial class UC_Add_user
    {

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.change = new System.Windows.Forms.Button();
            this.information = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FIO_label_0 = new System.Windows.Forms.Label();
            this.FIO_textBox_0 = new System.Windows.Forms.TextBox();
            this.Position_label_0 = new System.Windows.Forms.Label();
            this.Position_comboBox_0 = new System.Windows.Forms.ComboBox();
            this.status_label_0 = new System.Windows.Forms.Label();
            this.status_comboBox_0 = new System.Windows.Forms.ComboBox();
            this.Login_textBox_0 = new System.Windows.Forms.TextBox();
            this.password_textBox_0 = new System.Windows.Forms.TextBox();
            this.Save = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.Clear);
            this.panel1.Controls.Add(this.change);
            this.panel1.Controls.Add(this.information);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Controls.Add(this.Save);
            this.panel1.Controls.Add(this.Add);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1292, 728);
            this.panel1.TabIndex = 0;
            // 
            // change
            // 
            this.change.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.change.Enabled = false;
            this.change.Location = new System.Drawing.Point(1011, 696);
            this.change.Name = "change";
            this.change.Size = new System.Drawing.Size(75, 23);
            this.change.TabIndex = 15;
            this.change.Text = "Изменить";
            this.change.UseVisualStyleBackColor = true;
            this.change.Click += new System.EventHandler(this.change_Click);
            // 
            // information
            // 
            this.information.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.information.AutoSize = true;
            this.information.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.information.ForeColor = System.Drawing.Color.DarkRed;
            this.information.Location = new System.Drawing.Point(26, 696);
            this.information.Name = "information";
            this.information.Size = new System.Drawing.Size(252, 20);
            this.information.TabIndex = 1;
            this.information.Text = "Отчет об выполнении операции";
            this.information.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(20, 20);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.5F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1252, 667);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(47)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1246, 324);
            this.dataGridView1.TabIndex = 13;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Ф.И.О.";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Должность";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Статус";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "Логин";
            this.Column4.Name = "Column4";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(47)))));
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.8144F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.38851F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.03079F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.38315F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.38315F));
            this.tableLayoutPanel1.Controls.Add(this.FIO_label_0, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.FIO_textBox_0, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Position_label_0, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.Position_comboBox_0, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.status_label_0, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.status_comboBox_0, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.Login_textBox_0, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.password_textBox_0, 7, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 339);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1246, 325);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // FIO_label_0
            // 
            this.FIO_label_0.AutoSize = true;
            this.FIO_label_0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FIO_label_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FIO_label_0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.FIO_label_0.Location = new System.Drawing.Point(3, 0);
            this.FIO_label_0.Name = "FIO_label_0";
            this.FIO_label_0.Size = new System.Drawing.Size(155, 325);
            this.FIO_label_0.TabIndex = 0;
            this.FIO_label_0.Text = "Ф.И.О сотрудника:";
            // 
            // FIO_textBox_0
            // 
            this.FIO_textBox_0.BackColor = System.Drawing.SystemColors.Window;
            this.FIO_textBox_0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FIO_textBox_0.Location = new System.Drawing.Point(164, 3);
            this.FIO_textBox_0.Name = "FIO_textBox_0";
            this.FIO_textBox_0.Size = new System.Drawing.Size(227, 20);
            this.FIO_textBox_0.TabIndex = 4;
            this.FIO_textBox_0.Click += new System.EventHandler(this.Click_on_tc);
            // 
            // Position_label_0
            // 
            this.Position_label_0.AutoSize = true;
            this.Position_label_0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Position_label_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Position_label_0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Position_label_0.Location = new System.Drawing.Point(397, 0);
            this.Position_label_0.Name = "Position_label_0";
            this.Position_label_0.Size = new System.Drawing.Size(100, 325);
            this.Position_label_0.TabIndex = 3;
            this.Position_label_0.Text = "Должность:";
            // 
            // Position_comboBox_0
            // 
            this.Position_comboBox_0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Position_comboBox_0.DropDownWidth = 163;
            this.Position_comboBox_0.FormattingEnabled = true;
            this.Position_comboBox_0.Location = new System.Drawing.Point(503, 3);
            this.Position_comboBox_0.Name = "Position_comboBox_0";
            this.Position_comboBox_0.Size = new System.Drawing.Size(169, 21);
            this.Position_comboBox_0.TabIndex = 1;
            this.Position_comboBox_0.Click += new System.EventHandler(this.Click_on_tc);
            // 
            // status_label_0
            // 
            this.status_label_0.AutoSize = true;
            this.status_label_0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.status_label_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status_label_0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.status_label_0.Location = new System.Drawing.Point(678, 0);
            this.status_label_0.Name = "status_label_0";
            this.status_label_0.Size = new System.Drawing.Size(69, 325);
            this.status_label_0.TabIndex = 5;
            this.status_label_0.Text = "Статус:";
            // 
            // status_comboBox_0
            // 
            this.status_comboBox_0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.status_comboBox_0.FormattingEnabled = true;
            this.status_comboBox_0.Items.AddRange(new object[] {
            "Активен",
            "Заблокирован"});
            this.status_comboBox_0.Location = new System.Drawing.Point(753, 3);
            this.status_comboBox_0.Name = "status_comboBox_0";
            this.status_comboBox_0.Size = new System.Drawing.Size(138, 21);
            this.status_comboBox_0.TabIndex = 12;
            this.status_comboBox_0.Click += new System.EventHandler(this.Click_on_tc);
            // 
            // Login_textBox_0
            // 
            this.Login_textBox_0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Login_textBox_0.Location = new System.Drawing.Point(897, 3);
            this.Login_textBox_0.Name = "Login_textBox_0";
            this.Login_textBox_0.Size = new System.Drawing.Size(169, 20);
            this.Login_textBox_0.TabIndex = 14;
            this.Login_textBox_0.Text = "Логин";
            this.Login_textBox_0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Login_textBox_0.Enter += new System.EventHandler(this.textBox_Enter);
            this.Login_textBox_0.Click += new System.EventHandler(this.Click_on_tc);
            // 
            // password_textBox_0
            // 
            this.password_textBox_0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.password_textBox_0.Location = new System.Drawing.Point(1072, 3);
            this.password_textBox_0.Name = "password_textBox_0";
            this.password_textBox_0.Size = new System.Drawing.Size(171, 20);
            this.password_textBox_0.TabIndex = 5;
            this.password_textBox_0.Text = "Пароль";
            this.password_textBox_0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.password_textBox_0.Enter += new System.EventHandler(this.textBox_Enter);
            this.password_textBox_0.Click += new System.EventHandler(this.Click_on_tc);
            // 
            // Save
            // 
            this.Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Save.Location = new System.Drawing.Point(1092, 696);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 2;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Add
            // 
            this.Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Add.Location = new System.Drawing.Point(1173, 696);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(75, 23);
            this.Add.TabIndex = 1;
            this.Add.Text = "Добавить";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Clear
            // 
            this.Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Clear.Location = new System.Drawing.Point(930, 696);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 23);
            this.Clear.TabIndex = 16;
            this.Clear.Text = "Очистить";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // UC_Add_user
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UC_Add_user";
            this.Size = new System.Drawing.Size(1292, 728);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox Login_textBox_0;
        private System.Windows.Forms.TextBox password_textBox_0;
        private System.Windows.Forms.ComboBox status_comboBox_0;
        private System.Windows.Forms.ComboBox Position_comboBox_0;
        private System.Windows.Forms.Label Position_label_0;
        private System.Windows.Forms.Label FIO_label_0;
        private System.Windows.Forms.Label status_label_0;
        private System.Windows.Forms.TextBox FIO_textBox_0;
        private System.Windows.Forms.Label information;
        private System.Windows.Forms.Button change;
        private System.Windows.Forms.Button Clear;
    }
}
