using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreTest.Models.SecondTry
{
    public class ImplementedConnector : IComponent
    {
        [Key]
        public int ImplementedConnectorId { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public Lookup2 Lookup { get; set; }
    }
}
