using System.Linq;
using Task3.BLL.Interfaces;

namespace Task3.DAL
{
    public class CountryDAO : ICountryDAO
    {
        public IQueryable<string> GetCountries() => new[] { "", "Nordland", "Nord Trøndelag", "Sør Trøndelag", "Møre og Romsdal", "Sogn og Fjordane", "Hordaland", "Rogaland", "Vest Agder" }.AsQueryable();
    }
}
