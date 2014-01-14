using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RealtyParser;

namespace RealtyParserEditor
{
    public partial class SiteForm : Form, IChildFormInterface
    {
        private readonly RealtyParserDatabase _database = RealtyParserUtils.GetDatabase();

        public SiteForm()
        {
            InitializeComponent();
            Dictionary<long, string> sites = _database.GetTable("Site");
            foreach (var site in sites)
            {
                comboBox1.Items.Add(site);
            }
            comboBox1.SelectedItem = sites.FirstOrDefault();
        }

        public void Save()
        {
            _database.SaveProperties((SiteProperties) propertyGridControl1.SelectedObject);
        }

        public void Reload()
        {
            propertyGridControl1.SelectedObject = _database.GetProperties(((KeyValuePair<long, string>)comboBox1.SelectedItem).Key);
        }

        private void comboBox1_SelectedValueChanged(object sender, System.EventArgs e)
        {
            Reload();
        }
    }
}
