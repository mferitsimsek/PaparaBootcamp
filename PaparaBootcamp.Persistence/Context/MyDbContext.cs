using Microsoft.EntityFrameworkCore;
using PaparaBootcamp.Domain.Entities;


namespace PaparaBootcamp.Persistence.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<ProductEntity> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<ProductEntity>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<ProductEntity>().Property(p => p.Description).IsRequired();
        }
    }
}
