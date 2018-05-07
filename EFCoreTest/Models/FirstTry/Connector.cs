using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreTest.Models.FirstTry
{
    public class Connector
    {
        [Key]
        public int ConnectorId { get; set; }

        public string Name { get; set; }

        // foreign key 
        public string Sku { get; set; }

        // navigation property
        public Lookup Lookup { get; set; }

    }
}
