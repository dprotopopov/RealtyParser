using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RealtyParser;

namespace RealtyParserEditor
{
    public partial class SitePropertiesForm : Form, IChildForm
    {
        private readonly Database _database = RealtyParserParsingModule.Database;

        public SitePropertiesForm()
        {
            InitializeComponent();
            Dictionary<object, object> sites = _database.GetDictionary("Site");
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
            Dictionary<object, object> sites = _database.GetDictionary("Site");
            foreach (var site in sites)
            {
                comboBoxSites.Items.Add(site);
            }
            comboBoxSites.SelectedItem = sites.FirstOrDefault();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            propertyGridControlMappingItem.SelectedObject = null;
            propertyGridControlReturnFieldInfo.SelectedObject = null;
            listBoxMappingItems.Items.Clear();
            comboBoxMapping.Items.Clear();
            listBoxReturnFieldInfos.Items.Clear();
            propertyGridControlReturnFieldInfo.SelectedObject = null;
            propertyGridControlSiteProperties.SelectedObject =
                _database.GetSiteProperties(((KeyValuePair<object, object>)comboBoxSites.SelectedItem).Key);
            ReturnFieldInfos returnFieldInfos =
                _database.GetReturnFieldInfos(((KeyValuePair<object, object>)comboBoxSites.SelectedItem).Key);
            foreach (
                ReturnFieldInfo returnField in returnFieldInfos)
            {
                listBoxReturnFieldInfos.Items.Add(returnField);
            }
            Mapping mapping = _database.GetMapping(((KeyValuePair<object, object>)comboBoxSites.SelectedItem).Key);
            foreach (var item in mapping)
            {
                comboBoxMapping.Items.Add(item);
            }
        }

        private void listBoxReturnFieldInfos_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlReturnFieldInfo.SelectedObject = listBoxReturnFieldInfos.SelectedItem;
        }

        private void comboBoxMapping_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlMappingItem.SelectedObject = null;
            listBoxMappingItems.Items.Clear();
            foreach (var item in ((KeyValuePair<string, Dictionary<object, object>>)comboBoxMapping.SelectedItem).Value)
            {
                listBoxMappingItems.Items.Add(item);
            }
        }

        private void listBoxMappingItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlMappingItem.SelectedObject = listBoxMappingItems.SelectedItem;
        }
    }
}