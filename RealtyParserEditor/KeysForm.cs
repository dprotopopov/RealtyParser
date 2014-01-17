using System;
using System.Windows.Forms;
using RealtyParser;

namespace RealtyParserEditor
{
    public partial class KeysForm : Form, IChildFormInterface
    {
        public KeysForm()
        {
            InitializeComponent();
            RealtyParserParsingModule parsingModule = new RealtyParserParsingModule();
            foreach (var item in parsingModule.Keys())
            {
                listBoxKeys.Items.Add(item);
            }
        }

        public void Save()
        {
        }

        public void Reload()
        {
            propertyGridControlBind.SelectedObject = null;
            RealtyParserParsingModule parsingModule = new RealtyParserParsingModule();
            foreach (var item in parsingModule.Keys())
            {
                listBoxKeys.Items.Add(item);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlBind.SelectedObject = listBoxKeys.SelectedItem;
        }
    }
}
