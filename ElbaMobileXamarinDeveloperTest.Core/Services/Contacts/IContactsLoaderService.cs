using ElbaMobileXamarinDeveloperTest.Core.DataBase.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElbaMobileXamarinDeveloperTest.Core.Services.Contacts
{
    public interface IContactsLoaderService
    {
        Task<IList<Contact>> LoadContactsAsync();
    }
}
