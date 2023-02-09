using Powder.Entities;
using System.Collections.Generic;

namespace Powder.Interfaces
{
    public interface IGenericRepository<Tablo> where Tablo:class,new()
    {
        void Add(Tablo tablo);
        void Update(Tablo tablo);
        void Delete(Tablo tablo);
        List<Tablo> GetAll();
        Tablo Get(int id);
    }
}
