using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Powder.Entities;
using Powder.Interfaces;
using Powder.Models;
using System;
using System.Collections.Generic;
using System.IO;


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
                    var uploadPlace = Path.Combine(Directory.GetCurrentDirectory(),"/git/Powder/Powder/wwwroot/img/" + newImageName);

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
                Id= getProduct.Id,
                Name = getProduct.Name,
                Price = getProduct.Price
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ProductUpdateModel model)
        {
            if(ModelState.IsValid)
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
    }
}
   