using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.聘任信息.EmploySqlTableAdapters;

namespace OAManager.聘任信息
{
    public partial class Employinfo : Form
    {
        public Employinfo()
        {
            InitializeComponent();
        }
        DataTable emplotTable;
        private void Employinfo_Load(object sender, EventArgs e)
        {
            emplotTable = new employmentinfoTableAdapter().GetDataBy(AppInfo.worknum);
            if (emplotTable.Rows.Count==0)
            {
                label3.Text = "暂无";
                label4.Text = "暂无";
                label6.Text = "暂无";
                label8.Text = "暂无";
            }
            else
            {
                int getRow = emplotTable.Rows.Count - 1;
                label3.Text = emplotTable.Rows[getRow]["jobtitle"].ToString();
                label4.Text = emplotTable.Rows[getRow]["department"].ToString();
                label6.Text = emplotTable.Rows[getRow]["jointime"].ToString().Split(' ')[0];
                label8.Text = emplotTable.Rows[getRow]["overtime"].ToString().Split(' ')[0];
            }
        }
    }
}
