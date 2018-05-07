using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreTest.Models
{
    public class Plug
    {
        [Key]
        public int PlugId { get; set; }

        public string Name { get; set; }

        // foreign key 
        public string Sku { get; set; }

        // navigation property
        public Lookup Lookup { get; set; }
    }
}
