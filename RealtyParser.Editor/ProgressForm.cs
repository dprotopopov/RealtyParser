using System;
using System.Threading;
using System.Windows.Forms;
using MyLibrary.Trace;
using MyParser;

namespace RealtyParser.Editor
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        private Thread Thread { get; set; }
        public Builder Builder { private get; set; }

        public void ProgressCallback(long current, long total)
        {
            if (progressBar1.InvokeRequired)
            {
                ProgressCallback d = ProgressCallback;
                object[] objects = {current, total};
                Invoke(d, objects);
            }
            else
            {
                progressBar1.Maximum = (int) Math.Min(total, 10000);
                progressBar1.Value = (int)Math.Min(current * progressBar1.Maximum / total, progressBar1.Maximum);
            }
        }

        public void CompliteCallback()
        {
            if (simpleButton1.InvokeRequired)
            {
                CompliteCallback d = CompliteCallback;
                object[] objects = {};
                Invoke(d, objects);
            }
            else
            {
                Close();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Thread != null) Thread.Abort();
            Close();
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            if (Builder != null)
            {
                Thread = new Thread(Builder.ThreadProc);
                Thread.Start(Builder);
                simpleButton1.Enabled = true;
            }
            else Close();
        }
    }
}