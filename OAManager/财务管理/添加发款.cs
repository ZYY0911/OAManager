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
    public delegate void DeledeMy(Panel panel);
    public partial class 添加发款 : UserControl
    {
        public DeledeMy deledeMy;
        Panel panel;
        public 添加发款(DeledeMy deledeMy,Panel panel)
        {
            this.panel = panel;
            this.deledeMy = deledeMy;
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            deledeMy(panel);
        }

        private void 添加发款_Load(object sender, EventArgs e)
        {

        }
    }
}
