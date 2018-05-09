using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTest.Models.FourthTry
{
    public class Lookup4
    {
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }

        public ICollection<DerivedPlug4> Plugs { get; set; }
        public ICollection<DerivedConnector4> Connectors { get; set; }
        public ICollection<DerivedCord4> Cords { get; set; }
    }
}
