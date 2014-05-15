﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RealtyParser.Arenda66;
using RealtyParser.BeBoss;
using RealtyParser.Bn;
using RealtyParser.Citystar;
using RealtyParser.Collections;
using RealtyParser.Egent;
using RealtyParser.Gdeetotdom;
using RealtyParser.Kvadrat66;
using RealtyParser.Mirkvartir;
using RealtyParser.NetAgenta;
using RealtyParser.Rosrealt;
using RealtyParser.Russianrealty;
using RealtyParser.Sdamka;
using RealtyParser.Upn;
using RealtyParser.Uralstudent;
using RT.ParsingLibs;

namespace RealtyParser.Editor.Children
{
    public partial class AboutForm : Form, IChildForm
    {
        public AboutForm()
        {
            InitializeComponent();
            listBoxDll.Items.AddRange(
                new StackListQueue<ParserModule>
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