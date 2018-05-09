using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreTest.Models.FourthTry
{
    public class DerivedConnector4 : BaseComponent4
    {
        [Key]
        public int DerivedConnectorId { get; set; }

    }
}
