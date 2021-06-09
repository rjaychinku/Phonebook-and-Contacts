using System.Collections.Generic;
using Phonebook.DAL.Models.DTO;

namespace SalesTaxes.Tests
{
    public class ControllerTestBase
    {
        public int phonebookId = 1;
        public List<ContactInfoDTO> contactInfoDTOList;
        public ContactInfoDTO contactInfoDTOToAdd;
        public ControllerTestBase()
        {
            SetDummyData();
        }
        private void SetDummyData()
        {
            contactInfoDTOList = new List<ContactInfoDTO> {
                                                    new ContactInfoDTO
                                                    {
                                                        Name = "Yakapwasha",
                                                        Number = "0810000000"
                                                    },
                                                     new ContactInfoDTO
                                                    {
                                                        Name = "Junior",
                                                        Number = "0820000000"
                                                    }
                                                };
            contactInfoDTOToAdd = new ContactInfoDTO
            {
                Name = "Tester",
                Number = "0710000000"
            };
        }
    }
}
