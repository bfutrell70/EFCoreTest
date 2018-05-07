using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EFCoreTest.Models.ThirdTry;

namespace EFCoreTest.Models.ThirdTry
{
    public class DataContext3 : DbContext
    {
        public DataContext3() : base()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("InMemoryTest3");
            }
        }

        public DbSet<DerivedPlug> Plugs { get; set; }
        public DbSet<DerivedConnector> Connectors { get; set; }
        public DbSet<DerivedCord> Cords { get; set; }
        public DbSet<Lookup3> Lookups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // plug, connector and cord objects are derived from BaseComponent
            modelBuilder.Entity<DerivedPlug>().ToTable("Plugs").HasKey("DerivedPlugId");
            modelBuilder.Entity<DerivedConnector>().ToTable("Connectors").HasKey("DerivedConnectorId");
            modelBuilder.Entity<DerivedCord>().ToTable("Cords").HasKey("DerivedCordId");

            modelBuilder.Entity<Lookup3>().ToTable("Lookups").HasKey("Sku");

            // relationships between lookup and plug/connector/cord
            modelBuilder.Entity<Lookup3>().HasMany(x => x.Plugs).WithOne(x => x.Lookup).HasForeignKey(f => f.Sku).HasPrincipalKey(p => p.Sku);
            modelBuilder.Entity<Lookup3>().HasMany(x => x.Connectors).WithOne(x => x.Lookup).HasForeignKey(f => f.Sku).HasPrincipalKey(p => p.Sku);
            modelBuilder.Entity<Lookup3>().HasMany(x => x.Cords).WithOne(x => x.Lookup).HasForeignKey(f => f.Sku).HasPrincipalKey(p => p.Sku);
        }
    }
}
