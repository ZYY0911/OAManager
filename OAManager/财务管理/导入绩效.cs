using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.MyInfoSqlTableAdapters;
using OAManager.聘任信息.EmploySqlTableAdapters;
using OAManager.财务管理.MageorderSqlTableAdapters;
using OAManager.财务管理.PerformanceSqlTableAdapters;
using System.Data.OleDb;

namespace OAManager.财务管理
{
    public partial class 导入绩效 : Form
    {
        public 导入绩效()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Excel 2003|*.xls|Excel|*.xlsx|所有文件|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Thread thread = new Thread(LoadExcel);
                thread.IsBackground = true;
                thread.Start(fileDialog.FileName);
            }
        }
        private void LoadExcel(object path)
        {

            string ExclePath = (string)path;
            string Filetype = ExclePath.Substring(ExclePath.LastIndexOf('.'));
            string connstr = "";
            if (Filetype == ".xls")
            {
                connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExclePath
                + ";Extended Properties=Excel 8.0;";
            }
            else if (Filetype == ".xlsx")
            {
                connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExclePath
                + ";;Extended Properties='Excel 12.0;HDR=YES;IMEX=1'";
            }
            else
            {
                MessageBox.Show("您选的文件有错误");
                return;
            }
            OleDbConnection oleDb = new OleDbConnection(connstr);
            oleDb.Open();
            DataTable sheeName = oleDb.GetOleDbSchemaTable(OleDbSchemaGuid.Tables
                , new object[] { null, null, null, "TABLE" });
            string firstname = sheeName.Rows[0][2].ToString();
            string sql = "select * from [" + firstname + "]";
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connstr);
            DataSet set = new DataSet();
            adapter.Fill(set);
            oleDb.Close();
            DataTable table = set.Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string worknum = table.Rows[i][0].ToString();
                DataTable per = new performanceinfoTableAdapter().GetDataBy(worknum, DateTime.Now.Year + "", DateTime.Now.Month + "");
                if (per.Rows.Count == 0)
                {
                    try
                    {
                        new performanceinfoTableAdapter().Insert(worknum, Convert.ToInt32(table.Rows[i][1].ToString()), DateTime.Now.Year + "", DateTime.Now.Month + "");
                    }
                    catch
                    {
                        continue;
                    }
                }
                else
                {
                    try
                    {
                        new performanceinfoTableAdapter().DeleteQuery(worknum, DateTime.Now.Year + "", DateTime.Now.Month + "");
                        new performanceinfoTableAdapter().Insert(worknum, Convert.ToInt32(table.Rows[i][1].ToString()), DateTime.Now.Year + "", DateTime.Now.Month + "");
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            initView(new myinfoTableAdapter().GetData());

        }

        DataTable table;

        private void 导入绩效_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            table = new DataTable();
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
            table.Columns.Add(column1);
            table.Columns.Add(column2);
            table.Columns.Add(column3);
            table.Columns.Add(column4);
            table.Columns.Add(column5);
            table.Columns.Add(column6);
            table.Columns.Add(column7);
            table.Columns.Add(column8);
            table.Columns.Add(column9);
            table.Columns.Add(column10);
            table.Columns.Add(column11);
            table.Columns.Add(column12);
            table.Columns.Add(column13);
            table.Columns.Add(column14);
            table.Columns.Add(column15);
            table.Columns.Add(column16);
            dataGridView1.AllowUserToAddRows = false;
            initView(new myinfoTableAdapter().GetData());

        }

        private void initView(DataTable dataTable)
        {
            dataGridView1.DataSource = null;
            table.Rows.Clear();
            DataRow row;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = table.NewRow();
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
                table.Rows.Add(row);
            }
            dataGridView1.DataSource = table;


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
    }
}
