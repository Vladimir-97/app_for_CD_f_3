namespace app_for_CD
{
    partial class filter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(filter));
            this.CustomerName = new System.Windows.Forms.CheckBox();
            this.CRP = new System.Windows.Forms.CheckBox();
            this.PeriodOfImprisonment = new System.Windows.Forms.CheckBox();
            this.Ok = new System.Windows.Forms.Button();
            this.dateTimePicker_st = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_end = new System.Windows.Forms.DateTimePicker();
            this.ComboBox_CRP = new System.Windows.Forms.ComboBox();
            this.textBox_name_cl = new System.Windows.Forms.TextBox();
            this.CRP_search = new System.Windows.Forms.Button();
            this.ContractPrice = new System.Windows.Forms.CheckBox();
            this.Сalculus = new System.Windows.Forms.CheckBox();
            this.comboBox_calculus = new System.Windows.Forms.ComboBox();
            this.textBox_price = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBox_currency = new System.Windows.Forms.ComboBox();
            this.ContractSeries = new System.Windows.Forms.CheckBox();
            this.comboBox_ser = new System.Windows.Forms.ComboBox();
            this.INN = new System.Windows.Forms.CheckBox();
            this.textBox_INN = new System.Windows.Forms.TextBox();
            this.СontractStatus = new System.Windows.Forms.CheckBox();
            this.comboBox_status = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CustomerName
            // 
            this.CustomerName.AutoSize = true;
            this.CustomerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.CustomerName.Location = new System.Drawing.Point(7, 131);
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.Size = new System.Drawing.Size(273, 29);
            this.CustomerName.TabIndex = 2;
            this.CustomerName.Text = "Наименование клиента:";
            this.CustomerName.UseVisualStyleBackColor = true;
            this.CustomerName.CheckedChanged += new System.EventHandler(this.CustomerName_CheckedChanged);
            // 
            // CRP
            // 
            this.CRP.AutoSize = true;
            this.CRP.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CRP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.CRP.Location = new System.Drawing.Point(7, 100);
            this.CRP.Name = "CRP";
            this.CRP.Size = new System.Drawing.Size(79, 29);
            this.CRP.TabIndex = 3;
            this.CRP.Text = "КЗЛ:";
            this.CRP.UseVisualStyleBackColor = true;
            this.CRP.CheckedChanged += new System.EventHandler(this.CRP_CheckedChanged);
            // 
            // PeriodOfImprisonment
            // 
            this.PeriodOfImprisonment.AutoSize = true;
            this.PeriodOfImprisonment.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PeriodOfImprisonment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.PeriodOfImprisonment.Location = new System.Drawing.Point(7, 7);
            this.PeriodOfImprisonment.Name = "PeriodOfImprisonment";
            this.PeriodOfImprisonment.Size = new System.Drawing.Size(123, 29);
            this.PeriodOfImprisonment.TabIndex = 4;
            this.PeriodOfImprisonment.Text = "Период с";
            this.PeriodOfImprisonment.UseVisualStyleBackColor = true;
            this.PeriodOfImprisonment.CheckedChanged += new System.EventHandler(this.PeriodOfImprisonment_CheckedChanged);
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(996, 330);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 5;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // dateTimePicker_st
            // 
            this.dateTimePicker_st.CustomFormat = "dd.MM.yyyy";
            this.dateTimePicker_st.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_st.Location = new System.Drawing.Point(297, 8);
            this.dateTimePicker_st.Name = "dateTimePicker_st";
            this.dateTimePicker_st.Size = new System.Drawing.Size(138, 20);
            this.dateTimePicker_st.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.label1.Location = new System.Drawing.Point(441, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "по";
            // 
            // dateTimePicker_end
            // 
            this.dateTimePicker_end.CustomFormat = "dd.MM.yyyy";
            this.dateTimePicker_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_end.Location = new System.Drawing.Point(483, 8);
            this.dateTimePicker_end.Name = "dateTimePicker_end";
            this.dateTimePicker_end.Size = new System.Drawing.Size(138, 20);
            this.dateTimePicker_end.TabIndex = 8;
            // 
            // ComboBox_CRP
            // 
            this.ComboBox_CRP.FormattingEnabled = true;
            this.ComboBox_CRP.Location = new System.Drawing.Point(297, 100);
            this.ComboBox_CRP.Name = "ComboBox_CRP";
            this.ComboBox_CRP.Size = new System.Drawing.Size(718, 21);
            this.ComboBox_CRP.TabIndex = 39;
            // 
            // textBox_name_cl
            // 
            this.textBox_name_cl.Location = new System.Drawing.Point(297, 131);
            this.textBox_name_cl.Name = "textBox_name_cl";
            this.textBox_name_cl.Size = new System.Drawing.Size(747, 20);
            this.textBox_name_cl.TabIndex = 41;
            // 
            // CRP_search
            // 
            this.CRP_search.Image = ((System.Drawing.Image)(resources.GetObject("CRP_search.Image")));
            this.CRP_search.Location = new System.Drawing.Point(1021, 100);
            this.CRP_search.Name = "CRP_search";
            this.CRP_search.Size = new System.Drawing.Size(23, 23);
            this.CRP_search.TabIndex = 42;
            this.CRP_search.UseVisualStyleBackColor = true;
            this.CRP_search.Click += new System.EventHandler(this.CRP_search_Click);
            // 
            // ContractPrice
            // 
            this.ContractPrice.AutoSize = true;
            this.ContractPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContractPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ContractPrice.Location = new System.Drawing.Point(7, 162);
            this.ContractPrice.Name = "ContractPrice";
            this.ContractPrice.Size = new System.Drawing.Size(186, 29);
            this.ContractPrice.TabIndex = 43;
            this.ContractPrice.Text = "Цена договора:";
            this.ContractPrice.UseVisualStyleBackColor = true;
            this.ContractPrice.CheckedChanged += new System.EventHandler(this.ContractPrice_CheckedChanged);
            // 
            // Сalculus
            // 
            this.Сalculus.AutoSize = true;
            this.Сalculus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Сalculus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Сalculus.Location = new System.Drawing.Point(7, 193);
            this.Сalculus.Name = "Сalculus";
            this.Сalculus.Size = new System.Drawing.Size(157, 29);
            this.Сalculus.TabIndex = 44;
            this.Сalculus.Text = "Исчисление:";
            this.Сalculus.UseVisualStyleBackColor = true;
            this.Сalculus.CheckedChanged += new System.EventHandler(this.Сalculus_CheckedChanged);
            // 
            // comboBox_calculus
            // 
            this.comboBox_calculus.FormattingEnabled = true;
            this.comboBox_calculus.Items.AddRange(new object[] {
            "БРВ",
            "Сумма",
            ""});
            this.comboBox_calculus.Location = new System.Drawing.Point(297, 193);
            this.comboBox_calculus.Name = "comboBox_calculus";
            this.comboBox_calculus.Size = new System.Drawing.Size(747, 21);
            this.comboBox_calculus.TabIndex = 45;
            // 
            // textBox_price
            // 
            this.textBox_price.Location = new System.Drawing.Point(297, 162);
            this.textBox_price.Name = "textBox_price";
            this.textBox_price.Size = new System.Drawing.Size(336, 20);
            this.textBox_price.TabIndex = 46;
            this.textBox_price.TextChanged += new System.EventHandler(this.CRP_search_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.label18.Location = new System.Drawing.Point(626, 158);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(85, 25);
            this.label18.TabIndex = 47;
            this.label18.Text = "валюта";
            // 
            // comboBox_currency
            // 
            this.comboBox_currency.FormattingEnabled = true;
            this.comboBox_currency.Items.AddRange(new object[] {
            "AFN",
            "EUR",
            "ALL",
            "DZD",
            "USD",
            "AOA",
            "XCD",
            "ARS",
            "AMD",
            "AWG",
            "AUD",
            "AZN",
            "BSD",
            "BHD",
            "BDT",
            "BBD",
            "BYN",
            "BZD",
            "XOF",
            "BMD",
            "INR",
            "BTN",
            "BOB",
            "BOV",
            "BAM",
            "BWP",
            "NOK",
            "BRL",
            "BND",
            "BGN",
            "BIF",
            "CVE",
            "KHR",
            "XAF",
            "CAD",
            "KYD",
            "CLP",
            "CLF",
            "CNY",
            "COP",
            "COU",
            "KMF",
            "CDF",
            "NZD",
            "CRC",
            "HRK",
            "CUP",
            "CUC",
            "ANG",
            "CZK",
            "DKK",
            "DJF",
            "DOP",
            "EGP",
            "SVC",
            "ERN",
            "SZL",
            "ETB",
            "FKP",
            "FJD",
            "XPF",
            "GMD",
            "GEL",
            "GHS",
            "GIP",
            "GTQ",
            "GBP",
            "GNF",
            "GYD",
            "HTG",
            "HNL",
            "HKD",
            "HUF",
            "ISK",
            "IDR",
            "XDR",
            "IRR",
            "IQD",
            "ILS",
            "JMD",
            "JPY",
            "JOD",
            "KZT",
            "KES",
            "KPW",
            "KRW",
            "KWD",
            "KGS",
            "LAK",
            "LBP",
            "LSL",
            "ZAR",
            "LRD",
            "LYD",
            "CHF",
            "MOP",
            "MKD",
            "MGA",
            "MWK",
            "MYR",
            "MVR",
            "MRU",
            "MUR",
            "XUA",
            "MXN",
            "MXV",
            "MDL",
            "MNT",
            "MAD",
            "MZN",
            "MMK",
            "NAD",
            "NPR",
            "NIO",
            "NGN",
            "OMR",
            "PKR",
            "PAB",
            "PGK",
            "PYG",
            "PEN",
            "PHP",
            "PLN",
            "QAR",
            "RON",
            "RUB",
            "RWF",
            "SHP",
            "WST",
            "STN",
            "SAR",
            "RSD",
            "SCR",
            "SLL",
            "SGD",
            "XSU",
            "SBD",
            "SOS",
            "SSP",
            "LKR",
            "SDG",
            "SRD",
            "SEK",
            "CHE",
            "CHW",
            "SYP",
            "TWD",
            "TJS",
            "TZS",
            "THB",
            "TOP",
            "TTD",
            "TND",
            "TRY",
            "TMT",
            "UGX",
            "UAH",
            "AED",
            "USN",
            "UYU",
            "UYI",
            "UYW",
            "UZS",
            "VUV",
            "VES",
            "VND",
            "YER",
            "ZMW",
            "ZWL",
            "XBA",
            "XBB",
            "XBC",
            "XBD",
            "XTS",
            "XXX",
            "XAU",
            "XPD",
            "XPT",
            "XAG"});
            this.comboBox_currency.Location = new System.Drawing.Point(708, 162);
            this.comboBox_currency.Name = "comboBox_currency";
            this.comboBox_currency.Size = new System.Drawing.Size(336, 21);
            this.comboBox_currency.TabIndex = 48;
            // 
            // ContractSeries
            // 
            this.ContractSeries.AutoSize = true;
            this.ContractSeries.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContractSeries.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ContractSeries.Location = new System.Drawing.Point(7, 38);
            this.ContractSeries.Name = "ContractSeries";
            this.ContractSeries.Size = new System.Drawing.Size(196, 29);
            this.ContractSeries.TabIndex = 49;
            this.ContractSeries.Text = "Серия договора:";
            this.ContractSeries.UseVisualStyleBackColor = true;
            this.ContractSeries.CheckedChanged += new System.EventHandler(this.ContractSeries_CheckedChanged);
            // 
            // comboBox_ser
            // 
            this.comboBox_ser.FormattingEnabled = true;
            this.comboBox_ser.Items.AddRange(new object[] {
            "Э",
            "Ц",
            "ЭГ",
            "ИП",
            "Х",
            "ОЦ",
            "ИК",
            "К",
            "ИУ",
            "WS",
            "ИФ",
            "КО",
            ""});
            this.comboBox_ser.Location = new System.Drawing.Point(297, 38);
            this.comboBox_ser.Name = "comboBox_ser";
            this.comboBox_ser.Size = new System.Drawing.Size(747, 21);
            this.comboBox_ser.TabIndex = 50;
            // 
            // INN
            // 
            this.INN.AutoSize = true;
            this.INN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.INN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.INN.Location = new System.Drawing.Point(7, 224);
            this.INN.Name = "INN";
            this.INN.Size = new System.Drawing.Size(82, 29);
            this.INN.TabIndex = 51;
            this.INN.Text = "ИНН:";
            this.INN.UseVisualStyleBackColor = true;
            this.INN.CheckedChanged += new System.EventHandler(this.INN_CheckedChanged);
            // 
            // textBox_INN
            // 
            this.textBox_INN.Location = new System.Drawing.Point(297, 224);
            this.textBox_INN.Name = "textBox_INN";
            this.textBox_INN.Size = new System.Drawing.Size(747, 20);
            this.textBox_INN.TabIndex = 52;
            // 
            // СontractStatus
            // 
            this.СontractStatus.AutoSize = true;
            this.СontractStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.СontractStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.СontractStatus.Location = new System.Drawing.Point(7, 69);
            this.СontractStatus.Name = "СontractStatus";
            this.СontractStatus.Size = new System.Drawing.Size(203, 29);
            this.СontractStatus.TabIndex = 53;
            this.СontractStatus.Text = "Статус договора:";
            this.СontractStatus.UseVisualStyleBackColor = true;
            this.СontractStatus.CheckedChanged += new System.EventHandler(this.СontractStatus_CheckedChanged);
            // 
            // comboBox_status
            // 
            this.comboBox_status.FormattingEnabled = true;
            this.comboBox_status.Items.AddRange(new object[] {
            "действующий документ",
            "недействительный документ",
            "формируется",
            "блокированный документ",
            "нераспознанный документ"});
            this.comboBox_status.Location = new System.Drawing.Point(297, 69);
            this.comboBox_status.Name = "comboBox_status";
            this.comboBox_status.Size = new System.Drawing.Size(747, 21);
            this.comboBox_status.TabIndex = 54;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(47)))));
            this.panel1.Controls.Add(this.PeriodOfImprisonment);
            this.panel1.Controls.Add(this.comboBox_status);
            this.panel1.Controls.Add(this.CustomerName);
            this.panel1.Controls.Add(this.СontractStatus);
            this.panel1.Controls.Add(this.CRP);
            this.panel1.Controls.Add(this.textBox_INN);
            this.panel1.Controls.Add(this.INN);
            this.panel1.Controls.Add(this.dateTimePicker_st);
            this.panel1.Controls.Add(this.comboBox_ser);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ContractSeries);
            this.panel1.Controls.Add(this.dateTimePicker_end);
            this.panel1.Controls.Add(this.comboBox_currency);
            this.panel1.Controls.Add(this.ComboBox_CRP);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.textBox_price);
            this.panel1.Controls.Add(this.textBox_name_cl);
            this.panel1.Controls.Add(this.comboBox_calculus);
            this.panel1.Controls.Add(this.CRP_search);
            this.panel1.Controls.Add(this.Сalculus);
            this.panel1.Controls.Add(this.ContractPrice);
            this.panel1.Location = new System.Drawing.Point(11, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1071, 298);
            this.panel1.TabIndex = 55;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.Ok);
            this.panel2.Location = new System.Drawing.Point(1, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1093, 365);
            this.panel2.TabIndex = 55;
            // 
            // filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1095, 365);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1111, 404);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1111, 404);
            this.Name = "filter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Фильтр";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.filter_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox CustomerName;
        private System.Windows.Forms.CheckBox CRP;
        private System.Windows.Forms.CheckBox PeriodOfImprisonment;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.DateTimePicker dateTimePicker_st;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_end;
        private System.Windows.Forms.ComboBox ComboBox_CRP;
        private System.Windows.Forms.TextBox textBox_name_cl;
        private System.Windows.Forms.Button CRP_search;
        private System.Windows.Forms.CheckBox ContractPrice;
        private System.Windows.Forms.CheckBox Сalculus;
        private System.Windows.Forms.ComboBox comboBox_calculus;
        private System.Windows.Forms.TextBox textBox_price;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox comboBox_currency;
        private System.Windows.Forms.CheckBox ContractSeries;
        private System.Windows.Forms.ComboBox comboBox_ser;
        private System.Windows.Forms.CheckBox INN;
        private System.Windows.Forms.TextBox textBox_INN;
        private System.Windows.Forms.CheckBox СontractStatus;
        private System.Windows.Forms.ComboBox comboBox_status;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}