using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using OAManager.MyInfoAndEmpTableAdapters;
using OAManager.财务管理.WageinfoSqlTableAdapters;

namespace OAManager.工资信息
{
    public partial class 查看本部门工资 : Form
    {
        public 查看本部门工资()
        {
            InitializeComponent();
        }
        string dem;
        private void button1_Click(object sender, EventArgs e)
        {
            loadQj();

        }

        private void 查看本部门工资_Load(object sender, EventArgs e)
        {
            dem = GetWorknumInof.getWorknumDeparment(AppInfo.worknum);
            loadQj();
            loadYG();
            dataGridView1.AllowUserToAddRows = false;
            chart2.Visible = false;
            panel1.Visible = false;
        }
        DataTable table;
        private void loadYG()
        {
            table = new DataTable();
            DataColumn column1 = new DataColumn("姓名");
            DataColumn column2 = new DataColumn("工号");
            table.Columns.Add(column1);
            table.Columns.Add(column2);
            initView2(table);
        }

        private void initView2(DataTable dataTable)
        {
            DataTable empTable = new employmentinfoTableAdapter().GetData();
            empTable = ReplaceRever(empTable);
            List<string> worknumlist = new List<string>();
            for (int i = 0; i < empTable.Rows.Count; i++)
            {
                if (empTable.Rows[i]["department"].ToString() == dem)
                {
                    worknumlist.Add(empTable.Rows[i]["worknum"].ToString());
                }
            }
            for (int i = 0; i < worknumlist.Count; i++)
            {
                DataRow row = table.NewRow();
                row[0] = GetWorknumInof.getWorknumName(worknumlist[i]);
                row[1] = worknumlist[i];
                table.Rows.Add(row);
            }
            dataGridView1.DataSource = table;
        }

        private void loadQj()
        {
            DataTable empTable = new employmentinfoTableAdapter().GetData();
            empTable = ReplaceRever(empTable);
            int count = 0;
            List<string> worknumlist = new List<string>();
            for (int i = 0; i < empTable.Rows.Count; i++)
            {
                if (empTable.Rows[i]["department"].ToString() == dem)
                {
                    worknumlist.Add(empTable.Rows[i]["worknum"].ToString());
                    count++;
                }
            }
            label2.Text = count + "";
            double moneytotle = 0;
            List<double> moneyList = new List<double>();
            List<double> totalshould = new List<double>();
            wageinfoTableAdapter wageinfo = new wageinfoTableAdapter();
            Console.WriteLine(dateTimePicker1.Value.Year + "====" + dateTimePicker1.Value.Month);
            for (int i = 0; i < worknumlist.Count; i++)
            {
                DataTable dataTable = wageinfo.GetDataBy1(worknumlist[i], dateTimePicker1.Value.Year + "", dateTimePicker1.Value.Month + "");
                if (dataTable.Rows.Count > 0)
                {
                    moneytotle += Convert.ToDouble(dataTable.Rows[0]["totalissued"].ToString());
                    totalshould.Add(Convert.ToDouble(dataTable.Rows[0]["totalshould"].ToString()));
                    moneyList.Add(Convert.ToDouble(dataTable.Rows[0]["totalissued"].ToString()));
                }
            }
            Dictionary<string, int> keys = new Dictionary<string, int>();
            Dictionary<string, int> keys2 = new Dictionary<string, int>();
            keys["2000-4000"] = 0;
            keys["4001-6000"] = 0;
            keys["4001-8000"] = 0;
            keys["8001+"] = 0;
            keys2["2000-4000"] = 0;
            keys2["4001-6000"] = 0;
            keys2["4001-8000"] = 0;
            keys2["8001+"] = 0;
            for (int i = 0; i < totalshould.Count; i++)
            {
                double num = totalshould[i];
                string str = "";
                if (num < 4000)
                {
                    str = "2000-4000";
                }
                else if (num >= 4001 && num <= 6000)
                {
                    str = "4001-6000";
                }
                else if (num >= 6001 && num <= 8000)
                {
                    str = "4001-8000";
                }
                else
                {
                    str = "8001+";
                }
                keys[str] = keys.ContainsKey(str) ? keys[str] + 1 : 1;
            }
            for (int i = 0; i < moneyList.Count; i++)
            {
                double num = moneyList[i];
                string str = "";
                if (num < 4000)
                {
                    str = "2000-4000";
                }
                else if (num >= 4001 && num <= 6000)
                {
                    str = "4001-6000";
                }
                else if (num >= 6001 && num <= 8000)
                {
                    str = "4001-8000";
                }
                else
                {
                    str = "8001+";
                }
                keys2[str] = keys2.ContainsKey(str) ? keys2[str] + 1 : 1;
            }
            label3.Text = moneytotle + "";
            label6.Text = (moneytotle / count) + "";
            chart1.Series.Clear();
            Series series = new Series();
            series.Color = Color.Red;
            series.Name = "应发工资";
            series.ChartType = SeriesChartType.Line;
            Series series2 = new Series();
            series2.Color = Color.Green;
            series2.BorderWidth = 3;
            series2.Name = "实发工资";
            series2.ChartType = SeriesChartType.Line;
            foreach (KeyValuePair<string, int> item in keys)
            {
                series.Points.AddXY(item.Key, item.Value);

            }
            foreach (KeyValuePair<string, int> item in keys2)
            {
                series2.Points.AddXY(item.Key, item.Value);
            }
            chart1.Series.Add(series);
            chart1.Series.Add(series2);

            foreach (Legend item in chart1.Legends)
            {
                item.Docking = Docking.Top;
            }
        }

