using Powder.Entities;
using System.Collections.Generic;

namespace Powder.Interfaces
{
    public interface IBasketRepository
    {
        void BasketAdd(Product product);
        void BasketRemove(Product product);
        List<Product> GetBasketProduct();
        void BasketEmpty();
    }
}
