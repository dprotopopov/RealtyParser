using System.Windows.Forms;
using RealtyParser;
using RT.ParsingLibs.Models;

namespace RealtyParserEditor
{
    public partial class SourcesForm : Form, IChildFormInterface
    {
        public SourcesForm()
        {
            InitializeComponent();
            propertyGridControl1.SelectedObject = new Bind();
        }

        public void Save()
        {
            RealtyParserParsingModule parsingModule = new RealtyParserParsingModule();
            propertyGridControl2.SelectedObject = parsingModule.Sources(propertyGridControl1.SelectedObject as Bind);
        }

        public void Reload()
        {
            propertyGridControl1.SelectedObject = new Bind();
            propertyGridControl2.SelectedObject = null;
        }
    }
}
