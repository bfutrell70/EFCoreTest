using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EFCoreTest.Models.SecondTry
{
    public class ImplementedPlug : IComponent
    {
        [Key]
        public int ImplementedPlugId { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public Lookup2 Lookup { get; set; }
    }
}
