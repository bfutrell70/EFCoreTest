using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreTest.Models.ThirdTry
{
    public class DerivedConnector : BaseComponent
    {
        [Key]
        public int DerivedConnectorId { get; set; }

    }
}
