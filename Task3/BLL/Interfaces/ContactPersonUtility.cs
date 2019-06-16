using System;
using System.Linq;
using Task3.BLL.BO;

namespace Task3.BLL.Interfaces
{
    public class ContactPersonUtility : IContactPersonUtility
    {
        private readonly IContactPersonDAO _contactPersonDAO;

        public ContactPersonUtility(IContactPersonDAO countaPersonDAO)
        {
            _contactPersonDAO = countaPersonDAO ?? throw new ArgumentNullException(nameof(countaPersonDAO));
        }

        public IQueryable<ContactPerson> GetContactPersons() => _contactPersonDAO.GetContactPersons();
    }
}