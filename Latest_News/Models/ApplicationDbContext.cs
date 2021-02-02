using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Latest_News.Models
{
    public class ApplicationDbContext: IdentityDbContext<AppUsers>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Categories> categories { get; set; }
        public DbSet<Video_News> video_News { get; set; }
        public DbSet<Latest_News> GetLatest_News { get; set; }
        public DbSet<Publiciter> GetPubliciters { get; set; }
        public DbSet<Commentaire> GetCommentaires { get; set; }
    }
}
