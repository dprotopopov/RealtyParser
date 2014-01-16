using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RosrealtPrepare
{
    public partial class RubricForm : Form
    {
        public RubricForm()
        {
            InitializeComponent();
        }

        private async void RubricForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = await RosrealtClass.GetRubricSql();
        }
    }
}
