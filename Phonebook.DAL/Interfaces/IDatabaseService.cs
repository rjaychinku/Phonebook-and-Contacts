using System.Collections.Generic;
using System.Threading.Tasks;
using model = Phonebook.DAL.Models;
using Phonebook.DAL.Models;

namespace Phonebook.DAL.Interfaces
{
    public interface IDatabaseService
    {
        Task<bool> AddContactAsync(Contact entry);
        Task<bool> AddPhonebookAsync(model.Phonebook phonebook);
        Task<IEnumerable<Contact>> GetContactsAsync(int phonebookId);
        Task<IEnumerable<model.Phonebook>> GetPhonebooksAsync();
    }
}