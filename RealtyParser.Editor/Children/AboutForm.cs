﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RT.ParsingLibs;

namespace RealtyParser.Editor.Children
{
    public partial class AboutForm : Form, IChildForm
    {
        public AboutForm()
        {
            InitializeComponent();
            listBoxParserModule.Items.AddRange(
                MainForm.ParserModules.Select(
                    item => new KeyValuePair<string, IParsingModule>(item.ModuleClassname, item))
                    .Cast<object>().ToArray());
        }

        public void Save()
        {
        }

        public void ClearResults()
        {
            propertyGridControl1.SelectedObject = null;
        }

        public void Execute()
        {
            if (listBoxParserModule.SelectedItem == null) return;
            IParsingModule module = ((KeyValuePair<string, IParsingModule>) listBoxParserModule.SelectedItem).Value;
            propertyGridControl1.SelectedObject = module.About();
        }

        private void listBoxDll_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxParserModule.SelectedItem == null) return;
            IParsingModule module = ((KeyValuePair<string, IParsingModule>) listBoxParserModule.SelectedItem).Value;
            propertyGridControl1.SelectedObject = module.About();
        }
    }
}