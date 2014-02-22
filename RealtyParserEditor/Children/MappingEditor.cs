using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RealtyParser;
using RealtyParser.Collections;

namespace RealtyParserEditor.Children
{
    public partial class MappingEditor : Form, IChildForm
    {
        private static readonly Builder Builder = new Builder();
        private static readonly Database Database = Builder.Database;
        private readonly ProgressForm _progressForm = new ProgressForm();

        public MappingEditor()
        {
            InitializeComponent();
            Mapping sites = Database.GetMapping("Site");
            repositoryItemComboBoxSite.Items.AddRange(sites.ToArray());
            IEnumerable<object> mapping = Database.GetList("Mapping", "TableName");
            repositoryItemComboBoxTableName.Items.AddRange(mapping.ToArray());
            for (int level = 0; level < 10; level++) repositoryItemComboBoxLevel.Items.Add(level);
            propertyGridControlWorkspace.SelectedObject = Builder;
            gridControlMapping.DataSource = Builder.GridItems;
            _progressForm.Builder = Builder;
            Builder.AppendLineCallback = AppendLineCallback;
        }

        public async void Save()
        {
            if (Builder.TableName == null || Builder.SiteId == null) return;
            Builder.Total = 0;
            Builder.Current = 0;
            textBoxCommandText.Text = "";
            Builder.CompliteCallback = _progressForm.CompliteCallback;
            Builder.ProgressCallback = _progressForm.ProgressCallback;

            gridControlMapping.DefaultView.RefreshData();

            Builder.MethodInfo = typeof (Builder).GetMethod("RefreshGridItems", new Type[] {});
            _progressForm.simpleButton1.Enabled = false;
            _progressForm.ShowDialog();

            Builder.CompliteCallback = null;
            Builder.ProgressCallback = null;
            gridControlMapping.Refresh();
            textBoxCommandText.Refresh();

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
            if (Builder.TableName == null || Builder.SiteId == null) return;
            Builder.Total = 0;
            Builder.Current = 0;
            repositoryItemComboBoxLeft.Items.Clear();
            repositoryItemComboBoxRight.Items.Clear();
            textBoxCommandText.Text = "";
            Builder.CompliteCallback = _progressForm.CompliteCallback;
            Builder.ProgressCallback = _progressForm.ProgressCallback;

            Builder.MethodInfo = typeof (Builder).GetMethod("BuildMappingData", new Type[] {});
            _progressForm.simpleButton1.Enabled = false;
            _progressForm.ShowDialog();
            repositoryItemComboBoxLeft.Items.AddRange(Builder.TitleData.Key);
            repositoryItemComboBoxRight.Items.AddRange(Builder.TitleData.Value);

            Builder.MethodInfo = typeof (Builder).GetMethod("BuildMapping", new Type[] {});
            _progressForm.simpleButton1.Enabled = false;
            _progressForm.ShowDialog();

            gridControlMapping.DefaultView.RefreshData();

            Builder.MethodInfo = typeof (Builder).GetMethod("RefreshGridItems", new Type[] {});
            _progressForm.simpleButton1.Enabled = false;
            _progressForm.ShowDialog();

            Builder.CompliteCallback = null;
            Builder.ProgressCallback = null;
            gridControlMapping.Refresh();
            textBoxCommandText.Refresh();
        }

        public void Execute()
        {
            if (Builder.TableName == null || Builder.SiteId == null) return;
            Builder.Total = 0;
            Builder.Current = 0;
            repositoryItemComboBoxLeft.Items.Clear();
            repositoryItemComboBoxRight.Items.Clear();
            textBoxCommandText.Text = "";

            Builder.CompliteCallback = _progressForm.CompliteCallback;
            Builder.ProgressCallback = _progressForm.ProgressCallback;
            Builder.MethodInfo = typeof (Builder).GetMethod("BuildMappingData", new Type[] {});
            _progressForm.simpleButton1.Enabled = false;
            _progressForm.ShowDialog();

            repositoryItemComboBoxLeft.Items.AddRange(Builder.TitleData.Key);
            repositoryItemComboBoxRight.Items.AddRange(Builder.TitleData.Value);
            Builder.MethodInfo = typeof (Builder).GetMethod("BuildGridItems", new Type[] {});
            _progressForm.simpleButton1.Enabled = false;
            _progressForm.ShowDialog();

            gridControlMapping.DefaultView.RefreshData();

            _progressForm.simpleButton1.Enabled = false;
            Builder.MethodInfo = typeof (Builder).GetMethod("RefreshGridItems", new Type[] {});
            _progressForm.ShowDialog();

            Builder.CompliteCallback = null;
            Builder.ProgressCallback = null;
            gridControlMapping.Refresh();
            textBoxCommandText.Refresh();
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