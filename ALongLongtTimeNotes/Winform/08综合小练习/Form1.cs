using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _11PictureBox和Timer的练习
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          //播放音乐
            SoundPlayer sp = new SoundPlayer();
           // sp.SoundLocation = @"C:\Users\SpringRain\Desktop\1.wav";
            //sp.Play();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            //在窗体加载的时候 给每一个PictureBox都赋值一张图片的路径
            pictureBox1.Image = Image.FromFile(@"C:\Users\SpringRain\Desktop\Picture\11.jpg");
            pictureBox2.Image = Image.FromFile(@"C:\Users\SpringRain\Desktop\Picture\11.jpg");
            pictureBox3.Image = Image.FromFile(@"C:\Users\SpringRain\Desktop\Picture\11.jpg");
            pictureBox4.Image = Image.FromFile(@"C:\Users\SpringRain\Desktop\Picture\11.jpg");
            pictureBox5.Image = Image.FromFile(@"C:\Users\SpringRain\Desktop\Picture\11.jpg");
            pictureBox6.Image = Image.FromFile(@"C:\Users\SpringRain\Desktop\Picture\11.jpg");
        }
        string[] path = System.IO.Directory.GetFiles(@"C:\Users\SpringRain\Desktop\Picture");
        int i = 0;
        Random r = new Random();
        private void timer1_Tick(object sender, EventArgs e)
        {
            //每隔一秒钟 换一张图片
            i++;
            if (i == path.Length)
            {
                i = 0;
            }
            pictureBox1.Image = Image.FromFile(path[r.Next(0,path.Length)]);
            pictureBox2.Image = Image.FromFile(path[r.Next(0, path.Length)]);
            pictureBox3.Image = Image.FromFile(path[r.Next(0, path.Length)]);
            pictureBox4.Image = Image.FromFile(path[r.Next(0, path.Length)]);
            pictureBox5.Image = Image.FromFile(path[r.Next(0, path.Length)]);
            pictureBox6.Image = Image.FromFile(path[r.Next(0, path.Length)]);
        }
    }
}
