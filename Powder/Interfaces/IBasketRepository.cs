using Powder.Entities;

namespace Powder.Interfaces
{
    public interface IBasketRepository
    {
        void BasketAdd(Product product);
        void BasketRemove(Product product);
    }
}
