using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.聘任信息.EmploySqlTableAdapters;

namespace OAManager.人事管理
{
    public partial class 添加应聘 : Form
    {
        string worknum;
        public 添加应聘(string worknum)
        {
            this.worknum = worknum;
            InitializeComponent();
        }

        private void 添加应聘_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'yPSql.department' table. You can move, or remove it, as needed.
            this.departmentTableAdapter.Fill(this.yPSql.department);
            // TODO: This line of code loads data into the 'yPSql.jobtitle' table. You can move, or remove it, as needed.
            this.jobtitleTableAdapter.Fill(this.yPSql.jobtitle);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int row = new employmentinfoTableAdapter().Insert(worknum, comboBox1.Text
                , comboBox2.Text, dateTimePicker1.Value, dateTimePicker2.Value
                , comboBox3.Text);
            if (row == 1)
            {
                DialogResult = DialogResult.OK;
                MessageBox.Show("录入成功");
            }
            else
            {
                MessageBox.Show("录入失败");
            }
            this.Close();
        }
    }
}
