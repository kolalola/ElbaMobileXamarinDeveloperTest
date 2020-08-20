using ElbaMobileXamarinDeveloperTest.Core.DataBase.Models;
using ElbaMobileXamarinDeveloperTest.Core.Dto;
using ElbaMobileXamarinDeveloperTest.Core.Services.Rest;
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
        private static IList<string> _sources = new List<string>
        {
            "https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-01.json",
            "https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-02.json",
            "https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-03.json"
        };

        public ContactsLoaderService(IRestService restService)
        {
            _client = restService;
        }

        public async Task<IList<Contact>> LoadContactsAsync()
        {
            var result = new List<ContactDto>();

            foreach (var source in _sources)
            {
                var contacts = await _client.GetOrDefaultAsync<List<ContactDto>>(source);
                if (contacts != null)
                    result.AddRange(contacts);
            }

            return result.Select(c => new Contact
            {
                Biography = c.Biography,
                EndDate = c.EducationPeriod.EndDate,
                Height = c.Height,
                Name = c.Name,
                Phone = c.Phone,
                Temperament = (Temperament)c.Temperament,
                StartDate = c.EducationPeriod.StartDate,
                ExternalId = c.Id
            })
                .OrderBy(c => c.Name)
                .ToList();
        }
    }
}
