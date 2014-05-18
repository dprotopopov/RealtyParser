using System;
using System.Collections.Generic;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using RealtyParser.Arenda66;
using RealtyParser.BeBoss;
using RealtyParser.Bn;
using RealtyParser.Citystar;
using RealtyParser.Collections;
using RealtyParser.Editor.Children;
using RealtyParser.Egent;
using RealtyParser.Forms;
using RealtyParser.Gdeetotdom;
using RealtyParser.Kvadrat66;
using RealtyParser.Mail;
using RealtyParser.Mirkvartir;
using RealtyParser.NetAgenta;
using RealtyParser.Rosrealt;
using RealtyParser.Russianrealty;
using RealtyParser.Sdamka;
using RealtyParser.Upn;
using RealtyParser.Uralstudent;

namespace RealtyParser.Editor
{
    public partial class MainForm : RibbonForm
    {
        public static readonly IEnumerable<ParserModule> ParserModules = new StackListQueue<ParserModule>
        {
            new ParserModule(),
            new RosrealtParser(),
            new MirkvartirParser(),
            new EgentParser(),
            new NetAgentaParser(),
            new BnParser(),
            new BeBossParser(),
            new Arenda66Parser(),
            new UpnParser(),
            new Kvadrat66Parser(),
            new SdamkaParser(),
            new UralstudentParser(),
            new CitystarParser(),
            new GdeetotdomParser(),
            new RussianrealtyParser(),
            new MailParser(),
        };

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