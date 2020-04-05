using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OAManager.健康信息
{
    public partial class AddHealthyInfo : Form
    {
        Form form;
        public AddHealthyInfo(Form form)
        {
            this.form = form;
            InitializeComponent();
        }

        private void AddHealthyInfo_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            label3.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                panel1.Visible = true;
            }
            else
            {
                panel1.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            if (text == "")
            {
                MessageBox.Show("请输入体温");
                return;
            }
            if (radioButton1.Checked | radioButton2.Checked)
            {
                string errorinfo = "";
                if (radioButton1.Checked)
                {
                    if (radioButton3.Checked)
                    {
                        errorinfo = radioButton3.Text;
                    }
                    else if (radioButton4.Checked)
                    {
                        errorinfo = radioButton4.Text;
                    }
                    else if (radioButton5.Checked)
                    {
                        errorinfo = radioButton5.Text;
                    }
                    else if (radioButton6.Checked)
                    {
                        errorinfo = radioButton6.Text;
                    }
                    else if (radioButton7.Checked)
                    {
                        errorinfo = radioButton7.Text;
                    }
                    else
                    {
                        MessageBox.Show("请选则不适类型");
                        return;
                    }
                }
                if (MessageBox.Show("您确定要提交吗，提交后不可修改", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    using (健康信息.HealthySqlTableAdapters.healthyinfoTableAdapter healthyinfo = new HealthySqlTableAdapters.healthyinfoTableAdapter())
                    {
                        int row = healthyinfo.InsertQuery(AppInfo.worknum, label3.Text
                              , textBox1.Text, radioButton1.Checked ? 1 : 0, errorinfo,
                              new MyInfoSqlTableAdapters.myinfoTableAdapter()
                              .GetDataBy1(AppInfo.worknum).Rows[0]["name"].ToString());
                        if (row==1)
                        {
                            MessageBox.Show("提交成功");
                            form.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("提交失败，请重试");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择有无不适");
            }

        }
    }
}
