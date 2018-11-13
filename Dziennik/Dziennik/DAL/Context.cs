using Dziennik.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Dziennik.DAL
{
    public class Context : DbContext
    {

        public Context() : base("Context")
        {
        }

        public DbSet<Uczen> Uczniowie { get; set; }
        public DbSet<Przedmiot> Przedmioty { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}