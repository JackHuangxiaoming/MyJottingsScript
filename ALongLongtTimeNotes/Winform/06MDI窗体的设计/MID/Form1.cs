using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MID
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 显示子船体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.MdiParent = this;
            f2.Show();
            Form3 f3= new Form3();
            f3.MdiParent = this;
            f3.Show();
            Form4 f4 = new Form4();
            f4.MdiParent = this;
            f4.Show();
        }
        /// <summary>
        /// 如何去排列我们的窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 横向显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //横向的布局
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void 纵向显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
    }
}
