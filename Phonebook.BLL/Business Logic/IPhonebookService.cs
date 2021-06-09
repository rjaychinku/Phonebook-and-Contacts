using System.Collections.Generic;
using System.Threading.Tasks;
using Phonebook.DAL.Models.DTO;

namespace Phonebook.BLL
{
    public interface IPhonebookService
    {
        Task<bool> Save(ContactInfoDTO entry);
        Task<IEnumerable<ContactInfoDTO>> GetEntriesAsync(int phonebookId);
    }
}
