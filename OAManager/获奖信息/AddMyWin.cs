using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.获奖信息.WinSqlTableAdapters;

namespace OAManager.获奖信息
{
    public delegate void EndLoad();
    public partial class AddMyWin : Form
    {
        public EndLoad endLoad;
        public AddMyWin(EndLoad endLoad)
        {
            this.endLoad = endLoad;
            InitializeComponent();
        }
        string myPhotoPath;
        private void AddMyWin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'winSql.wintype' table. You can move, or remove it, as needed.
            this.wintypeTableAdapter.Fill(this.winSql.wintype);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "JPG|*.jpg|PNG|*.png|所有文件|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                myPhotoPath = fileDialog.FileName;
                pictureBox1.Load(myPhotoPath);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string type = comboBox1.Text;
            string name = textBox1.Text;
            string level = textBox2.Text;
            string order = textBox2.Text;
            string rank = textBox3.Text;
            int row =new winninginfoTableAdapter().InsertQuery(AppInfo.worknum
                , type, name, level, order, dateTimePicker1.Value.ToString(), rank, myPhotoPath, LogName.name, "", 1, "");
            if (row==1)
            {
                MessageBox.Show("添加成功");
                endLoad();
                this.Close();
            }
            else
            {
                MessageBox.Show("添加失败，请重试");
            }
        }
    }
}
