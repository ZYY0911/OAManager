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
using OAManager.MyInfoSqlTableAdapters;
using OAManager.聘任信息.EmploySqlTableAdapters;
using OAManager.财务管理.MageorderSqlTableAdapters;
using OAManager.财务管理.PerformanceSqlTableAdapters;
using OAManager.财务管理.OtherwageSqlTableAdapters;

namespace OAManager.财务管理
{
    public partial class 生成当月工资 : Form
    {
        public 生成当月工资()
        {
            InitializeComponent();
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
        DataTable table;
        private void 生成当月工资_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            table = new DataTable();
            DataColumn column1 = new DataColumn("工号");
            DataColumn column5 = new DataColumn("应发合计");
            DataColumn column6 = new DataColumn("扣款合计");
            DataColumn column7 = new DataColumn("实发合计");
            DataColumn column8 = new DataColumn("税费");
            table.Columns.Add(column1);
            table.Columns.Add(column5);
            table.Columns.Add(column6);
            table.Columns.Add(column7);
            table.Columns.Add(column8);
            initView(new wageinfoTableAdapter().GetDataBy(DateTime.Now.Year + "", DateTime.Now.Month + ""));
        }

        private void initView(DataTable dataTable)
        {
            table.Rows.Clear();
            DataRow row;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = table.NewRow();
                row[0] = dataTable.Rows[i]["worknum"].ToString();
                row[1] = dataTable.Rows[i]["totalshould"].ToString();
                row[2] = dataTable.Rows[i]["totaldeductions"].ToString();
                row[3] = dataTable.Rows[i]["totalissued"].ToString();
                row[4] = dataTable.Rows[i]["tax"].ToString();
                table.Rows.Add(row);
            }
            dataGridView1.DataSource = table;
        }
        DataTable Gztable;
        private void button5_Click(object sender, EventArgs e)
        {
            Gztable = new DataTable();
            DataColumn column1 = new DataColumn("工号");
            DataColumn column2 = new DataColumn("姓名");
            DataColumn column3 = new DataColumn("职称");
            DataColumn column4 = new DataColumn("岗位工资");
            DataColumn column5 = new DataColumn("薪级基本工资");
            DataColumn column6 = new DataColumn("调增津贴");
            DataColumn column7 = new DataColumn("职务补贴");
            DataColumn column8 = new DataColumn("地方补贴");
            DataColumn column9 = new DataColumn("考核津贴");
            DataColumn column10 = new DataColumn("住房补贴");
            DataColumn column11 = new DataColumn("公积金");
            DataColumn column12 = new DataColumn("养老保险");
            DataColumn column13 = new DataColumn("失业保险");
            DataColumn column14 = new DataColumn("医疗保险");
            DataColumn column15 = new DataColumn("职业年金");
            DataColumn column16 = new DataColumn("绩效工资");
            Gztable.Columns.Add(column1);
            Gztable.Columns.Add(column2);
            Gztable.Columns.Add(column3);
            Gztable.Columns.Add(column4);
            Gztable.Columns.Add(column5);
            Gztable.Columns.Add(column6);
            Gztable.Columns.Add(column7);
            Gztable.Columns.Add(column8);
            Gztable.Columns.Add(column9);
            Gztable.Columns.Add(column10);
            Gztable.Columns.Add(column11);
            Gztable.Columns.Add(column12);
            Gztable.Columns.Add(column13);
            Gztable.Columns.Add(column14);
            Gztable.Columns.Add(column15);
            Gztable.Columns.Add(column16);
            initViewJX(new myinfoTableAdapter().GetData());
            initView(new wageinfoTableAdapter().GetDataBy(DateTime.Now.Year + "", DateTime.Now.Month + ""));
        }

