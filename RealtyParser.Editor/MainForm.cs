using System;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using RealtyParser.Editor.Children;
using RealtyParser.Forms;

namespace RealtyParser.Editor
{
    public partial class MainForm : RibbonForm
    {
        private readonly AboutForm _aboutForm;
        private readonly BuilderForm _builderForm;
        private readonly HapForm _hapForm;
        private readonly KeysForm _keysForm;
        private readonly MappingEditor _mappingEditor;
        private readonly ResultForm _resultForm;
        private readonly SitePropertiesForm _siteForm;
        private readonly SourcesForm _sourcesForm;

        public MainForm()
        {
            InitializeComponent();
            _siteForm = new SitePropertiesForm {MdiParent = this};
            _aboutForm = new AboutForm {MdiParent = this};
            _sourcesForm = new SourcesForm {MdiParent = this};
            _keysForm = new KeysForm {MdiParent = this};
            _resultForm = new ResultForm {MdiParent = this};
            _builderForm = new BuilderForm {MdiParent = this};
            _mappingEditor = new MappingEditor {MdiParent = this};
            _hapForm = new HapForm {MdiParent = this};
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _siteForm.Show();
            _sourcesForm.Show();
            _keysForm.Show();
            _resultForm.Show();
            _builderForm.Show();
            _aboutForm.Show();
            _mappingEditor.Show();
            _hapForm.Show();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            var child = ActiveMdiChild as IChildForm;
            if (child != null) child.Save();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            var child = ActiveMdiChild as IChildForm;
            if (child != null) child.ClearResults();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            var child = ActiveMdiChild as IChildForm;
            if (child != null) child.Execute();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            var aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }
    }
}