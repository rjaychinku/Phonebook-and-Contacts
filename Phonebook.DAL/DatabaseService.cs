using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phonebook.DAL.DataContext;
using Phonebook.DAL.Interfaces;
using Phonebook.DAL.Models;
using models = Phonebook.DAL.Models;

namespace Phonebook.DAL.Extensions
{
    public class DatabaseService : IDatabaseService
    {
        private readonly PhonebookApiContext _dbContext;
        public DatabaseService(PhonebookApiContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }
        public async Task<bool> AddContactAsync(Contact contact)
        {
            _dbContext.Entries.Add(contact);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Contact>> GetContactsAsync(int phonebookId)
        {
            var test = await _dbContext.Entries.ToListAsync();
            List<models.Phonebook> phonebooks = await _dbContext.Phonebooks.ToListAsync();
            return phonebooks.Find(c => c.PhonebookId == phonebookId).Entries;
        }

        public async Task<bool> AddPhonebookAsync(models.Phonebook phonebook)
        {
            _dbContext.Phonebooks.Add(phonebook);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<models.Phonebook>> GetPhonebooksAsync()
        {
            return await _dbContext.Phonebooks.ToListAsync();
        }
    }
}
