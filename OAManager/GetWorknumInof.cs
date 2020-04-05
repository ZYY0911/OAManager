using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAManager.MyInfoSqlTableAdapters;
using OAManager.MyInfoAndEmpTableAdapters;

namespace OAManager
{
    class GetWorknumInof
    {

        public static string getWorknumName(string worknum)
        {
            DataTable data = new myinfoTableAdapter().GetDataBy1(worknum);
            return data.Rows.Count > 0 ? data.Rows[0]["name"].ToString() : "";
        }


        public static string getWorknumJobtitle(string worknum)
        {
            DataTable data = new employmentinfoTableAdapter().GetDataBy(worknum);
            return data.Rows.Count > 0 ? data.Rows[data.Rows.Count-1]["jobtitle"].ToString() : "";
        }

        public static string getWorknumDeparment(string worknum)
        {
            DataTable data = new employmentinfoTableAdapter().GetDataBy(worknum);
            return data.Rows.Count > 0 ? data.Rows[data.Rows.Count - 1]["department"].ToString() : "";
        }
    }
}