        private DataTable ReplaceRever(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = table.Rows.Count - 1; j > i; j--)
                {
                    if (table.Rows[i]["worknum"].ToString() == table.Rows[j]["worknum"].ToString())
                    {
                        table.Rows.RemoveAt(i);
                    }
                }
            }
            return table;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                chart2.Visible =true;
                panel1.Visible =true;
                label9.Text = dataGridView1.Rows[e.RowIndex].Cells["姓名"].Value.ToString();
                string worknum = dataGridView1.Rows[e.RowIndex].Cells["工号"].Value.ToString();
                label11.Text = worknum;
                label12.Text = GetWorknumInof.getWorknumJobtitle(worknum);
                List<MyGz> myGzs = new List<MyGz>();
                for (int i = 6; i >= 0; i--)
                {
                    DateTime now = DateTime.Now.AddMonths(-i);
                    myGzs.Insert(0, new MyGz(now.Year + "", now.Month + ""));
                }
                double allmoney = 0;
                wageinfoTableAdapter wageinfo = new wageinfoTableAdapter();
                Dictionary<string, double> keys = new Dictionary<string, double>();
                Dictionary<string, double> keys2 = new Dictionary<string, double>();

                for (int i = 0; i < myGzs.Count; i++)
                {
                    keys[myGzs[i].Yaer + "/" + myGzs[i].Month] = 0;
                    keys2[myGzs[i].Yaer + "/" + myGzs[i].Month] = 0;
                    DataTable dataTable = wageinfo.GetDataBy1(worknum, myGzs[i].Yaer, myGzs[i].Month);
                    if (dataTable.Rows.Count > 0)
                    {
                        allmoney += Convert.ToDouble(dataTable.Rows[0]["totalissued"].ToString());
                        keys[myGzs[i].Yaer + "/" + myGzs[i].Month] = Convert.ToDouble(dataTable.Rows[0]["totalissued"].ToString());
                        keys2[myGzs[i].Yaer + "/" + myGzs[i].Month] = Convert.ToDouble(dataTable.Rows[0]["totalshould"].ToString());
                    }
                }
                label14.Text = (allmoney / 6) + "";
                chart2.Series.Clear();
                Series series = new Series();
                series.Color = Color.Green;
                series.Name = "实发工资";
                Series series2 = new Series();
                series2.Color = Color.Red;
                series2.Name = "应发工资";
                foreach (KeyValuePair<string, double> item in keys)
                {
                    series.Points.AddXY(item.Key, item.Value);
                }
                foreach (KeyValuePair<string, double> item in keys2)
                {
                    series2.Points.AddXY(item.Key, item.Value);
                }
                chart2.Series.Add(series);
                chart2.Series.Add(series2);

                foreach (Legend item in chart2.Legends)
                {
                    item.Docking = Docking.Top;
                }
            }
        }


    }
    class MyGz
    {
        string yaer, month;

        public MyGz(string yaer, string month)
        {
            this.yaer = yaer;
            this.month = month;
        }

        public string Yaer { get => yaer; set => yaer = value; }
        public string Month { get => month; set => month = value; }
    }
}
