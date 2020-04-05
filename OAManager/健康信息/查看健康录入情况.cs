using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.MyInfoSqlTableAdapters;
using OAManager.健康信息.HealthySqlTableAdapters;
using OAManager.MyInfoAndEmpTableAdapters;


namespace OAManager.健康信息
{
    public partial class 查看健康录入情况 : Form
    {
        public 查看健康录入情况()
        {
            InitializeComponent();
        }
        DataTable data;
        string myDep;
        DataTable depTable;
        DataTable dataTable1, dataTable2;
        Dictionary<string, List<string>> yg = new Dictionary<string, List<string>>();
        private void 查看健康录入情况_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataTable1 = new DataTable();
            DataColumn column1 = new DataColumn("工号");
            DataColumn column2 = new DataColumn("姓名");
            DataColumn column3 = new DataColumn("身份证");
            DataColumn column4 = new DataColumn("性别");
            dataTable1.Columns.Add(column1);
            dataTable1.Columns.Add(column2);
            dataTable1.Columns.Add(column3);
            dataTable1.Columns.Add(column4);
            dataTable2 = dataTable1.Clone();
            yg["录入"] = new List<string>();
            yg["未录入"] = new List<string>();
            label2.Text = DateTime.Now.ToString("yyyy-MM-dd");
            data = new MyInfoAndEmpTableAdapters.DataTable1TableAdapter().GetDataBy(AppInfo.worknum);
            myDep = data.Rows[data.Rows.Count-1]["department"].ToString();
            //本部门所有员工
            depTable = new MyInfoAndEmpTableAdapters.DataTable1TableAdapter().GetDataBy1(myDep);
            for (int i = 0; i < depTable.Rows.Count; i++)
            {
                string worknum = depTable.Rows[i]["worknum"].ToString();
                DataTable heathy = new healthyinfoTableAdapter().GetDataBy1(worknum
                    , DateTime.Now.ToString());
                if (heathy.Rows.Count==0)
                {
                    List<string> vs = yg["未录入"];
                    vs.Add(worknum);
                    yg["未录入"] = vs;
                }
                else
                {
                    List<string> vs = yg["录入"];
                    vs.Add(worknum);
                    yg["录入"] = vs;
                }
            }
            initView(yg["录入"],dataTable1,dataGridView1);
            initView(yg["未录入"], dataTable2, dataGridView2);
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.SeaGreen;
            }
            else
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Khaki;
            }
        }

        private void dataGridView2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.SeaGreen;
            }
            else
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Khaki;
            }
        }
      
        private void initView(List<string>list,DataTable table,DataGridView dataGridView)
        {
            DataRow row;
            for (int i = 0; i < list.Count; i++)
            {
                row = table.NewRow();
                DataTable myTable = new myinfoTableAdapter().GetDataBy1(list[i]);
                row[0] = myTable.Rows[0]["worknum"].ToString();
                row[1] = myTable.Rows[0]["name"].ToString();
                row[2] = myTable.Rows[0]["sid"].ToString();
                row[3] = myTable.Rows[0]["sex"].ToString();
                table.Rows.Add(row);
            }
            dataGridView.DataSource = table;
        }
    }
}
