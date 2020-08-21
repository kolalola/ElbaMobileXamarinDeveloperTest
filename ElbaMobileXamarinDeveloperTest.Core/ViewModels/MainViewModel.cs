using ElbaMobileXamarinDeveloperTest.Core.DataBase.Models;
using ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.Contacts;
using ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.DownloadsHistory;
using ElbaMobileXamarinDeveloperTest.Core.Helpers;
using ElbaMobileXamarinDeveloperTest.Core.Services.Contacts;
using ElbaMobileXamarinDeveloperTest.Core.Services.Phone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElbaMobileXamarinDeveloperTest.Core.ViewModels
{
    public class MainViewModel
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IDownloadsHistoryRepository _historyRepository;
        private readonly IContactsLoaderService _loadService;
        private readonly IPhoneService _phoneService;

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

        public int LoadCount => 30;
        public TimeSpan UpdatePeriod => TimeSpan.FromMinutes(1);
        public int Page { get; private set; }
        public string SearchText { get; set; }
        public bool IsSearching => !string.IsNullOrEmpty(SearchText);

        /// <summary>
        /// Запрашивает с источников контакты, если не смог загрузить, то ничего не делает
        /// </summary>
        public async Task<MainViewModel> UpdateOrNothing(Action<string> errorAction = null)
        {
            if(_historyRepository.IsNeedToUpdate(UpdatePeriod))
            {
                var contacts = await _loadService.LoadContactsAsync();

                if (contacts == null && errorAction != null)
                    errorAction(StringHelper.NotLoaded);

                _contactsRepository.RefreshData(contacts);
            }

            return this;
        }

        /// <summary>
        /// Прогружает дальше список
        /// </summary>
        /// <returns></returns>
        public MainViewModel LoadMore()
        {
            if (IsSearching)
                SearchMore();
            else
                LoadMoreContacts();

            return this;
        }

        /// <summary>
        /// Сбрасывает список, ищет или прогружает заново, в зависимости от SearchText
        /// SearchText нужно обновлять
        /// </summary>
        public MainViewModel RefreshContacts()
        {
            Contacts.Clear();
            Page = 0;

            if (IsSearching)
                SearchMore();
            else
                LoadMoreContacts();
            return this;
        }

        private MainViewModel LoadMoreContacts()
        {
            var contacts = _contactsRepository.GetContacts(Page, LoadCount);
            if (contacts != null)
                this.Contacts.AddRange(contacts.Select(c => Map(c)));

            Page++;
            return this;
        }

        private MainViewModel SearchMore()
        {
            var contacts = _contactsRepository.Search(SearchText, Page, LoadCount);
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
