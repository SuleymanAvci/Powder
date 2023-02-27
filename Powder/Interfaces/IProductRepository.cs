using Powder.Entities;
using System.Collections.Generic;

namespace Powder.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        List<Category> GetCategories(int productId);
        void AddCategory(ProductCategory productCategory);
        void DeleteCategory(ProductCategory productCategory);
        List<Product> GetWithCategoryId(int categoryId);

    }
}
