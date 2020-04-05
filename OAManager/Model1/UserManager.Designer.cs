namespace OAManager.Model1
{
    partial class UserManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("系统通知");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("个人基本信息");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("聘任信息");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("获奖信息");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("培训信息");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("健康信息");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("出差信息");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("工资信息");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("档案信息", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("部门管理");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("人员管理");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("人员报表");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("员工应聘");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("人事管理", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("信息审核");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("人事管理", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode14,
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("工资数据");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("导入绩效");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("添加发/扣款项");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("上交税费");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("管理工资", new System.Windows.Forms.TreeNode[] {
            treeNode18,
            treeNode19,
            treeNode20});
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("生成工资");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("工资统计");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("财务管理", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode21,
            treeNode22,
            treeNode23});
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("修改职工权限");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("审核信息");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("部门领导", new System.Windows.Forms.TreeNode[] {
            treeNode25,
            treeNode26});
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(142)))), ((int)(((byte)(185)))));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1379, 100);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1036, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(343, 100);
            this.panel2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(125, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = "欢饮您,";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(125, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "欢迎您,";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(142)))), ((int)(((byte)(185)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("STXinwei", 20F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Image = global::OAManager.Properties.Resources.叶子__1_;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 100);
            this.label1.TabIndex = 1;
            this.label1.Text = "师资管理系统";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.Location = new System.Drawing.Point(0, 100);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "tz";
            treeNode1.Text = "系统通知";
            treeNode2.Name = "Node1";
            treeNode2.Text = "个人基本信息";
            treeNode3.Name = "Node4";
            treeNode3.Text = "聘任信息";
            treeNode4.Name = "Node5";
            treeNode4.Text = "获奖信息";
            treeNode5.Name = "Node7";
            treeNode5.Text = "培训信息";
            treeNode6.Name = "Node8";
            treeNode6.Text = "健康信息";
            treeNode7.Name = "Node9";
            treeNode7.Text = "出差信息";
            treeNode8.Name = "Node10";
            treeNode8.Text = "工资信息";
            treeNode9.Name = "da";
            treeNode9.Text = "档案信息";
            treeNode10.Name = "Node11";
            treeNode10.Text = "部门管理";
            treeNode11.Name = "Node13";
            treeNode11.Text = "人员管理";
            treeNode12.Name = "Node14";
            treeNode12.Text = "人员报表";
            treeNode13.Name = "Node15";
            treeNode13.Text = "员工应聘";
            treeNode14.Name = "Node12";
            treeNode14.Text = "人事管理";
            treeNode15.Name = "Node16";
            treeNode15.Text = "信息审核";
            treeNode16.Name = "rs";
            treeNode16.Text = "人事管理";
            treeNode17.Name = "Node17";
            treeNode17.Text = "工资数据";
            treeNode18.Name = "Node19";
            treeNode18.Text = "导入绩效";
            treeNode19.Name = "Node20";
            treeNode19.Text = "添加发/扣款项";
            treeNode20.Name = "Node21";
            treeNode20.Text = "上交税费";
            treeNode21.Name = "Node18";
            treeNode21.Text = "管理工资";
            treeNode22.Name = "Node22";
            treeNode22.Text = "生成工资";
            treeNode23.Name = "Node23";
            treeNode23.Text = "工资统计";
            treeNode24.Name = "cw";
            treeNode24.Text = "财务管理";
            treeNode25.Name = "Node24";
            treeNode25.Text = "修改职工权限";
            treeNode26.Name = "Node25";
            treeNode26.Text = "审核信息";
            treeNode27.Name = "bmld";
            treeNode27.Text = "部门领导";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode9,
            treeNode16,
            treeNode24,
            treeNode27});
            this.treeView1.Size = new System.Drawing.Size(216, 972);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(216, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1163, 972);
            this.panel3.TabIndex = 2;
            // 
            // UserManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1379, 1072);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.panel1);
            this.Name = "UserManager";
            this.Text = "师资管理系统";
            this.Load += new System.EventHandler(this.UserManager_Load);
            this.Shown += new System.EventHandler(this.UserManager_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel3;
    }
}