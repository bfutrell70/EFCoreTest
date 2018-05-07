using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreTest.Models.ThirdTry
{
    public class DerivedCord : BaseComponent
    {
        [Key]
        public int DerivedCordId { get; set; }
    }
}
