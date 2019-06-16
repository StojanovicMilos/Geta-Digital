using System.Linq;

namespace Task3.BLL.Interfaces
{
    public interface ICountryDAO
    {
        IQueryable<string> GetCountries();
    }
}