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
        private Mock<IPhonebookService> iPhonebookServiceMoq;
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

            iPhonebookServiceMoq.Setup(x => x.GetEntriesAsync(It.IsAny<int>()))
                                    .ReturnsAsync(new List<ContactInfoDTO>(contactInfoDTOs));

            //Act
            phonebookController = new PhonebookController(iPhonebookServiceMoq.Object);
            IEnumerable<ContactInfoDTO> result = await phonebookController.GetEntries(phonebookId);

            //Assert
            Assert.Equal(contactInfoDTOs, result);
            Assert.True(contactInfoDTOs.Count == result.Count());
        }

        [Fact]
        public async Task For_AddEntry_Given_Valid_ContactInfo_Must_Return_True_Async()
        {
            //Arrange
            ContactInfoDTO contactInfoDTO = fixture.contactInfoDTOToAdd;

            iPhonebookServiceMoq.Setup(x => x.Save(It.IsAny<ContactInfoDTO>()))
                                    .ReturnsAsync(true);

            //Act
            phonebookController = new PhonebookController(iPhonebookServiceMoq.Object);
            ActionResult<bool> result = await phonebookController.AddEntry(contactInfoDTO);
            var okObjectResult = (OkObjectResult)result.Result;
            var finalResult = okObjectResult.Value; 

            //Assert
            Assert.True(finalResult is bool);
            Assert.True((bool)finalResult);
        }
    }
}
