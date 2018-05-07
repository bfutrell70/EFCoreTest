using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCoreTest.Models.FirstTry
{
    public class Cord
    {
        [Key]
        public int CordId { get; set; }

        public string Name { get; set; }

        // foreign key 
        public string Sku { get; set; }

        // navigation property
        public Lookup Lookup { get; set; }
    }
}
