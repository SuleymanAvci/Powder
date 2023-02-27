using Powder.Entities;
using System;
using System.Linq.Expressions;

namespace Powder.Interfaces
{
    public interface IProductCategoryRepository:IGenericRepository<ProductCategory>
    {
        ProductCategory GetWithFilter(Expression<Func<ProductCategory, bool>> filter);
    }
}
