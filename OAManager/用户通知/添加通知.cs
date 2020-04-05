using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.NewsSqlTableAdapters;

namespace OAManager.用户通知
{
    public partial class 添加通知 : Form
    {
        public 添加通知()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "所有文件|*.*";
            if (fileDialog.ShowDialog()==DialogResult.OK)
            {
                textBox3.Text = fileDialog.FileName;
            }
        }
        string photopath = "";
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "图片|*.png;*.jpg;*.bmp;*.gif|所有文件|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                photopath = fileDialog.FileName;
               //pictureBox1.Load(photopath);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int row=  new newsTableAdapter().Insert(textBox1.Text
                , richTextBox1.Text, photopath
                , comboBox2.Text, LogName.name, textBox3.Text
                , 1, comboBox1.Text, dateTimePicker1.Value);
            if (row==1)
            {
                MessageBox.Show("添加成功,请等待领导审核!");
            }
            else
            {
                MessageBox.Show("添加失败");
            }
            this.Close();
        }

        private void 添加通知_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'departmentSql.department' table. You can move, or remove it, as needed.
            this.departmentTableAdapter.Fill(this.departmentSql.department);

        }

        private void 插入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "图片文件|*.jpg|所有文件|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                photopath = openFileDialog1.FileName;
                Clipboard.SetDataObject(Image.FromFile(openFileDialog1.FileName), false);
                richTextBox1.Paste();
            }
        }
    }
}
