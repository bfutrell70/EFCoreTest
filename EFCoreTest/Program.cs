using System;
using EFCoreTest.Models;
using EFCoreTest.Models.FirstTry;
using EFCoreTest.Models.SecondTry;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EFCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstTry();
            SecondTry();
        }

        public static void FirstTry()
        {
            Console.Clear();
            Console.WriteLine("Starting First EF Core Test - distinct object, no inheritance/interface!\n");

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
                        Name = $"Plug {i}",
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

            // let's add some data that does not correspond to values in the lookup table.
            context.Plugs.Add(new Plug
            {
                Sku = "100",
                Name = "Test plug 100",
                PlugId = 100
            });
            context.SaveChanges();

            context.Connectors.Add(new Connector
            {
                Sku = "101",
                Name = "Test connector 101",
                ConnectorId = 101
            });
            context.SaveChanges();

            context.Cords.Add(new Cord
            {
                Sku = "102",
                Name = "Test cord 102",
                CordId = 102
            });
            context.SaveChanges();

            // add a lookup value that does not correspond to a plug, connector or cord.
            context.Lookups.Add(new Lookup
            {
                Sku = "202",
                Weight = 1.25m,
                Price = 50.00m
            });
            context.SaveChanges();

            // tests using objects without a related lookup object.
            // having to convert to list after the include because it was returning no records.
            var plugs = context.Plugs.Include(x => x.Lookup).ToList();
            var orphanplugs = plugs.Where(x => x.Lookup == null).ToList();
            var pairedplugs = plugs.Where(x => x.Lookup != null).ToList();
            Console.WriteLine($"\nPlugs without lookup: {orphanplugs.Count()}");
            foreach (var item in orphanplugs)
            {
                Console.WriteLine($"\tPlug Sku {item.Sku}");
            }
            Console.WriteLine($"Plugs with lookup: {pairedplugs.Count()}");
            foreach (var item in pairedplugs)
            {
                Console.WriteLine($"\tPlug Sku {item.Sku}");
            }

            var connectors = context.Connectors.Include(x => x.Lookup).ToList();
            var orphanconnectors = connectors.Where(x => x.Lookup == null).ToList();
            var pairedconnectors = connectors.Where(x => x.Lookup != null).ToList();
            Console.WriteLine($"Connectors without lookup: {orphanconnectors.Count()}");
            foreach (var item in orphanconnectors)
            {
                Console.WriteLine($"\tConnector Sku {item.Sku}");
            }
            Console.WriteLine($"Connectors with lookup: {pairedconnectors.Count()}");
            foreach (var item in pairedconnectors)
            {
                Console.WriteLine($"\tConnector Sku {item.Sku}");
            }

            var cords = context.Cords.Include(x => x.Lookup).ToList();
            var orphancords = cords.Where(x => x.Lookup == null).ToList();
            var pairedcords = cords.Where(x => x.Lookup != null).ToList();
            Console.WriteLine($"Cords without lookup: {orphancords.Count()}");
            foreach (var item in orphancords)
            {
                Console.WriteLine($"\tCord Sku {item.Sku}");
            }
            Console.WriteLine($"Cords with lookup: {pairedcords.Count()}");
            foreach (var item in pairedcords)
            {
                Console.WriteLine($"\tCord Sku {item.Sku}");
            }

            // test lookup record without a related plug, connector or cord object
            var lookups = context.Lookups
                .Include(x => x.Plugs).Include(x => x.Connectors).Include(x => x.Cords).ToList();
            var orphanlookups = lookups.Where(x => x.Plugs.Count() == 0 && x.Connectors.Count() == 0 && x.Cords.Count() == 0).ToList();
            var pairedlookups = lookups.Where(x => x.Plugs.Count() != 0 || x.Connectors.Count() != 0 || x.Cords.Count() != 0).ToList();
            Console.WriteLine($"Lookup without any related plug/connector/cord objects: {orphanlookups.Count()} ");
            Console.WriteLine($"Lookup with a related plug/connector/cord object: {pairedlookups.Count()} ");


            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        public static void SecondTry()
        {
            Console.Clear();
            Console.WriteLine(" -- Starting Second EF Core Test - distinct object, implementing an interface! -- \n");

            var context = new DataContext2();

            void PrintSkus<T>(IEnumerable<IComponent> components) where T : IComponent
            {
                foreach (var item in components)
                {
                    Console.WriteLine($"\tSku: {item.Sku} Name: {item.Name}");
                }
            }

            // seed lookup values if none are present
            // since I'm using an in-memory database none should be present.
            if (context.Lookups.Count() == 0)
            {
                Console.WriteLine("Seeding Lookup records");
                for (int i = 1; i <= 10; i++)
                {
                    context.Lookups.Add(new Lookup2
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
                    context.Plugs.Add(new ImplementedPlug
                    {
                        Sku = i.ToString(),
                        Name = $"Plug {i}",
                        ImplementedPlugId = i
                    });
                }
                context.SaveChanges();
            }

            if (context.Connectors.Count() == 0)
            {
                Console.WriteLine("Seeding Connector records");
                for (int i = 5; i <= 7; i++)
                {
                    context.Connectors.Add(new ImplementedConnector
                    {
                        Sku = i.ToString(),
                        Name = $"Connector {i}",
                        ImplementedConnectorId = i
                    });
                }
                context.SaveChanges();
            }

            if (context.Cords.Count() == 0)
            {
                Console.WriteLine("Seeding Cord records");
                for (int i = 8; i <= 10; i++)
                {
                    context.Cords.Add(new ImplementedCord
                    {
                        Sku = i.ToString(),
                        Name = $"Cord {i}",
                        ImplementedCordId = i
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

            // let's add some data that does not correspond to values in the lookup table.
            context.Plugs.Add(new ImplementedPlug
            {
                Sku = "100",
                Name = "Test plug 100",
                ImplementedPlugId = 100
            });
            context.SaveChanges();

            context.Connectors.Add(new ImplementedConnector
            {
                Sku = "101",
                Name = "Test connector 101",
                ImplementedConnectorId = 101
            });
            context.SaveChanges();

            context.Cords.Add(new ImplementedCord
            {
                Sku = "102",
                Name = "Test cord 102",
                ImplementedCordId = 102
            });
            context.SaveChanges();

            // add a lookup value that does not correspond to a plug, connector or cord.
            context.Lookups.Add(new Lookup2
            {
                Sku = "202",
                Weight = 1.25m,
                Price = 50.00m
            });
            context.SaveChanges();

            // tests using objects without a related lookup object.
            // having to convert to list after the include because it was returning no records.
            var plugs = context.Plugs.Include(x => x.Lookup).ToList();
            var orphanplugs = plugs.Where(x => x.Lookup == null).ToList();
            var pairedplugs = plugs.Where(x => x.Lookup != null).ToList();
            Console.WriteLine($"\nPlugs without lookup: {orphanplugs.Count()}");
            PrintSkus<IComponent>(orphanplugs);
            Console.WriteLine($"\nPlugs with lookup: {pairedplugs.Count()}");
            PrintSkus<IComponent>(pairedplugs);

            var connectors = context.Connectors.Include(x => x.Lookup).ToList();
            var orphanconnectors = connectors.Where(x => x.Lookup == null).ToList();
            var pairedconnectors = connectors.Where(x => x.Lookup != null).ToList();
            Console.WriteLine($"Connectors without lookup: {orphanconnectors.Count()}");
            PrintSkus<IComponent>(orphanconnectors);
            Console.WriteLine($"Connectors with lookup: {pairedconnectors.Count()}");
            PrintSkus<IComponent>(pairedconnectors);

            var cords = context.Cords.Include(x => x.Lookup).ToList();
            var orphancords = cords.Where(x => x.Lookup == null).ToList();
            var pairedcords = cords.Where(x => x.Lookup != null).ToList();
            Console.WriteLine($"Cords without lookup: {orphancords.Count()}");
            PrintSkus<IComponent>(orphancords);
            Console.WriteLine($"Cords with lookup: {pairedcords.Count()}");
            PrintSkus<IComponent>(pairedcords);

            // test lookup record without a related plug, connector or cord object
            var lookups = context.Lookups
                .Include(x => x.Plugs).Include(x => x.Connectors).Include(x => x.Cords).ToList();
            var orphanlookups = lookups.Where(x => x.Plugs.Count() == 0 && x.Connectors.Count() == 0 && x.Cords.Count() == 0).ToList();
            var pairedlookups = lookups.Where(x => x.Plugs.Count() != 0 || x.Connectors.Count() != 0 || x.Cords.Count() != 0).ToList();
            Console.WriteLine($"Lookup without any related plug/connector/cord objects: {orphanlookups.Count()} ");
            Console.WriteLine($"Lookup with a related plug/connector/cord object: {pairedlookups.Count()} ");
            
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();

            
        }
    }
}
