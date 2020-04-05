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

namespace OAManager.部门领导
{
    public partial class 信息审核 : Form
    {
        public 信息审核()
        {
            InitializeComponent();
        }
        DataTable table;
        private void 信息审核_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            DataColumn column1 = new DataColumn("标题");
            DataColumn column2 = new DataColumn("内容");
            DataColumn column3 = new DataColumn("发布部门");
            DataColumn column4 = new DataColumn("发布人");
            DataColumn column5 = new DataColumn("审核状态");
            DataColumn column6 = new DataColumn("时间");
            DataColumn column7 = new DataColumn("编号");
            table.Columns.Add(column1);
            table.Columns.Add(column2);
            table.Columns.Add(column3);
            table.Columns.Add(column4);
            table.Columns.Add(column5);
            table.Columns.Add(column6);
            table.Columns.Add(column7);
            initView(new newsTableAdapter().GetDataBy3());
        }

        private void initView(DataTable dataTable)
        {
            table.Rows.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            DataRow row;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = table.NewRow();
                row[0] = dataTable.Rows[i]["title"].ToString();
                row[1] = dataTable.Rows[i]["context"].ToString();
                row[2] = dataTable.Rows[i]["releasedepartment"].ToString();
                row[3] = dataTable.Rows[i]["publisher"].ToString();
                string reviewstate = dataTable.Rows[i]["reviewstate"].ToString();
                switch (reviewstate)
                {
                    case "1":
                        row[4] = "未审核";
                        break;
                    case "2":
                        continue;
                    case "3":
                        continue;
                    default:
                        continue;
                }
                row[5] = dataTable.Rows[i]["date"].ToString().Split(' ')[0];
                row[6] = dataTable.Rows[i]["notifinumber"].ToString().Split(' ')[0];
                table.Rows.Add(row);
            }
            dataGridView1.DataSource = table;
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "审核";
            dataGridView1.Columns.Add(buttonColumn);
            dataGridView1.Columns["编号"].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridView1.Columns["审核"].Index)
                {
                    if (new 查看审核(dataGridView1.Rows[e.RowIndex].Cells["编号"].Value.ToString()).ShowDialog() == DialogResult.OK)
                    {
                        initView(new newsTableAdapter().GetDataBy3());
                    }
                }
            }
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
            dataGridView1.Rows[i].Cells["审核"].Value = "审核";

        }
    }
}
