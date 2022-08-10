# WinForm入门 #

C/S结构：

     客户端项目

B/S结构：

     浏览器项目

定义：

    所有的窗体都是组件+事件完成

    事件需要结合不同的控件



# 案例 #
----------

## 1.窗体的打开和关系 ##

1）在工具箱中找到组件Button，将其放在你想要他在的位置

    Button:当客户单击它时引发事件

1.1）可以通过Button的属性来设置

    Text：文本设置（更改内容）

    BackColor:组件背景颜色
   
    等....

2）给组件注册事件，默认双击是执行点击操作

    private void button1_Click(object sender, EventArgs e)
        {}

    object sender：你点击的对象是谁？

    EventArgs e：你执行事件所需要的资源

3）整个组件的属性和事件的组合编程

3.1）当我们要在第一个窗体中点击出第二个窗体，先创建第二个窗体，再在第一窗体的Button组件中创建第二个窗体的对象

    Label:为控件提供运行时信息或说明性文字   

    private void button1_Click(object sender, EventArgs e)
        {
            //在内存中创建窗体2对象
            Form2 frm2 = new Form2();
            //展示当前窗体
            frm2.Show();

            this.label1.Text = "10";
        }

3.2）同上，创建第三个窗体，并在第二个窗体的Button组件中创建第三个窗体的对象

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();

            frm3.Show();
        }
3.3）在第三个窗体关闭所有的窗体

3.3.1）先创建一个类，创建一个静态变量

    public static class Test
    {
        //全局静态变量
        public static Form1 _fr1Test;
    }

3.3.2）在一个窗体Load事件中，将窗体的对象放当静态变量中

    Load：每当用户加载窗体时发生

        private void Form1_Load(object sender, EventArgs e)
        {
            Test._fr1Test = this;
        }

3.3.3）在第三个窗体的Button组件中，调用Test类中的静态变量来关闭窗体

    close：关闭窗体的调用方法

        private void button1_Click(object sender, EventArgs e)
        {
            Test._fr1Test.Close();
        }

总结：

    多窗体的展示：在第一个窗体的Button组件中调第二个窗体的对象，并show出来；以此类推。

    创建一个全局的静态变量，在一个窗体的Load事件中，将第一个窗体的对象赋值给静态变量。

    在最后一个窗体中，调用被赋值后的静态变量的关闭窗体方法。



## 2.鼠标移入事件 ##

1）创建Button单击组件，并更改Text文字，在属性（Name）中可以更改组件的名字

2）当鼠标进入按钮的可见部分时，按钮就更改位置

2.1）鼠标的处理，在Button组件中，找到MouseEnter，并设置事件

    MouseEnter：鼠标进入控件的可见部分时发生

    Point:捕获鼠标的位置

        private void btnUnLove_MouseEnter(object sender, EventArgs e)
        {
            //这个按钮活动的最大宽度就是，窗体的宽度减去按钮的宽度
            int x = this.ClientSize.Width - btnUnLove.Width;
            int y = this.ClientSize.Height - btnUnLove.Height;

            Random r = new Random();
            //给按钮随机一个的新坐标
            btnUnLove.Location = new Point(r.Next(0, x + 1), r.Next(0, y + 1));
        }

3）当单击组件时发生

     MessageBox.Show(""); ：相当于cw();

        private void btnLove_Click(object sender, EventArgs e)
        {
            MessageBox.Show("我也爱你哟思密达");
            //关闭主窗体
            this.Close();
        }

        private void btnUnLove_Click(object sender, EventArgs e)
        {
            MessageBox.Show("还是被你这个屌丝点到了");
            this.Close();
        }

总结：

    在窗体中增加当鼠标移入所发生的事件，控件的移动范围在用户工作区，参考：
    //窗体的宽
    this.Form.ClientSize.Width
    //窗体的长
    this.Form.ClientSize.Height

    因为没有设置具体的大小，所以窗体可以拉伸

## 3.老师学生登录 ##
1）创建2个Label组件、2个textBox组件、1个button组件、2个RadioButton组件

    TextBox:允许用户输入文本，并提供多行编辑及密码掩码功能

    RadioButton：单选按键，当与其他单选按键成对出现时，允许用户从一组选择中选择单个选项

2）思考：

     1.判断两个控件的内容不是空
   
     2.判断用户的登录身份

     3.判断账号或者密码错误，进行相应的提示并且无论两个控件哪个出错都自动清空所有控件

     4.账号密码正确，打开一个新的窗口

-------

    Checked：指示单击按钮是否已击中

        private void button1_Click(object sender, EventArgs e)
        {
            //判断学生或者老师的单选控件是否击中
            if (rdoStudent.Checked || rdoTeacher.Checked)
            {
                //用户名
                string name = txtName.Text.Trim();
                //密码
                string pwd = txtPwd.Text;

                //判断学生按钮被击中
                if (rdoStudent.Checked)
                {
                    //用户名和密码都如设置一样，便成功
                    if (name == "student" && pwd == "student")
                    {
                        MessageBox.Show("学生登陆成功");
                    }
                    //否则就失败
                    else
                    {
                        MessageBox.Show("登陆失败");
                        txtName.Clear();
                        txtPwd.Clear();
                        //清空的调用方法
                        txtName.Focus();
                    }
                }
                else//选择的是老师
                {
                    if (name == "teacher" && pwd == "teacher")
                    {
                        MessageBox.Show("老师登陆成功");
                    }
                    else
                    {
                        MessageBox.Show("登陆失败");
                        txtName.Clear();
                        txtPwd.Clear();
                        txtName.Focus();
                    }
                }
            }
            //都没有选中
            else
            {
                MessageBox.Show("请首先选择登陆身份");
            }
        }

## 4.跑马灯 ##
1）创建一个2个Label组件、2个Timer组件

2）在timer1组件中，设置跑马灯，第二个图像代替第一个图像，以此类推

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = label1.Text.Substring(1) + label1.Text.Substring(0, 1);
        }

3）时间设置

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblTme.Text = DateTime.Now.ToString();
            //15:32分播放音乐叫我起床
            if (DateTime.Now.Hour == 16 && DateTime.Now.Minute == 16 && DateTime.Now.Second == 30)
            { 
                //播放音乐
                SoundPlayer sp = new SoundPlayer();

                //new From2().show();
                sp.SoundLocation = @"C:\Users\SpringRain\Desktop\1.wav";
                sp.Play();

            }
        }

4）加载窗体时，将当前系统的事件赋值给label

        private void Form1_Load(object sender, EventArgs e)
        {
            lblTme.Text = DateTime.Now.ToString();
        }

## 5.单选和多选 ##
1）利用RadioButton组件和GroupBox组件

     GroupBox:在一组控件周围，显示一个带有可选标题的框架

2）关联选项CheckBox

     CheckBox：允许用户选择或清除关联选项