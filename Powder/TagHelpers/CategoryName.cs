using Microsoft.AspNetCore.Razor.TagHelpers;
using Powder.Interfaces;
using System.Linq;

namespace Powder.TagHelpers
{
    [HtmlTargetElement("getCategoryName")]
    public class CategoryName : TagHelper
    {
        IProductRepository _productRepository;
        public CategoryName(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public int ProductId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string data = "";
            var getCategories = _productRepository.GetCategories(ProductId).Select(I => I.Name);
            foreach (var item in getCategories)
            {
                data += item+" ";
            }
            output.Content.SetContent(data);
        }
    }
}
