using System.Collections.Generic;

namespace Task3.BLL.BO
{
    public class EmailData
    {
        public string EmailHeader { get; set; }
        public string EmailFooter { get; set; }
        public string ApplicationSender { get; set; }
        public string Subject { get; set; }
        public string ApplicationReceiver { get; set; }
        public string Bcc { get; set; }
        public List<AttachmentFile> Attachments { get; set; }
        public string County { get; set; }
        public string Municipality { get; set; }
        public string Applicator { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string PostArea { get; set; }
        public string BirthNumber { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string FinancePlan { get; set; }
        public string BusinessDescription { get; set; }
        public string ApplicationAmount { get; set; }
    }
}