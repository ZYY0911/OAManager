using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OAManager.财务管理
{
    public partial class 工资详情Item : UserControl
    {
        string name, value;
        public 工资详情Item(string name, string value)
        {
            this.name = name;
            this.value = value;
            InitializeComponent();
        }

        private void 工资详情Item_Load(object sender, EventArgs e)
        {
            label1.Text = name;
            label2.Text = value;
        }
    }
}
