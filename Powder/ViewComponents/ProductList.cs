using Microsoft.AspNetCore.Mvc;
using Powder.Interfaces;

namespace Powder.ViewComponents
{
    public class ProductList:ViewComponent
    {
        private readonly IProductRepository _productRepository;

        public ProductList(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IViewComponentResult Invoke()
        {
            return View(_productRepository.GetAll());
        }
    }
}
