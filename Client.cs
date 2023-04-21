using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yangpeng1._0
{
    //客户端
    public partial class Client : Form
    {
        public Socket client; //创建一个Socket对像,声明一个客户端对象

        public object ReciveMsg { get; private set; }

        public Client()
        {
            InitializeComponent();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            textBox1.Text = GetLocalIP();
        }
        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        private string GetLocalIP()
        {
            string LocalIP = string.Empty;
            foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    LocalIP = ip.ToString();
                }
            }
            return LocalIP;
        }

        //连接服务
        private void button1_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                MessageBox.Show("客户端已连接至服务器，请勿重复连接！");
                return;
            }
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("请输入服务器信息！");
                return;
            }
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));//实例化端点
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//实例化Socket
            MsgEntity me = new MsgEntity(); //消息实体
            try
            {
                client.Connect(iep); //连接至远程主机
                me.Message = "已连接至服务器 [" + client.RemoteEndPoint.ToString() + "] ！";
                //开启接受消息线程
                Thread thr = new Thread(reciveMsg);
                thr.IsBackground = true;
                thr.Start();
            }
            catch
            {
                me.Message = "连接到服务器时发生异常 ！";
                client = null;
            }
            finally
            {
                me.Message = "系统消息";
                me.time = "【" + DateTime.Now.ToString() + "】";
                listBox1.Items.Add(me);
              //listBox1.ScrollIntoView(me);
            }
        }
        /// <summary>
        /// 循环接受来自服务器的消息
        /// </summary>
        private void reciveMsg()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1500]; //声明消息缓冲区
                    int size = client.Receive(buffer); //接受消息存储到缓冲区，并返回消息字节数
                    if (size == 0) break;
                    string msg = Encoding.UTF8.GetString(buffer, 0, size); //信息转码
                    //显示信息
                    Invoke(new Action(() =>
                    {
                        MsgEntity me = new MsgEntity();
                        me.address = "来自：" + client.RemoteEndPoint.ToString();
                        me.time = DateTime.Now.ToString();
                        me.Message = msg;
                        listBox1.Items.Add(me);
                    }));
                }
                catch
                {
                    break;
                }
            }
        }

        //发送消息
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(listBox2.Text))
            {
                MessageBox.Show("您发送的消息不能为空！");
                return;
            }
            if (listBox2.Text.Length > 500)
            {
                MessageBox.Show("您输入的消息过长！");
                return;
            }
            MsgEntity me = new MsgEntity(); //消息实体
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(listBox2.Text);
                client.Send(buffer);
                me.Message = listBox2.Text;
            }
            catch
            {
                me.Message = "通信发生异常，消息发送失败！";
            }
            finally
            {
                me.time = "【" + DateTime.Now.ToString() + "】";
                me.address = "发送至：" + client.RemoteEndPoint.ToString();
                listBox2.Items.Add(me);
                listBox2.ClearSelected();
            }
        }
    }
}
