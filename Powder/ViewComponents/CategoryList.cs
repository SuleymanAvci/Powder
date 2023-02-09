using Microsoft.AspNetCore.Mvc;
using Powder.Interfaces;

namespace Powder.ViewComponents
{
    public class CategoryList:ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryList(ICategoryRepository categoryRepository)
        {
            _categoryRepository=categoryRepository;
        }
        public IViewComponentResult Invoke() 
        { 
            return View(_categoryRepository.GetAll() ); 
        }
    }
}
