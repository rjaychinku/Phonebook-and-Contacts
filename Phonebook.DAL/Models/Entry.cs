using System.ComponentModel.DataAnnotations;

namespace Phonebook.DAL.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int PhonebookId { get; set; }
        public Phonebook Phonebook { get; set; }
    }
}
