using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Powder.Interfaces;

namespace Powder.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View(_productRepository.GetAll());
        }
    }
}
