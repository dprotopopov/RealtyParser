using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MyLibrary.Collections;
using MyLibrary.Trace;
using MyParser;

namespace RealtyParser.Editor.Children
{
    public partial class BuilderForm : Form, IChildForm
    {
        private static readonly string ModuleClassname = typeof (ParserModule).Namespace;

        private static readonly IDatabase Database = new Database
        {
            ModuleClassname = ModuleClassname,
        };

        private static readonly Builder Builder = new Builder
        {
            ModuleClassname = ModuleClassname,
            Database = Database
        };


        private readonly ProgressForm _progressForm = new ProgressForm();

        public BuilderForm()
        {
            InitializeComponent();

            Database.Connect();

            Mapping sites = Database.GetMapping("Site");
            repositoryItemComboBoxSite.Items.AddRange(sites.ToArray());
            IEnumerable<object> mapping = Database.GetList("Mapping", "TableName");
            IEnumerable<object> fielding = Database.GetList("Fielding", "FieldName");
            repositoryItemComboBoxTableName.Items.AddRange(mapping.ToArray());
            repositoryItemComboBoxFieldName.Items.AddRange(fielding.ToArray());
            object[] methods =
            {
                typeof (Builder).GetMethod("BuildGridItems", new Type[] {}),
                typeof (Builder).GetMethod("BuildMappingData", new Type[] {}),
                typeof (Builder).GetMethod("BuildMapping", new Type[] {}),
                typeof (Builder).GetMethod("RefreshGridItems", new Type[] {}),
                typeof (Builder).GetMethod("DownloadTable", new Type[] {}),
                typeof (Builder).GetMethod("DownloadTableField", new Type[] {}),
                typeof (Builder).GetMethod("ExecuteNonQuery", new Type[] {})
            };
            repositoryItemComboBoxMethodInfo.Items.AddRange(methods);
            for (int level = 0; level < 10; level++) repositoryItemComboBoxLevel.Items.Add(level);
            propertyGridControlWorkspace.SelectedObject = Builder;
            _progressForm.Builder = Builder;
            Builder.AppendLineCallback = AppendLineCallback;
            Builder.CompliteCallback = _progressForm.CompliteCallback;
            Builder.ProgressCallback = _progressForm.ProgressCallback;
            Builder.CommandText = textBoxCommandText.Text;
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
            if (Builder.TableName == null || Builder.SiteId == null || Builder.MethodInfo == null) return;
            Builder.Total = 0;
            Builder.Current = 0;
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