using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebApplicationEmailService.Models;


namespace WebApplicationEmailService.Data
{
    public class EmailDbContext : DbContext
    {

        static EmailDbContext()
        {
            Database.SetInitializer<EmailDbContext>(null);
        }
        public EmailDbContext() : base("")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}
