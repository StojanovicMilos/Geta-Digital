using System.Linq;
using Task3.BLL.BO;

namespace Task3.BLL.Interfaces
{
    public interface IContactPersonDAO
    {
        IQueryable<ContactPerson> GetContactPersons();
    }
}