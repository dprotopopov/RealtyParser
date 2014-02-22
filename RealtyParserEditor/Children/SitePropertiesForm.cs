using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RealtyParser;
using RealtyParser.Collections;
using Mappings = RealtyParser.Mappings;

namespace RealtyParserEditor.Children
{
    public partial class SitePropertiesForm : Form, IChildForm
    {
        private static readonly ParserModule ParserModule = new ParserModule();
        private static readonly Database Database = ParserModule.Database;

        public SitePropertiesForm()
        {
            InitializeComponent();
            Mapping sites = Database.GetMapping(Database.SiteTable);
            comboBoxSites.Items.AddRange(sites.Cast<object>().ToArray());
            comboBoxSites.SelectedItem = sites.FirstOrDefault();
        }

        public void Save()
        {
        }

        public void ClearResults()
        {
            propertyGridControlMappingItem.SelectedObject = null;
            propertyGridControlReturnFieldInfo.SelectedObject = null;
            propertyGridControlBuilderInfo.SelectedObject = null;
            listBoxMappingItems.Items.Clear();
            comboBoxMapping.Items.Clear();
            listBoxReturnFieldInfos.Items.Clear();
            listBoxBuilderInfos.Items.Clear();
            propertyGridControlReturnFieldInfo.SelectedObject = null;
            propertyGridControlSiteProperties.SelectedObject = null;
            comboBoxSites.Items.Clear();
            Mapping sites = Database.GetMapping(Database.SiteTable);
            comboBoxSites.Items.AddRange(sites.Cast<object>().ToArray());
            comboBoxSites.SelectedItem = sites.FirstOrDefault();
        }

        public void Execute()
        {
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            propertyGridControlMappingItem.SelectedObject = null;
            propertyGridControlReturnFieldInfo.SelectedObject = null;
            propertyGridControlBuilderInfo.SelectedObject = null;
            listBoxMappingItems.Items.Clear();
            comboBoxMapping.Items.Clear();
            listBoxReturnFieldInfos.Items.Clear();
            listBoxBuilderInfos.Items.Clear();
            propertyGridControlReturnFieldInfo.SelectedObject = null;
            propertyGridControlSiteProperties.SelectedObject =
                Database.GetSiteProperties(((KeyValuePair<object, object>) comboBoxSites.SelectedItem).Key);
            ReturnFieldInfos returnFieldInfos =
                Database.GetReturnFieldInfos(((KeyValuePair<object, object>) comboBoxSites.SelectedItem).Key);
            BuilderInfos builderInfos =
                Database.GetBuilderInfos(((KeyValuePair<object, object>) comboBoxSites.SelectedItem).Key);
            Mappings mappings = Database.GetMappings(((KeyValuePair<object, object>) comboBoxSites.SelectedItem).Key);
            var list = returnFieldInfos.ToList().Select(item => new KeyValuePair<string, ReturnFieldInfo>(item.ReturnFieldId.ToString(), item));
            listBoxReturnFieldInfos.Items.AddRange(list.Cast<object>().ToArray());
            listBoxBuilderInfos.Items.AddRange(builderInfos.Cast<object>().ToArray());
            comboBoxMapping.Items.AddRange(mappings.Cast<object>().ToArray());
        }

        private void listBoxReturnFieldInfos_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlReturnFieldInfo.SelectedObject =
                ((KeyValuePair<string, ReturnFieldInfo>)listBoxReturnFieldInfos.SelectedItem).Value;
        }

        private void comboBoxMapping_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlMappingItem.SelectedObject = null;
            listBoxMappingItems.Items.Clear();
            Mapping mapping = ((KeyValuePair<string, Mapping>) comboBoxMapping.SelectedItem).Value;
            listBoxMappingItems.Items.AddRange(
                mapping.Cast<object>()
                    .ToArray());
        }

        private void listBoxMappingItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlMappingItem.SelectedObject = listBoxMappingItems.SelectedItem;
        }

        private void listBoxBuilderInfos_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlBuilderInfo.SelectedObject =
                ((KeyValuePair<string, BuilderInfo>) listBoxBuilderInfos.SelectedItem).Value;
        }
    }
}