using System;
using System.Linq;
using Task3.BLL.BO;

namespace Task3.BLL.Interfaces
{
    public class ContactPersonUtility : IContactPersonUtility
    {
        private readonly IContactPersonDAO _dao;

        public ContactPersonUtility(IContactPersonDAO dao)
        {
            _dao = dao ?? throw new ArgumentNullException(nameof(dao));
        }

        public IQueryable<ContactPerson> GetContactPersons() => _dao.GetContactPersons();
    }
}