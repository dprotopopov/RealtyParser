﻿using System;
using System.Windows.Forms;
using RealtyParser;

namespace RealtyParserEditor
{
    public partial class AboutForm : Form, IChildFormInterface
    {
        public AboutForm()
        {
            InitializeComponent();
            RealtyParserParsingModule parsingModule = new RealtyParserParsingModule();
            propertyGridControl1.SelectedObject = parsingModule.About();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            RealtyParserParsingModule parsingModule = new RealtyParserParsingModule();
            propertyGridControl1.SelectedObject = parsingModule.About();
        }
    }
}