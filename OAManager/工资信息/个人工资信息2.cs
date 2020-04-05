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
    public partial class 个人工资信息2 : Form
    {
        public 个人工资信息2()
        {
            InitializeComponent();
        }

        private void 个人工资信息2_Load(object sender, EventArgs e)
        {
            if (AppInfo.type == 6 || AppInfo.type == 4)
            {
                button1.Visible = false;
            }
            label2.Text = AppInfo.worknum;
            label4.Text = GetWorknumInof.getWorknumName(AppInfo.worknum);
            label5.Text = GetWorknumInof.getWorknumDeparment(AppInfo.worknum);
            loadMyMage();
        }

        private void loadMyMage()
        {
            string worknum = AppInfo.worknum;
            string dateYear = dateTimePicker1.Value.Year + "";
            string dateMonth = dateTimePicker1.Value.Month + "";
            wageinfoTableAdapter wageinfo = new wageinfoTableAdapter();
            DataTable wageTable = wageinfo.GetDataBy1(worknum, dateYear, dateMonth);
            Console.WriteLine(wageTable.Rows.Count);
            if (wageTable.Rows.Count > 0)
            {
                List<string> lendname = new List<string> { "岗位工资", "薪级基本工资", "调增津贴", "职务补贴"
            ,"地方补贴","考核津贴","住房补贴", "公积金", "养老保险", "失业保险", "医疗保险","上月缴税","应发工资","实发工资" };
                List<string> lendnameen = new List<string> { "amount","salary","addwage","workwage"
                        ,"locationwage","assesswage","houswwage","providentfund","pension","unemployment"
                        ,"medical","上月缴税","totalshould","totalissued"};
                label9.Text = wageTable.Rows[0]["totalissued"].ToString();
                label10.Text = wageTable.Rows[0]["totalshould"].ToString();
                DataTable table = new DataTable();
                Dictionary<string, double> value = new Dictionary<string, double>();
                for (int i = 0; i < lendnameen.Count; i++)
                {
                    DataColumn column1 = new DataColumn(lendname[i]);
                    table.Columns.Add(column1);
                    if (lendnameen[i] == "上月缴税")
                    {
                        DateTime time = dateTimePicker1.Value;
                        time = time.AddMonths(-1);
                        DataTable desTAbel = wageinfo.GetDataBy1(worknum, time.Year + "", time.Month + "");
                        string dex = "";
                        if (desTAbel.Rows.Count > 0)
                        {
                            dex = desTAbel.Rows[0]["tax"].ToString();
                        }
                        else
                        {
                            dex = "0";
                        }
                        value[lendname[i]]= Convert.ToDouble(dex);
                    }
                    else
                    {
                        value[lendname[i]] =Convert.ToDouble(wageTable.Rows[0][lendnameen[i]].ToString());
                    }
                }

                DataTable other1 = null, other2 = null;
                using (财务管理.OtherwageSqlTableAdapters.otherwageTableAdapter otherwage = new 财务管理.OtherwageSqlTableAdapters.otherwageTableAdapter())
                {
                    other1 = otherwage.GetDataBy1(worknum, dateYear, dateMonth);
                }
                using (财务管理.OtherwageSqlTableAdapters.otherwage2TableAdapter otherwage = new 财务管理.OtherwageSqlTableAdapters.otherwage2TableAdapter())
                {
                    other2 = otherwage.GetDataBy1(worknum, dateYear, dateMonth);
                }
                for (int i = 0; i < other1.Rows.Count; i++)
                {
                    DataColumn column = new DataColumn("奖项:" + other1.Rows[i]["rewardtype"].ToString());
                    if (table.Columns.Contains(column.ColumnName))
                    {
                        double mont = value[column.ColumnName];
                        value[column.ColumnName] = mont + Convert.ToDouble(other1.Rows[i]["amount"].ToString());
                    }
                    else
                    {
                        table.Columns.Add(column);
                        value[column.ColumnName] = Convert.ToDouble(other1.Rows[i]["amount"].ToString());

                    }

                }
                for (int i = 0; i < other2.Rows.Count; i++)
                {
                    DataColumn column = new DataColumn("罚款:" + other2.Rows[i]["rewardtype"].ToString());
                    if (table.Columns.Contains(column.ColumnName))
                    {
                        double mont = value[column.ColumnName];
                        value[column.ColumnName] = mont + Convert.ToDouble(other1.Rows[i]["amount"].ToString());
                    }
                    else
                    {
                        table.Columns.Add(column);
                        value[column.ColumnName] = Convert.ToDouble(other1.Rows[i]["amount"].ToString());

                    }
                }
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.DataSource = null;
                DataRow row = table.NewRow();
                int j = 0;
                foreach (KeyValuePair<string,double> item in value)
                {
                    row[j] = item.Value;
                    j++;
                }
                table.Rows.Add(row);
                dataGridView1.DataSource = table;
            }
            else
            {
                label9.Text = "";
                label10.Text = "";
                dataGridView1.DataSource = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new 查看本部门工资().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadMyMage();
        }
    }

   
}
