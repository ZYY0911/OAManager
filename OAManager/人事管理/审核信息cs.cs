using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OAManager.人事管理
{
    public partial class 审核信息cs : Form
    {
        int lx;
        string id;
        public 审核信息cs(int lx,string id)
        {
            this.id = id;
            this.lx = lx;
            InitializeComponent();
        }

        private void 审核信息cs_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
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
            if (radioButton1.Checked)
            {
                if (lx==1)
                {
                    using(WinninginfoSqlTableAdapters.winninginfoTableAdapter winninginfo = new WinninginfoSqlTableAdapters.winninginfoTableAdapter())
                    {
                      int row =  winninginfo.UpdateQuery(3, LogName.name, "", Convert.ToInt16(id));
                        if (row==1)
                        {
                            MessageBox.Show("修改成功");
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("修改失败");
                        }
                    }
                }
                else 
                {
                    using(TraininginfoSqlTableAdapters.traininginfoTableAdapter traininginfo = new TraininginfoSqlTableAdapters.traininginfoTableAdapter())
                    {
                        int row = traininginfo.UpdateQuery( LogName.name,1, "", Convert.ToInt16(id));
                        if (row == 1)
                        {
                            MessageBox.Show("修改成功");
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("修改失败");
                        }
                    }
                }
            }
            else if (radioButton2.Checked)
            {
                if (lx == 1)
                {
                    using (WinninginfoSqlTableAdapters.winninginfoTableAdapter winninginfo = new WinninginfoSqlTableAdapters.winninginfoTableAdapter())
                    {
                        int row = winninginfo.UpdateQuery(2, LogName.name, textBox1.Text, Convert.ToInt16(id));
                        if (row == 1)
                        {
                            MessageBox.Show("修改成功");
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("修改失败");
                        }
                    }
                }
                else
                {
                    using (TraininginfoSqlTableAdapters.traininginfoTableAdapter traininginfo = new TraininginfoSqlTableAdapters.traininginfoTableAdapter())
                    {
                        int row = traininginfo.UpdateQuery(LogName.name, 1, textBox1.Text, Convert.ToInt16(id));
                        if (row == 1)
                        {
                            MessageBox.Show("修改成功");
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("修改失败");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择");
            }
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
