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
using OAManager.ContactinfoSqlTableAdapters;
using OAManager.SpecialinfoSqlTableAdapters;

namespace OAManager.个人基本信息
{
    public partial class MyBaseInfo : Form
    {
        public MyBaseInfo()
        {
            InitializeComponent();
        }
        string myPhotoPath;
        private void MyBaseInfo_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'baseSql.education' table. You can move, or remove it, as needed.
            this.educationTableAdapter.Fill(this.baseSql.education);
            // TODO: This line of code loads data into the 'baseSql.political' table. You can move, or remove it, as needed.
            this.politicalTableAdapter.Fill(this.baseSql.political);
            loadJbxx();
            loadLxxx();
            loadTcxx();
        }
        DataTable tcxxTable;
        string[] lifePhoto;
        List<string> mylistphoto = new List<string>();
        private void loadTcxx()
        {
            tcxxTable = new specialinfoTableAdapter().GetDataBy(AppInfo.worknum);
            if (tcxxTable.Rows.Count > 0)
            {
                textBox20.Text = tcxxTable.Rows[0]["major"].ToString();
                textBox21.Text = tcxxTable.Rows[0]["specialty"].ToString();
                textBox23.Text = tcxxTable.Rows[0]["myinfo"].ToString();
                textBox22.Text = tcxxTable.Rows[0]["speinfo"].ToString();
                lifePhoto = tcxxTable.Rows[0]["lifephoto"].ToString().Split('#');
                listView1.View = View.LargeIcon;
                for (int i = 0; i < lifePhoto.Length; i++)
                {
                    if (File.Exists(lifePhoto[i]))
                    {
                        mylistphoto.Add(lifePhoto[i]);

                    }
                }
                LoadImageList();
            }
        }

        private void LoadImageList()
        {

            this.listView1.Items.Clear();
            this.listView1.Columns.Clear();
            
            imageList1.Images.Clear();
            for (int i = 0; i < mylistphoto.Count; i++)
            {
                this.imageList1.Images.Add(Image.FromFile(mylistphoto[i]));
            }
            //this.listView1.Columns.Add("", 220, HorizontalAlignment.Left);
            this.listView1.LargeImageList = imageList1;
            for (int i = 0; i < mylistphoto.Count; i++)
            {
                
                listView1.Items.Add("照片"+(i+1));
                listView1.Items[i].ImageIndex = i;
            }
        }

        DataTable lxxxTable;
        private void loadLxxx()
        {
            lxxxTable = new contactinfoTableAdapter().GetDataBy(AppInfo.worknum);
            if (lxxxTable.Rows.Count > 0)
            {
                textBox6.Text = lxxxTable.Rows[0]["officetel"].ToString();
                textBox7.Text = lxxxTable.Rows[0]["mobiletel"].ToString();
                textBox8.Text = lxxxTable.Rows[0]["email"].ToString();
                textBox9.Text = lxxxTable.Rows[0]["incount"].ToString();
                textBox10.Text = lxxxTable.Rows[0]["postcode"].ToString();
                textBox11.Text = lxxxTable.Rows[0]["qq"].ToString();
                textBox12.Text = lxxxTable.Rows[0]["faxnumber"].ToString();
                textBox13.Text = lxxxTable.Rows[0]["address"].ToString();
                textBox15.Text = lxxxTable.Rows[0]["accounttyoe"].ToString();
                textBox14.Text = lxxxTable.Rows[0]["accountoffice"].ToString();
                textBox17.Text = lxxxTable.Rows[0]["accountaddress"].ToString();
                textBox16.Text = lxxxTable.Rows[0]["detaddress"].ToString();
                textBox19.Text = lxxxTable.Rows[0]["emergencycontact"].ToString();
                textBox18.Text = lxxxTable.Rows[0]["emergencytel"].ToString();

            }
        }

