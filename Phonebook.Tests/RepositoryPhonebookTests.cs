using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Phonebook.DAL.Extensions;
using Phonebook.DAL.Models;

namespace Phonebook.Tests
{
    public class RepositoryPhonebookTests : IClassFixture<RepositoryTestBase>
    {
        private readonly RepositoryTestBase fixture;
        public RepositoryPhonebookTests(RepositoryTestBase testFixture)
        {
            fixture = testFixture;
        }

        [Fact]
        public async Task For_GetEntries_Given_Valid_PhonebookId_Must_Return_EntriesAsync()
        {
            //Arrange
            int phonebookId = fixture.phonebookId;

            //Act
            DatabaseService databaseService = new DatabaseService(fixture.DBContext);
            IEnumerable<Entry> entriesResult = await databaseService.GetEntriesAsync(phonebookId);

            //Asset
            Assert.True(entriesResult.Count() > 0);
        }

        [Fact]
        public async Task For_AddEntry_Given_Valid_ContactInfo_Must_Return_True_Async()
        {
            //Arrange
            Entry entry = fixture.entryToAdd;

            //Act
            DatabaseService databaseService = new DatabaseService(fixture.DBContext);
            bool entriesResult = await databaseService.Save(entry);

            //Asset
            Assert.True(entriesResult);
        }
    }
}
