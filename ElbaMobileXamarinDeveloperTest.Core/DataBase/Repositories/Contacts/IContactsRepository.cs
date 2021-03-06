﻿using ElbaMobileXamarinDeveloperTest.Core.DataBase.Models;
using System.Collections.Generic;

namespace ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.Contacts
{
    public interface IContactsRepository
    {
        void RefreshData(IList<Contact> contacts );

        IList<Contact> GetContacts(int page, int loadCount);

        Contact GetContact(int id);

        IList<Contact> Search(string text, int page, int loadCount);
    }
}
