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
using OAManager.NewsSqlTableAdapters;

namespace OAManager.部门领导
{
    public partial class 查看审核 : Form
    {
        int notnum;
        public 查看审核(string notnum)
        {
            this.notnum = Convert.ToInt16(notnum);
            InitializeComponent();
        }
        DataTable table;
        private void 查看审核_Load(object sender, EventArgs e)
        {
            Console.WriteLine(notnum);
            panel1.Visible = false;
            radioButton1.Checked = true;
            table = new newsTableAdapter().GetDataBy1(notnum);
            textBox1.Text = table.Rows[0]["title"].ToString();
            textBox2.Text = table.Rows[0]["context"].ToString();
            comboBox2.Text = table.Rows[0]["releasedepartment"].ToString();
            textBox3.Text = table.Rows[0]["annex"].ToString();
            string path = table.Rows[0]["photo"].ToString();
            if (File.Exists(path))
            {
                pictureBox1.Load(path);
            }
            comboBox1.Text = table.Rows[0]["notifitype"].ToString();
            dateTimePicker1.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            dateTimePicker1.Value = Convert.ToDateTime(table.Rows[0]["date"].ToString());
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

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int row= new newsTableAdapter().UpdateQuery(radioButton1.Checked ? 3 : 2,notnum);
            if (row==1)
            {
                MessageBox.Show("设置成功");
            DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("设置失败");
            }
            this.Close();
        }
    }
}
