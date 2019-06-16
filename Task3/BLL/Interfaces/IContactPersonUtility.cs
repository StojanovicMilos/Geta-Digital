using System.Linq;
using Task3.BLL.BO;

namespace Task3.BLL.Interfaces
{
    public interface IContactPersonUtility
    {
        IQueryable<ContactPerson> GetContactPersons();
    }
}
