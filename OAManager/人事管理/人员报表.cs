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
using OAManager.MyInfoSqlTableAdapters;

namespace OAManager.人事管理
{
    public partial class 人员报表 : Form
    {
        public 人员报表()
        {
            InitializeComponent();
        }

        private void 人员报表_Load(object sender, EventArgs e)
        {
            loadSex();
            loadNation();
            loadYear();
            loadXl();
            loadZZ();
            loadZC();
            loadBM();
        }

        private void loadBM()
        {
            Dictionary<string, int> keys = new Dictionary<string, int>();
            using (DepartmentSqlTableAdapters.departmentTableAdapter department = new DepartmentSqlTableAdapters.departmentTableAdapter())
            {
                DataTable table = department.GetData();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    keys[table.Rows[i][1].ToString()] = 0;
                }
            }
            using (DataTable1TableAdapter employmentinfo = new DataTable1TableAdapter())
            {
                DataTable data = employmentinfo.GetData();
                getDatatAble(data);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string education = data.Rows[i]["department"].ToString();
                    keys[education] = keys[education] + 1;
                }
            }
            foreach (KeyValuePair<string, int> item in keys)
            {
                chart7.Series[0].Points.AddXY(item.Key, item.Value);
            }
            chart7.Legends[0].Enabled = false;
            chart7.Series[0].Color = Color.OrangeRed;
            chart7.Series[0].Label = "#VALY";
        }

        /// <summary>
        /// chart5
        /// </summary>
        private void loadZC()
        {
            Dictionary<string, int> keys = new Dictionary<string, int>();
            using (JobSqlTableAdapters.jobtitleTableAdapter jobSqlTable = new JobSqlTableAdapters.jobtitleTableAdapter())
            {
                DataTable table = jobSqlTable.GetData();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    keys[table.Rows[i][1].ToString()] = 0;
                }
            }
            DataTable1TableAdapter employmentinfo = new DataTable1TableAdapter();
            DataTable data = employmentinfo.GetData();
            getDatatAble(data);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (int j = data.Rows.Count-1; j >i ; j--)
                {
                    if (data.Rows[i]["worknum"].ToString()==data.Rows[j]["worknum"].ToString())
                    {
                        data.Rows.RemoveAt(j);
                    }
                }
            }
            for (int i = 0; i < data.Rows.Count; i++)
            {
                string education = data.Rows[i]["jobtitle"].ToString();
                keys[education] = keys[education] + 1;
            }
            foreach (KeyValuePair<string, int> item in keys)
            {
                chart5.Series[0].Points.AddXY(item.Key, item.Value);
            }
            chart5.Legends[0].Enabled = false;
            chart5.Series[0].Color = Color.OrangeRed;
            chart5.Series[0].Label = "#VALY";
        }
        private void getDatatAble(DataTable data)
        {
            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (int j = data.Rows.Count - 1; j > i; j--)
                {
                    if (data.Rows[i]["worknum"].ToString() == data.Rows[j]["worknum"].ToString())
                    {
                        data.Rows.RemoveAt(i);
                        getDatatAble(data);
                    }
                }
            }
        }
        /// <summary>
        /// chart6
        /// </summary>
        private void loadZZ()
        {
            Dictionary<string, int> keys = new Dictionary<string, int>();
            using (个人基本信息.BaseSqlTableAdapters.politicalTableAdapter political = new 个人基本信息.BaseSqlTableAdapters.politicalTableAdapter())
            {
                DataTable table = political.GetData();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    keys[table.Rows[i][1].ToString()] = 0;
                }
            }
            using (MyInfoSqlTableAdapters.myinfoTableAdapter myInfoSql = new MyInfoSqlTableAdapters.myinfoTableAdapter())
            {
                DataTable data = myInfoSql.GetData();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string education = data.Rows[i]["political"].ToString();
                    keys[education] = keys[education] + 1;
                }
            }
            foreach (KeyValuePair<string, int> item in keys)
            {
                chart6.Series[0].Points.AddXY(item.Key, item.Value);
            }
            chart6.Legends[0].Enabled = false;
            chart6.Series[0].Color = Color.OrangeRed;
            chart6.Series[0].Label = "#VALY";
        }

        private void loadXl()
        {
            Dictionary<string, int> keys = new Dictionary<string, int>();
            using (个人基本信息.BaseSqlTableAdapters.educationTableAdapter education = new 个人基本信息.BaseSqlTableAdapters.educationTableAdapter())
            {
                DataTable table = education.GetData();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    keys[table.Rows[i][1].ToString()] = 0;
                }
            }
            using (MyInfoSqlTableAdapters.myinfoTableAdapter myInfoSql = new MyInfoSqlTableAdapters.myinfoTableAdapter())
            {
                DataTable data = myInfoSql.GetData();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string education = data.Rows[i]["education"].ToString();
                    keys[education] = keys[education] + 1;
                }
            }
            foreach (KeyValuePair<string, int> item in keys)
            {
                chart3.Series[0].Points.AddXY(item.Key, item.Value);
            }
            chart3.Legends[0].Enabled = false;
            chart3.Series[0].Color = Color.OliveDrab;
            chart3.Series[0].Label = "#VALY";
        }

        private void loadYear()
        {
            Dictionary<string, int> keys = new Dictionary<string, int>();
            DateTime now = DateTime.Now;
            using (MyInfoSqlTableAdapters.myinfoTableAdapter myInfoSql = new MyInfoSqlTableAdapters.myinfoTableAdapter())
            {
                DataTable data = myInfoSql.GetData();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    DateTime sex = Convert.ToDateTime(data.Rows[i]["birth"].ToString());
                    int year = now.Year - sex.Year;
                    if (year <= 30)
                    {
                        keys["30"] = keys.ContainsKey("30") ? keys["30"] + 1 : 1;
                    }
                    else if (year > 30 && year <= 45)
                    {
                        keys["30-45"] = keys.ContainsKey("30-45") ? keys["30-45"] + 1 : 1;
                    }
                    else if (year > 45 && year <= 60)
                    {
                        keys["45-60"] = keys.ContainsKey("45-60") ? keys["45-60"] + 1 : 1;
                    }
                    else if (year > 60)
                    {
                        keys["60"] = keys.ContainsKey("60") ? keys["60"] + 1 : 1;
                    }
                }
            }
            if (!keys.ContainsKey("60"))
            {
                keys["60"] = 0;
            }
            foreach (KeyValuePair<string, int> item in keys)
            {
                chart4.Series[0].Points.AddXY(item.Key, item.Value);
            }
            chart4.Legends[0].Enabled = false;
            chart4.Series[0].Color = Color.OliveDrab;
            chart4.Series[0].Label = "#VALY";
        }

        private void loadNation()
        {
            Dictionary<string, int> keys = new Dictionary<string, int>();
            using (NationSqlTableAdapters.nationTableAdapter nationTable = new NationSqlTableAdapters.nationTableAdapter())
            {
                DataTable table = nationTable.GetData();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    keys[table.Rows[i]["nationname"].ToString()] = 0;
                }
            }
            using (MyInfoSqlTableAdapters.myinfoTableAdapter myInfoSql = new MyInfoSqlTableAdapters.myinfoTableAdapter())
            {
                DataTable data = myInfoSql.GetData();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string sex = data.Rows[i]["nation"].ToString();
                    int count = keys[sex];
                    keys[sex] = count + 1;
                }
            }
            chart2.Series.Clear();
            Series series = new Series();
            series.ChartType = SeriesChartType.Pie;
            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Black";
            foreach (KeyValuePair<string, int> item in keys)
            {
                series.Points.AddXY(item.Key, item.Value);
            }
            series.Label = "#VALX;#PERCENT{P2}";
            chart2.Series.Add(series);
            chart2.ChartAreas[0].Position.X = 0;
            chart2.ChartAreas[0].Position.Y = 0;
            chart2.ChartAreas[0].Position.Width = 100;
            chart2.ChartAreas[0].Position.Height = 100;
            chart2.Legends[0].Docking = Docking.Top;
        }

        private void loadSex()
        {
            chart1.Series.Clear();
            Series series = new Series();
            series.ChartType = SeriesChartType.Pie;
            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Black";
            Dictionary<string, int> keys = new Dictionary<string, int>();
            keys["男"] = 0;
            keys["女"] = 0;
            using (MyInfoSqlTableAdapters.myinfoTableAdapter myInfoSql = new MyInfoSqlTableAdapters.myinfoTableAdapter())
            {
                DataTable data = myInfoSql.GetData();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string sex = data.Rows[i]["sex"].ToString();
                    int count = keys[sex];
                    keys[sex] = count + 1;
                }
            }
            foreach (KeyValuePair<string, int> item in keys)
            {
                series.Points.AddXY(item.Key, item.Value);
            }
            series.Label = "#VALX;#PERCENT{P2}";
            chart1.Series.Add(series);
            chart1.Legends[0].Docking = Docking.Top;
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            new 报表(1).ShowDialog();
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            new 报表(2).ShowDialog();
        }

        private void chart4_Click(object sender, EventArgs e)
        {
            new 报表(3).ShowDialog();
        }

        private void chart3_Click(object sender, EventArgs e)
        {
            new 报表(4).ShowDialog();
        }

        private void chart6_Click(object sender, EventArgs e)
        {
            new 报表(5).ShowDialog();
        }

        private void chart5_Click(object sender, EventArgs e)
        {
            new 报表(6).ShowDialog();
        }

        private void chart7_Click(object sender, EventArgs e)
        {
            new 报表(7).ShowDialog();
        }
    }
}
