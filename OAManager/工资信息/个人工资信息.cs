using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.财务管理;
using OAManager.财务管理.WageinfoSqlTableAdapters;

namespace OAManager.工资信息
{
    public partial class 个人工资信息 : Form
    {
        public 个人工资信息()
        {
            InitializeComponent();
        }

        private void 个人工资信息_Load(object sender, EventArgs e)
        {
            if (AppInfo.type == 6||AppInfo.type==4)
            {
                button1.Visible = false;
            }
            listView1.View = View.Details;
            label2.Text = AppInfo.worknum;
            label4.Text = GetWorknumInof.getWorknumName(AppInfo.worknum);
            label5.Text = GetWorknumInof.getWorknumDeparment(AppInfo.worknum);
            loadMyMage();
        }

        private void loadMyMage()
        {
            listView1.Columns.Clear();
            listView1.Columns.Add("类型", 200, HorizontalAlignment.Center);
            listView1.Columns.Add("金额", 200, HorizontalAlignment.Center);
            panel4.Controls.Clear();
            listView1.Items.Clear();
            string worknum = AppInfo.worknum;
            string dateYear = dateTimePicker1.Value.Year+"";
            string dateMonth = dateTimePicker1.Value.Month+"";
            DataTable other1 = null, other2 = null;
            using (财务管理.OtherwageSqlTableAdapters.otherwageTableAdapter otherwage = new 财务管理.OtherwageSqlTableAdapters.otherwageTableAdapter())
            {
                other1 = otherwage.GetDataBy1(worknum, dateYear, dateMonth);
            }
            using (财务管理.OtherwageSqlTableAdapters.otherwage2TableAdapter otherwage = new 财务管理.OtherwageSqlTableAdapters.otherwage2TableAdapter())
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
                        DateTime time = dateTimePicker1.Value;
                        time = time.AddMonths(-1);
                        DataTable desTAbel = wageinfo.GetDataBy1(worknum, time.Year+"", time.Month + "");
                        string dex = "";
                        if (desTAbel.Rows.Count > 0)
                        {
                            dex = desTAbel.Rows[0]["tax"].ToString();
                        }
                        else
                        {
                            dex = "暂无";
                        }
                        panel.Controls.Add(new 工资详情Item(lendname[i],dex));

                    }
                    else
                    {
                        panel.Controls.Add(new 工资详情Item(lendname[i], wageTable.Rows[0][lendnameen[i]].ToString()));
                    }
                        panel4.Controls.Add(panel);
                }
                label9.Text = wageTable.Rows[0]["totalissued"].ToString();
                label10.Text = wageTable.Rows[0]["totalshould"].ToString();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadMyMage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new 查看本部门工资().Show();
        }
    }
}
