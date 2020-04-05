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

namespace OAManager.人事管理
{
    public partial class 人事管理 : Form
    {
        public 人事管理()
        {
            InitializeComponent();
        }

        private void 人事管理_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'baseSql.education' table. You can move, or remove it, as needed.
            this.educationTableAdapter.Fill(this.baseSql.education);
            // TODO: This line of code loads data into the 'baseSql.political' table. You can move, or remove it, as needed.
            this.politicalTableAdapter.Fill(this.baseSql.political);
            loadRylg();
        }
        DataTable ryTable, AllRyTable;
        int allPager, allRow, indexPager, indexRow, showRow = 10;
        private void loadRylg()
        {
            ryTable = new DataTable();
            DataColumn column1 = new DataColumn("工号");
            DataColumn column2 = new DataColumn("姓名");
            DataColumn column3 = new DataColumn("身份证号");
            DataColumn column12 = new DataColumn("生日");
            DataColumn column4 = new DataColumn("参加工作时间");
            DataColumn column5 = new DataColumn("性别");
            DataColumn column6 = new DataColumn("民族");
            DataColumn column7 = new DataColumn("出生地");
            DataColumn column8 = new DataColumn("婚否");
            DataColumn column9 = new DataColumn("政治面貌");
            DataColumn column10 = new DataColumn("国籍");
            DataColumn column11 = new DataColumn("学历");
            ryTable.Columns.Add(column1);
            ryTable.Columns.Add(column2);
            ryTable.Columns.Add(column3);
            ryTable.Columns.Add(column12);
            ryTable.Columns.Add(column4);
            ryTable.Columns.Add(column5);
            ryTable.Columns.Add(column6);
            ryTable.Columns.Add(column7);
            ryTable.Columns.Add(column8);
            ryTable.Columns.Add(column9);
            ryTable.Columns.Add(column10);
            ryTable.Columns.Add(column11);

            SetPager(new myinfoTableAdapter().GetData());

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (indexPager == 1)
            {
                MessageBox.Show("已经是第一页");
                return;
            }
            indexPager = 1;
            indexRow = 0;
            initViewRy();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            indexPager--;
            if (indexPager == 0)
            {
                MessageBox.Show("已经是第一页");
                indexPager = 1;
                return;
            }
            indexRow = indexRow - showRow;
            initViewRy();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            indexPager++;
            if (indexPager > allPager)
            {
                MessageBox.Show("已经是最后一页");
                indexPager = allPager;
                return;
            }
            indexRow = indexRow + showRow;
            initViewRy();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (indexPager == allPager)
            {
                MessageBox.Show("已经是最后一页");
                indexPager = allPager;
            }
            indexPager = allPager;
            indexRow = (indexPager - 1) * showRow;
            initViewRy();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string worknum = dataGridView1.Rows[e.RowIndex].Cells["工号"].Value.ToString();
                if (e.ColumnIndex == dataGridView1.Columns["删除"].Index)
                {
                    new 删除判断(worknum,UpDate).Show();
                }
                if (e.ColumnIndex == dataGridView1.Columns["修改"].Index)
                {
                    if(new 信息修改(worknum).ShowDialog() == DialogResult.OK)
                    {
                        UpDate();
                    }
                }
            }
        }

        void UpDate()
        {
            SetPager(new myinfoTableAdapter().GetData());
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0)
            {
                string worknum = dataGridView1.Rows[e.RowIndex].Cells["工号"].Value.ToString();
                new 用户详细信息(worknum).ShowDialog();
            }
        }

        private void SetPager(DataTable data)
        {
            AllRyTable = data;
            allRow = AllRyTable.Rows.Count;
            indexPager = 1;
            allPager = allRow / showRow;
            if (allRow % showRow > 0)
            {
                allPager++;
            }
            initViewRy();

        }

        private void initViewRy()
        {
            label12.Text = "当前" + indexPager + "/共" + allPager + "页";
            DataRow row;
            ryTable.Rows.Clear();
            int nowTorw = indexRow + showRow;
            if (nowTorw > allRow)
            {
                nowTorw = allRow;
            }
            for (int i = indexRow; i < nowTorw; i++)
            {
                row = ryTable.NewRow();
                row[0] = AllRyTable.Rows[i]["worknum"].ToString();
                row[1] = AllRyTable.Rows[i]["name"].ToString();
                row[2] = AllRyTable.Rows[i]["sid"].ToString();
                row[3] = AllRyTable.Rows[i]["birth"].ToString().Split(' ')[0];
                row[4] = AllRyTable.Rows[i]["joinwork"].ToString();
                row[5] = AllRyTable.Rows[i]["sex"].ToString();
                row[6] = AllRyTable.Rows[i]["nation"].ToString();
                row[7] = AllRyTable.Rows[i]["birthlocation"].ToString();
                row[8] = AllRyTable.Rows[i]["marry"].ToString();
                row[9] = AllRyTable.Rows[i]["political"].ToString();
                row[10] = AllRyTable.Rows[i]["country"].ToString();
                row[11] = AllRyTable.Rows[i]["education"].ToString();
                ryTable.Rows.Add(row);
            }
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = ryTable;
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "删除";
            DataGridViewButtonColumn buttonColumn2 = new DataGridViewButtonColumn();
            buttonColumn2.Name = "修改";
            dataGridView1.Columns.Add(buttonColumn);
            dataGridView1.Columns.Add(buttonColumn2);
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
            dataGridView1.Rows[e.RowIndex].Cells["删除"].Value = "删除";
            dataGridView1.Rows[e.RowIndex].Cells["修改"].Value = "修改";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string worknum = textBox1.Text;
            string name = textBox2.Text;
            string sid = textBox3.Text;
            string sex = comboBox1.Text;
            string nation = textBox5.Text;
            string jointime = dateTimePicker1.Value.ToString();
            string birth = textBox6.Text;
            string marry = comboBox2.Text;
            string political = comboBox3.Text;
            string country = textBox9.Text;
            string education = comboBox4.Text;
            if (sex == "全部")
            {
                sex = "";
            }
            if (marry == "全部")
            {
                marry = "";
            }

            SetPager(new myinfoTableAdapter().GetDataBy2(worknum
                , sid, sex, nation, birth, marry, political, country, education
                , jointime));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (new 添加人员().ShowDialog() == DialogResult.OK)
            {
                SetPager(new myinfoTableAdapter().GetData());
            }
        }
    }
}
