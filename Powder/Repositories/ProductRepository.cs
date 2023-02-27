using Powder.Contexts;
using Powder.Entities;
using Powder.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Powder.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductRepository(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }


        public List<Category> GetCategories(int productId)
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
            }).Where(I => I.prduct.Id == productId).Select(I => new Category
            {
                Name = I.category.Name,
                Id = I.category.Id,
            }).ToList();
        }

        public List<Product> GetWithCategoryId(int categoryId)
        {
            using var context = new PowderContext();

            return context.Products.Join(context.ProductCategories, p => p.Id, pc => pc.ProductId, (product, productCategory) => new
            {
                Product = product,
                ProductCategory = productCategory,
            }).Where(I => I.ProductCategory.CategoryId == categoryId).Select(I => new Product
            {
                Id = I.Product.Id,
                Name = I.Product.Name,
                Price = I.Product.Price,
                Image = I.Product.Image
            }).ToList();
        }


        public void AddCategory(ProductCategory productCategory)
        {
            var controlRecord = _productCategoryRepository.GetWithFilter(I => I.CategoryId == productCategory.CategoryId && I.ProductId == productCategory.ProductId);
            if (controlRecord == null)
            {
                _productCategoryRepository.Add(productCategory);
            }
        }

        public void DeleteCategory(ProductCategory productCategory)
        {
            var controlRecord = _productCategoryRepository.GetWithFilter(I => I.CategoryId == productCategory.CategoryId && I.ProductId == productCategory.ProductId);
            if (controlRecord != null)
            {
                _productCategoryRepository.Delete(controlRecord);
            }
        }

    }
}
