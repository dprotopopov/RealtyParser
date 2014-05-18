using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RT.ParsingLibs;
using RT.ParsingLibs.Models;

namespace RealtyParser.Editor.Children
{
    public partial class SourcesForm : Form, IChildForm
    {
        public SourcesForm()
        {
            InitializeComponent();
            listBoxParserModule.Items.AddRange(
                MainForm.ParserModules.Select(
                    item => new KeyValuePair<string, IParsingModule>(item.ModuleClassname, item))
                    .Cast<object>().ToArray());
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
            if (listBoxParserModule.SelectedItem == null) return;
            IParsingModule module = ((KeyValuePair<string, IParsingModule>) listBoxParserModule.SelectedItem).Value;
            listBoxSources.Items.Clear();
            listBoxSources.Items.AddRange(
                module.Sources(propertyGridControlBind.SelectedObject as Bind).Cast<object>().ToArray());
        }
    }
}