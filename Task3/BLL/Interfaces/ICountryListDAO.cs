using System.Linq;

namespace Task3.BLL.Interfaces
{
    public interface ICountryListDAO
    {
        IQueryable<string> GetCountryList();
    }
}