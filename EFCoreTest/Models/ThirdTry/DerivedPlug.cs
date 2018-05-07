using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreTest.Models.ThirdTry
{
    public class DerivedPlug : BaseComponent
    {
        [Key]
        public int DerivedPlugId { get; set; }
    }
}
