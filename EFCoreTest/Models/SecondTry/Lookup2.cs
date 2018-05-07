using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreTest.Models.SecondTry
{
    public class Lookup2
    {
        [Key]
        public string Sku { get; set; }

        public decimal Weight { get; set; }

        public decimal Price { get; set; }

        // each sku can reference zero or more plugs
        public ICollection<ImplementedPlug> Plugs { get; set; }

        // each sku can reference zero or more connectors
        public ICollection<ImplementedConnector> Connectors { get; set; }

        // each sku can reference zero or one cord.
        // When I tried this using EF 6 it errored until I changed it to a collection
        public ICollection<ImplementedCord> Cords { get; set; }
    }
}
