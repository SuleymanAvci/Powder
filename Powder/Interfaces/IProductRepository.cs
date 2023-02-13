using Powder.Entities;
using System.Collections.Generic;

namespace Powder.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        List<Category> GetAllCategories(int productId);
    }
}
