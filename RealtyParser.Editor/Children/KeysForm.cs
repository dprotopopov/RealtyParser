using System;
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
using RealtyParser.Sdamka;
using RealtyParser.Upn;
using RealtyParser.Uralstudent;
using RT.ParsingLibs;

namespace RealtyParser.Editor.Children
{
    public partial class KeysForm : Form, IChildForm
    {
        public KeysForm()
        {
            InitializeComponent();
            listBoxControlDll.Items.AddRange(
                new StackListQueue<ParserModule>
                {
//                    new ParserModule(),
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
                }.Select(item => new KeyValuePair<string, IParsingModule>(item.ModuleClassname, item))
                    .Cast<object>().ToArray());
        }

        public void Save()
        {
        }

        public void ClearResults()
        {
            propertyGridControlBind.SelectedObject = null;
            listBoxControlKeysActions.Items.Clear();
            listBoxControlKeysRubrics.Items.Clear();
            listBoxControlKeysRegions.Items.Clear();
        }

        public void Execute()
        {
            if (listBoxControlDll.SelectedItem == null) return;
            IParsingModule module = ((KeyValuePair<string, IParsingModule>) listBoxControlDll.SelectedItem).Value;
            propertyGridControlBind.SelectedObject = null;
            listBoxControlKeysActions.Items.Clear();
            listBoxControlKeysRubrics.Items.Clear();
            listBoxControlKeysRegions.Items.Clear();
            listBoxControlKeysActions.Items.AddRange(module.KeysActions().Cast<object>().ToArray());
            listBoxControlKeysRubrics.Items.AddRange(module.KeysRubrics().Cast<object>().ToArray());
            listBoxControlKeysRegions.Items.AddRange(module.KeysRegions().Cast<object>().ToArray());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlBind.SelectedObject = listBoxControlKeysActions.SelectedItem;
        }

        private void listBoxControlKeysRubrics_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlBind.SelectedObject = listBoxControlKeysRubrics.SelectedItem;
        }

        private void listBoxControlKeysRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControlBind.SelectedObject = listBoxControlKeysRegions.SelectedItem;
        }
    }
}