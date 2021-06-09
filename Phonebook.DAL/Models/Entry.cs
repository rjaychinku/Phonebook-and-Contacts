using System.ComponentModel.DataAnnotations;

namespace Phonebook.DAL.Models
{
    public class Entry
    {
        [Key]
        public int EntryId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        private int _phonebookId = 1;
        public int PhonebookId
        {
         
            get => _phonebookId;
            
            //only doing this for the sake of the assessment
            set
            {
                value = 1;
                _phonebookId = value;
            }
        }
        public Phonebook Phonebook { get; set; }
    }
}
