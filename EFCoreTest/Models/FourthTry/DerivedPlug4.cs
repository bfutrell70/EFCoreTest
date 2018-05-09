using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreTest.Models.FourthTry
{
    public class DerivedPlug4 : BaseComponent4
    {
        [Key]
        public int DerivedPlugId { get; set; }
    }
}
