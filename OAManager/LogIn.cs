using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.UserLogSqlTableAdapters;

namespace OAManager
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            textBox1.Text = "100002";
            textBox2.Text = "100002";
            initCode();
            textBox3.Text = nowstr;
        }
        string str = "QWERTYUIOP1234567890ASDFGHJKL1234567890ZXCVBNM";
        string nowstr = "";
        private void initCode()
        {
            Bitmap bitmap = new Bitmap(174, 50);
            Random random = new Random();
            Graphics graphics = Graphics.FromImage(bitmap);

            for (int i = 0; i < 4; i++)
            {
                nowstr += str[random.Next(0, str.Length)];
            }
            string[] fonts = { "微软雅黑", "宋体", "仿宋", "华文隶书", "黑体" };
            for (int i = 0; i < 4; i++)
            {
                graphics.DrawString(nowstr[i].ToString(), new Font(fonts[random.Next(0, 4)], 20, FontStyle.Regular)
                    , Brushes.Black, new Point(i * 20, 0));
            }
            for (int i = 0; i < 2000; i++)
            {
                bitmap.SetPixel(random.Next(0, bitmap.Width), random.Next(0, bitmap.Height), Color.Gray);
            }
            pictureBox1.Image = bitmap;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            initCode();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string pass = textBox2.Text;
            string yzm = textBox3.Text.ToString().ToUpper();
            Console.WriteLine(yzm + "---" + nowstr);
            if ("" == username)
            {
                MessageBox.Show("请输入验证码");
                return;
            }
            if ("" == pass)
            {
                MessageBox.Show("请输入密码");
                return;
            }
            if (yzm != nowstr)
            {
                MessageBox.Show("验证码输入不正确");
                return;
            }
            DataTable tables = new usermanagerTableAdapter().GetDataBy(username, pass);
            if (tables.Rows.Count == 0)
            {
                MessageBox.Show("用户名或密码不正确");
                return;
            }
            else
            {
                Form form = null;
                AppInfo.worknum = username;
                string type = tables.Rows[0]["type"].ToString();
                AppInfo.type = Convert.ToInt16(type);
                form = new Model1.UserManager();
                if (form != null)
                {
                    using (健康信息.HealthySqlTableAdapters.healthyinfoTableAdapter healthyinfo = new 健康信息.HealthySqlTableAdapters.healthyinfoTableAdapter())
                    {
                        DataTable healthyTable = healthyinfo.GetDataBy1(username, DateTime.Now.ToString("yyyy-MM-dd"));
                        if (healthyTable.Rows.Count == 0)
                        {
                            健康信息.AddHealthyInfo add = new 健康信息.AddHealthyInfo(form);
                            add.Show();
                        }
                        else
                        {
                            form.Show();
                        }
                    }
                    this.Hide();
                }
            }
        }
    }
}
