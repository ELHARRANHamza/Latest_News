using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.Models.Rep
{
    public class Rep_Commentaire : Repository<Commentaire>
    {
        public Rep_Commentaire(ApplicationDbContext dbContext_)
        {
            DbContext_ = dbContext_;
        }

        public ApplicationDbContext DbContext_ { get; }

        public void Add(Commentaire entity)
        {
            DbContext_.GetCommentaires.Add(entity);
            DbContext_.SaveChanges();
        }

        public void Delete(int id)
        {
            var commentaire = Find(id);
            DbContext_.GetCommentaires.Remove(commentaire);
            DbContext_.SaveChanges();
        }

        public Commentaire Find(int id)
        {
            var commentaire = DbContext_.GetCommentaires.Include(c => c.news).Include(c => c.users).SingleOrDefault(cm => cm.id == id);
            return commentaire;
        }

        public IList<Commentaire> List()
        {
            var commentaires = DbContext_.GetCommentaires.Include(c => c.news).Include(c => c.users).ToList();
            return commentaires;
        }

        public void Update(Commentaire entity)
        {
            DbContext_.GetCommentaires.Update(entity);
            DbContext_.SaveChanges();
        }

    }
}
