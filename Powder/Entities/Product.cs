using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Powder.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        public decimal Price { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
