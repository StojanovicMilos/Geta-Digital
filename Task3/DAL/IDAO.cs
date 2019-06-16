using System.Collections.Generic;
using Legacy.Web.Templates.Pages;

namespace Task3.DAL
{
    public interface IDAO
    {
        List<ListItem> PopulateMunicipalityList(string country);
        List<ContactPerson> PopulateContactPersonList();
        string[] GetCountryList();
    }
}