using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Powder.Entities;
using Powder.Interfaces;
using Powder.Models;
using System.Reflection.Metadata.Ecma335;

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

        public IActionResult Update(int id)
        {
            var getCategory = _categoryRepository.Get(id);
            CategoryUpdateModel model = new CategoryUpdateModel()
            {
                Id = getCategory.Id,
                Name = getCategory.Name
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(CategoryUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var updatedCategory = _categoryRepository.Get(model.Id);
                if (updatedCategory != null)
                {
                    updatedCategory.Name = model.Name;
                    _categoryRepository.Update(updatedCategory);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            _categoryRepository.Delete(new Category {Id=id });
            return RedirectToAction("Index");
        }
    }
}
