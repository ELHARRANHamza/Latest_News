using Latest_News.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.ViewModel
{
    public class HomeViewModel
    {
        public IList<Publiciter> GetPubliciters { get; set; }
        public IList<Categories> GetCategories { get; set; }
        public IList<Video_News> GetVideo_News { get; set; }

        public IList<Models.Latest_News> GetLatest_News  { get; set; }

    }
}
