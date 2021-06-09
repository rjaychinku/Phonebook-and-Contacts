using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phonebook.BLL;
using Phonebook.DAL.Extensions;
using Phonebook.DAL.Interfaces;
using Microsoft.OpenApi.Models;
using Phonebook.DAL.DataContext;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using System.IO;

namespace Phonebook.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            return services.AddDbContext<PhonebookApiContext>(options => options
                    .UseSqlite(CreateDatabase()));
        }

        private static DbConnection CreateDatabase()
        {
            string dbName = "MyDatabase.sqlite";

            if (!File.Exists(dbName))
            {
                SQLiteConnection.CreateFile(dbName);
            }

            SqliteConnection connection = new SqliteConnection("Data Source = " + dbName + ";");
            connection.Open();

            return connection;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddScoped<IPhonebookService, PhonebookService>();

            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Phonebook app Swagger",
                    Description = "Swagger for Phonebook Controllers",
                    Contact = new OpenApiContact()
                    {
                        Name = "Ronald Chinku",
                        Email = "ronaldc@blahblah.co.za"
                    }
                });
            });
        }
    }
}
