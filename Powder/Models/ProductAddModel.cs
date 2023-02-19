using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Powder.Models
{
    public class ProductAddModel
    {

        [Required(ErrorMessage = "Ad Alanı gereklidir")]
        public string Name { get; set; }
        [Range(1,double.MaxValue, ErrorMessage = "Fiyat 0 dan yüksek olmalıdır.")]
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
    }
}

