using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.Models.Rep
{
    public class Rep_VideoNews : Repository<Video_News>
    {
        public Rep_VideoNews(ApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; }

        public void Add(Video_News entity)
        {
            Context.video_News.Add(entity);
            Save();
        }

        public void Delete(int id)
        {
            var find_ = Find(id);
            Context.video_News.Remove(find_);
            Save();
        }

        public Video_News Find(int id)
        {
            var find_ = Context.video_News.SingleOrDefault(v => v.id == id);
            return find_;
        }

        public IList<Video_News> List()
        {
            var list_ = Context.video_News.ToList();
            return list_;
        }
        
        public void Update(Video_News entity)
        {
            Context.video_News.Update(entity);
            Save();
     }
        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
