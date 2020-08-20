using ElbaMobileXamarinDeveloperTest.Core.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.Contacts
{
    public class ContactsRepository : Repository, IContactsRepository
    {
        public IList<Contact> GetContacts(int page)
        {
            lock(Locker)
            {
                return Database.Table<Contact>()
                    .Skip(page * 30)
                    .Take(30)
                    .ToList();
            }
        }

        public void RefreshData(IList<Contact> contacts)
        {
            lock (Locker)
            {
                Database.DeleteAll<Contact>();
                Database.InsertAll(contacts);
            }
        }

        protected override void CreateTables()
        {
            Database.CreateTable<Contact>();
        }
    }
}
