using ElbaMobileXamarinDeveloperTest.Core.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.Contacts
{
    public interface IContactsRepository
    {
        void RefreshData(IList<Contact> contacts );

        IList<Contact> GetContacts(int page);
    }
}
