using System.Windows.Forms;

namespace RosrealtPrepare
{
    public partial class LinkForm : Form
    {
        public LinkForm()
        {
            InitializeComponent();
        }

        private async void RegionForm_Load(object sender, System.EventArgs e)
        {
            textBox1.Text = await RosrealtClass.GetLinks();            
        }

    }
}
