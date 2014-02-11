using System.Windows.Forms;
using RealtyParser;

namespace RealtyParserEditor
{
    public partial class AboutForm : Form, IChildForm
    {
        public AboutForm()
        {
            InitializeComponent();
            var parsingModule = new RealtyParserParsingModule();
            propertyGridControl1.SelectedObject = parsingModule.About();
        }

        public void Save()
        {
        }

        public void Reload()
        {
            var parsingModule = new RealtyParserParsingModule();
            propertyGridControl1.SelectedObject = parsingModule.About();
        }
    }
}