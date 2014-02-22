using System;
using System.Linq;
using System.Windows.Forms;
using RealtyParser;
using RT.ParsingLibs.Models;
using RT.ParsingLibs.Requests;
using RT.ParsingLibs.Responses;

namespace RealtyParserEditor.Children
{
    public partial class ResultForm : Form, IChildForm
    {
        private static readonly ParserModule ParserModule = new ParserModule();

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
            propertyGridControlParseResponse.SelectedObject =
                await ParserModule.Result(propertyGridControlParseRequest.SelectedObject as ParseRequest);
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
            listBoxPhotos.Items.AddRange(
                ((WebPublication)listBoxPublications.SelectedItem).Photos.Cast<object>().ToArray());
            listBoxPhone.Items.AddRange(
                ((WebPublication)listBoxPublications.SelectedItem).Contact.Phone.Cast<object>().ToArray());
            listBoxEmail.Items.AddRange(
                ((WebPublication)listBoxPublications.SelectedItem).Contact.Email.Cast<object>().ToArray());
        }
    }
}