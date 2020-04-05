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
using OAManager.聘任信息.EmploySqlTableAdapters;

namespace OAManager.人事管理
{
    public partial class 员工应聘 : Form
    {
        public 员工应聘()
        {
            InitializeComponent();
        }
        DataTable table;
        string photoPath = "";
        DataTable lxxxTable;
        string worknum = "";
        private void 员工应聘_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
        }

        private void loadGrxx(string name)
        {
            table = new myinfoTableAdapter().GetDataBy5(worknum, name);
            if (table.Rows.Count == 0)
            {
                MessageBox.Show("没有找到此人信息");
                return;
            }
            worknum = table.Rows[0]["worknum"].ToString();
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
            loadYplb();
        }
        DataTable dataGrid;
        private void loadYplb()
        {
            dataGrid = new DataTable();
            DataColumn column1 = new DataColumn("职务");
            DataColumn column2 = new DataColumn("岗位名称");
            DataColumn column3 = new DataColumn("合同类型");
            DataColumn column4 = new DataColumn("开始时间");
            DataColumn column5 = new DataColumn("结束时间");
            DataColumn column6 = new DataColumn("id");
            dataGrid.Columns.Add(column1);
            dataGrid.Columns.Add(column2);
            dataGrid.Columns.Add(column3);
            dataGrid.Columns.Add(column4);
            dataGrid.Columns.Add(column5);
            dataGrid.Columns.Add(column6);
            using (聘任信息.EmploySqlTableAdapters.employmentinfoTableAdapter employmentinfo = new 聘任信息.EmploySqlTableAdapters.employmentinfoTableAdapter())
            {
                DataTable dataTable = employmentinfo.GetDataBy(worknum);
                DataRow row;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    row = dataGrid.NewRow();
                    row[0] = dataTable.Rows[i]["jobtitle"].ToString();
                    row[1] = dataTable.Rows[i]["department"].ToString();
                    row[2] = dataTable.Rows[i]["typename"].ToString();
                    row[3] = dataTable.Rows[i]["jointime"].ToString();
                    row[4] = dataTable.Rows[i]["overtime"].ToString();
                    row[5] = dataTable.Rows[i]["id"].ToString();
                    dataGrid.Rows.Add(row);
                }
                dataGridView1.DataSource = dataGrid;
                dataGridView1.Columns["id"].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            worknum = textBox21.Text;
            string name = textBox22.Text;
            if (worknum == "" && name == "")
            {
                MessageBox.Show("请输入查询消息");
                return;
            }
            loadGrxx(name);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((DataTable)dataGridView1.DataSource).Rows.Add(dataGrid.NewRow());
            
            //if (new 添加应聘(worknum).ShowDialog() == DialogResult.OK)
            //{
            //    loadYplb();
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells["id"].Value.ToString() == "")
                {
                    int row = new employmentinfoTableAdapter().Insert(worknum, dataGridView1.Rows[i].Cells["职务"].Value.ToString(),
                 dataGridView1.Rows[i].Cells["岗位名称"].Value.ToString(),
                 Convert.ToDateTime(dataGridView1.Rows[i].Cells["开始时间"].Value.ToString()),
                 Convert.ToDateTime(dataGridView1.Rows[i].Cells["结束时间"].Value.ToString()),
                 dataGridView1.Rows[i].Cells["合同类型"].Value.ToString());
                    if (row == 1)
                    {
                        DialogResult = DialogResult.OK;
                        MessageBox.Show("录入成功");
                    }
                    else
                    {
                        MessageBox.Show("录入失败");
                    }
                }
            }
        }
    }
}
