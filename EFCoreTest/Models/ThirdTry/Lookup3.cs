using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTest.Models.ThirdTry
{
    public class Lookup3
    {
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }

        public ICollection<DerivedPlug> Plugs { get; set; }
        public ICollection<DerivedConnector> Connectors { get; set; }
        public ICollection<DerivedCord> Cords { get; set; }
    }
}
