using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private TaskFactory _factory = new TaskFactory(new MyScheduler());
        public Form1()
        {
            InitializeComponent();
        }

       private TaskCompletionSource<object> _source = new TaskCompletionSource<object>();
        private int count = 0;
        private void _okButton_Click(object sender, EventArgs e)
        {

            Task.Run(() => backgroundWorker1_DoWork());
            var firstTask = _source.Task;
            firstTask.ContinueWith(
                x => BeginInvoke(new Action(() => progressBar1.Value = count)));
            firstTask.ContinueWith((x) => this.Invoke(new Action(()=> MessageBox.Show("BLBLAL")))).ContinueWith((x) => this.BeginInvoke(new Action(() => MessageBox.Show("BLBLaaaaaaaAL"))));
        } 

        private void backgroundWorker1_DoWork()
        {
            var source  = new TaskCompletionSource<object>();

            while (++count < 100 )
            {
                  Thread.Sleep(50);
                progressBar1.BeginInvoke(new Action(() => progressBar1.Value = count));
            }
            _source.SetResult(null);
            count = 0;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
           
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 0;
        }
    }
}
