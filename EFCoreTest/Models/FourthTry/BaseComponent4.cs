using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTest.Models.FourthTry
{
    public abstract class BaseComponent4
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public Lookup4 Lookup { get; set; }
        
    }
}
