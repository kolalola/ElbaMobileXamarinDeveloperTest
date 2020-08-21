using ElbaMobileXamarinDeveloperTest.Core.DataBase.Models;
using ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.Contacts;
using ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.DownloadsHistory;
using ElbaMobileXamarinDeveloperTest.Core.Services.Contacts;
using ElbaMobileXamarinDeveloperTest.Core.Services.Phone;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElbaMobileXamarinDeveloperTest.Core.ViewModels
{
    public class MainViewModel
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IDownloadsHistoryRepository _historyRepository;
        private readonly IContactsLoaderService _loadService;
        private readonly IPhoneService _phoneService;

        public int LoadCount => 30;
        public TimeSpan UpdatePeriod => TimeSpan.FromMinutes(1);
        public int Page { get; private set; }

        public MainViewModel(IContactsRepository contactsRepository,
            IDownloadsHistoryRepository historyRepository,
            IContactsLoaderService loadService,
            IPhoneService phoneService)
        {
            _contactsRepository = contactsRepository;
            _historyRepository = historyRepository;
            _loadService = loadService;
            _phoneService = phoneService;
            Contacts = new List<ContactViewModel>();
        }

        public List<ContactViewModel> Contacts { get; set; }

        public async Task<MainViewModel> UpdateOrNothing()
        {
            if(_historyRepository.IsNeedToUpdate(UpdatePeriod))
            {
                var contacts = await _loadService.LoadContactsAsync();
                _contactsRepository.RefreshData(contacts);
            }

            return this;
        }

        public MainViewModel LoadMoreContacts()
        {
            var contacts = _contactsRepository.GetContacts(Page, LoadCount);
            if (contacts != null)
                this.Contacts.AddRange(contacts.Select(c => Map(c)));

            Page++;
            return this;
        }

        public MainViewModel ReloadContacts()
        {
            Page = 0;
            LoadMoreContacts();

            return this;
        }

        public MainViewModel RefreshContacts()
        {
            Contacts.Clear();
            ReloadContacts();
            Page = 0;
            return this;
        }

        public MainViewModel StartNewSearch(string text)
        {
            Page = 0;
            Contacts.Clear();
            SearchMore(text);
            return this;
        }

        public MainViewModel SearchMore(string text)
        {
            var contacts = _contactsRepository.Search(text, Page, LoadCount);
            if (contacts != null)
                this.Contacts.AddRange(contacts.Select(c => Map(c))
                .ToList());

            Page++;
            return this;
        }

        private ContactViewModel Map(Contact contact) => new ContactViewModel
        {
            Height = contact.Height,
            Name = contact.Name,
            Phone = _phoneService.FormatNormalizedPhone(contact.Phone),
            Id = contact.Id
        };
    }
}
