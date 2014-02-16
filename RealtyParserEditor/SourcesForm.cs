using System.Linq;
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
        }

        public void ClearResults()
        {
            listBoxSources.Items.Clear();
            propertyGridControlBind.SelectedObject = new Bind();
        }

        public void Execute()
        {
            var module = new ParsingModule();
            listBoxSources.Items.Clear();
            listBoxSources.Items.AddRange(module.Sources(propertyGridControlBind.SelectedObject as Bind).ToArray());
        }
    }
}