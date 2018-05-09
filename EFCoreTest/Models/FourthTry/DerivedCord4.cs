using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreTest.Models.FourthTry
{
    public class DerivedCord4 : BaseComponent4
    {
        [Key]
        public int DerivedCordId { get; set; }
    }
}
