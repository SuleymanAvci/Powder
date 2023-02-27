using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Powder.Entities;
using Powder.Interfaces;
using Powder.Models;
using System;

namespace Powder.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IProductRepository _productRepository;
        private readonly IBasketRepository _basketRepository;

        public HomeController(IProductRepository productRepository, SignInManager<AppUser> signInManager, IBasketRepository basketRepository)
        {
            _productRepository = productRepository;
            _signInManager = signInManager;
            _basketRepository = basketRepository;
        }

        public IActionResult Index(int? categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return View();
        }

        public IActionResult ProductDetail(int id)
        {

            return View(_productRepository.Get(id));
        }

        public IActionResult LoginIn() 
        {
            return View(new UserLoginModel());
        }
        [HttpPost]
        public IActionResult LoginIn(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult= _signInManager.PasswordSignInAsync(model.UserName, model.Key, model.RememberMe, false).Result;

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index","Home", new {area="Admin"});
                }
                ModelState.AddModelError ("","UserName or password is Wrong!");
            }
            return View(model);
        }


        public IActionResult AddBasket(int id)
        {
            var product=_productRepository.Get(id);
            _basketRepository.BasketAdd(product);
            TempData["notice"] = "Product add to the basket";
            return RedirectToAction("Index");
        }

    }

}
