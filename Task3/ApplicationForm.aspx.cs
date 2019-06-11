using System;
using System.Collections.Generic;
using Task3;

namespace Legacy.Web.Templates.Pages
{
    public partial class ApplicationForm : TemplatePageBase<ApplicationFormPage>
    {
        private readonly DAL _dal;
        private readonly Language _language;
        private readonly Email _email;

        protected List<ContactPerson> contactPersonList;
        protected string[] countyList = { "", "Nordland", "Nord Trøndelag", "Sør Trøndelag", "Møre og Romsdal", "Sogn og Fjordane", "Hordaland", "Rogaland", "Vest Agder" };
        

        public ApplicationForm(DAL dal, Language language, Email email) : base()
        {
            _dal = dal ?? throw new ArgumentNullException(nameof(dal));
            _language = language ?? throw new ArgumentNullException(nameof(language));
            _email = email ?? throw new ArgumentNullException(nameof(email));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                DataBind();
                PopulateCountyList();
            }
        }
        protected bool SendFormContentByEmail()
        {
            string subject = PropertyService.GetStringProperty(CurrentPage, "EmailSubject");

            string applicationReciever = _email.GetEmailForMunicipality(Ddl_Municipality.SelectedValue);
            string applicationSender = Txt_Email.Text;
            var attachments = Request.Files;

            ContactPerson details = GetDetailsFromUI();

            _email.SendMail(subject, details, attachments, applicationReciever, applicationSender);
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
            Ddl_County.DataSource = countyList;
            Ddl_County.DataBind();
        }

        /// <summary>
        /// Populate Ddl_Municipality with municipality from the given county
        /// </summary>
        /// <param name="county"></param>
        protected void PopulateMunicipalityList(string county)
        {
            Ddl_Municipality.Items.Clear();
            Ddl_Municipality.Items.Add(new ListItem("", ""));
            Ddl_Municipality.Items = _dal.PopulateMunicipalityList(county);
        }

        /// <summary>
        /// Creates as many FileUpload controls as configured on the page.
        /// </summary>
        private void BuildDynamicControls()
        {
            if (pnlFileUpload.Visible)
            {
                //Create dummy datasource to display the correct number of FileUpload controls.
                if (!CurrentPage.Property["NumberOfFileUploads"].IsNull)
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