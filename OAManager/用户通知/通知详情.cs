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
    public partial class 通知详情 : Form
    {
        string noytype;
        int allRow, indexRow, showRow = 10, indesPager, allPager;
        public 通知详情(string noytype)
        {
            this.noytype = noytype;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (indesPager > allPager)
            {
                MessageBox.Show("已经是最后一页");
                return;
            }
            indesPager = allPager;
            indexRow = (indesPager - 1) * showRow;
            initView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            indesPager++;
            if (indesPager > allPager)
            {
                MessageBox.Show("已经是最后一页");
                indesPager = allPager;
                return;
            }
            indexRow = indexRow + showRow;
            initView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            indesPager--;
            if (indesPager == 0)
            {
                MessageBox.Show("已经是第一一页");
                indesPager = 1;
                return;
            }
            indexRow = indexRow - showRow;
            initView();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (indesPager == 1)
            {
                MessageBox.Show("已经是第一一页");
                return;
            }
            indesPager = 1;
            indexRow = 0;
            initView();
        }

        DataTable table, allTable;
        private void 通知详情_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            label2.Text = noytype;
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
            allTable = new newsTableAdapter().GetDataBy(noytype);
            setstart();
        }
        private void setstart()
        {
            allRow = allTable.Rows.Count;
            allPager = allRow / allRow;
            if (allRow % allRow > 0)
            {
                allPager++;
            }
            indesPager = 1;
            initView();
        }

        private void initView()
        {
            label9.Text = "当前" + indesPager + "/共" + allPager;
            dataGridView1.DataSource = null;
            table.Rows.Clear();
            DataRow row;
            int j = indexRow + showRow;
            if (j > allRow)
            {
                j = allRow;
            }
            for (int i = indexRow; i < j; i++)
            {
                row = table.NewRow();
                row[0] = allTable.Rows[i]["title"].ToString();
                row[1] = allTable.Rows[i]["context"].ToString();
                row[2] = allTable.Rows[i]["photo"].ToString();
                row[3] = allTable.Rows[i]["releasedepartment"].ToString();
                row[4] = allTable.Rows[i]["publisher"].ToString();
                row[5] = allTable.Rows[i]["date"].ToString();
                table.Rows.Add(row);
            }
            dataGridView1.DataSource = table;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "照片";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.Columns.Insert(dataGridView1.Columns["path"].Index, imageColumn);
            dataGridView1.Columns["path"].Visible = false;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells["照片"].Value = Image.FromFile(dataGridView1.Rows[i].Cells["path"].Value.ToString());
                dataGridView1.Rows[i].Height = 150;
            }
        }

    }
}
