using System.Threading.Tasks;
using AutoMapper;
using Phonebook.DAL.Interfaces;
using Phonebook.DAL.Models.DTO;
using model = Phonebook.DAL.Models;
using System.Collections.Generic;
using Phonebook.DAL.Models;

namespace Phonebook.BLL
{
    public class PhonebookService : IPhonebookService
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseService _dbService;
        public PhonebookService(IDatabaseService dbService, IMapper mapper)
        {
            _dbService = dbService;
            _mapper = mapper;
        }

        public async Task<bool> Add(PhonebookDTO phonebook)
        {
            model.Phonebook entry = _mapper.Map<model.Phonebook>(source: phonebook);
            return await _dbService.AddPhonebook(entry);
        }

        public async Task<IEnumerable<PhonebookDTO>> GetAsync()
        {
            IEnumerable<model.Phonebook> dbPhonebooks = await _dbService.GetPhonebooksAsync();
            IEnumerable<PhonebookDTO> phonebooks = _mapper.Map<IEnumerable<PhonebookDTO>>(source: dbPhonebooks);
            return phonebooks;
        }

        public async Task<IEnumerable<ContactInfoDTO>> GetEntriesAsync(int phonebookId)
        {
            IEnumerable<Entry> entries = await _dbService.GetEntriesAsync(phonebookId);
            return _mapper.Map<IEnumerable<ContactInfoDTO>>(source: entries);
        }

        public async Task<bool> Save(ContactInfoDTO contactInfo)
        {
            Entry entry = _mapper.Map<Entry>(source: contactInfo);
            return await _dbService.Save(entry);
        }
    }
}
