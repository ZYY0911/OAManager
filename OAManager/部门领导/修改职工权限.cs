using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.UserLogSqlTableAdapters;

namespace OAManager.部门领导
{
    public partial class 修改职工权限 : Form
    {
        public 修改职工权限()
        {
            InitializeComponent();
        }

        private void 修改职工权限_Load(object sender, EventArgs e)
        {
            loadGZ();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            loadHe();
        }
        DataTable tableHe;
        private void loadHe()
        {
            tableHe = tablegz.Clone();
            initView2(new usermanagerTableAdapter().GetDataBy1());
        }

        private void initView2(DataTable dataTable)
        {
            tableHe.Rows.Clear();
            dataGridView2.DataSource = null;
            dataGridView2.Columns.Clear();
            DataRow row;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = tableHe.NewRow();
                string worknum = dataTable.Rows[i]["worknum"].ToString();
                row[0] = GetWorknumInof.getWorknumName(worknum);
                row[1] = worknum;
                row[2] = GetTypeHeal(dataTable.Rows[i]["type"].ToString());
                tableHe.Rows.Add(row);
            }
            dataGridView2.DataSource = tableHe;
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "修改";
            dataGridView2.Columns.Add(buttonColumn);
        }

        DataTable tablegz;
        private void loadGZ()
        {
            tablegz = new DataTable();
            DataColumn column1 = new DataColumn("姓名");
            DataColumn column2 = new DataColumn("工号");
            DataColumn column3 = new DataColumn("是否具有权限");
            tablegz.Columns.Add(column1);
            tablegz.Columns.Add(column2);
            tablegz.Columns.Add(column3);
            initView(new usermanagerTableAdapter().GetDataBy1());
        }

        private void initView(DataTable dataTable)
        {
            tablegz.Rows.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            DataRow row;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = tablegz.NewRow();
                string worknum = dataTable.Rows[i]["worknum"].ToString();
                row[0] = GetWorknumInof.getWorknumName(worknum);
                row[1] = worknum;
                row[2] = GetTypeWage(dataTable.Rows[i]["type"].ToString());
                tablegz.Rows.Add(row);
            }
            dataGridView1.DataSource = tablegz;
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "修改";
            dataGridView1.Columns.Add(buttonColumn);
        }

        private string GetTypeHeal(string type)
        {
            switch (type)
            {
                case "6":
                    return "是";
              
                default:
                    return "否";
            }
        }
        private string GetTypeWage(string type)
        {
            switch (type)
            {
                case "5":
                    return "是";

                default:
                    return "否";
            }
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
            dataGridView1.Rows[e.RowIndex].Cells["修改"].Value = "修改";
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
            dataGridView2.Rows[e.RowIndex].Cells["修改"].Value = "修改";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0)
            {
                if (e.ColumnIndex==dataGridView1.Columns["修改"].Index)
                {
                    string worknum = dataGridView1.Rows[e.RowIndex].Cells["工号"].Value.ToString();
                    if (dataGridView1.Rows[e.RowIndex].Cells["是否具有权限"].Value.ToString()=="是")
                    {
                        if (MessageBox.Show("您确定修改当前用户，不允许查看工资权限吗", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            int row = new usermanagerTableAdapter().UpdateQuery(4, worknum);
                            if (row==1)
                            {
                                MessageBox.Show("修改成功");
                                initView(new usermanagerTableAdapter().GetDataBy1());

                            }
                            else
                            {
                                MessageBox.Show("修改失败");
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("您确定修改当前用户，允许查看工资权限吗", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            int row = new usermanagerTableAdapter().UpdateQuery(5, worknum);
                            if (row == 1)
                            {
                                MessageBox.Show("修改成功");
                                initView(new usermanagerTableAdapter().GetDataBy1());

                            }
                            else
                            {
                                MessageBox.Show("修改失败");
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridView2.Columns["修改"].Index)
                {
                    string worknum = dataGridView2.Rows[e.RowIndex].Cells["工号"].Value.ToString();
                    if (dataGridView2.Rows[e.RowIndex].Cells["是否具有权限"].Value.ToString() == "是")
                    {
                        if (MessageBox.Show("您确定修改当前用户，不允许查看健康统计权限吗", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            int row = new usermanagerTableAdapter().UpdateQuery(4, worknum);
                            if (row == 1)
                            {
                                MessageBox.Show("修改成功");
                                initView2(new usermanagerTableAdapter().GetDataBy1());

                            }
                            else
                            {
                                MessageBox.Show("修改失败");
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("您确定修改当前用户，允许查看健康统计权限吗", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            int row = new usermanagerTableAdapter().UpdateQuery(6, worknum);
                            if (row == 1)
                            {
                                MessageBox.Show("修改成功");
                                initView2(new usermanagerTableAdapter().GetDataBy1());

                            }
                            else
                            {
                                MessageBox.Show("修改失败");
                            }
                        }
                    }
                }
            }
        }
    }
}
