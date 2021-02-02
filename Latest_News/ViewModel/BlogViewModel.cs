using Latest_News.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.ViewModel
{
    public class BlogViewModel
    {
        public int id { get; set; }
        public IList<Models.Latest_News> GetLatest_News { get; set; }
        public IList<Categories> GetCategories { get; set; }
        public IList<Commentaire> GetCommentaires { get; set; }

        public Models.Latest_News latest_ { get; set; }
    }
}
