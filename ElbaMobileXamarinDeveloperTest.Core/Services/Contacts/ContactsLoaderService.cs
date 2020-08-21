using ElbaMobileXamarinDeveloperTest.Core.DataBase.Models;
using ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.DownloadsHistory;
using ElbaMobileXamarinDeveloperTest.Core.Dto;
using ElbaMobileXamarinDeveloperTest.Core.Services.Phone;
using ElbaMobileXamarinDeveloperTest.Core.Services.Rest;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElbaMobileXamarinDeveloperTest.Core.Services.Contacts
{
    public class ContactsLoaderService : IContactsLoaderService
    {
        private readonly IRestService _client;
        private readonly IDownloadsHistoryRepository _historyRepository;
        private readonly IPhoneService _phoneService;
        private static IList<string> _sources = new List<string>
        {
            "https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-01.json",
            "https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-02.json",
            "https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-03.json"
        };

        public ContactsLoaderService(IRestService restService,
            IDownloadsHistoryRepository historyRepository,
            IPhoneService phoneService)
        {
            _client = restService;
            _historyRepository = historyRepository;
            _phoneService = phoneService;
        }

        public async Task<IList<Contact>> LoadContactsAsync()
        {
            var result = new List<ContactDto>();

            foreach (var source in _sources)
            {
                var contacts = await _client.GetOrDefaultAsync<List<ContactDto>>(source);

                if (contacts != null)
                    result.AddRange(contacts);
                else
                    return null;
            }

            _historyRepository.CreateOrUpdateHistory(DateTime.UtcNow);
            return result.Select(c => new Contact
            {
                Biography = c.Biography,
                EndDate = c.EducationPeriod.EndDate,
                Height = c.Height,
                Name = c.Name,
                Phone = _phoneService.Normalize(c.Phone),
                Temperament = (Temperament)c.Temperament,
                StartDate = c.EducationPeriod.StartDate,
                ExternalId = c.Id
            })
                .OrderBy(c => c.Name)
                .ToList();
        }
    }
}
