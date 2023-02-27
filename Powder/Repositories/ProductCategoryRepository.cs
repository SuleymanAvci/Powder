using Powder.Contexts;
using Powder.Entities;
using Powder.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Powder.Repositories
{
    public class ProductCategoryRepository : GenericRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategory GetWithFilter(Expression<Func<ProductCategory, bool>> filter)
        {
            using var context = new PowderContext();
            return context.ProductCategories.FirstOrDefault(filter);
        }
    }
}
