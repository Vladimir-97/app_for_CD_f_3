namespace app_for_CD
{
    partial class registration_of_an_invoice
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(registration_of_an_invoice));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_CRP_INN = new System.Windows.Forms.Label();
            this.label_number_of_doc = new System.Windows.Forms.Label();
            this.label_add = new System.Windows.Forms.Label();
            this.textBox_CRP = new System.Windows.Forms.TextBox();
            this.Add = new System.Windows.Forms.Button();
            this.tableAdapterManager1 = new app_for_CD.NeedDatasetTableAdapters.TableAdapterManager();
            this.textBox_number_of_invoice = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comboBox_CRP_INN = new System.Windows.Forms.ComboBox();
            this.search_for_CRP_INN = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dateTimePicker_invoice_data = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_ser_name = new System.Windows.Forms.TextBox();
            this.search_nom_ser = new System.Windows.Forms.Button();
            this.Docu_num_ser = new System.Windows.Forms.ComboBox();
            this.label_invoice_data = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel_main = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.CRP_search = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ComboBox_ser = new System.Windows.Forms.ComboBox();
            this.search_ser = new System.Windows.Forms.Button();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox_ser = new System.Windows.Forms.TextBox();
            this.mainpanel_reg = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.New = new System.Windows.Forms.Button();
            this.Change = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel_main.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.mainpanel_reg.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.label1.Location = new System.Drawing.Point(18, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Клиент";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(18, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Данные по документу";
            // 
            // label_CRP_INN
            // 
            this.label_CRP_INN.AutoSize = true;
            this.label_CRP_INN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_CRP_INN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.label_CRP_INN.Location = new System.Drawing.Point(10, 35);
            this.label_CRP_INN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_CRP_INN.Name = "label_CRP_INN";
            this.label_CRP_INN.Size = new System.Drawing.Size(153, 25);
            this.label_CRP_INN.TabIndex = 2;
            this.label_CRP_INN.Text = "КЗЛ-ИНН код:";
            // 
            // label_number_of_doc
            // 
            this.label_number_of_doc.CausesValidation = false;
            this.label_number_of_doc.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_number_of_doc.ForeColor = System.Drawing.Color.White;
            this.label_number_of_doc.Location = new System.Drawing.Point(10, 35);
            this.label_number_of_doc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_number_of_doc.Name = "label_number_of_doc";
            this.label_number_of_doc.Size = new System.Drawing.Size(110, 24);
            this.label_number_of_doc.TabIndex = 45;
            this.label_number_of_doc.Text = "Номер:";
            // 
            // label_add
            // 
            this.label_add.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_add.AutoSize = true;
            this.label_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_add.ForeColor = System.Drawing.Color.White;
            this.label_add.Location = new System.Drawing.Point(815, 29);
            this.label_add.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_add.Name = "label_add";
            this.label_add.Size = new System.Drawing.Size(165, 24);
            this.label_add.TabIndex = 46;
            this.label_add.Text = "Добавить услугу:";
            // 
            // textBox_CRP
            // 
            this.textBox_CRP.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBox_CRP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_CRP.Location = new System.Drawing.Point(376, 39);
            this.textBox_CRP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_CRP.Name = "textBox_CRP";
            this.textBox_CRP.ReadOnly = true;
            this.textBox_CRP.Size = new System.Drawing.Size(696, 20);
            this.textBox_CRP.TabIndex = 44;
            // 
            // Add
            // 
            this.Add.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Add.Location = new System.Drawing.Point(997, 29);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(75, 23);
            this.Add.TabIndex = 47;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.MyCreateButton_Click);
            // 
            // tableAdapterManager1
            // 
            this.tableAdapterManager1.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager1.Connection = null;
            this.tableAdapterManager1.NEW_TBCBTableAdapter = null;
            this.tableAdapterManager1.TABLE_FOR_CDTableAdapter = null;
            this.tableAdapterManager1.TBCB_CRP_DOCU_INFOTableAdapter = null;
            this.tableAdapterManager1.TBCB_CRP_INFOTableAdapter = null;
            this.tableAdapterManager1.UpdateOrder = app_for_CD.NeedDatasetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // textBox_number_of_invoice
            // 
            this.textBox_number_of_invoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_number_of_invoice.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBox_number_of_invoice.Enabled = false;
            this.textBox_number_of_invoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_number_of_invoice.ForeColor = System.Drawing.Color.Black;
            this.textBox_number_of_invoice.Location = new System.Drawing.Point(162, 35);
            this.textBox_number_of_invoice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_number_of_invoice.Name = "textBox_number_of_invoice";
            this.textBox_number_of_invoice.ReadOnly = true;
            this.textBox_number_of_invoice.Size = new System.Drawing.Size(189, 20);
            this.textBox_number_of_invoice.TabIndex = 55;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(89)))), ((int)(((byte)(17)))));
            this.panel3.Location = new System.Drawing.Point(15, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(3, 16);
            this.panel3.TabIndex = 47;
            // 
            // comboBox_CRP_INN
            // 
            this.comboBox_CRP_INN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_CRP_INN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_CRP_INN.FormattingEnabled = true;
            this.comboBox_CRP_INN.Location = new System.Drawing.Point(162, 39);
            this.comboBox_CRP_INN.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBox_CRP_INN.MaxLength = 12;
            this.comboBox_CRP_INN.Name = "comboBox_CRP_INN";
            this.comboBox_CRP_INN.Size = new System.Drawing.Size(157, 21);
            this.comboBox_CRP_INN.TabIndex = 63;
            this.comboBox_CRP_INN.SelectedValueChanged += new System.EventHandler(this.comboBox_CRP_SelectedValueChanged);
            this.comboBox_CRP_INN.TextChanged += new System.EventHandler(this.comboBox_CRP_INN_TextChanged);
            // 
            // search_for_CRP_INN
            // 
            this.search_for_CRP_INN.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.search_for_CRP_INN.Image = ((System.Drawing.Image)(resources.GetObject("search_for_CRP_INN.Image")));
            this.search_for_CRP_INN.Location = new System.Drawing.Point(328, 40);
            this.search_for_CRP_INN.Name = "search_for_CRP_INN";
            this.search_for_CRP_INN.Size = new System.Drawing.Size(23, 21);
            this.search_for_CRP_INN.TabIndex = 64;
            this.search_for_CRP_INN.UseVisualStyleBackColor = true;
            this.search_for_CRP_INN.Click += new System.EventHandler(this.search_for_CRP_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(47)))));
            this.panel2.Controls.Add(this.dateTimePicker_invoice_data);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBox_ser_name);
            this.panel2.Controls.Add(this.search_nom_ser);
            this.panel2.Controls.Add(this.Docu_num_ser);
            this.panel2.Controls.Add(this.label_invoice_data);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.tableLayoutPanel_main);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.Add);
            this.panel2.Controls.Add(this.label_number_of_doc);
            this.panel2.Controls.Add(this.label_add);
            this.panel2.Controls.Add(this.textBox_number_of_invoice);
            this.panel2.Location = new System.Drawing.Point(0, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1116, 433);
            this.panel2.TabIndex = 59;
            // 
            // dateTimePicker_invoice_data
            // 
            this.dateTimePicker_invoice_data.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dateTimePicker_invoice_data.CustomFormat = "dd.MM.yyyy";
            this.dateTimePicker_invoice_data.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_invoice_data.Location = new System.Drawing.Point(508, 35);
            this.dateTimePicker_invoice_data.Name = "dateTimePicker_invoice_data";
            this.dateTimePicker_invoice_data.Size = new System.Drawing.Size(145, 20);
            this.dateTimePicker_invoice_data.TabIndex = 60;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.label6.Location = new System.Drawing.Point(10, 70);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 24);
            this.label6.TabIndex = 48;
            this.label6.Text = "Договор:";
            // 
            // textBox_ser_name
            // 
            this.textBox_ser_name.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBox_ser_name.Location = new System.Drawing.Point(376, 75);
            this.textBox_ser_name.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_ser_name.Name = "textBox_ser_name";
            this.textBox_ser_name.ReadOnly = true;
            this.textBox_ser_name.Size = new System.Drawing.Size(696, 20);
            this.textBox_ser_name.TabIndex = 46;
            // 
            // search_nom_ser
            // 
            this.search_nom_ser.Image = ((System.Drawing.Image)(resources.GetObject("search_nom_ser.Image")));
            this.search_nom_ser.Location = new System.Drawing.Point(328, 73);
            this.search_nom_ser.Name = "search_nom_ser";
            this.search_nom_ser.Size = new System.Drawing.Size(23, 21);
            this.search_nom_ser.TabIndex = 62;
            this.search_nom_ser.UseVisualStyleBackColor = true;
            this.search_nom_ser.Click += new System.EventHandler(this.search_nom_ser_Click);
            // 
            // Docu_num_ser
            // 
            this.Docu_num_ser.FormattingEnabled = true;
            this.Docu_num_ser.Location = new System.Drawing.Point(162, 73);
            this.Docu_num_ser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Docu_num_ser.Name = "Docu_num_ser";
            this.Docu_num_ser.Size = new System.Drawing.Size(157, 21);
            this.Docu_num_ser.TabIndex = 61;
            this.Docu_num_ser.TextChanged += new System.EventHandler(this.Docu_num_ser_TextChanged);
            // 
            // label_invoice_data
            // 
            this.label_invoice_data.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_invoice_data.AutoSize = true;
            this.label_invoice_data.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_invoice_data.ForeColor = System.Drawing.Color.White;
            this.label_invoice_data.Location = new System.Drawing.Point(379, 31);
            this.label_invoice_data.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_invoice_data.Name = "label_invoice_data";
            this.label_invoice_data.Size = new System.Drawing.Size(59, 24);
            this.label_invoice_data.TabIndex = 59;
            this.label_invoice_data.Text = "Дата:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(89)))), ((int)(((byte)(17)))));
            this.panel4.Location = new System.Drawing.Point(14, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(3, 16);
            this.panel4.TabIndex = 46;
            // 
            // tableLayoutPanel_main
            // 
            this.tableLayoutPanel_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_main.AutoScroll = true;
            this.tableLayoutPanel_main.ColumnCount = 3;
            this.tableLayoutPanel_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 152F));
            this.tableLayoutPanel_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 733F));
            this.tableLayoutPanel_main.Controls.Add(this.flowLayoutPanel5, 0, 0);
            this.tableLayoutPanel_main.Controls.Add(this.flowLayoutPanel6, 0, 1);
            this.tableLayoutPanel_main.Controls.Add(this.flowLayoutPanel3, 1, 1);
            this.tableLayoutPanel_main.Controls.Add(this.flowLayoutPanel2, 2, 1);
            this.tableLayoutPanel_main.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel_main.Controls.Add(this.flowLayoutPanel4, 2, 0);
            this.tableLayoutPanel_main.Location = new System.Drawing.Point(3, 102);
            this.tableLayoutPanel_main.Name = "tableLayoutPanel_main";
            this.tableLayoutPanel_main.RowCount = 2;
            this.tableLayoutPanel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.92661F));
            this.tableLayoutPanel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.07339F));
            this.tableLayoutPanel_main.Size = new System.Drawing.Size(1099, 327);
            this.tableLayoutPanel_main.TabIndex = 58;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.label3);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(114, 32);
            this.flowLayoutPanel5.TabIndex = 63;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.label3.Location = new System.Drawing.Point(4, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 32);
            this.label3.TabIndex = 55;
            this.label3.Text = "Услуга - 1:";
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Controls.Add(this.label8);
            this.flowLayoutPanel6.Location = new System.Drawing.Point(3, 42);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(85, 32);
            this.flowLayoutPanel6.TabIndex = 58;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.label8.Location = new System.Drawing.Point(4, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 24);
            this.label8.TabIndex = 55;
            this.label8.Text = "Сумма:";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.textBox3);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(155, 42);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(205, 32);
            this.flowLayoutPanel3.TabIndex = 59;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(4, 3);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(158, 20);
            this.textBox3.TabIndex = 59;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label9);
            this.flowLayoutPanel2.Controls.Add(this.comboBox6);
            this.flowLayoutPanel2.Controls.Add(this.CRP_search);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(369, 42);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(264, 32);
            this.flowLayoutPanel2.TabIndex = 58;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.label9.Location = new System.Drawing.Point(4, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 24);
            this.label9.TabIndex = 56;
            this.label9.Text = "Валюта:";
            // 
            // comboBox6
            // 
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Items.AddRange(new object[] {
            "СУМ",
            "АМ.ДОЛЛАР",
            "ЕВРО",
            "ЙЕНА",
            "ФУНТ",
            "АВ.ДОЛЛАР"});
            this.comboBox6.Location = new System.Drawing.Point(92, 3);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(121, 21);
            this.comboBox6.TabIndex = 55;
            // 
            // CRP_search
            // 
            this.CRP_search.Image = ((System.Drawing.Image)(resources.GetObject("CRP_search.Image")));
            this.CRP_search.Location = new System.Drawing.Point(219, 3);
            this.CRP_search.Name = "CRP_search";
            this.CRP_search.Size = new System.Drawing.Size(23, 23);
            this.CRP_search.TabIndex = 54;
            this.CRP_search.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ComboBox_ser);
            this.flowLayoutPanel1.Controls.Add(this.search_ser);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(155, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(205, 26);
            this.flowLayoutPanel1.TabIndex = 58;
            // 
            // ComboBox_ser
            // 
            this.ComboBox_ser.FormattingEnabled = true;
            this.ComboBox_ser.Location = new System.Drawing.Point(4, 3);
            this.ComboBox_ser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ComboBox_ser.Name = "ComboBox_ser";
            this.ComboBox_ser.Size = new System.Drawing.Size(158, 21);
            this.ComboBox_ser.TabIndex = 40;
            // 
            // search_ser
            // 
            this.search_ser.Image = ((System.Drawing.Image)(resources.GetObject("search_ser.Image")));
            this.search_ser.Location = new System.Drawing.Point(169, 3);
            this.search_ser.Name = "search_ser";
            this.search_ser.Size = new System.Drawing.Size(23, 21);
            this.search_ser.TabIndex = 55;
            this.search_ser.UseVisualStyleBackColor = true;
            this.search_ser.Click += new System.EventHandler(this.search_ser_Click);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.textBox_ser);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(369, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(700, 26);
            this.flowLayoutPanel4.TabIndex = 59;
            // 
            // textBox_ser
            // 
            this.textBox_ser.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBox_ser.Location = new System.Drawing.Point(4, 3);
            this.textBox_ser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_ser.Name = "textBox_ser";
            this.textBox_ser.Size = new System.Drawing.Size(698, 20);
            this.textBox_ser.TabIndex = 55;
            // 
            // mainpanel_reg
            // 
            this.mainpanel_reg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainpanel_reg.Controls.Add(this.panel5);
            this.mainpanel_reg.Controls.Add(this.label11);
            this.mainpanel_reg.Controls.Add(this.New);
            this.mainpanel_reg.Controls.Add(this.Change);
            this.mainpanel_reg.Controls.Add(this.Save);
            this.mainpanel_reg.Controls.Add(this.panel2);
            this.mainpanel_reg.Location = new System.Drawing.Point(10, 11);
            this.mainpanel_reg.Name = "mainpanel_reg";
            this.mainpanel_reg.Size = new System.Drawing.Size(1118, 547);
            this.mainpanel_reg.TabIndex = 65;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(47)))));
            this.panel5.Controls.Add(this.textBox_CRP);
            this.panel5.Controls.Add(this.panel3);
            this.panel5.Controls.Add(this.search_for_CRP_INN);
            this.panel5.Controls.Add(this.comboBox_CRP_INN);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.label_CRP_INN);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1116, 66);
            this.panel5.TabIndex = 64;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label11.Location = new System.Drawing.Point(11, 522);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(252, 20);
            this.label11.TabIndex = 63;
            this.label11.Text = "Отчет об выполнении операции";
            // 
            // New
            // 
            this.New.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.New.Location = new System.Drawing.Point(764, 513);
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(100, 30);
            this.New.TabIndex = 62;
            this.New.Text = "Новый";
            this.New.UseVisualStyleBackColor = true;
            // 
            // Change
            // 
            this.Change.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Change.Location = new System.Drawing.Point(870, 513);
            this.Change.Name = "Change";
            this.Change.Size = new System.Drawing.Size(100, 30);
            this.Change.TabIndex = 61;
            this.Change.Text = "Изменить";
            this.Change.UseVisualStyleBackColor = true;
            // 
            // Save
            // 
            this.Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Save.Location = new System.Drawing.Point(980, 513);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(100, 30);
            this.Save.TabIndex = 60;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // registration_of_an_invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1130, 557);
            this.Controls.Add(this.mainpanel_reg);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "registration_of_an_invoice";
            this.Text = "Регистрация счет фактуры";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel_main.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.mainpanel_reg.ResumeLayout(false);
            this.mainpanel_reg.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_CRP_INN;
        private System.Windows.Forms.Label label_number_of_doc;
        private System.Windows.Forms.Label label_add;
        private System.Windows.Forms.TextBox textBox_CRP;
        private System.Windows.Forms.Button Add;
        private NeedDatasetTableAdapters.TableAdapterManager tableAdapterManager1;
        private System.Windows.Forms.TextBox textBox_number_of_invoice;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_main;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.Button CRP_search;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox ComboBox_ser;
        private System.Windows.Forms.Button search_ser;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.TextBox textBox_ser;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label_invoice_data;
        private System.Windows.Forms.TextBox textBox_ser_name;
        private System.Windows.Forms.Button search_nom_ser;
        private System.Windows.Forms.ComboBox Docu_num_ser;
        private System.Windows.Forms.DateTimePicker dateTimePicker_invoice_data;
        private System.Windows.Forms.Button search_for_CRP_INN;
        private System.Windows.Forms.ComboBox comboBox_CRP_INN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel mainpanel_reg;
        private System.Windows.Forms.Button New;
        private System.Windows.Forms.Button Change;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
    }
}