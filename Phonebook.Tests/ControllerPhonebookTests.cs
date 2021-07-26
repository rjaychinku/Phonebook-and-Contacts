using Moq;
using Phonebook.BLL;
using Phonebook.Web.Controllers;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using Phonebook.DAL.Models.DTO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Phonebook.Tests
{
    public class ControllerPhonebookTests : IClassFixture<ControllerTestBase>
    {
        private readonly ControllerTestBase fixture;
        private PhonebookController phonebookController;
        private readonly Mock<IPhonebookService> iPhonebookServiceMoq;
        public ControllerPhonebookTests(ControllerTestBase testFixture)
        {
            fixture = testFixture;
            iPhonebookServiceMoq = new Mock<IPhonebookService>();
        }

        [Fact]
        public async Task For_GetEntries_Given_Valid_PhonebookId_Must_Return_EntriesAsync()
        {
            //Arrange
            int phonebookId = fixture.phonebookId; //using a fixed phonebook
            List<ContactInfoDTO> contactInfoDTOs = fixture.contactInfoDTOList;

            iPhonebookServiceMoq.Setup(x => x.GetContactsAsync(It.IsAny<int>()))
                                    .ReturnsAsync(new List<ContactInfoDTO>(contactInfoDTOs));

            //Act
            phonebookController = new PhonebookController(iPhonebookServiceMoq.Object);
            IEnumerable<ContactInfoDTO> result = await phonebookController.GetContacts(phonebookId);

            //Assert
            Assert.Equal(contactInfoDTOs, result);
            Assert.True(contactInfoDTOs.Count == result.Count());
        }

        [Fact]
        public async Task For_AddEntry_Given_Valid_ContactInfo_Must_Return_True_Async()
        {
            //Arrange
            ContactInfoDTO contactInfoDTO = fixture.contactInfoDTOToAdd;

            iPhonebookServiceMoq.Setup(x => x.AddContactAsync(It.IsAny<ContactInfoDTO>()))
                                    .ReturnsAsync(true);

            //Act
            phonebookController = new PhonebookController(iPhonebookServiceMoq.Object);
            ActionResult<bool> result = await phonebookController.AddContact(contactInfoDTO);
            OkObjectResult okObjectResult = (OkObjectResult)result.Result;
            object finalResult = okObjectResult.Value;

            //Assert
            Assert.True(finalResult is bool);
            Assert.True((bool)finalResult);
        }
    }
}
