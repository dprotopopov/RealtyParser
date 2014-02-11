using System.Windows.Forms;

namespace RosrealtPrepare
{
    public partial class MainForm : Form
    {
        private ActionForm _actionForm;
        private LinkForm _linkForm;
        private MappingForm _mappingForm;
        private RegionForm _regionForm;
        private RubricForm _rubricForm;

        public MainForm()
        {
            InitializeComponent();
            _linkForm = new LinkForm {MdiParent = this};
            _regionForm = new RegionForm {MdiParent = this};
            _actionForm = new ActionForm {MdiParent = this};
            _rubricForm = new RubricForm {MdiParent = this};
            _mappingForm = new MappingForm {MdiParent = this};
            _linkForm.Show();
            _regionForm.Show();
            _actionForm.Show();
            _rubricForm.Show();
            _mappingForm.Show();
        }
    }
}