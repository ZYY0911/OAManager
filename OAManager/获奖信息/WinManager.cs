using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.获奖信息.WinSqlTableAdapters;

namespace OAManager.获奖信息
{
    public partial class WinManager : Form
    {
        public WinManager()
        {
            InitializeComponent();
        }
        DataTable table;
        DataRow row;
        private void WinManager_Load(object sender, EventArgs e)
        {

            dataGridView1.AllowUserToAddRows = false;
            table = new DataTable();
            DataColumn column10 = new DataColumn("类别");
            DataColumn column1 = new DataColumn("名称");
            DataColumn column2 = new DataColumn("级别");
            DataColumn column3 = new DataColumn("等次");
            DataColumn column4 = new DataColumn("获奖时间");
            DataColumn column5 = new DataColumn("本人排名");
            DataColumn column6 = new DataColumn("证书图片");
            DataColumn column7 = new DataColumn("录入人");
            DataColumn column8 = new DataColumn("审核人");
            DataColumn column9 = new DataColumn("审核状态");
            table.Columns.Add(column10);
            table.Columns.Add(column1);
            table.Columns.Add(column2);
            table.Columns.Add(column3);
            table.Columns.Add(column4);
            table.Columns.Add(column5);
            table.Columns.Add(column6);
            table.Columns.Add(column7);
            table.Columns.Add(column8);
            table.Columns.Add(column9);
            initView(new winninginfoTableAdapter().GetDataBy(AppInfo.worknum));
        }

        private void initView(DataTable dataTable)
        {
            table.Rows.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = table.NewRow();
                row[0] = dataTable.Rows[i]["pricetype"].ToString();
                row[1] = dataTable.Rows[i]["pricename"].ToString();
                row[2] = dataTable.Rows[i]["level"].ToString();
                row[3] = dataTable.Rows[i]["order"].ToString();
                row[4] = dataTable.Rows[i]["date"].ToString();
                row[5] = dataTable.Rows[i]["rank"].ToString();
                row[6] = dataTable.Rows[i]["photo"].ToString();
                row[7] = dataTable.Rows[i]["inputperson"].ToString();
                row[8] = dataTable.Rows[i]["reviewer"].ToString();
                string reviewstate = dataTable.Rows[i]["reviewstate"].ToString();
                switch (reviewstate)
                {
                    case "1":
                        row[9] = "未审核";
                        break;
                    case "2":
                        row[9] = "拒绝";
                        break;
                    case "3":
                        row[9] = "通过";
                        break;
                    default:
                        break;
                }
                table.Rows.Add(row);
            }
            dataGridView1.DataSource = table;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "Images";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.Columns["证书图片"].Visible = false;
            dataGridView1.Columns.Insert(dataGridView1.Columns["证书图片"].Index, imageColumn);
            dataGridView1.Columns["Images"].Width = 150;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells["Images"].Value = Image.FromFile(dataGridView1.Rows[i].Cells["证书图片"].Value.ToString());
                dataGridView1.Rows[i].Height = 150;
            }
        }
        bool isFirst = true;


        private void button1_Click(object sender, EventArgs e)
        {
            new AddMyWin(UpDate).Show();
        }

     

        private void UpDate()
        {

            initView(new winninginfoTableAdapter().GetDataBy(AppInfo.worknum));
        }

        private void WinManager_Activated(object sender, EventArgs e)
        {

        }

        private void WinManager_Shown(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int i = e.RowIndex;
           
            if (i % 2 == 0)
            {
                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.SeaGreen;
            }
            else
            {
                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
            }

        }
    }
}
