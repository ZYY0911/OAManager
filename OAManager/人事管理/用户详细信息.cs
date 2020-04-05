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
using OAManager.获奖信息.WinSqlTableAdapters;
using employmentinfoTableAdapter = OAManager.聘任信息.EmploySqlTableAdapters.employmentinfoTableAdapter;

namespace OAManager.人事管理
{
    public partial class 用户详细信息 : Form
    {
        string worknum;
        public 用户详细信息(string worknum)
        {
            this.worknum = worknum;
            InitializeComponent();
        }
        DataTable table;
        string photoPath = "";
        DataTable lxxxTable;
        private void 用户详细信息_Load(object sender, EventArgs e)
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
            loadLxxx();
        }

        private void loadLxxx()
        {
            lxxxTable = new contactinfoTableAdapter().GetDataBy(worknum);
            if (lxxxTable.Rows.Count > 0)
            {
                textBox20.Text = lxxxTable.Rows[0]["officetel"].ToString();
                textBox4.Text = lxxxTable.Rows[0]["mobiletel"].ToString();
                textBox9.Text = lxxxTable.Rows[0]["email"].ToString();
                textBox8.Text = lxxxTable.Rows[0]["incount"].ToString();
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
            loadTcxx();
            loafPyxx();
            loadWin();
        }
        DataTable Wintable;
        private void loadWin()
        {
            dataGridView1.AllowUserToAddRows = false;
            Wintable = new DataTable();
            DataColumn column10 = new DataColumn("类别");
            DataColumn column1 = new DataColumn("名称");
            DataColumn column2 = new DataColumn("级别");
            DataColumn column3 = new DataColumn("等次");
            DataColumn column4 = new DataColumn("获奖时间");
            DataColumn column5 = new DataColumn("本人排名");
            DataColumn column6 = new DataColumn("证书图片");
            DataColumn column7 = new DataColumn("录入人");
            DataColumn column8 = new DataColumn("审核人");
            DataColumn column9 = new DataColumn("审核状态");
           Wintable.Columns.Add(column10);
           Wintable.Columns.Add(column1);
           Wintable.Columns.Add(column2);
           Wintable.Columns.Add(column3);
           Wintable.Columns.Add(column4);
           Wintable.Columns.Add(column5);
           Wintable.Columns.Add(column6);
           Wintable.Columns.Add(column7);
           Wintable.Columns.Add(column8);
            Wintable.Columns.Add(column9);
            initViewWin(new winninginfoTableAdapter().GetDataBy(AppInfo.worknum));
        }

        private void initViewWin(DataTable dataTable)
        {
            Wintable.Rows.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            DataRow row;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = Wintable.NewRow();
                row[0] = dataTable.Rows[i]["pricetype"].ToString();
                row[1] = dataTable.Rows[i]["pricename"].ToString();
                row[2] = dataTable.Rows[i]["level"].ToString();
                row[3] = dataTable.Rows[i]["order"].ToString();
                row[4] = dataTable.Rows[i]["date"].ToString();
                row[5] = dataTable.Rows[i]["rank"].ToString();
                row[6] = dataTable.Rows[i]["photo"].ToString();
                row[7] = dataTable.Rows[i]["inputperson"].ToString();
                row[8] = dataTable.Rows[i]["reviewer"].ToString();
                string reviewstate = dataTable.Rows[i]["reviewstate"].ToString();
                switch (reviewstate)
                {
                    case "1":
                        row[9] = "审核中";
                        break;
                    case "2":
                        row[9] = "拒绝";
                        break;
                    case "3":
                        row[9] = "通过";
                        break;
                    default:
                        break;
                }
                Wintable.Rows.Add(row);
            }
            dataGridView1.DataSource = Wintable;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "Images";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.Columns["证书图片"].Visible = false;
            dataGridView1.Columns.Insert(dataGridView1.Columns["证书图片"].Index, imageColumn);
            dataGridView1.Columns["Images"].Width = 150;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells["Images"].Value = Image.FromFile(dataGridView1.Rows[i].Cells["证书图片"].Value.ToString());
                dataGridView1.Rows[i].Height = 150;
                if (i % 2 == 0)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.SeaGreen;
                }
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                }
            }
        }

        DataTable emplotTable;
        private void loafPyxx()
        {
            emplotTable = new employmentinfoTableAdapter().GetDataBy(AppInfo.worknum);
            if (emplotTable.Rows.Count == 0)
            {
                label44.Text = "暂无";
                label42.Text = "暂无";
                label40.Text = "暂无";
                label15.Text = "暂无";
            }
            else
            {
                int getRow = emplotTable.Rows.Count - 1;
                label44.Text = emplotTable.Rows[getRow]["jobtitle"].ToString();
                label42.Text = emplotTable.Rows[getRow]["department"].ToString();
                label40.Text = emplotTable.Rows[getRow]["jointime"].ToString().Split(' ')[0];
                label15.Text = emplotTable.Rows[getRow]["overtime"].ToString().Split(' ')[0];
            }
        }

        DataTable tcxxTable;
        string[] lifePhoto;
        List<string> mylistphoto = new List<string>();
        private void loadTcxx()
        {
            tcxxTable = new specialinfoTableAdapter().GetDataBy(worknum);
            if (tcxxTable.Rows.Count > 0)
            {
                textBox24.Text = tcxxTable.Rows[0]["major"].ToString();
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

                listView1.Items.Add("照片" + (i + 1));
                listView1.Items[i].ImageIndex = i;
            }
        }
    }
}
