﻿namespace RealtyParserEditor
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
            this.propertyGridControlBind = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.listBoxKeys = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.listBoxKeys);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGridControlBind);
            this.splitContainer1.Size = new System.Drawing.Size(442, 379);
            this.splitContainer1.SplitterDistance = 190;
            this.splitContainer1.TabIndex = 0;
            // 
            // propertyGridControlBind
            // 
            this.propertyGridControlBind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControlBind.Location = new System.Drawing.Point(0, 0);
            this.propertyGridControlBind.Name = "propertyGridControlBind";
            this.propertyGridControlBind.Size = new System.Drawing.Size(248, 379);
            this.propertyGridControlBind.TabIndex = 1;
            // 
            // listBoxKeys
            // 
            this.listBoxKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxKeys.FormattingEnabled = true;
            this.listBoxKeys.ItemHeight = 16;
            this.listBoxKeys.Location = new System.Drawing.Point(0, 0);
            this.listBoxKeys.Name = "listBoxKeys";
            this.listBoxKeys.Size = new System.Drawing.Size(190, 379);
            this.listBoxKeys.TabIndex = 0;
            this.listBoxKeys.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // KeysForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 379);
            this.Controls.Add(this.splitContainer1);
            this.Name = "KeysForm";
            this.Text = "KeysForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlBind)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBoxKeys;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControlBind;

    }
}