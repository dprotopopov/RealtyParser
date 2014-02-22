namespace RealtyParserEditor.Children
{
    partial class BuilderForm
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
            this.repositoryItemComboBoxSite = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxTableName = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxMethodInfo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxLevel = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.Site = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.TableName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.MethodInfo = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.MaxLevel = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.MinLevel = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.textBoxCommandText = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SiteMaxLevel = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.SiteMinLevel = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlWorkspace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxSite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxTableName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxMethodInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxLevel)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.textBoxCommandText);
            this.splitContainer1.Size = new System.Drawing.Size(905, 519);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 0;
            // 
            // propertyGridControlWorkspace
            // 
            this.propertyGridControlWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControlWorkspace.Location = new System.Drawing.Point(0, 0);
            this.propertyGridControlWorkspace.Name = "propertyGridControlWorkspace";
            this.propertyGridControlWorkspace.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBoxSite,
            this.repositoryItemComboBoxTableName,
            this.repositoryItemComboBoxMethodInfo,
            this.repositoryItemComboBoxLevel});
            this.propertyGridControlWorkspace.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.Site,
            this.TableName,
            this.MethodInfo,
            this.MaxLevel,
            this.MinLevel,
            this.SiteMaxLevel,
            this.SiteMinLevel});
            this.propertyGridControlWorkspace.Size = new System.Drawing.Size(905, 154);
            this.propertyGridControlWorkspace.TabIndex = 0;
            // 
            // repositoryItemComboBoxSite
            // 
            this.repositoryItemComboBoxSite.AutoHeight = false;
            this.repositoryItemComboBoxSite.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxSite.Name = "repositoryItemComboBoxSite";
            // 
            // repositoryItemComboBoxTableName
            // 
            this.repositoryItemComboBoxTableName.AutoHeight = false;
            this.repositoryItemComboBoxTableName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxTableName.Name = "repositoryItemComboBoxTableName";
            // 
            // repositoryItemComboBoxMethodInfo
            // 
            this.repositoryItemComboBoxMethodInfo.AutoHeight = false;
            this.repositoryItemComboBoxMethodInfo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxMethodInfo.Name = "repositoryItemComboBoxMethodInfo";
            // 
            // repositoryItemComboBoxLevel
            // 
            this.repositoryItemComboBoxLevel.AutoHeight = false;
            this.repositoryItemComboBoxLevel.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxLevel.Name = "repositoryItemComboBoxLevel";
            // 
            // Site
            // 
            this.Site.Name = "Site";
            this.Site.Properties.Caption = "Site";
            this.Site.Properties.FieldName = "Site";
            this.Site.Properties.RowEdit = this.repositoryItemComboBoxSite;
            // 
            // TableName
            // 
            this.TableName.Name = "TableName";
            this.TableName.Properties.Caption = "TableName";
            this.TableName.Properties.FieldName = "TableName";
            this.TableName.Properties.RowEdit = this.repositoryItemComboBoxTableName;
            // 
            // MethodInfo
            // 
            this.MethodInfo.Name = "MethodInfo";
            this.MethodInfo.Properties.Caption = "MethodInfo";
            this.MethodInfo.Properties.FieldName = "MethodInfo";
            this.MethodInfo.Properties.RowEdit = this.repositoryItemComboBoxMethodInfo;
            // 
            // MaxLevel
            // 
            this.MaxLevel.Name = "MaxLevel";
            this.MaxLevel.Properties.Caption = "MaxLevel";
            this.MaxLevel.Properties.FieldName = "MaxLevel";
            this.MaxLevel.Properties.RowEdit = this.repositoryItemComboBoxLevel;
            // 
            // MinLevel
            // 
            this.MinLevel.Name = "MinLevel";
            this.MinLevel.Properties.Caption = "MinLevel";
            this.MinLevel.Properties.FieldName = "MinLevel";
            this.MinLevel.Properties.RowEdit = this.repositoryItemComboBoxLevel;
            // 
            // textBoxCommandText
            // 
            this.textBoxCommandText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCommandText.Location = new System.Drawing.Point(0, 0);
            this.textBoxCommandText.Multiline = true;
            this.textBoxCommandText.Name = "textBoxCommandText";
            this.textBoxCommandText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCommandText.Size = new System.Drawing.Size(905, 361);
            this.textBoxCommandText.TabIndex = 0;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "sql";
            // 
            // SiteMaxLevel
            // 
            this.SiteMaxLevel.Name = "SiteMaxLevel";
            this.SiteMaxLevel.Properties.Caption = "SiteMaxLevel";
            this.SiteMaxLevel.Properties.FieldName = "SiteMaxLevel";
            this.SiteMaxLevel.Properties.RowEdit = this.repositoryItemComboBoxLevel;
            // 
            // SiteMinLevel
            // 
            this.SiteMinLevel.Name = "SiteMinLevel";
            this.SiteMinLevel.Properties.Caption = "SiteMinLevel";
            this.SiteMinLevel.Properties.FieldName = "SiteMinLevel";
            this.SiteMinLevel.Properties.RowEdit = this.repositoryItemComboBoxLevel;
            // 
            // BuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 519);
            this.Controls.Add(this.splitContainer1);
            this.Name = "BuilderForm";
            this.Text = "BuilderForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlWorkspace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxSite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxTableName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxMethodInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBoxCommandText;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControlWorkspace;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxSite;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxTableName;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxMethodInfo;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow Site;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow TableName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow MethodInfo;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxLevel;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow MaxLevel;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow MinLevel;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow SiteMaxLevel;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow SiteMinLevel;
    }
}