
namespace RosrealtPrepare
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private LinkForm _linkForm;
        private RegionForm _regionForm;
        private ActionForm _actionForm;
        private RubricForm _rubricForm;
        private MappingForm _mappingForm;
        public MainForm()
        {
            InitializeComponent();
            _linkForm = new LinkForm { MdiParent = this };
            _regionForm = new RegionForm { MdiParent = this };
            _actionForm = new ActionForm { MdiParent = this };
            _rubricForm = new RubricForm { MdiParent = this };
            _mappingForm = new MappingForm { MdiParent = this };
            _linkForm.Show();
            _regionForm.Show();
            _actionForm.Show();
            _rubricForm.Show();
            _mappingForm.Show();
        }
    }
}
