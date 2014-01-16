using System;
using System.Windows.Forms;
using RealtyParser;
using RT.ParsingLibs.Requests;

namespace RealtyParserEditor
{
    public partial class ResultForm : Form, IChildFormInterface
    {
        public ResultForm()
        {
            InitializeComponent();
            propertyGridControl1.SelectedObject = new ParseRequest();
        }

        public async void Save()
        {
            RealtyParserParsingModule parsingModule = new RealtyParserParsingModule();
            propertyGridControl2.SelectedObject = await parsingModule.Result(propertyGridControl1.SelectedObject as ParseRequest);
        }

        public void Reload()
        {
            propertyGridControl1.SelectedObject = new ParseRequest();
            propertyGridControl2.SelectedObject = null;
        }
    }
}
