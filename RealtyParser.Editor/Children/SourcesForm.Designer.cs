namespace RealtyParser.Editor.Children
{
    partial class SourcesForm
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
            this.listBoxSources = new System.Windows.Forms.ListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.propertyGridControlBind = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.listBoxDll = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlBind)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listBoxSources);
            this.splitContainer1.Size = new System.Drawing.Size(803, 555);
            this.splitContainer1.SplitterDistance = 322;
            this.splitContainer1.TabIndex = 0;
            // 
            // listBoxSources
            // 
            this.listBoxSources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSources.FormattingEnabled = true;
            this.listBoxSources.ItemHeight = 16;
            this.listBoxSources.Location = new System.Drawing.Point(0, 0);
            this.listBoxSources.Name = "listBoxSources";
            this.listBoxSources.Size = new System.Drawing.Size(477, 555);
            this.listBoxSources.TabIndex = 2;
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
            this.splitContainer2.Panel1.Controls.Add(this.listBoxDll);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyGridControlBind);
            this.splitContainer2.Size = new System.Drawing.Size(322, 555);
            this.splitContainer2.SplitterDistance = 198;
            this.splitContainer2.TabIndex = 0;
            // 
            // propertyGridControlBind
            // 
            this.propertyGridControlBind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControlBind.Location = new System.Drawing.Point(0, 0);
            this.propertyGridControlBind.Name = "propertyGridControlBind";
            this.propertyGridControlBind.Size = new System.Drawing.Size(322, 353);
            this.propertyGridControlBind.TabIndex = 1;
            // 
            // listBoxDll
            // 
            this.listBoxDll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxDll.FormattingEnabled = true;
            this.listBoxDll.ItemHeight = 16;
            this.listBoxDll.Location = new System.Drawing.Point(0, 0);
            this.listBoxDll.Name = "listBoxDll";
            this.listBoxDll.Size = new System.Drawing.Size(322, 198);
            this.listBoxDll.TabIndex = 1;
            // 
            // SourcesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 555);
            this.Controls.Add(this.splitContainer1);
            this.Name = "SourcesForm";
            this.Text = "SourcesForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlBind)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBoxSources;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControlBind;
        private System.Windows.Forms.ListBox listBoxDll;
    }
}