using System;
using System.Linq;
using System.Windows.Forms;
using RealtyParser;

namespace RealtyParserEditor
{
    public partial class KeysForm : Form, IChildForm
    {
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
            var module = new ParsingModule();
            listBoxKeys.Items.AddRange(module.Keys().Cast<object>().ToArray());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlBind.SelectedObject = listBoxKeys.SelectedItem;
        }
    }
}