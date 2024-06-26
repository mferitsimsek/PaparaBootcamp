using Microsoft.EntityFrameworkCore;
using PaparaBootcamp.RestfulAPI.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PaparaBootcamp.RestfulAPI.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
        }
    }
}
