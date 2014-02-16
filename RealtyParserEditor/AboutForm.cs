using System.Windows.Forms;
using RealtyParser;

namespace RealtyParserEditor
{
    public partial class AboutForm : Form, IChildForm
    {
        public AboutForm()
        {
            InitializeComponent();
            var module = new ParsingModule();
            propertyGridControl1.SelectedObject = module.About();
        }

        public void Save()
        {
        }

        public void ClearResults()
        {
            propertyGridControl1.SelectedObject = null;
        }

        public void Execute()
        {
            var module = new ParsingModule();
            propertyGridControl1.SelectedObject = module.About();
        }
    }
}