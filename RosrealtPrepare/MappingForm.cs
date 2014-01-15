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
    public partial class MappingForm : Form
    {
        public MappingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = RosrealtClass.GetMappingSql(comboBox1.Text);
        }
    }
}
