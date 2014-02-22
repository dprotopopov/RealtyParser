namespace RealtyParserEditor.Children
{
    partial class MappingEditor
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
            this.repositoryItemComboBoxLevel = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.Site = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.TableName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.MinLevel = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.MaxLevel = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.SiteMinLevel = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.SiteMaxLevel = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gridControlMapping = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Left = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBoxLeft = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.Right = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBoxRight = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.textBoxCommandText = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.MaxDistance = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlWorkspace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxSite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxTableName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMapping)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxRight)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(890, 674);
            this.splitContainer1.SplitterDistance = 126;
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
            this.repositoryItemComboBoxLevel});
            this.propertyGridControlWorkspace.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.Site,
            this.TableName,
            this.MinLevel,
            this.MaxLevel,
            this.SiteMinLevel,
            this.SiteMaxLevel,
            this.MaxDistance});
            this.propertyGridControlWorkspace.Size = new System.Drawing.Size(890, 126);
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
            // MinLevel
            // 
            this.MinLevel.Name = "MinLevel";
            this.MinLevel.Properties.Caption = "MinLevel";
            this.MinLevel.Properties.FieldName = "MinLevel";
            this.MinLevel.Properties.RowEdit = this.repositoryItemComboBoxLevel;
            // 
            // MaxLevel
            // 
            this.MaxLevel.Name = "MaxLevel";
            this.MaxLevel.Properties.Caption = "MaxLevel";
            this.MaxLevel.Properties.FieldName = "MaxLevel";
            this.MaxLevel.Properties.RowEdit = this.repositoryItemComboBoxLevel;
            // 
            // SiteMinLevel
            // 
            this.SiteMinLevel.Name = "SiteMinLevel";
            this.SiteMinLevel.Properties.Caption = "SiteMinLevel";
            this.SiteMinLevel.Properties.FieldName = "SiteMinLevel";
            this.SiteMinLevel.Properties.RowEdit = this.repositoryItemComboBoxLevel;
            // 
            // SiteMaxLevel
            // 
            this.SiteMaxLevel.Name = "SiteMaxLevel";
            this.SiteMaxLevel.Properties.Caption = "SiteMaxLevel";
            this.SiteMaxLevel.Properties.FieldName = "SiteMaxLevel";
            this.SiteMaxLevel.Properties.RowEdit = this.repositoryItemComboBoxLevel;
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
            this.splitContainer2.Panel1.Controls.Add(this.gridControlMapping);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textBoxCommandText);
            this.splitContainer2.Size = new System.Drawing.Size(890, 544);
            this.splitContainer2.SplitterDistance = 366;
            this.splitContainer2.TabIndex = 0;
            // 
            // gridControlMapping
            // 
            this.gridControlMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlMapping.Location = new System.Drawing.Point(0, 0);
            this.gridControlMapping.MainView = this.gridView1;
            this.gridControlMapping.Name = "gridControlMapping";
            this.gridControlMapping.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBoxRight,
            this.repositoryItemComboBoxLeft});
            this.gridControlMapping.Size = new System.Drawing.Size(890, 366);
            this.gridControlMapping.TabIndex = 1;
            this.gridControlMapping.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Left,
            this.Right});
            this.gridView1.GridControl = this.gridControlMapping;
            this.gridView1.Name = "gridView1";
            // 
            // Left
            // 
            this.Left.Caption = "Key";
            this.Left.ColumnEdit = this.repositoryItemComboBoxLeft;
            this.Left.FieldName = "Key";
            this.Left.Name = "Left";
            this.Left.Visible = true;
            this.Left.VisibleIndex = 0;
            // 
            // repositoryItemComboBoxLeft
            // 
            this.repositoryItemComboBoxLeft.AutoHeight = false;
            this.repositoryItemComboBoxLeft.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxLeft.Name = "repositoryItemComboBoxLeft";
            // 
            // Right
            // 
            this.Right.Caption = "Value";
            this.Right.ColumnEdit = this.repositoryItemComboBoxRight;
            this.Right.FieldName = "Value";
            this.Right.Name = "Right";
            this.Right.Visible = true;
            this.Right.VisibleIndex = 1;
            // 
            // repositoryItemComboBoxRight
            // 
            this.repositoryItemComboBoxRight.AutoHeight = false;
            this.repositoryItemComboBoxRight.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxRight.Name = "repositoryItemComboBoxRight";
            // 
            // textBoxCommandText
            // 
            this.textBoxCommandText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCommandText.Location = new System.Drawing.Point(0, 0);
            this.textBoxCommandText.Multiline = true;
            this.textBoxCommandText.Name = "textBoxCommandText";
            this.textBoxCommandText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCommandText.Size = new System.Drawing.Size(890, 174);
            this.textBoxCommandText.TabIndex = 0;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "sql";
            // 
            // MaxDistance
            // 
            this.MaxDistance.Name = "MaxDistance";
            this.MaxDistance.Properties.Caption = "MaxDistance";
            this.MaxDistance.Properties.FieldName = "MaxDistance";
            // 
            // MappingEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 674);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MappingEditor";
            this.Text = "MappingEditor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlWorkspace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxSite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxTableName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxLevel)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMapping)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxRight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControlWorkspace;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxSite;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxTableName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow Site;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow TableName;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraGrid.GridControl gridControlMapping;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn Left;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxLeft;
        private DevExpress.XtraGrid.Columns.GridColumn Right;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxRight;
        private System.Windows.Forms.TextBox textBoxCommandText;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxLevel;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow MinLevel;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow MaxLevel;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow SiteMinLevel;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow SiteMaxLevel;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow MaxDistance;
    }
}