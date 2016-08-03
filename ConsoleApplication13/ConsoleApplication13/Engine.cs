using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication13
{
    public class Engine : IEngine
    {
        public  void StartAsync()
        {

            WebClient wc = new WebClient();
            Action<object> d = x => wc.DownloadString(new Uri("www.google.com"));
            d.BeginInvoke(null, (x) =>
            {
                IsReadyToGo = true;
                StartCompleted?.Invoke(this, new EventArgs());
            }, null);
        }

        private void Wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            IsReadyToGo = true;
            StartCompleted?.Invoke(this, new EventArgs());
        }

        public bool IsReadyToGo { get; private set; }
        public event EventHandler StartCompleted;

    }
}
