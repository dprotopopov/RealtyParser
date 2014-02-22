using System.Linq;
using System.Windows.Forms;
using RealtyParser;
using RT.ParsingLibs.Models;

namespace RealtyParserEditor.Children
{
    public partial class SourcesForm : Form, IChildForm
    {
        private static readonly ParserModule ParserModule = new ParserModule();

        public SourcesForm()
        {
            InitializeComponent();
            propertyGridControlBind.SelectedObject = new Bind();
        }

        public void Save()
        {
        }

        public void ClearResults()
        {
            listBoxSources.Items.Clear();
            propertyGridControlBind.SelectedObject = new Bind();
        }

        public void Execute()
        {
            listBoxSources.Items.Clear();
            listBoxSources.Items.AddRange(ParserModule.Sources(propertyGridControlBind.SelectedObject as Bind).ToArray());
        }
    }
}