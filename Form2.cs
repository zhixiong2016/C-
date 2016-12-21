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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public static string checklogin = "";// 全局变量 存储用户名
        public static string dlmm = "";     // 全局变量  存储密码
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 reg = new Form3(); // 实例化 注册窗口
            this.Hide();             // 隐藏 登陆窗口
            reg.ShowDialog();        
            this.Show();             //弹出  注册窗口
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "") //用户名密码 不可以为空
                check();                                   // 调用 check（）判断用户名密码
            else
                MessageBox.Show("填完所有的空!");
        }
        private void check()
        {
            string lan = "";
            string[] user;
            int i;
            if (!File.Exists("d:\\user.ini"))
                MessageBox.Show("没有注册用户!请注册!");
            else
            {
                using (StreamReader sr = new StreamReader("d:\\user.ini"))
                {
                    lan = sr.ReadLine();
                }
                if (lan == null)
                    MessageBox.Show("没有注册用户!请注册!");
                else
                {
                    lan.Substring(0, lan.Length - 1);
                    user = lan.Split(',');
                    for (i = 0; i < user.Length; i = i + 4)
                    {
                        if (textBox1.Text == user[i]&&textBox2.Text==user[i+1])
                            break;
                    }
                    if (i >= user.Length)
                        MessageBox.Show("用户名或密码错误!");
                    else
                    {
                        checklogin = textBox1.Text;
                        dlmm = textBox2.Text;
                        //MessageBox.Show("登陆成功!");
                        this.Close();
                    }
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("忘记密码及其他问题请联系管理员");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;// 显示 动态图  pictureBox1
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "") //用户名密码 不可以为空
                check();                                   // 调用 check（）判断用户名密码
            else
                MessageBox.Show("填完所有的空!");
        }
    }
}
