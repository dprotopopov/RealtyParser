using System.Windows.Forms;
using RealtyParser;
using RT.ParsingLibs.Models;

namespace RealtyParserEditor
{
    public partial class SourcesForm : Form, IChildForm
    {
        public SourcesForm()
        {
            InitializeComponent();
            propertyGridControlBind.SelectedObject = new Bind();
        }

        public void Save()
        {
            listBoxSources.Items.Clear();
            var parsingModule = new RealtyParserParsingModule();
            foreach (string item in parsingModule.Sources(propertyGridControlBind.SelectedObject as Bind))
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