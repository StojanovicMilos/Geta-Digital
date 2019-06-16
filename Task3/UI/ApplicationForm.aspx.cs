using System;
using System.Collections.Generic;
using System.Web;
using Legacy.Web.Templates.Pages;
using Task3.BL;
using Task3.DAL;
using ListItem = Task3.DAL.ListItem;

namespace Task3.UI
{
    public partial class ApplicationForm : TemplatePageBase<ApplicationFormPage>
    {
        private readonly IDAO _dao;
        private readonly ILanguage _language;
        private readonly IEmail _email;
        private readonly IPropertyService _propertyService;

        public ApplicationForm(IDAO dao, Language language, IEmail email, IPropertyService propertyService) : base()
        {
            _dao = dao ?? throw new ArgumentNullException(nameof(dao));
            _language = language ?? throw new ArgumentNullException(nameof(language));
            _email = email ?? throw new ArgumentNullException(nameof(email));
            _propertyService = propertyService ?? throw new ArgumentNullException(nameof(propertyService));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (IsPostBack)
                return;

            DataBind();
            PopulateCountyList();
        }

        protected void PopulateCountyList()
        {
            Ddl_County.DataSource = _dao.GetCountryList();
            Ddl_County.DataBind();
        }

        protected void PopulateMunicipalityList(string county)
        {
            Ddl_Municipality.Items.Clear();
            Ddl_Municipality.Items.Add("");
            Ddl_Municipality.Items.AddRange(ConvertToListItemCollection(_dao.PopulateMunicipalityList(county)));
        }

        private System.Web.UI.WebControls.ListItem[] ConvertToListItemCollection(List<ListItem> populateMunicipalityList)
        {
            throw new NotImplementedException();
        }

        private void BuildDynamicControls()
        {
            if (pnlFileUpload.Visible)
            {
                //Create dummy datasource to display the correct number of FileUpload controls.
                if (CurrentPage.Property["NumberOfFileUploads"] != null)
                {
                    int numberOfFiles = (int)CurrentPage.Property["NumberOfFileUploads"].Value;

                    if (numberOfFiles > 0)
                    {
                        List<int> list = new List<int>();
                        for (int i = 0; i < numberOfFiles; i++)
                        {
                            list.Add(i);
                        }

                        rptFileUpload.DataSource = list;
                        rptFileUpload.DataBind();
                    }
                }
            }
        }

        protected void btnShowFileUpload_Click(object sender, EventArgs e)
        {
            pnlFileUpload.Visible = true;
            BuildDynamicControls();
            btnShowFileUpload.Visible = false;
        }

        protected void Btn_SubmitForm_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            if (SendFormContentByEmail())
            {
                string receiptUrl = _propertyService.GetPageDataPropertyLinkUrl(CurrentPage, "FormReceiptPage");
                Response.Redirect(receiptUrl);
            }
            else
            {
                string errorUrl = _propertyService.GetPageDataPropertyLinkUrl(CurrentPage, "FormErrorPage");
                Response.Redirect(errorUrl);
            }
        }

        private bool SendFormContentByEmail()
        {
            string subject = _propertyService.GetStringProperty(CurrentPage, "EmailSubject");

            string applicationReceiver = _email.GetEmailForMunicipality(Ddl_Municipality.SelectedValue);
            string applicationSender = Txt_Email.Text;
            List<AttachmentFile> attachments = ConvertToAttachmentFiles(Request.Files);

            ContactPerson details = GetDetailsFromUI();
            string emailHeader = _propertyService.GetStringProperty(CurrentPage, "EmailHeader");

            return _email.SendMail(subject, details, attachments, applicationReceiver, applicationSender, emailHeader);
        }

        private List<AttachmentFile> ConvertToAttachmentFiles(HttpFileCollection requestFiles)
        {
            throw new NotImplementedException();
        }

        private ContactPerson GetDetailsFromUI()
        {
            //TODO fetch data from UI
            throw new NotImplementedException();
        }

        protected void Ddl_County_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Ddl_County.SelectedValue.Equals(string.Empty))
            {
                Ddl_Municipality.Items.Clear();
                Ddl_Municipality.DataBind();
            }
            else
            {
                PopulateMunicipalityList(Ddl_County.SelectedValue);
            }
        }

        protected string GetLanguageString(string xmlPath) => _language.GetLanguageString(xmlPath);

        protected string GetCurrentLanguage() => _language.GetCurrentLanguage();
    }
}