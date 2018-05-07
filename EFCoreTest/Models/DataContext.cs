using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTest.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("InMemoryTest");
            }
        }

        public DbSet<Plug> Plugs { get; set; }
        public DbSet<Connector> Connectors { get; set; }
        public DbSet<Cord> Cords { get; set; }
        public DbSet<Lookup> Lookups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plug>().ToTable("Plugs").HasKey("PlugId");
            modelBuilder.Entity<Connector>().ToTable("Connectors").HasKey("ConnectorId");
            modelBuilder.Entity<Cord>().ToTable("Cords").HasKey("CordId");

            modelBuilder.Entity<Lookup>().ToTable("Lookups").HasKey("Sku");

            // relationships between lookup and plug/connector/cord
            modelBuilder.Entity<Lookup>().HasMany(x => x.Plugs).WithOne(x => x.Lookup).HasForeignKey(f => f.Sku).HasPrincipalKey(p => p.Sku);
            modelBuilder.Entity<Lookup>().HasMany(x => x.Connectors).WithOne(x => x.Lookup).HasForeignKey(f => f.Sku).HasPrincipalKey(p => p.Sku);
            modelBuilder.Entity<Lookup>().HasMany(x => x.Cords).WithOne(x => x.Lookup).HasForeignKey(f => f.Sku).HasPrincipalKey(p => p.Sku);
        }
    }
}
