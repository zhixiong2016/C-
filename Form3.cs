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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label5.Text != "非法字符!" && label7.Text != "密码不匹配!"&&textBox2.Text!="")
            {
                check();// 输入格式正确  调用check（）存入 用户名 密码
            }
            else
                MessageBox.Show("请正确填写你的信息！");
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 13 && e.KeyChar != 8 && e.KeyChar < 'a' || e.KeyChar > 'z' && e.KeyChar < 'A' || e.KeyChar > 'z')
            {
                textBox1.BackColor = Color.Red;//  非法字符 背景变红
                label5.Text = "非法字符!";     //  提示 有非法字符
                e.Handled = true;             //   清楚非法 字符
            }
            else
            {
                textBox1.BackColor = Color.White; //正确  背景变白
                label5.Text = "";  //提示 为空 不显示
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 13 && e.KeyChar != 8 && e.KeyChar < 'a' || e.KeyChar > 'z' && e.KeyChar < 'A' || e.KeyChar > 'z')
            {
                textBox2.BackColor = Color.Red;
                label6.Text = "非法字符!";
                e.Handled = true;
            }
            else
            {
                textBox2.BackColor = Color.White;
                label6.Text = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != textBox3.Text)// 输入的 两次密码 要相同
                label7.Text = "密码不匹配!"; // 不同 则 提示 错误信息
            else
                label7.Text = "";
        }
        private void reg()
        {
            // string lan = "";
            if (File.Exists("d:\\user.ini"))
            {
                using (StreamWriter sw = new StreamWriter("d:\\user.ini", true))
                {
                    sw.Write(textBox1.Text + ',' + textBox2.Text + ",0,0,");
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText("d:\\user.ini"))
                {
                    sw.Write(textBox1.Text + ',' + textBox2.Text + ",0,0,");
                }
            }
            MessageBox.Show("注册成功!");
            this.Close();
        }
        private void check()
        {
            string lan = "";
            string[] user;
            if (!File.Exists("d:\\user.ini"))
                reg();
            else
            {
                using (StreamReader sr = new StreamReader("d:\\user.ini"))
                {
                    lan = sr.ReadLine();
                }
                if (lan != null)
                {
                    lan = lan.Substring(0, lan.Length - 1);
                    user = lan.Split(',');
                    int i;
                    for (i = 0; i < user.Length; i = i + 4)
                    {
                        if (textBox1.Text == user[i])
                            break;
                    }
                    if (i >= user.Length)
                        reg();
                    else
                        MessageBox.Show("用户名已存在!");
                }
                else
                    reg();
            }
        }
    }
}
