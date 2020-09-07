using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Notify
{
    public partial class Toast : Form
    {
        
        public Toast()
        {
            InitializeComponent();
            Region = Region.FromHrgn(Win32.CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }

        public Toast(string title) : this()
        {
            label1.Text = title;
        }

        private void Toast_KeyUp(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("Key code {0}", e.KeyCode);
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Toast_Load(object sender, EventArgs e)
        {
            var virtualScreen = SystemInformation.VirtualScreen;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(virtualScreen.X, (virtualScreen.Height - 200));
            var tmr = new Timer();
            tmr.Tick += delegate { Close(); };
            tmr.Interval = (int)TimeSpan.FromSeconds(10).TotalMilliseconds;
            tmr.Start();
        }
    }
}
