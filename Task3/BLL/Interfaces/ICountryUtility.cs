using System;
using System.Linq;

namespace Task3.BLL.Interfaces
{
    public interface ICountryUtility
    {
        IQueryable<string> GetCountryList();
    }

    public class CountryUtility : ICountryUtility
    {
        private readonly ICountryListDAO _dao;

        public CountryUtility(ICountryListDAO dao)
        {
            _dao = dao ?? throw new ArgumentNullException(nameof(dao));
        }

        public IQueryable<string> GetCountryList() =>
            _dao.GetCountryList();
    }
}
