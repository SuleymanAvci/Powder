using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Powder.Entities;
using Powder.Interfaces;
using Powder.Models;

namespace Powder.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }
        public IActionResult Index()
        {
            return View(_categoryRepository.GetAll());
        }

        public IActionResult Add()
        {
            return View(new CategoryAddModel());
        }
        [HttpPost]
        public IActionResult Add(CategoryAddModel model) 
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(new Category
                {
                    Name = model.Name
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
