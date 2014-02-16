using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RealtyParser;
using RealtyParser.Collections;
using Type = System.Type;

namespace RealtyParserEditor
{
    public partial class BuilderForm : Form, IChildForm
    {
        private readonly Builder _builder = new Builder();
        private readonly ProgressForm _progressForm = new ProgressForm();

        public BuilderForm()
        {
            InitializeComponent();
            Mapping sites = ParsingModule.Database.GetMapping("Site");
            repositoryItemComboBoxSite.Items.AddRange(sites.ToArray());
            IEnumerable<object> mapping = ParsingModule.Database.GetList("Mapping", "TableName");
            repositoryItemComboBoxTableName.Items.AddRange(mapping.ToArray());
            object[] methods =
            {
                typeof (Builder).GetMethod("BuildGridItems", new Type[] {}),
                typeof (Builder).GetMethod("BuildMappingData", new Type[] {}),
                typeof (Builder).GetMethod("BuildMapping", new Type[] {}),
                typeof (Builder).GetMethod("RefreshGridItems", new Type[] {}),
                typeof (Builder).GetMethod("DownloadTable", new Type[] {}),
                typeof (Builder).GetMethod("ExecuteNonQuery", new Type[] {})
            };
            repositoryItemComboBoxMethodInfo.Items.AddRange(methods);
            for (int level = 0; level < 10; level++) repositoryItemComboBoxLevel.Items.Add(level);
            propertyGridControlWorkspace.SelectedObject = _builder;
            _progressForm.Builder = _builder;
            _builder.AppendLineCallback = AppendLineCallback;
            _builder.CompliteCallback = _progressForm.CompliteCallback;
            _builder.ProgressCallback = _progressForm.ProgressCallback;
            _builder.CommandText = textBoxCommandText.Text;
        }


        public async void Save()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = File.CreateText(saveFileDialog1.FileName))
                {
                    foreach (string line in textBoxCommandText.Lines)
                        await writer.WriteLineAsync(line);
                    writer.Close();
                }
            }
        }

        public void ClearResults()
        {
            textBoxCommandText.Clear();
        }

        public void Execute()
        {
            if (_builder.TableName == null || _builder.SiteId == null || _builder.MethodInfo == null) return;
            _builder.Total = 0;
            _builder.Current = 0;
            _progressForm.simpleButton1.Enabled = false;
            _progressForm.ShowDialog();
        }

        private void AppendLineCallback(string line)
        {
            if (textBoxCommandText.InvokeRequired)
            {
                AppendLineCallback d = AppendLineCallback;
                object[] objects = {line};
                Invoke(d, objects);
            }
            else
            {
                textBoxCommandText.AppendText(line + Environment.NewLine);
            }
        }
    }
}