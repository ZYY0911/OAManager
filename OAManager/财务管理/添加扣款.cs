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
    public delegate void DeledeMy2(Panel panel);
    public partial class 添加扣款 : UserControl
    {
        public DeledeMy2 deledeMy;
        Panel panel;
        public 添加扣款(DeledeMy2 deledeMy, Panel panel)
        {
            this.panel = panel;
            this.deledeMy = deledeMy;
            InitializeComponent();

        }
        private void 添加扣款_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            deledeMy(panel);
        }
    }
}
