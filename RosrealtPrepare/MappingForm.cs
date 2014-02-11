using System;
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