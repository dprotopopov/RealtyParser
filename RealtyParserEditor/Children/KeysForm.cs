using System;
using System.Linq;
using System.Windows.Forms;
using RealtyParser;

namespace RealtyParserEditor.Children
{
    public partial class KeysForm : Form, IChildForm
    {
        private static readonly ParserModule ParserModule = new ParserModule();

        public KeysForm()
        {
            InitializeComponent();
        }

        public void Save()
        {
        }

        public void ClearResults()
        {
            propertyGridControlBind.SelectedObject = null;
            listBoxKeys.Items.Clear();
        }

        public void Execute()
        {
            propertyGridControlBind.SelectedObject = null;
            listBoxKeys.Items.Clear();
            listBoxKeys.Items.AddRange(ParserModule.Keys().Cast<object>().ToArray());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlBind.SelectedObject = listBoxKeys.SelectedItem;
        }
    }
}