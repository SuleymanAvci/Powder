using Microsoft.AspNetCore.Http;
using Powder.CustomExtensions;
using Powder.Entities;
using Powder.Interfaces;
using System.Collections.Generic;

namespace Powder.Repositories
{
    public class BasketRepository:IBasketRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BasketRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void BasketAdd(Product product)
        {
            var incomingList = _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("basket");

            if (incomingList==null)
            {
                incomingList = new List<Product>();
                incomingList.Add(product);
            }
            else
            {
                incomingList.Add(product);
            }
            _httpContextAccessor.HttpContext.Session.SetObject("basket", incomingList);

        }

        public void BasketRemove(Product product)
        {
            var incomingList = _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("basket");
            incomingList.Remove(product);

            _httpContextAccessor.HttpContext.Session.SetObject("basket", incomingList);
        }

        public List<Product> GetBasketProduct()
        {
            return _httpContextAccessor.HttpContext.Session.GetObject<List<Product>>("basket");
        }

        public void BasketEmpty()
        {
            _httpContextAccessor.HttpContext.Session.Remove("basket");
        }
    }
}
