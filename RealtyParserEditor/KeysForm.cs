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
            propertyGridControl1.SelectedObject = parsingModule.Keys();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            RealtyParserParsingModule parsingModule = new RealtyParserParsingModule();
            propertyGridControl1.SelectedObject = parsingModule.Keys();
        }
    }
}
