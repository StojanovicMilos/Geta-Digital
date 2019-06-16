using System;
using System.Collections.Generic;
using System.Web;
using Legacy.Web.Templates.Pages;
using Task3.DAL;
using ListItem = Task3.DAL.ListItem;

namespace Task3.UI
{
    public partial class ApplicationForm : TemplatePageBase<ApplicationFormPage>
    {
        private readonly IDAO _dao;
        private readonly ILanguage _language;
        private readonly IEmail _email;

        public ApplicationForm(IDAO dao, Language language, IEmail email) : base()
        {
            _dao = dao ?? throw new ArgumentNullException(nameof(dao));
            _language = language ?? throw new ArgumentNullException(nameof(language));
            _email = email ?? throw new ArgumentNullException(nameof(email));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (IsPostBack)
                return;

            DataBind();
            PopulateCountyList();
        }

        private bool SendFormContentByEmail()
        {
            string subject = PropertyService.GetStringProperty(CurrentPage, "EmailSubject");

            string applicationReceiver = _email.GetEmailForMunicipality(Ddl_Municipality.SelectedValue);
            string applicationSender = Txt_Email.Text;
            List<AttachmentFile> attachments = ConvertToAttachmentFiles(Request.Files);

            ContactPerson details = GetDetailsFromUI();
            string emailHeader = PropertyService.GetStringProperty(CurrentPage, "EmailHeader");

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

        #region Fill GUI controls
        /// <summary>
        /// Populates the County dropDownList
        /// </summary>
        protected void PopulateCountyList()
        {
            Ddl_County.DataSource = _dao.GetCountryList();
            Ddl_County.DataBind();
        }

        /// <summary>
        /// Populate Ddl_Municipality with municipality from the given county
        /// </summary>
        /// <param name="county"></param>
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

        /// <summary>
        /// Creates as many FileUpload controls as configured on the page.
        /// </summary>
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
        #endregion

        #region Events

        /// <summary>
        /// Attachement button clicked
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        protected void btnShowFileUpload_Click(object sender, EventArgs e)
        {
            pnlFileUpload.Visible = true;
            BuildDynamicControls();
            btnShowFileUpload.Visible = false;
        }

        /// <summary>
        /// Submit button clicked
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        protected void Btn_SubmitForm_Click(object sender, EventArgs e)
        {
            // Server side validation, if javascript is disabled
            Page.Validate();
            if (Page.IsValid)
            {
                if (SendFormContentByEmail())
                {
                    string receiptUrl = PropertyService.GetPageDataPropertyLinkUrl(CurrentPage, "FormReceiptPage");
                    Response.Redirect(receiptUrl);
                }
                else
                {
                    string errorUrl = PropertyService.GetPageDataPropertyLinkUrl(CurrentPage, "FormErrorPage");
                    Response.Redirect(errorUrl);
                }
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the Ddl_County control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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

        #endregion

        /// <summary>
        /// Returns the current language string for a specified xml language file entry.
        /// </summary>
        /// <param name="xmlPath">The path to the string in the xml file.</param>
        /// <returns></returns>
        protected string GetLanguageString(string xmlPath) => _language.GetLanguageString(xmlPath);

        /// <summary>
        /// Returns the current language as a two letter code (no or en for instance).
        /// </summary>
        /// <returns></returns>
        protected string GetCurrentLanguage() => _language.GetCurrentLanguage();
    }
}