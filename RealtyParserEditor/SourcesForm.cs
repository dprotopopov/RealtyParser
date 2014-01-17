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
            propertyGridControlBind.SelectedObject = new Bind();
        }

        public void Save()
        {
            listBoxSources.Items.Clear();
            RealtyParserParsingModule parsingModule = new RealtyParserParsingModule();
            foreach (var item in parsingModule.Sources(propertyGridControlBind.SelectedObject as Bind))
            {
                listBoxSources.Items.Add(item);
            }
        }

        public void Reload()
        {
            listBoxSources.Items.Clear();
            propertyGridControlBind.SelectedObject = new Bind();
        }
    }
}
