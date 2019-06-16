using System.Linq;
using Task3.BL;

namespace Task3.DAL
{
    public interface IDAO
    {
        IQueryable<DropdownListItem> GetMunicipalities(string country);
        string GetEmailForMunicipality(string municipality);
        IQueryable<string> GetCountryList();
    }
}