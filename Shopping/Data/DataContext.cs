using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopping.Data.Entities;

namespace Shopping.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option)
        {

        }

        public DbSet<Country> countries { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<State> states { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Nombre).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Nombre).IsUnique();
            modelBuilder.Entity<City>().HasIndex("Nombre","statesId").IsUnique();
            modelBuilder.Entity<State>().HasIndex("Nombre", "countryId").IsUnique();
        }
    }
}
