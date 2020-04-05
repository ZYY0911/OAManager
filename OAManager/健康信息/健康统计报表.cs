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
using OAManager.健康信息.HealthySqlTableAdapters;

namespace OAManager.健康信息
{
    public partial class 健康统计报表 : Form
    {
        public 健康统计报表()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                panel1.Visible = true;
                panel2.Visible = true;
                panel4.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            string worknum = textBox1.Text;
            string name = textBox2.Text;
            DataTable table = new DataTable1TableAdapter().GetDataBy(name, worknum, dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString());
            if (table.Rows.Count == 0)
            {
                MessageBox.Show("没有查询到此人的数据");
                return;
            }
            Dictionary<DateTime, double> keys = new Dictionary<DateTime, double>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                keys[Convert.ToDateTime(table.Rows[i]["date"].ToString().Split(' ')[0])] = Convert.ToDouble(table.Rows[i]["bodytemperature"].ToString());

            }
            var dicSort = from objDic in keys orderby objDic.Key descending select objDic;
            int k = 0;
            foreach (KeyValuePair<DateTime, double> item in dicSort)
            {
                chart1.Series[0].Points.AddXY(item.Key.ToString(), item.Value);
                chart1.Series[0].Points[k].IsValueShownAsLabel = true;
                chart1.Series[0].Points[k].Label = item.Value + "";
                k++;
            }
            chart1.Legends[0].Enabled = false;
            chart1.Titles.Clear();
            chart1.Titles.Add("职工健康数据统计图");


        }

        private void 健康统计报表_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            chart2.Series.Clear();

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                panel1.Visible = false;
                panel2.Visible = false;
                panel4.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart2.Series.Clear();
            Series series = new Series();
            series.ChartType = SeriesChartType.Pie;
            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Black";
            Dictionary<string, int> keys = new Dictionary<string, int>();
            keys["异常"] = 0;
            keys["正常"] = 0;
            DataTable healthy = new healthyinfoTableAdapter().GetDataBy3(dateTimePicker3.Value.ToString());
            for (int i = 0; i < healthy.Rows.Count; i++)
            {
                double wd = Convert.ToDouble(healthy.Rows[i]["bodytemperature"].ToString());
                if (wd > 37.2)
                {
                    keys["异常"] = keys["异常"] + 1;
                }
                else
                {
                    keys["正常"] = keys["正常"] + 1;
                }
            }
            foreach (KeyValuePair<string, int> item in keys)
            {
                series.Points.AddXY(item.Key, item.Value);
            }
            series.Label = "#VALX;#PERCENT{p2}";
            chart2.Series.Add(series);
            chart2.Titles.Clear();
            chart2.Titles.Add("体温异常统计图");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart3.Series.Clear();

            Series series = new Series();

            DataTable healthy = new healthyinfoTableAdapter().GetDataBy3(dateTimePicker4.Value.ToString());
            Dictionary<string, int> keys = new Dictionary<string, int>();
            keys["感冒"] = 0;
            keys["发热"] = 0;
            keys["外伤"] = 0;
            keys["慢性病"] = 0;
            keys["其他"] = 0;
            for (int i = 0; i < healthy.Rows.Count; i++)
            {
                double wd = Convert.ToDouble(healthy.Rows[i]["bodytemperature"].ToString());
                if (wd > 37.2)
                {
                    string dir = healthy.Rows[i]["description"].ToString();
                    if (dir=="")
                    {
                        dir = "其他";
                    }
                    keys[dir] = keys[dir] + 1;
                }
            }
            foreach (KeyValuePair<string, int> item in keys)
            {
                series.Points.AddXY(item.Key, item.Value);
            }
            chart3.Series.Add(series);
            chart3.Legends[0].Enabled = false;
            chart3.Series[0].Label = "#VALY";
            chart3.Titles.Clear();
            chart3.Titles.Add("异常类别统计图");
        }
        /// <summary>
        /// 打印Chart2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            chart2.SaveImage("chart2.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            printPreviewDialog1.UseAntiAlias = true;
            printPreviewDialog1.Document = printDocument2;
            printPreviewDialog1.ShowDialog();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                Image image = Image.FromFile("chart1.bmp");
                e.Graphics.DrawImage(image, 50, 10);
            }
            catch
            {
                MessageBox.Show("文件或打印机怎在使用中,请稍后重试");
            }
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                Image image = Image.FromFile("chart2.bmp");
                e.Graphics.DrawImage(image, 50, 10);
            }
            catch
            {
                MessageBox.Show("文件或打印机怎在使用中,请稍后重试");
            }
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                Image image = Image.FromFile("chart3.bmp");
                e.Graphics.DrawImage(image, 50, 10);
            }
            catch
            {
                MessageBox.Show("文件或打印机怎在使用中,请稍后重试");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            chart3.SaveImage("chart3.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            printPreviewDialog1.UseAntiAlias = true;
            printPreviewDialog1.Document = printDocument3;
            printPreviewDialog1.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            chart1.SaveImage("chart1.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            printPreviewDialog1.UseAntiAlias = true;
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
    }
}
