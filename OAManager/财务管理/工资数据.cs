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
using System.Windows.Forms.DataVisualization.Charting;
using OAManager.财务管理.MageorderSqlTableAdapters;

namespace OAManager.财务管理
{
    public partial class 工资数据 : Form
    {
        public 工资数据()
        {
            InitializeComponent();
        }

        private void 工资数据_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            loadManager();
            loadTJ();
        }
        Dictionary<string, int> keyBt = new Dictionary<string, int>();
        List<string> jobtitle = new List<string> { "初级", "中级", "副高", "正高" };
        List<string> lendname = new List<string> { "岗位工资", "薪级基本工资", "调增津贴", "职务补贴" 
            ,"地方补贴","考核津贴","住房补贴"};
        List<string> lendname2 = new List<string> { "公积金", "养老保险", "失业保险", "医疗保险"
            ,"职业年金","上月税收"};
        private void loadTJ()
        {
            chart1.Series.Clear();
            DataTable gzTable = new mageorderTableAdapter().GetData();
            for (int i = 2; i < 9; i++)
            {
                Series series = new Series();
                series.Name = lendname[i - 2];
                series.ChartType = SeriesChartType.Column;
                for (int j = 0; j < gzTable.Rows.Count; j++)
                {
                    series.Points.AddXY(jobtitle[j],Convert.ToInt32(gzTable.Rows[j][i].ToString()));
                }
                chart1.Series.Add(series);
            }
            foreach (Legend item in chart1.Legends)
            {
                item.Docking = Docking.Top;
            }
            chart2.Series.Clear();
            for (int i = 9; i < 14; i++)
            {
                Series series = new Series();
                series.Name = lendname2[i - 9];
                series.ChartType = SeriesChartType.Column;
                for (int j = 0; j < gzTable.Rows.Count; j++)
                {
                    series.Points.AddXY(jobtitle[j], Convert.ToInt32(gzTable.Rows[j][i].ToString()));
                }
                chart2.Series.Add(series);
            }
            foreach (Legend item in chart2.Legends)
            {
                item.Docking = Docking.Top;
            }
        }

        DataTable table;
        private void loadManager()
        {
            dataGridView1.AllowUserToAddRows = false;
            table = new DataTable();
            DataColumn column1 = new DataColumn("职称");
            DataColumn column2 = new DataColumn("职称工资");
            DataColumn column3 = new DataColumn("薪资");
            DataColumn column4 = new DataColumn("调增津贴");
            DataColumn column5 = new DataColumn("职务补贴");
            DataColumn column6 = new DataColumn("地方补贴");
            DataColumn column7 = new DataColumn("考核补贴");
            DataColumn column8 = new DataColumn("住房补贴");
            DataColumn column9 = new DataColumn("公积金");
            DataColumn column10 = new DataColumn("养老保险");
            DataColumn column11 = new DataColumn("事业保险");
            DataColumn column12 = new DataColumn("医疗保险");
            DataColumn column13 = new DataColumn("职业年金");
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
            initView(new mageorderTableAdapter().GetData());
        }

        private void initView(DataTable dataTable)
        {
            dataGridView1.DataSource = null;
            table.Rows.Clear();
            DataRow row;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = table.NewRow();
                row[0] = dataTable.Rows[i]["jobtitle"].ToString();
                row[1] = dataTable.Rows[i]["amount"].ToString();
                row[2] = dataTable.Rows[i]["salary"].ToString();
                row[3] = dataTable.Rows[i]["addwage"].ToString();
                row[4] = dataTable.Rows[i]["workwage"].ToString();
                row[5] = dataTable.Rows[i]["locationwage"].ToString();
                row[6] = dataTable.Rows[i]["assesswage"].ToString();
                row[7] = dataTable.Rows[i]["houswwage"].ToString();
                row[8] = dataTable.Rows[i]["providentfund"].ToString();
                row[9] = dataTable.Rows[i]["pension"].ToString();
                row[10] = dataTable.Rows[i]["unemployment"].ToString();
                row[11] = dataTable.Rows[i]["medical"].ToString();
                row[12] = dataTable.Rows[i]["yearwage"].ToString();
                table.Rows.Add(row);
            }
            dataGridView1.DataSource = table;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "CSV|*.csv|所有文件|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Thread thread = new Thread(LoadCSV);
                thread.IsBackground = true;
                thread.Start(fileDialog.FileName);
            }
        }
        string[] columnsname = {"jobtitle","amount","salary"
                ,"addwage","workwage","locationwage"
                ,"assesswage","houswwage","providentfund"
                ,"pension","unemployment","medical","yearwage"};
        private void LoadCSV(object path)
        {
            string csvPath = (string)path;
            if (csvPath.Substring(csvPath.LastIndexOf('.')) == ".csv")
            {
                try
                {
                    using (System.IO.StreamReader stream = new System.IO.StreamReader(csvPath, Encoding.Default))
                    {
                        string[] arr;
                        string strline = "";
                        bool isFirst = true;
                        mageorderTableAdapter mageorderTableAdapter = new mageorderTableAdapter();
                        while ((strline = stream.ReadLine()) != null)
                        {
                            if (isFirst)
                            {
                                isFirst = false;
                                arr = strline.Split(',');
                                for (int i = 0; i < arr.Length; i++)
                                {
                                    if (arr[i] != columnsname[i])
                                    {
                                        MessageBox.Show("CSV文件列标题不符合规定");
                                        return;
                                    }
                                }
                                mageorderTableAdapter.DeleteQuery();
                            }
                            else
                            {
                                arr = strline.Split(',');
                                mageorderTableAdapter.Insert(arr[0], Convert.ToDouble(arr[1])
                                    , Convert.ToDouble(arr[2]), Convert.ToDouble(arr[3]), Convert.ToDouble(arr[4])
                                    , Convert.ToDouble(arr[4]), Convert.ToDouble(arr[5]), Convert.ToDouble(arr[6])
                                    , Convert.ToDouble(arr[7]), Convert.ToDouble(arr[8]), Convert.ToDouble(arr[9])
                                    , Convert.ToDouble(arr[10]), Convert.ToDouble(arr[11]));
                            }
                        }
                    }
                    initView(new mageorderTableAdapter().GetData());
                    MessageBox.Show("设置成功");
                }
                catch (System.IO.IOException) { MessageBox.Show("文件读取失败"); }
            }
            else
            {
                MessageBox.Show("您选择的不是有效的CSV文件");
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.UseAntiAlias = true;
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Show();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            setMyChart(checkBox1, chart1);
        }

        private void setMyChart(CheckBox checkBox,Chart chart)
        {
            string name = checkBox.Text;
            if (checkBox.Checked)
            {
                foreach (Series item in chart.Series)
                {
                    if (item.Name == name)
                    {
                        item.Enabled = true;
                    }
                }
            }
            else
            {
                foreach (Series item in chart.Series)
                {
                    if (item.Name == name)
                    {
                        item.Enabled = false;
                    }
                }

            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            setMyChart(checkBox2, chart1);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            setMyChart(checkBox3, chart1);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            setMyChart(checkBox4, chart1);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            setMyChart(checkBox5, chart1);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            setMyChart(checkBox6, chart1);
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            setMyChart(checkBox7, chart1);
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            setMyChart(checkBox13, chart2);
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            setMyChart(checkBox10, chart2);
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            setMyChart(checkBox11, chart2);
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            setMyChart(checkBox12, chart2);
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            setMyChart(checkBox9, chart2);
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            setMyChart(checkBox14, chart2);
        }
    }
}
