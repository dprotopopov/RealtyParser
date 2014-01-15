using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RealtyParser;

namespace RealtyParserEditor
{
    public partial class SitePropertiesForm : Form, IChildFormInterface
    {
        private readonly RealtyParserDatabase _database = RealtyParserUtils.GetDatabase();

        public SitePropertiesForm()
        {
            InitializeComponent();
            Dictionary<long, string> sites = _database.GetDictionary<long>("Site");
            foreach (var site in sites)
            {
                comboBox1.Items.Add(site);
            }
            comboBox1.SelectedItem = sites.FirstOrDefault();
        }

        public void Save()
        {
            _database.SaveSiteProperties(propertyGridControl1.SelectedObject as SiteProperties);
        }

        public void Reload()
        {
            comboBox1.Items.Clear();
            propertyGridControl1.SelectedObject = null;
            Dictionary<long, string> sites = _database.GetDictionary<long>("Site");
            foreach (var site in sites)
            {
                comboBox1.Items.Add(site);
            }
            comboBox1.SelectedItem = sites.FirstOrDefault();
        }

        private void comboBox1_SelectedValueChanged(object sender, System.EventArgs e)
        {
            propertyGridControl1.SelectedObject = _database.GetSiteProperties(((KeyValuePair<long, string>)comboBox1.SelectedItem).Key);
        }
    }
}
