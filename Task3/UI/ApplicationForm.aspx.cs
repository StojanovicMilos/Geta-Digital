using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Task3.BL;

namespace Task3.UI
{
    public partial class ApplicationForm : TemplatePageBase<ApplicationFormPage>
    {
        private readonly IDAO _dao;
        private readonly ILanguage _language;
        private readonly IEmail _email;
        private readonly IPropertyService _propertyService;

        // ReSharper disable once RedundantBaseConstructorCall
        public ApplicationForm(IDAO dao, ILanguage language, IEmail email, IPropertyService propertyService) : base()
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
        
        protected void btnShowFileUpload_Click(object sender, EventArgs e)
        {
            pnlFileUpload.Visible = true;
            CreateDummyDataSource();
            btnShowFileUpload.Visible = false;
        }

        private void CreateDummyDataSource()
        {
            if (!pnlFileUpload.Visible)
                return;
            if (CurrentPage.Property["NumberOfFileUploads"] == null)
                return;

            int numberOfFiles = (int) CurrentPage.Property["NumberOfFileUploads"].Value;
            if (numberOfFiles <= 0)
                return;

            rptFileUpload.DataSource = Enumerable.Range(0, numberOfFiles).Select(number => number);
            rptFileUpload.DataBind();
        }

        protected void Btn_SubmitForm_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            EmailData emailData = GetEmailDataFromUI();
            bool emailSent = _email.SendMail(emailData);

            var redirectFormName = emailSent ? "FormReceiptPage" : "FormErrorPage";
            string redirectUrl = _propertyService.GetPageDataPropertyLinkUrl(CurrentPage, redirectFormName);
            Response.Redirect(redirectUrl);
        }

        private EmailData GetEmailDataFromUI() => new EmailData
        {
            Subject = _propertyService.GetStringProperty(CurrentPage, "EmailSubject"),
            ApplicationReceiver = _dao.GetEmailForMunicipality(Ddl_Municipality.SelectedValue),
            ApplicationSender = Txt_Email.Text,
            Attachments = ConvertToAttachmentFiles(Request.Files),
            County = Ddl_County.SelectedValue,
            Municipality = Ddl_Municipality.SelectedValue,
            EmailHeader = _propertyService.GetStringProperty(CurrentPage, "EmailHeader"),
            EmailFooter = _propertyService.GetStringProperty(CurrentPage, "EmailFooter"),
            Applicator = Txt_Applicator.Text,
            Address = Txt_Address.Text,
            PostCode = Txt_PostCode.Text,
            PostArea = Txt_PostArea.Text,
            BirthNumber = Txt_OrgNoBirthNumber.Text,
            ContactPerson = Txt_ContactPerson.Text,
            Phone = Txt_Phone.Text,
            Email = Txt_Email.Text,
            Description = Txt_Description.Text,
            FinancePlan = Txt_FinancePlan.Text,
            BusinessDescription = Txt_BusinessDescription.Text,
            ApplicationAmount = Txt_ApplicationAmount.Text,
            Bcc = _dao.GetEmailForMunicipality(Ddl_Municipality.SelectedValue)
        };

        private static List<AttachmentFile> ConvertToAttachmentFiles(HttpFileCollection requestFiles)
        {
            List<AttachmentFile> attachments = new List<AttachmentFile>();
            foreach (HttpPostedFile httpPostedFile in requestFiles)
            {
                attachments.Add(new AttachmentFile
                {
                    ContentLength = httpPostedFile.ContentLength,
                    ContentType = httpPostedFile.ContentType,
                    FileName = httpPostedFile.FileName,
                    InputStream = httpPostedFile.InputStream
                });
            }

            return attachments;
        }

        protected void Ddl_County_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ddl_Municipality.Items.Clear();
            if (Ddl_County.SelectedValue.Equals(string.Empty))
            {
                Ddl_Municipality.DataBind();
            }
            else
            {
                Ddl_Municipality.Items.Add(new ListItem {Text = string.Empty, Value = string.Empty});
                var municipalities = _dao.GetMunicipalities(Ddl_County.SelectedValue);
                Ddl_Municipality.Items.AddRange(ConvertToListItemCollection(municipalities));
            }
        }

        private ListItem[] ConvertToListItemCollection(IQueryable<DropdownListItem> municipalityList) => municipalityList.Select(m => new ListItem(m.Text, m.Value)).ToArray();

        protected string GetLanguageString(string xmlPath) => _language.GetLanguageString(xmlPath);

        protected string GetCurrentLanguage() => _language.GetCurrentLanguage();
    }
}