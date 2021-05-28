using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _5_tutorial_apbd.Models
{
    public class WarehouseProduct
    {
        [Required(ErrorMessage ="IdProduct is needed")]
        public int IdProduct { get; set; }
        [Required(ErrorMessage = "IdWarehouse is needed")]
        public int IdWarehouse { get; set; }
        [Required(ErrorMessage = "Amount is needed")]
        [Range(1,int.MaxValue,ErrorMessage = "Value should be greater than 0")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "CreatedAt cannot be emtpy")]
        public DateTime CreatedAt { get; set; }

    }
}
