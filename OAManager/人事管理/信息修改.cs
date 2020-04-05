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

namespace OAManager.人事管理
{
    public partial class 信息修改 : Form
    {
        string worknum;
        public 信息修改(string worknum)
        {
            this.worknum = worknum;
            InitializeComponent();
        }
        DataTable table;
        string photoPath = "";
        private void 信息修改_Load(object sender, EventArgs e)
        {
            table = new myinfoTableAdapter().GetDataBy1(worknum);
            textBox1.Text = worknum;
            textBox2.Text = table.Rows[0]["name"].ToString();
            textBox3.Text = table.Rows[0]["sid"].ToString();
            dateTimePicker2.Value = Convert.ToDateTime(table.Rows[0]["birth"].ToString());
            dateTimePicker1.Value = Convert.ToDateTime(table.Rows[0]["joinwork"].ToString());
            comboBox1.Text = table.Rows[0]["sex"].ToString();
            textBox5.Text = table.Rows[0]["nation"].ToString();
            textBox6.Text = table.Rows[0]["birthlocation"].ToString();
            comboBox2.Text = table.Rows[0]["marry"].ToString();
            comboBox3.Text = table.Rows[0]["political"].ToString();
            textBox7.Text = table.Rows[0]["country"].ToString();
            comboBox4.Text = table.Rows[0]["education"].ToString();
            photoPath = table.Rows[0]["photo"].ToString();
            if (File.Exists(photoPath))
            {
                pictureBox1.Load(photoPath);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "JPG|*.jpg|PNG|*.png|所有文件|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                photoPath = fileDialog.FileName;
                pictureBox1.Load(photoPath);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string worknum = textBox1.Text;
            string name = textBox2.Text;
            string sid = textBox3.Text;
            string jointime = dateTimePicker1.Value.ToString();
            string sex = comboBox1.Text;
            string nation = textBox5.Text;
            string birth = textBox6.Text;
            string marry = comboBox2.Text;
            string political = comboBox3.Text;
            string country = textBox7.Text;
            string education = comboBox4.Text;
            int row = new myinfoTableAdapter().UpdateQuery( name, sid, dateTimePicker2.Value.ToString()
                , dateTimePicker1.Value.ToString()
                , sex, nation, birth, marry, political, country, education, photoPath,worknum);
            if (row == 1)
            {
                DialogResult = DialogResult.OK;
                MessageBox.Show("修改成功");
            }
            else
            {
                MessageBox.Show("修改失败，请稍后重试");
            }
            this.Close();
        }
    }
}
