using System;
using System.Linq;

namespace Task3.BLL.Interfaces
{
    public class CountryUtility : ICountryUtility
    {
        private readonly ICountryDAO _countryListDAO;

        public CountryUtility(ICountryDAO countryListDAO)
        {
            _countryListDAO = countryListDAO ?? throw new ArgumentNullException(nameof(countryListDAO));
        }

        public IQueryable<string> GetCountries() =>
            _countryListDAO.GetCountries();
    }
}