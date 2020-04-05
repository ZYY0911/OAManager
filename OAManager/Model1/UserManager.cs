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

namespace OAManager.Model1
{
    public partial class UserManager : Form
    {
        public UserManager()
        {
            InitializeComponent();
        }

        private void UserManager_Load(object sender, EventArgs e)
        {

            initMyInfo();
            treeView1.ExpandAll();
            panel3.Controls.Clear();
            Form form = new 个人基本信息.MyBaseInfo();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
            form.Parent = this.panel3;
            panel3.Controls.Add(form);
            form.Show();
            //foreach (TreeNode item in treeView1.Nodes)
            //{
            //    if (item.Name == "系统通知")
            //    {
            //        item.Remove();
            //    }
            //}

        }

        private void initMyInfo()
        {
            DataTable table = new myinfoTableAdapter().GetDataBy1(AppInfo.worknum);
            string photo = table.Rows[0]["photo"].ToString();
            if (File.Exists(photo))
            {
                pictureBox1.Load(photo);
            }
            LogName.name = table.Rows[0]["name"].ToString();
            label3.Text = LogName.name + " 老师！";
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Form form = null;
            TreeNode treeNode = treeView1.SelectedNode;

            string selectText = treeNode.Text;
            switch (selectText)
            {
                case "系统通知":
                    form = new 用户通知.用户通知();
                    break;
                case "个人基本信息":
                    form = new 个人基本信息.MyBaseInfo();
                    break;
                case "聘任信息":
                    form = new 聘任信息.Employinfo();
                    break;
                case "获奖信息":
                    form = new 获奖信息.WinManager();
                    break;
                case "健康信息":
                    form = new 健康信息.HealthyinfoManager();
                    break;
                case "人员管理":
                    form = new 人事管理.人事管理();
                    break;
                case "人员报表":
                    form = new 人事管理.人员报表();
                    break;
                case "员工应聘":
                    form = new 人事管理.员工应聘();
                    break;
                case "信息审核":
                    form = new 人事管理.信息审核();
                    break;
                case "工资数据":
                    form = new 财务管理.工资数据();
                    break;
                case "导入绩效":
                    form = new 财务管理.导入绩效();
                    break;
                case "添加发/扣款项":
                    form = new 财务管理.添加发款项();
                    break;
                case "生成工资":
                    form = new 财务管理.生成当月工资();
                    break;
                case "工资统计":
                    form = new 财务管理.工资统计();
                    break;
                case "工资信息":
                    form = new 工资信息.个人工资信息2();
                    break;
                case "修改职工权限":
                    form = new 部门领导.修改职工权限();
                    break;
                case "审核信息":
                    form = new 部门领导.信息审核();
                    break;
                default:
                    break;
            }
            if (form != null)
            {
                panel3.Controls.Clear();
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Maximized;
                //form.Parent = this.panel3;
                panel3.Controls.Add(form);
                form.Show();
                SetFrom(form);
            }


        }

        private void SetFrom(Form form)
        {
            if (form.Name == "WinManager")
            {
                获奖信息.WinManager winManager = form as 获奖信息.WinManager;
                for (int i = 0; i < winManager.myDat.RowCount; i++)
                {
                    winManager.myDat.Rows[i].Cells["Images"].Value = Image.FromFile(winManager.myDat.Rows[i].Cells["证书图片"].Value.ToString());
                    winManager.myDat.Rows[i].Height = 150;
                }
            }
        }

        private void UserManager_Shown(object sender, EventArgs e)
        {

            switch (AppInfo.type)
            {
                case 1:

                    Deletetree("rs");
                    Deletetree("cw");
                    break;
                case 2:
                    Deletetree("bmld");
                    Deletetree("cw");
                    break;
                case 3:
                    Deletetree("bmld");
                    Deletetree("rs");
                    break;
                case 4:
                case 5:
                case 6:
                    Deletetree("cw");
                    Deletetree("bmld");
                    Deletetree("rs");
                    break;
                default:
                    break;
            }
        }

        private void Deletetree(string name)
        {
            TreeNodeCollection collection = treeView1.Nodes;
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (collection[i].Name == name)
                {
                    treeView1.Nodes.Remove(collection[i]);
                }
            }
        }
    }
}
