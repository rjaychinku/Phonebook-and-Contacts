using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using Phonebook.DAL.DataContext;
using Phonebook.DAL.Models;

namespace SalesTaxes.Tests
{
    public class RepositoryTestBase : IDisposable
    {
        private readonly string dbName = "TestDatabase.sqlite";
        private SqliteConnection connection;

        public int phonebookId = 1;
        public PhonebookApiContext DBContext;
        public List<Entry> dbEntriesList;
        public Entry entryToAdd;

        public RepositoryTestBase()
        {
            SetDummyData();

            DbContextOptions<PhonebookApiContext> options = new DbContextOptionsBuilder<PhonebookApiContext>()
            .UseSqlite(CreateDatabase())
            .Options;

            DBContext = new PhonebookApiContext(options);
            DBContext.Database.EnsureCreated();
            DBContext.Entries.AddRange(dbEntriesList);
            DBContext.SaveChanges(); //seed some data
        }
        private void SetDummyData()
        {
            dbEntriesList = new List<Entry> {
                                                    new Entry
                                                    {
                                                        Name = "David",
                                                        Number = "0810000000",
                                                        PhonebookId = phonebookId
                                                    },
                                                     new Entry
                                                    {
                                                        Name = "Goliath",
                                                        Number = "0820000000",
                                                        PhonebookId = phonebookId
                                                    },
                                                     new Entry
                                                    {
                                                        Name = "Telemundo",
                                                        Number = "0830000000",
                                                        PhonebookId = phonebookId
                                                    }
                                                };

            entryToAdd = new Entry
            {
                Name = "Tester",
                Number = "0710000000",
                PhonebookId = phonebookId
            };
        }
        private DbConnection CreateDatabase()
        {
            if (!File.Exists(dbName))
            {
                SQLiteConnection.CreateFile(dbName);
            }

            connection = new SqliteConnection("Data Source = " + dbName + ";");
            connection.Open();

            return connection;
        }
        public void Dispose()
        {
            if (connection != null)
                connection.Close();

            File.Delete(dbName);
        }
    }
}
