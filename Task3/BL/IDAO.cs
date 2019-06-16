using System.Linq;

namespace Task3.BL
{
    public interface IDAO
    {
        IQueryable<DropdownListItem> GetMunicipalities(string country);
        string GetEmailForMunicipality(string municipality);
        IQueryable<string> GetCountryList();
    }
}