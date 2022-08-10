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
namespace PictureBox
{
    public partial class Form1 : Form
    {
        string[] files;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile(@"C:\Users\admin\Desktop\PIC\cat1.JPG");
            //指定文件夹下的全路径
            files = Directory.GetFiles(@"C:\Users\admin\Desktop\PIC");

        }
        int i = 0;
        //点击更换下一个的事件
        private void button2_Click(object sender, EventArgs e)
        {

            i++;
            if (i == files.Length)
            {
                i = 0;
            }
            pictureBox1.Image = Image.FromFile
                (files[i]);
            //存在1个小问题
        }

        private void button1_Click(object sender, EventArgs e)
        {
            i--;
            if (i < 0)
            {
                i = files.Length - 1;
            }
            pictureBox1.Image = Image.FromFile
              (files[i]);
        }
    }
}
