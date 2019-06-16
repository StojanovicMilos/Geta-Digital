using System;
using System.Collections.Generic;
using Task3.BLL.Interfaces;

namespace Task3.BLL.BO
{
    public class EmailData
    {
        public EmailData(IEmailAddressValidationUtility emailAddressValidationUtility, string emailHeader, string emailFooter, string applicationSender, string subject, List<string> emailReceivers, List<string> bcc,
            List<AttachmentFile> attachments, string county, string municipality, string applicator, string address, string postCode, string postArea,
            string birthNumber, string contactPerson, string phone, string email, string description, string financePlan, string businessDescription, string applicationAmount)
        {
            if (string.IsNullOrEmpty(applicationSender)) throw new ArgumentException(nameof(applicationSender));
            if (string.IsNullOrEmpty(subject)) throw new ArgumentException(nameof(subject));
            if (string.IsNullOrEmpty(postCode)) throw new ArgumentException(nameof(postCode));
            if (string.IsNullOrEmpty(postArea)) throw new ArgumentException(nameof(postArea));
            if (string.IsNullOrEmpty(birthNumber)) throw new ArgumentException(nameof(birthNumber));
            
            if (emailReceivers.Count == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(emailReceivers));

            IEmailAddressValidationUtility stringValidationUtility1 = emailAddressValidationUtility ?? throw new ArgumentNullException(nameof(emailAddressValidationUtility));
            foreach (string emailReceiverAddress in emailReceivers)
            {
                if (!stringValidationUtility1.IsValidEmailAddress(emailReceiverAddress))
                {
                    throw new ArgumentException(emailReceiverAddress + " is not a valid email address.");
                }
            }

            EmailHeader = emailHeader ?? throw new ArgumentNullException(nameof(emailHeader));
            EmailFooter = emailFooter ?? throw new ArgumentNullException(nameof(emailFooter));
            ApplicationSender = applicationSender;
            Subject = subject;
            EmailReceivers = emailReceivers;
            Bcc = bcc ?? throw new ArgumentNullException(nameof(bcc));
            Attachments = attachments ?? throw new ArgumentNullException(nameof(attachments));
            County = county ?? throw new ArgumentNullException(nameof(county));
            Municipality = municipality ?? throw new ArgumentNullException(nameof(municipality));
            Applicator = applicator ?? throw new ArgumentNullException(nameof(applicator));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            PostCode = postCode;
            PostArea = postArea;
            BirthNumber = birthNumber;
            ContactPerson = contactPerson ?? throw new ArgumentNullException(nameof(contactPerson));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            FinancePlan = financePlan ?? throw new ArgumentNullException(nameof(financePlan));
            BusinessDescription = businessDescription ?? throw new ArgumentNullException(nameof(businessDescription));
            ApplicationAmount = applicationAmount ?? throw new ArgumentNullException(nameof(applicationAmount));
        }

        public string EmailHeader { get; }
        public string EmailFooter { get; }
        public string ApplicationSender { get; }
        public string Subject { get; }
        public List<string> EmailReceivers { get; }
        public List<string> Bcc { get; }
        public List<AttachmentFile> Attachments { get; }
        public string County { get;  }
        public string Municipality { get; }
        public string Applicator { get; }
        public string Address { get; }
        public string PostCode { get; }
        public string PostArea { get; }
        public string BirthNumber { get; }
        public string ContactPerson { get; }
        public string Phone { get; }
        public string Email { get; }
        public string Description { get; }
        public string FinancePlan { get; }
        public string BusinessDescription { get; }
        public string ApplicationAmount { get; }
    }
}