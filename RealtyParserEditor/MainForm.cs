using System;

namespace RealtyParserEditor
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly SiteForm _siteForm;
        private readonly MappingForm _mappingForm;
        private readonly ReturnFieldForm _returnFieldForm;
        public MainForm()
        {
            InitializeComponent();
            _siteForm = new SiteForm { MdiParent = this };
            _mappingForm = new MappingForm { MdiParent = this };
            _returnFieldForm = new ReturnFieldForm { MdiParent = this };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _siteForm.Show();
            _mappingForm.Show();
            _returnFieldForm.Show();
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
