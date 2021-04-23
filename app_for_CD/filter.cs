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


    public partial class filter : Form
    {
        OracleConnection con = null;
        public filter()
        {
            InitializeComponent();
            this.SetConnection();
            ComboBox_CRP.MaxLength = 12;
        }
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


        #region CheckedChanged
        // Период заключения
        private void PeriodOfImprisonment_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.f_d = true;
            }
            else
            {
                Data.f_d = false;
            }
        }
        // КЗЛ
        private void CRP_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.f_CRP = true;
            }
            else
            {
                Data.f_CRP = false;
            }
        }
        // Наименование клиента
        private void CustomerName_CheckedChanged(object sender, EventArgs e)

        {

            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {

                Data.f_n = true;
            }
            else
            {
                Data.f_n = false;
            }
        }
        // Цена договора 
        private void ContractPrice_CheckedChanged(object sender, EventArgs e)

        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {

                Data.f_p = true;
            }
            else
            {
                Data.f_p = false;
            }
        }
        // Исчисление
        private void Сalculus_CheckedChanged(object sender, EventArgs e)

        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {

                Data.f_i = true;
            }
            else
            {
                Data.f_i = false;
            }
        }
        // ИНН
        private void INN_CheckedChanged(object sender, EventArgs e)

        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {

                Data.f_inn = true;
            }
            else
            {
                Data.f_inn = false;
            }
        }
        //Серия договора
        private void ContractSeries_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.f_ser = true;
            }
            else
            {
                Data.f_ser = false;
            }
        }
        //Статус договора
        private void СontractStatus_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.f_status = true;
            }
            else
            {
                Data.f_status = false;
            }
        }
        #endregion

        private void CRP_search_Click(object sender, EventArgs e)

        {
            this.SetConnection();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CRP_CD FROM TBCB_CRP_INFO where rownum <=1000";


            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            // List<string[]> data = new List<string[]>();
            while (dr.Read())
            {
                ComboBox_CRP.Items.Add(dr[0].ToString());
            }
        }

        int check_stat(string tmp_str)
        {
            if (tmp_str == "действующий документ")
            {
                return 1;
            }
            if (tmp_str == "недействительный документ")
            {
                return 2;
            }
            if (tmp_str == "формируется")
            {
                return 3;
            }
            if (tmp_str == "блокированный документ")
            {
                return 4;
            }
            if (tmp_str == "нераспознанный документ")
            {
                return 5;
            }
            return 0;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            Data.it_ok = true;
            // for create new
            if (Data.f_d == true)
            {
                DateTime thisDate_st = dateTimePicker_st.Value;
                DateTime thisDate_end = dateTimePicker_end.Value;
                Data.st_date_orig = thisDate_st.ToString("yyyyMMdd").ToString();
                Data.end_date_orig = thisDate_end.ToString("yyyyMMdd").ToString();
            }
            if (Data.f_CRP == true)
            {
                Data.number_ser = ComboBox_CRP.Text.ToString();
            }
            if (Data.f_n == true)
            {
                Data.name_cl = textBox_name_cl.Text;
            }
            if (Data.f_p == true)
            {
                Data.price = textBox_price.Text;
                Data.val = comboBox_currency.Text.ToString();
            }
            if (Data.f_i == true)
            {
                Data.isch = comboBox_calculus.Text.ToString();
            }
            if (Data.f_inn == true)
            {
                Data.INN = textBox_INN.Text;
            }
            if (Data.f_ser == true)
            {
                Data.ser = comboBox_ser.Text;
            }
            if (Data.f_status == true)
            {
                Data.status = check_stat(comboBox_status.Text);

            }
            this.Close();
        }

        private void CloseConnection()
        {
            con.Close();
        }

        private void filter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Data.it_ok != true)
            {


                Data.f_d = false;
                Data.f_CRP = false;
                Data.f_n = false;
                Data.f_p = false;
                Data.f_i = false;
                Data.f_inn = false;
                Data.f_ser = false;
                Data.f_status = false;
                CloseConnection();
            }
            if ((Data.it_ok == true) && ((Data.name_cl == "" && Data.f_n == true) || (Data.number_ser == "" && Data.f_CRP == true) || (Data.price == "" && Data.name_cl == "" && Data.f_p == true) || (Data.isch == "" && Data.f_i == true) || (Data.INN == "" && Data.f_inn == true) || (Data.ser == "" && Data.f_ser == true) || (Data.status == 0 && Data.f_status == true)))

            {
                e.Cancel = true;
                Data.it_ok = false;
                MessageBox.Show("Заполните все строки!");
            }
            
            
        }
    }
}
