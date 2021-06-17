namespace app_for_CD
{
    partial class UC_Invoice
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.update = new System.Windows.Forms.Button();
            this.print = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGridView_invoice = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttom2 = new System.Windows.Forms.Button();
            this.filtr = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.excel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.sum_of_pay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Choice = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.filtr = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.excel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_invoice)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.update);
            this.panel3.Controls.Add(this.print);
            this.panel3.Location = new System.Drawing.Point(16, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1890, 48);
            this.panel3.TabIndex = 3;
            // 
            // update
            // 
            this.update.FlatAppearance.BorderSize = 2;
            this.update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.update.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.update.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.update.Location = new System.Drawing.Point(1735, 7);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(137, 32);
            this.update.TabIndex = 2;
            this.update.Text = "Обновить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // print
            // 
            this.print.FlatAppearance.BorderSize = 2;
            this.print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.print.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.print.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.print.Location = new System.Drawing.Point(1584, 7);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(109, 32);
            this.print.TabIndex = 0;
            this.print.Text = "Печать";
            this.print.UseVisualStyleBackColor = true;
            this.print.Click += new System.EventHandler(this.print_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel4.Controls.Add(this.dataGridView_invoice);
            this.panel4.Location = new System.Drawing.Point(16, 67);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1890, 855);
            this.panel4.TabIndex = 4;
            // 
            // dataGridView_invoice
            // 
            this.dataGridView_invoice.AllowUserToAddRows = false;
            this.dataGridView_invoice.AllowUserToDeleteRows = false;
            this.dataGridView_invoice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_invoice.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_invoice.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_invoice.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(47)))));
            this.dataGridView_invoice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Process,
            this.sum_of_pay,
            this.Column11,
            this.Choice});
            this.dataGridView_invoice.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_invoice.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_invoice.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_invoice.Name = "dataGridView_invoice";
            this.dataGridView_invoice.Size = new System.Drawing.Size(1872, 852);
            this.dataGridView_invoice.TabIndex = 2;
            this.dataGridView_invoice.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_invoice_CellClick);
            this.dataGridView_invoice.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_invoice_EditingControlShowing);
            this.dataGridView_invoice.SelectionChanged += new System.EventHandler(this.dataGridView_invoice_SelectionChanged);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel5.Controls.Add(this.buttom2);
            this.panel5.Controls.Add(this.filtr);
            this.panel5.Controls.Add(this.add);
            this.panel5.Controls.Add(this.excel);
            this.panel5.Location = new System.Drawing.Point(16, 930);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1890, 86);
            this.panel5.TabIndex = 5;
            // 
            // buttom2
            // 
            this.buttom2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttom2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttom2.FlatAppearance.BorderSize = 2;
            this.buttom2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttom2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttom2.ForeColor = System.Drawing.Color.White;
            this.buttom2.Location = new System.Drawing.Point(1645, 25);
            this.buttom2.Name = "buttom2";
            this.buttom2.Size = new System.Drawing.Size(137, 35);
            this.buttom2.TabIndex = 6;
            this.buttom2.Text = "E-contract";
            this.buttom2.UseVisualStyleBackColor = true;
            this.buttom2.Click += new System.EventHandler(this.button2_Click);
            // 
            // filtr
            // 
            this.filtr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filtr.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.filtr.FlatAppearance.BorderSize = 2;
            this.filtr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filtr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.filtr.ForeColor = System.Drawing.Color.MintCream;
            this.filtr.Location = new System.Drawing.Point(30, 26);
            this.filtr.Name = "filtr";
            this.filtr.Size = new System.Drawing.Size(137, 34);
            this.filtr.TabIndex = 5;
            this.filtr.Text = "Фильтр";
            this.filtr.UseVisualStyleBackColor = true;
            this.filtr.Click += new System.EventHandler(this.filtr_Click);
            // 
            // add
            // 
            this.add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.add.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.add.FlatAppearance.BorderSize = 2;
            this.add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add.ForeColor = System.Drawing.Color.White;
            this.add.Location = new System.Drawing.Point(1466, 26);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(137, 35);
            this.add.TabIndex = 3;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // excel
            // 
            this.excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.excel.BackgroundImage = global::app_for_CD.Properties.Resources.excel_pic_32;
            this.excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.excel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.excel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.excel.ForeColor = System.Drawing.Color.White;
            this.excel.Location = new System.Drawing.Point(1823, 26);
            this.excel.Name = "excel";
            this.excel.Size = new System.Drawing.Size(36, 32);
            this.excel.TabIndex = 2;
            this.excel.UseVisualStyleBackColor = true;
            this.excel.Click += new System.EventHandler(this.excel_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1925, 1031);
            this.panel1.TabIndex = 0;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewImageColumn1.FillWeight = 20F;
            this.dataGridViewImageColumn1.HeaderText = "Изменить";
            this.dataGridViewImageColumn1.Image = global::app_for_CD.Properties.Resources.change;
            this.dataGridViewImageColumn1.MinimumWidth = 20;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 200F;
            this.Column1.HeaderText = "Номер и дата счет- фактуры:";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 179;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 300F;
            this.Column2.HeaderText = "Номер, серия и дата договора:";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 190;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "КЗЛ:";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 57;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Наименование Клиента:";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 156;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "ИНН:";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 59;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Код НДС:";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 81;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "ПИНФЛ:";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 78;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Вид товара (услуг):";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 128;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Стоимость поставки:";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 140;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Статус:";
            this.Column10.Name = "Column10";
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column10.Width = 69;
            // 
            // Process
            // 
            this.Process.HeaderText = "Процесс";
            this.Process.Items.AddRange(new object[] {
            "Выставлена",
            "Часть оплаты",
            "Оплачена"});
            this.Process.Name = "Process";
            this.Process.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Process.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Process.Width = 76;
            // 
            // sum_of_pay
            // 
            this.sum_of_pay.HeaderText = "Сумма оплаты";
            this.sum_of_pay.Name = "sum_of_pay";
            this.sum_of_pay.Width = 106;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Ф.И.О исполнителя:";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 136;
            // 
            // Choice
            // 
            this.Choice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Choice.FillWeight = 20F;
            this.Choice.HeaderText = "Изменить";
            this.Choice.Image = global::app_for_CD.Properties.Resources.change;
            this.Choice.MinimumWidth = 20;
            this.Choice.Name = "Choice";
            this.Choice.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Choice.Width = 64;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel5.Controls.Add(this.button2);
            this.panel5.Controls.Add(this.filtr);
            this.panel5.Controls.Add(this.add);
            this.panel5.Controls.Add(this.excel);
            this.panel5.Location = new System.Drawing.Point(16, 930);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1890, 86);
            this.panel5.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(1645, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 35);
            this.button2.TabIndex = 6;
            this.button2.Text = "E-contract";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // filtr
            // 
            this.filtr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filtr.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.filtr.FlatAppearance.BorderSize = 2;
            this.filtr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filtr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.filtr.ForeColor = System.Drawing.Color.MintCream;
            this.filtr.Location = new System.Drawing.Point(30, 26);
            this.filtr.Name = "filtr";
            this.filtr.Size = new System.Drawing.Size(137, 34);
            this.filtr.TabIndex = 5;
            this.filtr.Text = "Фильтр";
            this.filtr.UseVisualStyleBackColor = true;
            this.filtr.Click += new System.EventHandler(this.filtr_Click);
            // 
            // add
            // 
            this.add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.add.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.add.FlatAppearance.BorderSize = 2;
            this.add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add.ForeColor = System.Drawing.Color.White;
            this.add.Location = new System.Drawing.Point(1466, 26);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(137, 35);
            this.add.TabIndex = 3;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // excel
            // 
            this.excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.excel.BackgroundImage = global::app_for_CD.Properties.Resources.excel_pic_32;
            this.excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.excel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.excel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.excel.ForeColor = System.Drawing.Color.White;
            this.excel.Location = new System.Drawing.Point(1823, 26);
            this.excel.Name = "excel";
            this.excel.Size = new System.Drawing.Size(36, 32);
            this.excel.TabIndex = 2;
            this.excel.UseVisualStyleBackColor = true;
            this.excel.Click += new System.EventHandler(this.excel_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1925, 1031);
            this.panel1.TabIndex = 0;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewImageColumn1.FillWeight = 20F;
            this.dataGridViewImageColumn1.HeaderText = "Изменить";
            this.dataGridViewImageColumn1.Image = global::app_for_CD.Properties.Resources.change;
            this.dataGridViewImageColumn1.MinimumWidth = 20;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // UC_Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.panel1);
            this.Name = "UC_Invoice";
            this.Size = new System.Drawing.Size(1925, 1032);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_invoice)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button excel;
        private System.Windows.Forms.Button filtr;
        private System.Windows.Forms.DataGridView dataGridView_invoice;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Button print;
        private System.Windows.Forms.Button buttom2;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewComboBoxColumn Process;
        private System.Windows.Forms.DataGridViewTextBoxColumn sum_of_pay;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewImageColumn Choice;
    }
}
