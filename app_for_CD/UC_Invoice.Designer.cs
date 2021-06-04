namespace app_for_CD
{
    partial class UC_Invoice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.top_tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.print = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.dataGridView_invoice = new System.Windows.Forms.DataGridView();
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
            this.bottom_tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.excel = new System.Windows.Forms.Button();
            this.filtr = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.top_tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_invoice)).BeginInit();
            this.bottom_tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.top_tableLayoutPanel);
            this.panel1.Location = new System.Drawing.Point(14, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1224, 46);
            this.panel1.TabIndex = 0;
            // 
            // top_tableLayoutPanel
            // 
            this.top_tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.top_tableLayoutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(47)))));
            this.top_tableLayoutPanel.ColumnCount = 3;
            this.top_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.top_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.top_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.top_tableLayoutPanel.Controls.Add(this.print, 1, 1);
            this.top_tableLayoutPanel.Controls.Add(this.update, 2, 1);
            this.top_tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.top_tableLayoutPanel.Name = "top_tableLayoutPanel";
            this.top_tableLayoutPanel.RowCount = 3;
            this.top_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.75F));
            this.top_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.5F));
            this.top_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.75F));
            this.top_tableLayoutPanel.Size = new System.Drawing.Size(1224, 43);
            this.top_tableLayoutPanel.TabIndex = 0;
            // 
            // print
            // 
            this.print.Enabled = false;
            this.print.Location = new System.Drawing.Point(1027, 11);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(75, 20);
            this.print.TabIndex = 1;
            this.print.Text = "Печать";
            this.print.UseVisualStyleBackColor = true;
            this.print.Click += new System.EventHandler(this.print_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(1127, 11);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 20);
            this.update.TabIndex = 0;
            this.update.Text = "Обновить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // dataGridView_invoice
            // 
            this.dataGridView_invoice.AllowUserToAddRows = false;
            this.dataGridView_invoice.AllowUserToDeleteRows = false;
            this.dataGridView_invoice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_invoice.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_invoice.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(47)))));
            this.dataGridView_invoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_invoice.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_invoice.Location = new System.Drawing.Point(14, 66);
            this.dataGridView_invoice.Name = "dataGridView_invoice";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_invoice.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_invoice.Size = new System.Drawing.Size(1224, 536);
            this.dataGridView_invoice.TabIndex = 1;
            this.dataGridView_invoice.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_invoice_CellClick);
            this.dataGridView_invoice.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_invoice_EditingControlShowing);
            this.dataGridView_invoice.SelectionChanged += new System.EventHandler(this.dataGridView_invoice_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 200F;
            this.Column1.HeaderText = "Номер и дата счет- фактуры:";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 300F;
            this.Column2.HeaderText = "Номер, серия и дата договора:";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 300;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "КЗЛ:";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Наименование Клиента:";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "ИНН:";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Код НДС:";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "ПИНФЛ:";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Вид товара (услуг):";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Стоимость поставки:";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Статус:";
            this.Column10.Name = "Column10";
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Process
            // 
            this.Process.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Process.HeaderText = "Процесс";
            this.Process.Items.AddRange(new object[] {
            "Выставлена",
            "Часть оплаты",
            "Оплачена"});
            this.Process.Name = "Process";
            this.Process.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Process.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // sum_of_pay
            // 
            this.sum_of_pay.HeaderText = "Сумма оплаты";
            this.sum_of_pay.Name = "sum_of_pay";
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Ф.И.О исполнителя:";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Choice
            // 
            this.Choice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Choice.FillWeight = 20F;
            this.Choice.HeaderText = "Изменить";
            this.Choice.MinimumWidth = 20;
            this.Choice.Name = "Choice";
            this.Choice.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Choice.Width = 64;
            // 
            // bottom_tableLayoutPanel
            // 
            this.bottom_tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bottom_tableLayoutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(47)))));
            this.bottom_tableLayoutPanel.ColumnCount = 6;
            this.bottom_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.bottom_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.bottom_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bottom_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.bottom_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.bottom_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.bottom_tableLayoutPanel.Controls.Add(this.excel, 5, 1);
            this.bottom_tableLayoutPanel.Controls.Add(this.filtr, 0, 1);
            this.bottom_tableLayoutPanel.Controls.Add(this.add, 4, 1);
            this.bottom_tableLayoutPanel.Location = new System.Drawing.Point(14, 608);
            this.bottom_tableLayoutPanel.Name = "bottom_tableLayoutPanel";
            this.bottom_tableLayoutPanel.RowCount = 3;
            this.bottom_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.75F));
            this.bottom_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.5F));
            this.bottom_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.75F));
            this.bottom_tableLayoutPanel.Size = new System.Drawing.Size(1224, 46);
            this.bottom_tableLayoutPanel.TabIndex = 2;
            // 
            // excel
            // 
            this.excel.Location = new System.Drawing.Point(1127, 11);
            this.excel.Name = "excel";
            this.excel.Size = new System.Drawing.Size(75, 22);
            this.excel.TabIndex = 0;
            this.excel.Text = "Excel";
            this.excel.UseVisualStyleBackColor = true;
            // 
            // filtr
            // 
            this.filtr.Location = new System.Drawing.Point(3, 11);
            this.filtr.Name = "filtr";
            this.filtr.Size = new System.Drawing.Size(75, 22);
            this.filtr.TabIndex = 3;
            this.filtr.Text = "Фильтр";
            this.filtr.UseVisualStyleBackColor = true;
            this.filtr.Click += new System.EventHandler(this.filtr_Click);

            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(1027, 11);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 22);
            this.add.TabIndex = 2;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // UC_Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Controls.Add(this.bottom_tableLayoutPanel);
            this.Controls.Add(this.dataGridView_invoice);
            this.Controls.Add(this.panel1);
            this.Name = "UC_Invoice";
            this.Size = new System.Drawing.Size(1252, 667);
            this.panel1.ResumeLayout(false);
            this.top_tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_invoice)).EndInit();
            this.bottom_tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel top_tableLayoutPanel;
        private System.Windows.Forms.Button print;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.DataGridView dataGridView_invoice;
        private System.Windows.Forms.TableLayoutPanel bottom_tableLayoutPanel;
        private System.Windows.Forms.Button excel;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button filtr;
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
