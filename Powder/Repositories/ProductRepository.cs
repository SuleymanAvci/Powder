using Powder.Contexts;
using Powder.Entities;
using Powder.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Powder.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public List<Category> GetAllCategories(int productId)
        {
            using var context = new PowderContext();
            return context.Products.Join(context.ProductCategories, product => product.Id, productCategory => productCategory.ProductId, (p, pc) => new
            {
                product = p,
                productCategory = pc
            }).Join(context.Categories, two => two.productCategory.CategoryId, category => category.Id, (ppc, c) => new
            {
                prduct = ppc.product,
                ProductCategory = ppc.productCategory,
                category = c
            }).Where(I=>I.prduct.Id==productId).Select(I=>new Category {
            Name=I.category.Name,
            Id=I.category.Id,
            }).ToList();
        }
    }
}
