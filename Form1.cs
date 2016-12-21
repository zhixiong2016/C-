using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 算术练习
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int truth,score=0,time=0,num=1;// 题号 num  时间 time 得分 scoure
        private void Form1_Load(object sender, EventArgs e)
        {
            Form2 login = new Form2();// 默认打开  检测界面，加载事件中弹出  登陆界面
            login.ShowDialog();
            if (Form2.checklogin == "")
                Application.Exit();
            else
            {
                //MessageBox.Show("欢迎" + Form2.checklogin);
                label4.Text = "当前用户:"+Form2.checklogin;
                // load();
                sx();//===================   刷新成绩 面板
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)// 改变符号 调用load（） 加法
        {
            label6.Text = "+";
            load();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)// 改变符号 调用load（） 减法
        {
            label6.Text = "-";
            load();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)// 改变符号 调用load（） 乘法
        {
            label6.Text = "*";
            load();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)// 改变符号 调用load（） 除法
        {
            label6.Text = "/";
            load();
        }
        private void load()
        {
            int a, c;
            if(label6.Text=="+") // ========================================= 判断运算符 使用加法
            {
                randomcreate(label6.Text, out a, out c);// randomcreate(string b, out int a, out int c) 生成随机数
                label5.Text = a.ToString();
                label7.Text = c.ToString();
                truth = a + c;
            }
            else if(label6.Text=="-") // ============================== =======判断运算符 使用减法
            {
                randomcreate(label6.Text, out a, out c);
                label5.Text = a.ToString();
                label7.Text = c.ToString();
                truth = a - c;
            }
            else if (label6.Text == "*")  // ===================================判断运算符 使用乘法
            {
                randomcreate(label6.Text, out a, out c);
                label5.Text = a.ToString();
                label7.Text = c.ToString();
                truth = a * c;
            }
            else // ========================================================= 判断运算符 使用除法
            {
                randomcreate(label6.Text, out a, out c);
                label5.Text = a.ToString();
                label7.Text = c.ToString();
                truth = a / c;
            }
            textBox1.Text = "";//  ==============================  运算符的改变 再次调用load() 都会使答案文本框 清空
        }
        private void randomcreate(string b, out int a, out int c)// 根据不同的运算符 生成不同关系的   随机数
        {
            a = 0;
            c = 0;
            if (b == "+")
            {
                Random ran = new Random();
                a = ran.Next(0, 100);
                c = ran.Next(0, 100);
            }
            else if (b == "-")
            {
                int t;
                Random ran = new Random();
                a = ran.Next(0, 100);
                c = ran.Next(0, 100);
                if (a < c)
                {
                    t = a;
                    a = c;
                    c = t;
                }
            }
            else if (b == "*")
            {
                Random ran = new Random();
                a = ran.Next(1, 100);
                c = ran.Next(1, 10);
            }
            else
            {
                while (true)//==================   除法 的随机数 生成
                {
                    Random ran = new Random();
                    a = ran.Next(0, 100);
                    c = ran.Next(1, 100);
                    if (a > c && a % c == 0)//=== 可以整除 循环结束 返回 随机数
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)// 开始测试 暂停测试
        {
            load();
            if (button1.Text == "开始测试")
            {
                timer1.Enabled = true; //============== 开始计时
                button1.Text = "暂停测试";
            }
            else
            {
                timer1.Enabled = false;// ======  暂停测试  停止计时
                button1.Text="开始测试";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            label2.Text = "时间:"+time;//=======  timer 每一秒 使 time+1 并返回到 时间 label2  
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (button1.Text == "暂停测试")
            {
                if (e.KeyChar == 13 && textBox1.Text != "")
                {
                    scoreall();// 调用函数 是否答案正确
                    //load();//==== 重新生成随机数 开始下一题                 
                    num++;// ==== 题号 加一
                    label8.Text = "当前题号:" + num;// 改变显示的 当前题号
                    if (num <= 10)
                    {
                        load();//==== 重新生成随机数 开始下一题
                    }
                    else
                    {
                        //timer1.Enabled = false;//停止计时
                        //label8.Text = "当前题号:" + (num-1).ToString();// 题号为 10
                        stopall();//====  初始值 所有标签
                        MessageBox.Show("    测试结束\n"+"你的得分为:" + score +"  分\n"+ "你的用时为:" + time.ToString()+"  秒");
                        if (MessageBox.Show("已完成本轮测试,是否保存成绩?", "保存?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            save();// 点击保存  成绩写入文件
                            times();// 将所用时间  写入文件
                        }
                        //stopall();
                        score = time = num = 0;// 初始值 算有变量
                    }

                }

            }
            else
            {
                MessageBox.Show("请点击开始测试!");
                e.Handled = true;
            }
        }
        private void stopall()// 初始值 所有标签
        {
            timer1.Enabled = false;
            button1.Text = "开始测试";
            label2.Text = "时间:0";
            label3.Text = "得分:0";
            label8.Text = "当前题号:1";
            //score = time = num = 0;
        }

        private void label15_Click(object sender, EventArgs e)
        {
            sx();//====================================手动刷新   分数排行榜
        }

        private void scoreall()// 判断 答案是否正确
        {
            //MessageBox.Show(truth.ToString());
            if (textBox1.Text == truth.ToString())
            {
                score++;
                label3.Text = "得分:" + score.ToString();// 刷新并显示得分
            }
        }
        private void save()  // 存入 得分
        {
            string lan = "";
            string[] array = { "aa", "aa" };
            int n;
            if (!File.Exists("d:\\user.ini"))
            {
                MessageBox.Show("文件丢失!请重新注册用户!");
            }
            else
            {
                using (StreamReader sr = new StreamReader("d:\\user.ini"))
                {
                    lan = sr.ReadLine();
                    sr.Close();
                }
                if (lan == null)
                {
                    MessageBox.Show("文件丢失!");
                    Application.Exit();
                }
                else
                {
                    lan = lan.Substring(0, lan.Length - 1);
                    array = lan.Split(',');
                    for (n = 0; n < array.Length; n = n + 3)
                    {
                        if (Form2.checklogin == array[n])
                            break;
                    }
                    if (n >= array.Length)
                    { }
                    else
                    {
                        array[n + 2] = score.ToString();
                    }

                }
                using (StreamWriter sw = new StreamWriter("d:\\user.ini"))
                {
                    for (int a = 0; a < array.Length; a++)
                    {
                        sw.Write(array[a] + ',');
                    }
                }
            }
        }

        private void times()   //  存入 所用的时间
        {
            string lan = "";
            string[] array = { "aa", "aa" };
            int n;
            if (!File.Exists("d:\\user.ini"))
            {
                MessageBox.Show("文件丢失!请重新注册用户!");
            }
            else
            {
                using (StreamReader sr = new StreamReader("d:\\user.ini"))
                {
                    lan = sr.ReadLine();
                    sr.Close();
                }
                if (lan == null)
                {
                    MessageBox.Show("文件丢失!");
                    Application.Exit();
                }
                else
                {
                    lan = lan.Substring(0, lan.Length - 1);
                    array = lan.Split(',');
                    for (n = 0; n < array.Length; n = n + 4)
                    {
                        if (Form2.checklogin == array[n])
                            break;
                    }
                    if (n >= array.Length)
                    { }
                    else
                    {
                        array[n + 3] = time.ToString();
                    }

                }
                using (StreamWriter sw = new StreamWriter("d:\\user.ini"))
                {
                    for (int a = 0; a < array.Length; a++)
                    {
                        sw.Write(array[a] + ',');
                    }
                }
            }
        }

        private void sx()//  访问文件            刷新分数排行榜  
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            if (!File.Exists("d:\\user.ini"))
            {
                using (StreamWriter sw = File.CreateText("d:\\user.ini")) 
                {

                }
                MessageBox.Show("文件丢失!请重新注册用户!");
            }
            else
            {
                string lan = "";
                string[] user;
                using (StreamReader sr = new StreamReader("d:\\user.ini"))
                {
                    lan = sr.ReadLine();
                }
                if (lan == null)
                {
                    MessageBox.Show("没有用户，请注册！");
                }
                lan.Substring(0, lan.Length - 1);
                user = lan.Split(',');
                for (int i = 0; i < user.Length; i += 4)
                {
                    listBox1.Items.Add(user[i]);
                }
                for (int j = 2; j < user.Length; j += 4)
                {
                    listBox2.Items.Add(user[j]);
                }
                for (int k = 3; k < user.Length; k += 4)
                {
                    listBox3.Items.Add(user[k]);
                }
            }
        }

    }
}
