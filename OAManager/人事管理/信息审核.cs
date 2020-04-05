using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.WinninginfoSqlTableAdapters;
using OAManager.TraininginfoSqlTableAdapters;

namespace OAManager.人事管理
{
    public partial class 信息审核 : Form
    {
        public 信息审核()
        {
            InitializeComponent();
        }

        DataTable table,table2;
        private void 信息审核_Load(object sender, EventArgs e)
        {
            loadView1();
            loadView2();
        }

        private void loadView2()
        {
            table2 = new DataTable();
            DataColumn column8 = new DataColumn("工号");
            DataColumn column1 = new DataColumn("项目名称");
            DataColumn column2 = new DataColumn("工作单位");
            DataColumn column3 = new DataColumn("学时");
            DataColumn column4 = new DataColumn("开始时间");
            DataColumn column5 = new DataColumn("结束时间");
            DataColumn column6 = new DataColumn("录入人");
            DataColumn column7 = new DataColumn("id");
            table2.Columns.Add(column8);
            table2.Columns.Add(column1);
            table2.Columns.Add(column2);
            table2.Columns.Add(column3);
            table2.Columns.Add(column4);
            table2.Columns.Add(column5);
            table2.Columns.Add(column6);
            table2.Columns.Add(column7);
            initView2(new traininginfoTableAdapter().GetDataBy(1));
        }

        private void initView2(DataTable dataTable)
        {
            table2.Rows.Clear();
            DataRow row;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = table2.NewRow();
                row[0] = dataTable.Rows[i]["worknum"].ToString();
                row[1] = dataTable.Rows[i]["projectname"].ToString();
                row[2] = dataTable.Rows[i]["workunit"].ToString();
                row[3] = dataTable.Rows[i]["studytime"].ToString();
                row[4] = dataTable.Rows[i]["starttime"].ToString().Split(' ')[0];
                row[5] = dataTable.Rows[i]["overtime"].ToString().Split(' ')[0];
                row[6] = dataTable.Rows[i]["inputperson"].ToString();
                row[7] = dataTable.Rows[i]["id"].ToString();
                table2.Rows.Add(row);
            }
            dataGridView2.Columns.Clear();
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = table2;
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "审核";
            dataGridView2.Columns.Add(buttonColumn);
            dataGridView2.Columns["id"].Visible = false; ;
        }

        private void loadView1()
        {
            table = new DataTable();
            DataColumn column1 = new DataColumn("工号");
            DataColumn column2 = new DataColumn("奖项类型");
            DataColumn column3 = new DataColumn("奖项名称");
            DataColumn column4 = new DataColumn("级别");
            DataColumn column5 = new DataColumn("等次");
            DataColumn column6 = new DataColumn("获奖时间");
            DataColumn column7 = new DataColumn("本人排名");
            DataColumn column8 = new DataColumn("录入人");
            DataColumn column9 = new DataColumn("id");
            table.Columns.Add(column1);
            table.Columns.Add(column2);
            table.Columns.Add(column3);
            table.Columns.Add(column4);
            table.Columns.Add(column5);
            table.Columns.Add(column6);
            table.Columns.Add(column7);
            table.Columns.Add(column8);
            table.Columns.Add(column9);
            initView(new winninginfoTableAdapter().GetDataBy(1));
        }

        private void initView(DataTable dataTable)
        {
            table.Rows.Clear();
            DataRow row;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = table.NewRow();
                row[0] = dataTable.Rows[i]["worknum"].ToString();
                row[1] = dataTable.Rows[i]["pricetype"].ToString();
                row[2] = dataTable.Rows[i]["pricename"].ToString();
                row[3] = dataTable.Rows[i]["level"].ToString();
                row[4] = dataTable.Rows[i]["order"].ToString();
                row[5] = dataTable.Rows[i]["date"].ToString().Split(' ')[0];
                row[6] = dataTable.Rows[i]["rank"].ToString();
                row[7] = dataTable.Rows[i]["inputperson"].ToString();
                row[8] = dataTable.Rows[i]["id"].ToString();
                table.Rows.Add(row);
            }
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = table;
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "审核";
            dataGridView1.Columns.Add(buttonColumn);
            dataGridView1.Columns["id"].Visible = false;
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

        private void dataGridView2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int i = e.RowIndex;
            if (i % 2 == 0)
            {
                dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.SeaGreen;
            }
            else
            {
                dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
            }
            dataGridView2.Rows[i].Cells["审核"].Value = "审核";
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridView2.Columns["审核"].Index)
                {
                    if (new 审核信息cs(2, table2.Rows[e.RowIndex]["id"].ToString()).ShowDialog() == DialogResult.OK)
                    {
                        initView2(new traininginfoTableAdapter().GetDataBy(1));
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridView1.Columns["审核"].Index)
                {
                    if (new 审核信息cs(1,table.Rows[e.RowIndex]["id"].ToString()).ShowDialog() == DialogResult.OK)
                    {
                        initView(new winninginfoTableAdapter().GetDataBy(1));
                    }
                }
            }
        }
    }
}
