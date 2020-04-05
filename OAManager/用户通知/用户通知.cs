using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.NewsSqlTableAdapters;

namespace OAManager.用户通知
{
    public partial class 用户通知 : Form
    {
        public 用户通知()
        {
            InitializeComponent();
        }
        DataTable table;
        private void 用户通知_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            table = new DataTable();
            DataColumn column1 = new DataColumn("标题");
            DataColumn column2 = new DataColumn("内容");
            DataColumn column3 = new DataColumn("path");
            DataColumn column4 = new DataColumn("发布部门");
            DataColumn column5 = new DataColumn("发布人");
            DataColumn column6 = new DataColumn("时间");
            table.Columns.Add(column1);
            table.Columns.Add(column2);
            table.Columns.Add(column3);
            table.Columns.Add(column4);
            table.Columns.Add(column5);
            table.Columns.Add(column6);
            initView(new newsTableAdapter().GetDataBy("通知公告"));
        }

        private void initView(DataTable dataTable)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            table.Rows.Clear();
            DataRow row;
            int rowcount = 10;
            if (dataTable.Rows.Count<10)
            {
                rowcount = dataTable.Rows.Count;
            }
            for (int i = 0; i < rowcount; i++)
            {
                row = table.NewRow();
                row[0] = dataTable.Rows[i]["title"].ToString();
                row[1] = dataTable.Rows[i]["context"].ToString();
                row[2] = dataTable.Rows[i]["photo"].ToString();
                row[3] = dataTable.Rows[i]["releasedepartment"].ToString();
                row[4] = dataTable.Rows[i]["publisher"].ToString();
                row[5] = dataTable.Rows[i]["date"].ToString();
                table.Rows.Add(row);
                Console.WriteLine(dataTable.Rows[i]["photo"].ToString());
            }
            dataGridView1.DataSource = table;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
            imageColumn.Name = "照片";
            dataGridView1.Columns.Insert(dataGridView1.Columns["path"].Index, imageColumn);
            dataGridView1.Columns["path"].Visible = false;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells["照片"].Value = Image.FromFile(dataGridView1.Rows[i].Cells["path"].Value.ToString());
                dataGridView1.Rows[i].Height = 150;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new 通知详情(comboBox1.Text).Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            initView(new newsTableAdapter().GetDataBy(comboBox1.Text));
        }
        /// <summary>
        /// 添加通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            new 添加通知().Show();
        }
    }
}
