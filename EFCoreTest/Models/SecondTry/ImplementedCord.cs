using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreTest.Models.SecondTry
{
    public class ImplementedCord : IComponent
    {
        [Key]
        public int ImplementedCordId { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public Lookup2 Lookup { get; set; }
    }
}
