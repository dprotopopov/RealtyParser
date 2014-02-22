using System.Windows.Forms;
using RealtyParser;

namespace RealtyParserEditor.Children
{
    public partial class AboutForm : Form, IChildForm
    {
        private static readonly ParserModule ParserModule = new ParserModule();

        public AboutForm()
        {
            InitializeComponent();
            propertyGridControl1.SelectedObject = ParserModule.About();
        }

        public void Save()
        {
        }

        public void ClearResults()
        {
            propertyGridControl1.SelectedObject = null;
        }

        public void Execute()
        {
            propertyGridControl1.SelectedObject = ParserModule.About();
        }
    }
}