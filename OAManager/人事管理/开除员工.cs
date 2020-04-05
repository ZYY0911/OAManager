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
using OAManager.KeepoutSqlTableAdapters;

namespace OAManager.人事管理
{
    public partial class 开除员工 : Form
    {
        string worknum;
        public UpDate upDate;
        public 开除员工(string worknum, UpDate upDate)
        {
            this.upDate = upDate;
            this.worknum = worknum;
            InitializeComponent();
        }

        private void 开除员工_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //退休
            DataTable table = new myinfoTableAdapter().GetDataBy1(worknum);
            new keepoutTableAdapter().Insert(DateTime.Now,textBox2.Text,textBox1.Text
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
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "PDF|*.pdf|Word|*doc|图片|*.jpg;*.bmp;*.png;*.gif|所有文件|*.*";
            if (fileDialog.ShowDialog()==DialogResult.OK)
            {
                textBox2.Text = fileDialog.FileName;
            }
        }
    }
}
