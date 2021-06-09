using System.Collections.Generic;
using System.Threading.Tasks;
using Phonebook.DAL.Models;
using Phonebook.DAL.Models.DTO;

namespace Phonebook.DAL.Interfaces
{
    public interface IDatabaseService
    {
        Task<bool> Save(Entry entry);

        Task<IEnumerable<Entry>> GetEntriesAsync(int phonebookId);
    }
}