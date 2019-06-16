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

        public Email(ILanguage language)
        {
            _language = language ?? throw new ArgumentNullException(nameof(language));
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
            if (data == null) throw new ArgumentNullException(nameof(data));
            MailMessage mail = new MailMessage();

            mail.Body = content ?? throw new ArgumentNullException(nameof(content));
            mail.Subject = data.Subject;
            mail.From = new MailAddress(data.ApplicationSender, data.ApplicationSender);
            foreach (var attendee in data.EmailReceivers)
            {
                mail.To.Add(attendee);
            }

            foreach (var bcc in data.Bcc)
            {
                mail.Bcc.Add(bcc);
            }

            foreach (Attachment attachment in GetAttachments(data.Attachments))
            {
                mail.Attachments.Add(attachment);
            }

            return mail;
        }

        private Attachment[] GetAttachments(List<AttachmentFile> files) =>
            (from file in files.Where(f => f != null && f.ContentLength > 0)
                let fileName = Path.GetFileName(file.FileName)
                where fileName != string.Empty
                select new Attachment(file.InputStream, fileName, file.ContentType)
            ).ToArray();

        private bool SendMail(MailMessage mail, bool isBodyHtml)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                mail.IsBodyHtml = isBodyHtml;
                smtp.Send(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
