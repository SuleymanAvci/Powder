﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Powder.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
