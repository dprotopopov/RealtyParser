﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RealtyParser.BeBoss;
using RealtyParser.Bn;
using RealtyParser.Collections;
using RealtyParser.Egent;
using RealtyParser.Mirkvartir;
using RealtyParser.NetAgenta;
using RealtyParser.Rosrealt;
using RT.ParsingLibs;
using RT.ParsingLibs.Models;

namespace RealtyParser.Editor.Children
{
    public partial class SourcesForm : Form, IChildForm
    {
        public SourcesForm()
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
                }.Select(item => new KeyValuePair<string, IParsingModule>(item.ModuleClassname, item))
                    .Cast<object>().ToArray());
            propertyGridControlBind.SelectedObject = new Bind();
        }

        public void Save()
        {
        }

        public void ClearResults()
        {
            listBoxSources.Items.Clear();
            propertyGridControlBind.SelectedObject = new Bind();
        }

        public void Execute()
        {
            if (listBoxDll.SelectedItem == null) return;
            IParsingModule module = ((KeyValuePair<string, IParsingModule>) listBoxDll.SelectedItem).Value;
            listBoxSources.Items.Clear();
            listBoxSources.Items.AddRange(
                module.Sources(propertyGridControlBind.SelectedObject as Bind).Cast<object>().ToArray());
        }
    }
}