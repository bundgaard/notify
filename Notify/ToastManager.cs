using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Notify
{
    public class ToastManager
    {
        public ToastManager()
        {

        }

        public ToastManager(TcpClient client)
        {

        }
        public void ShowToast()
        {
            var t = new Toast("Hello, World");
            t.Show();
        }
    }
}
