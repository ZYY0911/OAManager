using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.MyInfoSqlTableAdapters;
using OAManager.RetirementSqlTableAdapters;
using OAManager.聘任信息.EmploySqlTableAdapters;
using OAManager.KeepoutSqlTableAdapters;
using OAManager.DeathSqlTableAdapters;
using OAManager.OutpersonSqlTableAdapters;

namespace OAManager.人事管理
{
    public delegate void UpDate();
    public partial class 删除判断 : Form
    {
        string worknum;
        public UpDate upDate;
        public 删除判断(string worknum,UpDate upDate)
        {
            this.upDate = upDate;
            this.worknum = worknum;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                //开除
                DataTable table = new myinfoTableAdapter().GetDataBy1(worknum);
                new keepoutTableAdapter().Insert(dateTimePicker2.Value, textBox2.Text, textBox1.Text
                    , table.Rows[0]["name"].ToString(), table.Rows[0]["sid"].ToString()
                    , Convert.ToDateTime(table.Rows[0]["birth"].ToString())
                    , Convert.ToDateTime(table.Rows[0]["joinwork"].ToString())
                    , table.Rows[0]["sex"].ToString(), table.Rows[0]["nation"].ToString()
                    , table.Rows[0]["birthlocation"].ToString(), table.Rows[0]["marry"].ToString()
                    , table.Rows[0]["political"].ToString(), table.Rows[0]["country"].ToString()
                    , table.Rows[0]["education"].ToString(), table.Rows[0]["photo"].ToString());
                int rwo = new myinfoTableAdapter().DeleteQuery(worknum);
                if (rwo == 1)
                {
                    MessageBox.Show("删除成功");
                    this.DialogResult = DialogResult.OK;
                    upDate();
                }
                else
                {
                    MessageBox.Show("删除失败");
                }

            }
            else if (radioButton2.Checked)
            {
                //退休
                DataTable table = new myinfoTableAdapter().GetDataBy1(worknum);
                new retirementTableAdapter().Insert(dateTimePicker1.Value
                    , table.Rows[0]["name"].ToString(), table.Rows[0]["sid"].ToString()
                    , Convert.ToDateTime(table.Rows[0]["birth"].ToString())
                    , Convert.ToDateTime(table.Rows[0]["joinwork"].ToString())
                    , table.Rows[0]["sex"].ToString(), table.Rows[0]["nation"].ToString()
                    , table.Rows[0]["birthlocation"].ToString(), table.Rows[0]["marry"].ToString()
                    , table.Rows[0]["political"].ToString(), table.Rows[0]["country"].ToString()
                    , table.Rows[0]["education"].ToString(), table.Rows[0]["photo"].ToString());
                int rwo =new myinfoTableAdapter().DeleteQuery(worknum);

                if (rwo==1)
                {
                    MessageBox.Show("删除成功");
                    this.DialogResult = DialogResult.OK;
                    upDate();
                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            }
            else if (radioButton3.Checked)
            {
                //亡故
                DataTable table = new myinfoTableAdapter().GetDataBy1(worknum);
                new deathTableAdapter().Insert(dateTimePicker4.Value, textBox3.Text
                    , table.Rows[0]["name"].ToString(), table.Rows[0]["sid"].ToString()
                    , Convert.ToDateTime(table.Rows[0]["birth"].ToString())
                    , Convert.ToDateTime(table.Rows[0]["joinwork"].ToString())
                    , table.Rows[0]["sex"].ToString(), table.Rows[0]["nation"].ToString()
                    , table.Rows[0]["birthlocation"].ToString(), table.Rows[0]["marry"].ToString()
                    , table.Rows[0]["political"].ToString(), table.Rows[0]["country"].ToString()
                    , table.Rows[0]["education"].ToString(), table.Rows[0]["photo"].ToString());
                int rwo = new myinfoTableAdapter().DeleteQuery(worknum);
                if (rwo == 1)
                {
                    MessageBox.Show("删除成功");
                    this.DialogResult = DialogResult.OK;
                    upDate();
                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            }
            else if (radioButton4.Checked)
            {
                DataTable table = new myinfoTableAdapter().GetDataBy1(worknum);
                new outpersonTableAdapter().Insert(dateTimePicker3.Value, textBox4.Text
                    , table.Rows[0]["name"].ToString(), table.Rows[0]["sid"].ToString()
                    , Convert.ToDateTime(table.Rows[0]["birth"].ToString())
                    , Convert.ToDateTime(table.Rows[0]["joinwork"].ToString())
                    , table.Rows[0]["sex"].ToString(), table.Rows[0]["nation"].ToString()
                    , table.Rows[0]["birthlocation"].ToString(), table.Rows[0]["marry"].ToString()
                    , table.Rows[0]["political"].ToString(), table.Rows[0]["country"].ToString()
                    , table.Rows[0]["education"].ToString(), table.Rows[0]["photo"].ToString());
                int rwo = new myinfoTableAdapter().DeleteQuery(worknum);
                if (rwo == 1)
                {
                    MessageBox.Show("删除成功");
                    this.DialogResult = DialogResult.OK;
                    upDate();
                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            }
            else
            {
                MessageBox.Show("请选择操作");
                return;
            }
            new employmentinfoTableAdapter().DeleteQuery(worknum);
            this.Close();
        }

        private void 删除判断_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                panel1.Visible = true;
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                panel1.Visible = false;
                panel2.Visible = true;
                panel3.Visible = false;
                panel4.Visible = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                panel3.Visible = true;
                panel2.Visible = false;
                panel1.Visible = false;
                panel4.Visible = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                panel3.Visible = false;
                panel2.Visible = false;
                panel1.Visible = false;
                panel4.Visible = true;
            }
        }
    }
}
