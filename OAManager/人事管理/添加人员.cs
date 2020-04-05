using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OAManager.MyInfoSqlTableAdapters;

namespace OAManager.人事管理
{
    public partial class 添加人员 : Form
    {
        public 添加人员()
        {
            InitializeComponent();
        }
        private void 添加人员_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'baseSql.education' table. You can move, or remove it, as needed.
            this.educationTableAdapter.Fill(this.baseSql.education);
            // TODO: This line of code loads data into the 'baseSql.political' table. You can move, or remove it, as needed.
            this.politicalTableAdapter.Fill(this.baseSql.political);
            textBox1.Text = getWorkNum();
        }

        List<int> worknumlist = new List<int>();
        int worknum = 0;
        private string getWorkNum()
        {
            DataTable myInfoTable = new myinfoTableAdapter().GetData();
            for (int i = 0; i < myInfoTable.Rows.Count; i++)
            {
                worknumlist.Add(Convert.ToInt32(myInfoTable.Rows[i]["worknum"].ToString()));
            }
            worknumlist.Sort();
            for (int i = 0; i < worknumlist.Count - 1; i++)
            {
                if (worknumlist[i] + 1 != worknumlist[i + 1])
                {
                    worknum = worknumlist[i] + 1;
                    break;
                }
            }
            if (worknum == 0)
            {
                worknum = worknumlist[worknumlist.Count - 1] + 1;
            }
            return worknum + "";
        }


        string myPhotoPath;
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
            int row = new myinfoTableAdapter().Insert(worknum, name, sid, dateTimePicker2.Value, dateTimePicker1.Value
                 , sex, nation, birth, marry, political, country, education, myPhotoPath);
            if (row == 1)
            {
                DialogResult = DialogResult.OK;
                MessageBox.Show("添加成功");
            }
            else
            {
                MessageBox.Show("添加失败，请稍后重试");
            }
            this.Close();
        }
        bool isLoad = false;
        private void button4_Click(object sender, EventArgs e)
        {
            if (!isLoad)
            {
                DialogResult = DialogResult.No;
            }
            else
            {

                DialogResult = DialogResult.OK;
            }
           
            this.Close();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString() != "")
            {
                int InputWorknum = Convert.ToInt32(textBox1.Text.ToString());
                for (int i = 0; i < worknumlist.Count; i++)
                {
                    if (worknumlist[i] == InputWorknum)
                    {
                        MessageBox.Show("您输入的工号已使用，请重新输入");
                        textBox1.Text = "";
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "所有文件|*.*|Excel 2003|*.xls|Excel|*.xlsx";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Thread thread = new Thread(LoadExcle);
                thread.IsBackground = true;
                thread.Start(fileDialog.FileName);
                isLoad = true;
            }


        }
        private void LoadExcle(object path)
        {
            string ExclePath = (string)path;
            string FileType = ExclePath.Substring(ExclePath.LastIndexOf('.'));
            string connstr = "";
            if (FileType == ".xls")
            {
                connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExclePath
                    + ";Extented Properties=Excel 8.0;";
            }
            else if (FileType == ".xlsx")
            {
                connstr = "Provider= Microsoft.ACE.12.0;Data Source= " + ExclePath
                    + ";;Extented Properties= 'Excel 12.0;HDR = Yes;IMEx=1'";
            }
            else
            {
                MessageBox.Show("选择的文件不正确");
                return;
            }
            OleDbConnection oleDb = new OleDbConnection(connstr);
            oleDb.Open();
            DataTable sheetName = oleDb.GetOleDbSchemaTable(OleDbSchemaGuid.Tables
                , new object[] { null, null, null, "TABLE" });
            string firstname = sheetName.Rows[0][2].ToString();
            string sql = "select * from [" + firstname + "]";
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connstr);
            DataSet set = new DataSet();
            adapter.Fill(set);
            oleDb.Close();
            DataTable table = set.Tables[0];
            string[] arr = { "name", "sid", "birthday", "jointime", "sex", "nation", "bitrhloaction", "marry", "political", "country", "education" };
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Columns[i].ColumnName!=arr[i])
                {
                    MessageBox.Show("Excel表格不符合规定");
                    return;
                }
            }
            int rows = 0;
            int allnum = 0,error= 0;
            try
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string worknum = getWorkNum();
                    string name = table.Rows[i][0].ToString();
                    string sid = table.Rows[i][1].ToString();
                    string birthday = table.Rows[i][2].ToString();
                    string jointime = table.Rows[i][3].ToString();
                    string sex = table.Rows[i][4].ToString();
                    string nation = table.Rows[i][5].ToString();
                    string birth = table.Rows[i][6].ToString();
                    string marry = table.Rows[i][7].ToString();
                    string political = table.Rows[i][8].ToString();
                    string country = table.Rows[i][9].ToString();
                    string education = table.Rows[i][10].ToString();
                   int a =  new myinfoTableAdapter().Insert( worknum,name, sid, Convert.ToDateTime(birthday)
                       , Convert.ToDateTime(jointime), sex, nation, birth,
                       marry, political, country, education, myPhotoPath);
                    if (a==1)
                    {
                        allnum++;
                    }
                    else
                    {
                        error++;
                    }
                    rows = i;
                }
                MessageBox.Show("成功导入:" + allnum + "失败:" + error);
                DialogResult =  DialogResult.OK;
            }
            catch
            {
                MessageBox.Show("Excel 表格中第" + rows + "行数据有误，请检查");
            }
        }
    }
}
