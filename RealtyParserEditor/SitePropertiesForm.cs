using System.Collections;
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
            foreach (var item in sites)
            {
                comboBoxSites.Items.Add(item);
            }
            comboBoxSites.SelectedItem = sites.FirstOrDefault();
        }

        public void Save()
        {
        }

        public void Reload()
        {
            propertyGridControlMappingItem.SelectedObject = null;
            propertyGridControlReturnFieldInfo.SelectedObject = null;
            listBoxMappingItems.Items.Clear();
            comboBoxMapping.Items.Clear();
            listBoxReturnFieldInfos.Items.Clear();
            propertyGridControlReturnFieldInfo.SelectedObject = null;
            propertyGridControlSiteProperties.SelectedObject = null;
            comboBoxSites.Items.Clear();
            Dictionary<long, string> sites = _database.GetDictionary<long>("Site");
            foreach (var site in sites)
            {
                comboBoxSites.Items.Add(site);
            }
            comboBoxSites.SelectedItem = sites.FirstOrDefault();
        }

        private void comboBox1_SelectedValueChanged(object sender, System.EventArgs e)
        {
            propertyGridControlMappingItem.SelectedObject = null;
            propertyGridControlReturnFieldInfo.SelectedObject = null;
            listBoxMappingItems.Items.Clear();
            comboBoxMapping.Items.Clear();
            listBoxReturnFieldInfos.Items.Clear();
            propertyGridControlReturnFieldInfo.SelectedObject = null;
            propertyGridControlSiteProperties.SelectedObject = _database.GetSiteProperties(((KeyValuePair<long, string>)comboBoxSites.SelectedItem).Key);
            foreach (var returnField in  ((SiteProperties) propertyGridControlSiteProperties.SelectedObject).ReturnFieldInfos)
            {
                listBoxReturnFieldInfos.Items.Add(returnField);
            }
            foreach (var item in ((SiteProperties)propertyGridControlSiteProperties.SelectedObject).Mapping)
            {
                comboBoxMapping.Items.Add(item);
            }
        }

        private void listBoxReturnFieldInfos_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            propertyGridControlReturnFieldInfo.SelectedObject = listBoxReturnFieldInfos.SelectedItem;
        }

        private void comboBoxMapping_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            propertyGridControlMappingItem.SelectedObject = null;
            listBoxMappingItems.Items.Clear();
            foreach (var item in ((KeyValuePair<string,Dictionary<long,string>>) comboBoxMapping.SelectedItem).Value)
            {
                listBoxMappingItems.Items.Add(item);
            }
        }

        private void listBoxMappingItems_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            propertyGridControlMappingItem.SelectedObject = listBoxMappingItems.SelectedItem;
        }
    }
}
