using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EFCoreTest.Models.ThirdTry;

namespace EFCoreTest.Models.FourthTry
{
    public class DataContext4 : DbContext
    {
        public DataContext4() : base()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("InMemoryTest4");
            }
        }

        public DbSet<DerivedPlug4> Plugs { get; set; }
        public DbSet<DerivedConnector4> Connectors { get; set; }
        public DbSet<DerivedCord4> Cords { get; set; }
        public DbSet<Lookup4> Lookups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // plug, connector and cord objects are derived from BaseComponent
            modelBuilder.Entity<DerivedPlug4>().ToTable("Plugs").HasKey("DerivedPlugId");
            modelBuilder.Entity<DerivedConnector4>().ToTable("Connectors").HasKey("DerivedConnectorId");
            modelBuilder.Entity<DerivedCord4>().ToTable("Cords").HasKey("DerivedCordId");

            modelBuilder.Entity<Lookup4>().ToTable("Lookups").HasKey("Sku");

            // relationships between lookup and plug/connector/cord
            modelBuilder.Entity<Lookup4>().HasMany(x => x.Plugs).WithOne(x => x.Lookup).HasForeignKey(f => f.Sku).HasPrincipalKey(p => p.Sku);
            modelBuilder.Entity<Lookup4>().HasMany(x => x.Connectors).WithOne(x => x.Lookup).HasForeignKey(f => f.Sku).HasPrincipalKey(p => p.Sku);
            modelBuilder.Entity<Lookup4>().HasMany(x => x.Cords).WithOne(x => x.Lookup).HasForeignKey(f => f.Sku).HasPrincipalKey(p => p.Sku);
        }
    }
}
