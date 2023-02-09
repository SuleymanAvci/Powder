using Microsoft.AspNetCore.Mvc;
using Powder.Interfaces;

namespace Powder.Controllers
{
    public class HomeController : Controller
    {
        IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products=_productRepository.GetAll();
            return View(products);
        }

        public IActionResult ProductDetail(int id)
        {
            return View(_productRepository.Get(id));
        }

    }

    public class Musteri
    {
        public string Ad { get; set; }
    }
}
