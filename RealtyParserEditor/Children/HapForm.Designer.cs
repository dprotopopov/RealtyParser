namespace RealtyParserEditor.Children
{
    partial class HapForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyGridControlWorkspace = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.repositoryItemComboBoxMethod = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxEncoding = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxUrl = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxCompression = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.Url = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.Xpath = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.Method = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.Encoding = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.Compression = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listBoxDocument = new System.Windows.Forms.ListBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.listBoxNode = new System.Windows.Forms.ListBox();
            this.propertyGridControlNode = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlWorkspace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxMethod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxEncoding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxUrl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxCompression)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlNode)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.propertyGridControlWorkspace);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(790, 519);
            this.splitContainer1.SplitterDistance = 125;
            this.splitContainer1.TabIndex = 0;
            // 
            // propertyGridControlWorkspace
            // 
            this.propertyGridControlWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControlWorkspace.Location = new System.Drawing.Point(0, 0);
            this.propertyGridControlWorkspace.Name = "propertyGridControlWorkspace";
            this.propertyGridControlWorkspace.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBoxMethod,
            this.repositoryItemComboBoxEncoding,
            this.repositoryItemComboBoxUrl,
            this.repositoryItemComboBoxCompression});
            this.propertyGridControlWorkspace.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.Url,
            this.Xpath,
            this.Method,
            this.Encoding,
            this.Compression});
            this.propertyGridControlWorkspace.Size = new System.Drawing.Size(790, 125);
            this.propertyGridControlWorkspace.TabIndex = 0;
            // 
            // repositoryItemComboBoxMethod
            // 
            this.repositoryItemComboBoxMethod.AutoHeight = false;
            this.repositoryItemComboBoxMethod.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxMethod.Name = "repositoryItemComboBoxMethod";
            // 
            // repositoryItemComboBoxEncoding
            // 
            this.repositoryItemComboBoxEncoding.AutoHeight = false;
            this.repositoryItemComboBoxEncoding.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxEncoding.Name = "repositoryItemComboBoxEncoding";
            // 
            // repositoryItemComboBoxUrl
            // 
            this.repositoryItemComboBoxUrl.AutoHeight = false;
            this.repositoryItemComboBoxUrl.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxUrl.Name = "repositoryItemComboBoxUrl";
            // 
            // repositoryItemComboBoxCompression
            // 
            this.repositoryItemComboBoxCompression.AutoHeight = false;
            this.repositoryItemComboBoxCompression.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxCompression.Name = "repositoryItemComboBoxCompression";
            // 
            // Url
            // 
            this.Url.Name = "Url";
            this.Url.Properties.Caption = "Url";
            this.Url.Properties.FieldName = "Url";
            this.Url.Properties.RowEdit = this.repositoryItemComboBoxUrl;
            // 
            // Xpath
            // 
            this.Xpath.Name = "Xpath";
            this.Xpath.Properties.Caption = "Xpath";
            this.Xpath.Properties.FieldName = "Xpath";
            // 
            // Method
            // 
            this.Method.Name = "Method";
            this.Method.Properties.Caption = "Method";
            this.Method.Properties.FieldName = "Method";
            this.Method.Properties.RowEdit = this.repositoryItemComboBoxMethod;
            // 
            // Encoding
            // 
            this.Encoding.Name = "Encoding";
            this.Encoding.Properties.Caption = "Encoding";
            this.Encoding.Properties.FieldName = "Encoding";
            this.Encoding.Properties.RowEdit = this.repositoryItemComboBoxEncoding;
            // 
            // Compression
            // 
            this.Compression.Name = "Compression";
            this.Compression.Properties.Caption = "Compression";
            this.Compression.Properties.FieldName = "Compression";
            this.Compression.Properties.RowEdit = this.repositoryItemComboBoxCompression;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listBoxDocument);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(790, 390);
            this.splitContainer2.SplitterDistance = 48;
            this.splitContainer2.TabIndex = 0;
            // 
            // listBoxDocument
            // 
            this.listBoxDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxDocument.FormattingEnabled = true;
            this.listBoxDocument.ItemHeight = 16;
            this.listBoxDocument.Location = new System.Drawing.Point(0, 0);
            this.listBoxDocument.Name = "listBoxDocument";
            this.listBoxDocument.Size = new System.Drawing.Size(790, 48);
            this.listBoxDocument.TabIndex = 0;
            this.listBoxDocument.SelectedValueChanged += new System.EventHandler(this.listBoxDocument_SelectedValueChanged);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(790, 338);
            this.splitContainer3.SplitterDistance = 154;
            this.splitContainer3.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(790, 154);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(782, 125);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(776, 119);
            this.textBox1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.webBrowser1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(782, 125);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.CausesValidation = false;
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(776, 119);
            this.webBrowser1.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.listBoxNode);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.propertyGridControlNode);
            this.splitContainer4.Size = new System.Drawing.Size(790, 180);
            this.splitContainer4.SplitterDistance = 375;
            this.splitContainer4.TabIndex = 0;
            // 
            // listBoxNode
            // 
            this.listBoxNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxNode.FormattingEnabled = true;
            this.listBoxNode.ItemHeight = 16;
            this.listBoxNode.Location = new System.Drawing.Point(0, 0);
            this.listBoxNode.Name = "listBoxNode";
            this.listBoxNode.Size = new System.Drawing.Size(375, 180);
            this.listBoxNode.TabIndex = 0;
            this.listBoxNode.SelectedIndexChanged += new System.EventHandler(this.listBoxNode_SelectedIndexChanged);
            // 
            // propertyGridControlNode
            // 
            this.propertyGridControlNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControlNode.Location = new System.Drawing.Point(0, 0);
            this.propertyGridControlNode.Name = "propertyGridControlNode";
            this.propertyGridControlNode.Size = new System.Drawing.Size(411, 180);
            this.propertyGridControlNode.TabIndex = 0;
            // 
            // HapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 519);
            this.Controls.Add(this.splitContainer1);
            this.Name = "HapForm";
            this.Text = "HapForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlWorkspace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxMethod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxEncoding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxUrl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxCompression)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlNode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControlWorkspace;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox listBoxDocument;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ListBox listBoxNode;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControlNode;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxMethod;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxEncoding;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow Url;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow Xpath;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow Method;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow Encoding;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxUrl;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxCompression;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow Compression;
    }
}