        private void initViewJX(DataTable dataTable)
        {
            Gztable.Rows.Clear();
            DataRow row;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = Gztable.NewRow();
                string worknum = dataTable.Rows[i]["worknum"].ToString().Trim();
                row[0] = worknum;
                row[1] = dataTable.Rows[i]["name"].ToString();
                DataTable emp = new employmentinfoTableAdapter().GetDataBy(worknum);
                DateTime jontime = Convert.ToDateTime(emp.Rows[emp.Rows.Count - 1]["jointime"].ToString());
                string jobtitile = emp.Rows[emp.Rows.Count - 1]["jobtitle"].ToString();
                row[2] = jobtitile;
                DataTable mageTabel = new mageorderTableAdapter().GetDataBy1(jobtitile);
                row[3] = mageTabel.Rows[0]["amount"].ToString();
                row[4] = getMySalary(Convert.ToInt32(mageTabel.Rows[0]["salary"].ToString())
                    , jontime, jobtitile);
                row[5] = mageTabel.Rows[0]["addwage"].ToString();
                row[6] = mageTabel.Rows[0]["workwage"].ToString();
                row[7] = mageTabel.Rows[0]["locationwage"].ToString();
                row[8] = mageTabel.Rows[0]["assesswage"].ToString();
                row[9] = mageTabel.Rows[0]["houswwage"].ToString();
                row[10] = mageTabel.Rows[0]["providentfund"].ToString();
                row[11] = mageTabel.Rows[0]["pension"].ToString();
                row[12] = mageTabel.Rows[0]["unemployment"].ToString();
                row[13] = mageTabel.Rows[0]["medical"].ToString();
                row[14] = mageTabel.Rows[0]["yearwage"].ToString();
                DataTable per = new performanceinfoTableAdapter().GetDataBy(worknum, DateTime.Now.Year + "", DateTime.Now.Month + "");
                row[15] = per.Rows.Count > 0 ? per.Rows[0]["performance"].ToString() : "0";
                Gztable.Rows.Add(row);
            }
            for (int i = 0; i < Gztable.Rows.Count; i++)
            {
                long allmoney = 0;
                long desmoney = 0;
                allmoney += Convert.ToInt32(Gztable.Rows[i]["岗位工资"].ToString());
                allmoney += Convert.ToInt32(Gztable.Rows[i]["薪级基本工资"].ToString());
                allmoney += Convert.ToInt32(Gztable.Rows[i]["调增津贴"].ToString());
                allmoney += Convert.ToInt32(Gztable.Rows[i]["职务补贴"].ToString());
                allmoney += Convert.ToInt32(Gztable.Rows[i]["地方补贴"].ToString());
                allmoney += Convert.ToInt32(Gztable.Rows[i]["考核津贴"].ToString());
                allmoney += Convert.ToInt32(Gztable.Rows[i]["绩效工资"].ToString());
                desmoney += Convert.ToInt32(Gztable.Rows[i]["公积金"].ToString());
                desmoney += Convert.ToInt32(Gztable.Rows[i]["养老保险"].ToString());
                desmoney += Convert.ToInt32(Gztable.Rows[i]["失业保险"].ToString());
                desmoney += Convert.ToInt32(Gztable.Rows[i]["医疗保险"].ToString());
                string worknumo = Gztable.Rows[i]["工号"].ToString().Trim();
                allmoney += new otherwageTableAdapter().ScalarQuery(worknumo, DateTime.Now.Year + ""
                        , DateTime.Now.Month + "")==null?0: (long)new otherwageTableAdapter().ScalarQuery(worknumo, DateTime.Now.Year + ""
                        , DateTime.Now.Month + "").Value;
                desmoney += new otherwage2TableAdapter().ScalarQuery(worknumo, DateTime.Now.Year + ""
                        , DateTime.Now.Month + "") == null?0:(long)new otherwage2TableAdapter().ScalarQuery(worknumo, DateTime.Now.Year + ""
                        , DateTime.Now.Month + "").Value;
                wageinfoTableAdapter wageinfo = new wageinfoTableAdapter();
                if (wageinfo.GetDataBy1(worknumo, DateTime.Now.Year + ""
                    , DateTime.Now.Month + "").Rows.Count != 0)
                {
                    wageinfo.UpdateQuery(allmoney, desmoney, allmoney - desmoney, GetDex(allmoney)
                        , worknumo, DateTime.Now.Year + ""
                        , DateTime.Now.Month + "");
                }
                else
                {
                    int yearmount = 0;
                    if (DateTime.Now.Year == 12)
                    {
                        yearmount = Convert.ToInt32(Gztable.Rows[i]["yearwage"].ToString());
                    }
                    DataTable desTable = wageinfo.GetDataBy1(worknumo, DateTime.Now.Year + "", DateTime.Now.AddMonths(-1).Month + "");
                    double des = 0;
                    if (desTable.Rows.Count > 0)
                    {
                        des = Convert.ToDouble(desTable.Rows[0]["tax"].ToString());
                    }
                    wageinfo.Insert(worknumo, DateTime.Now.Year + ""
                        , DateTime.Now.Month + "", allmoney, desmoney, allmoney - desmoney - des, GetDex(allmoney - desmoney)
                        , Convert.ToInt32(Gztable.Rows[i]["岗位工资"].ToString()), Convert.ToInt32(Gztable.Rows[i]["薪级基本工资"].ToString())
                        , Convert.ToInt32(Gztable.Rows[i]["调增津贴"].ToString()), Convert.ToInt32(Gztable.Rows[i]["职务补贴"].ToString())
                        , Convert.ToInt32(Gztable.Rows[i]["地方补贴"].ToString()), Convert.ToInt32(Gztable.Rows[i]["考核津贴"].ToString())
                        , Convert.ToInt32(Gztable.Rows[i]["绩效工资"].ToString()), Convert.ToInt32(Gztable.Rows[i]["公积金"].ToString())
                        , Convert.ToInt32(Gztable.Rows[i]["养老保险"].ToString()), Convert.ToInt32(Gztable.Rows[i]["失业保险"].ToString())
                        , Convert.ToInt32(Gztable.Rows[i]["医疗保险"].ToString()), yearmount);
                }

            }

        }

        private double GetDex(long money)
        {
            long des = 0;
            if (money < 5000)
            {
                return 0;
            }
            else if (money >= 5000 && money < 8000)
            {
                des = money - 5000;
                return des * 0.01;
            }
            else if (money >= 8000 && money < 12000)
            {
                des = money - 8000;
                return 8000 * 0.01 + des * 0.03;
            }
            else if (money >= 12000 && money < 15000)
            {
                des = money - 1200;
                return 8000 * 0.01 + 4000 * 0.03 + des * 0.06;
            }
            else
            {
                des = money - 15000;
                return 8000 * 0.01 + 4000 * 0.03 + 3000 * 0.06 + des * 0.1;
            }
        }

        private int getMySalary(int salary, DateTime dateTime, string jobtitle)
        {
            int startMoney = 0;
            switch (jobtitle)
            {
                case "初级":
                    startMoney = 30;
                    break;
                case "中级":
                    startMoney = 50;
                    break;
                case "副高":
                    startMoney = 80;
                    break;
                case "正高":
                    startMoney = 150;
                    break;
                default:
                    break;
            }
            int year = DateTime.Now.Year - dateTime.Year;
            if (year > 0)
            {
                startMoney = startMoney * year;
            }
            return startMoney + salary;
        }


    }
}
