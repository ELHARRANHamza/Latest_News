using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.Models.Rep
{
    public class Rep_News : Repository<Latest_News>
    {
        public Rep_News(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        public void Add(Latest_News entity)
        {
            DbContext.GetLatest_News.Add(entity);
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var news = Find(id);
            DbContext.GetLatest_News.Remove(news);
            DbContext.SaveChanges();
        }

        public Latest_News Find(int id)
        {
            var find_News = DbContext.GetLatest_News.Include(n => n.commentaires).Include(n => n.GetCategories).SingleOrDefault(n => n.id == id);
            return find_News;
        }

        public IList<Latest_News> List()
        {
            var List_News = DbContext.GetLatest_News.Include(n => n.commentaires).Include(n => n.GetCategories).ToList();
            return List_News;
        }

        public void Update(Latest_News entity)
        {
            DbContext.GetLatest_News.Update(entity);
            DbContext.SaveChanges();
        }
    }
}