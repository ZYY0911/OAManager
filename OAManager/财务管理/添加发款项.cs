using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.MyInfoSqlTableAdapters;
using OAManager.财务管理.OtherwageSqlTableAdapters;

namespace OAManager.财务管理
{
    public partial class 添加发款项 : Form
    {
        public 添加发款项()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string worknum = textBox1.Text;
            if (worknum == "")
            {
                MessageBox.Show("请输入工号");
                button2.Enabled = false;
                button3.Enabled = false;
                return;
            }
            DataTable myInfo = new myinfoTableAdapter().GetDataBy1(worknum);
            if (myInfo.Rows.Count == 0)
            {
                MessageBox.Show("未找到此人");
                button2.Enabled = false;
                button3.Enabled = false;
                return;
            }
            label3.Text = myInfo.Rows[0]["name"].ToString();
            label4.Text = myInfo.Rows[0]["sex"].ToString();
            label10.Text = myInfo.Rows[0]["political"].ToString();
            label12.Text = myInfo.Rows[0]["education"].ToString();
            label8.Text = myInfo.Rows[0]["sid"].ToString();
            string photot = myInfo.Rows[0]["photo"].ToString();
            if (File.Exists(photot))
            {
                pictureBox1.Load(photot);
            }
            else
            {
                pictureBox1.Image = Properties.Resources.暂无此人;
            }
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void 添加发款项_Load(object sender, EventArgs e)
        {

        }
        List<UserControl> MyControls = new List<UserControl>();
        List<UserControl> MyControls2 = new List<UserControl>();
        private void button2_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Size = new Size(450, 154);
            UserControl userControl = new 添加发款(DeleteMy, panel);
            MyControls.Add(userControl);
            panel.Controls.Add(userControl);
            this.panel7.Controls.Add(panel);
        }

        private void DeleteMy(Panel panel)
        {
            MyControls.Remove(panel.Controls[0] as UserControl);
            panel7.Controls.Remove(panel);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < MyControls.Count; i++)
                {
                    添加发款 m = MyControls[i] as 添加发款;
                    new otherwageTableAdapter().Insert(m.MycomboBox1.Text
                        , Convert.ToDouble(m.MytextBox.Text.ToString())
                        , textBox1.Text.ToString(), DateTime.Now.Year + "", DateTime.Now.Month + "");
                }
                MessageBox.Show("保存成功");
            }
            catch
            {
                MessageBox.Show("保存失败");
            }
        }
        /// <summary>
        /// 保存扣款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < MyControls2.Count; i++)
                {
                    添加扣款 m = MyControls2[i] as 添加扣款;
                    new otherwage2TableAdapter().Insert(m.MycomboBox1.Text
                        , Convert.ToDouble(m.MytextBox.Text.ToString())
                        , textBox1.Text.ToString(), DateTime.Now.Year + "", DateTime.Now.Month + "");
                }
                MessageBox.Show("保存成功");
            }
            catch
            {
                MessageBox.Show("保存失败");
            }
        }
        /// <summary>
        /// 添加扣款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Size = new Size(450, 154);
            UserControl userControl = new 添加扣款(DeleteMy2, panel);
            MyControls.Add(userControl);
            panel.Controls.Add(userControl);
            this.panel8.Controls.Add(panel);
        }

        private void DeleteMy2(Panel panel)
        {
            MyControls2.Remove(panel.Controls[0] as UserControl);
            panel8.Controls.Remove(panel);
        }
    }
}
