using ElbaMobileXamarinDeveloperTest.Core.DataBase.Models;
using PhoneNumbers;
using System.Collections.Generic;
using System.Linq;

namespace ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.Contacts
{
    public class ContactsRepository : Repository, IContactsRepository
    {
        public Contact GetContact(int id)
        {
            lock(Locker)
            {
                return Database.Table<Contact>().FirstOrDefault(c => c.Id == id);
            }
        }

        public IList<Contact> GetContacts(int page, int loadCount)
        {
            lock(Locker)
            {
                return Database.Table<Contact>()
                    .Skip(page * loadCount)
                    .Take(loadCount)
                    .ToList();
            }
        }

        public void RefreshData(IList<Contact> contacts)
        {
            if (contacts != null && contacts.Any())
            {
                lock (Locker)
                {
                    Database.DeleteAll<Contact>();
                    Database.InsertAll(contacts);
                }
            }
        }

        public IList<Contact> Search(string text, int page, int loadCount)
        {
            var isPhone = !text.Any(c => char.IsLetter(c));

            text = text.ToLower();
            var normalizedPhone = PhoneNumberUtil.Normalize(text);

            var query = Database.Table<Contact>();

            query = isPhone
                    ? query.Where(c => c.Phone.Contains(normalizedPhone))
                    : query.Where(c => c.Name.ToLower().StartsWith(text));

            lock (Locker)
            {
                return query
                    .Skip(page * loadCount)
                    .Take(loadCount)
                    .ToList();
            }
        }

        protected override void CreateTables()
        {
            Database.CreateTable<Contact>();
        }
    }
}
