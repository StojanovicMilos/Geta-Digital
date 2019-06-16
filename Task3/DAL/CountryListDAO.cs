using System.Linq;
using Task3.BLL.Interfaces;

namespace Task3.DAL
{
    public class CountryListDAO : ICountryListDAO
    {
        public IQueryable<string> GetCountryList() => new[] { "", "Nordland", "Nord Trøndelag", "Sør Trøndelag", "Møre og Romsdal", "Sogn og Fjordane", "Hordaland", "Rogaland", "Vest Agder" }.AsQueryable();
    }
}
