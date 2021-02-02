using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.Models.Rep
{
    public class Rep_Cat : Repository<Categories>
    {
        public Rep_Cat(ApplicationDbContext dbContext_)
        {
            DbContext_ = dbContext_;
        }

        public ApplicationDbContext DbContext_ { get; }

        public void Add(Categories entity)
        {
            DbContext_.categories.Add(entity);
            DbContext_.SaveChanges();
        }

        public void Delete(int id)
        {
            var Categories = Find(id);
            DbContext_.categories.Remove(Categories);
            DbContext_.SaveChanges();
        }

        public Categories Find(int id)
        {
            var Categories = DbContext_.categories.Include(cm => cm.News).SingleOrDefault(cm => cm.id == id);
            return Categories;
        }

        public IList<Categories> List()
        {
            var categories = DbContext_.categories.Include(cm => cm.News).ToList();
            return categories;
        }

        public void Update(Categories entity)
        {
            DbContext_.categories.Update(entity);
            DbContext_.SaveChanges();
        }

    }
}
