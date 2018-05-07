using System;
using EFCoreTest.Models;
using System.Linq;

namespace EFCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting EF Core Test!");

            var context = new DataContext();

            // seed lookup values if none are present
            // since I'm using an in-memory database none should be present.
            if (context.Lookups.Count() == 0)
            {
                Console.WriteLine("Seeding Lookup records");
                for (int i = 1; i <= 10; i++)
                {
                    context.Lookups.Add(new Lookup
                    {
                        Sku = i.ToString(),
                        Price = (decimal)i * 5,
                        Weight = (decimal)i * 7,
                    });
                }
                context.SaveChanges();
            }

            if (context.Plugs.Count() == 0)
            {
                Console.WriteLine("Seeding Plug records");
                for (int i = 1; i <= 4; i++)
                {
                    context.Plugs.Add(new Plug
                    {
                        Sku = i.ToString(),
                        Name =$"Plug {i}",
                        PlugId = i
                    });
                }
                context.SaveChanges();
            }

            if (context.Connectors.Count() == 0)
            {
                Console.WriteLine("Seeding Connector records");
                for (int i = 5; i <= 7; i++)
                {
                    context.Connectors.Add(new Connector
                    {
                        Sku = i.ToString(),
                        Name = $"Connector {i}",
                        ConnectorId = i
                    });
                }
                context.SaveChanges();
            }

            if (context.Cords.Count() == 0)
            {
                Console.WriteLine("Seeding Cord records");
                for (int i = 8; i <= 10; i++)
                {
                    context.Cords.Add(new Cord
                    {
                        Sku = i.ToString(),
                        Name = $"Cord {i}",
                        CordId = i
                    });
                }
                context.SaveChanges();
            }

            // Sample code to test the relationships set up in OnModelCreating
            // This assumes that each cord, plug and connector has a corresponding lookup record, which may not always be the case.
            //
            // Need to allow lookup records to not have a corresponding cord, plug or connector as well as 
            // allow a cord, plug or connector to not have a corresponding lookup record.
            var cord = context.Cords.First();
            Console.WriteLine($"\nCord {cord.Sku} Price: {cord.Lookup.Price} Weight: {cord.Lookup.Weight}");

            var plug = context.Plugs.First();
            Console.WriteLine($"Plug {plug.Sku} Price: {plug.Lookup.Price} Weight: {plug.Lookup.Weight}");

            var connector = context.Connectors.First();
            Console.WriteLine($"Connector {connector.Sku} Price: {connector.Lookup.Price} Weight: {connector.Lookup.Weight}");


            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
