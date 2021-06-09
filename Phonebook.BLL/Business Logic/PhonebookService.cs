using System.Threading.Tasks;
using AutoMapper;
using Phonebook.DAL.Interfaces;
using Phonebook.DAL.Models.DTO;
using Phonebook.DAL.Models;
using System.Collections.Generic;

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
