namespace RealtyParser.Editor.Children
{
    partial class KeysForm
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
            this.listBoxParserModule = new DevExpress.XtraEditors.ListBoxControl();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.listBoxControlKeysActions = new DevExpress.XtraEditors.ListBoxControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.propertyGridControlBind = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.listBoxControlKeysRubrics = new DevExpress.XtraEditors.ListBoxControl();
            this.listBoxControlKeysRegions = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlBind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlKeysRubrics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlKeysRegions)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxParserModule);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(704, 467);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.TabIndex = 0;
            // 
            // listBoxParserModule
            // 
            this.listBoxParserModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxParserModule.ItemHeight = 16;
            this.listBoxParserModule.Location = new System.Drawing.Point(0, 0);
            this.listBoxParserModule.Name = "listBoxParserModule";
            this.listBoxParserModule.Size = new System.Drawing.Size(302, 467);
            this.listBoxParserModule.TabIndex = 1;
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
            this.splitContainer2.Panel1.Controls.Add(this.xtraTabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyGridControlBind);
            this.splitContainer2.Size = new System.Drawing.Size(398, 467);
            this.splitContainer2.SplitterDistance = 330;
            this.splitContainer2.TabIndex = 0;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(398, 330);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.listBoxControlKeysActions);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(392, 299);
            this.xtraTabPage1.Text = "Actions";
            // 
            // listBoxControlKeysActions
            // 
            this.listBoxControlKeysActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxControlKeysActions.ItemHeight = 16;
            this.listBoxControlKeysActions.Location = new System.Drawing.Point(0, 0);
            this.listBoxControlKeysActions.Name = "listBoxControlKeysActions";
            this.listBoxControlKeysActions.Size = new System.Drawing.Size(392, 299);
            this.listBoxControlKeysActions.TabIndex = 2;
            this.listBoxControlKeysActions.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.listBoxControlKeysRubrics);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(392, 299);
            this.xtraTabPage2.Text = "Rubrics";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.listBoxControlKeysRegions);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(392, 299);
            this.xtraTabPage3.Text = "Regions";
            // 
            // propertyGridControlBind
            // 
            this.propertyGridControlBind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControlBind.Location = new System.Drawing.Point(0, 0);
            this.propertyGridControlBind.Name = "propertyGridControlBind";
            this.propertyGridControlBind.Size = new System.Drawing.Size(398, 133);
            this.propertyGridControlBind.TabIndex = 2;
            // 
            // listBoxControlKeysRubrics
            // 
            this.listBoxControlKeysRubrics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxControlKeysRubrics.Location = new System.Drawing.Point(0, 0);
            this.listBoxControlKeysRubrics.Name = "listBoxControlKeysRubrics";
            this.listBoxControlKeysRubrics.Size = new System.Drawing.Size(392, 299);
            this.listBoxControlKeysRubrics.TabIndex = 0;
            this.listBoxControlKeysRubrics.SelectedIndexChanged += new System.EventHandler(this.listBoxControlKeysRubrics_SelectedIndexChanged);
            // 
            // listBoxControlKeysRegions
            // 
            this.listBoxControlKeysRegions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxControlKeysRegions.Location = new System.Drawing.Point(0, 0);
            this.listBoxControlKeysRegions.Name = "listBoxControlKeysRegions";
            this.listBoxControlKeysRegions.Size = new System.Drawing.Size(392, 299);
            this.listBoxControlKeysRegions.TabIndex = 0;
            this.listBoxControlKeysRegions.SelectedIndexChanged += new System.EventHandler(this.listBoxControlKeysRegions_SelectedIndexChanged);
            // 
            // KeysForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 467);
            this.Controls.Add(this.splitContainer1);
            this.Name = "KeysForm";
            this.Text = "KeysForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlBind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlKeysRubrics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControlKeysRegions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControlBind;
        private DevExpress.XtraEditors.ListBoxControl listBoxParserModule;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.ListBoxControl listBoxControlKeysActions;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraEditors.ListBoxControl listBoxControlKeysRubrics;
        private DevExpress.XtraEditors.ListBoxControl listBoxControlKeysRegions;

    }
}