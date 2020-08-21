using ElbaMobileXamarinDeveloperTest.Core.DataBase.Models;
using ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.Contacts;
using ElbaMobileXamarinDeveloperTest.Core.Services.Phone;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElbaMobileXamarinDeveloperTest.Core.ViewModels
{
    public class FullContactViewModel
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IPhoneService _phoneService;
        private Contact _contact;

        public FullContactViewModel(IContactsRepository contactsRepository,
            IPhoneService phoneService)
        {
            _contactsRepository = contactsRepository;
            _phoneService = phoneService;
        }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Biography { get; set; }

        public string Temperament { get; set; }

        public string EducationPeriod { get; set; }

        public bool IsLoaded => _contact != null;

        public FullContactViewModel Load(int id)
        {
            _contact = _contactsRepository.GetContact(id);

            if (IsLoaded)
            {
                Name = _contact.Name;
                Phone = _phoneService.FormatNormalizedPhone(_contact.Phone);
                Biography = _contact.Biography;
                Temperament = _contact.Temperament.ToString();
                EducationPeriod = $"{_contact.StartDate.ToString()} - {_contact.EndDate.ToString()}";
            }

            return this;
        }
    }
}
