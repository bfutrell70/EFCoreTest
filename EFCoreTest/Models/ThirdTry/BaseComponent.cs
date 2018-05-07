using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTest.Models.ThirdTry
{
    public abstract class BaseComponent
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public Lookup3 Lookup { get; set; }
        
    }
}
