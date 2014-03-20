using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RealtyParser;
using RealtyParser.Collections;
using RealtyParser.Trace;

namespace RealtyParser.Editor.Children
{
    public partial class BuilderForm : Form, IChildForm
    {
        private static readonly Builder Builder = new Builder();
        private static readonly Database Database = Builder.Database;
        private readonly ProgressForm _progressForm = new ProgressForm();

        public BuilderForm()
        {
            InitializeComponent();
            
            Database.Connect();

            Mapping sites = Database.GetMapping("Site");
            repositoryItemComboBoxSite.Items.AddRange(sites.ToArray());
            IEnumerable<object> mapping = Database.GetList("Mapping", "TableName");
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