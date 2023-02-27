using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Powder.Entities;
using Powder.Interfaces;
using Powder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Powder.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            return View(_productRepository.GetAll());
        }

        public IActionResult Add()
        {
            return View(new ProductAddModel());
        }

        [HttpPost]
        public IActionResult Add(ProductAddModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();
                if (model.Image != null)
                {
                    var extension = Path.GetExtension(model.Image.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var uploadPlace = Path.Combine(Directory.GetCurrentDirectory(), "/git/Powder/Powder/wwwroot/img/" + newImageName);

                    var stream = new FileStream(uploadPlace, FileMode.Create);
                    model.Image.CopyTo(stream);
                    product.Image = newImageName;
                }
                product.Name = model.Name;
                product.Price = model.Price;
                _productRepository.Add(product);
                return RedirectToAction("index", "Home", new { area = "Admin" });
            }
            return View(model);
        }

        public IActionResult Update(int id)
        {
            var getProduct = _productRepository.Get(id);
            ProductUpdateModel model = new ProductUpdateModel
            {
                Id = getProduct.Id,
                Name = getProduct.Name,
                Price = getProduct.Price
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ProductUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var updatedProduct = _productRepository.Get(model.Id);
                if (model.Image != null)
                {
                    var extension = Path.GetExtension(model.Image.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var uploadPlace = Path.Combine(Directory.GetCurrentDirectory(), "/git/Powder/Powder/wwwroot/img/" + newImageName);

                    var stream = new FileStream(uploadPlace, FileMode.Create);
                    model.Image.CopyTo(stream);
                    updatedProduct.Image = newImageName;
                }
                updatedProduct.Name = model.Name;
                updatedProduct.Price = model.Price;
                _productRepository.Update(updatedProduct);
                return RedirectToAction("index", "Home", new { area = "Admin" });
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            _productRepository.Delete(new Product { Id = id });
            return RedirectToAction("index");
        }

        public IActionResult AssignCategory(int id)
        {
            var productAssignCategories = _productRepository.GetCategories(id).Select(I => I.Name);
            var categories = _categoryRepository.GetAll();

            TempData["ProductId"] = id;

            List<CategoryAssignModel> list = new List<CategoryAssignModel>();

            foreach (var item in categories)
            {
                CategoryAssignModel model = new CategoryAssignModel();
                model.CategoryId = item.Id;
                model.CategoryName = item.Name;
                model.IsThere = productAssignCategories.Contains(item.Name);

                list.Add(model);
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult AssignCategory(List<CategoryAssignModel> list)
        {
            int productId = (int)TempData["ProductId"];

            foreach (var item in list)
            {
                if (item.IsThere)
                {
                    _productRepository.AddCategory(new ProductCategory
                    {
                        CategoryId = item.CategoryId,
                        ProductId = productId
                    });
                }
                else
                {
                    _productRepository.DeleteCategory(new ProductCategory
                    {
                        CategoryId = item.CategoryId,
                        ProductId = productId
                    });
                }
            }
            return RedirectToAction("Index");
        }
    }
}
