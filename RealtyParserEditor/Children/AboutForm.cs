﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RealtyParser;
using RealtyParser.Mirkvartir;
using RealtyParser.Rosrealt;
using RT.ParsingLibs;

namespace RealtyParserEditor.Children
{
    public partial class AboutForm : Form, IChildForm
    {
        public AboutForm()
        {
            InitializeComponent();
            listBoxDll.Items.AddRange(
                new List<ParserModule>
                {
                    new ParserModule(),
                    new RosrealtParser(),
                    new MirkvartirParser(),
                }.Select(item => new KeyValuePair<string, IParsingModule>(item.ModuleClassname, item))
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
            if (listBoxDll.SelectedItem == null) return;
            IParsingModule module = ((KeyValuePair<string, IParsingModule>) listBoxDll.SelectedItem).Value;
            propertyGridControl1.SelectedObject = module.About();
        }

        private void listBoxDll_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxDll.SelectedItem == null) return;
            IParsingModule module = ((KeyValuePair<string, IParsingModule>) listBoxDll.SelectedItem).Value;
            propertyGridControl1.SelectedObject = module.About();
        }
    }
}