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
    public partial class MappingEditor : Form, IChildForm
    {
        private readonly Builder _builder = new Builder();
        private readonly ProgressForm _progressForm = new ProgressForm();

        public MappingEditor()
        {
            InitializeComponent();
            Mapping sites = ParsingModule.Database.GetMapping("Site");
            repositoryItemComboBoxSite.Items.AddRange(sites.ToArray());
            IEnumerable<object> mapping = ParsingModule.Database.GetList("Mapping", "TableName");
            repositoryItemComboBoxTableName.Items.AddRange(mapping.ToArray());
            for (int level = 0; level < 10; level++) repositoryItemComboBoxLevel.Items.Add(level);
            propertyGridControlWorkspace.SelectedObject = _builder;
            gridControlMapping.DataSource = _builder.GridItems;
            _progressForm.Builder = _builder;
            _builder.AppendLineCallback = AppendLineCallback;
        }

        public async void Save()
        {
            if (_builder.TableName == null || _builder.SiteId == null) return;
            _builder.Total = 0;
            _builder.Current = 0;
            textBoxCommandText.Text = "";
            _builder.CompliteCallback = _progressForm.CompliteCallback;
            _builder.ProgressCallback = _progressForm.ProgressCallback;
            
            gridControlMapping.DefaultView.RefreshData();

            _builder.MethodInfo = typeof (Builder).GetMethod("RefreshGridItems", new Type[] {});
            _progressForm.simpleButton1.Enabled = false;
            _progressForm.ShowDialog();
            
            _builder.CompliteCallback = null;
            _builder.ProgressCallback = null;
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
            if (_builder.TableName == null || _builder.SiteId == null) return;
            _builder.Total = 0;
            _builder.Current = 0;
            repositoryItemComboBoxLeft.Items.Clear();
            repositoryItemComboBoxRight.Items.Clear();
            textBoxCommandText.Text = "";
            _builder.CompliteCallback = _progressForm.CompliteCallback;
            _builder.ProgressCallback = _progressForm.ProgressCallback;

            _builder.MethodInfo = typeof (Builder).GetMethod("BuildMappingData", new Type[] {});
            _progressForm.simpleButton1.Enabled = false;
            _progressForm.ShowDialog();
            repositoryItemComboBoxLeft.Items.AddRange(_builder.TitleData.Key);
            repositoryItemComboBoxRight.Items.AddRange(_builder.TitleData.Value);

            _builder.MethodInfo = typeof (Builder).GetMethod("BuildMapping", new Type[] {});
            _progressForm.simpleButton1.Enabled = false;
            _progressForm.ShowDialog();

            gridControlMapping.DefaultView.RefreshData();

            _builder.MethodInfo = typeof (Builder).GetMethod("RefreshGridItems", new Type[] {});
            _progressForm.simpleButton1.Enabled = false;
            _progressForm.ShowDialog();

            _builder.CompliteCallback = null;
            _builder.ProgressCallback = null;
            gridControlMapping.Refresh();
            textBoxCommandText.Refresh();
        }

        public void Execute()
        {
            if (_builder.TableName == null || _builder.SiteId == null) return;
            _builder.Total = 0;
            _builder.Current = 0;
            repositoryItemComboBoxLeft.Items.Clear();
            repositoryItemComboBoxRight.Items.Clear();
            textBoxCommandText.Text = "";
            
            _builder.CompliteCallback = _progressForm.CompliteCallback;
            _builder.ProgressCallback = _progressForm.ProgressCallback;
            _builder.MethodInfo = typeof (Builder).GetMethod("BuildMappingData", new Type[] {});
            _progressForm.simpleButton1.Enabled = false;
            _progressForm.ShowDialog();

            repositoryItemComboBoxLeft.Items.AddRange(_builder.TitleData.Key);
            repositoryItemComboBoxRight.Items.AddRange(_builder.TitleData.Value);
            _builder.MethodInfo = typeof (Builder).GetMethod("BuildGridItems", new Type[] {});
            _progressForm.simpleButton1.Enabled = false;
            _progressForm.ShowDialog();
            
            gridControlMapping.DefaultView.RefreshData();
            
            _progressForm.simpleButton1.Enabled = false;
            _builder.MethodInfo = typeof (Builder).GetMethod("RefreshGridItems", new Type[] {});
            _progressForm.ShowDialog();
            
            _builder.CompliteCallback = null;
            _builder.ProgressCallback = null;
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