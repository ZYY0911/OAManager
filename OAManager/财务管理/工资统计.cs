using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.财务管理.WageinfoSqlTableAdapters;

namespace OAManager.财务管理
{
    public partial class 工资统计 : Form
    {
        int allpage, indexpager, allrow, indexrow, showrow = 10;
        public 工资统计()
        {
            InitializeComponent();
        }

        private void 工资统计_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            // TODO: This line of code loads data into the 'departmentSql.department' table. You can move, or remove it, as needed.
            this.departmentTableAdapter.Fill(this.departmentSql.department);
            // TODO: This line of code loads data into the 'jobSql.jobtitle' table. You can move, or remove it, as needed.
            this.jobtitleTableAdapter.Fill(this.jobSql.jobtitle);
            loadJS();
            dataGridView1.AllowUserToAddRows = false;
        }

        DataTable table;
        DataTable dataTableJs;
        private void loadJS()
        {
            table = new DataTable();
            DataColumn column1 = new DataColumn("工号");
            DataColumn column2 = new DataColumn("姓名");
            DataColumn column3 = new DataColumn("时间");
            DataColumn column4 = new DataColumn("应发");
            DataColumn column5 = new DataColumn("实发");
            table.Columns.Add(column1);
            table.Columns.Add(column2);
            table.Columns.Add(column3);
            table.Columns.Add(column4);
            table.Columns.Add(column5);
            dataTableJs = new wageinfoTableAdapter().GetData();
            setstart();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (indexpager == 1)
            {
                MessageBox.Show("已经是第一页");
                return;
            }
            indexpager = 1;
            indexrow = 0;
            initViewJS();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            indexpager--;
            if (indexpager == 0)
            {
                MessageBox.Show("已经是第一页");
                indexpager = 1;
                return;
            }
            indexrow = indexrow - showrow;
            initViewJS();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            indexpager++;
            if (indexpager > allpage)
            {
                MessageBox.Show("已经是最后一页");
                indexpager = allpage;
                return;
            }
            indexrow = indexrow + showrow;
            initViewJS();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (indexpager > allpage)
            {
                MessageBox.Show("已经是最后一页");
                return;
            }
            indexpager = allpage;
            indexrow = (indexpager - 1) * showrow;
            initViewJS();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                panel2.Controls.Clear();
                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.Columns.Add("类型", 100, HorizontalAlignment.Center);
                listView1.Columns.Add("金额", 100, HorizontalAlignment.Center);
                string worknum = dataGridView1.Rows[e.RowIndex].Cells["工号"].Value.ToString();
                string dateYear = dataGridView1.Rows[e.RowIndex].Cells["时间"].Value.ToString().Split('/')[0];
                string dateMonth = dataGridView1.Rows[e.RowIndex].Cells["时间"].Value.ToString().Split('/')[1];
                DataTable other1 = null, other2 = null;
                using (OtherwageSqlTableAdapters.otherwageTableAdapter otherwage = new OtherwageSqlTableAdapters.otherwageTableAdapter())
                {
                    other1 = otherwage.GetDataBy1(worknum, dateYear, dateMonth);
                }
                using (OtherwageSqlTableAdapters.otherwage2TableAdapter otherwage = new OtherwageSqlTableAdapters.otherwage2TableAdapter())
                {
                    other2 = otherwage.GetDataBy1(worknum, dateYear, dateMonth);
                }
                ListViewGroup group = new ListViewGroup();
                group.Header = "奖金项";
                group.HeaderAlignment = HorizontalAlignment.Left;
                this.listView1.Groups.Add(group);
                for (int i = 0; i < other1.Rows.Count; i++)
                {
                    ListViewItem list = new ListViewItem();
                    list.Text = other1.Rows[i]["rewardtype"].ToString();
                    list.SubItems.Add(other1.Rows[i]["amount"].ToString());
                    group.Items.Add(list);
                    this.listView1.Items.Add(list);
                }
                ListViewGroup group2 = new ListViewGroup();
                group2.Header = "罚款";
                group2.HeaderAlignment = HorizontalAlignment.Left;
                this.listView1.Groups.Add(group2);
                for (int i = 0; i < other2.Rows.Count; i++)
                {
                    ListViewItem list = new ListViewItem();
                    list.Text = other2.Rows[i]["rewardtype"].ToString();
                    list.SubItems.Add(other2.Rows[i]["amount"].ToString());
                    group2.Items.Add(list);
                    this.listView1.Items.Add(list);
                }
                wageinfoTableAdapter wageinfo = new wageinfoTableAdapter();
                DataTable wageTable = wageinfo.GetDataBy1(worknum, dateYear, dateMonth);
                if (wageTable.Rows.Count > 0)
                {
                    List<string> lendname = new List<string> { "岗位工资", "薪级基本工资", "调增津贴", "职务补贴"
            ,"地方补贴","考核津贴","住房补贴", "公积金", "养老保险", "失业保险", "医疗保险","上月缴税" };
                    List<string> lendnameen = new List<string> { "amount","salary","addwage","workwage"
                        ,"locationwage","assesswage","houswwage","providentfund","pension","unemployment"
                        ,"medical","上月缴税"};
                    for (int i = lendnameen.Count - 1; i >= 0; i--)
                    {
                        Panel panel = new Panel();
                        panel.Dock = DockStyle.Top;
                        panel.Size = new Size(389, 40);
                        if (lendnameen[i] == "上月缴税")
                        {
                            DateTime time = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["时间"].Value.ToString());
                            time = time.AddMonths(-1);
                            DataTable desTAbel = wageinfo.GetDataBy1(worknum, time.Year + "", time.Month + "");
                            string dex = "";
                            if (desTAbel.Rows.Count > 0)
                            {
                                dex = desTAbel.Rows[0]["tax"].ToString();
                            }
                            else
                            {
                                dex = "暂无";
                            }
                            panel.Controls.Add(new 工资详情Item(lendname[i], dex));

                        }
                        else
                        {
                            panel.Controls.Add(new 工资详情Item(lendname[i], wageTable.Rows[0][lendnameen[i]].ToString()));
                        }
                        panel2.Controls.Add(panel);

                    }
                }

            }
        }

        private void setstart()
        {
            allrow = dataTableJs.Rows.Count;
            allpage = allrow / showrow;
            if (allrow % showrow > 0)
            {
                allpage++;
            }
            indexpager = 1;
            initViewJS();
        }

        private void initViewJS()
        {
            label9.Text = "当前" + indexpager + "/共" + allpage;
            dataGridView1.DataSource = null;
            table.Rows.Clear();
            DataRow row;
            int j = indexrow + showrow;
            if (j > allrow)
            {
                j = allrow;
            }
            for (int i = indexrow; i < j; i++)
            {
                row = table.NewRow();
                string worknum = dataTableJs.Rows[i]["worknum"].ToString();
                row[0] = worknum;
                row[1] = GetWorknumInof.getWorknumName(worknum);
                row[2] = dataTableJs.Rows[i]["year"].ToString() + "/" + dataTableJs.Rows[i]["month"].ToString();
                row[3] = dataTableJs.Rows[i]["totalshould"].ToString();
                row[4] = dataTableJs.Rows[i]["totalissued"].ToString();
                table.Rows.Add(row);
            }
            dataGridView1.DataSource = table;
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

        private void button5_Click(object sender, EventArgs e)
        {
            string worknum = textBox1.Text;
            string jobtitle = comboBox1.Text;
            string dempart = comboBox2.Text;
            string name = textBox2.Text;
            string year = dateTimePicker1.Value.Year + "";
            string month = dateTimePicker1.Value.Month + "";
            Console.WriteLine(year + "===" + month);
            string maxnum = textBox3.Text;
            string minnum = textBox4.Text;
            string lx = radioButton1.Checked ? "1" : "2";
            if (minnum == "")
            {
                minnum = "0";
            }
            if (maxnum == "")
            {
                maxnum = "99999999";
            }
            dataTableJs.Rows.Clear();
            if (lx == "1")
            {
                dataTableJs = new wageinfoTableAdapter().GetDataBy3(worknum, year, month
                    , Convert.ToDouble(minnum), Convert.ToDouble(maxnum));
            }
            else
            {
                dataTableJs = new wageinfoTableAdapter().GetDataBy4(worknum, year, month
                    , Convert.ToDouble(minnum), Convert.ToDouble(maxnum));
            }
            if (comboBox1.Text != "")
            {
                for (int i = dataTableJs.Rows.Count - 1; i >= 0; i--)
                {
                    string myworknum = dataTableJs.Rows[i]["worknum"].ToString();
                    if (GetWorknumInof.getWorknumJobtitle(myworknum) != jobtitle)
                    {
                        dataTableJs.Rows.RemoveAt(i);
                    }
                }
            }
            if (comboBox2.Text != "")
            {
                for (int i = dataTableJs.Rows.Count - 1; i >= 0; i--)
                {
                    string myworknum = dataTableJs.Rows[i]["worknum"].ToString();
                    if (GetWorknumInof.getWorknumDeparment(myworknum) != dempart)
                    {
                        dataTableJs.Rows.RemoveAt(i);
                    }
                }
            }
            setstart();
        }
    }
}
