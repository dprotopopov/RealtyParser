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
            this.listBoxDll = new System.Windows.Forms.ListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listBoxKeys = new System.Windows.Forms.ListBox();
            this.propertyGridControlBind = new DevExpress.XtraVerticalGrid.PropertyGridControl();
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
            this.splitContainer1.Panel1.Controls.Add(this.listBoxDll);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(704, 467);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.TabIndex = 0;
            // 
            // listBoxDll
            // 
            this.listBoxDll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxDll.FormattingEnabled = true;
            this.listBoxDll.ItemHeight = 16;
            this.listBoxDll.Location = new System.Drawing.Point(0, 0);
            this.listBoxDll.Name = "listBoxDll";
            this.listBoxDll.Size = new System.Drawing.Size(302, 467);
            this.listBoxDll.TabIndex = 1;
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
            this.splitContainer2.Panel1.Controls.Add(this.listBoxKeys);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyGridControlBind);
            this.splitContainer2.Size = new System.Drawing.Size(398, 467);
            this.splitContainer2.SplitterDistance = 132;
            this.splitContainer2.TabIndex = 0;
            // 
            // listBoxKeys
            // 
            this.listBoxKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxKeys.FormattingEnabled = true;
            this.listBoxKeys.ItemHeight = 16;
            this.listBoxKeys.Location = new System.Drawing.Point(0, 0);
            this.listBoxKeys.Name = "listBoxKeys";
            this.listBoxKeys.Size = new System.Drawing.Size(398, 132);
            this.listBoxKeys.TabIndex = 1;
            this.listBoxKeys.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // propertyGridControlBind
            // 
            this.propertyGridControlBind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControlBind.Location = new System.Drawing.Point(0, 0);
            this.propertyGridControlBind.Name = "propertyGridControlBind";
            this.propertyGridControlBind.Size = new System.Drawing.Size(398, 331);
            this.propertyGridControlBind.TabIndex = 2;
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
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlBind)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox listBoxKeys;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControlBind;
        private System.Windows.Forms.ListBox listBoxDll;

    }
}