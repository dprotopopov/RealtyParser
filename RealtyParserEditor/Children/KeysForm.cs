using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RealtyParser;
using RealtyParser.Mirkvartir;
using RealtyParser.Rosrealt;
using RT.ParsingLibs;

namespace RealtyParserEditor.Children
{
    public partial class KeysForm : Form, IChildForm
    {
        public KeysForm()
        {
            InitializeComponent();
            listBoxDll.Items.AddRange(
                new List<ParserModule>
                {
                    new ParserModule(),
                    new RosrealtParser(),
                    new MirkvartirParser(),
                }.Select(item => new KeyValuePair<string, IParsingModule>(item.ModuleClassname, item))
                    .Cast<object>().ToArray());
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
            if (listBoxDll.SelectedItem == null) return;
            IParsingModule module = ((KeyValuePair<string, IParsingModule>) listBoxDll.SelectedItem).Value;
            propertyGridControlBind.SelectedObject = null;
            listBoxKeys.Items.Clear();
            //listBoxKeys.Items.AddRange(module.Keys().Cast<object>().ToArray());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlBind.SelectedObject = listBoxKeys.SelectedItem;
        }
    }
}