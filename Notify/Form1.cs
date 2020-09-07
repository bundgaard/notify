using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notify
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Du klikkede");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Icon = SystemIcons.Shield;
            var t1 = new Thread(() =>
            {
                Debug.WriteLine("Hello, World from Thread");
                var listener = TcpListener.Create(10555);

                listener.Start(); // listen
                Debug.WriteLine("started server :10555");

                while (true)
                {
                    var client = listener.AcceptTcpClient();
                    Byte[] bytes;
                    new Thread(() =>
                    {
                        var networkStream = client.GetStream();
                        if (client.ReceiveBufferSize > 0)
                        {
                            bytes = new byte[client.ReceiveBufferSize];
                            var readSize = networkStream.Read(bytes, 0, client.ReceiveBufferSize);

                            var text = Encoding.UTF8.GetString(bytes.Take(readSize).ToArray());
                            Debug.WriteLine($"Read from network stream {readSize} {bytes} {text}");
                            notifyIcon1.BalloonTipText = text;
                            notifyIcon1.ShowBalloonTip(5000);
                            var t = new Toast(text);
                            
                            Application.Run(t);
                            

                        }

                        client.Close();

                    }).Start();
                }
            });
            t1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(3000);
            var virtualScreen = SystemInformation.VirtualScreen;
            for (var i = 0; i < 10; i++)
            {
                Toast t = new Toast($"i = {i}");
                t.StartPosition = FormStartPosition.Manual;
                t.Location = new Point(virtualScreen.X, (virtualScreen.Height - 200) - (100 * i)/* - */);
                t.Show();
            }

        }
    }
}
