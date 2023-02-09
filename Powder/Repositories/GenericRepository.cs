using Powder.Contexts;
using Powder.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Powder.Repositories
{
    public class GenericRepository<Tablo> where Tablo: class,new()
    {
        public void Add(Tablo tablo)
        {
            using var context = new PowderContext();
            context.Set<Tablo>().Add(tablo);
            context.SaveChanges();
        }
        public void Update(Tablo tablo)
        {
            using var context = new PowderContext();
            context.Set<Tablo>().Update(tablo);
            context.SaveChanges();
        }
        public void Delete(Tablo tablo)
        {
            using var context = new PowderContext();
            context.Set<Tablo>().Remove(tablo);
            context.SaveChanges();
        }

        public List<Tablo> GetAll()
        {
            using var context = new PowderContext();
            var tabloList = context.Set<Tablo>().ToList();
            return tabloList;
        }
        public Tablo Get(int id)
        {
            using var context = new PowderContext();
            return context.Set<Tablo>().Find(id);
            
        }
    }
}
