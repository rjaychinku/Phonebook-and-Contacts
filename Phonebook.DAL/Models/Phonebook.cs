using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.DAL.Models
{
    public class Phonebook
    {
        [Key]
        public int PhonebookId { get; set; }
        public string Name { get; set; }
        public List<Contact> Entries { get; set; }
    }
}
