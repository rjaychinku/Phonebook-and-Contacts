using Microsoft.EntityFrameworkCore;
using models = Phonebook.DAL.Models;

namespace Phonebook.DAL.DataContext
{
    public class PhonebookApiContext : DbContext
    {
        public PhonebookApiContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<models.Entry> Entries { get; set; }
        public DbSet<models.Phonebook> Phonebooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            models.Phonebook phonebook = new models.Phonebook //Wouldn't be hardcoded on an actual phone/device
            {
                Name = "Ronald's phonebook",
                PhonebookId = 1
            };

            modelBuilder.Entity<models.Phonebook>().HasData(phonebook);

            modelBuilder.Entity<models.Phonebook>()
                         .HasMany(p => p.Entries)
                         .WithOne(e => e.Phonebook)
                         .HasForeignKey(e => e.PhonebookId);
        }
    }
}
