using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Task3.BLL.BO;
using Task3.BLL.Interfaces;

namespace Task3.BLL.BL
{
    public class Email : IEmail
    {
        private readonly ILanguage _language;
        private readonly IStringValidationUtility _stringValidationUtility;

        public Email(ILanguage language, IStringValidationUtility stringValidationUtility)
        {
            _language = language ?? throw new ArgumentNullException(nameof(language));
            _stringValidationUtility = stringValidationUtility ?? throw new ArgumentNullException(nameof(stringValidationUtility));
        }

        public bool SendMail(EmailData data)
        { 
            var content = BuildEmailContent(data);
            MailMessage mailMessage = BuildMail(content, data);
            return SendMail(mailMessage, isBodyHtml: true);
        }

        private string BuildEmailContent(EmailData emailData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            const string contentStart = "<html>";
            stringBuilder.AppendLine(contentStart);
            stringBuilder.AppendLine(emailData.EmailHeader);
            const string summaryStart = "<table>";
            stringBuilder.AppendLine(summaryStart);
            const string labelElementStart = "<tr><td><strong>";
            const string labelElementEnd = "</strong></td>";
            const string valueElementStart = "<td>";
            const string valueElementEnd = "</td></tr>";
            stringBuilder.AppendLine(labelElementStart + _language.GetLanguageString("/applicationform/county") + labelElementEnd + valueElementStart + emailData.County + valueElementEnd);
            stringBuilder.AppendLine(labelElementStart + _language.GetLanguageString("/applicationform/municipality") + labelElementEnd + valueElementStart + emailData.Municipality + valueElementEnd);
            stringBuilder.AppendLine(labelElementStart + _language.GetLanguageString("/applicationform/applicator") + labelElementEnd + valueElementStart + emailData.Applicator + valueElementEnd);
            stringBuilder.AppendLine(labelElementStart + _language.GetLanguageString("/applicationform/address") + labelElementEnd + valueElementStart + emailData.Address + valueElementEnd);
            stringBuilder.AppendLine(labelElementStart + _language.GetLanguageString("/applicationform/postcode") + " / " + _language.GetLanguageString("/applicationform/postarea") + labelElementEnd + valueElementStart + emailData.PostCode + " " + emailData.PostArea + valueElementEnd);
            stringBuilder.AppendLine(labelElementStart + _language.GetLanguageString("/applicationform/orgnobirthnumber") + labelElementEnd + valueElementStart + emailData.BirthNumber + valueElementEnd);
            stringBuilder.AppendLine(labelElementStart + _language.GetLanguageString("/applicationform/contactperson") + labelElementEnd + valueElementStart + emailData.ContactPerson + valueElementEnd);
            stringBuilder.AppendLine(labelElementStart + _language.GetLanguageString("/applicationform/phone") + labelElementEnd + valueElementStart + emailData.Phone + valueElementEnd);
            stringBuilder.AppendLine(labelElementStart + _language.GetLanguageString("/applicationform/email") + labelElementEnd + valueElementStart + emailData.Email + valueElementEnd);
            const string labelElementFullWidthStart = "<tr><td colspan=\"2\"><strong>";
            const string labelElementFullWidthEnd = "</strong></td></tr>";
            const string valueElementFullWidthStart = "<tr><td colspan=\"2\">";
            const string valueElementFullWidthEnd = "</td></tr>";
            stringBuilder.AppendLine(labelElementFullWidthStart + _language.GetLanguageString("/applicationform/description") + labelElementFullWidthEnd + valueElementFullWidthStart + emailData.Description + valueElementFullWidthEnd);
            stringBuilder.AppendLine(labelElementFullWidthStart + _language.GetLanguageString("/applicationform/financeplan") + labelElementFullWidthEnd + valueElementFullWidthStart + emailData.FinancePlan + valueElementFullWidthEnd);
            stringBuilder.AppendLine(labelElementFullWidthStart + _language.GetLanguageString("/applicationform/businessdescription") + labelElementFullWidthEnd + valueElementFullWidthStart + emailData.BusinessDescription + valueElementFullWidthEnd);
            stringBuilder.AppendLine(labelElementStart + _language.GetLanguageString("/applicationform/applicationAmount") + labelElementEnd + valueElementStart + emailData.ApplicationAmount + valueElementEnd);
            const string summaryEnd = "</table>";
            stringBuilder.AppendLine(summaryEnd);
            stringBuilder.AppendLine(emailData.EmailFooter);
            const string contentEnd = "</html>";
            stringBuilder.AppendLine(contentEnd);

            return stringBuilder.ToString();
        }

        private MailMessage BuildMail(string content, EmailData data)
        {
            string toAddresses = data.ApplicationReceiver;
            string subject = data.Subject;
            string fromAdress = data.ApplicationSender;
            string bccAddress = data.Bcc;
            Attachment[] attachmentCol = GetAttachments(data.Attachments);

            //Receipents
            MailAddressCollection recipients = new MailAddressCollection();

            if (toAddresses.Contains(";"))
            {
                string[] addresses = toAddresses.Split(';');

                foreach (string s in addresses)
                {
                    if (!s.StartsWith(";"))
                    {
                        recipients.Add(s);
                    }
                }
            }
            else
            {
                recipients.Add(toAddresses);
            }

            //From
            MailAddress from = new MailAddress(fromAdress, fromAdress);
            MailMessage mail = new MailMessage();

            //To
            foreach (MailAddress attendee in recipients)
            {
                mail.To.Add(attendee);
            }

            mail.From = from;
            mail.Subject = subject;
            mail.Body = content;

            if (!string.IsNullOrEmpty(bccAddress))
            {
                mail.Bcc.Add(bccAddress);
            }

            //Attachment
            if (attachmentCol != null)
            {
                foreach (Attachment attachment in attachmentCol)
                {
                    if (attachment != null)
                    {
                        mail.Attachments.Add(attachment);
                    }
                }
            }

            return mail;
        }

        public Attachment[] GetAttachments(List<AttachmentFile> files) =>
            (from file in files.Where(f => f != null && f.ContentLength > 0)
                let fileName = Path.GetFileName(file.FileName)
                where fileName != string.Empty
                select new Attachment(file.InputStream, fileName, file.ContentType)
            ).ToArray();

        private bool SendMail(MailMessage mail, bool isBodyHtml)
        {
            SmtpClient smtp = new SmtpClient();
            mail.IsBodyHtml = isBodyHtml;
            bool retStatus = false;

            if (mail.To.Count > 0 && mail.From.ToString().Length > 0 && mail.Subject.Length > 0)
            {
                try
                {
                    bool ok = true;
                    foreach (MailAddress singleToAddress in mail.To)
                    {
                        if (!_stringValidationUtility.IsValidEmailAddress(singleToAddress.Address))
                        {
                            ok = false;
                        }
                    }

                    if (ok)
                    {
                        //Send mail
                        smtp.Send(mail);
                        retStatus = true;
                    }

                    //Returns true if successful
                    return retStatus;

                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }
    }
}
