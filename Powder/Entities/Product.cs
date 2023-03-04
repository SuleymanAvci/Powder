using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Powder.Entities
{
    // [Dapper.Contrib.Extensions.Table("Products")]
    public class Product:IEquatable<Product>
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        public decimal Price { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }

        public bool Equals([AllowNull] Product other)

        {
            return Id==other.Id && Name==other.Name && Image==other.Image && Price==other.Price;
        }
    }

}
