using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.Models.Rep
{
    public class Rep_Publiciter : Repository<Publiciter>
    {
        public ApplicationDbContext Context { get; }

        public Rep_Publiciter(ApplicationDbContext context)
        {
            Context = context;
        }
        public void Add(Publiciter entity)
        {
            Context.GetPubliciters.Add(entity);
            Save();
        }

        public void Delete(int id)
        {
            var find_ = Find(id);
            Context.GetPubliciters.Remove(find_);
            Save();
        }

        public Publiciter Find(int id)
        {
            var find_ = Context.GetPubliciters.SingleOrDefault(p => p.id == id);
            return find_;
        }

        public IList<Publiciter> List()
        {
            var list_Pub = Context.GetPubliciters.ToList();
            return list_Pub;
        }

        public void Update(Publiciter entity)
        {
            Context.GetPubliciters.Update(entity);
            Save();
        }
        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
