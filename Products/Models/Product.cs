using System;
using System.ComponentModel.DataAnnotations;

namespace Products.Models
{
    public class Product
    {

        private const double MaxTenDigitVal = 9999999999;

        public int      Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Product Type")]
        public string   Type { get; set; }

        [Required]
        [Range(0.00, MaxTenDigitVal)]
        [DataType(DataType.Currency)]
        public double   Cost { get; set; }

        [Required]
        [Range(0.00, MaxTenDigitVal)]
        [DataType(DataType.Currency)]
        public double   Price { get; set; }

        [Required]
        [Range(0, UInt32.MaxValue)]
        public uint     Quantity { get; set; }

    }
}
