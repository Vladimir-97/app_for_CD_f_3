using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_for_CD
{
    public static class Data_bill
    {
        #region lab
        public static bool date_from = false;
        public static bool ser_num = false;
        public static bool ser_aggr = false;
        public static bool crp = false;
        public static bool inn = false;
        public static bool pinfl = false;
        public static bool code_nds = false;
        public static bool name = false;
        public static bool serv = false;
        public static bool fio = false;
        public static bool status = false;
        public static bool its_ok = false;
        #endregion
        #region data
        public static string s_date_from = "";
        public static string s_date_to = "";
        public static string s_ser_num = "";
        public static string s_ser_aggr = "";
        public static string s_crp = "";
        public static string s_inn = "";
        public static string s_pinfl = "";
        public static string s_code_nds = "";
        public static string s_name = "";
        public static string s_serv = "";
        public static string s_fio = "";
        public static string s_status = "";
        public static string s_its_ok = "";
        #endregion
        public static void clear()
        {
            date_from = false;
            ser_num = false;
            ser_aggr = false;
            crp = false;
            inn = false;
            pinfl = false;
            code_nds = false;
            name = false;
            serv = false;
            fio = false;
            status = false;
            its_ok = false ;

            s_date_from = "";
            s_date_to = "";
            s_ser_num = "";
            s_ser_aggr = "";
            s_crp = "";
            s_inn = "";
            s_pinfl = "";
            s_code_nds = "";
            s_name = "";
            s_serv = "";
            s_fio = "";
            s_status = "";
            s_its_ok = "";
    }
    }
}
