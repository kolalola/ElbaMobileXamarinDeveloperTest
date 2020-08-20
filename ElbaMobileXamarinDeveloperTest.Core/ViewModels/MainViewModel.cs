using ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElbaMobileXamarinDeveloperTest.Core.ViewModels
{
    public class MainViewModel
    {
        private readonly IContactsRepository _contactsRepository;

        public int LoadCount => 30;
        public int Page { get; private set; }

        public MainViewModel(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
            Contacts = new List<ContactViewModel>();
        }

        public List<ContactViewModel> Contacts { get; set; }

        public MainViewModel LoadMoreContacts(int page)
        {
            var contacts = _contactsRepository.GetContacts(page);
            if (contacts != null)
                this.Contacts.AddRange(contacts.Select(c => new ContactViewModel
                {
                    Biography = c.Biography,
                    EndDate = c.EndDate,
                    Height = c.Height,
                    Name = c.Name,
                    Phone = c.Phone,
                    Temperament = c.Temperament,
                    StartDate = c.StartDate
                }));

            Page++;
            return this;
        }

        public MainViewModel LoadFirstContacts()
        {
            var contacts = _contactsRepository.GetContacts(0);
            if (contacts != null)
                this.Contacts.AddRange(contacts.Select(c => new ContactViewModel
                {
                    Biography = c.Biography,
                    EndDate = c.EndDate,
                    Height = c.Height,
                    Name = c.Name,
                    Phone = c.Phone,
                    Temperament = c.Temperament,
                    StartDate = c.StartDate
                }));

            return this;
        }

        public MainViewModel RefreshContacts()
        {
            Contacts.Clear();
            LoadFirstContacts();
            Page = 0;
            return this;
        }

    }
}
