using System.Linq;

namespace Task3.BLL.Interfaces
{
    public interface ICountryUtility
    {
        IQueryable<string> GetCountries();
    }
}
