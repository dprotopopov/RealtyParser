using System;

namespace RealtyParserEditor
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly SitePropertiesForm _siteForm;
        private readonly AboutForm _aboutForm;
        private readonly SourcesForm _sourcesForm;
        private readonly KeysForm _keysForm;
        private readonly ResultForm _resultForm;
        public MainForm()
        {
            InitializeComponent();
            _siteForm = new SitePropertiesForm { MdiParent = this };
            _aboutForm = new AboutForm { MdiParent = this };
            _sourcesForm = new SourcesForm { MdiParent = this };
            _keysForm = new KeysForm { MdiParent = this };
            _resultForm = new ResultForm { MdiParent = this };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _siteForm.Show();
            _sourcesForm.Show();
            _keysForm.Show();
            _resultForm.Show();
            _aboutForm.Show();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IChildFormInterface child = ActiveMdiChild as IChildFormInterface;
            if (child != null) child.Save();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IChildFormInterface child = ActiveMdiChild as IChildFormInterface;
            if (child != null) child.Reload();
        }
    }
}
