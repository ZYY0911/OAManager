using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.健康信息.HealthySqlTableAdapters;

namespace OAManager.健康信息
{
    public partial class HealthyinfoManager : Form
    {
        public HealthyinfoManager()
        {
            InitializeComponent();
        }
        DataTable table;
        DataRow row;
        private void HealthyinfoManager_Load(object sender, EventArgs e)
        {
            if (AppInfo.type==4||AppInfo.type==5)
            {
                button1.Visible = false;
                button2.Visible = false;
            }
            table = new DataTable();
            DataColumn column1 = new DataColumn("日期");
            DataColumn column2 = new DataColumn("体温");
            DataColumn column3 = new DataColumn("有无不适");
            DataColumn column4 = new DataColumn("不适描述");
            DataColumn column5 = new DataColumn("录入人");
            table.Columns.Add(column1);
            table.Columns.Add(column2);
            table.Columns.Add(column3);
            table.Columns.Add(column4);
            table.Columns.Add(column5);
            initView(new healthyinfoTableAdapter().GetDataBy(AppInfo.worknum,DateTime.Now.AddDays(-30).ToString()));

        }

        private void initView(DataTable dataTable)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = table.NewRow();
                row[0] = dataTable.Rows[i]["date"].ToString().Split(' ')[0];
                row[1] = dataTable.Rows[i]["bodytemperature"].ToString();
                Console.WriteLine(dataTable.Rows[i]["sure"].ToString() + "0");
                row[2] = dataTable.Rows[i]["sure"].ToString()=="0"?"无":"有";
                row[3] = dataTable.Rows[i]["description"].ToString()==""?"无": dataTable.Rows[i]["description"].ToString();
                row[4] = dataTable.Rows[i]["inputperson"].ToString();
                table.Rows.Add(row);
            }
            dataGridView1.DataSource = table;
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex%2==0)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.SeaGreen;
            }
            else
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Khaki;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new 查看健康录入情况().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new 健康统计报表().Show();
        }
    }
}
