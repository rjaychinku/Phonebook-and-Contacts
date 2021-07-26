using System.Collections.Generic;
using System.Threading.Tasks;
using Phonebook.DAL.Models.DTO;

namespace Phonebook.BLL
{
    public interface IPhonebookService
    {
        Task<bool> AddContactAsync(ContactInfoDTO entry);
        Task<IEnumerable<ContactInfoDTO>> GetContactsAsync(int phonebookId);
        Task<IEnumerable<PhonebookDTO>> GetPhonebookAsync();
        Task<bool> AddPhonebookAsync(PhonebookDTO phonebook);
    }
}
