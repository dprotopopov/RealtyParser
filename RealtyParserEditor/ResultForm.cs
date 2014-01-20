using System;
using System.Windows.Forms;
using RealtyParser;
using RT.ParsingLibs.Models;
using RT.ParsingLibs.Requests;
using RT.ParsingLibs.Responses;

namespace RealtyParserEditor
{
    public partial class ResultForm : Form, IChildFormInterface
    {
        public ResultForm()
        {
            InitializeComponent();
            propertyGridControlParseRequest.SelectedObject = new ParseRequest();
        }

        public async void Save()
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
            RealtyParserParsingModule parsingModule = new RealtyParserParsingModule();
            propertyGridControlParseResponse.SelectedObject = await parsingModule.Result(propertyGridControlParseRequest.SelectedObject as ParseRequest);
            try
            {
                foreach (var item in ((ParseResponse)propertyGridControlParseResponse.SelectedObject).Publications)
                {
                    listBoxPublications.Items.Add(item);
                }
            }
            catch (Exception)
            {
            }
        }

        public void Reload()
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

        private void listBoxPublications_SelectedValueChanged(object sender, EventArgs e)
        {
            listBoxPhotos.Items.Clear();
            listBoxPhone.Items.Clear();
            listBoxEmail.Items.Clear();
            propertyGridControlWebPublication.SelectedObject = listBoxPublications.SelectedItem as WebPublication;
            propertyGridControlAdditionalInfo.SelectedObject = ((WebPublication)listBoxPublications.SelectedItem).AdditionalInfo;
            propertyGridControlContact.SelectedObject = ((WebPublication)listBoxPublications.SelectedItem).Contact;
            propertyGridControlRealtyAdditionalInfo.SelectedObject = ((WebPublication)listBoxPublications.SelectedItem).AdditionalInfo.RealtyAdditionalInfo;
            foreach (var item in ((WebPublication)listBoxPublications.SelectedItem).Photos)
            {
                listBoxPhotos.Items.Add(item);
            }
            foreach (var item in ((WebPublication)listBoxPublications.SelectedItem).Contact.Phone)
            {
                listBoxPhone.Items.Add(item);
            }
            foreach (var item in ((WebPublication)listBoxPublications.SelectedItem).Contact.Email)
            {
                listBoxEmail.Items.Add(item);
            }
        }
    }
}
