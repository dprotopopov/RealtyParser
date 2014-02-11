using System;
using System.Windows.Forms;
using RealtyParser;
using RT.ParsingLibs.Models;

namespace RealtyParserEditor
{
    public partial class KeysForm : Form, IChildForm
    {
        public KeysForm()
        {
            InitializeComponent();
            var parsingModule = new RealtyParserParsingModule();
            foreach (Bind item in parsingModule.Keys())
            {
                //listBoxKeys.Items.Add(item);
            }
        }

        public void Save()
        {
        }

        public void Reload()
        {
            propertyGridControlBind.SelectedObject = null;
            var parsingModule = new RealtyParserParsingModule();
            foreach (Bind item in parsingModule.Keys())
            {
                //listBoxKeys.Items.Add(item);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlBind.SelectedObject = listBoxKeys.SelectedItem;
        }
    }
}