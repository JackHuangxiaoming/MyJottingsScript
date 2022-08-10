using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDI窗体的设计
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 给窗体注册的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 显示子窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.MdiParent = this;
            f2.Show();
            Form3 f3 = new Form3();
            f3.MdiParent = this;
            f3.Show();
            Form4 f4 = new Form4();
            f3.MdiParent = this;
            f4.Show();
        }
    }
}
