using System.Collections.Generic;
using System.Threading.Tasks;
using model = Phonebook.DAL.Models;
using Phonebook.DAL.Models;

namespace Phonebook.DAL.Interfaces
{
    public interface IDatabaseService
    {
        Task<bool> Save(Entry entry);
        Task<bool> AddPhonebook(model.Phonebook phonebook);
        Task<IEnumerable<Entry>> GetEntriesAsync(int phonebookId);
        Task<IEnumerable<model.Phonebook>> GetPhonebooksAsync();
    }
}