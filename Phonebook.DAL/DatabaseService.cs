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
        private readonly PhonebookApiContext _dbcontext;
        public DatabaseService(PhonebookApiContext dbcontext)
        {
            _dbcontext = dbcontext;
            _dbcontext.Database.EnsureCreated();
        }
        public async Task<bool> Save(Entry entry)
        {
            _dbcontext.Entries.Add(entry);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Entry>> GetEntriesAsync(int phonebookId)
        {
            var test = await _dbcontext.Entries.ToListAsync();
            List<models.Phonebook> phonebooks = await _dbcontext.Phonebooks.ToListAsync();
            return phonebooks.Find(c => c.PhonebookId == phonebookId).Entries;
        }

        public async Task<bool> AddPhonebook(models.Phonebook phonebook)
        {
            _dbcontext.Phonebooks.Add(phonebook);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<models.Phonebook>> GetPhonebooksAsync()
        {
            List<models.Phonebook> phonebooks = await _dbcontext.Phonebooks.ToListAsync();
            return phonebooks;
        }
    }
}
