using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RosrealtPrepare
{
    public partial class ActionForm : Form
    {
        public ActionForm()
        {
            InitializeComponent();
        }

        private async void ActionForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = await RosrealtClass.GetActionSql();
        }
    }
}
