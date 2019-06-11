namespace Legacy.Web.Templates.Pages
{
    public class ContactPerson
    {
        public ContactPerson(string municipality, string county, string email)
        {
            Municipality = municipality;
            County = county;
            Email = email;
        }

        public string Municipality { get; set; }
        public string County { get; set; }
        public string Email { get; set; }
    }
}