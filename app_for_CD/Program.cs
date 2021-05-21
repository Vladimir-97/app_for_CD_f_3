using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app_for_CD
{
    static class Data
    {
        public static bool it_ok = false;
        #region Save
        public static bool yes = false;
        #endregion
        #region value for return filter
        public static string st_date_orig { get; set; }
            public static string end_date_orig { get; set; }
            public static string number_ser = "";
            public static string name_cl = "";
            public static string price = "";
            public static string val = "";
            public static string isch = "";
            public static string INN = "";
            public static string ser = "";
            public static string crp_str1 = "";
            public static string crp_str2 = "";
            public static string client_str1 = "";
            public static string client_str2 = "";
            public static string emitent_str = "";
            public static string code_stock_str = "";
            public static string name_stock_str = "";
            public static string filter_fio = "";
        public static int status = 0;
            
        #endregion
        #region flag for checkBox
        public static bool f_n { get; set; }
            public static bool f_CRP { get; set; }
            public static bool f_d { get; set; }
            public static bool f_p { get; set; }
            public static bool f_i { get; set; }
            public static bool f_inn { get; set; }
            public static bool f_ser { get; set; }
            public static bool f_status { get; set; }
            public static bool f_fio { get; set; }
        #endregion
        public static int login = 0;
        public static int role = 0;
        public static bool exit = false;
        public static int status_t = 0;
        public static string get_fio = "";
        public static int was_count = 0;

        #region flag_for_filter_of_stocks
        public static bool fil_date { get; set; }
        public static bool fil_crp1 { get; set; }
        public static bool fil_crp2 { get; set; }
        public static bool fil_client1 { get; set; }
        public static bool fil_client2 { get; set; }
        public static bool fil_emitent { get; set; }
        public static bool fil_code_stocks { get; set; }
        public static bool fil_name_stocks { get; set; }
        #endregion


    }
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Auth());
            Application.Exit();
            if (Data.exit == true)
                Application.Run(new TC_Menu());

        }
    }

}
