using System;
using System.Windows.Forms;
using RealtyParser;
using RT.ParsingLibs.Models;
using RT.ParsingLibs.Requests;
using RT.ParsingLibs.Responses;

namespace RealtyParserEditor
{
    public partial class ResultForm : Form, IChildForm
    {
        public ResultForm()
        {
            InitializeComponent();
            propertyGridControlParseRequest.SelectedObject = new ParseRequest();
        }

        public void Save()
        {
        }

        public void ClearResults()
        {
            propertyGridControlParseRequest.SelectedObject = new ParseRequest();
            listBoxPublications.Items.Clear();
            listBoxPhotos.Items.Clear();
            listBoxPhone.Items.Clear();
            listBoxEmail.Items.Clear();
            propertyGridControlParseResponse.SelectedObject = null;
            propertyGridControlWebPublication.SelectedObject = null;
            propertyGridControlAdditionalInfo.SelectedObject = null;
            propertyGridControlContact.SelectedObject = null;
            propertyGridControlRealtyAdditionalInfo.SelectedObject = null;
        }

        public async void Execute()
        {
            listBoxPublications.Items.Clear();
            listBoxPhotos.Items.Clear();
            listBoxPhone.Items.Clear();
            listBoxEmail.Items.Clear();
            propertyGridControlParseResponse.SelectedObject = null;
            propertyGridControlWebPublication.SelectedObject = null;
            propertyGridControlAdditionalInfo.SelectedObject = null;
            propertyGridControlContact.SelectedObject = null;
            propertyGridControlRealtyAdditionalInfo.SelectedObject = null;
            var parsingModule = new ParsingModule();
            propertyGridControlParseResponse.SelectedObject =
                await parsingModule.Result(propertyGridControlParseRequest.SelectedObject as ParseRequest);
            try
            {
                foreach (
                    WebPublication item in
                        ((ParseResponse) propertyGridControlParseResponse.SelectedObject).Publications)
                {
                    listBoxPublications.Items.Add(item);
                }
            }
            catch (Exception)
            {
            }
        }

        private void listBoxPublications_SelectedValueChanged(object sender, EventArgs e)
        {
            listBoxPhotos.Items.Clear();
            listBoxPhone.Items.Clear();
            listBoxEmail.Items.Clear();
            propertyGridControlWebPublication.SelectedObject = listBoxPublications.SelectedItem as WebPublication;
            propertyGridControlAdditionalInfo.SelectedObject =
                ((WebPublication) listBoxPublications.SelectedItem).AdditionalInfo;
            propertyGridControlContact.SelectedObject = ((WebPublication) listBoxPublications.SelectedItem).Contact;
            propertyGridControlRealtyAdditionalInfo.SelectedObject =
                ((WebPublication) listBoxPublications.SelectedItem).AdditionalInfo.RealtyAdditionalInfo;
            foreach (Uri item in ((WebPublication) listBoxPublications.SelectedItem).Photos)
            {
                listBoxPhotos.Items.Add(item);
            }
            foreach (string item in ((WebPublication) listBoxPublications.SelectedItem).Contact.Phone)
            {
                listBoxPhone.Items.Add(item);
            }
            foreach (string item in ((WebPublication) listBoxPublications.SelectedItem).Contact.Email)
            {
                listBoxEmail.Items.Add(item);
            }
        }
    }
}