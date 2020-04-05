using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OAManager.人事管理
{
    public partial class 报表 : Form
    {
        int lx;
        public 报表(int lx)
        {
            this.lx = lx;
            InitializeComponent();
        }

        private void 民族报表_Load(object sender, EventArgs e)
        {
            this.listView1.Columns.Add("工号", 120, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("姓名", 120, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("身份证号", 180, HorizontalAlignment.Center); //一步
            this.listView1.Columns.Add("生日", 180, HorizontalAlignment.Center); //一步
            this.listView1.View = View.Details;
            switch (lx)
            {
                case 1:
                    loadXb();
                    break;
                case 2:
                    loadNation();
                    break;
                case 3:
                    loadNl();
                    break;
                case 4:
                    loadXl();
                    break;
                case 5:
                    loadZZ();
                    break;
                case 6:
                    loadZC();
                    break;
                case 7:
                    loadBM();
                    break;
                default:
                    break;
            }
        }

        private void loadBM()
        {
            Dictionary<string, DataTable> keys = new Dictionary<string, DataTable>();
            using (DepartmentSqlTableAdapters.departmentTableAdapter department = new DepartmentSqlTableAdapters.departmentTableAdapter())
            {
                DataTable table = department.GetData();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    keys[table.Rows[i][1].ToString()] = null;
                }
            }
            using (MyInfoAndEmpTableAdapters.DataTable1TableAdapter employmentinfo = new MyInfoAndEmpTableAdapters.DataTable1TableAdapter())
            {
                DataTable data = employmentinfo.GetData();
                getDatatAble(data);
                object[] obj = new object[data.Columns.Count];
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string sex = data.Rows[i]["department"].ToString();
                    DataTable count = keys[sex];
                    if (count == null)
                    {
                        count = data.Clone();
                    }
                    data.Rows[i].ItemArray.CopyTo(obj, 0);
                    count.Rows.Add(obj);
                    keys[sex] = count;
                }
            }
            setLsitView(keys);

        }

        private void loadZC()
        {
            Dictionary<string, DataTable> keys = new Dictionary<string, DataTable>();
            using (JobSqlTableAdapters.jobtitleTableAdapter jobSqlTable = new JobSqlTableAdapters.jobtitleTableAdapter())
            {
                DataTable table = jobSqlTable.GetData();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    keys[table.Rows[i][1].ToString()] = null;
                }
            }
            using (MyInfoAndEmpTableAdapters.DataTable1TableAdapter employmentinfo = new MyInfoAndEmpTableAdapters.DataTable1TableAdapter())
            {
                DataTable data = employmentinfo.GetData();
                getDatatAble(data);
                object[] obj = new object[data.Columns.Count];
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string sex = data.Rows[i]["jobtitle"].ToString();
                    DataTable count = keys[sex];
                    if (count == null)
                    {
                        count = data.Clone();
                    }
                    data.Rows[i].ItemArray.CopyTo(obj, 0);
                    count.Rows.Add(obj);
                    keys[sex] = count;
                }
            }
            setLsitView(keys);
          
        }

        private void getDatatAble(DataTable data)
        {
            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (int j = data.Rows.Count - 1; j > i; j--)
                {
                    if (data.Rows[i]["worknum"].ToString() == data.Rows[j]["worknum"].ToString())
                    {
                        data.Rows.RemoveAt(i);
                        getDatatAble(data);
                    }
                }
            }
        }

        private void loadZZ()
        {
            Dictionary<string, DataTable> keys = new Dictionary<string, DataTable>();
            using (个人基本信息.BaseSqlTableAdapters.politicalTableAdapter political = new 个人基本信息.BaseSqlTableAdapters.politicalTableAdapter())
            {
                DataTable table = political.GetData();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    keys[table.Rows[i][1].ToString()] = null;
                }
            }
            using (MyInfoSqlTableAdapters.myinfoTableAdapter myInfoSql = new MyInfoSqlTableAdapters.myinfoTableAdapter())
            {
              
                DataTable data = myInfoSql.GetData();
                object[] obj = new object[data.Columns.Count];
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string sex = data.Rows[i]["political"].ToString();
                    DataTable count = keys[sex];
                    if (count == null)
                    {
                        count = data.Clone();
                    }
                    data.Rows[i].ItemArray.CopyTo(obj, 0);
                    count.Rows.Add(obj);
                    keys[sex] = count;
                }
            }
            setLsitView(keys);

        }

        private void loadXl()
        {
            Dictionary<string, DataTable> keys = new Dictionary<string, DataTable>();
            using (个人基本信息.BaseSqlTableAdapters.educationTableAdapter education = new 个人基本信息.BaseSqlTableAdapters.educationTableAdapter())
            {
                DataTable table = education.GetData();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    keys[table.Rows[i][1].ToString()] = null;
                }
            }
            using (MyInfoSqlTableAdapters.myinfoTableAdapter myInfoSql = new MyInfoSqlTableAdapters.myinfoTableAdapter())
            {
                DataTable data = myInfoSql.GetData();
                object[] obj = new object[data.Columns.Count];
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string sex = data.Rows[i]["education"].ToString();
                    DataTable count = keys[sex];
                    if (count == null)
                    {
                        count = data.Clone();
                    }
                    data.Rows[i].ItemArray.CopyTo(obj, 0);
                    count.Rows.Add(obj);
                    keys[sex] = count;
                }
            }
            setLsitView(keys);
        }

        private void loadNl()
        {
            Dictionary<string, DataTable> keys = new Dictionary<string, DataTable>();
            keys["30"] = null;
            keys["30-45"] = null;
            keys["45-60"] = null;
            keys["60"] = null;
            DateTime now = DateTime.Now;
            DataTable data = null;
            using (MyInfoSqlTableAdapters.myinfoTableAdapter myInfoSql = new MyInfoSqlTableAdapters.myinfoTableAdapter())
            {
                 data= myInfoSql.GetData();
                object[] obj = new object[data.Columns.Count];
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    DateTime sex = Convert.ToDateTime(data.Rows[i]["birth"].ToString());
                    int year = now.Year - sex.Year;
                    DataTable count = null;
                    string key = "";
                    if (year <= 30)
                    {
                        count = keys["30"];
                        key = "30";
                    }
                    else if (year > 30 && year <= 45)
                    {
                        count = keys["30-45"]; key = "30-45";
                    }
                    else if (year > 45 && year <= 60)
                    {
                        count = keys["45-60"]; key = "45-60";
                    }
                    else if (year > 60)
                    {
                        count = keys["60"]; key = "60";
                    }
                    if (count == null)
                    {
                        count = data.Clone();
                    }
                    data.Rows[i].ItemArray.CopyTo(obj, 0);
                    count.Rows.Add(obj);
                    keys[key] = count;
                }
            }
            setLsitView(keys);

        }

        private void loadXb()
        {
            Dictionary<string, DataTable> keys = new Dictionary<string, DataTable>();
            keys["男"] = null;
            keys["女"] = null;
            using (MyInfoSqlTableAdapters.myinfoTableAdapter myInfoSql = new MyInfoSqlTableAdapters.myinfoTableAdapter())
            {
                DataTable data = myInfoSql.GetData();
                object[] obj = new object[data.Columns.Count];
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string sex = data.Rows[i]["sex"].ToString();
                    DataTable count = keys[sex];
                    if (count == null)
                    {
                        count = data.Clone();
                    }
                    data.Rows[i].ItemArray.CopyTo(obj, 0);
                    count.Rows.Add(obj);
                    keys[sex] = count;
                }
            }
            setLsitView(keys);

        }

        private void setLsitView(Dictionary<string, DataTable> keys)
        {
            foreach (KeyValuePair<string, DataTable> item in keys)
            {
                DataTable data = item.Value;
                Console.WriteLine(item.Key+"--"+item.Value);
                ListViewGroup group = new ListViewGroup();
                group.Header = item.Key;
                group.HeaderAlignment = HorizontalAlignment.Left;
                this.listView1.Groups.Add(group);
                if (data != null)
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = data.Rows[i]["worknum"].ToString();
                        lvi.SubItems.Add(data.Rows[i]["name"].ToString());
                        lvi.SubItems.Add(data.Rows[i]["sid"].ToString());
                        lvi.SubItems.Add(data.Rows[i]["birth"].ToString().Split(' ')[0]);
                        group.Items.Add(lvi);
                        this.listView1.Items.Add(lvi);
                    }
                }
            }
        }

        private void loadNation()
        {
            Dictionary<string, DataTable> keys = new Dictionary<string, DataTable>();
            using (NationSqlTableAdapters.nationTableAdapter nationTable = new NationSqlTableAdapters.nationTableAdapter())
            {
                DataTable table = nationTable.GetData();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    keys[table.Rows[i]["nationname"].ToString()] = null;
                }
            }
            using (MyInfoSqlTableAdapters.myinfoTableAdapter myInfoSql = new MyInfoSqlTableAdapters.myinfoTableAdapter())
            {
                DataTable data = myInfoSql.GetData();
                object[] obj = new object[data.Columns.Count];
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string sex = data.Rows[i]["nation"].ToString();
                    DataTable count = keys[sex];
                    if (count == null)
                    {
                        count = data.Clone();
                    }
                    data.Rows[i].ItemArray.CopyTo(obj, 0);
                    count.Rows.Add(obj);
                    keys[sex] = count;
                }
            }
            setLsitView(keys);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