        DataTable myUserInfo;
        private void loadJbxx()
        {
            myUserInfo = new myinfoTableAdapter().GetDataBy1(AppInfo.worknum);
            label3.Text = AppInfo.worknum;
            textBox1.Text = myUserInfo.Rows[0]["name"].ToString();
            textBox2.Text = myUserInfo.Rows[0]["sid"].ToString();
            dateTimePicker1.Value = Convert.ToDateTime(myUserInfo.Rows[0]["birth"].ToString());
            dateTimePicker2.Value = Convert.ToDateTime(myUserInfo.Rows[0]["joinwork"].ToString());
            comboBox1.Text = myUserInfo.Rows[0]["sex"].ToString();
            textBox3.Text = myUserInfo.Rows[0]["nation"].ToString();
            textBox4.Text = myUserInfo.Rows[0]["birthlocation"].ToString();
            comboBox2.Text = myUserInfo.Rows[0]["marry"].ToString();
            comboBox3.Text = myUserInfo.Rows[0]["political"].ToString();
            textBox5.Text = myUserInfo.Rows[0]["country"].ToString();
            comboBox4.Text = myUserInfo.Rows[0]["education"].ToString();
            myPhotoPath = myUserInfo.Rows[0]["photo"].ToString();
            if (File.Exists(myPhotoPath))
            {
                pictureBox1.Load(myPhotoPath);
            }
            else
            {
                pictureBox1.Image = Properties.Resources.暂无此人;
            }
        }
        /// <summary>
        /// 基本信息保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要保存修改后的内容吗", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int row = new myinfoTableAdapter().UpdateQuery(textBox1.Text, textBox2.Text.ToString()
                    , dateTimePicker1.Text, dateTimePicker2.Text, comboBox1.Text.ToString()
                    , textBox3.Text.ToString(), textBox4.Text.ToString(), comboBox2.Text.ToString()
                    , comboBox3.Text.ToString(), textBox5.Text, comboBox4.Text
                    , myPhotoPath, AppInfo.worknum);
                if (row == 1)
                {
                    MessageBox.Show("修改成功");
                }
                else
                {
                    MessageBox.Show("修改失败，请稍后重试");
                }
            }
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要保存修改后的内容吗", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int row = new contactinfoTableAdapter().UpdateQuery(textBox6.Text, textBox7.Text
                     , textBox9.Text, textBox8.Text, textBox11.Text, textBox10.Text, textBox12.Text,
                     textBox15.Text, textBox14.Text, textBox17.Text, textBox16.Text,
                     textBox19.Text, textBox18.Text, AppInfo.worknum);
                if (row == 1)
                {
                    MessageBox.Show("修改成功");
                }
                else
                {
                    MessageBox.Show("修改失败，请稍后重试");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "JPG|*.jpg|PNG|*.png|所有文件|*.*";
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] fil = fileDialog.FileNames;
                for (int i = 0; i < fil.Length; i++)
                {
                    mylistphoto.Add(fil[i]);
                }
                LoadImageList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要保存修改后的内容吗", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //textBox20.Text = tcxxTable.Rows[0]["major"].ToString();
                //textBox21.Text = tcxxTable.Rows[0]["specialty"].ToString();
                //textBox23.Text = tcxxTable.Rows[0]["myinfo"].ToString();
                //textBox22.Text = tcxxTable.Rows[0]["speinfo"].ToString();
                //lifePhoto = tcxxTable.Rows[0]["lifephoto"].ToString().Split('#');\
                string lifesphoto = "";
                for (int i = 0; i < mylistphoto.Count; i++)
                {
                    if (i==0)
                    {
                        lifesphoto = mylistphoto[i];
                    }
                    else
                    {
                        lifesphoto+="#"+ mylistphoto[i];
                    }
                }
                int row = new specialinfoTableAdapter().UpdateQuery(textBox20.Text
                    ,textBox21.Text,textBox22.Text
                    ,textBox23.Text, lifesphoto,AppInfo.worknum);
                if (row == 1)
                {
                    MessageBox.Show("修改成功");
                }
                else
                {
                    MessageBox.Show("修改失败，请稍后重试");
                }
            }
        }
    }
}
